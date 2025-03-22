using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerHealth : IInitializable
{
    [Inject(Id = "Healthbar")]
    private readonly Image _healthbar;
    [Inject(Id = "PlayerHealth")]
    private readonly int _startHealth;

    private int _currentHealth;

    [Inject]
    public void Construct(EventHandler eventHandler)
    {
        eventHandler.OnBlockFell += TakeDamage;
    }

    public void Initialize()
    {
        _currentHealth = _startHealth;
        UpdateHealthbar();
    }

    private void UpdateHealthbar()
    {
        _healthbar.fillAmount = (float)_currentHealth / _startHealth;
    }

    private void TakeDamage()
    {
        _currentHealth--;
        UpdateHealthbar();
        if (_currentHealth <= 0)
        {
            Debug.Log("Death");
        }
    }
}