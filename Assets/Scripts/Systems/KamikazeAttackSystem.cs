using Leopotam.Ecs;
using UnityEngine;

namespace ECSReaction
{
    public class KamikazeAttackSystem : IEcsRunSystem
    {
        private EcsFilter<KamikazeAttack, TransformComponent> attackerFilter;
        private EcsFilter<PlayerControlledMoving, TransformComponent> targetFilter;

        public void Run()
        {
            foreach(var attacker in attackerFilter)
            {
                ref var attack = ref attackerFilter.Get1(attacker);
                EcsEntity selectedTarget = attackerFilter.GetEntity(attacker);
                bool selected = false;

                foreach(var target in targetFilter)
                {
                    Vector3 direction = targetFilter.Get2(target).transform.position - attackerFilter.Get2(attacker).transform.position;
                    direction.y = 0f;

                    if(direction.magnitude <= attack.attackDistance)
                    {
                        selected = true;
                        selectedTarget = targetFilter.GetEntity(target);
                        break;
                    }
                }

                if (!selected) continue;

                ref var damage = ref selectedTarget.Get<ApplyDamage>();
                damage.damageAmount += attack.attackDamage;

                EcsEntity attackEntity = attackerFilter.GetEntity(attacker);
                ref var destroy = ref attackEntity.Get<DestroyEntity>();
            }
        }
    }
}
