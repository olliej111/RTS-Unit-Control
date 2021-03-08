using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProduceUnits : MonoBehaviour
{
    public Transform tank;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void instantiateTank()
    {
        Instantiate(tank, transform.position+transform.forward, Quaternion.identity);
    }
}
