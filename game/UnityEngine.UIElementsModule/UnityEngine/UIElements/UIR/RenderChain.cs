using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Profiling;
using UnityEngine.UIElements.UIR.Implementation;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000311 RID: 785
	internal class RenderChain : IDisposable
	{
		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x060019BD RID: 6589 RVA: 0x0006C0D4 File Offset: 0x0006A2D4
		internal RenderChainCommand firstCommand
		{
			get
			{
				return this.m_FirstCommand;
			}
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x0006C0EC File Offset: 0x0006A2EC
		static RenderChain()
		{
			Utility.RegisterIntermediateRenderers += RenderChain.OnRegisterIntermediateRenderers;
			Utility.RenderNodeExecute += RenderChain.OnRenderNodeExecute;
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x0006C190 File Offset: 0x0006A390
		public RenderChain(BaseVisualElementPanel panel)
		{
			this.Constructor(panel, new UIRenderDevice(0U, 0U), panel.atlas, new VectorImageManager(panel.atlas));
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x0006C254 File Offset: 0x0006A454
		protected RenderChain(BaseVisualElementPanel panel, UIRenderDevice device, AtlasBase atlas, VectorImageManager vectorImageManager)
		{
			this.Constructor(panel, device, atlas, vectorImageManager);
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x0006C304 File Offset: 0x0006A504
		private void Constructor(BaseVisualElementPanel panelObj, UIRenderDevice deviceObj, AtlasBase atlas, VectorImageManager vectorImageMan)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			this.m_DirtyTracker.heads = new List<VisualElement>(8);
			this.m_DirtyTracker.tails = new List<VisualElement>(8);
			this.m_DirtyTracker.minDepths = new int[5];
			this.m_DirtyTracker.maxDepths = new int[5];
			this.m_DirtyTracker.Reset();
			bool flag = this.m_RenderNodesData.Count < 1;
			if (flag)
			{
				this.m_RenderNodesData.Add(new RenderChain.RenderNodeData
				{
					matPropBlock = new MaterialPropertyBlock()
				});
			}
			this.panel = panelObj;
			this.device = deviceObj;
			this.atlas = atlas;
			this.vectorImageManager = vectorImageMan;
			this.shaderInfoAllocator.Construct();
			this.painter = new UIRStylePainter(this);
			Font.textureRebuilt += this.OnFontReset;
			BaseRuntimePanel baseRuntimePanel = this.panel as BaseRuntimePanel;
			bool flag2 = baseRuntimePanel != null && baseRuntimePanel.drawToCameras;
			if (flag2)
			{
				this.drawInCameras = true;
				this.m_StaticIndex = RenderChain.RenderChainStaticIndexAllocator.AllocateIndex(this);
			}
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x0006C428 File Offset: 0x0006A628
		private void Destructor()
		{
			bool flag = this.m_StaticIndex >= 0;
			if (flag)
			{
				RenderChain.RenderChainStaticIndexAllocator.FreeIndex(this.m_StaticIndex);
			}
			this.m_StaticIndex = -1;
			RenderChainCommand firstCommand = this.m_FirstCommand;
			for (VisualElement visualElement = RenderChain.GetFirstElementInPanel((firstCommand != null) ? firstCommand.owner : null); visualElement != null; visualElement = visualElement.renderChainData.next)
			{
				this.ResetTextures(visualElement);
			}
			UIRUtility.Destroy(this.m_DefaultMat);
			UIRUtility.Destroy(this.m_DefaultWorldSpaceMat);
			this.m_DefaultMat = (this.m_DefaultWorldSpaceMat = null);
			Font.textureRebuilt -= this.OnFontReset;
			UIRStylePainter painter = this.painter;
			if (painter != null)
			{
				painter.Dispose();
			}
			UIRTextUpdatePainter textUpdatePainter = this.m_TextUpdatePainter;
			if (textUpdatePainter != null)
			{
				textUpdatePainter.Dispose();
			}
			VectorImageManager vectorImageManager = this.vectorImageManager;
			if (vectorImageManager != null)
			{
				vectorImageManager.Dispose();
			}
			this.shaderInfoAllocator.Dispose();
			UIRenderDevice device = this.device;
			if (device != null)
			{
				device.Dispose();
			}
			this.painter = null;
			this.m_TextUpdatePainter = null;
			this.atlas = null;
			this.shaderInfoAllocator = default(UIRVEShaderInfoAllocator);
			this.device = null;
			this.m_ActiveRenderNodes = 0;
			this.m_RenderNodesData.Clear();
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x060019C3 RID: 6595 RVA: 0x0006C55D File Offset: 0x0006A75D
		// (set) Token: 0x060019C4 RID: 6596 RVA: 0x0006C565 File Offset: 0x0006A765
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

		// Token: 0x060019C5 RID: 6597 RVA: 0x0006C56E File Offset: 0x0006A76E
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x0006C580 File Offset: 0x0006A780
		protected void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.Destructor();
				}
				this.disposed = true;
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x060019C7 RID: 6599 RVA: 0x0006C5B0 File Offset: 0x0006A7B0
		internal ChainBuilderStats stats
		{
			get
			{
				return this.m_Stats;
			}
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x0006C5C8 File Offset: 0x0006A7C8
		public void ProcessChanges()
		{
			RenderChain.s_MarkerProcess.Begin();
			this.m_Stats = default(ChainBuilderStats);
			this.m_Stats.elementsAdded = this.m_Stats.elementsAdded + this.m_StatsElementsAdded;
			this.m_Stats.elementsRemoved = this.m_Stats.elementsRemoved + this.m_StatsElementsRemoved;
			this.m_StatsElementsAdded = (this.m_StatsElementsRemoved = 0U);
			this.m_DirtyTracker.dirtyID = this.m_DirtyTracker.dirtyID + 1U;
			int num = 0;
			RenderDataDirtyTypes renderDataDirtyTypes = RenderDataDirtyTypes.Clipping | RenderDataDirtyTypes.ClippingHierarchy;
			RenderDataDirtyTypes dirtyTypesInverse = ~renderDataDirtyTypes;
			RenderChain.s_MarkerClipProcessing.Begin();
			for (int i = this.m_DirtyTracker.minDepths[num]; i <= this.m_DirtyTracker.maxDepths[num]; i++)
			{
				VisualElement visualElement = this.m_DirtyTracker.heads[i];
				while (visualElement != null)
				{
					VisualElement nextDirty = visualElement.renderChainData.nextDirty;
					bool flag = (visualElement.renderChainData.dirtiedValues & renderDataDirtyTypes) > RenderDataDirtyTypes.None;
					if (flag)
					{
						bool flag2 = visualElement.renderChainData.isInChain && visualElement.renderChainData.dirtyID != this.m_DirtyTracker.dirtyID;
						if (flag2)
						{
							RenderEvents.ProcessOnClippingChanged(this, visualElement, this.m_DirtyTracker.dirtyID, ref this.m_Stats);
						}
						this.m_DirtyTracker.ClearDirty(visualElement, dirtyTypesInverse);
					}
					visualElement = nextDirty;
					this.m_Stats.dirtyProcessed = this.m_Stats.dirtyProcessed + 1U;
				}
			}
			RenderChain.s_MarkerClipProcessing.End();
			this.m_DirtyTracker.dirtyID = this.m_DirtyTracker.dirtyID + 1U;
			num = 1;
			renderDataDirtyTypes = (RenderDataDirtyTypes.Opacity | RenderDataDirtyTypes.OpacityHierarchy);
			dirtyTypesInverse = ~renderDataDirtyTypes;
			RenderChain.s_MarkerOpacityProcessing.Begin();
			for (int j = this.m_DirtyTracker.minDepths[num]; j <= this.m_DirtyTracker.maxDepths[num]; j++)
			{
				VisualElement visualElement2 = this.m_DirtyTracker.heads[j];
				while (visualElement2 != null)
				{
					VisualElement nextDirty2 = visualElement2.renderChainData.nextDirty;
					bool flag3 = (visualElement2.renderChainData.dirtiedValues & renderDataDirtyTypes) > RenderDataDirtyTypes.None;
					if (flag3)
					{
						bool flag4 = visualElement2.renderChainData.isInChain && visualElement2.renderChainData.dirtyID != this.m_DirtyTracker.dirtyID;
						if (flag4)
						{
							RenderEvents.ProcessOnOpacityChanged(this, visualElement2, this.m_DirtyTracker.dirtyID, ref this.m_Stats);
						}
						this.m_DirtyTracker.ClearDirty(visualElement2, dirtyTypesInverse);
					}
					visualElement2 = nextDirty2;
					this.m_Stats.dirtyProcessed = this.m_Stats.dirtyProcessed + 1U;
				}
			}
			RenderChain.s_MarkerOpacityProcessing.End();
			this.m_DirtyTracker.dirtyID = this.m_DirtyTracker.dirtyID + 1U;
			num = 2;
			renderDataDirtyTypes = RenderDataDirtyTypes.Color;
			dirtyTypesInverse = ~renderDataDirtyTypes;
			RenderChain.s_MarkerColorsProcessing.Begin();
			for (int k = this.m_DirtyTracker.minDepths[num]; k <= this.m_DirtyTracker.maxDepths[num]; k++)
			{
				VisualElement visualElement3 = this.m_DirtyTracker.heads[k];
				while (visualElement3 != null)
				{
					VisualElement nextDirty3 = visualElement3.renderChainData.nextDirty;
					bool flag5 = (visualElement3.renderChainData.dirtiedValues & renderDataDirtyTypes) > RenderDataDirtyTypes.None;
					if (flag5)
					{
						bool flag6 = visualElement3.renderChainData.isInChain && visualElement3.renderChainData.dirtyID != this.m_DirtyTracker.dirtyID;
						if (flag6)
						{
							RenderEvents.ProcessOnColorChanged(this, visualElement3, this.m_DirtyTracker.dirtyID, ref this.m_Stats);
						}
						this.m_DirtyTracker.ClearDirty(visualElement3, dirtyTypesInverse);
					}
					visualElement3 = nextDirty3;
					this.m_Stats.dirtyProcessed = this.m_Stats.dirtyProcessed + 1U;
				}
			}
			RenderChain.s_MarkerColorsProcessing.End();
			this.m_DirtyTracker.dirtyID = this.m_DirtyTracker.dirtyID + 1U;
			num = 3;
			renderDataDirtyTypes = (RenderDataDirtyTypes.Transform | RenderDataDirtyTypes.ClipRectSize);
			dirtyTypesInverse = ~renderDataDirtyTypes;
			RenderChain.s_MarkerTransformProcessing.Begin();
			for (int l = this.m_DirtyTracker.minDepths[num]; l <= this.m_DirtyTracker.maxDepths[num]; l++)
			{
				VisualElement visualElement4 = this.m_DirtyTracker.heads[l];
				while (visualElement4 != null)
				{
					VisualElement nextDirty4 = visualElement4.renderChainData.nextDirty;
					bool flag7 = (visualElement4.renderChainData.dirtiedValues & renderDataDirtyTypes) > RenderDataDirtyTypes.None;
					if (flag7)
					{
						bool flag8 = visualElement4.renderChainData.isInChain && visualElement4.renderChainData.dirtyID != this.m_DirtyTracker.dirtyID;
						if (flag8)
						{
							RenderEvents.ProcessOnTransformOrSizeChanged(this, visualElement4, this.m_DirtyTracker.dirtyID, ref this.m_Stats);
						}
						this.m_DirtyTracker.ClearDirty(visualElement4, dirtyTypesInverse);
					}
					visualElement4 = nextDirty4;
					this.m_Stats.dirtyProcessed = this.m_Stats.dirtyProcessed + 1U;
				}
			}
			RenderChain.s_MarkerTransformProcessing.End();
			this.m_BlockDirtyRegistration = true;
			this.m_DirtyTracker.dirtyID = this.m_DirtyTracker.dirtyID + 1U;
			num = 4;
			renderDataDirtyTypes = (RenderDataDirtyTypes.Visuals | RenderDataDirtyTypes.VisualsHierarchy);
			dirtyTypesInverse = ~renderDataDirtyTypes;
			RenderChain.s_MarkerVisualsProcessing.Begin();
			for (int m = this.m_DirtyTracker.minDepths[num]; m <= this.m_DirtyTracker.maxDepths[num]; m++)
			{
				VisualElement visualElement5 = this.m_DirtyTracker.heads[m];
				while (visualElement5 != null)
				{
					VisualElement nextDirty5 = visualElement5.renderChainData.nextDirty;
					bool flag9 = (visualElement5.renderChainData.dirtiedValues & renderDataDirtyTypes) > RenderDataDirtyTypes.None;
					if (flag9)
					{
						bool flag10 = visualElement5.renderChainData.isInChain && visualElement5.renderChainData.dirtyID != this.m_DirtyTracker.dirtyID;
						if (flag10)
						{
							RenderEvents.ProcessOnVisualsChanged(this, visualElement5, this.m_DirtyTracker.dirtyID, ref this.m_Stats);
						}
						this.m_DirtyTracker.ClearDirty(visualElement5, dirtyTypesInverse);
					}
					visualElement5 = nextDirty5;
					this.m_Stats.dirtyProcessed = this.m_Stats.dirtyProcessed + 1U;
				}
			}
			RenderChain.s_MarkerVisualsProcessing.End();
			this.m_BlockDirtyRegistration = false;
			this.m_DirtyTracker.Reset();
			this.ProcessTextRegen(true);
			bool fontWasReset = this.m_FontWasReset;
			if (fontWasReset)
			{
				for (int n = 0; n < 2; n++)
				{
					bool flag11 = !this.m_FontWasReset;
					if (flag11)
					{
						break;
					}
					this.m_FontWasReset = false;
					this.ProcessTextRegen(false);
				}
			}
			AtlasBase atlas = this.atlas;
			if (atlas != null)
			{
				atlas.InvokeUpdateDynamicTextures(this.panel);
			}
			VectorImageManager vectorImageManager = this.vectorImageManager;
			if (vectorImageManager != null)
			{
				vectorImageManager.Commit();
			}
			this.shaderInfoAllocator.IssuePendingStorageChanges();
			UIRenderDevice device = this.device;
			if (device != null)
			{
				device.OnFrameRenderingBegin();
			}
			RenderChain.s_MarkerProcess.End();
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x0006CCAC File Offset: 0x0006AEAC
		public void Render()
		{
			Material standardMaterial = this.GetStandardMaterial();
			this.panel.InvokeUpdateMaterial(standardMaterial);
			Exception ex = null;
			bool flag = this.m_FirstCommand != null;
			if (flag)
			{
				bool flag2 = !this.drawInCameras;
				if (flag2)
				{
					Rect layout = this.panel.visualTree.layout;
					if (standardMaterial != null)
					{
						standardMaterial.SetPass(0);
					}
					Matrix4x4 mat = ProjectionUtils.Ortho(layout.xMin, layout.xMax, layout.yMax, layout.yMin, -0.001f, 1.001f);
					GL.LoadProjectionMatrix(mat);
					GL.modelview = Matrix4x4.identity;
					UIRenderDevice device = this.device;
					RenderChainCommand firstCommand = this.m_FirstCommand;
					Material initialMat = standardMaterial;
					Material defaultMat = standardMaterial;
					VectorImageManager vectorImageManager = this.vectorImageManager;
					device.EvaluateChain(firstCommand, initialMat, defaultMat, (vectorImageManager != null) ? vectorImageManager.atlas : null, this.shaderInfoAllocator.atlas, this.panel.scaledPixelsPerPoint, this.shaderInfoAllocator.transformConstants, this.shaderInfoAllocator.clipRectConstants, this.m_RenderNodesData[0].matPropBlock, true, ref ex);
				}
			}
			bool flag3 = ex != null;
			if (!flag3)
			{
				bool drawStats = this.drawStats;
				if (drawStats)
				{
					this.DrawStats();
				}
				return;
			}
			bool flag4 = GUIUtility.IsExitGUIException(ex);
			if (flag4)
			{
				throw ex;
			}
			throw new ImmediateModeException(ex);
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x0006CDEC File Offset: 0x0006AFEC
		private void ProcessTextRegen(bool timeSliced)
		{
			bool flag = (timeSliced && this.m_DirtyTextRemaining == 0) || this.m_TextElementCount == 0;
			if (!flag)
			{
				RenderChain.s_MarkerTextRegen.Begin();
				bool flag2 = this.m_TextUpdatePainter == null;
				if (flag2)
				{
					this.m_TextUpdatePainter = new UIRTextUpdatePainter();
				}
				VisualElement visualElement = this.m_FirstTextElement;
				this.m_DirtyTextStartIndex = (timeSliced ? (this.m_DirtyTextStartIndex % this.m_TextElementCount) : 0);
				for (int i = 0; i < this.m_DirtyTextStartIndex; i++)
				{
					visualElement = visualElement.renderChainData.nextText;
				}
				bool flag3 = visualElement == null;
				if (flag3)
				{
					visualElement = this.m_FirstTextElement;
				}
				int num = timeSliced ? Math.Min(50, this.m_DirtyTextRemaining) : this.m_TextElementCount;
				for (int j = 0; j < num; j++)
				{
					RenderEvents.ProcessRegenText(this, visualElement, this.m_TextUpdatePainter, this.device, ref this.m_Stats);
					visualElement = visualElement.renderChainData.nextText;
					this.m_DirtyTextStartIndex++;
					bool flag4 = visualElement == null;
					if (flag4)
					{
						visualElement = this.m_FirstTextElement;
						this.m_DirtyTextStartIndex = 0;
					}
				}
				this.m_DirtyTextRemaining = Math.Max(0, this.m_DirtyTextRemaining - num);
				bool flag5 = this.m_DirtyTextRemaining > 0;
				if (flag5)
				{
					BaseVisualElementPanel panel = this.panel;
					if (panel != null)
					{
						panel.OnVersionChanged(this.m_FirstTextElement, VersionChangeType.Transform);
					}
				}
				RenderChain.s_MarkerTextRegen.End();
			}
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x0006CF60 File Offset: 0x0006B160
		public void UIEOnChildAdded(VisualElement ve)
		{
			VisualElement parent = ve.hierarchy.parent;
			int index = (parent != null) ? parent.hierarchy.IndexOf(ve) : 0;
			bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
			if (blockDirtyRegistration)
			{
				throw new InvalidOperationException("VisualElements cannot be added to an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
			}
			bool flag = parent != null && !parent.renderChainData.isInChain;
			if (!flag)
			{
				uint num = RenderEvents.DepthFirstOnChildAdded(this, parent, ve, index, true);
				Debug.Assert(ve.renderChainData.isInChain);
				Debug.Assert(ve.panel == this.panel);
				this.UIEOnClippingChanged(ve, true);
				this.UIEOnOpacityChanged(ve, false);
				this.UIEOnVisualsChanged(ve, true);
				this.m_StatsElementsAdded += num;
			}
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x0006D020 File Offset: 0x0006B220
		public void UIEOnChildrenReordered(VisualElement ve)
		{
			bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
			if (blockDirtyRegistration)
			{
				throw new InvalidOperationException("VisualElements cannot be moved under an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
			}
			int childCount = ve.hierarchy.childCount;
			for (int i = 0; i < childCount; i++)
			{
				RenderEvents.DepthFirstOnChildRemoving(this, ve.hierarchy[i]);
			}
			for (int j = 0; j < childCount; j++)
			{
				RenderEvents.DepthFirstOnChildAdded(this, ve, ve.hierarchy[j], j, false);
			}
			this.UIEOnClippingChanged(ve, true);
			this.UIEOnOpacityChanged(ve, true);
			this.UIEOnVisualsChanged(ve, true);
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x0006D0C8 File Offset: 0x0006B2C8
		public void UIEOnChildRemoving(VisualElement ve)
		{
			bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
			if (blockDirtyRegistration)
			{
				throw new InvalidOperationException("VisualElements cannot be removed from an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
			}
			this.m_StatsElementsRemoved += RenderEvents.DepthFirstOnChildRemoving(this, ve);
			Debug.Assert(!ve.renderChainData.isInChain);
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x0006D113 File Offset: 0x0006B313
		public void StopTrackingGroupTransformElement(VisualElement ve)
		{
			this.m_LastGroupTransformElementScale.Remove(ve);
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x0006D124 File Offset: 0x0006B324
		public void UIEOnRenderHintsChanged(VisualElement ve)
		{
			bool isInChain = ve.renderChainData.isInChain;
			if (isInChain)
			{
				bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
				if (blockDirtyRegistration)
				{
					throw new InvalidOperationException("Render Hints cannot change under an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
				}
				bool flag = (ve.renderHints & RenderHints.DirtyAll) == RenderHints.DirtyDynamicColor;
				bool flag2 = flag;
				if (flag2)
				{
					this.UIEOnVisualsChanged(ve, false);
				}
				else
				{
					this.UIEOnChildRemoving(ve);
					this.UIEOnChildAdded(ve);
				}
				ve.MarkRenderHintsClean();
			}
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x0006D198 File Offset: 0x0006B398
		public void UIEOnClippingChanged(VisualElement ve, bool hierarchical)
		{
			bool isInChain = ve.renderChainData.isInChain;
			if (isInChain)
			{
				bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
				if (blockDirtyRegistration)
				{
					throw new InvalidOperationException("VisualElements cannot change clipping state under an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
				}
				this.m_DirtyTracker.RegisterDirty(ve, RenderDataDirtyTypes.Clipping | (hierarchical ? RenderDataDirtyTypes.ClippingHierarchy : RenderDataDirtyTypes.None), RenderDataDirtyTypeClasses.Clipping);
			}
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x0006D1E4 File Offset: 0x0006B3E4
		public void UIEOnOpacityChanged(VisualElement ve, bool hierarchical = false)
		{
			bool isInChain = ve.renderChainData.isInChain;
			if (isInChain)
			{
				bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
				if (blockDirtyRegistration)
				{
					throw new InvalidOperationException("VisualElements cannot change opacity under an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
				}
				this.m_DirtyTracker.RegisterDirty(ve, RenderDataDirtyTypes.Opacity | (hierarchical ? RenderDataDirtyTypes.OpacityHierarchy : RenderDataDirtyTypes.None), RenderDataDirtyTypeClasses.Opacity);
			}
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x0006D234 File Offset: 0x0006B434
		public void UIEOnColorChanged(VisualElement ve)
		{
			bool isInChain = ve.renderChainData.isInChain;
			if (isInChain)
			{
				bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
				if (blockDirtyRegistration)
				{
					throw new InvalidOperationException("VisualElements cannot change background color under an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
				}
				this.m_DirtyTracker.RegisterDirty(ve, RenderDataDirtyTypes.Color, RenderDataDirtyTypeClasses.Color);
			}
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x0006D27C File Offset: 0x0006B47C
		public void UIEOnTransformOrSizeChanged(VisualElement ve, bool transformChanged, bool clipRectSizeChanged)
		{
			bool isInChain = ve.renderChainData.isInChain;
			if (isInChain)
			{
				bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
				if (blockDirtyRegistration)
				{
					throw new InvalidOperationException("VisualElements cannot change size or transform under an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
				}
				RenderDataDirtyTypes dirtyTypes = (transformChanged ? RenderDataDirtyTypes.Transform : RenderDataDirtyTypes.None) | (clipRectSizeChanged ? RenderDataDirtyTypes.ClipRectSize : RenderDataDirtyTypes.None);
				this.m_DirtyTracker.RegisterDirty(ve, dirtyTypes, RenderDataDirtyTypeClasses.TransformSize);
			}
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x0006D2D0 File Offset: 0x0006B4D0
		public void UIEOnVisualsChanged(VisualElement ve, bool hierarchical)
		{
			bool isInChain = ve.renderChainData.isInChain;
			if (isInChain)
			{
				bool blockDirtyRegistration = this.m_BlockDirtyRegistration;
				if (blockDirtyRegistration)
				{
					throw new InvalidOperationException("VisualElements cannot be marked for dirty repaint under an active visual tree during generateVisualContent callback execution nor during visual tree rendering");
				}
				this.m_DirtyTracker.RegisterDirty(ve, RenderDataDirtyTypes.Visuals | (hierarchical ? RenderDataDirtyTypes.VisualsHierarchy : RenderDataDirtyTypes.None), RenderDataDirtyTypeClasses.Visuals);
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x060019D5 RID: 6613 RVA: 0x0006D31D File Offset: 0x0006B51D
		// (set) Token: 0x060019D6 RID: 6614 RVA: 0x0006D325 File Offset: 0x0006B525
		internal BaseVisualElementPanel panel
		{
			[CompilerGenerated]
			get
			{
				return this.<panel>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<panel>k__BackingField = value;
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x060019D7 RID: 6615 RVA: 0x0006D32E File Offset: 0x0006B52E
		// (set) Token: 0x060019D8 RID: 6616 RVA: 0x0006D336 File Offset: 0x0006B536
		internal UIRenderDevice device
		{
			[CompilerGenerated]
			get
			{
				return this.<device>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<device>k__BackingField = value;
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x060019D9 RID: 6617 RVA: 0x0006D33F File Offset: 0x0006B53F
		// (set) Token: 0x060019DA RID: 6618 RVA: 0x0006D347 File Offset: 0x0006B547
		internal AtlasBase atlas
		{
			[CompilerGenerated]
			get
			{
				return this.<atlas>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<atlas>k__BackingField = value;
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x060019DB RID: 6619 RVA: 0x0006D350 File Offset: 0x0006B550
		// (set) Token: 0x060019DC RID: 6620 RVA: 0x0006D358 File Offset: 0x0006B558
		internal VectorImageManager vectorImageManager
		{
			[CompilerGenerated]
			get
			{
				return this.<vectorImageManager>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<vectorImageManager>k__BackingField = value;
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x060019DD RID: 6621 RVA: 0x0006D361 File Offset: 0x0006B561
		// (set) Token: 0x060019DE RID: 6622 RVA: 0x0006D369 File Offset: 0x0006B569
		internal UIRStylePainter painter
		{
			[CompilerGenerated]
			get
			{
				return this.<painter>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<painter>k__BackingField = value;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x060019DF RID: 6623 RVA: 0x0006D372 File Offset: 0x0006B572
		// (set) Token: 0x060019E0 RID: 6624 RVA: 0x0006D37A File Offset: 0x0006B57A
		internal bool drawStats
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

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x060019E1 RID: 6625 RVA: 0x0006D383 File Offset: 0x0006B583
		// (set) Token: 0x060019E2 RID: 6626 RVA: 0x0006D38B File Offset: 0x0006B58B
		internal bool drawInCameras
		{
			[CompilerGenerated]
			get
			{
				return this.<drawInCameras>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<drawInCameras>k__BackingField = value;
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x060019E3 RID: 6627 RVA: 0x0006D394 File Offset: 0x0006B594
		// (set) Token: 0x060019E4 RID: 6628 RVA: 0x0006D3AC File Offset: 0x0006B5AC
		internal Shader defaultShader
		{
			get
			{
				return this.m_DefaultShader;
			}
			set
			{
				bool flag = this.m_DefaultShader == value;
				if (!flag)
				{
					this.m_DefaultShader = value;
					UIRUtility.Destroy(this.m_DefaultMat);
					this.m_DefaultMat = null;
				}
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x060019E5 RID: 6629 RVA: 0x0006D3E8 File Offset: 0x0006B5E8
		// (set) Token: 0x060019E6 RID: 6630 RVA: 0x0006D400 File Offset: 0x0006B600
		internal Shader defaultWorldSpaceShader
		{
			get
			{
				return this.m_DefaultWorldSpaceShader;
			}
			set
			{
				bool flag = this.m_DefaultWorldSpaceShader == value;
				if (!flag)
				{
					this.m_DefaultWorldSpaceShader = value;
					UIRUtility.Destroy(this.m_DefaultWorldSpaceMat);
					this.m_DefaultWorldSpaceMat = null;
				}
			}
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x0006D43C File Offset: 0x0006B63C
		internal Material GetStandardMaterial()
		{
			bool flag = this.m_DefaultMat == null && this.m_DefaultShader != null;
			if (flag)
			{
				this.m_DefaultMat = new Material(this.m_DefaultShader);
				this.m_DefaultMat.hideFlags |= HideFlags.DontSaveInEditor;
			}
			return this.m_DefaultMat;
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x0006D49C File Offset: 0x0006B69C
		internal Material GetStandardWorldSpaceMaterial()
		{
			bool flag = this.m_DefaultWorldSpaceMat == null && this.m_DefaultWorldSpaceShader != null;
			if (flag)
			{
				this.m_DefaultWorldSpaceMat = new Material(this.m_DefaultWorldSpaceShader);
				this.m_DefaultWorldSpaceMat.hideFlags |= HideFlags.DontSaveInEditor;
			}
			return this.m_DefaultWorldSpaceMat;
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x0006D4FC File Offset: 0x0006B6FC
		internal void EnsureFitsDepth(int depth)
		{
			this.m_DirtyTracker.EnsureFits(depth);
		}

		// Token: 0x060019EA RID: 6634 RVA: 0x0006D50C File Offset: 0x0006B70C
		internal void ChildWillBeRemoved(VisualElement ve)
		{
			bool flag = ve.renderChainData.dirtiedValues > RenderDataDirtyTypes.None;
			if (flag)
			{
				this.m_DirtyTracker.ClearDirty(ve, ~ve.renderChainData.dirtiedValues);
			}
			Debug.Assert(ve.renderChainData.dirtiedValues == RenderDataDirtyTypes.None);
			Debug.Assert(ve.renderChainData.prevDirty == null);
			Debug.Assert(ve.renderChainData.nextDirty == null);
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x0006D584 File Offset: 0x0006B784
		internal RenderChainCommand AllocCommand()
		{
			RenderChainCommand renderChainCommand = this.m_CommandPool.Get();
			renderChainCommand.Reset();
			return renderChainCommand;
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x0006D5AC File Offset: 0x0006B7AC
		internal void FreeCommand(RenderChainCommand cmd)
		{
			bool flag = cmd.state.material != null;
			if (flag)
			{
				this.m_CustomMaterialCommands--;
			}
			cmd.Reset();
			this.m_CommandPool.Return(cmd);
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x0006D5F4 File Offset: 0x0006B7F4
		internal void OnRenderCommandAdded(RenderChainCommand command)
		{
			bool flag = command.prev == null;
			if (flag)
			{
				this.m_FirstCommand = command;
			}
			bool flag2 = command.state.material != null;
			if (flag2)
			{
				this.m_CustomMaterialCommands++;
			}
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x0006D63C File Offset: 0x0006B83C
		internal void OnRenderCommandsRemoved(RenderChainCommand firstCommand, RenderChainCommand lastCommand)
		{
			bool flag = firstCommand.prev == null;
			if (flag)
			{
				this.m_FirstCommand = lastCommand.next;
			}
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x0006D664 File Offset: 0x0006B864
		internal void AddTextElement(VisualElement ve)
		{
			bool flag = this.m_FirstTextElement != null;
			if (flag)
			{
				this.m_FirstTextElement.renderChainData.prevText = ve;
				ve.renderChainData.nextText = this.m_FirstTextElement;
			}
			this.m_FirstTextElement = ve;
			this.m_TextElementCount++;
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x0006D6B8 File Offset: 0x0006B8B8
		internal void RemoveTextElement(VisualElement ve)
		{
			bool flag = ve.renderChainData.prevText != null;
			if (flag)
			{
				ve.renderChainData.prevText.renderChainData.nextText = ve.renderChainData.nextText;
			}
			bool flag2 = ve.renderChainData.nextText != null;
			if (flag2)
			{
				ve.renderChainData.nextText.renderChainData.prevText = ve.renderChainData.prevText;
			}
			bool flag3 = this.m_FirstTextElement == ve;
			if (flag3)
			{
				this.m_FirstTextElement = ve.renderChainData.nextText;
			}
			ve.renderChainData.prevText = (ve.renderChainData.nextText = null);
			this.m_TextElementCount--;
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x0006D770 File Offset: 0x0006B970
		internal void OnGroupTransformElementChangedTransform(VisualElement ve)
		{
			Vector2 vector;
			bool flag = !this.m_LastGroupTransformElementScale.TryGetValue(ve, out vector) || ve.worldTransform.m00 != vector.x || ve.worldTransform.m11 != vector.y;
			if (flag)
			{
				this.m_DirtyTextRemaining = this.m_TextElementCount;
				this.m_LastGroupTransformElementScale[ve] = new Vector2(ve.worldTransform.m00, ve.worldTransform.m11);
			}
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x0006D7F4 File Offset: 0x0006B9F4
		private unsafe static RenderChain.RenderNodeData AccessRenderNodeData(IntPtr obj)
		{
			int* ptr = (int*)obj.ToPointer();
			RenderChain renderChain = RenderChain.RenderChainStaticIndexAllocator.AccessIndex(*ptr);
			return renderChain.m_RenderNodesData[ptr[1]];
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x0006D828 File Offset: 0x0006BA28
		private static void OnRenderNodeExecute(IntPtr obj)
		{
			RenderChain.RenderNodeData renderNodeData = RenderChain.AccessRenderNodeData(obj);
			Exception ex = null;
			renderNodeData.device.EvaluateChain(renderNodeData.firstCommand, renderNodeData.initialMaterial, renderNodeData.standardMaterial, renderNodeData.vectorAtlas, renderNodeData.shaderInfoAtlas, renderNodeData.dpiScale, renderNodeData.transformConstants, renderNodeData.clipRectConstants, renderNodeData.matPropBlock, false, ref ex);
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x0006D884 File Offset: 0x0006BA84
		private static void OnRegisterIntermediateRenderers(Camera camera)
		{
			int num = 0;
			Dictionary<int, Panel>.Enumerator panelsIterator = UIElementsUtility.GetPanelsIterator();
			while (panelsIterator.MoveNext())
			{
				KeyValuePair<int, Panel> keyValuePair = panelsIterator.Current;
				Panel value = keyValuePair.Value;
				UIRRepaintUpdater uirrepaintUpdater = value.GetUpdater(VisualTreeUpdatePhase.Repaint) as UIRRepaintUpdater;
				RenderChain renderChain = (uirrepaintUpdater != null) ? uirrepaintUpdater.renderChain : null;
				bool flag = renderChain == null || renderChain.m_StaticIndex < 0 || renderChain.m_FirstCommand == null;
				if (!flag)
				{
					BaseRuntimePanel baseRuntimePanel = (BaseRuntimePanel)value;
					Material standardWorldSpaceMaterial = renderChain.GetStandardWorldSpaceMaterial();
					RenderChain.RenderNodeData renderNodeData = default(RenderChain.RenderNodeData);
					renderNodeData.device = renderChain.device;
					renderNodeData.standardMaterial = standardWorldSpaceMaterial;
					VectorImageManager vectorImageManager = renderChain.vectorImageManager;
					renderNodeData.vectorAtlas = ((vectorImageManager != null) ? vectorImageManager.atlas : null);
					renderNodeData.shaderInfoAtlas = renderChain.shaderInfoAllocator.atlas;
					renderNodeData.dpiScale = baseRuntimePanel.scaledPixelsPerPoint;
					renderNodeData.transformConstants = renderChain.shaderInfoAllocator.transformConstants;
					renderNodeData.clipRectConstants = renderChain.shaderInfoAllocator.clipRectConstants;
					bool flag2 = renderChain.m_CustomMaterialCommands == 0;
					if (flag2)
					{
						renderNodeData.initialMaterial = standardWorldSpaceMaterial;
						renderNodeData.firstCommand = renderChain.m_FirstCommand;
						RenderChain.OnRegisterIntermediateRendererMat(baseRuntimePanel, renderChain, ref renderNodeData, camera, num++);
					}
					else
					{
						Material material = null;
						RenderChainCommand renderChainCommand = renderChain.m_FirstCommand;
						RenderChainCommand renderChainCommand2 = renderChainCommand;
						while (renderChainCommand != null)
						{
							bool flag3 = renderChainCommand.type > CommandType.Draw;
							if (flag3)
							{
								renderChainCommand = renderChainCommand.next;
							}
							else
							{
								Material material2 = (renderChainCommand.state.material == null) ? standardWorldSpaceMaterial : renderChainCommand.state.material;
								bool flag4 = material2 != material;
								if (flag4)
								{
									bool flag5 = material != null;
									if (flag5)
									{
										renderNodeData.initialMaterial = material;
										renderNodeData.firstCommand = renderChainCommand2;
										RenderChain.OnRegisterIntermediateRendererMat(baseRuntimePanel, renderChain, ref renderNodeData, camera, num++);
										renderChainCommand2 = renderChainCommand;
									}
									material = material2;
								}
								renderChainCommand = renderChainCommand.next;
							}
						}
						bool flag6 = renderChainCommand2 != null;
						if (flag6)
						{
							renderNodeData.initialMaterial = material;
							renderNodeData.firstCommand = renderChainCommand2;
							RenderChain.OnRegisterIntermediateRendererMat(baseRuntimePanel, renderChain, ref renderNodeData, camera, num++);
						}
					}
				}
			}
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x0006DAB0 File Offset: 0x0006BCB0
		private unsafe static void OnRegisterIntermediateRendererMat(BaseRuntimePanel rtp, RenderChain renderChain, ref RenderChain.RenderNodeData rnd, Camera camera, int sameDistanceSortPriority)
		{
			int activeRenderNodes = renderChain.m_ActiveRenderNodes;
			renderChain.m_ActiveRenderNodes = activeRenderNodes + 1;
			int num = activeRenderNodes;
			bool flag = num < renderChain.m_RenderNodesData.Count;
			if (flag)
			{
				RenderChain.RenderNodeData renderNodeData = renderChain.m_RenderNodesData[num];
				rnd.matPropBlock = renderNodeData.matPropBlock;
				renderChain.m_RenderNodesData[num] = rnd;
			}
			else
			{
				rnd.matPropBlock = new MaterialPropertyBlock();
				num = renderChain.m_RenderNodesData.Count;
				renderChain.m_RenderNodesData.Add(rnd);
			}
			int* ptr = stackalloc int[(UIntPtr)8];
			*ptr = renderChain.m_StaticIndex;
			ptr[1] = num;
			Utility.RegisterIntermediateRenderer(camera, rnd.initialMaterial, rtp.panelToWorld, new Bounds(Vector3.zero, new Vector3(float.MaxValue, float.MaxValue, float.MaxValue)), 3, 0, false, sameDistanceSortPriority, (ulong)((long)camera.cullingMask), 2, new IntPtr((void*)ptr), 8);
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x0006DB98 File Offset: 0x0006BD98
		internal void RepaintTexturedElements()
		{
			RenderChainCommand firstCommand = this.m_FirstCommand;
			for (VisualElement visualElement = RenderChain.GetFirstElementInPanel((firstCommand != null) ? firstCommand.owner : null); visualElement != null; visualElement = visualElement.renderChainData.next)
			{
				bool flag = visualElement.renderChainData.textures != null;
				if (flag)
				{
					this.UIEOnVisualsChanged(visualElement, false);
				}
			}
			this.UIEOnOpacityChanged(this.panel.visualTree, false);
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x0006DC04 File Offset: 0x0006BE04
		private void OnFontReset(Font font)
		{
			this.m_FontWasReset = true;
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x0006DC10 File Offset: 0x0006BE10
		public void AppendTexture(VisualElement ve, Texture src, TextureId id, bool isAtlas)
		{
			BasicNode<TextureEntry> basicNode = this.m_TexturePool.Get();
			basicNode.data.source = src;
			basicNode.data.actual = id;
			basicNode.data.replaced = isAtlas;
			basicNode.AppendTo(ref ve.renderChainData.textures);
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x0006DC64 File Offset: 0x0006BE64
		public void ResetTextures(VisualElement ve)
		{
			AtlasBase atlas = this.atlas;
			TextureRegistry textureRegistry = this.m_TextureRegistry;
			BasicNodePool<TextureEntry> texturePool = this.m_TexturePool;
			BasicNode<TextureEntry> basicNode = ve.renderChainData.textures;
			ve.renderChainData.textures = null;
			while (basicNode != null)
			{
				BasicNode<TextureEntry> next = basicNode.next;
				bool replaced = basicNode.data.replaced;
				if (replaced)
				{
					atlas.ReturnAtlas(ve, basicNode.data.source as Texture2D, basicNode.data.actual);
				}
				else
				{
					textureRegistry.Release(basicNode.data.actual);
				}
				texturePool.Return(basicNode);
				basicNode = next;
			}
		}

		// Token: 0x060019FA RID: 6650 RVA: 0x0006DD08 File Offset: 0x0006BF08
		private void DrawStats()
		{
			bool flag = this.device != null;
			float num = 12f;
			Rect position = new Rect(30f, 60f, 1000f, 100f);
			GUI.Box(new Rect(20f, 40f, 200f, (float)(flag ? 380 : 256)), "UI Toolkit Draw Stats");
			GUI.Label(position, "Elements added\t: " + this.m_Stats.elementsAdded.ToString());
			position.y += num;
			GUI.Label(position, "Elements removed\t: " + this.m_Stats.elementsRemoved.ToString());
			position.y += num;
			GUI.Label(position, "Mesh allocs allocated\t: " + this.m_Stats.newMeshAllocations.ToString());
			position.y += num;
			GUI.Label(position, "Mesh allocs updated\t: " + this.m_Stats.updatedMeshAllocations.ToString());
			position.y += num;
			GUI.Label(position, "Clip update roots\t: " + this.m_Stats.recursiveClipUpdates.ToString());
			position.y += num;
			GUI.Label(position, "Clip update total\t: " + this.m_Stats.recursiveClipUpdatesExpanded.ToString());
			position.y += num;
			GUI.Label(position, "Opacity update roots\t: " + this.m_Stats.recursiveOpacityUpdates.ToString());
			position.y += num;
			GUI.Label(position, "Opacity update total\t: " + this.m_Stats.recursiveOpacityUpdatesExpanded.ToString());
			position.y += num;
			GUI.Label(position, "Xform update roots\t: " + this.m_Stats.recursiveTransformUpdates.ToString());
			position.y += num;
			GUI.Label(position, "Xform update total\t: " + this.m_Stats.recursiveTransformUpdatesExpanded.ToString());
			position.y += num;
			GUI.Label(position, "Xformed by bone\t: " + this.m_Stats.boneTransformed.ToString());
			position.y += num;
			GUI.Label(position, "Xformed by skipping\t: " + this.m_Stats.skipTransformed.ToString());
			position.y += num;
			GUI.Label(position, "Xformed by nudging\t: " + this.m_Stats.nudgeTransformed.ToString());
			position.y += num;
			GUI.Label(position, "Xformed by repaint\t: " + this.m_Stats.visualUpdateTransformed.ToString());
			position.y += num;
			GUI.Label(position, "Visual update roots\t: " + this.m_Stats.recursiveVisualUpdates.ToString());
			position.y += num;
			GUI.Label(position, "Visual update total\t: " + this.m_Stats.recursiveVisualUpdatesExpanded.ToString());
			position.y += num;
			GUI.Label(position, "Visual update flats\t: " + this.m_Stats.nonRecursiveVisualUpdates.ToString());
			position.y += num;
			GUI.Label(position, "Dirty processed\t: " + this.m_Stats.dirtyProcessed.ToString());
			position.y += num;
			GUI.Label(position, "Group-xform updates\t: " + this.m_Stats.groupTransformElementsChanged.ToString());
			position.y += num;
			GUI.Label(position, "Text regens\t: " + this.m_Stats.textUpdates.ToString());
			position.y += num;
			bool flag2 = !flag;
			if (!flag2)
			{
				position.y += num;
				UIRenderDevice.DrawStatistics drawStatistics = this.device.GatherDrawStatistics();
				GUI.Label(position, "Frame index\t: " + drawStatistics.currentFrameIndex.ToString());
				position.y += num;
				GUI.Label(position, "Command count\t: " + drawStatistics.commandCount.ToString());
				position.y += num;
				GUI.Label(position, "Draw commands\t: " + drawStatistics.drawCommandCount.ToString());
				position.y += num;
				GUI.Label(position, "Draw ranges\t: " + drawStatistics.drawRangeCount.ToString());
				position.y += num;
				GUI.Label(position, "Draw range calls\t: " + drawStatistics.drawRangeCallCount.ToString());
				position.y += num;
				GUI.Label(position, "Material sets\t: " + drawStatistics.materialSetCount.ToString());
				position.y += num;
				GUI.Label(position, "Stencil changes\t: " + drawStatistics.stencilRefChanges.ToString());
				position.y += num;
				GUI.Label(position, "Immediate draws\t: " + drawStatistics.immediateDraws.ToString());
				position.y += num;
				GUI.Label(position, "Total triangles\t: " + (drawStatistics.totalIndices / 3U).ToString());
				position.y += num;
			}
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x0006E30C File Offset: 0x0006C50C
		private static VisualElement GetFirstElementInPanel(VisualElement ve)
		{
			for (;;)
			{
				bool flag;
				if (ve != null)
				{
					VisualElement prev = ve.renderChainData.prev;
					flag = (prev != null && prev.renderChainData.isInChain);
				}
				else
				{
					flag = false;
				}
				if (!flag)
				{
					break;
				}
				ve = ve.renderChainData.prev;
			}
			return ve;
		}

		// Token: 0x04000B55 RID: 2901
		private RenderChainCommand m_FirstCommand;

		// Token: 0x04000B56 RID: 2902
		private RenderChain.DepthOrderedDirtyTracking m_DirtyTracker;

		// Token: 0x04000B57 RID: 2903
		private LinkedPool<RenderChainCommand> m_CommandPool = new LinkedPool<RenderChainCommand>(() => new RenderChainCommand(), delegate(RenderChainCommand cmd)
		{
		}, 10000);

		// Token: 0x04000B58 RID: 2904
		private BasicNodePool<TextureEntry> m_TexturePool = new BasicNodePool<TextureEntry>();

		// Token: 0x04000B59 RID: 2905
		private List<RenderChain.RenderNodeData> m_RenderNodesData = new List<RenderChain.RenderNodeData>();

		// Token: 0x04000B5A RID: 2906
		private Shader m_DefaultShader;

		// Token: 0x04000B5B RID: 2907
		private Shader m_DefaultWorldSpaceShader;

		// Token: 0x04000B5C RID: 2908
		private Material m_DefaultMat;

		// Token: 0x04000B5D RID: 2909
		private Material m_DefaultWorldSpaceMat;

		// Token: 0x04000B5E RID: 2910
		private bool m_BlockDirtyRegistration;

		// Token: 0x04000B5F RID: 2911
		private int m_StaticIndex = -1;

		// Token: 0x04000B60 RID: 2912
		private int m_ActiveRenderNodes = 0;

		// Token: 0x04000B61 RID: 2913
		private int m_CustomMaterialCommands = 0;

		// Token: 0x04000B62 RID: 2914
		private ChainBuilderStats m_Stats;

		// Token: 0x04000B63 RID: 2915
		private uint m_StatsElementsAdded;

		// Token: 0x04000B64 RID: 2916
		private uint m_StatsElementsRemoved;

		// Token: 0x04000B65 RID: 2917
		private VisualElement m_FirstTextElement;

		// Token: 0x04000B66 RID: 2918
		private UIRTextUpdatePainter m_TextUpdatePainter;

		// Token: 0x04000B67 RID: 2919
		private int m_TextElementCount;

		// Token: 0x04000B68 RID: 2920
		private int m_DirtyTextStartIndex;

		// Token: 0x04000B69 RID: 2921
		private int m_DirtyTextRemaining;

		// Token: 0x04000B6A RID: 2922
		private bool m_FontWasReset;

		// Token: 0x04000B6B RID: 2923
		private Dictionary<VisualElement, Vector2> m_LastGroupTransformElementScale = new Dictionary<VisualElement, Vector2>();

		// Token: 0x04000B6C RID: 2924
		private TextureRegistry m_TextureRegistry = TextureRegistry.instance;

		// Token: 0x04000B6D RID: 2925
		private static ProfilerMarker s_MarkerProcess = new ProfilerMarker("RenderChain.Process");

		// Token: 0x04000B6E RID: 2926
		private static ProfilerMarker s_MarkerClipProcessing = new ProfilerMarker("RenderChain.UpdateClips");

		// Token: 0x04000B6F RID: 2927
		private static ProfilerMarker s_MarkerOpacityProcessing = new ProfilerMarker("RenderChain.UpdateOpacity");

		// Token: 0x04000B70 RID: 2928
		private static ProfilerMarker s_MarkerColorsProcessing = new ProfilerMarker("RenderChain.UpdateColors");

		// Token: 0x04000B71 RID: 2929
		private static ProfilerMarker s_MarkerTransformProcessing = new ProfilerMarker("RenderChain.UpdateTransforms");

		// Token: 0x04000B72 RID: 2930
		private static ProfilerMarker s_MarkerVisualsProcessing = new ProfilerMarker("RenderChain.UpdateVisuals");

		// Token: 0x04000B73 RID: 2931
		private static ProfilerMarker s_MarkerTextRegen = new ProfilerMarker("RenderChain.RegenText");

		// Token: 0x04000B74 RID: 2932
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private bool <disposed>k__BackingField;

		// Token: 0x04000B75 RID: 2933
		internal static Action OnPreRender = null;

		// Token: 0x04000B76 RID: 2934
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private BaseVisualElementPanel <panel>k__BackingField;

		// Token: 0x04000B77 RID: 2935
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private UIRenderDevice <device>k__BackingField;

		// Token: 0x04000B78 RID: 2936
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private AtlasBase <atlas>k__BackingField;

		// Token: 0x04000B79 RID: 2937
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private VectorImageManager <vectorImageManager>k__BackingField;

		// Token: 0x04000B7A RID: 2938
		internal UIRVEShaderInfoAllocator shaderInfoAllocator;

		// Token: 0x04000B7B RID: 2939
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private UIRStylePainter <painter>k__BackingField;

		// Token: 0x04000B7C RID: 2940
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <drawStats>k__BackingField;

		// Token: 0x04000B7D RID: 2941
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <drawInCameras>k__BackingField;

		// Token: 0x02000312 RID: 786
		private struct DepthOrderedDirtyTracking
		{
			// Token: 0x060019FC RID: 6652 RVA: 0x0006E354 File Offset: 0x0006C554
			public void EnsureFits(int maxDepth)
			{
				while (this.heads.Count <= maxDepth)
				{
					this.heads.Add(null);
					this.tails.Add(null);
				}
			}

			// Token: 0x060019FD RID: 6653 RVA: 0x0006E398 File Offset: 0x0006C598
			public void RegisterDirty(VisualElement ve, RenderDataDirtyTypes dirtyTypes, RenderDataDirtyTypeClasses dirtyTypeClass)
			{
				Debug.Assert(dirtyTypes > RenderDataDirtyTypes.None);
				int hierarchyDepth = ve.renderChainData.hierarchyDepth;
				this.minDepths[(int)dirtyTypeClass] = ((hierarchyDepth < this.minDepths[(int)dirtyTypeClass]) ? hierarchyDepth : this.minDepths[(int)dirtyTypeClass]);
				this.maxDepths[(int)dirtyTypeClass] = ((hierarchyDepth > this.maxDepths[(int)dirtyTypeClass]) ? hierarchyDepth : this.maxDepths[(int)dirtyTypeClass]);
				bool flag = ve.renderChainData.dirtiedValues > RenderDataDirtyTypes.None;
				if (flag)
				{
					ve.renderChainData.dirtiedValues = (ve.renderChainData.dirtiedValues | dirtyTypes);
				}
				else
				{
					ve.renderChainData.dirtiedValues = dirtyTypes;
					bool flag2 = this.tails[hierarchyDepth] != null;
					if (flag2)
					{
						this.tails[hierarchyDepth].renderChainData.nextDirty = ve;
						ve.renderChainData.prevDirty = this.tails[hierarchyDepth];
						this.tails[hierarchyDepth] = ve;
					}
					else
					{
						List<VisualElement> list = this.heads;
						int index = hierarchyDepth;
						this.tails[hierarchyDepth] = ve;
						list[index] = ve;
					}
				}
			}

			// Token: 0x060019FE RID: 6654 RVA: 0x0006E4A0 File Offset: 0x0006C6A0
			public void ClearDirty(VisualElement ve, RenderDataDirtyTypes dirtyTypesInverse)
			{
				Debug.Assert(ve.renderChainData.dirtiedValues > RenderDataDirtyTypes.None);
				ve.renderChainData.dirtiedValues = (ve.renderChainData.dirtiedValues & dirtyTypesInverse);
				bool flag = ve.renderChainData.dirtiedValues == RenderDataDirtyTypes.None;
				if (flag)
				{
					bool flag2 = ve.renderChainData.prevDirty != null;
					if (flag2)
					{
						ve.renderChainData.prevDirty.renderChainData.nextDirty = ve.renderChainData.nextDirty;
					}
					bool flag3 = ve.renderChainData.nextDirty != null;
					if (flag3)
					{
						ve.renderChainData.nextDirty.renderChainData.prevDirty = ve.renderChainData.prevDirty;
					}
					bool flag4 = this.tails[ve.renderChainData.hierarchyDepth] == ve;
					if (flag4)
					{
						Debug.Assert(ve.renderChainData.nextDirty == null);
						this.tails[ve.renderChainData.hierarchyDepth] = ve.renderChainData.prevDirty;
					}
					bool flag5 = this.heads[ve.renderChainData.hierarchyDepth] == ve;
					if (flag5)
					{
						Debug.Assert(ve.renderChainData.prevDirty == null);
						this.heads[ve.renderChainData.hierarchyDepth] = ve.renderChainData.nextDirty;
					}
					ve.renderChainData.prevDirty = (ve.renderChainData.nextDirty = null);
				}
			}

			// Token: 0x060019FF RID: 6655 RVA: 0x0006E618 File Offset: 0x0006C818
			public void Reset()
			{
				for (int i = 0; i < this.minDepths.Length; i++)
				{
					this.minDepths[i] = int.MaxValue;
					this.maxDepths[i] = int.MinValue;
				}
			}

			// Token: 0x04000B7E RID: 2942
			public List<VisualElement> heads;

			// Token: 0x04000B7F RID: 2943
			public List<VisualElement> tails;

			// Token: 0x04000B80 RID: 2944
			public int[] minDepths;

			// Token: 0x04000B81 RID: 2945
			public int[] maxDepths;

			// Token: 0x04000B82 RID: 2946
			public uint dirtyID;
		}

		// Token: 0x02000313 RID: 787
		private struct RenderChainStaticIndexAllocator
		{
			// Token: 0x06001A00 RID: 6656 RVA: 0x0006E65C File Offset: 0x0006C85C
			public static int AllocateIndex(RenderChain renderChain)
			{
				int num = RenderChain.RenderChainStaticIndexAllocator.renderChains.IndexOf(null);
				bool flag = num >= 0;
				if (flag)
				{
					RenderChain.RenderChainStaticIndexAllocator.renderChains[num] = renderChain;
				}
				else
				{
					num = RenderChain.RenderChainStaticIndexAllocator.renderChains.Count;
					RenderChain.RenderChainStaticIndexAllocator.renderChains.Add(renderChain);
				}
				return num;
			}

			// Token: 0x06001A01 RID: 6657 RVA: 0x0006E6AE File Offset: 0x0006C8AE
			public static void FreeIndex(int index)
			{
				RenderChain.RenderChainStaticIndexAllocator.renderChains[index] = null;
			}

			// Token: 0x06001A02 RID: 6658 RVA: 0x0006E6C0 File Offset: 0x0006C8C0
			public static RenderChain AccessIndex(int index)
			{
				return RenderChain.RenderChainStaticIndexAllocator.renderChains[index];
			}

			// Token: 0x06001A03 RID: 6659 RVA: 0x0006E6DD File Offset: 0x0006C8DD
			// Note: this type is marked as 'beforefieldinit'.
			static RenderChainStaticIndexAllocator()
			{
			}

			// Token: 0x04000B83 RID: 2947
			private static List<RenderChain> renderChains = new List<RenderChain>(4);
		}

		// Token: 0x02000314 RID: 788
		private struct RenderNodeData
		{
			// Token: 0x04000B84 RID: 2948
			public Material standardMaterial;

			// Token: 0x04000B85 RID: 2949
			public Material initialMaterial;

			// Token: 0x04000B86 RID: 2950
			public MaterialPropertyBlock matPropBlock;

			// Token: 0x04000B87 RID: 2951
			public RenderChainCommand firstCommand;

			// Token: 0x04000B88 RID: 2952
			public UIRenderDevice device;

			// Token: 0x04000B89 RID: 2953
			public Texture vectorAtlas;

			// Token: 0x04000B8A RID: 2954
			public Texture shaderInfoAtlas;

			// Token: 0x04000B8B RID: 2955
			public float dpiScale;

			// Token: 0x04000B8C RID: 2956
			public NativeSlice<Transform3x4> transformConstants;

			// Token: 0x04000B8D RID: 2957
			public NativeSlice<Vector4> clipRectConstants;
		}

		// Token: 0x02000315 RID: 789
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001A04 RID: 6660 RVA: 0x0006E6EA File Offset: 0x0006C8EA
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001A05 RID: 6661 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c()
			{
			}

			// Token: 0x06001A06 RID: 6662 RVA: 0x0006E6F6 File Offset: 0x0006C8F6
			internal RenderChainCommand <.ctor>b__37_0()
			{
				return new RenderChainCommand();
			}

			// Token: 0x06001A07 RID: 6663 RVA: 0x00002166 File Offset: 0x00000366
			internal void <.ctor>b__37_1(RenderChainCommand cmd)
			{
			}

			// Token: 0x06001A08 RID: 6664 RVA: 0x0006E6F6 File Offset: 0x0006C8F6
			internal RenderChainCommand <.ctor>b__38_0()
			{
				return new RenderChainCommand();
			}

			// Token: 0x06001A09 RID: 6665 RVA: 0x00002166 File Offset: 0x00000366
			internal void <.ctor>b__38_1(RenderChainCommand cmd)
			{
			}

			// Token: 0x04000B8E RID: 2958
			public static readonly RenderChain.<>c <>9 = new RenderChain.<>c();

			// Token: 0x04000B8F RID: 2959
			public static Func<RenderChainCommand> <>9__37_0;

			// Token: 0x04000B90 RID: 2960
			public static Action<RenderChainCommand> <>9__37_1;

			// Token: 0x04000B91 RID: 2961
			public static Func<RenderChainCommand> <>9__38_0;

			// Token: 0x04000B92 RID: 2962
			public static Action<RenderChainCommand> <>9__38_1;
		}
	}
}
