using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum PlayerStatistics
{ 
    hp = 0,
    attack = 1,
    attackSpeed = 2,
    speed = 3,
    defense = 4
}


public class Stats : MonoBehaviour
{
    private int skillsPoints = 10;
    public Text skillsPointsValue;

    public List<Text> skillsText = new List<Text>(5);
    public List<int> skillsValues = new List<int>()  { 1, 1,1,1,1};

    public void Awake()
    {
        skillsPoints = 10;
        skillsValues[0] = 1;
        
        skillsValues[1] = 1;
        skillsValues[2] = 1;
        skillsValues[3] = 1;
        skillsValues[4] = 1;

        for (int i = 0; i < skillsText.Count; i++)
            skillsText[i].text = skillsValues[i].ToString();

        skillsPointsValue.text = skillsPoints.ToString();
    }


    public void Update()
    {
        
    }

    private void AddPoint(PlayerStatistics stat)
    {
        if (skillsPoints > 0)
        {
            skillsPoints--;
            skillsValues[(int)stat]++;
            skillsPointsValue.text = skillsPoints.ToString();
            skillsText[(int)stat].text = skillsValues[(int)stat].ToString();
        }
    }

    private void SubstractPoint(PlayerStatistics stat)
    {
        if (skillsValues[(int)stat] > 1)
        {
            skillsPoints++;
            skillsValues[(int)stat]--;
            skillsPointsValue.text = skillsPoints.ToString();
            skillsText[(int)stat].text = skillsValues[(int)stat].ToString();
        }
    }


    public void AddHpPoint() => AddPoint(PlayerStatistics.hp);
    public void SubstractHpPoint() => SubstractPoint(PlayerStatistics.hp);
    public void AddAttackPoint() => AddPoint(PlayerStatistics.attack);
    public void SubstractAttackPoint() => SubstractPoint(PlayerStatistics.attack);
    public void AddSpeedAttackPoint() => AddPoint(PlayerStatistics.attackSpeed);
    public void SubstractSpeedAttackPoint() => SubstractPoint(PlayerStatistics.attackSpeed);
    public void AddSpeedPoint() => AddPoint(PlayerStatistics.speed);
    public void SubstractSpeedPoint() => SubstractPoint(PlayerStatistics.speed);
    public void AddDefensePoint() => AddPoint(PlayerStatistics.defense);
    public void SubstractDefensePoint() => SubstractPoint(PlayerStatistics.defense);



}
