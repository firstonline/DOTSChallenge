using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[Header("Spawn Info")]
	[SerializeField] private bool m_spawn = true;
	[SerializeField] private bool m_useECS = false;
	[SerializeField] private GameObject[] m_prefabs;

	[Header("Spawn Timing")]
	[Range(1, 100)]
	[SerializeField] private int m_spawnsPerInterval = 1;

	[Range(.1f, 2f)]
	[SerializeField] private float m_spawnInterval = 1f;

	[Range(5f, 100f)]
	[SerializeField] private float m_spawnRadius = 5f;
	
	EntityManager m_manager;
	Entity[] m_entityPrefabs;

	float m_cooldown;


	void Start()
	{
		if (m_useECS)
		{
			m_manager = World.Active.EntityManager;
			m_entityPrefabs = new Entity[m_prefabs.Length];
			for(int i = 0; i < m_prefabs.Length; i++)
			{
				// converts gameobject to entity
				m_entityPrefabs[i] = GameObjectConversionUtility.ConvertGameObjectHierarchy(m_prefabs[i], World.Active);
			}
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
			Vector3 randomInSphere = Random.insideUnitSphere * m_spawnRadius;
			randomInSphere.y = 0;
			Vector3 pos = transform.position + randomInSphere;

			if (!m_useECS)
			{
				var randomPrefab = m_prefabs[Random.Range(0, m_prefabs.Length)];
				Instantiate(randomPrefab, pos, Quaternion.identity);
			}
			else
			{
				var randomEntityPrefab = m_entityPrefabs[Random.Range(0, m_entityPrefabs.Length)];
				Entity enemy = m_manager.Instantiate(randomEntityPrefab);
				// setting the position of entity
				m_manager.SetComponentData(enemy, new Translation { Value = pos });
			}
		}
	}
}
