using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _leftArrowPrefab;
    [SerializeField]
    private GameObject _rightArrowPrefab;
    [SerializeField]
    private GameObject _arrowContainer;

    [SerializeField]
    private bool left = false;


    private bool _stopSpawning = false;
    // Start is called before the first frame update
   
    IEnumerator SpawnArrowRoutine()
    {
        while(!_stopSpawning)
        {
            Vector2 posToSpawn = new Vector2(transform.position.x, transform.position.y);
            if(left)
            {
                GameObject newArrow = Instantiate(_leftArrowPrefab, posToSpawn, Quaternion.Euler(0, 0, 0));
                //newArrow.transform.parent = _arrowContainer.transform;
            }
            else
            {
                GameObject newArrow = Instantiate(_rightArrowPrefab, posToSpawn, Quaternion.Euler(0, 0, 0));
                //newArrow.transform.parent = _arrowContainer.transform;
            }
                

           
           
            yield return new WaitForSeconds(4.0f);
        }
    }

    
    private void OnBecameVisible()
    {
        _stopSpawning = false;
        StartCoroutine(SpawnArrowRoutine());
    }

    private void OnBecameInvisible()
    {
        _stopSpawning = true;
    }
}
