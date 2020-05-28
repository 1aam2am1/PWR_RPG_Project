using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    [SerializeField]
    private GameObject _bridge;

    // Start is called before the first frame update
    private Equipment _playerEquipment;
    [SerializeField]
    private int _logsForBridge = 3;
    [SerializeField]
    private int _stonesForBridge = 1;

    void Start()
    {
        _bridge.SetActive(false);
        GameObject player = GameObject.FindWithTag("Player");
        _playerEquipment = player.GetComponent<Equipment>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log("ssdfsd");
            var pos = GameObject.Find("Player").transform.position;
            if(pos.x > this.transform.position.x - 1)
            {
                if(pos.x < this.transform.position.x + 1)
                {
                    if (_playerEquipment.getStones() >= _stonesForBridge && _playerEquipment.getWoodenLogs() >= _logsForBridge)
                    {
                        _bridge.SetActive(true);
                        _playerEquipment.removeLogs(_logsForBridge);
                        _playerEquipment.removeStones(_stonesForBridge);
                    }
                }
            } 
        }
    }


}
