using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(LineRenderer))]
public class RockCreator : MonoBehaviour
{
    [SerializeField] private int vertices;
    [SerializeField] private Vector3 initialPos;
    [SerializeField] private float speed;
    [SerializeField] private float threshold;

    private float randomStartDeformation;
    
    // Start is called before the first frame update
    void Start()
    {
        randomStartDeformation = Random.Range(0f, 10f);
        GenerateGeometry();
    }

    // Update is called once per frame
    void Update()
    {
        GenerateGeometry();   
    }

    private void GenerateGeometry()
    {
        LineRenderer lr = GetComponent<LineRenderer>();

        lr.positionCount=vertices;
        lr.startWidth=0.25f;
        lr.endWidth=0.25f;
        
        //Quaternion rotate=Quaternion.Euler(0,0,360/Vertices);
        Vector3[] positions=new Vector3[vertices];
        
        positions[0]=initialPos;
        for(int i=1;i<vertices;i++)
        {
            positions[i]=Quaternion.Euler(0,0,360*i/(float)vertices)*initialPos;
            
            float deformation = randomStartDeformation + Time.time * speed;
            float offset = Mathf.PerlinNoise(
                positions[i].x + deformation, 
                positions[i].y + deformation ) * threshold;
            float remappedOffset = math.remap(0f, 1f, -1f, 1f, offset);
            positions[i].x += remappedOffset;
            positions[i].y += remappedOffset;
        }
        lr.SetPositions(positions);
        lr.loop=true;
    }

    private void OnValidate()
    {
        GenerateGeometry();
    }
}
