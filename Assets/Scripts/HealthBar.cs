using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public float maxHealth = 100f;
    public float health;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health != healthSlider.value)
        {
            healthSlider.value = health;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            health -= 10;
        }

        if (health <= 0)
        {
            health = 0;
            //death
        }
        //add if statement for damage
    }
}
