using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanonBehaviour : MonoBehaviour
{
    public GameManager _gameManager;

    public bool _isControlable = true;

    public bool _isSuper;
    public float _superPower = 1;

    public bool _hasBall;
    public GameObject _feedback;

    GameObject _ball;
    public Transform _shooter;

    public float _degreesPerSec;

    public AudioClip _sndCatch;
    public AudioClip _sndSpit;

    public bool _randomPos;

    public ParticleSystem _rewardFX;
    public string[] _rewardText;
    TextMeshPro _textFX;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _degreesPerSec = Random.Range(90f, 180f);

        _textFX = _rewardFX.GetComponent<TextMeshPro>();

        if (_randomPos)
            transform.position = new Vector3(Random.Range(-5f, 5f), transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = Quaternion.Euler(0f, 0f, Mathf.PingPong(_degreesPerSec * Time.time, 180));
    }

    void SetTextFX (int index)
    {
        _textFX.text = _rewardText[index];
    }

    public void SpitBall()
    {
        if (_hasBall)
        {
            if (!_isSuper)
            {
                _ball.transform.position = _shooter.position;
                _ball.SetActive(true);
                _ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
                _ball.GetComponent<Rigidbody>().AddForce(transform.right * 15f * _superPower, ForceMode.Impulse);
                _ball = null;
                GetComponent<Animator>().SetTrigger("Spit");
                _hasBall = false;
                _gameManager._activeCanon = null;
                GetComponent<AudioSource>().clip = _sndSpit;
                GetComponent<AudioSource>().Play();
                _feedback.SetActive(false);
            }
            else
            {
                GetComponent<Animator>().enabled = false;
                if (_superPower == 1)
                {
                    StartCoroutine("SuperCanon");
                }
                _superPower += 0.1f;
                transform.localScale = new Vector3(_superPower + .25f, _superPower + .25f, _superPower + .25f);
            }
        }
    }

    IEnumerator SuperCanon()
    {
        yield return new WaitForSeconds(3.0f);

        _isSuper = false;
        GetComponent<Animator>().enabled = true;
        SpitBall();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _ball = other.gameObject;
            _ball.transform.position = transform.position;
            _ball.SetActive(false);
            _hasBall = true;
            GetComponent<Animator>().SetTrigger("Catch");
            _gameManager._activeCanon = GetComponent<CanonBehaviour>();
            GetComponent<AudioSource>().clip = _sndCatch;
            GetComponent<AudioSource>().Play();
            _feedback.SetActive(true);

            _textFX.transform.position = other.transform.position;
            SetTextFX(other.GetComponent<BallBehaviour>()._combo);
            _textFX.GetComponent<ParticleSystem>().Play();

            other.GetComponent<BallBehaviour>()._combo = 0;
        }
    }
}
