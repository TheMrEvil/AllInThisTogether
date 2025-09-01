using System;
using System.Collections.Generic;

namespace Mono.CSharp
{
	// Token: 0x02000285 RID: 645
	public abstract class AbstractMessage
	{
		// Token: 0x06001F8D RID: 8077 RVA: 0x0009B30C File Offset: 0x0009950C
		protected AbstractMessage(int code, Location loc, string msg, List<string> extraInfo)
		{
			this.code = code;
			if (code < 0)
			{
				this.code = 8000 - code;
			}
			this.location = loc;
			this.message = msg;
			if (extraInfo.Count != 0)
			{
				this.extra_info = extraInfo.ToArray();
			}
		}

		// Token: 0x06001F8E RID: 8078 RVA: 0x0009B35B File Offset: 0x0009955B
		protected AbstractMessage(AbstractMessage aMsg)
		{
			this.code = aMsg.code;
			this.location = aMsg.location;
			this.message = aMsg.message;
			this.extra_info = aMsg.extra_info;
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06001F8F RID: 8079 RVA: 0x0009B393 File Offset: 0x00099593
		public int Code
		{
			get
			{
				return this.code;
			}
		}

		// Token: 0x06001F90 RID: 8080 RVA: 0x0009B39C File Offset: 0x0009959C
		public override bool Equals(object obj)
		{
			AbstractMessage abstractMessage = obj as AbstractMessage;
			return abstractMessage != null && (this.code == abstractMessage.code && this.location.Equals(abstractMessage.location)) && this.message == abstractMessage.message;
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x0009B3EC File Offset: 0x000995EC
		public override int GetHashCode()
		{
			return this.code.GetHashCode();
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06001F92 RID: 8082
		public abstract bool IsWarning { get; }

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06001F93 RID: 8083 RVA: 0x0009B407 File Offset: 0x00099607
		public Location Location
		{
			get
			{
				return this.location;
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06001F94 RID: 8084
		public abstract string MessageType { get; }

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06001F95 RID: 8085 RVA: 0x0009B40F File Offset: 0x0009960F
		public string[] RelatedSymbols
		{
			get
			{
				return this.extra_info;
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06001F96 RID: 8086 RVA: 0x0009B417 File Offset: 0x00099617
		public string Text
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x04000B8F RID: 2959
		private readonly string[] extra_info;

		// Token: 0x04000B90 RID: 2960
		protected readonly int code;

		// Token: 0x04000B91 RID: 2961
		protected readonly Location location;

		// Token: 0x04000B92 RID: 2962
		private readonly string message;
	}
}
