using UnityEngine;
using System.Collections;
public class NPC : MonoBehaviour
{
    [Header("Patrol Point")]
    [SerializeField] private Transform[] positions;
    [SerializeField] private float timePatrol;
    private int currentPositionPatrol;
    private void Update()
    {
        if(Vector3.Distance(transform.position, positions[currentPositionPatrol].position)< 1)
        {
            UpdateTarget();
        }
        transform.position =Vector3.MoveTowards(transform.position, positions[currentPositionPatrol].position, timePatrol*Time.deltaTime);
    }
    private void UpdateTarget()
    {
        if(currentPositionPatrol <positions.Length-1)
        {
            ++currentPositionPatrol;
        }
        else
        {
            currentPositionPatrol = 0;
        }
    }
}
