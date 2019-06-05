using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    void Update()
    {
        if ((GameStates.playerPosition - gameObject.transform.position).magnitude < 3.0f)
        {
            AkSoundEngine.PostEvent("Play_gold", gameObject);
            GameStates.goldRemaining--;
            AkSoundEngine.PostEvent("Stop_goldShine", gameObject);
            GameStates.instance.GoldAsBeenFound();
            Destroy(gameObject);
        }
    }
}
