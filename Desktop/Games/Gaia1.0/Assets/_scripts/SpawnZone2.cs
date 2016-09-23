using UnityEngine;
using System.Collections;

public class SpawnZone2 : MonoBehaviour, spawns
{
    public GameObject enemy1, enemy2;
    public bool trig = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!trig && other.gameObject.tag == "Player")
        {
            Instantiate(enemy1, new Vector3(-21.96149f, -18.9861f, 0f), Quaternion.identity);
            Instantiate(enemy1, new Vector3(-18.76f, -21.68f, 0f), Quaternion.identity);
            Instantiate(enemy1, new Vector3(-17.89f, -17.75f, 0f), Quaternion.identity);
            Instantiate(enemy2, new Vector3(-24.56f, -19.58f, 0f), Quaternion.identity);
            trig = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
       /* if (other.gameObject.tag == "Player")
            trig = false;
        */
    }
    public void isTrig()
    {
        trig = false;
    }
}
