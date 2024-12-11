using Leopotam.Ecs;
using UnityEngine;

namespace ECSReaction
{
    [RequireComponent(typeof(EntityView))]
    public class PlayerControlledView : MonoBehaviour
    {
        [SerializeField] private CharacterController pcController;
        [SerializeField] private Animator pcAnimator;
        [SerializeField] private float velocity;
        [SerializeField] private int characterHealth;
        [SerializeField] Vector3 cameraOffset;
        [SerializeField] float cameraSmoothTime;

        public int MaxHealth => characterHealth;

        private void Start()
        {
            if(pcController == null)
            {
                Debug.LogError($"[{nameof(PlayerControlledView)}] No PlayerCharacter");
            }

            EntityView entityView = GetComponent<EntityView>();
            if (entityView == null) 
            {
                Debug.LogError($"[{nameof(PlayerControlledView)}] No EntityView component");
                return;
            }

            EcsEntity entity = entityView.GetEntity();

            ref CharacterData charData = ref entity.Get<CharacterData>();
            charData.controller = pcController;
            charData.movementSpeed = velocity;

            ref CharacterHealth health = ref entity.Get<CharacterHealth>();
            health.healthAmount = characterHealth;

            ref PlayerControlledMoving pcMoving = ref entity.Get<PlayerControlledMoving>();
            pcMoving.pcAnimator = pcAnimator;

            ref CameraFollowTarget follow = ref entity.Get<CameraFollowTarget>();
            follow.cameraOffset = cameraOffset;
            follow.smoothTime = cameraSmoothTime;
        }
    }
}
