using UnityEngine;

namespace ECSReaction
{
    [CreateAssetMenu(fileName = "GameConfigData", menuName = "ECSReaction/Game config")]
    public class ConfigData : ScriptableObject
    {
        public GameObject bulletPrefab;
    }
}
