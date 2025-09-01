using System;
using System.Data.SqlClient.SNI;

namespace System.Data.SqlClient
{
	// Token: 0x02000279 RID: 633
	internal sealed class TdsParserStateObjectFactory
	{
		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001DAA RID: 7594 RVA: 0x00006D61 File Offset: 0x00004F61
		public static bool UseManagedSNI
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001DAB RID: 7595 RVA: 0x0008D3E5 File Offset: 0x0008B5E5
		public EncryptionOptions EncryptionOptions
		{
			get
			{
				return SNILoadHandle.SingletonInstance.Options;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001DAC RID: 7596 RVA: 0x0008D3F1 File Offset: 0x0008B5F1
		public uint SNIStatus
		{
			get
			{
				return SNILoadHandle.SingletonInstance.Status;
			}
		}

		// Token: 0x06001DAD RID: 7597 RVA: 0x0008D3FD File Offset: 0x0008B5FD
		public TdsParserStateObject CreateTdsParserStateObject(TdsParser parser)
		{
			return new TdsParserStateObjectManaged(parser);
		}

		// Token: 0x06001DAE RID: 7598 RVA: 0x0008D405 File Offset: 0x0008B605
		internal TdsParserStateObject CreateSessionObject(TdsParser tdsParser, TdsParserStateObject _pMarsPhysicalConObj, bool v)
		{
			return new TdsParserStateObjectManaged(tdsParser, _pMarsPhysicalConObj, true);
		}

		// Token: 0x06001DAF RID: 7599 RVA: 0x00003D93 File Offset: 0x00001F93
		public TdsParserStateObjectFactory()
		{
		}

		// Token: 0x06001DB0 RID: 7600 RVA: 0x0008D40F File Offset: 0x0008B60F
		// Note: this type is marked as 'beforefieldinit'.
		static TdsParserStateObjectFactory()
		{
		}

		// Token: 0x04001497 RID: 5271
		public static readonly TdsParserStateObjectFactory Singleton = new TdsParserStateObjectFactory();
	}
}
