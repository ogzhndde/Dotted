using System;
using System.Collections;
using System.Collections.Generic;
using ParticleFactoryStatic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameData data;

    [SerializeField] private GameObject OBJ_FailPanel;
    [SerializeField] private GameObject OBJ_SettingPanel;
    [SerializeField] private Animator Anim_FailPanel => OBJ_FailPanel.GetComponent<Animator>();
    [SerializeField] private Animator Anim_SettingPanel => OBJ_SettingPanel.GetComponent<Animator>();

    [Space(15)]
    [Header("Buttons")]
    [SerializeField] private Button BTN_PanelActivate;
    [SerializeField] private Button BTN_SpeedUp;
    [SerializeField] private Button BTN_SpeedDown;
    [SerializeField] private Button BTN_AddDot;
    [SerializeField] private Button BTN_Restart;

    [Space(15)]
    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI TMP_ScoreText;
    [SerializeField] private TextMeshProUGUI TMP_HighScoreText;
    [SerializeField] private TextMeshProUGUI TMP_SpeedText;


    void Start()
    {
        InvokeRepeating(nameof(TextCheck), 0f, 0.2f);
    }

    void TextCheck()
    {
        TMP_ScoreText.text = data.CurrentScore.ToString("0");
        TMP_HighScoreText.text = "High Score: " + data.HighScore;

        TMP_SpeedText.text = "Speed\n" + data.DotSpeedMultiplier;
    }

    //######################################################### BUTTONS ##############################################################

    void ButtonSettingPanelActivation()
    {
        bool state = Anim_SettingPanel.GetBool("_isActive");
        Anim_SettingPanel.SetBool("_isActive", !state);
    }
    void ButtonSpeed(bool UpDown)
    {
        data.DotSpeedMultiplier += UpDown ? 0.25f : -0.25f;
    }
    void ButtonAddDot()
    {
        DotSpawner.SpawnDot();
    }

    void ButtonRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //########################################    EVENTS    ###################################################################

    private void OnEnable()
    {
        BTN_PanelActivate.onClick.AddListener(ButtonSettingPanelActivation);
        BTN_SpeedUp.onClick.AddListener(() => ButtonSpeed(true));
        BTN_SpeedDown.onClick.AddListener(() => ButtonSpeed(false));
        BTN_AddDot.onClick.AddListener(ButtonAddDot);
        BTN_Restart.onClick.AddListener(ButtonRestart);

        EventManager.AddHandler(GameEvent.OnScore, OnScore);
        EventManager.AddHandler(GameEvent.OnFinish, OnFinish);
    }

    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnScore, OnScore);
        EventManager.RemoveHandler(GameEvent.OnFinish, OnFinish);
    }

    private void OnFinish()
    {
        Anim_FailPanel.SetTrigger("_gameFail");
    }


    private void OnScore(object _scoreAdd, object _lastDot)
    {
        var lastDot = (GameObject)_lastDot;
        var scoreAdd = (float)_scoreAdd;

        data.CurrentScore += (int)scoreAdd;
        ParticleFactory.SpawnParticle(ParticleType.EarnScore, lastDot.transform.position, scoreAdd);
    }

}
