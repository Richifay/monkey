using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyDrop : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject monk;
    public Sprite green;
    public Sprite red;
    public Sprite blau;
    private bool cimen;
    private GameObject rMonkey;
    private GameObject gMonkey;
    private GameObject bMonkey;

    void RainDrop()
    {
        gMonkey = Instantiate(monk, new Vector3(this.transform.position.x + 1.5f, this.transform.position.y + 0, this.transform.position.z - 0.03f ), Quaternion.identity, this.transform);
        gMonkey.name = "2Monkey";
        gMonkey.GetComponent<MonkeyM>().monkpic = green;
        rMonkey = Instantiate(monk, new Vector3(this.transform.position.x + 1.5f, this.transform.position.y + 2, this.transform.position.z - 0.03f), Quaternion.identity, this.transform);
        rMonkey.name = "4Monkey";
        rMonkey.GetComponent<MonkeyM>().monkpic = red;
        bMonkey = Instantiate(monk, new Vector3(this.transform.position.x + 1.5f, this.transform.position.y - 2, this.transform.position.z - 0.03f), Quaternion.identity, this.transform);
        bMonkey.name = "05Monkey";
        bMonkey.GetComponent<MonkeyM>().monkpic = blau;
        cimen = false;
    }


    public void DropTop()
    {
        if (!cimen)
        {
        Destroy(rMonkey);
        Destroy(gMonkey);
        Destroy(bMonkey);
        }
        cimen = true;
    }


    public void FickDeineMutterInGladbach()
    {

        if (cimen)
        {
            RainDrop();
        }
        else
        {
            DropTop();
        }

    }

    void Start()
    {
        cimen = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
