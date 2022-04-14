using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    [Header("Required Properties")]
    public Animator playerAnimator;
    public GameObject attackPoint;
    public LayerMask layers;

    [Header("Skill 1")]
    public Image skill1;
    public float cooldown;
    private float currentCooldown1;

    [Header("Skill 2")]
    public GameObject tornado;
    public Image skill2;
    public float cooldown2;
    private float currentCooldown2;

    [Header("Skill 3")]
    public SpriteRenderer playerSprite;
    public GameObject waterBall;
    public Image skill3;
    public float cooldown3;
    private float currentCooldown3;


    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            print("INSTANTIATED");
        } 
        currentCooldown1 = 0f;
    }


    private void Update()
    {
        Skill1();
        Skill2();
        Skill3();
    }


    private void LateUpdate()
    {
        
    }

    /** This skill enables user to have a strong damage to an enemy. */
    public void Skill1()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            if (currentCooldown1 <= 0f)
            {
                currentCooldown1 = cooldown;
                float attackRange = 3f;

                playerAnimator.SetBool("Skill_1", true);

                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRange, layers);

                foreach (Collider2D enemy in hitEnemies)
                {
                    if (enemy.gameObject.CompareTag("Boss"))
                        enemy.gameObject.GetComponent<Boss>().TakeDamage(30);
                    else
                        enemy.gameObject.GetComponent<Opponent>().TakeDamage(30);
                }
                skill1.fillAmount = 0;
            }
        }

        currentCooldown1 -= Time.deltaTime;
        skill1.fillAmount += (1f / cooldown) * Time.deltaTime;
    }


    /** This skills summons the Water Tornado. */
    public void Skill2()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (currentCooldown2 <= 0f)
            {
                GameObject tornadoObj = Instantiate(tornado);
                tornadoObj.transform.position = new Vector3(playerAnimator.gameObject.transform.position.x, tornadoObj.transform.position.y, 0);
                currentCooldown2 = cooldown2;
                skill2.fillAmount = 0;
            }
        }
        currentCooldown2 -= Time.deltaTime;
        skill2.fillAmount += (1f / cooldown2) * Time.deltaTime;
    }


    /** This skill summon the water ball **/
    public void Skill3()
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (currentCooldown3 <= 0f)
            {
                if (playerSprite.flipX)
                    waterBall.GetComponent<WaterBall>().isFlipped = true;
                
                StartCoroutine("WaterBallObj");
                skill3.fillAmount = 0;
                currentCooldown3 = cooldown3;
                skill3.fillAmount = 0f;
            }
        }
        currentCooldown3 -= Time.deltaTime;
        skill3.fillAmount += (1f / cooldown3) * Time.deltaTime;
    }


    IEnumerator WaterBallObj()
    {
        int loop = 3;

        while (loop > 0)
        {
            yield return new WaitForSeconds(.2f);

            Instantiate(waterBall);
            
            loop -= 1;
        }
        waterBall.GetComponent<WaterBall>().isFlipped = false;
    }
}
