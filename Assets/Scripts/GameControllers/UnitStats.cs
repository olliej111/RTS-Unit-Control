using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="NewUnitStats", menuName = "Stats/UnitStats")]
public class UnitStats : ScriptableObject
{
    public new string name;
    public float speed;
    public float acceleration;
    public float angularAcceleration;
    
    public float hitPoints;
    public float armour;

    public float turretSpeed;
    public float fireRate;
    public float reloadTime;
    public float range;
    public float projVelocity;
    public bool trajectory;
    public float damage;
    public float armourPenetration;
    public string damageType;

    public string targetPriority;
    public string targetSecondary;

    public LayerMask _mLayerMask;
}
