using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Profiling;
using UnityEngine.UIElements.Experimental;

namespace UnityEngine.UIElements
{
	// Token: 0x020000F4 RID: 244
	internal class VisualElementAnimationSystem : BaseVisualTreeUpdater
	{
		// Token: 0x0600079C RID: 1948 RVA: 0x0001BFA0 File Offset: 0x0001A1A0
		private long CurrentTimeMs()
		{
			return Panel.TimeSinceStartupMs();
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x0001BFB7 File Offset: 0x0001A1B7
		public override ProfilerMarker profilerMarker
		{
			get
			{
				return VisualElementAnimationSystem.s_ProfilerMarker;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x0001BFBE File Offset: 0x0001A1BE
		private static ProfilerMarker stylePropertyAnimationProfilerMarker
		{
			get
			{
				return VisualElementAnimationSystem.s_StylePropertyAnimationProfilerMarker;
			}
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0001BFC5 File Offset: 0x0001A1C5
		public void UnregisterAnimation(IValueAnimationUpdate anim)
		{
			this.m_Animations.Remove(anim);
			this.m_IterationListDirty = true;
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0001BFDC File Offset: 0x0001A1DC
		public void UnregisterAnimations(List<IValueAnimationUpdate> anims)
		{
			foreach (IValueAnimationUpdate item in anims)
			{
				this.m_Animations.Remove(item);
			}
			this.m_IterationListDirty = true;
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0001C03C File Offset: 0x0001A23C
		public void RegisterAnimation(IValueAnimationUpdate anim)
		{
			this.m_Animations.Add(anim);
			this.m_HasNewAnimations = true;
			this.m_IterationListDirty = true;
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0001C05C File Offset: 0x0001A25C
		public void RegisterAnimations(List<IValueAnimationUpdate> anims)
		{
			foreach (IValueAnimationUpdate item in anims)
			{
				this.m_Animations.Add(item);
			}
			this.m_HasNewAnimations = true;
			this.m_IterationListDirty = true;
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0001C0C4 File Offset: 0x0001A2C4
		public override void Update()
		{
			long num = Panel.TimeSinceStartupMs();
			bool iterationListDirty = this.m_IterationListDirty;
			if (iterationListDirty)
			{
				this.m_IterationList = this.m_Animations.ToList<IValueAnimationUpdate>();
				this.m_IterationListDirty = false;
			}
			bool flag = this.m_HasNewAnimations || this.lastUpdate != num;
			if (flag)
			{
				foreach (IValueAnimationUpdate valueAnimationUpdate in this.m_IterationList)
				{
					valueAnimationUpdate.Tick(num);
				}
				this.m_HasNewAnimations = false;
				this.lastUpdate = num;
			}
			IStylePropertyAnimationSystem styleAnimationSystem = base.panel.styleAnimationSystem;
			using (VisualElementAnimationSystem.stylePropertyAnimationProfilerMarker.Auto())
			{
				styleAnimationSystem.Update();
			}
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00002166 File Offset: 0x00000366
		public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0001C1B8 File Offset: 0x0001A3B8
		public VisualElementAnimationSystem()
		{
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0001C1E5 File Offset: 0x0001A3E5
		// Note: this type is marked as 'beforefieldinit'.
		static VisualElementAnimationSystem()
		{
		}

		// Token: 0x0400030D RID: 781
		private HashSet<IValueAnimationUpdate> m_Animations = new HashSet<IValueAnimationUpdate>();

		// Token: 0x0400030E RID: 782
		private List<IValueAnimationUpdate> m_IterationList = new List<IValueAnimationUpdate>();

		// Token: 0x0400030F RID: 783
		private bool m_HasNewAnimations = false;

		// Token: 0x04000310 RID: 784
		private bool m_IterationListDirty = false;

		// Token: 0x04000311 RID: 785
		private static readonly string s_Description = "Animation Update";

		// Token: 0x04000312 RID: 786
		private static readonly ProfilerMarker s_ProfilerMarker = new ProfilerMarker(VisualElementAnimationSystem.s_Description);

		// Token: 0x04000313 RID: 787
		private static readonly string s_StylePropertyAnimationDescription = "StylePropertyAnimation Update";

		// Token: 0x04000314 RID: 788
		private static readonly ProfilerMarker s_StylePropertyAnimationProfilerMarker = new ProfilerMarker(VisualElementAnimationSystem.s_StylePropertyAnimationDescription);

		// Token: 0x04000315 RID: 789
		private long lastUpdate;
	}
}
