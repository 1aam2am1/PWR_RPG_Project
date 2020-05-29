using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private AudioClip _explosionClip;
    [SerializeField]
    private AudioSource _audioSource;
    private Renderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _explosionClip;
        _renderer = GetComponent<Renderer>();


        _audioSource.Play();
    }

    
    void AfterExplosion()
    {
        Destroy(this.gameObject);
    }
}
