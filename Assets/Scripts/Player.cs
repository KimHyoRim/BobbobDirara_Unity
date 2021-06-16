
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;

    Vector3 moveVec;

    Animator anim;

    [SerializeField]
    private Camera theCamera;
    private Rigidbody myRigid;

    bool isBorder;
    bool isRoadScene = false;

    public int foodnum = 0;
    public Queue<GameObject> myFoodList = new Queue<GameObject>();
    public Queue<GameObject> myHandList = new Queue<GameObject>();
    public List<GameObject> dropFoodList = new List<GameObject>();
    public GameObject food;
    public GameObject[] foodType = new GameObject[4];

    public GameObject foodleftpoint;
    public GameObject foodrightpoint;

    public GameObject[] arrayChild = new GameObject[6];
    public GameObject collidedTable;

    public static GameObject mainCamera;
    public static GameObject miniCamera;

    float rx;
    float ry;

    int cnt = 0;

    public AudioSource CleanSound;
    public AudioSource PickupSound;
    public AudioSource DropSound;

    public ParticleSystem Twinkle;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        myRigid = GetComponent<Rigidbody>();

        KeyboardInput.playerVisited = false;

        mainCamera = GameObject.FindWithTag("MainCamera");
        miniCamera = GameObject.FindWithTag("MiniCamera");
        miniCamera.SetActive(false);

        foodType[0] = GameObject.Find("Pizza_Mesh");
        foodType[1] = GameObject.Find("french frice");
        foodType[2] = GameObject.Find("Sushi");
        foodType[3] = GameObject.Find("Chicken");

        foodleftpoint = GameObject.Find("foodleftpoint");
        foodrightpoint = GameObject.Find("foodrightpoint");

        CleanSound = GameObject.Find("Clean").GetComponent<AudioSource>();
        PickupSound = GameObject.Find("Pickup").GetComponent<AudioSource>();
        DropSound = GameObject.Find("Drop").GetComponent<AudioSource>();

        //Twinkle = GameObject.Find("Twinkle").GetComponent<ParticleSystem>();
    }

    void makeFood()
    {
        food = (GameObject)Instantiate(foodType[KeyboardInput.counteridx], new Vector3(-327.5f + 1.2f * KeyboardInput.counteridx, 71.416f, 87.9f),
                    Quaternion.identity);
        foodnum += 1;
        PickupSound.Play();

        if (KeyboardInput.counteridx == 1)
        {
            Transform tr = food.GetComponent<Transform>();
            tr.Rotate(new Vector3(0f, 180f, 0f));
        }

        myFoodList.Enqueue(food);
        KeyboardInput.isCorrected = false;
    }

    void Update()
    {
        if (KeyboardInput.isCorrected == true)
        {
            makeFood();
            KeyboardInput.playerVisited = false;
            KeyboardInput.isCorrected = false;
        }
        Move();
        CharactorRotation();
        PickupFood();
        DropFood();
        CleanTable();
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
        float char_Y_Ro = Input.GetAxisRaw("Mouse Y");
        float char_X_Ro = Input.GetAxisRaw("Mouse X");

        rx += speed * char_Y_Ro * Time.deltaTime * 10;
        ry += speed * char_X_Ro * Time.deltaTime * 10;

        rx = Mathf.Clamp(rx, -45, 30);

        this.transform.eulerAngles = new Vector3(0, ry, 0);
        theCamera.transform.eulerAngles = new Vector3(-rx, ry, 0);
    }

    void PickupFood()
    {
        if (Input.GetKeyUp(KeyCode.P) && myFoodList != null)
        {
            if (myHandList.Count == 0)
            {
                Transform tr = myFoodList.Peek().GetComponent<Transform>();
                tr.SetParent(foodleftpoint.transform);
                tr.localPosition = Vector3.zero;
                tr.rotation = new Quaternion(0, 0, 0, 0);
                myHandList.Enqueue(myFoodList.Dequeue());
            }
            else if (myHandList.Count == 1)
            {
                Transform tr = myFoodList.Peek().GetComponent<Transform>();
                tr.SetParent(foodrightpoint.transform);
                tr.localPosition = Vector3.zero;
                tr.rotation = new Quaternion(0, 0, 0, 0);
                myHandList.Enqueue(myFoodList.Dequeue());
            }

            anim.SetBool("isPickup", true);
        }
    }

    void DropFood()
    {
        if (Input.GetKeyDown(KeyCode.X) && collidedTable != null)
        {
            if (collidedTable.gameObject.GetComponentsInChildren<Transform>().Length < 2)
            {
                Transform tr = myHandList.Dequeue().GetComponent<Transform>();
                tr.SetParent(collidedTable.transform);
                tr.localPosition = new Vector3(0.0f, 17.5f, 0.0f);
                tr.rotation = new Quaternion(0, 0, 0, 0);
            }
            else
            {
                Transform ytr = collidedTable.gameObject.GetComponentsInChildren<Transform>()[1];
                ytr.localPosition = new Vector3(0.0f, 17.5f, -4.0f);

                Transform tr = myHandList.Dequeue().GetComponent<Transform>();

                tr.SetParent(collidedTable.transform);
                tr.localPosition = new Vector3(0.0f, 17.5f, 4.0f);
                tr.rotation = new Quaternion(0, 0, 0, 0);
            }
            if (myHandList.Count == 0)
                anim.SetBool("isPickup", false);
            collidedTable = null;
            DropSound.Play();
        }
    }

    void CleanTable()
    {
        if (Input.GetKeyDown(KeyCode.C) && collidedTable != null)
        {
            if (collidedTable.gameObject.GetComponentsInChildren<Transform>().Length > 1)
            {
                cnt += 1;
                CleanSound.Play();
                if (cnt == 10)
                {
                    Transform[] childList = collidedTable.gameObject.GetComponentsInChildren<Transform>(true);
                    if (childList != null)
                    {
                        for (int i = 1; i < childList.Length; i++)
                        {
                            if (childList[i] != transform)
                                Destroy(childList[i].gameObject);
                        }
                    }
                    cnt = 0;
                    Twinkle.GetComponent<Transform>().position = collidedTable.transform.position;
                    Twinkle.GetComponent<Transform>().position += new Vector3(0, 1.0f, 0);
                    //Twinkle.Play();
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "counter" && isRoadScene == false)
        {
            if (other.name == "PW_stove")
                KeyboardInput.counteridx = 0;
            else if (other.name == "PW_stove (1)")
                KeyboardInput.counteridx = 1;
            else if (other.name == "PW_stove (2)")
                KeyboardInput.counteridx = 2;
            else if (other.name == "PW_stove (3)")
                KeyboardInput.counteridx = 3;

            mainCamera.SetActive(false);
            miniCamera.SetActive(true);
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
            for (int i = 0; i < arrayChild.Length; i++)
            {
                if (other.gameObject == arrayChild[i])
                {
                    collidedTable = arrayChild[i];
                }
            }
        }
    }
}