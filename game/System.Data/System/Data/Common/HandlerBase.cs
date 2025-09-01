using System;
using System.Globalization;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x020003DA RID: 986
	internal static class HandlerBase
	{
		// Token: 0x06002F64 RID: 12132 RVA: 0x000CB889 File Offset: 0x000C9A89
		internal static void CheckForChildNodes(XmlNode node)
		{
			if (node.HasChildNodes)
			{
				throw ADP.ConfigBaseNoChildNodes(node.FirstChild);
			}
		}

		// Token: 0x06002F65 RID: 12133 RVA: 0x000CB89F File Offset: 0x000C9A9F
		private static void CheckForNonElement(XmlNode node)
		{
			if (XmlNodeType.Element != node.NodeType)
			{
				throw ADP.ConfigBaseElementsOnly(node);
			}
		}

		// Token: 0x06002F66 RID: 12134 RVA: 0x000CB8B1 File Offset: 0x000C9AB1
		internal static void CheckForUnrecognizedAttributes(XmlNode node)
		{
			if (node.Attributes.Count != 0)
			{
				throw ADP.ConfigUnrecognizedAttributes(node);
			}
		}

		// Token: 0x06002F67 RID: 12135 RVA: 0x000CB8C7 File Offset: 0x000C9AC7
		internal static bool IsIgnorableAlsoCheckForNonElement(XmlNode node)
		{
			if (XmlNodeType.Comment == node.NodeType || XmlNodeType.Whitespace == node.NodeType)
			{
				return true;
			}
			HandlerBase.CheckForNonElement(node);
			return false;
		}

		// Token: 0x06002F68 RID: 12136 RVA: 0x000CB8E8 File Offset: 0x000C9AE8
		internal static string RemoveAttribute(XmlNode node, string name, bool required, bool allowEmpty)
		{
			XmlNode xmlNode = node.Attributes.RemoveNamedItem(name);
			if (xmlNode == null)
			{
				if (required)
				{
					throw ADP.ConfigRequiredAttributeMissing(name, node);
				}
				return null;
			}
			else
			{
				string value = xmlNode.Value;
				if (!allowEmpty && value.Length == 0)
				{
					throw ADP.ConfigRequiredAttributeEmpty(name, node);
				}
				return value;
			}
		}

		// Token: 0x06002F69 RID: 12137 RVA: 0x000CB92D File Offset: 0x000C9B2D
		internal static DataSet CloneParent(DataSet parentConfig, bool insenstive)
		{
			if (parentConfig == null)
			{
				parentConfig = new DataSet("system.data");
				parentConfig.CaseSensitive = !insenstive;
				parentConfig.Locale = CultureInfo.InvariantCulture;
			}
			else
			{
				parentConfig = parentConfig.Copy();
			}
			return parentConfig;
		}
	}
}
