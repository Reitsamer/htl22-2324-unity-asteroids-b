using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class SpaceshipController : Loopable, IStartStop
{
    [SerializeField]
    public float RotSpeed = 300;
    [SerializeField]
    public float MoveSpeed = 10;

/*
// <<<<<<< HEAD
    [SerializeField]
    public GameObject bulletPrefab;
    [SerializeField]
    private GameController gameController;

    public float Accel = 0;
    private Vector3 AccelVec = Vector3.zero;

    private LineRenderer lineRenderer;

    Rigidbody2D rb;

    public UnityEvent AddToScore;
// =======
    [SerializeField] private Transform bullet;

    public VariableJoystick variableJoystick;


// >>>>>>> UI-InGameControls
*/
    // Start is called before the first frame update
    void Start()
    {
        Accel = 0;
        lineRenderer = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
        ResetPosition();
    }

    public void ResetPosition()
    {
        transform.position = Vector3.zero;
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
/*
// <<<<<<< HEAD
        if (gameController.canMove)
            Move();
    }
    void Move()
    {
        float horizontal = 
            ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) ? 1 : 0) - 
            ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) ? 1 : 0);

        //Accel += horizontal / 4;
        //Accel -= Accel * (2f * Time.deltaTime);
        //Accel = Mathf.Clamp(Accel, -1, 1);
        //if (Mathf.Abs(Accel) < 0.1)
        //    Accel = 0;

        transform.Rotate(new Vector3(0, 0, -1), RotSpeed * Time.deltaTime * horizontal);

        // shoot bullet
// =======
        Debug.Log(variableJoystick.Direction);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // LineRenderer lr = GetComponent<LineRenderer>();
            // Transform transform = GetComponent<Transform>();
            transform.Rotate(new Vector3(0, 0, 1), speed * Time.deltaTime);
           
        }
        
        if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(Vector3.forward, -speed * Time.deltaTime);

        transform.Rotate(new Vector3(0, 0, 1), speed * Time.deltaTime* -variableJoystick.Direction.x);

        // if (true)//variableJoystick.y!=0)
        //{
        //transform.Rotate(variableJoystick.Direction, speed * Time.deltaTime);
        //}


// >>>>>>> UI-InGameControls
*/
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var bullet = Instantiate(bulletPrefab);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;

            BulletMove move = bullet.GetComponent<BulletMove>();
            move.controller = gameController;

            bullet.transform.Translate(Vector3.up * 2.8f, transform);
        }

        CorrectPosition(lineRenderer);
    }

   
   
    private void FixedUpdate()
    {
        if (!gameController.canMove)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0;
        }

        float forward = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) ? MoveSpeed : 0;
        rb.AddForce(rb.transform.up * forward);
    }
    public void StartGame()
    {
        ResetPosition();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0;
        lineRenderer.enabled = true;

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        gameController.Crash();
    }

    public void StopGame()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0;
    }
}