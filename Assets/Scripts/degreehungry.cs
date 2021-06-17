using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class degreehungry : MonoBehaviour
{
    public Image hungryBar;
    public float temp;
    public bool Add = false;

    // Start is called before the first frame update
    void Start()
    { 
        temp = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Add == true && hungryBar.rectTransform.localScale.x < 0.0f)
        {
            Debug.Log("줄어든다");
            temp += Time.deltaTime * 0.1f;

            hungryBar.rectTransform.localScale = new Vector3(-0.2f + temp / 10.0f, 0.1875f, 0.1875f);
        }

        else if (Add == false)
        {
            Debug.Log("스탑");
            temp = 0.0f;
            hungryBar.rectTransform.localScale = new Vector3(-0.2f, 0.1875f, 0.1875f);
        }

        else
        {
            Debug.Log("끝");
            hungryBar.rectTransform.localScale = new Vector3(0.0f, 0.1875f, 0.1875f);
        }
        
    }
}

