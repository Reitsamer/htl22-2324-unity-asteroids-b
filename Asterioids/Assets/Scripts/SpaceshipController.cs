using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using static UnityEngine.UI.Button;

[RequireComponent(typeof(Rigidbody2D))]
public class SpaceshipController : Loopable, IStartStop
{
    [SerializeField]
    public float RotSpeed = 300;
    [SerializeField]
    public float MoveSpeed = 10;


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

    [SerializeField] 
    private Transform bullet;

    public VariableJoystick RotateJoystick, MoveJoystick;
    public Button shootButton;
    private bool shootPressed;

    // Start is called before the first frame update
    void Start()
    {
        shootPressed = false;
        Accel = 0;
        lineRenderer = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
        shootButton.onClick.AddListener(ButtonClickedEvent);

        ResetPosition();
    }
    
    public void ButtonClickedEvent()
    {
        shootPressed = true;
    }

    public void ResetPosition()
    {
        transform.position = Vector3.zero;
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.canMove)
            Move();
    }

    void Move()
    {
        float horizontal =
            ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) ? 1 : 0) -
            ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) ? 1 : 0);

        if (RotateJoystick.Direction.x != 0)
            horizontal = RotateJoystick.Direction.x;

        transform.Rotate(new Vector3(0, 0, -1), RotSpeed * Time.deltaTime * horizontal);


        if (Input.GetKeyDown(KeyCode.Space) || shootPressed)
        {
            if (shootPressed)
                shootPressed = false;

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

        if (MoveJoystick.Direction.y  > 0)
        {
            forward = MoveJoystick.Direction.y * MoveSpeed;
        }

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