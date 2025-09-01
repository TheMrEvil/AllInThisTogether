using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Yoga;

namespace UnityEngine.UIElements
{
	// Token: 0x0200005A RID: 90
	internal abstract class BaseVisualElementPanel : IPanel, IDisposable, IGroupBox
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000201 RID: 513
		// (set) Token: 0x06000202 RID: 514
		public abstract EventInterests IMGUIEventInterests { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000203 RID: 515
		// (set) Token: 0x06000204 RID: 516
		public abstract ScriptableObject ownerObject { get; protected set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000205 RID: 517
		// (set) Token: 0x06000206 RID: 518
		public abstract SavePersistentViewData saveViewData { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000207 RID: 519
		// (set) Token: 0x06000208 RID: 520
		public abstract GetViewDataDictionary getViewDataDictionary { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000209 RID: 521
		// (set) Token: 0x0600020A RID: 522
		public abstract int IMGUIContainersCount { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600020B RID: 523
		// (set) Token: 0x0600020C RID: 524
		public abstract FocusController focusController { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600020D RID: 525
		// (set) Token: 0x0600020E RID: 526
		public abstract IMGUIContainer rootIMGUIContainer { get; set; }

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600020F RID: 527 RVA: 0x00008CA8 File Offset: 0x00006EA8
		// (remove) Token: 0x06000210 RID: 528 RVA: 0x00008CE0 File Offset: 0x00006EE0
		internal event Action<BaseVisualElementPanel> panelDisposed
		{
			[CompilerGenerated]
			add
			{
				Action<BaseVisualElementPanel> action = this.panelDisposed;
				Action<BaseVisualElementPanel> action2;
				do
				{
					action2 = action;
					Action<BaseVisualElementPanel> value2 = (Action<BaseVisualElementPanel>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<BaseVisualElementPanel>>(ref this.panelDisposed, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<BaseVisualElementPanel> action = this.panelDisposed;
				Action<BaseVisualElementPanel> action2;
				do
				{
					action2 = action;
					Action<BaseVisualElementPanel> value2 = (Action<BaseVisualElementPanel>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<BaseVisualElementPanel>>(ref this.panelDisposed, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00008D18 File Offset: 0x00006F18
		protected BaseVisualElementPanel()
		{
			this.yogaConfig = new YogaConfig();
			this.yogaConfig.UseWebDefaults = YogaConfig.Default.UseWebDefaults;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00008D9A File Offset: 0x00006F9A
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00008DAC File Offset: 0x00006FAC
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					bool flag = this.ownerObject != null;
					if (flag)
					{
						UIElementsUtility.RemoveCachedPanel(this.ownerObject.GetInstanceID());
					}
					PointerDeviceState.RemovePanelData(this);
				}
				Action<BaseVisualElementPanel> action = this.panelDisposed;
				if (action != null)
				{
					action(this);
				}
				this.yogaConfig = null;
				this.disposed = true;
			}
		}

		// Token: 0x06000214 RID: 532
		public abstract void Repaint(Event e);

		// Token: 0x06000215 RID: 533
		public abstract void ValidateLayout();

		// Token: 0x06000216 RID: 534
		public abstract void UpdateAnimations();

		// Token: 0x06000217 RID: 535
		public abstract void UpdateBindings();

		// Token: 0x06000218 RID: 536
		public abstract void ApplyStyles();

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00008E1C File Offset: 0x0000701C
		// (set) Token: 0x0600021A RID: 538 RVA: 0x00008E34 File Offset: 0x00007034
		internal float scale
		{
			get
			{
				return this.m_Scale;
			}
			set
			{
				bool flag = !Mathf.Approximately(this.m_Scale, value);
				if (flag)
				{
					this.m_Scale = value;
					this.visualTree.IncrementVersion(VersionChangeType.Layout);
					this.yogaConfig.PointScaleFactor = this.scaledPixelsPerPoint;
					this.visualTree.IncrementVersion(VersionChangeType.StyleSheet);
				}
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00008E8C File Offset: 0x0000708C
		// (set) Token: 0x0600021C RID: 540 RVA: 0x00008EA4 File Offset: 0x000070A4
		internal float pixelsPerPoint
		{
			get
			{
				return this.m_PixelsPerPoint;
			}
			set
			{
				bool flag = !Mathf.Approximately(this.m_PixelsPerPoint, value);
				if (flag)
				{
					this.m_PixelsPerPoint = value;
					this.visualTree.IncrementVersion(VersionChangeType.Layout);
					this.yogaConfig.PointScaleFactor = this.scaledPixelsPerPoint;
					this.visualTree.IncrementVersion(VersionChangeType.StyleSheet);
				}
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00008EFC File Offset: 0x000070FC
		public float scaledPixelsPerPoint
		{
			get
			{
				return this.m_PixelsPerPoint * this.m_Scale;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00008F1C File Offset: 0x0000711C
		// (set) Token: 0x0600021F RID: 543 RVA: 0x00008F5C File Offset: 0x0000715C
		public PanelClearFlags clearFlags
		{
			get
			{
				PanelClearFlags panelClearFlags = PanelClearFlags.None;
				bool clearColor = this.clearSettings.clearColor;
				if (clearColor)
				{
					panelClearFlags |= PanelClearFlags.Color;
				}
				bool clearDepthStencil = this.clearSettings.clearDepthStencil;
				if (clearDepthStencil)
				{
					panelClearFlags |= PanelClearFlags.Depth;
				}
				return panelClearFlags;
			}
			set
			{
				PanelClearSettings clearSettings = this.clearSettings;
				clearSettings.clearColor = ((value & PanelClearFlags.Color) == PanelClearFlags.Color);
				clearSettings.clearDepthStencil = ((value & PanelClearFlags.Depth) == PanelClearFlags.Depth);
				this.clearSettings = clearSettings;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00008F93 File Offset: 0x00007193
		// (set) Token: 0x06000221 RID: 545 RVA: 0x00008F9B File Offset: 0x0000719B
		internal PanelClearSettings clearSettings
		{
			[CompilerGenerated]
			get
			{
				return this.<clearSettings>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<clearSettings>k__BackingField = value;
			}
		} = new PanelClearSettings
		{
			clearDepthStencil = true,
			clearColor = true,
			color = Color.clear
		};

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00008FA4 File Offset: 0x000071A4
		// (set) Token: 0x06000223 RID: 547 RVA: 0x00008FAC File Offset: 0x000071AC
		internal bool duringLayoutPhase
		{
			[CompilerGenerated]
			get
			{
				return this.<duringLayoutPhase>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<duringLayoutPhase>k__BackingField = value;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00008FB8 File Offset: 0x000071B8
		internal bool isDirty
		{
			get
			{
				return this.version != this.repaintVersion;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000225 RID: 549
		internal abstract uint version { get; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000226 RID: 550
		internal abstract uint repaintVersion { get; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000227 RID: 551
		internal abstract uint hierarchyVersion { get; }

		// Token: 0x06000228 RID: 552
		internal abstract void OnVersionChanged(VisualElement ele, VersionChangeType changeTypeFlag);

		// Token: 0x06000229 RID: 553
		internal abstract void SetUpdater(IVisualTreeUpdater updater, VisualTreeUpdatePhase phase);

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00008FDB File Offset: 0x000071DB
		// (set) Token: 0x0600022B RID: 555 RVA: 0x00008FE3 File Offset: 0x000071E3
		internal virtual RepaintData repaintData
		{
			[CompilerGenerated]
			get
			{
				return this.<repaintData>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<repaintData>k__BackingField = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00008FEC File Offset: 0x000071EC
		// (set) Token: 0x0600022D RID: 557 RVA: 0x00008FF4 File Offset: 0x000071F4
		internal virtual ICursorManager cursorManager
		{
			[CompilerGenerated]
			get
			{
				return this.<cursorManager>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<cursorManager>k__BackingField = value;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00008FFD File Offset: 0x000071FD
		// (set) Token: 0x0600022F RID: 559 RVA: 0x00009005 File Offset: 0x00007205
		public ContextualMenuManager contextualMenuManager
		{
			[CompilerGenerated]
			get
			{
				return this.<contextualMenuManager>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<contextualMenuManager>k__BackingField = value;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000230 RID: 560
		public abstract VisualElement visualTree { get; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000231 RID: 561
		// (set) Token: 0x06000232 RID: 562
		public abstract EventDispatcher dispatcher { get; set; }

		// Token: 0x06000233 RID: 563 RVA: 0x0000900E File Offset: 0x0000720E
		internal void SendEvent(EventBase e, DispatchMode dispatchMode = DispatchMode.Default)
		{
			Debug.Assert(this.dispatcher != null);
			EventDispatcher dispatcher = this.dispatcher;
			if (dispatcher != null)
			{
				dispatcher.Dispatch(e, this, dispatchMode);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000234 RID: 564
		internal abstract IScheduler scheduler { get; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000235 RID: 565
		// (set) Token: 0x06000236 RID: 566
		internal abstract IStylePropertyAnimationSystem styleAnimationSystem { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000237 RID: 567
		// (set) Token: 0x06000238 RID: 568
		public abstract ContextType contextType { get; protected set; }

		// Token: 0x06000239 RID: 569
		public abstract VisualElement Pick(Vector2 point);

		// Token: 0x0600023A RID: 570
		public abstract VisualElement PickAll(Vector2 point, List<VisualElement> picked);

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00009035 File Offset: 0x00007235
		// (set) Token: 0x0600023C RID: 572 RVA: 0x0000903D File Offset: 0x0000723D
		internal bool disposed
		{
			[CompilerGenerated]
			get
			{
				return this.<disposed>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<disposed>k__BackingField = value;
			}
		}

		// Token: 0x0600023D RID: 573
		internal abstract IVisualTreeUpdater GetUpdater(VisualTreeUpdatePhase phase);

		// Token: 0x0600023E RID: 574 RVA: 0x00009048 File Offset: 0x00007248
		internal VisualElement GetTopElementUnderPointer(int pointerId)
		{
			return this.m_TopElementUnderPointers.GetTopElementUnderPointer(pointerId);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00009068 File Offset: 0x00007268
		internal VisualElement RecomputeTopElementUnderPointer(int pointerId, Vector2 pointerPos, EventBase triggerEvent)
		{
			VisualElement visualElement = null;
			bool flag = PointerDeviceState.GetPanel(pointerId, this.contextType) == this && !PointerDeviceState.HasLocationFlag(pointerId, this.contextType, PointerDeviceState.LocationFlag.OutsidePanel);
			if (flag)
			{
				visualElement = this.Pick(pointerPos);
			}
			this.m_TopElementUnderPointers.SetElementUnderPointer(visualElement, pointerId, triggerEvent);
			return visualElement;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000090BC File Offset: 0x000072BC
		internal void ClearCachedElementUnderPointer(int pointerId, EventBase triggerEvent)
		{
			this.m_TopElementUnderPointers.SetTemporaryElementUnderPointer(null, pointerId, triggerEvent);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000090CE File Offset: 0x000072CE
		internal void CommitElementUnderPointers()
		{
			this.m_TopElementUnderPointers.CommitElementUnderPointers(this.dispatcher, this.contextType);
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000242 RID: 578
		// (set) Token: 0x06000243 RID: 579
		internal abstract Shader standardShader { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000244 RID: 580 RVA: 0x000090EC File Offset: 0x000072EC
		// (set) Token: 0x06000245 RID: 581 RVA: 0x00002166 File Offset: 0x00000366
		internal virtual Shader standardWorldSpaceShader
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000246 RID: 582 RVA: 0x00009100 File Offset: 0x00007300
		// (remove) Token: 0x06000247 RID: 583 RVA: 0x00009138 File Offset: 0x00007338
		internal event Action standardShaderChanged
		{
			[CompilerGenerated]
			add
			{
				Action action = this.standardShaderChanged;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.standardShaderChanged, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.standardShaderChanged;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.standardShaderChanged, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000248 RID: 584 RVA: 0x00009170 File Offset: 0x00007370
		// (remove) Token: 0x06000249 RID: 585 RVA: 0x000091A8 File Offset: 0x000073A8
		internal event Action standardWorldSpaceShaderChanged
		{
			[CompilerGenerated]
			add
			{
				Action action = this.standardWorldSpaceShaderChanged;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.standardWorldSpaceShaderChanged, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.standardWorldSpaceShaderChanged;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.standardWorldSpaceShaderChanged, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x000091E0 File Offset: 0x000073E0
		protected void InvokeStandardShaderChanged()
		{
			bool flag = this.standardShaderChanged != null;
			if (flag)
			{
				this.standardShaderChanged();
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00009208 File Offset: 0x00007408
		protected void InvokeStandardWorldSpaceShaderChanged()
		{
			bool flag = this.standardWorldSpaceShaderChanged != null;
			if (flag)
			{
				this.standardWorldSpaceShaderChanged();
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600024C RID: 588 RVA: 0x00009230 File Offset: 0x00007430
		// (remove) Token: 0x0600024D RID: 589 RVA: 0x00009268 File Offset: 0x00007468
		internal event Action atlasChanged
		{
			[CompilerGenerated]
			add
			{
				Action action = this.atlasChanged;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.atlasChanged, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.atlasChanged;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.atlasChanged, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000929D File Offset: 0x0000749D
		protected void InvokeAtlasChanged()
		{
			Action action = this.atlasChanged;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600024F RID: 591
		// (set) Token: 0x06000250 RID: 592
		public abstract AtlasBase atlas { get; set; }

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000251 RID: 593 RVA: 0x000092B4 File Offset: 0x000074B4
		// (remove) Token: 0x06000252 RID: 594 RVA: 0x000092EC File Offset: 0x000074EC
		internal event Action<Material> updateMaterial
		{
			[CompilerGenerated]
			add
			{
				Action<Material> action = this.updateMaterial;
				Action<Material> action2;
				do
				{
					action2 = action;
					Action<Material> value2 = (Action<Material>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<Material>>(ref this.updateMaterial, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<Material> action = this.updateMaterial;
				Action<Material> action2;
				do
				{
					action2 = action;
					Action<Material> value2 = (Action<Material>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<Material>>(ref this.updateMaterial, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00009321 File Offset: 0x00007521
		internal void InvokeUpdateMaterial(Material mat)
		{
			Action<Material> action = this.updateMaterial;
			if (action != null)
			{
				action(mat);
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000254 RID: 596 RVA: 0x00009338 File Offset: 0x00007538
		// (remove) Token: 0x06000255 RID: 597 RVA: 0x00009370 File Offset: 0x00007570
		internal event HierarchyEvent hierarchyChanged
		{
			[CompilerGenerated]
			add
			{
				HierarchyEvent hierarchyEvent = this.hierarchyChanged;
				HierarchyEvent hierarchyEvent2;
				do
				{
					hierarchyEvent2 = hierarchyEvent;
					HierarchyEvent value2 = (HierarchyEvent)Delegate.Combine(hierarchyEvent2, value);
					hierarchyEvent = Interlocked.CompareExchange<HierarchyEvent>(ref this.hierarchyChanged, value2, hierarchyEvent2);
				}
				while (hierarchyEvent != hierarchyEvent2);
			}
			[CompilerGenerated]
			remove
			{
				HierarchyEvent hierarchyEvent = this.hierarchyChanged;
				HierarchyEvent hierarchyEvent2;
				do
				{
					hierarchyEvent2 = hierarchyEvent;
					HierarchyEvent value2 = (HierarchyEvent)Delegate.Remove(hierarchyEvent2, value);
					hierarchyEvent = Interlocked.CompareExchange<HierarchyEvent>(ref this.hierarchyChanged, value2, hierarchyEvent2);
				}
				while (hierarchyEvent != hierarchyEvent2);
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x000093A8 File Offset: 0x000075A8
		internal void InvokeHierarchyChanged(VisualElement ve, HierarchyChangeType changeType)
		{
			bool flag = this.hierarchyChanged != null;
			if (flag)
			{
				this.hierarchyChanged(ve, changeType);
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000257 RID: 599 RVA: 0x000093D4 File Offset: 0x000075D4
		// (remove) Token: 0x06000258 RID: 600 RVA: 0x0000940C File Offset: 0x0000760C
		internal event Action<IPanel> beforeUpdate
		{
			[CompilerGenerated]
			add
			{
				Action<IPanel> action = this.beforeUpdate;
				Action<IPanel> action2;
				do
				{
					action2 = action;
					Action<IPanel> value2 = (Action<IPanel>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<IPanel>>(ref this.beforeUpdate, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<IPanel> action = this.beforeUpdate;
				Action<IPanel> action2;
				do
				{
					action2 = action;
					Action<IPanel> value2 = (Action<IPanel>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<IPanel>>(ref this.beforeUpdate, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00009441 File Offset: 0x00007641
		internal void InvokeBeforeUpdate()
		{
			Action<IPanel> action = this.beforeUpdate;
			if (action != null)
			{
				action(this);
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00009458 File Offset: 0x00007658
		internal void UpdateElementUnderPointers()
		{
			foreach (int pointerId in PointerId.hoveringPointers)
			{
				bool flag = PointerDeviceState.GetPanel(pointerId, this.contextType) != this || PointerDeviceState.HasLocationFlag(pointerId, this.contextType, PointerDeviceState.LocationFlag.OutsidePanel);
				if (flag)
				{
					this.m_TopElementUnderPointers.SetElementUnderPointer(null, pointerId, new Vector2(float.MinValue, float.MinValue));
				}
				else
				{
					Vector2 pointerPosition = PointerDeviceState.GetPointerPosition(pointerId, this.contextType);
					VisualElement newElementUnderPointer = this.PickAll(pointerPosition, null);
					this.m_TopElementUnderPointers.SetElementUnderPointer(newElementUnderPointer, pointerId, pointerPosition);
				}
			}
			this.CommitElementUnderPointers();
		}

		// Token: 0x0600025B RID: 603 RVA: 0x000094F8 File Offset: 0x000076F8
		public virtual void Update()
		{
			this.scheduler.UpdateScheduledEvents();
			this.ValidateLayout();
			this.UpdateAnimations();
			this.UpdateBindings();
		}

		// Token: 0x04000113 RID: 275
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<BaseVisualElementPanel> panelDisposed;

		// Token: 0x04000114 RID: 276
		private float m_Scale = 1f;

		// Token: 0x04000115 RID: 277
		internal YogaConfig yogaConfig;

		// Token: 0x04000116 RID: 278
		private float m_PixelsPerPoint = 1f;

		// Token: 0x04000117 RID: 279
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private PanelClearSettings <clearSettings>k__BackingField;

		// Token: 0x04000118 RID: 280
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <duringLayoutPhase>k__BackingField;

		// Token: 0x04000119 RID: 281
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private RepaintData <repaintData>k__BackingField;

		// Token: 0x0400011A RID: 282
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ICursorManager <cursorManager>k__BackingField;

		// Token: 0x0400011B RID: 283
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ContextualMenuManager <contextualMenuManager>k__BackingField;

		// Token: 0x0400011C RID: 284
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <disposed>k__BackingField;

		// Token: 0x0400011D RID: 285
		internal ElementUnderPointer m_TopElementUnderPointers = new ElementUnderPointer();

		// Token: 0x0400011E RID: 286
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action standardShaderChanged;

		// Token: 0x0400011F RID: 287
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action standardWorldSpaceShaderChanged;

		// Token: 0x04000120 RID: 288
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Action atlasChanged;

		// Token: 0x04000121 RID: 289
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<Material> updateMaterial;

		// Token: 0x04000122 RID: 290
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private HierarchyEvent hierarchyChanged;

		// Token: 0x04000123 RID: 291
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<IPanel> beforeUpdate;
	}
}
