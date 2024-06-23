using UnityEngine;

namespace MySummerCarTweaks.Tweaks.FallDamage
{
    internal class FallDamageModule : MscTweak
    {
        private CharacterController _playerCharController;
        private Vector3 _playerLastVelocity;
        private Vector3 _minVel;
        private GameObject _death;
        internal override string Name => "Fall Damage";


        internal override void MSC_OnLoad(MySummerCarTweaksMod mod)
        {
            _playerCharController = GameObject.Find("PLAYER").GetComponent<CharacterController>();
            foreach (var gameObject in Resources.FindObjectsOfTypeAll<GameObject>())
            {
                if (gameObject.name == "Death" && gameObject.transform.parent.name == "Systems")
                {
                    _death = gameObject;
                }
            }
        }

        internal override void MSC_OnUpdate(MySummerCarTweaksMod mod)
        {
            var velocity = _playerCharController.velocity;
            _minVel = Vector3.Min(_playerLastVelocity, _minVel);
            _playerLastVelocity = velocity;
            if (_minVel.y < -5f && _playerCharController.isGrounded)
            {
                _death.SetActive(true);
            }
        }
    }
}