using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRotateAround : MonoBehaviour
{
    public Transform _model;
    public bool _isClockwise;
    int rotWay;
    
    // Start is called before the first frame update
    void Start()
    {
        if (_isClockwise) rotWay = -1;
        else rotWay = 1;
    }

    // Update is called once per frame
    void Update()
    {
        _model.RotateAround(transform.position, transform.forward, 90 * rotWay * Time.deltaTime);
    }
}
