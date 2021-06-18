using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class degreetime : MonoBehaviour
{
    public bool gameover = false;
    public GameObject ResultPanel;
    public Image TimeBar;
     float temp;
     
     public GameObject player;
     public GameObject Guest;
    // Start is called before the first frame update
    void Start()
    {
          temp = 0.0f;
          ResultPanel= GameObject.Find("ResultPanel");
    }

    // Update is called once per frame
    void Update()
    {
        temp += Time.deltaTime*0.000002f;
        //temp += Time.deltaTime*0.5f;
        TimeBar.fillAmount -= temp;
        
        //TimeBar.fillAmount =0되면 게임 끝
        if (TimeBar.fillAmount <= 0.0f)
        {
            gameover = true;
            ResultPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

        }
    }
}
