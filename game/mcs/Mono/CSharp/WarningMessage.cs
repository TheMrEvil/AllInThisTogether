using System;
using System.Collections.Generic;

namespace Mono.CSharp
{
	// Token: 0x02000286 RID: 646
	internal sealed class WarningMessage : AbstractMessage
	{
		// Token: 0x06001F97 RID: 8087 RVA: 0x0009B41F File Offset: 0x0009961F
		public WarningMessage(int code, Location loc, string message, List<string> extra_info) : base(code, loc, message, extra_info)
		{
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06001F98 RID: 8088 RVA: 0x0000212D File Offset: 0x0000032D
		public override bool IsWarning
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06001F99 RID: 8089 RVA: 0x0009B42C File Offset: 0x0009962C
		public override string MessageType
		{
			get
			{
				return "warning";
			}
		}
	}
}
