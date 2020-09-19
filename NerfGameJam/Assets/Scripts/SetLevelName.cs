using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetLevelName : MonoBehaviour
{
    TextMeshPro _tmp;
    
    // Start is called before the first frame update
    void Start()
    {
        _tmp = GetComponent<TextMeshPro>();
        _tmp.text = _tmp.transform.root.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
