using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    GameManager GameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        GameManagerScript = gameManager.GetComponent<GameManager>();
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
