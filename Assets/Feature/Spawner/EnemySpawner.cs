using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [Serializable]
    private class SpawnerModel
    {
        [Min(0.1f)] public float SpawnInterval = 0.5f;
        [Min(1.0f)] public float SpawnDistance = 5.0f;
    }

    [SerializeField] private Transform _target;
    [SerializeField] private Transform _parent;
    [SerializeField] private List<EnemyBase> _enemyPrefabs = new List<EnemyBase>();
    [SerializeField] private SpawnerModel _model;

    private WaitForSeconds _waitForSpawnInterval;
    private Camera _mainCamera;

    private readonly Dictionary<IPooling, ObjectPool<EnemyBase>> _objectPools = new Dictionary<IPooling, ObjectPool<EnemyBase>>();

    private void OnValidate() => _waitForSpawnInterval = new WaitForSeconds(_model.SpawnInterval);

    private void Awake() => _waitForSpawnInterval = new WaitForSeconds(_model.SpawnInterval);

    private void Start()
    {
        _mainCamera = Camera.main;
        CreatePool();
        StartCoroutine(SpawnEnemies());
    }

    private void CreatePool()
    {
        foreach (EnemyBase enemy in _enemyPrefabs)
        {
            if (_objectPools.ContainsKey(enemy))
                return;

            ObjectPool<EnemyBase> newPool = new ObjectPool<EnemyBase>(enemy, _parent);
            _objectPools.Add(enemy, newPool);
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return _waitForSpawnInterval;

            if (_target != null)
                SpawnRandomEnemy();
        }
    }

    private void SpawnRandomEnemy()
    {
        EnemyBase selectPrefab = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Count)];  
        EnemyBase newEnemy = GetNewEnemy(selectPrefab);
        newEnemy.transform.position = GetRandomSpawnPosition();
        InitEnemy(newEnemy);
    }

    private EnemyBase GetNewEnemy(EnemyBase prefab)
    {
        EnemyBase newEnemy;
        if (_objectPools.TryGetValue(prefab, out ObjectPool<EnemyBase> selectObjectPool))
            newEnemy = selectObjectPool.GetFromPool();
        else
        {
            ObjectPool<EnemyBase> newPool = new ObjectPool<EnemyBase>(prefab, _parent);
            _objectPools.Add(prefab, newPool);
            newEnemy = newPool.GetFromPool();
        }

        newEnemy.gameObject.SetActive(true);
        return newEnemy;
    }

    private void InitEnemy(IEnemy enemy)
    {
        enemy.Resurrection();
        enemy.SetTarget(_target);
    }

    private Vector2 GetRandomSpawnPosition()
    {
        float camHeight = _mainCamera.orthographicSize;
        float camWidth = camHeight * _mainCamera.aspect;
        Vector2 targetPosition = _target.position;

        float leftEdge = targetPosition.x - camWidth;
        float rightEdge = targetPosition.x + camWidth;
        float topEdge = targetPosition.y + camHeight;
        float bottomEdge = targetPosition.y - camHeight;

        int side = Random.Range(0, 4);
        return side switch
        {
            0 => new Vector2(
                Random.Range(leftEdge - _model.SpawnDistance, rightEdge + _model.SpawnDistance),
                topEdge + _model.SpawnDistance
                ),
            1 => new Vector2(
                Random.Range(leftEdge - _model.SpawnDistance, rightEdge + _model.SpawnDistance),
                bottomEdge - _model.SpawnDistance
                ),
            2 => new Vector2(
                leftEdge - _model.SpawnDistance,
                Random.Range(bottomEdge - _model.SpawnDistance, topEdge + _model.SpawnDistance)
                ),
            3 => new Vector2(
                rightEdge + _model.SpawnDistance, Random.Range(bottomEdge - _model.SpawnDistance,
                topEdge + _model.SpawnDistance)
                ),
            _ => Vector2.zero
        };
    }

    public void SetSpawnInterval(float interval) => _model.SpawnInterval = interval;

}