using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField]
    public Transform cam;
    
    // Start is called before the first frame update
    void Start()
    {
        cam=Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(transform.position + cam.rotation * Vector3.forward, cam.rotation * Vector3.up);
    }
}
