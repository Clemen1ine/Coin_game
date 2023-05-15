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
    
    public bool spawned = false;
    private SpawnerRooms _variants;
    private int _rand;
    
    private float _waitTime =4f;

    private void Start()
    {
        _variants = GameObject.FindGameObjectWithTag("Roome").GetComponent<SpawnerRooms>();
        Destroy(gameObject, _waitTime);
        Invoke("Spawn", 0.2f);
    }

    private void Spawn()
    {
        if (!spawned)   
        {
            if (direction == Direction.Top)
            {
                _rand = UnityEngine.Random.Range(0, _variants.topRoom.Length);
                Instantiate(_variants.topRoom[_rand], transform.position, _variants.topRoom[_rand].transform.rotation);
            }
            else if (direction == Direction.Down)
            {
                _rand = UnityEngine.Random.Range(0, _variants.downRoom.Length);
                Instantiate(_variants.downRoom[_rand], transform.position, _variants.downRoom[_rand].transform.rotation);
            }
            else if (direction == Direction.Right)
            {
                _rand = UnityEngine.Random.Range(0, _variants.rightRoom.Length);
                Instantiate(_variants.rightRoom[_rand], transform.position, _variants.rightRoom[_rand].transform.rotation);
            }
            else if (direction == Direction.Left)
            {
                _rand = UnityEngine.Random.Range(0, _variants.leftRoom.Length);
                Instantiate(_variants.leftRoom[_rand], transform.position, _variants.leftRoom[_rand].transform.rotation);
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