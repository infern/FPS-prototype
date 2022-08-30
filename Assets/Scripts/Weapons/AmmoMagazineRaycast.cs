using UnityEngine;

namespace FPS_Prototype.Weapons
{
    public class AmmoMagazineRaycast : AmmoMagazine
    {
        #region Variables

        [SerializeField] Color laserColor = Color.yellow;
        [SerializeField] [Range(0.1f, 3f)] float laserWidth = 1f;
        public float LaserWidth => laserWidth;

        [SerializeField] [Range(0.1f, 1f)] float laserDuration = 0.4f;
        public float LaserDuration => laserDuration;

        #endregion

        #region Unique Methods
        public void AssignLaserValues(LineRenderer laserLine)
        {
            laserLine.startWidth = laserWidth;
            laserLine.startColor = laserColor;
        }

        #endregion
    }

}