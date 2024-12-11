using Leopotam.Ecs;
using UnityEngine;

namespace ECSReaction
{
    public class CameraFollowSystem : IEcsRunSystem
    {
        private EcsFilter<CameraFollowTarget, TransformComponent> filter;
        private Vector3 currentVel;

        public void Run()
        {
            foreach (var target in filter)
            {
                ref var targetSettings = ref filter.Get1(target);
                ref var targetTransform = ref filter.Get2(target);

                Vector3 cPos = Camera.main.transform.position;
                cPos = Vector3.SmoothDamp(cPos, targetTransform.transform.position + targetSettings.cameraOffset, ref currentVel, targetSettings.smoothTime);

                Camera.main.transform.position = cPos;
                Camera.main.transform.LookAt(targetTransform.transform.position);
                break;
            }
        }
    }
}
