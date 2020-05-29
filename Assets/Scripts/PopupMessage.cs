using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupMessage : MonoBehaviour
{
    [SerializeField]
    private GameObject _popupMessagePrefab;

    private GameObject _popupMessage;
    private bool _isOn = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isOn == true)
            return;

        if(collision.tag == "Player")
        {
            Vector2 posToSpawn = new Vector2(transform.position.x - 0.3f, transform.position.y + 1);
            _popupMessage = Instantiate(_popupMessagePrefab, posToSpawn, Quaternion.identity);
            _isOn = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Destroy(this._popupMessage);
            _isOn = false;
        }
    }
}
