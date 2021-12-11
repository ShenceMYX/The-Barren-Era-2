using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ns
{
	/// <summary>
	/// 
	/// </summary>
	public class CharacterStatus : MonoBehaviour
	{
		public float maxHealth;
		public float currentHealth;
		public event Action OnDeathHandler;

		private void Start()
		{
			currentHealth = maxHealth;
		}

		public void Damage(float amount)
		{
			currentHealth -= amount;
			if (currentHealth <= 0)
				Death();
		}

		private void Death()
		{
			OnDeathHandler?.Invoke();
			Destroy(gameObject);
		}
	}
}
