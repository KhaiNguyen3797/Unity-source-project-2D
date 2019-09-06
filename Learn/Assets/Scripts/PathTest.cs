using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTest : MonoBehaviour
{
    public enum Pathtypes
    {
        linear,
        loop
    }

    public Pathtypes Pathtype;
    public int movingTo = 0;
    public int movementDirection = 1;
    private int saveDirection;
    public bool turnOn = false;
    public bool nextPoint = false;
    public Transform[] PathPoint;

    public void OnDrawGizmos()
    {
        if (PathPoint.Length < 2 || PathPoint == null)
        {
            Debug.Log("Not point");
            return;
        }

        for (int i = 1; i < PathPoint.Length; i++)
        {
            Gizmos.DrawLine(PathPoint[i - 1].position, PathPoint[i].position);
        }

        if (Pathtype == Pathtypes.loop)
        {
            Gizmos.DrawLine(PathPoint[0].position, PathPoint[PathPoint.Length - 1].position);
        }
    }

    public IEnumerator<Transform> NextMovePath()
    {
        if (PathPoint == null || PathPoint.Length < 2)
        {
            Debug.Log("not point, not next point");
            yield break;
        }

        while (true)
        {
            yield return PathPoint[movingTo];

            if (PathPoint.Length == 1)
            {
                continue;
            }

            if (turnOn)
            {
                if (Pathtype == Pathtypes.linear)
                {
                    if (movingTo <= 0)
                    {
                        saveDirection = 1;
                    }
                    else if (movingTo >= PathPoint.Length - 1)
                    {
                        saveDirection = -1;
                    }

                    if(saveDirection < 0)
                    {
                        movementDirection = -1;
                    }
                    else if(saveDirection > 0)
                    {
                        movementDirection = 1;
                    }
                }
            }
            else
            {
                movementDirection = 0;
            }

            movingTo += movementDirection;

            if (Pathtype == Pathtypes.loop)
            {
                if (movingTo >= PathPoint.Length)
                {
                    movingTo = 0;
                }
                else if (movingTo < 0)
                {
                    movingTo = PathPoint.Length - 1;
                }
            }
        }
    }
}
