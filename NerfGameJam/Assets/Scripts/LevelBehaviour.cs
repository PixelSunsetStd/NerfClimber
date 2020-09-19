using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBehaviour : MonoBehaviour
{
    public List<GameObject> _targets;

    GameManager _gameManager;

    bool _nextLevel;
    // Start is called before the first frame update
    void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();

        //_targets.Clear();
        /*for (int i = 0; i < transform.childCount; i++)
        {
            _targets.Add(transform.GetChild(i).gameObject);
        }*/
    }

    public void ActivateTargets()
    {
        _targets.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            _targets.Add(transform.GetChild(i).gameObject);
        }

        foreach (GameObject target in _targets)
        {
            target.GetComponent<Animator>().SetTrigger("Open");

            Collider[] colls = target.GetComponentInChildren<TargetManager>().GetComponentsInChildren<Collider>();

            foreach (Collider coll in colls)
            {
                coll.enabled = true;
            }

            Renderer[] rends = target.GetComponentInChildren<TargetManager>().GetComponentsInChildren<Renderer>();

            foreach (Renderer rend in rends)
            {
                rend.enabled = true;
            }
        }

        if (GetComponentsInChildren<Rotate>().Length > 0)
        {
            foreach (Rotate rot in GetComponentsInChildren<Rotate>())
            {
                rot.StartRot();
            }
            foreach (Translator trans in GetComponentsInChildren<Translator>(true))
            {
                trans.enabled = true;
            }
        }

        StartCoroutine(DeactivateTargets());
    }

    public IEnumerator DeactivateTargets()
    {
        yield return new WaitForSeconds(_gameManager._targetTime);

        foreach (GameObject target in _targets)
        {
            target.GetComponent<Animator>().SetTrigger("Close");

            Collider[] colls = target.GetComponentInChildren<TargetManager>().GetComponentsInChildren<Collider>();

            foreach (Collider coll in colls)
            {
                coll.enabled = false;
            }

            Renderer[] rends = target.GetComponentInChildren<TargetManager>().GetComponentsInChildren<Renderer>();

            foreach (Renderer rend in rends)
            {
                rend.enabled = true;
            }
        }

        if (GetComponentsInChildren<Rotate>().Length > 0)
        {
            foreach (Rotate rot in GetComponentsInChildren<Rotate>())
            {
                rot.StopRot();
            }

            foreach (Translator trans in GetComponentsInChildren<Translator>())
            {
                trans.enabled = false;
            }
        }

        StartCoroutine(NextLevel());
    }

    // Update is called once per frame
    void Update()
    {
        if (_targets.Count == 0 && !_nextLevel)
        {
            //StartCoroutine("NextLevel");
            _nextLevel = true;
        }
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(1.0f);

        if (_gameManager._chunkIndex < _gameManager._chunks.Count - 1)
        {
            FindObjectOfType<GameManager>()._chunkIndex++;
            FindObjectOfType<GameManager>()._gamePhase = GameManager.GamePhase.isMoving;
            //if (GetComponentInChildren<Animator>() != null)
                //GetComponentInChildren<Animator>().SetTrigger("Open");
        }

        //Destroy(gameObject);

    }
}
