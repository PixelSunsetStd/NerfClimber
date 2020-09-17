using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public List<GameObject> _targets;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TargetTouched()
    {
        foreach (GameObject target in _targets)
        {
            target.GetComponent<Collider>().enabled = false;
        }

        GetComponentInParent<Animator>().SetTrigger("Close");
        GetComponentInParent<LevelBehaviour>()._targets.Remove(transform.parent.gameObject);
    }
}
