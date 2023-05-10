using System.Collections;
using TMPro;
using UnityEngine;

public class noisemaker : MonoBehaviour
{
    public Sprite sprite;
    public bool zombiekilled;
    private Animator anim;
    private SpriteRenderer sprRend;
    public zombcontroller zombcontroller;
    public bool chaseme = false;
    public moevemtn player;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sprRend = GetComponent<SpriteRenderer>();
        StartCoroutine("chasemezombies");
        StartCoroutine(dieme("owie my heart",10f)); // makes sure that the noisemaker never stays alive for too long
        anim.StartPlayback();
    }

    // Update is called once per frame
    void Update()
    {
        if(zombiekilled){
            anim.enabled = false;
            sprRend.sprite = sprite;
            Destroy(this.gameObject,5);
            zombcontroller.wanderTarget = transform.position;
        }
        if(chaseme) {
            var tempenmilist = FindObjectsOfType<enmi>();
            text.text = "*bang*";
            foreach (enmi enmi in tempenmilist) {
                enmi.ai.destination = transform.position;
                enmi.DontWander = false;// forces ALL zombies to chase him, no matter where
            }
        }
    }
    void OnDestroy(){
        transform.position += new Vector3(0,100000,0);
        player.placednoise = false;
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "death" && chaseme) {
            StopAllCoroutines();
            StartCoroutine(dieme("oof ouch owie they beat me up",0f));// kills noisemaker if zombi toucheds him
        }
    }
    IEnumerator dieme(string tex,float delay){
        yield return new WaitForSeconds(delay);
        zombiekilled = true;
        chaseme = false;// literally just used to kill the poor man
        text.text = tex;
    }
    IEnumerator chasemezombies(){
        yield return new WaitForSeconds(2.5f);
        if(!zombiekilled) {
            chaseme = true;
            anim.enabled = true; // maakes him start banging the pans together after 2 and a hlaf seconds, if he didnt die already.
            anim.StopPlayback();
        }
    }
}
