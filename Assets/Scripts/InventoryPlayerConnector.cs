using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPlayerConnector : MonoBehaviour
{
    GameObject inventory;

    private void Awake()
    {
        inventory = Resources.Load<GameObject>("Inventory");
    }
    // Start is called before the first frame update
    void Start()
    {
        inventory = Instantiate(inventory, transform.position, Quaternion.identity, transform);
        inventory.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            DisplayEQ();
        }
    }

    void DisplayEQ()
    {
        if (!inventory.activeSelf)
        {
            var player = GameObject.FindGameObjectsWithTag("Player").GetValue(0) as GameObject;

            var system = player?.GetComponent<InventorySystem>();
            if (system != null)
            {
                inventory.GetComponentInChildren<InventoryManager>().Connect(system);
            }
        }
        else
        {
            inventory.GetComponentInChildren<InventoryManager>().Connect(null);
        }

        inventory.SetActive(!inventory.activeSelf);
    }
}
