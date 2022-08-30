using UnityEngine.Events;
using UnityEngine;

namespace FPS_Prototype.General
{
    public class StatsEventManager : MonoBehaviour
    {
        public static event UnityAction PauseGameTrigger;
        public static void PauseToggle() => PauseGameTrigger?.Invoke();

        public static event UnityAction ObjectDestroyedTrigger;
        public static void ObjectDestroyed() => ObjectDestroyedTrigger?.Invoke();
    }

}