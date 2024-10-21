using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Stats", menuName = "Nora/TDS/Create Player Stats")]
public class PlayerStats : ActionStats
{
    [Header("Level Up Base:")]
    public int level;
    public int maxLevel;
    public float xp;
    public float levelUpXpRequired;

    [Header("Level Up:")]
    public float levelUpXpUpRequireUp;
    public float hpUp;

    [Header("Attack Basic:")]
    public float damageBasicAttack;
    public float damageBasicAttackUpPerLevel;

    [Header("Attack Skill 2:")]
    public float damageAttackSkill2;
    public float damageAttackSkill2Up;
    public float attackSkill2Cooldown;
    public float attackSkill2CooldownUp;
    public int attackSkill2UpgradeCost;
    public int attackSkill2UpgradeCostUp;
    public int attackSkill2Level;

    [Header("Attack Skill 3:")]
    public float damageAttackSkill3;
    public float damageAttackSkill3Up;
    public float attackSkill3Cooldown;
    public float attackSkill3CooldownUp;
    public int attackSkill3UpgradeCost;
    public int attackSkill3UpgradeCostUp;
    public int attackSkill3Level;

    [Header("Attack Until:")]
    public float damageAttackUntil;
    public float damageAttackUntilUp;
    public float attackUntilCooldown;
    public float attackUntillCooldownUp;
    public int attackUntilUpgradeCost;
    public int attackUntilUpgradeCostUp;
    public int attackUntilLevel;
    public int attackUntilRequireBuyCons;

    [Header("Skill Upgrade Limits")]
    public int maxSkillLevel;

    // Sự kiện thay đổi chỉ số nhân vật
    public delegate void StatsChangedHandler();
    public event StatsChangedHandler OnStatsChanged;

    private void NotifyStatsChanged()
    {
        OnStatsChanged?.Invoke();
    }

    // Nâng cấp level của nhân vật
    public override void Upgrade(Action OnSuccess = null, Action OnFailed = null)
    {
        if (xp >= levelUpXpRequired && !IsMaxLevel())
        {
            while (xp >= levelUpXpRequired && !IsMaxLevel())
            {
                level++;
                xp -= levelUpXpRequired;

                damageBasicAttack += damageBasicAttackUpPerLevel;

                hp += hpUp * Helper.GetUpgradeFormuala(level);
                levelUpXpRequired += levelUpXpUpRequireUp * Helper.GetUpgradeFormuala(level);
                Save();
                OnSuccess?.Invoke();
                NotifyStatsChanged();
            }

            if (IsMaxLevel())
            {
                OnFailed?.Invoke();
            }
        }
        else
        {
            OnFailed?.Invoke();
        }
    }

    // Kiểm tra và nâng cấp Skill 2
    public bool UpgradeSkill2()
    {
        if (Pref.IsEnoughCoins(attackSkill2UpgradeCost) && attackSkill2Level < maxSkillLevel)
        {
            Pref.coins -= attackSkill2UpgradeCost;
            attackSkill2Level++;
            damageAttackSkill2 += damageAttackSkill2Up;
            attackSkill2Cooldown -= attackSkill2CooldownUp;
            attackSkill2UpgradeCost += attackSkill2UpgradeCostUp * attackSkill2Level;
            Save();
            NotifyStatsChanged();
            return true;
        }
        return false;
    }

    // Kiểm tra và nâng cấp Skill 3
    public bool UpgradeSkill3()
    {
        if (Pref.IsEnoughCoins(attackSkill3UpgradeCost) && attackSkill3Level < maxSkillLevel)
        {
            Pref.coins -= attackSkill3UpgradeCost;
            attackSkill3Level++;
            damageAttackSkill3 += damageAttackSkill3Up;
            attackSkill3Cooldown -= attackSkill3CooldownUp;
            attackSkill3UpgradeCost += attackSkill3UpgradeCostUp * attackSkill3Level;
            Save();
            NotifyStatsChanged();
            return true;
        }
        return false;
    }

    // Kiểm tra và nâng cấp Attack Until
    public bool UpgradeAttackUntil()
    {
        if (attackUntilLevel == 0)
        {
            // Kiểm tra nếu người chơi cần mua kỹ năng này trước
            if (Pref.IsEnoughCoins(attackUntilRequireBuyCons))
            {
                Pref.coins -= attackUntilRequireBuyCons;
                attackUntilLevel = 1;
                Save();
                NotifyStatsChanged();
                return true;
            }
        }
        else
        {
            if (Pref.IsEnoughCoins(attackUntilUpgradeCost) && attackUntilLevel < maxSkillLevel)
            {
                Pref.coins -= attackUntilUpgradeCost;
                attackUntilLevel++;
                damageAttackUntil += damageAttackUntilUp;
                attackUntilCooldown -= attackUntillCooldownUp;
                attackUntilUpgradeCost += attackUntilUpgradeCostUp * attackUntilLevel;
                Save();
                NotifyStatsChanged();
                return true;
            }
        }
        return false;
    }

    // Kiểm tra xem nhân vật có đạt max level hay không
    public override bool IsMaxLevel()
    {
        return level >= maxLevel;
    }

    // Tải dữ liệu từ Pref
    public override void Load()
    {
        if (!string.IsNullOrEmpty(Pref.playerData))
        {
            JsonUtility.FromJsonOverwrite(Pref.playerData, this);
        }
    }

    // Lưu dữ liệu nhân vật vào Pref
    public override void Save()
    {
        Pref.playerData = JsonUtility.ToJson(this);
    }
}
