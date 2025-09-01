using System;
using System.IO;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Threading;

namespace System.Xml
{
	// Token: 0x02000092 RID: 146
	internal abstract class XmlStreamNodeWriter : XmlNodeWriter
	{
		// Token: 0x060007E7 RID: 2023 RVA: 0x0002037F File Offset: 0x0001E57F
		protected XmlStreamNodeWriter()
		{
			this.buffer = new byte[512];
			this.encoding = XmlStreamNodeWriter.UTF8Encoding;
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x000203A2 File Offset: 0x0001E5A2
		protected void SetOutput(Stream stream, bool ownsStream, Encoding encoding)
		{
			this.stream = stream;
			this.ownsStream = ownsStream;
			this.offset = 0;
			if (encoding != null)
			{
				this.encoding = encoding;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x000203C3 File Offset: 0x0001E5C3
		// (set) Token: 0x060007EA RID: 2026 RVA: 0x000203CB File Offset: 0x0001E5CB
		public Stream Stream
		{
			get
			{
				return this.stream;
			}
			set
			{
				this.stream = value;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x000203D4 File Offset: 0x0001E5D4
		public byte[] StreamBuffer
		{
			get
			{
				return this.buffer;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x000203DC File Offset: 0x0001E5DC
		public int BufferOffset
		{
			get
			{
				return this.offset;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x000203E4 File Offset: 0x0001E5E4
		public int Position
		{
			get
			{
				return (int)this.stream.Position + this.offset;
			}
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x000203FC File Offset: 0x0001E5FC
		protected byte[] GetBuffer(int count, out int offset)
		{
			int num = this.offset;
			if (num + count <= 512)
			{
				offset = num;
			}
			else
			{
				this.FlushBuffer();
				offset = 0;
			}
			return this.buffer;
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00020430 File Offset: 0x0001E630
		internal AsyncCompletionResult GetBufferAsync(XmlStreamNodeWriter.GetBufferAsyncEventArgs getBufferState)
		{
			int count = getBufferState.Arguments.Count;
			int num = this.offset;
			int num2;
			if (num + count <= 512)
			{
				num2 = num;
			}
			else
			{
				if (XmlStreamNodeWriter.onGetFlushComplete == null)
				{
					XmlStreamNodeWriter.onGetFlushComplete = new AsyncEventArgsCallback(XmlStreamNodeWriter.GetBufferFlushComplete);
				}
				if (this.flushBufferState == null)
				{
					this.flushBufferState = new AsyncEventArgs<object>();
				}
				this.flushBufferState.Set(XmlStreamNodeWriter.onGetFlushComplete, getBufferState, this);
				if (this.FlushBufferAsync(this.flushBufferState) != AsyncCompletionResult.Completed)
				{
					return AsyncCompletionResult.Queued;
				}
				num2 = 0;
				this.flushBufferState.Complete(true);
			}
			getBufferState.Result = (getBufferState.Result ?? new XmlStreamNodeWriter.GetBufferEventResult());
			getBufferState.Result.Buffer = this.buffer;
			getBufferState.Result.Offset = num2;
			return AsyncCompletionResult.Completed;
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x000204F4 File Offset: 0x0001E6F4
		private static void GetBufferFlushComplete(IAsyncEventArgs completionState)
		{
			XmlStreamNodeWriter xmlStreamNodeWriter = (XmlStreamNodeWriter)completionState.AsyncState;
			XmlStreamNodeWriter.GetBufferAsyncEventArgs getBufferAsyncEventArgs = (XmlStreamNodeWriter.GetBufferAsyncEventArgs)xmlStreamNodeWriter.flushBufferState.Arguments;
			getBufferAsyncEventArgs.Result = (getBufferAsyncEventArgs.Result ?? new XmlStreamNodeWriter.GetBufferEventResult());
			getBufferAsyncEventArgs.Result.Buffer = xmlStreamNodeWriter.buffer;
			getBufferAsyncEventArgs.Result.Offset = 0;
			getBufferAsyncEventArgs.Complete(false, completionState.Exception);
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x0002055C File Offset: 0x0001E75C
		private AsyncCompletionResult FlushBufferAsync(AsyncEventArgs<object> state)
		{
			if (Interlocked.CompareExchange(ref this.hasPendingWrite, 1, 0) != 0)
			{
				throw FxTrace.Exception.AsError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("Flush buffer is already in use.")));
			}
			if (this.offset != 0)
			{
				if (XmlStreamNodeWriter.onFlushBufferComplete == null)
				{
					XmlStreamNodeWriter.onFlushBufferComplete = new AsyncCallback(XmlStreamNodeWriter.OnFlushBufferCompete);
				}
				IAsyncResult asyncResult = this.stream.BeginWrite(this.buffer, 0, this.offset, XmlStreamNodeWriter.onFlushBufferComplete, this);
				if (!asyncResult.CompletedSynchronously)
				{
					return AsyncCompletionResult.Queued;
				}
				this.stream.EndWrite(asyncResult);
				this.offset = 0;
			}
			if (Interlocked.CompareExchange(ref this.hasPendingWrite, 0, 1) != 1)
			{
				throw FxTrace.Exception.AsError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("No async write operation is pending.")));
			}
			return AsyncCompletionResult.Completed;
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0002061C File Offset: 0x0001E81C
		private static void OnFlushBufferCompete(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			XmlStreamNodeWriter xmlStreamNodeWriter = (XmlStreamNodeWriter)result.AsyncState;
			Exception exception = null;
			try
			{
				xmlStreamNodeWriter.stream.EndWrite(result);
				xmlStreamNodeWriter.offset = 0;
				if (Interlocked.CompareExchange(ref xmlStreamNodeWriter.hasPendingWrite, 0, 1) != 1)
				{
					throw FxTrace.Exception.AsError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("No async write operation is pending.")));
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				exception = ex;
			}
			xmlStreamNodeWriter.flushBufferState.Complete(false, exception);
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x000206AC File Offset: 0x0001E8AC
		protected IAsyncResult BeginGetBuffer(int count, AsyncCallback callback, object state)
		{
			return new XmlStreamNodeWriter.GetBufferAsyncResult(count, this, callback, state);
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x000206B7 File Offset: 0x0001E8B7
		protected byte[] EndGetBuffer(IAsyncResult result, out int offset)
		{
			return XmlStreamNodeWriter.GetBufferAsyncResult.End(result, out offset);
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x000206C0 File Offset: 0x0001E8C0
		protected void Advance(int count)
		{
			this.offset += count;
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x000206D0 File Offset: 0x0001E8D0
		private void EnsureByte()
		{
			if (this.offset >= 512)
			{
				this.FlushBuffer();
			}
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x000206E8 File Offset: 0x0001E8E8
		protected void WriteByte(byte b)
		{
			this.EnsureByte();
			byte[] array = this.buffer;
			int num = this.offset;
			this.offset = num + 1;
			array[num] = b;
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x00020714 File Offset: 0x0001E914
		protected void WriteByte(char ch)
		{
			this.WriteByte((byte)ch);
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00020720 File Offset: 0x0001E920
		protected void WriteBytes(byte b1, byte b2)
		{
			byte[] array = this.buffer;
			int num = this.offset;
			if (num + 1 >= 512)
			{
				this.FlushBuffer();
				num = 0;
			}
			array[num] = b1;
			array[num + 1] = b2;
			this.offset += 2;
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00020763 File Offset: 0x0001E963
		protected void WriteBytes(char ch1, char ch2)
		{
			this.WriteBytes((byte)ch1, (byte)ch2);
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00020770 File Offset: 0x0001E970
		public void WriteBytes(byte[] byteBuffer, int byteOffset, int byteCount)
		{
			if (byteCount < 512)
			{
				int dstOffset;
				byte[] dst = this.GetBuffer(byteCount, out dstOffset);
				Buffer.BlockCopy(byteBuffer, byteOffset, dst, dstOffset, byteCount);
				this.Advance(byteCount);
				return;
			}
			this.FlushBuffer();
			this.stream.Write(byteBuffer, byteOffset, byteCount);
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x000207B5 File Offset: 0x0001E9B5
		public IAsyncResult BeginWriteBytes(byte[] byteBuffer, int byteOffset, int byteCount, AsyncCallback callback, object state)
		{
			return new XmlStreamNodeWriter.WriteBytesAsyncResult(byteBuffer, byteOffset, byteCount, this, callback, state);
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x000207C4 File Offset: 0x0001E9C4
		public void EndWriteBytes(IAsyncResult result)
		{
			XmlStreamNodeWriter.WriteBytesAsyncResult.End(result);
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x000207CC File Offset: 0x0001E9CC
		[SecurityCritical]
		protected unsafe void UnsafeWriteBytes(byte* bytes, int byteCount)
		{
			this.FlushBuffer();
			byte[] array = this.buffer;
			while (byteCount > 512)
			{
				for (int i = 0; i < 512; i++)
				{
					array[i] = bytes[i];
				}
				this.stream.Write(array, 0, 512);
				bytes += 512;
				byteCount -= 512;
			}
			if (byteCount > 0)
			{
				for (int j = 0; j < byteCount; j++)
				{
					array[j] = bytes[j];
				}
				this.stream.Write(array, 0, byteCount);
			}
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x00020850 File Offset: 0x0001EA50
		[SecuritySafeCritical]
		protected unsafe void WriteUTF8Char(int ch)
		{
			if (ch < 128)
			{
				this.WriteByte((byte)ch);
				return;
			}
			if (ch <= 65535)
			{
				char* ptr = stackalloc char[(UIntPtr)2];
				*ptr = (char)ch;
				this.UnsafeWriteUTF8Chars(ptr, 1);
				return;
			}
			SurrogateChar surrogateChar = new SurrogateChar(ch);
			char* ptr2 = stackalloc char[(UIntPtr)4];
			*ptr2 = surrogateChar.HighChar;
			ptr2[1] = surrogateChar.LowChar;
			this.UnsafeWriteUTF8Chars(ptr2, 2);
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x000208B4 File Offset: 0x0001EAB4
		protected void WriteUTF8Chars(byte[] chars, int charOffset, int charCount)
		{
			if (charCount < 512)
			{
				int dstOffset;
				byte[] dst = this.GetBuffer(charCount, out dstOffset);
				Buffer.BlockCopy(chars, charOffset, dst, dstOffset, charCount);
				this.Advance(charCount);
				return;
			}
			this.FlushBuffer();
			this.stream.Write(chars, charOffset, charCount);
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x000208FC File Offset: 0x0001EAFC
		[SecuritySafeCritical]
		protected unsafe void WriteUTF8Chars(string value)
		{
			int length = value.Length;
			if (length > 0)
			{
				fixed (string text = value)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					this.UnsafeWriteUTF8Chars(ptr, length);
				}
			}
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00020930 File Offset: 0x0001EB30
		[SecurityCritical]
		protected unsafe void UnsafeWriteUTF8Chars(char* chars, int charCount)
		{
			while (charCount > 170)
			{
				int num = 170;
				if ((chars[num - 1] & 'ﰀ') == '\ud800')
				{
					num--;
				}
				int num2;
				byte[] array = this.GetBuffer(num * 3, out num2);
				this.Advance(this.UnsafeGetUTF8Chars(chars, num, array, num2));
				charCount -= num;
				chars += num;
			}
			if (charCount > 0)
			{
				int num3;
				byte[] array2 = this.GetBuffer(charCount * 3, out num3);
				this.Advance(this.UnsafeGetUTF8Chars(chars, charCount, array2, num3));
			}
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x000209B4 File Offset: 0x0001EBB4
		[SecurityCritical]
		protected unsafe void UnsafeWriteUnicodeChars(char* chars, int charCount)
		{
			while (charCount > 256)
			{
				int num = 256;
				if ((chars[num - 1] & 'ﰀ') == '\ud800')
				{
					num--;
				}
				int num2;
				byte[] array = this.GetBuffer(num * 2, out num2);
				this.Advance(this.UnsafeGetUnicodeChars(chars, num, array, num2));
				charCount -= num;
				chars += num;
			}
			if (charCount > 0)
			{
				int num3;
				byte[] array2 = this.GetBuffer(charCount * 2, out num3);
				this.Advance(this.UnsafeGetUnicodeChars(chars, charCount, array2, num3));
			}
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00020A38 File Offset: 0x0001EC38
		[SecurityCritical]
		protected unsafe int UnsafeGetUnicodeChars(char* chars, int charCount, byte[] buffer, int offset)
		{
			char* ptr = chars + charCount;
			while (chars < ptr)
			{
				char c = *(chars++);
				buffer[offset++] = (byte)c;
				c >>= 8;
				buffer[offset++] = (byte)c;
			}
			return charCount * 2;
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00020A78 File Offset: 0x0001EC78
		[SecurityCritical]
		protected unsafe int UnsafeGetUTF8Length(char* chars, int charCount)
		{
			char* ptr = chars + charCount;
			while (chars < ptr && *chars < '\u0080')
			{
				chars++;
			}
			if (chars == ptr)
			{
				return charCount;
			}
			return (int)((long)(chars - (ptr - charCount))) + this.encoding.GetByteCount(chars, (int)((long)(ptr - chars)));
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00020AC8 File Offset: 0x0001ECC8
		[SecurityCritical]
		protected unsafe int UnsafeGetUTF8Chars(char* chars, int charCount, byte[] buffer, int offset)
		{
			if (charCount > 0)
			{
				fixed (byte* ptr = &buffer[offset])
				{
					byte* ptr2 = ptr;
					byte* ptr3 = ptr2;
					byte* ptr4 = ptr3 + (buffer.Length - offset);
					char* ptr5 = chars + charCount;
					do
					{
						if (chars < ptr5)
						{
							char c = *chars;
							if (c < '\u0080')
							{
								*ptr3 = (byte)c;
								ptr3++;
								chars++;
								continue;
							}
						}
						if (chars >= ptr5)
						{
							break;
						}
						char* ptr6 = chars;
						while (chars < ptr5 && *chars >= '\u0080')
						{
							chars++;
						}
						ptr3 += this.encoding.GetBytes(ptr6, (int)((long)(chars - ptr6)), ptr3, (int)((long)(ptr4 - ptr3)));
					}
					while (chars < ptr5);
					return (int)((long)(ptr3 - ptr2));
				}
			}
			return 0;
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00020B6A File Offset: 0x0001ED6A
		protected virtual void FlushBuffer()
		{
			if (this.offset != 0)
			{
				this.stream.Write(this.buffer, 0, this.offset);
				this.offset = 0;
			}
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x00020B93 File Offset: 0x0001ED93
		protected virtual IAsyncResult BeginFlushBuffer(AsyncCallback callback, object state)
		{
			return new XmlStreamNodeWriter.FlushBufferAsyncResult(this, callback, state);
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00020B9D File Offset: 0x0001ED9D
		protected virtual void EndFlushBuffer(IAsyncResult result)
		{
			XmlStreamNodeWriter.FlushBufferAsyncResult.End(result);
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00020BA5 File Offset: 0x0001EDA5
		public override void Flush()
		{
			this.FlushBuffer();
			this.stream.Flush();
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00020BB8 File Offset: 0x0001EDB8
		public override void Close()
		{
			if (this.stream != null)
			{
				if (this.ownsStream)
				{
					this.stream.Close();
				}
				this.stream = null;
			}
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00020BDC File Offset: 0x0001EDDC
		// Note: this type is marked as 'beforefieldinit'.
		static XmlStreamNodeWriter()
		{
		}

		// Token: 0x0400037F RID: 895
		private Stream stream;

		// Token: 0x04000380 RID: 896
		private byte[] buffer;

		// Token: 0x04000381 RID: 897
		private int offset;

		// Token: 0x04000382 RID: 898
		private bool ownsStream;

		// Token: 0x04000383 RID: 899
		private const int bufferLength = 512;

		// Token: 0x04000384 RID: 900
		private const int maxEntityLength = 32;

		// Token: 0x04000385 RID: 901
		private const int maxBytesPerChar = 3;

		// Token: 0x04000386 RID: 902
		private Encoding encoding;

		// Token: 0x04000387 RID: 903
		private int hasPendingWrite;

		// Token: 0x04000388 RID: 904
		private AsyncEventArgs<object> flushBufferState;

		// Token: 0x04000389 RID: 905
		private static UTF8Encoding UTF8Encoding = new UTF8Encoding(false, true);

		// Token: 0x0400038A RID: 906
		private static AsyncCallback onFlushBufferComplete;

		// Token: 0x0400038B RID: 907
		private static AsyncEventArgsCallback onGetFlushComplete;

		// Token: 0x02000093 RID: 147
		private class GetBufferAsyncResult : AsyncResult
		{
			// Token: 0x0600080D RID: 2061 RVA: 0x00020BEC File Offset: 0x0001EDEC
			public GetBufferAsyncResult(int count, XmlStreamNodeWriter writer, AsyncCallback callback, object state) : base(callback, state)
			{
				this.count = count;
				this.writer = writer;
				int num = writer.offset;
				bool flag;
				if (num + count <= 512)
				{
					this.offset = num;
					flag = true;
				}
				else
				{
					IAsyncResult result = writer.BeginFlushBuffer(base.PrepareAsyncCompletion(XmlStreamNodeWriter.GetBufferAsyncResult.onComplete), this);
					flag = base.SyncContinue(result);
				}
				if (flag)
				{
					base.Complete(true);
				}
			}

			// Token: 0x0600080E RID: 2062 RVA: 0x00020C53 File Offset: 0x0001EE53
			private static bool OnComplete(IAsyncResult result)
			{
				return ((XmlStreamNodeWriter.GetBufferAsyncResult)result.AsyncState).HandleFlushBuffer(result);
			}

			// Token: 0x0600080F RID: 2063 RVA: 0x00020C66 File Offset: 0x0001EE66
			private bool HandleFlushBuffer(IAsyncResult result)
			{
				this.writer.EndFlushBuffer(result);
				this.offset = 0;
				return true;
			}

			// Token: 0x06000810 RID: 2064 RVA: 0x00020C7C File Offset: 0x0001EE7C
			public static byte[] End(IAsyncResult result, out int offset)
			{
				XmlStreamNodeWriter.GetBufferAsyncResult getBufferAsyncResult = AsyncResult.End<XmlStreamNodeWriter.GetBufferAsyncResult>(result);
				offset = getBufferAsyncResult.offset;
				return getBufferAsyncResult.writer.buffer;
			}

			// Token: 0x06000811 RID: 2065 RVA: 0x00020CA3 File Offset: 0x0001EEA3
			// Note: this type is marked as 'beforefieldinit'.
			static GetBufferAsyncResult()
			{
			}

			// Token: 0x0400038C RID: 908
			private XmlStreamNodeWriter writer;

			// Token: 0x0400038D RID: 909
			private int offset;

			// Token: 0x0400038E RID: 910
			private int count;

			// Token: 0x0400038F RID: 911
			private static AsyncResult.AsyncCompletion onComplete = new AsyncResult.AsyncCompletion(XmlStreamNodeWriter.GetBufferAsyncResult.OnComplete);
		}

		// Token: 0x02000094 RID: 148
		private class WriteBytesAsyncResult : AsyncResult
		{
			// Token: 0x06000812 RID: 2066 RVA: 0x00020CB8 File Offset: 0x0001EEB8
			public WriteBytesAsyncResult(byte[] byteBuffer, int byteOffset, int byteCount, XmlStreamNodeWriter writer, AsyncCallback callback, object state) : base(callback, state)
			{
				this.byteBuffer = byteBuffer;
				this.byteOffset = byteOffset;
				this.byteCount = byteCount;
				this.writer = writer;
				bool flag;
				if (byteCount < 512)
				{
					flag = this.HandleGetBuffer(null);
				}
				else
				{
					flag = this.HandleFlushBuffer(null);
				}
				if (flag)
				{
					base.Complete(true);
				}
			}

			// Token: 0x06000813 RID: 2067 RVA: 0x00020D12 File Offset: 0x0001EF12
			private static bool OnHandleGetBufferComplete(IAsyncResult result)
			{
				return ((XmlStreamNodeWriter.WriteBytesAsyncResult)result.AsyncState).HandleGetBuffer(result);
			}

			// Token: 0x06000814 RID: 2068 RVA: 0x00020D25 File Offset: 0x0001EF25
			private static bool OnHandleFlushBufferComplete(IAsyncResult result)
			{
				return ((XmlStreamNodeWriter.WriteBytesAsyncResult)result.AsyncState).HandleFlushBuffer(result);
			}

			// Token: 0x06000815 RID: 2069 RVA: 0x00020D38 File Offset: 0x0001EF38
			private static bool OnHandleWrite(IAsyncResult result)
			{
				return ((XmlStreamNodeWriter.WriteBytesAsyncResult)result.AsyncState).HandleWrite(result);
			}

			// Token: 0x06000816 RID: 2070 RVA: 0x00020D4C File Offset: 0x0001EF4C
			private bool HandleGetBuffer(IAsyncResult result)
			{
				if (result == null)
				{
					result = this.writer.BeginGetBuffer(this.byteCount, base.PrepareAsyncCompletion(XmlStreamNodeWriter.WriteBytesAsyncResult.onHandleGetBufferComplete), this);
					if (!result.CompletedSynchronously)
					{
						return false;
					}
				}
				int dstOffset;
				byte[] dst = this.writer.EndGetBuffer(result, out dstOffset);
				Buffer.BlockCopy(this.byteBuffer, this.byteOffset, dst, dstOffset, this.byteCount);
				this.writer.Advance(this.byteCount);
				return true;
			}

			// Token: 0x06000817 RID: 2071 RVA: 0x00020DBF File Offset: 0x0001EFBF
			private bool HandleFlushBuffer(IAsyncResult result)
			{
				if (result == null)
				{
					result = this.writer.BeginFlushBuffer(base.PrepareAsyncCompletion(XmlStreamNodeWriter.WriteBytesAsyncResult.onHandleFlushBufferComplete), this);
					if (!result.CompletedSynchronously)
					{
						return false;
					}
				}
				this.writer.EndFlushBuffer(result);
				return this.HandleWrite(null);
			}

			// Token: 0x06000818 RID: 2072 RVA: 0x00020DFC File Offset: 0x0001EFFC
			private bool HandleWrite(IAsyncResult result)
			{
				if (result == null)
				{
					result = this.writer.stream.BeginWrite(this.byteBuffer, this.byteOffset, this.byteCount, base.PrepareAsyncCompletion(XmlStreamNodeWriter.WriteBytesAsyncResult.onHandleWrite), this);
					if (!result.CompletedSynchronously)
					{
						return false;
					}
				}
				this.writer.stream.EndWrite(result);
				return true;
			}

			// Token: 0x06000819 RID: 2073 RVA: 0x00020E58 File Offset: 0x0001F058
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<XmlStreamNodeWriter.WriteBytesAsyncResult>(result);
			}

			// Token: 0x0600081A RID: 2074 RVA: 0x00020E61 File Offset: 0x0001F061
			// Note: this type is marked as 'beforefieldinit'.
			static WriteBytesAsyncResult()
			{
			}

			// Token: 0x04000390 RID: 912
			private static AsyncResult.AsyncCompletion onHandleGetBufferComplete = new AsyncResult.AsyncCompletion(XmlStreamNodeWriter.WriteBytesAsyncResult.OnHandleGetBufferComplete);

			// Token: 0x04000391 RID: 913
			private static AsyncResult.AsyncCompletion onHandleFlushBufferComplete = new AsyncResult.AsyncCompletion(XmlStreamNodeWriter.WriteBytesAsyncResult.OnHandleFlushBufferComplete);

			// Token: 0x04000392 RID: 914
			private static AsyncResult.AsyncCompletion onHandleWrite = new AsyncResult.AsyncCompletion(XmlStreamNodeWriter.WriteBytesAsyncResult.OnHandleWrite);

			// Token: 0x04000393 RID: 915
			private byte[] byteBuffer;

			// Token: 0x04000394 RID: 916
			private int byteOffset;

			// Token: 0x04000395 RID: 917
			private int byteCount;

			// Token: 0x04000396 RID: 918
			private XmlStreamNodeWriter writer;
		}

		// Token: 0x02000095 RID: 149
		private class FlushBufferAsyncResult : AsyncResult
		{
			// Token: 0x0600081B RID: 2075 RVA: 0x00020E98 File Offset: 0x0001F098
			public FlushBufferAsyncResult(XmlStreamNodeWriter writer, AsyncCallback callback, object state) : base(callback, state)
			{
				this.writer = writer;
				bool flag = true;
				if (writer.offset != 0)
				{
					flag = this.HandleFlushBuffer(null);
				}
				if (flag)
				{
					base.Complete(true);
				}
			}

			// Token: 0x0600081C RID: 2076 RVA: 0x00020ED0 File Offset: 0x0001F0D0
			private static bool OnComplete(IAsyncResult result)
			{
				return ((XmlStreamNodeWriter.FlushBufferAsyncResult)result.AsyncState).HandleFlushBuffer(result);
			}

			// Token: 0x0600081D RID: 2077 RVA: 0x00020EE4 File Offset: 0x0001F0E4
			private bool HandleFlushBuffer(IAsyncResult result)
			{
				if (result == null)
				{
					result = this.writer.stream.BeginWrite(this.writer.buffer, 0, this.writer.offset, base.PrepareAsyncCompletion(XmlStreamNodeWriter.FlushBufferAsyncResult.onComplete), this);
					if (!result.CompletedSynchronously)
					{
						return false;
					}
				}
				this.writer.stream.EndWrite(result);
				this.writer.offset = 0;
				return true;
			}

			// Token: 0x0600081E RID: 2078 RVA: 0x00020F51 File Offset: 0x0001F151
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<XmlStreamNodeWriter.FlushBufferAsyncResult>(result);
			}

			// Token: 0x0600081F RID: 2079 RVA: 0x00020F5A File Offset: 0x0001F15A
			// Note: this type is marked as 'beforefieldinit'.
			static FlushBufferAsyncResult()
			{
			}

			// Token: 0x04000397 RID: 919
			private static AsyncResult.AsyncCompletion onComplete = new AsyncResult.AsyncCompletion(XmlStreamNodeWriter.FlushBufferAsyncResult.OnComplete);

			// Token: 0x04000398 RID: 920
			private XmlStreamNodeWriter writer;
		}

		// Token: 0x02000096 RID: 150
		internal class GetBufferArgs
		{
			// Token: 0x17000110 RID: 272
			// (get) Token: 0x06000820 RID: 2080 RVA: 0x00020F6D File Offset: 0x0001F16D
			// (set) Token: 0x06000821 RID: 2081 RVA: 0x00020F75 File Offset: 0x0001F175
			public int Count
			{
				[CompilerGenerated]
				get
				{
					return this.<Count>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Count>k__BackingField = value;
				}
			}

			// Token: 0x06000822 RID: 2082 RVA: 0x0000222F File Offset: 0x0000042F
			public GetBufferArgs()
			{
			}

			// Token: 0x04000399 RID: 921
			[CompilerGenerated]
			private int <Count>k__BackingField;
		}

		// Token: 0x02000097 RID: 151
		internal class GetBufferEventResult
		{
			// Token: 0x17000111 RID: 273
			// (get) Token: 0x06000823 RID: 2083 RVA: 0x00020F7E File Offset: 0x0001F17E
			// (set) Token: 0x06000824 RID: 2084 RVA: 0x00020F86 File Offset: 0x0001F186
			internal byte[] Buffer
			{
				[CompilerGenerated]
				get
				{
					return this.<Buffer>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Buffer>k__BackingField = value;
				}
			}

			// Token: 0x17000112 RID: 274
			// (get) Token: 0x06000825 RID: 2085 RVA: 0x00020F8F File Offset: 0x0001F18F
			// (set) Token: 0x06000826 RID: 2086 RVA: 0x00020F97 File Offset: 0x0001F197
			internal int Offset
			{
				[CompilerGenerated]
				get
				{
					return this.<Offset>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Offset>k__BackingField = value;
				}
			}

			// Token: 0x06000827 RID: 2087 RVA: 0x0000222F File Offset: 0x0000042F
			public GetBufferEventResult()
			{
			}

			// Token: 0x0400039A RID: 922
			[CompilerGenerated]
			private byte[] <Buffer>k__BackingField;

			// Token: 0x0400039B RID: 923
			[CompilerGenerated]
			private int <Offset>k__BackingField;
		}

		// Token: 0x02000098 RID: 152
		internal class GetBufferAsyncEventArgs : AsyncEventArgs<XmlStreamNodeWriter.GetBufferArgs, XmlStreamNodeWriter.GetBufferEventResult>
		{
			// Token: 0x06000828 RID: 2088 RVA: 0x00020FA0 File Offset: 0x0001F1A0
			public GetBufferAsyncEventArgs()
			{
			}
		}
	}
}
