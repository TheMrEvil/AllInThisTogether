using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000211 RID: 529
public class SettingElement : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IDeselectHandler, IPointerExitHandler, ISelectHandler
{
	// Token: 0x0600165D RID: 5725 RVA: 0x0008CFB9 File Offset: 0x0008B1B9
	private void Awake()
	{
		this.selectable = base.GetComponent<Selectable>();
	}

	// Token: 0x0600165E RID: 5726 RVA: 0x0008CFC8 File Offset: 0x0008B1C8
	public virtual void Setup(SettingsPanel.SettingOption settingInfo)
	{
		foreach (TextMeshProUGUI textMeshProUGUI in this.TitleTexts)
		{
			textMeshProUGUI.text = settingInfo.Label;
		}
		this.systemSettingID = settingInfo.settingID;
		this.infoRef = settingInfo;
	}

	// Token: 0x0600165F RID: 5727 RVA: 0x0008D034 File Offset: 0x0008B234
	public virtual void UpdateStateExternal()
	{
	}

	// Token: 0x06001660 RID: 5728 RVA: 0x0008D036 File Offset: 0x0008B236
	public virtual void Reset()
	{
	}

	// Token: 0x06001661 RID: 5729 RVA: 0x0008D038 File Offset: 0x0008B238
	internal virtual void OnSelected()
	{
	}

	// Token: 0x06001662 RID: 5730 RVA: 0x0008D03A File Offset: 0x0008B23A
	internal virtual void OnDeselected()
	{
	}

	// Token: 0x06001663 RID: 5731 RVA: 0x0008D03C File Offset: 0x0008B23C
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (this.selectable.interactable)
		{
			this.selectable.Select();
		}
	}

	// Token: 0x06001664 RID: 5732 RVA: 0x0008D056 File Offset: 0x0008B256
	public void OnPointerExit(PointerEventData eventData)
	{
		if (EventSystem.current.currentSelectedGameObject == this.selectable.gameObject)
		{
			EventSystem.current.SetSelectedGameObject(null);
		}
	}

	// Token: 0x06001665 RID: 5733 RVA: 0x0008D07F File Offset: 0x0008B27F
	public void OnSelect(BaseEventData eventData)
	{
		SettingsPanel.instance.SettingSelected(this.infoRef);
		this.OnSelected();
	}

	// Token: 0x06001666 RID: 5734 RVA: 0x0008D097 File Offset: 0x0008B297
	public void OnDeselect(BaseEventData eventData)
	{
		if (this.selectable != null)
		{
			this.selectable.OnPointerExit(null);
		}
		this.OnDeselected();
	}

	// Token: 0x06001667 RID: 5735 RVA: 0x0008D0B9 File Offset: 0x0008B2B9
	public SettingElement()
	{
	}

	// Token: 0x040015F1 RID: 5617
	public List<TextMeshProUGUI> TitleTexts;

	// Token: 0x040015F2 RID: 5618
	public SystemSetting systemSettingID;

	// Token: 0x040015F3 RID: 5619
	private Selectable selectable;

	// Token: 0x040015F4 RID: 5620
	private SettingsPanel.SettingOption infoRef;
}
