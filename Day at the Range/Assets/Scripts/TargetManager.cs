using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    GameManager GameManagerScript;
    int score;
    // Start is called before the first frame update
    void Start()
    {
        GameManagerScript = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Projectile")
        {
            GameManagerScript.UpdateScore(5);
            Destroy(other.gameObject);
            //Reset target

        }
    }
}
