using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using FittingEngine;
using FittingEngine.Model;
using Serilog;

namespace EVE_Fast_Fitting_Assessment.Ui
{
    public class AmmoAnalysisViewModel
    {
        public string Weapon { get; set; }

        public string AmmoType { get; set; }

        public int WeaponCount { get; set; }

        public double Dps { get; set; }

        public double Alpha { get; set; }

        public double Falloff { get; set; }

        public double Optimal { get; set; }

        public double Tracking { get; set; }
    }

    public class FittingAnalysisViewModel
    {
        private static readonly DamageProfile THERMAL_PROFILE = new DamageProfile(0, 0, 1.0, 0);
        private static readonly DamageProfile KINETIC_PROFILE = new DamageProfile(0, 1.0, 0, 0);
        private static readonly DamageProfile EM_PROFILE = new DamageProfile(1.0, 0, 0, 0);
        private static readonly DamageProfile EXPLOSIVE_PROFILE = new DamageProfile(0, 0, 0, 1.0);

        public FittingAnalysisViewModel(Context context)
        {
            var ship = (Ship) context.Ship;
            DamageAnalysis.AddDrones(ship);

            context.Char.Online(context);
            BaseVelocity = ship.MaxVelocityInMetersPerSecond;

            context.Char.Activate(context);
            PropulsionModVelocity = ship.MaxVelocityInMetersPerSecond;

            context.Char.Overload(context);
            HeatedPropulsionModVelocity = ship.MaxVelocityInMetersPerSecond;

            InitDamage(context);
            InitTank(ship);
        }

        public double EhpEm { get; set; }

        public double EhpKinetic { get; set; }

        public double EhpExplosive { get; set; }

        public double EhpThermal { get; set; }

        public double BaseVelocity { get; set; }

        public double PropulsionModVelocity { get; set; }

        public double HeatedPropulsionModVelocity { get; set; }

        public double MaxDps { get; set; }

        public double MaxAlpha { get; set; }

        public double Ehp { get; set; }

        public IList<AmmoAnalysisViewModel> AmmoAnalysis { get; set; }

        public double MaxEhpRepairPerSecond { get; set; }

        private void InitTank(Ship ship)
        {
            var tank = ship.Tank;
            Ehp = tank.GetEhp();
            MaxEhpRepairPerSecond = GetAggregatedSelfReps(tank, null);

            EhpThermal = tank.GetEhp(THERMAL_PROFILE);
            EhpKinetic = tank.GetEhp(KINETIC_PROFILE);
            EhpExplosive = tank.GetEhp(EXPLOSIVE_PROFILE);
            EhpEm = tank.GetEhp(EM_PROFILE);

            MaxEhpRepairPerSecondThermal = GetAggregatedSelfReps(tank, THERMAL_PROFILE);
            MaxEhpRepairPerSecondKinetic = GetAggregatedSelfReps(tank, KINETIC_PROFILE);
            MaxEhpRepairPerSecondExplosive = GetAggregatedSelfReps(tank, EXPLOSIVE_PROFILE);
            MaxEhpRepairPerSecondEm = GetAggregatedSelfReps(tank, EM_PROFILE);
        }

        public double MaxEhpRepairPerSecondKinetic { get; set; }

        public double MaxEhpRepairPerSecondExplosive { get; set; }

        public double MaxEhpRepairPerSecondEm { get; set; }

        public double MaxEhpRepairPerSecondThermal { get; set; }

        private static double GetAggregatedSelfReps(Tank tank, DamageProfile damageProfile)
        {
            return tank.GetActiveArmorTankPerSecond(damageProfile) + tank.GetActiveShieldTankPerSecond(damageProfile) + tank.GetPeakPassiveTank(damageProfile)
                   + tank.GetActiveHullTankPerSecond(damageProfile);
        }

        private void InitDamage(Context context)
        {
            var damageOutput = context.GetDamageOutput();
            MaxAlpha = damageOutput.Entries.Sum(entry => entry.WeaponCount * entry.AmmoEntries.Max(x => x.Damage.AlphaDamage));
            MaxDps = damageOutput.Entries.Sum(entry => entry.WeaponCount * entry.AmmoEntries.Max(x => x.Damage.DamagePerSecond));
            AmmoAnalysis = damageOutput.Entries.SelectMany(x => x.AmmoEntries.Select(a => CreateAmmoAnalysisViewModel(x, a)))
                .ToArray();
        }

        private static AmmoAnalysisViewModel CreateAmmoAnalysisViewModel(DamageOutput.WeaponEntry x, DamageOutput.WeaponEntry.AmmoEntry a)
        {
            const int METER_PER_KM = 1000;
            return new AmmoAnalysisViewModel
                   {
                       WeaponCount = x.WeaponCount,
                       Alpha = a.Damage.AlphaDamage,
                       Dps = a.Damage.DamagePerSecond,
                       Weapon = x.WeaponType,
                       Falloff = a.FalloffInKm,
                       Optimal =
                           x.WeaponSystem == WeaponSystem.Missile || x.WeaponSystem == WeaponSystem.Bomb
                               ? a.VelocityInMeterPerSecond * TimeSpan.FromMilliseconds(a.FlightTimeInMilliseconds)
                                                                  .TotalSeconds / METER_PER_KM
                               : a.OptimalInKm,
                       Tracking = a.TrackingInRadPerSecond,
                       AmmoType = a.Ammunition
                   };
        }
    }

    /// <summary>
    ///     Interaction logic for FittingDisplay.xaml
    /// </summary>
    public partial class FittingDisplay : UserControl
    {
        public FittingDisplay()
        {
            InitializeComponent();
        }

        public void ShowShip(Context context)
        {
            Task.Factory.StartNew(
                                  () =>
                                  {
                                      FittingAnalysisViewModel viewModel = null;
                                      try
                                      {
                                          viewModel = new FittingAnalysisViewModel(context);
                                      }
                                      catch (Exception e)
                                      {
                                          Log.Logger.Warning(e, "Error in accessing fitting stats");
                                          //TODO messagebox? / visibility??
                                      }

                                      Application.Current.Dispatcher.Invoke(() => { DataContext = viewModel; });
                                  },
                                  TaskCreationOptions.LongRunning);
        }
    }
}
