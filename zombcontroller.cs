using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class zombcontroller : MonoBehaviour
{
    public int AmtOfZombs;
    public Transform player;
    public Transform ZombieSpawn;
    public GameObject ZombPrefab;
    public GameObject SlowZombPrefab;
    public GameObject FastZombPrefab;
    public List<GameObject> enmis;
    public bool Chase;
    public Vector2 wanderTarget;
    public GameObject winmenu;
    public GameObject losemenu;
    bool stopChase;
    // Start is called before the first frame update
    void Start()
    {
        SpawnZombs();// spawns a number of zombies = to amt of zombs
        wanderTarget = ZombieSpawn.position;
        ZombieSpawn.gameObject.SetActive(false);// hides zombie spawn
    }

    // Update is called once per frame
    void Update()
    {
        if(winmenu.activeSelf) stopChase = true;
        if(losemenu.activeSelf) stopChase = true;// stops zombie ai if player won or died alerady

     foreach(GameObject gm in enmis){
         enmi temp = gm.GetComponent<enmi>();
         if(!stopChase)temp.chase = Chase;
         temp.wanderTarget = wanderTarget;// makes sure the zombies act as a unit
         if(stopChase)temp.GetComponent<AIPath>().enabled = false;
     }
    }
    void SpawnZombs(){
        for(int i = 0; i < AmtOfZombs; i++){// randomly selects from three preset zombies to spawn
            int tmep = Random.Range(0,3);
            if(tmep == 0){
                GameObject temp = Instantiate(ZombPrefab);
                temp.GetComponent<enmi>().player = player;
                temp.transform.position = ZombieSpawn.position;
                temp.SetActive(true);
                enmis.Add(temp);
            }if(tmep == 1){
                GameObject temp = Instantiate(SlowZombPrefab);
                temp.GetComponent<enmi>().player = player;
                temp.transform.position = ZombieSpawn.position;
                temp.SetActive(true);
                enmis.Add(temp);
            }if(tmep == 2){
                GameObject temp = Instantiate(FastZombPrefab);
                temp.GetComponent<enmi>().player = player;
                temp.transform.position = ZombieSpawn.position;
                temp.SetActive(true);
                enmis.Add(temp);
            }
        }
    }
}
