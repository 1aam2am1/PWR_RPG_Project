using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShooter : MonoBehaviour
{

    [SerializeField]
    private GameObject _cannonBallPrefab;
    [SerializeField]
    private GameObject _ballContainer;

 

    void shoot()
    {
        Vector2 posToSpawn = new Vector2(transform.position.x- 0.76f, transform.position.y + 0.22f);
        GameObject newBall = Instantiate(_cannonBallPrefab, posToSpawn, Quaternion.identity);

        newBall.transform.parent = _ballContainer.transform;
    }
}
