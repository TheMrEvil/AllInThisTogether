using System;
using System.Collections.Specialized;

namespace System.Net
{
	// Token: 0x02000577 RID: 1399
	internal sealed class TrackingStringDictionary : StringDictionary
	{
		// Token: 0x06002D3B RID: 11579 RVA: 0x0009B158 File Offset: 0x00099358
		internal TrackingStringDictionary() : this(false)
		{
		}

		// Token: 0x06002D3C RID: 11580 RVA: 0x0009B161 File Offset: 0x00099361
		internal TrackingStringDictionary(bool isReadOnly)
		{
			this._isReadOnly = isReadOnly;
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06002D3D RID: 11581 RVA: 0x0009B170 File Offset: 0x00099370
		// (set) Token: 0x06002D3E RID: 11582 RVA: 0x0009B178 File Offset: 0x00099378
		internal bool IsChanged
		{
			get
			{
				return this._isChanged;
			}
			set
			{
				this._isChanged = value;
			}
		}

		// Token: 0x06002D3F RID: 11583 RVA: 0x0009B181 File Offset: 0x00099381
		public override void Add(string key, string value)
		{
			if (this._isReadOnly)
			{
				throw new InvalidOperationException("The collection is read-only.");
			}
			base.Add(key, value);
			this._isChanged = true;
		}

		// Token: 0x06002D40 RID: 11584 RVA: 0x0009B1A5 File Offset: 0x000993A5
		public override void Clear()
		{
			if (this._isReadOnly)
			{
				throw new InvalidOperationException("The collection is read-only.");
			}
			base.Clear();
			this._isChanged = true;
		}

		// Token: 0x06002D41 RID: 11585 RVA: 0x0009B1C7 File Offset: 0x000993C7
		public override void Remove(string key)
		{
			if (this._isReadOnly)
			{
				throw new InvalidOperationException("The collection is read-only.");
			}
			base.Remove(key);
			this._isChanged = true;
		}

		// Token: 0x17000917 RID: 2327
		public override string this[string key]
		{
			get
			{
				return base[key];
			}
			set
			{
				if (this._isReadOnly)
				{
					throw new InvalidOperationException("The collection is read-only.");
				}
				base[key] = value;
				this._isChanged = true;
			}
		}

		// Token: 0x04001898 RID: 6296
		private readonly bool _isReadOnly;

		// Token: 0x04001899 RID: 6297
		private bool _isChanged;
	}
}
