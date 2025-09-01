using System;

namespace System.Resources
{
	// Token: 0x0200086B RID: 2155
	internal struct ResourceLocator
	{
		// Token: 0x060047B8 RID: 18360 RVA: 0x000EA54A File Offset: 0x000E874A
		internal ResourceLocator(int dataPos, object value)
		{
			this._dataPos = dataPos;
			this._value = value;
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x060047B9 RID: 18361 RVA: 0x000EA55A File Offset: 0x000E875A
		internal int DataPosition
		{
			get
			{
				return this._dataPos;
			}
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x060047BA RID: 18362 RVA: 0x000EA562 File Offset: 0x000E8762
		// (set) Token: 0x060047BB RID: 18363 RVA: 0x000EA56A File Offset: 0x000E876A
		internal object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x060047BC RID: 18364 RVA: 0x000EA573 File Offset: 0x000E8773
		internal static bool CanCache(ResourceTypeCode value)
		{
			return value <= ResourceTypeCode.TimeSpan;
		}

		// Token: 0x04002DEC RID: 11756
		internal object _value;

		// Token: 0x04002DED RID: 11757
		internal int _dataPos;
	}
}
