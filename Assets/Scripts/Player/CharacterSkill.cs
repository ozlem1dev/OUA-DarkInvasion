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

    void Update()
    {
        if (!canUseSkill)
        {
            // Cooldown süresi boyunca bekleyin
            skillTimer += Time.deltaTime;
            if (skillTimer >= skillCooldown)
            {
                // Cooldown süresi tamamlandý, skill kullanýma açýk hale gelir
                canUseSkill = true;
                skillTimer = 0f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && canUseSkill)
        {
            Debug.Log("Q tuþuna basýldý");

            if (GetComponentInParent<CharacterMana>().currentMana > 30f)
            {

                gameObject.GetComponentInParent<CharacterMana>().Skill(30f);

                var skill = Instantiate(skillPrefab, skillSpawnPoint.position, skillSpawnPoint.rotation);
                skill.GetComponent<Rigidbody>().velocity = skillSpawnPoint.forward * skillSpeed;
                canUseSkill = false;
            }
            else
            {
                Debug.Log("Mana yetersizke");
            }
        }
    }
}

