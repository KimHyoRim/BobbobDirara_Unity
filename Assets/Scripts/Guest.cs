using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;
using System.Diagnostics;

public class Guest : MonoBehaviour
{
    public float speed = 7.0f;
    private int randGoal;
    private bool isCollision;
    Vector3 moveVec;
    Vector3 goalPos;
    Vector3 startPos;

    Animator anim;
    private Rigidbody myRigid;

    private GameObject goalObject;

    private Transform thisTrans;
    private NavMeshAgent nma;
    private Transform goalTrans;

    // 시간 측정
    Stopwatch watch;
    Stopwatch firstWaitWatch;
    Stopwatch waitWatch;

    private bool isFirst;

    bool isSitting = false;

    public int randFoodnum;
    public static int randFood;
    public GameObject[] foodType = new GameObject[4];
    public GameObject food;
    public List<GameObject> foodList = new List<GameObject>();

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
        watch = new Stopwatch();
        firstWaitWatch = new Stopwatch();

        startPos = new Vector3(-323.25f, 69.89f, 68.11f);
        isCollision = false;
        isFirst = true;

        firstWaitWatch.Start();

        foodType[0] = GameObject.Find("Pizza_Mesh");
        foodType[1] = GameObject.Find("french frice");
        foodType[2] = GameObject.Find("Sushi");
        foodType[3] = GameObject.Find("Chicken");
    }

    void Update()
    {
        if (isFirst == true)
        {
            for (int i = 0; i < 6; ++i)
            {
                if (this.gameObject.name == "guest" + (i + 1).ToString())
                {
                    if (firstWaitWatch.ElapsedMilliseconds >= 7000 * i + 1)
                    {
                        MakeRandGoal();
                        isFirst = false;

                    }
                }
            }
        }
        else
        {
            Move();
            if (!isSitting)
                nma.SetDestination(goalPos);
        }

        if (watch.ElapsedMilliseconds >= 10000)
        {
            anim.SetBool("Waiting", false);
            anim.SetBool("isLeave", true);

            watch.Stop();
            watch.Reset();

            thisTrans.parent = null;
            thisTrans.transform.position = startPos;
            goalObject.GetComponent<GoalScript>().SetisCollision(false);
            isCollision = false;
            isSitting = false;
            nma.enabled = true;
            MakeRandGoal();

            for (int i = 0; i < foodList.Count; i++)
                Destroy(foodList[i]);
        }


    }

    void FixedUpdate()
    {
        FreezeRotation();
    }

    void FreezeRotation()
    {
        myRigid.angularVelocity = Vector3.zero;
    }

    void Move()
    {
        if (isCollision == true)
            moveVec = Vector3.zero;
        else
        {
            moveVec = Vector3.forward * speed * Time.deltaTime;
            this.transform.Translate(moveVec);
        }

        anim.SetBool("isWalk", moveVec != Vector3.zero);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "goal")
        {
            isCollision = true;
            isSitting = true;
            anim.SetBool("Waiting", true);
            anim.SetBool("isLeave", false);

            nma.enabled = false;
            //nma.SetDestination(collision.transform.position);
            thisTrans.SetParent(goalObject.GetComponent<Transform>());
            thisTrans.localPosition = new Vector3(0.0f, 0.0f, 1.0f);
            this.transform.rotation = new Quaternion(0, 0, 0, 0);

            order();

            watch.Start();
        }
    }

    void order()
    {
        randFoodnum = UnityEngine.Random.Range(0, 1);

        if (randFoodnum == 0)
        {
            randFood = UnityEngine.Random.Range(0, 3);
            food = (GameObject)Instantiate(foodType[randFood], thisTrans.position + new Vector3(0.0f, 1.5f, 0.0f),
                    Quaternion.identity);
            food.GetComponent<Transform>().SetParent(thisTrans);
            foodList.Add(food);
        }
        else
        {
            randFood = UnityEngine.Random.Range(0, 3);
            food = (GameObject)Instantiate(foodType[randFood], thisTrans.position, Quaternion.identity);

            randFood = UnityEngine.Random.Range(0, 3);
            GameObject food2 = (GameObject)Instantiate(foodType[randFood], thisTrans.position, Quaternion.identity);

            food.GetComponent<Transform>().SetParent(thisTrans);
            food.GetComponent<Transform>().localPosition = new Vector3(-5.0f, 1.5f, 0.0f);
            foodList.Add(food);

            food2.GetComponent<Transform>().SetParent(thisTrans);
            food2.GetComponent<Transform>().localPosition = new Vector3(5.0f, 1.0f, 0.0f);
            foodList.Add(food2);
        }
    }

    void MakeRandGoal()
    {
        randGoal = UnityEngine.Random.Range(1, 7);
        goalObject = GameObject.Find("chair" + randGoal.ToString());

        if (goalObject.GetComponent<GoalScript>().GetisCollision() == true)
        {
            while (goalObject.GetComponent<GoalScript>().GetisCollision() != false)
            {
                randGoal = UnityEngine.Random.Range(1, 7);
                goalObject = GameObject.Find("chair" + randGoal.ToString());

            }
        }

        goalObject.GetComponent<GoalScript>().SetisCollision(true);

        thisTrans = GetComponent<Transform>();
        goalTrans = goalObject.GetComponent<Transform>();
        nma = GetComponent<NavMeshAgent>();


        if (randGoal == 1 || randGoal == 2 || randGoal == 3)
        {
            goalPos = new Vector3(goalTrans.position.x - 0.5f, goalTrans.position.y, goalTrans.position.z - 0.5f);
        }
        else if (randGoal == 4 || randGoal == 5 || randGoal == 6)
        {
            goalPos = new Vector3(goalTrans.position.x + 0.5f, goalTrans.position.y, goalTrans.position.z - 0.5f);
        }

        UnityEngine.Debug.Log(randGoal);
    }

    IEnumerator SomeCoroutine(int i)
    {
        yield return new WaitForSeconds(5f * i);
        UnityEngine.Debug.Log("하하");
    }
}