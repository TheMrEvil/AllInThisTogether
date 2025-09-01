using System;
using System.Diagnostics;
using System.IO;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000695 RID: 1685
	internal sealed class SerializationHeaderRecord : IStreamable
	{
		// Token: 0x06003E15 RID: 15893 RVA: 0x000D61F7 File Offset: 0x000D43F7
		internal SerializationHeaderRecord()
		{
		}

		// Token: 0x06003E16 RID: 15894 RVA: 0x000D6206 File Offset: 0x000D4406
		internal SerializationHeaderRecord(BinaryHeaderEnum binaryHeaderEnum, int topId, int headerId, int majorVersion, int minorVersion)
		{
			this.binaryHeaderEnum = binaryHeaderEnum;
			this.topId = topId;
			this.headerId = headerId;
			this.majorVersion = majorVersion;
			this.minorVersion = minorVersion;
		}

		// Token: 0x06003E17 RID: 15895 RVA: 0x000D623C File Offset: 0x000D443C
		public void Write(__BinaryWriter sout)
		{
			this.majorVersion = this.binaryFormatterMajorVersion;
			this.minorVersion = this.binaryFormatterMinorVersion;
			sout.WriteByte((byte)this.binaryHeaderEnum);
			sout.WriteInt32(this.topId);
			sout.WriteInt32(this.headerId);
			sout.WriteInt32(this.binaryFormatterMajorVersion);
			sout.WriteInt32(this.binaryFormatterMinorVersion);
		}

		// Token: 0x06003E18 RID: 15896 RVA: 0x000BF454 File Offset: 0x000BD654
		private static int GetInt32(byte[] buffer, int index)
		{
			return (int)buffer[index] | (int)buffer[index + 1] << 8 | (int)buffer[index + 2] << 16 | (int)buffer[index + 3] << 24;
		}

		// Token: 0x06003E19 RID: 15897 RVA: 0x000D62A0 File Offset: 0x000D44A0
		[SecurityCritical]
		public void Read(__BinaryParser input)
		{
			byte[] array = input.ReadBytes(17);
			if (array.Length < 17)
			{
				__Error.EndOfFile();
			}
			this.majorVersion = SerializationHeaderRecord.GetInt32(array, 9);
			if (this.majorVersion > this.binaryFormatterMajorVersion)
			{
				throw new SerializationException(Environment.GetResourceString("The input stream is not a valid binary format. The starting contents (in bytes) are: {0} ...", new object[]
				{
					BitConverter.ToString(array)
				}));
			}
			this.binaryHeaderEnum = (BinaryHeaderEnum)array[0];
			this.topId = SerializationHeaderRecord.GetInt32(array, 1);
			this.headerId = SerializationHeaderRecord.GetInt32(array, 5);
			this.minorVersion = SerializationHeaderRecord.GetInt32(array, 13);
		}

		// Token: 0x06003E1A RID: 15898 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void Dump()
		{
		}

		// Token: 0x06003E1B RID: 15899 RVA: 0x000D6165 File Offset: 0x000D4365
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			BCLDebug.CheckEnabled("BINARY");
		}

		// Token: 0x04002836 RID: 10294
		internal int binaryFormatterMajorVersion = 1;

		// Token: 0x04002837 RID: 10295
		internal int binaryFormatterMinorVersion;

		// Token: 0x04002838 RID: 10296
		internal BinaryHeaderEnum binaryHeaderEnum;

		// Token: 0x04002839 RID: 10297
		internal int topId;

		// Token: 0x0400283A RID: 10298
		internal int headerId;

		// Token: 0x0400283B RID: 10299
		internal int majorVersion;

		// Token: 0x0400283C RID: 10300
		internal int minorVersion;
	}
}
