﻿using System;
using System.Collections.Generic;
using MSCLoader;
using MySummerCarTweaks.Tweaks;
using MySummerCarTweaks.Tweaks.BetterFPSMeter;
using MySummerCarTweaks.Tweaks.BetterWindowHandles;
using MySummerCarTweaks.Tweaks.ChangeLoadingScreen;
using MySummerCarTweaks.Tweaks.DisableAmis2;
using MySummerCarTweaks.Tweaks.DisableGreenCar;
using MySummerCarTweaks.Tweaks.DisableKylajani;
using MySummerCarTweaks.Tweaks.DisableRally;
using MySummerCarTweaks.Tweaks.EnableConsole;
using MySummerCarTweaks.Tweaks.FallDamage;
using MySummerCarTweaks.Tweaks.GarageCustomizer;
using MySummerCarTweaks.Tweaks.GifuGaugeFix;
using MySummerCarTweaks.Tweaks.InfiniteNitrous;
using MySummerCarTweaks.Tweaks.MoneyChanges;
using MySummerCarTweaks.Tweaks.MoveJokkeInMenu;
using MySummerCarTweaks.Tweaks.PlayerCantDieFromUrine;
using MySummerCarTweaks.Tweaks.SaveCarTemperature;
using MySummerCarTweaks.Tweaks.WaterBills;

namespace MySummerCarTweaks
{
    public class MySummerCarTweaksMod : Mod
    {
        public override string ID => "MySummerCarTweaks";
        public override string Name => "My Summer Car Tweaks";
        public override string Version => "2.0";
        public override string Author => "アカツキ";
        public override string Description => "A set of tweaks for My Summer Car";

        private readonly List<MscTweak> _tweaks = new List<MscTweak>
        {
            new BetterFPSMeterModule(),
            new BetterWindowHandlesModule(),
            new DisableAmis2Module(),
            new DisableKylajaniModule(),
            new DisableRallyModule(),
            new EnableConsoleModule(),
            new FallDamageModule(),
            new DisableGreenCarModule(),
            new SaveCarTemperatureModule(),
            new PlayerCantDieFromUrineModule(),
            new MoveJokkeInMenuModule(),
            new ChangeLoadingScreenModule(),
            new GarageCustomizerModule(),
            new MoneyChangesModule(),
            new GifuGaugeFixModule(),
            new InfiniteNitrousModule(),
            new WaterBillsModule()
        };

        public override void ModSetup()
        {
            SetupFunction(Setup.OnLoad, () =>
            {
                _tweaks.ForEach(x =>
                {
                    if (!x.IsEnabled) return;
                    try
                    {
                        x.MSC_OnLoad(this);
                    }
                    catch (Exception e)
                    {
                        ModConsole.LogError($"Module {x.Name} threw an error!");
                        ModConsole.LogError("Message: " + e.Message);
                        ModConsole.LogError(e.StackTrace);
                    }
                    
                    x.HasFinishedLoadMethod = true;
                    ModConsole.Log("[MSCTweaks]Enable Update method for " + x.Name);
                });
            });
            SetupFunction(Setup.Update, () =>
            {
                if (ModLoader.GetCurrentScene() != CurrentScene.Game) return;

                _tweaks.ForEach(x =>
                {
                    if (!x.IsEnabled) return;
                    if (!x.HasFinishedLoadMethod) return;
                    try
                    {
                        x.MSC_OnUpdate(this);
                    }
                    catch (Exception e)
                    {
                        if (x.HasThrownUpdateError) return;
                        ModConsole.LogError($"Module {x.Name} threw an error!");
                        ModConsole.LogError("Message: " + e.Message);
                        ModConsole.LogError(e.StackTrace);
                        x.HasThrownUpdateError = true;
                    }
                });
            });
            SetupFunction(Setup.OnSave, () =>
            {
                _tweaks.ForEach(x =>
                {
                    if (!x.IsEnabled) return;
                    try
                    {
                        x.MSC_OnSave(this);
                    }
                    catch (Exception e)
                    {
                        ModConsole.LogError($"Module {x.Name} threw an error!");
                        ModConsole.LogError("Message: " + e.Message);
                        ModConsole.LogError(e.StackTrace);
                    }
                });
            });
            SetupFunction(Setup.OnMenuLoad, () =>
            {
                _tweaks.ForEach(x =>
                {
                    if (!x.IsEnabled) return;
                    try
                    {
                        x.MSC_OnMenuLoad(this);
                    }
                    catch (Exception e)
                    {
                        ModConsole.LogError($"Module {x.Name} threw an error!");
                        ModConsole.LogError("Message: " + e.Message);
                        ModConsole.LogError(e.StackTrace);
                    }
                });
            });
        }

        public override void ModSettings()
        {
            _tweaks.ForEach(x =>
            {
                Settings.AddHeader(this, x.Name, collapsedByDefault: true);
                x.IsEnabledCheckbox = Settings.AddCheckBox(this, $"Enabled {x.Name}", x.Name);
                x.MSC_ModSettings(this);
            });
        }
    }
}