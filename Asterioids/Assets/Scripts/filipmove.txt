using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[Serializable]
public class HitEvent : UnityEvent { }


[Serializable]
public class DeadEvent : UnityEvent { }


[RequireComponent(typeof(Rigidbody2D))]
public class SpaceshipController : Moveable
{
    public static SpaceshipController instance;


    public int hp;


    public HitEvent hitEvent;
    public DeadEvent deadEvent;


    public float rotationSpeed = 1;

    [SerializeField] private ParticleSystem ps;


    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        rb = GetComponent<Rigidbody2D>();

        hp = 5;
    }
    Vector2 input;
    float forwardInput;
    public void MoveInput(InputAction.CallbackContext ctx)
    {
        input = ctx.ReadValue<Vector2>();
    }
    public void MoveForward(InputAction.CallbackContext ctx)
    {
        forwardInput = ctx.ReadValue<float>();

        rb.AddForce(rb.transform.up * forwardInput);
    }



    public void RestartGame(InputAction.CallbackContext ctx)
    {
        UIController.instance.LoadMainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    // LineRenderer lr = GetComponent<LineRenderer>();
        //    // Transform transform = GetComponent<Transform>();
        //    transform.Rotate(new Vector3(0, 0, 1), rotationSpeed * Time.deltaTime);
        //}
        transform.Rotate(new Vector3(0, 0, -input.x), rotationSpeed * Time.deltaTime);
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);

        //}

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    LineRenderer lr = GetComponent<LineRenderer>();
        //    Vector3 position = lr.GetPosition(0);

        //    var thePosition = transform.TransformPoint(position);

        //    //Instantiate(bullet, thePosition, transform.rotation);


        //    GameObject bullet = BulletPool.instance.GetBullet();

        //    if (bullet != null)
        //    {
        //        bullet.transform.position = thePosition;
        //        bullet.transform.rotation = transform.rotation;
        //        bullet.SetActive(true);
        //        StopCoroutine(DestroyAfterTime(bullet));
        //    }

        //}

        CorrectPosition();
    }
    private void FixedUpdate()
    {

        //transform.Translate(Vector3.up * speed * Time.deltaTime);
        rb.AddForce(rb.transform.up * forwardInput * speed);

    }

    IEnumerator DestroyAfterTime(GameObject obj)
    {
        yield return new WaitForSeconds(5);

        obj.SetActive(false);
    }


    public VisualTreeAsset startScreen;

    public void Shoot(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (UIController.instance.uIDocument.visualTreeAsset == startScreen)
            {
                UIController.instance.Play();
                
            }


            LineRenderer lr = GetComponent<LineRenderer>();
            Vector3 position = lr.GetPosition(0);

            var thePosition = transform.TransformPoint(position);

            //Instantiate(bullet, thePosition, transform.rotation);


            GameObject bullet = BulletPool.instance.GetBullet();

            if (bullet != null)
            {
                bullet.transform.position = thePosition;
                bullet.transform.rotation = transform.rotation;
                bullet.SetActive(true);

                StartCoroutine(DestroyAfterTime(bullet));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag.Equals("Bullet"))
            return;

        hp--;


        if (hp == 0)
        {
            deadEvent?.Invoke();
            enabled = false;
            return;
        }

        ps.Play();

        StartCoroutine(DieRespawn());




    }

    IEnumerator DieRespawn()
    {
        GetComponent<LineRenderer>().enabled = false;
        GetComponent<EdgeCollider2D>().enabled = false;


        yield return new WaitForSeconds(1);


        GetComponent<LineRenderer>().enabled = true;

        GetComponent<EdgeCollider2D>().enabled = true;


        hitEvent?.Invoke();

        transform.position = new Vector3(0, 0, 0);
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;

    }
}
