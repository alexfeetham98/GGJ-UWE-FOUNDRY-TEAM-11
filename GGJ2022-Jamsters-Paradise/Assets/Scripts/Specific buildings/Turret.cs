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
        base.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            targetList.Add(other.GetComponent<Enemy>());
        }
    }

    private void SetCurrentTarget()
    {

    }

    private void Attack()
    {

        if (isSingleTarget)
        {

        }
        else
        {

        }
    }
}
