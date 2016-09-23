using UnityEngine;
using System.Collections;

public class SpawnZone : MonoBehaviour, spawns
{
    public GameObject enemy1;
    public GameObject oneByOne;
    public bool trig = false;


	void OnTriggerEnter2D(Collider2D other){
        if (!trig && other.gameObject.tag == "Player")
        {
            Instantiate(enemy1, new Vector3(-12f, -19f, 0f), Quaternion.identity);
            Instantiate(enemy1, new Vector3(-1.5f, -19f, 0f), Quaternion.identity);
            Instantiate(enemy1, new Vector3(-4f, -21f, 0f), Quaternion.identity);
            trig = true;
            /*Instantiate(oneByOne, new Vector3(-1.63f, 1.37f), Quaternion.identity);
            Instantiate(oneByOne, new Vector3(-3.23f, 1.370001f), Quaternion.identity);
            Instantiate(oneByOne, new Vector3(-1.63f, 2.98f), Quaternion.identity);
            Instantiate(oneByOne, new Vector3(-3.2f, 2.98f), Quaternion.identity);
            Instantiate(oneByOne, new Vector3(-3.2f, 4.56f), Quaternion.identity);
            Instantiate(oneByOne, new Vector3(-1.64f, 4.559999f), Quaternion.identity);
            Instantiate(oneByOne, new Vector3(4f, 4.559999f), Quaternion.identity);
            Instantiate(oneByOne, new Vector3(4f, 3f), Quaternion.identity);
            Instantiate(oneByOne, new Vector3(4f, 1.39f), Quaternion.identity);*/
        }
    }
    void OnTriggerExit2D(Collider2D other){
        /*if(other.gameObject.tag == "Player")
            trig = false;*/
        
    }
    public void isTrig()
    {
        trig = false;
    }
}
