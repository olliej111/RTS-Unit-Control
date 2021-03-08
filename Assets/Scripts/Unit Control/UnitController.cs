using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public UnitStats _mStats;
    private IEnumerator coroutine;
    private Transform closestEnemy;
    private float currentClosestDistance;
    private Transform oldClosestTarget = null;
    private void Awake()
    {
        coroutine = detectNearestEnemy();
        StartCoroutine(coroutine);
        currentClosestDistance = _mStats.range*_mStats.range;
    }
    private IEnumerator detectNearestEnemy()
    {
        while (true)
        {
            
            currentClosestDistance = _mStats.range * _mStats.range;
            yield return new WaitForSeconds(0.2f);
            //Debug.Log("checking for enemies");
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _mStats.range, _mStats._mLayerMask);
            //Debug.Log("I have detected an enemy");
            if (hitColliders.Length <= 0)
            {
                //Debug.Log("I am removing the target");
                gameObject.BroadcastMessage("RemoveTarget");
                currentClosestDistance = _mStats.range * _mStats.range;
            }
            foreach (var hitCollider in hitColliders)
            {
                Debug.Log("I am iterating through all detected enemy, currently at " + hitCollider.gameObject);
                float distanceTargetMe = (transform.position - hitCollider.transform.position).sqrMagnitude;
                Debug.Log("the distance sqrd is " + distanceTargetMe);
                Debug.Log("comparing with the old closest distance which was " + closestEnemy + " at " + currentClosestDistance);
                if (distanceTargetMe < currentClosestDistance)
                {
                    Debug.Log("the new target is closer than the old");
                    //Debug.Log("this target is closer");
                    currentClosestDistance = distanceTargetMe;
                    closestEnemy = hitCollider.transform;
                    Debug.Log("the new current closest dist target is " + currentClosestDistance);

                }

            }

            if (oldClosestTarget != closestEnemy)
            {
                Debug.Log("the target was the same old gameObject");
                oldClosestTarget = closestEnemy;
                gameObject.BroadcastMessage("RemoveTarget");
                gameObject.BroadcastMessage("SetTarget", closestEnemy);
            }
        }
    }
}
