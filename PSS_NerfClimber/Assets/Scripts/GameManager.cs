using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Slider _rotSlider;

    public CanonBehaviour _activeCanon;
    public CanonBehaviour _prevCanon;

    public Transform _ball;

    public int _meter;
    public Text _meterText;

    [SerializeField]
    public int _numberOfLives;

    public int _score;

    public ParticleSystem _rewardTextFX;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_ball.position.y > _meter)
        {
            _meter = Mathf.FloorToInt(_ball.transform.position.y);
        }

        _meterText.text = Mathf.FloorToInt(_meter).ToString();
    }

    public void SetCanonRot()
    {
        if (_activeCanon != null)
        {
            if (_activeCanon._isControlable)
                _activeCanon.transform.rotation = Quaternion.Euler(_activeCanon.transform.rotation.x, _activeCanon.transform.rotation.y, _rotSlider.value);
        }
    }

    public void StartActiveCanon()
    {
        if (_activeCanon != null)
        {
            _prevCanon = _activeCanon;
            _prevCanon._givePoints = true;
            _activeCanon.SpitBall();
        }
    }

    public void NextBall()
    {
        if (_numberOfLives > 0)
        {
            _ball.transform.position = _prevCanon.transform.position;
            Camera.main.GetComponent<CameraFollow>().SetPosition();
            //_ball.GetComponent<BallBehaviour>()._gi
            _prevCanon._givePoints = false;
            _ball.gameObject.SetActive(true);
            transform.position = new Vector3(0, _prevCanon.transform.position.y + 5, -10);
        }
        else ReloadScene();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
