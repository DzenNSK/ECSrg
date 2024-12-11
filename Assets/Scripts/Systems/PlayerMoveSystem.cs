using Leopotam.Ecs;
using UnityEngine;

namespace ECSReaction
{
    public class PlayerMoveSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerControlledMoving, CharacterData> filter;

        public void Run()
        {
            foreach (var entity in filter)
            {
                ref var pcMoving = ref filter.Get1(entity);
                ref var charData = ref filter.Get2(entity);

                Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
                Vector3 movement = direction.normalized * charData.movementSpeed + Physics.gravity;
                charData.controller.Move(movement * Time.deltaTime);

                Plane playerPlane = new Plane(Vector3.up, charData.controller.transform.position);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (playerPlane.Raycast(ray, out var hitDistance)) charData.controller.transform.forward = ray.GetPoint(hitDistance) - charData.controller.transform.position;

                pcMoving.pcAnimator.SetFloat("Vertical", Vector3.Dot(direction.normalized, charData.controller.transform.forward), 0.1f, Time.deltaTime);
                pcMoving.pcAnimator.SetFloat("Horizontal", Vector3.Dot(direction.normalized, charData.controller.transform.right), 0.1f, Time.deltaTime);
            }
        }
    }
}
