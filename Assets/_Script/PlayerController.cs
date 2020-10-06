using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public BulletManager bulletManager;

    private Rigidbody2D rigidBody;
    
    public float horizontalBoundary;
    public float horizontalSpeed;
    public float maxSpeed;

    private Vector3 touchEnd;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        touchEnd = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        _Move();
        _CheckBounds();
        _FireBullet();
    }

    private void _FireBullet()
    {
        if(Time.frameCount % 20 == 0)
            bulletManager.GetBullet(transform.position);
    }

    private void _Move()
    {
        float direction = 0.0f;

        // Simple touch input
        foreach (Touch touch in Input.touches)
        {
            Vector3 worldTouch = Camera.main.ScreenToWorldPoint(touch.position);

            // Keyboard and touch input
            if (worldTouch.x > transform.position.x)
            {
                direction = 1.0f;
            }

            if (worldTouch.x < transform.position.x)
            {
                direction = -1.0f;
            }

            touchEnd = worldTouch;
        }


        // Keyboard input
        if (Input.GetAxis("Horizontal") >= 0.1f)
        {
            direction = 1.0f;
        }

        if (Input.GetAxis("Horizontal") <= -0.1f)
        {
            direction = -1.0f;
        }


        if (touchEnd.x != 0)
        {
            transform.position = new Vector2(Mathf.Lerp(transform.position.x, touchEnd.x, 0.1f), transform.position.y);
        }
        else
        {
            Vector2 newVelocity = rigidBody.velocity + new Vector2(direction * horizontalSpeed, 0.0f);
            rigidBody.velocity = Vector2.ClampMagnitude(newVelocity, maxSpeed);
            rigidBody.velocity *= 0.99f;
        }
    }

    private void _CheckBounds()
    {
        if (transform.position.x >= horizontalBoundary)
        {
            transform.position = new Vector3(horizontalBoundary, transform.position.y, 0);
        }

        if (transform.position.x <= -horizontalBoundary)
        {
            transform.position = new Vector3(-horizontalBoundary, transform.position.y, 0);
        }
    }
}
