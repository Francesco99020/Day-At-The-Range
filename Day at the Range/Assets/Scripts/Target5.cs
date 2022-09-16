using UnityEngine;

public class Target5 : TargetLogic
{
    //To change target position
    Vector3[] positionArray = new[] {
        new Vector3(-8f, 0.8f, -7.5f),
        new Vector3(-8f, 0.8f, -5f),
        new Vector3(-8f, 0.8f, -2.9f),
        new Vector3(4f, 6.7f, -3.6f),
        new Vector3(4f, 6.7f, -6.5f)
    };

    //How much to move target
    Vector3 startMove = new Vector3(0f, 1f, 0f);
    Vector3 endMove = new Vector3(0f, -1f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        SetPositionArray(positionArray);
        SetMoveDistance(startMove, endMove);
        SetupTarget();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(TargetController());
        SmoothTransition();
    }
}