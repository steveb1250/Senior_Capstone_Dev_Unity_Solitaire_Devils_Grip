using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevilsGripUIButtons : MonoBehaviour
{
    public GameObject highScorePanel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayAgain()
    {
        highScorePanel.SetActive(false);
        ResetScene();
    }

    public void ResetScene()
    {
        // find all the cards and remove them
        DevilsGripUpdateSprite[] cards = FindObjectsOfType<DevilsGripUpdateSprite>();
        foreach (DevilsGripUpdateSprite card in cards)
        {
            Destroy(card.gameObject);
        }
        ClearTopValues();
        // deal new cards
        FindObjectOfType<DevilsGrip>().PlayCards();
    }

    void ClearTopValues()
    {
        DevilsGripSelectable[] selectables = FindObjectsOfType<DevilsGripSelectable>();
        foreach (DevilsGripSelectable selectable in selectables)
        {
            if (selectable.CompareTag("Top"))
            {
                selectable.suit = null;
                selectable.value = 0;
            }
        }
    }

    //Function to return to main menu screen of main menu scene
    public void SolitaireMainMenuReturn()
    {
        SceneManager.LoadScene(0);
    }

}
