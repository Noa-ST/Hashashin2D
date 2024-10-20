﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : Singleton<GUIManager>
{
    [SerializeField] private GameObject _homeGUI;
    [SerializeField] private GameObject _gameGUI;
    [SerializeField] private ImageFilled _levelProgressBar;
    [SerializeField] private ImageFilled _hpProgressBar;
    [SerializeField] private Text _levelCountingTxt;
    [SerializeField] private Text _xpCountingTxt;
    [SerializeField] private Text _hpCountingTxt;
    [SerializeField] private Text _coinCountingTxt;
    [SerializeField] private Dialog _skillUpgradeDialog;
    [SerializeField] private Dialog _gameoverDialog;
    public GameObject damageTextPb;
    public Canvas gameCanvas;
    private Camera _mainCamera;
    private Dialog m_activeDialog;
    public Dialog ActiveDialog { get => m_activeDialog; private set => m_activeDialog = value; }

    protected override void Awake()
    {
        MakeSingleton(false);
        gameCanvas = FindObjectOfType<Canvas>();
        _mainCamera = Camera.main;
    }


    private void OnEnable()
    {
        CharacterEvents.characterDamaged += CharacterTookDamage;
    }

    private void OnDisable()
    {
        CharacterEvents.characterDamaged -= CharacterTookDamage;
    }

    public void CharacterTookDamage(GameObject character, float damageReceived)
    {
        // Lấy vị trí của nhân vật trong không gian world và chuyển đổi sang vị trí trong không gian màn hình
        Vector3 spawnPosition = _mainCamera.WorldToScreenPoint(character.transform.position);

        // Tạo và gán giá trị cho TextMeshPro
        TMP_Text tmpText = Instantiate(damageTextPb, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();
        tmpText.text = damageReceived.ToString();
    }


    public void ShowGameGui(bool isShow)
    {
        if (_gameGUI != null)
        {
            _gameGUI.SetActive(isShow);
        }

        if (_homeGUI != null)
        {
           _homeGUI.SetActive(!isShow);
        }
    }

    private void ShowDiaLog(Dialog dialog)
    {
        if (dialog == null) return;

        m_activeDialog = dialog;
        m_activeDialog.Show(true);
    }

    public void ShowSkillUpgradeDialog()
    {
        ShowDiaLog(_skillUpgradeDialog);
    }

    public void ShowGameoverDialog()
    {
        ShowDiaLog(_gameoverDialog);
    }

    public void UpdateLevelInfo(int curLevel, float curXp, float levelUpXpRequired)
    {
        _levelProgressBar?.UpdateValue(curXp, levelUpXpRequired);

        if (_levelCountingTxt != null) _levelCountingTxt.text = $"LEVEL {curLevel.ToString("00")}";

        if (_xpCountingTxt != null) _xpCountingTxt.text = $"{curXp.ToString("00")} / {levelUpXpRequired.ToString("00")}";
    }

    public void UpdateHpInfo(float curHp, float maxHp)
    {
        _hpProgressBar?.UpdateValue(curHp, maxHp);
        if (_hpCountingTxt != null) _hpCountingTxt.text = $"{curHp.ToString("00")} / {maxHp.ToString("00")}";
    }

    public void UpdateCoinsCounting(int coins)
    {
        if (_coinCountingTxt != null) _coinCountingTxt.text = coins.ToString("n0");
    }
}


