using System.Collections.Generic;
using UnityEngine;

namespace FPS_Prototype.Player
{
    [CreateAssetMenu(menuName = "Player/Settings", fileName = "PlayerData")]
    public class PlayerSettings : ScriptableObject
    {
        #region Variables

        [Header("PlayerGroundedAndLand")]    /********/
        [SerializeField] [Range(.6f, 1.6f)] float groundRayRange;
        [SerializeField] [Range(.1f, .5f)] float groundRaySphereRadius;
        [SerializeField] [Range(.1f, .4f)] float groundedBufferDuration;
        [SerializeField] List<AudioClip> landSounds = new List<AudioClip>();
        [SerializeField] LayerMask groundLayer;

        [Header("PlayerRun")]    /********/
        [SerializeField] [Range(0.5f, 3f)] float runSpeed;
        [SerializeField] [Range(0f, 2f)] float onGroundRunSpeedMultiplier;

        [SerializeField] [Range(0f, 2f)] float inAirRunSpeedMultiplier;

        [Header("PlayerAim")]    /********/
        [SerializeField] [Range(0.5f, 6f)] float aimSpeed;

        [Header("PlayerJump")]    /********/
        [SerializeField] [Range(2f, 5f)] float lowJumpForce;
        [SerializeField] [Range(.15f, 0.4f)] private float jumpBufferDuration;
        [SerializeField] [Range(.2f, 1.5f)] float jumpCooldownDuration;
        [SerializeField] [Range(2f, 6f)] float highJumpForce;
        [SerializeField] [Range(0.1f, 1f)] float highJumpMaxDuration;
        [SerializeField] [Range(11f, 15f)] float doubleJumpForce;
        [SerializeField] [Range(0f, .6f)] float doubleJumpCastingDuration;
        [SerializeField] List<AudioClip> jumpSounds = new List<AudioClip>();



        #endregion


        #region Getters

        /////////////////PlayerGroundedAndLand
        public float GroundRayRange { get { return groundRayRange; } }
        public float GroundRaySphereRadius { get { return groundRaySphereRadius; } }
        public float GroundedBufferDuration { get { return groundedBufferDuration; } }
        public List<AudioClip> LandSounds { get { return landSounds; } }
        public LayerMask GroundLayer { get { return groundLayer; } }

        /////////////////PlayerRun
        public float RunSpeed { get { return runSpeed; } }
        public float OnGroundRunSpeedMultiplier { get { return onGroundRunSpeedMultiplier; } }
        public float InAirRunSpeedMultiplier { get { return inAirRunSpeedMultiplier; } }

        /////////////////PlayerAim
        public float AimSpeed { get { return aimSpeed; } }

        /////////////////PlayerJump
        public float LowJumpForce { get { return lowJumpForce; } }
        public float JumpBufferDuration { get { return jumpBufferDuration; } }
        public float JumpCooldownDuration { get { return jumpCooldownDuration; } }
        public float HighJumpForce { get { return highJumpForce; } }
        public float HighJumpMaxDuration { get { return highJumpMaxDuration; } }
        public float DoubleJumpForce { get { return doubleJumpForce; } }
        public float DoubleJumpCastingDuration { get { return doubleJumpCastingDuration; } }
        public List<AudioClip> JumpSounds { get { return jumpSounds; } }



        #endregion











    }

}