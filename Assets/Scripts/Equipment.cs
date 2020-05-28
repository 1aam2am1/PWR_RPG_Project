using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    private int _woodenLogs = 10;
    private int _stones = 10;

    public int getWoodenLogs()
    {
        return _woodenLogs;
    }

    public int getStones()
    {
        return _stones;
    }

    public void removeStones(int usedStones)
    {
        _stones -= usedStones;
    }

    public void removeLogs(int usedLogs)
    {
        _woodenLogs -= usedLogs;
    }
}
