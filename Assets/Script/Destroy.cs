using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    GameObject goldIcon;
    void Update()
    {
        if ((GameStates.playerPosition - gameObject.transform.position).magnitude < 3.0f)
        {
            AkSoundEngine.PostEvent("Play_gold", gameObject);
            GameStates.instance.goldRemaining--;
            AkSoundEngine.PostEvent("Stop_goldShine", gameObject);
            GameStates.instance.GoldAsBeenFound();
            Destroy(goldIcon);
            Destroy(gameObject);
        }
    }

    public GameObject GetGoldIcon()
    {
        return goldIcon;
    }

    public void SetGoldIcon(GameObject icon)
    {
        goldIcon = icon;
    }
}
