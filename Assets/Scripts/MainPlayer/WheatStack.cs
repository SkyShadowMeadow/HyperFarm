using System;
using System.Collections.Generic;
using Data;
using UnityEngine;
using DG.Tweening;
using Harvesting;
using UI;

namespace MainPlayer
{
    public class WheatStack : MonoBehaviour
    {
        [SerializeField] private GameData _gameData;
        [SerializeField] private GameObject _stackGameObject;
        
        private Vector3 offset = new Vector3(0f, 1.5f, 0.5f);
        private Player _player;
        private Stack<GameObject> _stackOfWheat;
        private bool _isFull;
        private bool _isUnloaded;
        private bool _isUnloading;
        private Sequence sequence;
        private Quaternion _idleRotation;

        public Action OnWheatBlockTaken;
        public Action OnWheatBlockSold;

        private void Awake()
        {
            _player = GetComponentInParent<Player>();
            _stackOfWheat = new Stack<GameObject>(_gameData.MaxNumberOfWheat);
            _idleRotation = _stackGameObject.transform.localRotation;
        }
        public Vector3 GetStackPosition()
            => _player.transform.position + offset;

        public void ShowStack()
        {
            _stackGameObject.SetActive(true);
        }

        public void HideStack()
        {
            _stackGameObject.SetActive(false);
        }
        public void PutBlockInStack(GameObject wheatBlock)
        {
            if(_stackOfWheat.Count == 0)
                ShowStack();
            wheatBlock.transform.SetParent(gameObject.transform);
            _stackOfWheat.Push(wheatBlock);
            _isUnloaded = false;
            if (_stackOfWheat.Count == _gameData.MaxNumberOfWheat)
                _isFull = true;
            OnWheatBlockTaken?.Invoke();
        }

        public bool StackIsFull()
            => _isFull;

        public bool StackIsUnloading()
            => _isUnloading;

        public void Sway()
        {
            sequence = DOTween.Sequence();
            sequence.Append(_stackGameObject.transform.DOShakeRotation(1f, new Vector3(0, 0, 2f), 3, 50f, false));
            sequence.SetLoops(-1, LoopType.Restart);
        }

        public void StopSwaying()
        {
            sequence.Kill();
            _stackGameObject.transform.localRotation = _idleRotation;
        }

        public void UnloadWheat(Transform barnPosition)
        {
            GameObject wheatBlock = _stackOfWheat.Pop();
            wheatBlock.GetComponent<WheatBlock>().SendBlockToBarn(wheatBlock.transform, barnPosition.position);
            _isFull = false;
            OnWheatBlockSold?.Invoke();
        }

        public void UpdateUnloadingStatus(bool status)
            => _isUnloading = status;

        public bool IsUnloaded()
        {
            if (_stackOfWheat.Count != 0)
                return false;
            else
            {
                HideStack();
                return _isUnloaded = true;
            }
        }
    }
}
