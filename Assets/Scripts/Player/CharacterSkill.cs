using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CharacterSkill : MonoBehaviour
{
    public Transform skillSpawnPoint;
    public GameObject skillPrefab;

    public float skillSpeed = 20;
    public float reloadMana;
    public bool canUseSkill = true;
    public bool isManaRefilling = false; // Yeniden dolum islemi devam ediyor mu?

    public Animator _animator;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (canUseSkill)
            {
                StartCoroutine(ThrowBombWithDelay(2f)); // 2 saniye gecikmeyle bomba atma islemini baslatir
                canUseSkill = false;

                // Mana sifirlanir ve asamali olarak yenilenmeye baslar
                GetComponentInParent<CharacterMana>().currentMana = 0f;
                StartCoroutine(RefillManaOverTime(0.5f, reloadMana)); // 5 saniyede 20 birim mana yenilenmesi
            }
            else
            {
            }
        }
    }

    IEnumerator ThrowBombWithDelay(float delay)
    {
        _animator.SetBool("Grenade", true);

        yield return new WaitForSeconds(delay);

        var skill = Instantiate(skillPrefab, skillSpawnPoint.position, skillSpawnPoint.rotation);
        skill.GetComponent<Rigidbody>().velocity = skillSpawnPoint.forward * skillSpeed;

        _animator.SetBool("Grenade", false);
    }


    IEnumerator RefillManaOverTime(float refillDuration, float refillAmount)
    {
        isManaRefilling = true;

        while (isManaRefilling)
        {
            yield return new WaitForSeconds(refillDuration);

            GetComponentInParent<CharacterMana>().currentMana += refillAmount;

            if (GetComponentInParent<CharacterMana>().currentMana >= GetComponentInParent<CharacterMana>().maxMana)
            {
                GetComponentInParent<CharacterMana>().currentMana = GetComponentInParent<CharacterMana>().maxMana;
                isManaRefilling = false;

                // Mana tamamen doldugunda kontrol edilir ve kullanima acik hale gelir
                canUseSkill = true;
            }
        }
    }
}