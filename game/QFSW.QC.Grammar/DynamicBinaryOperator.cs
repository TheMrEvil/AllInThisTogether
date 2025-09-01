using System;
using System.Runtime.CompilerServices;

namespace QFSW.QC.Grammar
{
	// Token: 0x02000009 RID: 9
	internal class DynamicBinaryOperator : IBinaryOperator
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000025B2 File Offset: 0x000007B2
		public Type LArg
		{
			[CompilerGenerated]
			get
			{
				return this.<LArg>k__BackingField;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000025BA File Offset: 0x000007BA
		public Type RArg
		{
			[CompilerGenerated]
			get
			{
				return this.<RArg>k__BackingField;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000025C2 File Offset: 0x000007C2
		public Type Ret
		{
			[CompilerGenerated]
			get
			{
				return this.<Ret>k__BackingField;
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000025CA File Offset: 0x000007CA
		public DynamicBinaryOperator(Delegate del, Type lArg, Type rArg, Type ret)
		{
			this._del = del;
			this.LArg = lArg;
			this.RArg = rArg;
			this.Ret = ret;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000025EF File Offset: 0x000007EF
		public object Invoke(object left, object right)
		{
			return this._del.DynamicInvoke(new object[]
			{
				left,
				right
			});
		}

		// Token: 0x0400000A RID: 10
		[CompilerGenerated]
		private readonly Type <LArg>k__BackingField;

		// Token: 0x0400000B RID: 11
		[CompilerGenerated]
		private readonly Type <RArg>k__BackingField;

		// Token: 0x0400000C RID: 12
		[CompilerGenerated]
		private readonly Type <Ret>k__BackingField;

		// Token: 0x0400000D RID: 13
		private readonly Delegate _del;
	}
}
