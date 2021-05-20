using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyboardInput : MonoBehaviour
{
    public GameObject[] ballList = new GameObject[8];
    private int i = 0;
    AudioSource audio;

    public void Start()
    {
        i = 0;
        Debug.Log(ballList.Length);
    }

    void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && ballList[i].tag == "Up")
        {
            ballList[i].SetActive(false);
            i += 1;
            audio.Play();
            Debug.Log(i);
            Debug.Log("Up");
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && ballList[i].tag == "Down")
        {
            Debug.Log("Down");
            ballList[i].SetActive(false);
            audio.Play();
            i += 1;
            Debug.Log(i);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && ballList[i].tag == "Left")
        {
            Debug.Log("Left");
            ballList[i].SetActive(false);
            audio.Play();
            i += 1;
            Debug.Log(i);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && ballList[i].tag == "Right")
        {
            Debug.Log("Right");
            ballList[i].SetActive(false);
            audio.Play();
            i += 1;
            Debug.Log(i);
        }

        if (i == ballList.Length)
        {
            SceneManager.LoadScene("MainStageScene");
        }
    }
}