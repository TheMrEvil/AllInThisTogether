using System;
using System.Buffers;

namespace System.Text
{
	// Token: 0x020003BB RID: 955
	internal ref struct ValueUtf8Converter
	{
		// Token: 0x06002796 RID: 10134 RVA: 0x00090346 File Offset: 0x0008E546
		public ValueUtf8Converter(Span<byte> initialBuffer)
		{
			this._arrayToReturnToPool = null;
			this._bytes = initialBuffer;
		}

		// Token: 0x06002797 RID: 10135 RVA: 0x00090358 File Offset: 0x0008E558
		public unsafe Span<byte> ConvertAndTerminateString(ReadOnlySpan<char> value)
		{
			int num = Encoding.UTF8.GetMaxByteCount(value.Length) + 1;
			if (this._bytes.Length < num)
			{
				this.Dispose();
				this._arrayToReturnToPool = ArrayPool<byte>.Shared.Rent(num);
				this._bytes = new Span<byte>(this._arrayToReturnToPool);
			}
			int bytes = Encoding.UTF8.GetBytes(value, this._bytes);
			*this._bytes[bytes] = 0;
			return this._bytes.Slice(0, bytes + 1);
		}

		// Token: 0x06002798 RID: 10136 RVA: 0x000903E0 File Offset: 0x0008E5E0
		public void Dispose()
		{
			byte[] arrayToReturnToPool = this._arrayToReturnToPool;
			if (arrayToReturnToPool != null)
			{
				this._arrayToReturnToPool = null;
				ArrayPool<byte>.Shared.Return(arrayToReturnToPool, false);
			}
		}

		// Token: 0x04001E12 RID: 7698
		private byte[] _arrayToReturnToPool;

		// Token: 0x04001E13 RID: 7699
		private Span<byte> _bytes;
	}
}
