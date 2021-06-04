using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyboardInput : MonoBehaviour
{
    public GameObject[] ballList = new GameObject[8];
    public GameObject[] BallType = new GameObject[4];
    private int i = 0;
    private AudioSource audio;

    public static bool playerVisited = false;

    public void Start()
    {
        i = 0;
        Debug.Log(ballList.Length);
        InitBall();
        audio = GetComponent<AudioSource>();
    }

    void Awake()
    {
       
    }

    void InitBall()
    {
        for (int j = 0; j < 8; j++)
        {
            int randomObj = Random.Range(0, 3);
            ballList[j] = (GameObject)Instantiate(BallType[randomObj], new Vector3(-7.0f + 1.2f * j, 6.819498f, -11.89f),
                    Quaternion.identity);
            ballList[j].transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && ballList[i].tag == "Up")
        {
            ballList[i].SetActive(false);
            i += 1;
            GameObject.Find("Up").GetComponent<AudioSource>().Play();
            //audio.Play();
            Debug.Log(i);
            Debug.Log("Up");
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && ballList[i].tag == "Down")
        {
            Debug.Log("Down");
            ballList[i].SetActive(false);
            GameObject.Find("Down").GetComponent<AudioSource>().Play();
            //audio.Play();
            i += 1;
            Debug.Log(i);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && ballList[i].tag == "Left")
        {
            Debug.Log("Left");
            ballList[i].SetActive(false);
            GameObject.Find("Left").GetComponent<AudioSource>().Play(); 
            //audio.Play();
            i += 1;
            Debug.Log(i);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && ballList[i].tag == "Right")
        {
            Debug.Log("Right");
            ballList[i].SetActive(false);
            GameObject.Find("Right").GetComponent<AudioSource>().Play();
            //audio.Play();
            i += 1;
            Debug.Log(i);
        }

        if (i == ballList.Length)
        {
            playerVisited = true;
            SceneManager.LoadScene("MainStageScene");
        }
    }
}