using System;
using System.Collections.Generic;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	// Token: 0x020000F9 RID: 249
	internal class VisualTreeStyleUpdater : BaseVisualTreeUpdater
	{
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x0001C524 File Offset: 0x0001A724
		// (set) Token: 0x060007B7 RID: 1975 RVA: 0x0001C52C File Offset: 0x0001A72C
		public VisualTreeStyleUpdaterTraversal traversal
		{
			get
			{
				return this.m_StyleContextHierarchyTraversal;
			}
			set
			{
				this.m_StyleContextHierarchyTraversal = value;
				BaseVisualElementPanel panel = base.panel;
				if (panel != null)
				{
					panel.visualTree.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Transform);
				}
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x0001C552 File Offset: 0x0001A752
		public override ProfilerMarker profilerMarker
		{
			get
			{
				return VisualTreeStyleUpdater.s_ProfilerMarker;
			}
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0001C55C File Offset: 0x0001A75C
		public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			bool flag = (versionChangeType & (VersionChangeType.StyleSheet | VersionChangeType.TransitionProperty)) == (VersionChangeType)0;
			if (!flag)
			{
				this.m_Version += 1U;
				bool flag2 = (versionChangeType & VersionChangeType.StyleSheet) > (VersionChangeType)0;
				if (flag2)
				{
					bool isApplyingStyles = this.m_IsApplyingStyles;
					if (isApplyingStyles)
					{
						this.m_ApplyStyleUpdateList.Add(ve);
					}
					else
					{
						this.m_StyleContextHierarchyTraversal.AddChangedElement(ve, versionChangeType);
					}
				}
				bool flag3 = (versionChangeType & VersionChangeType.TransitionProperty) > (VersionChangeType)0;
				if (flag3)
				{
					this.m_TransitionPropertyUpdateList.Add(ve);
				}
			}
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0001C5E0 File Offset: 0x0001A7E0
		public override void Update()
		{
			bool flag = this.m_Version == this.m_LastVersion;
			if (!flag)
			{
				this.m_LastVersion = this.m_Version;
				this.ApplyStyles();
				this.m_StyleContextHierarchyTraversal.Clear();
				foreach (VisualElement ve in this.m_ApplyStyleUpdateList)
				{
					this.m_StyleContextHierarchyTraversal.AddChangedElement(ve, VersionChangeType.StyleSheet);
				}
				this.m_ApplyStyleUpdateList.Clear();
				foreach (VisualElement visualElement in this.m_TransitionPropertyUpdateList)
				{
					bool flag2 = visualElement.hasRunningAnimations || visualElement.hasCompletedAnimations;
					if (flag2)
					{
						ComputedTransitionUtils.UpdateComputedTransitions(visualElement.computedStyle);
						this.m_StyleContextHierarchyTraversal.CancelAnimationsWithNoTransitionProperty(visualElement, visualElement.computedStyle);
					}
				}
				this.m_TransitionPropertyUpdateList.Clear();
			}
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0001C70C File Offset: 0x0001A90C
		private void ApplyStyles()
		{
			Debug.Assert(base.visualTree.panel != null);
			this.m_IsApplyingStyles = true;
			this.m_StyleContextHierarchyTraversal.PrepareTraversal(base.panel.scaledPixelsPerPoint);
			this.m_StyleContextHierarchyTraversal.Traverse(base.visualTree);
			this.m_IsApplyingStyles = false;
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0001C765 File Offset: 0x0001A965
		public VisualTreeStyleUpdater()
		{
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0001C7A4 File Offset: 0x0001A9A4
		// Note: this type is marked as 'beforefieldinit'.
		static VisualTreeStyleUpdater()
		{
		}

		// Token: 0x04000324 RID: 804
		private HashSet<VisualElement> m_ApplyStyleUpdateList = new HashSet<VisualElement>();

		// Token: 0x04000325 RID: 805
		private HashSet<VisualElement> m_TransitionPropertyUpdateList = new HashSet<VisualElement>();

		// Token: 0x04000326 RID: 806
		private bool m_IsApplyingStyles = false;

		// Token: 0x04000327 RID: 807
		private uint m_Version = 0U;

		// Token: 0x04000328 RID: 808
		private uint m_LastVersion = 0U;

		// Token: 0x04000329 RID: 809
		private VisualTreeStyleUpdaterTraversal m_StyleContextHierarchyTraversal = new VisualTreeStyleUpdaterTraversal();

		// Token: 0x0400032A RID: 810
		private static readonly string s_Description = "Update Style";

		// Token: 0x0400032B RID: 811
		private static readonly ProfilerMarker s_ProfilerMarker = new ProfilerMarker(VisualTreeStyleUpdater.s_Description);
	}
}
