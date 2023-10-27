using UnityEngine;

public class EnemyRangeController : MonoBehaviour
{
    private EnemyController _enemyController;

    // Start is called before the first frame update
    void Start()
    {
        _enemyController = GetComponentInParent<EnemyController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _enemyController.StartFollowingPlayer(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _enemyController.StopFollowingPlayer();
        }
    }
}