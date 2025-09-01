using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000210 RID: 528
public class SelectorSetting : SettingElement
{
	// Token: 0x06001655 RID: 5717 RVA: 0x0008CC1C File Offset: 0x0008AE1C
	public override void Setup(SettingsPanel.SettingOption settingInfo)
	{
		if (settingInfo != null)
		{
			base.Setup(settingInfo);
			this.Options = settingInfo.options.ToArray();
			this.defaultSelection = (int)settingInfo.GetDefaultValue();
		}
		if (this.ItemIndicatorRef != null)
		{
			this.SetupIndicators();
		}
		this.OnChangeSystemSetting = (Action<SystemSetting, int>)Delegate.Combine(this.OnChangeSystemSetting, new Action<SystemSetting, int>(Settings.SetInt));
		this.OnChangeSystemSetting = (Action<SystemSetting, int>)Delegate.Combine(this.OnChangeSystemSetting, new Action<SystemSetting, int>(SettingsPanel.instance.SelectorValueChanged));
		int @int = Settings.GetInt(this.systemSettingID, this.defaultSelection);
		this.SetSelection(@int, false);
		UINavReceiver component = base.GetComponent<UINavReceiver>();
		if (component != null)
		{
			UINavReceiver uinavReceiver = component;
			uinavReceiver.OnLeft = (Action)Delegate.Combine(uinavReceiver.OnLeft, new Action(this.Prev));
			UINavReceiver uinavReceiver2 = component;
			uinavReceiver2.OnRight = (Action)Delegate.Combine(uinavReceiver2.OnRight, new Action(this.Next));
		}
	}

	// Token: 0x06001656 RID: 5718 RVA: 0x0008CD20 File Offset: 0x0008AF20
	private void SetupIndicators()
	{
		this.ItemIndicatorRef.gameObject.SetActive(false);
		foreach (Image image in this.itemIndicators)
		{
			UnityEngine.Object.Destroy(image.gameObject);
		}
		this.itemIndicators.Clear();
		foreach (string text in this.Options)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemIndicatorRef.gameObject, this.ItemIndicatorRef.transform.parent);
			gameObject.SetActive(true);
			Image component = gameObject.GetComponent<Image>();
			this.itemIndicators.Add(component);
		}
	}

	// Token: 0x06001657 RID: 5719 RVA: 0x0008CDE4 File Offset: 0x0008AFE4
	public void Next()
	{
		if (!this.interactable)
		{
			return;
		}
		this.curID++;
		if (this.curID >= this.Options.Length)
		{
			this.curID = 0;
		}
		this.SetSelection(this.curID, true);
	}

	// Token: 0x06001658 RID: 5720 RVA: 0x0008CE21 File Offset: 0x0008B021
	public int GetValue()
	{
		return this.curID;
	}

	// Token: 0x06001659 RID: 5721 RVA: 0x0008CE29 File Offset: 0x0008B029
	public void Prev()
	{
		if (!this.interactable)
		{
			return;
		}
		this.curID--;
		if (this.curID < 0)
		{
			this.curID = this.Options.Length - 1;
		}
		this.SetSelection(this.curID, true);
	}

	// Token: 0x0600165A RID: 5722 RVA: 0x0008CE68 File Offset: 0x0008B068
	public void SetSelection(int id, bool notify = true)
	{
		if (!this.interactable)
		{
			return;
		}
		if (id != this.curID)
		{
			this.curID = id;
		}
		if (this.curID < 0 || this.curID >= this.Options.Length)
		{
			this.curID = 0;
		}
		this.UpdateText();
		if (!notify)
		{
			return;
		}
		SelectorSetting.IntEvent onChangeBase = this.OnChangeBase;
		if (onChangeBase != null)
		{
			onChangeBase.Invoke(this.curID);
		}
		Action<SystemSetting, int> onChangeSystemSetting = this.OnChangeSystemSetting;
		if (onChangeSystemSetting == null)
		{
			return;
		}
		onChangeSystemSetting(this.systemSettingID, this.curID);
	}

	// Token: 0x0600165B RID: 5723 RVA: 0x0008CEEC File Offset: 0x0008B0EC
	public void UpdateText()
	{
		if (this.curID >= this.Options.Length)
		{
			return;
		}
		this.valueLabel.text = this.Options[this.curID];
		if (this.extraValueLabel != null)
		{
			this.extraValueLabel.text = this.Options[this.curID];
		}
		if (this.itemIndicators.Count > 0)
		{
			for (int i = 0; i < this.itemIndicators.Count; i++)
			{
				Color color = this.ItemIndicatorRef.color;
				if (this.curID != i)
				{
					color.a = 0.075f;
				}
				this.itemIndicators[i].color = color;
			}
		}
	}

	// Token: 0x0600165C RID: 5724 RVA: 0x0008CF9F File Offset: 0x0008B19F
	public SelectorSetting()
	{
	}

	// Token: 0x040015E7 RID: 5607
	public TextMeshProUGUI valueLabel;

	// Token: 0x040015E8 RID: 5608
	public TextMeshProUGUI extraValueLabel;

	// Token: 0x040015E9 RID: 5609
	public string[] Options;

	// Token: 0x040015EA RID: 5610
	private int curID;

	// Token: 0x040015EB RID: 5611
	public Image ItemIndicatorRef;

	// Token: 0x040015EC RID: 5612
	private List<Image> itemIndicators = new List<Image>();

	// Token: 0x040015ED RID: 5613
	public SelectorSetting.IntEvent OnChangeBase;

	// Token: 0x040015EE RID: 5614
	public Action<SystemSetting, int> OnChangeSystemSetting;

	// Token: 0x040015EF RID: 5615
	public int defaultSelection;

	// Token: 0x040015F0 RID: 5616
	[NonSerialized]
	public bool interactable = true;

	// Token: 0x020005F5 RID: 1525
	[Serializable]
	public class IntEvent : UnityEvent<int>
	{
		// Token: 0x060026A3 RID: 9891 RVA: 0x000D3E63 File Offset: 0x000D2063
		public IntEvent()
		{
		}
	}
}
