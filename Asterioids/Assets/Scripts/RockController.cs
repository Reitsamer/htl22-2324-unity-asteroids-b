using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : MonoBehaviour
{
    [SerializeField] private Vector3 direction;

    [SerializeField] private float speed;
    
    // Update is called once per frame
    void Update()
    {
        transform.position +=
            new Vector3(
                direction.x * speed * Time.deltaTime,
                direction.y * speed * Time.deltaTime,
                0
            );
    }
}
