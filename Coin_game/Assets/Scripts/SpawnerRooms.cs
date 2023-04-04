using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerRooms  : MonoBehaviour
{
    public GameObject[] topRoom;
    public GameObject[] downRoom;
    public GameObject[] rightRoom;
    public GameObject[] leftRoom;

    public List<GameObject> roome;
    
    public float waitTime;
    private bool spawnedExit;
    public GameObject Exit;
    
    void Update(){

        if(waitTime <= 0 && spawnedExit == false){
            for (int i = 0; i < roome.Count; i++) {
                if(i == roome.Count-1){
                    Instantiate(Exit, roome[i].transform.position, Quaternion.identity);
                    spawnedExit = true;
                }
            }
        } else {
            waitTime -= Time.deltaTime;
        }
    }
}