using TMPro;
using UnityEngine;

public class funyscript : MonoBehaviour
{
    public TextMeshProUGUI funny;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void dotheroar(bool forced){
        if(Random.Range(0,10) == 9 || forced){
            funny.text = "these LOSERS made BLACKJACK!"; // this is for an easter egg, you get no documentation or comments!!!!
        }
    }
    public void stoptheroar(){
        funny.text = "johnson and sohnson\n\n they r prety col";
    }
}
