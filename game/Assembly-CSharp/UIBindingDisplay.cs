using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000201 RID: 513
public class UIBindingDisplay : MonoBehaviour
{
	// Token: 0x060015F0 RID: 5616 RVA: 0x0008A8DC File Offset: 0x00088ADC
	private void Awake()
	{
		if (this.IconDisplay == null)
		{
			this.IconDisplay = base.GetComponent<Image>();
		}
		this.UpdateIcon();
		InputManager.OnInputMethodChanged = (Action)Delegate.Combine(InputManager.OnInputMethodChanged, new Action(this.UpdateIcon));
	}

	// Token: 0x060015F1 RID: 5617 RVA: 0x0008A92C File Offset: 0x00088B2C
	public void UpdateIcon()
	{
		PlayerDB.InputSprite mainBinding = PlayerDB.GetMainBinding(this.Binding);
		if (mainBinding == null)
		{
			return;
		}
		if (this.IconDisplay == null)
		{
			Debug.Log("No Icon", base.gameObject);
		}
		this.IconDisplay.sprite = mainBinding.Sprite;
	}

	// Token: 0x060015F2 RID: 5618 RVA: 0x0008A978 File Offset: 0x00088B78
	private void OnDestroy()
	{
		InputManager.OnInputMethodChanged = (Action)Delegate.Remove(InputManager.OnInputMethodChanged, new Action(this.UpdateIcon));
	}

	// Token: 0x060015F3 RID: 5619 RVA: 0x0008A99A File Offset: 0x00088B9A
	public UIBindingDisplay()
	{
	}

	// Token: 0x040015A9 RID: 5545
	public InputActions.InputAction Binding;

	// Token: 0x040015AA RID: 5546
	public Image IconDisplay;
}
