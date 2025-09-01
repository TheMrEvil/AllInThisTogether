using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000215 RID: 533
public class ToggleSetting : SettingElement
{
	// Token: 0x0600168B RID: 5771 RVA: 0x0008EBE0 File Offset: 0x0008CDE0
	public override void Setup(SettingsPanel.SettingOption settingInfo)
	{
		base.Setup(settingInfo);
		this.defaultValue = (bool)settingInfo.GetDefaultValue();
		bool @bool = Settings.GetBool(this.systemSettingID, this.defaultValue);
		this.ChangeValue(@bool, true, true);
		this.OnDeselected();
		this.OnChangeSystemSetting = (Action<SystemSetting, bool>)Delegate.Combine(this.OnChangeSystemSetting, new Action<SystemSetting, bool>(Settings.SetBool));
		this.OnChangeSystemSetting = (Action<SystemSetting, bool>)Delegate.Combine(this.OnChangeSystemSetting, new Action<SystemSetting, bool>(SettingsPanel.instance.ToggleValueChanged));
	}

	// Token: 0x0600168C RID: 5772 RVA: 0x0008EC6E File Offset: 0x0008CE6E
	public void Toggle()
	{
		this.ChangeValue(!this.curValue, true, false);
	}

	// Token: 0x0600168D RID: 5773 RVA: 0x0008EC84 File Offset: 0x0008CE84
	public void ChangeValue(bool value, bool notify = false, bool force = false)
	{
		if (value == this.curValue && !force)
		{
			return;
		}
		this.curValue = value;
		foreach (GameObject gameObject in this.OnObjects)
		{
			gameObject.SetActive(this.curValue);
		}
		foreach (GameObject gameObject2 in this.OffObjects)
		{
			gameObject2.SetActive(!this.curValue);
		}
		this.ToggleText.text = (this.curValue ? "On" : "Off");
		if (!notify)
		{
			return;
		}
		ToggleSetting.BoolEvent onChangeBase = this.OnChangeBase;
		if (onChangeBase != null)
		{
			onChangeBase.Invoke(this.curValue);
		}
		Action<SystemSetting, bool> onChangeSystemSetting = this.OnChangeSystemSetting;
		if (onChangeSystemSetting == null)
		{
			return;
		}
		onChangeSystemSetting(this.systemSettingID, this.curValue);
	}

	// Token: 0x0600168E RID: 5774 RVA: 0x0008ED8C File Offset: 0x0008CF8C
	internal override void OnSelected()
	{
		this.SelectedGroup.SetActive(true);
		this.DeselectedGroup.SetActive(false);
	}

	// Token: 0x0600168F RID: 5775 RVA: 0x0008EDA6 File Offset: 0x0008CFA6
	internal override void OnDeselected()
	{
		this.SelectedGroup.SetActive(false);
		this.DeselectedGroup.SetActive(true);
	}

	// Token: 0x06001690 RID: 5776 RVA: 0x0008EDC0 File Offset: 0x0008CFC0
	public ToggleSetting()
	{
	}

	// Token: 0x0400161A RID: 5658
	public bool defaultValue;

	// Token: 0x0400161B RID: 5659
	private bool curValue;

	// Token: 0x0400161C RID: 5660
	public TextMeshProUGUI ToggleText;

	// Token: 0x0400161D RID: 5661
	public Action<SystemSetting, bool> OnChangeSystemSetting;

	// Token: 0x0400161E RID: 5662
	public GameObject SelectedGroup;

	// Token: 0x0400161F RID: 5663
	public GameObject DeselectedGroup;

	// Token: 0x04001620 RID: 5664
	public List<GameObject> OnObjects;

	// Token: 0x04001621 RID: 5665
	public List<GameObject> OffObjects;

	// Token: 0x04001622 RID: 5666
	public ToggleSetting.BoolEvent OnChangeBase;

	// Token: 0x04001623 RID: 5667
	private Coroutine curSeq;

	// Token: 0x020005F9 RID: 1529
	[Serializable]
	public class BoolEvent : UnityEvent<bool>
	{
		// Token: 0x060026AB RID: 9899 RVA: 0x000D3F4F File Offset: 0x000D214F
		public BoolEvent()
		{
		}
	}
}
