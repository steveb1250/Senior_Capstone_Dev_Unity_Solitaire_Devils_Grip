using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilsGripUpdateSprite : MonoBehaviour
{
    public Sprite cardFace;
    public Sprite cardBack;
    private SpriteRenderer spriteRenderer;
    private DevilsGripSelectable DevilsGripSelectable;
    private DevilsGrip devilsGrip;
    private DevilsGripUserInput DevilsGripuserInput;



    // Start is called before the first frame update
    void Start()
    {
        List<string> deck = DevilsGrip.GenerateDeck();
        devilsGrip = FindObjectOfType<DevilsGrip>();
        DevilsGripuserInput = FindObjectOfType<DevilsGripUserInput>();

        int i = 0;
        foreach (string card in deck)
        {
            if (this.name == card)
            {
                cardFace = devilsGrip.cardFaces[i];
                break;
            }
            i++;
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        DevilsGripSelectable = GetComponent<DevilsGripSelectable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DevilsGripSelectable.faceUp == true)
        {
            spriteRenderer.sprite = cardFace;
        }
        else
        {
            spriteRenderer.sprite = cardBack;
        }

        if (DevilsGripuserInput.slot1)
        {

            if (name == DevilsGripuserInput.slot1.name)
            {
                spriteRenderer.color = Color.yellow;
            }
            else
            {
                spriteRenderer.color = Color.white;
            }
        }
    }
}
