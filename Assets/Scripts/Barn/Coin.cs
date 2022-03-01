using DG.Tweening;
using UnityEngine;

namespace Barn
{
    public class Coin : MonoBehaviour
    {
        private Vector3 _startPosition;
        private Vector3 _startScale;
        private Vector3 _launchOffset = new Vector3(3, 8, 15);

        private void Start()
        {
            _startPosition = gameObject.transform.position;
            _startScale = gameObject.transform.localScale;
        }

        public void LaunchCoin(GameObject currentCoin, global::Barn.Barn barn, int poolIndex)
        {
            gameObject.SetActive(true);
            Sequence coinSequence = DOTween.Sequence();
            coinSequence.Append(currentCoin.transform.DOMove(_startPosition + _launchOffset, 1f));
            coinSequence.Insert(0, currentCoin.transform.DOScale(Vector3.zero, 1f).SetEase(Ease.InBounce));
            coinSequence.Insert(0, currentCoin.transform.DORotate(new Vector3(300, 0, 0), 0.5f).SetEase(Ease.InBounce));
            coinSequence.OnComplete(() =>
            {
                barn.ReturnToPool(poolIndex);
                gameObject.transform.position = _startPosition;
                gameObject.transform.localScale = _startScale;
            });
            
        }

    }
}