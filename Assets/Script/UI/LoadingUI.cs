using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
	[SerializeField] Slider slider;

	private Animator anim;

	private void Awake()
	{
		anim = GetComponent<Animator>(); 
	}

	public static void FadeIn()
	{ 
	
	}

	public static void FadeOut()
	{

	}

	public void SetProgress()
	{ 
		slider.value = 0;
	}
}
