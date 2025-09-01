using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000151 RID: 337
public class ES3SlotDialog : MonoBehaviour
{
	// Token: 0x06000EFD RID: 3837 RVA: 0x0005F5DA File Offset: 0x0005D7DA
	protected virtual void OnEnable()
	{
		this.cancelButton.onClick.AddListener(delegate()
		{
			base.gameObject.SetActive(false);
		});
	}

	// Token: 0x06000EFE RID: 3838 RVA: 0x0005F5F8 File Offset: 0x0005D7F8
	protected virtual void OnDisable()
	{
		this.cancelButton.onClick.RemoveAllListeners();
	}

	// Token: 0x06000EFF RID: 3839 RVA: 0x0005F60A File Offset: 0x0005D80A
	public ES3SlotDialog()
	{
	}

	// Token: 0x06000F00 RID: 3840 RVA: 0x0005F612 File Offset: 0x0005D812
	[CompilerGenerated]
	private void <OnEnable>b__2_0()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x04000CB0 RID: 3248
	[Tooltip("The UnityEngine.UI.Button Component for the Confirm button.")]
	public Button confirmButton;

	// Token: 0x04000CB1 RID: 3249
	[Tooltip("The UnityEngine.UI.Button Component for the Cancel button.")]
	public Button cancelButton;
}
