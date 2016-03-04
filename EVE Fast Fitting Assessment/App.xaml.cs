using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using CrestSharp;
using CrestSharp.Caches;
using CrestSharp.Model;
using EVE_Fast_Fitting_Assessment.Properties;
using EVE_Fast_Fitting_Assessment.Ui;
using FittingEngine;
using Serilog;
using Serilog.Sinks.RollingFile;

namespace EVE_Fast_Fitting_Assessment
{
    public delegate void LoggedIntoCrest(IAuthenticatedCrest authenticatedCrest);

    public delegate void LoggedOutFromCrest();

    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string CLIENT_ID = "";
        private const string CLIENT_SECRET = "";
        private const string URL = "http://localhost:49871/crestAuth/";
        public static readonly Crest CREST = new Crest();
        public static FittingService FittingService = new FittingService();
        public static IAuthenticatedCrest AuthenticatedCrest;

        static App()
        {
            System.Net.ServicePointManager.DefaultConnectionLimit = 20;

            Log.Logger = new LoggerConfiguration().WriteTo.RollingFile("logs\\application-{Date}.log")
                .MinimumLevel.Information()
                .CreateLogger();
            Log.Logger.Information("Starting Application");

            AppDomain.CurrentDomain.UnhandledException += TopLevelExceptionHandler;

            Crest.Settings.Cache = new CrestCacheWithSessionCache(new CrestSqliteCache());

            if (Settings.Default.ResetCacheOnNextStartup)
            {
                Settings.Default.ResetCacheOnNextStartup = false;
                Settings.Default.Save();
                try
                {
                    Crest.Settings.Cache.ClearCache();
                }
                catch (Exception e)
                {
                    Log.Logger.Warning(e, "Could not reset crest cache");
                }
            }

            Crest.UserAgent = $"EVE Fast Fitting Assessment (v{Assembly.GetExecutingAssembly() .GetName() .Version})";
        }

        public static event LoggedIntoCrest LoggedIntoCrest;

        public static event LoggedOutFromCrest LoggedOutFromCrest;

        protected override async void OnStartup(StartupEventArgs ev)
        {
            var splash = new Splashscreen();
            splash.Show();
            base.OnStartup(ev);
            var main = new MainWindow();

            var start = DateTime.UtcNow;
            try
            {
                await FittingService.Initialize();
            }
            catch (Exception e)
            {
                FittingService = null;
                Log.Logger.Error(e, "Error initializing fitting engine");
                MessageBox.Show(
                                $"This is embarassing.\nThe fitting engine couldn't be initialized.\nI am very sorry and if you contact me under jonas.jacobi@web.de, will try to fix this bug as fast as possible.\n\nError: {e.Message}");
            }
            finally
            {
                Log.Logger.Information($"Startup time: {(DateTime.UtcNow - start).TotalSeconds}s .");
            }

            if (!string.IsNullOrEmpty(Settings.Default.RefreshToken))
            {
                if (!await TryRefreshCrestLogin())
                {
                    Settings.Default.RefreshToken = null;
                    Settings.Default.Save();
                }
            }

            splash.Close();
            main.Show();
        }

        private static void TopLevelExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            var exceptionObject = (Exception)e.ExceptionObject;

            Log.Logger.Error(exceptionObject, "Toplevel Exception");
            var errorMessage = exceptionObject.Message;

            ShowError($"This is embarassing and shouldn't happen.\nI am very sorry and if you contact me under jonas.jacobi@web.de, will try to fix this bug as fast as possible.\n\nError: {errorMessage}");
        }

        private static void ShowError(string message)
        {
            var parent = Current?.MainWindow;
            if (parent == null)
            {
                MessageBox.Show(message, "ERROR");
            }
            else
            {
                MessageBox.Show(parent, message, "ERROR");
            }
        }

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Log.Logger.Error(e.Exception, "Toplevel dispatcher exception");

            MessageBox.Show($"This is embarassing and shouldn't happen.\nI am very sorry and if you contact me under jonas.jacobi@web.de, will try to fix this bug as fast as possible.\n\nError: {e.Exception?.Message}", "ERROR");
            
            e.Handled = true; //TODO nur fuers dbeugging
        }

        private async Task<bool> TryRefreshCrestLogin()
        {
            try
            {
                AuthenticatedCrest = await CREST.Authenticate(CLIENT_ID, CLIENT_SECRET, Settings.Default.RefreshToken);

                if (AuthenticatedCrest.State == AuthenticatedCrestState.Authorized)
                {
                    LoggedIntoCrest?.Invoke(AuthenticatedCrest);
                    return true;
                }
            }
            catch
            {
                // ignored
            }

            return false;
        }

        public static async Task<bool> LoginToCrest()
        {
            try
            {
                AuthenticatedCrest =
                    await
                    CREST.Authenticate(
                                       CLIENT_ID,
                                       CLIENT_SECRET,
                                       new Uri(URL),
                                       AuthenticatedCrestScope.CharacterFittingsRead | AuthenticatedCrestScope.CharacterFittingsWrite
                                       | AuthenticatedCrestScope.CharacterLocationRead | AuthenticatedCrestScope.CharacterNavigationWrite);

                if (AuthenticatedCrest.State == AuthenticatedCrestState.Authorized)
                {
                    Settings.Default.RefreshToken = AuthenticatedCrest.CrestAuthorization.RefreshToken;
                    Settings.Default.Save();
                    LoggedIntoCrest?.Invoke(AuthenticatedCrest);
                    return true;
                }
            }
            catch
            {
                // ignored
            }

            return false;
        }

        public static void LogoutFromCrest()
        {
            LoggedOutFromCrest?.Invoke();
        }
    }
}
