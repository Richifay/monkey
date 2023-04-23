using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    public string opid;
    public GameObject[] scards;
    public GameObject[] lists = new GameObject[6];
    public bool movedpile = false;
    public float pointsgame;

    // Start is called before the first frame update
    void Start()
    {
    pointsgame = 0;
    }
    
    public void resetGame()
    {
    movedpile = false;
     foreach (GameObject x in lists)
    {
    Destroy(x);
    }
    for (int ix = 0; ix < 6; ix++)
    {
    lists[ix] = null;
    }
    }

   
    public void newcard(GameObject card)
    {
        lists[card.GetComponent<Selectable>().capos] = card;
    }

    public void descard(GameObject card)
    {
        lists[card.GetComponent<Selectable>().capos] = null;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
