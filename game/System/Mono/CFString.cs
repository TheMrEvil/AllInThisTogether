using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono
{
	// Token: 0x0200002A RID: 42
	internal class CFString : CFObject
	{
		// Token: 0x06000056 RID: 86 RVA: 0x00002474 File Offset: 0x00000674
		public CFString(IntPtr handle, bool own) : base(handle, own)
		{
		}

		// Token: 0x06000057 RID: 87
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFStringCreateWithCharacters(IntPtr alloc, IntPtr chars, IntPtr length);

		// Token: 0x06000058 RID: 88 RVA: 0x00002700 File Offset: 0x00000900
		public unsafe static CFString Create(string value)
		{
			IntPtr intPtr;
			fixed (string text = value)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				intPtr = CFString.CFStringCreateWithCharacters(IntPtr.Zero, (IntPtr)((void*)ptr), (IntPtr)value.Length);
			}
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return new CFString(intPtr, true);
		}

		// Token: 0x06000059 RID: 89
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFStringGetLength(IntPtr handle);

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002751 File Offset: 0x00000951
		public int Length
		{
			get
			{
				if (this.str != null)
				{
					return this.str.Length;
				}
				return (int)CFString.CFStringGetLength(base.Handle);
			}
		}

		// Token: 0x0600005B RID: 91
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern int CFStringCompare(IntPtr theString1, IntPtr theString2, int compareOptions);

		// Token: 0x0600005C RID: 92 RVA: 0x00002777 File Offset: 0x00000977
		public static int Compare(IntPtr string1, IntPtr string2, int compareOptions = 0)
		{
			return CFString.CFStringCompare(string1, string2, compareOptions);
		}

		// Token: 0x0600005D RID: 93
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFStringGetCharactersPtr(IntPtr handle);

		// Token: 0x0600005E RID: 94
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFStringGetCharacters(IntPtr handle, CFRange range, IntPtr buffer);

		// Token: 0x0600005F RID: 95 RVA: 0x00002784 File Offset: 0x00000984
		public unsafe static string AsString(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				return null;
			}
			int num = (int)CFString.CFStringGetLength(handle);
			if (num == 0)
			{
				return string.Empty;
			}
			IntPtr intPtr = CFString.CFStringGetCharactersPtr(handle);
			IntPtr intPtr2 = IntPtr.Zero;
			if (intPtr == IntPtr.Zero)
			{
				CFRange range = new CFRange(0, num);
				intPtr2 = Marshal.AllocHGlobal(num * 2);
				CFString.CFStringGetCharacters(handle, range, intPtr2);
				intPtr = intPtr2;
			}
			string result = new string((char*)((void*)intPtr), 0, num);
			if (intPtr2 != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(intPtr2);
			}
			return result;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000280C File Offset: 0x00000A0C
		public override string ToString()
		{
			if (this.str == null)
			{
				this.str = CFString.AsString(base.Handle);
			}
			return this.str;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000282D File Offset: 0x00000A2D
		public static implicit operator string(CFString str)
		{
			return str.ToString();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002835 File Offset: 0x00000A35
		public static implicit operator CFString(string str)
		{
			return CFString.Create(str);
		}

		// Token: 0x04000116 RID: 278
		private string str;
	}
}
