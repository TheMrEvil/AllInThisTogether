using System;
using UnityEngine.Serialization;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200001D RID: 29
	[ExcludeFromObjectFactory]
	[Serializable]
	public abstract class TextAsset : ScriptableObject
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00007A94 File Offset: 0x00005C94
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00007AAC File Offset: 0x00005CAC
		public string version
		{
			get
			{
				return this.m_Version;
			}
			internal set
			{
				this.m_Version = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00007AB8 File Offset: 0x00005CB8
		public int instanceID
		{
			get
			{
				bool flag = this.m_InstanceID == 0;
				if (flag)
				{
					this.m_InstanceID = base.GetInstanceID();
				}
				return this.m_InstanceID;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00007AEC File Offset: 0x00005CEC
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x00007B22 File Offset: 0x00005D22
		public int hashCode
		{
			get
			{
				bool flag = this.m_HashCode == 0;
				if (flag)
				{
					this.m_HashCode = TextUtilities.GetHashCodeCaseInSensitive(base.name);
				}
				return this.m_HashCode;
			}
			set
			{
				this.m_HashCode = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00007B2B File Offset: 0x00005D2B
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x00007B33 File Offset: 0x00005D33
		public Material material
		{
			get
			{
				return this.m_Material;
			}
			set
			{
				this.m_Material = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00007B3C File Offset: 0x00005D3C
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00007B8D File Offset: 0x00005D8D
		public int materialHashCode
		{
			get
			{
				bool flag = this.m_MaterialHashCode == 0;
				if (flag)
				{
					bool flag2 = this.m_Material == null;
					if (flag2)
					{
						return 0;
					}
					this.m_MaterialHashCode = TextUtilities.GetHashCodeCaseInSensitive(this.m_Material.name);
				}
				return this.m_MaterialHashCode;
			}
			set
			{
				this.m_MaterialHashCode = value;
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00007B96 File Offset: 0x00005D96
		protected TextAsset()
		{
		}

		// Token: 0x040000B3 RID: 179
		[SerializeField]
		internal string m_Version;

		// Token: 0x040000B4 RID: 180
		internal int m_InstanceID;

		// Token: 0x040000B5 RID: 181
		internal int m_HashCode;

		// Token: 0x040000B6 RID: 182
		[SerializeField]
		[FormerlySerializedAs("material")]
		internal Material m_Material;

		// Token: 0x040000B7 RID: 183
		internal int m_MaterialHashCode;
	}
}
