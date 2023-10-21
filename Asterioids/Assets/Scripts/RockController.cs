using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RockController : MonoBehaviour
{
    [SerializeField] private Vector3 direction;

    [SerializeField] private float speed;

    private float Left => Camera.main.ViewportToWorldPoint(Vector3.zero).x;
    private float Right => Camera.main.ViewportToWorldPoint(Vector3.right).x;
    private float Top => Camera.main.ViewportToWorldPoint(Vector3.up).y;
    private float Bottom => Camera.main.ViewportToWorldPoint(Vector3.zero).y - lr.bounds.size.y * 0.5f;

    private float SpawnLeft => Left - lr.bounds.size.x * 0.5f;
    private float SpawnRight => Right + lr.bounds.size.x * 0.5f;
    private float SpawnTop => Top + lr.bounds.size.y * 0.5f;
    private float SpawnBottom => Bottom - lr.bounds.size.y * 0.5f;

    private float ScreenHeight => (Top - Bottom);
    private float ScreenWidth => (Right - Left);

    private LineRenderer lr;
    private MeshCollider mc;

    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        mc = GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position +=
            new Vector3(
                direction.normalized.x * speed * Time.deltaTime,
                direction.normalized.y * speed * Time.deltaTime,
                0
            );

        CorrectPosition();
    }

    private void CorrectPosition()
    {
        // if (direction.normalized.x < 0)
        // {
        //     if (lr.bounds.max.x < Left)
        //     {
        //         transform.position = 
        //             new Vector3(
        //                 Right + lr.bounds.size.x * 0.5f,
        //                 transform.position.y,
        //                 transform.position.z);
        //     }
        // }

        transform.position += direction.normalized * Time.deltaTime * speed;
        transform.position = new Vector3(
            (transform.position.x + SpawnRight + ScreenWidth) % ScreenWidth - SpawnRight,
            (transform.position.y + SpawnTop + ScreenHeight) % ScreenHeight - SpawnTop,
            transform.position.z
        );
    }
}