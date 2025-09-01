using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	// Token: 0x0200005F RID: 95
	internal class Panel : BaseVisualElementPanel
	{
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000951C File Offset: 0x0000771C
		public sealed override VisualElement visualTree
		{
			get
			{
				return this.m_RootContainer;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600026D RID: 621 RVA: 0x00009534 File Offset: 0x00007734
		// (set) Token: 0x0600026E RID: 622 RVA: 0x0000953C File Offset: 0x0000773C
		public sealed override EventDispatcher dispatcher
		{
			[CompilerGenerated]
			get
			{
				return this.<dispatcher>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<dispatcher>k__BackingField = value;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00009548 File Offset: 0x00007748
		public TimerEventScheduler timerEventScheduler
		{
			get
			{
				TimerEventScheduler result;
				if ((result = this.m_Scheduler) == null)
				{
					result = (this.m_Scheduler = new TimerEventScheduler());
				}
				return result;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000270 RID: 624 RVA: 0x00009574 File Offset: 0x00007774
		internal override IScheduler scheduler
		{
			get
			{
				return this.timerEventScheduler;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000958C File Offset: 0x0000778C
		internal VisualTreeUpdater visualTreeUpdater
		{
			get
			{
				return this.m_VisualTreeUpdater;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000272 RID: 626 RVA: 0x000095A4 File Offset: 0x000077A4
		// (set) Token: 0x06000273 RID: 627 RVA: 0x000095AC File Offset: 0x000077AC
		internal override IStylePropertyAnimationSystem styleAnimationSystem
		{
			get
			{
				return this.m_StylePropertyAnimationSystem;
			}
			set
			{
				bool flag = this.m_StylePropertyAnimationSystem == value;
				if (!flag)
				{
					IStylePropertyAnimationSystem stylePropertyAnimationSystem = this.m_StylePropertyAnimationSystem;
					if (stylePropertyAnimationSystem != null)
					{
						stylePropertyAnimationSystem.CancelAllAnimations();
					}
					this.m_StylePropertyAnimationSystem = value;
				}
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000274 RID: 628 RVA: 0x000095E2 File Offset: 0x000077E2
		// (set) Token: 0x06000275 RID: 629 RVA: 0x000095EA File Offset: 0x000077EA
		public override ScriptableObject ownerObject
		{
			[CompilerGenerated]
			get
			{
				return this.<ownerObject>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<ownerObject>k__BackingField = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000276 RID: 630 RVA: 0x000095F3 File Offset: 0x000077F3
		// (set) Token: 0x06000277 RID: 631 RVA: 0x000095FB File Offset: 0x000077FB
		public override ContextType contextType
		{
			[CompilerGenerated]
			get
			{
				return this.<contextType>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<contextType>k__BackingField = value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000278 RID: 632 RVA: 0x00009604 File Offset: 0x00007804
		// (set) Token: 0x06000279 RID: 633 RVA: 0x0000960C File Offset: 0x0000780C
		public override SavePersistentViewData saveViewData
		{
			[CompilerGenerated]
			get
			{
				return this.<saveViewData>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<saveViewData>k__BackingField = value;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600027A RID: 634 RVA: 0x00009615 File Offset: 0x00007815
		// (set) Token: 0x0600027B RID: 635 RVA: 0x0000961D File Offset: 0x0000781D
		public override GetViewDataDictionary getViewDataDictionary
		{
			[CompilerGenerated]
			get
			{
				return this.<getViewDataDictionary>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<getViewDataDictionary>k__BackingField = value;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600027C RID: 636 RVA: 0x00009626 File Offset: 0x00007826
		// (set) Token: 0x0600027D RID: 637 RVA: 0x0000962E File Offset: 0x0000782E
		public sealed override FocusController focusController
		{
			[CompilerGenerated]
			get
			{
				return this.<focusController>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<focusController>k__BackingField = value;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600027E RID: 638 RVA: 0x00009637 File Offset: 0x00007837
		// (set) Token: 0x0600027F RID: 639 RVA: 0x0000963F File Offset: 0x0000783F
		public override EventInterests IMGUIEventInterests
		{
			[CompilerGenerated]
			get
			{
				return this.<IMGUIEventInterests>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IMGUIEventInterests>k__BackingField = value;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000280 RID: 640 RVA: 0x00009648 File Offset: 0x00007848
		// (set) Token: 0x06000281 RID: 641 RVA: 0x0000964F File Offset: 0x0000784F
		internal static LoadResourceFunction loadResourceFunc
		{
			[CompilerGenerated]
			private get
			{
				return Panel.<loadResourceFunc>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				Panel.<loadResourceFunc>k__BackingField = value;
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00009658 File Offset: 0x00007858
		internal static Object LoadResource(string pathName, Type type, float dpiScaling)
		{
			bool flag = Panel.loadResourceFunc != null;
			Object result;
			if (flag)
			{
				result = Panel.loadResourceFunc(pathName, type, dpiScaling);
			}
			else
			{
				result = Resources.Load(pathName, type);
			}
			return result;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00009695 File Offset: 0x00007895
		internal void Focus()
		{
			FocusController focusController = this.focusController;
			if (focusController != null)
			{
				focusController.SetFocusToLastFocusedElement();
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x000096AA File Offset: 0x000078AA
		internal void Blur()
		{
			FocusController focusController = this.focusController;
			if (focusController != null)
			{
				focusController.BlurLastFocusedElement();
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000285 RID: 645 RVA: 0x000096C0 File Offset: 0x000078C0
		// (set) Token: 0x06000286 RID: 646 RVA: 0x000096D8 File Offset: 0x000078D8
		internal string name
		{
			get
			{
				return this.m_PanelName;
			}
			set
			{
				this.m_PanelName = value;
				this.CreateMarkers();
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x000096EC File Offset: 0x000078EC
		private void CreateMarkers()
		{
			bool flag = !string.IsNullOrEmpty(this.m_PanelName);
			if (flag)
			{
				this.m_MarkerBeforeUpdate = new ProfilerMarker("Panel.BeforeUpdate." + this.m_PanelName);
				this.m_MarkerUpdate = new ProfilerMarker("Panel.Update." + this.m_PanelName);
				this.m_MarkerLayout = new ProfilerMarker("Panel.Layout." + this.m_PanelName);
				this.m_MarkerBindings = new ProfilerMarker("Panel.Bindings." + this.m_PanelName);
				this.m_MarkerAnimations = new ProfilerMarker("Panel.Animations." + this.m_PanelName);
			}
			else
			{
				this.m_MarkerBeforeUpdate = new ProfilerMarker("Panel.BeforeUpdate");
				this.m_MarkerUpdate = new ProfilerMarker("Panel.Update");
				this.m_MarkerLayout = new ProfilerMarker("Panel.Layout");
				this.m_MarkerBindings = new ProfilerMarker("Panel.Bindings");
				this.m_MarkerAnimations = new ProfilerMarker("Panel.Animations");
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000288 RID: 648 RVA: 0x000097EC File Offset: 0x000079EC
		// (set) Token: 0x06000289 RID: 649 RVA: 0x000097F3 File Offset: 0x000079F3
		internal static TimeMsFunction TimeSinceStartup
		{
			[CompilerGenerated]
			private get
			{
				return Panel.<TimeSinceStartup>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				Panel.<TimeSinceStartup>k__BackingField = value;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600028A RID: 650 RVA: 0x000097FB File Offset: 0x000079FB
		// (set) Token: 0x0600028B RID: 651 RVA: 0x00009803 File Offset: 0x00007A03
		public override int IMGUIContainersCount
		{
			[CompilerGenerated]
			get
			{
				return this.<IMGUIContainersCount>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IMGUIContainersCount>k__BackingField = value;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000980C File Offset: 0x00007A0C
		// (set) Token: 0x0600028D RID: 653 RVA: 0x00009814 File Offset: 0x00007A14
		public override IMGUIContainer rootIMGUIContainer
		{
			[CompilerGenerated]
			get
			{
				return this.<rootIMGUIContainer>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<rootIMGUIContainer>k__BackingField = value;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000981D File Offset: 0x00007A1D
		internal override uint version
		{
			get
			{
				return this.m_Version;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00009825 File Offset: 0x00007A25
		internal override uint repaintVersion
		{
			get
			{
				return this.m_RepaintVersion;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0000982D File Offset: 0x00007A2D
		internal override uint hierarchyVersion
		{
			get
			{
				return this.m_HierarchyVersion;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00009838 File Offset: 0x00007A38
		// (set) Token: 0x06000292 RID: 658 RVA: 0x00009850 File Offset: 0x00007A50
		internal override Shader standardShader
		{
			get
			{
				return this.m_StandardShader;
			}
			set
			{
				bool flag = this.m_StandardShader != value;
				if (flag)
				{
					this.m_StandardShader = value;
					base.InvokeStandardShaderChanged();
				}
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00009880 File Offset: 0x00007A80
		// (set) Token: 0x06000294 RID: 660 RVA: 0x00009898 File Offset: 0x00007A98
		public override AtlasBase atlas
		{
			get
			{
				return this.m_Atlas;
			}
			set
			{
				bool flag = this.m_Atlas != value;
				if (flag)
				{
					AtlasBase atlas = this.m_Atlas;
					if (atlas != null)
					{
						atlas.InvokeRemovedFromPanel(this);
					}
					this.m_Atlas = value;
					base.InvokeAtlasChanged();
					AtlasBase atlas2 = this.m_Atlas;
					if (atlas2 != null)
					{
						atlas2.InvokeAssignedToPanel(this);
					}
				}
			}
		}

		// Token: 0x06000295 RID: 661 RVA: 0x000098EC File Offset: 0x00007AEC
		internal static Panel CreateEditorPanel(ScriptableObject ownerObject)
		{
			return new Panel(ownerObject, ContextType.Editor, EventDispatcher.CreateDefault());
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000990C File Offset: 0x00007B0C
		public Panel(ScriptableObject ownerObject, ContextType contextType, EventDispatcher dispatcher)
		{
			this.ownerObject = ownerObject;
			this.contextType = contextType;
			this.dispatcher = dispatcher;
			this.repaintData = new RepaintData();
			this.cursorManager = new CursorManager();
			base.contextualMenuManager = null;
			this.m_VisualTreeUpdater = new VisualTreeUpdater(this);
			this.m_RootContainer = new VisualElement
			{
				name = VisualElementUtils.GetUniqueName("unity-panel-container"),
				viewDataKey = "PanelContainer",
				pickingMode = ((contextType == ContextType.Editor) ? PickingMode.Position : PickingMode.Ignore)
			};
			this.visualTree.SetPanel(this);
			this.focusController = new FocusController(new VisualElementFocusRing(this.visualTree, VisualElementFocusRing.DefaultFocusOrder.ChildOrder));
			this.styleAnimationSystem = new StylePropertyAnimationSystem();
			this.CreateMarkers();
			base.InvokeHierarchyChanged(this.visualTree, HierarchyChangeType.Add);
			this.atlas = new DynamicAtlas();
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00009A0C File Offset: 0x00007C0C
		protected override void Dispose(bool disposing)
		{
			bool disposed = base.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.atlas = null;
					this.m_VisualTreeUpdater.Dispose();
				}
				base.Dispose(disposing);
			}
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00009A4C File Offset: 0x00007C4C
		public static long TimeSinceStartupMs()
		{
			TimeMsFunction timeSinceStartup = Panel.TimeSinceStartup;
			return (timeSinceStartup != null) ? timeSinceStartup() : Panel.DefaultTimeSinceStartupMs();
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00009A74 File Offset: 0x00007C74
		internal static long DefaultTimeSinceStartupMs()
		{
			return (long)(Time.realtimeSinceStartup * 1000f);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00009A94 File Offset: 0x00007C94
		internal static VisualElement PickAllWithoutValidatingLayout(VisualElement root, Vector2 point)
		{
			return Panel.PickAll(root, point, null);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00009AB0 File Offset: 0x00007CB0
		private static VisualElement PickAll(VisualElement root, Vector2 point, List<VisualElement> picked = null)
		{
			Panel.s_MarkerPickAll.Begin();
			VisualElement result = Panel.PerformPick(root, point, picked);
			Panel.s_MarkerPickAll.End();
			return result;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00009AE4 File Offset: 0x00007CE4
		private static VisualElement PerformPick(VisualElement root, Vector2 point, List<VisualElement> picked = null)
		{
			bool flag = root.resolvedStyle.display == DisplayStyle.None;
			VisualElement result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = root.pickingMode == PickingMode.Ignore && root.hierarchy.childCount == 0;
				if (flag2)
				{
					result = null;
				}
				else
				{
					bool flag3 = !root.worldBoundingBox.Contains(point);
					if (flag3)
					{
						result = null;
					}
					else
					{
						Vector2 localPoint = root.WorldToLocal(point);
						bool flag4 = root.ContainsPoint(localPoint);
						bool flag5 = !flag4 && root.ShouldClip();
						if (flag5)
						{
							result = null;
						}
						else
						{
							VisualElement visualElement = null;
							int childCount = root.hierarchy.childCount;
							for (int i = childCount - 1; i >= 0; i--)
							{
								VisualElement root2 = root.hierarchy[i];
								VisualElement visualElement2 = Panel.PerformPick(root2, point, picked);
								bool flag6 = visualElement == null && visualElement2 != null;
								if (flag6)
								{
									bool flag7 = picked == null;
									if (flag7)
									{
										return visualElement2;
									}
									visualElement = visualElement2;
								}
							}
							bool flag8 = root.visible && root.pickingMode == PickingMode.Position && flag4;
							if (flag8)
							{
								if (picked != null)
								{
									picked.Add(root);
								}
								bool flag9 = visualElement == null;
								if (flag9)
								{
									visualElement = root;
								}
							}
							result = visualElement;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00009C3C File Offset: 0x00007E3C
		public override VisualElement PickAll(Vector2 point, List<VisualElement> picked)
		{
			this.ValidateLayout();
			bool flag = picked != null;
			if (flag)
			{
				picked.Clear();
			}
			return Panel.PickAll(this.visualTree, point, picked);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00009C74 File Offset: 0x00007E74
		public override VisualElement Pick(Vector2 point)
		{
			this.ValidateLayout();
			Vector2 p;
			bool flag;
			VisualElement topElementUnderPointer = this.m_TopElementUnderPointers.GetTopElementUnderPointer(PointerId.mousePointerId, out p, out flag);
			bool flag2 = !flag && Panel.<Pick>g__PixelOf|99_0(p) == Panel.<Pick>g__PixelOf|99_0(point);
			VisualElement result;
			if (flag2)
			{
				result = topElementUnderPointer;
			}
			else
			{
				result = Panel.PickAll(this.visualTree, point, null);
			}
			return result;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00009CD4 File Offset: 0x00007ED4
		public override void ValidateLayout()
		{
			bool flag = !this.m_ValidatingLayout;
			if (flag)
			{
				this.m_ValidatingLayout = true;
				this.m_MarkerLayout.Begin();
				this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Styles);
				this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Layout);
				this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.TransformClip);
				this.m_MarkerLayout.End();
				this.m_ValidatingLayout = false;
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00009D3E File Offset: 0x00007F3E
		public override void UpdateAnimations()
		{
			this.m_MarkerAnimations.Begin();
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Animation);
			this.m_MarkerAnimations.End();
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00009D66 File Offset: 0x00007F66
		public override void UpdateBindings()
		{
			this.m_MarkerBindings.Begin();
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Bindings);
			this.m_MarkerBindings.End();
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00009D8E File Offset: 0x00007F8E
		public override void ApplyStyles()
		{
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Styles);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00009DA0 File Offset: 0x00007FA0
		private void UpdateForRepaint()
		{
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.ViewData);
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Styles);
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Layout);
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.TransformClip);
			this.m_VisualTreeUpdater.UpdateVisualTreePhase(VisualTreeUpdatePhase.Repaint);
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060002A4 RID: 676 RVA: 0x00009DF0 File Offset: 0x00007FF0
		// (remove) Token: 0x060002A5 RID: 677 RVA: 0x00009E24 File Offset: 0x00008024
		internal static event Action<Panel> beforeAnyRepaint
		{
			[CompilerGenerated]
			add
			{
				Action<Panel> action = Panel.beforeAnyRepaint;
				Action<Panel> action2;
				do
				{
					action2 = action;
					Action<Panel> value2 = (Action<Panel>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<Panel>>(ref Panel.beforeAnyRepaint, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<Panel> action = Panel.beforeAnyRepaint;
				Action<Panel> action2;
				do
				{
					action2 = action;
					Action<Panel> value2 = (Action<Panel>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<Panel>>(ref Panel.beforeAnyRepaint, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00009E58 File Offset: 0x00008058
		public override void Repaint(Event e)
		{
			this.m_RepaintVersion = this.version;
			bool flag = this.contextType == ContextType.Editor;
			if (flag)
			{
				base.pixelsPerPoint = GUIUtility.pixelsPerPoint;
			}
			this.repaintData.repaintEvent = e;
			using (this.m_MarkerBeforeUpdate.Auto())
			{
				base.InvokeBeforeUpdate();
			}
			Action<Panel> action = Panel.beforeAnyRepaint;
			if (action != null)
			{
				action(this);
			}
			using (this.m_MarkerUpdate.Auto())
			{
				this.UpdateForRepaint();
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00009F14 File Offset: 0x00008114
		internal override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			this.m_Version += 1U;
			this.m_VisualTreeUpdater.OnVersionChanged(ve, versionChangeType);
			bool flag = (versionChangeType & VersionChangeType.Hierarchy) == VersionChangeType.Hierarchy;
			if (flag)
			{
				this.m_HierarchyVersion += 1U;
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00009F56 File Offset: 0x00008156
		internal override void SetUpdater(IVisualTreeUpdater updater, VisualTreeUpdatePhase phase)
		{
			this.m_VisualTreeUpdater.SetUpdater(updater, phase);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00009F68 File Offset: 0x00008168
		internal override IVisualTreeUpdater GetUpdater(VisualTreeUpdatePhase phase)
		{
			return this.m_VisualTreeUpdater.GetUpdater(phase);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00009F86 File Offset: 0x00008186
		// Note: this type is marked as 'beforefieldinit'.
		static Panel()
		{
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00009F97 File Offset: 0x00008197
		[CompilerGenerated]
		internal static Vector2Int <Pick>g__PixelOf|99_0(Vector2 p)
		{
			return Vector2Int.FloorToInt(p);
		}

		// Token: 0x04000124 RID: 292
		private VisualElement m_RootContainer;

		// Token: 0x04000125 RID: 293
		private VisualTreeUpdater m_VisualTreeUpdater;

		// Token: 0x04000126 RID: 294
		private IStylePropertyAnimationSystem m_StylePropertyAnimationSystem;

		// Token: 0x04000127 RID: 295
		private string m_PanelName;

		// Token: 0x04000128 RID: 296
		private uint m_Version = 0U;

		// Token: 0x04000129 RID: 297
		private uint m_RepaintVersion = 0U;

		// Token: 0x0400012A RID: 298
		private uint m_HierarchyVersion = 0U;

		// Token: 0x0400012B RID: 299
		private ProfilerMarker m_MarkerBeforeUpdate;

		// Token: 0x0400012C RID: 300
		private ProfilerMarker m_MarkerUpdate;

		// Token: 0x0400012D RID: 301
		private ProfilerMarker m_MarkerLayout;

		// Token: 0x0400012E RID: 302
		private ProfilerMarker m_MarkerBindings;

		// Token: 0x0400012F RID: 303
		private ProfilerMarker m_MarkerAnimations;

		// Token: 0x04000130 RID: 304
		private static ProfilerMarker s_MarkerPickAll = new ProfilerMarker("Panel.PickAll");

		// Token: 0x04000131 RID: 305
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private EventDispatcher <dispatcher>k__BackingField;

		// Token: 0x04000132 RID: 306
		private TimerEventScheduler m_Scheduler;

		// Token: 0x04000133 RID: 307
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ScriptableObject <ownerObject>k__BackingField;

		// Token: 0x04000134 RID: 308
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private ContextType <contextType>k__BackingField;

		// Token: 0x04000135 RID: 309
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private SavePersistentViewData <saveViewData>k__BackingField;

		// Token: 0x04000136 RID: 310
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private GetViewDataDictionary <getViewDataDictionary>k__BackingField;

		// Token: 0x04000137 RID: 311
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private FocusController <focusController>k__BackingField;

		// Token: 0x04000138 RID: 312
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private EventInterests <IMGUIEventInterests>k__BackingField;

		// Token: 0x04000139 RID: 313
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static LoadResourceFunction <loadResourceFunc>k__BackingField;

		// Token: 0x0400013A RID: 314
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static TimeMsFunction <TimeSinceStartup>k__BackingField;

		// Token: 0x0400013B RID: 315
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int <IMGUIContainersCount>k__BackingField;

		// Token: 0x0400013C RID: 316
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private IMGUIContainer <rootIMGUIContainer>k__BackingField;

		// Token: 0x0400013D RID: 317
		private Shader m_StandardShader;

		// Token: 0x0400013E RID: 318
		private AtlasBase m_Atlas;

		// Token: 0x0400013F RID: 319
		private bool m_ValidatingLayout = false;

		// Token: 0x04000140 RID: 320
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action<Panel> beforeAnyRepaint;
	}
}
