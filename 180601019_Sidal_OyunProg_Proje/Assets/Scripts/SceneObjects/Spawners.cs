using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;

public class Spawners : MonoBehaviour
{
    public Collectable collectable;

    public List<Collectable> spawnedList = new List<Collectable>();
    public int maxCount = 10;
    public float spawnRadius = 10;
    public float spawnPeriod = 2f;
    public bool isTrainSpawner;

    private float nextSpawnTime = 0;
    void Update()
    {
        HandleNullElements();
        if (spawnedList.Count >= maxCount)
        {
            return;
        }

        if (Time.time >= nextSpawnTime)
        {
            nextSpawnTime = Time.time + spawnPeriod;
            SpawnObject();
        }

    }

    private void SpawnObject()
    {
        var circlePos = Random.insideUnitCircle;
       
        if (isTrainSpawner)
        {
            Vector3 spawnPosition = new Vector3(2,1,0);
            spawnPosition += transform.position;
            var collectable = Instantiate(this.collectable, transform);
            collectable.transform.position = spawnPosition;
            spawnedList.Add(collectable);
        }
        else
        {
            Vector3 spawnPosition = new Vector3(circlePos.x, 0.06f, circlePos.y) * spawnRadius;
            spawnPosition += transform.position;
            var collectable = Instantiate(this.collectable, null);
            collectable.transform.position = spawnPosition;
            spawnedList.Add(collectable);
            collectable.transform.DORotate(Vector3.up * 360f, 5f, RotateMode.LocalAxisAdd).SetLoops(-1);
        }
      


    }

    private void HandleNullElements()
    {
        for (int i = spawnedList.Count - 1; i >= 0; i--)
        {
            if (spawnedList[i] == null)
            {
                spawnedList.RemoveAt(i);
            }
        }

    }
}
