using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {

    public Vector3 location;
    public GameObject sender;
    public bool toWorld;
    private bool usable = true;
    private AudioSource[] audio;

    void Start()
    {
        audio = GetComponents<AudioSource>();
    }
	
	void OnTriggerEnter2D(Collider2D other){
        if (usable)
        {
            other.gameObject.transform.position = location;
           // usable = false;
            if (toWorld)
            {
                foreach (AudioSource tunes in sender.GetComponents<AudioSource>() )
                {
                    tunes.Stop();
                }
                GameObject world = GameObject.FindGameObjectWithTag("GameController");
                world.GetComponent<AudioSource>().Play();
                audio[0].Play();
            }
            else
            {
                GameObject world = GameObject.FindGameObjectWithTag("GameController");
                world.GetComponent<AudioSource>().Stop();
                foreach (AudioSource tunes in audio)
                {
                    tunes.Play();
                }
            }
        }
        GameObject[] aliveEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in aliveEnemies)
        {
            Destroy(enemy);
            //Debug.Log("Playerfield should be clear");
        }
        GameObject[] otherSpawns = GameObject.FindGameObjectsWithTag("SpawnZone");
        foreach (GameObject zone in otherSpawns)
            zone.GetComponent<GenericSpawn>().isTrig();

    }
}
