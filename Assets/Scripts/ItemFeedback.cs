using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFeedback : MonoBehaviour
{
	public void OnCompleteAnimation()
	{
		Destroy(this.gameObject);
	}

	public void OnFeedbackSound()
	{
		SoundManager.Instance.PlaySE(SESoundData.SE.Kill);
	}

}
