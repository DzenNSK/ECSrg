using Leopotam.Ecs;
using UnityEngine;

namespace ECSReaction
{
    public class DestroyEntitySystem : IEcsRunSystem
    {
        private EcsFilter<DestroyEntity, TransformComponent> filter;
        public void Run()
        {
            foreach(var entity in filter)
            {
                GameObject go = filter.Get2(entity).transform.gameObject;
                EcsEntity ecsEntity = filter.GetEntity(entity);

                GameObject.Destroy(go);
                ecsEntity.Destroy();
            }
        }
    }
}
