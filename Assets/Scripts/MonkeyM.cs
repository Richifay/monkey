using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyM : MonoBehaviour
{
    // Start is called before the first frame update
    public string type;
    public Sprite resetsprite;
    public Sprite monkpic;
    private MonkeyG monkeyg;
    private SpriteRenderer spriteRenderer;




    public void repmonk()
    {
        if (this.type != "activ")
        {
            GameObject par = this.transform.parent.gameObject;
            par.GetComponent<MonkeyM>().monkpic = this.monkpic;
            par.GetComponent<MonkeyM>().type = "activ";
            par.GetComponent<MonkeyDrop>().FickDeineMutterInGladbach();
            monkeyg.monkeyGetCalled(this.name);
        }
    }

    public void reset()
    {
     type = "Drop";
     monkpic = resetsprite;
     this.GetComponent<MonkeyDrop>().DropTop();
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        monkeyg = FindObjectOfType<MonkeyG>();

    }

    // Update is called once per frame
    void Update()
    {
        if (monkpic)
        {
            spriteRenderer.sprite = monkpic;
        }
    }
}
