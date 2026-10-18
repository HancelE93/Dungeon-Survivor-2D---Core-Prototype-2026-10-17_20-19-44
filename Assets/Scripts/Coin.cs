
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player" ||
            other.CompareTag("Player") ||
            other.transform.root.name == "Player" ||
            other.transform.root.CompareTag("Player"))
        {
            CoinUI coinUI = FindFirstObjectByType<CoinUI>();

            if (coinUI != null)
            {
                coinUI.AddCoin();
                Destroy(transform.root.gameObject);
            }
        }
    }
}
