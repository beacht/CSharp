using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject meleeEnemy;
    [SerializeField]
    private GameObject rangedEnemy;

    IEnumerator SpawnRoutine()
    {
        while(true)
        {
            Vector3 spawnHere = new Vector3(Random.Range(-8f, 8f), 7, 0);
            Instantiate(meleeEnemy, spawnHere, Quaternion.identity);
            Instantiate(rangedEnemy, spawnHere, Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
