using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUpgradeDialog : Dialog
{
    [SerializeField] private SkillStatUI _damageBasicStatUI;
    [SerializeField] private SkillStatUI _damageSkill2StatUI;
    [SerializeField] private SkillStatUI _cooldownSkill2StatUI;
    [SerializeField] private SkillStatUI _damageSkill3StatUI;
    [SerializeField] private SkillStatUI _cooldownSkill3StatUI;
    [SerializeField] private SkillStatUI _damageUntilStatUI;
    [SerializeField] private SkillStatUI _cooldownUntilStatUI;

    private PlayerController _player;
    private PlayerStats _playerStats;

    public override void Show(bool isShow)
    {
        base.Show(isShow);
        Time.timeScale = 0f;

        _player = GameManager.Ins.Player;
        //_playerStats = _player.statData;
    }
}
