using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    public bool faceUp = false;
    public string inOwn = "N";
    public int capos = -1;
    private MonkeyG monkey;
    public bool gedeckelt = false;
    public bool selet = false;
    // Start is called before the first frame update
    void Start()
    {
        monkey = FindObjectOfType<MonkeyG>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gedeckelt && !monkey.showed)
        {
            gedeckelt = false;
            faceUp = false;
        }
    }
}
