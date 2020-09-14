using System.Collections;
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
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                if (_gameManager._activeCanon != null)
                {
                    _fingerPoint.gameObject.SetActive(true);
                    _gameManager._activeCanon.transform.LookAt(hitInfo.point);
                    _fingerPoint.position = hitInfo.point;
                    _fingerPoint.GetComponent<LineRenderer>().SetPosition(0, _fingerPoint.position);
                    _fingerPoint.GetComponent<LineRenderer>().SetPosition(1, _gameManager._activeCanon.transform.position);
                    _power = Vector3.Distance(_fingerPoint.position, _gameManager._activeCanon.transform.position);
                }
            } else _fingerPoint.gameObject.SetActive(false);

            //if (touch.phase == TouchPhase.Moved)
            //{

            //}
        }

        if (Input.GetMouseButtonUp(0) && _gameManager._activeCanon != null)
        {
            _fingerPoint.gameObject.SetActive(false);
            _gameManager._activeCanon.SpitBall(_power * 3);
        }
    }
}
