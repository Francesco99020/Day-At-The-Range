using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target2Logic : MonoBehaviour
{
    int randomStartRange = 3;
    int randomEndRange = 6;
    [SerializeField] AudioSource targetAudioSource;
    [SerializeField] AudioClip targetHitSound;

    //To reset position when target is hit
    Vector3 endPosForReset;
    bool reset = false;

    //To change target position
    Vector3[] positionArray = new[] {
        new Vector3(8.028f, 0.8f, 2.5f), new Vector3(8.028f, 0.8f, -1f),
        new Vector3(8.028f, 0.8f, -4.2f), new Vector3(8.028f, 0.8f, -7.7f), 
        new Vector3(8.028f, 0.8f, -11.19f), new Vector3(8.028f, 0.8f, -14.85f),
        new Vector3(8.028f, 0.8f, -18.60f), new Vector3(8.028f, 0.8f, -22.7f)
    };

    //for coroutines
    bool isCoroutine2Working;

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
        StartCoroutine(Target2Controller());
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Target2Controller());

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

    IEnumerator Target2Controller()
    {
        if (isCoroutine2Working || preformingSmoothTransition)
        {
            yield break;
        }

        isCoroutine2Working = true;
        yield return new WaitForSeconds(Random.Range(randomStartRange, randomEndRange));
        moveTarget(new Vector3(0, 1.25f, 0), true);
        yield return new WaitForSeconds(Random.Range(randomStartRange, randomEndRange));
        moveTarget(new Vector3(0, -1.25f, 0), false);
        yield return new WaitForSeconds(Random.Range(randomStartRange, randomEndRange));
        ChangePosition();

        isCoroutine2Working = false;
        StartCoroutine(Target2Controller());
    }

    IEnumerator DelayTarget2Controller()
    {
        yield return new WaitForSeconds(2);
        durationOfTransition = 2;
        elapsedTime = 0;
        reset = false;
        isCoroutine2Working = false;
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
        StartCoroutine(DelayTarget2Controller());
    }
}
