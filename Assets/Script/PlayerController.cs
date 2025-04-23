using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] LayerMask groundLayer;

    [Header("Attack")]
    [SerializeField] Transform attackPoint;
    [SerializeField] LayerMask enemyLayer;
    public AttackData[] attacks;  // Array de golpes 
    private float[] nextAttackTime;  // Cooldowns individuais

    [Header("Enemy")]
    [SerializeField] GameObject enemy;

    Rigidbody2D rb;
    Vector2 moveInput;
    bool isGrounded;
    Vector2 attackRangeInspec;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        nextAttackTime = new float[attacks.Length];
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.525f, groundLayer);
        // anima��es no ch�o

        if(enemy.transform.position.x > gameObject.transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            attackPoint.position = new Vector2(gameObject.transform.position.x + 2f, gameObject.transform.position.y + 1.5f);
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
            attackPoint.position = new Vector2(gameObject.transform.position.x - 2f, gameObject.transform.position.y + 1.5f);
        }

    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        // anima��o de movimento
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

    }

    public void OnJump(InputAction.CallbackContext context)
    {

        if (context.performed && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            // anima��o do pulo
        }

    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Verifica qual golpe foi solicitado
            for (int i = 0; i < attacks.Length; i++)
            {
                if (context.action.name == attacks[i].inputKey.ToString())
                {
                    TryAttack(i);
                    break;
                }
            }
        }
    }

    public void OnSquat(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            gameObject.transform.localScale = new Vector2(1f, 0.5f);
        }
        else
        {
            gameObject.transform.localScale = new Vector2(1f, 1f);
        }

        
    }
    void TryAttack(int attackIndex)
    {
        attackRangeInspec = attacks[attackIndex].range;
        if (Time.time >= nextAttackTime[attackIndex])
        {
            AttackData attack = attacks[attackIndex];
            nextAttackTime[attackIndex] = Time.time + attack.cooldown;

            // Anima��o de ataque

            // L�gica de hit

            GetComponent<ComboSystem>().RegisterAttack(attack);  // Registra no combo

            // L�gica de hit (opcional: remover se o combo s� ativar no finisher)
            Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(
                attackPoint.position,
                attack.range,
                enemyLayer
            );
            
            foreach (Collider2D enemy in hitEnemies)
            {
                // Colis�o com inimigo, acerto de golpe
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
            
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPoint.position, attackRangeInspec);
            
    }
}
