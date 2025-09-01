using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x0200023F RID: 575
	[Serializable]
	internal class EventDebuggerEventRecord
	{
		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x0600114B RID: 4427 RVA: 0x00044F7A File Offset: 0x0004317A
		// (set) Token: 0x0600114C RID: 4428 RVA: 0x00044F82 File Offset: 0x00043182
		public string eventBaseName
		{
			[CompilerGenerated]
			get
			{
				return this.<eventBaseName>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<eventBaseName>k__BackingField = value;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x0600114D RID: 4429 RVA: 0x00044F8B File Offset: 0x0004318B
		// (set) Token: 0x0600114E RID: 4430 RVA: 0x00044F93 File Offset: 0x00043193
		public long eventTypeId
		{
			[CompilerGenerated]
			get
			{
				return this.<eventTypeId>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<eventTypeId>k__BackingField = value;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x0600114F RID: 4431 RVA: 0x00044F9C File Offset: 0x0004319C
		// (set) Token: 0x06001150 RID: 4432 RVA: 0x00044FA4 File Offset: 0x000431A4
		public ulong eventId
		{
			[CompilerGenerated]
			get
			{
				return this.<eventId>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<eventId>k__BackingField = value;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06001151 RID: 4433 RVA: 0x00044FAD File Offset: 0x000431AD
		// (set) Token: 0x06001152 RID: 4434 RVA: 0x00044FB5 File Offset: 0x000431B5
		private ulong triggerEventId
		{
			[CompilerGenerated]
			get
			{
				return this.<triggerEventId>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<triggerEventId>k__BackingField = value;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06001153 RID: 4435 RVA: 0x00044FBE File Offset: 0x000431BE
		// (set) Token: 0x06001154 RID: 4436 RVA: 0x00044FC6 File Offset: 0x000431C6
		internal long timestamp
		{
			[CompilerGenerated]
			get
			{
				return this.<timestamp>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<timestamp>k__BackingField = value;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06001155 RID: 4437 RVA: 0x00044FCF File Offset: 0x000431CF
		// (set) Token: 0x06001156 RID: 4438 RVA: 0x00044FD7 File Offset: 0x000431D7
		public IEventHandler target
		{
			[CompilerGenerated]
			get
			{
				return this.<target>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<target>k__BackingField = value;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06001157 RID: 4439 RVA: 0x00044FE0 File Offset: 0x000431E0
		// (set) Token: 0x06001158 RID: 4440 RVA: 0x00044FE8 File Offset: 0x000431E8
		private List<IEventHandler> skipElements
		{
			[CompilerGenerated]
			get
			{
				return this.<skipElements>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<skipElements>k__BackingField = value;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001159 RID: 4441 RVA: 0x00044FF1 File Offset: 0x000431F1
		// (set) Token: 0x0600115A RID: 4442 RVA: 0x00044FF9 File Offset: 0x000431F9
		public bool hasUnderlyingPhysicalEvent
		{
			[CompilerGenerated]
			get
			{
				return this.<hasUnderlyingPhysicalEvent>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<hasUnderlyingPhysicalEvent>k__BackingField = value;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x0600115B RID: 4443 RVA: 0x00045002 File Offset: 0x00043202
		// (set) Token: 0x0600115C RID: 4444 RVA: 0x0004500A File Offset: 0x0004320A
		private bool isPropagationStopped
		{
			[CompilerGenerated]
			get
			{
				return this.<isPropagationStopped>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<isPropagationStopped>k__BackingField = value;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x0600115D RID: 4445 RVA: 0x00045013 File Offset: 0x00043213
		// (set) Token: 0x0600115E RID: 4446 RVA: 0x0004501B File Offset: 0x0004321B
		private bool isImmediatePropagationStopped
		{
			[CompilerGenerated]
			get
			{
				return this.<isImmediatePropagationStopped>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<isImmediatePropagationStopped>k__BackingField = value;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x0600115F RID: 4447 RVA: 0x00045024 File Offset: 0x00043224
		// (set) Token: 0x06001160 RID: 4448 RVA: 0x0004502C File Offset: 0x0004322C
		private bool isDefaultPrevented
		{
			[CompilerGenerated]
			get
			{
				return this.<isDefaultPrevented>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<isDefaultPrevented>k__BackingField = value;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06001161 RID: 4449 RVA: 0x00045035 File Offset: 0x00043235
		// (set) Token: 0x06001162 RID: 4450 RVA: 0x0004503D File Offset: 0x0004323D
		public PropagationPhase propagationPhase
		{
			[CompilerGenerated]
			get
			{
				return this.<propagationPhase>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<propagationPhase>k__BackingField = value;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06001163 RID: 4451 RVA: 0x00045046 File Offset: 0x00043246
		// (set) Token: 0x06001164 RID: 4452 RVA: 0x0004504E File Offset: 0x0004324E
		private IEventHandler currentTarget
		{
			[CompilerGenerated]
			get
			{
				return this.<currentTarget>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<currentTarget>k__BackingField = value;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06001165 RID: 4453 RVA: 0x00045057 File Offset: 0x00043257
		// (set) Token: 0x06001166 RID: 4454 RVA: 0x0004505F File Offset: 0x0004325F
		private bool dispatch
		{
			[CompilerGenerated]
			get
			{
				return this.<dispatch>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<dispatch>k__BackingField = value;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06001167 RID: 4455 RVA: 0x00045068 File Offset: 0x00043268
		// (set) Token: 0x06001168 RID: 4456 RVA: 0x00045070 File Offset: 0x00043270
		private Vector2 originalMousePosition
		{
			[CompilerGenerated]
			get
			{
				return this.<originalMousePosition>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<originalMousePosition>k__BackingField = value;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06001169 RID: 4457 RVA: 0x00045079 File Offset: 0x00043279
		// (set) Token: 0x0600116A RID: 4458 RVA: 0x00045081 File Offset: 0x00043281
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

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x0600116B RID: 4459 RVA: 0x0004508A File Offset: 0x0004328A
		// (set) Token: 0x0600116C RID: 4460 RVA: 0x00045092 File Offset: 0x00043292
		public Vector2 mousePosition
		{
			[CompilerGenerated]
			get
			{
				return this.<mousePosition>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<mousePosition>k__BackingField = value;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x0600116D RID: 4461 RVA: 0x0004509B File Offset: 0x0004329B
		// (set) Token: 0x0600116E RID: 4462 RVA: 0x000450A3 File Offset: 0x000432A3
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

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x0600116F RID: 4463 RVA: 0x000450AC File Offset: 0x000432AC
		// (set) Token: 0x06001170 RID: 4464 RVA: 0x000450B4 File Offset: 0x000432B4
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

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06001171 RID: 4465 RVA: 0x000450BD File Offset: 0x000432BD
		// (set) Token: 0x06001172 RID: 4466 RVA: 0x000450C5 File Offset: 0x000432C5
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

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06001173 RID: 4467 RVA: 0x000450CE File Offset: 0x000432CE
		// (set) Token: 0x06001174 RID: 4468 RVA: 0x000450D6 File Offset: 0x000432D6
		public Vector3 delta
		{
			[CompilerGenerated]
			get
			{
				return this.<delta>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<delta>k__BackingField = value;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06001175 RID: 4469 RVA: 0x000450DF File Offset: 0x000432DF
		// (set) Token: 0x06001176 RID: 4470 RVA: 0x000450E7 File Offset: 0x000432E7
		public char character
		{
			[CompilerGenerated]
			get
			{
				return this.<character>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<character>k__BackingField = value;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06001177 RID: 4471 RVA: 0x000450F0 File Offset: 0x000432F0
		// (set) Token: 0x06001178 RID: 4472 RVA: 0x000450F8 File Offset: 0x000432F8
		public KeyCode keyCode
		{
			[CompilerGenerated]
			get
			{
				return this.<keyCode>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<keyCode>k__BackingField = value;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06001179 RID: 4473 RVA: 0x00045101 File Offset: 0x00043301
		// (set) Token: 0x0600117A RID: 4474 RVA: 0x00045109 File Offset: 0x00043309
		public string commandName
		{
			[CompilerGenerated]
			get
			{
				return this.<commandName>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<commandName>k__BackingField = value;
			}
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x00045114 File Offset: 0x00043314
		private void Init(EventBase evt)
		{
			Type type = evt.GetType();
			this.eventBaseName = EventDebugger.GetTypeDisplayName(type);
			this.eventTypeId = evt.eventTypeId;
			this.eventId = evt.eventId;
			this.triggerEventId = evt.triggerEventId;
			this.timestamp = evt.timestamp;
			this.target = evt.target;
			this.skipElements = evt.skipElements;
			this.isPropagationStopped = evt.isPropagationStopped;
			this.isImmediatePropagationStopped = evt.isImmediatePropagationStopped;
			this.isDefaultPrevented = evt.isDefaultPrevented;
			IMouseEvent mouseEvent = evt as IMouseEvent;
			IMouseEventInternal mouseEventInternal = evt as IMouseEventInternal;
			this.hasUnderlyingPhysicalEvent = (mouseEvent != null && mouseEventInternal != null && mouseEventInternal.triggeredByOS);
			this.propagationPhase = evt.propagationPhase;
			this.originalMousePosition = evt.originalMousePosition;
			this.currentTarget = evt.currentTarget;
			this.dispatch = evt.dispatch;
			bool flag = mouseEvent != null;
			if (flag)
			{
				this.modifiers = mouseEvent.modifiers;
				this.mousePosition = mouseEvent.mousePosition;
				this.button = mouseEvent.button;
				this.pressedButtons = mouseEvent.pressedButtons;
				this.clickCount = mouseEvent.clickCount;
				WheelEvent wheelEvent = mouseEvent as WheelEvent;
				bool flag2 = wheelEvent != null;
				if (flag2)
				{
					this.delta = wheelEvent.delta;
				}
			}
			IPointerEvent pointerEvent = evt as IPointerEvent;
			bool flag3 = pointerEvent != null;
			if (flag3)
			{
				IPointerEventInternal pointerEventInternal = evt as IPointerEventInternal;
				this.hasUnderlyingPhysicalEvent = (pointerEvent != null && pointerEventInternal != null && pointerEventInternal.triggeredByOS);
				this.modifiers = pointerEvent.modifiers;
				this.mousePosition = pointerEvent.position;
				this.button = pointerEvent.button;
				this.pressedButtons = pointerEvent.pressedButtons;
				this.clickCount = pointerEvent.clickCount;
			}
			IKeyboardEvent keyboardEvent = evt as IKeyboardEvent;
			bool flag4 = keyboardEvent != null;
			if (flag4)
			{
				this.modifiers = keyboardEvent.modifiers;
				this.character = keyboardEvent.character;
				this.keyCode = keyboardEvent.keyCode;
			}
			ICommandEvent commandEvent = evt as ICommandEvent;
			bool flag5 = commandEvent != null;
			if (flag5)
			{
				this.commandName = commandEvent.commandName;
			}
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x0004534E File Offset: 0x0004354E
		public EventDebuggerEventRecord(EventBase evt)
		{
			this.Init(evt);
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x00045360 File Offset: 0x00043560
		public string TimestampString()
		{
			long ticks = (long)((float)this.timestamp / 1000f * 10000000f);
			return new DateTime(ticks).ToString("HH:mm:ss.ffffff");
		}

		// Token: 0x040007A6 RID: 1958
		[SerializeField]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string <eventBaseName>k__BackingField;

		// Token: 0x040007A7 RID: 1959
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[SerializeField]
		private long <eventTypeId>k__BackingField;

		// Token: 0x040007A8 RID: 1960
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[SerializeField]
		private ulong <eventId>k__BackingField;

		// Token: 0x040007A9 RID: 1961
		[SerializeField]
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ulong <triggerEventId>k__BackingField;

		// Token: 0x040007AA RID: 1962
		[CompilerGenerated]
		[SerializeField]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private long <timestamp>k__BackingField;

		// Token: 0x040007AB RID: 1963
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private IEventHandler <target>k__BackingField;

		// Token: 0x040007AC RID: 1964
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private List<IEventHandler> <skipElements>k__BackingField;

		// Token: 0x040007AD RID: 1965
		[SerializeField]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <hasUnderlyingPhysicalEvent>k__BackingField;

		// Token: 0x040007AE RID: 1966
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <isPropagationStopped>k__BackingField;

		// Token: 0x040007AF RID: 1967
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <isImmediatePropagationStopped>k__BackingField;

		// Token: 0x040007B0 RID: 1968
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <isDefaultPrevented>k__BackingField;

		// Token: 0x040007B1 RID: 1969
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private PropagationPhase <propagationPhase>k__BackingField;

		// Token: 0x040007B2 RID: 1970
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private IEventHandler <currentTarget>k__BackingField;

		// Token: 0x040007B3 RID: 1971
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <dispatch>k__BackingField;

		// Token: 0x040007B4 RID: 1972
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Vector2 <originalMousePosition>k__BackingField;

		// Token: 0x040007B5 RID: 1973
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private EventModifiers <modifiers>k__BackingField;

		// Token: 0x040007B6 RID: 1974
		[SerializeField]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Vector2 <mousePosition>k__BackingField;

		// Token: 0x040007B7 RID: 1975
		[SerializeField]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int <clickCount>k__BackingField;

		// Token: 0x040007B8 RID: 1976
		[SerializeField]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int <button>k__BackingField;

		// Token: 0x040007B9 RID: 1977
		[SerializeField]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int <pressedButtons>k__BackingField;

		// Token: 0x040007BA RID: 1978
		[SerializeField]
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Vector3 <delta>k__BackingField;

		// Token: 0x040007BB RID: 1979
		[SerializeField]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private char <character>k__BackingField;

		// Token: 0x040007BC RID: 1980
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[SerializeField]
		[CompilerGenerated]
		private KeyCode <keyCode>k__BackingField;

		// Token: 0x040007BD RID: 1981
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[SerializeField]
		[CompilerGenerated]
		private string <commandName>k__BackingField;
	}
}
