using UnityEngine;

public class Target1 : TargetLogic
{
    //To change target position
    Vector3[] positionArray = new[] {
        new Vector3(0f, 0.6f, 5f),
        new Vector3(0f, 2.2f, 5f),
        new Vector3(0f, 3.5f, 5f),
        new Vector3(4f, 5f, -6.5f),
        new Vector3(4f, 6.8f, -6.5f)
    };

    //How much to move target
    Vector3 startMove = new Vector3(0f, 0f, -1.5f);
    Vector3 endMove = new Vector3(0f, 0f, 1.5f);

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
