using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    public int _combo;


    // Start is called before the first frame update
    void Start()
    {
        //_textFX = GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
