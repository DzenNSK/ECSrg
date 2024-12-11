using Leopotam.Ecs;
using UnityEngine;

namespace ECSReaction
{
    public class BulletSystem : IEcsRunSystem
    {
        private EcsFilter<Bullet, TransformComponent>.Exclude<DestroyEntity> filter;

        public void Run()
        {
            foreach(var entity in filter)
            {
                ref var bulletInfo = ref filter.Get1(entity);
                ref var bulletTransform = ref filter.Get2(entity);
                var bulletEntity = filter.GetEntity(entity);
                bulletInfo.remainingTime -= Time.fixedDeltaTime;

                if(bulletInfo.remainingTime <= 0)
                {
                    ref var destroy = ref bulletEntity.Get<DestroyEntity>();
                    continue;
                }

                Vector3 trajectory = bulletInfo.velocity * Time.fixedDeltaTime;
                Ray ray = new Ray(bulletTransform.transform.position, trajectory);
                if(Physics.Raycast(ray, out var hitInfo, trajectory.magnitude))
                {
                    ref var destroy = ref bulletEntity.Get<DestroyEntity>();
                    GameObject hitGo = hitInfo.collider.gameObject;
                    EntityView entityView = hitGo.GetComponent<EntityView>();
                    if(entityView != null)
                    {
                        EcsEntity hitEntity = entityView.GetEntity();
                        ref var damage = ref hitEntity.Get<ApplyDamage>();
                        damage.damageAmount += bulletInfo.damage;
                    }

                    continue;
                }

                Vector3 newPos = bulletTransform.transform.position + trajectory;
                bulletTransform.transform.position = newPos;
            }
        }
    }
}
