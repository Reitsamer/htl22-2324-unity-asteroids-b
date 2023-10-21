using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public float speed = 1;

    [SerializeField] private Transform bullet;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // LineRenderer lr = GetComponent<LineRenderer>();
            // Transform transform = GetComponent<Transform>();
            transform.Rotate(new Vector3(0, 0, 1), speed * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(Vector3.forward, -speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            LineRenderer lr = GetComponent<LineRenderer>();
            Vector3 position = lr.GetPosition(0);

            var thePosition = transform.TransformPoint(position);
            Instantiate(bullet, thePosition, transform.rotation);
        }
    }
}
