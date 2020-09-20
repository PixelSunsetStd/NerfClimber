using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
    public Sprite[] _starsSprite;

    GameManager _gameManager;
    Text _text;
    Image _starsImg;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _text = GetComponentInChildren<Text>();
        _starsImg = GetComponentsInChildren<Image>()[1];
    }

    // Update is called once per frame
    void Update()
    {
        //_text.text = _gameManager._score.ToString() + " / " + _gameManager._levelScore[1];

        if (_gameManager._score < _gameManager._levelScore[0])
        {
            _text.text = _gameManager._score.ToString() + " / " + _gameManager._levelScore[0];
            _starsImg.sprite = _starsSprite[0];
        }
        else if (_gameManager._score < _gameManager._levelScore[1])
        {
            _text.text = _gameManager._score.ToString() + " / " + _gameManager._levelScore[1];
            _starsImg.sprite = _starsSprite[1];
        }
        else if (_gameManager._score < _gameManager._levelScore[2])
        {
            _text.text = _gameManager._score.ToString() + " / " + _gameManager._levelScore[2];
            _starsImg.sprite = _starsSprite[2];
        }
        else
        {
            _text.text = _gameManager._score.ToString() + " / " + _gameManager._levelScore[2];
            _starsImg.sprite = _starsSprite[3];
        }

    }
}
