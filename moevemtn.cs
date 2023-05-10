using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moevemtn : MonoBehaviour
{
    private float vertmove; //stores a -1 to 1 var for up and down
    private float hormove; //same as above but left/right
    public Rigidbody2D rb; //player reference
    public float maxSpeed = 10f; //explanatory
    public float acceleration = 10.0f; //explanatory
    public GameObject noisemakerprefab;
    public bool placednoise;
    private bool dead;
    public Sprite deadbody;
    public bool crouched;
    public GameObject winmenu;
    public GameObject losemenu;
    public GameObject escapemenu;
private Vector2 timeadjust;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();// automatically gets player upon start
        winmenu.SetActive(false);
        losemenu.SetActive(false);// make sure no menus show up
        escapemenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        vertmove = Input.GetAxis("Vertical"); //gets W/S or uparrow/downarrow from unity
        hormove = Input.GetAxis("Horizontal");// does the same but with right left and A/D

        if(Input.GetKey(KeyCode.LeftControl)){
            crouched = true;
            transform.localScale = new Vector3(0.2732739f,0.1f,0.2732739f);
        }else{
            crouched = false; // we want the player to look like he's crouching when holding crouch, so we shrink and grow him
            transform.localScale = new Vector3(0.2732739f,0.2732739f,0.2732739f);
        }
        if(Input.GetKeyDown(KeyCode.Space) && !placednoise && !dead){
            GameObject temp = Instantiate(noisemakerprefab);
            temp.transform.position = transform.position;// spawns a noise maker, makes sure its spawned at the player, then starts the cooldown
            temp.SetActive(true);
            placednoise = true;
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            escapemenu.SetActive(!escapemenu.activeSelf);// really?
        }
    }
    void FixedUpdate(){
        if (rb.velocity.magnitude < maxSpeed && !dead) {
            rb.velocity += new Vector2(hormove, vertmove) * acceleration; // moves player
        }
    }
    // glass color is 109,149,255,119
    void OnTriggerEnter2D(Collider2D other) {
    if(other.tag == "death" && other.gameObject.layer != 7){
        if(!winmenu.activeSelf)die();
        Debug.Log(other.gameObject.layer);
// makes sure player can win and lose
    }
    if(other.tag == "exit"){
        if(!losemenu.activeSelf)win();
    }
}
    void die(){
        dead = true;
        rb.gameObject.GetComponent<SpriteRenderer>().sprite = deadbody;
        losemenu.SetActive(true);
    }
    void win(){// code for winning and losing
        dead = true;
       winmenu.SetActive(true);
    }
    public void quit(){
        Debug.Log("quiting");
        Application.Quit();// also quiting
    }
}
