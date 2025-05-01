using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour
{
    [Header("Patrol Point")]
    [SerializeField] private Transform[] positions;
    [SerializeField] private float timePatrol;
    [SerializeField] private float timeWait;
    private int currentPositionPatrol;

    private bool isWaiting = false;
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
}
