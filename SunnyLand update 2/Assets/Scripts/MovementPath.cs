using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPath : MonoBehaviour {

    public enum PathTypes
    {
        linear,
        loop
    }

    public PathTypes PathType;
    public int movementDirection = 1;
    public int movingto = 0;
    public Transform[] PathSequece;

    // OnDrawGizmos vẽ các đối tưọng
    public void OnDrawGizmos()
    {
        if(PathSequece == null || PathSequece.Length < 2)
        {
            return;
        }

        for(var i = 1; i < PathSequece.Length; i++)
        {
            Gizmos.DrawLine(PathSequece[i - 1].position, PathSequece[i].position);
        }

        if(PathType == PathTypes.loop)
        {
            Gizmos.DrawLine(PathSequece[0].position, PathSequece[PathSequece.Length - 1].position);
        }


    }

    public IEnumerator<Transform> GetNextPathPoint()
    {
        if(PathSequece == null || PathSequece.Length < 1)
        {
            yield break;
        }

        while (true)
        {
            yield return PathSequece[movingto];

            if(PathSequece.Length == 1)
            {
                continue;
            }

            if(PathType == PathTypes.linear)
            {
                if(movingto <= 0)
                {
                    movementDirection = 1;
                }
                else if(movingto >= PathSequece.Length - 1)
                {
                    movementDirection = -1;
                }
            }

            movingto += movementDirection;

            if(PathType == PathTypes.loop)
            {
                if(movingto >= PathSequece.Length)
                {
                    movingto = 0;
                }
                if(movingto < 0)
                {
                    movingto = PathSequece.Length - 1;
                }
            }
        }
    }
}
