using UnityEngine;
using System.Collections;

public class PlayerStats
{
    public int curHealth, maxHealth;
    public int rangedOff, rangedDeff;
    public int discovery;

    public int armor;

    public int strength, agility, skill, luck, resistance, perception, concentration; //get up to three digits

    void CalculateStatsFromSkills()
    {
        maxHealth = Mathf.FloorToInt(strength + resistance + 0.5f * agility);
        rangedOff = Mathf.FloorToInt(skill + concentration + perception * 0.5f);
        rangedDeff = Mathf.FloorToInt(resistance + agility + luck * 0.5f);
        discovery = Mathf.FloorToInt(perception + luck + 0.5f * concentration);
    }

}