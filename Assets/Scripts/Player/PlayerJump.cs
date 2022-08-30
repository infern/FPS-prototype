using UnityEngine;
namespace FPS_Prototype.Player
{
    public class PlayerJump : MonoBehaviour
    {
        #region Variables

        [Header("Data")]    /********/
        float highJumpMaxTimer;
        bool buttonHeld = false;
        float bufferTimer;
        float cooldownTimer;
        bool midAirBounceCanBeUsed = false;
        int jumpSoundsQueue;

        [Header("Components")]    /********/
        [SerializeField]
        PlayerComponents player;

        enum JumpTypeEnum
        {
            normal,
            midAir,
            hyper

        }

        #endregion



        #region Base Methods

        void FixedUpdate()
        {
            HighJumpForceWhileHoldingButton();
        }

        #endregion


        #region Unique Methods

        public void ButtonDown()
        {
            if (!midAirBounceCanBeUsed)
            {
                Attempt();
            }
            else
            {
                MidAirBounce();
            }
            buttonHeld = true;
        }

        public void ButtonUp()
        {
            highJumpMaxTimer = Time.time;
            buttonHeld = false;
        }

        void Attempt()
        {
            bool grounded = player.status.IsGrounded || !player.IsTimerFinished(player.status.GroundedBufferTimer);
            bool canJump = player.IsTimerFinished(cooldownTimer) && grounded;

            if (canJump)
            {
                cooldownTimer = player.StartTimer(player.settings.JumpCooldownDuration);
                DoJump(JumpTypeEnum.normal);
            }
            else if (!grounded)
            {
                bufferTimer = player.StartTimer(player.settings.JumpBufferDuration);
            }
        }

        void MidAirBounce()
        {
            if (player.rigidBody.velocity.y >= -9f)
            {
                DoJump(JumpTypeEnum.midAir);
            }
        }


        void DoJump(JumpTypeEnum jumpType)
        {
            float force = 1f;
            switch (jumpType)
            {
                case JumpTypeEnum.normal:
                    {
                        highJumpMaxTimer = player.StartTimer(player.settings.HighJumpMaxDuration);
                        midAirBounceCanBeUsed = true;
                        force = player.settings.LowJumpForce;
                        break;
                    }

                case JumpTypeEnum.midAir:
                    {
                        highJumpMaxTimer = Time.time;
                        midAirBounceCanBeUsed = false;
                        force = player.settings.DoubleJumpForce;
                        break;
                    }
                case JumpTypeEnum.hyper:
                    {
                        midAirBounceCanBeUsed = true;
                        force = player.settings.DoubleJumpForce;
                        force *= force;
                        break;
                    }
                default: { force = 1; break; }
            }


            player.rigidBody.velocity = new Vector3(player.rigidBody.velocity.x, force, player.rigidBody.velocity.z);
            PlaySound();
        }

        void HighJumpForceWhileHoldingButton()
        {
            if (!player.IsTimerFinished(highJumpMaxTimer) && buttonHeld)
            {
                player.rigidBody.AddForce(new Vector3(0f, player.settings.HighJumpForce, 0), ForceMode.Impulse);
            }
        }

        void PlaySound()
        {
            player.PlaySound(0, player.settings.JumpSounds[jumpSoundsQueue]);
            jumpSoundsQueue++;
            if (jumpSoundsQueue >= player.settings.JumpSounds.Capacity) jumpSoundsQueue = 0;
        }


        //This is executed in Land() method
        public void DisableDoubleJump()
        {
            midAirBounceCanBeUsed = false;
        }


        //This is executed in Land() method
        public void JumpBuffer()
        {
            if (!player.IsTimerFinished(player.jump.bufferTimer))
            {
                Attempt();
            }
        }
    }

    #endregion


}