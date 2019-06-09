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
    float WorldMapHeight = 197.0f;
    float WorldMapWidth = 197.0f;
    float MiniMapHeight = 244.1f;
    float MiniMapWidth = 278.0f;

    void Start()
    {
        CharacterPosition = Character.transform.position;
    }

    void Update()
    {
        Vector3 CharacterPosition = new Vector3((Character.transform.position.x / WorldMapHeight) * MiniMapHeight, (Character.transform.position.z / MiniMapHeight) * MiniMapWidth, 0.0f);

        if(GameStates.instance.GetSelectedGoldList() != null)
        {
            if(!isAlreadyInit)
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

        LancelotIcon.rectTransform.position = CharacterPosition;
    }

    public Vector3 FromWorldToMiniMapSpace(Vector3 goldWS)
    {
        return new Vector3((goldWS.x / 197.0f) * 244.1f, (goldWS.z / 197.0f) * 278.0f, 0.0f);
    }
}
