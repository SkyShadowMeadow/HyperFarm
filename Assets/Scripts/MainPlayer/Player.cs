using System;
using Data;
using UnityEngine;

namespace MainPlayer
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private GameData _gameData;
        
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private Transform _wheatDetection;
        [SerializeField] private GameObject _weapon;
        [SerializeField] private WheatStack _wheatStack;

        private Camera _camera;
        private Vector3 _movementVector;
        private float _movementSpeed;
        private BoxCollider _weaponCollider;

        private void Awake()
            =>_weaponCollider = _weapon.GetComponentInChildren<BoxCollider>();

        private void Start()
        {
            _camera = Camera.main;
            _movementSpeed = _playerData.Speed;
        }
        public void Move()
        {
            _movementVector = _camera.transform.TransformDirection(_playerInput.InputAxes());
            _movementVector.y = 0;
            _movementVector.Normalize();
            transform.forward = _movementVector;
            _characterController.Move(_movementSpeed * _movementVector * Time.deltaTime);
        }

        public bool CanHarvesting()
            =>Physics.OverlapSphere(_wheatDetection.position, _playerData.CheckRadius,
                _playerData.WhatIsWheat).Length > 0;

        public bool StackIsFull()
            => _wheatStack.StackIsFull();
        
        public bool StackIsEmpty()
            => _wheatStack.IsUnloaded();
        
        public bool StackIsUnloading()
            => _wheatStack.StackIsUnloading();

        public void ShowWeapon()
            => _weapon.SetActive(true);

        public void HideWeapon()
            => _weapon.SetActive(false);
                
        //Events on animation "Harvesting"
        public void OnWheatHit()
            => _weaponCollider.enabled = true;

        public void OnWheatHitEnded()
            => _weaponCollider.enabled = false;
    }
}
