using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    bool Press_StartKey = false;
    bool Opening_Options = false;
    enum Option_State
    {
        None,
        Top,
        GameSetting,
        Keybinds
    }
    Option_State Current_OptionState;

    [SerializeField] GameObject Option_Window;
    [SerializeField] GameObject Keybind_Window;
    [SerializeField] GameObject Setting_Window;
    [SerializeField] Slider Sound_Slider;
    [SerializeField] Toggle Enable_FullScreen;

    AudioScript audioScript;
    [SerializeField] AudioClip BGM_File;

    // Start is called before the first frame update
    void Start()
    {
        Option_Window.SetActive(false);
        Setting_Window.SetActive(false);
        Keybind_Window.SetActive(false);
        Current_OptionState = Option_State.None;
        audioScript = GameObject.Find("BGM").GetComponent<AudioScript>();
        audioScript.PlayAudio(BGM_File);
    }

    // Update is called once per frame
    void Update()
    {
        //ゲームスタート
        if (Input.GetKeyDown(KeyCode.Space) && !Opening_Options)
        {
            Press_StartKey = true;
            SceneManager.LoadScene("Stage1");
        }
        if (Input.GetKeyDown(KeyCode.F) && !Press_StartKey)
        {
            Opening_Options = true;
            Option_Window.SetActive(true);
            Current_OptionState = Option_State.Top;
        }

        if (Opening_Options) { OptionWindow_Script(); }

        //ゲーム終了
        if (Input.GetKeyDown(KeyCode.Escape) && !Opening_Options && !Press_StartKey)
        {
            Application.Quit();
        }
    }

    void OptionWindow_Script()
    {
        switch (Current_OptionState)
        {
            case Option_State.Top:
                Option_Window.SetActive(true);
                Setting_Window.SetActive(false);
                Keybind_Window.SetActive(false);
                break;
            case Option_State.GameSetting:
                Option_Window.SetActive(false);
                Setting_Window.SetActive(true);
                Keybind_Window.SetActive(false);

                audioScript.Set_AudioParameter(Sound_Slider.value);
                Screen.fullScreen = Enable_FullScreen.isOn;
                break;
            case Option_State.Keybinds:
                Option_Window.SetActive(false);
                Setting_Window.SetActive(false);
                Keybind_Window.SetActive(true);
                break;
        }
    }

    /*ボタンイベント*/
    public void Open_GameSettings()
    {
        Current_OptionState = Option_State.GameSetting;
        Sound_Slider.value = audioScript.Get_AudioVolume();
        Enable_FullScreen.isOn = Screen.fullScreen;
    }
    public void Open_Keybinds()
    {
        Current_OptionState = Option_State.Keybinds;
    }
    public void Backto_OptionWindow()
    {
        Current_OptionState = Option_State.Top;
    }
    public void Close_OptionWindow()
    {
        Current_OptionState = Option_State.None;
        Opening_Options = false;
        Option_Window.SetActive(false);
    }
}
