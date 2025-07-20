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

    [Header("BGMリスト")]
    AudioScript audioScript;
    [SerializeField] AudioClip BGM_File;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        player_Script = GameObject.FindWithTag("Player").GetComponent<Player>();
        audioScript = GameObject.Find("BGM").GetComponent<AudioScript>();

        StageInitialize(Default_StageTimer);
        Debug.Log("player_Script");
    }

    // Update is called once per frame
    void Update()
    {
        if (Player_Goal)
        {
            audioScript.Fadeout_AudioVolume();
            Invoke("NextStage", 7);
        }
        else if (!Player_Dead)
        {
            if (player_Script.Get_Player_IsDead() == true || Set_StageTimer <= 0)
            {
                Player_Dead = true;
                audioScript.Stop_Audio();
                Debug.Log("Player Dead!");
                Invoke("ReloadScene", 3);
            }
            if (Set_StageTimer > 0)
            {
                Set_StageTimer -= Time.deltaTime;
                Label_Timer.text = string.Format("Time : {0:D3}", (int)Set_StageTimer);
            }
            else
            {
                Label_Timer.text = string.Format("Time : {0:D3}", 0);
            }
        }
    }

    void StageInitialize(float T)
    {
        Set_StageTimer = T;
        Current_StageNo = SceneManager.GetActiveScene().buildIndex;
        Label_StageCount.text = $"Stage {Current_StageNo}";
        audioScript.PlayAudio(BGM_File);
    }

    void ReloadScene()
    {
        //ステージを再読み込み
        SceneManager.LoadScene(Current_StageNo);
    }

    void NextStage()
    {
        Current_StageNo = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(Current_StageNo);
    }

    public void Set_GoalState(bool a)
    {
        Player_Goal = a;
        player_Script.Set_Player_Goal(a);
    }
    public bool Get_Is_TimeUP() { return Set_StageTimer <= 0; }
}
