using System;
using System.Collections.Specialized;

namespace System.Configuration
{
	// Token: 0x02000011 RID: 17
	internal class ConfigNameValueCollection : NameValueCollection
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002605 File Offset: 0x00000805
		public ConfigNameValueCollection()
		{
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000260D File Offset: 0x0000080D
		public ConfigNameValueCollection(ConfigNameValueCollection col) : base(col.Count, col)
		{
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000261C File Offset: 0x0000081C
		public void ResetModified()
		{
			this.modified = false;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002625 File Offset: 0x00000825
		public bool IsModified
		{
			get
			{
				return this.modified;
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000262D File Offset: 0x0000082D
		public override void Set(string name, string value)
		{
			base.Set(name, value);
			this.modified = true;
		}

		// Token: 0x04000038 RID: 56
		private bool modified;
	}
}
