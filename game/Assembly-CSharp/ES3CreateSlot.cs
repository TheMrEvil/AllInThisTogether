using System;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200014F RID: 335
public class ES3CreateSlot : MonoBehaviour
{
	// Token: 0x06000EED RID: 3821 RVA: 0x0005F158 File Offset: 0x0005D358
	protected virtual void OnEnable()
	{
		base.gameObject.SetActive(this.mgr.showCreateSlotButton);
		this.createButton.onClick.AddListener(new UnityAction(this.ShowCreateSlotDialog));
		this.createDialog.confirmButton.onClick.AddListener(new UnityAction(this.TryCreateNewSlot));
	}

	// Token: 0x06000EEE RID: 3822 RVA: 0x0005F1B9 File Offset: 0x0005D3B9
	protected virtual void OnDisable()
	{
		this.inputField.text = string.Empty;
		this.createButton.onClick.RemoveAllListeners();
		this.createDialog.confirmButton.onClick.RemoveAllListeners();
	}

	// Token: 0x06000EEF RID: 3823 RVA: 0x0005F1F0 File Offset: 0x0005D3F0
	protected void ShowCreateSlotDialog()
	{
		this.createDialog.gameObject.SetActive(true);
		this.inputField.Select();
		this.inputField.ActivateInputField();
	}

	// Token: 0x06000EF0 RID: 3824 RVA: 0x0005F21C File Offset: 0x0005D41C
	public virtual void TryCreateNewSlot()
	{
		if (string.IsNullOrEmpty(this.inputField.text))
		{
			this.mgr.ShowErrorDialog("You must specify a name for your save slot");
			return;
		}
		string slotPath = this.mgr.GetSlotPath(this.inputField.text);
		if (ES3.FileExists(slotPath))
		{
			ES3Slot es3Slot = (from go in this.mgr.slots
			select go.GetComponent<ES3Slot>()).FirstOrDefault((ES3Slot slot) => this.mgr.GetSlotPath(slot.nameLabel.text) == slotPath && slot.markedForDeletion);
			if (es3Slot == null)
			{
				this.mgr.ShowErrorDialog("A slot already exists with this name. Please choose a different name.");
				return;
			}
			es3Slot.DeleteSlot();
		}
		this.CreateNewSlot(this.inputField.text);
		this.inputField.text = "";
		this.createDialog.gameObject.SetActive(false);
	}

	// Token: 0x06000EF1 RID: 3825 RVA: 0x0005F318 File Offset: 0x0005D518
	protected virtual void CreateNewSlot(string slotName)
	{
		DateTime now = DateTime.Now;
		this.mgr.InstantiateSlot(slotName, now).transform.SetSiblingIndex(1);
		if (this.mgr.autoCreateSaveFile)
		{
			ES3.SaveRaw("{}", this.mgr.GetSlotPath(slotName));
		}
	}

	// Token: 0x06000EF2 RID: 3826 RVA: 0x0005F366 File Offset: 0x0005D566
	public ES3CreateSlot()
	{
	}

	// Token: 0x04000CA4 RID: 3236
	[Tooltip("The button used to bring up the 'Create Slot' dialog.")]
	public Button createButton;

	// Token: 0x04000CA5 RID: 3237
	[Tooltip("The ES3SlotDialog Component of the Create Slot dialog")]
	public ES3SlotDialog createDialog;

	// Token: 0x04000CA6 RID: 3238
	[Tooltip("The TMP_Text input text field of the create slot dialog.")]
	public TMP_InputField inputField;

	// Token: 0x04000CA7 RID: 3239
	[Tooltip("The ES3SlotManager this Create Slot Dialog belongs to.")]
	public ES3SlotManager mgr;

	// Token: 0x02000547 RID: 1351
	[CompilerGenerated]
	private sealed class <>c__DisplayClass7_0
	{
		// Token: 0x06002443 RID: 9283 RVA: 0x000CDA65 File Offset: 0x000CBC65
		public <>c__DisplayClass7_0()
		{
		}

		// Token: 0x06002444 RID: 9284 RVA: 0x000CDA6D File Offset: 0x000CBC6D
		internal bool <TryCreateNewSlot>b__1(ES3Slot slot)
		{
			return this.<>4__this.mgr.GetSlotPath(slot.nameLabel.text) == this.slotPath && slot.markedForDeletion;
		}

		// Token: 0x04002696 RID: 9878
		public ES3CreateSlot <>4__this;

		// Token: 0x04002697 RID: 9879
		public string slotPath;
	}

	// Token: 0x02000548 RID: 1352
	[CompilerGenerated]
	[Serializable]
	private sealed class <>c
	{
		// Token: 0x06002445 RID: 9285 RVA: 0x000CDA9F File Offset: 0x000CBC9F
		// Note: this type is marked as 'beforefieldinit'.
		static <>c()
		{
		}

		// Token: 0x06002446 RID: 9286 RVA: 0x000CDAAB File Offset: 0x000CBCAB
		public <>c()
		{
		}

		// Token: 0x06002447 RID: 9287 RVA: 0x000CDAB3 File Offset: 0x000CBCB3
		internal ES3Slot <TryCreateNewSlot>b__7_0(GameObject go)
		{
			return go.GetComponent<ES3Slot>();
		}

		// Token: 0x04002698 RID: 9880
		public static readonly ES3CreateSlot.<>c <>9 = new ES3CreateSlot.<>c();

		// Token: 0x04002699 RID: 9881
		public static Func<GameObject, ES3Slot> <>9__7_0;
	}
}
