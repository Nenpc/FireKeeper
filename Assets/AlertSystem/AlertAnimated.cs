using UnityEngine;
//using DG.Tweening;
using UnityEngine.UI;

public class AlertAnimated : MonoBehaviour {

	Transform panelTr;

	//public void Close()
	//{
	//	foreach (Graphic nestedGraphic in panelTr.GetComponentsInChildren<Graphic>())
	//	{
	//		nestedGraphic.DOFade(0, 0.2f);
	//	}
	//	transform.GetComponent<Image>().DOFade(0f, 0.2f).OnComplete(() => gameObject.SetActive(false));
	//}

	//private void OnEnable()
	//{
	//	transform.GetComponent<Image>().DOFade(0.5f, 0.3f);
	//	panelTr = transform.Find("ShadowPanel");
	//	panelTr.DOScale(0.8f, 0.3f).From().SetEase(Ease.OutQuad).SetDelay(0.1f);
	//	foreach (Graphic nestedGraphic in panelTr.GetComponentsInChildren<Graphic>())
	//	{
	//		nestedGraphic.color = new Color(nestedGraphic.color.r, nestedGraphic.color.g, nestedGraphic.color.b, 0);
	//		nestedGraphic.DOFade(1, 0.3f).SetDelay(0.1f);
	//	}
	//}

	// Из старого алерта
	//public void Destroy()
	//{
	//	foreach (Graphic nestedGraphic in panelTr.GetComponentsInChildren<Graphic>())
	//	{
	//		nestedGraphic.DOFade(0, 0.2f);
	//	}
	//	transform.Find("Background").GetComponent<Image>().DOFade(0f, 0.2f).OnComplete(() => { Destroy(gameObject); });
	//}

	//private void OnEnable()
	//{
	//	transform.Find("Background").GetComponent<Image>().DOFade(0.5f, 0.3f);
	//	panelTr = transform.Find("Background/Panel");
	//	panelTr.DOScale(0.8f, 0.3f).From().SetEase(Ease.OutQuad).SetDelay(0.1f);
	//	foreach (Graphic nestedGraphic in panelTr.GetComponentsInChildren<Graphic>())
	//	{
	//		nestedGraphic.color = new Color(nestedGraphic.color.r, nestedGraphic.color.g, nestedGraphic.color.b, 0);
	//		nestedGraphic.DOFade(1, 0.3f).SetDelay(0.1f);
	//	}
	//}

}
