using System;
using System.IO;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200020E RID: 526
	internal static class ManagedStreamHelpers
	{
		// Token: 0x0600172A RID: 5930 RVA: 0x00025454 File Offset: 0x00023654
		internal static void ValidateLoadFromStream(Stream stream)
		{
			bool flag = stream == null;
			if (flag)
			{
				throw new ArgumentNullException("ManagedStream object must be non-null", "stream");
			}
			bool flag2 = !stream.CanRead;
			if (flag2)
			{
				throw new ArgumentException("ManagedStream object must be readable (stream.CanRead must return true)", "stream");
			}
			bool flag3 = !stream.CanSeek;
			if (flag3)
			{
				throw new ArgumentException("ManagedStream object must be seekable (stream.CanSeek must return true)", "stream");
			}
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x000254B4 File Offset: 0x000236B4
		[RequiredByNativeCode]
		internal unsafe static void ManagedStreamRead(byte[] buffer, int offset, int count, Stream stream, IntPtr returnValueAddress)
		{
			bool flag = returnValueAddress == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("Return value address cannot be 0.", "returnValueAddress");
			}
			ManagedStreamHelpers.ValidateLoadFromStream(stream);
			*(int*)((void*)returnValueAddress) = stream.Read(buffer, offset, count);
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x000254FC File Offset: 0x000236FC
		[RequiredByNativeCode]
		internal unsafe static void ManagedStreamSeek(long offset, uint origin, Stream stream, IntPtr returnValueAddress)
		{
			bool flag = returnValueAddress == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("Return value address cannot be 0.", "returnValueAddress");
			}
			ManagedStreamHelpers.ValidateLoadFromStream(stream);
			*(long*)((void*)returnValueAddress) = stream.Seek(offset, (SeekOrigin)origin);
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x00025540 File Offset: 0x00023740
		[RequiredByNativeCode]
		internal unsafe static void ManagedStreamLength(Stream stream, IntPtr returnValueAddress)
		{
			bool flag = returnValueAddress == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("Return value address cannot be 0.", "returnValueAddress");
			}
			ManagedStreamHelpers.ValidateLoadFromStream(stream);
			*(long*)((void*)returnValueAddress) = stream.Length;
		}
	}
}
