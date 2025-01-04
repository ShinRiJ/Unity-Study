using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using System.Drawing;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<String> _tanksTags;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private Single _spawnTime = 4f;

    [SerializeField] private HealthUp _medKitPrefab;
    private HealthUp _firstAid;

    [SerializeField] private ReloadReduceTime _reloadReducedPrefab;
    private static ReloadReduceTime _reloadReduceKit;

    public Texture2D cursorTexture;

    private PointF _leftUpCorner = new PointF(-8.384f, 4.477f);
    private PointF _leftDownCorner = new PointF(-8.384f, -4.51f);
    private PointF _rightUpCorner = new PointF(8.3f, 4.477f);
    private PointF _rightDownCorner = new PointF(8.3f, -4.51f);

    private ObjectPooler _pooler;

    private void Start()
    {

        _pooler = ObjectPooler.Instance;
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        StartCoroutine(SpawnTankCarotine());
        _firstAid = Instantiate(_medKitPrefab, RandomCoordInField(), Quaternion.identity);
        _firstAid.HealthUpUsed += HealthUpReInit;

        _reloadReduceKit = Instantiate(_reloadReducedPrefab, RandomCoordInField(), Quaternion.identity);
        _reloadReduceKit.ReloadReduceUsed += ReloadReduceReInit;
        Stats.LvLChanged += ReloadReduceActivate;
    }

    private void ReloadReduceActivate()
    {
        if(_reloadReduceKit == null)
        {
            _reloadReduceKit = Instantiate(_reloadReducedPrefab, RandomCoordInField(), Quaternion.identity);
            _reloadReduceKit.ReloadReduceUsed += ReloadReduceReInit;
            Stats.LvLChanged += ReloadReduceActivate;
        }    
        _reloadReduceKit?.gameObject.SetActive(true);
    }

    private void ReloadReduceReInit()
    {
        _reloadReduceKit.gameObject.SetActive(false);
        _reloadReduceKit.transform.position = RandomCoordInField();
    }
    
    private void HealthUpReInit()
    {
        _firstAid.transform.position = RandomCoordInField();
    }

    private IEnumerator SpawnTankCarotine()
    {
        while(true)
        {
            String tag = _tanksTags[UnityEngine.Random.Range(0, (Stats.Level > _tanksTags.Count) ? _tanksTags.Count : Stats.Level)];
            Vector3 position = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Count)].position;
            GameObject spawnObj = _pooler.SpawnFromPool(
                                        tag,
                                        position,
                                        Quaternion.identity
                                        );
            spawnObj.gameObject.GetComponent<Tank>().RestoreMaxHealth();
            yield return new WaitForSeconds((_spawnTime - Stats.Level / 4) > 0.1 ? (_spawnTime - Stats.Level / 4) : 0.1f);
        }
    }

    public Vector2 RandomCoordInField() => new Vector2(
        UnityEngine.Random.Range(_leftUpCorner.X, _rightUpCorner.X),
        UnityEngine.Random.Range(_leftUpCorner.Y, _leftDownCorner.Y)
    );
}
