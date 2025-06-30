using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CollisionExtentionMethods
{
    public static Vector3 GetCollisionNormal(this Collision collision)
    {
        List<Vector3> collisionNormals = new List<Vector3>();
        collisionNormals.AddRange(
            from contactPoint in collision.contacts
            select contactPoint.normal
            );

        Vector3 sum = Vector3.zero;
        foreach (Vector3 normal in collisionNormals)
            sum += normal;

        return sum / collisionNormals.Count;
    }
}
