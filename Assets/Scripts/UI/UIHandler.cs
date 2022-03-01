using System;
using Data;
using MainPlayer;
using UnityEngine;

namespace UI
{
    public class UIHandler : MonoBehaviour
    {
        [SerializeField] private Barn.Barn _barn;
        [SerializeField] private WheatStack _wheatStack;
        [SerializeField] private UIView _uiView;
        [SerializeField] private GameData _gameData;

        private float _barStep;

        private void Start()
        {
            _uiView.ResetCoinCount();
            _uiView.ResetBar();
            _barStep = 1f / (float)_gameData.MaxNumberOfWheat;
        }

        private void OnEnable()
        {
            _barn.OnWheatSold += IncreaseCoins;
            _wheatStack.OnWheatBlockTaken += IncreaseBar;
            _wheatStack.OnWheatBlockSold += DecreaseBar;
        }

        private void DecreaseBar()
            => _uiView.UpdateBarValue( -_barStep);

        private void IncreaseBar()
        {
            _uiView.UpdateBarValue(_barStep);
        }

        private void IncreaseCoins(int coinsNumber)
        {
            _uiView.AddCoins(coinsNumber);
        }
    }
}
