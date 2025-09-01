using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x0200054D RID: 1357
	[StructLayout(LayoutKind.Sequential)]
	internal class SecPkgContext_ConnectionInfo
	{
		// Token: 0x06002C20 RID: 11296 RVA: 0x00096258 File Offset: 0x00094458
		internal unsafe SecPkgContext_ConnectionInfo(byte[] nativeBuffer)
		{
			fixed (byte[] array = nativeBuffer)
			{
				void* value;
				if (nativeBuffer == null || array.Length == 0)
				{
					value = null;
				}
				else
				{
					value = (void*)(&array[0]);
				}
				try
				{
					IntPtr ptr = new IntPtr(value);
					this.Protocol = Marshal.ReadInt32(ptr);
					this.DataCipherAlg = Marshal.ReadInt32(ptr, 4);
					this.DataKeySize = Marshal.ReadInt32(ptr, 8);
					this.DataHashAlg = Marshal.ReadInt32(ptr, 12);
					this.DataHashKeySize = Marshal.ReadInt32(ptr, 16);
					this.KeyExchangeAlg = Marshal.ReadInt32(ptr, 20);
					this.KeyExchKeySize = Marshal.ReadInt32(ptr, 24);
				}
				catch (OverflowException)
				{
					NetEventSource.Fail(this, "Negative size", ".ctor");
					throw;
				}
			}
		}

		// Token: 0x040017BA RID: 6074
		public readonly int Protocol;

		// Token: 0x040017BB RID: 6075
		public readonly int DataCipherAlg;

		// Token: 0x040017BC RID: 6076
		public readonly int DataKeySize;

		// Token: 0x040017BD RID: 6077
		public readonly int DataHashAlg;

		// Token: 0x040017BE RID: 6078
		public readonly int DataHashKeySize;

		// Token: 0x040017BF RID: 6079
		public readonly int KeyExchangeAlg;

		// Token: 0x040017C0 RID: 6080
		public readonly int KeyExchKeySize;
	}
}
