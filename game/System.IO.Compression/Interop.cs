using System;
using System.IO.Compression;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

// Token: 0x02000002 RID: 2
internal static class Interop
{
	// Token: 0x02000003 RID: 3
	internal static class Brotli
	{
		// Token: 0x06000001 RID: 1
		[DllImport("__Internal")]
		internal static extern SafeBrotliDecoderHandle BrotliDecoderCreateInstance(IntPtr allocFunc, IntPtr freeFunc, IntPtr opaque);

		// Token: 0x06000002 RID: 2
		[DllImport("__Internal")]
		internal unsafe static extern int BrotliDecoderDecompressStream(SafeBrotliDecoderHandle state, ref IntPtr availableIn, byte** nextIn, ref IntPtr availableOut, byte** nextOut, out IntPtr totalOut);

		// Token: 0x06000003 RID: 3
		[DllImport("__Internal")]
		internal unsafe static extern bool BrotliDecoderDecompress(IntPtr availableInput, byte* inBytes, ref IntPtr availableOutput, byte* outBytes);

		// Token: 0x06000004 RID: 4
		[DllImport("__Internal")]
		internal static extern void BrotliDecoderDestroyInstance(IntPtr state);

		// Token: 0x06000005 RID: 5
		[DllImport("__Internal")]
		internal static extern bool BrotliDecoderIsFinished(SafeBrotliDecoderHandle state);

		// Token: 0x06000006 RID: 6
		[DllImport("__Internal")]
		internal static extern SafeBrotliEncoderHandle BrotliEncoderCreateInstance(IntPtr allocFunc, IntPtr freeFunc, IntPtr opaque);

		// Token: 0x06000007 RID: 7
		[DllImport("__Internal")]
		internal static extern bool BrotliEncoderSetParameter(SafeBrotliEncoderHandle state, BrotliEncoderParameter parameter, uint value);

		// Token: 0x06000008 RID: 8
		[DllImport("__Internal")]
		internal unsafe static extern bool BrotliEncoderCompressStream(SafeBrotliEncoderHandle state, BrotliEncoderOperation op, ref IntPtr availableIn, byte** nextIn, ref IntPtr availableOut, byte** nextOut, out IntPtr totalOut);

		// Token: 0x06000009 RID: 9
		[DllImport("__Internal")]
		internal static extern bool BrotliEncoderHasMoreOutput(SafeBrotliEncoderHandle state);

		// Token: 0x0600000A RID: 10
		[DllImport("__Internal")]
		internal static extern void BrotliEncoderDestroyInstance(IntPtr state);

		// Token: 0x0600000B RID: 11
		[DllImport("__Internal")]
		internal unsafe static extern bool BrotliEncoderCompress(int quality, int window, int v, IntPtr availableInput, byte* inBytes, ref IntPtr availableOutput, byte* outBytes);
	}

	// Token: 0x02000004 RID: 4
	internal static class Libraries
	{
		// Token: 0x04000001 RID: 1
		internal const string CompressionNative = "__Internal";
	}
}
