using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    public float speed = 1;
    
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
    }
}
