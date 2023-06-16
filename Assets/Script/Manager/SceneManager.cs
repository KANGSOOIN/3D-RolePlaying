using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager; // ���� SceneManager�� ����Ƽ ���� SceneManager�� ��ħ ����

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
        Time.timeScale = 0f; //�ε� �߿��� ������ �ð��� ������
    
        AsyncOperation oper = UnitySceneManager.LoadSceneAsync(SceneName);
        oper.allowSceneActivation = false; // -> �ε� �� ó��������� ���� ������ �Ѿ��
		// oper.allowSceneActivation = true; // -> ���� ������ �ٷ� �ε�
	
        while (!oper.isDone)
        {
            //loadingUI.SetProgress(Mathf.Lerp(0f, 0.5f, oper.progress));
            yield return null;
        }

        //      // �߰����� ������ �غ��� �ε��� �����ϰ� �Ѿ�� ��
        //      // ���� ���� ��ġ
        //      Debug.Log("���� ���� ��ġ");
        //      //loadingUI.SetProgress(0.6f);
        //yield return new WaitForSecondsRealtime(1f); // ���� �ð��� ���������� ���� �ð���ŭ �귯���� �� 
        //// ���ҽ� �ҷ�����
        //Debug.Log("���ҽ� �ҷ�����");
        //      //loadingUI.SetProgress(0.7f);
        //yield return new WaitForSecondsRealtime(1f);
        //// Ǯ��
        //Debug.Log("Ǯ��");
        ////loadingUI.SetProgress(0.8f);
        //yield return new WaitForSecondsRealtime(1f);
        //// ���� ������ ��ġ
        //Debug.Log("���� ������ ��ġ");
        ////loadingUI.SetProgress(0.9f);
        //yield return new WaitForSecondsRealtime(1f);
        //// ���� �� ����
        //Debug.Log("���� �� ����");
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
