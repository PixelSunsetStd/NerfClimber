using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeDisplay : MonoBehaviour
{
    public GameManager _gameManager;

    public Image[] _lifeUI;
    public Color _lifeColor;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();

        foreach (Image s in _lifeUI)
        {
            s.color = _lifeColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (_gameManager._numberOfLives)
        {
            case 0:
                _lifeUI[0].color = Color.grey;
                _lifeUI[1].color = Color.grey;
                _lifeUI[2].color = Color.grey;
                break;

            case 1:
                _lifeUI[0].color = _lifeColor;
                _lifeUI[1].color = Color.grey;
                _lifeUI[2].color = Color.grey;
                break;

            case 2:
                _lifeUI[0].color = _lifeColor;
                _lifeUI[1].color = _lifeColor;
                _lifeUI[2].color = Color.grey;
                break;

            case 3:
                _lifeUI[0].color = _lifeColor;
                _lifeUI[1].color = _lifeColor;
                _lifeUI[2].color = _lifeColor;
                break;
        }
    }
}
