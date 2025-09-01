using System;
using System.IO;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Net
{
	// Token: 0x02000571 RID: 1393
	internal sealed class Base64Stream : DelegatedStream, IEncodableStream
	{
		// Token: 0x06002D00 RID: 11520 RVA: 0x0009A2D5 File Offset: 0x000984D5
		internal Base64Stream(Stream stream, Base64WriteStateInfo writeStateInfo) : base(stream)
		{
			this._writeState = new Base64WriteStateInfo();
			this._lineLength = writeStateInfo.MaxLineLength;
		}

		// Token: 0x06002D01 RID: 11521 RVA: 0x0009A2F5 File Offset: 0x000984F5
		internal Base64Stream(Base64WriteStateInfo writeStateInfo) : base(new MemoryStream())
		{
			this._lineLength = writeStateInfo.MaxLineLength;
			this._writeState = writeStateInfo;
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06002D02 RID: 11522 RVA: 0x0009A318 File Offset: 0x00098518
		private Base64Stream.ReadStateInfo ReadState
		{
			get
			{
				Base64Stream.ReadStateInfo result;
				if ((result = this._readState) == null)
				{
					result = (this._readState = new Base64Stream.ReadStateInfo());
				}
				return result;
			}
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06002D03 RID: 11523 RVA: 0x0009A33D File Offset: 0x0009853D
		internal Base64WriteStateInfo WriteState
		{
			get
			{
				return this._writeState;
			}
		}

		// Token: 0x06002D04 RID: 11524 RVA: 0x0009A348 File Offset: 0x00098548
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			Base64Stream.ReadAsyncResult readAsyncResult = new Base64Stream.ReadAsyncResult(this, buffer, offset, count, callback, state);
			readAsyncResult.Read();
			return readAsyncResult;
		}

		// Token: 0x06002D05 RID: 11525 RVA: 0x0009A3A0 File Offset: 0x000985A0
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			Base64Stream.WriteAsyncResult writeAsyncResult = new Base64Stream.WriteAsyncResult(this, buffer, offset, count, callback, state);
			writeAsyncResult.Write();
			return writeAsyncResult;
		}

		// Token: 0x06002D06 RID: 11526 RVA: 0x0009A3F8 File Offset: 0x000985F8
		public override void Close()
		{
			if (this._writeState != null && this.WriteState.Length > 0)
			{
				int padding = this.WriteState.Padding;
				if (padding != 1)
				{
					if (padding == 2)
					{
						this.WriteState.Append(new byte[]
						{
							Base64Stream.s_base64EncodeMap[(int)this.WriteState.LastBits],
							Base64Stream.s_base64EncodeMap[64],
							Base64Stream.s_base64EncodeMap[64]
						});
					}
				}
				else
				{
					this.WriteState.Append(new byte[]
					{
						Base64Stream.s_base64EncodeMap[(int)this.WriteState.LastBits],
						Base64Stream.s_base64EncodeMap[64]
					});
				}
				this.WriteState.Padding = 0;
				this.FlushInternal();
			}
			base.Close();
		}

		// Token: 0x06002D07 RID: 11527 RVA: 0x0009A4BC File Offset: 0x000986BC
		public unsafe int DecodeBytes(byte[] buffer, int offset, int count)
		{
			byte* ptr;
			if (buffer == null || buffer.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &buffer[0];
			}
			byte* ptr2 = ptr + offset;
			byte* ptr3 = ptr2;
			byte* ptr4 = ptr2;
			byte* ptr5 = ptr2 + count;
			while (ptr3 < ptr5)
			{
				if (*ptr3 == 13 || *ptr3 == 10 || *ptr3 == 61 || *ptr3 == 32 || *ptr3 == 9)
				{
					ptr3++;
				}
				else
				{
					byte b = Base64Stream.s_base64DecodeMap[(int)(*ptr3)];
					if (b == 255)
					{
						throw new FormatException("An invalid character was found in the Base-64 stream.");
					}
					switch (this.ReadState.Pos)
					{
					case 0:
					{
						this.ReadState.Val = (byte)(b << 2);
						Base64Stream.ReadStateInfo readState = this.ReadState;
						byte pos = readState.Pos;
						readState.Pos = pos + 1;
						break;
					}
					case 1:
					{
						*(ptr4++) = (byte)((int)this.ReadState.Val + (b >> 4));
						this.ReadState.Val = (byte)(b << 4);
						Base64Stream.ReadStateInfo readState2 = this.ReadState;
						byte pos = readState2.Pos;
						readState2.Pos = pos + 1;
						break;
					}
					case 2:
					{
						*(ptr4++) = (byte)((int)this.ReadState.Val + (b >> 2));
						this.ReadState.Val = (byte)(b << 6);
						Base64Stream.ReadStateInfo readState3 = this.ReadState;
						byte pos = readState3.Pos;
						readState3.Pos = pos + 1;
						break;
					}
					case 3:
						*(ptr4++) = this.ReadState.Val + b;
						this.ReadState.Pos = 0;
						break;
					}
					ptr3++;
				}
			}
			return (int)((long)(ptr4 - ptr2));
		}

		// Token: 0x06002D08 RID: 11528 RVA: 0x0009A64C File Offset: 0x0009884C
		public int EncodeBytes(byte[] buffer, int offset, int count)
		{
			return this.EncodeBytes(buffer, offset, count, true, true);
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x0009A65C File Offset: 0x0009885C
		internal int EncodeBytes(byte[] buffer, int offset, int count, bool dontDeferFinalBytes, bool shouldAppendSpaceToCRLF)
		{
			this.WriteState.AppendHeader();
			int i = offset;
			int num = this.WriteState.Padding;
			if (num != 1)
			{
				if (num == 2)
				{
					this.WriteState.Append(Base64Stream.s_base64EncodeMap[(int)this.WriteState.LastBits | (buffer[i] & 240) >> 4]);
					if (count == 1)
					{
						this.WriteState.LastBits = (byte)((buffer[i] & 15) << 2);
						this.WriteState.Padding = 1;
						i++;
						return i - offset;
					}
					this.WriteState.Append(Base64Stream.s_base64EncodeMap[(int)(buffer[i] & 15) << 2 | (buffer[i + 1] & 192) >> 6]);
					this.WriteState.Append(Base64Stream.s_base64EncodeMap[(int)(buffer[i + 1] & 63)]);
					i += 2;
					count -= 2;
					this.WriteState.Padding = 0;
				}
			}
			else
			{
				this.WriteState.Append(Base64Stream.s_base64EncodeMap[(int)this.WriteState.LastBits | (buffer[i] & 192) >> 6]);
				this.WriteState.Append(Base64Stream.s_base64EncodeMap[(int)(buffer[i] & 63)]);
				i++;
				count--;
				this.WriteState.Padding = 0;
			}
			int num2 = i + (count - count % 3);
			while (i < num2)
			{
				if (this._lineLength != -1 && this.WriteState.CurrentLineLength + 4 + this._writeState.FooterLength > this._lineLength)
				{
					this.WriteState.AppendCRLF(shouldAppendSpaceToCRLF);
				}
				this.WriteState.Append(Base64Stream.s_base64EncodeMap[(buffer[i] & 252) >> 2]);
				this.WriteState.Append(Base64Stream.s_base64EncodeMap[(int)(buffer[i] & 3) << 4 | (buffer[i + 1] & 240) >> 4]);
				this.WriteState.Append(Base64Stream.s_base64EncodeMap[(int)(buffer[i + 1] & 15) << 2 | (buffer[i + 2] & 192) >> 6]);
				this.WriteState.Append(Base64Stream.s_base64EncodeMap[(int)(buffer[i + 2] & 63)]);
				i += 3;
			}
			i = num2;
			if (count % 3 != 0 && this._lineLength != -1 && this.WriteState.CurrentLineLength + 4 + this._writeState.FooterLength >= this._lineLength)
			{
				this.WriteState.AppendCRLF(shouldAppendSpaceToCRLF);
			}
			num = count % 3;
			if (num != 1)
			{
				if (num == 2)
				{
					this.WriteState.Append(Base64Stream.s_base64EncodeMap[(buffer[i] & 252) >> 2]);
					this.WriteState.Append(Base64Stream.s_base64EncodeMap[(int)(buffer[i] & 3) << 4 | (buffer[i + 1] & 240) >> 4]);
					if (dontDeferFinalBytes)
					{
						this.WriteState.Append(Base64Stream.s_base64EncodeMap[(int)(buffer[i + 1] & 15) << 2]);
						this.WriteState.Append(Base64Stream.s_base64EncodeMap[64]);
						this.WriteState.Padding = 0;
					}
					else
					{
						this.WriteState.LastBits = (byte)((buffer[i + 1] & 15) << 2);
						this.WriteState.Padding = 1;
					}
					i += 2;
				}
			}
			else
			{
				this.WriteState.Append(Base64Stream.s_base64EncodeMap[(buffer[i] & 252) >> 2]);
				if (dontDeferFinalBytes)
				{
					this.WriteState.Append(Base64Stream.s_base64EncodeMap[(int)((byte)((buffer[i] & 3) << 4))]);
					this.WriteState.Append(Base64Stream.s_base64EncodeMap[64]);
					this.WriteState.Append(Base64Stream.s_base64EncodeMap[64]);
					this.WriteState.Padding = 0;
				}
				else
				{
					this.WriteState.LastBits = (byte)((buffer[i] & 3) << 4);
					this.WriteState.Padding = 2;
				}
				i++;
			}
			this.WriteState.AppendFooter();
			return i - offset;
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x000075E1 File Offset: 0x000057E1
		public Stream GetStream()
		{
			return this;
		}

		// Token: 0x06002D0B RID: 11531 RVA: 0x0009A9FD File Offset: 0x00098BFD
		public string GetEncodedString()
		{
			return Encoding.ASCII.GetString(this.WriteState.Buffer, 0, this.WriteState.Length);
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x0009AA20 File Offset: 0x00098C20
		public override int EndRead(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			return Base64Stream.ReadAsyncResult.End(asyncResult);
		}

		// Token: 0x06002D0D RID: 11533 RVA: 0x0009AA36 File Offset: 0x00098C36
		public override void EndWrite(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			Base64Stream.WriteAsyncResult.End(asyncResult);
		}

		// Token: 0x06002D0E RID: 11534 RVA: 0x0009AA4C File Offset: 0x00098C4C
		public override void Flush()
		{
			if (this._writeState != null && this.WriteState.Length > 0)
			{
				this.FlushInternal();
			}
			base.Flush();
		}

		// Token: 0x06002D0F RID: 11535 RVA: 0x0009AA70 File Offset: 0x00098C70
		private void FlushInternal()
		{
			base.Write(this.WriteState.Buffer, 0, this.WriteState.Length);
			this.WriteState.Reset();
		}

		// Token: 0x06002D10 RID: 11536 RVA: 0x0009AA9C File Offset: 0x00098C9C
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			for (;;)
			{
				int num = base.Read(buffer, offset, count);
				if (num == 0)
				{
					break;
				}
				num = this.DecodeBytes(buffer, offset, num);
				if (num > 0)
				{
					return num;
				}
			}
			return 0;
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x0009AB00 File Offset: 0x00098D00
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			int num = 0;
			for (;;)
			{
				num += this.EncodeBytes(buffer, offset + num, count - num, false, false);
				if (num >= count)
				{
					break;
				}
				this.FlushInternal();
			}
		}

		// Token: 0x06002D12 RID: 11538 RVA: 0x0009AB63 File Offset: 0x00098D63
		// Note: this type is marked as 'beforefieldinit'.
		static Base64Stream()
		{
		}

		// Token: 0x0400187F RID: 6271
		private static readonly byte[] s_base64DecodeMap = new byte[]
		{
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			62,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			63,
			52,
			53,
			54,
			55,
			56,
			57,
			58,
			59,
			60,
			61,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			0,
			1,
			2,
			3,
			4,
			5,
			6,
			7,
			8,
			9,
			10,
			11,
			12,
			13,
			14,
			15,
			16,
			17,
			18,
			19,
			20,
			21,
			22,
			23,
			24,
			25,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			26,
			27,
			28,
			29,
			30,
			31,
			32,
			33,
			34,
			35,
			36,
			37,
			38,
			39,
			40,
			41,
			42,
			43,
			44,
			45,
			46,
			47,
			48,
			49,
			50,
			51,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue
		};

		// Token: 0x04001880 RID: 6272
		private static readonly byte[] s_base64EncodeMap = new byte[]
		{
			65,
			66,
			67,
			68,
			69,
			70,
			71,
			72,
			73,
			74,
			75,
			76,
			77,
			78,
			79,
			80,
			81,
			82,
			83,
			84,
			85,
			86,
			87,
			88,
			89,
			90,
			97,
			98,
			99,
			100,
			101,
			102,
			103,
			104,
			105,
			106,
			107,
			108,
			109,
			110,
			111,
			112,
			113,
			114,
			115,
			116,
			117,
			118,
			119,
			120,
			121,
			122,
			48,
			49,
			50,
			51,
			52,
			53,
			54,
			55,
			56,
			57,
			43,
			47,
			61
		};

		// Token: 0x04001881 RID: 6273
		private readonly int _lineLength;

		// Token: 0x04001882 RID: 6274
		private readonly Base64WriteStateInfo _writeState;

		// Token: 0x04001883 RID: 6275
		private Base64Stream.ReadStateInfo _readState;

		// Token: 0x04001884 RID: 6276
		private const int SizeOfBase64EncodedChar = 4;

		// Token: 0x04001885 RID: 6277
		private const byte InvalidBase64Value = 255;

		// Token: 0x02000572 RID: 1394
		private sealed class ReadAsyncResult : LazyAsyncResult
		{
			// Token: 0x06002D13 RID: 11539 RVA: 0x0009AB96 File Offset: 0x00098D96
			internal ReadAsyncResult(Base64Stream parent, byte[] buffer, int offset, int count, AsyncCallback callback, object state) : base(null, state, callback)
			{
				this._parent = parent;
				this._buffer = buffer;
				this._offset = offset;
				this._count = count;
			}

			// Token: 0x06002D14 RID: 11540 RVA: 0x0009ABC0 File Offset: 0x00098DC0
			private bool CompleteRead(IAsyncResult result)
			{
				this._read = this._parent.BaseStream.EndRead(result);
				if (this._read == 0)
				{
					base.InvokeCallback();
					return true;
				}
				this._read = this._parent.DecodeBytes(this._buffer, this._offset, this._read);
				if (this._read > 0)
				{
					base.InvokeCallback();
					return true;
				}
				return false;
			}

			// Token: 0x06002D15 RID: 11541 RVA: 0x0009AC2C File Offset: 0x00098E2C
			internal void Read()
			{
				IAsyncResult asyncResult;
				do
				{
					asyncResult = this._parent.BaseStream.BeginRead(this._buffer, this._offset, this._count, Base64Stream.ReadAsyncResult.s_onRead, this);
				}
				while (asyncResult.CompletedSynchronously && !this.CompleteRead(asyncResult));
			}

			// Token: 0x06002D16 RID: 11542 RVA: 0x0009AC74 File Offset: 0x00098E74
			private static void OnRead(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					Base64Stream.ReadAsyncResult readAsyncResult = (Base64Stream.ReadAsyncResult)result.AsyncState;
					try
					{
						if (!readAsyncResult.CompleteRead(result))
						{
							readAsyncResult.Read();
						}
					}
					catch (Exception result2)
					{
						if (readAsyncResult.IsCompleted)
						{
							throw;
						}
						readAsyncResult.InvokeCallback(result2);
					}
				}
			}

			// Token: 0x06002D17 RID: 11543 RVA: 0x0009ACCC File Offset: 0x00098ECC
			internal static int End(IAsyncResult result)
			{
				Base64Stream.ReadAsyncResult readAsyncResult = (Base64Stream.ReadAsyncResult)result;
				readAsyncResult.InternalWaitForCompletion();
				return readAsyncResult._read;
			}

			// Token: 0x06002D18 RID: 11544 RVA: 0x0009ACE0 File Offset: 0x00098EE0
			// Note: this type is marked as 'beforefieldinit'.
			static ReadAsyncResult()
			{
			}

			// Token: 0x04001886 RID: 6278
			private readonly Base64Stream _parent;

			// Token: 0x04001887 RID: 6279
			private readonly byte[] _buffer;

			// Token: 0x04001888 RID: 6280
			private readonly int _offset;

			// Token: 0x04001889 RID: 6281
			private readonly int _count;

			// Token: 0x0400188A RID: 6282
			private int _read;

			// Token: 0x0400188B RID: 6283
			private static readonly AsyncCallback s_onRead = new AsyncCallback(Base64Stream.ReadAsyncResult.OnRead);
		}

		// Token: 0x02000573 RID: 1395
		private sealed class WriteAsyncResult : LazyAsyncResult
		{
			// Token: 0x06002D19 RID: 11545 RVA: 0x0009ACF3 File Offset: 0x00098EF3
			internal WriteAsyncResult(Base64Stream parent, byte[] buffer, int offset, int count, AsyncCallback callback, object state) : base(null, state, callback)
			{
				this._parent = parent;
				this._buffer = buffer;
				this._offset = offset;
				this._count = count;
			}

			// Token: 0x06002D1A RID: 11546 RVA: 0x0009AD20 File Offset: 0x00098F20
			internal void Write()
			{
				for (;;)
				{
					this._written += this._parent.EncodeBytes(this._buffer, this._offset + this._written, this._count - this._written, false, false);
					if (this._written >= this._count)
					{
						break;
					}
					IAsyncResult asyncResult = this._parent.BaseStream.BeginWrite(this._parent.WriteState.Buffer, 0, this._parent.WriteState.Length, Base64Stream.WriteAsyncResult.s_onWrite, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return;
					}
					this.CompleteWrite(asyncResult);
				}
				base.InvokeCallback();
			}

			// Token: 0x06002D1B RID: 11547 RVA: 0x0009ADC7 File Offset: 0x00098FC7
			private void CompleteWrite(IAsyncResult result)
			{
				this._parent.BaseStream.EndWrite(result);
				this._parent.WriteState.Reset();
			}

			// Token: 0x06002D1C RID: 11548 RVA: 0x0009ADEC File Offset: 0x00098FEC
			private static void OnWrite(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					Base64Stream.WriteAsyncResult writeAsyncResult = (Base64Stream.WriteAsyncResult)result.AsyncState;
					try
					{
						writeAsyncResult.CompleteWrite(result);
						writeAsyncResult.Write();
					}
					catch (Exception result2)
					{
						if (writeAsyncResult.IsCompleted)
						{
							throw;
						}
						writeAsyncResult.InvokeCallback(result2);
					}
				}
			}

			// Token: 0x06002D1D RID: 11549 RVA: 0x0009AE40 File Offset: 0x00099040
			internal static void End(IAsyncResult result)
			{
				((Base64Stream.WriteAsyncResult)result).InternalWaitForCompletion();
			}

			// Token: 0x06002D1E RID: 11550 RVA: 0x0009AE4E File Offset: 0x0009904E
			// Note: this type is marked as 'beforefieldinit'.
			static WriteAsyncResult()
			{
			}

			// Token: 0x0400188C RID: 6284
			private static readonly AsyncCallback s_onWrite = new AsyncCallback(Base64Stream.WriteAsyncResult.OnWrite);

			// Token: 0x0400188D RID: 6285
			private readonly Base64Stream _parent;

			// Token: 0x0400188E RID: 6286
			private readonly byte[] _buffer;

			// Token: 0x0400188F RID: 6287
			private readonly int _offset;

			// Token: 0x04001890 RID: 6288
			private readonly int _count;

			// Token: 0x04001891 RID: 6289
			private int _written;
		}

		// Token: 0x02000574 RID: 1396
		private sealed class ReadStateInfo
		{
			// Token: 0x1700090E RID: 2318
			// (get) Token: 0x06002D1F RID: 11551 RVA: 0x0009AE61 File Offset: 0x00099061
			// (set) Token: 0x06002D20 RID: 11552 RVA: 0x0009AE69 File Offset: 0x00099069
			internal byte Val
			{
				[CompilerGenerated]
				get
				{
					return this.<Val>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Val>k__BackingField = value;
				}
			}

			// Token: 0x1700090F RID: 2319
			// (get) Token: 0x06002D21 RID: 11553 RVA: 0x0009AE72 File Offset: 0x00099072
			// (set) Token: 0x06002D22 RID: 11554 RVA: 0x0009AE7A File Offset: 0x0009907A
			internal byte Pos
			{
				[CompilerGenerated]
				get
				{
					return this.<Pos>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Pos>k__BackingField = value;
				}
			}

			// Token: 0x06002D23 RID: 11555 RVA: 0x0000219B File Offset: 0x0000039B
			public ReadStateInfo()
			{
			}

			// Token: 0x04001892 RID: 6290
			[CompilerGenerated]
			private byte <Val>k__BackingField;

			// Token: 0x04001893 RID: 6291
			[CompilerGenerated]
			private byte <Pos>k__BackingField;
		}
	}
}
