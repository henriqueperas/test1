using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboSystem : MonoBehaviour
{
    [SerializeField] ComboData[] combos;  // Combos disponíveis
    private List<AttackData> currentComboSequence = new List<AttackData>();
    private float lastAttackTime;

    void Update()
    {
        // Reseta o combo se o jogador demorar muito
        if (Time.time - lastAttackTime > 0.5f && currentComboSequence.Count > 0)
        {
            ResetCombo();
        }
    }

    public void RegisterAttack(AttackData attack)
    {
        currentComboSequence.Add(attack);
        lastAttackTime = Time.time;
        CheckCombos();
    }

    void CheckCombos()
    {
        foreach (ComboData combo in combos)
        {
            if (combo.sequence.Length != currentComboSequence.Count)
                continue;

            bool comboMatch = true;
            for (int i = 0; i < combo.sequence.Length; i++)
            {
                if (currentComboSequence[i] != combo.sequence[i])
                {
                    comboMatch = false;
                    break;
                }
            }

            if (comboMatch)
            {
                ExecuteFinisher(combo.finisher);
                ResetCombo();
                return;
            }
        }

        // Se a sequência for maior que o combo, reseta
        if (currentComboSequence.Count >= 3)
            ResetCombo();
    }

    void ExecuteFinisher(AttackData finisher)
    {/*
        // Aplica o golpe final (com dano multiplicado)
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
            attackPoint.position,
            finisher.range,
            enemyLayer
        );

        foreach (Collider2D enemy in hitEnemies)
        {
            float totalDamage = finisher.damage * finisher.damageMultiplier;
            enemy.GetComponent<HealthSystem>()?.TakeDamage(totalDamage);
        }

        Debug.Log("Combo completo! Dano: " + finisher.damage * finisher.damageMultiplier);*/
    }

    void ResetCombo()
    {
        currentComboSequence.Clear();
    }
}
