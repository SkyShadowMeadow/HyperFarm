using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Data/GameData", fileName = "GameData", order = 40)]
    public class GameData : ScriptableObject
    {
        [SerializeField] private int _maxNumberOfWheat;
        [SerializeField] private int _wheatPrice;
        [SerializeField] private float _cropsGrowingTime;
        
        public int MaxNumberOfWheat => _maxNumberOfWheat;
        public int WheatPrice => _wheatPrice;
        public float CropsGrowingTime => _cropsGrowingTime;
    }
}