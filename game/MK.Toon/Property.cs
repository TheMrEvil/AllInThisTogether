using System;
using UnityEngine;

namespace MK.Toon
{
	// Token: 0x02000025 RID: 37
	public abstract class Property<T>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002FF0 File Offset: 0x000011F0
		public Uniform uniform
		{
			get
			{
				return this._uniform;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002FF8 File Offset: 0x000011F8
		public Property(Uniform uniform, params string[] keywords)
		{
			this._keywords = keywords;
			this._uniform = uniform;
		}

		// Token: 0x06000006 RID: 6
		public abstract T GetValue(Material material);

		// Token: 0x06000007 RID: 7
		public abstract void SetValue(Material material, T value);

		// Token: 0x06000008 RID: 8 RVA: 0x0000300E File Offset: 0x0000120E
		protected void SetKeyword(Material material, bool b, int keywordIndex)
		{
			if (b && this._keywords != null && this._keywords.Length > keywordIndex && this._keywords.Length != 0)
			{
				this.CleanKeywords(material);
				material.EnableKeyword(this._keywords[keywordIndex]);
				return;
			}
			this.CleanKeywords(material);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000304C File Offset: 0x0000124C
		private void CleanKeywords(Material material)
		{
			for (int i = 0; i < this._keywords.Length; i++)
			{
				material.DisableKeyword(this._keywords[i]);
			}
		}

		// Token: 0x04000183 RID: 387
		protected string[] _keywords;

		// Token: 0x04000184 RID: 388
		protected Uniform _uniform;
	}
}
