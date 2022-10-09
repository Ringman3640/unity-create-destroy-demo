using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    // Health system info
    public int hitpoints = 4;
    private int currHealth;
    private const int DEFAULT_HITPOINTS = 4;

    // Reference to self's Material component
    private Material materialComp;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "Enemy";
        materialComp = GetComponent<Renderer>().material;

        if (hitpoints <= 0)
        {
            hitpoints = DEFAULT_HITPOINTS;
        }

        currHealth = hitpoints;
    }

    // Collision handler
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Hero")
        {
            DestroySelf(true);
            return;
        }

        if (collision.gameObject.name == "Egg")
        {
            // Decrement health
            --currHealth;
            if (currHealth <= 0)
            {
                DestroySelf(false);
            }

            // Change transparency
            float transparency = (float)currHealth / hitpoints;
            Color nextColor = materialComp.color;
            nextColor.a = transparency;
            materialComp.color = nextColor;
        }
    }

    // Destroy the Enemy instance and decrement the total Enemy count
    private void DestroySelf(bool destroyedByTouch)
    {
        if (!gameObject.activeSelf)
        {
            return;
        }

        EnemySystem.Instance.DecrementSpawnCount();
        EnemySystem.Instance.IncrementDestroyedCount(destroyedByTouch);
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
