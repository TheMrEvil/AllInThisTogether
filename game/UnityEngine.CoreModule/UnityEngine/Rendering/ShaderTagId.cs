using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000410 RID: 1040
	public struct ShaderTagId : IEquatable<ShaderTagId>
	{
		// Token: 0x060023DC RID: 9180 RVA: 0x0003C842 File Offset: 0x0003AA42
		public ShaderTagId(string name)
		{
			this.m_Id = Shader.TagToID(name);
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x060023DD RID: 9181 RVA: 0x0003C854 File Offset: 0x0003AA54
		// (set) Token: 0x060023DE RID: 9182 RVA: 0x0003C86C File Offset: 0x0003AA6C
		internal int id
		{
			get
			{
				return this.m_Id;
			}
			set
			{
				this.m_Id = value;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x060023DF RID: 9183 RVA: 0x0003C878 File Offset: 0x0003AA78
		public string name
		{
			get
			{
				return Shader.IDToTag(this.id);
			}
		}

		// Token: 0x060023E0 RID: 9184 RVA: 0x0003C898 File Offset: 0x0003AA98
		public override bool Equals(object obj)
		{
			return obj is ShaderTagId && this.Equals((ShaderTagId)obj);
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x0003C8C4 File Offset: 0x0003AAC4
		public bool Equals(ShaderTagId other)
		{
			return this.m_Id == other.m_Id;
		}

		// Token: 0x060023E2 RID: 9186 RVA: 0x0003C8E4 File Offset: 0x0003AAE4
		public override int GetHashCode()
		{
			int num = 2079669542;
			return num * -1521134295 + this.m_Id.GetHashCode();
		}

		// Token: 0x060023E3 RID: 9187 RVA: 0x0003C914 File Offset: 0x0003AB14
		public static bool operator ==(ShaderTagId tag1, ShaderTagId tag2)
		{
			return tag1.Equals(tag2);
		}

		// Token: 0x060023E4 RID: 9188 RVA: 0x0003C930 File Offset: 0x0003AB30
		public static bool operator !=(ShaderTagId tag1, ShaderTagId tag2)
		{
			return !(tag1 == tag2);
		}

		// Token: 0x060023E5 RID: 9189 RVA: 0x0003C94C File Offset: 0x0003AB4C
		public static explicit operator ShaderTagId(string name)
		{
			return new ShaderTagId(name);
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x0003C964 File Offset: 0x0003AB64
		public static explicit operator string(ShaderTagId tagId)
		{
			return tagId.name;
		}

		// Token: 0x04000D39 RID: 3385
		public static readonly ShaderTagId none;

		// Token: 0x04000D3A RID: 3386
		private int m_Id;
	}
}
