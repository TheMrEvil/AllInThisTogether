using System;
using System.Text.RegularExpressions;

namespace System.Net
{
	// Token: 0x02000610 RID: 1552
	[Serializable]
	internal class DelayedRegex
	{
		// Token: 0x06003124 RID: 12580 RVA: 0x000A9A88 File Offset: 0x000A7C88
		internal DelayedRegex(string regexString)
		{
			if (regexString == null)
			{
				throw new ArgumentNullException("regexString");
			}
			this._AsString = regexString;
		}

		// Token: 0x06003125 RID: 12581 RVA: 0x000A9AA5 File Offset: 0x000A7CA5
		internal DelayedRegex(Regex regex)
		{
			if (regex == null)
			{
				throw new ArgumentNullException("regex");
			}
			this._AsRegex = regex;
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06003126 RID: 12582 RVA: 0x000A9AC2 File Offset: 0x000A7CC2
		internal Regex AsRegex
		{
			get
			{
				if (this._AsRegex == null)
				{
					this._AsRegex = new Regex(this._AsString + "[/]?", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.CultureInvariant);
				}
				return this._AsRegex;
			}
		}

		// Token: 0x06003127 RID: 12583 RVA: 0x000A9AF4 File Offset: 0x000A7CF4
		public override string ToString()
		{
			if (this._AsString == null)
			{
				return this._AsString = this._AsRegex.ToString();
			}
			return this._AsString;
		}

		// Token: 0x04001C98 RID: 7320
		private Regex _AsRegex;

		// Token: 0x04001C99 RID: 7321
		private string _AsString;
	}
}
