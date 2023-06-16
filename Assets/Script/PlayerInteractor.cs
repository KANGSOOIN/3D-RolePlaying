using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] bool debug;

    [SerializeField] Transform point;
    [SerializeField] float range;

    public void Interact()
    {
        Collider[] colliders = Physics.OverlapSphere(point.position, range);
        foreach (Collider collider in colliders)
        {
			IInteractable interacterable = GetComponent<Collider>().GetComponent<IInteractable>();
            interacterable?.Interact();
        }
    }

    private void OnInteract(InputValue value)
    {
        Interact();
    }

	private void OnDrawGizmosSelected()
	{
        if (!debug)
            return;
        
		Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(point.position, range);
	}
}



