using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI関係")]
    [SerializeField] Text Label_StageCount;
    [SerializeField] Text Label_Timer;
    Player player_Script;
    [SerializeField] float Default_StageTimer;
    float Set_StageTimer;
    bool Player_Dead;
    bool Player_Goal;

    static int Current_StageNo = 0;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        StageInitialize(Default_StageTimer);

        player_Script = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player_Goal)
        {
            Invoke("NextStage", 5);
        }
        else if (!Player_Dead)
        {
            if (player_Script.Get_Player_IsDead() == true)
            {
                Player_Dead = true;
                Debug.Log("Player Dead!");
                Invoke("ReloadScene", 3);

                return;
            }
            Set_StageTimer -= Time.deltaTime;
            Label_Timer.text = string.Format("Time : {0:D3}", (int)Set_StageTimer);
        }

    }

    void StageInitialize(float T)
    {
        Set_StageTimer = T;
        Current_StageNo = SceneManager.GetActiveScene().buildIndex;
        Label_StageCount.text = $"Stage {Current_StageNo}";
    }

    void ReloadScene()
    {
        //ステージを再読み込み
        SceneManager.LoadScene(Current_StageNo);
    }

    void NextStage()
    {
        //Current_StageNo = SceneManager.GetActiveScene().buildIndex+1;
        Current_StageNo = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(Current_StageNo);
    }

    public void Set_GoalState(bool a)
    {
        Player_Goal = a;
    }

    public bool Get_GoalState() { return Player_Goal; }
}
