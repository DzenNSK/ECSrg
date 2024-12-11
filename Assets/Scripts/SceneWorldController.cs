using Leopotam.Ecs;
using UnityEngine;

namespace ECSReaction
{
    //В идеале вынести в сервисную модель или zenject, но в рамках тестовой задачи просто квазисинглтон
    public class SceneWorldController : MonoBehaviour
    {
        [SerializeField] private ConfigData configData;

        private EcsWorld sceneWorld;
        private EcsSystems frameSystems;
        private EcsSystems fixedSystems;

        public static SceneWorldController Instance { get; private set; }

        public EcsWorld GetSceneWorld()
        {
            if (sceneWorld == null) InitSceneWorld();
            return sceneWorld;
        }

        private void InitSceneWorld()
        {
            sceneWorld = new EcsWorld();
            frameSystems = new EcsSystems(sceneWorld);
            fixedSystems = new EcsSystems(sceneWorld);

            frameSystems
                .Add(new PlayerMoveSystem())
                .Add(new MoveToTransformSystem())
                .Add(new CameraFollowSystem())
                .Add(new HunterBehaviourSystem())
                .Add(new PatrolBehaviourSystem())
                .Add(new ShootingBehaviourSystem())
                .Add(new WeaponControlSystem())
                .Add(new KamikazeAttackSystem())
                .Add(new DamageSystem())
                .Add(new DestroyEntitySystem())
                .Inject(configData)
                .Init();

            fixedSystems
                .Add(new BulletSystem())
                .Init();
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Debug.LogWarning($"[{nameof(SceneWorldController)}] Not single instance, destroying");
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            if(sceneWorld == null) InitSceneWorld();
        }

        private void Update()
        {
            frameSystems?.Run();
        }

        private void FixedUpdate()
        {
            fixedSystems?.Run();
        }
    }
}