using System.Collections;
using UnityEngine;

namespace FPS_Prototype.Weapons
{
    public class Reload : MonoBehaviour
    {
        #region Variables

        [Header("Data")]    /********/
        Coroutine Co_Reload;
        bool finishedReloading = false;
        bool actionStarted = false;

        public bool ActionStarted => actionStarted;


        [Header("Components")]    /********/
        [SerializeField] AudioSource audioSource;
        [SerializeField] Weapon weapon;



        #endregion

        #region Base Methods


        void OnDisable()
        {
            Stop();
        }

        #endregion

        #region Unique Methods

        public void Begin()
        {
            if (Co_Reload == null)
            {
                audioSource.Play();
                transform.GetComponent<Animator>().Play("reload");
                finishedReloading = false;
                actionStarted = true;
                Co_Reload = StartCoroutine(WaitForAnimationToFinish());
            }
        }

        public void Stop()
        {
            if (Co_Reload != null)
            {
                actionStarted = false;
                StopCoroutine(Co_Reload);
                Co_Reload = null;

            }
        }
        IEnumerator WaitForAnimationToFinish()
        {
            yield return new WaitUntil(() => finishedReloading == true);
            End();
        }

        //Trigger this method in Animation events
        public void ReloadAnimationFinished() => finishedReloading = true;

        private void End()
        {
            actionStarted = false;
            weapon.Ammo.LoadMax();
            Co_Reload = null;
        }

        #endregion

    }

}