using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TargetLogic : MonoBehaviour
{
    int randomStartRange = 3;
    int randomEndRange = 6;
    Vector3 endPosForReset;
    bool reset = false;
    [SerializeField] AudioSource targetAudioSource;
    [SerializeField] AudioClip targetHitSound;

    //To change target position
    Vector3[] positionArray;

    //for coroutines
    bool isCoroutineWorking;

    //To smoothly move targets
    Vector3 startMove;
    Vector3 endMove;
    Vector3 endPoint;
    Vector3 startPoint;
    float durationOfTransition = 2f;
    float elapsedTime;
    bool preformingSmoothTransition;

    protected void SmoothTransition()
    {
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

    protected IEnumerator TargetController()
    {
        if (isCoroutineWorking || preformingSmoothTransition)
        {
            yield break;
        }

        isCoroutineWorking = true;
        yield return new WaitForSeconds(Random.Range(randomStartRange, randomEndRange));
        moveTarget(startMove, true);
        yield return new WaitForSeconds(Random.Range(randomStartRange, randomEndRange));
        moveTarget(endMove, false);
        yield return new WaitForSeconds(Random.Range(randomStartRange, randomEndRange));
        ChangePosition();

        isCoroutineWorking = false;
        StartCoroutine(TargetController());
    }

    protected IEnumerator DelayTargetController()
    {
        yield return new WaitForSeconds(2);
        durationOfTransition = 2;
        elapsedTime = 0;
        reset = false;
        isCoroutineWorking = false;
        ChangePosition();
    }

    protected void moveTarget(Vector3 shift, bool isStartPos)
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

    protected void ChangePosition()
    {
        transform.position = positionArray[Random.Range(0, positionArray.Length)];
    }

    protected void SetupTarget()
    {
        preformingSmoothTransition = false;
        targetAudioSource = GetComponent<AudioSource>();
        StartCoroutine(TargetController());
    }

    protected void OnTriggerEnter(Collider other)
    {
        StopAllCoroutines();
        targetAudioSource.PlayOneShot(targetHitSound, 1.0f);
        reset = true;
        preformingSmoothTransition = true;
        startPoint = transform.position;
        endPoint = endPosForReset;
        durationOfTransition = 0.2f;
        elapsedTime = 0;
        StartCoroutine(DelayTargetController());
    }

    protected void SetPositionArray(Vector3[] PositionArray)
    {
        positionArray = PositionArray;
    }

    protected void SetMoveDistance(Vector3 start, Vector3 end)
    {
        startMove = start;
        endMove = end;
    }
}
