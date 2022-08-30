 using UnityEngine;
namespace FPS_Prototype.Player
{
    public class PlayerRun : MonoBehaviour
    {
        #region Variables


        [Header("Data")]    /********/
        public Vector2 inputDirection;
        public bool running = false;
        float speedMulltiplier = 1f;

        [Header("Components")]    /********/
        [SerializeField]
        PlayerComponents player;


        #endregion


        #region Base Methods

        void FixedUpdate()
        {
            Run();
        }

        #endregion

        #region Unique Methods

        public void Run()
        {
            if (inputDirection != Vector2.zero)
            {
                speedMulltiplier = player.status.IsGrounded ? player.settings.OnGroundRunSpeedMultiplier : player.settings.InAirRunSpeedMultiplier;
                Vector2 move = inputDirection * player.settings.RunSpeed * speedMulltiplier;
                player.rigidBody.AddRelativeForce(new Vector3(move.x, 0f, move.y), ForceMode.Impulse);
                running = true;
            }
            else running = false;
        }

        #endregion
    }

}