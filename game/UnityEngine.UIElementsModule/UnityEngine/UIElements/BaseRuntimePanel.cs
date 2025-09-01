using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace UnityEngine.UIElements
{
	// Token: 0x02000060 RID: 96
	internal abstract class BaseRuntimePanel : Panel
	{
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002AC RID: 684 RVA: 0x00009F9F File Offset: 0x0000819F
		// (set) Token: 0x060002AD RID: 685 RVA: 0x00009FA8 File Offset: 0x000081A8
		public GameObject selectableGameObject
		{
			get
			{
				return this.m_SelectableGameObject;
			}
			set
			{
				bool flag = this.m_SelectableGameObject != value;
				if (flag)
				{
					this.AssignPanelToComponents(null);
					this.m_SelectableGameObject = value;
					this.AssignPanelToComponents(this);
				}
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002AE RID: 686 RVA: 0x00009FDF File Offset: 0x000081DF
		// (set) Token: 0x060002AF RID: 687 RVA: 0x00009FE8 File Offset: 0x000081E8
		public float sortingPriority
		{
			get
			{
				return this.m_SortingPriority;
			}
			set
			{
				bool flag = !Mathf.Approximately(this.m_SortingPriority, value);
				if (flag)
				{
					this.m_SortingPriority = value;
					bool flag2 = this.contextType == ContextType.Player;
					if (flag2)
					{
						UIElementsRuntimeUtility.SetPanelOrderingDirty();
					}
				}
			}
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060002B0 RID: 688 RVA: 0x0000A028 File Offset: 0x00008228
		// (remove) Token: 0x060002B1 RID: 689 RVA: 0x0000A060 File Offset: 0x00008260
		public event Action destroyed
		{
			[CompilerGenerated]
			add
			{
				Action action = this.destroyed;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.destroyed, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.destroyed;
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.destroyed, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000A098 File Offset: 0x00008298
		protected BaseRuntimePanel(ScriptableObject ownerObject, EventDispatcher dispatcher = null) : base(ownerObject, ContextType.Player, dispatcher)
		{
			this.m_RuntimePanelCreationIndex = BaseRuntimePanel.s_CurrentRuntimePanelCounter++;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000A0EC File Offset: 0x000082EC
		protected override void Dispose(bool disposing)
		{
			bool disposed = base.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					Action action = this.destroyed;
					if (action != null)
					{
						action();
					}
				}
				base.Dispose(disposing);
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000A128 File Offset: 0x00008328
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x0000A140 File Offset: 0x00008340
		internal override Shader standardWorldSpaceShader
		{
			get
			{
				return this.m_StandardWorldSpaceShader;
			}
			set
			{
				bool flag = this.m_StandardWorldSpaceShader != value;
				if (flag)
				{
					this.m_StandardWorldSpaceShader = value;
					base.InvokeStandardWorldSpaceShaderChanged();
				}
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000A170 File Offset: 0x00008370
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x0000A188 File Offset: 0x00008388
		internal bool drawToCameras
		{
			get
			{
				return this.m_DrawToCameras;
			}
			set
			{
				bool flag = this.m_DrawToCameras != value;
				if (flag)
				{
					this.m_DrawToCameras = value;
					UIRRepaintUpdater uirrepaintUpdater = this.GetUpdater(VisualTreeUpdatePhase.Repaint) as UIRRepaintUpdater;
					if (uirrepaintUpdater != null)
					{
						uirrepaintUpdater.DestroyRenderChain();
					}
				}
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000A1C7 File Offset: 0x000083C7
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x0000A1CF File Offset: 0x000083CF
		internal int targetDisplay
		{
			[CompilerGenerated]
			get
			{
				return this.<targetDisplay>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<targetDisplay>k__BackingField = value;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000A1D8 File Offset: 0x000083D8
		internal int screenRenderingWidth
		{
			get
			{
				return (this.targetDisplay > 0 && this.targetDisplay < Display.displays.Length) ? Display.displays[this.targetDisplay].renderingWidth : Screen.width;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000A20A File Offset: 0x0000840A
		internal int screenRenderingHeight
		{
			get
			{
				return (this.targetDisplay > 0 && this.targetDisplay < Display.displays.Length) ? Display.displays[this.targetDisplay].renderingHeight : Screen.height;
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000A23C File Offset: 0x0000843C
		public override void Repaint(Event e)
		{
			bool flag = this.targetTexture == null;
			if (flag)
			{
				RenderTexture active = RenderTexture.active;
				int num = (active != null) ? active.width : this.screenRenderingWidth;
				int num2 = (active != null) ? active.height : this.screenRenderingHeight;
				GL.Viewport(new Rect(0f, 0f, (float)num, (float)num2));
				base.Repaint(e);
			}
			else
			{
				Camera current = Camera.current;
				RenderTexture active2 = RenderTexture.active;
				Camera.SetupCurrent(null);
				RenderTexture.active = this.targetTexture;
				GL.Viewport(new Rect(0f, 0f, (float)this.targetTexture.width, (float)this.targetTexture.height));
				base.Repaint(e);
				Camera.SetupCurrent(current);
				RenderTexture.active = active2;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000A31A File Offset: 0x0000851A
		// (set) Token: 0x060002BE RID: 702 RVA: 0x0000A322 File Offset: 0x00008522
		public Func<Vector2, Vector2> screenToPanelSpace
		{
			get
			{
				return this.m_ScreenToPanelSpace;
			}
			set
			{
				this.m_ScreenToPanelSpace = (value ?? BaseRuntimePanel.DefaultScreenToPanelSpace);
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000A334 File Offset: 0x00008534
		internal Vector2 ScreenToPanel(Vector2 screen)
		{
			return this.screenToPanelSpace(screen) / base.scale;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000A360 File Offset: 0x00008560
		internal bool ScreenToPanel(Vector2 screenPosition, Vector2 screenDelta, out Vector2 panelPosition, out Vector2 panelDelta, bool allowOutside = false)
		{
			panelPosition = this.ScreenToPanel(screenPosition);
			bool flag = !allowOutside;
			Vector2 vector;
			if (flag)
			{
				Rect layout = this.visualTree.layout;
				bool flag2 = !layout.Contains(panelPosition);
				if (flag2)
				{
					panelDelta = screenDelta;
					return false;
				}
				vector = this.ScreenToPanel(screenPosition - screenDelta);
				bool flag3 = !layout.Contains(vector);
				if (flag3)
				{
					panelDelta = screenDelta;
					return true;
				}
			}
			else
			{
				vector = this.ScreenToPanel(screenPosition - screenDelta);
			}
			panelDelta = panelPosition - vector;
			return true;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000A410 File Offset: 0x00008610
		private void AssignPanelToComponents(BaseRuntimePanel panel)
		{
			bool flag = this.selectableGameObject == null;
			if (!flag)
			{
				List<IRuntimePanelComponent> list = ObjectListPool<IRuntimePanelComponent>.Get();
				try
				{
					this.selectableGameObject.GetComponents<IRuntimePanelComponent>(list);
					foreach (IRuntimePanelComponent runtimePanelComponent in list)
					{
						runtimePanelComponent.panel = panel;
					}
				}
				finally
				{
					ObjectListPool<IRuntimePanelComponent>.Release(list);
				}
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000A4A4 File Offset: 0x000086A4
		internal void PointerLeavesPanel(int pointerId, Vector2 position)
		{
			base.ClearCachedElementUnderPointer(pointerId, null);
			base.CommitElementUnderPointers();
			PointerDeviceState.SavePointerPosition(pointerId, position, null, this.contextType);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000A4C6 File Offset: 0x000086C6
		internal void PointerEntersPanel(int pointerId, Vector2 position)
		{
			PointerDeviceState.SavePointerPosition(pointerId, position, this, this.contextType);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000A4D8 File Offset: 0x000086D8
		// Note: this type is marked as 'beforefieldinit'.
		static BaseRuntimePanel()
		{
		}

		// Token: 0x04000141 RID: 321
		private GameObject m_SelectableGameObject;

		// Token: 0x04000142 RID: 322
		private static int s_CurrentRuntimePanelCounter = 0;

		// Token: 0x04000143 RID: 323
		internal readonly int m_RuntimePanelCreationIndex;

		// Token: 0x04000144 RID: 324
		private float m_SortingPriority = 0f;

		// Token: 0x04000145 RID: 325
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action destroyed;

		// Token: 0x04000146 RID: 326
		private Shader m_StandardWorldSpaceShader;

		// Token: 0x04000147 RID: 327
		private bool m_DrawToCameras;

		// Token: 0x04000148 RID: 328
		internal RenderTexture targetTexture = null;

		// Token: 0x04000149 RID: 329
		internal Matrix4x4 panelToWorld = Matrix4x4.identity;

		// Token: 0x0400014A RID: 330
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private int <targetDisplay>k__BackingField;

		// Token: 0x0400014B RID: 331
		internal static readonly Func<Vector2, Vector2> DefaultScreenToPanelSpace = (Vector2 p) => p;

		// Token: 0x0400014C RID: 332
		private Func<Vector2, Vector2> m_ScreenToPanelSpace = BaseRuntimePanel.DefaultScreenToPanelSpace;

		// Token: 0x02000061 RID: 97
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060002C5 RID: 709 RVA: 0x0000A4F5 File Offset: 0x000086F5
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060002C6 RID: 710 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c()
			{
			}

			// Token: 0x060002C7 RID: 711 RVA: 0x0000A501 File Offset: 0x00008701
			internal Vector2 <.cctor>b__44_0(Vector2 p)
			{
				return p;
			}

			// Token: 0x0400014D RID: 333
			public static readonly BaseRuntimePanel.<>c <>9 = new BaseRuntimePanel.<>c();
		}
	}
}
