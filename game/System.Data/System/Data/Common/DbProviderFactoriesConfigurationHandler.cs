using System;
using System.Configuration;
using System.Globalization;
using System.Xml;

namespace System.Data.Common
{
	/// <summary>This type supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
	// Token: 0x020003D8 RID: 984
	public class DbProviderFactoriesConfigurationHandler : IConfigurationSectionHandler
	{
		/// <summary>This type supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x06002F5B RID: 12123 RVA: 0x00003D93 File Offset: 0x00001F93
		public DbProviderFactoriesConfigurationHandler()
		{
		}

		/// <summary>This type supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="parent">This type supports the .NET Framework infrastructure and is not intended to be used directly from your code.</param>
		/// <param name="configContext">This type supports the .NET Framework infrastructure and is not intended to be used directly from your code.</param>
		/// <param name="section">This type supports the .NET Framework infrastructure and is not intended to be used directly from your code.</param>
		/// <returns>This type supports the .NET Framework infrastructure and is not intended to be used directly from your code.</returns>
		// Token: 0x06002F5C RID: 12124 RVA: 0x000CB519 File Offset: 0x000C9719
		public virtual object Create(object parent, object configContext, XmlNode section)
		{
			return DbProviderFactoriesConfigurationHandler.CreateStatic(parent, configContext, section);
		}

		// Token: 0x06002F5D RID: 12125 RVA: 0x000CB524 File Offset: 0x000C9724
		internal static object CreateStatic(object parent, object configContext, XmlNode section)
		{
			object obj = parent;
			if (section != null)
			{
				obj = HandlerBase.CloneParent(parent as DataSet, false);
				bool flag = false;
				HandlerBase.CheckForUnrecognizedAttributes(section);
				foreach (object obj2 in section.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj2;
					if (!HandlerBase.IsIgnorableAlsoCheckForNonElement(xmlNode))
					{
						string name = xmlNode.Name;
						if (!(name == "DbProviderFactories"))
						{
							throw ADP.ConfigUnrecognizedElement(xmlNode);
						}
						if (flag)
						{
							throw ADP.ConfigSectionsUnique("DbProviderFactories");
						}
						flag = true;
						DbProviderFactoriesConfigurationHandler.HandleProviders(obj as DataSet, configContext, xmlNode, name);
					}
				}
			}
			return obj;
		}

		// Token: 0x06002F5E RID: 12126 RVA: 0x000CB5E0 File Offset: 0x000C97E0
		private static void HandleProviders(DataSet config, object configContext, XmlNode section, string sectionName)
		{
			DataTableCollection tables = config.Tables;
			DataTable dataTable = tables[sectionName];
			bool flag = dataTable != null;
			dataTable = DbProviderFactoriesConfigurationHandler.DbProviderDictionarySectionHandler.CreateStatic(dataTable, configContext, section);
			if (!flag)
			{
				tables.Add(dataTable);
			}
		}

		// Token: 0x06002F5F RID: 12127 RVA: 0x000CB614 File Offset: 0x000C9814
		internal static DataTable CreateProviderDataTable()
		{
			DataColumn dataColumn = new DataColumn("Name", typeof(string));
			dataColumn.ReadOnly = true;
			DataColumn dataColumn2 = new DataColumn("Description", typeof(string));
			dataColumn2.ReadOnly = true;
			DataColumn dataColumn3 = new DataColumn("InvariantName", typeof(string));
			dataColumn3.ReadOnly = true;
			DataColumn dataColumn4 = new DataColumn("AssemblyQualifiedName", typeof(string));
			dataColumn4.ReadOnly = true;
			DataColumn[] primaryKey = new DataColumn[]
			{
				dataColumn3
			};
			DataColumn[] columns = new DataColumn[]
			{
				dataColumn,
				dataColumn2,
				dataColumn3,
				dataColumn4
			};
			DataTable dataTable = new DataTable("DbProviderFactories");
			dataTable.Locale = CultureInfo.InvariantCulture;
			dataTable.Columns.AddRange(columns);
			dataTable.PrimaryKey = primaryKey;
			return dataTable;
		}

		// Token: 0x04001C8B RID: 7307
		internal const string sectionName = "system.data";

		// Token: 0x04001C8C RID: 7308
		internal const string providerGroup = "DbProviderFactories";

		// Token: 0x04001C8D RID: 7309
		internal const string odbcProviderName = "Odbc Data Provider";

		// Token: 0x04001C8E RID: 7310
		internal const string odbcProviderDescription = ".Net Framework Data Provider for Odbc";

		// Token: 0x04001C8F RID: 7311
		internal const string oledbProviderName = "OleDb Data Provider";

		// Token: 0x04001C90 RID: 7312
		internal const string oledbProviderDescription = ".Net Framework Data Provider for OleDb";

		// Token: 0x04001C91 RID: 7313
		internal const string oracleclientProviderName = "OracleClient Data Provider";

		// Token: 0x04001C92 RID: 7314
		internal const string oracleclientProviderNamespace = "System.Data.OracleClient";

		// Token: 0x04001C93 RID: 7315
		internal const string oracleclientProviderDescription = ".Net Framework Data Provider for Oracle";

		// Token: 0x04001C94 RID: 7316
		internal const string sqlclientProviderName = "SqlClient Data Provider";

		// Token: 0x04001C95 RID: 7317
		internal const string sqlclientProviderDescription = ".Net Framework Data Provider for SqlServer";

		// Token: 0x04001C96 RID: 7318
		internal const string sqlclientPartialAssemblyQualifiedName = "System.Data.SqlClient.SqlClientFactory, System.Data,";

		// Token: 0x04001C97 RID: 7319
		internal const string oracleclientPartialAssemblyQualifiedName = "System.Data.OracleClient.OracleClientFactory, System.Data.OracleClient,";

		// Token: 0x020003D9 RID: 985
		private static class DbProviderDictionarySectionHandler
		{
			// Token: 0x06002F60 RID: 12128 RVA: 0x000CB6E0 File Offset: 0x000C98E0
			internal static DataTable CreateStatic(DataTable config, object context, XmlNode section)
			{
				if (section != null)
				{
					HandlerBase.CheckForUnrecognizedAttributes(section);
					if (config == null)
					{
						config = DbProviderFactoriesConfigurationHandler.CreateProviderDataTable();
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
									DbProviderFactoriesConfigurationHandler.DbProviderDictionarySectionHandler.HandleClear(xmlNode, config);
								}
								else
								{
									DbProviderFactoriesConfigurationHandler.DbProviderDictionarySectionHandler.HandleRemove(xmlNode, config);
								}
							}
							else
							{
								DbProviderFactoriesConfigurationHandler.DbProviderDictionarySectionHandler.HandleAdd(xmlNode, config);
							}
						}
					}
					config.AcceptChanges();
				}
				return config;
			}

			// Token: 0x06002F61 RID: 12129 RVA: 0x000CB7AC File Offset: 0x000C99AC
			private static void HandleAdd(XmlNode child, DataTable config)
			{
				HandlerBase.CheckForChildNodes(child);
				DataRow dataRow = config.NewRow();
				dataRow[0] = HandlerBase.RemoveAttribute(child, "name", true, false);
				dataRow[1] = HandlerBase.RemoveAttribute(child, "description", true, false);
				dataRow[2] = HandlerBase.RemoveAttribute(child, "invariant", true, false);
				dataRow[3] = HandlerBase.RemoveAttribute(child, "type", true, false);
				HandlerBase.RemoveAttribute(child, "support", false, false);
				HandlerBase.CheckForUnrecognizedAttributes(child);
				config.Rows.Add(dataRow);
			}

			// Token: 0x06002F62 RID: 12130 RVA: 0x000CB838 File Offset: 0x000C9A38
			private static void HandleRemove(XmlNode child, DataTable config)
			{
				HandlerBase.CheckForChildNodes(child);
				string key = HandlerBase.RemoveAttribute(child, "invariant", true, false);
				HandlerBase.CheckForUnrecognizedAttributes(child);
				DataRow dataRow = config.Rows.Find(key);
				if (dataRow != null)
				{
					dataRow.Delete();
				}
			}

			// Token: 0x06002F63 RID: 12131 RVA: 0x000CB875 File Offset: 0x000C9A75
			private static void HandleClear(XmlNode child, DataTable config)
			{
				HandlerBase.CheckForChildNodes(child);
				HandlerBase.CheckForUnrecognizedAttributes(child);
				config.Clear();
			}
		}
	}
}
