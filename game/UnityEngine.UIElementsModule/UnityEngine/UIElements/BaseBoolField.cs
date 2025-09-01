using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000119 RID: 281
	public abstract class BaseBoolField : BaseField<bool>
	{
		// Token: 0x0600092C RID: 2348 RVA: 0x000243A0 File Offset: 0x000225A0
		public BaseBoolField(string label) : base(label, null)
		{
			this.m_CheckMark = new VisualElement
			{
				name = "unity-checkmark",
				pickingMode = PickingMode.Ignore
			};
			base.visualInput.Add(this.m_CheckMark);
			base.visualInput.pickingMode = PickingMode.Position;
			this.text = null;
			this.AddManipulator(this.m_Clickable = new Clickable(new Action<EventBase>(this.OnClickEvent)));
			base.RegisterCallback<NavigationSubmitEvent>(new EventCallback<NavigationSubmitEvent>(this.OnNavigationSubmit), TrickleDown.NoTrickleDown);
			base.RegisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.OnKeyDown), TrickleDown.NoTrickleDown);
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x00024446 File Offset: 0x00022646
		private void OnNavigationSubmit(NavigationSubmitEvent evt)
		{
			this.ToggleValue();
			evt.StopPropagation();
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x00024458 File Offset: 0x00022658
		private void OnKeyDown(KeyDownEvent evt)
		{
			IPanel panel = base.panel;
			bool flag = panel == null || panel.contextType != ContextType.Editor;
			if (!flag)
			{
				bool flag2 = evt.keyCode == KeyCode.KeypadEnter || evt.keyCode == KeyCode.Return || evt.keyCode == KeyCode.Space;
				if (flag2)
				{
					this.ToggleValue();
					evt.StopPropagation();
				}
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x000244BC File Offset: 0x000226BC
		// (set) Token: 0x06000930 RID: 2352 RVA: 0x000244E0 File Offset: 0x000226E0
		public string text
		{
			get
			{
				Label label = this.m_Label;
				return (label != null) ? label.text : null;
			}
			set
			{
				bool flag = !string.IsNullOrEmpty(value);
				if (flag)
				{
					bool flag2 = this.m_Label == null;
					if (flag2)
					{
						this.InitLabel();
					}
					this.m_Label.text = value;
				}
				else
				{
					bool flag3 = this.m_Label != null;
					if (flag3)
					{
						this.m_Label.RemoveFromHierarchy();
						this.m_Label = null;
					}
				}
			}
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x00024544 File Offset: 0x00022744
		protected virtual void InitLabel()
		{
			this.m_Label = new Label
			{
				pickingMode = PickingMode.Ignore
			};
			base.visualInput.Add(this.m_Label);
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0002456C File Offset: 0x0002276C
		public override void SetValueWithoutNotify(bool newValue)
		{
			if (newValue)
			{
				base.visualInput.pseudoStates |= PseudoStates.Checked;
				base.pseudoStates |= PseudoStates.Checked;
			}
			else
			{
				base.visualInput.pseudoStates &= ~PseudoStates.Checked;
				base.pseudoStates &= ~PseudoStates.Checked;
			}
			base.SetValueWithoutNotify(newValue);
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x000245D8 File Offset: 0x000227D8
		private void OnClickEvent(EventBase evt)
		{
			bool flag = evt.eventTypeId == EventBase<MouseUpEvent>.TypeId();
			if (flag)
			{
				IMouseEvent mouseEvent = (IMouseEvent)evt;
				bool flag2 = mouseEvent.button == 0;
				if (flag2)
				{
					this.ToggleValue();
				}
			}
			else
			{
				bool flag3 = evt.eventTypeId == EventBase<PointerUpEvent>.TypeId() || evt.eventTypeId == EventBase<ClickEvent>.TypeId();
				if (flag3)
				{
					IPointerEvent pointerEvent = (IPointerEvent)evt;
					bool flag4 = pointerEvent.button == 0;
					if (flag4)
					{
						this.ToggleValue();
					}
				}
			}
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0002465C File Offset: 0x0002285C
		protected virtual void ToggleValue()
		{
			this.value = !this.value;
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x00024670 File Offset: 0x00022870
		protected override void UpdateMixedValueContent()
		{
			bool showMixedValue = base.showMixedValue;
			if (showMixedValue)
			{
				base.visualInput.pseudoStates &= ~PseudoStates.Checked;
				base.pseudoStates &= ~PseudoStates.Checked;
				this.m_CheckMark.RemoveFromHierarchy();
				base.visualInput.Add(base.mixedValueLabel);
				this.m_OriginalText = this.text;
				this.text = "";
			}
			else
			{
				base.mixedValueLabel.RemoveFromHierarchy();
				base.visualInput.Add(this.m_CheckMark);
				bool flag = this.m_OriginalText != null;
				if (flag)
				{
					this.text = this.m_OriginalText;
				}
			}
		}

		// Token: 0x040003CA RID: 970
		protected Label m_Label;

		// Token: 0x040003CB RID: 971
		protected readonly VisualElement m_CheckMark;

		// Token: 0x040003CC RID: 972
		internal Clickable m_Clickable;

		// Token: 0x040003CD RID: 973
		private string m_OriginalText;
	}
}
