using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// @kurtdekker

public class OneSingleTile : MonoBehaviour
{
	[SerializeField]Button Button;
	[SerializeField]Text Caption;
	[SerializeField]Image Icon;

	public void SetCaptionText( string caption)
	{
		Caption.text = caption;
	}

	public void SetIconSprite (Sprite sprite)
	{
		Icon.sprite = sprite;
	}

	public void SetButtonDelegate( System.Action action)
	{
		Button.onClick.AddListener(
			delegate {
				action();
			}
		);
	}
}
