using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLine : MonoBehaviour
{
    public Transform _pointA;
    public Transform _pointB;
    public Transform _model;

    public float _speed;

    Transform _target;

    LineRenderer lr;
    
    // Start is called before the first frame update
    void Start()
    {
        _target = _pointA;

        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _model.localPosition = Vector3.MoveTowards(_model.localPosition, _target.localPosition, _speed * Time.deltaTime);

        if (_model.localPosition == _pointA.localPosition)
            _target = _pointB;

        if (_model.localPosition == _pointB.localPosition)
            _target = _pointA;


        lr.SetPosition(0, _pointA.localPosition);
        lr.SetPosition(1, _pointB.localPosition);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_pointA.position, _pointB.position);
    }
}
