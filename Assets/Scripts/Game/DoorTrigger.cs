using UnityEngine;
using UnityEngine.SceneManagement;
public class DoorTrigger : MonoBehaviour
{
    [Header("DoorTrigger Configuration")]
    [SerializeField] private string sceneToLoad;
    [SerializeField] private float delayBeforeLoad;
    [SerializeField] private AudioClip doorSound;
    [SerializeField] private ChannelPlayer channelPlayer;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider collision)
    {
        if (hasTriggered) return;

        if (collision.CompareTag("Player"))
        {
            hasTriggered = true;
            Invoke(nameof(LoadScene), delayBeforeLoad);
        }
    }
    private void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    } 
}
