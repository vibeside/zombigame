using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wire : MonoBehaviour
{
    public bool button;
    public bool once;
    public GameObject othe;
    public List<GameObject> others;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == transform.tag){
            othe.SetActive(!othe.activeSelf);// toggles the attached object
            if(once){
                gameObject.SetActive(false);
            }
            if(others.Count >0){
                foreach(GameObject obj in others){
                    obj.SetActive(!obj.activeSelf);
                }
            }
        }

        AstarPath.active.Scan();

    }
    void OnTriggerExit2D(Collider2D other){
        if(button && other.tag == transform.tag){ // if tagged button, untoggles it
            othe.SetActive(!othe.activeSelf);
            if(others.Count >0){
                foreach(GameObject obj in others){
                    obj.SetActive(!obj.activeSelf);
                }
            }
        }
        AstarPath.active.Scan();
    }
}
