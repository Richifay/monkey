using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    private MonkeyG monkey;
    private Timer timer;
    private Graves graves;
    private IHandler ihandler;
    public bool cardhited = false;
    public GameObject slot;
    public GameObject inplayer;

    // Start is called before the first frame update
    void Start()
    {
        monkey = FindObjectOfType<MonkeyG>();
        ihandler = FindObjectOfType<IHandler>();
    }

    void reset()
    {
    cardhited = false;
    slotreset();
    }

    // Update is called once per frame
    void Update()
    {
        GetUI();
    }

    void GetUI()
    {
        // if( yourturn || (opturn & ceffekt k))
        inplayer = monkey.player1;
        int tar = -1;
        if (Input.GetMouseButtonDown(0))
        {
            tar = 1;
        }
        else if (Input.GetMouseButtonUp(0) && cardhited)
        {
            cardhited = false;
            slot.GetComponent<BoxCollider2D>().enabled = false;
            tar = 2;

        }
        else if (cardhited)
        {
            Vector3 mp = GetMouseAsWorldPoint();
            ihandler.mouseInput(inplayer,mp.x, mp.y);
        }

        if (tar != -1)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10));
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                if (hit.collider.CompareTag("Card") && tar == 1 && (hit.collider.GetComponent<Selectable>().inOwn == inplayer.name || hit.collider.GetComponent<Selectable>().inOwn == "N"))
                {
                    cardhited = true;
                    slot = hit.collider.gameObject;
                }
                ihandler.input(inplayer, tar, hit.collider.gameObject);
            }
            else if (!hit)
            {
                slotreset();
                ihandler.noInput(inplayer, tar);
            }
            if (slot && tar == 2)
            {
                slotreset();
            }
        }
    }

    private void slotreset()
    {
        if (slot)
        {
            slot.GetComponent<BoxCollider2D>().enabled = true;
            slot = null;
        }
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

}
