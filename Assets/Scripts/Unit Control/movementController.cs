using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class movementController : MonoBehaviour
{
    private NavMeshAgent m_agent;
    private UnitController m_Controller;
    // Start is called before the first frame update
    void Start()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_Controller = GetComponent<UnitController>();
        m_agent.speed = m_Controller._mStats.speed;
        m_agent.acceleration = m_Controller._mStats.acceleration;
        m_agent.angularSpeed = m_Controller._mStats.angularAcceleration;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void setDestination(Vector3 destination)
    {
        m_agent.destination = destination;
    }
}
