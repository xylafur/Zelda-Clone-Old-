using UnityEngine;
using System.Collections;

public class heartPickup : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D other){
        //Debug.Log("TRIGGERED");
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            Player player = other.gameObject.GetComponent<Player>();
            player.getHeart();
        }
    }
}
