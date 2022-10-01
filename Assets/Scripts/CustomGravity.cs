using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class CustomGravity : MonoBehaviour
{
    private Vector3 A;
    private Vector3 B;

    public GameObject testPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        A = transform.position + transform.up * transform.lossyScale.x/2;
        B = transform.position + (transform.up * transform.lossyScale.x / 2) * -1;
    }

    private void FixedUpdate()
    {
        foreach (CustomGravityAgent agent in CustomGravityAgent.AgentList)
        {
            var gravCenter = CalculateClosestPoint(agent.transform.position);

            Vector3 gravDirectionInv = agent.transform.position - gravCenter;
            gravDirectionInv.Normalize();
            agent.transform.up = gravDirectionInv;
            agent.Gravity(0.0000001f * Time.fixedTime);
        }
    }

    public Vector3 CalculateClosestPoint(Vector3 point)
    {
        Vector3 AB = B - A;
        Vector3 AP = point - A;

        float lengthAB = AB.x * AB.x + AB.y * AB.y + AB.z * AB.z;
        float t = Vector3.Dot(AP, AB) / lengthAB;

        if (t < 0)
            t = 0;
        if (t > 1)
            t = 1;

        return A + t * AB;
    }
    
    
}

public class CustomGravityGizmoRenderer
{
    [DrawGizmo(GizmoType.Selected | GizmoType.Active | GizmoType.NonSelected)]
    static void DrawGizmoForCustomGravity(CustomGravity grav, GizmoType gizmoType)
    {
        Vector3 A = grav.transform.position + grav.transform.up * grav.transform.lossyScale.x;
        Vector3 B = grav.transform.position + grav.transform.up * -1 * grav.transform.lossyScale.x;
        
        Gizmos.DrawLine(A, B);
        Gizmos.DrawSphere(CalculateClosestPoint(grav.transform, grav.testPoint.transform.position), 1);
    }
    
    static public Vector3 CalculateClosestPoint(Transform Cylinder, Vector3 point)
    {
        Vector3 A = Cylinder.position + Cylinder.up * Cylinder.lossyScale.x;
        Vector3 B = Cylinder.position + Cylinder.up * -1 * Cylinder.lossyScale.x;
            
        Vector3 AB = B - A;
        Vector3 AP = point - A;

        float lengthAB = AB.x * AB.x + AB.y * AB.y + AB.z * AB.z;
        float t = Vector3.Dot(AP, AB) / lengthAB;

        if (t < 0)
            t = 0;
        if (t > 1)
            t = 1;

        return A + t * AB;
    }
}
