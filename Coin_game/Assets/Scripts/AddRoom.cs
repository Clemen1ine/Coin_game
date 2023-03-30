using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    public GameObject[] doors;
    public GameObject enemyType;
    public Transform[] enemySpawners;

    public List<GameObject> enemies;

    private bool spawned;
    private bool doorsDestroyed;

    private SpawnerRooms variants;

    void Start()
    {
        variants = GameObject.FindGameObjectWithTag("Roome").GetComponent<SpawnerRooms>();
        variants.roome.Add(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !spawned)
        {
            spawned = true;

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

        doorsDestroyed = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (doorsDestroyed && other.CompareTag("Door"))
        {
            Destroy(other.gameObject);
        }
    }
}