using UnityEngine;

public class ScoreUp : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.ScoreUP();
        }
        
    }
}
