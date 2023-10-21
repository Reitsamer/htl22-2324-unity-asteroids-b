using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(LineRenderer), typeof(PolygonCollider2D))]
public class RockCreator : MonoBehaviour
{
    [SerializeField] private int vertices;
    [SerializeField] private Vector3 initialPos;
    [SerializeField] private float speed;
    [SerializeField] private float threshold;
    [SerializeField] private bool animate;

    private float randomStartDeformation;
    
    private LineRenderer lr;
    // private MeshCollider mc;
    private PolygonCollider2D pc;
    
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        // mc = GetComponent<MeshCollider>();
        pc = GetComponent<PolygonCollider2D>();
        
        randomStartDeformation = Random.Range(0f, 10f);
        GenerateGeometry();
        GenerateCollider();
    }

    // Update is called once per frame
    void Update()
    {
        if (animate)
        {
            GenerateGeometry();
            GenerateCollider();
        }
    }

    private void GenerateGeometry()
    {
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

    // private void GenerateCollider()
    // {
    //     Mesh mesh = new Mesh();
    //     lr.BakeMesh(mesh);
    //     mc.sharedMesh = mesh;
    // }

    private void GenerateCollider()
    {
        Vector3[] positions = new Vector3[lr.positionCount];
        lr.GetPositions(positions);

        Vector2[] positions2d = new Vector2[positions.Length];
        for (int i = 0; i < positions.Length; i++)
            positions2d[i] = positions[i];
        
        pc.SetPath(0, positions2d);
    }
    
    // private void OnValidate()
    // {
    //     lr = GetComponent<LineRenderer>();
    //     // mc = GetComponent<MeshCollider>();
    //     pc = GetComponent<PolygonCollider2D>();
    //
    //
    //     GenerateGeometry();
    //     GenerateCollider();
    // }
}
