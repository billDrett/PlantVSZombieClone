using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [SerializeField] float health;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MakeDamage(float damagePoints)
    {
        Debug.Log("Took Damage");
        health -= damagePoints;

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
