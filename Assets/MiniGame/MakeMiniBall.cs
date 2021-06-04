using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeMiniBall : MonoBehaviour
{
    public GameObject Ball;

    void Start()
    {
        InvokeRepeating("Spawnball", 1, 1);
    }

    void Spawnball()
    {
        float randomX = Random.Range(-23f, 19f);
        if (true)
        {
            Debug.Log("»ý¼º");
            GameObject ball = (GameObject)Instantiate(Ball, new Vector3(randomX, 1.1f, 1.0f),
                Quaternion.identity);
        }
    }
}