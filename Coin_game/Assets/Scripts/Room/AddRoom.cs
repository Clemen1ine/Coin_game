using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    public GameObject[] doors;
    public GameObject enemyType;
    public Transform[] enemySpawners;

    public List<GameObject> enemies;

    private bool _spawned;
    private bool _doorsDestroyed;

    private SpawnerRooms _variants;

    void Start()
    {
        _variants = GameObject.FindGameObjectWithTag("Roome").GetComponent<SpawnerRooms>();
        _variants.roome.Add(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !_spawned)
        {
            _spawned = true;

            StartCoroutine(CheckEnemies());

            foreach (Transform spawner in enemySpawners)
            {
                GameObject enemy = Instantiate(enemyType, spawner.position, Quaternion.identity) as GameObject;
                enemy.transform.parent = transform;
                enemies.Add(enemy);
            }
        }
    }

    IEnumerator CheckEnemies()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => enemies.Count == 0);
        DestroyDoors();
    }

    public void DestroyDoors()
    {
        foreach (GameObject door in doors)
        {
            if (door != null && door.transform.childCount != 0)
            {
                Destroy(door);
            }
        }

        _doorsDestroyed = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (_doorsDestroyed && other.CompareTag("Door"))
        {
            Destroy(other.gameObject);
        }
    }
}