using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Variables")]
    public float followSpeed = 5.0f;
    public float swapSpeed = 1.3f;
    public string enemyTag = "Enemy";

    [Header("References")]
    public Transform player;
    public Vector3 offset;

    private Transform target;
    private bool tabPressed = false;
    private bool targetSwapped = false;

    void Start()
    {
        target = player;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!targetSwapped)
            {
                tabPressed = true;
            }
            else
            {
                ToggleTarget();
                targetSwapped = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (tabPressed)
        {
            ToggleTarget();
            tabPressed = false;
            targetSwapped = true;
        }

        if (target == null)
            return;

        Vector3 desiredPosition;

        if (target == player)
        {
            desiredPosition = player.position + offset;
        }
        else
        {
            Transform nearestEnemy = FindNearestEnemy();
            Vector3 midpoint = (player.position + nearestEnemy.position) / 2f;

            GameObject midpointObject = new GameObject("Midpoint");
            midpointObject.transform.position = midpoint;

            desiredPosition = midpointObject.transform.position + offset;

            Destroy(midpointObject);
        }

        float currentSpeed = targetSwapped ? swapSpeed : followSpeed;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, currentSpeed * Time.fixedDeltaTime);
    }

    void ToggleTarget()
    {
        if (target == player)
        {
            target = FindNearestEnemy();
        }
        else
        {
            target = player;
        }
    }

    Transform FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float minDistance = Mathf.Infinity;
        Transform nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(player.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy.transform;
            }
        }

        return nearestEnemy;
    }
}
