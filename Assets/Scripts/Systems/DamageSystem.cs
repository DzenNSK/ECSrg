using Leopotam.Ecs;

namespace ECSReaction
{
    public class DamageSystem : IEcsRunSystem
    {
        private EcsFilter<ApplyDamage, CharacterHealth> filter;

        public void Run()
        {
            foreach(var entity in filter)
            {
                int damage = filter.Get1(entity).damageAmount;
                EcsEntity ecsEntity = filter.GetEntity(entity);
                ref var health = ref filter.Get2(entity);
                health.healthAmount -= damage;
                if (health.healthAmount < 0) health.healthAmount = 0;

                ecsEntity.Del<ApplyDamage>();
            }
        }
    }
}
