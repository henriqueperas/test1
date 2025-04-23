using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCombo", menuName = "Combat/Combo Data")]
public class ComboData : ScriptableObject
{
    [Header("Sequência")]
    public AttackData[] sequence;  // Ex: [Soco, Soco, Chute]

    [Header("Golpe Final")]
    public AttackData finisher;    // Ataque especial ao completar o combo
    public float damageMultiplier = 1.5f;  // Dano extra

    [Header("Timing")]
    public float maxDelayBetweenAttacks = 0.5f;  // Tempo máximo entre golpes

}
