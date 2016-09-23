using UnityEngine;
using System.Collections;

public class GameOverScreen : MonoBehaviour {
    public GameObject[] spawnZones;
    void Update()
    {
        for(int i = 0; i < spawnZones.Length; i++)
            spawnZones[i].GetComponent<spawns>().isTrig();
        if (Input.GetKey(KeyCode.A))
            GetComponent<Renderer>().enabled = false;
    }
    public void resetSpawns()
    {
        //for (int i = 0; i < spawnZones.Length; i++)
           // spawnZones[i].GetComponent<spawns>().isTrig();
        GameObject[] otherSpawns = GameObject.FindGameObjectsWithTag("SpawnZone");
        foreach (GameObject zone in otherSpawns)
        {
            
                zone.GetComponent<GenericSpawn>().isTrig();
           
        }
    }
}
