using Leopotam.Ecs;
using UnityEngine;

namespace ECSReaction
{
    public class MoveToTransformSystem : IEcsRunSystem
    {
        private EcsFilter<MoveToTransform, CharacterData> filter;

        public void Run()
        {
            foreach(var entity in filter)
            {
                ref var moveData = ref filter.Get1(entity);
                ref var charData = ref filter.Get2(entity);

                Vector3 direction = moveData.target.position - charData.controller.transform.position;
                direction.y = 0f;
                if (direction.magnitude <= moveData.distanceTolerance)
                {
                    EcsEntity ecsEntity = filter.GetEntity(entity);
                    ecsEntity.Del<MoveToTransform>();
                    continue;
                }

                direction = direction.normalized * charData.movementSpeed + Physics.gravity;
                charData.controller.Move(direction * Time.deltaTime);
            }
        }
    }
}
