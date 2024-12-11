using Leopotam.Ecs;
using UnityEngine;

namespace ECSReaction
{
    public class WeaponControlSystem : IEcsRunSystem
    {
        private EcsFilter<ShootingWeapon, Shooting>.Exclude<WeaponReload> shootingFilter;
        private EcsFilter<ShootingWeapon, WeaponReload> reloadFilter;
        private ConfigData configData;

        public void Run()
        {
            foreach(var reload in reloadFilter)
            {
                ref var reloadInfo = ref reloadFilter.Get2(reload);
                reloadInfo.remainingTime -= Time.deltaTime;
                if(reloadInfo.remainingTime <= 0)
                {
                    var reloadingEntity = reloadFilter.GetEntity(reload);
                    reloadingEntity.Del<WeaponReload>();
                }
            }

            foreach(var shooting in shootingFilter)
            {
                ref var shootingInfo = ref shootingFilter.Get2(shooting);
                ref var weaponInfo = ref shootingFilter.Get1(shooting);
                var shootingEntity = shootingFilter.GetEntity(shooting);

                GameObject go = GameObject.Instantiate(configData.bulletPrefab);
                go.transform.position = weaponInfo.emitter.position;
                var bulletEntity = shootingEntity.GetInternalWorld().NewEntity();
                ref var transform = ref bulletEntity.Get<TransformComponent>();
                transform.transform = go.transform;
                ref var bullet = ref bulletEntity.Get<Bullet>();
                bullet.damage = weaponInfo.bulletDamage;
                bullet.remainingTime = weaponInfo.bulletLifetime;
                bullet.radius = weaponInfo.bulletRadius;
                Vector3 direction = shootingInfo.target.position - go.transform.position;
                direction.y = 0f;
                bullet.velocity = direction.normalized * weaponInfo.bulletSpeed;

                ref var reloading = ref shootingEntity.Get<WeaponReload>();
                reloading.remainingTime = weaponInfo.reloadTime;
            }
        }
    }
}
