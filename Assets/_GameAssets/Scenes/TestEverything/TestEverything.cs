using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestEverything : MonoBehaviour
{
    [SerializeField] private NavMeshAgent destination;

    [SerializeField] private Camera cam;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                destination.SetDestination(hit.point);
            }
        }
    }
}
