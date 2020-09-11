using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    public int _combo;
    public bool _levelCompleted;

    public ParticleSystem _winFX;

    bool _rewardedFx;

    Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rewardedFx = false;
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_levelCompleted && !_rewardedFx)
        {
            if (_rb.velocity.y <= 0)
            {
                _winFX.transform.parent = null;
                _winFX.transform.position = transform.position;
                _winFX.gameObject.SetActive(true);
                _rewardedFx = true;
            }
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.collider.tag)
        {
            case "Obstacle":
                if (GetComponent<AudioSource>().enabled)
                    GetComponent<AudioSource>().Play();
                _combo++;
                break;

            case "Canon":
                _combo = 0;
                //gameObject.SetActive(false);
                break;
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (GetComponent<AudioSource>().enabled)
            GetComponent<AudioSource>().Play();

        switch (other.tag)
        {
            case "Canon":
                _textFX.transform.position = other.transform.position;
                _textFX.GetComponent<ParticleSystem>().Play();
                _combo = 0;
                gameObject.SetActive(false);
                break;
        }
    }*/
}
