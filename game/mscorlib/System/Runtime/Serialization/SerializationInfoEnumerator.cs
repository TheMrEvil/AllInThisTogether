using System;
using System.Collections;
using Unity;

namespace System.Runtime.Serialization
{
	/// <summary>Provides a formatter-friendly mechanism for parsing the data in <see cref="T:System.Runtime.Serialization.SerializationInfo" />. This class cannot be inherited.</summary>
	// Token: 0x0200064D RID: 1613
	public sealed class SerializationInfoEnumerator : IEnumerator
	{
		// Token: 0x06003C58 RID: 15448 RVA: 0x000D14DF File Offset: 0x000CF6DF
		internal SerializationInfoEnumerator(string[] members, object[] info, Type[] types, int numItems)
		{
			this._members = members;
			this._data = info;
			this._types = types;
			this._numItems = numItems - 1;
			this._currItem = -1;
			this._current = false;
		}

		/// <summary>Updates the enumerator to the next item.</summary>
		/// <returns>
		///   <see langword="true" /> if a new element is found; otherwise, <see langword="false" />.</returns>
		// Token: 0x06003C59 RID: 15449 RVA: 0x000D1514 File Offset: 0x000CF714
		public bool MoveNext()
		{
			if (this._currItem < this._numItems)
			{
				this._currItem++;
				this._current = true;
			}
			else
			{
				this._current = false;
			}
			return this._current;
		}

		/// <summary>Gets the current item in the collection.</summary>
		/// <returns>A <see cref="T:System.Runtime.Serialization.SerializationEntry" /> that contains the current serialization data.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumeration has not started or has already ended.</exception>
		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06003C5A RID: 15450 RVA: 0x000D1548 File Offset: 0x000CF748
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		/// <summary>Gets the item currently being examined.</summary>
		/// <returns>The item currently being examined.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator has not started enumerating items or has reached the end of the enumeration.</exception>
		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06003C5B RID: 15451 RVA: 0x000D1558 File Offset: 0x000CF758
		public SerializationEntry Current
		{
			get
			{
				if (!this._current)
				{
					throw new InvalidOperationException("Enumeration has either not started or has already finished.");
				}
				return new SerializationEntry(this._members[this._currItem], this._data[this._currItem], this._types[this._currItem]);
			}
		}

		/// <summary>Resets the enumerator to the first item.</summary>
		// Token: 0x06003C5C RID: 15452 RVA: 0x000D15A4 File Offset: 0x000CF7A4
		public void Reset()
		{
			this._currItem = -1;
			this._current = false;
		}

		/// <summary>Gets the name for the item currently being examined.</summary>
		/// <returns>The item name.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator has not started enumerating items or has reached the end of the enumeration.</exception>
		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06003C5D RID: 15453 RVA: 0x000D15B4 File Offset: 0x000CF7B4
		public string Name
		{
			get
			{
				if (!this._current)
				{
					throw new InvalidOperationException("Enumeration has either not started or has already finished.");
				}
				return this._members[this._currItem];
			}
		}

		/// <summary>Gets the value of the item currently being examined.</summary>
		/// <returns>The value of the item currently being examined.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator has not started enumerating items or has reached the end of the enumeration.</exception>
		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06003C5E RID: 15454 RVA: 0x000D15D6 File Offset: 0x000CF7D6
		public object Value
		{
			get
			{
				if (!this._current)
				{
					throw new InvalidOperationException("Enumeration has either not started or has already finished.");
				}
				return this._data[this._currItem];
			}
		}

		/// <summary>Gets the type of the item currently being examined.</summary>
		/// <returns>The type of the item currently being examined.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator has not started enumerating items or has reached the end of the enumeration.</exception>
		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06003C5F RID: 15455 RVA: 0x000D15F8 File Offset: 0x000CF7F8
		public Type ObjectType
		{
			get
			{
				if (!this._current)
				{
					throw new InvalidOperationException("Enumeration has either not started or has already finished.");
				}
				return this._types[this._currItem];
			}
		}

		// Token: 0x06003C60 RID: 15456 RVA: 0x000173AD File Offset: 0x000155AD
		internal SerializationInfoEnumerator()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04002719 RID: 10009
		private readonly string[] _members;

		// Token: 0x0400271A RID: 10010
		private readonly object[] _data;

		// Token: 0x0400271B RID: 10011
		private readonly Type[] _types;

		// Token: 0x0400271C RID: 10012
		private readonly int _numItems;

		// Token: 0x0400271D RID: 10013
		private int _currItem;

		// Token: 0x0400271E RID: 10014
		private bool _current;
	}
}
