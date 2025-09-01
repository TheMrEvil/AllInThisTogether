using System;
using System.Runtime.InteropServices;
using Unity;

namespace System.Globalization
{
	/// <summary>Represents the result of mapping a string to its sort key.</summary>
	// Token: 0x0200099D RID: 2461
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class SortKey
	{
		/// <summary>Compares two sort keys.</summary>
		/// <param name="sortkey1">The first sort key to compare.</param>
		/// <param name="sortkey2">The second sort key to compare.</param>
		/// <returns>A signed integer that indicates the relationship between <paramref name="sortkey1" /> and <paramref name="sortkey2" />.  
		///   Value  
		///
		///   Condition  
		///
		///   Less than zero  
		///
		///  <paramref name="sortkey1" /> is less than <paramref name="sortkey2" />.  
		///
		///   Zero  
		///
		///  <paramref name="sortkey1" /> is equal to <paramref name="sortkey2" />.  
		///
		///   Greater than zero  
		///
		///  <paramref name="sortkey1" /> is greater than <paramref name="sortkey2" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sortkey1" /> or <paramref name="sortkey2" /> is <see langword="null" />.</exception>
		// Token: 0x06005899 RID: 22681 RVA: 0x0012AA70 File Offset: 0x00128C70
		public static int Compare(SortKey sortkey1, SortKey sortkey2)
		{
			if (sortkey1 == null)
			{
				throw new ArgumentNullException("sortkey1");
			}
			if (sortkey2 == null)
			{
				throw new ArgumentNullException("sortkey2");
			}
			if (sortkey1 == sortkey2 || sortkey1.OriginalString == sortkey2.OriginalString)
			{
				return 0;
			}
			byte[] keyData = sortkey1.KeyData;
			byte[] keyData2 = sortkey2.KeyData;
			int num = (keyData.Length > keyData2.Length) ? keyData2.Length : keyData.Length;
			int i = 0;
			while (i < num)
			{
				if (keyData[i] != keyData2[i])
				{
					if (keyData[i] >= keyData2[i])
					{
						return 1;
					}
					return -1;
				}
				else
				{
					i++;
				}
			}
			if (keyData.Length == keyData2.Length)
			{
				return 0;
			}
			if (keyData.Length >= keyData2.Length)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x0600589A RID: 22682 RVA: 0x0012AB04 File Offset: 0x00128D04
		internal SortKey(int lcid, string source, CompareOptions opt)
		{
			this.lcid = lcid;
			this.source = source;
			this.options = opt;
			int length = source.Length;
			byte[] array = new byte[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = (byte)source[i];
			}
			this.key = array;
		}

		// Token: 0x0600589B RID: 22683 RVA: 0x0012AB58 File Offset: 0x00128D58
		internal SortKey(int lcid, string source, byte[] buffer, CompareOptions opt, int lv1Length, int lv2Length, int lv3Length, int kanaSmallLength, int markTypeLength, int katakanaLength, int kanaWidthLength, int identLength)
		{
			this.lcid = lcid;
			this.source = source;
			this.key = buffer;
			this.options = opt;
		}

		// Token: 0x0600589C RID: 22684 RVA: 0x0012AB7D File Offset: 0x00128D7D
		internal SortKey(string localeName, string str, CompareOptions options, byte[] keyData)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the original string used to create the current <see cref="T:System.Globalization.SortKey" /> object.</summary>
		/// <returns>The original string used to create the current <see cref="T:System.Globalization.SortKey" /> object.</returns>
		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x0600589D RID: 22685 RVA: 0x0012AB8A File Offset: 0x00128D8A
		public virtual string OriginalString
		{
			get
			{
				return this.source;
			}
		}

		/// <summary>Gets the byte array representing the current <see cref="T:System.Globalization.SortKey" /> object.</summary>
		/// <returns>A byte array representing the current <see cref="T:System.Globalization.SortKey" /> object.</returns>
		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x0600589E RID: 22686 RVA: 0x0012AB92 File Offset: 0x00128D92
		public virtual byte[] KeyData
		{
			get
			{
				return this.key;
			}
		}

		/// <summary>Determines whether the specified object is equal to the current <see cref="T:System.Globalization.SortKey" /> object.</summary>
		/// <param name="value">The object to compare with the current <see cref="T:System.Globalization.SortKey" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="value" /> parameter is equal to the current <see cref="T:System.Globalization.SortKey" /> object; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x0600589F RID: 22687 RVA: 0x0012AB9C File Offset: 0x00128D9C
		public override bool Equals(object value)
		{
			SortKey sortKey = value as SortKey;
			return sortKey != null && this.lcid == sortKey.lcid && this.options == sortKey.options && SortKey.Compare(this, sortKey) == 0;
		}

		/// <summary>Serves as a hash function for the current <see cref="T:System.Globalization.SortKey" /> object that is suitable for hashing algorithms and data structures such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Globalization.SortKey" /> object.</returns>
		// Token: 0x060058A0 RID: 22688 RVA: 0x0012ABDC File Offset: 0x00128DDC
		public override int GetHashCode()
		{
			if (this.key.Length == 0)
			{
				return 0;
			}
			int num = (int)this.key[0];
			for (int i = 1; i < this.key.Length; i++)
			{
				num ^= (int)this.key[i] << (i & 3);
			}
			return num;
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Globalization.SortKey" /> object.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Globalization.SortKey" /> object.</returns>
		// Token: 0x060058A1 RID: 22689 RVA: 0x0012AC24 File Offset: 0x00128E24
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"SortKey - ",
				this.lcid.ToString(),
				", ",
				this.options.ToString(),
				", ",
				this.source
			});
		}

		// Token: 0x060058A2 RID: 22690 RVA: 0x000173AD File Offset: 0x000155AD
		internal SortKey()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040036C7 RID: 14023
		private readonly string source;

		// Token: 0x040036C8 RID: 14024
		private readonly byte[] key;

		// Token: 0x040036C9 RID: 14025
		private readonly CompareOptions options;

		// Token: 0x040036CA RID: 14026
		private readonly int lcid;
	}
}
