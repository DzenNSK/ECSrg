using Leopotam.Ecs;
using UnityEngine;

namespace ECSReaction
{
    [RequireComponent(typeof(EntityView))]
    public class PatrolView : MonoBehaviour, IWaypointResolver
    {
        [SerializeField] private CharacterController controller;
        [SerializeField] private float movementSpeed;
        [SerializeField] private float distanceTolerance;
        [SerializeField] private Transform[] waypoints;
        private int currentWP;

        public Transform GetNextWaypoint()
        {
            int counter = 0;
            while (counter < waypoints.Length)
            {
                counter++;
                Transform wp = waypoints[currentWP];
                Vector3 direction = wp.position - transform.position;
                direction.y = 0;
                if(direction.magnitude > distanceTolerance)
                {
                    break;
                }
                currentWP++;
                if (currentWP > waypoints.Length - 1) currentWP = 0;
            }

            return waypoints[currentWP];
        }

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

            ref var patrol = ref entity.Get<PatrolBehaviour>();
            patrol.resolver = this;
            patrol.distanceTolerance = distanceTolerance;
        }
    }
}
