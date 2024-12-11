using Leopotam.Ecs;
using UnityEngine;

namespace ECSReaction
{
    [RequireComponent(typeof(EntityView))]
    public class KamikazeView : MonoBehaviour
    {
        [SerializeField] private float attackDistance;
        [SerializeField] private int attackDamage;

        private void Start()
        {
            EntityView entityView = GetComponent<EntityView>();
            if (entityView == null)
            {
                Debug.LogError($"[{nameof(PlayerControlledView)}] No EntityView component");
                return;
            }

            EcsEntity entity = entityView.GetEntity();

            ref var attackData = ref entity.Get<KamikazeAttack>();
            attackData.attackDistance = attackDistance;
            attackData.attackDamage = attackDamage;
        }
    }
}
