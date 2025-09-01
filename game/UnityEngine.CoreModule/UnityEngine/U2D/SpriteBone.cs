using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.U2D
{
	// Token: 0x02000272 RID: 626
	[NativeType(CodegenOptions.Custom, "ScriptingSpriteBone")]
	[NativeHeader("Runtime/2D/Common/SpriteDataMarshalling.h")]
	[MovedFrom("UnityEngine.Experimental.U2D")]
	[NativeHeader("Runtime/2D/Common/SpriteDataAccess.h")]
	[RequiredByNativeCode]
	[Serializable]
	public struct SpriteBone
	{
		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001B24 RID: 6948 RVA: 0x0002B908 File Offset: 0x00029B08
		// (set) Token: 0x06001B25 RID: 6949 RVA: 0x0002B920 File Offset: 0x00029B20
		public string name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				this.m_Name = value;
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001B26 RID: 6950 RVA: 0x0002B92C File Offset: 0x00029B2C
		// (set) Token: 0x06001B27 RID: 6951 RVA: 0x0002B944 File Offset: 0x00029B44
		public string guid
		{
			get
			{
				return this.m_Guid;
			}
			set
			{
				this.m_Guid = value;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001B28 RID: 6952 RVA: 0x0002B950 File Offset: 0x00029B50
		// (set) Token: 0x06001B29 RID: 6953 RVA: 0x0002B968 File Offset: 0x00029B68
		public Vector3 position
		{
			get
			{
				return this.m_Position;
			}
			set
			{
				this.m_Position = value;
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001B2A RID: 6954 RVA: 0x0002B974 File Offset: 0x00029B74
		// (set) Token: 0x06001B2B RID: 6955 RVA: 0x0002B98C File Offset: 0x00029B8C
		public Quaternion rotation
		{
			get
			{
				return this.m_Rotation;
			}
			set
			{
				this.m_Rotation = value;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001B2C RID: 6956 RVA: 0x0002B998 File Offset: 0x00029B98
		// (set) Token: 0x06001B2D RID: 6957 RVA: 0x0002B9B0 File Offset: 0x00029BB0
		public float length
		{
			get
			{
				return this.m_Length;
			}
			set
			{
				this.m_Length = value;
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001B2E RID: 6958 RVA: 0x0002B9BC File Offset: 0x00029BBC
		// (set) Token: 0x06001B2F RID: 6959 RVA: 0x0002B9D4 File Offset: 0x00029BD4
		public int parentId
		{
			get
			{
				return this.m_ParentId;
			}
			set
			{
				this.m_ParentId = value;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001B30 RID: 6960 RVA: 0x0002B9E0 File Offset: 0x00029BE0
		// (set) Token: 0x06001B31 RID: 6961 RVA: 0x0002B9F8 File Offset: 0x00029BF8
		public Color32 color
		{
			get
			{
				return this.m_Color;
			}
			set
			{
				this.m_Color = value;
			}
		}

		// Token: 0x040008F0 RID: 2288
		[NativeName("name")]
		[SerializeField]
		private string m_Name;

		// Token: 0x040008F1 RID: 2289
		[NativeName("guid")]
		[SerializeField]
		private string m_Guid;

		// Token: 0x040008F2 RID: 2290
		[NativeName("position")]
		[SerializeField]
		private Vector3 m_Position;

		// Token: 0x040008F3 RID: 2291
		[SerializeField]
		[NativeName("rotation")]
		private Quaternion m_Rotation;

		// Token: 0x040008F4 RID: 2292
		[NativeName("length")]
		[SerializeField]
		private float m_Length;

		// Token: 0x040008F5 RID: 2293
		[NativeName("parentId")]
		[SerializeField]
		private int m_ParentId;

		// Token: 0x040008F6 RID: 2294
		[NativeName("color")]
		[SerializeField]
		private Color32 m_Color;
	}
}
