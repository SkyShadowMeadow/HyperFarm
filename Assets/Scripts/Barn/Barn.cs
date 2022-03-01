using System;
using System.Collections;
using Data;
using MainPlayer;
using UnityEngine;

namespace Barn
{
    public class Barn : MonoBehaviour
    {
        [SerializeField] private Transform _barnFacade;
        [SerializeField] private Coin _coinPrefab;
        [SerializeField] private GameData _gameData;
        
        private Coin[] _coinPool;
        private int _counter = 0;
        private int _allCoins = 0;

        public Action<int> OnWheatSold;
        private void Start()
        {
            _coinPool = new Coin[_gameData.MaxNumberOfWheat];
            CreateCoins();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<WheatStack>().StackIsFull())
            {
                WheatStack wheatStack = other.GetComponent<WheatStack>();
                StartCoroutine(UnloadStack(wheatStack));
            }
        }

        public void ReturnToPool(int poolIndex)
        {
            _coinPool[poolIndex].gameObject.transform.position = gameObject.transform.position;
            _coinPool[poolIndex].gameObject.SetActive(false);
            _allCoins += _gameData.WheatPrice;
            OnWheatSold?.Invoke(_allCoins);
        }
        private IEnumerator UnloadStack(WheatStack wheatStack)
        {
            while (!wheatStack.IsUnloaded())
            {
                wheatStack.UpdateUnloadingStatus(true);
                wheatStack.UnloadWheat(_barnFacade);
                yield return new WaitForSeconds(0.5f);
                GiveReward();
            }
            wheatStack.UpdateUnloadingStatus(false);
            _counter = 0;
        }

        private void GiveReward()
        {
            Coin currentCoin = _coinPool[_counter];
            currentCoin.LaunchCoin(currentCoin.gameObject, this, _counter);
            _counter++;
        }
        
        private void CreateCoins()
        {
            for (int i = 0; i < _gameData.MaxNumberOfWheat; i++)
            {
                Coin coin = Instantiate(_coinPrefab, gameObject.transform.position, Quaternion.identity, gameObject.transform);
                coin.gameObject.SetActive(false);
                _coinPool[i] = coin;
            }
        }

    }
}
