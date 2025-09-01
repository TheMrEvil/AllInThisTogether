using System;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	[AttributeUsage(AttributeTargets.Class)]
	public class CustomGridBrushAttribute : Attribute
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public bool hideAssetInstances
		{
			get
			{
				return this.m_HideAssetInstances;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002068 File Offset: 0x00000268
		public bool hideDefaultInstance
		{
			get
			{
				return this.m_HideDefaultInstance;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002080 File Offset: 0x00000280
		public bool defaultBrush
		{
			get
			{
				return this.m_DefaultBrush;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002098 File Offset: 0x00000298
		public string defaultName
		{
			get
			{
				return this.m_DefaultName;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020B0 File Offset: 0x000002B0
		public CustomGridBrushAttribute()
		{
			this.m_HideAssetInstances = false;
			this.m_HideDefaultInstance = false;
			this.m_DefaultBrush = false;
			this.m_DefaultName = "";
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020DA File Offset: 0x000002DA
		public CustomGridBrushAttribute(bool hideAssetInstances, bool hideDefaultInstance, bool defaultBrush, string defaultName)
		{
			this.m_HideAssetInstances = hideAssetInstances;
			this.m_HideDefaultInstance = hideDefaultInstance;
			this.m_DefaultBrush = defaultBrush;
			this.m_DefaultName = defaultName;
		}

		// Token: 0x04000001 RID: 1
		private bool m_HideAssetInstances;

		// Token: 0x04000002 RID: 2
		private bool m_HideDefaultInstance;

		// Token: 0x04000003 RID: 3
		private bool m_DefaultBrush;

		// Token: 0x04000004 RID: 4
		private string m_DefaultName;
	}
}
