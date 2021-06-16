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
    private bool isFirstWait;




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
        isFirstWait = false;

        firstWaitWatch.Start();


        

        
        
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
            nma.SetDestination(goalPos);
        }

        
        

        if(watch.ElapsedMilliseconds >= 10000)
        {
            watch.Stop();
            watch.Reset();
            thisTrans.transform.localPosition = startPos;
            goalObject.GetComponent<GoalScript>().SetisCollision(false);
            isCollision = false;
            MakeRandGoal();
        }

       // UnityEngine.Debug.Log(watch.ElapsedMilliseconds);
        
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
            moveVec = Vector3.forward * speed * Time.deltaTime;
        
 
        this.transform.Translate(moveVec);

        anim.SetBool("isWalk", moveVec != Vector3.zero);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "goal")
        {
            isCollision = true;
            goalObject.GetComponent<GoalScript>().SetisCollision(true);
            watch.Start();
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

        thisTrans = GetComponent<Transform>();
        goalTrans = goalObject.GetComponent<Transform>();
        nma = GetComponent<NavMeshAgent>();

        goalPos = new Vector3(goalTrans.position.x, goalTrans.position.y, goalTrans.position.z - 0.7f);

        UnityEngine.Debug.Log(randGoal);
    }

    IEnumerator SomeCoroutine(int i)
    {
        yield return new WaitForSeconds(5f * i);
        UnityEngine.Debug.Log("씨발");
    }
}
