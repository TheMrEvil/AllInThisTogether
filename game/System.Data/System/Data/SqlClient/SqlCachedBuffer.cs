using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml;

namespace System.Data.SqlClient
{
	// Token: 0x020001AD RID: 429
	internal sealed class SqlCachedBuffer : INullable
	{
		// Token: 0x06001516 RID: 5398 RVA: 0x00003D93 File Offset: 0x00001F93
		private SqlCachedBuffer()
		{
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x000606D8 File Offset: 0x0005E8D8
		private SqlCachedBuffer(List<byte[]> cachedBytes)
		{
			this._cachedBytes = cachedBytes;
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001518 RID: 5400 RVA: 0x000606E7 File Offset: 0x0005E8E7
		internal List<byte[]> CachedBytes
		{
			get
			{
				return this._cachedBytes;
			}
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x000606F0 File Offset: 0x0005E8F0
		internal static bool TryCreate(SqlMetaDataPriv metadata, TdsParser parser, TdsParserStateObject stateObj, out SqlCachedBuffer buffer)
		{
			int num = 0;
			List<byte[]> list = new List<byte[]>();
			buffer = null;
			ulong num2;
			if (!parser.TryPlpBytesLeft(stateObj, out num2))
			{
				return false;
			}
			while (num2 != 0UL)
			{
				do
				{
					num = ((num2 > 2048UL) ? 2048 : ((int)num2));
					byte[] array = new byte[num];
					if (!stateObj.TryReadPlpBytes(ref array, 0, num, out num))
					{
						return false;
					}
					if (list.Count == 0)
					{
						SqlCachedBuffer.AddByteOrderMark(array, list);
					}
					list.Add(array);
					num2 -= (ulong)((long)num);
				}
				while (num2 > 0UL);
				if (!parser.TryPlpBytesLeft(stateObj, out num2))
				{
					return false;
				}
				if (num2 <= 0UL)
				{
					break;
				}
			}
			buffer = new SqlCachedBuffer(list);
			return true;
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x0006077D File Offset: 0x0005E97D
		private static void AddByteOrderMark(byte[] byteArr, List<byte[]> cachedBytes)
		{
			if (byteArr.Length < 2 || byteArr[0] != 223 || byteArr[1] != 255)
			{
				cachedBytes.Add(TdsEnums.XMLUNICODEBOMBYTES);
			}
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x000607A4 File Offset: 0x0005E9A4
		internal Stream ToStream()
		{
			return new SqlCachedStream(this);
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x000607AC File Offset: 0x0005E9AC
		public override string ToString()
		{
			if (this.IsNull)
			{
				throw new SqlNullValueException();
			}
			if (this._cachedBytes.Count == 0)
			{
				return string.Empty;
			}
			return new SqlXml(this.ToStream()).Value;
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x000607DF File Offset: 0x0005E9DF
		internal SqlString ToSqlString()
		{
			if (this.IsNull)
			{
				return SqlString.Null;
			}
			return new SqlString(this.ToString());
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x000607FA File Offset: 0x0005E9FA
		internal SqlXml ToSqlXml()
		{
			return new SqlXml(this.ToStream());
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x00060807 File Offset: 0x0005EA07
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal XmlReader ToXmlReader()
		{
			return SqlTypeWorkarounds.SqlXmlCreateSqlXmlReader(this.ToStream(), false, false);
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06001520 RID: 5408 RVA: 0x00060816 File Offset: 0x0005EA16
		public bool IsNull
		{
			get
			{
				return this._cachedBytes == null;
			}
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x00060823 File Offset: 0x0005EA23
		// Note: this type is marked as 'beforefieldinit'.
		static SqlCachedBuffer()
		{
		}

		// Token: 0x04000D6E RID: 3438
		public static readonly SqlCachedBuffer Null = new SqlCachedBuffer();

		// Token: 0x04000D6F RID: 3439
		private const int _maxChunkSize = 2048;

		// Token: 0x04000D70 RID: 3440
		private List<byte[]> _cachedBytes;
	}
}
