using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]GameObject enemy;
    [SerializeField][Range(0,50)] int poolSize = 5;
    float currTime;
    EnemyMovement path;

    GameObject[] pool;

    
    [Tooltip("Time between spawning the crates")]
    [SerializeField]
    [Range(0.1f, 30f)]
    float maxTime = 1f;

    private void Awake()
    {
        PopulatePool();
    }


    private void Start()
    {
        currTime = 0;
    }

    void FixedUpdate()
    {
        if(currTime>maxTime)
        {
            EnableObjectInPool();
            currTime = 0;
        }
        else
        {
            currTime += Time.deltaTime;
            //Debug.Log(currTime);
        }
    }
    void EnableObjectInPool()
    {
        for(int i =0; i <pool.Length; i++)
        {
            if (pool[i].activeInHierarchy == false)
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for(int i = 0; i<pool.Length; i++)
        {
            pool[i] = Instantiate(enemy, transform);
            pool[i].SetActive(false); 
            
        }
    }
}
