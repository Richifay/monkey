using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MonkeyG : MonoBehaviour
{
    private Timer timer;
    private Graves graves;
    private Animators animators;
    private AI ai;
    private IHandler ihandler;
    public Sprite[] cardFaces;
    public Pointsbar pointsbar;
    public GameObject cardPrefab;
    public GameObject Player;
    public string turnplay;

    public string opplay;
    public static string[] values = new string[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
    public List<string> deck;
    public string matchphase;
    public string gamephase;
    public string roundphase;
    private string monkeycall;
    private string monkeyplayer;
    private int monkeyturncount;
    private float monkeyfactor;
    
    public bool showed;

    public GameObject deckpile;
    public GameObject player1;
    public GameObject player2;
    public GameObject newCarda;

    public GameObject monkeybut;

    public GameObject[] bottomPos;
    public GameObject[] topPos;
    public float ztak = -0.03f;

    public bool reset;
    public bool kv;
    
    bool aion = true;

    // Start is called before the first frame update
    void Start()
    { 
        kv = false;
        reset = false;
        timer = FindObjectOfType<Timer>();
        graves = FindObjectOfType<Graves>();
        animators = FindObjectOfType<Animators>();
        pointsbar = FindObjectOfType<Pointsbar>();
        ihandler = FindObjectOfType<IHandler>();
        ai = FindObjectOfType<AI>();
        ai.aireset();
        CreatePlayer();
        turnplay = "P1";
        monkeycall = "_";
        StartGame();
    }

    public void resetgame()
    {
    reset = true;
    deck = null;
    gamephase = null;
    roundphase = null;
    monkeycall = "_";
    monkeyplayer = null;
    monkeyturncount = 0;
    monkeyfactor = 0;
    showed = false;
    turnplay = "P2";
    opplay = "P1";
    ztak = -0.03f;
    resetmovelists (player1);
    resetmovelists (player2);
    player1.GetComponent<Players>().resetGame();
    player2.GetComponent<Players>().resetGame();
    graves.reset();
    timer.reset();
    ihandler.reset();
    monkeybut.GetComponent<MonkeyM>().reset();
    StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (kv && gamephase != "showcard")
        {
        ShowCards(player1, 2);
        ShowCards(player1, 3);

        if (aion)
        {
        ai.aigive(5,"P1",2,"-");
        ai.aigive(5,"P1",3,"-");
        ai.aigive(4,"P2",0,player2.GetComponent<Players>().lists[0].name);
        ai.aigive(4,"P2",1,player2.GetComponent<Players>().lists[1].name);
        }

        gamephase = "showcard";
        }

        if (!timer.timeractiv && gamephase == "showcard" && kv )
        { 
            kv = false;
            showed = false;
            gamephase = "startrounds";
            StartTP1();
        }

        if (gamephase == "startrounds")
        {

            if (!timer.timeractiv && roundphase == "turnphase1" && graves.effect == "_")
            {
                MonkeyDraw();
            }

            else if (!timer.timeractiv && roundphase == "turnphase2" && graves.effect == "_")
            {
                if (newCarda && newCarda.GetComponent<Selectable>().inOwn == "N")
                {
                    graves.fillgrave(newCarda);
                    timer.calltimer(5);
                }
                else
                {
                    roundphase = "endturn";
                }
            }

            else if (roundphase == "endturn")
            {
                if (monkeycall != "_")
                {
                    monkeyturnchanger();
                }
                else
                {
                    ChangeTurn();
                }
            }
        }

    }






    public void StartGame()
    {
        kv = false;
        PlayCards();
    }


    public void StartTP1()
    {
        if (aion && turnplay == "P2")
        {
        ai.aistart();
        }
        roundphase = "turnphase1";
        timer.calltimer(15);
        StartCoroutine(animators.banner());
    }


    public void StartTP2()
    {
        roundphase = "turnphase2";
        timer.calltimer(10);
    }


    public void ChangeTurn()
    {

        if (turnplay == player1.name)
        {
            turnplay = player2.name;
            opplay = player1.name;
        }
        else
        {
            turnplay = player1.name;
            opplay = player2.name;
        }
        StartTP1();
    }

    public void monkeyturnchanger()
    {
        if (monkeyturncount > 0)
        {
            monkeyturncount -= 1;
            if (monkeyplayer == player1.name)
            {
                turnplay = player2.name;
                opplay = player1.name;
            }
            else
            {
                turnplay = player1.name;
                opplay = player2.name;
            }
            StartTP1();
        }
        else
        {
            openall();
            monkeycalc();
        }
    }

    public void monkeycalc()
    {
        gamephase = "EoG";
        print("End of Game");
        float tpp = 0;
        float opp = 0;
        tpp = calcsingplayerpoints(player1);
        opp = calcsingplayerpoints(player2);
        if (tpp > opp)
        {
            tpp = tpp * monkeyfactor;
        }
        else
        {
            opp = opp * monkeyfactor;
        }
        player1.GetComponent<Players>().pointsgame += tpp;
        player2.GetComponent<Players>().pointsgame += opp;
        pointsbar.UpdateBars();
    }

 public void pystraf(GameObject py)
    {
    py.GetComponent<Players>().pointsgame += 10;
    pointsbar.UpdateBars();
    }


    public void ShowCards(GameObject sc, int x)
    {
        showed = true;
        GameObject pcard = sc.GetComponent<Players>().lists[x];
        pcard.GetComponent<Selectable>().faceUp = true;
        pcard.GetComponent<Selectable>().gedeckelt = true;
        timer.calltimer(10);
    }


    public void neunswap(GameObject p1, GameObject p2)
    {
        GameObject slot;

        if (aion)
        {
        ai.aigivenine(turnplay,p1.GetComponent<Selectable>().capos, p2.GetComponent<Selectable>().capos);
        }
        StartCoroutine(animators.neumate(p1, p2));

        slot = p1.transform.parent.gameObject;
        int cap = p1.GetComponent<Selectable>().capos;

        p1.transform.SetParent(p2.transform.parent);
        p2.transform.SetParent(slot.transform);

        p1.GetComponent<Selectable>().capos = p2.GetComponent<Selectable>().capos;
        p2.GetComponent<Selectable>().capos = cap;

        GameObject oplay = OPlay();
        GameObject tplay = TPlay();
        oplay.GetComponent<Players>().newcard(p1);
        tplay.GetComponent<Players>().newcard(p2);

        p1.GetComponent<Selectable>().inOwn = opplay;
        p2.GetComponent<Selectable>().inOwn = turnplay;
    }

    public GameObject TPlay()
    {
        GameObject tp;
        if (player1.name == turnplay)
        {
            tp = player1;
        }
        else
        {
            tp = player2;
        }
        return tp;
    }




    public GameObject OPlay()
    {
        GameObject op;
        if (player1.name != turnplay)
        {
            op = player1;
        }
        else
        {
            op = player2;
        }
        return op;
    }




    public void CreatePlayer()
    {
        player1 = Instantiate(Player);
        player1.name = "P1";
        player1.GetComponent<Players>().scards = bottomPos;
        player1.GetComponent<Players>().opid = "P2";
        ai.OPPlayer = player1;

        player2 = Instantiate(Player);
        player2.name = "P2";
        player2.GetComponent<Players>().scards = topPos;
        player2.GetComponent<Players>().opid = "P1";
        ai.AIPlayer = player2;
        // player2.GetComponent<Players>().lists = toplists;
    }


    public void PlayCards()
    {
        deck = GenerateDeck();
        Shuffle(deck);
        foreach (string card in deck)
        {
            print(card);
        }
       StartCoroutine(aDeal());
     
    }

    IEnumerator aDeal()

      {
        for (int n = 0; n < 4; n++)
            {
      
       MonkeyDeal(player1, 1);
            yield return new WaitForSeconds(0.1f);
        MonkeyDeal(player2, 1);
           yield return new WaitForSeconds(0.1f);
            }
         kv = true;
      }

    public static List<string> GenerateDeck()
    {
        List<string> newDeck = new List<string>();
        foreach (string v in values)
        {
            for (int n = 0; n < 4; n++)
            {
                newDeck.Add(v);
            }
        }

        return newDeck;
    }

    void Shuffle<T>(List<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            int k = random.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }



    public void MonkeyDeal(GameObject py, int x)
    {
        for (int i = 0; i < x; i++)
        {
            if (deck.Count > 0)
            {
                bool suck = true;
                for (int z = 0; z < 6; z++)
                { 
                    if (py.GetComponent<Players>().lists[z] == null)
                    {
                        GameObject newCard = Instantiate(cardPrefab, new Vector3( deckpile.transform.position.x, deckpile.transform.position.y , py.GetComponent<Players>().scards[z].transform.position.z - 0.03f), Quaternion.identity, py.GetComponent<Players>().scards[z].transform);
                        newCard.name = deck.Last<string>();
                        deck.RemoveAt(deck.Count - 1);
                        py.GetComponent<Players>().lists[z] = newCard;
                        newCard.GetComponent<Selectable>().faceUp = false;
                        newCard.GetComponent<Selectable>().capos = z;
                        if(aion)
                        {
                        ai.aigive(3,py.name,z,"U");
                        }
                        movelists(py);
                        newCard.transform.position = deckpile.transform.position;
                        StartCoroutine(animators.cardsmove(py,z,newCard));
                        suck = false;
                        break;
                    }
                }
                if (suck)
                {
                 pystraf(py);
                }
            }
            else
            {
                deckout();
                break;
            }
        }
    }

    

    void movelists(GameObject py)
    {

        if ((py.GetComponent<Players>().lists[4] != null || py.GetComponent<Players>().lists[5] != null) && !py.GetComponent<Players>().movedpile)
        {
            py.GetComponent<Players>().movedpile = true;
            for (int i = 0; i < 6; i++)
            {
                py.GetComponent<Players>().scards[i].transform.localPosition += new Vector3(-1, 0, 0);
            }
        }
    }


    void resetmovelists (GameObject py)
    {
    if (py.GetComponent<Players>().movedpile)
        {
            for (int i = 0; i < 6; i++)
            {
                py.GetComponent<Players>().scards[i].transform.localPosition -= new Vector3(-1, 0, 0);
            }
        }
    }

    public void MonkeyDraw()
    {

        if (deck.Count > 0)
        {
            string newdraw = deck.Last<string>();
            deck.RemoveAt(deck.Count - 1);
            ztak = ztak - 0.2f;
            newCarda = Instantiate(cardPrefab, new Vector3(deckpile.transform.position.x, deckpile.transform.position.y, deckpile.transform.position.z + ztak), Quaternion.identity, deckpile.transform);
            newCarda.name = newdraw;
            newCarda.transform.localScale += new Vector3(-1, -1, 0);
            newCarda.GetComponent<Selectable>().inOwn = "N";
            newCarda.GetComponent<Selectable>().faceUp = true;

            StartTP2();
        }
        else
        {
            deckout();
        }
    }

    void deckout()
    {

        openall();
        docountpoints();
    }


    void openall()
    {
        foreach (GameObject x in player1.GetComponent<Players>().lists)
        {
            if (x != null)
            {
                x.GetComponent<Selectable>().faceUp = true;
            }
        }

        foreach (GameObject x in player2.GetComponent<Players>().lists)
        {
            if (x != null)
            {
                x.GetComponent<Selectable>().faceUp = true;
            }
        }
    }

    void docountpoints()
    {
        gamephase = "EoG";
        print("End of Game");
        float tpp = 0;
        float opp = 0;
        tpp = calcsingplayerpoints(player1);
        opp = calcsingplayerpoints(player2);
        player1.GetComponent<Players>().pointsgame += tpp;
        player2.GetComponent<Players>().pointsgame += opp;
        pointsbar.UpdateBars();
    }

    float calcsingplayerpoints(GameObject player)
    {

        List<string> oplist = new List<string>();
        oplist.Clear();

        foreach (GameObject x in player.GetComponent<Players>().lists)
        {
            if (x != null)
            {
                oplist.Add(x.name);
            }
        }

        oplist.Sort();
        float pp = 0;
        string pre = "";
        foreach (String x in oplist)
        {
            if (x != pre)
            {
                if (x == "K")
                {
                    pp += 13;
                }
                else if (x == "Q" || x == "J")
                {
                    pp += 10;
                }
                else if (x == "A")
                {
                    pp += 1;
                }
                else
                {
                    pp += float.Parse(x);
                }
            }
            pre = x;
        }
        return pp;
    }

    public GameObject randomcard(GameObject player)
    {
        GameObject rc = null;
        System.Random random = new System.Random();
        bool find = true;
        while (find)
        {
            int k = random.Next(6);
            if (player.GetComponent<Players>().lists[k] != null)
            {
                rc = player.GetComponent<Players>().lists[k];
                find = false;
            }
        }
        return rc;
    }

    public void monkeyGetCalled(string monkc)
    {
        monkeycall = monkc;
        monkeyplayer = turnplay;
        if (monkeycall == "2Monkey")
        {
            monkeyfactor = 2;
            monkeyturncount = 1;
            StartCoroutine(animators.monkeymate(0));
        }
        else if (monkeycall == "4Monkey")
        {
            monkeyfactor = 4;
            monkeyturncount = 3;
            StartCoroutine(animators.monkeymate(1));
        }
        else if (monkeycall == "05Monkey")
        {
            monkeyfactor = 0.5f;
            monkeyturncount = 1;
            StartCoroutine(animators.monkeymate(2));
            MonkeyDeal(TPlay(), 1);
        }

        roundphase = "endturn";
    }

    public void testfunction()
    {
    resetgame();
    }

    public void OPAuge(GameObject lookAt)
    {
        StartCoroutine(animators.aiAuge(lookAt));
    }

}