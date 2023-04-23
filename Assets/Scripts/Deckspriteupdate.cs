using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deckspriteupdate : MonoBehaviour
{
    public Sprite back;
    private SpriteRenderer spriteRenderer;
    private MonkeyG monkeyg;

    // Start is called before the first frame update
    void Start()
    {
        monkeyg = FindObjectOfType<MonkeyG>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
		{
        	if (monkeyg.deck.Count == 0)
        {
            	spriteRenderer.sprite = null;
        }
        else
        {
            spriteRenderer.sprite = back;
        }
    }
}
