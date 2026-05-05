using UnityEngine;

public class thronsScript : MonoBehaviour
{
    private Player player;

    public void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            player.TakeDamage();
        }
    }
}
