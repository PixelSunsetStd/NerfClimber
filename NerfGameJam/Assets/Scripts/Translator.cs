using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translator : MonoBehaviour
{
    public Transform _startPoint;
    public Transform _endPoint;

    public float _speed = 1;

    Transform _target;
    
    // Start is called before the first frame update
    void Start()
    {
        _target = _startPoint;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);

        if (transform.position == _startPoint.position)
            _target = _endPoint;
        if (transform.position == _endPoint.position)
            _target = _startPoint;

    }
}
