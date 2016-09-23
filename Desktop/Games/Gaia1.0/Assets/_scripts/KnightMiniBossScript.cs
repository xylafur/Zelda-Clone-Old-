using UnityEngine;
using System.Collections;

public class KnightMiniBossScript : MonoBehaviour {

    static bool dropKey = false;
    public int  health, experience;
    public int maxMover;
    public float moveSpeed, runSpeed, playerRadius, stopRadius;
    public Sprite[] sprites;
    public GameObject player;
    public GameObject drop;

    private int moveTimer, animTimer = 0, orieTimer, maxOrie, animWalk = 15, animRun = 10;
    private float TOD;
    private bool moving, dead = false, playerWithinRadius, orientation = false;
    private string direction;

    public GameObject sword, shield;
    private Rigidbody2D rb;
    private AudioSource[] audios;
    private BoxCollider2D[] bc;
    private SpriteRenderer sr;
    
	void Start () {
        bc = new[] { this.GetComponent<BoxCollider2D>() };//, sword.GetComponent<BoxCollider2D>(),
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        audios = GetComponents<AudioSource>();
        maxOrie = maxMover * 4;
        moveTimer = maxMover / 2;

        }
	
	
	void Update () {
        animation();
        movement();
        
        upkeep();

        //Debug.Log(direction);
	}
    void upkeep()
    {
        //transform.rotation = Quaternion.identity;
        sword.GetComponent<KnightSword>().switchCollider(direction);
        shield.GetComponent<Shield>().switchCollider(direction);
        moveTimer--;
        orieTimer--;
        animTimer--;
        transform.rotation = Quaternion.identity;
        if (health <= 0)
            Death();
    }
    void Death()
    {

        audios[1].Play();
        bc[0].enabled = false;
        sword.GetComponent<BoxCollider2D>().enabled = false;
        if (!dropKey)
        {
            Instantiate(drop, this.transform.position, Quaternion.identity); 
            dropKey = true;
        }
        GetComponent<Renderer>().enabled = false;
        if (!dead)
            TOD = Time.time;
        dead = true;
        if (Time.time - TOD >= .5)
            Destroy(gameObject);
    }
    void animation()
    {
        if (animTimer <= 0)
        {
            switch (direction)
            {
                case "forward":
                    if (sr.sprite == sprites[0])
                        sr.sprite = sprites[1];
                    else
                        sr.sprite = sprites[0];
                    break;
                case "back":
                    if (sr.sprite == sprites[2])
                        sr.sprite = sprites[3];
                    else
                        sr.sprite = sprites[2];
                    break;
                case "left":
                    if (sr.sprite == sprites[4])
                        sr.sprite = sprites[5];
                    else
                        sr.sprite = sprites[4];
                    break;
                case "right":
                    if (sr.sprite == sprites[6])
                        sr.sprite = sprites[7];
                    else
                        sr.sprite = sprites[6];
                    break;
            }
            if (animTimer <= 0)
            {
                if (!playerInRadius())
                    animTimer = animWalk;
                else
                    animTimer = animRun;
            }
        }
    }
    bool playerInRadius()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 mag = player.transform.position - this.transform.position;
        
        if (mag.magnitude <= playerRadius)
        {
            playerWithinRadius = true;
            return true;
        }
        //Debug.Log(mag.magnitude);
        playerWithinRadius = false;
        return false;
    }
    void movement()
    {
        //orientation 0(False): Horizontal 1(True): vertical
        //Debug.Log(player.GetComponent<Player>().IsDead());
        if ( player.GetComponent<Player>().IsDead() || !playerInRadius() )
        {
            if (orientation)
            {
                if (direction == "right")
                {
                    transform.position += new Vector3(moveSpeed, 0f, 0f);
                }
                else if (direction == "left")
                {
                    transform.position += new Vector3(-moveSpeed, 0f, 0f);
                }
            }
            else
            {
                if (direction == "forward")
                {
                    transform.position += new Vector3(0f, moveSpeed, 0f);
                }
                else if (direction == "back")
                {
                    transform.position += new Vector3(0f, -moveSpeed, 0f);
                }
            }
            if (moveTimer <= 0)
            {
                switchDirection();
                animTimer = 0;
                moveTimer = maxMover;
            }
            if (orieTimer <= 0)
            {
                if (orientation)
                    direction = "forward";
                else
                    direction = "right";
                orientation = !orientation;
                orieTimer = maxOrie;
                animTimer = 0;

            }
        }
        else
        {
            chasePlayer();
        }
    }
    void chasePlayer()
    {
        //Debug.Log("Player is i radius");
        Vector3 mag = player.transform.position - this.transform.position;
        if (mag.sqrMagnitude > stopRadius)
        {
            if (Mathf.Abs(mag.y) >= .5)
            {
                if (player.transform.position.y > this.transform.position.y)
                {
                    transform.position += new Vector3(0f, runSpeed, 0f);
                    if (direction != "forward")
                        animTimer = 0;
                    direction = "forward";
                }
                else
                {
                    transform.position += new Vector3(0f, -runSpeed, 0f);
                    if (direction != "back")
                        animTimer = 0;
                    direction = "back";
                }
            }
            else
            {
                if (player.transform.position.x > this.transform.position.x)
                {
                    transform.position += new Vector3(runSpeed, 0f, 0f);
                    if (direction != "right")
                        animTimer = 0;
                    direction = "right";
                }
                else
                {
                    transform.position += new Vector3(-runSpeed, -0f, 0f);
                    if (direction != "left")
                        animTimer = 0;
                    direction = "left";
                }
            }
        }
    }
    public void takeDamage(int dam)
    {
        health -= dam;
        audios[0].Play();
    }
    void switchDirection()
    {
        switch (direction)
        {
            case "forward":
                direction = "back";
                break;
            case "back":
                direction = "forward";
                break;
            case "left":
                direction = "right";
                break;
            case "right":
                direction = "left";
                    break;
        }
        
    }

    void OnCollisionEnter2D()
    {
        switchDirection();
    }
}
