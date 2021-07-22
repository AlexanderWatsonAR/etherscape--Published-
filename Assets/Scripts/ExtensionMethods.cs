using UnityEngine;
using System.Linq;
using System.Collections;

public static class ExtensionMethods
{
    // Mesh Extentions.
    public static Mesh CopyMesh(this Mesh mesh)
    {
        Mesh duplicateMesh = new Mesh
        {
            vertices = mesh.vertices,
            triangles = mesh.triangles,
            uv = mesh.uv,
            normals = mesh.normals,
            colors = mesh.colors,
            tangents = mesh.tangents
        };

        return duplicateMesh;
    }

    public static bool EqualMesh(this Mesh mesh, Mesh comparison)
    {
        if(!mesh.vertices.SequenceEqual(comparison.vertices))
        {
            return false;
        }
        if (!mesh.triangles.SequenceEqual(comparison.triangles))
        {
            return false;
        }
        if (!mesh.uv.SequenceEqual(comparison.uv))
        {
            return false;
        }
        if (!mesh.normals.SequenceEqual(comparison.normals))
        {
            return false;
        }
        if (!mesh.colors.SequenceEqual(comparison.colors))
        {
            return false;
        }
        if (!mesh.tangents.SequenceEqual(comparison.tangents))
        {
            return false;
        }

        return true;
    }

    public static FixedJoint CopyJoint(this FixedJoint joint)
    {

        FixedJoint duplicateJoint = new FixedJoint
        {
            //anchor = joint.anchor,
            autoConfigureConnectedAnchor = joint.autoConfigureConnectedAnchor,
            axis = joint.axis,
            breakForce = joint.breakForce,
            breakTorque = joint.breakTorque,
            connectedAnchor = joint.connectedAnchor,
            connectedBody = joint.connectedBody,
            connectedMassScale = joint.connectedMassScale,
            enableCollision = joint.enableCollision
        };

        return duplicateJoint;
    }

    // returns true if the x component is less than -range or greater that range.
    public static bool IsObjectMovingOnTheXAxis(this Vector3 velocity, float range)
    {
        bool isObjectMoving = false;
        if (velocity.x < -range || velocity.x > range)
        {
            isObjectMoving = true;
        }
        return isObjectMoving;
    }

    // returns true if the y component is less than -range or greater that range.
    public static bool IsObjectMovingOnTheYAxis(this Vector3 velocity, float range)
    {
        bool isObjectMoving = false;
        if (velocity.y < -range || velocity.y > range)
        {
            isObjectMoving = true;
        }
        return isObjectMoving;
    }

    // returns true if the z component is less than -range or greater that range.
    public static bool IsObjectMovingOnTheZAxis(this Vector3 velocity, float range)
    {
        bool isObjectMoving = false;
        if (velocity.z < -range || velocity.z > range)
        {
            isObjectMoving = true;
        }
        return isObjectMoving;
    }
}
