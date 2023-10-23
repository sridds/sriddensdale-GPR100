using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    [SerializeField] private Collectable collectablePrefab;
    [SerializeField] private Vector2 range;
    [SerializeField] private int spawnAmount;

    private void Start()
    {
        // spawn all collectables
        for(int i = 0; i <= spawnAmount; i++)
        {
            Collectable c = Instantiate(collectablePrefab);
            c.transform.position = SetRandomPosition();
        }

        FindObjectOfType<GameManager>().RegisterCollectables();
    }

    private Vector2 SetRandomPosition()
    {
        bool isValid = false;
        // spawns around the player so the player doesnt immedietly collect

        Vector2 spawnPos;
        do
        {
            spawnPos = new Vector2(Random.Range(-range.x, range.x), Random.Range(-range.y, range.y));

            if(Vector2.Distance(spawnPos, transform.position) > 4.0f)
            {
                isValid = true;
            }
        } while (!isValid);

        return spawnPos;
    }
}
