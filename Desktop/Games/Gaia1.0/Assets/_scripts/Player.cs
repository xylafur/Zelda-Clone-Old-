using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public Sprite forward, forwardW, forwardA;
    public Sprite back, backW, backA;
    public Sprite right, rightW, rightA;
    public Sprite left, leftW, leftA;
    public GameObject[] life = new GameObject[3];
    public GameObject gameOver, Bullet, FireBall;
    public GameObject healthBar, manaBar, bulletDisplay, moneyDisplay;
    


    string direction = "forward";
    bool walk = false, attacking = false, isDead = false, usingInterface = false;
    int maxMana = 10, currentMana = 10, bullets = 10, maxBullets = 10;
    int animationTimer = 0, health = 3, maxHealth = 3, deathCounter = 0, loc = 0, keys = 0;
    float attackTime;
    public int experience = 0, level = 1, xpToLevel, money = 0;

   
    private Sprite[] spin;
    private BulletScript bullet;
    private FireBallScript magic;
    private AudioSource[] AudioSources;
    private SpriteRenderer sr;
    private BoxCollider2D bc;
    private Sword sword;
    private Vector3 checkPoint;
	// Use this for initialization
	void Start () {
        sr = gameObject.GetComponent<SpriteRenderer>();
        bc = gameObject.GetComponent<BoxCollider2D>();
        AudioSources = gameObject.GetComponents<AudioSource>();
        sword = gameObject.GetComponentInChildren<Sword>();
        bc.offset = new Vector2(0f, 0f);
        checkPoint = new Vector3(-6.98f, -28.31f, 0f);
        if (sr.sprite == null)
            sr.sprite = forward;
        gameOver.GetComponent<Renderer>().enabled = false;
        xpToLevel = (int)50 * level;
        setHearts();
        spin = new Sprite[] {forward, right, back , left};
        healthBarUpkeep(true);
        manaBar.GetComponent<ManaBarScript>().setMaxMana(maxMana);
        bulletDisplay.GetComponent<BulletCountScript>().setBullets(bullets);
        moneyDisplay.GetComponent<MoneyCountScript>().setMoney(money);
	}
    void healthBarUpkeep(bool newMax)
    {
        HealthScript tempScript = healthBar.GetComponent<HealthScript>();
        tempScript.setHealth(maxHealth);
        if (newMax)
        {
            tempScript.maxHealthUpdate(maxHealth);
        }
        else
        {
            tempScript.healthUpdate(health);
        }
    }
    public void checkpoint(Vector3 check)
    {
        checkPoint = check;
    }
    public bool useKey()
    {
        if (keys > 0)
        {
            keys--;
            return true;
        }
        return false;
    }
    public void getKey()
    {
        keys++;
    }
    public void gainXP(int xp)
    {
        experience += xp;
        levelUp();
    }
    public void levelUp()
    {
        if (experience >= xpToLevel)
        {
            experience = experience - xpToLevel;

            level++;
            xpToLevel = (int)50 * level;
            gainHeart();
            restoreHealth();
            healthBarUpkeep(true);
        }
    }
    public void gainHeart()
    {
        health = maxHealth++;
       
        setHearts();
    }
    public void setHearts()
    {
        for (int i = 0; i < health; i++)
        {
            life[i].GetComponent<Renderer>().enabled = true;
        }
        for (int i = health; i < life.Length; i++)
        {
            life[i].GetComponent<Renderer>().enabled = false;
        }
    }
    public void aquireMana(int ammount)
    {
        currentMana += ammount;
        currentMana = currentMana > maxMana ? maxMana : currentMana;
        manaBar.GetComponent<ManaBarScript>().setMana(currentMana);
    }
    public void getMoney(int value)
    {
        money += value;

    }
    public void getHeart()
    {
        int temp = health;
        if (health < maxHealth)
        {
            health++;
            life[temp].GetComponent<Renderer>().enabled = true;
        }

    }
    public void getBullet()
    {
        int temp = bullets;
        if (bullets < maxBullets)
        {
            bullets++;
            bulletDisplay.GetComponent<BulletCountScript>().setBullets(bullets);
        }
    }
    public void restoreHealth()
    {
        health = maxHealth;
        for (int i = 0; i < maxHealth; i++)
        {
            life[i].GetComponent<Renderer>().enabled = true;
        }
        
    }
	// Update is called once per frame
    void Update()
    {
        //bc = null;
        if (!isDead && !usingInterface)
        {
            upkeep();
            checkInputs();
            healthBarUpkeep(false);
        }
        if (isDead)
            death();
    }
    public int getAmmount(string type)
    {
        switch (type)
        {
            case "Bullet":
                return bullets;
            case "Mana":
                return currentMana;
            case "Heart":
                return health;
            case "Money":
                return money;
        }
        return 0;
    }
    public void getItem(string type)
    {
        switch (type)
        {
            case "Bullet":
                bullets++;
                break;
            case "Mana":
                currentMana++;
                break;
            case "Heart":
                health++;
                break;
        }
    }
    public bool transaction(int ammount)
    {
        if (money - ammount >= 0)
        {
            money = money - ammount;
            return true;
        }
        return false;
    }
    public int getMax(string type)
    {
        switch (type)
        {
            case "Bullet":
                return maxBullets;
            case "Mana":
                return maxMana;
            case "Heart":
                return maxHealth;
        }
        return 0;
    }

    public void modeSwitch(bool enter)
    {
        usingInterface = enter;
    }
    void checkInputs()
    {
        if (Input.GetKey(KeyCode.H))
            restoreHealth();
        if (attacking && Time.time - attackTime >= .25)
        {
            attacking = false;
            endAttack();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            attack();
        }
        else if (Input.GetKeyDown(KeyCode.S) && bullets >0)
        {
            bulletDisplay.GetComponent<BulletCountScript>().setBullets(--bullets);
            shoot();
        }
        else if (Input.GetKeyDown(KeyCode.F) && currentMana - FireBall.GetComponent<FireBallScript>().getManaCost() >= 0)
        {
            currentMana -= FireBall.GetComponent<FireBallScript>().getManaCost();
            manaBar.GetComponent<ManaBarScript>().setMana(currentMana);
            shootFire();
        }
        else if (!attacking)
        {
            sword.noAttack();
            bc.offset = new Vector2(0f, 0f);
            movement();

        }
    }
    void upkeep()
    {
        transform.rotation = Quaternion.identity;
        sword.updatePos(transform.position);
        moneyDisplay.GetComponent<MoneyCountScript>().setMoney(money);
        manaBar.GetComponent<ManaBarScript>().setMana(currentMana);
        bulletDisplay.GetComponent<BulletCountScript>().setBullets(bullets);
        if (bullets > maxBullets)
            bullets = maxBullets;
        if (currentMana > maxMana)
            currentMana = maxMana;
        if (health > maxHealth)
            health = maxHealth;
    }
    void shootFire()
    {
        magic = FireBall.GetComponent<FireBallScript>();
        string temp = magic.changeDirection(direction);
        switch (temp)
        {
            case "forward":
                GameObject.Instantiate(FireBall, this.transform.position + new Vector3(0f, .2f, 0f), magic.rotation());
                break;
            case "back":
                GameObject.Instantiate(FireBall, this.transform.position + new Vector3(0f, -.2f, 0f), magic.rotation());
                break;
            case "right":
                GameObject.Instantiate(FireBall, this.transform.position + new Vector3(0.2f, 0f, 0f), magic.rotation());
                break;
            case "left":
                GameObject.Instantiate(FireBall, this.transform.position + new Vector3(-.2f, 0f, 0f), magic.rotation());
                break;
        }
    }
    void shoot()
    {
        bullet = Bullet.GetComponent<BulletScript>();
        string temp = bullet.changeDirection(direction);
        switch(temp){
            case "forward":
                 GameObject.Instantiate(Bullet, this.transform.position + new Vector3(0f, .2f, 0f), bullet.rotation());
                 break;
            case "back":
                 GameObject.Instantiate(Bullet, this.transform.position + new Vector3(0f, -.2f, 0f), bullet.rotation() );
                break;
            case "right":
                GameObject.Instantiate(Bullet, this.transform.position + new Vector3(0.2f, 0f, 0f), bullet.rotation());
                break;
            case "left":
                GameObject.Instantiate(Bullet, this.transform.position + new Vector3(-.2f, 0f, 0f), bullet.rotation());
                break;
    }
    }
    void attack()
    {
        sword.playAudio();
        attacking = true;
        //bc = gameObject.GetComponentInChildren<BoxCollider2D>();
        if (direction == "forward")
        {

            bc.offset = new Vector2(0f, -.06f);
            sr.sprite = forwardA;
           // transform.position += new Vector3(0f, .25f, 0f);
            sword.attackF();
        }
        else if (direction == "back")
        {
            bc.offset = new Vector2(0.0007350981f, 0.05982215f);
            sr.sprite = backA;
            sword.attackB();
        }
        else if (direction == "left")
        {
            bc.offset = new Vector2(0.0601184f, 0.0004388273f);
            sr.sprite = leftA;
            sword.attackL();
        }
            
        else if (direction == "right")
        {
            bc.offset = new Vector2(-0.06384977f, 0.0004388273f);
            sr.sprite = rightA;
            sword.attackR();
        }
        attackTime = Time.time;
    }
    void endAttack()
    {
        sword.noAttack();
        if (direction == "forward")
        {
            sr.sprite = forward;
            //transform.position -= new Vector3(0f, .25f, 0f);
        }
        else if (direction == "back")
            sr.sprite = back;
        else if (direction == "left")
            sr.sprite = left;
        else if (direction == "right")
            sr.sprite = right;
    }
    void movement()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (direction != "forward")
                sr.sprite = forward;
            direction = "forward";

                if (animationTimer == 0){
                    if(sr.sprite == forward||attacking)
                        sr.sprite = forwardW;
                    else if (sr.sprite == forwardW)
                        sr.sprite = forward;
                    //transform.position += new Vector3(0f, .8f, 0f);
                }
                transform.position += new Vector3(0f, .05f, 0f);
        }

        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (direction != "back")
                sr.sprite = back;
            direction = "back";

            if (animationTimer == 0)
            {
                if (sr.sprite == back)
                    sr.sprite = backW;
                else if (sr.sprite == backW)
                    sr.sprite = back;
                //transform.position += new Vector3(0f, -.8f, 0f);
            }
            transform.position += new Vector3(0f, -.05f, 0f);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (direction != "left")
                sr.sprite = left;
            direction = "left";

            if (animationTimer == 0)
            {
                if (sr.sprite == left)
                    sr.sprite = leftW;
                else if (sr.sprite == leftW)
                    sr.sprite = left;
                //transform.position += new Vector3(-.8f, 0f, 0f);
            }
            transform.position += new Vector3(-.05f, 0f, 0f);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (direction != "right")
                sr.sprite = right;
            direction = "right";

            if (animationTimer == 0)
            {
                if (sr.sprite == right)
                    sr.sprite = rightW;
                else if (sr.sprite == rightW)
                    sr.sprite = right;
                //transform.position += new Vector3(.8f, 0f, 0f);
            }
            transform.position += new Vector3(.05f, 0f, 0f);
        }

        

        else if(Input.anyKey == false)
        {
            if (direction == "forward")
                sr.sprite = forward;
            else if (direction == "back")
                sr.sprite = back;
            else if(direction == "left")
                sr.sprite = left;
            else if (direction == "right")
                sr.sprite = right;

            animationTimer = -1;
        }
        animationTimer++;

        if (animationTimer >= 10)
            animationTimer = 0;
        attacking = false;
	}
    public bool IsDead()
    {
        return isDead;
    }
    public void TakeDamage(int damage)
    {
        if (health > 0)
            AudioSources[0].Play();
        //life[health -1].GetComponent<Renderer>().enabled = false;
        
        health -= damage;
        healthBarUpkeep(false);

        if (health <= 0)
        {
            isDead = true;
            AudioSources[1].Play();

        }
    }
    void death()
    {
        GameObject background = GameObject.FindWithTag("GameController");
        background.GetComponent<AudioSource>().Pause();
        bc.enabled = false;


        if (isDead && deathCounter % 5 == 0)
        {
            sr.sprite = spin[loc];
            loc++;
            
        }
        if (loc > 3)
            loc = 0;

        deathCounter++;

        if (deathCounter >= 150)
            isDead = false;
        
        if (!isDead)
        {
            deathCounter = 0;
            background.GetComponent<AudioSource>().UnPause();
            GameObject[] aliveEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in aliveEnemies)
            {
                Destroy(enemy);
                //Debug.Log("Playerfield should be clear");
            }
            //Destroy(gameObject);
            //gameOver.gameObject.GetComponent<Renderer>().enabled = true;
            gameOver.gameObject.GetComponent<GameOverScreen>().resetSpawns();
            transform.position = checkPoint;
            bc.enabled = true;
            sr.sprite = forward;
            restoreHealth();
            healthBarUpkeep(false);
        }
    }
    /*public void checkpoint(Vector3 pos)
    {
        checkPoint = pos;
    }*/
}

