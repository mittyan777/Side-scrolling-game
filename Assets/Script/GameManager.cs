using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI関係")]
    [SerializeField] Text Label_StageCount;
    [SerializeField] Text Label_Timer;
    Player player_Script;
    [SerializeField] float Stage_Timer;

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
        else
        {
            if (player_Script.Get_Player_IsDead() == true)
            {
                Debug.Log("Player Dead!");
                return;
            }
            Stage_Timer -= Time.deltaTime;
            Label_Timer.text = string.Format("Time : {0:D3}", (int)Stage_Timer);
        }

    }
    private void FixedUpdate()
    {

    }

    void StageInitialize(int StageTime)
    {
        Stage_Timer = StageTime;
    }
}
