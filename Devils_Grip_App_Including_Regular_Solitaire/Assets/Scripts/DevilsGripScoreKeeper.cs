﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilsGripScoreKeeper : MonoBehaviour
{
    public DevilsGripSelectable[] topStacks;
    public GameObject highScorePanel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (HasWon())
        {
            Win();
        }
    }

    public bool HasWon()

    {
        int i = 0;
        foreach (DevilsGripSelectable topstack in topStacks)
        {
            i += topstack.value;
        }
        if (i >= 52)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    void Win()
    {
        highScorePanel.SetActive(true);
        print("Yay....You win");
    }

}
