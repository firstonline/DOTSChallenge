using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningObject : MonoBehaviour
{
	[Range(5f, 50f)]
	[SerializeField] private float m_rotationSpeed = 10f;

    void Update()
    {
		transform.Rotate(Vector3.up, m_rotationSpeed * Time.deltaTime);
    }
}
