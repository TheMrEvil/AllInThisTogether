using System;
using UnityEngine.Internal;

namespace UnityEngine.Rendering
{
	// Token: 0x020003FF RID: 1023
	public struct FilteringSettings : IEquatable<FilteringSettings>
	{
		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x060022C8 RID: 8904 RVA: 0x0003A89E File Offset: 0x00038A9E
		public static FilteringSettings defaultValue
		{
			get
			{
				return new FilteringSettings(new RenderQueueRange?(RenderQueueRange.all), -1, uint.MaxValue, 0);
			}
		}

		// Token: 0x060022C9 RID: 8905 RVA: 0x0003A8B4 File Offset: 0x00038AB4
		public FilteringSettings([DefaultValue("RenderQueueRange.all")] RenderQueueRange? renderQueueRange = null, int layerMask = -1, uint renderingLayerMask = 4294967295U, int excludeMotionVectorObjects = 0)
		{
			this = default(FilteringSettings);
			this.m_RenderQueueRange = (renderQueueRange ?? RenderQueueRange.all);
			this.m_LayerMask = layerMask;
			this.m_RenderingLayerMask = renderingLayerMask;
			this.m_ExcludeMotionVectorObjects = excludeMotionVectorObjects;
			this.m_SortingLayerRange = SortingLayerRange.all;
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x060022CA RID: 8906 RVA: 0x0003A90C File Offset: 0x00038B0C
		// (set) Token: 0x060022CB RID: 8907 RVA: 0x0003A924 File Offset: 0x00038B24
		public RenderQueueRange renderQueueRange
		{
			get
			{
				return this.m_RenderQueueRange;
			}
			set
			{
				this.m_RenderQueueRange = value;
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x060022CC RID: 8908 RVA: 0x0003A930 File Offset: 0x00038B30
		// (set) Token: 0x060022CD RID: 8909 RVA: 0x0003A948 File Offset: 0x00038B48
		public int layerMask
		{
			get
			{
				return this.m_LayerMask;
			}
			set
			{
				this.m_LayerMask = value;
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x060022CE RID: 8910 RVA: 0x0003A954 File Offset: 0x00038B54
		// (set) Token: 0x060022CF RID: 8911 RVA: 0x0003A96C File Offset: 0x00038B6C
		public uint renderingLayerMask
		{
			get
			{
				return this.m_RenderingLayerMask;
			}
			set
			{
				this.m_RenderingLayerMask = value;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x060022D0 RID: 8912 RVA: 0x0003A978 File Offset: 0x00038B78
		// (set) Token: 0x060022D1 RID: 8913 RVA: 0x0003A993 File Offset: 0x00038B93
		public bool excludeMotionVectorObjects
		{
			get
			{
				return this.m_ExcludeMotionVectorObjects != 0;
			}
			set
			{
				this.m_ExcludeMotionVectorObjects = (value ? 1 : 0);
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x060022D2 RID: 8914 RVA: 0x0003A9A4 File Offset: 0x00038BA4
		// (set) Token: 0x060022D3 RID: 8915 RVA: 0x0003A9BC File Offset: 0x00038BBC
		public SortingLayerRange sortingLayerRange
		{
			get
			{
				return this.m_SortingLayerRange;
			}
			set
			{
				this.m_SortingLayerRange = value;
			}
		}

		// Token: 0x060022D4 RID: 8916 RVA: 0x0003A9C8 File Offset: 0x00038BC8
		public bool Equals(FilteringSettings other)
		{
			return this.m_RenderQueueRange.Equals(other.m_RenderQueueRange) && this.m_LayerMask == other.m_LayerMask && this.m_RenderingLayerMask == other.m_RenderingLayerMask && this.m_ExcludeMotionVectorObjects == other.m_ExcludeMotionVectorObjects;
		}

		// Token: 0x060022D5 RID: 8917 RVA: 0x0003AA1C File Offset: 0x00038C1C
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is FilteringSettings && this.Equals((FilteringSettings)obj);
		}

		// Token: 0x060022D6 RID: 8918 RVA: 0x0003AA54 File Offset: 0x00038C54
		public override int GetHashCode()
		{
			int num = this.m_RenderQueueRange.GetHashCode();
			num = (num * 397 ^ this.m_LayerMask);
			num = (num * 397 ^ (int)this.m_RenderingLayerMask);
			return num * 397 ^ this.m_ExcludeMotionVectorObjects;
		}

		// Token: 0x060022D7 RID: 8919 RVA: 0x0003AAA8 File Offset: 0x00038CA8
		public static bool operator ==(FilteringSettings left, FilteringSettings right)
		{
			return left.Equals(right);
		}

		// Token: 0x060022D8 RID: 8920 RVA: 0x0003AAC4 File Offset: 0x00038CC4
		public static bool operator !=(FilteringSettings left, FilteringSettings right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000CE4 RID: 3300
		private RenderQueueRange m_RenderQueueRange;

		// Token: 0x04000CE5 RID: 3301
		private int m_LayerMask;

		// Token: 0x04000CE6 RID: 3302
		private uint m_RenderingLayerMask;

		// Token: 0x04000CE7 RID: 3303
		private int m_ExcludeMotionVectorObjects;

		// Token: 0x04000CE8 RID: 3304
		private SortingLayerRange m_SortingLayerRange;
	}
}
