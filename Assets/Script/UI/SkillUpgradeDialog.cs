using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUpgradeDialog : Dialog
{
    [SerializeField] private Text _damageBasicStatUI;
    [SerializeField] private SkillStatUI _damageSkill2StatUI;
    [SerializeField] private SkillStatUI _cooldownSkill2StatUI;
    [SerializeField] private SkillStatUI _damageSkill3StatUI;
    [SerializeField] private SkillStatUI _cooldownSkill3StatUI;
    [SerializeField] private SkillStatUI _damageUntilStatUI;
    [SerializeField] private SkillStatUI _cooldownUntilStatUI;
    [SerializeField] private Text _upgradeSkill2BtnTxt;
    [SerializeField] private Text _upgradeSkill3BtnTxt;
    [SerializeField] private Text _upgradeUntilBtnTxt;
    [SerializeField] private Text _buyUntilBtnTxt;

    private PlayerController _player;
    private PlayerStats _playerStats;

    public override void Show(bool isShow)
    {
        base.Show(isShow);
        Time.timeScale = 0f;

        _player = GameManager.Ins.Player;
        _playerStats =(PlayerStats) _player.statData;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (_player == null || _playerStats == null) return;

        if (titleTxt) titleTxt.text = $"LEVEL {_playerStats.level.ToString("00")}";

        if (_upgradeSkill2BtnTxt) _upgradeSkill2BtnTxt.text = $"UP[${_playerStats.attackSkill2UpgradeCost.ToString("n0")}]";

        if (_upgradeSkill3BtnTxt) _upgradeSkill3BtnTxt.text = $"UP[${_playerStats.attackSkill3UpgradeCost.ToString("n0")}]";

        if (_upgradeUntilBtnTxt) _upgradeUntilBtnTxt.text = $"UP[${_playerStats.attackUntilUpgradeCost.ToString("n0")}]";

        if (_buyUntilBtnTxt) _buyUntilBtnTxt.text = $"UP[${_playerStats.attackUntilRequireBuyCons.ToString("n0")}]";

        if (_damageBasicStatUI) _damageBasicStatUI.text = $"[{_playerStats.damageBasicAttack.ToString("n0")}]";

        if (_damageSkill2StatUI)
        {
            _damageSkill2StatUI.UpdateStat("Damage : ", _playerStats.damageAttackSkill2.ToString("n"),
                $" +({_playerStats.damageAttackSkill2Up.ToString("n")})"
            );
        }

        if (_cooldownSkill2StatUI)
        {
            _cooldownSkill2StatUI.UpdateStat("Cooldown : ", _playerStats.attackSkill2Cooldown.ToString("n"),
                $" -({_playerStats.attackSkill2CooldownUp.ToString("n")})"
            );
        }

        if (_damageSkill3StatUI)
        {
            _damageSkill3StatUI.UpdateStat("Damage : ", _playerStats.damageAttackSkill3.ToString("n"),
                $" +({_playerStats.damageAttackSkill3Up.ToString("n")})"
            );
        }

        if (_cooldownSkill3StatUI)
        {
            _cooldownSkill3StatUI.UpdateStat("Cooldown : ", _playerStats.attackSkill3Cooldown.ToString("n"),
                $" -({_playerStats.attackSkill3CooldownUp.ToString("n")})"
            );
        }

        if (_damageUntilStatUI)
        {
            _damageUntilStatUI.UpdateStat("Damage : ", _playerStats.damageAttackUntil.ToString("n"),
                $" +({_playerStats.damageAttackUntilUp.ToString("n")})"
            );
        }

        if (_cooldownUntilStatUI)
        {
            _cooldownUntilStatUI.UpdateStat("Cooldown : ", _playerStats.attackUntilCooldown.ToString("n"),
                $" -({_playerStats.attackUntillCooldownUp.ToString("n")})"
            );
        }
    }

    public void UpgradeSkill()
    {
        if (_playerStats == null) return;
        _playerStats.Upgrade(OnUpgradeSucess, OnUpgradeFailed);
    }

    private void OnUpgradeSucess()
    {
        UpdateUI();
        GUIManager.Ins.UpdateCoinsCounting(Pref.coins);
    }

    private void OnUpgradeFailed()
    {

    }

    public override void Close()
    {
        base.Close();
        Time.timeScale = 1f;
    }
}
