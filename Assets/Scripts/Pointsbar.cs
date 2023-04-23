
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pointsbar : MonoBehaviour
{
     public Slider left;
     public Slider right;
     private MonkeyG monkey; 
    // Start is called before the first frame update
    void Start()
    {
    monkey = FindObjectOfType<MonkeyG>();
    }
 

    public void UpdateBars()
    {
         float lval = monkey.player1.GetComponent<Players>().pointsgame;
         float rval = monkey.player2.GetComponent<Players>().pointsgame; 
         left.value = lval ;
         right.value = 100 - rval; 
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
