using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IHandler : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject slot1;
    public GameObject slot2;
    private MonkeyG monkey;

    private AI ai;
    // private Animators animators;
    private Timer timer;
    private Graves graves;
    public bool cardhit;
    private bool toreset;
    private GameObject slotUsedBy;
    private bool aib = true;
    void Start()
    {
        monkey = FindObjectOfType<MonkeyG>();
        graves = FindObjectOfType<Graves>();
        timer = FindObjectOfType<Timer>();
        ai = FindObjectOfType<AI>();
        // animators = FindObjectOfType<Animators>();
        toreset = false;
    }

    public void reset()
    {
        slotUsedBy = null;
        slotreset();
        cardhit = false;
        toreset = false;
    }
    public void input(GameObject player, int dx, GameObject selected)
    {
        if (player.name == monkey.turnplay || (player.name == monkey.opplay && graves.react))
        {
            if (monkey.gamephase == "startrounds")
            {
                if (dx == 1)
                {
                    if ((graves.react && player.name == monkey.opplay) || graves.effect == "K")
                    // Input für Konter
                    {
                        print("jetzt könig amk");

                        if (selected.CompareTag("Card") && (selected.GetComponent<Selectable>().inOwn == player.name))
                        {
                            toreset = true;
                            cardhit = true;
                            slot1 = selected;
                            slotUsedBy = player;
                            selected.GetComponent<Selectable>().selet = true;
                        }
                    }
                    

                    if (player.name == monkey.turnplay)
                    {

                        if (graves.effect != "_" && timer.timeractiv)
                        {
                            if (graves.effect == "7")
                            {
                                if (selected.CompareTag("Card") && selected.GetComponent<Selectable>().inOwn == player.GetComponent<Players>().opid)
                                {

                                    graves.effect = "_";
                                    timer.calltimer(10);
                                    if (aib)
                                    {
                                        if (monkey.turnplay == "P1")
                                        {
                                            ai.aigive(5, "P2", selected.GetComponent<Selectable>().capos, "-");
                                        }
                                        if (monkey.turnplay == "P2")
                                        {
                                            ai.aigive(4, "P1", selected.GetComponent<Selectable>().capos, selected.name);
                                        }
                                    }
                                    StartCoroutine(showcardlimit(selected, 3));


                                    if (monkey.turnplay == "P2")
                                    {
                                        monkey.OPAuge(selected);
                                    }
                                }
                            }
                            else if (graves.effect == "8")
                            {
                                if (selected.CompareTag("Card") && selected.GetComponent<Selectable>().inOwn == player.name)
                                {
                                    
                                    graves.effect = "_";
                                    timer.calltimer(10);
                                    if (aib)
                                    {
                                        if (monkey.turnplay == "P1")
                                        {
                                            ai.aigive(5, "P1", selected.GetComponent<Selectable>().capos, "-");
                                        }
                                        if (monkey.turnplay == "P2")
                                        {
                                            ai.aigive(4, "P2", selected.GetComponent<Selectable>().capos, selected.name);
                                        }
                                    }
                                    StartCoroutine(showcardlimit(selected, 3));
                                      if (monkey.turnplay == "P2")
                                    {
                                        monkey.OPAuge(selected);
                                    }

                                }
                            }

                            else if (graves.effect == "9")
                            {
                                if (selected.CompareTag("Card") && selected.GetComponent<Selectable>().inOwn == player.name)
                                {
                                    if (slot1)
                                    {
                                        slot1.GetComponent<Selectable>().selet = false;
                                    }
                                    slot1 = selected;
                                    slotUsedBy = player;
                                    toreset = true;
                                    slot1.GetComponent<Selectable>().selet = true;
                                }
                                else if (selected.CompareTag("Card") && selected.GetComponent<Selectable>().inOwn == player.GetComponent<Players>().opid)
                                {
                                    if (slot2)
                                    {
                                        slot2.GetComponent<Selectable>().selet = false;
                                    }
                                    slot2 = selected;
                                    slot2.GetComponent<Selectable>().selet = true;
                                }
                                if (slot1 && slot2)
                                {
                                    monkey.neunswap(slot1, slot2);
                                    graves.effect = "_";
                                    slotreset();
                                    timer.calltimer(10);
                                }
                            }

                        }

                        else
                        {
                            if (monkey.roundphase == "turnphase1")
                            {
                                if (selected.CompareTag("Deck"))
                                {
                                    Deck();
                                }

                                else if (selected.CompareTag("Monkey"))
                                {
                                    if (selected.GetComponent<MonkeyM>().type == "Drop")
                                    {
                                        selected.GetComponent<MonkeyDrop>().FickDeineMutterInGladbach();
                                    }
                                    else
                                    {
                                        selected.GetComponent<MonkeyM>().repmonk();
                                    }
                                }
                            }
                            if (selected.CompareTag("Card") && (selected.GetComponent<Selectable>().inOwn == player.name || selected.GetComponent<Selectable>().inOwn == "N"))
                            {
                                cardhit = true;
                                slot1 = selected;
                                slotUsedBy = player;
                                selected.GetComponent<Selectable>().selet = true;
                                toreset = true;
                            }
                        }
                    }
                }

                else if (dx == 2 && cardhit)
                {
                    cardhit = false;
                    print("Cardhit");
                    if (slot1.GetComponent<Selectable>().inOwn == player.name && ((graves.react && player.name == monkey.opplay) || graves.effect == "K"))
                    {
                        if (selected.CompareTag("Grave") || (selected.CompareTag("Card") && selected.GetComponent<Selectable>().inOwn == "G"))
                        {
                            if (graves.kontercheck(slot1))
                            {
                                if (aib)
                                {
                                    ai.aigive(6, player.name, slot1.GetComponent<Selectable>().capos, "-");
                                }
                                player.GetComponent<Players>().descard(slot1);
                                graves.fillgrave(slot1);
                                graves.effect = "_";
                                graves.react = false;
                            }
                            else
                            {
                                monkey.MonkeyDeal(player, 1);
                                StartCoroutine(showcardlimit(slot1, 0.7f));
                            }
                        }
                    }


                    else if (slot1.GetComponent<Selectable>().inOwn == "N")
                    {
                        if (selected.CompareTag("Card") && selected.GetComponent<Selectable>().inOwn == player.name)
                        {
                            Swop(selected);
                        }
                        else if (selected.CompareTag("Grave") || (selected.CompareTag("Card") && selected.GetComponent<Selectable>().inOwn == "G"))
                        {
                            graves.fillgrave(slot1);
                        }
                    }
                    else if (slot1.GetComponent<Selectable>().inOwn == player.name)
                    {
                        if (selected.CompareTag("Grave") || (selected.CompareTag("Card") && selected.GetComponent<Selectable>().inOwn == "G"))
                        {
                            if (graves.fillgravecheck(slot1))
                            {
                                if (aib)
                                {
                                    ai.aigive(6, player.name, slot1.GetComponent<Selectable>().capos, "-");
                                }
                                player.GetComponent<Players>().descard(slot1);
                                graves.fillgrave(slot1);
                            }
                            else
                            {
                                monkey.MonkeyDeal(player, 1);
                                StartCoroutine(showcardlimit(slot1, 0.7f));
                                if (aib)
                                {
                                    ai.aigive(2, player.name, slot1.GetComponent<Selectable>().capos, slot1.name);
                                }

                            }
                            timer.calltimer(10);
                        }
                    }
                    if (slot1)
                    {
                        posreset(slot1);
                    }
                    slotreset();
                }
            }
        }
    }
    public void mouseInput(GameObject player, float x, float y)
    {
        if (cardhit && ((player.name == monkey.turnplay && !graves.react) || (player.name == monkey.opplay && graves.react)))
        {
            slot1.transform.position = new Vector3(x, y, -1);
        }
    }


    public void noInput(GameObject player, int tar)
    {
        if (((player.name == monkey.turnplay) && !(tar == 2 && graves.effect == "9")) || (player.name == monkey.opplay && graves.react))
        {
            if (slot1)
            {
                posreset(slot1);
            }
            slotreset();
            cardhit = false;
        }
    }




    private void slotreset()
    {
        if (slot1)
        {
            slot1.GetComponent<BoxCollider2D>().enabled = true;
            slot1.GetComponent<Selectable>().selet = false;
            slot1 = null;
        }
        if (slot2)
        {
            slot2.GetComponent<BoxCollider2D>().enabled = true;
            slot2.GetComponent<Selectable>().selet = false;
            slot2 = null;
        }
        toreset = false;
    }


    private void posreset(GameObject card)
    {
        Vector3 lomp = new Vector3(0, 0, -0.3f);
        card.transform.position = slot1.transform.parent.gameObject.transform.position + lomp;
    }


    void Deck()
    {
        monkey.MonkeyDraw();
    }


    void Swop(GameObject selected)
    {
        slot1.transform.position = selected.transform.position;
        slot1.transform.SetParent(selected.transform.parent);
        slot1.GetComponent<Selectable>().capos = selected.GetComponent<Selectable>().capos;
        GameObject tplay = monkey.TPlay();
        tplay.GetComponent<Players>().newcard(slot1);
        slot1.GetComponent<Selectable>().inOwn = tplay.name;
        graves.fillgrave(selected);

        if (aib)
        {
            ai.aigive(1, tplay.name, slot1.GetComponent<Selectable>().capos, slot1.name);
        }



        StartCoroutine(showcardlimit(slot1, 0.7f));
        slotreset();
        timer.calltimer(10);
    }

    IEnumerator showcardlimit(GameObject sg, float sec)
    {
        sg.GetComponent<Selectable>().faceUp = true;
        yield return new WaitForSeconds(sec);
        sg.GetComponent<Selectable>().faceUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        // verschieben in eine andere klasse:
        if ((graves.effect == "9") && !timer.timeractiv)
        {
            monkey.neunswap(monkey.randomcard(monkey.TPlay()), monkey.randomcard(monkey.OPlay()));
            graves.effect = "_";
            timer.calltimer(10);
        }
        // verschieben in eine andere klasse:
        if ((graves.effect == "7" || graves.effect == "8") && !timer.timeractiv)
        {
            graves.effect = "_";
            timer.calltimer(10);
        }

        if (toreset && slotUsedBy.name == monkey.opplay && !graves.react)
        {
            if (slot1)
            {
                posreset(slot1);
            }
            slotreset();
            cardhit = false;
            toreset = false;
        }

    }

}
