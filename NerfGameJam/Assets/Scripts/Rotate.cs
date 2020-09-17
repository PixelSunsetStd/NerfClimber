using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public bool _isRotating = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isRotating)
        {
            transform.Rotate(Vector3.forward, 90 * Time.deltaTime); 
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
