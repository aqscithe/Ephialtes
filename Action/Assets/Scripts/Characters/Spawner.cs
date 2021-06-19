using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject playerSpawn = null;
    [SerializeField] GameObject player = null;

    GameObject[] darkEnemySpawns;
    [SerializeField] GameObject darkEnemyPrefab = null;

    GameObject[] lightEnemySpawns;
    [SerializeField] GameObject lightEnemyPrefab = null;

    [SerializeField] float pillarHeightSmall = 1f;
    [SerializeField] float pillarHeightMedium = 2f;
    [SerializeField] float pillarHeightLarge = 4f;

    GameObject enemies = null;
    public GameObject transformedEnemies = null;
    [SerializeField] GameObject transformedPrefab = null;

    bool isForestScene = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        darkEnemySpawns = GameObject.FindGameObjectsWithTag("Spawn_Enemy_Dark");
        lightEnemySpawns = GameObject.FindGameObjectsWithTag("Spawn_Enemy_Light");
        enemies = new GameObject("Enemies");
        if (SceneManager.GetActiveScene().name == "Forest")
        {
            transformedEnemies = new GameObject("TransformedEnemies");
            isForestScene = true;
        }
            
    }
    void Start()
    {
        SpawnPlayer();
        SpawnEnemies();
    }

    private void SpawnPlayer()
    {
        player.transform.position = playerSpawn.transform.position;
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < darkEnemySpawns.Length; ++i)
        {
            
            GameObject obj = Instantiate(darkEnemyPrefab, darkEnemySpawns[i].transform.position, Quaternion.identity);
            obj.transform.parent = enemies.transform;

            if (isForestScene)
            {

                GameObject stairObj = Instantiate(transformedPrefab, darkEnemySpawns[i].transform.position, Quaternion.identity);
                stairObj.transform.parent = transformedEnemies.transform;
                stairObj.gameObject.SetActive(false);
                switch ((i + 1) % darkEnemySpawns.Length)
                {
                    case 0:
                        stairObj.transform.localScale = new Vector3(
                            stairObj.transform.localScale.x, pillarHeightSmall, stairObj.transform.localScale.z
                        );
                        break;
                    case 1:
                        stairObj.transform.localScale = new Vector3(
                            stairObj.transform.localScale.x, pillarHeightMedium, stairObj.transform.localScale.z
                        );

                        break;
                    case 2:
                        stairObj.transform.localScale = new Vector3(
                            stairObj.transform.localScale.x, pillarHeightLarge, stairObj.transform.localScale.z
                        );
                        break;

                    default:
                        break;
                }
            }
            
            
        }

        for (int i = 0; i < lightEnemySpawns.Length; ++i)
        {
            GameObject obj = Instantiate(lightEnemyPrefab, lightEnemySpawns[i].transform.position, Quaternion.identity);
            obj.transform.parent = enemies.transform;
        }
    }
}
