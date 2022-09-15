using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target3Logic : MonoBehaviour
{
    int randomStartRange = 3;
    int randomEndRange = 6;
    Vector3 endPosForReset;
    bool reset = false;
    [SerializeField] AudioSource targetAudioSource;
    [SerializeField] AudioClip targetHitSound;

    //To change target position
    Vector3[] positionArray = new[] {
        new Vector3(0f, 3.3f, 8.6f),
        new Vector3(0f, 3.3f, 6.6f),
        new Vector3(0f, 3.3f, 4.8f),
        new Vector3(-1.5f, 0.5f, 5.9f)
    };

    //for coroutines
    bool isCoroutine3Working;

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
        StartCoroutine(Target3Controller());
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Target3Controller());

        if (preformingSmoothTransition)
        {
            elapsedTime += Time.deltaTime;
            float percentComplete = elapsedTime / durationOfTransition;

            transform.position = Vector3.Lerp(startPoint, endPoint, percentComplete);
            if (transform.position == endPoint)
            {
                elapsedTime = 0;
                preformingSmoothTransition = false;
            }
        }
    }

    IEnumerator Target3Controller()
    {
        if (isCoroutine3Working || preformingSmoothTransition)
        {
            yield break;
        }

        isCoroutine3Working = true;
        yield return new WaitForSeconds(Random.Range(randomStartRange, randomEndRange));
        moveTarget(new Vector3(0, 1.5f, 0), true);
        yield return new WaitForSeconds(Random.Range(randomStartRange, randomEndRange));
        moveTarget(new Vector3(0, -1.5f, 0), false);
        yield return new WaitForSeconds(Random.Range(randomStartRange, randomEndRange));
        ChangePosition();

        isCoroutine3Working = false;
        StartCoroutine(Target3Controller());
    }

    IEnumerator DelayTarget3Controller()
    {
        yield return new WaitForSeconds(2);
        durationOfTransition = 2;
        elapsedTime = 0;
        reset = false;
        isCoroutine3Working = false;
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
        StartCoroutine(DelayTarget3Controller());
    }
}
