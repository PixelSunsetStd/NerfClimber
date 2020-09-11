using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopBottomDetectors : MonoBehaviour
{
    public GameManager _gameManager;

    public bool _isFinish;

    public bool _isPlayerDone;

    public ParticleSystem _winFX;
    public GameObject _nextBallPanel;
    public GameObject _victoryPanel;
    public GameObject _gameOverPanel;

    public TopBottomDetectors _otherTrigger;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_isFinish)
            {
                Debug.Log("WIN !");
                //other.transform.position = transform.position;
                //other.gameObject.SetActive(false);
                //_winFX.transform.position = other.transform.position;
                _winFX.gameObject.SetActive(true);
                GetComponent<AudioSource>().Play();
                //_victoryPanel.SetActive(true);
                _otherTrigger._isPlayerDone = true;
            }
            else
            {
                other.gameObject.SetActive(false);
                other.GetComponent<BallBehaviour>()._combo = 0;

                if (_isPlayerDone)
                {
                    _victoryPanel.SetActive(true);
                    return;
                }
                else
                {
                    if (_gameManager._numberOfLives > 1)
                    {
                        _gameManager._numberOfLives--;
                        _nextBallPanel.SetActive(true);
                    }
                    else
                    {
                        _gameOverPanel.SetActive(true);
                    }
                }


                
                //SceneManager.LoadScene(0);
            }
        }
    }
}
