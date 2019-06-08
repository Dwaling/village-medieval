using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    [SerializeField]
    Transform player;
    List<Transform> golds;

    [SerializeField]
    Sprite playerIcon;
    [SerializeField]
    Sprite goldIcon;

    [SerializeField]
    Transform mapSize;

    [SerializeField]
    Sprite miniMap;

    float iconSize = 10;
    float iconHalfSize;
    // Start is called before the first frame update
    void Start()
    {
       // Instantiate<Transform>(player, miniMap  , true);
       // Vector3 playerPositionOnMinimap = player.position * mapSize.position;
        //Instantiate<Sprite>(playerIcon, mapSize, true);
    }

    // Update is called once per frame
    void Update()
    {
        iconHalfSize = iconSize / 2;

        
    }

    float GetMapPosX()
    {
        return player.transform.position.x * miniMap.border.x / mapSize.transform.localScale.x;
    }

    float GetMapPosY()
    {
        return player.transform.position.z * miniMap.border.y / mapSize.transform.localScale.y;
    }
}
