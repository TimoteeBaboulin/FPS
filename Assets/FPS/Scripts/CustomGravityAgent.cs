using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGravityAgent : MonoBehaviour
{
    public static List<CustomGravityAgent> AgentList;
    private float _GravSpeed;

    void Start()
    {
        _GravSpeed = 0;
        if (AgentList == null)
            AgentList = new List<CustomGravityAgent>();
        AgentList.Add(this);
    }

    public void Gravity(float gravity)
    {
        transform.up.Normalize();
        RaycastHit hit;
        
        Ray ray = new Ray(transform.position, transform.up * -1);
        if (!Physics.Raycast(ray, out hit, 20, LayerMask.GetMask("Terrain")))
        {
            transform.position = transform.position + (transform.up * -1 * gravity);
            return;
        }

        transform.position = hit.point;
    }
    
    private void OnDisable()
    {
        AgentList.Remove(this);
    }

    private void OnDestroy()
    {
        AgentList.Remove(this);
    }

    private void OnDrawGizmos()
    {
        Vector3 Start = transform.position;
        Vector3 End = Start + -10 * transform.up;
        Debug.DrawLine(Start, End, Color.red, duration: 100, false);
    }
}
