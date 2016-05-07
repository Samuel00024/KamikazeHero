using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
    #region Inspector fields

    [Header("Parameters")]
    [Range(1f, 100f)]
	public int hp;                                   			// The current health the player has.
	public int startingHealth = 100;                            // The amount of health the player starts the game with.

    [Range(0f, 100f)]
    public int exp;

	public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.

	bool damaged;                                               // True when the player gets damaged.

	[Header("References")]
	public Slider healthSlider;                                 // Reference to the UI's health bar.
	public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.

    #endregion


	void Awake ()
	{
		// Set the initial health of the player.
		hp = startingHealth;
	}

	void Update ()
	{
		healthSlider.value = hp;
		// If the player has just been damaged...
		if(damaged)
		{
			// ... set the colour of the damageImage to the flash colour.
			damageImage.color = flashColour;
		}
		// Otherwise...
		else
		{
			// ... transition the colour back to clear.
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}

		// Reset the damaged flag.
		damaged = false;
	}

	public int sub(int damage)
    {
		// Set the damaged flag so the screen will flash.
		damaged = true;

		// Reduce the current health by the damage amount.
        hp -= damage;

		// Set the health bar's value to the current health.
		healthSlider.value = hp;

		// If the player has lost all it's health and the death flag hasn't been set yet...
		if (hp < 1)
        {
			// Tell the animator that the player is dead
			// Set the audiosource to play the death clip
			// Turn off the movement
            return exp;
        }
        else
        {
            return 0;
        }
    }
}
