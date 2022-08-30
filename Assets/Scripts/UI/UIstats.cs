using UnityEngine;
using TMPro;
using System;
using FPS_Prototype.General;

namespace FPS_Prototype.UI
{
    public class UIstats : MonoBehaviour
    {
        #region Variables

        [Header("Data")]    /********/
        int objectsDestroyed = 0;

        [Header("Components")]    /********/
        [SerializeField]
        TextMeshProUGUI timeTextMeshPro;
        [SerializeField]
        TextMeshProUGUI destroyedObjectsTextMeshPro;
        [SerializeField]
        Animator destroyedObjectsTextAnimator;

        #endregion

        void OnEnable()
        {
            StatsEventManager.ObjectDestroyedTrigger += UpdateObjectsDestroyed;
        }

        void OnDisable()
        {
            StatsEventManager.ObjectDestroyedTrigger -= UpdateObjectsDestroyed;
        }

        void Update()
        {
            UpdateTimeSinceStartupText();
        }

        private void UpdateTimeSinceStartupText()
        {
            var ts = TimeSpan.FromSeconds(Time.realtimeSinceStartup);
            timeTextMeshPro.text = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
        }

        void UpdateObjectsDestroyed()
        {
            objectsDestroyed++;
            destroyedObjectsTextMeshPro.text = objectsDestroyed.ToString();
            destroyedObjectsTextAnimator.Play("bounce", -1, -1);
        }
    }

}