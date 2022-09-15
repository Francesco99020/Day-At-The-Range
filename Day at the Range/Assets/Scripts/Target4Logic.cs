using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target4Logic : MonoBehaviour
{
    int randomStartRange = 3;
    int randomEndRange = 6;
    [SerializeField] AudioSource targetAudioSource;
    [SerializeField] AudioClip targetHitSound;

    //To reset target position
    Vector3 endPosForReset;
    bool reset = false;

    //To change target position
    Vector3[] positionArray = new[] {
        new Vector3(4f, 4.4f, -1.8f),
        new Vector3(4f, 4.4f, -3.6f),
        new Vector3(4f, 4.4f, -4.95f),
        new Vector3(4f, 4.4f, -6.7f)
    };

    //for coroutines
    bool isCoroutine4Working;

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
        StartCoroutine(Target4Controller());
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Target4Controller());

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

    IEnumerator Target4Controller()
    {
        if (isCoroutine4Working || preformingSmoothTransition)
        {
            yield break;
        }

        isCoroutine4Working = true;
        yield return new WaitForSeconds(Random.Range(randomStartRange, randomEndRange));
        moveTarget(new Vector3(0, -1.25f, 0), true);
        yield return new WaitForSeconds(Random.Range(randomStartRange, randomEndRange));
        moveTarget(new Vector3(0, 1.25f, 0), false);
        yield return new WaitForSeconds(Random.Range(randomStartRange, randomEndRange));
        ChangePosition();

        isCoroutine4Working = false;
        StartCoroutine(Target4Controller());
    }

    IEnumerator DelayTarget4Controller()
    {
        yield return new WaitForSeconds(2);
        durationOfTransition = 2;
        elapsedTime = 0;
        reset = false;
        isCoroutine4Working = false;
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
        StartCoroutine(DelayTarget4Controller());
    }
}