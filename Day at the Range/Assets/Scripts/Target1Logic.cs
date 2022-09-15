using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class Target1Logic : MonoBehaviour
{
    int randomStartRange = 3;
    int randomEndRange = 6;
    Vector3 endPosForReset;
    bool reset = false;
    [SerializeField] AudioSource targetAudioSource;
    [SerializeField] AudioClip targetHitSound;

    //To change target position
    Vector3[] positionArray = new[] {
        new Vector3(0f, 0.6f, 5f),
        new Vector3(0f, 2.2f, 5f),
        new Vector3(0f, 3.5f, 5f),
        new Vector3(4f, 5f, -6.5f),
        new Vector3(4f, 6.8f, -6.5f)
    };

    //for coroutines
    bool isCoroutine1Working;

    //To smoothly move targets
    Vector3 endPoint;
    Vector3 startPoint;
    float durationOfTransition = 2f;
    float elapsedTime;
    bool preformingSmoothTransition;

    // Start is called before the first frame update
    void Start()
    {
        preformingSmoothTransition = false;
        targetAudioSource = GetComponent<AudioSource>();
        StartCoroutine(Target1Controller());
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Target1Controller());

        if (preformingSmoothTransition)
        {
            elapsedTime += Time.deltaTime;
            float percentComplete = elapsedTime / durationOfTransition;

            transform.position = Vector3.Lerp(startPoint, endPoint, percentComplete);
            if(transform.position == endPoint)
            {
                elapsedTime = 0;
                preformingSmoothTransition = false;
            }
        }
    }

    IEnumerator Target1Controller()
    {
        if (isCoroutine1Working || preformingSmoothTransition)
        {
            yield break;
        }

        isCoroutine1Working = true;
        yield return new WaitForSeconds(Random.Range(randomStartRange, randomEndRange));
        moveTarget(new Vector3(0, 0, -1.5f), true);
        yield return new WaitForSeconds(Random.Range(randomStartRange, randomEndRange));
        moveTarget(new Vector3(0, 0, 1.5f), false);
        yield return new WaitForSeconds(Random.Range(randomStartRange, randomEndRange));
        ChangePosition();

        isCoroutine1Working = false;
        StartCoroutine(Target1Controller());
    }

    IEnumerator DelayTarget1Controller()
    {
        yield return new WaitForSeconds(2);
        durationOfTransition = 2;
        elapsedTime = 0;
        reset = false;
        isCoroutine1Working = false;
        ChangePosition();
    }

    void moveTarget(Vector3 shift, bool isStartPos)
    {
        if (isStartPos)
        {
            endPosForReset = transform.position;
        }
        //Sets variables to move target in update function
        if (!reset)
        {
            startPoint = transform.position;
            endPoint = startPoint + shift;
            preformingSmoothTransition = true;
        }
    }

    void ChangePosition()
    {
        transform.position = positionArray[Random.Range(0, positionArray.Length)];
    }

    private void OnTriggerEnter(Collider other)
    {
        StopAllCoroutines();
        targetAudioSource.PlayOneShot(targetHitSound, 1.0f);
        reset = true;
        preformingSmoothTransition = true;
        startPoint = transform.position;
        endPoint = endPosForReset;
        durationOfTransition = 0.2f;
        elapsedTime = 0;
        StartCoroutine(DelayTarget1Controller());
    }
}
