using System;
using System.Collections.Generic;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	// Token: 0x02000103 RID: 259
	internal class VisualTreeViewDataUpdater : BaseVisualTreeUpdater
	{
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x0001D7EB File Offset: 0x0001B9EB
		public override ProfilerMarker profilerMarker
		{
			get
			{
				return VisualTreeViewDataUpdater.s_ProfilerMarker;
			}
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0001D7F4 File Offset: 0x0001B9F4
		public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			bool flag = (versionChangeType & VersionChangeType.ViewData) != VersionChangeType.ViewData;
			if (!flag)
			{
				this.m_Version += 1U;
				this.m_UpdateList.Add(ve);
				this.PropagateToParents(ve);
			}
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0001D834 File Offset: 0x0001BA34
		public override void Update()
		{
			bool flag = this.m_Version == this.m_LastVersion;
			if (!flag)
			{
				int num = 0;
				while (this.m_LastVersion != this.m_Version)
				{
					this.m_LastVersion = this.m_Version;
					this.ValidateViewDataOnSubTree(base.visualTree, true);
					num++;
					bool flag2 = num > 5;
					if (flag2)
					{
						string str = "UIElements: Too many children recursively added that rely on persistent view data: ";
						VisualElement visualTree = base.visualTree;
						Debug.LogError(str + ((visualTree != null) ? visualTree.ToString() : null));
						break;
					}
				}
				this.m_UpdateList.Clear();
				this.m_ParentList.Clear();
			}
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0001D8D4 File Offset: 0x0001BAD4
		private void ValidateViewDataOnSubTree(VisualElement ve, bool enablePersistence)
		{
			enablePersistence = ve.IsViewDataPersitenceSupportedOnChildren(enablePersistence);
			bool flag = this.m_UpdateList.Contains(ve);
			if (flag)
			{
				this.m_UpdateList.Remove(ve);
				ve.OnViewDataReady(enablePersistence);
			}
			bool flag2 = this.m_ParentList.Contains(ve);
			if (flag2)
			{
				this.m_ParentList.Remove(ve);
				int childCount = ve.hierarchy.childCount;
				for (int i = 0; i < childCount; i++)
				{
					this.ValidateViewDataOnSubTree(ve.hierarchy[i], enablePersistence);
				}
			}
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0001D970 File Offset: 0x0001BB70
		private void PropagateToParents(VisualElement ve)
		{
			for (VisualElement parent = ve.hierarchy.parent; parent != null; parent = parent.hierarchy.parent)
			{
				bool flag = !this.m_ParentList.Add(parent);
				if (flag)
				{
					break;
				}
			}
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0001D9BE File Offset: 0x0001BBBE
		public VisualTreeViewDataUpdater()
		{
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0001D9EB File Offset: 0x0001BBEB
		// Note: this type is marked as 'beforefieldinit'.
		static VisualTreeViewDataUpdater()
		{
		}

		// Token: 0x0400034C RID: 844
		private HashSet<VisualElement> m_UpdateList = new HashSet<VisualElement>();

		// Token: 0x0400034D RID: 845
		private HashSet<VisualElement> m_ParentList = new HashSet<VisualElement>();

		// Token: 0x0400034E RID: 846
		private const int kMaxValidatePersistentDataCount = 5;

		// Token: 0x0400034F RID: 847
		private uint m_Version = 0U;

		// Token: 0x04000350 RID: 848
		private uint m_LastVersion = 0U;

		// Token: 0x04000351 RID: 849
		private static readonly string s_Description = "Update ViewData";

		// Token: 0x04000352 RID: 850
		private static readonly ProfilerMarker s_ProfilerMarker = new ProfilerMarker(VisualTreeViewDataUpdater.s_Description);
	}
}
