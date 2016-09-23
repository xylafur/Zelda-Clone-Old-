using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
    public Sprite forward, back, left, right;
    public Sprite forwardW, backW, leftW, rightW;
    public int health = 2;
    public int damage = 1;
    public float speed = .03f;
    public GameObject shot;
    public GameObject[] drops;
    public int experience;
    

    private int turnTimer, movementTimer = 15, animationTimer, attackTimer = 100, stallTimer = 20;
    private string[] directions = {"forward", "back", "left", "right" };
    private string direction;
    private bool attacking = false, dead = false;
    private float TOD;
    

    private BoxCollider2D bc;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private ShotScript ss;
    private AudioSource audio;

	void Start () {
        turnTimer = (int)(Random.value * 100) + 50;
        bc = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
        direction = directions[0];
        
	}
	
	
	void Update () {
        if (!dead)
        {
            if (stallTimer <= 0)
            {
                movement();
                if (turnTimer <= 0)
                    switchDirection();
                if (attackTimer <= 0)
                    attack();
                turnTimer--;
                attackTimer--;
            }

            stallTimer--;
            transform.rotation = Quaternion.identity;
        }

        if (health <= 0)
            death();
        
    //    float hpi = 3.14f / 2;
      //  transform.rotation = new Quaternion(0f, 0f, .01f, 0f);
	}
    void drop()
    {
        int chance = (int)(Random.value * 100);
        if (chance > 40)
        {
            //int pos = (Random.Range(0, drops.Length - 1 ));
            int pos = (Random.Range(0, drops.Length));
            Instantiate(drops[pos], transform.position, Quaternion.identity);
        }

    }
    void attack()
    {
        //attacking = true;
        //GameObject shot;
        
        ss = shot.GetComponent<ShotScript>();
        ss.changeDirection(direction);
        Instantiate(shot, transform.position, ss.rotation() );
        //ss.changeDirection(direction);

        stallTimer = 20;
        attackTimer = 200;
        
    }
    public void takeDamage(int damage)
    {
        health -= damage;
    }
    void movement()
    {
        if (!attacking)
        {
            if (direction == "forward")
            {
                if (animationTimer == 0)
                {
                    if (sr.sprite == forward )
                        sr.sprite = forwardW;
                    else if (sr.sprite == forwardW)
                        sr.sprite = forward;
                    //transform.position += new Vector3(0f, .8f, 0f);
                }
                transform.position += new Vector3(0f, speed, 0f);
            }
            else if (direction == "back")
            {
                if (animationTimer == 0)
                {
                    if (sr.sprite == back)
                        sr.sprite = backW;
                    else if (sr.sprite == backW)
                        sr.sprite = back;
                    //transform.position += new Vector3(0f, -.8f, 0f);
                }
                transform.position += new Vector3(0f, -speed, 0f);
            }
            else if (direction == "left")
            {
                if (animationTimer == 0)
                {
                    if (sr.sprite == left)
                        sr.sprite = leftW;
                    else if (sr.sprite == leftW)
                        sr.sprite = left;
                    //transform.position += new Vector3(-.8f, 0f, 0f);
                }
                transform.position += new Vector3(-speed, 0f, 0f);
            }
            else if (direction == "right")
            {
                if (animationTimer == 0)
                {
                    if (sr.sprite == right)
                        sr.sprite = rightW;
                    else if (sr.sprite == rightW)
                        sr.sprite = right;
                    //transform.position += new Vector3(.8f, 0f, 0f);
                }
                transform.position += new Vector3(speed, .0f, 0f);
            }
        }
        animationTimer++;

        if (animationTimer >= 10)
            animationTimer = 0;
        //movementTimer--;

    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {coll.gameObject.SendMessage("TakeDamage", damage);}
        else if (coll.gameObject.tag == "PlayerWeapon")
        {
            audio.Play();
            movementTimer = 0;
           // health--;
        }
        else
        {
            switchDirection();
            movement();
            //print("Should Switch");
        }
       
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
        if(Time.time - TOD >=1)
            Destroy(gameObject);

    }
    void switchDirection()
    {
        string str = directions[Random.Range(0, directions.Length)];
        while(str == direction)
            str = directions[Random.Range(0, directions.Length)];
        direction = str;
        turnTimer = (int)(Random.value * 100) + 25;
        movementTimer = 15;
        if (direction == "forward")
            sr.sprite = forward;
        else if (direction == "back")
            sr.sprite = back;
        else if (direction == "left")
            sr.sprite = left;
        else if (direction == "right")
            sr.sprite = right;
    }
    void switchDirection(string str)
    {
        direction = str;
        turnTimer = (int)(Random.value * 100) + 25;
        movementTimer = 0;
        if (direction == "forward")
            sr.sprite = forward;
        else if (direction == "back")
            sr.sprite = back;
        else if (direction == "left")
            sr.sprite = left;
        else if (direction == "right")
            sr.sprite = right;
        
    }
}
