using System;
using System.Runtime.Serialization;

namespace IKVM.Reflection
{
	// Token: 0x02000042 RID: 66
	[Serializable]
	public sealed class MissingMemberException : InvalidOperationException
	{
		// Token: 0x0600029E RID: 670 RVA: 0x00009B83 File Offset: 0x00007D83
		internal MissingMemberException(MemberInfo member) : base("Member '" + member + "' is a missing member and does not support the requested operation.")
		{
			this.member = member;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00009B40 File Offset: 0x00007D40
		private MissingMemberException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x00009BA2 File Offset: 0x00007DA2
		public MemberInfo MemberInfo
		{
			get
			{
				return this.member;
			}
		}

		// Token: 0x0400016E RID: 366
		[NonSerialized]
		private readonly MemberInfo member;
	}
}
