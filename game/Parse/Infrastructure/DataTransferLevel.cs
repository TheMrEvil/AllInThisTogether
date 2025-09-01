using System;
using System.Runtime.CompilerServices;
using Parse.Abstractions.Infrastructure;

namespace Parse.Infrastructure
{
	// Token: 0x02000040 RID: 64
	public class DataTransferLevel : EventArgs, IDataTransferLevel
	{
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000B36B File Offset: 0x0000956B
		// (set) Token: 0x060002FD RID: 765 RVA: 0x0000B373 File Offset: 0x00009573
		public double Amount
		{
			[CompilerGenerated]
			get
			{
				return this.<Amount>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Amount>k__BackingField = value;
			}
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000B37C File Offset: 0x0000957C
		public DataTransferLevel()
		{
		}

		// Token: 0x04000091 RID: 145
		[CompilerGenerated]
		private double <Amount>k__BackingField;
	}
}
