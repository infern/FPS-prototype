using UnityEngine;
using FPS_Prototype.Player;

namespace FPS_Prototype.General
{
    public class GameController : MonoBehaviour
    {
        [Header("Settings")]    /********/
        [SerializeField] AudioClip pauseOnSound;
        [SerializeField] AudioClip pauseOffSound;



        [Header("Data")]    /********/
        bool paused = false;
        bool floorPanelActive = false;


        [Header("Components")]    /********/
        [SerializeField] PlayerComponents player;
        [SerializeField] GameObject pausePanel;
        [SerializeField] AudioSource aS;
        [SerializeField] Animator anim;

        #region Base Methods

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]


        void OnEnable()
        {
            StatsEventManager.PauseGameTrigger += PauseToggle;
        }

        void OnDisable()
        {
            StatsEventManager.PauseGameTrigger -= PauseToggle;
        }

        void Start()
        {
            Time.timeScale = 1;
            DisplayCursor(false);
        }



        #endregion


        #region Unique Methods


        void PauseToggle()
        {
            if (!floorPanelActive)
            {
                if (paused) PauseOff();
                else PauseOn();
                paused = !paused;
            }
        }

        void PauseOn()
        {
            aS.clip = pauseOnSound;
            aS.Play();
            pausePanel.SetActive(true);
            DisplayCursor(true);
            Time.timeScale = 0f;
        }

        void PauseOff()
        {
            aS.clip = pauseOffSound;
            aS.Play();
            pausePanel.SetActive(false);
            DisplayCursor(false);
            Time.timeScale = 1;

        }

        public void DisplayCursor(bool display)
        {
            Cursor.lockState = display ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = display;
        }

        #endregion
    }

}