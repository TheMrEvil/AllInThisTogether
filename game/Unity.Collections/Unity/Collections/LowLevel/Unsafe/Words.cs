using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200010B RID: 267
	[Obsolete("This storage will no longer be used. (RemovedAfter 2021-06-01)")]
	public struct Words
	{
		// Token: 0x060009E7 RID: 2535 RVA: 0x0001DE43 File Offset: 0x0001C043
		public void ToFixedString<T>(ref T value) where T : IUTF8Bytes, INativeList<byte>
		{
			WordStorage.Instance.GetFixedString<T>(this.Index, ref value);
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0001DE58 File Offset: 0x0001C058
		public override string ToString()
		{
			FixedString512Bytes fixedString512Bytes = default(FixedString512Bytes);
			this.ToFixedString<FixedString512Bytes>(ref fixedString512Bytes);
			return fixedString512Bytes.ToString();
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0001DE82 File Offset: 0x0001C082
		public void SetFixedString<T>(ref T value) where T : IUTF8Bytes, INativeList<byte>
		{
			this.Index = WordStorage.Instance.GetOrCreateIndex<T>(ref value);
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0001DE98 File Offset: 0x0001C098
		public void SetString(string value)
		{
			FixedString512Bytes fixedString512Bytes = value;
			this.SetFixedString<FixedString512Bytes>(ref fixedString512Bytes);
		}

		// Token: 0x0400034B RID: 843
		private int Index;
	}
}
