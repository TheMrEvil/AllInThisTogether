using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System.Xml
{
	/// <summary>Enables optimized strings to be managed in a dynamic way.</summary>
	// Token: 0x0200004C RID: 76
	public class XmlBinaryReaderSession : IXmlDictionary
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlBinaryReaderSession" /> class.</summary>
		// Token: 0x06000315 RID: 789 RVA: 0x0000222F File Offset: 0x0000042F
		public XmlBinaryReaderSession()
		{
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlDictionaryString" /> from the input parameters and adds it to an internal collection.</summary>
		/// <param name="id">The key value.</param>
		/// <param name="value">The value.</param>
		/// <returns>The newly created <see cref="T:System.Xml.XmlDictionaryString" /> that is added to an internal collection.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="id" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">An entry with key = <paramref name="id" /> already exists.</exception>
		// Token: 0x06000316 RID: 790 RVA: 0x0000FA78 File Offset: 0x0000DC78
		public XmlDictionaryString Add(int id, string value)
		{
			if (id < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException(System.Runtime.Serialization.SR.GetString("ID must be >= 0.")));
			}
			if (value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
			}
			XmlDictionaryString xmlDictionaryString;
			if (this.TryLookup(id, out xmlDictionaryString))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(System.Runtime.Serialization.SR.GetString("ID already defined.")));
			}
			xmlDictionaryString = new XmlDictionaryString(this, value, id);
			if (id >= 2048)
			{
				if (this.stringDict == null)
				{
					this.stringDict = new Dictionary<int, XmlDictionaryString>();
				}
				this.stringDict.Add(id, xmlDictionaryString);
			}
			else
			{
				if (this.strings == null)
				{
					this.strings = new XmlDictionaryString[Math.Max(id + 1, 16)];
				}
				else if (id >= this.strings.Length)
				{
					XmlDictionaryString[] destinationArray = new XmlDictionaryString[Math.Min(Math.Max(id + 1, this.strings.Length * 2), 2048)];
					Array.Copy(this.strings, destinationArray, this.strings.Length);
					this.strings = destinationArray;
				}
				this.strings[id] = xmlDictionaryString;
			}
			return xmlDictionaryString;
		}

		/// <summary>Checks whether the internal collection contains an entry matching a key.</summary>
		/// <param name="key">The key to search on.</param>
		/// <param name="result">When this method returns, contains a string if an entry is found; otherwise, <see langword="null" />. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if an entry matching the <paramref name="key" /> was found; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is <see langword="null" />.</exception>
		// Token: 0x06000317 RID: 791 RVA: 0x0000FB70 File Offset: 0x0000DD70
		public bool TryLookup(int key, out XmlDictionaryString result)
		{
			if (this.strings != null && key >= 0 && key < this.strings.Length)
			{
				result = this.strings[key];
				return result != null;
			}
			if (key >= 2048 && this.stringDict != null)
			{
				return this.stringDict.TryGetValue(key, out result);
			}
			result = null;
			return false;
		}

		/// <summary>Checks whether the internal collection contains an entry matching a value.</summary>
		/// <param name="value">The value to search for.</param>
		/// <param name="result">When this method returns, contains a string if an entry is found; otherwise, <see langword="null" />. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if an entry matching the <paramref name="value" /> was found; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06000318 RID: 792 RVA: 0x0000FBC8 File Offset: 0x0000DDC8
		public bool TryLookup(string value, out XmlDictionaryString result)
		{
			if (value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
			}
			if (this.strings != null)
			{
				for (int i = 0; i < this.strings.Length; i++)
				{
					XmlDictionaryString xmlDictionaryString = this.strings[i];
					if (xmlDictionaryString != null && xmlDictionaryString.Value == value)
					{
						result = xmlDictionaryString;
						return true;
					}
				}
			}
			if (this.stringDict != null)
			{
				foreach (XmlDictionaryString xmlDictionaryString2 in this.stringDict.Values)
				{
					if (xmlDictionaryString2.Value == value)
					{
						result = xmlDictionaryString2;
						return true;
					}
				}
			}
			result = null;
			return false;
		}

		/// <summary>Checks whether the internal collection contains an entry matching a value.</summary>
		/// <param name="value">The value to search for.</param>
		/// <param name="result">When this method returns, contains a string if an entry is found; otherwise, <see langword="null" />. This parameter is passed uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if an entry matching the <paramref name="value" /> was found; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06000319 RID: 793 RVA: 0x0000FC88 File Offset: 0x0000DE88
		public bool TryLookup(XmlDictionaryString value, out XmlDictionaryString result)
		{
			if (value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("value"));
			}
			if (value.Dictionary != this)
			{
				result = null;
				return false;
			}
			result = value;
			return true;
		}

		/// <summary>Clears the internal collection of all contents.</summary>
		// Token: 0x0600031A RID: 794 RVA: 0x0000FCAF File Offset: 0x0000DEAF
		public void Clear()
		{
			if (this.strings != null)
			{
				Array.Clear(this.strings, 0, this.strings.Length);
			}
			if (this.stringDict != null)
			{
				this.stringDict.Clear();
			}
		}

		// Token: 0x04000212 RID: 530
		private const int MaxArrayEntries = 2048;

		// Token: 0x04000213 RID: 531
		private XmlDictionaryString[] strings;

		// Token: 0x04000214 RID: 532
		private Dictionary<int, XmlDictionaryString> stringDict;
	}
}
