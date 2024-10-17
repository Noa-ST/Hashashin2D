using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Stats", menuName = "Nora/TDS/Create Player Stats")]
public class PlayerStats : ActionStats
{
    [Header("Level Up Base:")]
    public int level;
    public int maxLevel;
    public float xp;
    public float levelUpXpRequied;

    [Header("Level Up:")]
    public float levelUpXpUpRequireUp;
    public float hpUp;

    [Header("Attack Bassic:")]
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


    public override void Upgrade(Action OnSuccess = null, Action OnFailed = null)
    {
        if (xp >= levelUpXpRequied && !IsMaxLevel())
        {
            while (xp >= levelUpXpRequied && IsMaxLevel())
            {
                level++;
                xp -= levelUpXpRequied;

                damageBasicAttack += damageBasicAttackUpPerLevel;

                hp += hpUp * Helper.GetUpgradeFormuala(level);
                levelUpXpRequied += levelUpXpUpRequireUp * Helper.GetUpgradeFormuala(level);
                Save();
                OnSuccess?.Invoke();
            }

            if (IsMaxLevel())
            {
                OnFailed?.Invoke();
            }
        }
        else if (UpgradeSkill("Attack Skill 2") || UpgradeSkill("Attack Skill 3") || UpgradeSkill("Attack Until"))
        {
            OnSuccess?.Invoke();
        }
        else
        {
            OnFailed?.Invoke();
        }
    }

    public bool UpgradeSkill(string skillName)
    {
        switch (skillName)
        {
            case "Attack Skill 2":
                if (Pref.IsEnoughCoins(attackSkill2UpgradeCost) && attackSkill2Level < maxSkillLevel)
                {
                    Pref.coins -= attackSkill2UpgradeCost;
                    attackSkill2Level++;
                    damageAttackSkill2 *= damageAttackSkill2Up * Helper.GetUpgradeFormuala(attackSkill2Level);
                    attackSkill2Cooldown -= attackSkill2CooldownUp * Helper.GetUpgradeFormuala(attackSkill2Level);
                    attackSkill2UpgradeCost += attackSkill2UpgradeCostUp * attackSkill2Level;
                    Save();
                    return true;
                }
                break;
            case "Attack Skill 3":
                if (Pref.IsEnoughCoins(attackSkill3UpgradeCost) && attackSkill3Level < maxSkillLevel)
                {
                    Pref.coins -= attackSkill3UpgradeCost;
                    attackSkill3Level++;
                    damageAttackSkill3 *= damageAttackSkill3Up * Helper.GetUpgradeFormuala(attackSkill3Level);
                    attackSkill3Cooldown -= attackSkill3CooldownUp * Helper.GetUpgradeFormuala(attackSkill3Level);
                    attackSkill3UpgradeCost += attackSkill3UpgradeCostUp * attackSkill3Level;
                    Save();
                    return true;
                }
                break;
            case "Attack Until":
                if (attackUntilLevel == 0) // Nếu kỹ năng chưa được mua
                {
                    if (Pref.IsEnoughCoins(attackUntilRequireBuyCons)) // Kiểm tra số tiền cần thiết để mua kỹ năng
                    {
                        Pref.coins -= attackUntilRequireBuyCons; // Trừ tiền
                        attackUntilLevel = 1; // Đánh dấu kỹ năng là đã mua
                        damageAttackUntil *= damageAttackUntilUp; // Thiết lập sát thương cho kỹ năng
                        Save();
                        return true;
                    }
                }
                else // Nếu kỹ năng đã được mua, cho phép nâng cấp
                {
                    if (Pref.IsEnoughCoins(attackUntilUpgradeCost) && attackUntilLevel < maxSkillLevel)
                    {
                        Pref.coins -= attackUntilUpgradeCost;
                        attackUntilLevel++;
                        damageAttackUntil *= damageAttackUntilUp * Helper.GetUpgradeFormuala(attackUntilLevel);
                        attackUntilCooldown -= attackUntillCooldownUp * Helper.GetUpgradeFormuala(attackUntilLevel);
                        attackUntilUpgradeCost += attackUntilUpgradeCostUp * attackUntilLevel;
                        Save();
                        return true;
                    }
                }
                break;
        }
        return false;
    }


    public override bool IsMaxLevel()
    {
        return level >= maxLevel;
    }

    public override void Load()
    {
        if (!string.IsNullOrEmpty(Pref.playerData))
        {
            JsonUtility.FromJsonOverwrite(Pref.playerData, this);
        }
    }

    public override void Save()
    {
        Pref.playerData = JsonUtility.ToJson(this);
    }
}
