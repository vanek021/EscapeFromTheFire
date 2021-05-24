using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    public float volume = 0;
    public int quality = 0;
    public bool isFullscreen = false;
    public AudioMixer audioMixer;
    public Button[] buttons = new Button[5];
    public Text[] buttonTexts = new Text[5];

    public Button settingsBackButton;
    public Text textSettingsBackButton;

    public Button registerButton;
    public Text textRegisterButton;

    public Button loginButton;
    public Text textLoginButton;

    public Button materialsBackButton;
    public Text textMaterialsBackButton;

    public Button statsBackButton;
    public Text textStatsBackButton;

    public Toggle toggleHelp;

    // Start is called before the first frame update
    void Start()
    {
        AddTriggersToButtons();
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void AddTriggersToButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            var button = buttons[i];
            var text = buttonTexts[i];
            AddTriggers(button, text);
        }
        AddTriggers(settingsBackButton, textSettingsBackButton);
        AddTriggers(materialsBackButton, textMaterialsBackButton);
        AddTriggers(statsBackButton, textStatsBackButton);
    }

    public void ChangeVolume(float val)
    {
        DataHolder.volume = val;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void AddTriggers(Button button, Text text)
    {
        var eventPEnter = new EventTrigger.Entry();
        var eventPExit = new EventTrigger.Entry();
        var eventPClick = new EventTrigger.Entry();
        var trig = button.gameObject.GetComponent<EventTrigger>();

        eventPEnter.eventID = EventTriggerType.PointerEnter;
        eventPEnter.callback.AddListener((data) => {
            text.color = new Color(0, 0, 0, 255);
            button.image.color = new Color(255, 2555, 255, 255);
        });
        trig.triggers.Add(eventPEnter);

        eventPExit.eventID = EventTriggerType.PointerExit;
        eventPExit.callback.AddListener((x) => {
            text.color = new Color(255, 255, 255, 255);
            button.image.color = new Color(255, 255, 255, 0);
        });
        trig.triggers.Add(eventPExit);

        eventPClick.eventID = EventTriggerType.PointerClick;
        eventPClick.callback.AddListener((x) => {
            text.color = new Color(255, 255, 255, 255);
            button.image.color = new Color(255, 255, 255, 0);
        });
        trig.triggers.Add(eventPClick);
    }

    public void PlayButton()
    {
        SceneManager.UnloadSceneAsync("Menu");
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void LearningStatusUpdate() 
    {
        if (toggleHelp.enabled)
            DataHolder.isLearnEnabled = false;
        else
            DataHolder.isLearnEnabled = true;
    }
}