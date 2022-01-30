using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Turret : Building, IInteractable
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
    public Enemy currentTarget = null;
    private float nextTimeToFire = 0f;
    private Quaternion lookRotation;
    private Vector3 direction;
    private SphereCollider rangeCollider;

    [SerializeField] private float Range = 1f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float damage = 1f;
    [SerializeField] private TargetingType targeting = TargetingType.NULL;


    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        rangeCollider = GetComponent<SphereCollider>();
        rangeCollider.radius = Range;
        Enemy.OnDeath += ClearDeadEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        {
            return;
        }

        isIdle = (currentTarget == null) ? true : false;
        //Actions like pointing turret if it has to point at specific target
        if (isSingleTarget)
        {
            if (currentTarget != null)
            {
                direction = (currentTarget.transform.position - buildingHead.transform.position).normalized;
                lookRotation = Quaternion.LookRotation(direction);
                 buildingHead.transform.rotation = lookRotation;
            }
        }
        //upadate events for turrets that just apply AOE/ don't have to change orientation based on target
        else
        {

        }


        SetCurrentTarget();

        if (targetList.Count > 0 && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Attack();
        }
        base.Update();
    }



    private void SetCurrentTarget()
    {
        float distance;
        if (targetList.Count == 0)
        {
            return;
        }

        switch (targeting)
        {
            case TargetingType.NULL:
                break;
            case TargetingType.CLOSEST:
                {
                    float closestDistance = float.MaxValue;
                    foreach (var target in targetList)
                    {
                        distance = Vector3.Distance(transform.position, target.transform.position);
                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            currentTarget = target;
                        }
                    }
                }
                break;
            case TargetingType.FURTHEST:
                {
                    float furthestDistance = float.MinValue;
                    foreach (var target in targetList)
                    {
                        distance = Vector3.Distance(transform.position, target.transform.position);
                        if (distance > furthestDistance)
                        {
                            furthestDistance = distance;
                            currentTarget = target;
                        }
                    }
                }

                break;
            case TargetingType.CLOSEST_TO_END:
                {

                }

                break;
            default:
                break;
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
            if (currentTarget == enemy)
            {
                currentTarget = null;
            }
        }
    }

    public void ClearDeadEnemy(Enemy enemy)
    {
        if (targetList.Contains(enemy))
        {
            targetList.Remove(enemy);
        }
        if (currentTarget == enemy)
        {
            currentTarget = null;
        }
    }
}
