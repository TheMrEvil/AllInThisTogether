using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x02000150 RID: 336
public class ES3Slot : MonoBehaviour
{
	// Token: 0x06000EF3 RID: 3827 RVA: 0x0005F370 File Offset: 0x0005D570
	public virtual void OnEnable()
	{
		this.selectButton.onClick.AddListener(new UnityAction(this.TrySelectSlot));
		this.deleteButton.onClick.AddListener(new UnityAction(this.MarkSlotForDeletion));
		this.undoButton.onClick.AddListener(new UnityAction(this.UnmarkSlotForDeletion));
	}

	// Token: 0x06000EF4 RID: 3828 RVA: 0x0005F3D4 File Offset: 0x0005D5D4
	public virtual void OnDisable()
	{
		this.selectButton.onClick.RemoveAllListeners();
		this.deleteButton.onClick.RemoveAllListeners();
		this.undoButton.onClick.RemoveAllListeners();
		if (this.markedForDeletion)
		{
			this.DeleteSlot();
		}
	}

	// Token: 0x06000EF5 RID: 3829 RVA: 0x0005F414 File Offset: 0x0005D614
	protected virtual void TrySelectSlot()
	{
		if (this.mgr.showConfirmationIfExists)
		{
			if (this.confirmationDialog == null)
			{
				Debug.LogError("The confirmationDialog field of this ES3SelectSlot Component hasn't been set in the inspector.", this);
			}
			if (ES3.FileExists(this.GetSlotPath()))
			{
				this.confirmationDialog.SetActive(true);
				this.confirmationDialog.GetComponent<ES3SlotDialog>().confirmButton.onClick.AddListener(new UnityAction(this.DeleteThenSelectSlot));
				return;
			}
		}
		this.SelectSlot();
	}

	// Token: 0x06000EF6 RID: 3830 RVA: 0x0005F48E File Offset: 0x0005D68E
	protected virtual void DeleteThenSelectSlot()
	{
		this.DeleteSlot();
		this.SelectSlot();
	}

	// Token: 0x06000EF7 RID: 3831 RVA: 0x0005F49C File Offset: 0x0005D69C
	protected virtual void SelectSlot()
	{
		GameObject gameObject = this.confirmationDialog;
		if (gameObject != null)
		{
			gameObject.SetActive(false);
		}
		ES3SlotManager.selectedSlotPath = this.GetSlotPath();
		ES3Settings.defaultSettings.path = ES3SlotManager.selectedSlotPath;
		UnityEvent onAfterSelectSlot = this.mgr.onAfterSelectSlot;
		if (onAfterSelectSlot != null)
		{
			onAfterSelectSlot.Invoke();
		}
		if (!string.IsNullOrEmpty(this.mgr.loadSceneAfterSelectSlot))
		{
			SceneManager.LoadScene(this.mgr.loadSceneAfterSelectSlot);
		}
	}

	// Token: 0x06000EF8 RID: 3832 RVA: 0x0005F50D File Offset: 0x0005D70D
	protected virtual void MarkSlotForDeletion()
	{
		this.markedForDeletion = true;
		this.undoButton.gameObject.SetActive(true);
		this.deleteButton.gameObject.SetActive(false);
	}

	// Token: 0x06000EF9 RID: 3833 RVA: 0x0005F538 File Offset: 0x0005D738
	protected virtual void UnmarkSlotForDeletion()
	{
		this.markedForDeletion = false;
		this.undoButton.gameObject.SetActive(false);
		this.deleteButton.gameObject.SetActive(true);
	}

	// Token: 0x06000EFA RID: 3834 RVA: 0x0005F564 File Offset: 0x0005D764
	public virtual void DeleteSlot()
	{
		ES3.DeleteFile(this.GetSlotPath(), new ES3Settings(new Enum[]
		{
			ES3.Location.Cache
		}));
		ES3.DeleteFile(this.GetSlotPath(), new ES3Settings(new Enum[]
		{
			ES3.Location.File
		}));
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000EFB RID: 3835 RVA: 0x0005F5BA File Offset: 0x0005D7BA
	public virtual string GetSlotPath()
	{
		return this.mgr.GetSlotPath(this.nameLabel.text);
	}

	// Token: 0x06000EFC RID: 3836 RVA: 0x0005F5D2 File Offset: 0x0005D7D2
	public ES3Slot()
	{
	}

	// Token: 0x04000CA8 RID: 3240
	[Tooltip("The text label containing the slot name.")]
	public TMP_Text nameLabel;

	// Token: 0x04000CA9 RID: 3241
	[Tooltip("The text label containing the last updated timestamp for the slot.")]
	public TMP_Text timestampLabel;

	// Token: 0x04000CAA RID: 3242
	[Tooltip("The confirmation dialog to show if showConfirmationIfExists is true.")]
	public GameObject confirmationDialog;

	// Token: 0x04000CAB RID: 3243
	public ES3SlotManager mgr;

	// Token: 0x04000CAC RID: 3244
	[Tooltip("The button for selecting this slot.")]
	public Button selectButton;

	// Token: 0x04000CAD RID: 3245
	[Tooltip("The button for deleting this slot.")]
	public Button deleteButton;

	// Token: 0x04000CAE RID: 3246
	[Tooltip("The button for undoing the deletion of this slot.")]
	public Button undoButton;

	// Token: 0x04000CAF RID: 3247
	public bool markedForDeletion;
}
