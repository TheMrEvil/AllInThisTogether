using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System.Xml
{
	/// <summary>Enables using a dynamic dictionary to compress common strings that appear in a message and maintain state.</summary>
	// Token: 0x02000051 RID: 81
	public class XmlBinaryWriterSession
	{
		/// <summary>Creates an instance of this class.</summary>
		// Token: 0x0600037F RID: 895 RVA: 0x000118D0 File Offset: 0x0000FAD0
		public XmlBinaryWriterSession()
		{
			this.nextKey = 0;
			this.maps = new XmlBinaryWriterSession.PriorityDictionary<IXmlDictionary, XmlBinaryWriterSession.IntArray>();
			this.strings = new XmlBinaryWriterSession.PriorityDictionary<string, int>();
		}

		/// <summary>Tries to add an <see cref="T:System.Xml.XmlDictionaryString" /> to the internal collection.</summary>
		/// <param name="value">The <see cref="T:System.Xml.XmlDictionaryString" /> to add.</param>
		/// <param name="key">The key of the <see cref="T:System.Xml.XmlDictionaryString" /> that was successfully added.</param>
		/// <returns>
		///   <see langword="true" /> if the string could be added; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An entry with key = <paramref name="key" /> already exists.</exception>
		// Token: 0x06000380 RID: 896 RVA: 0x000118F8 File Offset: 0x0000FAF8
		public virtual bool TryAdd(XmlDictionaryString value, out int key)
		{
			if (value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
			}
			XmlBinaryWriterSession.IntArray intArray;
			if (!this.maps.TryGetValue(value.Dictionary, out intArray))
			{
				key = this.Add(value.Value);
				intArray = this.AddKeys(value.Dictionary, value.Key + 1);
				intArray[value.Key] = key + 1;
				return true;
			}
			key = intArray[value.Key] - 1;
			if (key != -1)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("The specified key already exists in the dictionary.")));
			}
			key = this.Add(value.Value);
			intArray[value.Key] = key + 1;
			return true;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x000119A8 File Offset: 0x0000FBA8
		private int Add(string s)
		{
			int num = this.nextKey;
			this.nextKey = num + 1;
			int num2 = num;
			this.strings.Add(s, num2);
			return num2;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x000119D8 File Offset: 0x0000FBD8
		private XmlBinaryWriterSession.IntArray AddKeys(IXmlDictionary dictionary, int minCount)
		{
			XmlBinaryWriterSession.IntArray intArray = new XmlBinaryWriterSession.IntArray(Math.Max(minCount, 16));
			this.maps.Add(dictionary, intArray);
			return intArray;
		}

		/// <summary>Clears out the internal collections.</summary>
		// Token: 0x06000383 RID: 899 RVA: 0x00011A01 File Offset: 0x0000FC01
		public void Reset()
		{
			this.nextKey = 0;
			this.maps.Clear();
			this.strings.Clear();
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00011A20 File Offset: 0x0000FC20
		internal bool TryLookup(XmlDictionaryString s, out int key)
		{
			XmlBinaryWriterSession.IntArray intArray;
			if (this.maps.TryGetValue(s.Dictionary, out intArray))
			{
				key = intArray[s.Key] - 1;
				if (key != -1)
				{
					return true;
				}
			}
			if (this.strings.TryGetValue(s.Value, out key))
			{
				if (intArray == null)
				{
					intArray = this.AddKeys(s.Dictionary, s.Key + 1);
				}
				intArray[s.Key] = key + 1;
				return true;
			}
			key = -1;
			return false;
		}

		// Token: 0x04000223 RID: 547
		private XmlBinaryWriterSession.PriorityDictionary<string, int> strings;

		// Token: 0x04000224 RID: 548
		private XmlBinaryWriterSession.PriorityDictionary<IXmlDictionary, XmlBinaryWriterSession.IntArray> maps;

		// Token: 0x04000225 RID: 549
		private int nextKey;

		// Token: 0x02000052 RID: 82
		private class PriorityDictionary<K, V> where K : class
		{
			// Token: 0x06000385 RID: 901 RVA: 0x00011A9B File Offset: 0x0000FC9B
			public PriorityDictionary()
			{
				this.list = new XmlBinaryWriterSession.PriorityDictionary<K, V>.Entry[16];
			}

			// Token: 0x06000386 RID: 902 RVA: 0x00011AB0 File Offset: 0x0000FCB0
			public void Clear()
			{
				this.now = 0;
				this.listCount = 0;
				Array.Clear(this.list, 0, this.list.Length);
				if (this.dictionary != null)
				{
					this.dictionary.Clear();
				}
			}

			// Token: 0x06000387 RID: 903 RVA: 0x00011AE8 File Offset: 0x0000FCE8
			public bool TryGetValue(K key, out V value)
			{
				for (int i = 0; i < this.listCount; i++)
				{
					if (this.list[i].Key == key)
					{
						value = this.list[i].Value;
						this.list[i].Time = this.Now;
						return true;
					}
				}
				for (int j = 0; j < this.listCount; j++)
				{
					if (this.list[j].Key.Equals(key))
					{
						value = this.list[j].Value;
						this.list[j].Time = this.Now;
						return true;
					}
				}
				if (this.dictionary == null)
				{
					value = default(V);
					return false;
				}
				if (!this.dictionary.TryGetValue(key, out value))
				{
					return false;
				}
				int num = 0;
				int time = this.list[0].Time;
				for (int k = 1; k < this.listCount; k++)
				{
					if (this.list[k].Time < time)
					{
						num = k;
						time = this.list[k].Time;
					}
				}
				this.list[num].Key = key;
				this.list[num].Value = value;
				this.list[num].Time = this.Now;
				return true;
			}

			// Token: 0x06000388 RID: 904 RVA: 0x00011C70 File Offset: 0x0000FE70
			public void Add(K key, V value)
			{
				if (this.listCount < this.list.Length)
				{
					this.list[this.listCount].Key = key;
					this.list[this.listCount].Value = value;
					this.listCount++;
					return;
				}
				if (this.dictionary == null)
				{
					this.dictionary = new Dictionary<K, V>();
					for (int i = 0; i < this.listCount; i++)
					{
						this.dictionary.Add(this.list[i].Key, this.list[i].Value);
					}
				}
				this.dictionary.Add(key, value);
			}

			// Token: 0x1700006A RID: 106
			// (get) Token: 0x06000389 RID: 905 RVA: 0x00011D28 File Offset: 0x0000FF28
			private int Now
			{
				get
				{
					int num = this.now + 1;
					this.now = num;
					if (num == 2147483647)
					{
						this.DecreaseAll();
					}
					return this.now;
				}
			}

			// Token: 0x0600038A RID: 906 RVA: 0x00011D5C File Offset: 0x0000FF5C
			private void DecreaseAll()
			{
				for (int i = 0; i < this.listCount; i++)
				{
					XmlBinaryWriterSession.PriorityDictionary<K, V>.Entry[] array = this.list;
					int num = i;
					array[num].Time = array[num].Time / 2;
				}
				this.now /= 2;
			}

			// Token: 0x04000226 RID: 550
			private Dictionary<K, V> dictionary;

			// Token: 0x04000227 RID: 551
			private XmlBinaryWriterSession.PriorityDictionary<K, V>.Entry[] list;

			// Token: 0x04000228 RID: 552
			private int listCount;

			// Token: 0x04000229 RID: 553
			private int now;

			// Token: 0x02000053 RID: 83
			private struct Entry
			{
				// Token: 0x0400022A RID: 554
				public K Key;

				// Token: 0x0400022B RID: 555
				public V Value;

				// Token: 0x0400022C RID: 556
				public int Time;
			}
		}

		// Token: 0x02000054 RID: 84
		private class IntArray
		{
			// Token: 0x0600038B RID: 907 RVA: 0x00011D9E File Offset: 0x0000FF9E
			public IntArray(int size)
			{
				this.array = new int[size];
			}

			// Token: 0x1700006B RID: 107
			public int this[int index]
			{
				get
				{
					if (index >= this.array.Length)
					{
						return 0;
					}
					return this.array[index];
				}
				set
				{
					if (index >= this.array.Length)
					{
						int[] destinationArray = new int[Math.Max(index + 1, this.array.Length * 2)];
						Array.Copy(this.array, destinationArray, this.array.Length);
						this.array = destinationArray;
					}
					this.array[index] = value;
				}
			}

			// Token: 0x0400022D RID: 557
			private int[] array;
		}
	}
}
