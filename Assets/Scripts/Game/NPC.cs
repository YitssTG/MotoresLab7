using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using System.Threading.Tasks;

public class NPC : MonoBehaviour
{
    [Header("Dialgue")]
    [SerializeField] private string dialogue;
    private PlayerController player;

    [Header("Patrol Point")]
    [SerializeField] private Transform[] positions;
    [SerializeField] private float timePatrol;
    [SerializeField] private float timeWait;
    private int currentPositionPatrol;

    private bool isWaiting = false;
    private bool isPlayer;
    private void Update()
    {
        if (!isWaiting)
        {
            if (Vector3.Distance(transform.position, positions[currentPositionPatrol].position) == 0)
            {
                StartCoroutine(UpdateTarget());
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, positions[currentPositionPatrol].position, timePatrol * Time.deltaTime);
            }
        }
    }
    private IEnumerator UpdateTarget()
    {
        isWaiting = true;
        yield return new WaitForSecondsRealtime(timeWait);
        if (currentPositionPatrol < positions.Length - 1)
        {
            currentPositionPatrol++;
        }
        else
        {
            currentPositionPatrol = 0;
        }
        isWaiting = false;
    }
    public void Interactuve(InputAction.CallbackContext context)
    {
        if (context.performed && isPlayer)
        {
            StartCoroutine(ShowDialogueAndPause());
        }
    }

    private IEnumerator ShowDialogueAndPause()
    {
        isWaiting = true; 
        UIManager.Instance.DialogueNPC(dialogue); 
        yield return new WaitForSeconds(2f); 
        UIManager.Instance.HideDialogue();
        isWaiting = false; 
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayer = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayer = false;
        }
    }
}
