using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public enum MovementType
    {
        MoveTowards,
        Lerfowards
    }

    public MovementPath myPath;
    public MovementType type = MovementType.MoveTowards;
    public float speed = 1;
    public float maxDistanceToGoal = 0.1f;

    private IEnumerator<Transform> pointInPath;
    // Start is called before the first frame update
    void Start()
    {
        if(myPath == null)
        {
            return;
        }

        pointInPath = myPath.GetNextPathPoint();
        pointInPath.MoveNext();

        if(pointInPath.Current == null)
        {
            return;
        }

        transform.position = pointInPath.Current.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(pointInPath == null || pointInPath.Current == null)
        {
            return;
        }

        if(type == MovementType.MoveTowards)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointInPath.Current.position, Time.deltaTime * speed);
        }

        if(type == MovementType.Lerfowards)
        {
            transform.position = Vector3.Lerp(transform.position, pointInPath.Current.position, Time.deltaTime * speed);
        }

        var distanceSquared = (transform.position - pointInPath.Current.position).sqrMagnitude;
        if(distanceSquared < maxDistanceToGoal * maxDistanceToGoal)
        {
            pointInPath.MoveNext();
        }
    }
}
