using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPath : MonoBehaviour
{
    public enum PathTypes
    {
        linear,
        loop
    }

    public PathTypes pathTypes;
    public int movementDirection = 1;
    public int movingTo = 0;
    public Transform[] PathSequece;

    // Start is called before the first frame update
    public void OnDrawGizmos()
    {
        if(PathSequece == null || PathSequece.Length < 2)
        {
            return;
        }

        for(int i = 1; i <PathSequece.Length; i++)
        {
            Gizmos.DrawLine(PathSequece[i - 1].position, PathSequece[i].position);
        }

        if(pathTypes == PathTypes.loop)
        {
            Gizmos.DrawLine(PathSequece[0].position, PathSequece[PathSequece.Length - 1].position);
        }
    }

    // Update is called once per frame
    public IEnumerator<Transform> GetNextPathPoint()
    {
        if(PathSequece == null || PathSequece.Length < 1)
        {
            yield break;
        }

        while (true)
        {
            yield return PathSequece[movingTo];
            if(PathSequece.Length == 1)
            {
                continue;
            }

            if(pathTypes == PathTypes.linear)
            {
                if(movingTo <= 0)
                {
                    movementDirection = 1;
                }
                else if(movingTo >= PathSequece.Length - 1)
                {
                    movementDirection = -1;
                }
            }

            movingTo += movementDirection;
            if(pathTypes == PathTypes.loop)
            {
                if(movingTo >= PathSequece.Length)
                {
                    movingTo = 0;
                }
                if(movingTo < 0)
                {
                    movingTo = PathSequece.Length - 1;
                }
            }
        }
    }
}
