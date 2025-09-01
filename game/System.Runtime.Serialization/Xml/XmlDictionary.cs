using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace System.Xml
{
	/// <summary>Implements a dictionary used to optimize Windows Communication Foundation (WCF)'s XML reader/writer implementations.</summary>
	// Token: 0x0200005D RID: 93
	public class XmlDictionary : IXmlDictionary
	{
		/// <summary>Gets a <see langword="static" /> empty <see cref="T:System.Xml.IXmlDictionary" />.</summary>
		/// <returns>A <see langword="static" /> empty <see cref="T:System.Xml.IXmlDictionary" />.</returns>
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00016B76 File Offset: 0x00014D76
		public static IXmlDictionary Empty
		{
			get
			{
				if (XmlDictionary.empty == null)
				{
					XmlDictionary.empty = new XmlDictionary.EmptyDictionary();
				}
				return XmlDictionary.empty;
			}
		}

		/// <summary>Creates an empty <see cref="T:System.Xml.XmlDictionary" />.</summary>
		// Token: 0x06000468 RID: 1128 RVA: 0x00016B8E File Offset: 0x00014D8E
		public XmlDictionary()
		{
			this.lookup = new Dictionary<string, XmlDictionaryString>();
			this.strings = null;
			this.nextId = 0;
		}

		/// <summary>Creates a <see cref="T:System.Xml.XmlDictionary" /> with an initial capacity.</summary>
		/// <param name="capacity">The initial size of the dictionary.</param>
		// Token: 0x06000469 RID: 1129 RVA: 0x00016BAF File Offset: 0x00014DAF
		public XmlDictionary(int capacity)
		{
			this.lookup = new Dictionary<string, XmlDictionaryString>(capacity);
			this.strings = new XmlDictionaryString[capacity];
			this.nextId = 0;
		}

		/// <summary>Adds a string to the <see cref="T:System.Xml.XmlDictionary" />.</summary>
		/// <param name="value">String to add to the dictionary.</param>
		/// <returns>The <see cref="T:System.Xml.XmlDictionaryString" /> that was added.</returns>
		// Token: 0x0600046A RID: 1130 RVA: 0x00016BD8 File Offset: 0x00014DD8
		public virtual XmlDictionaryString Add(string value)
		{
			XmlDictionaryString xmlDictionaryString;
			if (!this.lookup.TryGetValue(value, out xmlDictionaryString))
			{
				if (this.strings == null)
				{
					this.strings = new XmlDictionaryString[4];
				}
				else if (this.nextId == this.strings.Length)
				{
					int num = this.nextId * 2;
					if (num == 0)
					{
						num = 4;
					}
					Array.Resize<XmlDictionaryString>(ref this.strings, num);
				}
				xmlDictionaryString = new XmlDictionaryString(this, value, this.nextId);
				this.strings[this.nextId] = xmlDictionaryString;
				this.lookup.Add(value, xmlDictionaryString);
				this.nextId++;
			}
			return xmlDictionaryString;
		}

		/// <summary>Checks the dictionary for a specified string value.</summary>
		/// <param name="value">String value being checked for.</param>
		/// <param name="result">The corresponding <see cref="T:System.Xml.XmlDictionaryString" />, if found; otherwise <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if value is in the dictionary; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x0600046B RID: 1131 RVA: 0x00016C6D File Offset: 0x00014E6D
		public virtual bool TryLookup(string value, out XmlDictionaryString result)
		{
			return this.lookup.TryGetValue(value, out result);
		}

		/// <summary>Attempts to look up an entry in the dictionary.</summary>
		/// <param name="key">Key to look up.</param>
		/// <param name="result">If <paramref name="key" /> is defined, the <see cref="T:System.Xml.XmlDictionaryString" /> that is mapped to the key; otherwise <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if key is in the dictionary; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600046C RID: 1132 RVA: 0x00016C7C File Offset: 0x00014E7C
		public virtual bool TryLookup(int key, out XmlDictionaryString result)
		{
			if (key < 0 || key >= this.nextId)
			{
				result = null;
				return false;
			}
			result = this.strings[key];
			return true;
		}

		/// <summary>Checks the dictionary for a specified <see cref="T:System.Xml.XmlDictionaryString" />.</summary>
		/// <param name="value">The <see cref="T:System.Xml.XmlDictionaryString" /> being checked for.</param>
		/// <param name="result">The matching <see cref="T:System.Xml.XmlDictionaryString" />, if found; otherwise, <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if <see cref="T:System.Xml.XmlDictionaryString" /> is in the dictionary; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x0600046D RID: 1133 RVA: 0x0000FC88 File Offset: 0x0000DE88
		public virtual bool TryLookup(XmlDictionaryString value, out XmlDictionaryString result)
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

		// Token: 0x04000276 RID: 630
		private static IXmlDictionary empty;

		// Token: 0x04000277 RID: 631
		private Dictionary<string, XmlDictionaryString> lookup;

		// Token: 0x04000278 RID: 632
		private XmlDictionaryString[] strings;

		// Token: 0x04000279 RID: 633
		private int nextId;

		// Token: 0x0200005E RID: 94
		private class EmptyDictionary : IXmlDictionary
		{
			// Token: 0x0600046E RID: 1134 RVA: 0x00016C9B File Offset: 0x00014E9B
			public bool TryLookup(string value, out XmlDictionaryString result)
			{
				result = null;
				return false;
			}

			// Token: 0x0600046F RID: 1135 RVA: 0x00016C9B File Offset: 0x00014E9B
			public bool TryLookup(int key, out XmlDictionaryString result)
			{
				result = null;
				return false;
			}

			// Token: 0x06000470 RID: 1136 RVA: 0x00016C9B File Offset: 0x00014E9B
			public bool TryLookup(XmlDictionaryString value, out XmlDictionaryString result)
			{
				result = null;
				return false;
			}

			// Token: 0x06000471 RID: 1137 RVA: 0x0000222F File Offset: 0x0000042F
			public EmptyDictionary()
			{
			}
		}
	}
}
