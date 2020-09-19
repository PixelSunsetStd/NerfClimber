using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public bool _isRotating = false;

    public bool _rotateX;
    public bool _rotateY;
    public bool _rotateZ = true;

    public bool _counterClockwise = false;

    public float _degPerSec = 90;

    int rotWay;
    // Start is called before the first frame update
    void Start()
    {
        if (_counterClockwise) rotWay = 1; else rotWay = -1;
    }

    // Update is called once per frame
    void Update()
    {


        if (_isRotating)
        {
            if (_counterClockwise) rotWay = 1; else rotWay = -1;
            transform.Rotate(new Vector3(_rotateX.GetHashCode(), _rotateY.GetHashCode(), _rotateZ.GetHashCode()), _degPerSec * rotWay * Time.deltaTime);
        }
    }

    public void StartRot()
    {
        _isRotating = true;
    }

    public void StopRot()
    {
        _isRotating = false;
    }
}
