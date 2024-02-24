using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;


public class RockSizeArgs : EventArgs
{
    public float size;

    public RockSizeArgs(float size)
    {
        this.size = size;
    }
}


[RequireComponent(typeof(LineRenderer), typeof(PolygonCollider2D))]
public class RockCreator : MonoBehaviour
{
    [SerializeField]
    public int vertices = 7;
    [SerializeField]
    public float threshold = 4;
    [SerializeField]
    public float speed = 0.3f;
    [SerializeField]
    public float startSize = 3;

    [SerializeField]
    private GameObject RockPrefab;

    public float randomStartDeformation = 1;
    private LineRenderer lineRenderer;
    private PolygonCollider2D polygonCollider2D;
    private Vector3[] rockPoints;
    public RockSpawner spawner;

    public bool isClone = false;

    public EventHandler<RockSizeArgs> SendMessageToPlayer;

    [SerializeField]
    public ParticleSystem ExplodeParticle;

    void Start()
    {
        ExplodeParticle.Stop();
        RegenerateRock();
        transform.parent = spawner.transform;
    }

    void OnValidate()
    {
        RegenerateRock();
    }

    void Update()
    {
        RegenerateRock();
    }

    public void RegenerateRock()
    {
        if (vertices < 1)
            vertices = 1;

        if (polygonCollider2D == null)
            polygonCollider2D = GetComponent<PolygonCollider2D>();

        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();

        GenerateGeometry();
        GenerateCollider();
    }

    private void GenerateGeometry()
    {
        Vector3 initialPos = new Vector3(0, startSize, 0);

        lineRenderer.positionCount = vertices;
        lineRenderer.startWidth = 0.08f;
        lineRenderer.endWidth = 0.08f;

        rockPoints = new Vector3[vertices];

        for (int i = 0; i < vertices; i++)
        {
            float angle = (360 * i) / (float)vertices;
            float angleDeg = (angle * Mathf.PI) / 180;
            rockPoints[i] = Quaternion.Euler(0, 0, angle) * initialPos;
            float deformation = randomStartDeformation + Time.time * speed + (i * 100);
            float offset = Mathf.PerlinNoise(
                rockPoints[i].x + deformation,
                rockPoints[i].y + deformation) * threshold;
            float remapStrenght = offset - (threshold / 2);
            rockPoints[i].x -= Mathf.Sin(angleDeg) * remapStrenght; //remappedOffset;
            rockPoints[i].y += Mathf.Cos(angleDeg) * remapStrenght; //remappedOffset;
        }
        lineRenderer.SetPositions(rockPoints);
        lineRenderer.loop = true;
    }

    public void GenerateCollider()
    {
        polygonCollider2D.SetPath(0,
            rockPoints.Select(p => (Vector2)p)
            .ToArray());
    }



    public void HandleShoot()
    {

        if (!isClone)
            for (int i = 0; i < 2; i++)
            {
                var rock = Instantiate(RockPrefab);
                var rockCreator = rock.GetComponent<RockCreator>();
                rockCreator.randomStartDeformation = randomStartDeformation + new System.Random().Next(1000);

                rockCreator.startSize /= 2;
                rockCreator.threshold /= 1.5f;
                rockCreator.isClone = true;
                rockCreator.spawner = spawner;

                var rockController = rock.GetComponent<RockController>();
                rockController.MoveSpeed *= 1.8f;
                rockController.controller = spawner.controller;
                spawner.RockCreated();
            }

        ExplodeParticle.Stop();
        ExplodeParticle.Play();

        spawner.RockShot();
        Destroy(gameObject);
    }

    public float GetSize()
    {
        return startSize;
    }
}
