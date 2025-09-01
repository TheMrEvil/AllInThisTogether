using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000558 RID: 1368
	[StructLayout(LayoutKind.Sequential)]
	internal class SecPkgContext_StreamSizes
	{
		// Token: 0x06002C7E RID: 11390 RVA: 0x00097808 File Offset: 0x00095A08
		internal unsafe SecPkgContext_StreamSizes(byte[] memory)
		{
			fixed (byte[] array = memory)
			{
				void* value;
				if (memory == null || array.Length == 0)
				{
					value = null;
				}
				else
				{
					value = (void*)(&array[0]);
				}
				IntPtr ptr = new IntPtr(value);
				checked
				{
					try
					{
						this.cbHeader = (int)((uint)Marshal.ReadInt32(ptr));
						this.cbTrailer = (int)((uint)Marshal.ReadInt32(ptr, 4));
						this.cbMaximumMessage = (int)((uint)Marshal.ReadInt32(ptr, 8));
						this.cBuffers = (int)((uint)Marshal.ReadInt32(ptr, 12));
						this.cbBlockSize = (int)((uint)Marshal.ReadInt32(ptr, 16));
					}
					catch (OverflowException)
					{
						NetEventSource.Fail(this, "Negative size.", ".ctor");
						throw;
					}
				}
			}
		}

		// Token: 0x06002C7F RID: 11391 RVA: 0x000978AC File Offset: 0x00095AAC
		// Note: this type is marked as 'beforefieldinit'.
		static SecPkgContext_StreamSizes()
		{
		}

		// Token: 0x040017D8 RID: 6104
		public int cbHeader;

		// Token: 0x040017D9 RID: 6105
		public int cbTrailer;

		// Token: 0x040017DA RID: 6106
		public int cbMaximumMessage;

		// Token: 0x040017DB RID: 6107
		public int cBuffers;

		// Token: 0x040017DC RID: 6108
		public int cbBlockSize;

		// Token: 0x040017DD RID: 6109
		public static readonly int SizeOf = Marshal.SizeOf<SecPkgContext_StreamSizes>();
	}
}
