using System;
using System.Configuration;
using System.Xml;

namespace System.Net.Configuration
{
	// Token: 0x0200076F RID: 1903
	internal class NetConfigurationHandler : IConfigurationSectionHandler
	{
		// Token: 0x06003BF5 RID: 15349 RVA: 0x000CD578 File Offset: 0x000CB778
		public virtual object Create(object parent, object configContext, XmlNode section)
		{
			NetConfig netConfig = new NetConfig();
			if (section.Attributes != null && section.Attributes.Count != 0)
			{
				HandlersUtil.ThrowException("Unrecognized attribute", section);
			}
			foreach (object obj in section.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNodeType nodeType = xmlNode.NodeType;
				if (nodeType != XmlNodeType.Whitespace && nodeType != XmlNodeType.Comment)
				{
					if (nodeType != XmlNodeType.Element)
					{
						HandlersUtil.ThrowException("Only elements allowed", xmlNode);
					}
					string name = xmlNode.Name;
					if (name == "ipv6")
					{
						string a = HandlersUtil.ExtractAttributeValue("enabled", xmlNode, false);
						if (xmlNode.Attributes != null && xmlNode.Attributes.Count != 0)
						{
							HandlersUtil.ThrowException("Unrecognized attribute", xmlNode);
						}
						if (a == "true")
						{
							netConfig.ipv6Enabled = true;
						}
						else if (a != "false")
						{
							HandlersUtil.ThrowException("Invalid boolean value", xmlNode);
						}
					}
					else
					{
						if (name == "httpWebRequest")
						{
							string text = HandlersUtil.ExtractAttributeValue("maximumResponseHeadersLength", xmlNode, true);
							HandlersUtil.ExtractAttributeValue("useUnsafeHeaderParsing", xmlNode, true);
							if (xmlNode.Attributes != null && xmlNode.Attributes.Count != 0)
							{
								HandlersUtil.ThrowException("Unrecognized attribute", xmlNode);
							}
							try
							{
								if (text != null)
								{
									int num = int.Parse(text.Trim());
									if (num < -1)
									{
										HandlersUtil.ThrowException("Must be -1 or >= 0", xmlNode);
									}
									netConfig.MaxResponseHeadersLength = num;
								}
								continue;
							}
							catch
							{
								HandlersUtil.ThrowException("Invalid int value", xmlNode);
								continue;
							}
						}
						HandlersUtil.ThrowException("Unexpected element", xmlNode);
					}
				}
			}
			return netConfig;
		}

		// Token: 0x06003BF6 RID: 15350 RVA: 0x0000219B File Offset: 0x0000039B
		public NetConfigurationHandler()
		{
		}
	}
}
