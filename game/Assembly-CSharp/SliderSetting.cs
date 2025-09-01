using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000213 RID: 531
public class SliderSetting : SettingElement
{
	// Token: 0x17000170 RID: 368
	// (get) Token: 0x06001669 RID: 5737 RVA: 0x0008D0C9 File Offset: 0x0008B2C9
	// (set) Token: 0x0600166A RID: 5738 RVA: 0x0008D0D6 File Offset: 0x0008B2D6
	public float CurrentValue
	{
		get
		{
			return this.slider.value;
		}
		set
		{
			this.ChangeValue(value, true, false);
		}
	}

	// Token: 0x0600166B RID: 5739 RVA: 0x0008D0E1 File Offset: 0x0008B2E1
	private IEnumerator Start()
	{
		UINavReceiver component = base.GetComponent<UINavReceiver>();
		if (component != null)
		{
			UINavReceiver uinavReceiver = component;
			uinavReceiver.OnLeft = (Action)Delegate.Combine(uinavReceiver.OnLeft, new Action(this.ControllerLeft));
			UINavReceiver uinavReceiver2 = component;
			uinavReceiver2.OnRight = (Action)Delegate.Combine(uinavReceiver2.OnRight, new Action(this.ControllerRight));
		}
		yield return new WaitForSeconds(0.5f);
		if (this.SetupOnStart)
		{
			this.Setup(this.InfoData);
		}
		yield break;
	}

	// Token: 0x0600166C RID: 5740 RVA: 0x0008D0F0 File Offset: 0x0008B2F0
	public override void Setup(SettingsPanel.SettingOption settingInfo)
	{
		base.Setup(settingInfo);
		this.defaultValue = (float)settingInfo.GetDefaultValue();
		this.slider.minValue = settingInfo.MinVal;
		this.slider.maxValue = settingInfo.MaxVal;
		this.slider.wholeNumbers = settingInfo.WholeNumbers;
		if (this.DefaultDisplay != null)
		{
			this.sliderRect = this.slider.GetComponent<RectTransform>();
			float num = (this.defaultValue - this.slider.minValue) / (this.slider.maxValue - this.slider.minValue);
			num *= this.sliderRect.rect.width;
			this.DefaultDisplay.anchoredPosition = new Vector2(num, this.DefaultDisplay.anchoredPosition.y);
		}
		this.InitializeSlider();
	}

	// Token: 0x0600166D RID: 5741 RVA: 0x0008D1D0 File Offset: 0x0008B3D0
	public void InitializeSlider()
	{
		this.slider.onValueChanged.AddListener(new UnityAction<float>(this.ValueChanged));
		float @float = Settings.GetFloat(this.systemSettingID, this.defaultValue);
		this.ChangeValue(@float, false, true);
		this.OnChangeSystemSetting = (Action<SystemSetting, float>)Delegate.Combine(this.OnChangeSystemSetting, new Action<SystemSetting, float>(Settings.SetFloat));
		this.OnChangeSystemSetting = (Action<SystemSetting, float>)Delegate.Combine(this.OnChangeSystemSetting, new Action<SystemSetting, float>(SettingsPanel.instance.SliderValueChanged));
	}

	// Token: 0x0600166E RID: 5742 RVA: 0x0008D25C File Offset: 0x0008B45C
	public override void UpdateStateExternal()
	{
		float @float = Settings.GetFloat(this.systemSettingID, this.defaultValue);
		this.ChangeValue(@float, false, true);
	}

	// Token: 0x0600166F RID: 5743 RVA: 0x0008D284 File Offset: 0x0008B484
	public void ChangeValue(float value, bool notify = false, bool force = false)
	{
		if (value == this.slider.value && !force)
		{
			return;
		}
		if (notify)
		{
			this.slider.value = value;
			return;
		}
		this.slider.SetValueWithoutNotify(value);
		this.UpdateValueText(value);
	}

	// Token: 0x06001670 RID: 5744 RVA: 0x0008D2BC File Offset: 0x0008B4BC
	private void ValueChanged(float value)
	{
		this.UpdateValueText(value);
		SliderSetting.FloatEvent onChangeBase = this.OnChangeBase;
		if (onChangeBase != null)
		{
			onChangeBase.Invoke(value);
		}
		Action<SystemSetting, float> onChangeSystemSetting = this.OnChangeSystemSetting;
		if (onChangeSystemSetting == null)
		{
			return;
		}
		onChangeSystemSetting(this.systemSettingID, value);
	}

	// Token: 0x06001671 RID: 5745 RVA: 0x0008D2FC File Offset: 0x0008B4FC
	private void UpdateValueText(float value)
	{
		if (this.valueLabel != null)
		{
			if (value == (float)((int)value))
			{
				this.valueLabel.text = ((int)value).ToString() + this.valueSuffix;
				return;
			}
			this.valueLabel.text = value.ToString("f1") + this.valueSuffix;
		}
	}

	// Token: 0x06001672 RID: 5746 RVA: 0x0008D360 File Offset: 0x0008B560
	private void ControllerRight()
	{
		float num = this.slider.value;
		float num2 = (this.slider.maxValue - this.slider.minValue) / 10f;
		if (this.slider.wholeNumbers && num2 < 1f)
		{
			num2 = 1f;
		}
		num += num2;
		num = Mathf.Clamp(num, this.slider.minValue, this.slider.maxValue);
		this.ChangeValue(num, true, false);
	}

	// Token: 0x06001673 RID: 5747 RVA: 0x0008D3DC File Offset: 0x0008B5DC
	private void ControllerLeft()
	{
		float num = this.slider.value;
		float num2 = (this.slider.maxValue - this.slider.minValue) / 10f;
		if (this.slider.wholeNumbers && num2 < 1f)
		{
			num2 = 1f;
		}
		num -= num2;
		num = Mathf.Clamp(num, this.slider.minValue, this.slider.maxValue);
		this.ChangeValue(num, true, false);
	}

	// Token: 0x06001674 RID: 5748 RVA: 0x0008D458 File Offset: 0x0008B658
	internal override void OnSelected()
	{
		this.BarImage.sprite = this.SelectedBar;
		this.FillImage.sprite = this.SelectedFill;
		this.HandleImage.sprite = this.SelectedHandle;
	}

	// Token: 0x06001675 RID: 5749 RVA: 0x0008D48D File Offset: 0x0008B68D
	internal override void OnDeselected()
	{
		this.BarImage.sprite = this.IdleBar;
		this.FillImage.sprite = this.IdleFill;
		this.HandleImage.sprite = this.IdleHandle;
	}

	// Token: 0x06001676 RID: 5750 RVA: 0x0008D4C2 File Offset: 0x0008B6C2
	public void RestoreDefaults()
	{
		this.ChangeValue(this.defaultValue, true, false);
	}

	// Token: 0x06001677 RID: 5751 RVA: 0x0008D4D2 File Offset: 0x0008B6D2
	public SliderSetting()
	{
	}

	// Token: 0x040015F5 RID: 5621
	public Slider slider;

	// Token: 0x040015F6 RID: 5622
	private RectTransform sliderRect;

	// Token: 0x040015F7 RID: 5623
	public TextMeshProUGUI valueLabel;

	// Token: 0x040015F8 RID: 5624
	public string valueSuffix;

	// Token: 0x040015F9 RID: 5625
	public SliderSetting.FloatEvent OnChangeBase;

	// Token: 0x040015FA RID: 5626
	public Action<SystemSetting, float> OnChangeSystemSetting;

	// Token: 0x040015FB RID: 5627
	[Header("Selection")]
	public Image BarImage;

	// Token: 0x040015FC RID: 5628
	public Image FillImage;

	// Token: 0x040015FD RID: 5629
	public Image HandleImage;

	// Token: 0x040015FE RID: 5630
	public RectTransform DefaultDisplay;

	// Token: 0x040015FF RID: 5631
	public Sprite SelectedBar;

	// Token: 0x04001600 RID: 5632
	public Sprite IdleBar;

	// Token: 0x04001601 RID: 5633
	public Sprite SelectedFill;

	// Token: 0x04001602 RID: 5634
	public Sprite IdleFill;

	// Token: 0x04001603 RID: 5635
	public Sprite SelectedHandle;

	// Token: 0x04001604 RID: 5636
	public Sprite IdleHandle;

	// Token: 0x04001605 RID: 5637
	public float defaultValue = 0.5f;

	// Token: 0x04001606 RID: 5638
	public bool SetupOnStart;

	// Token: 0x04001607 RID: 5639
	public SettingsPanel.SettingOption InfoData;

	// Token: 0x020005F6 RID: 1526
	[Serializable]
	public class FloatEvent : UnityEvent<float>
	{
		// Token: 0x060026A4 RID: 9892 RVA: 0x000D3E6B File Offset: 0x000D206B
		public FloatEvent()
		{
		}
	}

	// Token: 0x020005F7 RID: 1527
	[CompilerGenerated]
	private sealed class <Start>d__23 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060026A5 RID: 9893 RVA: 0x000D3E73 File Offset: 0x000D2073
		[DebuggerHidden]
		public <Start>d__23(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060026A6 RID: 9894 RVA: 0x000D3E82 File Offset: 0x000D2082
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060026A7 RID: 9895 RVA: 0x000D3E84 File Offset: 0x000D2084
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			SliderSetting sliderSetting = this;
			if (num == 0)
			{
				this.<>1__state = -1;
				UINavReceiver component = sliderSetting.GetComponent<UINavReceiver>();
				if (component != null)
				{
					UINavReceiver uinavReceiver = component;
					uinavReceiver.OnLeft = (Action)Delegate.Combine(uinavReceiver.OnLeft, new Action(sliderSetting.ControllerLeft));
					UINavReceiver uinavReceiver2 = component;
					uinavReceiver2.OnRight = (Action)Delegate.Combine(uinavReceiver2.OnRight, new Action(sliderSetting.ControllerRight));
				}
				this.<>2__current = new WaitForSeconds(0.5f);
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			if (sliderSetting.SetupOnStart)
			{
				sliderSetting.Setup(sliderSetting.InfoData);
			}
			return false;
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x060026A8 RID: 9896 RVA: 0x000D3F38 File Offset: 0x000D2138
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060026A9 RID: 9897 RVA: 0x000D3F40 File Offset: 0x000D2140
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x060026AA RID: 9898 RVA: 0x000D3F47 File Offset: 0x000D2147
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002954 RID: 10580
		private int <>1__state;

		// Token: 0x04002955 RID: 10581
		private object <>2__current;

		// Token: 0x04002956 RID: 10582
		public SliderSetting <>4__this;
	}
}
