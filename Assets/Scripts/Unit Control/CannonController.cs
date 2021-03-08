using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    private UnitController m_Controller;
    private IEnumerator shootProjectile;
    private Transform currentTarget;
    private Vector3 centerPosition;
    private float firingSolutionYPos;
    private float distance;

    public Rigidbody projectilePrefab;
    private float fireDirection;

    private float chosenAngle;
    private float timeToTarget;
    private Vector3 predictedPosition;
    void Start()
    {
        m_Controller = transform.parent.GetComponent<UnitController>();
        shootProjectile = fireProjectile();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(predictedPosition, Vector3.up);
    }
    private IEnumerator fireProjectile()
    {
        //Debug.Log("I have targeted " + currentTarget);
        while (currentTarget != null)
        {
            calculatePredPos();
            calculateAngle(predictedPosition);
            float initVelX = m_Controller._mStats.projVelocity * (Mathf.Cos(chosenAngle));
            float initVelY = m_Controller._mStats.projVelocity * (Mathf.Sin(chosenAngle));
            Vector3 velocityDirection = new Vector3(0, initVelY, initVelX);
            Rigidbody thisProjectile;
            thisProjectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            thisProjectile.AddRelativeForce(velocityDirection, ForceMode.VelocityChange);
            yield return new WaitForSeconds(m_Controller._mStats.fireRate);
        }
    }
    void calculateAngle(Vector3 position)
    {
        float posX = transform.position.x + ((position.x - transform.position.x) / 2);
        float posZ = transform.position.z + ((position.z - transform.position.z) / 2);
        float xDist = Mathf.Abs(transform.position.x - position.x);
        float zDist = Mathf.Abs(transform.position.z - position.z);

        distance = Mathf.Sqrt(Mathf.Pow(xDist, 2) + Mathf.Pow(zDist, 2));
        float elevation = 0;
        float vel2 = Mathf.Pow(m_Controller._mStats.projVelocity, 2);
        float vel4 = Mathf.Pow(m_Controller._mStats.projVelocity, 4);

        float underSqrt = (-Physics.gravity.y) * ((-Physics.gravity.y * (Mathf.Pow(distance, 2))) + (2 * -elevation * vel2));
        underSqrt = vel4 - underSqrt;

        float sqRoot = Mathf.Sqrt(underSqrt);
        float firstRoot = vel2 + sqRoot;
        float secondRoot = vel2 - sqRoot;
        float denominator = -Physics.gravity.y * distance;
        float theSolution = firstRoot / denominator;
        float firstAngle = Mathf.Atan(theSolution);
        float secondAngle = Mathf.Atan(secondRoot / denominator);
        if (m_Controller._mStats.trajectory)
        {
            chosenAngle = Mathf.Max(firstAngle, secondAngle);
        }
        else
        {
            chosenAngle = Mathf.Min(firstAngle, secondAngle);
        }
        timeToTarget = distance / (m_Controller._mStats.projVelocity * Mathf.Cos(chosenAngle));
        firingSolutionYPos = (distance / 2) * Mathf.Tan(chosenAngle) + transform.position.y;
        centerPosition = new Vector3(posX, firingSolutionYPos, posZ);
    }
    void calculatePredPos()
    {
        //Vector3 targetVelocity = currentTarget.GetComponent<targetMovementController>().returnVelocity();
        //float targetSpeed = currentTarget.GetComponent<targetMovementController>().returnSpeed();
        Vector3 targetVelocity = Vector3.zero;
        //float targetSpeed = 0;
        float projSpeed = m_Controller._mStats.projVelocity * Mathf.Cos(chosenAngle);
        float a = Mathf.Pow(targetVelocity.x, 2) + Mathf.Pow(targetVelocity.z, 2) - Mathf.Pow(projSpeed, 2);
        float bOne = targetVelocity.x * (currentTarget.position.x - transform.position.x);
        float bTwo = targetVelocity.z * (currentTarget.position.z - transform.position.z);
        float b = 2 * (bOne + bTwo);
        float c = Mathf.Pow(currentTarget.position.z - transform.position.z, 2) + Mathf.Pow(currentTarget.position.x - transform.position.x, 2);
        float discriminant = Mathf.Pow(b, 2) - (4 * a * c);
        if (discriminant < 0)
        {
        }
        else
        {
            float timeOne = (-b + Mathf.Sqrt(discriminant)) / (2 * a);
            float timeTwo = (-b - Mathf.Sqrt(discriminant)) / (2 * a);
            float timeSelected = Mathf.Max(timeOne, timeTwo);
            predictedPosition = new Vector3(timeSelected * targetVelocity.x + currentTarget.position.x,
                                            currentTarget.position.y,
                                                timeSelected * targetVelocity.z + currentTarget.position.z);
        }
    }
    public void SetTarget(Transform closestEnemy)
    {
            Debug.Log("and starting a new coroutine");
            currentTarget = closestEnemy;
            StartCoroutine(shootProjectile);
        
    }
    public void RemoveTarget()
    {
        Debug.Log("so I am stopping the coroutine");
        StopCoroutine(shootProjectile);
        currentTarget = null;
    }
}
