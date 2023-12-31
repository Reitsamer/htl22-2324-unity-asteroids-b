using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : Loopable
{
    [SerializeField]
    public float MoveSpeed = 10;
    [SerializeField]
    public float Accel = 2;

    private Vector3 startPos;
    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        lineRenderer = GetComponent<LineRenderer>();

        DoReset();
    }

    void DoReset()
    {
        transform.position = startPos;

        var direction = Random.rotation.z;

        var temp = transform.rotation;
        temp.z = direction;
        transform.rotation = temp;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            DoReset();
        }

        {
            float mult = Accel * Time.deltaTime;
            var pos = transform.position;
            var angle = (transform.eulerAngles.z * Mathf.PI) / 180;
            pos.x -= Mathf.Sin(angle) * MoveSpeed * mult;
            pos.y += Mathf.Cos(angle) * MoveSpeed * mult;
            transform.position = pos;
        }

        CorrectPosition(lineRenderer);
    }
}
