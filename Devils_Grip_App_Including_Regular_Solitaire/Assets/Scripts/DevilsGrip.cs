using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DevilsGrip : MonoBehaviour
{
    public Sprite[] cardFaces;
    public GameObject cardPrefab;
    public GameObject deckButton;
    public GameObject[] bottomPos;
    public GameObject[] middlePos;
    public GameObject[] topPos;



    //only difference here is that I doubled the number of suites and also created middle and top list arrays to be used for card assignments
    public static string[] suits = new string[] { "C", "D", "H", "S", "C", "D", "H", "S"};
    public static string[] values = new string[] {"2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"};
    public List<string>[] bottoms;
    public List<string>[] middles;
    public List<string>[] tops;
    public List<string> tripsOnDisplay = new List<string>();
    public List<List<string>> deckTrips = new List<List<string>>();

    private List<string> bottom0 = new List<string>();
    private List<string> bottom1 = new List<string>();
    private List<string> bottom2 = new List<string>();
    private List<string> bottom3 = new List<string>();
    private List<string> bottom4 = new List<string>();
    private List<string> bottom5 = new List<string>();
    private List<string> bottom6 = new List<string>();
    private List<string> bottom7 = new List<string>();

    private List<string> middle0 = new List<string>();
    private List<string> middle1 = new List<string>();
    private List<string> middle2 = new List<string>();
    private List<string> middle3 = new List<string>();
    private List<string> middle4 = new List<string>();
    private List<string> middle5 = new List<string>();
    private List<string> middle6 = new List<string>();
    private List<string> middle7 = new List<string>();

    private List<string> top0 = new List<string>();
    private List<string> top1 = new List<string>();
    private List<string> top2 = new List<string>();
    private List<string> top3 = new List<string>();
    private List<string> top4 = new List<string>();
    private List<string> top5 = new List<string>();
    private List<string> top6 = new List<string>();
    private List<string> top7 = new List<string>();


    public List<string> deck;
    public List<string> discardPile = new List<string>();
    private int deckLocation;
    private int trips;
    private int tripsRemainder;



    // Start is called before the first frame update
    // added arraylists for the top and middle rows here
    void Start()
    {
        bottoms = new List<string>[] { bottom0, bottom1, bottom2, bottom3, bottom4, bottom5, bottom6, bottom7, middle0, middle1, middle2, middle3, middle4, middle5, middle6, middle7, top0, top1, top2, top3, top4, top5, top6, top7};
        middles = new List<string>[] { middle0, middle1, middle2, middle3, middle4, middle5, middle6, middle7};
        tops = new List<string>[] {top0, top1, top2, top3, top4, top5, top6, top7};
        PlayCards();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayCards()
    {
        foreach (List<string> list in bottoms)
        {
            list.Clear();
        }

        deck = GenerateDeck();
        Shuffle(deck);

        //test the cards in the deck:
        foreach (string card in deck)
        {
            print(card);
        }
        DevilsGripSort();
        StartCoroutine(DevilsGripDeal());
        SortDeckIntoTrips();

    }


    //Generates the deck by combining the suites with values for each card in the deck that is generated.
    public static List<string> GenerateDeck()
    {
        List<string> newDeck = new List<string>();
        foreach (string s in suits)
        {
            foreach (string v in values)
            {
                newDeck.Add(s + v);
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

    //added an xOffset variable to stack the cards horizontally rather than vertically
    IEnumerator DevilsGripDeal()
    {
        for (int i = 0; i < 8; i++)
        {
            float yOffset = 0;
            float zOffset = 0.03f;
            foreach (string card in bottoms[i])
            {
                yield return new WaitForSeconds(0.05f);
                GameObject newCard = Instantiate(cardPrefab, new Vector3(bottomPos[i].transform.position.x, bottomPos[i].transform.position.y - yOffset, bottomPos[i].transform.position.z - zOffset), Quaternion.identity, bottomPos[i].transform);
               // GameObject newCard1 = Instantiate(cardPrefab, new Vector3(middlePos[i].transform.position.x, middlePos[i].transform.position.y - yOffset, middlePos[i].transform.position.z - zOffset), Quaternion.identity, middlePos[i].transform);
               // GameObject newCard2 = Instantiate(cardPrefab, new Vector3(topPos[i].transform.position.x, topPos[i].transform.position.y - yOffset, topPos[i].transform.position.z - zOffset), Quaternion.identity, topPos[i].transform);
                newCard.name = card;
                newCard.GetComponent<DevilsGripSelectable>().row = i;
                if (card == bottoms[i][bottoms[i].Count - 1])
                {
                    newCard.GetComponent<Selectable>().faceUp = true;
                }

                yOffset = yOffset + 0.0f;
                zOffset = zOffset + 0.03f;
                discardPile.Add(card);
            }
        }

        foreach (string card in discardPile)
        {
            if (deck.Contains(card))
            {
                deck.Remove(card);
            }
        }
        discardPile.Clear();

    }

    void DevilsGripSort()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = i; j < 8; j++)
            {
                bottoms[j].Add(deck.Last<string>());
                deck.RemoveAt(deck.Count - 1);
            }

        }

    }

    public void SortDeckIntoTrips()
    {
        trips = deck.Count / 3;
        tripsRemainder = deck.Count % 3;
        deckTrips.Clear();

        int modifier = 0;
        for (int i = 0; i < trips; i++)
        {
            List<string> myTrips = new List<string>();
            for (int j = 0; j < 3; j++)
            {
                myTrips.Add(deck[j + modifier]);
            }
            deckTrips.Add(myTrips);
            modifier = modifier + 3;
        }
        if (tripsRemainder != 0)
        {
            List<string> myRemainders = new List<string>();
            modifier = 0;
            for (int k = 0; k < tripsRemainder; k++)
            {
                myRemainders.Add(deck[deck.Count - tripsRemainder + modifier]);
                modifier++;
            }
            deckTrips.Add(myRemainders);
            trips++;
        }
        deckLocation = 0;

    }

    public void DealFromDeck()
    {
        // add remaining cards to discard pile

        foreach (Transform child in deckButton.transform)
        {
            if (child.CompareTag("Card"))
            {
                deck.Remove(child.name);
                discardPile.Add(child.name);
                Destroy(child.gameObject);
            }
        }


        if (deckLocation < trips)
        {
            // draw 3 new cards
            tripsOnDisplay.Clear();
            float xOffset = 2.5f;
            float zOffset = -0.2f;

            foreach (string card in deckTrips[deckLocation])
            {
                GameObject newTopCard = Instantiate(cardPrefab, new Vector3(deckButton.transform.position.x + xOffset, deckButton.transform.position.y, deckButton.transform.position.z + zOffset), Quaternion.identity, deckButton.transform);
                xOffset = xOffset + 0.5f;
                zOffset = zOffset - 0.2f;
                newTopCard.name = card;
                tripsOnDisplay.Add(card);
                newTopCard.GetComponent<Selectable>().faceUp = true;
                newTopCard.GetComponent<Selectable>().inDeckPile = true;
            }
            deckLocation++;

        }
        else
        {
            //Restack the top deck
            RestackTopDeck();
        }
    }

    void RestackTopDeck()
    {
        deck.Clear();
        foreach (string card in discardPile)
        {
            deck.Add(card);
        }
        discardPile.Clear();
        SortDeckIntoTrips();
    }
}
