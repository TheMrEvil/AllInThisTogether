using System;
using System.Reflection;

namespace System.Xml.Serialization
{
	// Token: 0x02000282 RID: 642
	internal class ChoiceIdentifierAccessor : Accessor
	{
		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x0600184F RID: 6223 RVA: 0x0008E7BE File Offset: 0x0008C9BE
		// (set) Token: 0x06001850 RID: 6224 RVA: 0x0008E7C6 File Offset: 0x0008C9C6
		internal string MemberName
		{
			get
			{
				return this.memberName;
			}
			set
			{
				this.memberName = value;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001851 RID: 6225 RVA: 0x0008E7CF File Offset: 0x0008C9CF
		// (set) Token: 0x06001852 RID: 6226 RVA: 0x0008E7D7 File Offset: 0x0008C9D7
		internal string[] MemberIds
		{
			get
			{
				return this.memberIds;
			}
			set
			{
				this.memberIds = value;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001853 RID: 6227 RVA: 0x0008E7E0 File Offset: 0x0008C9E0
		// (set) Token: 0x06001854 RID: 6228 RVA: 0x0008E7E8 File Offset: 0x0008C9E8
		internal MemberInfo MemberInfo
		{
			get
			{
				return this.memberInfo;
			}
			set
			{
				this.memberInfo = value;
			}
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x0008E7B6 File Offset: 0x0008C9B6
		public ChoiceIdentifierAccessor()
		{
		}

		// Token: 0x040018BC RID: 6332
		private string memberName;

		// Token: 0x040018BD RID: 6333
		private string[] memberIds;

		// Token: 0x040018BE RID: 6334
		private MemberInfo memberInfo;
	}
}
