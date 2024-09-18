using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    string[] prefabsPaths = { "Prefabs/Red Apple", "Prefabs/Green Apple", "Prefabs/Golden Apple" };

    float timer;
    const int cooldown = 1;

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            float appleIndex = Random.Range(0,1f);

            string appleSelected;

            switch(appleIndex)
            {
                case < 0.5f:
                    appleSelected = prefabsPaths[0];
                    break;
                case < 0.8f:
                    appleSelected = prefabsPaths[1];
                    break;
                default:
                    appleSelected = prefabsPaths[2];
                    break;
            }
            Vector2 screenBounds = GameManager.instance.ScreenBounds;
            NetworkManager.instance.Instantiate(appleSelected, new Vector2(Random.Range(-screenBounds.x, screenBounds.x), screenBounds.y), Quaternion.identity);
            timer = cooldown;
        }
    }
}
