using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks
{
	// Token: 0x020000C1 RID: 193
	internal class Utf8StringToNative : ICustomMarshaler
	{
		// Token: 0x06000A01 RID: 2561 RVA: 0x0001274C File Offset: 0x0001094C
		public unsafe IntPtr MarshalManagedToNative(object managedObj)
		{
			bool flag = managedObj == null;
			IntPtr result;
			if (flag)
			{
				result = IntPtr.Zero;
			}
			else
			{
				string text = managedObj as string;
				bool flag2 = text != null;
				if (flag2)
				{
					fixed (string text2 = text)
					{
						char* ptr = text2;
						if (ptr != null)
						{
							ptr += RuntimeHelpers.OffsetToStringData / 2;
						}
						int byteCount = Encoding.UTF8.GetByteCount(text);
						IntPtr intPtr = Marshal.AllocHGlobal(byteCount + 1);
						int bytes = Encoding.UTF8.GetBytes(ptr, text.Length, (byte*)((void*)intPtr), byteCount + 1);
						((byte*)((void*)intPtr))[bytes] = 0;
						result = intPtr;
					}
				}
				else
				{
					result = IntPtr.Zero;
				}
			}
			return result;
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x000127E2 File Offset: 0x000109E2
		public object MarshalNativeToManaged(IntPtr pNativeData)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x000127E9 File Offset: 0x000109E9
		public void CleanUpNativeData(IntPtr pNativeData)
		{
			Marshal.FreeHGlobal(pNativeData);
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x000127F2 File Offset: 0x000109F2
		public void CleanUpManagedData(object managedObj)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x000127F9 File Offset: 0x000109F9
		public int GetNativeDataSize()
		{
			return -1;
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x000127FC File Offset: 0x000109FC
		[Preserve]
		public static ICustomMarshaler GetInstance(string cookie)
		{
			return new Utf8StringToNative();
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00012803 File Offset: 0x00010A03
		public Utf8StringToNative()
		{
		}
	}
}
