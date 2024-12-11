using Leopotam.Ecs;
using UnityEngine;

namespace ECSReaction
{
    [RequireComponent(typeof(EntityView))]
    public class ShooterView : MonoBehaviour
    {
        [SerializeField] private CharacterController controller;
        [SerializeField] private float shootingRange;
        [SerializeField] private ShootingWeapon weapon;

        private void Start ()
        {
            EntityView entityView = GetComponent<EntityView>();
            if (entityView == null)
            {
                Debug.LogError($"[{nameof(PlayerControlledView)}] No EntityView component");
                return;
            }

            EcsEntity entity = entityView.GetEntity();

            ref var behaviourData = ref entity.Get<ShooterBehaviour>();
            behaviourData.fireRange = shootingRange;

            ref var characterData = ref entity.Get<CharacterData>();
            characterData.controller = controller;

            entity.Replace<ShootingWeapon>(weapon);
        }
    }
}
