using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float health = 0f;
    [SerializeField] private float maxHealth = 100f; // Maximum health of the player

    public HealthBar healthBar;
    public static PlayerController instance;

    private Animator animator; // Reference to the Animator component for animations

    private void Awake()
    {
        instance = this; // Ensure there is only one instance of Player
    }


    void Start()
    {
        animator = GetComponent<Animator>(); // Get the Animator component attached to the player
        animator.SetBool("Dead", false); // Initialize the animator's speed parameter
        health = maxHealth; // Initialize health to maximum at the start
        healthBar.SetMaxHealth(maxHealth); // Set the maximum health in the health bar

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateHealth(float mod)
    {
        health += mod; // Modify health by the given amount
        healthBar.SetHealth(health); // Update the health bar with the new health value

        if (health > maxHealth)
        {
            health = maxHealth; // Ensure health does not exceed maximum
        }
        else if (health <= 0f)
        {
            health = 0f; // Ensure health does not drop below zero
            Die(); // Call the Die method if health is zero
            animator.SetBool("Dead", true); // Set the animator's Dead parameter to true
        }
    }

    public void Die()
    {

    }
}
