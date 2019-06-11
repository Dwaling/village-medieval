using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    public GameObject LancelotIconParent;
    public Image LancelotIcon;
    public GameObject Character;
    bool isAlreadyInit = false;
    public GameObject GoldIconParent;

    Vector3 CharacterPosition = new Vector3(0.0f, 0.0f, 0.0f);
    Vector3 LancelotIconPosition = new Vector3(0.0f, 0.0f, 0.0f);
    [SerializeField]
    GameObject goldIcon;
    List<GameObject> GoldIconList;
    float WorldMapHeight = 197.5313f;
    float WorldMapWidth = 197.5313f;
    float MiniMapHeight = 321f;
    float MiniMapWidth = 348f;

    void Start()
    {
        CharacterPosition = Character.transform.position + gameObject.transform.position;
    }

    void Update()
    {
        CharacterPosition = new Vector3((Character.transform.position.x / WorldMapWidth) * MiniMapWidth, (Character.transform.position.z / WorldMapHeight) * MiniMapHeight, 0.0f);

        if(GameStates.instance.GetSelectedGoldList() != null)
        {
            if(!isAlreadyInit && GameStates.isGameStarted)
            {
                List<Transform> SelectedGoldListToSpawn = GameStates.instance.GetSelectedGoldList();
                isAlreadyInit = true;
                foreach(Transform items in SelectedGoldListToSpawn)
                {
                    GameObject newGoldIcon = Instantiate(goldIcon, FromWorldToMiniMapSpace(items.position), Quaternion.identity, GoldIconParent.transform);
                    GoldIconList.Add(newGoldIcon);
                }
            }
        }
        
        LancelotIcon.rectTransform.position = (CharacterPosition + gameObject.transform.position);
    }

    public Vector3 FromWorldToMiniMapSpace(Vector3 goldWS)
    {
        return new Vector3((goldWS.x / WorldMapWidth) * MiniMapWidth, (goldWS.z / WorldMapHeight) * MiniMapHeight, 0.0f);
    }
}
