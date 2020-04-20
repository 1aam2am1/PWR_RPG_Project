using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class Tooltip : MonoBehaviour
{

    [SerializeField] Text itemName;
    [SerializeField] Text itemDesc;
    [SerializeField] Text itemType;
    [SerializeField] Text itemStats;

    private StringBuilder sb = new StringBuilder();

    private void Awake()
    {
        if (itemName == null)
            itemName = transform.Find("ItemName").GetComponent<Text>();

        if (itemDesc == null)
            itemDesc = transform.Find("ItemDescription").GetComponent<Text>();

        if (itemType == null)
            itemType = transform.Find("ItemType").GetComponent<Text>();

        if (itemStats == null)
            itemStats = transform.Find("ItemStats").GetComponent<Text>();
    }

    public void ShowTooltip(Item item)
    {
        if (item != null)
        {
            itemName.text = item.itemName;
            itemType.text = item.itemType.ToString();
            itemDesc.text = item.itemDescription;

            sb.Length = 0;
            foreach (var stat in item.itemStatistics)
            {
                AddStat(stat.Value, stat.Key);
            }

            itemStats.text = sb.ToString();


            gameObject.SetActive(true);
        }
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    private void AddStat(float value, string statName)
    {
        if (sb.Length > 0)
            sb.AppendLine();

        sb.Append(statName);
        sb.Append(" ");

        if (value > 0)
            sb.Append("+");
        sb.Append(value);
    }


}
