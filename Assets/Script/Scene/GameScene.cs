using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
	public GameObject playerPrefab;
	public GameObject player;
	public Transform PlayerPosition;

	protected override IEnumerator LoadingRoutine()
	{
		// Fake Loading
		progress = 0.0f;
		Debug.Log("랜덤 맵 생성");
		yield return new WaitForSecondsRealtime(1f);
		
		progress = 0.2f;
		Debug.Log("랜덤 몬스터 생성");
		yield return new WaitForSecondsRealtime(1f);
		
		progress = 0.4f;
		Debug.Log("랜덤 아이템 생성");
		yield return new WaitForSecondsRealtime(1f);
		
		progress = 0.6f;
		Debug.Log("플레이어 배치");
		//Instantiate(playerPrefab, PlayerPosition.position, PlayerPosition.rotation);
		player.transform.position = PlayerPosition.position;
		yield return new WaitForSecondsRealtime(1f);

		progress = 0.8f;
		Debug.Log("카메라 설정");
		//freelookCamera.follow = player.transform;
		//lookat
		yield return new WaitForSecondsRealtime(1f);

		progress = 1.0f;
		yield return new WaitForSecondsRealtime(1f);
	}
}
