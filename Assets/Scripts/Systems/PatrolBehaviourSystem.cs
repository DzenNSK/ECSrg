using Leopotam.Ecs;
using UnityEngine;

namespace ECSReaction
{
    public class PatrolBehaviourSystem : IEcsRunSystem
    {
        private EcsFilter<PatrolBehaviour>.Exclude<MoveToTransform> filter;

        public void Run()
        {
            foreach(var entity in filter)
            {
                ref var patrol = ref filter.Get1(entity);
                if (patrol.resolver == null) continue;

                Transform waypoint = patrol.resolver.GetNextWaypoint();
                if (waypoint == null) continue;

                EcsEntity ecsEntity = filter.GetEntity(entity);
                ref var move = ref ecsEntity.Get<MoveToTransform>();
                move.target = waypoint;
                move.distanceTolerance = patrol.distanceTolerance;
            }
        }
    }
}
