using System;
using System.Collections.Specialized;

namespace System.Configuration
{
	/// <summary>Represents a collection of string elements separated by commas. This class cannot be inherited.</summary>
	// Token: 0x0200000F RID: 15
	public sealed class CommaDelimitedStringCollection : StringCollection
	{
		/// <summary>Gets a value that specifies whether the collection has been modified.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Configuration.CommaDelimitedStringCollection" /> has been modified; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000023C0 File Offset: 0x000005C0
		public bool IsModified
		{
			get
			{
				if (this.modified)
				{
					return true;
				}
				string text = this.ToString();
				return text != null && text.GetHashCode() != this.originalStringHash;
			}
		}

		/// <summary>Gets a value indicating whether the collection object is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the specified string element in the <see cref="T:System.Configuration.CommaDelimitedStringCollection" /> is read-only; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000023F4 File Offset: 0x000005F4
		public new bool IsReadOnly
		{
			get
			{
				return this.readOnly;
			}
		}

		/// <summary>Gets or sets a string element in the collection based on the index.</summary>
		/// <param name="index">The index of the string element in the collection.</param>
		/// <returns>A string element in the collection.</returns>
		// Token: 0x1700000D RID: 13
		public new string this[int index]
		{
			get
			{
				return base[index];
			}
			set
			{
				if (this.readOnly)
				{
					throw new ConfigurationErrorsException("The configuration is read only");
				}
				base[index] = value;
				this.modified = true;
			}
		}

		/// <summary>Adds a string to the comma-delimited collection.</summary>
		/// <param name="value">A string value.</param>
		// Token: 0x0600002C RID: 44 RVA: 0x00002429 File Offset: 0x00000629
		public new void Add(string value)
		{
			if (this.readOnly)
			{
				throw new ConfigurationErrorsException("The configuration is read only");
			}
			base.Add(value);
			this.modified = true;
		}

		/// <summary>Adds all the strings in a string array to the collection.</summary>
		/// <param name="range">An array of strings to add to the collection.</param>
		// Token: 0x0600002D RID: 45 RVA: 0x0000244D File Offset: 0x0000064D
		public new void AddRange(string[] range)
		{
			if (this.readOnly)
			{
				throw new ConfigurationErrorsException("The configuration is read only");
			}
			base.AddRange(range);
			this.modified = true;
		}

		/// <summary>Clears the collection.</summary>
		// Token: 0x0600002E RID: 46 RVA: 0x00002470 File Offset: 0x00000670
		public new void Clear()
		{
			if (this.readOnly)
			{
				throw new ConfigurationErrorsException("The configuration is read only");
			}
			base.Clear();
			this.modified = true;
		}

		/// <summary>Creates a copy of the collection.</summary>
		/// <returns>A copy of the <see cref="T:System.Configuration.CommaDelimitedStringCollection" />.</returns>
		// Token: 0x0600002F RID: 47 RVA: 0x00002494 File Offset: 0x00000694
		public CommaDelimitedStringCollection Clone()
		{
			CommaDelimitedStringCollection commaDelimitedStringCollection = new CommaDelimitedStringCollection();
			string[] array = new string[base.Count];
			base.CopyTo(array, 0);
			commaDelimitedStringCollection.AddRange(array);
			commaDelimitedStringCollection.originalStringHash = this.originalStringHash;
			return commaDelimitedStringCollection;
		}

		/// <summary>Adds a string element to the collection at the specified index.</summary>
		/// <param name="index">The index in the collection at which the new element will be added.</param>
		/// <param name="value">The value of the new element to add to the collection.</param>
		// Token: 0x06000030 RID: 48 RVA: 0x000024CD File Offset: 0x000006CD
		public new void Insert(int index, string value)
		{
			if (this.readOnly)
			{
				throw new ConfigurationErrorsException("The configuration is read only");
			}
			base.Insert(index, value);
			this.modified = true;
		}

		/// <summary>Removes a string element from the collection.</summary>
		/// <param name="value">The string to remove.</param>
		// Token: 0x06000031 RID: 49 RVA: 0x000024F1 File Offset: 0x000006F1
		public new void Remove(string value)
		{
			if (this.readOnly)
			{
				throw new ConfigurationErrorsException("The configuration is read only");
			}
			base.Remove(value);
			this.modified = true;
		}

		/// <summary>Sets the collection object to read-only.</summary>
		// Token: 0x06000032 RID: 50 RVA: 0x00002514 File Offset: 0x00000714
		public void SetReadOnly()
		{
			this.readOnly = true;
		}

		/// <summary>Returns a string representation of the object.</summary>
		/// <returns>A string representation of the object.</returns>
		// Token: 0x06000033 RID: 51 RVA: 0x00002520 File Offset: 0x00000720
		public override string ToString()
		{
			if (base.Count == 0)
			{
				return null;
			}
			string[] array = new string[base.Count];
			base.CopyTo(array, 0);
			return string.Join(",", array);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002558 File Offset: 0x00000758
		internal void UpdateStringHash()
		{
			string text = this.ToString();
			if (text == null)
			{
				this.originalStringHash = 0;
				return;
			}
			this.originalStringHash = text.GetHashCode();
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Configuration.CommaDelimitedStringCollection" /> class.</summary>
		// Token: 0x06000035 RID: 53 RVA: 0x00002583 File Offset: 0x00000783
		public CommaDelimitedStringCollection()
		{
		}

		// Token: 0x04000035 RID: 53
		private bool modified;

		// Token: 0x04000036 RID: 54
		private bool readOnly;

		// Token: 0x04000037 RID: 55
		private int originalStringHash;
	}
}
