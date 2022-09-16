using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] float speed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        GameObject camera = GameObject.Find("Main Camera");
        projectileRb.AddForce(camera.transform.forward * speed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        //checks if projectile leaves game
        if (transform.position.x > 25 || transform.position.x < -25 || transform.position.y < -1 || transform.position.y > 30 || transform.position.z > 25 || transform.position.z < -25)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
