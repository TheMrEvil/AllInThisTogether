using System;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.Xml;

namespace System.Data
{
	// Token: 0x0200013C RID: 316
	internal class XMLSchema
	{
		// Token: 0x060010F2 RID: 4338 RVA: 0x00047E57 File Offset: 0x00046057
		internal static TypeConverter GetConverter(Type type)
		{
			return TypeDescriptor.GetConverter(type);
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x00047E60 File Offset: 0x00046060
		internal static void SetProperties(object instance, XmlAttributeCollection attrs)
		{
			for (int i = 0; i < attrs.Count; i++)
			{
				if (attrs[i].NamespaceURI == "urn:schemas-microsoft-com:xml-msdata")
				{
					string localName = attrs[i].LocalName;
					string value = attrs[i].Value;
					if (!(localName == "DefaultValue") && !(localName == "RemotingFormat") && (!(localName == "Expression") || !(instance is DataColumn)))
					{
						PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(instance)[localName];
						if (propertyDescriptor != null)
						{
							Type propertyType = propertyDescriptor.PropertyType;
							TypeConverter converter = XMLSchema.GetConverter(propertyType);
							object value2;
							if (converter.CanConvertFrom(typeof(string)))
							{
								value2 = converter.ConvertFromInvariantString(value);
							}
							else if (propertyType == typeof(Type))
							{
								value2 = DataStorage.GetType(value);
							}
							else
							{
								if (!(propertyType == typeof(CultureInfo)))
								{
									throw ExceptionBuilder.CannotConvert(value, propertyType.FullName);
								}
								value2 = new CultureInfo(value);
							}
							propertyDescriptor.SetValue(instance, value2);
						}
					}
				}
			}
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x00047F85 File Offset: 0x00046185
		internal static bool FEqualIdentity(XmlNode node, string name, string ns)
		{
			return node != null && node.LocalName == name && node.NamespaceURI == ns;
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x00047FAC File Offset: 0x000461AC
		internal static bool GetBooleanAttribute(XmlElement element, string attrName, string attrNS, bool defVal)
		{
			string attribute = element.GetAttribute(attrName, attrNS);
			if (attribute == null || attribute.Length == 0)
			{
				return defVal;
			}
			if (attribute == "true" || attribute == "1")
			{
				return true;
			}
			if (attribute == "false" || attribute == "0")
			{
				return false;
			}
			throw ExceptionBuilder.InvalidAttributeValue(attrName, attribute);
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x00048010 File Offset: 0x00046210
		internal static string GenUniqueColumnName(string proposedName, DataTable table)
		{
			if (table.Columns.IndexOf(proposedName) >= 0)
			{
				for (int i = 0; i <= table.Columns.Count; i++)
				{
					string text = proposedName + "_" + i.ToString(CultureInfo.InvariantCulture);
					if (table.Columns.IndexOf(text) < 0)
					{
						return text;
					}
				}
			}
			return proposedName;
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x00003D93 File Offset: 0x00001F93
		public XMLSchema()
		{
		}
	}
}
