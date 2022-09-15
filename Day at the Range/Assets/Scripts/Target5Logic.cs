using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target5Logic : MonoBehaviour
{
    int randomStartRange = 3;
    int randomEndRange = 6;
    Vector3 endPosForReset;
    bool reset = false;
    [SerializeField] AudioSource targetAudioSource;
    [SerializeField] AudioClip targetHitSound;

    //To change target position
    Vector3[] positionArray = new[] {
        new Vector3(-8f, 0.8f, -7.5f),
        new Vector3(-8f, 0.8f, -5f),
        new Vector3(-8f, 0.8f, -2.9f),
        new Vector3(4f, 6.7f, -3.6f),
        new Vector3(4f, 6.7f, -6.5f)
    };

    //for coroutines
    bool isCoroutine5Working;

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
        StartCoroutine(Target5Controller());
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Target5Controller());

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

    IEnumerator Target5Controller()
    {
        if (isCoroutine5Working || preformingSmoothTransition)
        {
            yield break;
        }

        isCoroutine5Working = true;
        yield return new WaitForSeconds(Random.Range(randomStartRange, randomEndRange));
        moveTarget(new Vector3(0, 1, 0), true);
        yield return new WaitForSeconds(Random.Range(randomStartRange, randomEndRange));
        moveTarget(new Vector3(0, -1, 0), false);
        yield return new WaitForSeconds(Random.Range(randomStartRange, randomEndRange));
        ChangePosition();

        isCoroutine5Working = false;
        StartCoroutine(Target5Controller());
    }

    IEnumerator DelayTarget5Controller()
    {
        yield return new WaitForSeconds(2);
        durationOfTransition = 2;
        elapsedTime = 0;
        reset = false;
        isCoroutine5Working = false;
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
        StartCoroutine(DelayTarget5Controller());
    }
}