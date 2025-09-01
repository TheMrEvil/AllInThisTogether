using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x0200080D RID: 2061
	internal class QEncodedStream : DelegatedStream, IEncodableStream
	{
		// Token: 0x060041A4 RID: 16804 RVA: 0x000E263B File Offset: 0x000E083B
		internal QEncodedStream(WriteStateInfoBase wsi) : base(new MemoryStream())
		{
			this._writeState = wsi;
		}

		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x060041A5 RID: 16805 RVA: 0x000E2650 File Offset: 0x000E0850
		private QEncodedStream.ReadStateInfo ReadState
		{
			get
			{
				QEncodedStream.ReadStateInfo result;
				if ((result = this._readState) == null)
				{
					result = (this._readState = new QEncodedStream.ReadStateInfo());
				}
				return result;
			}
		}

		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x060041A6 RID: 16806 RVA: 0x000E2675 File Offset: 0x000E0875
		internal WriteStateInfoBase WriteState
		{
			get
			{
				return this._writeState;
			}
		}

		// Token: 0x060041A7 RID: 16807 RVA: 0x000E2680 File Offset: 0x000E0880
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
			QEncodedStream.WriteAsyncResult writeAsyncResult = new QEncodedStream.WriteAsyncResult(this, buffer, offset, count, callback, state);
			writeAsyncResult.Write();
			return writeAsyncResult;
		}

		// Token: 0x060041A8 RID: 16808 RVA: 0x000E26D6 File Offset: 0x000E08D6
		public override void Close()
		{
			this.FlushInternal();
			base.Close();
		}

		// Token: 0x060041A9 RID: 16809 RVA: 0x000E26E4 File Offset: 0x000E08E4
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
						byte b = QEncodedStream.s_hexDecodeMap[(int)(*ptr3)];
						byte b2 = QEncodedStream.s_hexDecodeMap[(int)ptr3[1]];
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
						byte b3 = QEncodedStream.s_hexDecodeMap[(int)this.ReadState.Byte];
						byte b4 = QEncodedStream.s_hexDecodeMap[(int)(*ptr3)];
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
								byte b5 = QEncodedStream.s_hexDecodeMap[(int)ptr3[1]];
								byte b6 = QEncodedStream.s_hexDecodeMap[(int)ptr3[2]];
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
				if (*ptr3 == 95)
				{
					*(ptr4++) = 32;
					ptr3++;
				}
				else
				{
					*(ptr4++) = *(ptr3++);
				}
			}
			return (int)((long)(ptr4 - ptr2));
		}

		// Token: 0x060041AA RID: 16810 RVA: 0x000E2960 File Offset: 0x000E0B60
		public int EncodeBytes(byte[] buffer, int offset, int count)
		{
			this._writeState.AppendHeader();
			int i;
			for (i = offset; i < count + offset; i++)
			{
				if ((this.WriteState.CurrentLineLength + 3 + this.WriteState.FooterLength >= this.WriteState.MaxLineLength && (buffer[i] == 32 || buffer[i] == 9 || buffer[i] == 13 || buffer[i] == 10)) || this.WriteState.CurrentLineLength + this._writeState.FooterLength >= this.WriteState.MaxLineLength)
				{
					this.WriteState.AppendCRLF(true);
				}
				if (buffer[i] == 13 && i + 1 < count + offset && buffer[i + 1] == 10)
				{
					i++;
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
				else if (buffer[i] == 32)
				{
					this.WriteState.Append(95);
				}
				else if (QEncodedStream.IsAsciiLetterOrDigit((char)buffer[i]))
				{
					this.WriteState.Append(buffer[i]);
				}
				else
				{
					this.WriteState.Append(61);
					this.WriteState.Append(QEncodedStream.s_hexEncodeMap[buffer[i] >> 4]);
					this.WriteState.Append(QEncodedStream.s_hexEncodeMap[(int)(buffer[i] & 15)]);
				}
			}
			this.WriteState.AppendFooter();
			return i - offset;
		}

		// Token: 0x060041AB RID: 16811 RVA: 0x000E2AB0 File Offset: 0x000E0CB0
		private static bool IsAsciiLetterOrDigit(char character)
		{
			return QEncodedStream.IsAsciiLetter(character) || (character >= '0' && character <= '9');
		}

		// Token: 0x060041AC RID: 16812 RVA: 0x00027FB4 File Offset: 0x000261B4
		private static bool IsAsciiLetter(char character)
		{
			return (character >= 'a' && character <= 'z') || (character >= 'A' && character <= 'Z');
		}

		// Token: 0x060041AD RID: 16813 RVA: 0x000075E1 File Offset: 0x000057E1
		public Stream GetStream()
		{
			return this;
		}

		// Token: 0x060041AE RID: 16814 RVA: 0x000E2ACB File Offset: 0x000E0CCB
		public string GetEncodedString()
		{
			return Encoding.ASCII.GetString(this.WriteState.Buffer, 0, this.WriteState.Length);
		}

		// Token: 0x060041AF RID: 16815 RVA: 0x000E2AEE File Offset: 0x000E0CEE
		public override void EndWrite(IAsyncResult asyncResult)
		{
			QEncodedStream.WriteAsyncResult.End(asyncResult);
		}

		// Token: 0x060041B0 RID: 16816 RVA: 0x000E2AF6 File Offset: 0x000E0CF6
		public override void Flush()
		{
			this.FlushInternal();
			base.Flush();
		}

		// Token: 0x060041B1 RID: 16817 RVA: 0x000E2B04 File Offset: 0x000E0D04
		private void FlushInternal()
		{
			if (this._writeState != null && this._writeState.Length > 0)
			{
				base.Write(this.WriteState.Buffer, 0, this.WriteState.Length);
				this.WriteState.Reset();
			}
		}

		// Token: 0x060041B2 RID: 16818 RVA: 0x000E2B44 File Offset: 0x000E0D44
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

		// Token: 0x060041B3 RID: 16819 RVA: 0x000E2BA5 File Offset: 0x000E0DA5
		// Note: this type is marked as 'beforefieldinit'.
		static QEncodedStream()
		{
		}

		// Token: 0x040027D4 RID: 10196
		private const int SizeOfFoldingCRLF = 3;

		// Token: 0x040027D5 RID: 10197
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

		// Token: 0x040027D6 RID: 10198
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

		// Token: 0x040027D7 RID: 10199
		private QEncodedStream.ReadStateInfo _readState;

		// Token: 0x040027D8 RID: 10200
		private WriteStateInfoBase _writeState;

		// Token: 0x0200080E RID: 2062
		private sealed class ReadStateInfo
		{
			// Token: 0x17000ED7 RID: 3799
			// (get) Token: 0x060041B4 RID: 16820 RVA: 0x000E2BD8 File Offset: 0x000E0DD8
			// (set) Token: 0x060041B5 RID: 16821 RVA: 0x000E2BE0 File Offset: 0x000E0DE0
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

			// Token: 0x17000ED8 RID: 3800
			// (get) Token: 0x060041B6 RID: 16822 RVA: 0x000E2BE9 File Offset: 0x000E0DE9
			// (set) Token: 0x060041B7 RID: 16823 RVA: 0x000E2BF1 File Offset: 0x000E0DF1
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

			// Token: 0x060041B8 RID: 16824 RVA: 0x000E2BFA File Offset: 0x000E0DFA
			public ReadStateInfo()
			{
			}

			// Token: 0x040027D9 RID: 10201
			[CompilerGenerated]
			private bool <IsEscaped>k__BackingField;

			// Token: 0x040027DA RID: 10202
			[CompilerGenerated]
			private short <Byte>k__BackingField;
		}

		// Token: 0x0200080F RID: 2063
		private class WriteAsyncResult : LazyAsyncResult
		{
			// Token: 0x060041B9 RID: 16825 RVA: 0x000E2C09 File Offset: 0x000E0E09
			internal WriteAsyncResult(QEncodedStream parent, byte[] buffer, int offset, int count, AsyncCallback callback, object state) : base(null, state, callback)
			{
				this._parent = parent;
				this._buffer = buffer;
				this._offset = offset;
				this._count = count;
			}

			// Token: 0x060041BA RID: 16826 RVA: 0x000E2C33 File Offset: 0x000E0E33
			private void CompleteWrite(IAsyncResult result)
			{
				this._parent.BaseStream.EndWrite(result);
				this._parent.WriteState.Reset();
			}

			// Token: 0x060041BB RID: 16827 RVA: 0x000E2C56 File Offset: 0x000E0E56
			internal static void End(IAsyncResult result)
			{
				((QEncodedStream.WriteAsyncResult)result).InternalWaitForCompletion();
			}

			// Token: 0x060041BC RID: 16828 RVA: 0x000E2C64 File Offset: 0x000E0E64
			private static void OnWrite(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					QEncodedStream.WriteAsyncResult writeAsyncResult = (QEncodedStream.WriteAsyncResult)result.AsyncState;
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

			// Token: 0x060041BD RID: 16829 RVA: 0x000E2CB0 File Offset: 0x000E0EB0
			internal void Write()
			{
				for (;;)
				{
					this._written += this._parent.EncodeBytes(this._buffer, this._offset + this._written, this._count - this._written);
					if (this._written >= this._count)
					{
						break;
					}
					IAsyncResult asyncResult = this._parent.BaseStream.BeginWrite(this._parent.WriteState.Buffer, 0, this._parent.WriteState.Length, QEncodedStream.WriteAsyncResult.s_onWrite, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return;
					}
					this.CompleteWrite(asyncResult);
				}
				base.InvokeCallback();
			}

			// Token: 0x060041BE RID: 16830 RVA: 0x000E2D55 File Offset: 0x000E0F55
			// Note: this type is marked as 'beforefieldinit'.
			static WriteAsyncResult()
			{
			}

			// Token: 0x040027DB RID: 10203
			private static readonly AsyncCallback s_onWrite = new AsyncCallback(QEncodedStream.WriteAsyncResult.OnWrite);

			// Token: 0x040027DC RID: 10204
			private readonly QEncodedStream _parent;

			// Token: 0x040027DD RID: 10205
			private readonly byte[] _buffer;

			// Token: 0x040027DE RID: 10206
			private readonly int _offset;

			// Token: 0x040027DF RID: 10207
			private readonly int _count;

			// Token: 0x040027E0 RID: 10208
			private int _written;
		}
	}
}
