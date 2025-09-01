using System;
using System.Configuration;
using System.Xml;

namespace System.Net.Configuration
{
	// Token: 0x02000763 RID: 1891
	internal class ConnectionManagementHandler : IConfigurationSectionHandler
	{
		// Token: 0x06003BAD RID: 15277 RVA: 0x000CC778 File Offset: 0x000CA978
		public virtual object Create(object parent, object configContext, XmlNode section)
		{
			ConnectionManagementData connectionManagementData = new ConnectionManagementData(parent);
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
					if (name == "clear")
					{
						if (xmlNode.Attributes != null && xmlNode.Attributes.Count != 0)
						{
							HandlersUtil.ThrowException("Unrecognized attribute", xmlNode);
						}
						connectionManagementData.Clear();
					}
					else
					{
						string address = HandlersUtil.ExtractAttributeValue("address", xmlNode);
						if (name == "add")
						{
							string nconns = HandlersUtil.ExtractAttributeValue("maxconnection", xmlNode, true);
							if (xmlNode.Attributes != null && xmlNode.Attributes.Count != 0)
							{
								HandlersUtil.ThrowException("Unrecognized attribute", xmlNode);
							}
							connectionManagementData.Add(address, nconns);
						}
						else if (name == "remove")
						{
							if (xmlNode.Attributes != null && xmlNode.Attributes.Count != 0)
							{
								HandlersUtil.ThrowException("Unrecognized attribute", xmlNode);
							}
							connectionManagementData.Remove(address);
						}
						else
						{
							HandlersUtil.ThrowException("Unexpected element", xmlNode);
						}
					}
				}
			}
			return connectionManagementData;
		}

		// Token: 0x06003BAE RID: 15278 RVA: 0x0000219B File Offset: 0x0000039B
		public ConnectionManagementHandler()
		{
		}
	}
}
