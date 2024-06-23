using UnityEngine;

namespace MySummerCarTweaks.Tweaks.EnableConsole
{
    internal class EnableConsoleModule : MscTweak
    {
        private PlayMakerFSM _consoleLogic;
        internal override string Name => "Enable Console";

        internal override void MSC_OnLoad(MySummerCarTweaksMod mod)
        {
            foreach (GameObject gameObject in Resources.FindObjectsOfTypeAll<GameObject>())
                if (gameObject.name == "Console" && gameObject.transform.parent.name == "OptionsMenu") _consoleLogic = gameObject.GetComponent<PlayMakerFSM>();
        }

        internal override void MSC_OnUpdate(MySummerCarTweaksMod mod)
        {
            if (_consoleLogic.ActiveStateName == "State 3") _consoleLogic.SendEvent("SHIT");
        }
    }
}