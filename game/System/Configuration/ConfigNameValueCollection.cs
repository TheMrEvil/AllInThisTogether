using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Configuration
{
	// Token: 0x0200019E RID: 414
	internal class ConfigNameValueCollection : NameValueCollection
	{
		// Token: 0x06000ADD RID: 2781 RVA: 0x0002EDF0 File Offset: 0x0002CFF0
		public ConfigNameValueCollection()
		{
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0002EDF8 File Offset: 0x0002CFF8
		public ConfigNameValueCollection(ConfigNameValueCollection col) : base(col.Count, col)
		{
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0002EE07 File Offset: 0x0002D007
		public ConfigNameValueCollection(IHashCodeProvider hashProvider, IComparer comparer) : base(hashProvider, comparer)
		{
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0002EE11 File Offset: 0x0002D011
		public void ResetModified()
		{
			this.modified = false;
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x0002EE1A File Offset: 0x0002D01A
		public bool IsModified
		{
			get
			{
				return this.modified;
			}
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0002EE22 File Offset: 0x0002D022
		public override void Set(string name, string value)
		{
			base.Set(name, value);
			this.modified = true;
		}

		// Token: 0x04000732 RID: 1842
		private bool modified;
	}
}
