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
    private float lookSensitivity; //���콺 �ΰ���, ������ �����ؾ� ���� 1��Ī ����

    [SerializeField]
    private float cameraRotationLimit; //�ִ� ī�޶� ȸ���� 75������ ����
    private float currentCameraRotationX = 0;
    private float currentCameraRotationY = 0;

    [SerializeField]
    private Camera theCamera;

    // Start is called before the first frame update
    void Start()
    {
        //���콺 Ŀ�� �����
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
        
        //Mathf.Clamp : �ִ� / �ּҰ� ������ float ���� ���� ���� ���� ���̵��� �ʵ����Ѵ�
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        //localEulerAngles: �θ� Ʈ�������� ȸ��(rotation)�� �������, ���� ���� Euler ������ ȸ��
        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, -currentCameraRotationY, 0f);
    }
}