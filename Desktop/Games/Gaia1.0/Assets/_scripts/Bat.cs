using UnityEngine;
using System.Collections;

public class Bat : MonoBehaviour {
    public Sprite bat, batF;
    public int animSpeed = 10, health = 2, damage = 1, experience, directionTimer = 200;
    public GameObject[] drops;

    private int animTimer = 0, damageCounter = 0;
    private float xVelocity, yVelocity, TOD;
    private bool dead = false;
    private bool invert = true;

    private SpriteRenderer sr;
    private AudioSource audio;
    private BoxCollider2D bc;
	
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();
        bc = GetComponent<BoxCollider2D>();
        xVelocity = (float) ((Random.value * .06) - .03);
        yVelocity = (float)((Random.value * .06) - .03);
	}
	
	// Update is called once per frame
    public void takeDamage(int damage)
    {
        health -= damage;
        audio.Play();

        //health--;
        if (health <= 0)
        {
            dead = true;
        }
    }
	void Update () {
        if (!dead)
        {
            movement();
            animation();
            upKeep();
        }
        else if (dead)
            death();
	}
    void switchDirection()
    {
        if ((xVelocity > .015 || xVelocity < -.015) && (yVelocity > .015 || yVelocity < -.015) && !bc.IsTouchingLayers())
        {
            inverVelocity();
            invert = false;
        }
        else
        {
            newSpeed();
            invert = true;
        }
    }
    void newSpeed()
    {
        xVelocity = (float)((Random.value * .06) - .03);
        yVelocity = (float)((Random.value * .06) - .03);
    }
    void movement()
    {
        transform.rotation = Quaternion.identity;
        transform.position += new Vector3(xVelocity, yVelocity, 0f);
        if (bc.IsTouchingLayers())
        {
            switchDirection();
        }
    }
    void inverVelocity()
    {
        if (invert)
        {
            invertXVelocity();
            invert = false;
        }
        else{
            invertYVelocity();
            invert = true;
        }
    }
    void invertYVelocity()
    {
        yVelocity = -yVelocity;
    }
    void invertXVelocity()
    {
        xVelocity = -xVelocity;
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player" && damageCounter<=0)
        {
            coll.gameObject.SendMessage("TakeDamage", damage);
             damageCounter = 30;
        }
        else if (coll.gameObject.tag == "PlayerWeapon")
        {
            
        }
        else
        {
            newSpeed();
        }
        
    }
    void animation()
    {
        if (animTimer >= animSpeed)
        {
            if (sr.sprite == bat)
                sr.sprite = batF;
            else if (sr.sprite == batF)
                sr.sprite = bat;
        }
    }
    void upKeep()
    {
        animTimer++;
        directionTimer--;
        damageCounter--;
        if (directionTimer <= 0)
        {
            directionTimer = 200;
            switchDirection();
        }
        if (animTimer > animSpeed)
            animTimer = 0;
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
