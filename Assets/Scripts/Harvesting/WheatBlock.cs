using DG.Tweening;
using MainPlayer;
using UnityEngine;

namespace Harvesting
{
    public class WheatBlock : MonoBehaviour
    {
        private Vector3 _normalScale;
        private BoxCollider _boxCollider;

        private void Awake()
        {
            _normalScale = gameObject.transform.localScale;
            _boxCollider = GetComponent<BoxCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            WheatStack wheatStack = other.GetComponentInParent<WheatStack>();
            if (wheatStack.StackIsFull()) 
                return;
            ProcessWheatBlockTaken(wheatStack);
        }

        private void ProcessWheatBlockTaken(WheatStack wheatStack)
        {
            _boxCollider.enabled = false;
            gameObject.transform.SetParent(wheatStack.transform);
            SendBlockToStack(gameObject.transform, wheatStack.GetStackPosition());
            wheatStack.PutBlockInStack(this.gameObject);
        }

        private void SendBlockToStack(Transform wheatBlock, Vector3 to)
        {
            Sequence wheatCutSequence = DOTween.Sequence();
            wheatCutSequence.Append(wheatBlock.DOMove(to, 0.5f));
            wheatCutSequence.Insert(0, wheatBlock.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBounce));
            wheatCutSequence.Insert(0, wheatBlock.DORotate(new Vector3(300, 0, 0), 0.5f).SetEase(Ease.InBounce));
        }

        public void SendBlockToBarn(Transform wheatBlockFromStack, Vector3 to)
        {
            Sequence wheatBlockSequence = DOTween.Sequence();
            wheatBlockSequence.Append(wheatBlockFromStack.DOMove(to, 0.5f));
            wheatBlockSequence.Insert(0, wheatBlockFromStack.DOScale(_normalScale, 0.25f).SetEase(Ease.InBounce));
            wheatBlockSequence.Insert(0, wheatBlockFromStack.DORotate(new Vector3(300, 0, 0), 0.5f).SetEase(Ease.InBounce));
            wheatBlockSequence.OnComplete(() =>
            {
                Destroy(wheatBlockFromStack.gameObject);
            });
        }
    }
}
