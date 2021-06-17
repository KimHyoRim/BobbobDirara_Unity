using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject StartCam;
    public GameObject GameCam;
    public static GameObject MiniCam;

    public GameObject StartPanel;
    public GameObject GamePanel;

    public GameObject player;
    public GameObject Guest;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        StartCam = GameObject.FindWithTag("StartCamera");
        GameCam = GameObject.FindWithTag("MainCamera");
        MiniCam = GameObject.FindWithTag("MiniCamera");

        StartPanel = GameObject.Find("StartPanel");
        GamePanel = GameObject.Find("GamePanel");

        player = GameObject.Find("Player");
        Guest = GameObject.Find("Guest");

        StartCam.SetActive(true);
        GameCam.SetActive(false);
        MiniCam.SetActive(false);

        StartPanel.SetActive(true);
        GamePanel.SetActive(false);

        player.gameObject.SetActive(false);
        Guest.gameObject.SetActive(false);
    }

    public void GameStart()
    {
        StartCam.SetActive(false);
        GameCam.SetActive(true);

        StartPanel.SetActive(false);
        GamePanel.SetActive(true);

        player.gameObject.SetActive(true);
        Guest.gameObject.SetActive(true);
    }

}
