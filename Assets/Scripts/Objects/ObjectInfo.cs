using System;

namespace FPS_Prototype.Objects
{
    public class ObjectInfo
    {
        [Flags]
        public enum MaterialType
        {
            Wood = 1,
            Metal = 2,
            Essence = 4,
        }
    }

}