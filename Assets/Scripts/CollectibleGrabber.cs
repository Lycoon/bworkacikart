using UnityEngine;

public class CollectibleGrabber : MonoBehaviour
{
    [Header("References")]
    public GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectible"))
        {
            Collectible collectible = other.gameObject.GetComponent<Collectible>();
            collectible.Collect();
            
            gameManager.PickCollectible();
        }
    }
}
