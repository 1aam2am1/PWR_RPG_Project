using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    [SerializeField]
    private GameObject whatToBuild;

    // Start is called before the first frame update
    private Equipment _playerEquipment;
    [SerializeField]
    private int _logsForBridge = 3;
    [SerializeField]
    private int _stonesForBridge = 1;
    [SerializeField]

    void Start()
    {
        if(whatToBuild == null)
        {
            Debug.LogError("nothing to build");
        }
        whatToBuild.SetActive(false);
        GameObject player = GameObject.FindWithTag("Player");
        if(player == null)
        {
            Debug.LogError("Player is null");
        }
       
        _playerEquipment = player.GetComponent<Equipment>();
        if(_playerEquipment == null)
        {
            Debug.LogError("_player equipment is null");
        }

        
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
                    /*if (check if you have it)
                    {
                        if(_bridge)
                        _bridge.SetActive(true);
                        _playerEquipment.removeLogs(_logsForBridge);
                        _playerEquipment.removeStones(_stonesForBridge);
                    }*/
                }
            } 
        }
    }


}
