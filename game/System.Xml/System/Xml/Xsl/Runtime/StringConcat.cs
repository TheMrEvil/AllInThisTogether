using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x0200044F RID: 1103
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct StringConcat
	{
		// Token: 0x06002B35 RID: 11061 RVA: 0x001036E1 File Offset: 0x001018E1
		public void Clear()
		{
			this.idxStr = 0;
			this.delimiter = null;
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06002B36 RID: 11062 RVA: 0x001036F1 File Offset: 0x001018F1
		// (set) Token: 0x06002B37 RID: 11063 RVA: 0x001036F9 File Offset: 0x001018F9
		public string Delimiter
		{
			get
			{
				return this.delimiter;
			}
			set
			{
				this.delimiter = value;
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06002B38 RID: 11064 RVA: 0x00103702 File Offset: 0x00101902
		internal int Count
		{
			get
			{
				return this.idxStr;
			}
		}

		// Token: 0x06002B39 RID: 11065 RVA: 0x0010370A File Offset: 0x0010190A
		public void Concat(string value)
		{
			if (this.delimiter != null && this.idxStr != 0)
			{
				this.ConcatNoDelimiter(this.delimiter);
			}
			this.ConcatNoDelimiter(value);
		}

		// Token: 0x06002B3A RID: 11066 RVA: 0x00103730 File Offset: 0x00101930
		public string GetResult()
		{
			switch (this.idxStr)
			{
			case 0:
				return string.Empty;
			case 1:
				return this.s1;
			case 2:
				return this.s1 + this.s2;
			case 3:
				return this.s1 + this.s2 + this.s3;
			case 4:
				return this.s1 + this.s2 + this.s3 + this.s4;
			default:
				return string.Concat(this.strList.ToArray());
			}
		}

		// Token: 0x06002B3B RID: 11067 RVA: 0x001037C8 File Offset: 0x001019C8
		internal void ConcatNoDelimiter(string s)
		{
			switch (this.idxStr)
			{
			case 0:
				this.s1 = s;
				goto IL_A8;
			case 1:
				this.s2 = s;
				goto IL_A8;
			case 2:
				this.s3 = s;
				goto IL_A8;
			case 3:
				this.s4 = s;
				goto IL_A8;
			case 4:
			{
				int capacity = (this.strList == null) ? 8 : this.strList.Count;
				List<string> list = this.strList = new List<string>(capacity);
				list.Add(this.s1);
				list.Add(this.s2);
				list.Add(this.s3);
				list.Add(this.s4);
				break;
			}
			}
			this.strList.Add(s);
			IL_A8:
			this.idxStr++;
		}

		// Token: 0x0400223E RID: 8766
		private string s1;

		// Token: 0x0400223F RID: 8767
		private string s2;

		// Token: 0x04002240 RID: 8768
		private string s3;

		// Token: 0x04002241 RID: 8769
		private string s4;

		// Token: 0x04002242 RID: 8770
		private string delimiter;

		// Token: 0x04002243 RID: 8771
		private List<string> strList;

		// Token: 0x04002244 RID: 8772
		private int idxStr;
	}
}
