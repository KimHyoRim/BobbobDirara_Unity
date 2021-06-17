using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyboardInput : MonoBehaviour
{
    List<GameObject> ballList = new List<GameObject>();
    public GameObject[] ballType = new GameObject[4];
    private AudioSource myaudio;

    public int i = 0;

    public static bool playerVisited = false;
    public static int counteridx = 0;
    public static bool isCorrected = false;

    public void Start()
    {
        ballType[0] = GameObject.Find("Up");
        ballType[1] = GameObject.Find("Down");
        ballType[2] = GameObject.Find("Left");
        ballType[3] = GameObject.Find("Right");

        myaudio = GetComponent<AudioSource>();
    }

    void Awake()
    {

    }

    private void InitBall()
    {
        for (int j = 0; j < 8; j++)
        {
            int randomObj = Random.Range(0, 3);
            GameObject myball = (GameObject)Instantiate(ballType[randomObj], new Vector3(-7.0f + 1.2f * j, 6.819498f, -11.89f),
                    Quaternion.identity);
            myball.transform.rotation = Quaternion.Euler(0, 90, 0);
            ballList.Add(myball);
        }
    }

    private void RebuildBall()
    {
        for (int j = 0; j < 8; j++)
        {
            int randomObj = Random.Range(0, 3);
            GameObject myball = (GameObject)Instantiate(ballType[randomObj], new Vector3(-7.0f + 1.2f * j, 6.819498f, -11.89f),
                    Quaternion.identity);
            myball.transform.rotation = Quaternion.Euler(0, 90, 0);
            ballList[j] = myball;
            ballList[j].SetActive(true);
        }
    }

    public void Update()
    {

        if (GameManager.MiniCam.activeSelf == true)
        {
            if (ballList.Count == 0)
                InitBall();

            if (ballList[7].activeSelf == false)
            {
                RebuildBall();
            }

            playerVisited = true;

            if (Input.GetKeyDown(KeyCode.UpArrow) && ballList[i].tag == "Up")
            {
                ballList[i].SetActive(false);
                GameObject.Find("Up").GetComponent<AudioSource>().Play();
                i += 1;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && ballList[i].tag == "Down")
            {
                ballList[i].SetActive(false);
                GameObject.Find("Down").GetComponent<AudioSource>().Play();
                i += 1;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && ballList[i].tag == "Left")
            {
                ballList[i].SetActive(false);
                GameObject.Find("Left").GetComponent<AudioSource>().Play();
                i += 1;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && ballList[i].tag == "Right")
            {
                ballList[i].SetActive(false);
                GameObject.Find("Right").GetComponent<AudioSource>().Play();
                i += 1;
            }

            if (i == 8)
            {
                GameObject player = GameObject.Find("Player");
                player.transform.position = new Vector3(-327.5f + 1.2f * counteridx, 69.941f, 86.734f);
                isCorrected = true;

                i = 0;

                GameManager.MiniCam.SetActive(false);
                Player.mainCamera.SetActive(true);
            }
        }
    }
}