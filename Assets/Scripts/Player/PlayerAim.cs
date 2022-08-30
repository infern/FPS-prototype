using UnityEngine;
using FPS_Prototype.General;


namespace FPS_Prototype.Player
{
    public class PlayerAim : MonoBehaviour
    {
        #region Variables

        [Header("Data")]    /********/
        Vector2 rotation = Vector2.zero;
        bool aimingEnabled = true;

        [Header("Components")]    /********/
        [SerializeField]
        PlayerComponents player;


        #endregion


        #region Base Methods

        void OnEnable()
        {
            StatsEventManager.PauseGameTrigger += AimingEnabledToggle;
        }

        void OnDisable()
        {
            StatsEventManager.PauseGameTrigger -= AimingEnabledToggle;
        }

        void Update()
        {
            RotateCamera();
        }

        #endregion

        #region Unique Methods

        public void RotateCamera()
        {
            if (aimingEnabled)
            {
                GetMouseInput();
                transform.eulerAngles = new Vector2(0, rotation.y) * player.settings.AimSpeed;
                Camera.main.transform.localRotation = Quaternion.Euler(rotation.x * player.settings.AimSpeed, 0, 0);
            }
        }

        private void GetMouseInput()
        {
            rotation.y += Input.GetAxis("Mouse X");
            rotation.x += -Input.GetAxis("Mouse Y");
            rotation.x = Mathf.Clamp(rotation.x, -15f, 15f);
        }

        void AimingEnabledToggle()
        {
            aimingEnabled = !aimingEnabled;
        }

        #endregion
    }

}