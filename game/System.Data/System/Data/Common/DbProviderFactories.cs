using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Data.Common
{
	/// <summary>Represents a set of static methods for creating one or more instances of <see cref="T:System.Data.Common.DbProviderFactory" /> classes.</summary>
	// Token: 0x020003D4 RID: 980
	public static class DbProviderFactories
	{
		/// <summary>Returns an instance of a <see cref="T:System.Data.Common.DbProviderFactory" />.</summary>
		/// <param name="providerInvariantName">Invariant name of a provider.</param>
		/// <returns>An instance of a <see cref="T:System.Data.Common.DbProviderFactory" /> for a specified provider name.</returns>
		// Token: 0x06002F41 RID: 12097 RVA: 0x000CADEC File Offset: 0x000C8FEC
		public static DbProviderFactory GetFactory(string providerInvariantName)
		{
			return DbProviderFactories.GetFactory(providerInvariantName, true);
		}

		// Token: 0x06002F42 RID: 12098 RVA: 0x000CADF8 File Offset: 0x000C8FF8
		public static DbProviderFactory GetFactory(string providerInvariantName, bool throwOnError)
		{
			if (throwOnError)
			{
				ADP.CheckArgumentLength(providerInvariantName, "providerInvariantName");
			}
			DataTable providerTable = DbProviderFactories.GetProviderTable();
			if (providerTable != null)
			{
				DataRow dataRow = providerTable.Rows.Find(providerInvariantName);
				if (dataRow != null)
				{
					return DbProviderFactories.GetFactory(dataRow);
				}
			}
			if (throwOnError)
			{
				throw ADP.ConfigProviderNotFound();
			}
			return null;
		}

		/// <summary>Returns an instance of a <see cref="T:System.Data.Common.DbProviderFactory" />.</summary>
		/// <param name="providerRow">
		///   <see cref="T:System.Data.DataRow" /> containing the provider's configuration information.</param>
		/// <returns>An instance of a <see cref="T:System.Data.Common.DbProviderFactory" /> for a specified <see cref="T:System.Data.DataRow" />.</returns>
		// Token: 0x06002F43 RID: 12099 RVA: 0x000CAE40 File Offset: 0x000C9040
		public static DbProviderFactory GetFactory(DataRow providerRow)
		{
			ADP.CheckArgumentNull(providerRow, "providerRow");
			DataColumn dataColumn = providerRow.Table.Columns["AssemblyQualifiedName"];
			if (dataColumn != null)
			{
				string text = providerRow[dataColumn] as string;
				if (!ADP.IsEmpty(text))
				{
					Type type = Type.GetType(text);
					if (null != type)
					{
						FieldInfo field = type.GetField("Instance", BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public);
						if (null != field && field.FieldType.IsSubclassOf(typeof(DbProviderFactory)))
						{
							object value = field.GetValue(null);
							if (value != null)
							{
								return (DbProviderFactory)value;
							}
						}
						throw ADP.ConfigProviderInvalid();
					}
					throw ADP.ConfigProviderNotInstalled();
				}
			}
			throw ADP.ConfigProviderMissing();
		}

		/// <summary>Returns an instance of a <see cref="T:System.Data.Common.DbProviderFactory" />.</summary>
		/// <param name="connection">The connection used.</param>
		/// <returns>An instance of a <see cref="T:System.Data.Common.DbProviderFactory" /> for a specified connection.</returns>
		// Token: 0x06002F44 RID: 12100 RVA: 0x000CAEEA File Offset: 0x000C90EA
		public static DbProviderFactory GetFactory(DbConnection connection)
		{
			ADP.CheckArgumentNull(connection, "connection");
			return connection.ProviderFactory;
		}

		/// <summary>Returns a <see cref="T:System.Data.DataTable" /> that contains information about all installed providers that implement <see cref="T:System.Data.Common.DbProviderFactory" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> containing <see cref="T:System.Data.DataRow" /> objects that contain the following data:  
		///   Column ordinal  
		///
		///   Column name  
		///
		///   Description  
		///
		///   0  
		///
		///   **Name**  
		///
		///   Human-readable name for the data provider.  
		///
		///   1  
		///
		///   **Description**  
		///
		///   Human-readable description of the data provider.  
		///
		///   2  
		///
		///   **InvariantName**  
		///
		///   Name that can be used programmatically to refer to the data provider.  
		///
		///   3  
		///
		///   **AssemblyQualifiedName**  
		///
		///   Fully qualified name of the factory class, which contains enough information to instantiate the object.</returns>
		// Token: 0x06002F45 RID: 12101 RVA: 0x000CAF00 File Offset: 0x000C9100
		public static DataTable GetFactoryClasses()
		{
			DataTable dataTable = DbProviderFactories.GetProviderTable();
			if (dataTable != null)
			{
				dataTable = dataTable.Copy();
			}
			else
			{
				dataTable = DbProviderFactoriesConfigurationHandler.CreateProviderDataTable();
			}
			return dataTable;
		}

		// Token: 0x06002F46 RID: 12102 RVA: 0x000CAF28 File Offset: 0x000C9128
		private static DataTable IncludeFrameworkFactoryClasses(DataTable configDataTable)
		{
			DataTable dataTable = DbProviderFactoriesConfigurationHandler.CreateProviderDataTable();
			string factoryAssemblyQualifiedName = typeof(SqlClientFactory).AssemblyQualifiedName.ToString().Replace("System.Data.SqlClient.SqlClientFactory, System.Data,", "System.Data.OracleClient.OracleClientFactory, System.Data.OracleClient,");
			DbProviderFactoryConfigSection[] array = new DbProviderFactoryConfigSection[]
			{
				new DbProviderFactoryConfigSection(typeof(OdbcFactory), "Odbc Data Provider", ".Net Framework Data Provider for Odbc"),
				new DbProviderFactoryConfigSection(typeof(OleDbFactory), "OleDb Data Provider", ".Net Framework Data Provider for OleDb"),
				new DbProviderFactoryConfigSection("OracleClient Data Provider", "System.Data.OracleClient", ".Net Framework Data Provider for Oracle", factoryAssemblyQualifiedName),
				new DbProviderFactoryConfigSection(typeof(SqlClientFactory), "SqlClient Data Provider", ".Net Framework Data Provider for SqlServer")
			};
			for (int i = 0; i < array.Length; i++)
			{
				if (!array[i].IsNull())
				{
					bool flag = false;
					if (i == 2)
					{
						Type type = Type.GetType(array[i].AssemblyQualifiedName);
						if (type != null)
						{
							FieldInfo field = type.GetField("Instance", BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public);
							if (null != field && field.FieldType.IsSubclassOf(typeof(DbProviderFactory)))
							{
								object value = field.GetValue(null);
								if (value != null)
								{
									flag = true;
								}
							}
						}
					}
					else
					{
						flag = true;
					}
					if (flag)
					{
						DataRow dataRow = dataTable.NewRow();
						dataRow["Name"] = array[i].Name;
						dataRow["InvariantName"] = array[i].InvariantName;
						dataRow["Description"] = array[i].Description;
						dataRow["AssemblyQualifiedName"] = array[i].AssemblyQualifiedName;
						dataTable.Rows.Add(dataRow);
					}
				}
			}
			int num = 0;
			while (configDataTable != null && num < configDataTable.Rows.Count)
			{
				try
				{
					bool flag2 = false;
					if (configDataTable.Rows[num]["AssemblyQualifiedName"].ToString().ToLowerInvariant().Contains("System.Data.OracleClient".ToString().ToLowerInvariant()))
					{
						Type type2 = Type.GetType(configDataTable.Rows[num]["AssemblyQualifiedName"].ToString());
						if (type2 != null)
						{
							FieldInfo field2 = type2.GetField("Instance", BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public);
							if (null != field2 && field2.FieldType.IsSubclassOf(typeof(DbProviderFactory)))
							{
								object value2 = field2.GetValue(null);
								if (value2 != null)
								{
									flag2 = true;
								}
							}
						}
					}
					else
					{
						flag2 = true;
					}
					if (flag2)
					{
						dataTable.Rows.Add(configDataTable.Rows[num].ItemArray);
					}
				}
				catch (ConstraintException)
				{
				}
				num++;
			}
			return dataTable;
		}

		// Token: 0x06002F47 RID: 12103 RVA: 0x000CB1D0 File Offset: 0x000C93D0
		private static DataTable GetProviderTable()
		{
			DbProviderFactories.Initialize();
			return DbProviderFactories._providerTable;
		}

		// Token: 0x06002F48 RID: 12104 RVA: 0x000CB1DC File Offset: 0x000C93DC
		private static void Initialize()
		{
			if (ConnectionState.Open != DbProviderFactories._initState)
			{
				object lockobj = DbProviderFactories._lockobj;
				lock (lockobj)
				{
					ConnectionState initState = DbProviderFactories._initState;
					if (initState != ConnectionState.Closed)
					{
						if (initState - ConnectionState.Open > 1)
						{
						}
					}
					else
					{
						DbProviderFactories._initState = ConnectionState.Connecting;
						try
						{
							DataSet dataSet = PrivilegedConfigurationManager.GetSection("system.data") as DataSet;
							DbProviderFactories._providerTable = ((dataSet != null) ? DbProviderFactories.IncludeFrameworkFactoryClasses(dataSet.Tables["DbProviderFactories"]) : DbProviderFactories.IncludeFrameworkFactoryClasses(null));
						}
						finally
						{
							DbProviderFactories._initState = ConnectionState.Open;
						}
					}
				}
			}
		}

		// Token: 0x06002F49 RID: 12105 RVA: 0x000CB280 File Offset: 0x000C9480
		public static bool TryGetFactory(string providerInvariantName, out DbProviderFactory factory)
		{
			factory = DbProviderFactories.GetFactory(providerInvariantName, false);
			return factory != null;
		}

		// Token: 0x06002F4A RID: 12106 RVA: 0x000CB290 File Offset: 0x000C9490
		public static IEnumerable<string> GetProviderInvariantNames()
		{
			return DbProviderFactories._registeredFactories.Keys.ToList<string>();
		}

		// Token: 0x06002F4B RID: 12107 RVA: 0x000CB2A1 File Offset: 0x000C94A1
		public static void RegisterFactory(string providerInvariantName, string factoryTypeAssemblyQualifiedName)
		{
			ADP.CheckArgumentLength(providerInvariantName, "providerInvariantName");
			ADP.CheckArgumentLength(factoryTypeAssemblyQualifiedName, "factoryTypeAssemblyQualifiedName");
			DbProviderFactories._registeredFactories[providerInvariantName] = new DbProviderFactories.ProviderRegistration(factoryTypeAssemblyQualifiedName, null);
		}

		// Token: 0x06002F4C RID: 12108 RVA: 0x000CB2CC File Offset: 0x000C94CC
		private static DbProviderFactory GetFactoryInstance(Type providerFactoryClass)
		{
			ADP.CheckArgumentNull(providerFactoryClass, "providerFactoryClass");
			if (!providerFactoryClass.IsSubclassOf(typeof(DbProviderFactory)))
			{
				throw ADP.Argument(SR.Format("The type '{0}' doesn't inherit from DbProviderFactory.", providerFactoryClass.FullName));
			}
			FieldInfo field = providerFactoryClass.GetField("Instance", BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public);
			if (null == field)
			{
				throw ADP.InvalidOperation("The requested .NET Data Provider's implementation does not have an Instance field of a System.Data.Common.DbProviderFactory derived type.");
			}
			if (!field.FieldType.IsSubclassOf(typeof(DbProviderFactory)))
			{
				throw ADP.InvalidOperation("The requested .NET Data Provider's implementation does not have an Instance field of a System.Data.Common.DbProviderFactory derived type.");
			}
			object value = field.GetValue(null);
			if (value == null)
			{
				throw ADP.InvalidOperation("The requested .NET Data Provider's implementation does not have an Instance field of a System.Data.Common.DbProviderFactory derived type.");
			}
			return (DbProviderFactory)value;
		}

		// Token: 0x06002F4D RID: 12109 RVA: 0x000CB36C File Offset: 0x000C956C
		public static void RegisterFactory(string providerInvariantName, Type providerFactoryClass)
		{
			DbProviderFactories.RegisterFactory(providerInvariantName, DbProviderFactories.GetFactoryInstance(providerFactoryClass));
		}

		// Token: 0x06002F4E RID: 12110 RVA: 0x000CB37A File Offset: 0x000C957A
		public static void RegisterFactory(string providerInvariantName, DbProviderFactory factory)
		{
			ADP.CheckArgumentLength(providerInvariantName, "providerInvariantName");
			ADP.CheckArgumentNull(factory, "factory");
			DbProviderFactories._registeredFactories[providerInvariantName] = new DbProviderFactories.ProviderRegistration(factory.GetType().AssemblyQualifiedName, factory);
		}

		// Token: 0x06002F4F RID: 12111 RVA: 0x000CB3B0 File Offset: 0x000C95B0
		public static bool UnregisterFactory(string providerInvariantName)
		{
			DbProviderFactories.ProviderRegistration providerRegistration;
			return !string.IsNullOrWhiteSpace(providerInvariantName) && DbProviderFactories._registeredFactories.TryRemove(providerInvariantName, out providerRegistration);
		}

		// Token: 0x06002F50 RID: 12112 RVA: 0x000CB3D4 File Offset: 0x000C95D4
		// Note: this type is marked as 'beforefieldinit'.
		static DbProviderFactories()
		{
		}

		// Token: 0x04001C74 RID: 7284
		private const string AssemblyQualifiedName = "AssemblyQualifiedName";

		// Token: 0x04001C75 RID: 7285
		private const string Instance = "Instance";

		// Token: 0x04001C76 RID: 7286
		private const string InvariantName = "InvariantName";

		// Token: 0x04001C77 RID: 7287
		private const string Name = "Name";

		// Token: 0x04001C78 RID: 7288
		private const string Description = "Description";

		// Token: 0x04001C79 RID: 7289
		private const string InstanceFieldName = "Instance";

		// Token: 0x04001C7A RID: 7290
		private static ConcurrentDictionary<string, DbProviderFactories.ProviderRegistration> _registeredFactories = new ConcurrentDictionary<string, DbProviderFactories.ProviderRegistration>();

		// Token: 0x04001C7B RID: 7291
		private static ConnectionState _initState;

		// Token: 0x04001C7C RID: 7292
		private static DataTable _providerTable;

		// Token: 0x04001C7D RID: 7293
		private static object _lockobj = new object();

		// Token: 0x020003D5 RID: 981
		private struct ProviderRegistration
		{
			// Token: 0x06002F51 RID: 12113 RVA: 0x000CB3EA File Offset: 0x000C95EA
			internal ProviderRegistration(string factoryTypeAssemblyQualifiedName, DbProviderFactory factoryInstance)
			{
				this.FactoryTypeAssemblyQualifiedName = factoryTypeAssemblyQualifiedName;
				this.FactoryInstance = factoryInstance;
			}

			// Token: 0x170007D2 RID: 2002
			// (get) Token: 0x06002F52 RID: 12114 RVA: 0x000CB3FA File Offset: 0x000C95FA
			internal readonly string FactoryTypeAssemblyQualifiedName
			{
				[CompilerGenerated]
				get
				{
					return this.<FactoryTypeAssemblyQualifiedName>k__BackingField;
				}
			}

			// Token: 0x170007D3 RID: 2003
			// (get) Token: 0x06002F53 RID: 12115 RVA: 0x000CB402 File Offset: 0x000C9602
			internal readonly DbProviderFactory FactoryInstance
			{
				[CompilerGenerated]
				get
				{
					return this.<FactoryInstance>k__BackingField;
				}
			}

			// Token: 0x04001C7E RID: 7294
			[CompilerGenerated]
			private readonly string <FactoryTypeAssemblyQualifiedName>k__BackingField;

			// Token: 0x04001C7F RID: 7295
			[CompilerGenerated]
			private readonly DbProviderFactory <FactoryInstance>k__BackingField;
		}
	}
}
