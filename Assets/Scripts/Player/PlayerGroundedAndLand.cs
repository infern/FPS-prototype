using UnityEngine;

namespace FPS_Prototype.Player
{
    public class PlayerGroundedAndLand : MonoBehaviour
    {
        #region Variables

        [Header("Data")]    /********/
        bool grounded;
        float groundedBufferTimer = 0f;
        bool groundedBufferActive;
        bool landed = true;
        int landSoundsQueue;
        Vector3 castDirection = new Vector3(0, -1);

        public bool IsGrounded => grounded;
        public float GroundedBufferTimer => groundedBufferTimer;


        [Header("Components")]    /********/
        [SerializeField]
        PlayerComponents player;
        [SerializeField] LayerMask groundLayer;


        #endregion


        #region Base Methods


        void Update()
        {
            GroundCheck();
        }

        #endregion


        #region Ground Detection
        void GroundCheck()
        {
            RaycastHit hit;
            bool groundDetected = Physics.SphereCast(transform.position, player.settings.GroundRaySphereRadius, castDirection,
                out hit, player.settings.GroundRayRange, player.settings.GroundLayer);

            if (groundDetected)
            {
                TouchingGround();
                if (!landed)
                {
                    Land();
                }
            }
            else
            {
                NotTouchingGround();
            }

            GroundBuffer();

        }

        private void TouchingGround()
        {
            grounded = true;
            groundedBufferActive = false;
        }

        private void NotTouchingGround()
        {
            grounded = false;
            landed = false;
        }

        void GroundBuffer()
        {
            bool activateGroundBuffer = (!grounded && !groundedBufferActive);
            if (activateGroundBuffer)
            {
                groundedBufferActive = true;
                groundedBufferTimer = player.StartTimer(player.settings.GroundedBufferDuration);
            }
        }

        void Land()
        {
            landed = true;
            player.jump.JumpBuffer();
            player.jump.DisableDoubleJump();
            PlayLandSoundFromQueue();
        }

        private void PlayLandSoundFromQueue()
        {
            player.PlaySound(1, player.settings.LandSounds[landSoundsQueue]);
            landSoundsQueue++;
            if (landSoundsQueue >= player.settings.LandSounds.Capacity) landSoundsQueue = 0;
        }



        #endregion






    }

}