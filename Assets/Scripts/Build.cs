using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    [SerializeField]
    private GameObject whatToBuild;
    [SerializeField]
    private int _logsForBridge = 1;
    [SerializeField]
    private int _stonesForBridge = 1;
    private GameObject _player;

    [SerializeField]
    private GameObject _popupMessagePrefab;
    [SerializeField]
    private GameObject _popupErrorPrefab;

    private GameObject _popupMessage;
    private GameObject _popupError;

    private bool _isOn = false;
    private bool _isErrorOn = false;

    //private Ref<Item>[] _playerInventory;

    void Start()
    {
        if(whatToBuild == null)
        {
            Debug.LogError("nothing to build");
        }
        whatToBuild.SetActive(false);
        _player = GameObject.FindWithTag("Player");
        if(_player == null)
        {
            Debug.LogError("Player is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.B))
        {
            
            var pos = GameObject.Find("Player").transform.position;
            if(pos.x > this.transform.position.x - 1)
            {
                if(pos.x < this.transform.position.x + 1)
                {
                    if (isWoodInInventory())
                    {
                        whatToBuild.SetActive(true);
                        /*
                         * TODO - removing from inventory
                         */
                    }
                    else
                    {
                        Debug.Log("No");
                        if (_isErrorOn == false)
                        {
                            _isErrorOn = true;
                            _isOn = false;
                            Destroy(this._popupMessage);
                            Vector2 posToSpawn = new Vector2(transform.position.x + 0.3f, transform.position.y + 1);
                            _popupError = Instantiate(_popupErrorPrefab, posToSpawn, Quaternion.identity);
                        }
                    }
                }
            } 
        }
    }

    bool isWoodInInventory()
    {

        Ref<Item>[] _playerInventory = _player.GetComponent<InventorySystem>().inventory;
        if (_playerInventory == null)
        {
            Debug.LogError("Player inventory is null");
        }

        int logs = 0;
       
        foreach (var n in _playerInventory)
        {
            if (n != null)
            {
                if (n.Item != null)
                {
                    if(n.Item.itemName  == "Wood")                 ///== "Gun2")    // CHANGE TO WOOD LATER
                    {
                        logs++;
                    }

                }
            }
        }
        if (logs >= _logsForBridge)
            return true;          
        else
            return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
           
            if(_isOn == true)
            {
                return;
            }
            else if(_isOn==false && _isErrorOn==false)
            {
                Vector2 posToSpawn = new Vector2(transform.position.x - 0.3f, transform.position.y + 1);
                _popupMessage = Instantiate(_popupMessagePrefab, posToSpawn, Quaternion.identity);
                _isOn = true;
            }



               
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if(_isOn)
            {
                Destroy(this._popupMessage);
                _isOn = false;
            }
            if(_isErrorOn)
            {
                Debug.Log("dffd");
                Destroy(this._popupError);
                _isErrorOn = false;
            }
        }
    }
}
