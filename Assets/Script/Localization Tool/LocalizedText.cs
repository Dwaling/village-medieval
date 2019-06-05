using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour
{
    public string key;
    
    void Start()
    {
        ChangeLanguageText();
    }

    public void ChangeLanguageText()
    {
        Text text = GetComponent<Text>();
        text.text = LocalizationManager.instance.GetLocalizedValue(key);
    }
}
