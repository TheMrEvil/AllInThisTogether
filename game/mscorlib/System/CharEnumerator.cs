using System;
using System.Collections;
using System.Collections.Generic;
using Unity;

namespace System
{
	/// <summary>Supports iterating over a <see cref="T:System.String" /> object and reading its individual characters. This class cannot be inherited.</summary>
	// Token: 0x02000106 RID: 262
	[Serializable]
	public sealed class CharEnumerator : IEnumerator, IEnumerator<char>, IDisposable, ICloneable
	{
		// Token: 0x0600082E RID: 2094 RVA: 0x000231B7 File Offset: 0x000213B7
		internal CharEnumerator(string str)
		{
			this._str = str;
			this._index = -1;
		}

		/// <summary>Creates a copy of the current <see cref="T:System.CharEnumerator" /> object.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is a copy of the current <see cref="T:System.CharEnumerator" /> object.</returns>
		// Token: 0x0600082F RID: 2095 RVA: 0x000231CD File Offset: 0x000213CD
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		/// <summary>Increments the internal index of the current <see cref="T:System.CharEnumerator" /> object to the next character of the enumerated string.</summary>
		/// <returns>
		///   <see langword="true" /> if the index is successfully incremented and within the enumerated string; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000830 RID: 2096 RVA: 0x000231D8 File Offset: 0x000213D8
		public bool MoveNext()
		{
			if (this._index < this._str.Length - 1)
			{
				this._index++;
				this._currentElement = this._str[this._index];
				return true;
			}
			this._index = this._str.Length;
			return false;
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.CharEnumerator" /> class.</summary>
		// Token: 0x06000831 RID: 2097 RVA: 0x00023233 File Offset: 0x00021433
		public void Dispose()
		{
			if (this._str != null)
			{
				this._index = this._str.Length;
			}
			this._str = null;
		}

		/// <summary>Gets the currently referenced character in the string enumerated by this <see cref="T:System.CharEnumerator" /> object. For a description of this member, see <see cref="P:System.Collections.IEnumerator.Current" />.</summary>
		/// <returns>The boxed Unicode character currently referenced by this <see cref="T:System.CharEnumerator" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">Enumeration has not started.  
		///  -or-  
		///  Enumeration has ended.</exception>
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x00023255 File Offset: 0x00021455
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		/// <summary>Gets the currently referenced character in the string enumerated by this <see cref="T:System.CharEnumerator" /> object.</summary>
		/// <returns>The Unicode character currently referenced by this <see cref="T:System.CharEnumerator" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The index is invalid; that is, it is before the first or after the last character of the enumerated string.</exception>
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000833 RID: 2099 RVA: 0x00023262 File Offset: 0x00021462
		public char Current
		{
			get
			{
				if (this._index == -1)
				{
					throw new InvalidOperationException("Enumeration has not started. Call MoveNext.");
				}
				if (this._index >= this._str.Length)
				{
					throw new InvalidOperationException("Enumeration already finished.");
				}
				return this._currentElement;
			}
		}

		/// <summary>Initializes the index to a position logically before the first character of the enumerated string.</summary>
		// Token: 0x06000834 RID: 2100 RVA: 0x0002329C File Offset: 0x0002149C
		public void Reset()
		{
			this._currentElement = '\0';
			this._index = -1;
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x000173AD File Offset: 0x000155AD
		internal CharEnumerator()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001081 RID: 4225
		private string _str;

		// Token: 0x04001082 RID: 4226
		private int _index;

		// Token: 0x04001083 RID: 4227
		private char _currentElement;
	}
}
