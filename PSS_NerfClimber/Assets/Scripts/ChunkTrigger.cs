using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
    GameManager _gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();             
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Enter");
        Debug.Log(collision.gameObject.tag);

        if (collision.gameObject.CompareTag("Player"))
            Debug.Log("Next Chunk");
    }*/

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        Debug.Log(other.tag);

        if (other.CompareTag("Player"))
            Debug.Log("Next Chunk");

        
        _gameManager._levelIndex++;
        _gameManager.NextChunk();
        GetComponent<Collider>().enabled = false;
    }
}
