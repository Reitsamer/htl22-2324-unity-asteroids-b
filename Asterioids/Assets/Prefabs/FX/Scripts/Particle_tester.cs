using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_tester : MonoBehaviour
{
    public ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            ps.Stop();
            ps.Play();
        }
    }
}
