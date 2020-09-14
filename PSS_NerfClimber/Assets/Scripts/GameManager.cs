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

    public List<GameObject> _levels;
    public int _levelIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        //_levels[_levelIndex].SetActive(true);
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
            //_activeCanon.SpitBall();
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

    public void RestartLevel()
    {
        _numberOfLives = 3;
        _meter = 0;
        _ball.transform.position = Vector3.zero;
        _ball.GetComponent<MeshRenderer>().enabled = true;
        Camera.main.GetComponent<CameraFollow>()._offset = 5;
        Camera.main.GetComponent<CameraFollow>().SetPosition();
        _ball.gameObject.SetActive(true);
    }

    public void NextLevel()
    {
        if (_levelIndex < _levels.Count - 1)
        {
            _levelIndex++;
        }
        else _levelIndex = 0;

        _levels[_levelIndex].gameObject.SetActive(true);

        _numberOfLives = 3;
        _meter = 0;
        _ball.GetComponent<BallBehaviour>()._levelCompleted = false;
        _ball.GetComponent<BallBehaviour>()._rewardedFx = false;
        _ball.GetComponent<BallBehaviour>()._planet.SetActive(false);
        _ball.GetComponent<BallBehaviour>()._winFX.gameObject.SetActive(false);
        _ball.transform.position = Vector3.zero;
        _ball.GetComponent<MeshRenderer>().enabled = true;
        Camera.main.GetComponent<CameraFollow>()._offset = 5;
        Camera.main.GetComponent<CameraFollow>().SetPosition();
        _ball.gameObject.SetActive(true);
    }

}
