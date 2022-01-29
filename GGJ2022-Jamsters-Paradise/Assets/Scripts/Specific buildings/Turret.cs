using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Turret : Building
{
    enum TargetingType
    {
        NULL,
        CLOSEST,
        FURTHEST,
        CLOSEST_TO_END
    }

    public bool isSingleTarget = true;

    private List<Enemy> targetList = new List<Enemy>();
    private Transform Target;
    private Enemy currentTarget = null;
    private float nextTimeToFire = 0f;

    [SerializeField] private float Range = 1f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private TargetingType targeting = TargetingType.NULL;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (targetList.Count > 0 && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Attack();
        }

        //Actions like pointing turret if it has to point at specific target
        if (isSingleTarget)
        {
            if (currentTarget != null)
            {
            }
        }
        //upadate events for turrets that just apply AOE/ don't have to change orientation based on target
        else
        {

        }


        SetCurrentTarget();
        base.Update();
    }



    private void SetCurrentTarget()
    {
        if (targetList.Count == 0)
        {
            return;
        }
    }

    private void Attack()
    {

        if (isSingleTarget)
        {
            currentTarget.TakeDamage(damage);
        }
        else
        {
            foreach (var target in targetList)
            {
                target.TakeDamage(damage);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            targetList.Add(other.GetComponent<Enemy>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (targetList.Contains(enemy))
            {
                targetList.Remove(enemy);
            }
        }
    }
}
