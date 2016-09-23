using UnityEngine;
using System.Collections;

public class KnightSword : MonoBehaviour {
    public GameObject player;
    public GameObject body;

    private BoxCollider2D bc;
    public int damage;
	// Use this for initialization
	void Start () {
        bc = GetComponent<BoxCollider2D>();
        //player = GameObject.FindGameObjectWithTag
	}
	
	// Update is called once per frame
    void Update()
    {
        transform.position = body.transform.position;
        transform.rotation = Quaternion.identity;
    }
    public void switchCollider(string dir){
        switch (dir)
        {
            case "forward":
                bc.offset = new Vector3(0.0007196154f, 0.03765946f);
                bc.size = new Vector3(0.03664468f, 0.07818711f);
                break;
            case "back":
                bc.offset = new Vector3(0.05101191f, -.05f);
                bc.size = new Vector3(0.02759253f, 0.05691177f);
                break;
            case "right":
                 bc.offset = new Vector3(0.04783849f, -.02f);
                 bc.size = new Vector3(0.05751399f, 0.03106475f);
                break;
            case "left":
                bc.offset = new Vector3(-0.0500863f, -0.01524239f);
                bc.size = new Vector3(0.06204756f, 0.03106475f);
                break;
        }
    }
	void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player")
        {
           other.SendMessage("TakeDamage", damage);
        }
       // Debug.Log("TRIGGGGGGG");
    }
}
