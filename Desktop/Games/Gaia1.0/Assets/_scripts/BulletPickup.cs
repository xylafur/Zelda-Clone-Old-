using UnityEngine;
using System.Collections;

public class BulletPickup : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("TRIGGERED");
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            Player player = other.gameObject.GetComponent<Player>();
            player.getBullet();
        }
    }
}
