using UnityEngine;
using System.Collections;

public class FireBallScript : MonoBehaviour {
    public float speed;
    public float cooldown;
    public int damage;
    public int manaCost = 1;

    float TOD, TOC;
    public string direction;
    bool des = false;
    AudioSource[] audio;
    BoxCollider2D bc;

	
	void Start () {
        audio = GetComponents<AudioSource>();
        bc = GetComponent<BoxCollider2D>();
        TOC = Time.time;
        audio[0].Play();
	}
    public int getManaCost()
    {
        return manaCost;
    }
    public string changeDirection(string dir)
    {
        return direction = dir;
    }
    public Quaternion rotation()
    {
       // Debug.Log(direction);
        //Debug.Log(direction);
        switch (direction)
        {
            case "back":
                return new Quaternion(0f, 0f, 180f, 0f);
            case "left":
                return Quaternion.Euler(0f, 0f, 90f);
            case "right":
                return Quaternion.Euler(0f, 0f, 270f);


        }
        return Quaternion.identity;
    }
    // Update is called once per frame
    void Update()
    {

        switch (direction)
        {
            case "back":
                transform.position += new Vector3(0f, -speed, 0f);
                break;
            case "left":
                transform.position += new Vector3(-speed, 0f, 0f);
                break;
            case "right":
                transform.position += new Vector3(speed, 0f, 0f);
                break;
            case "forward":
                transform.position += new Vector3(0f, speed, 0f);
                break;


        }
        //Debug.Log(direction);
        if (Time.time - TOC >= cooldown)
        {
            Destroy(gameObject);
        }
        if (des && Time.time - TOD >= .5)
        {
            Destroy(gameObject);
        }
    }
    void invertSpeed()
    {
        speed = -speed;
    }
    void invertDirection()
    {
        switch (direction)
        {
            case "forward":
                transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                break;
            case "right":
                transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                break;
            case "left":
                transform.rotation = Quaternion.Euler(0f, 0f, 270f);
                break;
            case "back":
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                break;


        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "EnemyShield")
        {
            invertSpeed();
            invertDirection();
            //Debug.Log("Hit Shield");
            return;
        }
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "SpawnZone")
        {
            bc.enabled = false;
            this.GetComponent<Renderer>().enabled = false;
            audio[1].Play();
            DestroyPhase();
        }
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.SendMessage("takeDamage", damage);
        }
    }
    void DestroyPhase()
    {
        TOD = Time.time;
        des = true;
    }
}
