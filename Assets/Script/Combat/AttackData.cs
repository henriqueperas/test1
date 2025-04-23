using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAttack", menuName = "Combat/Attack Data")]
public class AttackData : ScriptableObject
{
    [Header("Configuration")]
    public float damage = 10f;
    public Vector2 range;
    public float cooldown = 0.5f;
    public KeyCode inputKey;  // Tecla de input 

}
