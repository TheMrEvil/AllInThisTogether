using System;
using System.Runtime.CompilerServices;
using UnityEngine.EventSystems;

namespace UnityEngine.UIElements
{
	// Token: 0x0200004A RID: 74
	[AddComponentMenu("UI Toolkit/Panel Event Handler (UI Toolkit)")]
	public class PanelEventHandler : UIBehaviour, IPointerMoveHandler, IEventSystemHandler, IPointerUpHandler, IPointerDownHandler, ISubmitHandler, ICancelHandler, IMoveHandler, IScrollHandler, ISelectHandler, IDeselectHandler, IPointerExitHandler, IPointerEnterHandler, IRuntimePanelComponent
	{
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x0001723D File Offset: 0x0001543D
		// (set) Token: 0x060004F8 RID: 1272 RVA: 0x00017248 File Offset: 0x00015448
		public IPanel panel
		{
			get
			{
				return this.m_Panel;
			}
			set
			{
				BaseRuntimePanel baseRuntimePanel = (BaseRuntimePanel)value;
				if (this.m_Panel != baseRuntimePanel)
				{
					this.UnregisterCallbacks();
					this.m_Panel = baseRuntimePanel;
					this.RegisterCallbacks();
				}
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x00017278 File Offset: 0x00015478
		private GameObject selectableGameObject
		{
			get
			{
				BaseRuntimePanel panel = this.m_Panel;
				if (panel == null)
				{
					return null;
				}
				return panel.selectableGameObject;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x0001728B File Offset: 0x0001548B
		private EventSystem eventSystem
		{
			get
			{
				return UIElementsRuntimeUtility.activeEventSystem as EventSystem;
			}
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00017297 File Offset: 0x00015497
		protected override void OnEnable()
		{
			base.OnEnable();
			this.RegisterCallbacks();
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x000172A5 File Offset: 0x000154A5
		protected override void OnDisable()
		{
			base.OnDisable();
			this.UnregisterCallbacks();
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x000172B4 File Offset: 0x000154B4
		private void RegisterCallbacks()
		{
			if (this.m_Panel != null)
			{
				this.m_Panel.destroyed += this.OnPanelDestroyed;
				this.m_Panel.visualTree.RegisterCallback<FocusEvent>(new EventCallback<FocusEvent>(this.OnElementFocus), TrickleDown.TrickleDown);
				this.m_Panel.visualTree.RegisterCallback<BlurEvent>(new EventCallback<BlurEvent>(this.OnElementBlur), TrickleDown.TrickleDown);
			}
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0001731C File Offset: 0x0001551C
		private void UnregisterCallbacks()
		{
			if (this.m_Panel != null)
			{
				this.m_Panel.destroyed -= this.OnPanelDestroyed;
				this.m_Panel.visualTree.UnregisterCallback<FocusEvent>(new EventCallback<FocusEvent>(this.OnElementFocus), TrickleDown.TrickleDown);
				this.m_Panel.visualTree.UnregisterCallback<BlurEvent>(new EventCallback<BlurEvent>(this.OnElementBlur), TrickleDown.TrickleDown);
			}
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00017382 File Offset: 0x00015582
		private void OnPanelDestroyed()
		{
			this.panel = null;
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0001738B File Offset: 0x0001558B
		private void OnElementFocus(FocusEvent e)
		{
			if (!this.m_Selecting && this.eventSystem != null)
			{
				this.eventSystem.SetSelectedGameObject(this.selectableGameObject);
			}
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x000173B4 File Offset: 0x000155B4
		private void OnElementBlur(BlurEvent e)
		{
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x000173B8 File Offset: 0x000155B8
		public void OnSelect(BaseEventData eventData)
		{
			this.m_Selecting = true;
			try
			{
				BaseRuntimePanel panel = this.m_Panel;
				if (panel != null)
				{
					panel.Focus();
				}
			}
			finally
			{
				this.m_Selecting = false;
			}
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x000173F8 File Offset: 0x000155F8
		public void OnDeselect(BaseEventData eventData)
		{
			BaseRuntimePanel panel = this.m_Panel;
			if (panel == null)
			{
				return;
			}
			panel.Blur();
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0001740C File Offset: 0x0001560C
		public void OnPointerMove(PointerEventData eventData)
		{
			if (this.m_Panel == null || !this.ReadPointerData(this.m_PointerEvent, eventData, PanelEventHandler.PointerEventType.Default))
			{
				return;
			}
			using (PointerMoveEvent pooled = PointerEventBase<PointerMoveEvent>.GetPooled(this.m_PointerEvent))
			{
				this.SendEvent(pooled, eventData);
			}
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00017464 File Offset: 0x00015664
		public void OnPointerUp(PointerEventData eventData)
		{
			if (this.m_Panel == null || !this.ReadPointerData(this.m_PointerEvent, eventData, PanelEventHandler.PointerEventType.Up))
			{
				return;
			}
			using (PointerUpEvent pooled = PointerEventBase<PointerUpEvent>.GetPooled(this.m_PointerEvent))
			{
				this.SendEvent(pooled, eventData);
				if (pooled.pressedButtons == 0)
				{
					PointerDeviceState.SetPlayerPanelWithSoftPointerCapture(pooled.pointerId, null);
				}
			}
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x000174D0 File Offset: 0x000156D0
		public void OnPointerDown(PointerEventData eventData)
		{
			if (this.m_Panel == null || !this.ReadPointerData(this.m_PointerEvent, eventData, PanelEventHandler.PointerEventType.Down))
			{
				return;
			}
			if (this.eventSystem != null)
			{
				this.eventSystem.SetSelectedGameObject(this.selectableGameObject);
			}
			using (PointerDownEvent pooled = PointerEventBase<PointerDownEvent>.GetPooled(this.m_PointerEvent))
			{
				this.SendEvent(pooled, eventData);
				PointerDeviceState.SetPlayerPanelWithSoftPointerCapture(pooled.pointerId, this.m_Panel);
			}
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00017558 File Offset: 0x00015758
		public void OnPointerExit(PointerEventData eventData)
		{
			if (this.m_Panel == null || !this.ReadPointerData(this.m_PointerEvent, eventData, PanelEventHandler.PointerEventType.Default))
			{
				return;
			}
			if (eventData.pointerCurrentRaycast.gameObject == base.gameObject && eventData.pointerPressRaycast.gameObject != base.gameObject && this.m_PointerEvent.pointerId != PointerId.mousePointerId)
			{
				using (PointerCancelEvent pooled = PointerEventBase<PointerCancelEvent>.GetPooled(this.m_PointerEvent))
				{
					this.SendEvent(pooled, eventData);
				}
			}
			this.m_Panel.PointerLeavesPanel(this.m_PointerEvent.pointerId, this.m_PointerEvent.position);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0001761C File Offset: 0x0001581C
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (this.m_Panel == null || !this.ReadPointerData(this.m_PointerEvent, eventData, PanelEventHandler.PointerEventType.Default))
			{
				return;
			}
			this.m_Panel.PointerEntersPanel(this.m_PointerEvent.pointerId, this.m_PointerEvent.position);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00017668 File Offset: 0x00015868
		public void OnSubmit(BaseEventData eventData)
		{
			if (this.m_Panel == null)
			{
				return;
			}
			this.ProcessImguiEvents(true);
			using (NavigationSubmitEvent pooled = EventBase<NavigationSubmitEvent>.GetPooled())
			{
				this.SendEvent(pooled, eventData);
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x000176B0 File Offset: 0x000158B0
		public void OnCancel(BaseEventData eventData)
		{
			if (this.m_Panel == null)
			{
				return;
			}
			this.ProcessImguiEvents(true);
			using (NavigationCancelEvent pooled = EventBase<NavigationCancelEvent>.GetPooled())
			{
				this.SendEvent(pooled, eventData);
			}
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x000176F8 File Offset: 0x000158F8
		public void OnMove(AxisEventData eventData)
		{
			if (this.m_Panel == null)
			{
				return;
			}
			this.ProcessImguiEvents(true);
			using (NavigationMoveEvent pooled = NavigationMoveEvent.GetPooled(eventData.moveVector))
			{
				this.SendEvent(pooled, eventData);
			}
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00017748 File Offset: 0x00015948
		public void OnScroll(PointerEventData eventData)
		{
			if (this.m_Panel == null || !this.ReadPointerData(this.m_PointerEvent, eventData, PanelEventHandler.PointerEventType.Default))
			{
				return;
			}
			Vector2 vector = eventData.scrollDelta;
			vector.y = -vector.y;
			vector /= 20f;
			using (WheelEvent pooled = WheelEvent.GetPooled(vector, this.m_PointerEvent))
			{
				this.SendEvent(pooled, eventData);
			}
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x000177C8 File Offset: 0x000159C8
		private void SendEvent(EventBase e, BaseEventData sourceEventData)
		{
			this.m_Panel.SendEvent(e, DispatchMode.Default);
			if (e.isPropagationStopped)
			{
				sourceEventData.Use();
			}
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x000177E5 File Offset: 0x000159E5
		private void SendEvent(EventBase e, Event sourceEvent)
		{
			this.m_Panel.SendEvent(e, DispatchMode.Default);
			if (e.isPropagationStopped)
			{
				sourceEvent.Use();
			}
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00017802 File Offset: 0x00015A02
		private void Update()
		{
			if (this.m_Panel != null && this.eventSystem != null && this.eventSystem.currentSelectedGameObject == this.selectableGameObject)
			{
				this.ProcessImguiEvents(true);
			}
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00017839 File Offset: 0x00015A39
		private void LateUpdate()
		{
			this.ProcessImguiEvents(false);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00017844 File Offset: 0x00015A44
		private void ProcessImguiEvents(bool isSelected)
		{
			bool flag = true;
			while (Event.PopEvent(this.m_Event))
			{
				if (this.m_Event.type != EventType.Ignore && this.m_Event.type != EventType.Repaint && this.m_Event.type != EventType.Layout)
				{
					PanelEventHandler.s_Modifiers = (flag ? this.m_Event.modifiers : (PanelEventHandler.s_Modifiers | this.m_Event.modifiers));
					flag = false;
					if (isSelected)
					{
						this.ProcessKeyboardEvent(this.m_Event);
						if (this.m_Event.type != EventType.Used)
						{
							this.ProcessTabEvent(this.m_Event);
						}
					}
				}
			}
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x000178E2 File Offset: 0x00015AE2
		private void ProcessKeyboardEvent(Event e)
		{
			if (e.type == EventType.KeyUp)
			{
				this.SendKeyUpEvent(e);
				return;
			}
			if (e.type == EventType.KeyDown)
			{
				this.SendKeyDownEvent(e);
			}
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00017905 File Offset: 0x00015B05
		private void ProcessTabEvent(Event e)
		{
			if (e.type == EventType.KeyDown && e.character == '\t')
			{
				this.SendTabEvent(e, e.shift ? -1 : 1);
			}
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00017930 File Offset: 0x00015B30
		private void SendTabEvent(Event e, int direction)
		{
			using (NavigationTabEvent pooled = NavigationTabEvent.GetPooled(direction))
			{
				this.SendEvent(pooled, e);
			}
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00017968 File Offset: 0x00015B68
		private void SendKeyUpEvent(Event e)
		{
			using (KeyUpEvent pooled = KeyboardEventBase<KeyUpEvent>.GetPooled('\0', e.keyCode, e.modifiers))
			{
				this.SendEvent(pooled, e);
			}
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x000179AC File Offset: 0x00015BAC
		private void SendKeyDownEvent(Event e)
		{
			using (KeyDownEvent pooled = KeyboardEventBase<KeyDownEvent>.GetPooled(e.character, e.keyCode, e.modifiers))
			{
				this.SendEvent(pooled, e);
			}
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x000179F8 File Offset: 0x00015BF8
		private bool ReadPointerData(PanelEventHandler.PointerEvent pe, PointerEventData eventData, PanelEventHandler.PointerEventType eventType = PanelEventHandler.PointerEventType.Default)
		{
			if (this.eventSystem == null || this.eventSystem.currentInputModule == null)
			{
				return false;
			}
			pe.Read(this, eventData, eventType);
			Vector2 v;
			Vector2 v2;
			this.m_Panel.ScreenToPanel(pe.position, pe.deltaPosition, out v, out v2, true);
			pe.SetPosition(v, v2);
			return true;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00017A6B File Offset: 0x00015C6B
		public PanelEventHandler()
		{
		}

		// Token: 0x040001A5 RID: 421
		private BaseRuntimePanel m_Panel;

		// Token: 0x040001A6 RID: 422
		private readonly PanelEventHandler.PointerEvent m_PointerEvent = new PanelEventHandler.PointerEvent();

		// Token: 0x040001A7 RID: 423
		private bool m_Selecting;

		// Token: 0x040001A8 RID: 424
		private Event m_Event = new Event();

		// Token: 0x040001A9 RID: 425
		private static EventModifiers s_Modifiers;

		// Token: 0x020000BC RID: 188
		private enum PointerEventType
		{
			// Token: 0x04000321 RID: 801
			Default,
			// Token: 0x04000322 RID: 802
			Down,
			// Token: 0x04000323 RID: 803
			Up
		}

		// Token: 0x020000BD RID: 189
		private class PointerEvent : IPointerEvent
		{
			// Token: 0x170001DA RID: 474
			// (get) Token: 0x06000714 RID: 1812 RVA: 0x0001C3D2 File Offset: 0x0001A5D2
			// (set) Token: 0x06000715 RID: 1813 RVA: 0x0001C3DA File Offset: 0x0001A5DA
			public int pointerId
			{
				[CompilerGenerated]
				get
				{
					return this.<pointerId>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<pointerId>k__BackingField = value;
				}
			}

			// Token: 0x170001DB RID: 475
			// (get) Token: 0x06000716 RID: 1814 RVA: 0x0001C3E3 File Offset: 0x0001A5E3
			// (set) Token: 0x06000717 RID: 1815 RVA: 0x0001C3EB File Offset: 0x0001A5EB
			public string pointerType
			{
				[CompilerGenerated]
				get
				{
					return this.<pointerType>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<pointerType>k__BackingField = value;
				}
			}

			// Token: 0x170001DC RID: 476
			// (get) Token: 0x06000718 RID: 1816 RVA: 0x0001C3F4 File Offset: 0x0001A5F4
			// (set) Token: 0x06000719 RID: 1817 RVA: 0x0001C3FC File Offset: 0x0001A5FC
			public bool isPrimary
			{
				[CompilerGenerated]
				get
				{
					return this.<isPrimary>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<isPrimary>k__BackingField = value;
				}
			}

			// Token: 0x170001DD RID: 477
			// (get) Token: 0x0600071A RID: 1818 RVA: 0x0001C405 File Offset: 0x0001A605
			// (set) Token: 0x0600071B RID: 1819 RVA: 0x0001C40D File Offset: 0x0001A60D
			public int button
			{
				[CompilerGenerated]
				get
				{
					return this.<button>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<button>k__BackingField = value;
				}
			}

			// Token: 0x170001DE RID: 478
			// (get) Token: 0x0600071C RID: 1820 RVA: 0x0001C416 File Offset: 0x0001A616
			// (set) Token: 0x0600071D RID: 1821 RVA: 0x0001C41E File Offset: 0x0001A61E
			public int pressedButtons
			{
				[CompilerGenerated]
				get
				{
					return this.<pressedButtons>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<pressedButtons>k__BackingField = value;
				}
			}

			// Token: 0x170001DF RID: 479
			// (get) Token: 0x0600071E RID: 1822 RVA: 0x0001C427 File Offset: 0x0001A627
			// (set) Token: 0x0600071F RID: 1823 RVA: 0x0001C42F File Offset: 0x0001A62F
			public Vector3 position
			{
				[CompilerGenerated]
				get
				{
					return this.<position>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<position>k__BackingField = value;
				}
			}

			// Token: 0x170001E0 RID: 480
			// (get) Token: 0x06000720 RID: 1824 RVA: 0x0001C438 File Offset: 0x0001A638
			// (set) Token: 0x06000721 RID: 1825 RVA: 0x0001C440 File Offset: 0x0001A640
			public Vector3 localPosition
			{
				[CompilerGenerated]
				get
				{
					return this.<localPosition>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<localPosition>k__BackingField = value;
				}
			}

			// Token: 0x170001E1 RID: 481
			// (get) Token: 0x06000722 RID: 1826 RVA: 0x0001C449 File Offset: 0x0001A649
			// (set) Token: 0x06000723 RID: 1827 RVA: 0x0001C451 File Offset: 0x0001A651
			public Vector3 deltaPosition
			{
				[CompilerGenerated]
				get
				{
					return this.<deltaPosition>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<deltaPosition>k__BackingField = value;
				}
			}

			// Token: 0x170001E2 RID: 482
			// (get) Token: 0x06000724 RID: 1828 RVA: 0x0001C45A File Offset: 0x0001A65A
			// (set) Token: 0x06000725 RID: 1829 RVA: 0x0001C462 File Offset: 0x0001A662
			public float deltaTime
			{
				[CompilerGenerated]
				get
				{
					return this.<deltaTime>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<deltaTime>k__BackingField = value;
				}
			}

			// Token: 0x170001E3 RID: 483
			// (get) Token: 0x06000726 RID: 1830 RVA: 0x0001C46B File Offset: 0x0001A66B
			// (set) Token: 0x06000727 RID: 1831 RVA: 0x0001C473 File Offset: 0x0001A673
			public int clickCount
			{
				[CompilerGenerated]
				get
				{
					return this.<clickCount>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<clickCount>k__BackingField = value;
				}
			}

			// Token: 0x170001E4 RID: 484
			// (get) Token: 0x06000728 RID: 1832 RVA: 0x0001C47C File Offset: 0x0001A67C
			// (set) Token: 0x06000729 RID: 1833 RVA: 0x0001C484 File Offset: 0x0001A684
			public float pressure
			{
				[CompilerGenerated]
				get
				{
					return this.<pressure>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<pressure>k__BackingField = value;
				}
			}

			// Token: 0x170001E5 RID: 485
			// (get) Token: 0x0600072A RID: 1834 RVA: 0x0001C48D File Offset: 0x0001A68D
			// (set) Token: 0x0600072B RID: 1835 RVA: 0x0001C495 File Offset: 0x0001A695
			public float tangentialPressure
			{
				[CompilerGenerated]
				get
				{
					return this.<tangentialPressure>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<tangentialPressure>k__BackingField = value;
				}
			}

			// Token: 0x170001E6 RID: 486
			// (get) Token: 0x0600072C RID: 1836 RVA: 0x0001C49E File Offset: 0x0001A69E
			// (set) Token: 0x0600072D RID: 1837 RVA: 0x0001C4A6 File Offset: 0x0001A6A6
			public float altitudeAngle
			{
				[CompilerGenerated]
				get
				{
					return this.<altitudeAngle>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<altitudeAngle>k__BackingField = value;
				}
			}

			// Token: 0x170001E7 RID: 487
			// (get) Token: 0x0600072E RID: 1838 RVA: 0x0001C4AF File Offset: 0x0001A6AF
			// (set) Token: 0x0600072F RID: 1839 RVA: 0x0001C4B7 File Offset: 0x0001A6B7
			public float azimuthAngle
			{
				[CompilerGenerated]
				get
				{
					return this.<azimuthAngle>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<azimuthAngle>k__BackingField = value;
				}
			}

			// Token: 0x170001E8 RID: 488
			// (get) Token: 0x06000730 RID: 1840 RVA: 0x0001C4C0 File Offset: 0x0001A6C0
			// (set) Token: 0x06000731 RID: 1841 RVA: 0x0001C4C8 File Offset: 0x0001A6C8
			public float twist
			{
				[CompilerGenerated]
				get
				{
					return this.<twist>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<twist>k__BackingField = value;
				}
			}

			// Token: 0x170001E9 RID: 489
			// (get) Token: 0x06000732 RID: 1842 RVA: 0x0001C4D1 File Offset: 0x0001A6D1
			// (set) Token: 0x06000733 RID: 1843 RVA: 0x0001C4D9 File Offset: 0x0001A6D9
			public Vector2 radius
			{
				[CompilerGenerated]
				get
				{
					return this.<radius>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<radius>k__BackingField = value;
				}
			}

			// Token: 0x170001EA RID: 490
			// (get) Token: 0x06000734 RID: 1844 RVA: 0x0001C4E2 File Offset: 0x0001A6E2
			// (set) Token: 0x06000735 RID: 1845 RVA: 0x0001C4EA File Offset: 0x0001A6EA
			public Vector2 radiusVariance
			{
				[CompilerGenerated]
				get
				{
					return this.<radiusVariance>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<radiusVariance>k__BackingField = value;
				}
			}

			// Token: 0x170001EB RID: 491
			// (get) Token: 0x06000736 RID: 1846 RVA: 0x0001C4F3 File Offset: 0x0001A6F3
			// (set) Token: 0x06000737 RID: 1847 RVA: 0x0001C4FB File Offset: 0x0001A6FB
			public EventModifiers modifiers
			{
				[CompilerGenerated]
				get
				{
					return this.<modifiers>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<modifiers>k__BackingField = value;
				}
			}

			// Token: 0x170001EC RID: 492
			// (get) Token: 0x06000738 RID: 1848 RVA: 0x0001C504 File Offset: 0x0001A704
			public bool shiftKey
			{
				get
				{
					return (this.modifiers & EventModifiers.Shift) > EventModifiers.None;
				}
			}

			// Token: 0x170001ED RID: 493
			// (get) Token: 0x06000739 RID: 1849 RVA: 0x0001C511 File Offset: 0x0001A711
			public bool ctrlKey
			{
				get
				{
					return (this.modifiers & EventModifiers.Control) > EventModifiers.None;
				}
			}

			// Token: 0x170001EE RID: 494
			// (get) Token: 0x0600073A RID: 1850 RVA: 0x0001C51E File Offset: 0x0001A71E
			public bool commandKey
			{
				get
				{
					return (this.modifiers & EventModifiers.Command) > EventModifiers.None;
				}
			}

			// Token: 0x170001EF RID: 495
			// (get) Token: 0x0600073B RID: 1851 RVA: 0x0001C52B File Offset: 0x0001A72B
			public bool altKey
			{
				get
				{
					return (this.modifiers & EventModifiers.Alt) > EventModifiers.None;
				}
			}

			// Token: 0x170001F0 RID: 496
			// (get) Token: 0x0600073C RID: 1852 RVA: 0x0001C538 File Offset: 0x0001A738
			public bool actionKey
			{
				get
				{
					if (Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.OSXPlayer)
					{
						return this.ctrlKey;
					}
					return this.commandKey;
				}
			}

			// Token: 0x0600073D RID: 1853 RVA: 0x0001C558 File Offset: 0x0001A758
			public void Read(PanelEventHandler self, PointerEventData eventData, PanelEventHandler.PointerEventType eventType)
			{
				this.pointerId = self.eventSystem.currentInputModule.ConvertUIToolkitPointerId(eventData);
				this.pointerType = (PanelEventHandler.PointerEvent.<Read>g__InRange|82_0(this.pointerId, PointerId.touchPointerIdBase, PointerId.touchPointerCount) ? PointerType.touch : (PanelEventHandler.PointerEvent.<Read>g__InRange|82_0(this.pointerId, PointerId.penPointerIdBase, PointerId.penPointerCount) ? PointerType.pen : PointerType.mouse));
				this.isPrimary = (this.pointerId == PointerId.mousePointerId || this.pointerId == PointerId.touchPointerIdBase || this.pointerId == PointerId.penPointerIdBase);
				this.button = (int)eventData.button;
				this.clickCount = eventData.clickCount;
				int num = Screen.height;
				Vector3 vector = Display.RelativeMouseAt(eventData.position);
				if (vector != Vector3.zero)
				{
					int num2 = (int)vector.z;
					if (num2 > 0 && num2 < Display.displays.Length)
					{
						num = Display.displays[num2].systemHeight;
					}
				}
				else
				{
					vector = eventData.position;
				}
				Vector2 delta = eventData.delta;
				vector.y = (float)num - vector.y;
				delta.y = -delta.y;
				this.localPosition = (this.position = vector);
				this.deltaPosition = delta;
				this.deltaTime = 0f;
				this.pressure = eventData.pressure;
				this.tangentialPressure = eventData.tangentialPressure;
				this.altitudeAngle = eventData.altitudeAngle;
				this.azimuthAngle = eventData.azimuthAngle;
				this.twist = eventData.twist;
				this.radius = eventData.radius;
				this.radiusVariance = eventData.radiusVariance;
				this.modifiers = PanelEventHandler.s_Modifiers;
				if (eventType == PanelEventHandler.PointerEventType.Default)
				{
					this.button = -1;
					this.clickCount = 0;
				}
				else
				{
					this.button = ((this.button >= 0) ? this.button : 0);
					this.clickCount = Mathf.Max(1, this.clickCount);
					if (eventType == PanelEventHandler.PointerEventType.Down)
					{
						PointerDeviceState.PressButton(this.pointerId, this.button);
					}
					else if (eventType == PanelEventHandler.PointerEventType.Up)
					{
						PointerDeviceState.ReleaseButton(this.pointerId, this.button);
					}
				}
				this.pressedButtons = PointerDeviceState.GetPressedButtons(this.pointerId);
			}

			// Token: 0x0600073E RID: 1854 RVA: 0x0001C788 File Offset: 0x0001A988
			public void SetPosition(Vector3 positionOverride, Vector3 deltaOverride)
			{
				this.position = positionOverride;
				this.localPosition = positionOverride;
				this.deltaPosition = deltaOverride;
			}

			// Token: 0x0600073F RID: 1855 RVA: 0x0001C7AC File Offset: 0x0001A9AC
			public PointerEvent()
			{
			}

			// Token: 0x06000740 RID: 1856 RVA: 0x0001C7B4 File Offset: 0x0001A9B4
			[CompilerGenerated]
			internal static bool <Read>g__InRange|82_0(int i, int start, int count)
			{
				return i >= start && i < start + count;
			}

			// Token: 0x04000324 RID: 804
			[CompilerGenerated]
			private int <pointerId>k__BackingField;

			// Token: 0x04000325 RID: 805
			[CompilerGenerated]
			private string <pointerType>k__BackingField;

			// Token: 0x04000326 RID: 806
			[CompilerGenerated]
			private bool <isPrimary>k__BackingField;

			// Token: 0x04000327 RID: 807
			[CompilerGenerated]
			private int <button>k__BackingField;

			// Token: 0x04000328 RID: 808
			[CompilerGenerated]
			private int <pressedButtons>k__BackingField;

			// Token: 0x04000329 RID: 809
			[CompilerGenerated]
			private Vector3 <position>k__BackingField;

			// Token: 0x0400032A RID: 810
			[CompilerGenerated]
			private Vector3 <localPosition>k__BackingField;

			// Token: 0x0400032B RID: 811
			[CompilerGenerated]
			private Vector3 <deltaPosition>k__BackingField;

			// Token: 0x0400032C RID: 812
			[CompilerGenerated]
			private float <deltaTime>k__BackingField;

			// Token: 0x0400032D RID: 813
			[CompilerGenerated]
			private int <clickCount>k__BackingField;

			// Token: 0x0400032E RID: 814
			[CompilerGenerated]
			private float <pressure>k__BackingField;

			// Token: 0x0400032F RID: 815
			[CompilerGenerated]
			private float <tangentialPressure>k__BackingField;

			// Token: 0x04000330 RID: 816
			[CompilerGenerated]
			private float <altitudeAngle>k__BackingField;

			// Token: 0x04000331 RID: 817
			[CompilerGenerated]
			private float <azimuthAngle>k__BackingField;

			// Token: 0x04000332 RID: 818
			[CompilerGenerated]
			private float <twist>k__BackingField;

			// Token: 0x04000333 RID: 819
			[CompilerGenerated]
			private Vector2 <radius>k__BackingField;

			// Token: 0x04000334 RID: 820
			[CompilerGenerated]
			private Vector2 <radiusVariance>k__BackingField;

			// Token: 0x04000335 RID: 821
			[CompilerGenerated]
			private EventModifiers <modifiers>k__BackingField;
		}
	}
}
