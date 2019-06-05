using UnityEngine;
using System.Collections;
using UnityEditor;

//[ExecuteInEditMode]
[CustomEditor(typeof(LocalizationManager))]
public class LocalizationManagerEditor : Editor
{
    public int selected = 0;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LocalizationManager myTarget = (LocalizationManager)target;
        GUILayout.Space(14.0f);

        GUILayout.BeginHorizontal("", GUILayout.MaxHeight(14.0f));
    
        string[] languageName = new string[myTarget.languages.Count];
        for (int i = 0; i < languageName.Length; i++)
        {
            if(myTarget.languages[i].languageName != null && myTarget.languages[i].languageName != "" && myTarget.languages[i].fileName != null && myTarget.languages[i].fileName != "")
            languageName[i] = myTarget.languages[i].languageName;
        }
        selected = EditorGUILayout.Popup("Choose Language", selected, languageName, EditorStyles.popup);

        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Switch Language"))
        {
            //myTarget.ForceLanguage(myTarget.languages[selected].fileName);
            myTarget.LoadLocalizedText(myTarget.languages[selected].fileName);
            UpdateLocalization();
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(14.0f);
        EditorGUILayout.HelpBox("Tips: Each GameObject with Text component must have: \n\n1. The LocalizedText script(The key must be identical to the value in the file). \n\n2. The LocalizedText Tag.\n", MessageType.Info);
    }

    private void UpdateLocalization()
    {
        GameObject[] localizationTexts = GameObject.FindGameObjectsWithTag("LocalizedText");
        foreach (GameObject textObj in localizationTexts)
        {
            textObj.GetComponent<LocalizedText>().ChangeLanguageText();
        }
    }
}
