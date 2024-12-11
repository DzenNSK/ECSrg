using Leopotam.Ecs;
using UnityEngine;

namespace ECSReaction
{
    public class HunterBehaviourSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerControlledMoving, CharacterData> targetFilter;
        private EcsFilter<HunterBehaviour, CharacterData> huntersFilter;

        public void Run()
        {
            foreach(var entity in huntersFilter)
            {
                ref var huntData = ref huntersFilter.Get1(entity);
                ref var hunterCharData = ref huntersFilter.Get2(entity);
                Transform selectedTarget = null;

                foreach(var target in targetFilter)
                {
                    ref var targetData = ref targetFilter.Get2(target);
                    if ((targetData.controller.transform.position - hunterCharData.controller.transform.position).magnitude > huntData.huntDistance)
                        continue;

                    selectedTarget = targetData.controller.transform;
                }

                if (selectedTarget == null) continue;

                EcsEntity ecsEntity = huntersFilter.GetEntity(entity);
                ref var moveCommand = ref ecsEntity.Get<MoveToTransform>();
                moveCommand.target = selectedTarget;
            }
        }
    }
}
