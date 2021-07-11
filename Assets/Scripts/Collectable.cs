using UnityEngine;

public class Collectable : MonoBehaviour
{
    enum CollectType
    {
        AddHP
    }
    [SerializeField] CollectType collectType;
    [SerializeField] float value;
    [SerializeField] AudioClip collectSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            InvokeCorrectCollectEffect(other.gameObject);
            AudioManager.PlayClip2D(collectSound);
            Destroy(gameObject);
        }
    }

    void InvokeCorrectCollectEffect(GameObject obj)
    {
        switch (collectType)
        {
            case CollectType.AddHP:
                Player player = obj.GetComponent<Player>();
                if (player)
                    AddHPToPlayer(player);
                break;
            default:
                break;
        }
    }

    public void AddHPToPlayer(Player player)
    {
        player.AddHealth((int)value);
    }
}
