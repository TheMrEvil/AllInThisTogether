using System;

namespace System.Data.Common
{
	// Token: 0x02000371 RID: 881
	[Serializable]
	internal sealed class NameValuePair
	{
		// Token: 0x06002930 RID: 10544 RVA: 0x000B4C0C File Offset: 0x000B2E0C
		internal NameValuePair(string name, string value, int length)
		{
			this._name = name;
			this._value = value;
			this._length = length;
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06002931 RID: 10545 RVA: 0x000B4C29 File Offset: 0x000B2E29
		internal int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06002932 RID: 10546 RVA: 0x000B4C31 File Offset: 0x000B2E31
		internal string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06002933 RID: 10547 RVA: 0x000B4C39 File Offset: 0x000B2E39
		internal string Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06002934 RID: 10548 RVA: 0x000B4C41 File Offset: 0x000B2E41
		// (set) Token: 0x06002935 RID: 10549 RVA: 0x000B4C49 File Offset: 0x000B2E49
		internal NameValuePair Next
		{
			get
			{
				return this._next;
			}
			set
			{
				if (this._next != null || value == null)
				{
					throw ADP.InternalError(ADP.InternalErrorCode.NameValuePairNext);
				}
				this._next = value;
			}
		}

		// Token: 0x04001A5E RID: 6750
		private readonly string _name;

		// Token: 0x04001A5F RID: 6751
		private readonly string _value;

		// Token: 0x04001A60 RID: 6752
		private readonly int _length;

		// Token: 0x04001A61 RID: 6753
		private NameValuePair _next;
	}
}
