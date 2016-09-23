using UnityEngine;
using System.Collections;

public class GenericSpawn : MonoBehaviour {

    public bool trig = false;
    public GameObject[] enemies;
    public Vector3[] spawnLocations;
	// Use this for initialization
	void OnTriggerEnter2D(Collider2D other){
        if (!trig && other.gameObject.tag == "Player")
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                Instantiate(enemies[i], spawnLocations[i], Quaternion.identity);
            }
            trig = true;
        }
        
    }
    void OnTriggerExit2D(Collider2D other)
    {
        /*if(other.gameObject.tag == "Player")
            trig = false;*/

    }
    public void isTrig()
    {
        trig = false;
    }
}
