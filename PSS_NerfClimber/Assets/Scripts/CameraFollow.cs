using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    public Transform _target;
    Vector3 _startPos;
    public float _offset;
    
    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y - _offset < _target.position.y)
        {
            //transform.position = new Vector3(_target.position.x, _target.position.y, _startPos.z);
            transform.position = new Vector3(_target.position.x, _target.position.y + _offset, _startPos.z);
        }

        if (transform.position.x != _target.position.x)
        {
            transform.position = new Vector3(_target.position.x, transform.position.y, _startPos.z);
        }
    }

    public void SetPosition()
    {
        transform.position = new Vector3(transform.position.x, _target.position.y + _offset, transform.position.z);
    }
}
