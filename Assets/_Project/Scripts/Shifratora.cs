using System.Xml;
using UnityEngine;
using _Project.Scripts.Core;

public class Shifratora : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IDamageable>() != null)
        {
           IDamageable damageable = other.GetComponent<IDamageable>();
           damageable.TakeDamage(40);
        }
    }
}
