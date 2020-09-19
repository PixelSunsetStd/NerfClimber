using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehaviour : MonoBehaviour
{
    private GameManager _gameManager;

    public int _points;

    //public bool _isStart = false;

    public bool _isGold = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DetectRay()
    {
        _gameManager._score += _points;
        _gameManager.PlayPointsFX(transform.position, _points);
        GetComponent<AudioSource>().Play();

        if (!_isGold)
        {
            GetComponentInParent<TargetManager>().TargetTouched();
            GetComponent<MeshRenderer>().enabled = false;
        }
        //FindObjectOfType<TextParticleSystemController>().GetComponent<ParticleSystem>().Play();
    }
}
