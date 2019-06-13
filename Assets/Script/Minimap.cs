using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    public GameObject iconParent;
    public Image lancelotIcon;
    public GameObject character;
    bool isAlreadyInit = false;

    Vector3 characterPosition = new Vector3(0.0f, 0.0f, 0.0f);
    Vector3 lancelotIconPosition = new Vector3(0.0f, 0.0f, 0.0f);
    [SerializeField]
    GameObject goldIconPrefab;
    [SerializeField]
    List<GameObject> goldIconList;
    float worldMapHeight = 197.5313f;
    float worldMapWidth = 197.5313f;
    float miniMapHeight = 321f;
    float miniMapWidth = 348f;

    void Start()
    {
        characterPosition = character.transform.position + gameObject.transform.position;
    }

    void Update()
    {
        characterPosition = FromWorldToMiniMapSpace(character.transform.position);

        if (GameStates.instance.GetSelectedGoldList() != null)
        {
            if(!isAlreadyInit && GameStates.isGameStarted)
            {
                isAlreadyInit = true;

                foreach(GameObject gold in GameStates.instance.GetGoldList())
                {
                    GameObject newGoldIcon = Instantiate(goldIconPrefab, gold.transform.position, Quaternion.identity, iconParent.transform);
                    newGoldIcon.transform.position = FromWorldToMiniMapSpace(newGoldIcon.transform.position) + gameObject.transform.position;
                    gold.GetComponent<Destroy>().SetGoldIcon(newGoldIcon);
                    goldIconList.Add(newGoldIcon);
                }
            }
        }

        lancelotIcon.rectTransform.position = (characterPosition + gameObject.transform.position);
    }

    public Vector3 FromWorldToMiniMapSpace(Vector3 position)
    {
        return new Vector3((position.x / worldMapWidth) * miniMapWidth, (position.z / worldMapHeight) * miniMapHeight, 0.0f);
    }

    public void SetIsAlreadyInit(bool setInit)
    {
        isAlreadyInit = setInit;
    }
}
