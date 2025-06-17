
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed; // Speed of the enemy movement

    private Rigidbody2D _rigidbody;
    private UnityEngine.Vector2 _targetDirection; // Direction of the enemy movement
    private bool facingRight = true; // Flag to check if the player is facing right
    private Transform _player;
    public Vector2 DirectionToPlayer { get; private set; }
    private float attackDamage = 10f; // Damage dealt to the player on collision
    [SerializeField] private float attackSpeed = 1f; // Range within which the enemy can attack the player
    private float canAttack; // Timer to control attack frequency

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _player = PlayerController.instance.transform; 
        canAttack = attackSpeed;
    }

    private void FixedUpdate()
    {
        _targetDirection = DirectionToPlayer; // Get the direction to the player
        if (_targetDirection.x > 0 && !facingRight)
            Flip();
        else if (_targetDirection.x < 0 && facingRight)
            Flip();
        _rigidbody.linearVelocity = _targetDirection * _speed; // Set the linear velocity based on the target direction and speed
    }
    private void Update()
    {
        Vector2 enemyToPlayerVector = _player.position - transform.position; // Calculate the vector from the enemy to the player
        DirectionToPlayer = enemyToPlayerVector.normalized; // Normalize the vector to get the direction

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (attackSpeed <= canAttack)
            {
                PlayerController.instance.updateHealth(-attackDamage); // If the enemy collides with the player, reduce the player's health
                canAttack = 0; // Reset the attack timer
            }
            else
            {
                canAttack += Time.deltaTime;
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        UnityEngine.Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
