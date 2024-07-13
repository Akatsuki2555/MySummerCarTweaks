using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using MSCLoader;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MySummerCarTweaks.Tweaks.WaterBills
{
    internal class WaterBillsModule : MscTweak
    {
        internal override string Name => "Water Bills";

        private SettingsSlider _costPerLiter;
        private SettingsSlider _combustionTap;
        private SettingsSlider _combustionShower;

        private readonly FsmBool _waterBillsPaid = new FsmBool("WaterBillsPaid")
        {
            Value = true
        };

        private readonly FsmFloat _timer = new FsmFloat("WaterCutTimer")
        {
            Value = 50400f
        };

        private readonly FsmFloat _cost = new FsmFloat("Cost")
        {
            Value = 11
        };

        private readonly FsmFloat _litres = new FsmFloat("Litres")
        {
            Value = 0
        };

        private readonly FsmFloat _litresCalc = new FsmFloat("LitresCalc")
        {
            Value = 0
        };

        private GameObject _envelope;
        private FsmBool _kitchenTap;
        private FsmBool _bathroomTap, _bathroomShower;

        internal override void MSC_ModSettings(MySummerCarTweaksMod mod)
        {
#if DEBUG
            Settings.AddButton(mod, "DEBUG Set WaterBillsPaid True", () => { _waterBillsPaid.Value = true; });
            Settings.AddButton(mod, "DEBUG Set WaterBillsPaid False", () => { _waterBillsPaid.Value = false; });
            Settings.AddButton(mod, "DEBUG Set timer to 10", () => { _timer.Value = 10; });
            Settings.AddButton(mod, "DEBUG Set timer to 50400", () => { _timer.Value = 50400; });
            Settings.AddButton(mod, "DEBUG Add 1000L", () => { _litres.Value += 1000; });
            Settings.AddButton(mod, "DEBUG Add 100L", () => { _litres.Value += 100; });

#endif

            _costPerLiter = Settings.AddSlider(mod, "costperl", "Cost Per Litre", 5f, 15, 11);
            Settings.AddText(mod,
                "<color=red>WARNING: Changing the value above is not possible after the game is loaded.</color>");

            _combustionTap = Settings.AddSlider(mod, "combustiontap", "Combustion Tap Multiplier", 1, 10, 1f);
            _combustionShower = Settings.AddSlider(mod, "combustionshower", "Combustion Shower Multiplier", 1, 10, 2f);
        }

        internal override void MSC_OnLoad(MySummerCarTweaksMod mod)
        {
            if (!SaveLoad.ValueExists(mod, "WaterCost"))
                SaveLoad.WriteValue(mod, "WaterCost", _costPerLiter.GetValue());

            var waterCost = SaveLoad.ReadValue<float>(mod, "WaterCost");
            _cost.Value = waterCost;

            ModConsole.Log("[WaterBills]Creating WaterBillsMgr");
            var go = new GameObject("WaterBills");
            var fsm = go.AddComponent<PlayMakerFSM>();
            fsm.FsmName = "WaterBillsMgr";

            ModConsole.Log("[WaterBills]Adding variables to WaterBillsMgr");
            fsm.AddVariable(_waterBillsPaid);
            fsm.AddVariable(_timer);
            fsm.AddVariable(_cost);
            fsm.AddVariable(_litres);
            fsm.AddVariable(_litresCalc);

            ModConsole.Log("[WaterBills]Loading save items");
            if (SaveLoad.ValueExists(mod, "WaterCutTimer"))
                _timer.Value = SaveLoad.ReadValue<float>(mod, "WaterCutTimer");
            if (SaveLoad.ValueExists(mod, "WaterLitres"))
                _litres.Value = SaveLoad.ReadValue<float>(mod, "WaterLitres");

            ModConsole.Log("[WaterBills]Apply changes to KitchenTap");
            ApplyWaterBillsKitchenTap();
            ModConsole.Log("[WaterBills]Apply changes to ShowerTap");
            ApplyWaterBillsShower();

            ModConsole.Log("[WaterBills]Create water bill envelope");
            var waterBill = Object.Instantiate(GameObject.Find("Sheets").C(15));
            waterBill.SetActive(true);
            waterBill.name = "WaterBill";
            waterBill.transform.parent = GameObject.Find("Sheets").transform;

            waterBill.C(4).C(1).GetComponent<TextMesh>().text = "L";
            waterBill.C(5).C(1).GetComponent<TextMesh>().text = "mk/L";

            ModConsole.Log("[WaterBills]Edit FSM of water bill envelope");
            var waterBillFsm = waterBill.GetPlayMaker("Data");

            ModConsole.Log("[WaterBills]Change unpaid bills value to mod...");
            var waterBillFsmUnpaidBills = waterBillFsm.GetState("Set data").GetAction<GetFsmFloat>(0);
            waterBillFsmUnpaidBills.gameObject.GameObject = go;
            waterBillFsmUnpaidBills.fsmName = "WaterBillsMgr";
            waterBillFsmUnpaidBills.variableName = "LitresCalc";
            waterBillFsmUnpaidBills.everyFrame = false;

            ModConsole.Log("[WaterBills]Change price of bills to mod...");
            var waterBillFsmPrice = waterBillFsm.GetState("Set data").GetAction<GetFsmFloat>(1);
            waterBillFsmPrice.gameObject.GameObject = go;
            waterBillFsmPrice.fsmName = "WaterBillsMgr";
            waterBillFsmPrice.variableName = "Cost";

            var payFsm = waterBill.C(0).GetPlayMaker("Button");
            var payFsmCheckMoney = payFsm.GetState("Check money").GetAction<GetFsmFloat>(0);
            payFsmCheckMoney.gameObject.GameObject = go;
            payFsmCheckMoney.fsmName = "WaterBillsMgr";
            payFsmCheckMoney.variableName = "LitresCalc";
            
            ModConsole.Log("[WaterBills]Change pay button on envelope...");
            var payFsmPay = payFsm.GetState("Date");

            payFsmPay.RemoveAction(2);
            payFsmPay.InsertAction(2, new SetFloatValue()
            {
                floatVariable = _timer,
                floatValue = 50400
            });

            payFsmPay.InsertAction(3, new SetFloatValue()
            {
                floatVariable = _litres,
                floatValue = 0
            });

            waterBill.SetActive(false);
            
            ModConsole.Log("[WaterBills]Spawn envelope in envelope box...");
            _envelope = Object.Instantiate(GameObject.Find("YARD").C(4).C(1));
            _envelope.name = "EnvelopeWaterBill";
            _envelope.transform.parent = GameObject.Find("YARD").C(4).transform;
            _envelope.transform.localPosition = new Vector3(0.014f, -0.0026f, 0.1807f);
            _envelope.transform.localRotation = Quaternion.Euler(0, 0, 0);

            _envelope.GetPlayMaker("Use").enabled = true;
            _envelope.GetPlayMaker("Use").GetState("State 2").GetAction<SetStringValue>(1).stringValue =
                "WATER BILL";
            _envelope.GetPlayMaker("Use").GetState("Open bill").GetAction<ActivateGameObject>(1).gameObject
                .GameObject = waterBill;
            ModConsole.Log("[WaterBills]Done!");
        }

        internal override void MSC_OnUpdate(MySummerCarTweaksMod mod)
        {
            _timer.Value -= Time.deltaTime * Mathf.Max(1, _litresCalc.Value / 1000);
            _waterBillsPaid.Value = _timer.Value > 0;
            _envelope.SetActive(_timer.Value < 21600);

            if (_kitchenTap.Value) _litres.Value += (Time.deltaTime / 5) * _combustionTap.GetValue();
            if (_bathroomShower.Value) _litres.Value += (Time.deltaTime / 5) * _combustionShower.GetValue();
            if (_bathroomTap.Value) _litres.Value += (Time.deltaTime / 5) * _combustionTap.GetValue();

            _litresCalc.Value = _litres.Value * _cost.Value;
        }

        internal override void MSC_OnSave(MySummerCarTweaksMod mod)
        {
            SaveLoad.WriteValue(mod, "WaterCutTimer", _timer.Value);
            SaveLoad.WriteValue(mod, "WaterLitres", _litres.Value);
        }

        private void ApplyWaterBillsKitchenTap()
        {
            var kitchenTap = GameObject.Find("YARD").C(2).C(6).C(3).C(2).GetPlayMaker("Use");
            var kitchenTapAllTrue = kitchenTap.GetState("Check elec").GetAction<BoolAllTrue>(1);
            kitchenTapAllTrue.boolVariables = kitchenTapAllTrue.boolVariables.AddToArray(_waterBillsPaid);
            _kitchenTap = kitchenTap.GetVariable<FsmBool>("SwitchOn");
        }

        private void ApplyWaterBillsShower()
        {
            var showerTap = GameObject.Find("YARD").C(2).C(4).C(1).C(4).GetPlayMaker("Switch");
            showerTap.GetState("Shower").GetAction<BoolAllTrue>(4).boolVariables.AddToArray(_waterBillsPaid);
            showerTap.GetState("Tap").GetAction<BoolAllTrue>(3).boolVariables.AddToArray(_waterBillsPaid);

            _bathroomShower = showerTap.GetVariable<FsmBool>("ShowerOn");
            _bathroomTap = showerTap.GetVariable<FsmBool>("TapOn");
        }
    }
}