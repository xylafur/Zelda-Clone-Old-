using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour {

    private BoxCollider2D bc;
    private AudioSource audio;
    void Start () {
        bc = GetComponent<BoxCollider2D>();
        audio = GetComponent<AudioSource>();
        bc.size = new Vector2(0f, 0f);
	}
    void Update()
    {
        transform.rotation = Quaternion.identity;
        
    }
    public void updatePos(Vector3 vec)
    {
        transform.position = vec;
    }
    public void noAttack()
    {
        bc.size = new Vector2(0f, 0f);
    }
    public void playAudio()
    {
        audio.Play();
    }
    public void attackF()
    {
        
        bc.size = new Vector2(0.03688971f, .1137515f);
        //bc.size = new Vector2(5f, 5f);
        bc.offset = new Vector2(-.01543701f, 0.08859508f);
    }
    public void attackB()
    {
        
        bc.size = new Vector2(0.03580936f, 0.1115907f);
        bc.offset = new Vector2(0.004861587f, -0.07864284f);
    }
    public void attackR()
    {
        
        bc.size = new Vector2(0.1112306f, 0.03963726f);
        bc.offset = new Vector2(0.07984932f, -0.009723509f);
    }
    public void attackL()
    {
        
        bc.size = new Vector2(0.1103636f, 0.03963726f);
        bc.offset = new Vector2(-0.0799695f, -0.009723509f);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.SendMessage("takeDamage", 1);
        }
    }
}
