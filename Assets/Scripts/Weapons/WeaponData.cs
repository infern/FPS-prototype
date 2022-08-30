using UnityEngine;

namespace FPS_Prototype.Weapons
{
    [CreateAssetMenu(menuName = "Weapons", fileName = "WeaponData")]
    public class WeaponData : ScriptableObject
    {
        public string weaponName;
        public float fireRate = 0.5f;
        public float afterSwapFireCooldown = 0.15f;
        public string infoDamage;
        public Sprite image;
    }

}