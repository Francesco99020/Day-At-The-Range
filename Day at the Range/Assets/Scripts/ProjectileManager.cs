using System.Collections;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public GameObject projectile;
    public new GameObject camera;
    [SerializeField] AudioClip shoot;
    [SerializeField] AudioClip reload;
    [SerializeField] AudioSource playerAudioSource;
    bool readyToShoot;

    // Start is called before the first frame update
    void Start()
    {
        readyToShoot = true;
        playerAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && readyToShoot)
        {
            playerAudioSource.PlayOneShot(shoot, 1.0f);
            Instantiate(projectile, camera.transform.position, camera.transform.rotation);
            StartCoroutine(ReloadNextShot());
        }
    }

    IEnumerator ReloadNextShot()
    {
        readyToShoot = false;
        yield return new WaitForSeconds(0.5f);
        playerAudioSource.PlayOneShot(reload, 1.0f);
        yield return new WaitForSeconds(0.5f);
        readyToShoot = true;

    }
}
