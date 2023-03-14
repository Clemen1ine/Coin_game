using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Direction direction;

    public enum Direction
    {
        Top,
        Down,
        Left,
        Right,
        None
    }

    private SpawnerRooms variants;
    private int rand;
    private bool spawned = false;
    private float waitTime = 3f;

    private void Start()
    {
        variants = GameObject.FindGameObjectWithTag("Roome").GetComponent<SpawnerRooms>();
        Destroy(gameObject, waitTime);
        Invoke("Spawn", 0.2f);
    }

    private void Spawn()
    {
        if (!spawned)   
        {
            if (direction == Direction.Top)
            {
                rand = UnityEngine.Random.Range(0, variants.topRoom.Length);
                Instantiate(variants.topRoom[rand], transform.position, variants.topRoom[rand].transform.rotation);
            }
            else if (direction == Direction.Down)
            {
                rand = UnityEngine.Random.Range(0, variants.downRoom.Length);
                Instantiate(variants.downRoom[rand], transform.position, variants.downRoom[rand].transform.rotation);
            }
            else if (direction == Direction.Right)
            {
                rand = UnityEngine.Random.Range(0, variants.rightRoom.Length);
                Instantiate(variants.rightRoom[rand], transform.position, variants.rightRoom[rand].transform.rotation);
            }
            else if (direction == Direction.Left)
            {
                rand = UnityEngine.Random.Range(0, variants.leftRoom.Length);
                Instantiate(variants.leftRoom[rand], transform.position, variants.leftRoom[rand].transform.rotation);
            }

            spawned = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("SpawnPoint") && other.GetComponent<SpawnPoint>().spawned)
        {
            Destroy(gameObject);
        }
    }
}