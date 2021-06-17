using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLight : MonoBehaviour
{
    private Transform thelight;

   


    // Start is called before the first frame update
    void Start()
    {
        thelight = GetComponent<Transform>();
        

    }

    // Update is called once per frame
    void Update()
    {
        thelight.Rotate(Vector3.right * Time.deltaTime*1.5f);
         
       
 
    }
}