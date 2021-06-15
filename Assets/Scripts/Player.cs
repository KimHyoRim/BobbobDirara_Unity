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

    public GameObject food;
    public GameObject[] foodType = new GameObject[4];

    public GameObject[] arrayChild = new GameObject[6];
    public GameObject collidedTable;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        myRigid = GetComponent<Rigidbody>();

        if (KeyboardInput.playerVisited == true)
        {
            this.transform.position = new Vector3(-327.5f + 1.2f * KeyboardInput.counteridx, 69.941f, 86.734f);
            makeFood();
        }

        KeyboardInput.playerVisited = false;
    }

    void makeFood()
    {
        foodType[0] = GameObject.Find("Pizza_Mesh");
        foodType[1] = GameObject.Find("french frice");
        foodType[2] = GameObject.Find("Sushi");
        foodType[3] = GameObject.Find("Chicken");

        food = (GameObject)Instantiate(foodType[KeyboardInput.counteridx], new Vector3(-327.5f + 1.2f * KeyboardInput.counteridx, 71.416f, 87.9f),
                    Quaternion.identity);

        if (KeyboardInput.counteridx == 1)
        {
            Transform tr = food.GetComponent<Transform>();
            tr.Rotate(new Vector3(0f, 180f, 0f));
        }

    }

    void Update()
    {
        Move();
        CharactorRotation();
        PickupFood();
    }

    void FixedUpdate()
    {
        FreezeRotation();
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

    void PickupFood()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            GameObject foodPoint = GameObject.Find("foodpoint");
            Transform tr = food.GetComponent<Transform>();
            tr.SetParent(foodPoint.transform);
            tr.localPosition = Vector3.zero;
            tr.rotation = new Quaternion(0, 0, 0, 0);

            anim.SetBool("isPickup", true);
        }

        if (Input.GetKeyUp(KeyCode.X) && collidedTable != null)
        {
            Debug.Log("음식 내려놓기 ");
            Transform tr = food.GetComponent<Transform>();
            tr.SetParent(collidedTable.transform);
            //tr.localPosition = new Vector3(2.8f, 0.9f, -0.15f);
            tr.localPosition = new Vector3(0.0f, 17.5f, 0.0f);
            tr.rotation = new Quaternion(0, 0, 0, 0);

            anim.SetBool("isPickup", false);
            collidedTable = null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "counter")
        {
            if (other.name == "PW_stove")
                KeyboardInput.counteridx = 0;
            else if (other.name == "PW_stove (1)")
                KeyboardInput.counteridx = 1;
            else if (other.name == "PW_stove (2)")
                KeyboardInput.counteridx = 2;
            else if (other.name == "PW_stove (3)")
                KeyboardInput.counteridx = 3;
           
            SceneManager.LoadScene("MiniGame_01");
        }

        if (other.tag == "BurgerShop")
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Wall")
        {
            this.transform.Translate(-moveVec * 2);
        }

        if (other.gameObject.tag == "Table")
        {
            Debug.Log("충돌 발생");
            for (int i = 0; i < arrayChild.Length; i++)
            {
                if (other.gameObject == arrayChild[i])
                {
                    Debug.Log(i);
                    collidedTable = arrayChild[i];
                }
            }
        }
    }
}