using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity
{
    public enum State
    {
        Idle,
        Chasing,
        Attaking
    };

    State currentState;

    NavMeshAgent pathfinder;
    Transform target;
    LivingEntity targetEntity;
    [Range(0,1)] public float timeDistance = 1.5f;
    [Range(0,1)] float timeBetweenAttack = 1;
    [Range(0,5)] public float damage = 1;

    Material skinMaterial;
    Color originalColor;

    float nextAttackTime;
    float myCollisionRadius;
    float targetCollisionRadius;

    bool hasTarget;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        pathfinder = GetComponent<NavMeshAgent>();
        skinMaterial = GetComponent<Renderer>().material;
        originalColor = skinMaterial.color;

        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            currentState = State.Chasing;
            hasTarget = true;

            target = GameObject.FindGameObjectWithTag("Player").transform;
            targetEntity = target.GetComponent<LivingEntity>();
            targetEntity.OnDead += OnTargetDead;
            myCollisionRadius = GetComponent<CapsuleCollider>().radius;
            targetCollisionRadius = target.GetComponent<SphereCollider>().radius;
            StartCoroutine(UpdatePath());
        }
    }

    void Update()
    {
        if (hasTarget)
        {
            if (Time.time > nextAttackTime)
            {
                float sqrDtsToTarget = (target.position - transform.position).sqrMagnitude;

                if (sqrDtsToTarget < Mathf.Pow(timeDistance + myCollisionRadius + targetCollisionRadius, 2))
                {
                    nextAttackTime = Time.time + timeBetweenAttack;
                    StartCoroutine(Attack());
                }
            }
        }
    }

    void OnTargetDead()
    {
        currentState = State.Idle;
        hasTarget = false;
    }

    IEnumerator Attack()
    {
        currentState = State.Attaking;
        pathfinder.enabled = false;

        Vector3 originalPosition = transform.position;
        Vector3 dirToTaget = (target.position - transform.position).normalized;
        Vector3 attackPostion = target.position - dirToTaget * (myCollisionRadius + targetCollisionRadius);

        float attackSpeed = 3;
        float percent = 0;
        skinMaterial.color = Color.blue;
        bool hasAppliedDamage = false;

        while(percent <= 1)
        {
            if(percent >= .5f && !hasAppliedDamage)
            {
                hasAppliedDamage = true;
                targetEntity.TakeDamage(damage);
            }

            percent += Time.deltaTime * attackSpeed;
            // ham noi suy y = 4(-x^2 + x) de tham chieu tao duong cong doi xung khi chuyen tu trang thai khoi dau (0) sang trang thai tan cong (1) roi lai chuyen ve trang thai khoi dau (0)
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector3.Lerp(originalPosition, attackPostion, interpolation);
            yield return null;
        }

        skinMaterial.color = originalColor;
        currentState = State.Chasing;
        pathfinder.enabled = true;
    }

    IEnumerator UpdatePath()
    {
        float refreshRate = timeDistance;

        while(hasTarget)
        {
            if(currentState == State.Chasing)
            {
                Vector3 dirToTaget = (target.position - transform.position).normalized;
                Vector3 targetPosition = target.position - dirToTaget * (myCollisionRadius + targetCollisionRadius + timeDistance/2);
                if (!dead)
                {
                    pathfinder.SetDestination(target.position);
                }
            }
            yield return new WaitForSeconds(refreshRate);
        }
    }
}
