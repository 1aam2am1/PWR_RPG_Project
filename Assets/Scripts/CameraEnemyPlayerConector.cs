using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraEnemyPlayerConector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Connect());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Connect()
    {
        GameObject player = null;
        yield return null;
        while (true)
        {
            var objects = GameObject.FindGameObjectsWithTag("Player");
            if (objects.Count() != 0)
            {
                player = objects.First();
                break;
            }

            yield return null;
        }

        FindObjectsOfType<EagleMovement>().ToList().ForEach(n => n.target = player.transform);

        FindObjectsOfType<CinemachineVirtualCamera>().ToList().ForEach(n => n.Follow = player.transform);
    }
}
