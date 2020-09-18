﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject _player;
    public int _speed = 1;

    public enum GamePhase { isWaiting, isMoving, isShooting };
    public GamePhase _gamePhase;

    public int _totalScore;
    public int _score;
    public int[] _chunkScores;// = new int[3];

    public int _levelIndex;
    public int _sectionIndex;
    public int _chunkIndex;

    public List<GameObject> _levels;
    public List<GameObject> _sections;
    public List<GameObject> _chunks;

    public float _targetTime;
    public Slider _targetTimeSlider;

    public ParticleSystem _impactFX;

    public Text _startCountDownText;

    TextParticleSystemController tpsc;

    public Button _restartSectionBtn;
    public Button _nextLevelBtn;

    // Start is called before the first frame update
    void Start()
    {
        //Invoke("StartGame", 3f);
        //StartCoroutine(StartCountDown(3));
        tpsc = FindObjectOfType<TextParticleSystemController>();
        GetSections(_levelIndex);

        //_player.transform.position = _chunks[_chunkIndex].transform.position - Vector3.forward * 5;
    }

    public void StartGame()
    {
        StartCoroutine(StartCountDown(3f));
    }

    public void GetChunks(int index)
    {
        _chunks.Clear();
        _chunkScores = new int[3];
        for (int i = 0; i < _sections[index].transform.childCount; i++)
        {
            if (_sections[index].transform.GetChild(i).GetComponent<LevelBehaviour>() != null)
            {
                _chunks.Add(_sections[index].transform.GetChild(i).gameObject);
                int numberOfTargets = _sections[index].transform.GetChild(i).GetComponent<LevelBehaviour>()._targets.Count;
                _chunkScores[0] += numberOfTargets * 10;
                _chunkScores[1] += numberOfTargets * 25;
                _chunkScores[2] += numberOfTargets * 50;

            }
        }
    }

    public void GetSections(int index)
    {
        _sections.Clear();

        for (int i = 0; i < _levels[index].transform.childCount; i++)
        {
            _sections.Add(_levels[index].transform.GetChild(i).gameObject);
        }

        GetChunks(1);
    }

    public IEnumerator NextSection()
    {
        yield return new WaitForSeconds(3.0f);

        _player.transform.rotation = Quaternion.Euler(Vector3.zero);

        _sectionIndex++;
        GetChunks(_sectionIndex);
        _chunkIndex = 0;
        _score = 0;

        //_player.transform.position = Vector3.zero;
        //_sections[_sectionIndex].transform.position = _player.transform.position + Vector3.forward * 10;

        _gamePhase = GamePhase.isMoving;
    }

    IEnumerator StartCountDown(float time)
    {
        _startCountDownText.gameObject.SetActive(true);

        while (time > 0)
        {
            _startCountDownText.text = time.ToString();

            yield return new WaitForSeconds(1f);

            time--;
        }

        //_startCountDownText.gameObject.SetActive(false);
        _gamePhase = GamePhase.isMoving;
    }

    IEnumerator StartChunkTimer()
    {
        float time = _targetTime;

        _targetTimeSlider.maxValue = time;
        _targetTimeSlider.gameObject.SetActive(true);

        while (time > 0)
        {
            yield return new WaitForSeconds(0.1f);

            time -= 0.1f;
            _targetTimeSlider.value = time;

        }

        _targetTimeSlider.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(0);

        if (_gamePhase == GamePhase.isShooting)// || _gamePhase == GamePhase.isMoving)
        {
            _startCountDownText.text = "Shoot!";
            
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if (Physics.Raycast(ray, out hitInfo, 15f))
                {
                    GameObject impactFX = Instantiate(_impactFX.gameObject, ray.origin, Quaternion.identity);
                    Debug.DrawRay(ray.origin, ray.direction * 10f);
                    _player.GetComponent<Animator>().SetTrigger("Fire");
                    impactFX.transform.position = hitInfo.point;
                    impactFX.transform.LookAt(_player.transform.position);
                    //_player.GetComponentInChildren<ParticleSystem>().Play();

                    if (hitInfo.collider.GetComponent<TargetBehaviour>() != null)
                    {
                        hitInfo.collider.GetComponent<TargetBehaviour>().DetectRay();
                    }

                    if (hitInfo.collider.GetComponent<Rigidbody>() != null)
                    {
                        hitInfo.collider.GetComponent<Rigidbody>().AddForce(ray.direction * 10f, ForceMode.Impulse);
                        _score++;
                        PlayPointsFX(hitInfo.point, 1);
                    }

                }
            } 
        }

        if (_gamePhase == GamePhase.isMoving)
        {
            _startCountDownText.text = "Get ready ...";

            /*if (Input.GetMouseButtonDown(0))
            {
                _speed++;
            }*/

            //IF PLAYER'S POSITION = CHUNK POSITION
            if (_player.transform.position == _chunks[_chunkIndex].transform.position - Vector3.forward * 5)//Vector3.forward * _sectionIndex * 10)
            {
                if (_sectionIndex > 0)
                {
                    /*if (_chunkIndex == 0)
                    {
                        //Destroy(_sections[_sectionIndex - 1].gameObject);

                        float dist = Vector3.Distance(_player.transform.position, Vector3.zero);

                        _player.transform.position = new Vector3(0, 0, _player.transform.position.z - dist);
                        for (int i = 0; i < _sections.Count; i++)
                        {
                            _sections[i].transform.position = new Vector3(0, 0, _sections[i].transform.position.z - dist);

                        } 
                    }*/


                }

                //ACTIVATE CHUNK IF THERE IS ONE
                if (_chunkIndex < _chunks.Count - 1)
                {
                    //START TARGET ANIMATIONS
                    _chunks[_chunkIndex].GetComponent<LevelBehaviour>().ActivateTargets();
                    //STOP RUNNING ANIMATION
                    if (_player.GetComponent<Animator>().GetBool("isRunning"))
                    {
                        _player.GetComponent<Animator>().SetBool("isRunning", false);
                    }
                    //START SHOOTING PHASE
                    _gamePhase = GamePhase.isShooting;

                    StartCoroutine(StartChunkTimer());
                }
                else //VICTORY!
                {
                    //STOP RUNNING ANIMATION
                    if (_player.GetComponent<Animator>().GetBool("isRunning"))
                    {
                        _player.GetComponent<Animator>().SetBool("isRunning", false);
                    }
                    //START CHEERING ANIMATION
                    _player.transform.rotation = Quaternion.Euler(0, 180, 0);

                    if (_score < _chunkScores[0])
                    {
                        _player.GetComponent<Animator>().SetTrigger("Defeat");
                        _gamePhase = GamePhase.isWaiting;


                        _startCountDownText.text = "Section failed!";

                        DisplayRestartButton();
                    }
                    else
                    {
                        _player.GetComponent<Animator>().SetTrigger("Cheer");

                        //SET GAME PHASE
                        _gamePhase = GamePhase.isWaiting;
                        Debug.Log("Victory");
                        _startCountDownText.text = "Congratulations!";

                        if (_chunks[_chunkIndex].GetComponentInChildren<ParticleSystem>(true) != null)
                            _chunks[_chunkIndex].GetComponentInChildren<ParticleSystem>(true).gameObject.SetActive(true);


                        //START NEXT LEVEL
                        if (_sectionIndex < _sections.Count - 1)
                        {
                            StartCoroutine(NextSection());
                        }
                        else
                        {
                            Debug.Log("END OF LEVEL");
                            _nextLevelBtn.gameObject.SetActive(true);
                        }

                    }
                }
                //_speed = 1;
            }
            else //PLAYER MOVES TO THE NEXT CHUNK
            {
                if (!_player.GetComponent<Animator>().GetBool("isRunning"))
                {
                    _player.GetComponent<Animator>().SetBool("isRunning", true);
                    _player.GetComponentInChildren<CollectDebris>().Collect();
                }
                //_player.GetComponent<Animator>().speed = _speed / 10f;
                //_player.GetComponent<Animator>().SetTrigger("Run");
                _player.transform.position = Vector3.MoveTowards(_player.transform.position, _chunks[_chunkIndex].transform.position - Vector3.forward * 5, _speed * Time.deltaTime);//Vector3.forward * _sectionIndex * 10, _speed * Time.deltaTime);
            }
        }
    }

    public void PlayPointsFX(Vector3 pos, int points)
    {
        tpsc.transform.position = pos;
        tpsc._textToDisplay = "+" + points.ToString();
        tpsc.OnEnable();
    }

    public void TeleportPlayerToStartLevel()
    {
        _player.transform.position = _sections[_sectionIndex].transform.position - Vector3.forward * 5;
        StartCoroutine(StartCountDown(3));
        _player.transform.rotation = Quaternion.Euler(Vector3.zero);
        _chunkIndex = 0;

    }

    public void DisplayRestartButton()
    {
        _restartSectionBtn.gameObject.SetActive(true);
    }

    public void NextLevel()
    {
        _levelIndex++;
        _levels[_levelIndex - 1].gameObject.SetActive(false);
        _levels[_levelIndex].gameObject.SetActive(true);
        _levels[_levelIndex].transform.position = Vector3.zero;
        _player.transform.position = _levels[_levelIndex].transform.position;
        _player.transform.rotation = Quaternion.Euler(Vector3.zero);

        _sectionIndex = 1;
        _chunkIndex = 0;
        GetSections(_levelIndex);
        
        StartCoroutine(StartCountDown(3));
    }
}

