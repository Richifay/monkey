using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public GameObject AIPlayer;
    public GameObject OPPlayer;
    private IHandler ihandler;
    public GameObject SelectedAICard;
    public GameObject Grave;
    private Timer timer;
    
    public bool monkeyCall = false;
    public GameObject Deck;
    public int AICount;
    public string[] inownplayer;
    public string[] inownai;
    public string[] stateplayer;
    public string[] stateai;
    // public string drawedcard;
    // public string grave;
    // public string gamestate;
    private bool AIworking = false;
    private bool AImoving = false;

    private bool EffectRes = false;
    // Start is called before the first frame update
    void Start()
    {
        ihandler = FindObjectOfType<IHandler>();
        timer = FindObjectOfType<Timer>();
    }
    public void aireset()
    {
        stateai = new string[] { "U", "U", "U", "U", "-", "-" };
        stateplayer = new string[] { "U", "U", "U", "U", "-", "-" };
        inownai = new string[] { "U", "U", "U", "U", "-", "-" };
        inownplayer = new string[] { "U", "U", "U", "U", "-", "-" };
        AIworking = false;
        AImoving = false;
        EffectRes = false;
        monkeyCall = false;
        AICount=0;

    }
    public void aigive(int fall, string player, int pos, string x)
    {

        //Fall für Draw and Swap
        if (fall == 1)
        {
            if (player == "P1") //turnplayer
            {
                stateplayer[pos] = "PK";
                inownplayer[pos] = "U";
            }
            else if (player == "P2") //turnplayer
            {
                stateai[pos] = "AK";
                inownai[pos] = x;
            }
        }

        //Fall für aufgedeckte Strafe
        else if (fall == 2)
        {
            stateplayer[pos] = "BK";
            inownplayer[pos] = x;
        }

        //Fall für Strafe
        else if (fall == 3)
        {
            if (player == "P1")  //owner
            {
                stateplayer[pos] = "U";
                inownplayer[pos] = "U";
            }
            else if (player == "P2")  //owner
            {
                stateai[pos] = "U";
                inownai[pos] = "U";
            }
        }

        //Fall für Auge und Spion AI
        else if (fall == 4)
        {
            if (player == "P1")  //owner
            {
                inownplayer[pos] = x;
                if (stateplayer[pos] == "PK" || stateplayer[pos] == "BK")
                {
                    stateplayer[pos] = "BK";
                }
                else
                {
                    stateplayer[pos] = "AK";
                }
            }
            else if (player == "P2")  //owner
            {
                inownai[pos] = x;
                if (stateai[pos] == "PK" || stateai[pos] == "BK")
                {
                    stateai[pos] = "BK";
                }
                else
                {
                    stateai[pos] = "AK";
                }
            }
        }

        //Fall für Auge und Spion Player

        else if (fall == 5)
        {
            if (player == "P1")  //owner
            {
                if (stateplayer[pos] == "AK" || stateplayer[pos] == "BK")
                {
                    stateplayer[pos] = "BK";
                }
                else
                {
                    stateplayer[pos] = "PK";
                }
            }
            else if (player == "P2")  //owner
            {
                if (stateai[pos] == "AK" || stateai[pos] == "BK")
                {
                    stateai[pos] = "BK";
                }
                else
                {
                    stateai[pos] = "PK";
                }
            }
        }

        //Fall für Clear
        else if (fall == 6)
        {
            if (player == "P1")  //turnplayer
            {
                stateplayer[pos] = "-";
                inownplayer[pos] = "-";
            }
            if (player == "P2") //turnplayer
            {
                stateai[pos] = "-";
                inownai[pos] = "-";
            }
        }
    }

    public void aigivenine(string player, int cppos, int caipos)
    {
        string oldstateAI = "";
        string oldinownAI = "";
        if (player == "P1")  //turnplayer
        {
            oldstateAI = stateai[caipos];
            oldinownAI = inownai[caipos];

            stateai[caipos] = stateplayer[cppos];
            inownai[caipos] = inownplayer[cppos];

            stateplayer[cppos] = oldstateAI;
            inownplayer[cppos] = oldinownAI;
        }
        if (player == "P2")  //turnplayer
        {
            oldstateAI = stateai[cppos];
            oldinownAI = inownai[cppos];

            stateai[cppos] = stateplayer[caipos];
            inownai[cppos] = inownplayer[caipos];
            stateplayer[caipos] = oldstateAI;
            inownplayer[caipos] = oldinownAI;
        }
    }

    public void aistart()
    {
        print("AI Activ");
        AICount++;
        StartCoroutine(AIbreath(1));
    }

    void doTestGrave()
    {
        SelectedAICard = AIPlayer.GetComponent<Players>().lists[0];



        StartCoroutine(aiMoveCardfromAtoB(SelectedAICard, Grave));



    }

    IEnumerator AIbreath(int decission)
    {
        yield return new WaitForSeconds(1f);

        AIworking = true;
        StartCoroutine(checkclear());
        while (AIworking)
        {
            yield return new WaitForSeconds(0.1f);
        }

        AIworking = true;
        checkmonkey();
        while (AIworking)
        {
            yield return new WaitForSeconds(0.1f);
        }

        AIworking = true;
        StartCoroutine(checkdraw());
        while (AIworking)
        {
            yield return new WaitForSeconds(0.1f);
        }

        AIworking = true;
        StartCoroutine(checkclear());
        while (AIworking)
        {
            yield return new WaitForSeconds(0.1f);
        }
        timer.skip();

    }


    IEnumerator aiMoveCardfromAtoB(GameObject SAICard, GameObject AITo)
    {
        ihandler.input(AIPlayer, 1, SAICard);
        float baseX = SAICard.transform.position.x;
        float baseY = SAICard.transform.position.y;

        print(baseX);

        float aimX = AITo.transform.position.x;
        float aimY = AITo.transform.position.y;
        print(aimX);   
        float deltaX = (aimX - baseX) / 80;
        float deltaY = (aimY - baseY) / 80;
        for (int i = 0; i < 80; i++)
        {
            yield return new WaitForSeconds(0.01f);
            ihandler.mouseInput(AIPlayer, baseX + deltaX * i, baseY + deltaY * i);

        }
        ihandler.input(AIPlayer, 2, AITo);
        AImoving = false;
    }

    public void aicheckcounter()
    {
        StartCoroutine(checkKing());


    }

    IEnumerator checkKing()
    {
        int loop = 0;
        bool kingfound = false;
         yield return new WaitForSeconds(1f);
            foreach (string AIState in inownai)
        {
            if (AIState == "K")
            {
                SelectedAICard = AIPlayer.GetComponent<Players>().lists[loop];
                AImoving = true;
                kingfound = true;
                StartCoroutine(aiMoveCardfromAtoB(SelectedAICard, Grave));
                while (AImoving)
                {
                    yield return new WaitForSeconds(0.1f);
                }
            }
            loop++;
        }

        if (!kingfound)
        {
            timer.skip();
        }
    }



    IEnumerator checkclear()
    {
        if (Grave.transform.childCount > 0)
        {
            int loop = 0;
            foreach (string AIState in inownai)
            {

                if (AIState == Grave.transform.GetChild(0).gameObject.name)
                {

                    SelectedAICard = AIPlayer.GetComponent<Players>().lists[loop];

                    AImoving = true;
                    StartCoroutine(aiMoveCardfromAtoB(SelectedAICard, Grave));
                    while (AImoving)
                    {
                        yield return new WaitForSeconds(0.1f);
                    }
                    EffectRes = true;
                    StartCoroutine(checkeffect());
                    while (EffectRes)
                    {
                        yield return new WaitForSeconds(0.1f);
                    }

                }
                loop++;
            }

        }
        AIworking = false;
    }

    IEnumerator checkeffect()
    {
        while (timer.timerfz)
        {
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.1f);

        string ect = Grave.GetComponent<Graves>().effect;
        GameObject lookAt = null;
        int loop = 0;


        if (ect != "_")
        {
            if (ect == "8")
            {
                loop = 0;
                foreach (string AIState in stateai)
                {
                    if (AIState != "-")
                    {
                        lookAt = AIPlayer.GetComponent<Players>().lists[loop];
                    }
                    loop++;
                }

                loop = 0;
                foreach (string AIState in stateai)
                {
                    if (AIState == "U" || AIState == "PK")
                    {
                        lookAt = AIPlayer.GetComponent<Players>().lists[loop];
                    }
                    loop++;
                }

                if (lookAt != null)
                {
                    ihandler.input(AIPlayer, 1, lookAt);
                }
            }

            else if (ect == "7")
            {
                loop = 0;
                foreach (string AIState in stateplayer)
                {
                    if (AIState != "-")
                    {
                        lookAt = OPPlayer.GetComponent<Players>().lists[loop];
                    }
                    loop++;
                }

                loop = 0;
                foreach (string AIState in stateplayer)
                {
                    if (AIState == "U" || AIState == "PK")
                    {
                        lookAt = OPPlayer.GetComponent<Players>().lists[loop];
                    }
                    loop++;
                }

                if (lookAt != null)
                {
                    ihandler.input(AIPlayer, 1, lookAt);
                }
            }

            else if (ect == "x" && Grave.GetComponent<Graves>().ceffect == "9")
            {
                while (Grave.GetComponent<Graves>().effect == "x")
                {
                    yield return new WaitForSeconds(0.1f);
                }
                if (Grave.GetComponent<Graves>().effect == "9")
                {
                    timer.skip();
                    GameObject bAIcard;
                    GameObject pAIcard;
                    int cAvg = 7;
                    int playerKnowValue = 3;
                    int highestpos = -1;
                    int highestvalue = 0;
                    int lowestvalue = 14;
                    int AIStateValue = 0;
                    loop = 0;
                    foreach (string AIState in inownai)
                    {
                        if (AIState == "U")
                        {
                            AIStateValue = cAvg;
                        }
                        else
                        {
                            AIStateValue = AIValue(AIState);
                        }

                        if (AIStateValue > highestvalue)
                        {
                            highestvalue = AIStateValue;
                            highestpos = loop;
                        }
                        loop++;
                    }
                    bAIcard = AIPlayer.GetComponent<Players>().lists[highestpos];

                    loop = 0;
                    foreach (string PState in inownplayer)
                    {
                        if (PState == "U")
                        {
                            if (stateplayer[loop] == "U")
                            {
                                AIStateValue = cAvg;
                            }
                            else if (stateplayer[loop] == "PK")
                            {
                                AIStateValue = cAvg - playerKnowValue;
                            }
                        }
                        else if (PState == "-")
                        {
                            AIStateValue = 100;
                        }
                        else
                        {
                            AIStateValue = AIValue(PState);
                        }

                        if (AIStateValue < lowestvalue)
                        {
                            lowestvalue = AIStateValue;
                            highestpos = loop;
                        }
                        loop++;
                    }
                    pAIcard = OPPlayer.GetComponent<Players>().lists[highestpos];
                    
                    ihandler.input(AIPlayer, 1, bAIcard);
                    yield return new WaitForSeconds(0.1f);
                    ihandler.input(AIPlayer, 2, bAIcard);
                    yield return new WaitForSeconds(1f);

                    ihandler.input(AIPlayer, 1, pAIcard);
                    yield return new WaitForSeconds(0.1f);
                    ihandler.input(AIPlayer, 2, pAIcard);

                }
            }
        }
        EffectRes = false;
    }


    void checkmonkey()
    {
        if(!monkeyCall){

        }
        AIworking = false;
    }

    IEnumerator checkdraw()
    {
        bool close = false;
        ihandler.input(AIPlayer, 1, Deck);
        yield return new WaitForSeconds(1.2f);

        if (Deck.transform.childCount > 0)
        {
            SelectedAICard = Deck.transform.GetChild(0).gameObject;

            GameObject destination = Grave;
            int loop = 0;
            foreach (string AIState in stateai)
            {
                if (AIState == "U" || AIState == "PK")
                {
                    destination = AIPlayer.GetComponent<Players>().lists[loop];
                    close = true;
                }
                loop++;
            }

            if (!close)
            {

                int highestpos = -1;
                int highestvalue = 0;
                int AIStateValue;
                loop = 0;
                int compareValue = AIValue(SelectedAICard.name);
                foreach (string AIState in inownai)
                {
                    AIStateValue = AIValue(AIState);
                    if (AIStateValue > highestvalue)
                    {
                        highestvalue = AIStateValue;
                        highestpos = loop;
                    }
                    loop++;
                }

                if (highestvalue > compareValue)
                {
                    destination = AIPlayer.GetComponent<Players>().lists[highestpos];
                }
                close = true;
            }

            AImoving = true;
            StartCoroutine(aiMoveCardfromAtoB(SelectedAICard, destination));
            while (AImoving)
            {
                yield return new WaitForSeconds(0.1f);
            }

            EffectRes = true;
            StartCoroutine(checkeffect());
            while (EffectRes)
            {
                yield return new WaitForSeconds(0.1f);
            }
            AIworking = false;
        }
    }


    int AIValue(string Cardvalue)
    {
        int ReturnValue = 0;
        if (Cardvalue == "-")
            ReturnValue = -1;
        else if (Cardvalue == "K")
        {
            if (!monkeyCall)
            {
                ReturnValue = 2;
            }
            else
            {
                ReturnValue = 13;
            }

        }
        else if (Cardvalue == "Q" || Cardvalue == "J")
        {
            ReturnValue = 10;
        }
        else if (Cardvalue == "A")
        {
            ReturnValue = 1;
        }
        else
        {
            ReturnValue = int.Parse(Cardvalue);
        }

        return ReturnValue;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
