using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class degreetime : MonoBehaviour
{

    public Image TimeBar;
     float temp;
    // Start is called before the first frame update
    void Start()
    {
          temp = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        temp += Time.deltaTime*0.000002f;

        TimeBar.fillAmount -= temp;
        
        //TimeBar.fillAmount =0되면 게임 끝

    }
}
