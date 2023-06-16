using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundReactor : MonoBehaviour, IListenable
{
	public void Listen(Transform trans)
	{
		transform.LookAt(trans.position);
	}
}