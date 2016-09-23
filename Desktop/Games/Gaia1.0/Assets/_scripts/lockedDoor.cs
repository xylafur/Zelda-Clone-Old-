using UnityEngine;
using System.Collections;

public class lockedDoor : MonoBehaviour {
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            if(player.useKey() )
            {
                GetComponent<AudioSource>().Play();
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<Renderer>().enabled = false;
            }
        }
    }
}
