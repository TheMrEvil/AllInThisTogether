using System;
using System.Runtime.CompilerServices;

namespace Mono
{
	// Token: 0x0200005E RID: 94
	internal struct SafeStringMarshal : IDisposable
	{
		// Token: 0x0600012B RID: 299
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr StringToUtf8_icall(ref string str);

		// Token: 0x0600012C RID: 300 RVA: 0x00004B43 File Offset: 0x00002D43
		public static IntPtr StringToUtf8(string str)
		{
			return SafeStringMarshal.StringToUtf8_icall(ref str);
		}

		// Token: 0x0600012D RID: 301
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void GFree(IntPtr ptr);

		// Token: 0x0600012E RID: 302 RVA: 0x00004B4C File Offset: 0x00002D4C
		public SafeStringMarshal(string str)
		{
			this.str = str;
			this.marshaled_string = IntPtr.Zero;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00004B60 File Offset: 0x00002D60
		public IntPtr Value
		{
			get
			{
				if (this.marshaled_string == IntPtr.Zero && this.str != null)
				{
					this.marshaled_string = SafeStringMarshal.StringToUtf8(this.str);
				}
				return this.marshaled_string;
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00004B93 File Offset: 0x00002D93
		public void Dispose()
		{
			if (this.marshaled_string != IntPtr.Zero)
			{
				SafeStringMarshal.GFree(this.marshaled_string);
				this.marshaled_string = IntPtr.Zero;
			}
		}

		// Token: 0x04000E10 RID: 3600
		private readonly string str;

		// Token: 0x04000E11 RID: 3601
		private IntPtr marshaled_string;
	}
}
