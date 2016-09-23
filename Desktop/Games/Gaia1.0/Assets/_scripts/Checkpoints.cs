using UnityEngine;
using System.Collections;

public class Checkpoints : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject player = other.gameObject;
            player.SendMessage("checkpoint", transform.position);
        }
    }
}
