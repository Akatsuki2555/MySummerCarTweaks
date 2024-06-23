using HutongGames.PlayMaker;
using MSCLoader;

namespace MySummerCarTweaks.Tweaks.SaveCarTemperature
{
    internal class SaveCarTemperatureModule : MscTweak
    {
        internal override string Name => "Save Car Temperature";

        internal override void MSC_OnLoad(MySummerCarTweaksMod mod)
        {
            if (SaveLoad.ValueExists(mod, "car_temperature"))
            {
                FsmVariables.GlobalVariables.FindFsmFloat("EngineTemp").Value =
                    SaveLoad.ReadValue<float>(mod, "car_temperature");
            }
        }

        internal override void MSC_OnSave(MySummerCarTweaksMod mod)
        {
            var fsmFloat = FsmVariables.GlobalVariables.FindFsmFloat("EngineTemp");
            SaveLoad.WriteValue(mod, "car_temperature", fsmFloat.Value);
        }
    }
}