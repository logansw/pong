using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : PaddleController
{
    private Ball _ball;

    void Start()
    {
        _ball = FindObjectOfType<Ball>();
    }

    public override Vector3 Move()
    {
        return Vector3.zero;
    }
}
