using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    float hAxis;
    float vAxis;

    Vector3 moveVec;

    Animator anim;

    [SerializeField]
    private float lookSensitivity;

    [SerializeField]
    private float cameraRotationLimit; //최대 카메라 회전각 75정도면 적당

    // 캐릭터 회전 변수
    private float currentCharactorRotationX;
    private float currentCharactorRotationY;

    // 카메라 회전 변수
    private float currentCameraRotationX;
    private float currentCameraRotationY;

    [SerializeField]
    private Camera theCamera;
    private Rigidbody myRigid;


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
        CameraRotation();
    }

    void Move()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        transform.position += moveVec * speed * Time.deltaTime;

        anim.SetBool("isWalk", moveVec != Vector3.zero);
        //transform.LookAt(transform.position + moveVec);
    }

    void CameraRotation()
    {
        float yRotation = Input.GetAxisRaw("Mouse X");
        //float cameraRaotationY = yRotation * lookSensitivity;

        float xRotation = Input.GetAxisRaw("Mouse Y");
        //float cameraRaotationX = xRotation * lookSensitivity;

        theCamera.transform.eulerAngles = transform.eulerAngles + new Vector3(x: 0, y: yRotation, z: 0);
        //currentCameraRotationX -= cameraRaotationX;
        //currentCameraRotationY -= cameraRaotationY;

        //currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        //theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, -currentCameraRotationY, 0f);
    }

    void CharactorRotation()
    {
        float char_Y_Ro = Input.GetAxisRaw("Mouse X");
        //float CharactoRaotationY = char_Y_Ro * lookSensitivity;

        float char_X_Ro = Input.GetAxisRaw("Mouse Y");

        transform.eulerAngles = transform.eulerAngles + new Vector3(x: 0, y: char_Y_Ro, z: 0);
        //float CharactoRaotationX = char_X_Ro * lookSensitivity;

        //currentCharactorRotationX -= CharactoRaotationX;
        //currentCharactorRotationY -= CharactoRaotationY;

        //currentCharactorRotationX = Mathf.Clamp(currentCharactorRotationX, -cameraRotationLimit, cameraRotationLimit);

        //transform.localEulerAngles = new Vector3(0f, -currentCharactorRotationY, 0f);
    }
}
