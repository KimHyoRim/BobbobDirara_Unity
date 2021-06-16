using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{

    private bool isCollision = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool GetisCollision()
    {
        return isCollision;
    }

    public void SetisCollision(bool _isCollision)
    {
        isCollision = _isCollision;
    }
}
