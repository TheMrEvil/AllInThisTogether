using System;

namespace System.Xml
{
	/// <summary>An <see langword="interface" /> that defines the contract that an Xml dictionary must implement to be used by <see cref="T:System.Xml.XmlDictionaryReader" /> and <see cref="T:System.Xml.XmlDictionaryWriter" /> implementations.</summary>
	// Token: 0x0200001E RID: 30
	public interface IXmlDictionary
	{
		/// <summary>Checks the dictionary for a specified string value.</summary>
		/// <param name="value">String value being checked for.</param>
		/// <param name="result">The corresponding <see cref="T:System.Xml.XmlDictionaryString" />, if found; otherwise, <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if value is in the dictionary; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000091 RID: 145
		bool TryLookup(string value, out XmlDictionaryString result);

		/// <summary>Attempts to look up an entry in the dictionary.</summary>
		/// <param name="key">Key to look up.</param>
		/// <param name="result">If <paramref name="key" /> is defined, the <see cref="T:System.Xml.XmlDictionaryString" /> that is mapped to the key; otherwise <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if key is in the dictionary; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000092 RID: 146
		bool TryLookup(int key, out XmlDictionaryString result);

		/// <summary>Checks the dictionary for a specified <see cref="T:System.Xml.XmlDictionaryString" />.</summary>
		/// <param name="value">The <see cref="T:System.Xml.XmlDictionaryString" /> being checked for.</param>
		/// <param name="result">The matching <see cref="T:System.Xml.XmlDictionaryString" />, if found; otherwise, <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if <see cref="T:System.Xml.XmlDictionaryString" /> is in the dictionary; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000093 RID: 147
		bool TryLookup(XmlDictionaryString value, out XmlDictionaryString result);
	}
}
