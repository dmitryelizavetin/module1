using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum State
    {
        Idle,
        RunningToEnemy,
        RunningFromEnemy,
        ZRunningToEnemy,
        BeginAttack,
        ZBeginAttack,
        Attack,
        BeginShoot,
        Shoot,
        Died,
    }

    public enum Weapon
    {
        Pistol,
        Bat,
        Fist,
    }

    public float runSpeed;
    public float distanceFromEnemy;
    public Transform target;
    Character currenttarget;
    public Weapon weapon;
    Animator animator;
    Vector3 originalPosition;
    Quaternion originalRotation;
    State state = State.Idle;
    bool dead;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        dead = false;
        currenttarget = target.GetComponent<Character>();
    }

    [ContextMenu("Attack")]
    void AttackEnemy()
    {
        if (currenttarget.state == State.Died)
            return;

        switch (weapon) {
            case Weapon.Bat:
                state = State.RunningToEnemy;
                break;

            case Weapon.Pistol:
                state = State.BeginShoot;
                break;

            case Weapon.Fist:
                state = State.ZRunningToEnemy;
                break;
        }
    }

    public void SetState(State newState)
    {
        state = newState;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        switch (state) {
            case State.Idle:
                animator.SetFloat("speed", 0.0f);
                transform.rotation = originalRotation;
                break;

            case State.RunningToEnemy:
                animator.SetFloat("speed", runSpeed);
                if (RunTowards(target.position, distanceFromEnemy))   
                    state = State.BeginAttack;
                break;

            case State.RunningFromEnemy:
                animator.SetFloat("speed", runSpeed);
                if (RunTowards(originalPosition, 0.0f))
                    state = State.Idle;
                break;

            case State.ZRunningToEnemy:
                animator.SetFloat("speed", runSpeed);
                if (RunTowards(target.position, distanceFromEnemy))
                    state = State.ZBeginAttack;
                break;

            case State.BeginAttack:
                animator.SetFloat("speed", 0.0f);
                animator.SetTrigger("attack");
                state = State.Attack;
                break;

            case State.ZBeginAttack:
                animator.SetFloat("speed", 0.0f);
                animator.SetTrigger("zattack");
                state = State.Attack;
                break;

            case State.Attack:
                animator.SetFloat("speed", 0.0f);
                break;

            case State.BeginShoot:
                animator.SetFloat("speed", 0.0f);
                animator.SetTrigger("shoot");
                state = State.Shoot;
                break;

            case State.Shoot:
                animator.SetFloat("speed", 0.0f);
                break;

            case State.Died:
                break;
        }
    }

    public void Kill()
    {
        if (!dead)
        {
            dead = true;
            animator.SetTrigger("died");
            SetState(State.Died);
        }
    }

    public void KillTarget()
    {
        currenttarget.Kill();
    }

    bool RunTowards(Vector3 targetPosition, float distanceFromTarget)
    {
        Vector3 distance = targetPosition - transform.position;
        Vector3 direction = distance.normalized;
        transform.rotation = Quaternion.LookRotation(direction);

        targetPosition -= direction * distanceFromTarget;
        distance = (targetPosition - transform.position);

        Vector3 vector = direction * runSpeed;
        if (vector.magnitude < distance.magnitude) {
            transform.position += vector;
            return false;
        }

        transform.position = targetPosition;
        return true;
    }
}
