using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletMove : Loopable
{
    [SerializeField]
    public float MoveSpeed = 10;
    [SerializeField]
    public float Accel = 1;

    private SpriteRenderer spriteRenderer;
    private bool loopedOnce;

    public GameController controller;


    // Start is called before the first frame update
    void Start()
    {
        //startPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();

        loopedOnce = false;

        Destroy(gameObject, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!controller.canMove)
        {
            return;
        }

        float mult = Accel * Time.deltaTime;
        var pos = transform.position;
        var angle = (transform.eulerAngles.z * Mathf.PI) / 180;

        pos.x -= Mathf.Sin(angle) * MoveSpeed * mult;
        pos.y += Mathf.Cos(angle) * MoveSpeed * mult;
        transform.position = pos;

        if (CorrectPosition(spriteRenderer))
        {
            if (loopedOnce)
                Destroy(gameObject);
            loopedOnce = true;
        }
        CheckOutOfBoundsDeath(spriteRenderer);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        var rock = other.GetComponent<RockCreator>();
        if (rock != null)
        {
            AddScore(rock.GetSize());
            Destroy(gameObject);
            rock.HandleShoot();
        }
    }
    void AddScore(float size)
    {
        Highscore.Instance.IncreaseScore(size);
    }
}
