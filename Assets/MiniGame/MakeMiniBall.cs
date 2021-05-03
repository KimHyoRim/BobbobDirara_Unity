using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeMiniBall : MonoBehaviour
{
    public GameObject[] BallList;
    private Queue<GameObject> qballObjects;

    private int generateCount = 8;

    private Vector3 startPosition = new Vector3(-4.5f, 1.0f, -5.0f);
    private float distanceInterval = 0.4f;


    void Start()
    {
        qballObjects = new Queue<GameObject>();
        GenerateObjects();
    }

    void Update()
    {

    }

    private void GenerateObjects()
    {
        for (int i = 0; i <generateCount; i++)
        {
            GameObject go = Instantiate(BallList[Random.Range(0, 2)]) as GameObject;
            qballObjects.Enqueue(go);
        }
        RepositionObjects();
    }

    private void RepositionObjects()
    {
        int index = 0;
        foreach(GameObject go in qballObjects)
        {
            //Debug.Log(go.name);
            go.transform.position = startPosition + Vector3.right * (distanceInterval * index++);
        }
    }
}