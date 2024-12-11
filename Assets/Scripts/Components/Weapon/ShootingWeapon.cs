using System;
using UnityEngine;

namespace ECSReaction
{
    [Serializable]
    public struct ShootingWeapon
    {
        public float reloadTime;
        public float bulletSpeed;
        public int bulletDamage;
        public float bulletRadius;
        public float bulletLifetime;
        public Transform emitter;
    }
}
