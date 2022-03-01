using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _coinCount;
        [SerializeField] private RectTransform _coinCounterUI;
        [SerializeField] private Slider Slider;

        public void AddCoins(int number)
        {
            _coinCount.text = number.ToString();
            _coinCounterUI.DOShakeAnchorPos(0.5f, new Vector2(6, 6), 10, 90f);
        }

        public void ResetCoinCount()
            => _coinCount.text = "0";

        public void UpdateBarValue(float barStep)
            => Slider.value = Slider.value + barStep;

        public void ResetBar()
            => Slider.value = 0;
        
        
    }
}