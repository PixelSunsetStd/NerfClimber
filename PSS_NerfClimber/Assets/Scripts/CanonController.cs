﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonController : MonoBehaviour
{
    GameManager _gameManager;

    public Transform _fingerPoint;

    public float _power;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gameManager._activeCanon!= null)
        {
            if (Input.GetMouseButton(0) && !_gameManager._activeCanon._isSuper)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
                {
                    if (_gameManager._activeCanon != null)
                    {
                        Vector3 point = hitInfo.point;
                        point.z = 0;
                        _fingerPoint.gameObject.SetActive(true);
                        _gameManager._activeCanon.transform.LookAt(point);
                        //transform.localRotation = Quaternion.Euler(Vector3.RotateTowards(transform.position, hitInfo.point, 1, 1));
                        _fingerPoint.position = point;
                        _fingerPoint.GetComponent<LineRenderer>().SetPosition(0, _fingerPoint.position);
                        _fingerPoint.GetComponent<LineRenderer>().SetPosition(1, _gameManager._activeCanon._shooter.transform.position);
                        _power = Vector3.Distance(_fingerPoint.position, _gameManager._activeCanon._shooter.transform.position);
                        _power *= 3;
                        //GetComponent<SimulationTrajectory>().SimulatePath(_gameManager._ball.gameObject, _gameManager._activeCanon._shooter.transform.forward * _power, 1, 0);
                    }
                }
                else _fingerPoint.gameObject.SetActive(false);

                //if (touch.phase == TouchPhase.Moved)
                //{

                //}
            } 
        }

        if (Input.GetMouseButtonUp(0) && _gameManager._activeCanon != null)
        {
            _fingerPoint.gameObject.SetActive(false);
            _gameManager._activeCanon.SpitBall(_power);
        }
    }
}
