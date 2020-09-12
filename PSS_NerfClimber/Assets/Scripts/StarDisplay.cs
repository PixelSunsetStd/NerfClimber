using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarDisplay : MonoBehaviour
{
    public GameManager _gamemanager;

    public Animator[] _stars;

    private void Awake()
    {
        _gamemanager = FindObjectOfType<GameManager>();
    }

    private void OnEnable()
    {
        StartCoroutine(StartAnim());
    }

    // Start is called before the first frame update
    IEnumerator StartAnim()
    {
        for (int i = 0; i < _gamemanager._numberOfLives; i++)
        {
            _stars[i].SetTrigger("StarIn");

            yield return new WaitForSeconds(.25f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
