using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUI : MonoBehaviour
{
    Text uiText;
    int timer;
    
    void Awake()
    {
        uiText = GetComponent<Text>();
    }

    void Update()
    {
        timer = (int)GameStates.timeRemaining;

        if(GameStates.timeRemaining == 1200.0f)
        {
              uiText.text = "Time remaining: " + "∞" + "\nGold remaining: " + GameStates.goldRemaining + "/" + GameStates.numberOfGold;

        }
        else
        {
            uiText.text = "Time remaining: " + timer / 60 + ":" + timer % 60 + "\nGold remaining: " + GameStates.goldRemaining + "/" + GameStates.numberOfGold;

        }
        


       
    }
}
