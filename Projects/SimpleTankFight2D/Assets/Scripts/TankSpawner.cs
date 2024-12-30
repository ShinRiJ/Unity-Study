using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;

public class TankSpawner : MonoBehaviour
{
    [SerializeField] private List<Tank> _tanks;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private Single _spawnTime = 4f;
    public Texture2D cursorTexture;

    private void Start()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        StartCoroutine(SpawnTankCarotine());
    }

    private IEnumerator SpawnTankCarotine()
    {
        while(true)
        {
            Instantiate(
                _tanks[UnityEngine.Random.Range(0, (Stats.Level > _tanks.Count) ? _tanks.Count : Stats.Level)],
                _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Count)].position,
                Quaternion.identity);
            yield return new WaitForSeconds(_spawnTime - Stats.Level / 4);
        }
    }

}
