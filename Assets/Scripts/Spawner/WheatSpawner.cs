using System.Collections;
using Data;
using Unity.Mathematics;
using UnityEngine;

namespace Spawner
{
    public class WheatSpawner : MonoBehaviour
    {
        [SerializeField] private SpawnData _spawnData;
        [SerializeField] private Transform _startSpawnPoint;
        private Vector3 _sizeOfTheGarden;
        private float _stepX;
        private float _stepZ;
        private int _numberOfCropsX;
        private int _numberOfCropsZ;

        private void Awake()
        {
            _sizeOfTheGarden = gameObject.GetComponent<MeshRenderer>().bounds.size;
            CountNumberToSpawn();
            CountSpawnSteps();
        }
        void Start()
            => StartCoroutine(SpawnCrops());

        private IEnumerator SpawnCrops()
        {
            int countX = 0;
            int countZ = 0;
            Vector3 currentPositionZ = _startSpawnPoint.position;
            Vector3 currentPositionX = _startSpawnPoint.position;
            while (countZ < _numberOfCropsZ)
            {
                while (countX < _numberOfCropsX)
                {
                    Instantiate(_spawnData.CropPrefab, currentPositionX, quaternion.identity);
                    currentPositionX += new Vector3(_stepX, 0, 0);
                    countX++;
                    yield return null;
                }

                countX = 0;
                countZ++;
                currentPositionZ = currentPositionZ + new Vector3(0, 0, _stepZ);
                currentPositionX = currentPositionZ;
                yield return null;
            }
        }

        private void CountSpawnSteps()
        {
            _stepX = _sizeOfTheGarden.x / _numberOfCropsX;
            _stepZ = _sizeOfTheGarden.z / _numberOfCropsZ;
        }

        private void CountNumberToSpawn()
        {
            _numberOfCropsX = (int) (_sizeOfTheGarden.x / _spawnData.CropPrefab.gameObject.GetComponent<BoxCollider>().size.x);
            _numberOfCropsZ = (int) (_sizeOfTheGarden.z / _spawnData.CropPrefab.gameObject.GetComponent<BoxCollider>().size.z);
        }

     
    }
}
