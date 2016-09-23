using UnityEngine;
using System.Collections;

public class ShotScript : MonoBehaviour {

    public int damage = 1, timeOut = 25;
    public float speed = .05f;
    public string direction;

	// Use this for initialization
	void Start () {
	
	}
    public Quaternion rotation()
    {
        //Debug.Log(direction);
        switch (direction)
        {
            case "forward":
                return new Quaternion(0f, 0f, 180f, 0f);
            case "left":
                return Quaternion.Euler(0f, 0f, 270f);
            case "right":
                return Quaternion.Euler(0f, 0f, 90f);

               
        } 
        return Quaternion.identity;
    }
    public void changeDirection(string str)
    {
        direction = str;
        
        
    }
	// Update is called once per frame
	void Update () {
        timeOut--;
        if (timeOut <= 0)
            Destroy(gameObject);
        if (direction == "forward")
            transform.position += new Vector3(0f, speed, 0f);
        else if (direction == "back")
            transform.position += new Vector3(0f, -speed, 0f);
        else if (direction == "left")
            transform.position += new Vector3(-speed, 0f, 0f);
        else if (direction == "right")
            transform.position += new Vector3(speed, 0f, 0f);
	}
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
            coll.gameObject.SendMessage("TakeDamage", damage);
        if(coll.gameObject.tag != "Enemy")
            Destroy(gameObject);

    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag != "Enemy")
            Destroy(gameObject);
    }
}
