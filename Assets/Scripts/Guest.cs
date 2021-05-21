using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guest : MonoBehaviour
{
    public float speed = 7.0f;

    Vector3 moveVec;

    Animator anim;

    private Rigidbody myRigid;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
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
        moveVec = Vector3.forward * speed * Time.deltaTime;
        this.transform.Translate(moveVec);

        anim.SetBool("isWalk", moveVec != Vector3.zero);
    }
}
