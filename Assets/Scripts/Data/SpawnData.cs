using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Data/SpawnData", fileName = "SpawnData", order = 40)]
    public class SpawnData : ScriptableObject
    {
        [SerializeField] private GameObject _cropPrefab;

        public GameObject CropPrefab => _cropPrefab;
    }
}
