using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMove : MonoBehaviour
{
    Transform playerTransform;
    Vector3 Offset;

    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Offset = transform.position - playerTransform.position;
    }

    void LateUpdate()
    {
        transform.position = playerTransform.position + Offset;
    }

    [SerializeField]
    private float lookSensitivity; //마우스 민감도, 음수로 설정해야 흔한 1인칭 조작

    [SerializeField]
    private float cameraRotationLimit; //최대 카메라 회전각 75정도면 적당
    private float currentCameraRotationX = 0;
    private float currentCameraRotationY = 0;

    [SerializeField]
    private Camera theCamera;

    // Start is called before the first frame update
    void Start()
    {
        //마우스 커서 숨기기
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        CameraRotation();
    }
    void CameraRotation()
    {
        float yRotation = Input.GetAxisRaw("Mouse X");
        float cameraRaotationY = yRotation * lookSensitivity;
        currentCameraRotationY += cameraRaotationY;

        float xRotation = Input.GetAxisRaw("Mouse Y");
        float cameraRaotationX = xRotation * lookSensitivity;
        currentCameraRotationX += cameraRaotationX;
        
        //Mathf.Clamp : 최대 / 최소값 사이의 float 값이 값이 범위 외의 값이되지 않도록한다
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        //localEulerAngles: 부모 트랜스폼의 회전(rotation)과 상대적인, 각도 단위 Euler 각도의 회전
        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, -currentCameraRotationY, 0f);
    }
}