using System;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200020E RID: 526
[RequireComponent(typeof(TMP_InputField))]
public class ScreenKeyboard : MonoBehaviour
{
	// Token: 0x06001647 RID: 5703 RVA: 0x0008CA70 File Offset: 0x0008AC70
	private void Start()
	{
		this._inputField = base.GetComponent<TMP_InputField>();
		if (this._inputField != null)
		{
			this._inputField.onSelect.AddListener(new UnityAction<string>(this.onInputSelect));
			this._inputField.onDeselect.AddListener(new UnityAction<string>(this.onInputDeselect));
			return;
		}
		UnityEngine.Debug.LogError("Please add the TMP_InputField component to the object", this);
	}

	// Token: 0x06001648 RID: 5704 RVA: 0x0008CADB File Offset: 0x0008ACDB
	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter))
		{
			this.closeKeyboard();
		}
	}

	// Token: 0x06001649 RID: 5705 RVA: 0x0008CAF8 File Offset: 0x0008ACF8
	private void closeKeyboard()
	{
		if (this._keyboard != null)
		{
			try
			{
				this._keyboard.Kill();
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.Log("Cannot kill keyboard process: " + ex.Message);
			}
			this._keyboard = null;
		}
	}

	// Token: 0x0600164A RID: 5706 RVA: 0x0008CB4C File Offset: 0x0008AD4C
	private void launchKeyboard()
	{
		if (this._keyboard == null)
		{
			this._keyboard = Process.Start("tabtip.exe");
		}
	}

	// Token: 0x0600164B RID: 5707 RVA: 0x0008CB66 File Offset: 0x0008AD66
	private void onInputSelect(string pSelectionEvent)
	{
		if (!InputManager.IsUsingController)
		{
			return;
		}
		this.launchKeyboard();
	}

	// Token: 0x0600164C RID: 5708 RVA: 0x0008CB76 File Offset: 0x0008AD76
	private void onInputDeselect(string pSelectionEvent)
	{
		if (!InputManager.IsUsingController)
		{
			return;
		}
		this.closeKeyboard();
	}

	// Token: 0x0600164D RID: 5709 RVA: 0x0008CB86 File Offset: 0x0008AD86
	private void OnDestroy()
	{
		this.closeKeyboard();
		this._inputField.onSelect.RemoveListener(new UnityAction<string>(this.onInputSelect));
		this._inputField.onDeselect.RemoveListener(new UnityAction<string>(this.onInputDeselect));
	}

	// Token: 0x0600164E RID: 5710 RVA: 0x0008CBC6 File Offset: 0x0008ADC6
	public ScreenKeyboard()
	{
	}

	// Token: 0x040015E3 RID: 5603
	private TMP_InputField _inputField;

	// Token: 0x040015E4 RID: 5604
	private Process _keyboard;
}
