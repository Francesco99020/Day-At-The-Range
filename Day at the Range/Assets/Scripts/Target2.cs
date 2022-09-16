using UnityEngine;

public class Target2 : TargetLogic
{
    //To change target position
    Vector3[] positionArray = new[] {
        new Vector3(8.028f, 0.8f, 2.5f), new Vector3(8.028f, 0.8f, -1f),
        new Vector3(8.028f, 0.8f, -4.2f), new Vector3(8.028f, 0.8f, -7.7f), 
        new Vector3(8.028f, 0.8f, -11.19f), new Vector3(8.028f, 0.8f, -14.85f),
        new Vector3(8.028f, 0.8f, -18.60f), new Vector3(8.028f, 0.8f, -22.7f)
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
