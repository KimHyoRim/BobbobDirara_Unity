using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_move : MonoBehaviour
{
    public Rigidbody player_rigid;
    public float movementspeed = 10.0f;
    void Update()
    {
        // ���콺 �Է�
        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");

        transform.eulerAngles = transform.eulerAngles + new Vector3(x: 0, y: mouseX, z: 0);
        
        // Ű���� �Է�
        var keyX = Input.GetAxis("Horizontal");
        var keyZ = Input.GetAxis("Vertical");

        player_rigid.velocity = transform.forward * (keyZ * movementspeed) + transform.right * (keyX * movementspeed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "counter")
        {
            SceneManager.LoadScene("MiniGame_01");
        }
    }
}
