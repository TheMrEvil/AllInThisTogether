using System;

namespace Mono.Security.Interface
{
	// Token: 0x0200004B RID: 75
	public sealed class TlsException : Exception
	{
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000F32A File Offset: 0x0000D52A
		public Alert Alert
		{
			get
			{
				return this.alert;
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000F334 File Offset: 0x0000D534
		public TlsException(Alert alert) : this(alert, alert.Description.ToString())
		{
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000F35C File Offset: 0x0000D55C
		public TlsException(Alert alert, string message) : base(message)
		{
			this.alert = alert;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000F36C File Offset: 0x0000D56C
		public TlsException(AlertLevel level, AlertDescription description) : this(new Alert(level, description))
		{
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000F37B File Offset: 0x0000D57B
		public TlsException(AlertDescription description) : this(new Alert(description))
		{
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000F389 File Offset: 0x0000D589
		public TlsException(AlertDescription description, string message) : this(new Alert(description), message)
		{
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000F398 File Offset: 0x0000D598
		public TlsException(AlertDescription description, string format, params object[] args) : this(new Alert(description), string.Format(format, args))
		{
		}

		// Token: 0x0400028D RID: 653
		private Alert alert;
	}
}
