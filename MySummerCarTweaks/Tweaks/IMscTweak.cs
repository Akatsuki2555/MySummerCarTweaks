using MSCLoader;

namespace MySummerCarTweaks.Tweaks
{
    internal abstract class MscTweak
    {
        internal abstract string Name { get; }

        internal bool IsEnabled
        {
            get => IsEnabledCheckbox.GetValue();
            set => IsEnabledCheckbox.SetValue(value);
        }

        internal bool HasThrownUpdateError { get; set; } = false;
        internal bool HasFinishedLoadMethod { get; set; } = false;

        internal SettingsCheckBox IsEnabledCheckbox { get; set; }

        internal virtual void MSC_ModSettings(MySummerCarTweaksMod mod)
        {
        }

        internal virtual void MSC_OnLoad(MySummerCarTweaksMod mod)
        {
        }

        internal virtual void MSC_OnUpdate(MySummerCarTweaksMod mod)
        {
        }

        internal virtual void MSC_OnSave(MySummerCarTweaksMod mod)
        {
        }

        internal virtual void MSC_OnMenuLoad(MySummerCarTweaksMod mod)
        {
        }
    }
}