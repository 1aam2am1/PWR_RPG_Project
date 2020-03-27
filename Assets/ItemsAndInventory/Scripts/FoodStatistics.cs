using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodStatistics : StatisticsList
{
    public FoodStatistics()
    {
        dictionary = new Dictionary<string, float>()
        {
            {"hpBonus", 1f}
        };
    }
}