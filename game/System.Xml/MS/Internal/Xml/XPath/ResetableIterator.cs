using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200064C RID: 1612
	internal abstract class ResetableIterator : XPathNodeIterator
	{
		// Token: 0x0600416F RID: 16751 RVA: 0x001678FE File Offset: 0x00165AFE
		public ResetableIterator()
		{
			this.count = -1;
		}

		// Token: 0x06004170 RID: 16752 RVA: 0x0016790D File Offset: 0x00165B0D
		protected ResetableIterator(ResetableIterator other)
		{
			this.count = other.count;
		}

		// Token: 0x06004171 RID: 16753 RVA: 0x00167921 File Offset: 0x00165B21
		protected void ResetCount()
		{
			this.count = -1;
		}

		// Token: 0x06004172 RID: 16754
		public abstract void Reset();

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x06004173 RID: 16755
		public abstract override int CurrentPosition { get; }
	}
}
