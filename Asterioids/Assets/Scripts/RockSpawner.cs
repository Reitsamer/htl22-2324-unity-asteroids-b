using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour, IStartStop
{
    [SerializeField]
    public GameController controller;
    [SerializeField]
    private GameObject RockPrefab;

    // Start is called before the first frame update
    void Start()
    {
        ClearRocks();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnRocks()
    {
        // TODO: random pos along border
        int count = controller.TotalRockCount;

        for (int i = 0; i < count; i++)
        {
            var rock = Instantiate(RockPrefab);
            var rockCreator = rock.GetComponent<RockCreator>();
            rockCreator.randomStartDeformation = 1 + new System.Random().Next(1000);

            rockCreator.startSize = 4;
            rockCreator.threshold /= 1.5f;
            rockCreator.isClone = false;
            rockCreator.spawner = this;

            var rockController = rock.GetComponent<RockController>();
            rockController.MoveSpeed *= 1.8f;
            rockController.controller = controller;

            rock.transform.position = new Vector3(0, 100);

            RockCreated();
        }
    }

    public void ClearRocks()
    {
        foreach (Transform child in transform)
            GameObject.Destroy(child.gameObject);
    }

    public void StartGame()
    {
        SpawnRocks();
    }

    public void StopGame()
    {
        
    }

    public void RockCreated()
    {
        controller.CurrentRockCount++;
    }

    public void RockShot()
    {
        controller.CurrentRockCount--;
        if (controller.CurrentRockCount < 1)
        {
            Debug.Log("Level Done!");
            controller.NextLevel();
        }
    }
}
