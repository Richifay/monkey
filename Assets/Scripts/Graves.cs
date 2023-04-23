using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Graves : MonoBehaviour
{	
	private AI ai;
    private Animators animators;	
    private MonkeyG monkeyg;	
	private Timer timer;
	public string effect;
	public string ceffect;
	public bool react;
	public GameObject gravo;
    // Start is called before the first frame update
    void Start()
    {
       	monkeyg = FindObjectOfType<MonkeyG>(); 
		animators = FindObjectOfType<Animators>(); 
		timer = FindObjectOfType<Timer>();
		ai = FindObjectOfType<AI>();
		ceffect = "_";
		effect = "_";
		react = false;
    }
    // Update is called once per frame
    void Update()
    {
		if (gravo)
		{
		gravo.GetComponent<Selectable>().faceUp = true;
		}
		
		if (react)
		{
			if (!timer.timeractiv)
			{
					print("zuspät du nuttensohn");
				
				if ( ceffect == "9")
				{
				   timer.calltimer(10);
				   effect = "9";	
				}
				
				if ( ceffect == "10")
				{
					timer.calltimer(7);
					GameObject oplay = monkeyg.OPlay();
					monkeyg.MonkeyDeal(oplay, 1 );
					effect = "_";
				}
				react = false;
			}
		}
	}
	
   public void reset(){
		ceffect = "_";
		effect = "_";
		react = false;	
	if(gravo){
		Destroy(gravo);
	}
   }
	
   public bool kontercheck(GameObject gravi){
		bool check = false;
		if (gravi.name == "K"){
			check = true;
	     StartCoroutine( animators.animate(4));			
		}
	 return check;	
	}


	public bool fillgravecheck(GameObject gravi){
		bool check = false;
		if (gravo){
		if (gravi.name == gravo.name){
			check = true;
		}
		}
	 return check;	
	}
	
	public void fillgrave(GameObject gravi)
	{
		if(gravo){
			Destroy(gravo);
	}
	gravo = gravi;
	gravo.transform.position = transform.position;
    gravo.GetComponent<Selectable>().inOwn = "G";
	gravo.GetComponent<Selectable>().capos = -1 ;
	gravo.transform.SetParent(transform);
	checkeffect(gravo.name);
 	}
	
	public void checkeffect (string ce){

 if (ce == "7"){
	 effect = "7";

	 timer.calltimer(7);
	 StartCoroutine( animators.animate(0));
	 
	
 }
 if (ce == "8"){
	   effect = "8";

	    timer.calltimer(7);
	    StartCoroutine( animators.animate(1));
	 
    
 }
  if (ce == "9"){
	   effect = "x";
	   ceffect = "9";
	   timer.calltimer(10);
	   reactstart();
	   StartCoroutine( animators.animate(2));
		
		 
 }
 if (ce == "10"){
	  effect = "x";
	  ceffect = "10";
	  timer.calltimer(10);
	  reactstart();
	  StartCoroutine( animators.animate(3));
	}
	}
	

	
	void reactstart()
	{
		react = true;
		if(monkeyg.turnplay == "P1")
		{
			ai.aicheckcounter();
		}
		print("reagier du hundesohn");
	}
}

