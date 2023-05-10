using System.Collections;
using Pathfinding;
using UnityEngine;

public class enmi : MonoBehaviour
{
    public IAstarAI ai; // AI element from package
    public Transform player;
    public bool chase; // determines whether or not zombie should tell it's controller to chase player
    public Vector2 wanderTarget;
    private bool wandering;// only used once on line 32
    public bool DontWander;
    public zombcontroller gamecontroller;
    public LayerMask layerMask; // layer mask used when player isnt crouching
    public LayerMask crouchedLayermask; // layer mask used when player is crouching
    public bool forceChase; // debug element used to make the zombie go after player
    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<IAstarAI>(); // makes sure that ai element is never null
        wanderTarget = transform.position; // zombie will wander near where it starts
        InvokeRepeating("checkVisibility",0,0.01f); // check function for comments
    }

    // Update is called once per frame
    void Update()
    {
        if(forceChase)ai.destination = player.position;
        if(chase){ // forcechase and chase are identical, aside from chase being controlled by 'gamecontroller'
            ai.destination = player.position;
        }else{
            if(!wandering && !DontWander) { // don't wander is debug used for disabling this, has no effect otherwise
                StartCoroutine("wanderTimer");// if it's not chasing, start wandering where it last saw player
            }
        }
    }

    void checkVisibility() {
        if (Vector2.Angle(transform.up, player.transform.position - transform.position) < 100) { // checks if zombie is looking at player
            RaycastHit2D hit = Physics2D.Linecast(transform.position, player.position, layerMask); // this section raycasts to the player, if it's anything but it stops.
            if (hit.collider.gameObject.name == player.gameObject.name && !player.GetComponent<moevemtn>().crouched) { // when it's the player, make sure they're not crouching
                gamecontroller.Chase = true; // then makes the zombie chase (i set both individual and controller, because some zombies aren't part of a controller.
                chase = true;
            } else if (hit.collider.gameObject.name != player.gameObject.name && gamecontroller.Chase || chase) {
                gamecontroller.Chase = false;
                chase = false; // sets the zombies wander target to the last known location of the player, otherwise they run back to their spawn
                gamecontroller.wanderTarget = ai.destination;
                wanderTarget = ai.destination;
            } else {
                chase = false;
                gamecontroller.Chase = false;
            }
            if (player.GetComponent<moevemtn>().crouched) {// does the same as above, using a different layer to make sure the player isn't crouching
                RaycastHit2D crouchhit = Physics2D.Linecast(transform.position, player.position, crouchedLayermask);
                if (crouchhit.collider.gameObject.name == player.gameObject.name) {
                    gamecontroller.Chase = true;
                    chase = true;
                } else if (hit.collider.gameObject.name != player.gameObject.name && gamecontroller.Chase || chase) {
                    gamecontroller.Chase = false;
                    chase = false;
                    gamecontroller.wanderTarget = ai.destination;
                    wanderTarget = ai.destination;
                } else {
                    chase = false;
                    gamecontroller.Chase = false;
                }
            }
        }
    }
    void genPoint(){ // generates a random point nearby the zombies wander point, sets the destination fo rit.
        Vector2 temp = new Vector2(Random.Range(-1f,1f) + wanderTarget.x,Random.Range(-1f,1f) + wanderTarget.y);
        ai.destination = new Vector3(temp.x,temp.y,0);
    }
    IEnumerator wanderTimer(){
        wandering = true;
        genPoint(); // obvious
        yield return new WaitForSeconds(1);
        wandering = false;
    }
}
