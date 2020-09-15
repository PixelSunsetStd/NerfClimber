using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public bool _isNPC;
    //public GameManager _gameManager;
    public Slider _rotSlider;

    public float _moveSpeed;

    public bool _isMoving;
    public Rigidbody _rb;

    public GameObject _bullet;

    // Start is called before the first frame update
    void Start()
    {
        _rb.GetComponent<Rigidbody>();
        //_isMoving = true;
        //_gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && !_isNPC)
            Fire();

        
    }

    private void FixedUpdate()
    {
        if (_isMoving) MoveForward();

        if (_isNPC)
        {
            Ray ray = new Ray(transform.position + transform.up, transform.forward);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, 3.0f, ~9))
            {
                _isMoving = false;
                Fire();
            }
            else _isMoving = true;
        }
    }

    public void SetRotation()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x, _rotSlider.value, transform.rotation.z);
    }

    public void ResetRotation()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
    }

    public void MoveForward()
    {
        _rb.MovePosition(Vector3.MoveTowards(transform.position, transform.position + transform.forward, _moveSpeed * Time.deltaTime));
     }

    public void SetMoveState(bool isMoving)
    {
        _isMoving = isMoving;
    }

    public void Fire()
    {
        GameObject bullet = Instantiate(_bullet, transform.position + transform.up + transform.forward * 2, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 15f, ForceMode.Impulse);
            
    }
}
