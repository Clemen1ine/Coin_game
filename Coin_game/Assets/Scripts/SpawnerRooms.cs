using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerRooms : MonoBehaviour
{
    public GameObject[] topRoom;
    public GameObject[] downRoom;
    public GameObject[] rightRoom;
    public GameObject[] leftRoom;

    public List<GameObject> roome;

    public float waitTime;
    private bool spawnedExit;
    public GameObject Exit;
    public Vector2 exitSpawnOffset;

    void Update()
    {
        if (waitTime <= 0 && !spawnedExit)
        {
            for (int i = 0; i < roome.Count; i++)
            {
                if (i == roome.Count - 1)
                {
                    Vector3 exitSpawnPos = roome[i].transform.position + (Vector3)exitSpawnOffset;
                    Instantiate(Exit, exitSpawnPos, Quaternion.identity);
                    spawnedExit = true;
                }
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
}
