using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Net;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using MSCLoader;
using UnityEngine;

public class MySummerCarTweaks : Mod {
    public override string ID => nameof(MySummerCarTweaks);
    public override string Version => "1.0";
    public override string Author => "mldkyt";
    public override string Description => "A set of tweaks for My Summer Car";
    
    private SettingsCheckBox _disableGreenCar;
    private SettingsCheckBox _disableKylajani;
    private SettingsCheckBox _disableAmis2;
    private SettingsCheckBox _saveCarTemp;
    private SettingsCheckBox _playerCantDieFromUrine;
    private SettingsCheckBox _enableConsole;
    private SettingsCheckBox _fallDamage;
    private SettingsCheckBox _betterWindowHandles;

    private FsmFloat _playerUrine;
    private CharacterController _playerCharController;
    private Vector3 _playerLastVelocity;
    private Vector3 _lastDiff, _minVel;
    private PlayMakerFSM _pissLogic;
    private PlayMakerFSM _consoleLogic;
    private GameObject _death;
    
    private bool _showSuggestionBox;
    private string _suggestionBoxText = "";

    public override void ModSetup() {
        SetupFunction(Setup.OnLoad, On_Load);
        SetupFunction(Setup.Update, On_Update);
        SetupFunction(Setup.OnSave, On_Save);
    }

    public override void ModSettings() {
        _disableGreenCar = Settings.AddCheckBox(this, "disableGreenCar", "Disable green car");
        _disableKylajani = Settings.AddCheckBox(this, "disableKylajani", "Disable Kyläjäni");
        _disableAmis2 = Settings.AddCheckBox(this, "disableAmis2", "Disable Amis2");
        _saveCarTemp = Settings.AddCheckBox(this, "saveCarTemp", "Save Shitsuma's temperature");
        _playerCantDieFromUrine = Settings.AddCheckBox(this, "playerCantDieFromUrine", "Player can't die from urine like IRL");
        _enableConsole = Settings.AddCheckBox(this, "enableConsole", "Enable console");
        _fallDamage = Settings.AddCheckBox(this, "fallDamage", "Add fall damage");
        _betterWindowHandles = Settings.AddCheckBox(this, "betterWindowHandles", "Make the window handles slightly faster");
        Settings.AddButton(this, "Suggest an idea", delegate {
            Application.OpenURL("https://forms.gle/CXvB8hVuqbEEaann9");
        });
        Settings.AddButton(this, "Join my Discord", delegate {
            Application.OpenURL("https://redir.mldkyt.com/discord");
        });
    }

    private void On_Load() {
        _playerUrine = FsmVariables.GlobalVariables.FindFsmFloat("PlayerUrine");
        _pissLogic = GameObject.Find("PLAYER/Pivot/AnimPivot/Camera/FPSCamera/Piss")
            .GetComponents<PlayMakerFSM>()
            .First(x => x.FsmName == "Logic");
        _playerCharController = GameObject.Find("PLAYER").GetComponent<CharacterController>();

        if (_betterWindowHandles.GetValue()) {
            // Satsuma door left
            var opener = GameObject.Find("door left(Clone)/opener").GetComponent<PlayMakerFSM>();
            var rotateUp = opener.GetState("State 1").GetAction<iTweenRotateUpdate>(0);
            rotateUp.time.Value /= 4;
            var moveUp = opener.GetState("State 1").GetAction<iTweenMoveUpdate>(1);
            moveUp.time.Value /= 4;
            var rotateDown = opener.GetState("State 2").GetAction<iTweenRotateUpdate>(0);
            rotateDown.time.Value /= 4;
            var moveDown = opener.GetState("State 2").GetAction<iTweenMoveUpdate>(1);
            moveDown.time.Value /= 4;
            
            // Satsuma door right
            opener = GameObject.Find("door right(Clone)/opener").GetComponent<PlayMakerFSM>();
            rotateUp = opener.GetState("State 1").GetAction<iTweenRotateUpdate>(0);
            rotateUp.time.Value /= 2;
            moveUp = opener.GetState("State 1").GetAction<iTweenMoveUpdate>(1);
            moveUp.time.Value /= 2;
            rotateDown = opener.GetState("State 2").GetAction<iTweenRotateUpdate>(0);
            rotateDown.time.Value /= 2;
            moveDown = opener.GetState("State 2").GetAction<iTweenMoveUpdate>(1);
            moveDown.time.Value /= 2;
        }
        
        foreach (var go in Resources.FindObjectsOfTypeAll<GameObject>()) {
            if (go.name == "Console" && go.transform.parent.name == "OptionsMenu") {
                _consoleLogic = go.GetComponent<PlayMakerFSM>();
            }

            if (go.name == "Death" && go.transform.parent.name == "Systems") {
                _death = go;
            }
        }

        if (_saveCarTemp.GetValue() && SaveLoad.ValueExists(this, "car_temperature")) {
            var carTemp = FsmVariables.GlobalVariables.FindFsmFloat("EngineTemp");
            carTemp.Value = SaveLoad.ReadValue<float>(this, "car_temperature");
        }
    }

    private void On_Update() {
        if (_disableGreenCar.GetValue()) {
            var fittan = GameObject.Find("TRAFFIC/VehiclesDirtRoad/Rally/FITTAN");
            if (fittan != null) {
                fittan.SetActive(false);
            }
        }

        if (_disableKylajani.GetValue()) {
            var kylajani = GameObject.Find("NPC_CARS/Amikset/KYLAJANI");
            if (kylajani != null) {
                kylajani.SetActive(false);
            }
        }

        if (_disableAmis2.GetValue()) {
            var amis2 = GameObject.Find("NPC_CARS/Amikset/AMIS2");
            if (amis2 != null) {
                amis2.SetActive(false);
            }
        }

        if (_playerCantDieFromUrine.GetValue()) {
            if (_playerUrine.Value > 100 && _pissLogic.ActiveStateName == "Start urinate") {
                _pissLogic.SendEvent("FINISHED");
            }
        }

        if (_enableConsole.GetValue()) {
            if (_consoleLogic.ActiveStateName == "State 3") {
                _consoleLogic.SendEvent("SHIT");
            }
        }

        if (_fallDamage.GetValue()) {
            var velocity = _playerCharController.velocity;
            _lastDiff = _playerLastVelocity - velocity;
            _minVel = Vector3.Min(_playerLastVelocity, _minVel);
            _playerLastVelocity = velocity;
            if (_minVel.y < -5 && _playerCharController.isGrounded) {
                _death.SetActive(true);
            }
        }
    }

    private void On_Save() {
        if (_saveCarTemp.GetValue()) {
            var carTemp = FsmVariables.GlobalVariables.FindFsmFloat("EngineTemp");
            SaveLoad.WriteValue(this, "car_temperature", carTemp.Value);
        }
    }
}