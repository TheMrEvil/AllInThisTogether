using System;
using System.Threading;

namespace System.Text.RegularExpressions
{
	// Token: 0x020001F4 RID: 500
	internal sealed class ExclusiveReference
	{
		// Token: 0x06000D5C RID: 3420 RVA: 0x00036518 File Offset: 0x00034718
		public RegexRunner Get()
		{
			if (Interlocked.Exchange(ref this._locked, 1) != 0)
			{
				return null;
			}
			RegexRunner @ref = this._ref;
			if (@ref == null)
			{
				this._locked = 0;
				return null;
			}
			this._obj = @ref;
			return @ref;
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x00036554 File Offset: 0x00034754
		public void Release(RegexRunner obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (this._obj == obj)
			{
				this._obj = null;
				this._locked = 0;
				return;
			}
			if (this._obj == null && Interlocked.Exchange(ref this._locked, 1) == 0)
			{
				if (this._ref == null)
				{
					this._ref = obj;
				}
				this._locked = 0;
				return;
			}
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x0000219B File Offset: 0x0000039B
		public ExclusiveReference()
		{
		}

		// Token: 0x04000800 RID: 2048
		private RegexRunner _ref;

		// Token: 0x04000801 RID: 2049
		private RegexRunner _obj;

		// Token: 0x04000802 RID: 2050
		private volatile int _locked;
	}
}
