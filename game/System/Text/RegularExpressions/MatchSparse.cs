using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x020001F1 RID: 497
	internal class MatchSparse : Match
	{
		// Token: 0x06000D34 RID: 3380 RVA: 0x0003625A File Offset: 0x0003445A
		internal MatchSparse(Regex regex, Hashtable caps, int capcount, string text, int begpos, int len, int startpos) : base(regex, capcount, text, begpos, len, startpos)
		{
			this._caps = caps;
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000D35 RID: 3381 RVA: 0x00036273 File Offset: 0x00034473
		public override GroupCollection Groups
		{
			get
			{
				if (this._groupcoll == null)
				{
					this._groupcoll = new GroupCollection(this, this._caps);
				}
				return this._groupcoll;
			}
		}

		// Token: 0x040007F5 RID: 2037
		internal new readonly Hashtable _caps;
	}
}
