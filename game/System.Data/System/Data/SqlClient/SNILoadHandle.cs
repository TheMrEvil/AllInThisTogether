using System;
using System.Runtime.InteropServices;

namespace System.Data.SqlClient
{
	// Token: 0x0200026B RID: 619
	internal sealed class SNILoadHandle : SafeHandle
	{
		// Token: 0x06001CEC RID: 7404 RVA: 0x00089880 File Offset: 0x00087A80
		private SNILoadHandle() : base(IntPtr.Zero, true)
		{
			try
			{
			}
			finally
			{
				this._sniStatus = SNINativeMethodWrapper.SNIInitialize();
				uint num = 0U;
				if (this._sniStatus == 0U)
				{
					SNINativeMethodWrapper.SNIQueryInfo(SNINativeMethodWrapper.QTypes.SNI_QUERY_CLIENT_ENCRYPT_POSSIBLE, ref num);
				}
				this._encryptionOption = ((num == 0U) ? EncryptionOptions.NOT_SUP : EncryptionOptions.OFF);
				this.handle = (IntPtr)1;
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001CED RID: 7405 RVA: 0x00089910 File Offset: 0x00087B10
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero == this.handle;
			}
		}

		// Token: 0x06001CEE RID: 7406 RVA: 0x00089922 File Offset: 0x00087B22
		protected override bool ReleaseHandle()
		{
			if (this.handle != IntPtr.Zero)
			{
				if (this._sniStatus == 0U)
				{
					LocalDBAPI.ReleaseDLLHandles();
					SNINativeMethodWrapper.SNITerminate();
				}
				this.handle = IntPtr.Zero;
			}
			return true;
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001CEF RID: 7407 RVA: 0x00089955 File Offset: 0x00087B55
		public uint Status
		{
			get
			{
				return this._sniStatus;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001CF0 RID: 7408 RVA: 0x0008995D File Offset: 0x00087B5D
		public EncryptionOptions Options
		{
			get
			{
				return this._encryptionOption;
			}
		}

		// Token: 0x06001CF1 RID: 7409 RVA: 0x00089968 File Offset: 0x00087B68
		private static void ReadDispatcher(IntPtr key, IntPtr packet, uint error)
		{
			if (IntPtr.Zero != key)
			{
				TdsParserStateObject tdsParserStateObject = (TdsParserStateObject)((GCHandle)key).Target;
				if (tdsParserStateObject != null)
				{
					tdsParserStateObject.ReadAsyncCallback<IntPtr>(IntPtr.Zero, packet, error);
				}
			}
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x000899A8 File Offset: 0x00087BA8
		private static void WriteDispatcher(IntPtr key, IntPtr packet, uint error)
		{
			if (IntPtr.Zero != key)
			{
				TdsParserStateObject tdsParserStateObject = (TdsParserStateObject)((GCHandle)key).Target;
				if (tdsParserStateObject != null)
				{
					tdsParserStateObject.WriteAsyncCallback<IntPtr>(IntPtr.Zero, packet, error);
				}
			}
		}

		// Token: 0x06001CF3 RID: 7411 RVA: 0x000899E6 File Offset: 0x00087BE6
		// Note: this type is marked as 'beforefieldinit'.
		static SNILoadHandle()
		{
		}

		// Token: 0x04001420 RID: 5152
		internal static readonly SNILoadHandle SingletonInstance = new SNILoadHandle();

		// Token: 0x04001421 RID: 5153
		internal readonly SNINativeMethodWrapper.SqlAsyncCallbackDelegate ReadAsyncCallbackDispatcher = new SNINativeMethodWrapper.SqlAsyncCallbackDelegate(SNILoadHandle.ReadDispatcher);

		// Token: 0x04001422 RID: 5154
		internal readonly SNINativeMethodWrapper.SqlAsyncCallbackDelegate WriteAsyncCallbackDispatcher = new SNINativeMethodWrapper.SqlAsyncCallbackDelegate(SNILoadHandle.WriteDispatcher);

		// Token: 0x04001423 RID: 5155
		private readonly uint _sniStatus = uint.MaxValue;

		// Token: 0x04001424 RID: 5156
		private readonly EncryptionOptions _encryptionOption;
	}
}
