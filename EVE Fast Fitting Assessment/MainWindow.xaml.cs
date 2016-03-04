using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using CrestSharp;
using CrestSharp.Model;
using CrestSharp.Model.Implementation;
using EVE_Fast_Fitting_Assessment.Models;
using EVE_Fast_Fitting_Assessment.Ui;
using EVE_Fast_Fitting_Assessment.Utilities;
using Serilog;

namespace EVE_Fast_Fitting_Assessment
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const double LOCATION_CACHE_TIME_IN_MILISECONDS = 10000;
        private static readonly Regex CREST_KILLMAIL_REGEX = new Regex("https://public-crest.eveonline.com/killmails/\\d+/[a-f0-9]{40,40}/");
        private static readonly TimeSpan ANIMATION_SPEED = TimeSpan.FromMilliseconds(500);
        private readonly CharacterViewModel _characterViewModel = new CharacterViewModel();
        private readonly Timer _timer = new Timer(LOCATION_CACHE_TIME_IN_MILISECONDS);
        private IAuthenticatedCrest _authenticatedCrest;
        private FittingViewModel _fittingViewModel;
        private bool _isInitialLayout = true;

        public MainWindow()
        {
            InitializeComponent();
            App.LoggedIntoCrest += AppOnLoggedIntoCrest;
            App.LoggedOutFromCrest += AppOnLoggedOutFromCrest;

            TxtKillmailUri.TextChanged += TxtKillmailUriOnTextChanged;
            CharacterDisplay.DataContext = _characterViewModel;
            _timer.Elapsed += TimerOnElapsed;
        }

        private void AppOnLoggedOutFromCrest()
        {
            _timer.Stop();
            lock (this)
            {
                _authenticatedCrest = null;
            }
            Application.Current.Dispatcher.Invoke(() => { ImgBtnLogin.Visibility = Visibility.Visible; });
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            Task.Factory.StartNew(
                                  async () =>
                                  {
                                      try
                                      {
                                          IAuthenticatedCrest authCrest;
                                          lock (this)
                                          {
                                              authCrest = _authenticatedCrest;
                                              if (authCrest == null)
                                              {
                                                  return;
                                              }
                                          }

                                          var location = await authCrest.GetCharacterLocationAsync();
                                          _characterViewModel.Location = location?.SolarSystem?.Name ?? "(offline)";
                                      }
                                      catch (Exception)
                                      {
                                          _characterViewModel.Location = "(unknown)";
                                      }
                                  });
        }

        private void AppOnLoggedIntoCrest(IAuthenticatedCrest authenticatedCrest)
        {
            ImgBtnLogin.Visibility = Visibility.Collapsed;

            _authenticatedCrest = authenticatedCrest;
            _characterViewModel.Name = _authenticatedCrest.Character.Name;
            _characterViewModel.Id = _authenticatedCrest.Character.Id;

            _characterViewModel.ImageUrl = _authenticatedCrest.Character.Portrait.Image256.Href;
            CharacterDisplay.IsLoggedIn();

            if (!_timer.Enabled)
            {
                TimerOnElapsed(null, null);
                _timer.Start();
            }
        }

        private async void TxtKillmailUriOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            TxtValidUri.Visibility = Visibility.Hidden;

            if (!IsCrestKillmailLink(TxtKillmailUri.Text))
            {
                return;
            }

            TxtKillmailUri.IsEnabled = false;
            UriSpinner.Visibility = Visibility.Visible;

            AnimateLayout();

            try
            {
                var text = TxtKillmailUri.Text;
                var killMail =
                    await await Task.Factory.StartNew(() => App.CREST.Killmails.FetchAsync(text), TaskCreationOptions.LongRunning);

                var shipType = killMail?.Victim?.ShipType;
                if (shipType == null)
                {
                    NoShipTypeAvailable();
                    return;
                }

                UriSpinner.Visibility = Visibility.Hidden;
                TxtValidUri.Visibility = Visibility.Visible;

                InitViewModel(shipType, killMail);

                await Task.Delay(ANIMATION_SPEED)
                    .ContinueInDispatcherAsyncWith(
                                                   t =>
                                                   {
                                                       ShipGrid.Visibility = Visibility.Visible;
                                                       TxtKillmailUri.IsEnabled = true;
                                                   })
                    .ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Log.Logger.Warning(e, "Error in loading killmail");
                MessageBox.Show($"Sorry, that didn't work!\nReason: {e.Message}", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Application.Current.Dispatcher.Invoke(
                                                      () =>
                                                      {
                                                          TxtKillmailUri.IsEnabled = true;
                                                          UriSpinner.Visibility = Visibility.Hidden;
                                                      });
            }
        }

        private void InitViewModel(ICrestType shipType, ICrestKillmail killMail)
        {
            _fittingViewModel = new FittingViewModel();
            _fittingViewModel.Ship = new TypeViewModel(killMail.Victim.ShipType)
                                     {
                                         Icon = $"https://image.eveonline.com/Render/{shipType.Id}_{256}.png"
                                     };

            DataContext = _fittingViewModel;

            var priceModel = new PriceViewModel();
            Prices.DataContext = priceModel;

            Task.Factory.StartNew(
                                  () =>
                                  {
                                      Parallel.ForEach(killMail.Victim.Items, AssignToSlot);
                                      var fittingModel = Fitting.FromCrestKillmail("", "", killMail);

                                      Task.Factory.StartNew(
                                                            () =>
                                                            {
                                                                try
                                                                {
                                                                    var market = new Market(App.CREST);

                                                                    var jitaPrices = market.GetPricesForTypeIdsInSystem(
                                                                                                                        EveConstants.THE_FORGE_ID,
                                                                                                                        "Jita IV",
                                                                                                                        fittingModel);

                                                                    var slots = _fittingViewModel.HighSlots.Slots.Concat(_fittingViewModel.MediumSlots.Slots)
                                                                        .Concat(_fittingViewModel.LowSlots.Slots)
                                                                        .Concat(_fittingViewModel.RigSlots.Slots)
                                                                        .Concat(_fittingViewModel.SubSystemSlots.Slots)
                                                                        .Concat(new[] { _fittingViewModel.Ship })
                                                                        .Where(x => x?.Type != null)
                                                                        .ToArray();

                                                                    foreach (var curModel in slots)
                                                                    {
                                                                        double curPrice;
                                                                        curModel.Price =
                                                                            (jitaPrices.TryGetValue(curModel.Type.Id, out curPrice) ? curPrice : 0.0)
                                                                                .ToStringInIskFormat();
                                                                    }

                                                                    priceModel.JitaPrice = GetPriceInSystem(slots, jitaPrices);

                                                                    var amarrPrices = market.GetPricesForTypeIdsInSystem(
                                                                                                                         EveConstants.DOMAIN_ID,
                                                                                                                         "Amarr",
                                                                                                                         fittingModel);
                                                                    priceModel.AmarrPrice = GetPriceInSystem(slots, amarrPrices);

                                                                    var dodixiePrices = market.GetPricesForTypeIdsInSystem(
                                                                                                                           EveConstants.SINQ_LAISON_ID,
                                                                                                                           "Dodixie",
                                                                                                                           fittingModel);
                                                                    priceModel.DodixiePrice = GetPriceInSystem(slots, dodixiePrices);

                                                                    var hekPrices = market.GetPricesForTypeIdsInSystem(
                                                                                                                    EveConstants.METROPOLIS_ID,
                                                                                                                    "Hek",
                                                                                                                    fittingModel);
                                                                    priceModel.HekPrice = GetPriceInSystem(slots, hekPrices);

                                                                    var rensPrices = market.GetPricesForTypeIdsInSystem(
                                                                                                                        EveConstants.HEIMATAR_ID,
                                                                                                                        "Rens",
                                                                                                                        fittingModel);
                                                                    priceModel.RensPrice = GetPriceInSystem(slots, rensPrices);

                                                                }
                                                                catch (Exception e)
                                                                {
                                                                    Log.Logger.Warning(e, "Error in loading market data");
                                          //TODO messagebox? statusline?
                                      }
                                                            },
                                                            TaskCreationOptions.LongRunning)
                                          .ConfigureAwait(false);

                                      Task.Factory.StartNew(
                                                            async () =>
                                                            {
                                                                try
                                                                {
                                                                    CharacterDisplay.SetFitting(fittingModel);

                                                                    var shipContext = await App.FittingService.CreatContextFromFittingAsync(fittingModel);

                                                                    Application.Current.Dispatcher.Invoke(() => FittingDisplay.ShowShip(shipContext));
                                                                }
                                                                catch (Exception e)
                                                                {
                                                                    Log.Logger.Warning(e, "Error in loading of fit");

                                          //TODO sollte hier ne messagebox aufgehen?
                                      }
                                                            },
                                                            TaskCreationOptions.LongRunning)
                                          .ConfigureAwait(false);
                                  });

            
        }

        private void AssignToSlot(IVictimItem curItem)
        {
            if (curItem.IsFittedToRigSlot)
            {
                _fittingViewModel.RigSlots[curItem.Flag - VictimItem.FLAG_RIG_SLOT_0] = new TypeViewModel(curItem.ItemType);
                return;
            }

            if (curItem.IsFittedAsSubSystem)
            {
                _fittingViewModel.SubSystemSlots[curItem.Flag - VictimItem.FLAG_SUBSYSTEM_SLOT_0] = new TypeViewModel(curItem.ItemType);
                return;
            }

            if (curItem.IsFittedToHighSlot && IsModule(curItem))
            {
                _fittingViewModel.HighSlots[curItem.Flag - VictimItem.FLAG_HIGH_SLOT_0] = new TypeViewModel(curItem.ItemType);
                return;
            }

            if (curItem.IsFittedToMediumSlot && IsModule(curItem))
            {
                _fittingViewModel.MediumSlots[curItem.Flag - VictimItem.FLAG_MED_SLOT_0] = new TypeViewModel(curItem.ItemType);
                return;
            }

            if (curItem.IsFittedToLowSlot && IsModule(curItem))
            {
                _fittingViewModel.LowSlots[curItem.Flag - VictimItem.FLAG_LOW_SLOT_0] = new TypeViewModel(curItem.ItemType);
            }
        }

        private static string GetPriceInSystem(TypeViewModel[] slots, IDictionary<int, double> pricesInSystem)
        {
            return slots.Sum(
                             x =>
                             {
                                 double curPrice;
                                 return pricesInSystem.TryGetValue(x.Type.Id, out curPrice) ? curPrice : 0.0;
                             })
                .ToStringInIskFormat();
        }

        private static bool IsModule(IVictimItem curItem)
        {
            var dogmaEffects = curItem.ItemType?.Dogma?.Effects;
            return dogmaEffects != null
                   && dogmaEffects.Any(x => x.Effect.Name == "loPower" || x.Effect.Name == "hiPower" || x.Effect.Name == "medPower");
        }

        private void NoShipTypeAvailable()
        {
            MessageBox.Show(
                            "This killmail contains no target ship info, please try another one.",
                            "WARNING",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);

            TxtKillmailUri.Text = "";
            TxtKillmailUri.IsEnabled = true;
        }

        private static bool IsCrestKillmailLink(string text)
        {
            return CREST_KILLMAIL_REGEX.IsMatch(text);
        }

        private void AnimateLayout()
        {
            if (!_isInitialLayout)
            {
                return;
            }

            var storyboard = new Storyboard();
            var maxHeightAnimation = new DoubleAnimation
                                     {
                                         From = TopRow.ActualHeight,
                                         To = 0.0,
                                         AutoReverse = false,
                                         EasingFunction = new QuadraticEase
                                                          {
                                                              EasingMode = EasingMode.EaseOut
                                                          },
                                         Duration = new Duration(ANIMATION_SPEED)
                                     };

            Storyboard.SetTarget(maxHeightAnimation, TopRow);
            Storyboard.SetTargetProperty(maxHeightAnimation, new PropertyPath("MaxHeight"));
            storyboard.Children.Add(maxHeightAnimation);

            storyboard.Begin(this);
            _isInitialLayout = false;
        }

        private void ImgBtnLogin_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CharacterDisplay.Login();
        }
    }
}
