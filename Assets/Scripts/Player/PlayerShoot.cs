using UnityEngine;
using FPS_Prototype.General;

namespace FPS_Prototype.Player
{ 
    public class PlayerShoot : MonoBehaviour
    {
        #region Variables

        [Header("Data")]    /********/
        bool buttonDown = false;
        bool shootingEnabled = true;

        [Header("Components")]    /********/
        [SerializeField]
        PlayerComponents player;


        #endregion


        #region Base Methods

        void OnEnable()
        {
            StatsEventManager.PauseGameTrigger += DisableShoot;
        }

        void OnDisable()
        {
            StatsEventManager.PauseGameTrigger -= DisableShoot;
        }


        void Update()
        {
            ButtonHeld();
        }

        private void ButtonHeld()
        {
            if (buttonDown && player.inventory.EquippedWeapon() != null)
            {
                player.inventory.EquippedWeapon().Trigger();
            }
        }



        #endregion

        #region Unique Methods

        public void ButtonDown()
        {
            buttonDown = true;
        }

        public void ButtonUp()
        {
            buttonDown = false;
        }


        void DisableShoot()
        {
            shootingEnabled = !shootingEnabled;
        }


        #endregion
    }

}