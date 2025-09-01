using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Xml;

namespace System.Data.Common
{
	/// <summary>This class can be used by any provider to support a provider-specific configuration section.</summary>
	// Token: 0x020003D2 RID: 978
	public class DbProviderConfigurationHandler : IConfigurationSectionHandler
	{
		/// <summary>This class can be used by any provider to support a provider-specific configuration section.</summary>
		// Token: 0x06002F37 RID: 12087 RVA: 0x00003D93 File Offset: 0x00001F93
		public DbProviderConfigurationHandler()
		{
		}

		// Token: 0x06002F38 RID: 12088 RVA: 0x000CABA1 File Offset: 0x000C8DA1
		internal static NameValueCollection CloneParent(NameValueCollection parentConfig)
		{
			if (parentConfig == null)
			{
				parentConfig = new NameValueCollection();
			}
			else
			{
				parentConfig = new NameValueCollection(parentConfig);
			}
			return parentConfig;
		}

		/// <summary>Creates a new <see cref="T:System.Collections.Specialized.NameValueCollection" /> expression.</summary>
		/// <param name="parent">This type supports the .NET Framework infrastructure and is not intended to be used directly from your code.</param>
		/// <param name="configContext">This type supports the .NET Framework infrastructure and is not intended to be used directly from your code.</param>
		/// <param name="section">This type supports the .NET Framework infrastructure and is not intended to be used directly from your code.</param>
		/// <returns>The new expression.</returns>
		// Token: 0x06002F39 RID: 12089 RVA: 0x000CABB8 File Offset: 0x000C8DB8
		public virtual object Create(object parent, object configContext, XmlNode section)
		{
			return DbProviderConfigurationHandler.CreateStatic(parent, configContext, section);
		}

		// Token: 0x06002F3A RID: 12090 RVA: 0x000CABC4 File Offset: 0x000C8DC4
		internal static object CreateStatic(object parent, object configContext, XmlNode section)
		{
			object obj = parent;
			if (section != null)
			{
				obj = DbProviderConfigurationHandler.CloneParent(parent as NameValueCollection);
				bool flag = false;
				HandlerBase.CheckForUnrecognizedAttributes(section);
				foreach (object obj2 in section.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj2;
					if (!HandlerBase.IsIgnorableAlsoCheckForNonElement(xmlNode))
					{
						if (!(xmlNode.Name == "settings"))
						{
							throw ADP.ConfigUnrecognizedElement(xmlNode);
						}
						if (flag)
						{
							throw ADP.ConfigSectionsUnique("settings");
						}
						flag = true;
						DbProviderConfigurationHandler.DbProviderDictionarySectionHandler.CreateStatic(obj as NameValueCollection, configContext, xmlNode);
					}
				}
			}
			return obj;
		}

		// Token: 0x06002F3B RID: 12091 RVA: 0x000CAC78 File Offset: 0x000C8E78
		internal static string RemoveAttribute(XmlNode node, string name)
		{
			XmlNode xmlNode = node.Attributes.RemoveNamedItem(name);
			if (xmlNode == null)
			{
				throw ADP.ConfigRequiredAttributeMissing(name, node);
			}
			string value = xmlNode.Value;
			if (value.Length == 0)
			{
				throw ADP.ConfigRequiredAttributeEmpty(name, node);
			}
			return value;
		}

		// Token: 0x04001C73 RID: 7283
		internal const string settings = "settings";

		// Token: 0x020003D3 RID: 979
		private sealed class DbProviderDictionarySectionHandler
		{
			// Token: 0x06002F3C RID: 12092 RVA: 0x000CACB8 File Offset: 0x000C8EB8
			internal static NameValueCollection CreateStatic(NameValueCollection config, object context, XmlNode section)
			{
				if (section != null)
				{
					HandlerBase.CheckForUnrecognizedAttributes(section);
				}
				foreach (object obj in section.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (!HandlerBase.IsIgnorableAlsoCheckForNonElement(xmlNode))
					{
						string name = xmlNode.Name;
						if (!(name == "add"))
						{
							if (!(name == "remove"))
							{
								if (!(name == "clear"))
								{
									throw ADP.ConfigUnrecognizedElement(xmlNode);
								}
								DbProviderConfigurationHandler.DbProviderDictionarySectionHandler.HandleClear(xmlNode, config);
							}
							else
							{
								DbProviderConfigurationHandler.DbProviderDictionarySectionHandler.HandleRemove(xmlNode, config);
							}
						}
						else
						{
							DbProviderConfigurationHandler.DbProviderDictionarySectionHandler.HandleAdd(xmlNode, config);
						}
					}
				}
				return config;
			}

			// Token: 0x06002F3D RID: 12093 RVA: 0x000CAD70 File Offset: 0x000C8F70
			private static void HandleAdd(XmlNode child, NameValueCollection config)
			{
				HandlerBase.CheckForChildNodes(child);
				string name = DbProviderConfigurationHandler.RemoveAttribute(child, "name");
				string value = DbProviderConfigurationHandler.RemoveAttribute(child, "value");
				HandlerBase.CheckForUnrecognizedAttributes(child);
				config.Add(name, value);
			}

			// Token: 0x06002F3E RID: 12094 RVA: 0x000CADAC File Offset: 0x000C8FAC
			private static void HandleRemove(XmlNode child, NameValueCollection config)
			{
				HandlerBase.CheckForChildNodes(child);
				string name = DbProviderConfigurationHandler.RemoveAttribute(child, "name");
				HandlerBase.CheckForUnrecognizedAttributes(child);
				config.Remove(name);
			}

			// Token: 0x06002F3F RID: 12095 RVA: 0x000CADD8 File Offset: 0x000C8FD8
			private static void HandleClear(XmlNode child, NameValueCollection config)
			{
				HandlerBase.CheckForChildNodes(child);
				HandlerBase.CheckForUnrecognizedAttributes(child);
				config.Clear();
			}

			// Token: 0x06002F40 RID: 12096 RVA: 0x00003D93 File Offset: 0x00001F93
			public DbProviderDictionarySectionHandler()
			{
			}
		}
	}
}
