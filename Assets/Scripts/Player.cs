using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 7.0f;

    Vector3 moveVec;

    Animator anim;

    [SerializeField]
    private float lookSensitivity;

    [SerializeField]
    private float cameraRotationLimit;        
   
    private float currentCharactorRotationX;
    private float currentCharactorRotationY;
 
    private float currentCameraRotationX;
    private float currentCameraRotationY;

    [SerializeField]
    private Camera theCamera;
    private Rigidbody myRigid;

    bool isBorder;


    void Awake()
    {
        anim = GetComponent<Animator>();

    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        myRigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        CharactorRotation();
    }

    void FixedUpdate()
    {
        FreezeRotation();
        //StopToWall();
    }

    void FreezeRotation()
    {
        myRigid.angularVelocity = Vector3.zero;
    }

    void StopToWall()
    {
        Debug.DrawRay(transform.position, transform.forward * 1, Color.green);
        isBorder = Physics.Raycast(transform.position, transform.forward, 0.3f, LayerMask.GetMask("Wall"));
    }

    void Move()
    {

        if (Input.GetKey(KeyCode.W))
        {

            isBorder = Physics.Raycast(transform.position, transform.forward, 0.3f, LayerMask.GetMask("Wall"));
            if (!isBorder)
            {
                moveVec = Vector3.forward * speed * Time.deltaTime;

                this.transform.Translate(moveVec);
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            isBorder = Physics.Raycast(transform.position, transform.forward * -1, 0.3f, LayerMask.GetMask("Wall"));
            if (!isBorder)
            {
                moveVec = Vector3.back * speed * Time.deltaTime;

                this.transform.Translate(moveVec);
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            isBorder = Physics.Raycast(transform.position, transform.right * -1, 0.3f, LayerMask.GetMask("Wall"));
            if (!isBorder)
            {
                moveVec = Vector3.left * speed * Time.deltaTime;
                this.transform.Translate(moveVec);
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            isBorder = Physics.Raycast(transform.position, transform.right, 0.3f, LayerMask.GetMask("Wall"));
            if (!isBorder)
            {
                moveVec = Vector3.right * speed * Time.deltaTime;
                this.transform.Translate(moveVec);
            }
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) ||
            Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            moveVec = Vector3.zero;

        anim.SetBool("isWalk", moveVec != Vector3.zero);

    }


    void CharactorRotation()
    {
        float char_Y_Ro = Input.GetAxisRaw("Mouse X");
        float char_X_Ro = Input.GetAxisRaw("Mouse Y");

        this.transform.localRotation *= Quaternion.Euler(0, char_Y_Ro, 0);
        theCamera.transform.localRotation *= Quaternion.Euler(-char_X_Ro, 0, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "counter")
        {
            SceneManager.LoadScene("MiniGame_01");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Wall")
            this.transform.Translate(-moveVec * 2);
    }
}