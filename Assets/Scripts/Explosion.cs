using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private AudioClip _explosionClip;
    [SerializeField]
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _explosionClip;
        _audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AfterExplosion()
    {
        Destroy(this.gameObject);
    }
}
