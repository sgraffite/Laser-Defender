using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public float width = 10f;
    public float height = 5f;
    public float speed = 5f;
    public float spawnDelay = 0.5f;

    private float xMax;
    private float xMin;
    private bool movingRight = true;

    void Start () {
        var distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        var leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        var rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
        xMin = leftEdge.x;
        xMax = rightEdge.x;

        //InvokeRepeating("SpawnEnemyAtEnemyPositions", 1.0f, 0.2f);

        SpawnEnemyAtEnemyPositions();
    }

    private void Update()
    {
        var speedDirection = movingRight ? 1 : -1;
        transform.position += new Vector3(speedDirection * speed * Time.deltaTime, 0);

        float rightEdgeOfFormation = transform.position.x + (0.5f * width);
        float leftEdgeOfFormation = transform.position.x - (0.5f * width);
        
        if(rightEdgeOfFormation >= xMax)
        {
            movingRight = false;
        }else if(leftEdgeOfFormation <= xMin)
        {
            movingRight = true;
        }

        if (AllMembersDead())
        {
            SpawnEnemyAtEnemyPositions();
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }

    private bool AllMembersDead()
    {
        foreach (Transform enemyPosition in transform)
        {
            if (enemyPosition.childCount > 0)
            {
                return false;
            }
        }

        return true;
    }

    Transform NextFreePosition()
    {
        foreach (Transform enemyPosition in transform)
        {
            if (enemyPosition.childCount <= 0)
            {
                return enemyPosition;
            }
        }

        return null;
    }

    private void SpawnEnemyAtEnemyPositions()
    {
        var enemyPosition = NextFreePosition();
        if(enemyPosition == null)
        {
            return;
        }

        var enemy = Instantiate(enemyPrefab, enemyPosition.position, Quaternion.identity) as GameObject;
        enemy.transform.parent = enemyPosition;

        Invoke("SpawnEnemyAtEnemyPositions", spawnDelay);
    }
}
