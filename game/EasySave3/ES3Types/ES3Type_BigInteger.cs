using System;
using System.Numerics;
using UnityEngine.Scripting;

namespace ES3Types
{
	// Token: 0x0200002F RID: 47
	[Preserve]
	[ES3Properties(new string[]
	{
		"bytes"
	})]
	public class ES3Type_BigInteger : ES3Type
	{
		// Token: 0x0600026F RID: 623 RVA: 0x000099FC File Offset: 0x00007BFC
		public ES3Type_BigInteger() : base(typeof(BigInteger))
		{
			ES3Type_BigInteger.Instance = this;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00009A14 File Offset: 0x00007C14
		public override void Write(object obj, ES3Writer writer)
		{
			writer.WriteProperty("bytes", ((BigInteger)obj).ToByteArray(), ES3Type_byteArray.Instance);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00009A3F File Offset: 0x00007C3F
		public override object Read<T>(ES3Reader reader)
		{
			return new BigInteger(reader.ReadProperty<byte[]>(ES3Type_byteArray.Instance));
		}

		// Token: 0x04000080 RID: 128
		public static ES3Type Instance;
	}
}
