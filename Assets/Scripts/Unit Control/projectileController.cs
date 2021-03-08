using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileController : MonoBehaviour
{
    public float destroyTime;
    public Transform hitLocation;
    public ParticleSystem impactExplosion;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, contact.normal);
        Vector3 position = contact.point + (Vector3.up * 0.1f);
        ParticleSystem thisExplosion = Instantiate(impactExplosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
