using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : Loopable
{
    [SerializeField]
    public float RotSpeed = 300;
    [SerializeField]
    public float MoveSpeed = 3;
    [SerializeField]
    public float MoveSlowdown = 2f;

    [SerializeField]
    public GameObject bulletPrefab;

    public float Accel = 0;
    private float lastAngle = 0;
    private bool accelPressed = false;
    [SerializeField]
    private List<Vector3> Forces = new List<Vector3>();

    private LineRenderer lineRenderer;



    // Start is called before the first frame update
    void Start()
    {
        Accel = 0;
        lastAngle = 0;
        accelPressed = false;
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        bool space = Input.GetKeyDown(KeyCode.Space);
        bool left = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        bool right = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        bool fw = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);



        // shoot bullet
        if (space)
        {
            var bullet = Instantiate(bulletPrefab);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;

            bullet.transform.Translate(Vector3.up * 2.5f, transform);
        }

        if (left)
        {
            var pos = transform.eulerAngles;
            pos.z += Time.deltaTime * RotSpeed;
            transform.eulerAngles = pos;
        }

        if (right)
        {
            var pos = transform.eulerAngles;
            pos.z -= Time.deltaTime * RotSpeed;
            transform.eulerAngles = pos;
        }


        bool bw = false;// Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);


        if (fw && !accelPressed)
        {
            lastAngle = (transform.eulerAngles.z * Mathf.PI) / 180;
            accelPressed = true;
        }
        accelPressed &= fw;

        if (fw)
        {
            float angle = (transform.eulerAngles.z * Mathf.PI) / 180;
            Vector3 force = Vector3.zero;
            force.x = -Mathf.Sin(angle) * MoveSpeed;
            force.y = Mathf.Cos(angle) * MoveSpeed;
            Forces.Add(force);
        }

        Vector3 forceSum = Vector3.zero;
        for (int i = 0; i < Forces.Count; ++i)
        {
            Vector3 force = Forces[i];
            forceSum += force;
            force *= 0.995f;
            if (force.magnitude < 0.006f)
                Forces.RemoveAt(i--);
            else
                Forces[i] = force;
        }


        transform.position += forceSum * Time.deltaTime / MoveSlowdown;

        //Accel -= Accel * (1.7f * Time.deltaTime);
        //Accel += ((fw ? 1 : 0) - (bw ? 1 : 0)) * 10f * Time.deltaTime;
        //Accel = Mathf.Clamp(Accel, -3, 5);

        //{
        //    float mult = Accel * Time.deltaTime;
        //    var pos = transform.position;
        //    var angle = lastAngle;
        //    pos.x -= Mathf.Sin(angle) * MoveSpeed * mult;
        //    pos.y += Mathf.Cos(angle) * MoveSpeed * mult;
        //    transform.position = pos;
        //}

        CorrectPosition(lineRenderer);
    }
}
