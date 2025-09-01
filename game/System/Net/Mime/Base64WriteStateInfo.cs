using System;
using System.Runtime.CompilerServices;

namespace System.Net.Mime
{
	// Token: 0x020007F6 RID: 2038
	internal class Base64WriteStateInfo : WriteStateInfoBase
	{
		// Token: 0x060040F4 RID: 16628 RVA: 0x000DFCA5 File Offset: 0x000DDEA5
		internal Base64WriteStateInfo()
		{
		}

		// Token: 0x060040F5 RID: 16629 RVA: 0x000DFCAD File Offset: 0x000DDEAD
		internal Base64WriteStateInfo(int bufferSize, byte[] header, byte[] footer, int maxLineLength, int mimeHeaderLength) : base(bufferSize, header, footer, maxLineLength, mimeHeaderLength)
		{
		}

		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x060040F6 RID: 16630 RVA: 0x000DFCBC File Offset: 0x000DDEBC
		// (set) Token: 0x060040F7 RID: 16631 RVA: 0x000DFCC4 File Offset: 0x000DDEC4
		internal int Padding
		{
			[CompilerGenerated]
			get
			{
				return this.<Padding>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Padding>k__BackingField = value;
			}
		}

		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x060040F8 RID: 16632 RVA: 0x000DFCCD File Offset: 0x000DDECD
		// (set) Token: 0x060040F9 RID: 16633 RVA: 0x000DFCD5 File Offset: 0x000DDED5
		internal byte LastBits
		{
			[CompilerGenerated]
			get
			{
				return this.<LastBits>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<LastBits>k__BackingField = value;
			}
		}

		// Token: 0x04002775 RID: 10101
		[CompilerGenerated]
		private int <Padding>k__BackingField;

		// Token: 0x04002776 RID: 10102
		[CompilerGenerated]
		private byte <LastBits>k__BackingField;
	}
}
