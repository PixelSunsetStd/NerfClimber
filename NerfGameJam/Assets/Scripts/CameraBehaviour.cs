using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform _target;
    Vector3 _startPos;

    private void Awake()
    {
        _startPos = transform.position;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _startPos + _target.position;
    }
}
