using Leopotam.Ecs;
using UnityEngine;

namespace ECSReaction
{
    public class EntityView : MonoBehaviour
    {
        private bool initialised;
        private EcsEntity entity;

        public ref EcsEntity GetEntity()
        {
            if (!initialised)
            {
                InitEntity();
            }
            return ref entity;
        }

        private void InitEntity()
        {
            Debug.Log("Init " + name);
            EcsWorld world = SceneWorldController.Instance.GetSceneWorld();
            if (world != null)
            {
                entity = world.NewEntity();
                initialised = true;
            }
            else
            {
                Debug.LogError($"[{nameof(EntityView)}] World acquiring error");
            }

            ref TransformComponent transformComponent = ref entity.Get<TransformComponent>();
            transformComponent.transform = transform;
        }
    }
}
