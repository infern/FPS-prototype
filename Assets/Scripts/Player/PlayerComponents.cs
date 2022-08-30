using UnityEngine;


namespace FPS_Prototype.Player
{
    public class PlayerComponents : MonoBehaviour
    { 
        public PlayerGroundedAndLand status;
        public PlayerRun run;
        public PlayerAim aim;
        public PlayerJump jump;
        public PlayerShoot shoot;
        public PlayerInventory inventory;
        public ReadPlayerInput input;
        public PlayerSettings settings;

        public Rigidbody rigidBody;

        public AudioSource audioSource1;
        public AudioSource audioSource2;

        public BoxCollider boxCollider;

        void Start()
        {
            if (settings == null)
            {
                Debug.LogError("Settings missing from PlayerComponent!");
                Debug.Break();
            }
        }

        public void PlaySound(int source, AudioClip clip)
        {
            if (source == 0)
            {
                audioSource1.clip = clip;
                audioSource1.Stop();
                audioSource1.Play();
            }
            else
            {
                audioSource2.clip = clip;
                audioSource2.Play();
            }

        }

        public float StartTimer(float duration)
        {
            return Time.time + duration;
        }

        public bool IsTimerFinished(float duration)
        {
            return Time.time > duration;
        }
    }

}