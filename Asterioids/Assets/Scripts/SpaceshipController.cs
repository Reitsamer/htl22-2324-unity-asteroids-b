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

    [SerializeField]
    public GameObject bulletPrefab;
    [SerializeField]
    private GameController gameController;

    public float Accel = 0;
    private Vector3 AccelVec = Vector3.zero;

    private LineRenderer lineRenderer;

    bool canMove = false;

    Rigidbody2D rb;

    public UnityEvent AddToScore;
    // Start is called before the first frame update
    void Start()
    {
        Accel = 0;
        lineRenderer = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var bullet = Instantiate(bulletPrefab);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;

            bullet.transform.Translate(Vector3.up * 2.8f, transform);
        }

        CorrectPosition(lineRenderer);
    }

   
   
    private void FixedUpdate()
    {
        if (!canMove)
            return;

        float forward = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) ? MoveSpeed : 0;
        rb.AddForce(rb.transform.up * forward);
    }
    public void StartGame()
    {
        canMove = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        gameController.Crash();
    }

    public void StopGame()
    {
        canMove = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0;
    }
}