using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[Header("Spawn Info")]
	public bool m_spawn = true;
	public bool m_useECS = false;
	public GameObject m_prefab;

	[Header("Spawn Timing")]
	[Range(1, 100)] public int m_spawnsPerInterval = 1;
	[Range(.1f, 2f)] public float m_spawnInterval = 1f;
	
	EntityManager m_manager;
	Entity m_entityPrefab;

	float m_cooldown;


	void Start()
	{
		if (m_useECS)
		{
			m_manager = World.Active.EntityManager;
			m_entityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(m_prefab, World.Active);
		}
	}

	void Update()
    {
		m_cooldown -= Time.deltaTime;

		if (m_cooldown <= 0f)
		{
			m_cooldown += m_spawnInterval;
			Spawn();
		}
    }

	void Spawn()
	{
		for (int i = 0; i < m_spawnsPerInterval; i++)
		{
			Vector3 pos = transform.position;

			if (!m_useECS)
			{
				Instantiate(m_prefab, pos, Quaternion.identity);
			}
			else
			{
				Entity enemy = m_manager.Instantiate(m_entityPrefab);
				m_manager.SetComponentData(enemy, new Translation { Value = pos });
			}
		}
	}
}
