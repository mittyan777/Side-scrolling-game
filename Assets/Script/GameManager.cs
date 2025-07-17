using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Player player_Script;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (player_Script == null)
        {
            player_Script = GameObject.FindWithTag("Player").GetComponent<Player>();
            Debug.Log("Player OK");
        }

        if (player_Script.Get_Player_IsDead() == false)
        {
            Debug.Log("Player Aliving");
        }
    }
}
