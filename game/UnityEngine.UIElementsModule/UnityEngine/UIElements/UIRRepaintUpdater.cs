using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Profiling;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x02000265 RID: 613
	internal class UIRRepaintUpdater : BaseVisualTreeUpdater
	{
		// Token: 0x06001295 RID: 4757 RVA: 0x0004A624 File Offset: 0x00048824
		public UIRRepaintUpdater()
		{
			base.panelChanged += this.OnPanelChanged;
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001296 RID: 4758 RVA: 0x0004A641 File Offset: 0x00048841
		public override ProfilerMarker profilerMarker
		{
			get
			{
				return UIRRepaintUpdater.s_ProfilerMarker;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001297 RID: 4759 RVA: 0x0004A648 File Offset: 0x00048848
		// (set) Token: 0x06001298 RID: 4760 RVA: 0x0004A650 File Offset: 0x00048850
		public bool drawStats
		{
			[CompilerGenerated]
			get
			{
				return this.<drawStats>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<drawStats>k__BackingField = value;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06001299 RID: 4761 RVA: 0x0004A659 File Offset: 0x00048859
		// (set) Token: 0x0600129A RID: 4762 RVA: 0x0004A661 File Offset: 0x00048861
		public bool breakBatches
		{
			[CompilerGenerated]
			get
			{
				return this.<breakBatches>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<breakBatches>k__BackingField = value;
			}
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x0004A66C File Offset: 0x0004886C
		public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			bool flag = this.renderChain == null;
			if (!flag)
			{
				bool flag2 = (versionChangeType & VersionChangeType.Transform) > (VersionChangeType)0;
				bool flag3 = (versionChangeType & VersionChangeType.Size) > (VersionChangeType)0;
				bool flag4 = (versionChangeType & VersionChangeType.Overflow) > (VersionChangeType)0;
				bool flag5 = (versionChangeType & VersionChangeType.BorderRadius) > (VersionChangeType)0;
				bool flag6 = (versionChangeType & VersionChangeType.BorderWidth) > (VersionChangeType)0;
				bool flag7 = (versionChangeType & VersionChangeType.RenderHints) > (VersionChangeType)0;
				bool flag8 = flag7;
				if (flag8)
				{
					this.renderChain.UIEOnRenderHintsChanged(ve);
				}
				bool flag9 = flag2 || flag3 || flag6;
				if (flag9)
				{
					this.renderChain.UIEOnTransformOrSizeChanged(ve, flag2, flag3 || flag6);
				}
				bool flag10 = flag4 || flag5;
				if (flag10)
				{
					this.renderChain.UIEOnClippingChanged(ve, false);
				}
				bool flag11 = (versionChangeType & VersionChangeType.Opacity) > (VersionChangeType)0;
				if (flag11)
				{
					this.renderChain.UIEOnOpacityChanged(ve, false);
				}
				bool flag12 = (versionChangeType & VersionChangeType.Color) > (VersionChangeType)0;
				if (flag12)
				{
					this.renderChain.UIEOnColorChanged(ve);
				}
				bool flag13 = (versionChangeType & VersionChangeType.Repaint) > (VersionChangeType)0;
				if (flag13)
				{
					this.renderChain.UIEOnVisualsChanged(ve, false);
				}
			}
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x0004A774 File Offset: 0x00048974
		public override void Update()
		{
			bool flag = this.renderChain == null;
			if (flag)
			{
				this.InitRenderChain();
			}
			bool flag2 = this.renderChain == null || this.renderChain.device == null;
			if (!flag2)
			{
				this.renderChain.ProcessChanges();
				PanelClearSettings clearSettings = base.panel.clearSettings;
				bool flag3 = clearSettings.clearColor || clearSettings.clearDepthStencil;
				if (flag3)
				{
					Color color = clearSettings.color;
					color = color.RGBMultiplied(color.a);
					GL.Clear(clearSettings.clearDepthStencil, clearSettings.clearColor, color, 0.99f);
				}
				this.renderChain.drawStats = this.drawStats;
				this.renderChain.device.breakBatches = this.breakBatches;
				this.renderChain.Render();
			}
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x0004A850 File Offset: 0x00048A50
		protected virtual RenderChain CreateRenderChain()
		{
			return new RenderChain(base.panel);
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x0004A86D File Offset: 0x00048A6D
		static UIRRepaintUpdater()
		{
			Utility.GraphicsResourcesRecreate += UIRRepaintUpdater.OnGraphicsResourcesRecreate;
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x0004A89C File Offset: 0x00048A9C
		private static void OnGraphicsResourcesRecreate(bool recreate)
		{
			bool flag = !recreate;
			if (flag)
			{
				UIRenderDevice.PrepareForGfxDeviceRecreate();
			}
			Dictionary<int, Panel>.Enumerator panelsIterator = UIElementsUtility.GetPanelsIterator();
			while (panelsIterator.MoveNext())
			{
				if (recreate)
				{
					KeyValuePair<int, Panel> keyValuePair = panelsIterator.Current;
					AtlasBase atlas = keyValuePair.Value.atlas;
					if (atlas != null)
					{
						atlas.Reset();
					}
				}
				else
				{
					KeyValuePair<int, Panel> keyValuePair = panelsIterator.Current;
					UIRRepaintUpdater uirrepaintUpdater = keyValuePair.Value.GetUpdater(VisualTreeUpdatePhase.Repaint) as UIRRepaintUpdater;
					if (uirrepaintUpdater != null)
					{
						uirrepaintUpdater.DestroyRenderChain();
					}
				}
			}
			bool flag2 = !recreate;
			if (flag2)
			{
				UIRenderDevice.FlushAllPendingDeviceDisposes();
			}
			else
			{
				UIRenderDevice.WrapUpGfxDeviceRecreate();
			}
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x0004A932 File Offset: 0x00048B32
		private void OnPanelChanged(BaseVisualElementPanel obj)
		{
			this.DetachFromPanel();
			this.AttachToPanel();
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x0004A944 File Offset: 0x00048B44
		private void AttachToPanel()
		{
			Debug.Assert(this.attachedPanel == null);
			bool flag = base.panel == null;
			if (!flag)
			{
				this.attachedPanel = base.panel;
				this.attachedPanel.atlasChanged += this.OnPanelAtlasChanged;
				this.attachedPanel.standardShaderChanged += this.OnPanelStandardShaderChanged;
				this.attachedPanel.standardWorldSpaceShaderChanged += this.OnPanelStandardWorldSpaceShaderChanged;
				this.attachedPanel.hierarchyChanged += this.OnPanelHierarchyChanged;
			}
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x0004A9DC File Offset: 0x00048BDC
		private void DetachFromPanel()
		{
			bool flag = this.attachedPanel == null;
			if (!flag)
			{
				this.DestroyRenderChain();
				this.attachedPanel.atlasChanged -= this.OnPanelAtlasChanged;
				this.attachedPanel.standardShaderChanged -= this.OnPanelStandardShaderChanged;
				this.attachedPanel.standardWorldSpaceShaderChanged -= this.OnPanelStandardWorldSpaceShaderChanged;
				this.attachedPanel.hierarchyChanged -= this.OnPanelHierarchyChanged;
				this.attachedPanel = null;
			}
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x0004AA68 File Offset: 0x00048C68
		private void InitRenderChain()
		{
			this.renderChain = this.CreateRenderChain();
			bool flag = this.attachedPanel.visualTree != null;
			if (flag)
			{
				this.renderChain.UIEOnChildAdded(this.attachedPanel.visualTree);
			}
			this.OnPanelStandardShaderChanged();
			bool flag2 = base.panel.contextType == ContextType.Player;
			if (flag2)
			{
				this.OnPanelStandardWorldSpaceShaderChanged();
			}
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x0004AACC File Offset: 0x00048CCC
		internal void DestroyRenderChain()
		{
			bool flag = this.renderChain == null;
			if (!flag)
			{
				this.renderChain.Dispose();
				this.renderChain = null;
				this.ResetAllElementsDataRecursive(this.attachedPanel.visualTree);
			}
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x0004AB0E File Offset: 0x00048D0E
		private void OnPanelAtlasChanged()
		{
			this.DestroyRenderChain();
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x0004AB18 File Offset: 0x00048D18
		private void OnPanelHierarchyChanged(VisualElement ve, HierarchyChangeType changeType)
		{
			bool flag = this.renderChain == null;
			if (!flag)
			{
				switch (changeType)
				{
				case HierarchyChangeType.Add:
					this.renderChain.UIEOnChildAdded(ve);
					break;
				case HierarchyChangeType.Remove:
					this.renderChain.UIEOnChildRemoving(ve);
					break;
				case HierarchyChangeType.Move:
					this.renderChain.UIEOnChildrenReordered(ve);
					break;
				}
			}
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x0004AB7C File Offset: 0x00048D7C
		private void OnPanelStandardShaderChanged()
		{
			bool flag = this.renderChain == null;
			if (!flag)
			{
				Shader shader = base.panel.standardShader;
				bool flag2 = shader == null;
				if (flag2)
				{
					shader = Shader.Find(UIRUtility.k_DefaultShaderName);
					Debug.Assert(shader != null, "Failed to load UIElements default shader");
					bool flag3 = shader != null;
					if (flag3)
					{
						shader.hideFlags |= HideFlags.DontSaveInEditor;
					}
				}
				this.renderChain.defaultShader = shader;
			}
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x0004ABF8 File Offset: 0x00048DF8
		private void OnPanelStandardWorldSpaceShaderChanged()
		{
			bool flag = this.renderChain == null;
			if (!flag)
			{
				Shader shader = base.panel.standardWorldSpaceShader;
				bool flag2 = shader == null;
				if (flag2)
				{
					shader = Shader.Find(UIRUtility.k_DefaultWorldSpaceShaderName);
					Debug.Assert(shader != null, "Failed to load UIElements default world-space shader");
					bool flag3 = shader != null;
					if (flag3)
					{
						shader.hideFlags |= HideFlags.DontSaveInEditor;
					}
				}
				this.renderChain.defaultWorldSpaceShader = shader;
			}
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x0004AC74 File Offset: 0x00048E74
		private void ResetAllElementsDataRecursive(VisualElement ve)
		{
			ve.renderChainData = default(RenderChainVEData);
			int i = ve.hierarchy.childCount - 1;
			while (i >= 0)
			{
				this.ResetAllElementsDataRecursive(ve.hierarchy[i--]);
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060012AA RID: 4778 RVA: 0x0004ACC6 File Offset: 0x00048EC6
		// (set) Token: 0x060012AB RID: 4779 RVA: 0x0004ACCE File Offset: 0x00048ECE
		private protected bool disposed
		{
			[CompilerGenerated]
			protected get
			{
				return this.<disposed>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<disposed>k__BackingField = value;
			}
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x0004ACD8 File Offset: 0x00048ED8
		protected override void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.DetachFromPanel();
				}
				this.disposed = true;
			}
		}

		// Token: 0x040008A6 RID: 2214
		private BaseVisualElementPanel attachedPanel;

		// Token: 0x040008A7 RID: 2215
		internal RenderChain renderChain;

		// Token: 0x040008A8 RID: 2216
		private static readonly string s_Description = "Update Rendering";

		// Token: 0x040008A9 RID: 2217
		private static readonly ProfilerMarker s_ProfilerMarker = new ProfilerMarker(UIRRepaintUpdater.s_Description);

		// Token: 0x040008AA RID: 2218
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <drawStats>k__BackingField;

		// Token: 0x040008AB RID: 2219
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <breakBatches>k__BackingField;

		// Token: 0x040008AC RID: 2220
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <disposed>k__BackingField;
	}
}
