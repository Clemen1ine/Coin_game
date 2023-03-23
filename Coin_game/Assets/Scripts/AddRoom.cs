using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour {

    private SpawnerRooms variants;

    void Start()
    {
        variants = GameObject.FindGameObjectWithTag("Roome").GetComponent<SpawnerRooms>();
        variants.roome.Add(this.gameObject);
    }
}