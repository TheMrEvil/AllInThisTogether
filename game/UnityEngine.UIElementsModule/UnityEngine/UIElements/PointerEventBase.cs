using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x0200021C RID: 540
	public abstract class PointerEventBase<T> : EventBase<T>, IPointerEvent, IPointerEventInternal where T : PointerEventBase<T>, new()
	{
		// Token: 0x17000393 RID: 915
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x000420A6 File Offset: 0x000402A6
		// (set) Token: 0x0600107F RID: 4223 RVA: 0x000420AE File Offset: 0x000402AE
		public int pointerId
		{
			[CompilerGenerated]
			get
			{
				return this.<pointerId>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<pointerId>k__BackingField = value;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06001080 RID: 4224 RVA: 0x000420B7 File Offset: 0x000402B7
		// (set) Token: 0x06001081 RID: 4225 RVA: 0x000420BF File Offset: 0x000402BF
		public string pointerType
		{
			[CompilerGenerated]
			get
			{
				return this.<pointerType>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<pointerType>k__BackingField = value;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06001082 RID: 4226 RVA: 0x000420C8 File Offset: 0x000402C8
		// (set) Token: 0x06001083 RID: 4227 RVA: 0x000420D0 File Offset: 0x000402D0
		public bool isPrimary
		{
			[CompilerGenerated]
			get
			{
				return this.<isPrimary>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<isPrimary>k__BackingField = value;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06001084 RID: 4228 RVA: 0x000420D9 File Offset: 0x000402D9
		// (set) Token: 0x06001085 RID: 4229 RVA: 0x000420E1 File Offset: 0x000402E1
		public int button
		{
			[CompilerGenerated]
			get
			{
				return this.<button>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<button>k__BackingField = value;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06001086 RID: 4230 RVA: 0x000420EA File Offset: 0x000402EA
		// (set) Token: 0x06001087 RID: 4231 RVA: 0x000420F2 File Offset: 0x000402F2
		public int pressedButtons
		{
			[CompilerGenerated]
			get
			{
				return this.<pressedButtons>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<pressedButtons>k__BackingField = value;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06001088 RID: 4232 RVA: 0x000420FB File Offset: 0x000402FB
		// (set) Token: 0x06001089 RID: 4233 RVA: 0x00042103 File Offset: 0x00040303
		public Vector3 position
		{
			[CompilerGenerated]
			get
			{
				return this.<position>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<position>k__BackingField = value;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x0600108A RID: 4234 RVA: 0x0004210C File Offset: 0x0004030C
		// (set) Token: 0x0600108B RID: 4235 RVA: 0x00042114 File Offset: 0x00040314
		public Vector3 localPosition
		{
			[CompilerGenerated]
			get
			{
				return this.<localPosition>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<localPosition>k__BackingField = value;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x0600108C RID: 4236 RVA: 0x0004211D File Offset: 0x0004031D
		// (set) Token: 0x0600108D RID: 4237 RVA: 0x00042125 File Offset: 0x00040325
		public Vector3 deltaPosition
		{
			[CompilerGenerated]
			get
			{
				return this.<deltaPosition>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<deltaPosition>k__BackingField = value;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x0600108E RID: 4238 RVA: 0x0004212E File Offset: 0x0004032E
		// (set) Token: 0x0600108F RID: 4239 RVA: 0x00042136 File Offset: 0x00040336
		public float deltaTime
		{
			[CompilerGenerated]
			get
			{
				return this.<deltaTime>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<deltaTime>k__BackingField = value;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06001090 RID: 4240 RVA: 0x0004213F File Offset: 0x0004033F
		// (set) Token: 0x06001091 RID: 4241 RVA: 0x00042147 File Offset: 0x00040347
		public int clickCount
		{
			[CompilerGenerated]
			get
			{
				return this.<clickCount>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<clickCount>k__BackingField = value;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06001092 RID: 4242 RVA: 0x00042150 File Offset: 0x00040350
		// (set) Token: 0x06001093 RID: 4243 RVA: 0x00042158 File Offset: 0x00040358
		public float pressure
		{
			[CompilerGenerated]
			get
			{
				return this.<pressure>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<pressure>k__BackingField = value;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06001094 RID: 4244 RVA: 0x00042161 File Offset: 0x00040361
		// (set) Token: 0x06001095 RID: 4245 RVA: 0x00042169 File Offset: 0x00040369
		public float tangentialPressure
		{
			[CompilerGenerated]
			get
			{
				return this.<tangentialPressure>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<tangentialPressure>k__BackingField = value;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06001096 RID: 4246 RVA: 0x00042172 File Offset: 0x00040372
		// (set) Token: 0x06001097 RID: 4247 RVA: 0x0004217A File Offset: 0x0004037A
		public float altitudeAngle
		{
			[CompilerGenerated]
			get
			{
				return this.<altitudeAngle>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<altitudeAngle>k__BackingField = value;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001098 RID: 4248 RVA: 0x00042183 File Offset: 0x00040383
		// (set) Token: 0x06001099 RID: 4249 RVA: 0x0004218B File Offset: 0x0004038B
		public float azimuthAngle
		{
			[CompilerGenerated]
			get
			{
				return this.<azimuthAngle>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<azimuthAngle>k__BackingField = value;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x0600109A RID: 4250 RVA: 0x00042194 File Offset: 0x00040394
		// (set) Token: 0x0600109B RID: 4251 RVA: 0x0004219C File Offset: 0x0004039C
		public float twist
		{
			[CompilerGenerated]
			get
			{
				return this.<twist>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<twist>k__BackingField = value;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x0600109C RID: 4252 RVA: 0x000421A5 File Offset: 0x000403A5
		// (set) Token: 0x0600109D RID: 4253 RVA: 0x000421AD File Offset: 0x000403AD
		public Vector2 radius
		{
			[CompilerGenerated]
			get
			{
				return this.<radius>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<radius>k__BackingField = value;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x0600109E RID: 4254 RVA: 0x000421B6 File Offset: 0x000403B6
		// (set) Token: 0x0600109F RID: 4255 RVA: 0x000421BE File Offset: 0x000403BE
		public Vector2 radiusVariance
		{
			[CompilerGenerated]
			get
			{
				return this.<radiusVariance>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<radiusVariance>k__BackingField = value;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x060010A0 RID: 4256 RVA: 0x000421C7 File Offset: 0x000403C7
		// (set) Token: 0x060010A1 RID: 4257 RVA: 0x000421CF File Offset: 0x000403CF
		public EventModifiers modifiers
		{
			[CompilerGenerated]
			get
			{
				return this.<modifiers>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<modifiers>k__BackingField = value;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x060010A2 RID: 4258 RVA: 0x000421D8 File Offset: 0x000403D8
		public bool shiftKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Shift) > EventModifiers.None;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x060010A3 RID: 4259 RVA: 0x000421F8 File Offset: 0x000403F8
		public bool ctrlKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Control) > EventModifiers.None;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x060010A4 RID: 4260 RVA: 0x00042218 File Offset: 0x00040418
		public bool commandKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Command) > EventModifiers.None;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x060010A5 RID: 4261 RVA: 0x00042238 File Offset: 0x00040438
		public bool altKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Alt) > EventModifiers.None;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x060010A6 RID: 4262 RVA: 0x00042258 File Offset: 0x00040458
		public bool actionKey
		{
			get
			{
				bool flag = Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer;
				bool result;
				if (flag)
				{
					result = this.commandKey;
				}
				else
				{
					result = this.ctrlKey;
				}
				return result;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x060010A7 RID: 4263 RVA: 0x00042291 File Offset: 0x00040491
		// (set) Token: 0x060010A8 RID: 4264 RVA: 0x00042299 File Offset: 0x00040499
		bool IPointerEventInternal.triggeredByOS
		{
			[CompilerGenerated]
			get
			{
				return this.<UnityEngine.UIElements.IPointerEventInternal.triggeredByOS>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<UnityEngine.UIElements.IPointerEventInternal.triggeredByOS>k__BackingField = value;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x060010A9 RID: 4265 RVA: 0x000422A2 File Offset: 0x000404A2
		// (set) Token: 0x060010AA RID: 4266 RVA: 0x000422AA File Offset: 0x000404AA
		bool IPointerEventInternal.recomputeTopElementUnderPointer
		{
			[CompilerGenerated]
			get
			{
				return this.<UnityEngine.UIElements.IPointerEventInternal.recomputeTopElementUnderPointer>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<UnityEngine.UIElements.IPointerEventInternal.recomputeTopElementUnderPointer>k__BackingField = value;
			}
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x000422B3 File Offset: 0x000404B3
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x000422C4 File Offset: 0x000404C4
		private void LocalInit()
		{
			base.propagation = (EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable);
			base.propagateToIMGUI = false;
			this.pointerId = 0;
			this.pointerType = PointerType.unknown;
			this.isPrimary = false;
			this.button = -1;
			this.pressedButtons = 0;
			this.position = Vector3.zero;
			this.localPosition = Vector3.zero;
			this.deltaPosition = Vector3.zero;
			this.deltaTime = 0f;
			this.clickCount = 0;
			this.pressure = 0f;
			this.tangentialPressure = 0f;
			this.altitudeAngle = 0f;
			this.azimuthAngle = 0f;
			this.twist = 0f;
			this.radius = Vector2.zero;
			this.radiusVariance = Vector2.zero;
			this.modifiers = EventModifiers.None;
			((IPointerEventInternal)this).triggeredByOS = false;
			((IPointerEventInternal)this).recomputeTopElementUnderPointer = false;
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x060010AD RID: 4269 RVA: 0x000423B4 File Offset: 0x000405B4
		// (set) Token: 0x060010AE RID: 4270 RVA: 0x000423CC File Offset: 0x000405CC
		public override IEventHandler currentTarget
		{
			get
			{
				return base.currentTarget;
			}
			internal set
			{
				base.currentTarget = value;
				VisualElement visualElement = this.currentTarget as VisualElement;
				bool flag = visualElement != null;
				if (flag)
				{
					this.localPosition = visualElement.WorldToLocal(this.position);
				}
				else
				{
					this.localPosition = this.position;
				}
			}
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x00042428 File Offset: 0x00040628
		private static bool IsMouse(Event systemEvent)
		{
			EventType rawType = systemEvent.rawType;
			return rawType == EventType.MouseMove || rawType == EventType.MouseDown || rawType == EventType.MouseUp || rawType == EventType.MouseDrag || rawType == EventType.ContextClick || rawType == EventType.MouseEnterWindow || rawType == EventType.MouseLeaveWindow;
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x00042464 File Offset: 0x00040664
		public static T GetPooled(Event systemEvent)
		{
			T pooled = EventBase<T>.GetPooled();
			bool flag = !PointerEventBase<T>.IsMouse(systemEvent) && systemEvent.rawType != EventType.DragUpdated;
			if (flag)
			{
				Debug.Assert(false, string.Concat(new string[]
				{
					"Unexpected event type: ",
					systemEvent.rawType.ToString(),
					" (",
					systemEvent.type.ToString(),
					")"
				}));
			}
			PointerType pointerType = systemEvent.pointerType;
			PointerType pointerType2 = pointerType;
			if (pointerType2 != PointerType.Touch)
			{
				if (pointerType2 != PointerType.Pen)
				{
					pooled.pointerType = PointerType.mouse;
					pooled.pointerId = PointerId.mousePointerId;
				}
				else
				{
					pooled.pointerType = PointerType.pen;
					pooled.pointerId = PointerId.penPointerIdBase;
				}
			}
			else
			{
				pooled.pointerType = PointerType.touch;
				pooled.pointerId = PointerId.touchPointerIdBase;
			}
			pooled.isPrimary = true;
			pooled.altitudeAngle = 0f;
			pooled.azimuthAngle = 0f;
			pooled.twist = 0f;
			pooled.radius = Vector2.zero;
			pooled.radiusVariance = Vector2.zero;
			pooled.imguiEvent = systemEvent;
			bool flag2 = systemEvent.rawType == EventType.MouseDown;
			if (flag2)
			{
				PointerDeviceState.PressButton(PointerId.mousePointerId, systemEvent.button);
				pooled.button = systemEvent.button;
			}
			else
			{
				bool flag3 = systemEvent.rawType == EventType.MouseUp;
				if (flag3)
				{
					PointerDeviceState.ReleaseButton(PointerId.mousePointerId, systemEvent.button);
					pooled.button = systemEvent.button;
				}
				else
				{
					bool flag4 = systemEvent.rawType == EventType.MouseMove;
					if (flag4)
					{
						pooled.button = -1;
					}
				}
			}
			pooled.pressedButtons = PointerDeviceState.GetPressedButtons(pooled.pointerId);
			pooled.position = systemEvent.mousePosition;
			pooled.localPosition = systemEvent.mousePosition;
			pooled.deltaPosition = systemEvent.delta;
			pooled.clickCount = systemEvent.clickCount;
			pooled.modifiers = systemEvent.modifiers;
			PointerType pointerType3 = systemEvent.pointerType;
			PointerType pointerType4 = pointerType3;
			if (pointerType4 - PointerType.Touch > 1)
			{
				pooled.pressure = ((pooled.pressedButtons == 0) ? 0f : 0.5f);
			}
			else
			{
				pooled.pressure = systemEvent.pressure;
			}
			pooled.tangentialPressure = 0f;
			pooled.triggeredByOS = true;
			return pooled;
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x00042764 File Offset: 0x00040964
		public static T GetPooled(Touch touch, EventModifiers modifiers = EventModifiers.None)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.pointerId = touch.fingerId + PointerId.touchPointerIdBase;
			pooled.pointerType = PointerType.touch;
			bool flag = false;
			for (int i = PointerId.touchPointerIdBase; i < PointerId.touchPointerIdBase + PointerId.touchPointerCount; i++)
			{
				bool flag2 = i != pooled.pointerId && PointerDeviceState.GetPressedButtons(i) != 0;
				if (flag2)
				{
					flag = true;
					break;
				}
			}
			pooled.isPrimary = !flag;
			bool flag3 = touch.phase == TouchPhase.Began;
			if (flag3)
			{
				PointerDeviceState.PressButton(pooled.pointerId, 0);
				pooled.button = 0;
			}
			else
			{
				bool flag4 = touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled;
				if (flag4)
				{
					PointerDeviceState.ReleaseButton(pooled.pointerId, 0);
					pooled.button = 0;
				}
				else
				{
					pooled.button = -1;
				}
			}
			pooled.pressedButtons = PointerDeviceState.GetPressedButtons(pooled.pointerId);
			pooled.position = touch.position;
			pooled.localPosition = touch.position;
			pooled.deltaPosition = touch.deltaPosition;
			pooled.deltaTime = touch.deltaTime;
			pooled.clickCount = touch.tapCount;
			pooled.pressure = ((Mathf.Abs(touch.maximumPossiblePressure) > 1E-30f) ? (touch.pressure / touch.maximumPossiblePressure) : 1f);
			pooled.tangentialPressure = 0f;
			pooled.altitudeAngle = touch.altitudeAngle;
			pooled.azimuthAngle = touch.azimuthAngle;
			pooled.twist = 0f;
			pooled.radius = new Vector2(touch.radius, touch.radius);
			pooled.radiusVariance = new Vector2(touch.radiusVariance, touch.radiusVariance);
			pooled.modifiers = modifiers;
			pooled.triggeredByOS = true;
			return pooled;
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x000429E4 File Offset: 0x00040BE4
		internal static T GetPooled(IPointerEvent triggerEvent, Vector2 position, int pointerId)
		{
			bool flag = triggerEvent != null;
			T result;
			if (flag)
			{
				result = PointerEventBase<T>.GetPooled(triggerEvent);
			}
			else
			{
				T pooled = EventBase<T>.GetPooled();
				pooled.position = position;
				pooled.localPosition = position;
				pooled.pointerId = pointerId;
				pooled.pointerType = PointerType.GetPointerType(pointerId);
				result = pooled;
			}
			return result;
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x00042A54 File Offset: 0x00040C54
		public static T GetPooled(IPointerEvent triggerEvent)
		{
			T pooled = EventBase<T>.GetPooled();
			bool flag = triggerEvent != null;
			if (flag)
			{
				pooled.pointerId = triggerEvent.pointerId;
				pooled.pointerType = triggerEvent.pointerType;
				pooled.isPrimary = triggerEvent.isPrimary;
				pooled.button = triggerEvent.button;
				pooled.pressedButtons = triggerEvent.pressedButtons;
				pooled.position = triggerEvent.position;
				pooled.localPosition = triggerEvent.localPosition;
				pooled.deltaPosition = triggerEvent.deltaPosition;
				pooled.deltaTime = triggerEvent.deltaTime;
				pooled.clickCount = triggerEvent.clickCount;
				pooled.pressure = triggerEvent.pressure;
				pooled.tangentialPressure = triggerEvent.tangentialPressure;
				pooled.altitudeAngle = triggerEvent.altitudeAngle;
				pooled.azimuthAngle = triggerEvent.azimuthAngle;
				pooled.twist = triggerEvent.twist;
				pooled.radius = triggerEvent.radius;
				pooled.radiusVariance = triggerEvent.radiusVariance;
				pooled.modifiers = triggerEvent.modifiers;
				IPointerEventInternal pointerEventInternal = triggerEvent as IPointerEventInternal;
				bool flag2 = pointerEventInternal != null;
				if (flag2)
				{
					pooled.triggeredByOS |= pointerEventInternal.triggeredByOS;
				}
			}
			return pooled;
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x00042BEC File Offset: 0x00040DEC
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			bool triggeredByOS = ((IPointerEventInternal)this).triggeredByOS;
			if (triggeredByOS)
			{
				PointerDeviceState.SavePointerPosition(this.pointerId, this.position, panel, panel.contextType);
			}
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x00042C2C File Offset: 0x00040E2C
		protected internal override void PostDispatch(IPanel panel)
		{
			for (int i = 0; i < PointerId.maxPointers; i++)
			{
				panel.ProcessPointerCapture(i);
			}
			bool flag = !panel.ShouldSendCompatibilityMouseEvents(this) && ((IPointerEventInternal)this).triggeredByOS;
			if (flag)
			{
				BaseVisualElementPanel baseVisualElementPanel = panel as BaseVisualElementPanel;
				if (baseVisualElementPanel != null)
				{
					baseVisualElementPanel.CommitElementUnderPointers();
				}
			}
			base.PostDispatch(panel);
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x00042C8A File Offset: 0x00040E8A
		protected PointerEventBase()
		{
			this.LocalInit();
		}

		// Token: 0x04000752 RID: 1874
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <pointerId>k__BackingField;

		// Token: 0x04000753 RID: 1875
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <pointerType>k__BackingField;

		// Token: 0x04000754 RID: 1876
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <isPrimary>k__BackingField;

		// Token: 0x04000755 RID: 1877
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <button>k__BackingField;

		// Token: 0x04000756 RID: 1878
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <pressedButtons>k__BackingField;

		// Token: 0x04000757 RID: 1879
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Vector3 <position>k__BackingField;

		// Token: 0x04000758 RID: 1880
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Vector3 <localPosition>k__BackingField;

		// Token: 0x04000759 RID: 1881
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Vector3 <deltaPosition>k__BackingField;

		// Token: 0x0400075A RID: 1882
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private float <deltaTime>k__BackingField;

		// Token: 0x0400075B RID: 1883
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <clickCount>k__BackingField;

		// Token: 0x0400075C RID: 1884
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private float <pressure>k__BackingField;

		// Token: 0x0400075D RID: 1885
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private float <tangentialPressure>k__BackingField;

		// Token: 0x0400075E RID: 1886
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private float <altitudeAngle>k__BackingField;

		// Token: 0x0400075F RID: 1887
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private float <azimuthAngle>k__BackingField;

		// Token: 0x04000760 RID: 1888
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private float <twist>k__BackingField;

		// Token: 0x04000761 RID: 1889
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Vector2 <radius>k__BackingField;

		// Token: 0x04000762 RID: 1890
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Vector2 <radiusVariance>k__BackingField;

		// Token: 0x04000763 RID: 1891
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private EventModifiers <modifiers>k__BackingField;

		// Token: 0x04000764 RID: 1892
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <UnityEngine.UIElements.IPointerEventInternal.triggeredByOS>k__BackingField;

		// Token: 0x04000765 RID: 1893
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <UnityEngine.UIElements.IPointerEventInternal.recomputeTopElementUnderPointer>k__BackingField;
	}
}
