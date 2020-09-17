using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject _player;
    public int _speed = 1;

    public enum GamePhase { isWaiting, isMoving, isShooting };
    public GamePhase _gamePhase;

    public int _score;

    public int _levelIndex;
    public int _chunkIndex;

    public List<GameObject> _levels;
    public List<GameObject> _levelChunks;

    public float _targetTime;
    public Slider _targetTimeSlider;

    public ParticleSystem _impactFX;

    public Text _startCountDownText;

    TextParticleSystemController tpsc;

    // Start is called before the first frame update
    void Start()
    {
        //Invoke("StartGame", 3f);
        //StartCoroutine(StartCountDown(3));
        tpsc = FindObjectOfType<TextParticleSystemController>();

        GetChunks(_levelIndex);

        //_player.transform.position = _levelChunks[_chunkIndex].transform.position - Vector3.forward * 5;
    }

    public void StartGame()
    {
        StartCoroutine(StartCountDown(3f));
    }

    public void GetChunks(int index)
    {
        _levelChunks.Clear();
        for (int i = 0; i < _levels[index].transform.childCount; i++)
        {
            if (_levels[index].transform.GetChild(i).GetComponent<LevelBehaviour>() != null)
                _levelChunks.Add(_levels[index].transform.GetChild(i).gameObject);
        }
    }

    public IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(3.0f);

        //Destroy(_levels[_levelIndex].gameObject);

        _levelIndex++;
        GetChunks(_levelIndex);
        _chunkIndex = 0;

        //_player.transform.position = Vector3.zero;
        //_levels[_levelIndex].transform.position = Vector3.zero;

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
            if (_player.transform.position == _levelChunks[_chunkIndex].transform.position - Vector3.forward * 5)//Vector3.forward * _levelIndex * 10)
            {
                //ACTIVATE CHUNK IF THERE IS ONE
                if (_chunkIndex < _levelChunks.Count - 1)
                {
                    //START TARGET ANIMATIONS
                    _levelChunks[_chunkIndex].GetComponent<LevelBehaviour>().ActivateTargets();
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
                    _player.GetComponent<Animator>().SetTrigger("Cheer");
                    //SET GAME PHASE
                    _gamePhase = GamePhase.isWaiting;
                    Debug.Log("Victory");
                    _startCountDownText.text = "Congratulations!";

                    if (_levelChunks[_chunkIndex].GetComponentInChildren<ParticleSystem>(true) != null)
                        _levelChunks[_chunkIndex].GetComponentInChildren<ParticleSystem>(true).gameObject.SetActive(true);


                    //START NEXT LEVEL
                    if (_levelIndex < _levels.Count - 1)
                        StartCoroutine(NextLevel());
                    else Debug.Log("END OF LEVEL");
                }
                //_speed = 1;
            }
            else //PLAYER MOVES TO THE NEXT CHUNK
            {
                if (!_player.GetComponent<Animator>().GetBool("isRunning"))
                    _player.GetComponent<Animator>().SetBool("isRunning", true);
                //_player.GetComponent<Animator>().speed = _speed / 10f;
                //_player.GetComponent<Animator>().SetTrigger("Run");
                _player.transform.position = Vector3.MoveTowards(_player.transform.position, _levelChunks[_chunkIndex].transform.position - Vector3.forward * 5, _speed * Time.deltaTime);//Vector3.forward * _levelIndex * 10, _speed * Time.deltaTime);
            }
        }
    }

    public void PlayPointsFX(Vector3 pos, int points)
    {
        tpsc.transform.position = pos;
        tpsc._textToDisplay = "+" + points.ToString();
        tpsc.OnEnable();
    }
}
