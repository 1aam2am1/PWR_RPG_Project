using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearInGlory : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _appearParticle;
    // Start is called before the first frame update
    void OnAppear()
    {
        Instantiate(_appearParticle, transform.position, Quaternion.identity);
    }
}
