using UnityEngine;

public class Target3 : TargetLogic
{
    //To change target position
    Vector3[] positionArray = new[] {
        new Vector3(0f, 3.3f, 8.6f),
        new Vector3(0f, 3.3f, 6.6f),
        new Vector3(0f, 3.3f, 4.8f),
        new Vector3(-1.5f, 0.5f, 5.9f)
    };

    //How much to move target
    Vector3 startMove = new Vector3(0f, 1.5f, 0f);
    Vector3 endMove = new Vector3(0f, -1.5f, 0f);

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
