using System;
using System.Collections;
using Data;
using DG.Tweening;
using EzySlice;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Harvesting
{
    public class Wheat : MonoBehaviour
    {
        [SerializeField] private GameObject _fullGrownWheat;
        [SerializeField] private GameObject _midGrownWheat;
        [SerializeField] private GameObject _lowGrownWheat;
        
        [SerializeField] private GameObject _wheatBlockPrefab;
        [SerializeField] private Material _cutMaterial;
        [SerializeField] private ParticleSystem _wheatCutVFX;
        [SerializeField] private GameData _gameData;
        
        private int _currentHit = 0;
        private BoxCollider _wheatCollider;
        private WaitForSeconds _timeToGrowOnePart;
        private void Awake()
            => _wheatCollider = GetComponent<BoxCollider>();

        private void Start()
            => _timeToGrowOnePart = new WaitForSeconds(_gameData.CropsGrowingTime / 3f);

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.GetComponent<SlicePlaine>() != null)
            {
                if (_currentHit == 0)
                {
                    CutWheat(other, _fullGrownWheat);
                    _currentHit++;
                }
                else if (_currentHit == 1)
                {
                    CutWheat(other, _midGrownWheat);
                    _currentHit++;
                }
                else
                {
                    CutWheat(other, _lowGrownWheat);
                    _currentHit = 0;
                    _wheatCollider.enabled = false;
                    GrowWheat();
                }
            }
        }

        private void CutWheat(Collider other, GameObject partOfWheat)
        {
            SliceUpperPart(other, partOfWheat);
            partOfWheat.SetActive(false);
            _wheatCutVFX.Play();
            InstantiateWheatBlock();
        }
        private void SliceUpperPart(Collider other, GameObject partOfWheat)
        {
            SlicePlaine slicePlaine = other.GetComponent<SlicePlaine>();
                SlicedHull slicedHull = slicePlaine.SliceObject(partOfWheat, _cutMaterial);
                if (slicedHull != null)
                {
                    GameObject wheatCut = slicedHull.CreateUpperHull(partOfWheat.gameObject, _cutMaterial);
                    LaunchWheatPart(wheatCut.transform);
                }
            
        }
        private void LaunchWheatPart(Transform wheatCut)
        {
            wheatCut.position = gameObject.transform.position;
            Sequence wheatCutSequence = DOTween.Sequence();
            wheatCutSequence.Append(wheatCut.DOMove(gameObject.transform.position + new Vector3(-1.5f, 2f, 0), 0.5f));
            wheatCutSequence.Insert(0, wheatCut.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBounce));
            wheatCutSequence.OnComplete(() =>
            {
                Destroy(wheatCut.gameObject);
            });
        }
        private void InstantiateWheatBlock()
        {
            float randomXZposition = Random.Range(0.1f, 1.1f);
            Vector3 wheatBlockPosition = 
                _fullGrownWheat.transform.position + new Vector3(randomXZposition, 3, randomXZposition);
            GameObject wheatBlock = Instantiate(_wheatBlockPrefab, wheatBlockPosition, Quaternion.identity);
            LaunchWheatBlock(wheatBlock.transform);
        }
        
        private void LaunchWheatBlock(Transform wheatBlock)
        {
            Sequence wheatCutSequence = DOTween.Sequence();
            Vector3 newPosition = new Vector3(wheatBlock.position.x - 0.1f, 0.15f, wheatBlock.position.z + 0.2f);
            wheatCutSequence.Append(wheatBlock.DOMove(newPosition, 0.5f));
            wheatCutSequence.Insert(0, wheatBlock.DORotate(new Vector3(200, 0, 0), 0.5f).SetEase(Ease.InBounce));
        }
        private void GrowWheat()
            => StartCoroutine(GrowWheatByPart());

        IEnumerator GrowWheatByPart()
        {
            yield return _timeToGrowOnePart;
            _lowGrownWheat.SetActive(true);
            
            yield return _timeToGrowOnePart;
            _midGrownWheat.SetActive(true);
            
            yield return _timeToGrowOnePart;
            _fullGrownWheat.SetActive(true);
            _wheatCollider.enabled = true;
        }
    }
}
