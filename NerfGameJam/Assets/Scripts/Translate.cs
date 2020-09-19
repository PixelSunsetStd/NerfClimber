using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translate : MonoBehaviour
{
    public bool _isTranslatingX;

    public float _distance;

    public float _speed;

    public bool _startLeft = false;

    Vector3 _startPosition;
    Vector3 _target;
    Vector3 _leftPoint;
    Vector3 _rightPoint;

    // Start is called before the first frame update
    void OnEnable()
    {
        _startPosition = transform.position;
        _leftPoint = _startPosition + Vector3.left * _distance;
        _rightPoint = _startPosition + Vector3.right * _distance;

        if (_startLeft) _target = _leftPoint;
        else _target = _rightPoint;
    }



    // Update is called once per frame
    void Update()
    {
        if (_isTranslatingX)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);

            if (transform.position == _leftPoint)
            {
                _target = _rightPoint;
            }

            if (transform.position == _rightPoint)
            {
                _target = _leftPoint;
            }
        }
    }
}
