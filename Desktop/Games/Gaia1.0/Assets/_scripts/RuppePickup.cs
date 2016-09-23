using UnityEngine;
using System.Collections;

public class RuppePickup : MonoBehaviour {

    public int value;

    private bool des = false;
    private float TOD;
    void Update()
    {
        DestroyPhase();
    }
    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player")
        {
            GetComponent<AudioSource>().Play();
            des = true;
            TOD = Time.time;
            other.gameObject.GetComponent<Player>().getMoney(value);
            GetComponent<PolygonCollider2D>().enabled = false;
            GetComponent<Renderer>().enabled = false;
        }
    }
    void DestroyPhase()
    {
        if (des && TOD - Time.time >= .5)
            Destroy(gameObject);
    }
}
