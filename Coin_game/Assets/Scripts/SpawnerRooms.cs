using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnerRooms : MonoBehaviour
{
    public GameObject[] topRoom;
    public GameObject[] downRoom;
    public GameObject[] rightRoom;
    public GameObject[] leftRoom;

    public List<GameObject> roome;

    public float waitTime;
    private bool _spawnedExit;
    [FormerlySerializedAs("Exit")] public GameObject exit;
    public Vector2 exitSpawnOffset;

    void Update()
    {
        if (waitTime <= 0 && !_spawnedExit)
        {
            for (int i = 0; i < roome.Count; i++)
            {
                if (i == roome.Count - 1)
                {
                    Vector3 exitSpawnPos = roome[i].transform.position + (Vector3)exitSpawnOffset;
                    Instantiate(exit, exitSpawnPos, Quaternion.identity);
                    _spawnedExit = true;
                }
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
        }
    }
}
