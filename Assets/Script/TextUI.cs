using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUI : MonoBehaviour
{
    [SerializeField]
    Text timeText;
    [SerializeField]
    Text goldText;

    int timer;
    
    

    void Update()
    {
        timer = (int)GameStates.instance.timeRemaining;

        if(GameStates.instance.timeRemaining == 1200.0f)
        {
            timeText.text = "∞" ;
            goldText.text = GameStates.instance.goldRemaining + "/" + GameStates.instance.numberOfGold;

        }
        else
        {
            timeText.text = timer / 60 + ":" + timer % 60;
            goldText.text = GameStates.instance.goldRemaining + "/" + GameStates.instance.numberOfGold;
        }
        


       
    }
}
