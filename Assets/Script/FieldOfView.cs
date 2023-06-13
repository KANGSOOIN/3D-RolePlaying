using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] float range;
    [SerializeField, Range(0f, 360f)] float angle;
	[SerializeField] LayerMask targetMask;
	[SerializeField] LayerMask obstacleMask;

	private void Update()
	{
		FindTarget();
	}

	// 범위 공격
	// -> 내적, 외적
	public void FindTarget()
	{
		// 1. 범위 안에 있는지
		Collider[] colliders = Physics.OverlapSphere(transform.position, range, targetMask);
		foreach (Collider collider in colliders)
		{
			// 2. 각도 안에 있는지
			Vector3 dirTarget = (collider.transform.position - transform.position).normalized;
			if (Vector3.Dot(transform.forward, dirTarget) < Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad)) // => 각도를 호도법으로 변환
				continue;

			// 3. 중간에 장애물이 없는지 (시야각)
			// Ray를 쏘면 확인할 수 있음
			float disttoTarget = Vector3.Distance(transform.position, collider.transform.position);
			if (Physics.Raycast(transform.position, dirTarget, disttoTarget, obstacleMask))
				continue;

			Debug.DrawRay(transform.position, dirTarget * disttoTarget, Color.red);
;		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);

		Vector3 rightDir = AngleToDir(transform.eulerAngles.y + angle * 0.5f);
		Vector3 leftDir = AngleToDir(transform.eulerAngles.y - angle * 0.5f);
		Debug.DrawRay(transform.position, rightDir * range, Color.yellow);
		Debug.DrawRay(transform.position, leftDir * range, Color.yellow);
	}

	private Vector3 AngleToDir(float angle)
	{
		float radian = angle * Mathf.Deg2Rad;
		return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
	}
}
