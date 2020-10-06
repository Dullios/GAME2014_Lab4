using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public float verticalSpeed;
    public float verticalBoundary;


    // Update is called once per frame
    void Update()
    {
        _Move();
        _CheckBounds();
    }

    private void _Reset()
    {
        transform.position = new Vector3(0, verticalBoundary, 0);
    }

    private void _Move()
    {
        transform.position -= new Vector3(0, verticalSpeed, 0);
    }

    private void _CheckBounds()
    {
        if(transform.position.y <= -verticalBoundary)
        {
            _Reset();
        }
    }
}
