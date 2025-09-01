using System;
using System.Collections;
using System.Collections.Specialized;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x0200019C RID: 412
	internal class ConfigHelper
	{
		// Token: 0x06000AD3 RID: 2771 RVA: 0x0002EA60 File Offset: 0x0002CC60
		internal static IDictionary GetDictionary(IDictionary prev, XmlNode region, string nameAtt, string valueAtt)
		{
			Hashtable dict;
			if (prev == null)
			{
				dict = new Hashtable(CaseInsensitiveHashCodeProvider.Default, CaseInsensitiveComparer.Default);
			}
			else
			{
				dict = (Hashtable)((Hashtable)prev).Clone();
			}
			ConfigHelper.CollectionWrapper collectionWrapper = new ConfigHelper.CollectionWrapper(dict);
			collectionWrapper = ConfigHelper.GoGetThem(collectionWrapper, region, nameAtt, valueAtt);
			if (collectionWrapper == null)
			{
				return null;
			}
			return collectionWrapper.UnWrap() as IDictionary;
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0002EAB4 File Offset: 0x0002CCB4
		internal static ConfigNameValueCollection GetNameValueCollection(NameValueCollection prev, XmlNode region, string nameAtt, string valueAtt)
		{
			ConfigNameValueCollection configNameValueCollection = new ConfigNameValueCollection(CaseInsensitiveHashCodeProvider.Default, CaseInsensitiveComparer.Default);
			if (prev != null)
			{
				configNameValueCollection.Add(prev);
			}
			ConfigHelper.CollectionWrapper collectionWrapper = new ConfigHelper.CollectionWrapper(configNameValueCollection);
			collectionWrapper = ConfigHelper.GoGetThem(collectionWrapper, region, nameAtt, valueAtt);
			if (collectionWrapper == null)
			{
				return null;
			}
			return collectionWrapper.UnWrap() as ConfigNameValueCollection;
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0002EAFC File Offset: 0x0002CCFC
		private static ConfigHelper.CollectionWrapper GoGetThem(ConfigHelper.CollectionWrapper result, XmlNode region, string nameAtt, string valueAtt)
		{
			if (region.Attributes != null && region.Attributes.Count != 0 && (region.Attributes.Count != 1 || region.Attributes[0].Name != "xmlns"))
			{
				throw new ConfigurationException("Unknown attribute", region);
			}
			foreach (object obj in region.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNodeType nodeType = xmlNode.NodeType;
				if (nodeType != XmlNodeType.Whitespace && nodeType != XmlNodeType.Comment)
				{
					if (nodeType != XmlNodeType.Element)
					{
						throw new ConfigurationException("Only XmlElement allowed", xmlNode);
					}
					string name = xmlNode.Name;
					if (name == "clear")
					{
						if (xmlNode.Attributes != null && xmlNode.Attributes.Count != 0)
						{
							throw new ConfigurationException("Unknown attribute", xmlNode);
						}
						result.Clear();
					}
					else if (name == "remove")
					{
						XmlNode xmlNode2 = null;
						if (xmlNode.Attributes != null)
						{
							xmlNode2 = xmlNode.Attributes.RemoveNamedItem(nameAtt);
						}
						if (xmlNode2 == null)
						{
							throw new ConfigurationException("Required attribute not found", xmlNode);
						}
						if (xmlNode2.Value == string.Empty)
						{
							throw new ConfigurationException("Required attribute is empty", xmlNode);
						}
						if (xmlNode.Attributes.Count != 0)
						{
							throw new ConfigurationException("Unknown attribute", xmlNode);
						}
						result.Remove(xmlNode2.Value);
					}
					else
					{
						if (!(name == "add"))
						{
							throw new ConfigurationException("Unknown element", xmlNode);
						}
						XmlNode xmlNode2 = null;
						if (xmlNode.Attributes != null)
						{
							xmlNode2 = xmlNode.Attributes.RemoveNamedItem(nameAtt);
						}
						if (xmlNode2 == null)
						{
							throw new ConfigurationException("Required attribute not found", xmlNode);
						}
						if (xmlNode2.Value == string.Empty)
						{
							throw new ConfigurationException("Required attribute is empty", xmlNode);
						}
						XmlNode xmlNode3 = xmlNode.Attributes.RemoveNamedItem(valueAtt);
						if (xmlNode3 == null)
						{
							throw new ConfigurationException("Required attribute not found", xmlNode);
						}
						if (xmlNode.Attributes.Count != 0)
						{
							throw new ConfigurationException("Unknown attribute", xmlNode);
						}
						result[xmlNode2.Value] = xmlNode3.Value;
					}
				}
			}
			return result;
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0000219B File Offset: 0x0000039B
		public ConfigHelper()
		{
		}

		// Token: 0x0200019D RID: 413
		private class CollectionWrapper
		{
			// Token: 0x06000AD7 RID: 2775 RVA: 0x0002ED44 File Offset: 0x0002CF44
			public CollectionWrapper(IDictionary dict)
			{
				this.dict = dict;
				this.isDict = true;
			}

			// Token: 0x06000AD8 RID: 2776 RVA: 0x0002ED5A File Offset: 0x0002CF5A
			public CollectionWrapper(NameValueCollection collection)
			{
				this.collection = collection;
				this.isDict = false;
			}

			// Token: 0x06000AD9 RID: 2777 RVA: 0x0002ED70 File Offset: 0x0002CF70
			public void Remove(string s)
			{
				if (this.isDict)
				{
					this.dict.Remove(s);
					return;
				}
				this.collection.Remove(s);
			}

			// Token: 0x06000ADA RID: 2778 RVA: 0x0002ED93 File Offset: 0x0002CF93
			public void Clear()
			{
				if (this.isDict)
				{
					this.dict.Clear();
					return;
				}
				this.collection.Clear();
			}

			// Token: 0x170001B6 RID: 438
			public string this[string key]
			{
				set
				{
					if (this.isDict)
					{
						this.dict[key] = value;
						return;
					}
					this.collection[key] = value;
				}
			}

			// Token: 0x06000ADC RID: 2780 RVA: 0x0002EDD9 File Offset: 0x0002CFD9
			public object UnWrap()
			{
				if (this.isDict)
				{
					return this.dict;
				}
				return this.collection;
			}

			// Token: 0x0400072F RID: 1839
			private IDictionary dict;

			// Token: 0x04000730 RID: 1840
			private NameValueCollection collection;

			// Token: 0x04000731 RID: 1841
			private bool isDict;
		}
	}
}
