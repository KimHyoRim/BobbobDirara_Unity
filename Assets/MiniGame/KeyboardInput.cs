using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyboardInput : MonoBehaviour
{
    public GameObject[] ballList = new GameObject[8];
    public GameObject[] ballType = new GameObject[4];
    private int i = 0;
    private AudioSource myaudio;

    public static bool playerVisited = false;
    public static int counteridx = 0;

    public void Start()
    {
        i = 0;
        InitBall();
        myaudio = GetComponent<AudioSource>();
    }

    void Awake()
    {
    }

    private void InitBall()
    {
        ballType[0] = GameObject.Find("Up");
        ballType[1] = GameObject.Find("Down");
        ballType[2] = GameObject.Find("Left");
        ballType[3] = GameObject.Find("Right");

        for (int j = 0; j < 8; j++)
        {
            int randomObj = Random.Range(0, 3);
            ballList[j] = (GameObject)Instantiate(ballType[randomObj], new Vector3(-7.0f + 1.2f * j, 6.819498f, -11.89f),
                    Quaternion.identity);
            ballList[j].transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && ballList[i].tag == "Up")
        {
            ballList[i].SetActive(false);
            GameObject.Find("Up").GetComponent<AudioSource>().Play();
            //audio.Play();
            i += 1;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && ballList[i].tag == "Down")
        { 
            ballList[i].SetActive(false);
            GameObject.Find("Down").GetComponent<AudioSource>().Play();
            //audio.Play();
            i += 1;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && ballList[i].tag == "Left")
        {
            ballList[i].SetActive(false);
            GameObject.Find("Left").GetComponent<AudioSource>().Play(); 
            //audio.Play();
            i += 1;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && ballList[i].tag == "Right")
        {
            ballList[i].SetActive(false);
            GameObject.Find("Right").GetComponent<AudioSource>().Play();
            //audio.Play();
            i += 1;
        }

        if (i == ballList.Length)
        {
            playerVisited = true;
            //for (int j = 0; j < 8; j++)
            //    Destroy(ballList[j]);
            i = 0;
            SceneManager.LoadScene("MainStageScene");
        }
    }
}