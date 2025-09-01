using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000268 RID: 616
	internal sealed class _SqlRPC
	{
		// Token: 0x06001CDD RID: 7389 RVA: 0x00089729 File Offset: 0x00087929
		internal string GetCommandTextOrRpcName()
		{
			if (10 == this.ProcID)
			{
				return (string)this.parameters[0].Value;
			}
			return this.rpcName;
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x00003D93 File Offset: 0x00001F93
		public _SqlRPC()
		{
		}

		// Token: 0x0400140B RID: 5131
		internal string rpcName;

		// Token: 0x0400140C RID: 5132
		internal ushort ProcID;

		// Token: 0x0400140D RID: 5133
		internal ushort options;

		// Token: 0x0400140E RID: 5134
		internal SqlParameter[] parameters;

		// Token: 0x0400140F RID: 5135
		internal byte[] paramoptions;

		// Token: 0x04001410 RID: 5136
		internal int? recordsAffected;

		// Token: 0x04001411 RID: 5137
		internal int cumulativeRecordsAffected;

		// Token: 0x04001412 RID: 5138
		internal int errorsIndexStart;

		// Token: 0x04001413 RID: 5139
		internal int errorsIndexEnd;

		// Token: 0x04001414 RID: 5140
		internal SqlErrorCollection errors;

		// Token: 0x04001415 RID: 5141
		internal int warningsIndexStart;

		// Token: 0x04001416 RID: 5142
		internal int warningsIndexEnd;

		// Token: 0x04001417 RID: 5143
		internal SqlErrorCollection warnings;
	}
}
