using System;
using System.Runtime.InteropServices;

namespace Mono
{
	// Token: 0x0200002B RID: 43
	internal class CFData : CFObject
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00002474 File Offset: 0x00000674
		public CFData(IntPtr handle, bool own) : base(handle, own)
		{
		}

		// Token: 0x06000064 RID: 100
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFDataCreate(IntPtr allocator, IntPtr bytes, IntPtr length);

		// Token: 0x06000065 RID: 101 RVA: 0x00002840 File Offset: 0x00000A40
		public unsafe static CFData FromData(byte[] buffer)
		{
			byte* value;
			if (buffer == null || buffer.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &buffer[0];
			}
			return CFData.FromData((IntPtr)((void*)value), (IntPtr)buffer.Length);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002878 File Offset: 0x00000A78
		public static CFData FromData(IntPtr buffer, IntPtr length)
		{
			return new CFData(CFData.CFDataCreate(IntPtr.Zero, buffer, length), true);
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000067 RID: 103 RVA: 0x0000288C File Offset: 0x00000A8C
		public IntPtr Length
		{
			get
			{
				return CFData.CFDataGetLength(base.Handle);
			}
		}

		// Token: 0x06000068 RID: 104
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		internal static extern IntPtr CFDataGetLength(IntPtr theData);

		// Token: 0x06000069 RID: 105
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		internal static extern IntPtr CFDataGetBytePtr(IntPtr theData);

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002899 File Offset: 0x00000A99
		public IntPtr Bytes
		{
			get
			{
				return CFData.CFDataGetBytePtr(base.Handle);
			}
		}

		// Token: 0x1700000B RID: 11
		public byte this[long idx]
		{
			get
			{
				if (idx < 0L || idx > (long)this.Length)
				{
					throw new ArgumentException("idx");
				}
				return Marshal.ReadByte(new IntPtr(this.Bytes.ToInt64() + idx));
			}
			set
			{
				throw new NotImplementedException("NSData arrays can not be modified, use an NSMutableData instead");
			}
		}
	}
}
