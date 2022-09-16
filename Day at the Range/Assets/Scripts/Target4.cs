using UnityEngine;

public class Target4 : TargetLogic
{
    //To change target position
    Vector3[] positionArray = new[] {
        new Vector3(4f, 4.4f, -1.8f),
        new Vector3(4f, 4.4f, -3.6f),
        new Vector3(4f, 4.4f, -4.95f),
        new Vector3(4f, 4.4f, -6.7f)
    };

    //How much to move target
    Vector3 startMove = new Vector3(0f, -1.3f, 0f);
    Vector3 endMove = new Vector3(0f, 1.3f, 0f);

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