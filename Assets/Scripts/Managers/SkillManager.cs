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
    public int cooldown;
    private float currentCooldown1;

    [Header("Skill 2")]
    public Image skill2;
    public int cooldown2;
    public float currentCooldown2;
    public GameObject[] spawner;
    public GameObject power;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            print("INSTANTIATED");
        } 
        currentCooldown1 = 0f;

        StartCoroutine(Skill2());
    }

    private void Update()
    {
        Skill1();

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

    // Sprit of Death
    public IEnumerator Skill2()
    {
        if (Input.GetKey(KeyCode.W))
        {
            print("SKILL 2");

            yield return new WaitForSeconds(cooldown2);
        }
    }
}
