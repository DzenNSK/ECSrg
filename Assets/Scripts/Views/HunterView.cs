using Leopotam.Ecs;
using UnityEngine;

namespace ECSReaction
{
    [RequireComponent(typeof(EntityView))]
    public class HunterView : MonoBehaviour
    {
        [SerializeField] private CharacterController controller;
        [SerializeField] private float movementSpeed;
        [SerializeField] private float huntDistance;

        private void Start()
        {
            EntityView entityView = GetComponent<EntityView>();
            if (entityView == null)
            {
                Debug.LogError($"[{nameof(PlayerControlledView)}] No EntityView component");
                return;
            }

            EcsEntity entity = entityView.GetEntity();

            ref var charData = ref entity.Get<CharacterData>();
            charData.movementSpeed = movementSpeed;
            charData.controller = controller;

            ref var huntData = ref entity.Get<HunterBehaviour>();
            huntData.huntDistance = huntDistance;
        }
    }
}
