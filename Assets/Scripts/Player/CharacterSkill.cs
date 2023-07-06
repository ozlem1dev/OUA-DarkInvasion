using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CharacterSkill : MonoBehaviour
{
    public Transform skillSpawnPoint;
    public GameObject skillPrefab;
    public float skillSpeed = 10;
    private bool canUseSkill = true;
    public float skillCooldown = 3f;
    private float skillTimer = 0f;

    public Animator _animator;

    /*private void Awake()
    {
        _animator = GetComponent<Animator>();
    }*/


    void Update()
    {
        
        if (!canUseSkill)
        {
            // Cooldown süresi boyunca bekleyin
            skillTimer += Time.deltaTime;
            if (skillTimer >= skillCooldown)
            {
                // Cooldown süresi tamamlandý, skill kullanima açýk hale gelir
                canUseSkill = true;
                skillTimer = 0f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && canUseSkill)
        {
            Debug.Log("Q tusuna basildi");

            if (GetComponentInParent<CharacterMana>().currentMana > 30f)
            {
                _animator.SetBool("Grenade", true);
                
                gameObject.GetComponentInParent<CharacterMana>().Skill(30f);

                var skill = Instantiate(skillPrefab, skillSpawnPoint.position, skillSpawnPoint.rotation);
                skill.GetComponent<Rigidbody>().velocity = skillSpawnPoint.forward * skillSpeed;
                canUseSkill = false;

                StartCoroutine(ResetThrowingBombCoroutine());
            }
            else
            {
                Debug.Log("Mana yetersizke");
            }

        }
        
    }

    IEnumerator ResetThrowingBombCoroutine()
    {
        yield return new WaitForSeconds(2f); // 1 saniye bekle

        _animator.SetBool("Grenade", false); // Bomba atma durumunu false yap
    }
}