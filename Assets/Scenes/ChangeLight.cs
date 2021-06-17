using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLight : MonoBehaviour
{
    private Light thelight;

    private float currentintensity;


    // Start is called before the first frame update
    void Start()
    {
        thelight = GetComponent<Light>();
        currentintensity = thelight.intensity;

    }

    // Update is called once per frame
    void Update()
    {
        currentintensity -= Time.deltaTime * 0.01f;
        thelight.intensity = currentintensity;

    }
}