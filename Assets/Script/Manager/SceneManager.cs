using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager; // 만든 SceneManager와 유니티 내의 SceneManager와 겹침 방지

public class SceneManager : MonoBehaviour
{
	private LoadingUI loadingUI; 
	private BaseScene curScene;
    public BaseScene CurScene
	{
        get
        {
            if (curScene == null)
                curScene = GameObject.FindAnyObjectByType<BaseScene>();
           
            return curScene;
        }
    }

	private void Awake()
	{
        LoadingUI ui = Resources.Load<LoadingUI>("UI/LoadingUI");
        loadingUI = Instantiate(ui);
        loadingUI.transform.SetParent(transform, false);
	}

	public void LoadScene(string sceneName)
    {
        // UnitySceneManager.LoadScene(sceneName);
        StartCoroutine(LoadingRoutine(sceneName));
    }

    IEnumerator LoadingRoutine(string SceneName)
    {
        //loadingUI.FadeOut();
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0f; //로딩 중에는 게임의 시간을 멈춰줌
    
        AsyncOperation oper = UnitySceneManager.LoadSceneAsync(SceneName);
        oper.allowSceneActivation = false; // -> 로딩 후 처리해줘야지 다음 씬으로 넘어가짐
		// oper.allowSceneActivation = true; // -> 다음 씬으로 바로 로딩
	
        while (!oper.isDone)
        {
            //loadingUI.SetProgress(Mathf.Lerp(0f, 0.5f, oper.progress));
            yield return null;
        }

        //      // 추가적인 씬에서 준비할 로딩을 진행하고 넘어가야 함
        //      // 몬스터 랜덤 배치
        //      Debug.Log("몬스터 랜덤 배치");
        //      //loadingUI.SetProgress(0.6f);
        //yield return new WaitForSecondsRealtime(1f); // 게임 시간은 멈춰있으니 실제 시간만큼 흘러가게 함 
        //// 리소스 불러오기
        //Debug.Log("리소스 불러오기");
        //      //loadingUI.SetProgress(0.7f);
        //yield return new WaitForSecondsRealtime(1f);
        //// 풀링
        //Debug.Log("풀링");
        ////loadingUI.SetProgress(0.8f);
        //yield return new WaitForSecondsRealtime(1f);
        //// 랜덤 아이템 배치
        //Debug.Log("랜덤 아이템 배치");
        ////loadingUI.SetProgress(0.9f);
        //yield return new WaitForSecondsRealtime(1f);
        //// 랜덤 맵 생성
        //Debug.Log("랜덤 맵 생성");
        ////loadingUI.SetProgress(1.0f);
        //yield return new WaitForSecondsRealtime(1f);

        CurScene.LoadAsync();
        while (curScene.progress < 1f)
        { 
            //loadingUI.SetProgress(Mathf.Lerp(0.5f, 1.0f, curScene.progress));
            yield return null;
        }

        Time.timeScale = 1f;
		//loadingUI.FadeIn();
		yield return new WaitForSecondsRealtime(0.5f);
    }
}
