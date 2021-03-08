using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    private Vector3 vectorToTarget;
    private Vector3 planeNormal;
    private Vector3 response;
    public Transform currentTarget;
    private void Start()
    {
        
    }
    private void Update()
    {
        if (currentTarget != null)
        {
            //Debug.Log("I have " + currentTarget + " in my sights");
            vectorToTarget = currentTarget.position - transform.position;
            planeNormal = transform.parent.transform.up;
            response = Vector3.ProjectOnPlane(vectorToTarget, planeNormal);
            transform.rotation = Quaternion.LookRotation(response, planeNormal);
        }
    }
    public void SetTarget(Transform closestEnemy)
    {
        currentTarget = closestEnemy;

    }
    public void RemoveTarget()
    {
        currentTarget = null;
    }
}
