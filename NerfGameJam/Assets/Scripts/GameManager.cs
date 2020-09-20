using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool _testLevelMode;

    public GameObject _player;
    public int _speed = 1;

    public enum GamePhase { isWaiting, isMoving, isShooting };
    public GamePhase _gamePhase;

    public int _totalScore;
    public int _score;
    public int[] _levelScore;// = new int[3];

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

    public AudioSource _sfxAudioSource;
    public AudioClip[] _audioClips;

    public Transform _finalScorePanel;

    public Transform _trail;

    // Start is called before the first frame update
    void Start()
    {
        //Invoke("StartGame", 3f);
        //StartCoroutine(StartCountDown(3));
        tpsc = FindObjectOfType<TextParticleSystemController>();

        _totalScore = PlayerPrefs.GetInt("TotalScore", 0);

        if (!_testLevelMode)
        {
            _levelIndex = PlayerPrefs.GetInt("Level", 0); 
        }
        if (_levelIndex > _levels.Count - 1)
            _levelIndex = 0;

        LoadLevel(_levelIndex);

        GetSections(_levelIndex);

        //_player.transform.position = _chunks[_chunkIndex].transform.position - Vector3.forward * 5;
    }

    void LoadLevel(int levelIndex)
    {
        foreach (GameObject level in _levels)
        {
            level.SetActive(false);
        }

        for (int i = 0; i < _levels.Count; i++)
        {
            _levels[levelIndex].transform.position = Vector3.zero;
            _player.transform.position = _levels[levelIndex].transform.position;
            _player.transform.rotation = Quaternion.Euler(Vector3.zero);

            if (i != levelIndex)
                _levels[i].SetActive(false);
            else _levels[levelIndex].SetActive(true);
        }
    }

    public void StartGame()
    {
        StartCoroutine(StartCountDown(3f));
    }

    public void GetChunks(int index)
    {
        _chunks.Clear();
        _levelScore = new int[3];
        for (int i = 0; i < _sections[index].transform.childCount; i++)
        {
            if (_sections[index].transform.GetChild(i).GetComponent<LevelBehaviour>() != null)
            {
                _chunks.Add(_sections[index].transform.GetChild(i).gameObject);
                int numberOfTargets = _sections[index].transform.GetChild(i).GetComponent<LevelBehaviour>()._targets.Count;

                if (_levelIndex < 12)
                {
                    _levelScore[0] += numberOfTargets * 20;
                    _levelScore[1] += numberOfTargets * 30;
                    _levelScore[2] += numberOfTargets * 40;
                }
                else
                {
                    _levelScore[0] += numberOfTargets * 30;
                    _levelScore[1] += numberOfTargets * 35;
                    _levelScore[2] += numberOfTargets * 40;
                }

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
        Debug.Log("Next Section");

        //_player.transform.position = Vector3.zero;
        //_sections[_sectionIndex].transform.position = _player.transform.position + Vector3.forward * 10;

        _gamePhase = GamePhase.isMoving;
    }

    IEnumerator StartCountDown(float time)
    {

        _player.transform.rotation = Quaternion.Euler(Vector3.zero);

        _startCountDownText.gameObject.SetActive(true);

        while (time > 0)
        {
            _startCountDownText.text = time.ToString();
            _startCountDownText.GetComponent<Animator>().SetTrigger("Bounce");

            yield return new WaitForSeconds(1f);

            time--;
        }

        //_startCountDownText.gameObject.SetActive(false);
        _gamePhase = GamePhase.isMoving;
    }

    IEnumerator StartChunkTimer()
    {
        float time = _targetTime - 0.25f;

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
        {
            if (_testLevelMode)
            {
                //PlayerPrefs.SetInt("Level", 0);
                SceneManager.LoadScene(0); 
            }else 
                Application.Quit();
        }

        if (_gamePhase == GamePhase.isShooting)// || _gamePhase == GamePhase.isMoving)
        {

            _player.transform.rotation = Quaternion.Euler(Vector3.zero);
            _startCountDownText.text = "Shoot!";
            
            if (Input.GetMouseButtonDown(0))
            {
                

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                

                if (Physics.Raycast(ray, out hitInfo, 20f))
                {
                    _sfxAudioSource.clip = _audioClips[2];
                    _sfxAudioSource.Play();

                    StopCoroutine("Trail");
                    StartCoroutine("Trail", hitInfo.point);

                    GameObject impactFX = Instantiate(_impactFX.gameObject, ray.origin, Quaternion.identity);
                    //colorPick
                    _impactFX.GetComponentInChildren<Debris>().Init(hitInfo.collider.GetComponentInChildren<Renderer>().material.color);
                    Debug.DrawRay(ray.origin, ray.direction * 20f);
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
                        _score += 1;
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
            _player.transform.rotation = Quaternion.Euler(Vector3.zero);

            //IF PLAYER'S POSITION = CHUNK POSITION
            if (_player.transform.position == _chunks[_chunkIndex].transform.position - Vector3.forward * 5)//Vector3.forward * _sectionIndex * 10)
            {
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

                    if (_score < _levelScore[0])
                    {
                        _player.GetComponent<Animator>().SetTrigger("Defeat");
                        _gamePhase = GamePhase.isWaiting;


                        _startCountDownText.text = "Section failed!";

                        _sfxAudioSource.clip = _audioClips[1];
                        _sfxAudioSource.Play();

                        DisplayRestartButton();
                    }
                    else
                    {
                        _player.GetComponent<Animator>().SetTrigger("Cheer");

                        //SET GAME PHASE
                        _gamePhase = GamePhase.isWaiting;
                        Debug.Log("Victory");
                        _startCountDownText.text = "Congratulations!";
                        _totalScore += _score;
                        PlayerPrefs.SetInt("TotalScore", _totalScore);

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
                            _sfxAudioSource.clip = _audioClips[0];
                            _sfxAudioSource.Play();

                            if (_levelIndex == _levels.Count - 1)
                            {
                                _finalScorePanel.GetComponentInChildren<Text>().text = _totalScore.ToString();
                                _finalScorePanel.gameObject.SetActive(true);

                                if (_totalScore > PlayerPrefs.GetInt("BestScore", 0))
                                {
                                    PlayerPrefs.SetInt("BestScore", _totalScore);
                                    _startCountDownText.text = "New high score !";
                                } else _startCountDownText.text = "Best Score : " + PlayerPrefs.GetInt("BestScore", 0);


                            }
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
        _score = 0;
        _player.GetComponent<Animator>().SetTrigger("Idle");
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
        _score = 0;
        _levelIndex++;
        _levels[_levelIndex - 1].gameObject.SetActive(false);

        if (_levelIndex > _levels.Count - 1)
        {
            _levelIndex = 0;
            _totalScore = 0;
            PlayerPrefs.SetInt("TotalScore", _totalScore);
        }

        PlayerPrefs.SetInt("Level", _levelIndex);

        _levels[_levelIndex].transform.position = Vector3.zero;
        _player.transform.position = _levels[_levelIndex].transform.position;
        _player.transform.rotation = Quaternion.Euler(Vector3.zero);
        _player.GetComponent<Animator>().SetTrigger("Idle");
        _levels[_levelIndex].gameObject.SetActive(true);

        _sectionIndex = 1;
        _chunkIndex = 0;
        GetSections(_levelIndex);
        
        StartCoroutine(StartCountDown(3));
    }

    IEnumerator Trail(Vector3 pos)
    {
        _trail.gameObject.SetActive(false);
        _trail.position = _player.transform.position + Vector3.up + Vector3.forward;

        yield return new WaitForEndOfFrame();

        _trail.gameObject.SetActive(true);

        while (_trail.position != pos)
        {
            _trail.position = Vector3.MoveTowards(_trail.position, pos, 50f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
    
}

