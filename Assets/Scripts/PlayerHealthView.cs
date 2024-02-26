using UnityEngine;

public class PlayerHealthView : CharacterHealthView
{
    private void Start()
    {
        CharacterHealth playerHealth = FindAnyObjectByType<PlayerHealth>();
        Init(playerHealth);
    }
}