using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    public Transform _target;

    public float _offset;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y - _offset < _target.position.y && transform.position.y - _offset <= 45)
        {
            transform.position = new Vector3(transform.position.x, _target.position.y + _offset, transform.position.z);
        }

        if (transform.position.y <= 5)
        {
            transform.position = new Vector3(transform.position.x, 5, transform.position.z);
        }
        
        //if (_target.position.y <= 40 && _target.position.y <= transform.position.y)
        //    transform.position = new Vector3(transform.position.x, _target.position.y + 5, transform.position.z);
        //else transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }
}
