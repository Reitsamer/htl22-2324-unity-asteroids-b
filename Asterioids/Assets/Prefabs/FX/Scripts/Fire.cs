using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public ParticleSystem particlesystem;

    // Start is called before the first frame update
    void Start()
    {
        particlesystem.Stop();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            particlesystem.Play();

        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            particlesystem.Stop();
        }
    }
}
