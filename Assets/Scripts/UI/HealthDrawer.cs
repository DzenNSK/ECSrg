using Leopotam.Ecs;
using TMPro;
using UnityEngine;

namespace ECSReaction
{
    public class HealthDrawer : MonoBehaviour
    {
        [SerializeField] private TMP_Text label;
        [SerializeField] private EntityView target;

        private void Update()
        {
            EcsEntity targetEntity = target.GetEntity();
            ref var health = ref targetEntity.Get<CharacterHealth>();
            label.text = $"HEALTH {health.healthAmount}";
        }
    }
}
