using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x02000810 RID: 2064
	internal class QuotedPrintableStream : DelegatedStream, IEncodableStream
	{
		// Token: 0x060041BF RID: 16831 RVA: 0x000E2D68 File Offset: 0x000E0F68
		internal QuotedPrintableStream(Stream stream, int lineLength) : base(stream)
		{
			if (lineLength < 0)
			{
				throw new ArgumentOutOfRangeException("lineLength");
			}
			this._lineLength = lineLength;
		}

		// Token: 0x060041C0 RID: 16832 RVA: 0x000E2D87 File Offset: 0x000E0F87
		internal QuotedPrintableStream(Stream stream, bool encodeCRLF) : this(stream, 70)
		{
			this._encodeCRLF = encodeCRLF;
		}

		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x060041C1 RID: 16833 RVA: 0x000E2D9C File Offset: 0x000E0F9C
		private QuotedPrintableStream.ReadStateInfo ReadState
		{
			get
			{
				QuotedPrintableStream.ReadStateInfo result;
				if ((result = this._readState) == null)
				{
					result = (this._readState = new QuotedPrintableStream.ReadStateInfo());
				}
				return result;
			}
		}

		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x060041C2 RID: 16834 RVA: 0x000E2DC4 File Offset: 0x000E0FC4
		internal WriteStateInfoBase WriteState
		{
			get
			{
				WriteStateInfoBase result;
				if ((result = this._writeState) == null)
				{
					result = (this._writeState = new WriteStateInfoBase(1024, null, null, this._lineLength));
				}
				return result;
			}
		}

		// Token: 0x060041C3 RID: 16835 RVA: 0x000E2DF8 File Offset: 0x000E0FF8
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
			QuotedPrintableStream.WriteAsyncResult writeAsyncResult = new QuotedPrintableStream.WriteAsyncResult(this, buffer, offset, count, callback, state);
			writeAsyncResult.Write();
			return writeAsyncResult;
		}

		// Token: 0x060041C4 RID: 16836 RVA: 0x000E2E4E File Offset: 0x000E104E
		public override void Close()
		{
			this.FlushInternal();
			base.Close();
		}

		// Token: 0x060041C5 RID: 16837 RVA: 0x000E2E5C File Offset: 0x000E105C
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
			if (this.ReadState.IsEscaped)
			{
				if (this.ReadState.Byte == -1)
				{
					if (count == 1)
					{
						this.ReadState.Byte = (short)(*ptr3);
						return 0;
					}
					if (*ptr3 != 13 || ptr3[1] != 10)
					{
						byte b = QuotedPrintableStream.s_hexDecodeMap[(int)(*ptr3)];
						byte b2 = QuotedPrintableStream.s_hexDecodeMap[(int)ptr3[1]];
						if (b == 255)
						{
							throw new FormatException(SR.Format("Invalid hex digit '{0}'.", b));
						}
						if (b2 == 255)
						{
							throw new FormatException(SR.Format("Invalid hex digit '{0}'.", b2));
						}
						*(ptr4++) = (byte)(((int)b << 4) + (int)b2);
					}
					ptr3 += 2;
				}
				else
				{
					if (this.ReadState.Byte != 13 || *ptr3 != 10)
					{
						byte b3 = QuotedPrintableStream.s_hexDecodeMap[(int)this.ReadState.Byte];
						byte b4 = QuotedPrintableStream.s_hexDecodeMap[(int)(*ptr3)];
						if (b3 == 255)
						{
							throw new FormatException(SR.Format("Invalid hex digit '{0}'.", b3));
						}
						if (b4 == 255)
						{
							throw new FormatException(SR.Format("Invalid hex digit '{0}'.", b4));
						}
						*(ptr4++) = (byte)(((int)b3 << 4) + (int)b4);
					}
					ptr3++;
				}
				this.ReadState.IsEscaped = false;
				this.ReadState.Byte = -1;
			}
			while (ptr3 < ptr5)
			{
				if (*ptr3 == 61)
				{
					long num = (long)(ptr5 - ptr3);
					if (num != 1L)
					{
						if (num != 2L)
						{
							if (ptr3[1] != 13 || ptr3[2] != 10)
							{
								byte b5 = QuotedPrintableStream.s_hexDecodeMap[(int)ptr3[1]];
								byte b6 = QuotedPrintableStream.s_hexDecodeMap[(int)ptr3[2]];
								if (b5 == 255)
								{
									throw new FormatException(SR.Format("Invalid hex digit '{0}'.", b5));
								}
								if (b6 == 255)
								{
									throw new FormatException(SR.Format("Invalid hex digit '{0}'.", b6));
								}
								*(ptr4++) = (byte)(((int)b5 << 4) + (int)b6);
							}
							ptr3 += 3;
							continue;
						}
						this.ReadState.Byte = (short)ptr3[1];
					}
					this.ReadState.IsEscaped = true;
					break;
				}
				*(ptr4++) = *(ptr3++);
			}
			return (int)((long)(ptr4 - ptr2));
		}

		// Token: 0x060041C6 RID: 16838 RVA: 0x000E30C0 File Offset: 0x000E12C0
		public int EncodeBytes(byte[] buffer, int offset, int count)
		{
			int i;
			for (i = offset; i < count + offset; i++)
			{
				if ((this._lineLength != -1 && this.WriteState.CurrentLineLength + 3 + 2 >= this._lineLength && (buffer[i] == 32 || buffer[i] == 9 || buffer[i] == 13 || buffer[i] == 10)) || this._writeState.CurrentLineLength + 3 + 2 >= 70)
				{
					if (this.WriteState.Buffer.Length - this.WriteState.Length < 3)
					{
						return i - offset;
					}
					this.WriteState.Append(61);
					this.WriteState.AppendCRLF(false);
				}
				if (buffer[i] == 13 && i + 1 < count + offset && buffer[i + 1] == 10)
				{
					if (this.WriteState.Buffer.Length - this.WriteState.Length < (this._encodeCRLF ? 6 : 2))
					{
						return i - offset;
					}
					i++;
					if (this._encodeCRLF)
					{
						this.WriteState.Append(new byte[]
						{
							61,
							48,
							68,
							61,
							48,
							65
						});
					}
					else
					{
						this.WriteState.AppendCRLF(false);
					}
				}
				else if ((buffer[i] < 32 && buffer[i] != 9) || buffer[i] == 61 || buffer[i] > 126)
				{
					if (this.WriteState.Buffer.Length - this.WriteState.Length < 3)
					{
						return i - offset;
					}
					this.WriteState.Append(61);
					this.WriteState.Append(QuotedPrintableStream.s_hexEncodeMap[buffer[i] >> 4]);
					this.WriteState.Append(QuotedPrintableStream.s_hexEncodeMap[(int)(buffer[i] & 15)]);
				}
				else
				{
					if (this.WriteState.Buffer.Length - this.WriteState.Length < 1)
					{
						return i - offset;
					}
					if ((buffer[i] == 9 || buffer[i] == 32) && i + 1 >= count + offset)
					{
						if (this.WriteState.Buffer.Length - this.WriteState.Length < 3)
						{
							return i - offset;
						}
						this.WriteState.Append(61);
						this.WriteState.Append(QuotedPrintableStream.s_hexEncodeMap[buffer[i] >> 4]);
						this.WriteState.Append(QuotedPrintableStream.s_hexEncodeMap[(int)(buffer[i] & 15)]);
					}
					else
					{
						this.WriteState.Append(buffer[i]);
					}
				}
			}
			return i - offset;
		}

		// Token: 0x060041C7 RID: 16839 RVA: 0x000075E1 File Offset: 0x000057E1
		public Stream GetStream()
		{
			return this;
		}

		// Token: 0x060041C8 RID: 16840 RVA: 0x000E3308 File Offset: 0x000E1508
		public string GetEncodedString()
		{
			return Encoding.ASCII.GetString(this.WriteState.Buffer, 0, this.WriteState.Length);
		}

		// Token: 0x060041C9 RID: 16841 RVA: 0x000E332B File Offset: 0x000E152B
		public override void EndWrite(IAsyncResult asyncResult)
		{
			QuotedPrintableStream.WriteAsyncResult.End(asyncResult);
		}

		// Token: 0x060041CA RID: 16842 RVA: 0x000E3333 File Offset: 0x000E1533
		public override void Flush()
		{
			this.FlushInternal();
			base.Flush();
		}

		// Token: 0x060041CB RID: 16843 RVA: 0x000E3341 File Offset: 0x000E1541
		private void FlushInternal()
		{
			if (this._writeState != null && this._writeState.Length > 0)
			{
				base.Write(this.WriteState.Buffer, 0, this.WriteState.Length);
				this.WriteState.BufferFlushed();
			}
		}

		// Token: 0x060041CC RID: 16844 RVA: 0x000E3384 File Offset: 0x000E1584
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
				num += this.EncodeBytes(buffer, offset + num, count - num);
				if (num >= count)
				{
					break;
				}
				this.FlushInternal();
			}
		}

		// Token: 0x060041CD RID: 16845 RVA: 0x000E33E5 File Offset: 0x000E15E5
		// Note: this type is marked as 'beforefieldinit'.
		static QuotedPrintableStream()
		{
		}

		// Token: 0x040027E1 RID: 10209
		private bool _encodeCRLF;

		// Token: 0x040027E2 RID: 10210
		private const int SizeOfSoftCRLF = 3;

		// Token: 0x040027E3 RID: 10211
		private const int SizeOfEncodedChar = 3;

		// Token: 0x040027E4 RID: 10212
		private const int SizeOfEncodedCRLF = 6;

		// Token: 0x040027E5 RID: 10213
		private const int SizeOfNonEncodedCRLF = 2;

		// Token: 0x040027E6 RID: 10214
		private static readonly byte[] s_hexDecodeMap = new byte[]
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
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			10,
			11,
			12,
			13,
			14,
			15,
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
			10,
			11,
			12,
			13,
			14,
			15,
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

		// Token: 0x040027E7 RID: 10215
		private static readonly byte[] s_hexEncodeMap = new byte[]
		{
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
			65,
			66,
			67,
			68,
			69,
			70
		};

		// Token: 0x040027E8 RID: 10216
		private int _lineLength;

		// Token: 0x040027E9 RID: 10217
		private QuotedPrintableStream.ReadStateInfo _readState;

		// Token: 0x040027EA RID: 10218
		private WriteStateInfoBase _writeState;

		// Token: 0x02000811 RID: 2065
		private sealed class ReadStateInfo
		{
			// Token: 0x17000EDB RID: 3803
			// (get) Token: 0x060041CE RID: 16846 RVA: 0x000E3418 File Offset: 0x000E1618
			// (set) Token: 0x060041CF RID: 16847 RVA: 0x000E3420 File Offset: 0x000E1620
			internal bool IsEscaped
			{
				[CompilerGenerated]
				get
				{
					return this.<IsEscaped>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<IsEscaped>k__BackingField = value;
				}
			}

			// Token: 0x17000EDC RID: 3804
			// (get) Token: 0x060041D0 RID: 16848 RVA: 0x000E3429 File Offset: 0x000E1629
			// (set) Token: 0x060041D1 RID: 16849 RVA: 0x000E3431 File Offset: 0x000E1631
			internal short Byte
			{
				[CompilerGenerated]
				get
				{
					return this.<Byte>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<Byte>k__BackingField = value;
				}
			} = -1;

			// Token: 0x060041D2 RID: 16850 RVA: 0x000E343A File Offset: 0x000E163A
			public ReadStateInfo()
			{
			}

			// Token: 0x040027EB RID: 10219
			[CompilerGenerated]
			private bool <IsEscaped>k__BackingField;

			// Token: 0x040027EC RID: 10220
			[CompilerGenerated]
			private short <Byte>k__BackingField;
		}

		// Token: 0x02000812 RID: 2066
		private sealed class WriteAsyncResult : LazyAsyncResult
		{
			// Token: 0x060041D3 RID: 16851 RVA: 0x000E3449 File Offset: 0x000E1649
			internal WriteAsyncResult(QuotedPrintableStream parent, byte[] buffer, int offset, int count, AsyncCallback callback, object state) : base(null, state, callback)
			{
				this._parent = parent;
				this._buffer = buffer;
				this._offset = offset;
				this._count = count;
			}

			// Token: 0x060041D4 RID: 16852 RVA: 0x000E3473 File Offset: 0x000E1673
			private void CompleteWrite(IAsyncResult result)
			{
				this._parent.BaseStream.EndWrite(result);
				this._parent.WriteState.BufferFlushed();
			}

			// Token: 0x060041D5 RID: 16853 RVA: 0x000E3496 File Offset: 0x000E1696
			internal static void End(IAsyncResult result)
			{
				((QuotedPrintableStream.WriteAsyncResult)result).InternalWaitForCompletion();
			}

			// Token: 0x060041D6 RID: 16854 RVA: 0x000E34A4 File Offset: 0x000E16A4
			private static void OnWrite(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					QuotedPrintableStream.WriteAsyncResult writeAsyncResult = (QuotedPrintableStream.WriteAsyncResult)result.AsyncState;
					try
					{
						writeAsyncResult.CompleteWrite(result);
						writeAsyncResult.Write();
					}
					catch (Exception result2)
					{
						writeAsyncResult.InvokeCallback(result2);
					}
				}
			}

			// Token: 0x060041D7 RID: 16855 RVA: 0x000E34F0 File Offset: 0x000E16F0
			internal void Write()
			{
				for (;;)
				{
					this._written += this._parent.EncodeBytes(this._buffer, this._offset + this._written, this._count - this._written);
					if (this._written >= this._count)
					{
						break;
					}
					IAsyncResult asyncResult = this._parent.BaseStream.BeginWrite(this._parent.WriteState.Buffer, 0, this._parent.WriteState.Length, QuotedPrintableStream.WriteAsyncResult.s_onWrite, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return;
					}
					this.CompleteWrite(asyncResult);
				}
				base.InvokeCallback();
			}

			// Token: 0x060041D8 RID: 16856 RVA: 0x000E3595 File Offset: 0x000E1795
			// Note: this type is marked as 'beforefieldinit'.
			static WriteAsyncResult()
			{
			}

			// Token: 0x040027ED RID: 10221
			private readonly QuotedPrintableStream _parent;

			// Token: 0x040027EE RID: 10222
			private readonly byte[] _buffer;

			// Token: 0x040027EF RID: 10223
			private readonly int _offset;

			// Token: 0x040027F0 RID: 10224
			private readonly int _count;

			// Token: 0x040027F1 RID: 10225
			private static readonly AsyncCallback s_onWrite = new AsyncCallback(QuotedPrintableStream.WriteAsyncResult.OnWrite);

			// Token: 0x040027F2 RID: 10226
			private int _written;
		}
	}
}
