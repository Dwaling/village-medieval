using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputField : MonoBehaviour
{

    public Text text;
   
 
    public void SetUpperCase()
    {
        text.text = text.text.ToUpper();
    }
}
