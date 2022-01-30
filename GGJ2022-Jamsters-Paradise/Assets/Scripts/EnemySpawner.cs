using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField]
    private GameObject enemy;

    public List<Vector2> path;
    private bool pathCheck;
    
    public float spawnRate = 3f;
    public float timer;


    public void Setup(List<Vector2> path) {
        this.path = path;
        timer = 0;
        pathCheck = true;
        
    }

    private void Update() {

        if (pathCheck) {
            timer -= Time.deltaTime;

            if (timer <= 0) {
                SpawnEnemy();
                timer = spawnRate;
            }
        }

        

    }

    public void SpawnEnemy() {

        
        GameObject go_enemy = Instantiate(enemy, new Vector3(transform.position.x, 0.5f, transform.position.z), Quaternion.identity);
        go_enemy.GetComponent<EnemyMovement>().Setup(path);

        

    }



}
