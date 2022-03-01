using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Data/Player", fileName = "Player", order = 40)]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _checkRadius;
        [SerializeField] private LayerMask _whatIsWheat;

        public float Speed => _speed;
        public float CheckRadius => _checkRadius;
        public LayerMask WhatIsWheat => _whatIsWheat;
    }
}