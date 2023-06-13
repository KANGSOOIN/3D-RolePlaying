using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacker : MonoBehaviour
{

	[SerializeField] bool debug;

	[SerializeField] Weapon weapon;
	[SerializeField] int damage;
	[SerializeField] float range;
	[SerializeField, Range(0,360)] float angle;

	private Animator anim;

	private void Awake()
	{
		anim = GetComponent<Animator>();
	}

	public void Attack()
	{
		anim.SetTrigger("Attack");
	}

	private void OnAttack(InputValue value)
	{
		Attack();
	}

	public void StartAttack()
	{
		weapon.EnableWeapon();
		Debug.Log("공격 시작");

	}

	public void EndAttack()
	{
		weapon.DisableWeapon();
		Debug.Log("공격 끝");
	}

	// 범위 공격
	// -> 내적, 외적
	public void AttackTiming()
	{
		// 1. 범위 안에 있는지
		Collider[] colliders = Physics.OverlapSphere(transform.position, range);
		foreach (Collider collider in colliders)
		{
			// 2. 앞에 있는지
			// -> 양수면 앞, 음수면 뒤에 있음
			Vector3 dirTarget = (collider.transform.position - transform.position).normalized;
			if (Vector3.Dot(transform.forward, dirTarget) < Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad)) // => 각도를 호도법으로 변환
				continue;

			IHittable hittable = collider.GetComponent<IHittable>();
			hittable?.TakeHit(damage);
		}
	}

	// 각도에 대한 기즈모
	private void OnDrawGizmosSelected()
	{
		if (!debug)
			return;

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

