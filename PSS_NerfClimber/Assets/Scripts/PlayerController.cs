using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //public GameManager _gameManager;
    public Slider _rotSlider;

    public float _moveSpeed;

    public static bool _isMoving;
    public Rigidbody _rb;

    public GameObject _bullet;

    // Start is called before the first frame update
    void Start()
    {
        _rb.GetComponent<Rigidbody>();
        _isMoving = true;
        //_gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isMoving) MoveForward();

        if (Input.GetMouseButton(0)) Fire();
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

    public void SetMoveState()
    {
        _isMoving = !_isMoving;
    }

    public void Fire()
    {
        if (Input.GetMouseButton(0))
        {
            GameObject bullet = Instantiate(_bullet, transform.position + transform.up + transform.forward * 2, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 15f, ForceMode.Impulse);
            //Destroy(_bullet, 1.0f);
        }
    }
}
