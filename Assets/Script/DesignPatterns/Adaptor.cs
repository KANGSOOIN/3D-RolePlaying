using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adaptor : MonoBehaviour
{
	//========================================
	//##		디자인 패턴 Adaptor  		##
	//========================================
	/*
		어댑터 패턴 :
		호환되지 않은 두 객체를 협업할 수 있도록 중간 매개체를 구현하는 패턴

		구현 :
		1. 호환하지 않는 두 객체 사이에 어댑터 중간 객체를 구현
		2. 어댑터 객체는 두 객체 사이의 상호작용 방법을 기술

		장점 :
		1. 기존의 코드를 변경하지 않고 적용이 가능하므로 개방폐쇄 원칙이 준수된다.
		2. 클래스간의 상호작용에 대한 설계를 어댑터를 통해 상위모듈로 구현할 수 있으므로 의존성역전의 원칙이 준수된다.

		단점 :
		1. 인터페이스를 직접 포함하지 않으므로 인터페이스를 가짐으로 얻을 수 있는 가독성을 얻지 못한다.
	*/
}

namespace DesignPattern
{
	public class DollarCustomer
	{
		public Exchanger exchanger;

		public void Buy()
		{
			Debug.Log("물건구매");
			exchanger.Change();
		}
	}

	public class KRWStore
	{
		public Exchanger exchanger;

		public void Sell()
		{
			Debug.Log("물건판매");
		}
	}

	public class Exchanger
	{
		public DollarCustomer customer;
		public KRWStore store;

		public void Change()
		{
			store.Sell();
		}
	}
}