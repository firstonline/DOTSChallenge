using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
	[SerializeField] private float m_movementsSpeed = 5f;
	[SerializeField] private float m_maxWalkingDistance = 10f;

	private Vector3 m_startingPosition;
	private float m_currentWalkingDistance;

	private void Awake()
	{
		m_startingPosition = transform.position;
	}

	private void Update()
	{
		if (m_currentWalkingDistance >= m_maxWalkingDistance)
		{
			m_currentWalkingDistance = 0f;
			transform.position = m_startingPosition;
		}

		// just move forward
		var movementVector = transform.forward * m_movementsSpeed * Time.deltaTime;
		transform.position += movementVector;
		m_currentWalkingDistance += movementVector.magnitude;
	}
}
