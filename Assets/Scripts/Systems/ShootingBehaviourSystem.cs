using Leopotam.Ecs;
using UnityEngine;

namespace ECSReaction
{
    public class ShootingBehaviourSystem : IEcsRunSystem
    {
        private EcsFilter<ShooterBehaviour, CharacterData, ShootingWeapon> filter;
        private EcsFilter<PlayerControlledMoving, TransformComponent> targetFilter;

        public void Run()
        {
            foreach(var shooter in filter)
            {
                Transform shooterTransform = filter.Get2(shooter).controller.transform;
                ref var shootingData = ref filter.Get1(shooter);
                Transform selectedTarget = null;
                var shooterEntity = filter.GetEntity(shooter);

                foreach(var target in targetFilter)
                {
                    Transform targetTransform = targetFilter.Get2(target).transform;
                    Vector3 direction = targetTransform.position - shooterTransform.position;
                    if (direction.magnitude > shootingData.fireRange) continue;

                    selectedTarget = targetTransform;
                    direction.y = 0;
                    shooterTransform.forward = direction;
                    break;
                }

                if (selectedTarget != null)
                {
                    ref var shooting = ref shooterEntity.Get<Shooting>();
                    shooting.target = selectedTarget;
                }
                else
                {
                    shooterEntity.Del<Shooting>();
                }
            }
        }
    }
}
