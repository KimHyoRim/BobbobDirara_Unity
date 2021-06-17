using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class degreehungry : MonoBehaviour
{
    public Image hungryBar;
    float temp;
    // Start is called before the first frame update
    void Start()
    {
        temp = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
         if (hungryBar.rectTransform.localScale.x < 0.0f)
                {
                    temp += Time.deltaTime*0.1f;
                    
                    hungryBar.rectTransform.localScale = new Vector3(-0.2f + temp / 10.0f,0.1875f,0.1875f);
                }
                else
                {
                    hungryBar.rectTransform.localScale = new Vector3(0.0f, 0.1875f, 0.1875f);
                }
        
    }
}

