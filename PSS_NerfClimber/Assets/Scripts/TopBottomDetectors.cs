using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopBottomDetectors : MonoBehaviour
{
    public GameManager _gameManager;

    public bool _isFinish;

    public ParticleSystem _winFX;
    public GameObject _nextBallPanel;
    public GameObject _victoryPanel;
    public GameObject _gameOverPanel;
    
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
                other.transform.position = transform.position;
                other.gameObject.SetActive(false);
                //_winFX.transform.position = other.transform.position;
                _winFX.gameObject.SetActive(true);
                GetComponent<AudioSource>().Play();
                _victoryPanel.SetActive(true);
            }
            else
            {
                other.gameObject.SetActive(false);

                if (_gameManager._numberOfLives > 1)
                {
                    _gameManager._numberOfLives--;
                    _nextBallPanel.SetActive(true);
                }
                else
                {
                    _gameOverPanel.SetActive(true);
                }

                
                //SceneManager.LoadScene(0);
            }
        }
    }
}
