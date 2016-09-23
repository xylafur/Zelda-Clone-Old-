using UnityEngine;
using System.Collections;

public class SkeletonScript : MonoBehaviour {

    public int health, experience = 20, damage = 1;
    public float moveSpeed = .03f;
    public Sprite[] sprites;
    public GameObject[] drops;
    private int moveTimer = 50, animTimer = 15, directionTimer = 200, stallTimer, damageCounter = 0;
    float TOD;
    bool moving = false, dead = false;
    string curDir; 
    string[] directions = new[] { "forward", "back", "left", "right" };

    BoxCollider2D bc;
    SpriteRenderer sr;
    AudioSource[] audio;
    
	void Start () {
        bc = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        audio = GetComponents<AudioSource>();
        curDir = directions[Random.Range(0, 3)];
	}
    public void takeDamage(int damage)
    {
        health -= damage;
        if (health > 0)
            audio[0].Play();
        else
            audio[1].Play();
    }
	// Update is called once per frame
	void Update () {
        if (!dead)
        {
            if (moving)
            {
                movement();
            }
            
            if (health <= 0)
                death();
            upkeep();
        }
        else
        {
            death();
        }
        transform.rotation = Quaternion.identity;
	}
    void OnCollisionEnter2D(Collision2D coll)
    {
        switchDirection();
        if (coll.gameObject.tag == "Player" && damageCounter <= 0)
        {
            coll.gameObject.SendMessage("TakeDamage", damage);
            damageCounter = 30;
        }
        else if (coll.gameObject.tag == "PlayerWeapon")
        {
            
            //health--;
            
        }

    }
    void upkeep()
    {
        moveTimer--;
        animTimer--;
        directionTimer--;
        stallTimer--;
        damageCounter--;
        if (moving && moveTimer <= 0)
        {
            moving = false;
            stallTimer = 50;
        }
        if (!moving && stallTimer <= 0)
        {
            moveTimer = 75;
            moving = true;
        }
        if (directionTimer <= 0)
            switchDirection();
        if (animTimer <= 0)
        {
            if (sr.sprite == sprites[0])
            {
                sr.sprite = sprites[1];
            }
            else
                sr.sprite = sprites[0];
            animTimer = 12;
        }
    }
    void switchDirection()
    {
      //  Debug.Log("should switch direction");
        directionTimer = 200;
        string temp = directions[Random.Range(0, 3)];
        do
        {
            if (temp != curDir)
                curDir = temp;
            temp = directions[Random.Range(0, 3)];
        } while (temp == curDir);
    }
    void movement()
    {
        if (curDir == "forward")
        {
            transform.position += new Vector3(0f, moveSpeed, 0f);
        }
        else if (curDir == "back")
        {
            transform.position += new Vector3(0f, -moveSpeed, 0f);
        }
        else if (curDir == "left")
        {
            transform.position += new Vector3(-moveSpeed, 0f, 0f);
        }
        else//right
        {
            transform.position += new Vector3(moveSpeed, 0f, 0f);
        }
       /* if (bc.IsTouchingLayers())
        {
            switchDirection();
        } */
    }
    void death()
    {

        GetComponent<Renderer>().enabled = false;
        bc.enabled = false;

        if (!dead)
        {
            drop();
            TOD = Time.time;
            GameObject player = GameObject.FindWithTag("Player");

            player.SendMessage("gainXP", experience);
        }
        dead = true;
        if (Time.time - TOD >= 1)
            Destroy(gameObject);

    }
    void drop()
    {
        int chance = (int)(Random.value * 100);
        if (chance > 40)
        {
            int pos = (int)(Random.Range(0, drops.Length - 1));
            Instantiate(drops[pos], transform.position, Quaternion.identity);
        }

    }
}
