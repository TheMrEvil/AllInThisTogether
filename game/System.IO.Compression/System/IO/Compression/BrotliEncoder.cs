using System;
using System.Buffers;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Compression
{
	// Token: 0x0200000E RID: 14
	public struct BrotliEncoder : IDisposable
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00003028 File Offset: 0x00001228
		public BrotliEncoder(int quality, int window)
		{
			this._disposed = false;
			this._state = Interop.Brotli.BrotliEncoderCreateInstance(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
			if (this._state.IsInvalid)
			{
				throw new IOException("Failed to create BrotliEncoder instance");
			}
			this.SetQuality(quality);
			this.SetWindow(window);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000307C File Offset: 0x0000127C
		internal void InitializeEncoder()
		{
			this.EnsureNotDisposed();
			this._state = Interop.Brotli.BrotliEncoderCreateInstance(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
			if (this._state.IsInvalid)
			{
				throw new IOException("Failed to create BrotliEncoder instance");
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000030B6 File Offset: 0x000012B6
		internal void EnsureInitialized()
		{
			this.EnsureNotDisposed();
			if (this._state == null)
			{
				this.InitializeEncoder();
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000030CC File Offset: 0x000012CC
		public void Dispose()
		{
			this._disposed = true;
			SafeBrotliEncoderHandle state = this._state;
			if (state == null)
			{
				return;
			}
			state.Dispose();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000030E5 File Offset: 0x000012E5
		private void EnsureNotDisposed()
		{
			if (this._disposed)
			{
				throw new ObjectDisposedException("BrotliEncoder", "Can not access a closed Encoder.");
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003100 File Offset: 0x00001300
		internal void SetQuality(int quality)
		{
			this.EnsureNotDisposed();
			if (this._state == null || this._state.IsInvalid || this._state.IsClosed)
			{
				this.InitializeEncoder();
			}
			if (quality < 0 || quality > 11)
			{
				throw new ArgumentOutOfRangeException("quality", SR.Format("Provided BrotliEncoder Quality of {0} is not between the minimum value of {1} and the maximum value of {2}", quality, 0, 11));
			}
			if (!Interop.Brotli.BrotliEncoderSetParameter(this._state, BrotliEncoderParameter.Quality, (uint)quality))
			{
				throw new InvalidOperationException(SR.Format("The BrotliEncoder {0} can not be changed at current encoder state.", "Quality"));
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003190 File Offset: 0x00001390
		internal void SetWindow(int window)
		{
			this.EnsureNotDisposed();
			if (this._state == null || this._state.IsInvalid || this._state.IsClosed)
			{
				this.InitializeEncoder();
			}
			if (window < 10 || window > 24)
			{
				throw new ArgumentOutOfRangeException("window", SR.Format("Provided BrotliEncoder Window of {0} is not between the minimum value of {1} and the maximum value of {2}", window, 10, 24));
			}
			if (!Interop.Brotli.BrotliEncoderSetParameter(this._state, BrotliEncoderParameter.LGWin, (uint)window))
			{
				throw new InvalidOperationException(SR.Format("The BrotliEncoder {0} can not be changed at current encoder state.", "Window"));
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003224 File Offset: 0x00001424
		public static int GetMaxCompressedLength(int length)
		{
			if (length < 0 || length > 2147483132)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			if (length == 0)
			{
				return 1;
			}
			int num = length >> 24;
			int num2 = ((length & 16777215) > 1048576) ? 4 : 3;
			int num3 = 2 + 4 * num + num2 + 1;
			return length + num3;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003272 File Offset: 0x00001472
		internal OperationStatus Flush(Memory<byte> destination, out int bytesWritten)
		{
			return this.Flush(destination.Span, out bytesWritten);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003284 File Offset: 0x00001484
		public OperationStatus Flush(Span<byte> destination, out int bytesWritten)
		{
			int num;
			return this.Compress(ReadOnlySpan<byte>.Empty, destination, out num, out bytesWritten, BrotliEncoderOperation.Flush);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000032A1 File Offset: 0x000014A1
		internal OperationStatus Compress(ReadOnlyMemory<byte> source, Memory<byte> destination, out int bytesConsumed, out int bytesWritten, bool isFinalBlock)
		{
			return this.Compress(source.Span, destination.Span, out bytesConsumed, out bytesWritten, isFinalBlock);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000032BC File Offset: 0x000014BC
		public OperationStatus Compress(ReadOnlySpan<byte> source, Span<byte> destination, out int bytesConsumed, out int bytesWritten, bool isFinalBlock)
		{
			return this.Compress(source, destination, out bytesConsumed, out bytesWritten, isFinalBlock ? BrotliEncoderOperation.Finish : BrotliEncoderOperation.Process);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000032D4 File Offset: 0x000014D4
		internal unsafe OperationStatus Compress(ReadOnlySpan<byte> source, Span<byte> destination, out int bytesConsumed, out int bytesWritten, BrotliEncoderOperation operation)
		{
			this.EnsureInitialized();
			bytesWritten = 0;
			bytesConsumed = 0;
			IntPtr value = (IntPtr)destination.Length;
			IntPtr value2 = (IntPtr)source.Length;
			while ((int)value > 0)
			{
				fixed (byte* reference = MemoryMarshal.GetReference<byte>(source))
				{
					byte* ptr = reference;
					fixed (byte* reference2 = MemoryMarshal.GetReference<byte>(destination))
					{
						byte* ptr2 = reference2;
						byte* ptr3 = ptr;
						byte* ptr4 = ptr2;
						IntPtr intPtr;
						if (!Interop.Brotli.BrotliEncoderCompressStream(this._state, operation, ref value2, &ptr3, ref value, &ptr4, out intPtr))
						{
							return OperationStatus.InvalidData;
						}
						bytesConsumed += source.Length - (int)value2;
						bytesWritten += destination.Length - (int)value;
						if ((int)value == destination.Length && !Interop.Brotli.BrotliEncoderHasMoreOutput(this._state) && (int)value2 == 0)
						{
							return OperationStatus.Done;
						}
						source = source.Slice(source.Length - (int)value2);
						destination = destination.Slice(destination.Length - (int)value);
					}
				}
			}
			return OperationStatus.DestinationTooSmall;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000033D4 File Offset: 0x000015D4
		public static bool TryCompress(ReadOnlySpan<byte> source, Span<byte> destination, out int bytesWritten)
		{
			return BrotliEncoder.TryCompress(source, destination, out bytesWritten, 11, 22);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000033E4 File Offset: 0x000015E4
		public unsafe static bool TryCompress(ReadOnlySpan<byte> source, Span<byte> destination, out int bytesWritten, int quality, int window)
		{
			if (quality < 0 || quality > 11)
			{
				throw new ArgumentOutOfRangeException("quality", SR.Format("Provided BrotliEncoder Quality of {0} is not between the minimum value of {1} and the maximum value of {2}", quality, 0, 11));
			}
			if (window < 10 || window > 24)
			{
				throw new ArgumentOutOfRangeException("window", SR.Format("Provided BrotliEncoder Window of {0} is not between the minimum value of {1} and the maximum value of {2}", window, 10, 24));
			}
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(source))
			{
				byte* inBytes = reference;
				fixed (byte* reference2 = MemoryMarshal.GetReference<byte>(destination))
				{
					byte* outBytes = reference2;
					IntPtr value = (IntPtr)destination.Length;
					bool result = Interop.Brotli.BrotliEncoderCompress(quality, window, 0, (IntPtr)source.Length, inBytes, ref value, outBytes);
					bytesWritten = (int)value;
					return result;
				}
			}
		}

		// Token: 0x040000A2 RID: 162
		internal SafeBrotliEncoderHandle _state;

		// Token: 0x040000A3 RID: 163
		private bool _disposed;
	}
}
