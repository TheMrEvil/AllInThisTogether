using System;
using System.Data.ProviderBase;

namespace System.Data.Odbc
{
	// Token: 0x020002F5 RID: 757
	internal sealed class OdbcReferenceCollection : DbReferenceCollection
	{
		// Token: 0x060021CA RID: 8650 RVA: 0x0007A25A File Offset: 0x0007845A
		public override void Add(object value, int tag)
		{
			base.AddItem(value, tag);
		}

		// Token: 0x060021CB RID: 8651 RVA: 0x0009DCAA File Offset: 0x0009BEAA
		protected override void NotifyItem(int message, int tag, object value)
		{
			if (message != 0)
			{
				if (message == 1 && 1 == tag)
				{
					((OdbcCommand)value).RecoverFromConnection();
					return;
				}
			}
			else if (1 == tag)
			{
				((OdbcCommand)value).CloseFromConnection();
			}
		}

		// Token: 0x060021CC RID: 8652 RVA: 0x0007A342 File Offset: 0x00078542
		public override void Remove(object value)
		{
			base.RemoveItem(value);
		}

		// Token: 0x060021CD RID: 8653 RVA: 0x0007A34B File Offset: 0x0007854B
		public OdbcReferenceCollection()
		{
		}

		// Token: 0x040017FC RID: 6140
		internal const int Closing = 0;

		// Token: 0x040017FD RID: 6141
		internal const int Recover = 1;

		// Token: 0x040017FE RID: 6142
		internal const int CommandTag = 1;
	}
}
