using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollow : MonoBehaviour
{
    public enum MovementType
    {
        MoveTowards,
        LeftTowards
    }

    public MovementType moveType;

    public PathMoving myPath;

    public float speed;
    public float distance;

    public IEnumerator<Transform> FollowPoint;

    // Start is called before the first frame update
    void Start()
    {
        if(myPath == null)
        {
            Debug.Log("notPath");
            return;
        }

        FollowPoint = myPath.NextMovePath();
        FollowPoint.MoveNext();

        if(FollowPoint.Current == null)
        {
            Debug.LogError("Tau ko dich duoc", gameObject);
            return;
        }

        transform.position = FollowPoint.Current.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(FollowPoint == null || FollowPoint.Current == null)
        {
            Debug.Log("Error");
            return;
        }

        if(moveType == MovementType.MoveTowards)
        {
            transform.position = Vector3.MoveTowards(transform.position, FollowPoint.Current.position, speed * Time.deltaTime);
        }
        else if(moveType == MovementType.LeftTowards)
        {
            transform.position = Vector3.Lerp(transform.position, FollowPoint.Current.position, speed * Time.deltaTime);
        }

        var Distance = (transform.position - FollowPoint.Current.position).sqrMagnitude;
        if(Distance < distance * distance)
        {
            FollowPoint.MoveNext();
        }
    }
}
