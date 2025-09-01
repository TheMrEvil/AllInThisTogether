using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000245 RID: 581
	[Serializable]
	public class DynamicAtlasSettings
	{
		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x0600118F RID: 4495 RVA: 0x000454D8 File Offset: 0x000436D8
		// (set) Token: 0x06001190 RID: 4496 RVA: 0x000454E0 File Offset: 0x000436E0
		public int minAtlasSize
		{
			get
			{
				return this.m_MinAtlasSize;
			}
			set
			{
				this.m_MinAtlasSize = value;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06001191 RID: 4497 RVA: 0x000454E9 File Offset: 0x000436E9
		// (set) Token: 0x06001192 RID: 4498 RVA: 0x000454F1 File Offset: 0x000436F1
		public int maxAtlasSize
		{
			get
			{
				return this.m_MaxAtlasSize;
			}
			set
			{
				this.m_MaxAtlasSize = value;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06001193 RID: 4499 RVA: 0x000454FA File Offset: 0x000436FA
		// (set) Token: 0x06001194 RID: 4500 RVA: 0x00045502 File Offset: 0x00043702
		public int maxSubTextureSize
		{
			get
			{
				return this.m_MaxSubTextureSize;
			}
			set
			{
				this.m_MaxSubTextureSize = value;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06001195 RID: 4501 RVA: 0x0004550B File Offset: 0x0004370B
		// (set) Token: 0x06001196 RID: 4502 RVA: 0x00045513 File Offset: 0x00043713
		public DynamicAtlasFilters activeFilters
		{
			get
			{
				return (DynamicAtlasFilters)this.m_ActiveFilters;
			}
			set
			{
				this.m_ActiveFilters = (DynamicAtlasFiltersInternal)value;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06001197 RID: 4503 RVA: 0x0004551C File Offset: 0x0004371C
		public static DynamicAtlasFilters defaultFilters
		{
			get
			{
				return DynamicAtlas.defaultFilters;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06001198 RID: 4504 RVA: 0x00045523 File Offset: 0x00043723
		// (set) Token: 0x06001199 RID: 4505 RVA: 0x0004552B File Offset: 0x0004372B
		public DynamicAtlasCustomFilter customFilter
		{
			get
			{
				return this.m_CustomFilter;
			}
			set
			{
				this.m_CustomFilter = value;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x0600119A RID: 4506 RVA: 0x00045534 File Offset: 0x00043734
		public static DynamicAtlasSettings defaults
		{
			get
			{
				return new DynamicAtlasSettings
				{
					minAtlasSize = 64,
					maxAtlasSize = 4096,
					maxSubTextureSize = 64,
					activeFilters = DynamicAtlasSettings.defaultFilters,
					customFilter = null
				};
			}
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x000020C2 File Offset: 0x000002C2
		public DynamicAtlasSettings()
		{
		}

		// Token: 0x040007D0 RID: 2000
		[SerializeField]
		[HideInInspector]
		private int m_MinAtlasSize;

		// Token: 0x040007D1 RID: 2001
		[HideInInspector]
		[SerializeField]
		private int m_MaxAtlasSize;

		// Token: 0x040007D2 RID: 2002
		[SerializeField]
		[HideInInspector]
		private int m_MaxSubTextureSize;

		// Token: 0x040007D3 RID: 2003
		[SerializeField]
		[HideInInspector]
		private DynamicAtlasFiltersInternal m_ActiveFilters;

		// Token: 0x040007D4 RID: 2004
		private DynamicAtlasCustomFilter m_CustomFilter;
	}
}
