using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : Moveable
{
    [SerializeField] private float speed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        if (transform.position.x < Left ||
            transform.position.x > Right ||
            transform.position.y < Bottom ||
            transform.position.y > Top)
        {
            Destroy(gameObject);
        }
    }
}
