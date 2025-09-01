using System;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	// Token: 0x020000FD RID: 253
	internal class VisualTreeTransformClipUpdater : BaseVisualTreeUpdater
	{
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x0001D279 File Offset: 0x0001B479
		public override ProfilerMarker profilerMarker
		{
			get
			{
				return VisualTreeTransformClipUpdater.s_ProfilerMarker;
			}
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0001D280 File Offset: 0x0001B480
		public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			bool flag = (versionChangeType & (VersionChangeType.Hierarchy | VersionChangeType.Overflow | VersionChangeType.BorderWidth | VersionChangeType.Transform | VersionChangeType.Size | VersionChangeType.Picking)) == (VersionChangeType)0;
			if (!flag)
			{
				bool flag2 = (versionChangeType & VersionChangeType.Transform) > (VersionChangeType)0;
				bool flag3 = (versionChangeType & (VersionChangeType.Overflow | VersionChangeType.BorderWidth | VersionChangeType.Transform | VersionChangeType.Size)) > (VersionChangeType)0;
				flag2 = (flag2 && !ve.isWorldTransformDirty);
				flag3 = (flag3 && !ve.isWorldClipDirty);
				bool flag4 = flag2 || flag3;
				if (flag4)
				{
					VisualTreeTransformClipUpdater.DirtyHierarchy(ve, flag2, flag3);
				}
				VisualTreeTransformClipUpdater.DirtyBoundingBoxHierarchy(ve);
				this.m_Version += 1U;
			}
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0001D2FC File Offset: 0x0001B4FC
		private static void DirtyHierarchy(VisualElement ve, bool mustDirtyWorldTransform, bool mustDirtyWorldClip)
		{
			if (mustDirtyWorldTransform)
			{
				ve.isWorldTransformDirty = true;
				ve.isWorldBoundingBoxDirty = true;
			}
			if (mustDirtyWorldClip)
			{
				ve.isWorldClipDirty = true;
			}
			int childCount = ve.hierarchy.childCount;
			for (int i = 0; i < childCount; i++)
			{
				VisualElement visualElement = ve.hierarchy[i];
				bool flag = (mustDirtyWorldTransform && !visualElement.isWorldTransformDirty) || (mustDirtyWorldClip && !visualElement.isWorldClipDirty);
				if (flag)
				{
					VisualTreeTransformClipUpdater.DirtyHierarchy(visualElement, mustDirtyWorldTransform, mustDirtyWorldClip);
				}
			}
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0001D398 File Offset: 0x0001B598
		private static void DirtyBoundingBoxHierarchy(VisualElement ve)
		{
			ve.isBoundingBoxDirty = true;
			ve.isWorldBoundingBoxDirty = true;
			VisualElement parent = ve.hierarchy.parent;
			while (parent != null && !parent.isBoundingBoxDirty)
			{
				parent.isBoundingBoxDirty = true;
				parent.isWorldBoundingBoxDirty = true;
				parent = parent.hierarchy.parent;
			}
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0001D3FC File Offset: 0x0001B5FC
		public override void Update()
		{
			bool flag = this.m_Version == this.m_LastVersion;
			if (!flag)
			{
				this.m_LastVersion = this.m_Version;
				base.panel.UpdateElementUnderPointers();
				base.panel.visualTree.UpdateBoundingBox();
			}
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0001D447 File Offset: 0x0001B647
		public VisualTreeTransformClipUpdater()
		{
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0001D45E File Offset: 0x0001B65E
		// Note: this type is marked as 'beforefieldinit'.
		static VisualTreeTransformClipUpdater()
		{
		}

		// Token: 0x0400033A RID: 826
		private uint m_Version = 0U;

		// Token: 0x0400033B RID: 827
		private uint m_LastVersion = 0U;

		// Token: 0x0400033C RID: 828
		private static readonly string s_Description = "Update Transform";

		// Token: 0x0400033D RID: 829
		private static readonly ProfilerMarker s_ProfilerMarker = new ProfilerMarker(VisualTreeTransformClipUpdater.s_Description);
	}
}
