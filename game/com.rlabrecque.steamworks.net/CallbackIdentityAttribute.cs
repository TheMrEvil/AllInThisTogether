using System;
using System.Runtime.CompilerServices;

namespace Steamworks
{
	// Token: 0x02000184 RID: 388
	[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false)]
	internal class CallbackIdentityAttribute : Attribute
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x0000CDCF File Offset: 0x0000AFCF
		// (set) Token: 0x060008CC RID: 2252 RVA: 0x0000CDD7 File Offset: 0x0000AFD7
		public int Identity
		{
			[CompilerGenerated]
			get
			{
				return this.<Identity>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Identity>k__BackingField = value;
			}
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x0000CDE0 File Offset: 0x0000AFE0
		public CallbackIdentityAttribute(int callbackNum)
		{
			this.Identity = callbackNum;
		}

		// Token: 0x04000A3E RID: 2622
		[CompilerGenerated]
		private int <Identity>k__BackingField;
	}
}
