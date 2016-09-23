using UnityEngine;
using System.Collections;

public class ManaPickup : MonoBehaviour {
    public int strength = 1;
    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            Player player = other.gameObject.GetComponent<Player>();
            player.aquireMana(strength);
        }
    }
}
