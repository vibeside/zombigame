using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadscense : MonoBehaviour
{
    public int level;
    public bool custom;
    public string scene;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void doit() {
        if (!custom) {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level" + level.ToString());
        }
        if(custom){
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene); // literally just loads a scene
        }
    }
}
