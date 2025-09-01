using System;

namespace Mono.CSharp
{
	// Token: 0x0200028E RID: 654
	public class InternalErrorException : Exception
	{
		// Token: 0x06001FBF RID: 8127 RVA: 0x0009BD5E File Offset: 0x00099F5E
		public InternalErrorException(MemberCore mc, Exception e) : base(mc.Location + " " + mc.GetSignatureForError(), e)
		{
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x0009BD82 File Offset: 0x00099F82
		public InternalErrorException() : base("Internal error")
		{
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x00002058 File Offset: 0x00000258
		public InternalErrorException(string message) : base(message)
		{
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x0009BD8F File Offset: 0x00099F8F
		public InternalErrorException(string message, params object[] args) : base(string.Format(message, args))
		{
		}

		// Token: 0x06001FC3 RID: 8131 RVA: 0x0009BD9E File Offset: 0x00099F9E
		public InternalErrorException(Exception exception, string message, params object[] args) : base(string.Format(message, args), exception)
		{
		}

		// Token: 0x06001FC4 RID: 8132 RVA: 0x0009BDAE File Offset: 0x00099FAE
		public InternalErrorException(Exception e, Location loc) : base(loc.ToString(), e)
		{
		}
	}
}
