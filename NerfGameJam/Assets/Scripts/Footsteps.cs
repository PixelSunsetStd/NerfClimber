using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioClip[] _footstepSounds;
    AudioSource _audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayFootstepSound()
    {
        int randomClip = Random.Range(0, _footstepSounds.Length);
        _audioSource.clip = _footstepSounds[randomClip];
        _audioSource.Play();
    }
}
