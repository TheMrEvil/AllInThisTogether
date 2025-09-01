using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace WebSocketSharp
{
	// Token: 0x0200000C RID: 12
	internal class PayloadData : IEnumerable<byte>, IEnumerable
	{
		// Token: 0x06000104 RID: 260 RVA: 0x00008C09 File Offset: 0x00006E09
		static PayloadData()
		{
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00008C2B File Offset: 0x00006E2B
		internal PayloadData(byte[] data) : this(data, (long)data.Length)
		{
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00008C39 File Offset: 0x00006E39
		internal PayloadData(byte[] data, long length)
		{
			this._data = data;
			this._length = length;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00008C51 File Offset: 0x00006E51
		internal PayloadData(ushort code, string reason)
		{
			this._data = code.Append(reason);
			this._length = (long)this._data.Length;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00008C78 File Offset: 0x00006E78
		internal ushort Code
		{
			get
			{
				return (this._length >= 2L) ? this._data.SubArray(0, 2).ToUInt16(ByteOrder.Big) : 1005;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00008CB0 File Offset: 0x00006EB0
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00008CC8 File Offset: 0x00006EC8
		internal long ExtensionDataLength
		{
			get
			{
				return this._extDataLength;
			}
			set
			{
				this._extDataLength = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00008CD4 File Offset: 0x00006ED4
		internal bool HasReservedCode
		{
			get
			{
				return this._length >= 2L && this.Code.IsReserved();
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00008D00 File Offset: 0x00006F00
		internal string Reason
		{
			get
			{
				bool flag = this._length <= 2L;
				string result;
				if (flag)
				{
					result = string.Empty;
				}
				else
				{
					byte[] bytes = this._data.SubArray(2L, this._length - 2L);
					string text;
					result = (bytes.TryGetUTF8DecodedString(out text) ? text : string.Empty);
				}
				return result;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00008D54 File Offset: 0x00006F54
		public byte[] ApplicationData
		{
			get
			{
				return (this._extDataLength > 0L) ? this._data.SubArray(this._extDataLength, this._length - this._extDataLength) : this._data;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00008D98 File Offset: 0x00006F98
		public byte[] ExtensionData
		{
			get
			{
				return (this._extDataLength > 0L) ? this._data.SubArray(0L, this._extDataLength) : WebSocket.EmptyBytes;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00008DD0 File Offset: 0x00006FD0
		public ulong Length
		{
			get
			{
				return (ulong)this._length;
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00008DE8 File Offset: 0x00006FE8
		internal void Mask(byte[] key)
		{
			for (long num = 0L; num < this._length; num += 1L)
			{
				checked
				{
					this._data[(int)((IntPtr)num)] = (this._data[(int)((IntPtr)num)] ^ key[(int)((IntPtr)(num % 4L))]);
				}
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00008E28 File Offset: 0x00007028
		public IEnumerator<byte> GetEnumerator()
		{
			foreach (byte b in this._data)
			{
				yield return b;
			}
			byte[] array = null;
			yield break;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00008E38 File Offset: 0x00007038
		public byte[] ToArray()
		{
			return this._data;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00008E50 File Offset: 0x00007050
		public override string ToString()
		{
			return BitConverter.ToString(this._data);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00008E70 File Offset: 0x00007070
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000060 RID: 96
		private byte[] _data;

		// Token: 0x04000061 RID: 97
		private long _extDataLength;

		// Token: 0x04000062 RID: 98
		private long _length;

		// Token: 0x04000063 RID: 99
		public static readonly PayloadData Empty = new PayloadData(WebSocket.EmptyBytes, 0L);

		// Token: 0x04000064 RID: 100
		public static readonly ulong MaxLength = 9223372036854775807UL;

		// Token: 0x02000062 RID: 98
		[CompilerGenerated]
		private sealed class <GetEnumerator>d__25 : IEnumerator<byte>, IDisposable, IEnumerator
		{
			// Token: 0x06000594 RID: 1428 RVA: 0x0001E84E File Offset: 0x0001CA4E
			[DebuggerHidden]
			public <GetEnumerator>d__25(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06000595 RID: 1429 RVA: 0x0001DF38 File Offset: 0x0001C138
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06000596 RID: 1430 RVA: 0x0001E860 File Offset: 0x0001CA60
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					i++;
				}
				else
				{
					this.<>1__state = -1;
					array = this._data;
					i = 0;
				}
				if (i >= array.Length)
				{
					array = null;
					return false;
				}
				b = array[i];
				this.<>2__current = b;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x170001AA RID: 426
			// (get) Token: 0x06000597 RID: 1431 RVA: 0x0001E8FD File Offset: 0x0001CAFD
			byte IEnumerator<byte>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06000598 RID: 1432 RVA: 0x0001E136 File Offset: 0x0001C336
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170001AB RID: 427
			// (get) Token: 0x06000599 RID: 1433 RVA: 0x0001E905 File Offset: 0x0001CB05
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x040002AE RID: 686
			private int <>1__state;

			// Token: 0x040002AF RID: 687
			private byte <>2__current;

			// Token: 0x040002B0 RID: 688
			public PayloadData <>4__this;

			// Token: 0x040002B1 RID: 689
			private byte[] <>s__1;

			// Token: 0x040002B2 RID: 690
			private int <>s__2;

			// Token: 0x040002B3 RID: 691
			private byte <b>5__3;
		}
	}
}
