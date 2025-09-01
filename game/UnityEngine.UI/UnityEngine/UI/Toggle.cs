using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x0200003A RID: 58
	[AddComponentMenu("UI/Toggle", 30)]
	[RequireComponent(typeof(RectTransform))]
	public class Toggle : Selectable, IPointerClickHandler, IEventSystemHandler, ISubmitHandler, ICanvasElement
	{
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x000156EC File Offset: 0x000138EC
		// (set) Token: 0x06000469 RID: 1129 RVA: 0x000156F4 File Offset: 0x000138F4
		public ToggleGroup group
		{
			get
			{
				return this.m_Group;
			}
			set
			{
				this.SetToggleGroup(value, true);
				this.PlayEffect(true);
			}
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00015705 File Offset: 0x00013905
		protected Toggle()
		{
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0001571F File Offset: 0x0001391F
		public virtual void Rebuild(CanvasUpdate executing)
		{
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00015721 File Offset: 0x00013921
		public virtual void LayoutComplete()
		{
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00015723 File Offset: 0x00013923
		public virtual void GraphicUpdateComplete()
		{
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00015725 File Offset: 0x00013925
		protected override void OnDestroy()
		{
			if (this.m_Group != null)
			{
				this.m_Group.EnsureValidState();
			}
			base.OnDestroy();
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00015746 File Offset: 0x00013946
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetToggleGroup(this.m_Group, false);
			this.PlayEffect(true);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00015762 File Offset: 0x00013962
		protected override void OnDisable()
		{
			this.SetToggleGroup(null, false);
			base.OnDisable();
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00015774 File Offset: 0x00013974
		protected override void OnDidApplyAnimationProperties()
		{
			if (this.graphic != null)
			{
				bool flag = !Mathf.Approximately(this.graphic.canvasRenderer.GetColor().a, 0f);
				if (this.m_IsOn != flag)
				{
					this.m_IsOn = flag;
					this.Set(!flag, true);
				}
			}
			base.OnDidApplyAnimationProperties();
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x000157D4 File Offset: 0x000139D4
		private void SetToggleGroup(ToggleGroup newGroup, bool setMemberValue)
		{
			if (this.m_Group != null)
			{
				this.m_Group.UnregisterToggle(this);
			}
			if (setMemberValue)
			{
				this.m_Group = newGroup;
			}
			if (newGroup != null && this.IsActive())
			{
				newGroup.RegisterToggle(this);
			}
			if (newGroup != null && this.isOn && this.IsActive())
			{
				newGroup.NotifyToggleOn(this, true);
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x0001583E File Offset: 0x00013A3E
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x00015846 File Offset: 0x00013A46
		public bool isOn
		{
			get
			{
				return this.m_IsOn;
			}
			set
			{
				this.Set(value, true);
			}
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00015850 File Offset: 0x00013A50
		public void SetIsOnWithoutNotify(bool value)
		{
			this.Set(value, false);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0001585C File Offset: 0x00013A5C
		private void Set(bool value, bool sendCallback = true)
		{
			if (this.m_IsOn == value)
			{
				return;
			}
			this.m_IsOn = value;
			if (this.m_Group != null && this.m_Group.isActiveAndEnabled && this.IsActive() && (this.m_IsOn || (!this.m_Group.AnyTogglesOn() && !this.m_Group.allowSwitchOff)))
			{
				this.m_IsOn = true;
				this.m_Group.NotifyToggleOn(this, sendCallback);
			}
			this.PlayEffect(this.toggleTransition == Toggle.ToggleTransition.None);
			if (sendCallback)
			{
				UISystemProfilerApi.AddMarker("Toggle.value", this);
				this.onValueChanged.Invoke(this.m_IsOn);
			}
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00015901 File Offset: 0x00013B01
		private void PlayEffect(bool instant)
		{
			if (this.graphic == null)
			{
				return;
			}
			this.graphic.CrossFadeAlpha(this.m_IsOn ? 1f : 0f, instant ? 0f : 0.1f, true);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00015941 File Offset: 0x00013B41
		protected override void Start()
		{
			this.PlayEffect(true);
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0001594A File Offset: 0x00013B4A
		private void InternalToggle()
		{
			if (!this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			this.isOn = !this.isOn;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0001596C File Offset: 0x00013B6C
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.InternalToggle();
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0001597D File Offset: 0x00013B7D
		public virtual void OnSubmit(BaseEventData eventData)
		{
			this.InternalToggle();
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00015985 File Offset: 0x00013B85
		Transform ICanvasElement.get_transform()
		{
			return base.transform;
		}

		// Token: 0x04000176 RID: 374
		public Toggle.ToggleTransition toggleTransition = Toggle.ToggleTransition.Fade;

		// Token: 0x04000177 RID: 375
		public Graphic graphic;

		// Token: 0x04000178 RID: 376
		[SerializeField]
		private ToggleGroup m_Group;

		// Token: 0x04000179 RID: 377
		public Toggle.ToggleEvent onValueChanged = new Toggle.ToggleEvent();

		// Token: 0x0400017A RID: 378
		[Tooltip("Is the toggle currently on or off?")]
		[SerializeField]
		private bool m_IsOn;

		// Token: 0x020000AF RID: 175
		public enum ToggleTransition
		{
			// Token: 0x04000313 RID: 787
			None,
			// Token: 0x04000314 RID: 788
			Fade
		}

		// Token: 0x020000B0 RID: 176
		[Serializable]
		public class ToggleEvent : UnityEvent<bool>
		{
			// Token: 0x060006EF RID: 1775 RVA: 0x0001C27C File Offset: 0x0001A47C
			public ToggleEvent()
			{
			}
		}
	}
}
