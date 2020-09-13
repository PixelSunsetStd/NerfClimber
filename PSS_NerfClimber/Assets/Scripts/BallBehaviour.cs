using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    public int _combo;
    public bool _levelCompleted;

    public ParticleSystem _winFX;
    public GameObject _planet;
    Animator _planetAnimator;

    public bool _rewardedFx;

    Rigidbody _rb;

    public AudioSource[] _audioSources;

    public ParticleSystem _splatFX;
    public ParticleSystem _trailFX;

    // Start is called before the first frame update
    void Start()
    {
        _rewardedFx = false;
        _rb = GetComponent<Rigidbody>();
        GetComponent<MeshRenderer>().enabled = true;

        _planetAnimator = _planet.GetComponentInChildren<Animator>();

        _trailFX.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation = Quaternion.LookRotation(_rb.velocity, transform.up);//Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(_rb.velocity, Vector3.up), Time.deltaTime * 360f);
        //if (_rb.velocity.y >= 0)
        transform.localScale = new Vector3(.5f - (Mathf.Abs(_rb.velocity.y) / 100.0f), .5f - (Mathf.Abs(_rb.velocity.y) / 100.0f), .5f + (Mathf.Abs(_rb.velocity.y) / 100.0f));
        //else {
            //transform.localScale = new Vector3(.5f, .5f + (_rb.velocity.y / 100.0f), .5f);
        //}
        if (_levelCompleted && !_rewardedFx)
        {
            if (_rb.velocity.y <= 0)
            {
                _winFX.transform.parent = null;
                _winFX.transform.position = new Vector3(0, transform.position.y, 0);
                _winFX.gameObject.SetActive(true);
                _planet.transform.position = new Vector3(0, transform.position.y, 12);
                _planet.SetActive(true);
                _planetAnimator.SetTrigger("Activate");
                _rewardedFx = true;
                GetComponent<MeshRenderer>().enabled = false;
                _trailFX.Stop();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.collider.tag)
        {
            case "Obstacle":
                if (_audioSources[0].enabled)// GetComponent<AudioSource>().enabled)
                    _audioSources[0].Play();
                _splatFX.Play();
                _combo++;
                break;

            case "Canon":
                _combo = 0;
                //gameObject.SetActive(false);
                break;
        }
    }
}
