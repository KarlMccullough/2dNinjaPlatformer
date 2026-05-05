using UnityEngine;

public class BossWall : MonoBehaviour
{
    void Update()
    {
        if (FindObjectOfType<Enemy>())
        {
            return;
        }

        Destroy(gameObject);
    }
}
