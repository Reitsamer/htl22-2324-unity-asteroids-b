using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class SpaceshipController : Loopable, IShouldBeStopped
{
    [SerializeField]
    public float RotSpeed = 300;
    [SerializeField]
    public float MoveSpeed = 10;

    [SerializeField]
    public GameObject bulletPrefab;

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
        float x = 
            ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) ? RotSpeed : 0) - 
            ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) ? RotSpeed : 0);

        transform.Rotate(new Vector3(0, 0, -x), RotSpeed * Time.deltaTime);

        // shoot bullet
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var bullet = Instantiate(bulletPrefab);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;

            bullet.transform.Translate(Vector3.up * 2.5f, transform);
        }

        CorrectPosition(lineRenderer);
    }

   
   
    private void FixedUpdate()
    {
        if (!canMove)
            return;

        float fw = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) ? MoveSpeed : 0;
        rb.AddForce(rb.transform.up * fw);
    }
    public void StartGame()
    {
        canMove = true;
    }
}