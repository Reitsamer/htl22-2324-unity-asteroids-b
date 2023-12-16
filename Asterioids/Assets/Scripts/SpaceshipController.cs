using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : Loopable
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

    // Start is called before the first frame update
    void Start()
    {
        Accel = 0;
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // shoot bullet
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var bullet = Instantiate(bulletPrefab);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;

            bullet.transform.Translate(Vector3.up * 2.5f, transform);
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            var pos = transform.eulerAngles;
            pos.z += Time.deltaTime * RotSpeed;
            transform.eulerAngles = pos;
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            var pos = transform.eulerAngles;
            pos.z -= Time.deltaTime * RotSpeed;
            transform.eulerAngles = pos;
        }

        bool fw = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool bw = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);

        Accel -= Accel * (1.7f * Time.deltaTime);
        Accel += ((fw ? 1 : 0) - (bw ? 1 : 0)) * 10f * Time.deltaTime;
        Accel = Mathf.Clamp(Accel, -3, 5);

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
