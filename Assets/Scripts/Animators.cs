using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animators : MonoBehaviour
{
    private Timer timer;
    private MonkeyG monkey;
    private bool anim;
    public GameObject eli;
    public GameObject ere;
    public GameObject symboly;
    public GameObject effekty;
    public GameObject background;
    public GameObject banner1;
    public GameObject monkeyx;
    public GameObject banner2;
    public GameObject AIAuge;
    public Sprite[] symbole;
    public Sprite[] effekte;
    public Sprite[] monkeys;

    public Sprite[] augen;

    // Start is called before the first frame update
    void Start()
    {
        reset();
        anim = false;
        timer = FindObjectOfType<Timer>();
        monkey = FindObjectOfType<MonkeyG>();
    }

    void reset()
    {
        AIAuge.transform.localPosition = new Vector3(-100,-100,-3);
        AIAuge.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 1f);
        effekty.transform.localPosition = new Vector3(0, -2, -3);
        effekty.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 1f);
        background.transform.localScale = new Vector3(0, 0, 0);
        effekty.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        symboly.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        eli.transform.localScale = new Vector3(0.5f, 0.5f, 1);
        ere.transform.localScale = new Vector3(0.5f, 0.5f, 1);

        symboly.transform.localPosition = new Vector3(0, 2, -3);
        symboly.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 1f);
        eli.transform.localPosition = new Vector3(-5, -5, -3);
        eli.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 1f);
        ere.transform.localPosition = new Vector3(5, 5, -3);
        ere.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 1f);
    }



    public IEnumerator animate(int x)
    {


        while (anim)
        {
            yield return new WaitForSeconds(0.0001f);
        }
        anim = true;

        yield return new WaitForSeconds(0.005f);
        timer.timerFreeze();

        symboly.GetComponent<SpriteRenderer>().sprite = symbole[x];
        effekty.GetComponent<SpriteRenderer>().sprite = effekte[x];

        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForSeconds(0.001f);
            background.transform.localScale += new Vector3(0.01f, 0.01f, 0);

        }

        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.001f);
            effekty.transform.localPosition += new Vector3(0, 0.02f, 0);
            effekty.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 0.01f);
            symboly.transform.localPosition += new Vector3(0, -0.02f, 0);
            symboly.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 0.01f);
            eli.transform.localPosition += new Vector3(0.05f, 0.05f, 0);
            eli.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 0.01f);
            ere.transform.localPosition += new Vector3(-0.05f, -0.05f, 0);
            ere.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 0.01f);
        }
        yield return new WaitForSeconds(0.8f);

        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForSeconds(0.001f);
			
            background.transform.localScale -= new Vector3(0.01f, 0.01f, 0);
            eli.transform.localScale -= new Vector3(0.01f, 0.01f, 0);
            ere.transform.localScale -= new Vector3(0.01f, 0.01f, 0);
            symboly.transform.localScale -= new Vector3(0.01f, 0.01f, 0);
            effekty.transform.localScale -= new Vector3(0.01f, 0.01f, 0);
        }
        timer.timerWarm();
        reset();
        anim = false;
    }

    public IEnumerator neumate(GameObject p1, GameObject p2)
    {
        while (anim)
        {
            yield return new WaitForSeconds(0.0001f);
        }
        anim = true;

        yield return new WaitForSeconds(0.005f);
        timer.timerFreeze();

        GameObject x1;
        GameObject x2;


        if (p1.transform.position.y > p2.transform.position.y)
        {
            x1 = p2;
            x2 = p1;
        }
        else
        {
            x1 = p1;
            x2 = p2;
        }

        float x1orx = x1.transform.position.x;
        float x2orx = x2.transform.position.x;

        if (x2orx > x1orx)
        {
            while (x2orx > x1.transform.position.x)
            {
                yield return new WaitForSeconds(0.001f);
                x1.transform.localPosition += new Vector3(0.2f, 0, 0);
                x2.transform.localPosition -= new Vector3(0.2f, 0, 0);
            }
        }

        for (int i = 0; i < 60; i++)
        {
            yield return new WaitForSeconds(0.001f);
            x1.transform.localPosition += new Vector3(0.2f, 0.2f, 0);
            x2.transform.localPosition += new Vector3(-0.2f, -0.2f, 0);
        }

        float x2ory = x2.transform.position.y;

        while (x2ory > x1.transform.position.y)
        {
            yield return new WaitForSeconds(0.001f);
            x1.transform.localPosition += new Vector3(0, 0.2f, 0);
            x2.transform.localPosition += new Vector3(0, -0.2f, 0);
        }


        for (int i = 0; i < 60; i++)
        {
            yield return new WaitForSeconds(0.001f);
            x1.transform.localPosition += new Vector3(-0.2f, 0.2f, 0);
            x2.transform.localPosition += new Vector3(0.2f, -0.2f, 0);
        }


        if (x2orx < x1orx)
        {
            while (x2orx < x1.transform.position.x)
            {
                yield return new WaitForSeconds(0.001f);
                x1.transform.localPosition -= new Vector3(0.2f, 0, 0);
                x2.transform.localPosition += new Vector3(0.2f, 0, 0);
            }
        }
        timer.timerWarm();
        anim = false;
    }


    public IEnumerator cardsmove(GameObject py, int z, GameObject newcar){
        
     while (anim)
        {
            yield return new WaitForSeconds(0.0001f);
        }
        yield return new WaitForSeconds(0.0001f);
        anim = true;


        float x0 = newcar.transform.position.x;
        float y0 = newcar.transform.position.y;

        float x1 = py.GetComponent<Players>().scards[z].transform.position.x;
        float y1 = py.GetComponent<Players>().scards[z].transform.position.y;

        float xdif = x0 - x1;
        float ydif = y0 - y1;

    
      
    while ( !((x1 - newcar.transform.position.x) < 0.01f && (x1 - newcar.transform.position.x) > -0.01f) )
        {
        yield return new WaitForSeconds(0.01f);
        newcar.transform.localPosition -= new Vector3(xdif, ydif, 0);
        }

      newcar.GetComponent<Selectable>().inOwn = py.name;
      anim = false;
    }





    public IEnumerator banner()
    {
        while (anim)
        {
            yield return new WaitForSeconds(0.0001f);
        }
        anim = true;

        yield return new WaitForSeconds(0.005f);
        timer.timerFreeze();
        GameObject b;
        if (monkey.turnplay == monkey.player1.name)
        {
            b = banner1;
        }
        else
        {
            b = banner2;
        }
        for (int i = 0; i < 60; i++)
        {
            yield return new WaitForSeconds(0.001f);
            b.transform.localScale += new Vector3(0.02f, 0.02f, 0);
        }

        yield return new WaitForSeconds(1f);


        for (int i = 0; i < 120; i++)
        {
            yield return new WaitForSeconds(0.001f);
            b.transform.localScale -= new Vector3(0.01f, 0.01f, 0);
        }
        timer.timerWarm();
        anim = false;
    }


    public IEnumerator monkeymate(int x)
    {
        while (anim)
        {
            yield return new WaitForSeconds(0.0001f);
        }
        anim = true;

        yield return new WaitForSeconds(0.005f);
        timer.timerFreeze();

        print(" ist gestartet amk");

        monkeyx.GetComponent<SpriteRenderer>().sprite = monkeys[x];


        for (int i = 0; i < 60; i++)
        {
            yield return new WaitForSeconds(0.001f);
            monkeyx.transform.localScale += new Vector3(0.015f, 0.015f, 0);
            monkeyx.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 0.016f);
            monkeyx.transform.localPosition += new Vector3(0, 0.03f, 0);
        }

        yield return new WaitForSeconds(1f);


        for (int i = 0; i < 120; i++)
        {
            yield return new WaitForSeconds(0.001f);
            monkeyx.transform.localScale -= new Vector3(0.0075f, 0.0075f, 0);
            monkeyx.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.0083f);
            monkeyx.transform.localPosition -= new Vector3(0, 0.016f, 0);
        }


        timer.timerWarm();
        anim = false;
    }



    public IEnumerator aiAuge(GameObject lookAt)
    {
        while (anim)
        {
            yield return new WaitForSeconds(0.0001f);
        }
        anim = true;
    float AIX= lookAt.transform.position.x;
    float AIY= lookAt.transform.position.y;
AIAuge.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
    AIAuge.GetComponent<SpriteRenderer>().sprite = augen[0];
    AIAuge.transform.position = new Vector3(AIX, AIY, -1);
    for (var i = 0; i < 10; i++)
    {
     AIAuge.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 0.1f);   
     yield return new WaitForSeconds(0.01f);
    }
    AIAuge.GetComponent<SpriteRenderer>().sprite = augen[1];
    yield return new WaitForSeconds(0.1f);
    AIAuge.GetComponent<SpriteRenderer>().sprite = augen[2];
     yield return new WaitForSeconds(1.5f);
    AIAuge.GetComponent<SpriteRenderer>().sprite = augen[1];
    yield return new WaitForSeconds(0.1f);
    AIAuge.GetComponent<SpriteRenderer>().sprite = augen[0];
    for (var i = 0; i < 10; i++)
    {
     AIAuge.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.1f);   
     yield return new WaitForSeconds(0.01f);
      anim = false;
    }
    }




    // Update is called once per frame
    void Update()
    {

    }
}

