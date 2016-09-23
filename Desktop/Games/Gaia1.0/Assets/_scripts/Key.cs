using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour {
    private AudioSource audio;
    private float TOD;
    private bool used = false;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }
	void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == ("Player")){
            other.gameObject.SendMessage("getKey");
            audio.Play();
            GetComponent<Renderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            TOD = Time.time;
            used = true;
            //death();
            
        }
    }
    void Update()
    {
        death();
    }
    void death()
    {
        if(used && Time.time - TOD >= .5)
         Destroy(gameObject);
    }
}
