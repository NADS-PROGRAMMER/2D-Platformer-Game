using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : Opponent
{
    [SerializeField] private GameObject attackPoint;
    [SerializeField] private GameObject laser;
    GameObject bullet;
    private void Start()
    {
        animator.SetBool(IS_ATTACKING, false);
        animator.SetBool(IS_WALKING, true);
        localScaleX = transform.localScale.x;
        xPositionOfRay = lineOfSight.transform.localPosition.x;
        distanceOfRay = lengthOfRay;
  
    }

    private void FixedUpdate()
    {
        this.Attack();
    }

    public override void Move()
    {
        base.Move();
    }

    
    void FireBullet()
    {
        bullet = Instantiate(laser);
        bullet.transform.position = attackPoint.transform.position;

        if (toFollow.position.x < transform.position.x)
        {
            bullet.GetComponent<SpriteRenderer>().flipX = true;
            bullet.GetComponent<Bullet>().isFlipped = true;
        }
        else
        {
            bullet.GetComponent<SpriteRenderer>().flipX = false;
            bullet.GetComponent<Bullet>().isFlipped = false;
        }
    }


    public override void Attack()
    {
        print(distanceOfRay);

        sight = Physics2D.Raycast(lineOfSight.transform.position, new Vector2(lengthOfRay, 0), distanceOfRay, layers);

        if (sight)
        {
            animator.SetBool(IS_ATTACKING, true);
            animator.SetBool(IS_WALKING, false);
            FireBullet();
            return;
        }
        else
        {
            animator.SetBool(IS_ATTACKING, false);
            animator.SetBool(IS_WALKING, true);
        }
        Move();
    }
}
