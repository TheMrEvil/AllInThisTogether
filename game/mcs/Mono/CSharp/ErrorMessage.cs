using System;
using System.Collections.Generic;

namespace Mono.CSharp
{
	// Token: 0x02000287 RID: 647
	internal sealed class ErrorMessage : AbstractMessage
	{
		// Token: 0x06001F9A RID: 8090 RVA: 0x0009B41F File Offset: 0x0009961F
		public ErrorMessage(int code, Location loc, string message, List<string> extraInfo) : base(code, loc, message, extraInfo)
		{
		}

		// Token: 0x06001F9B RID: 8091 RVA: 0x0009B433 File Offset: 0x00099633
		public ErrorMessage(AbstractMessage aMsg) : base(aMsg)
		{
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06001F9C RID: 8092 RVA: 0x000022F4 File Offset: 0x000004F4
		public override bool IsWarning
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06001F9D RID: 8093 RVA: 0x0009B43C File Offset: 0x0009963C
		public override string MessageType
		{
			get
			{
				return "error";
			}
		}
	}
}
