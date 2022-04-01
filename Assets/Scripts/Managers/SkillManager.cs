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
    }

    // Sword Swiper
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
                    enemy.gameObject.GetComponent<Opponent>().TakeDamage(30);
                }
                skill1.fillAmount = 0;
            }
        }

        currentCooldown1 -= Time.deltaTime;
        skill1.fillAmount += (1f / cooldown) * Time.deltaTime;
    }

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
}
