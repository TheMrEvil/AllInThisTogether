using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data
{
	/// <summary>Represents an in-memory cache of data.</summary>
	// Token: 0x02000094 RID: 148
	[ToolboxItem("Microsoft.VSDesigner.Data.VS.DataSetToolboxItem, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[XmlRoot("DataSet")]
	[DefaultProperty("DataSetName")]
	[XmlSchemaProvider("GetDataSetSchema")]
	[Serializable]
	public class DataSet : MarshalByValueComponent, IListSource, IXmlSerializable, ISupportInitializeNotification, ISupportInitialize, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataSet" /> class.</summary>
		// Token: 0x06000820 RID: 2080 RVA: 0x0001BBB8 File Offset: 0x00019DB8
		public DataSet()
		{
			GC.SuppressFinalize(this);
			DataCommonEventSource.Log.Trace<int>("<ds.DataSet.DataSet|API> {0}", this.ObjectID);
			this._tableCollection = new DataTableCollection(this);
			this._relationCollection = new DataRelationCollection.DataSetRelationCollection(this);
			this._culture = CultureInfo.CurrentCulture;
		}

		/// <summary>Initializes a new instance of a <see cref="T:System.Data.DataSet" /> class with the given name.</summary>
		/// <param name="dataSetName">The name of the <see cref="T:System.Data.DataSet" />.</param>
		// Token: 0x06000821 RID: 2081 RVA: 0x0001BC5E File Offset: 0x00019E5E
		public DataSet(string dataSetName) : this()
		{
			this.DataSetName = dataSetName;
		}

		/// <summary>Gets or sets a <see cref="T:System.Data.SerializationFormat" /> for the <see cref="T:System.Data.DataSet" /> used during remoting.</summary>
		/// <returns>A <see cref="T:System.Data.SerializationFormat" /> object.</returns>
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x0001BC6D File Offset: 0x00019E6D
		// (set) Token: 0x06000823 RID: 2083 RVA: 0x0001BC78 File Offset: 0x00019E78
		[DefaultValue(SerializationFormat.Xml)]
		public SerializationFormat RemotingFormat
		{
			get
			{
				return this._remotingFormat;
			}
			set
			{
				if (value != SerializationFormat.Binary && value != SerializationFormat.Xml)
				{
					throw ExceptionBuilder.InvalidRemotingFormat(value);
				}
				this._remotingFormat = value;
				for (int i = 0; i < this.Tables.Count; i++)
				{
					this.Tables[i].RemotingFormat = value;
				}
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Data.SchemaSerializationMode" /> for a <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>A <see cref="T:System.Data.SchemaSerializationMode" /> for a <see cref="T:System.Data.DataSet" />.</returns>
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x00006D61 File Offset: 0x00004F61
		// (set) Token: 0x06000825 RID: 2085 RVA: 0x0001BCC2 File Offset: 0x00019EC2
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual SchemaSerializationMode SchemaSerializationMode
		{
			get
			{
				return SchemaSerializationMode.IncludeSchema;
			}
			set
			{
				if (value != SchemaSerializationMode.IncludeSchema)
				{
					throw ExceptionBuilder.CannotChangeSchemaSerializationMode();
				}
			}
		}

		/// <summary>Inspects the format of the serialized representation of the <see langword="DataSet" />.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> represents a <see langword="DataSet" /> serialized in its binary format, <see langword="false" /> otherwise.</returns>
		// Token: 0x06000826 RID: 2086 RVA: 0x0001BCD0 File Offset: 0x00019ED0
		protected bool IsBinarySerialized(SerializationInfo info, StreamingContext context)
		{
			SerializationFormat serializationFormat = SerializationFormat.Xml;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name == "DataSet.RemotingFormat")
				{
					serializationFormat = (SerializationFormat)enumerator.Value;
					break;
				}
			}
			return serializationFormat == SerializationFormat.Binary;
		}

		/// <summary>Determines the <see cref="P:System.Data.DataSet.SchemaSerializationMode" /> for a <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that a <see langword="DataSet" />'s protected constructor <see cref="M:System.Data.DataSet.#ctor(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" /> is invoked with during deserialization in remoting scenarios.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that a <see langword="DataSet" />'s protected constructor <see cref="M:System.Data.DataSet.#ctor(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" /> is invoked with during deserialization in remoting scenarios.</param>
		/// <returns>An <see cref="T:System.Data.SchemaSerializationMode" /> enumeration indicating whether schema information has been omitted from the payload.</returns>
		// Token: 0x06000827 RID: 2087 RVA: 0x0001BD14 File Offset: 0x00019F14
		protected SchemaSerializationMode DetermineSchemaSerializationMode(SerializationInfo info, StreamingContext context)
		{
			SchemaSerializationMode result = SchemaSerializationMode.IncludeSchema;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name == "SchemaSerializationMode.DataSet")
				{
					result = (SchemaSerializationMode)enumerator.Value;
					break;
				}
			}
			return result;
		}

		/// <summary>Determines the <see cref="P:System.Data.DataSet.SchemaSerializationMode" /> for a <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> instance that is passed during deserialization of the <see cref="T:System.Data.DataSet" />.</param>
		/// <returns>An <see cref="T:System.Data.SchemaSerializationMode" /> enumeration indicating whether schema information has been omitted from the payload.</returns>
		// Token: 0x06000828 RID: 2088 RVA: 0x0001BD58 File Offset: 0x00019F58
		protected SchemaSerializationMode DetermineSchemaSerializationMode(XmlReader reader)
		{
			SchemaSerializationMode result = SchemaSerializationMode.IncludeSchema;
			reader.MoveToContent();
			if (reader.NodeType == XmlNodeType.Element && reader.HasAttributes)
			{
				string attribute = reader.GetAttribute("SchemaSerializationMode", "urn:schemas-microsoft-com:xml-msdata");
				if (string.Equals(attribute, "ExcludeSchema", StringComparison.OrdinalIgnoreCase))
				{
					result = SchemaSerializationMode.ExcludeSchema;
				}
				else if (string.Equals(attribute, "IncludeSchema", StringComparison.OrdinalIgnoreCase))
				{
					result = SchemaSerializationMode.IncludeSchema;
				}
				else if (attribute != null)
				{
					throw ExceptionBuilder.InvalidSchemaSerializationMode(typeof(SchemaSerializationMode), attribute);
				}
			}
			return result;
		}

		/// <summary>Deserializes the table data from the binary or XML stream.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance.</param>
		/// <param name="context">The streaming context.</param>
		// Token: 0x06000829 RID: 2089 RVA: 0x0001BDCC File Offset: 0x00019FCC
		protected void GetSerializationData(SerializationInfo info, StreamingContext context)
		{
			SerializationFormat remotingFormat = SerializationFormat.Xml;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Name == "DataSet.RemotingFormat")
				{
					remotingFormat = (SerializationFormat)enumerator.Value;
					break;
				}
			}
			this.DeserializeDataSetData(info, context, remotingFormat);
		}

		/// <summary>Initializes a new instance of a <see cref="T:System.Data.DataSet" /> class that has the given serialization information and context.</summary>
		/// <param name="info">The data needed to serialize or deserialize an object.</param>
		/// <param name="context">The source and destination of a given serialized stream.</param>
		// Token: 0x0600082A RID: 2090 RVA: 0x0001BE15 File Offset: 0x0001A015
		protected DataSet(SerializationInfo info, StreamingContext context) : this(info, context, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataSet" /> class.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object.</param>
		/// <param name="ConstructSchema">The boolean value.</param>
		// Token: 0x0600082B RID: 2091 RVA: 0x0001BE20 File Offset: 0x0001A020
		protected DataSet(SerializationInfo info, StreamingContext context, bool ConstructSchema) : this()
		{
			SerializationFormat serializationFormat = SerializationFormat.Xml;
			SchemaSerializationMode schemaSerializationMode = SchemaSerializationMode.IncludeSchema;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				if (!(name == "DataSet.RemotingFormat"))
				{
					if (name == "SchemaSerializationMode.DataSet")
					{
						schemaSerializationMode = (SchemaSerializationMode)enumerator.Value;
					}
				}
				else
				{
					serializationFormat = (SerializationFormat)enumerator.Value;
				}
			}
			if (schemaSerializationMode == SchemaSerializationMode.ExcludeSchema)
			{
				this.InitializeDerivedDataSet();
			}
			if (serializationFormat == SerializationFormat.Xml && !ConstructSchema)
			{
				return;
			}
			this.DeserializeDataSet(info, context, serializationFormat, schemaSerializationMode);
		}

		/// <summary>Populates a serialization information object with the data needed to serialize the <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized data associated with the <see cref="T:System.Data.DataSet" />.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source and destination of the serialized stream associated with the <see cref="T:System.Data.DataSet" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600082C RID: 2092 RVA: 0x0001BEA0 File Offset: 0x0001A0A0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			SerializationFormat remotingFormat = this.RemotingFormat;
			this.SerializeDataSet(info, context, remotingFormat);
		}

		/// <summary>Deserialize all of the tables data of the DataSet from the binary or XML stream.</summary>
		// Token: 0x0600082D RID: 2093 RVA: 0x00007EED File Offset: 0x000060ED
		protected virtual void InitializeDerivedDataSet()
		{
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0001BEC0 File Offset: 0x0001A0C0
		private void SerializeDataSet(SerializationInfo info, StreamingContext context, SerializationFormat remotingFormat)
		{
			info.AddValue("DataSet.RemotingVersion", new Version(2, 0));
			if (remotingFormat != SerializationFormat.Xml)
			{
				info.AddValue("DataSet.RemotingFormat", remotingFormat);
			}
			if (SchemaSerializationMode.IncludeSchema != this.SchemaSerializationMode)
			{
				info.AddValue("SchemaSerializationMode.DataSet", this.SchemaSerializationMode);
			}
			if (remotingFormat != SerializationFormat.Xml)
			{
				if (this.SchemaSerializationMode == SchemaSerializationMode.IncludeSchema)
				{
					this.SerializeDataSetProperties(info, context);
					info.AddValue("DataSet.Tables.Count", this.Tables.Count);
					for (int i = 0; i < this.Tables.Count; i++)
					{
						BinaryFormatter binaryFormatter = new BinaryFormatter(null, new StreamingContext(context.State, false));
						MemoryStream memoryStream = new MemoryStream();
						binaryFormatter.Serialize(memoryStream, this.Tables[i]);
						memoryStream.Position = 0L;
						info.AddValue(string.Format(CultureInfo.InvariantCulture, "DataSet.Tables_{0}", i), memoryStream.GetBuffer());
					}
					for (int j = 0; j < this.Tables.Count; j++)
					{
						this.Tables[j].SerializeConstraints(info, context, j, true);
					}
					this.SerializeRelations(info, context);
					for (int k = 0; k < this.Tables.Count; k++)
					{
						this.Tables[k].SerializeExpressionColumns(info, context, k);
					}
				}
				else
				{
					this.SerializeDataSetProperties(info, context);
				}
				for (int l = 0; l < this.Tables.Count; l++)
				{
					this.Tables[l].SerializeTableData(info, context, l);
				}
				return;
			}
			string xmlSchemaForRemoting = this.GetXmlSchemaForRemoting(null);
			info.AddValue("XmlSchema", xmlSchemaForRemoting);
			StringWriter stringWriter = new StringWriter(new StringBuilder(this.EstimatedXmlStringSize() * 2), CultureInfo.InvariantCulture);
			XmlTextWriter writer = new XmlTextWriter(stringWriter);
			this.WriteXml(writer, XmlWriteMode.DiffGram);
			string value = stringWriter.ToString();
			info.AddValue("XmlDiffGram", value);
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0001C09E File Offset: 0x0001A29E
		internal void DeserializeDataSet(SerializationInfo info, StreamingContext context, SerializationFormat remotingFormat, SchemaSerializationMode schemaSerializationMode)
		{
			this.DeserializeDataSetSchema(info, context, remotingFormat, schemaSerializationMode);
			this.DeserializeDataSetData(info, context, remotingFormat);
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0001C0B4 File Offset: 0x0001A2B4
		private void DeserializeDataSetSchema(SerializationInfo info, StreamingContext context, SerializationFormat remotingFormat, SchemaSerializationMode schemaSerializationMode)
		{
			if (remotingFormat == SerializationFormat.Xml)
			{
				string text = (string)info.GetValue("XmlSchema", typeof(string));
				if (text != null)
				{
					this.ReadXmlSchema(new XmlTextReader(new StringReader(text)), true);
				}
				return;
			}
			if (schemaSerializationMode == SchemaSerializationMode.IncludeSchema)
			{
				this.DeserializeDataSetProperties(info, context);
				int @int = info.GetInt32("DataSet.Tables.Count");
				for (int i = 0; i < @int; i++)
				{
					MemoryStream memoryStream = new MemoryStream((byte[])info.GetValue(string.Format(CultureInfo.InvariantCulture, "DataSet.Tables_{0}", i), typeof(byte[])));
					memoryStream.Position = 0L;
					DataTable table = (DataTable)new BinaryFormatter(null, new StreamingContext(context.State, false)).Deserialize(memoryStream);
					this.Tables.Add(table);
				}
				for (int j = 0; j < @int; j++)
				{
					this.Tables[j].DeserializeConstraints(info, context, j, true);
				}
				this.DeserializeRelations(info, context);
				for (int k = 0; k < @int; k++)
				{
					this.Tables[k].DeserializeExpressionColumns(info, context, k);
				}
				return;
			}
			this.DeserializeDataSetProperties(info, context);
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0001C1EC File Offset: 0x0001A3EC
		private void DeserializeDataSetData(SerializationInfo info, StreamingContext context, SerializationFormat remotingFormat)
		{
			if (remotingFormat != SerializationFormat.Xml)
			{
				for (int i = 0; i < this.Tables.Count; i++)
				{
					this.Tables[i].DeserializeTableData(info, context, i);
				}
				return;
			}
			string text = (string)info.GetValue("XmlDiffGram", typeof(string));
			if (text != null)
			{
				this.ReadXml(new XmlTextReader(new StringReader(text)), XmlReadMode.DiffGram);
			}
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0001C258 File Offset: 0x0001A458
		private void SerializeDataSetProperties(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("DataSet.DataSetName", this.DataSetName);
			info.AddValue("DataSet.Namespace", this.Namespace);
			info.AddValue("DataSet.Prefix", this.Prefix);
			info.AddValue("DataSet.CaseSensitive", this.CaseSensitive);
			info.AddValue("DataSet.LocaleLCID", this.Locale.LCID);
			info.AddValue("DataSet.EnforceConstraints", this.EnforceConstraints);
			info.AddValue("DataSet.ExtendedProperties", this.ExtendedProperties);
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0001C2E4 File Offset: 0x0001A4E4
		private void DeserializeDataSetProperties(SerializationInfo info, StreamingContext context)
		{
			this._dataSetName = info.GetString("DataSet.DataSetName");
			this._namespaceURI = info.GetString("DataSet.Namespace");
			this._datasetPrefix = info.GetString("DataSet.Prefix");
			this._caseSensitive = info.GetBoolean("DataSet.CaseSensitive");
			int culture = (int)info.GetValue("DataSet.LocaleLCID", typeof(int));
			this._culture = new CultureInfo(culture);
			this._cultureUserSet = true;
			this._enforceConstraints = info.GetBoolean("DataSet.EnforceConstraints");
			this._extendedProperties = (PropertyCollection)info.GetValue("DataSet.ExtendedProperties", typeof(PropertyCollection));
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0001C394 File Offset: 0x0001A594
		private void SerializeRelations(SerializationInfo info, StreamingContext context)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.Relations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				int[] array = new int[dataRelation.ParentColumns.Length + 1];
				array[0] = this.Tables.IndexOf(dataRelation.ParentTable);
				for (int i = 1; i < array.Length; i++)
				{
					array[i] = dataRelation.ParentColumns[i - 1].Ordinal;
				}
				int[] array2 = new int[dataRelation.ChildColumns.Length + 1];
				array2[0] = this.Tables.IndexOf(dataRelation.ChildTable);
				for (int j = 1; j < array2.Length; j++)
				{
					array2[j] = dataRelation.ChildColumns[j - 1].Ordinal;
				}
				arrayList.Add(new ArrayList
				{
					dataRelation.RelationName,
					array,
					array2,
					dataRelation.Nested,
					dataRelation._extendedProperties
				});
			}
			info.AddValue("DataSet.Relations", arrayList);
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0001C4F8 File Offset: 0x0001A6F8
		private void DeserializeRelations(SerializationInfo info, StreamingContext context)
		{
			foreach (object obj in ((ArrayList)info.GetValue("DataSet.Relations", typeof(ArrayList))))
			{
				ArrayList arrayList = (ArrayList)obj;
				string relationName = (string)arrayList[0];
				int[] array = (int[])arrayList[1];
				int[] array2 = (int[])arrayList[2];
				bool nested = (bool)arrayList[3];
				PropertyCollection extendedProperties = (PropertyCollection)arrayList[4];
				DataColumn[] array3 = new DataColumn[array.Length - 1];
				for (int i = 0; i < array3.Length; i++)
				{
					array3[i] = this.Tables[array[0]].Columns[array[i + 1]];
				}
				DataColumn[] array4 = new DataColumn[array2.Length - 1];
				for (int j = 0; j < array4.Length; j++)
				{
					array4[j] = this.Tables[array2[0]].Columns[array2[j + 1]];
				}
				DataRelation dataRelation = new DataRelation(relationName, array3, array4, false);
				dataRelation.CheckMultipleNested = false;
				dataRelation.Nested = nested;
				dataRelation._extendedProperties = extendedProperties;
				this.Relations.Add(dataRelation);
				dataRelation.CheckMultipleNested = true;
			}
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0001C674 File Offset: 0x0001A874
		internal void FailedEnableConstraints()
		{
			this.EnforceConstraints = false;
			throw ExceptionBuilder.EnforceConstraint();
		}

		/// <summary>Gets or sets a value indicating whether string comparisons within <see cref="T:System.Data.DataTable" /> objects are case-sensitive.</summary>
		/// <returns>
		///   <see langword="true" /> if string comparisons are case-sensitive; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x0001C682 File Offset: 0x0001A882
		// (set) Token: 0x06000838 RID: 2104 RVA: 0x0001C68C File Offset: 0x0001A88C
		[DefaultValue(false)]
		public bool CaseSensitive
		{
			get
			{
				return this._caseSensitive;
			}
			set
			{
				if (this._caseSensitive != value)
				{
					bool caseSensitive = this._caseSensitive;
					this._caseSensitive = value;
					if (!this.ValidateCaseConstraint())
					{
						this._caseSensitive = caseSensitive;
						throw ExceptionBuilder.CannotChangeCaseLocale();
					}
					foreach (object obj in this.Tables)
					{
						((DataTable)obj).SetCaseSensitiveValue(value, false, true);
					}
				}
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.ComponentModel.IListSource.ContainsListCollection" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.ComponentModel.IListSource.ContainsListCollection" />.</returns>
		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x00006D61 File Offset: 0x00004F61
		bool IListSource.ContainsListCollection
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a custom view of the data contained in the <see cref="T:System.Data.DataSet" /> to allow filtering, searching, and navigating using a custom <see cref="T:System.Data.DataViewManager" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataViewManager" /> object.</returns>
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x0001C714 File Offset: 0x0001A914
		[Browsable(false)]
		public DataViewManager DefaultViewManager
		{
			get
			{
				if (this._defaultViewManager == null)
				{
					object defaultViewManagerLock = this._defaultViewManagerLock;
					lock (defaultViewManagerLock)
					{
						if (this._defaultViewManager == null)
						{
							this._defaultViewManager = new DataViewManager(this, true);
						}
					}
				}
				return this._defaultViewManager;
			}
		}

		/// <summary>Gets or sets a value indicating whether constraint rules are followed when attempting any update operation.</summary>
		/// <returns>
		///   <see langword="true" /> if rules are enforced; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		/// <exception cref="T:System.Data.ConstraintException">One or more constraints cannot be enforced.</exception>
		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x0001C774 File Offset: 0x0001A974
		// (set) Token: 0x0600083C RID: 2108 RVA: 0x0001C77C File Offset: 0x0001A97C
		[DefaultValue(true)]
		public bool EnforceConstraints
		{
			get
			{
				return this._enforceConstraints;
			}
			set
			{
				long scopeId = DataCommonEventSource.Log.EnterScope<int, bool>("<ds.DataSet.set_EnforceConstraints|API> {0}, {1}", this.ObjectID, value);
				try
				{
					if (this._enforceConstraints != value)
					{
						if (value)
						{
							this.EnableConstraints();
						}
						this._enforceConstraints = value;
					}
				}
				finally
				{
					DataCommonEventSource.Log.ExitScope(scopeId);
				}
			}
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0001C7D8 File Offset: 0x0001A9D8
		internal void RestoreEnforceConstraints(bool value)
		{
			this._enforceConstraints = value;
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0001C7E4 File Offset: 0x0001A9E4
		internal void EnableConstraints()
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.EnableConstraints|INFO> {0}", this.ObjectID);
			try
			{
				bool flag = false;
				ConstraintEnumerator constraintEnumerator = new ConstraintEnumerator(this);
				while (constraintEnumerator.GetNext())
				{
					Constraint constraint = constraintEnumerator.GetConstraint();
					flag |= constraint.IsConstraintViolated();
				}
				foreach (object obj in this.Tables)
				{
					foreach (object obj2 in ((DataTable)obj).Columns)
					{
						DataColumn dataColumn = (DataColumn)obj2;
						if (!dataColumn.AllowDBNull)
						{
							flag |= dataColumn.IsNotAllowDBNullViolated();
						}
						if (dataColumn.MaxLength >= 0)
						{
							flag |= dataColumn.IsMaxLengthViolated();
						}
					}
				}
				if (flag)
				{
					this.FailedEnableConstraints();
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
		}

		/// <summary>Gets or sets the name of the current <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>The name of the <see cref="T:System.Data.DataSet" />.</returns>
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x0001C908 File Offset: 0x0001AB08
		// (set) Token: 0x06000840 RID: 2112 RVA: 0x0001C910 File Offset: 0x0001AB10
		[DefaultValue("")]
		public string DataSetName
		{
			get
			{
				return this._dataSetName;
			}
			set
			{
				DataCommonEventSource.Log.Trace<int, string>("<ds.DataSet.set_DataSetName|API> {0}, '{1}'", this.ObjectID, value);
				if (value != this._dataSetName)
				{
					if (value == null || value.Length == 0)
					{
						throw ExceptionBuilder.SetDataSetNameToEmpty();
					}
					DataTable dataTable = this.Tables[value, this.Namespace];
					if (dataTable != null && !dataTable._fNestedInDataset)
					{
						throw ExceptionBuilder.SetDataSetNameConflicting(value);
					}
					this.RaisePropertyChanging("DataSetName");
					this._dataSetName = value;
				}
			}
		}

		/// <summary>Gets or sets the namespace of the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>The namespace of the <see cref="T:System.Data.DataSet" />.</returns>
		/// <exception cref="T:System.ArgumentException">The namespace already has data.</exception>
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000841 RID: 2113 RVA: 0x0001C989 File Offset: 0x0001AB89
		// (set) Token: 0x06000842 RID: 2114 RVA: 0x0001C994 File Offset: 0x0001AB94
		[DefaultValue("")]
		public string Namespace
		{
			get
			{
				return this._namespaceURI;
			}
			set
			{
				DataCommonEventSource.Log.Trace<int, string>("<ds.DataSet.set_Namespace|API> {0}, '{1}'", this.ObjectID, value);
				if (value == null)
				{
					value = string.Empty;
				}
				if (value != this._namespaceURI)
				{
					this.RaisePropertyChanging("Namespace");
					foreach (object obj in this.Tables)
					{
						DataTable dataTable = (DataTable)obj;
						if (dataTable._tableNamespace == null && (dataTable.NestedParentRelations.Length == 0 || (dataTable.NestedParentRelations.Length == 1 && dataTable.NestedParentRelations[0].ChildTable == dataTable)))
						{
							if (this.Tables.Contains(dataTable.TableName, value, false, true))
							{
								throw ExceptionBuilder.DuplicateTableName2(dataTable.TableName, value);
							}
							dataTable.CheckCascadingNamespaceConflict(value);
							dataTable.DoRaiseNamespaceChange();
						}
					}
					this._namespaceURI = value;
					if (string.IsNullOrEmpty(value))
					{
						this._datasetPrefix = string.Empty;
					}
				}
			}
		}

		/// <summary>Gets or sets an XML prefix that aliases the namespace of the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>The XML prefix for the <see cref="T:System.Data.DataSet" /> namespace.</returns>
		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x0001CA98 File Offset: 0x0001AC98
		// (set) Token: 0x06000844 RID: 2116 RVA: 0x0001CAA0 File Offset: 0x0001ACA0
		[DefaultValue("")]
		public string Prefix
		{
			get
			{
				return this._datasetPrefix;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (XmlConvert.DecodeName(value) == value && XmlConvert.EncodeName(value) != value)
				{
					throw ExceptionBuilder.InvalidPrefix(value);
				}
				if (value != this._datasetPrefix)
				{
					this.RaisePropertyChanging("Prefix");
					this._datasetPrefix = value;
				}
			}
		}

		/// <summary>Gets the collection of customized user information associated with the <see langword="DataSet" />.</summary>
		/// <returns>A <see cref="T:System.Data.PropertyCollection" /> with all custom user information.</returns>
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x0001CAFC File Offset: 0x0001ACFC
		[Browsable(false)]
		public PropertyCollection ExtendedProperties
		{
			get
			{
				PropertyCollection result;
				if ((result = this._extendedProperties) == null)
				{
					result = (this._extendedProperties = new PropertyCollection());
				}
				return result;
			}
		}

		/// <summary>Gets a value indicating whether there are errors in any of the <see cref="T:System.Data.DataTable" /> objects within this <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>
		///   <see langword="true" /> if any table contains an error; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x0001CB24 File Offset: 0x0001AD24
		[Browsable(false)]
		public bool HasErrors
		{
			get
			{
				for (int i = 0; i < this.Tables.Count; i++)
				{
					if (this.Tables[i].HasErrors)
					{
						return true;
					}
				}
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.DataSet" /> is initialized.</summary>
		/// <returns>
		///   <see langword="true" /> to indicate the component has completed initialization; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x0001CB5D File Offset: 0x0001AD5D
		[Browsable(false)]
		public bool IsInitialized
		{
			get
			{
				return !this._fInitInProgress;
			}
		}

		/// <summary>Gets or sets the locale information used to compare strings within the table.</summary>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> that contains data about the user's machine locale. The default is <see langword="null" />.</returns>
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000848 RID: 2120 RVA: 0x0001CB68 File Offset: 0x0001AD68
		// (set) Token: 0x06000849 RID: 2121 RVA: 0x0001CB70 File Offset: 0x0001AD70
		public CultureInfo Locale
		{
			get
			{
				return this._culture;
			}
			set
			{
				long scopeId = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.set_Locale|API> {0}", this.ObjectID);
				try
				{
					if (value != null)
					{
						if (!this._culture.Equals(value))
						{
							this.SetLocaleValue(value, true);
						}
						this._cultureUserSet = true;
					}
				}
				finally
				{
					DataCommonEventSource.Log.ExitScope(scopeId);
				}
			}
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0001CBD4 File Offset: 0x0001ADD4
		internal void SetLocaleValue(CultureInfo value, bool userSet)
		{
			bool flag = false;
			bool flag2 = false;
			int num = 0;
			CultureInfo culture = this._culture;
			bool cultureUserSet = this._cultureUserSet;
			try
			{
				this._culture = value;
				this._cultureUserSet = userSet;
				foreach (object obj in this.Tables)
				{
					DataTable dataTable = (DataTable)obj;
					if (!dataTable.ShouldSerializeLocale())
					{
						dataTable.SetLocaleValue(value, false, false);
					}
				}
				flag = this.ValidateLocaleConstraint();
				if (flag)
				{
					flag = false;
					foreach (object obj2 in this.Tables)
					{
						DataTable dataTable2 = (DataTable)obj2;
						num++;
						if (!dataTable2.ShouldSerializeLocale())
						{
							dataTable2.SetLocaleValue(value, false, true);
						}
					}
					flag = true;
				}
			}
			catch
			{
				flag2 = true;
				throw;
			}
			finally
			{
				if (!flag)
				{
					this._culture = culture;
					this._cultureUserSet = cultureUserSet;
					foreach (object obj3 in this.Tables)
					{
						DataTable dataTable3 = (DataTable)obj3;
						if (!dataTable3.ShouldSerializeLocale())
						{
							dataTable3.SetLocaleValue(culture, false, false);
						}
					}
					try
					{
						for (int i = 0; i < num; i++)
						{
							if (!this.Tables[i].ShouldSerializeLocale())
							{
								this.Tables[i].SetLocaleValue(culture, false, true);
							}
						}
					}
					catch (Exception e) when (ADP.IsCatchableExceptionType(e))
					{
						ADP.TraceExceptionWithoutRethrow(e);
					}
					if (!flag2)
					{
						throw ExceptionBuilder.CannotChangeCaseLocale(null);
					}
				}
			}
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0001CDE0 File Offset: 0x0001AFE0
		internal bool ShouldSerializeLocale()
		{
			return this._cultureUserSet;
		}

		/// <summary>Gets or sets an <see cref="T:System.ComponentModel.ISite" /> for the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ISite" /> for the <see cref="T:System.Data.DataSet" />.</returns>
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x0001CDE8 File Offset: 0x0001AFE8
		// (set) Token: 0x0600084D RID: 2125 RVA: 0x0001CDF0 File Offset: 0x0001AFF0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override ISite Site
		{
			get
			{
				return base.Site;
			}
			set
			{
				ISite site = this.Site;
				if (value == null && site != null)
				{
					IContainer container = site.Container;
					if (container != null)
					{
						for (int i = 0; i < this.Tables.Count; i++)
						{
							if (this.Tables[i].Site != null)
							{
								container.Remove(this.Tables[i]);
							}
						}
					}
				}
				base.Site = value;
			}
		}

		/// <summary>Gets the collection of relations that link tables and allow navigation from parent tables to child tables.</summary>
		/// <returns>A <see cref="T:System.Data.DataRelationCollection" /> that contains a collection of <see cref="T:System.Data.DataRelation" /> objects. An empty collection is returned if no <see cref="T:System.Data.DataRelation" /> objects exist.</returns>
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x0001CE56 File Offset: 0x0001B056
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DataRelationCollection Relations
		{
			get
			{
				return this._relationCollection;
			}
		}

		/// <summary>Gets a value indicating whether <see cref="P:System.Data.DataSet.Relations" /> property should be persisted.</summary>
		/// <returns>
		///   <see langword="true" /> if the property value has been changed from its default; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600084F RID: 2127 RVA: 0x00006D61 File Offset: 0x00004F61
		protected virtual bool ShouldSerializeRelations()
		{
			return true;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0001CE5E File Offset: 0x0001B05E
		private void ResetRelations()
		{
			this.Relations.Clear();
		}

		/// <summary>Gets the collection of tables contained in the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>The <see cref="T:System.Data.DataTableCollection" /> contained by this <see cref="T:System.Data.DataSet" />. An empty collection is returned if no <see cref="T:System.Data.DataTable" /> objects exist.</returns>
		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x0001CE6B File Offset: 0x0001B06B
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DataTableCollection Tables
		{
			get
			{
				return this._tableCollection;
			}
		}

		/// <summary>Gets a value indicating whether <see cref="P:System.Data.DataSet.Tables" /> property should be persisted.</summary>
		/// <returns>
		///   <see langword="true" /> if the property value has been changed from its default; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000852 RID: 2130 RVA: 0x00006D61 File Offset: 0x00004F61
		protected virtual bool ShouldSerializeTables()
		{
			return true;
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0001CE73 File Offset: 0x0001B073
		private void ResetTables()
		{
			this.Tables.Clear();
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x0001CE80 File Offset: 0x0001B080
		// (set) Token: 0x06000855 RID: 2133 RVA: 0x0001CE88 File Offset: 0x0001B088
		internal bool FBoundToDocument
		{
			get
			{
				return this._fBoundToDocument;
			}
			set
			{
				this._fBoundToDocument = value;
			}
		}

		/// <summary>Commits all the changes made to this <see cref="T:System.Data.DataSet" /> since it was loaded or since the last time <see cref="M:System.Data.DataSet.AcceptChanges" /> was called.</summary>
		// Token: 0x06000856 RID: 2134 RVA: 0x0001CE94 File Offset: 0x0001B094
		public void AcceptChanges()
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.AcceptChanges|API> {0}", this.ObjectID);
			try
			{
				for (int i = 0; i < this.Tables.Count; i++)
				{
					this.Tables[i].AcceptChanges();
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000857 RID: 2135 RVA: 0x0001CEFC File Offset: 0x0001B0FC
		// (remove) Token: 0x06000858 RID: 2136 RVA: 0x0001CF34 File Offset: 0x0001B134
		internal event PropertyChangedEventHandler PropertyChanging
		{
			[CompilerGenerated]
			add
			{
				PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanging;
				PropertyChangedEventHandler propertyChangedEventHandler2;
				do
				{
					propertyChangedEventHandler2 = propertyChangedEventHandler;
					PropertyChangedEventHandler value2 = (PropertyChangedEventHandler)Delegate.Combine(propertyChangedEventHandler2, value);
					propertyChangedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.PropertyChanging, value2, propertyChangedEventHandler2);
				}
				while (propertyChangedEventHandler != propertyChangedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				PropertyChangedEventHandler propertyChangedEventHandler = this.PropertyChanging;
				PropertyChangedEventHandler propertyChangedEventHandler2;
				do
				{
					propertyChangedEventHandler2 = propertyChangedEventHandler;
					PropertyChangedEventHandler value2 = (PropertyChangedEventHandler)Delegate.Remove(propertyChangedEventHandler2, value);
					propertyChangedEventHandler = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.PropertyChanging, value2, propertyChangedEventHandler2);
				}
				while (propertyChangedEventHandler != propertyChangedEventHandler2);
			}
		}

		/// <summary>Occurs when a target and source <see cref="T:System.Data.DataRow" /> have the same primary key value, and <see cref="P:System.Data.DataSet.EnforceConstraints" /> is set to true.</summary>
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000859 RID: 2137 RVA: 0x0001CF6C File Offset: 0x0001B16C
		// (remove) Token: 0x0600085A RID: 2138 RVA: 0x0001CFA4 File Offset: 0x0001B1A4
		public event MergeFailedEventHandler MergeFailed
		{
			[CompilerGenerated]
			add
			{
				MergeFailedEventHandler mergeFailedEventHandler = this.MergeFailed;
				MergeFailedEventHandler mergeFailedEventHandler2;
				do
				{
					mergeFailedEventHandler2 = mergeFailedEventHandler;
					MergeFailedEventHandler value2 = (MergeFailedEventHandler)Delegate.Combine(mergeFailedEventHandler2, value);
					mergeFailedEventHandler = Interlocked.CompareExchange<MergeFailedEventHandler>(ref this.MergeFailed, value2, mergeFailedEventHandler2);
				}
				while (mergeFailedEventHandler != mergeFailedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				MergeFailedEventHandler mergeFailedEventHandler = this.MergeFailed;
				MergeFailedEventHandler mergeFailedEventHandler2;
				do
				{
					mergeFailedEventHandler2 = mergeFailedEventHandler;
					MergeFailedEventHandler value2 = (MergeFailedEventHandler)Delegate.Remove(mergeFailedEventHandler2, value);
					mergeFailedEventHandler = Interlocked.CompareExchange<MergeFailedEventHandler>(ref this.MergeFailed, value2, mergeFailedEventHandler2);
				}
				while (mergeFailedEventHandler != mergeFailedEventHandler2);
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600085B RID: 2139 RVA: 0x0001CFDC File Offset: 0x0001B1DC
		// (remove) Token: 0x0600085C RID: 2140 RVA: 0x0001D014 File Offset: 0x0001B214
		internal event DataRowCreatedEventHandler DataRowCreated
		{
			[CompilerGenerated]
			add
			{
				DataRowCreatedEventHandler dataRowCreatedEventHandler = this.DataRowCreated;
				DataRowCreatedEventHandler dataRowCreatedEventHandler2;
				do
				{
					dataRowCreatedEventHandler2 = dataRowCreatedEventHandler;
					DataRowCreatedEventHandler value2 = (DataRowCreatedEventHandler)Delegate.Combine(dataRowCreatedEventHandler2, value);
					dataRowCreatedEventHandler = Interlocked.CompareExchange<DataRowCreatedEventHandler>(ref this.DataRowCreated, value2, dataRowCreatedEventHandler2);
				}
				while (dataRowCreatedEventHandler != dataRowCreatedEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				DataRowCreatedEventHandler dataRowCreatedEventHandler = this.DataRowCreated;
				DataRowCreatedEventHandler dataRowCreatedEventHandler2;
				do
				{
					dataRowCreatedEventHandler2 = dataRowCreatedEventHandler;
					DataRowCreatedEventHandler value2 = (DataRowCreatedEventHandler)Delegate.Remove(dataRowCreatedEventHandler2, value);
					dataRowCreatedEventHandler = Interlocked.CompareExchange<DataRowCreatedEventHandler>(ref this.DataRowCreated, value2, dataRowCreatedEventHandler2);
				}
				while (dataRowCreatedEventHandler != dataRowCreatedEventHandler2);
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600085D RID: 2141 RVA: 0x0001D04C File Offset: 0x0001B24C
		// (remove) Token: 0x0600085E RID: 2142 RVA: 0x0001D084 File Offset: 0x0001B284
		internal event DataSetClearEventhandler ClearFunctionCalled
		{
			[CompilerGenerated]
			add
			{
				DataSetClearEventhandler dataSetClearEventhandler = this.ClearFunctionCalled;
				DataSetClearEventhandler dataSetClearEventhandler2;
				do
				{
					dataSetClearEventhandler2 = dataSetClearEventhandler;
					DataSetClearEventhandler value2 = (DataSetClearEventhandler)Delegate.Combine(dataSetClearEventhandler2, value);
					dataSetClearEventhandler = Interlocked.CompareExchange<DataSetClearEventhandler>(ref this.ClearFunctionCalled, value2, dataSetClearEventhandler2);
				}
				while (dataSetClearEventhandler != dataSetClearEventhandler2);
			}
			[CompilerGenerated]
			remove
			{
				DataSetClearEventhandler dataSetClearEventhandler = this.ClearFunctionCalled;
				DataSetClearEventhandler dataSetClearEventhandler2;
				do
				{
					dataSetClearEventhandler2 = dataSetClearEventhandler;
					DataSetClearEventhandler value2 = (DataSetClearEventhandler)Delegate.Remove(dataSetClearEventhandler2, value);
					dataSetClearEventhandler = Interlocked.CompareExchange<DataSetClearEventhandler>(ref this.ClearFunctionCalled, value2, dataSetClearEventhandler2);
				}
				while (dataSetClearEventhandler != dataSetClearEventhandler2);
			}
		}

		/// <summary>Occurs after the <see cref="T:System.Data.DataSet" /> is initialized.</summary>
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600085F RID: 2143 RVA: 0x0001D0BC File Offset: 0x0001B2BC
		// (remove) Token: 0x06000860 RID: 2144 RVA: 0x0001D0F4 File Offset: 0x0001B2F4
		public event EventHandler Initialized
		{
			[CompilerGenerated]
			add
			{
				EventHandler eventHandler = this.Initialized;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.Initialized, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler eventHandler = this.Initialized;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.Initialized, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		/// <summary>Begins the initialization of a <see cref="T:System.Data.DataSet" /> that is used on a form or used by another component. The initialization occurs at run time.</summary>
		// Token: 0x06000861 RID: 2145 RVA: 0x0001D129 File Offset: 0x0001B329
		public void BeginInit()
		{
			this._fInitInProgress = true;
		}

		/// <summary>Ends the initialization of a <see cref="T:System.Data.DataSet" /> that is used on a form or used by another component. The initialization occurs at run time.</summary>
		// Token: 0x06000862 RID: 2146 RVA: 0x0001D134 File Offset: 0x0001B334
		public void EndInit()
		{
			this.Tables.FinishInitCollection();
			for (int i = 0; i < this.Tables.Count; i++)
			{
				this.Tables[i].Columns.FinishInitCollection();
			}
			for (int j = 0; j < this.Tables.Count; j++)
			{
				this.Tables[j].Constraints.FinishInitConstraints();
			}
			((DataRelationCollection.DataSetRelationCollection)this.Relations).FinishInitRelations();
			this._fInitInProgress = false;
			this.OnInitialized();
		}

		/// <summary>Clears the <see cref="T:System.Data.DataSet" /> of any data by removing all rows in all tables.</summary>
		// Token: 0x06000863 RID: 2147 RVA: 0x0001D1C4 File Offset: 0x0001B3C4
		public void Clear()
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.Clear|API> {0}", this.ObjectID);
			try
			{
				this.OnClearFunctionCalled(null);
				bool enforceConstraints = this.EnforceConstraints;
				this.EnforceConstraints = false;
				for (int i = 0; i < this.Tables.Count; i++)
				{
					this.Tables[i].Clear();
				}
				this.EnforceConstraints = enforceConstraints;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
		}

		/// <summary>Copies the structure of the <see cref="T:System.Data.DataSet" />, including all <see cref="T:System.Data.DataTable" /> schemas, relations, and constraints. Does not copy any data.</summary>
		/// <returns>A new <see cref="T:System.Data.DataSet" /> with the same schema as the current <see cref="T:System.Data.DataSet" />, but none of the data.</returns>
		// Token: 0x06000864 RID: 2148 RVA: 0x0001D248 File Offset: 0x0001B448
		[MethodImpl(MethodImplOptions.NoInlining)]
		public virtual DataSet Clone()
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.Clone|API> {0}", this.ObjectID);
			DataSet result;
			try
			{
				DataSet dataSet = (DataSet)Activator.CreateInstance(base.GetType(), true);
				if (dataSet.Tables.Count > 0)
				{
					dataSet.Reset();
				}
				dataSet.DataSetName = this.DataSetName;
				dataSet.CaseSensitive = this.CaseSensitive;
				dataSet._culture = this._culture;
				dataSet._cultureUserSet = this._cultureUserSet;
				dataSet.EnforceConstraints = this.EnforceConstraints;
				dataSet.Namespace = this.Namespace;
				dataSet.Prefix = this.Prefix;
				dataSet.RemotingFormat = this.RemotingFormat;
				dataSet._fIsSchemaLoading = true;
				DataTableCollection tables = this.Tables;
				for (int i = 0; i < tables.Count; i++)
				{
					DataTable dataTable = tables[i].Clone(dataSet);
					dataTable._tableNamespace = tables[i].Namespace;
					dataSet.Tables.Add(dataTable);
				}
				for (int j = 0; j < tables.Count; j++)
				{
					ConstraintCollection constraints = tables[j].Constraints;
					for (int k = 0; k < constraints.Count; k++)
					{
						if (!(constraints[k] is UniqueConstraint))
						{
							ForeignKeyConstraint foreignKeyConstraint = constraints[k] as ForeignKeyConstraint;
							if (foreignKeyConstraint.Table != foreignKeyConstraint.RelatedTable)
							{
								dataSet.Tables[j].Constraints.Add(constraints[k].Clone(dataSet));
							}
						}
					}
				}
				DataRelationCollection relations = this.Relations;
				for (int l = 0; l < relations.Count; l++)
				{
					DataRelation dataRelation = relations[l].Clone(dataSet);
					dataRelation.CheckMultipleNested = false;
					dataSet.Relations.Add(dataRelation);
					dataRelation.CheckMultipleNested = true;
				}
				if (this._extendedProperties != null)
				{
					foreach (object key in this._extendedProperties.Keys)
					{
						dataSet.ExtendedProperties[key] = this._extendedProperties[key];
					}
				}
				foreach (object obj in this.Tables)
				{
					DataTable dataTable2 = (DataTable)obj;
					foreach (object obj2 in dataTable2.Columns)
					{
						DataColumn dataColumn = (DataColumn)obj2;
						if (dataColumn.Expression.Length != 0)
						{
							dataSet.Tables[dataTable2.TableName, dataTable2.Namespace].Columns[dataColumn.ColumnName].Expression = dataColumn.Expression;
						}
					}
				}
				for (int m = 0; m < tables.Count; m++)
				{
					dataSet.Tables[m]._tableNamespace = tables[m]._tableNamespace;
				}
				dataSet._fIsSchemaLoading = false;
				result = dataSet;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
			return result;
		}

		/// <summary>Copies both the structure and data for this <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>A new <see cref="T:System.Data.DataSet" /> with the same structure (table schemas, relations, and constraints) and data as this <see cref="T:System.Data.DataSet" />.  
		///
		///  If these classes have been subclassed, the copy will also be of the same subclasses.</returns>
		// Token: 0x06000865 RID: 2149 RVA: 0x0001D5FC File Offset: 0x0001B7FC
		public DataSet Copy()
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.Copy|API> {0}", this.ObjectID);
			DataSet result;
			try
			{
				DataSet dataSet = this.Clone();
				bool enforceConstraints = dataSet.EnforceConstraints;
				dataSet.EnforceConstraints = false;
				foreach (object obj in this.Tables)
				{
					DataTable dataTable = (DataTable)obj;
					DataTable table = dataSet.Tables[dataTable.TableName, dataTable.Namespace];
					foreach (object obj2 in dataTable.Rows)
					{
						DataRow row = (DataRow)obj2;
						dataTable.CopyRow(table, row);
					}
				}
				dataSet.EnforceConstraints = enforceConstraints;
				result = dataSet;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
			return result;
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0001D714 File Offset: 0x0001B914
		internal int EstimatedXmlStringSize()
		{
			int num = 100;
			for (int i = 0; i < this.Tables.Count; i++)
			{
				int num2 = this.Tables[i].TableName.Length + 4 << 2;
				DataTable dataTable = this.Tables[i];
				for (int j = 0; j < dataTable.Columns.Count; j++)
				{
					num2 += dataTable.Columns[j].ColumnName.Length + 4 << 2;
					num2 += 20;
				}
				num += dataTable.Rows.Count * num2;
			}
			return num;
		}

		/// <summary>Gets a copy of the <see cref="T:System.Data.DataSet" /> that contains all changes made to it since it was loaded or since <see cref="M:System.Data.DataSet.AcceptChanges" /> was last called.</summary>
		/// <returns>A copy of the changes from this <see cref="T:System.Data.DataSet" /> that can have actions performed on it and later be merged back in using <see cref="M:System.Data.DataSet.Merge(System.Data.DataSet)" />. If no changed rows are found, the method returns <see langword="null" />.</returns>
		// Token: 0x06000867 RID: 2151 RVA: 0x0001D7B3 File Offset: 0x0001B9B3
		public DataSet GetChanges()
		{
			return this.GetChanges(DataRowState.Added | DataRowState.Deleted | DataRowState.Modified);
		}

		/// <summary>Gets a copy of the <see cref="T:System.Data.DataSet" /> containing all changes made to it since it was last loaded, or since <see cref="M:System.Data.DataSet.AcceptChanges" /> was called, filtered by <see cref="T:System.Data.DataRowState" />.</summary>
		/// <param name="rowStates">One of the <see cref="T:System.Data.DataRowState" /> values.</param>
		/// <returns>A filtered copy of the <see cref="T:System.Data.DataSet" /> that can have actions performed on it, and subsequently be merged back in using <see cref="M:System.Data.DataSet.Merge(System.Data.DataSet)" />. If no rows of the desired <see cref="T:System.Data.DataRowState" /> are found, the method returns <see langword="null" />.</returns>
		// Token: 0x06000868 RID: 2152 RVA: 0x0001D7C0 File Offset: 0x0001B9C0
		public DataSet GetChanges(DataRowState rowStates)
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int, DataRowState>("<ds.DataSet.GetChanges|API> {0}, rowStates={1}", this.ObjectID, rowStates);
			DataSet result;
			try
			{
				DataSet dataSet = null;
				bool enforceConstraints = false;
				if ((rowStates & ~(DataRowState.Unchanged | DataRowState.Added | DataRowState.Deleted | DataRowState.Modified)) != (DataRowState)0)
				{
					throw ExceptionBuilder.InvalidRowState(rowStates);
				}
				DataSet.TableChanges[] array = new DataSet.TableChanges[this.Tables.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new DataSet.TableChanges(this.Tables[i].Rows.Count);
				}
				this.MarkModifiedRows(array, rowStates);
				for (int j = 0; j < array.Length; j++)
				{
					if (0 < array[j].HasChanges)
					{
						if (dataSet == null)
						{
							dataSet = this.Clone();
							enforceConstraints = dataSet.EnforceConstraints;
							dataSet.EnforceConstraints = false;
						}
						DataTable dataTable = this.Tables[j];
						DataTable table = dataSet.Tables[dataTable.TableName, dataTable.Namespace];
						int num = 0;
						while (0 < array[j].HasChanges)
						{
							if (array[j][num])
							{
								dataTable.CopyRow(table, dataTable.Rows[num]);
								DataSet.TableChanges[] array2 = array;
								int num2 = j;
								int hasChanges = array2[num2].HasChanges;
								array2[num2].HasChanges = hasChanges - 1;
							}
							num++;
						}
					}
				}
				if (dataSet != null)
				{
					dataSet.EnforceConstraints = enforceConstraints;
				}
				result = dataSet;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
			return result;
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0001D948 File Offset: 0x0001BB48
		private void MarkModifiedRows(DataSet.TableChanges[] bitMatrix, DataRowState rowStates)
		{
			for (int i = 0; i < bitMatrix.Length; i++)
			{
				DataRowCollection rows = this.Tables[i].Rows;
				int count = rows.Count;
				for (int j = 0; j < count; j++)
				{
					DataRow dataRow = rows[j];
					DataRowState rowState = dataRow.RowState;
					if ((rowStates & rowState) != (DataRowState)0 && !bitMatrix[i][j])
					{
						bitMatrix[i][j] = true;
						if (DataRowState.Deleted != rowState)
						{
							this.MarkRelatedRowsAsModified(bitMatrix, dataRow);
						}
					}
				}
			}
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0001D9CC File Offset: 0x0001BBCC
		private void MarkRelatedRowsAsModified(DataSet.TableChanges[] bitMatrix, DataRow row)
		{
			DataRelationCollection parentRelations = row.Table.ParentRelations;
			int count = parentRelations.Count;
			for (int i = 0; i < count; i++)
			{
				foreach (DataRow dataRow in row.GetParentRows(parentRelations[i], DataRowVersion.Current))
				{
					int num = this.Tables.IndexOf(dataRow.Table);
					int index = dataRow.Table.Rows.IndexOf(dataRow);
					if (!bitMatrix[num][index])
					{
						bitMatrix[num][index] = true;
						if (DataRowState.Deleted != dataRow.RowState)
						{
							this.MarkRelatedRowsAsModified(bitMatrix, dataRow);
						}
					}
				}
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IListSource.GetList" />.</summary>
		/// <returns>For a description of this member, see <see cref="M:System.ComponentModel.IListSource.GetList" />.</returns>
		// Token: 0x0600086B RID: 2155 RVA: 0x0001DA86 File Offset: 0x0001BC86
		IList IListSource.GetList()
		{
			return this.DefaultViewManager;
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0001DA90 File Offset: 0x0001BC90
		internal string GetRemotingDiffGram(DataTable table)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			xmlTextWriter.Formatting = Formatting.Indented;
			if (stringWriter != null)
			{
				new NewDiffgramGen(table, false).Save(xmlTextWriter, table);
			}
			return stringWriter.ToString();
		}

		/// <summary>Returns the XML representation of the data stored in the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>A string that is a representation of the data stored in the <see cref="T:System.Data.DataSet" />.</returns>
		// Token: 0x0600086D RID: 2157 RVA: 0x0001DACC File Offset: 0x0001BCCC
		public string GetXml()
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.GetXml|API> {0}", this.ObjectID);
			string result;
			try
			{
				StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
				if (stringWriter != null)
				{
					XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
					xmlTextWriter.Formatting = Formatting.Indented;
					new XmlDataTreeWriter(this).Save(xmlTextWriter, false);
				}
				result = stringWriter.ToString();
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
			return result;
		}

		/// <summary>Returns the XML Schema for the XML representation of the data stored in the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>String that is the XML Schema for the XML representation of the data stored in the <see cref="T:System.Data.DataSet" />.</returns>
		// Token: 0x0600086E RID: 2158 RVA: 0x0001DB40 File Offset: 0x0001BD40
		public string GetXmlSchema()
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.GetXmlSchema|API> {0}", this.ObjectID);
			string result;
			try
			{
				StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
				XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
				xmlTextWriter.Formatting = Formatting.Indented;
				if (stringWriter != null)
				{
					new XmlTreeGen(SchemaFormat.Public).Save(this, xmlTextWriter);
				}
				result = stringWriter.ToString();
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
			return result;
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0001DBB0 File Offset: 0x0001BDB0
		internal string GetXmlSchemaForRemoting(DataTable table)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			xmlTextWriter.Formatting = Formatting.Indented;
			if (stringWriter != null)
			{
				if (table == null)
				{
					if (this.SchemaSerializationMode == SchemaSerializationMode.ExcludeSchema)
					{
						new XmlTreeGen(SchemaFormat.RemotingSkipSchema).Save(this, xmlTextWriter);
					}
					else
					{
						new XmlTreeGen(SchemaFormat.Remoting).Save(this, xmlTextWriter);
					}
				}
				else
				{
					new XmlTreeGen(SchemaFormat.Remoting).Save(table, xmlTextWriter);
				}
			}
			return stringWriter.ToString();
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Data.DataSet" /> has changes, including new, deleted, or modified rows.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Data.DataSet" /> has changes; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000870 RID: 2160 RVA: 0x0001DC14 File Offset: 0x0001BE14
		public bool HasChanges()
		{
			return this.HasChanges(DataRowState.Added | DataRowState.Deleted | DataRowState.Modified);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Data.DataSet" /> has changes, including new, deleted, or modified rows, filtered by <see cref="T:System.Data.DataRowState" />.</summary>
		/// <param name="rowStates">One of the <see cref="T:System.Data.DataRowState" /> values.</param>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Data.DataSet" /> has changes; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000871 RID: 2161 RVA: 0x0001DC20 File Offset: 0x0001BE20
		public bool HasChanges(DataRowState rowStates)
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int, int>("<ds.DataSet.HasChanges|API> {0}, rowStates={1}", this.ObjectID, (int)rowStates);
			bool result;
			try
			{
				if ((rowStates & ~(DataRowState.Detached | DataRowState.Unchanged | DataRowState.Added | DataRowState.Deleted | DataRowState.Modified)) != (DataRowState)0)
				{
					throw ExceptionBuilder.ArgumentOutOfRange("rowState");
				}
				for (int i = 0; i < this.Tables.Count; i++)
				{
					DataTable dataTable = this.Tables[i];
					for (int j = 0; j < dataTable.Rows.Count; j++)
					{
						if ((dataTable.Rows[j].RowState & rowStates) != (DataRowState)0)
						{
							return true;
						}
					}
				}
				result = false;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
			return result;
		}

		/// <summary>Applies the XML schema from the specified <see cref="T:System.Xml.XmlReader" /> to the <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="reader">The <see langword="XMLReader" /> from which to read the schema.</param>
		/// <param name="nsArray">An array of namespace Uniform Resource Identifier (URI) strings to be excluded from schema inference.</param>
		// Token: 0x06000872 RID: 2162 RVA: 0x0001DCCC File Offset: 0x0001BECC
		public void InferXmlSchema(XmlReader reader, string[] nsArray)
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.InferXmlSchema|API> {0}", this.ObjectID);
			try
			{
				if (reader != null)
				{
					XmlDocument xmlDocument = new XmlDocument();
					if (reader.NodeType == XmlNodeType.Element)
					{
						XmlNode newChild = xmlDocument.ReadNode(reader);
						xmlDocument.AppendChild(newChild);
					}
					else
					{
						xmlDocument.Load(reader);
					}
					if (xmlDocument.DocumentElement != null)
					{
						this.InferSchema(xmlDocument, nsArray, XmlReadMode.InferSchema);
					}
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
		}

		/// <summary>Applies the XML schema from the specified <see cref="T:System.IO.Stream" /> to the <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="stream">The <see langword="Stream" /> from which to read the schema.</param>
		/// <param name="nsArray">An array of namespace Uniform Resource Identifier (URI) strings to be excluded from schema inference.</param>
		// Token: 0x06000873 RID: 2163 RVA: 0x0001DD50 File Offset: 0x0001BF50
		public void InferXmlSchema(Stream stream, string[] nsArray)
		{
			if (stream == null)
			{
				return;
			}
			this.InferXmlSchema(new XmlTextReader(stream), nsArray);
		}

		/// <summary>Applies the XML schema from the specified <see cref="T:System.IO.TextReader" /> to the <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="reader">The <see langword="TextReader" /> from which to read the schema.</param>
		/// <param name="nsArray">An array of namespace Uniform Resource Identifier (URI) strings to be excluded from schema inference.</param>
		// Token: 0x06000874 RID: 2164 RVA: 0x0001DD63 File Offset: 0x0001BF63
		public void InferXmlSchema(TextReader reader, string[] nsArray)
		{
			if (reader == null)
			{
				return;
			}
			this.InferXmlSchema(new XmlTextReader(reader), nsArray);
		}

		/// <summary>Applies the XML schema from the specified file to the <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="fileName">The name of the file (including the path) from which to read the schema.</param>
		/// <param name="nsArray">An array of namespace Uniform Resource Identifier (URI) strings to be excluded from schema inference.</param>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="T:System.Security.Permissions.FileIOPermission" /> is not set to <see cref="F:System.Security.Permissions.FileIOPermissionAccess.Read" />.</exception>
		// Token: 0x06000875 RID: 2165 RVA: 0x0001DD78 File Offset: 0x0001BF78
		public void InferXmlSchema(string fileName, string[] nsArray)
		{
			XmlTextReader xmlTextReader = new XmlTextReader(fileName);
			try
			{
				this.InferXmlSchema(xmlTextReader, nsArray);
			}
			finally
			{
				xmlTextReader.Close();
			}
		}

		/// <summary>Reads the XML schema from the specified <see cref="T:System.Xml.XmlReader" /> into the <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> from which to read.</param>
		// Token: 0x06000876 RID: 2166 RVA: 0x0001DDB0 File Offset: 0x0001BFB0
		public void ReadXmlSchema(XmlReader reader)
		{
			this.ReadXmlSchema(reader, false);
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0001DDBC File Offset: 0x0001BFBC
		internal void ReadXmlSchema(XmlReader reader, bool denyResolving)
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int, bool>("<ds.DataSet.ReadXmlSchema|INFO> {0}, reader, denyResolving={1}", this.ObjectID, denyResolving);
			try
			{
				int depth = -1;
				if (reader != null)
				{
					if (reader is XmlTextReader)
					{
						((XmlTextReader)reader).WhitespaceHandling = WhitespaceHandling.None;
					}
					XmlDocument xmlDocument = new XmlDocument();
					if (reader.NodeType == XmlNodeType.Element)
					{
						depth = reader.Depth;
					}
					reader.MoveToContent();
					if (reader.NodeType == XmlNodeType.Element)
					{
						if (reader.LocalName == "Schema" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-data")
						{
							this.ReadXDRSchema(reader);
						}
						else if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
						{
							this.ReadXSDSchema(reader, denyResolving);
						}
						else
						{
							if (reader.LocalName == "schema" && reader.NamespaceURI.StartsWith("http://www.w3.org/", StringComparison.Ordinal))
							{
								throw ExceptionBuilder.DataSetUnsupportedSchema("http://www.w3.org/2001/XMLSchema");
							}
							XmlElement xmlElement = xmlDocument.CreateElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
							if (reader.HasAttributes)
							{
								int attributeCount = reader.AttributeCount;
								for (int i = 0; i < attributeCount; i++)
								{
									reader.MoveToAttribute(i);
									if (reader.NamespaceURI.Equals("http://www.w3.org/2000/xmlns/"))
									{
										xmlElement.SetAttribute(reader.Name, reader.GetAttribute(i));
									}
									else
									{
										XmlAttribute xmlAttribute = xmlElement.SetAttributeNode(reader.LocalName, reader.NamespaceURI);
										xmlAttribute.Prefix = reader.Prefix;
										xmlAttribute.Value = reader.GetAttribute(i);
									}
								}
							}
							reader.Read();
							while (this.MoveToElement(reader, depth))
							{
								if (reader.LocalName == "Schema" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-data")
								{
									this.ReadXDRSchema(reader);
									return;
								}
								if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
								{
									this.ReadXSDSchema(reader, denyResolving);
									return;
								}
								if (reader.LocalName == "schema" && reader.NamespaceURI.StartsWith("http://www.w3.org/", StringComparison.Ordinal))
								{
									throw ExceptionBuilder.DataSetUnsupportedSchema("http://www.w3.org/2001/XMLSchema");
								}
								XmlNode newChild = xmlDocument.ReadNode(reader);
								xmlElement.AppendChild(newChild);
							}
							this.ReadEndElement(reader);
							xmlDocument.AppendChild(xmlElement);
							this.InferSchema(xmlDocument, null, XmlReadMode.Auto);
						}
					}
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0001E048 File Offset: 0x0001C248
		internal bool MoveToElement(XmlReader reader, int depth)
		{
			while (!reader.EOF && reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.Element && reader.Depth > depth)
			{
				reader.Read();
			}
			return reader.NodeType == XmlNodeType.Element;
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0001E080 File Offset: 0x0001C280
		private static void MoveToElement(XmlReader reader)
		{
			while (!reader.EOF && reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.Element)
			{
				reader.Read();
			}
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0001E0A6 File Offset: 0x0001C2A6
		internal void ReadEndElement(XmlReader reader)
		{
			while (reader.NodeType == XmlNodeType.Whitespace)
			{
				reader.Skip();
			}
			if (reader.NodeType == XmlNodeType.None)
			{
				reader.Skip();
				return;
			}
			if (reader.NodeType == XmlNodeType.EndElement)
			{
				reader.ReadEndElement();
			}
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0001E0DC File Offset: 0x0001C2DC
		internal void ReadXSDSchema(XmlReader reader, bool denyResolving)
		{
			XmlSchemaSet xmlSchemaSet = new XmlSchemaSet();
			int num = 1;
			if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema" && reader.HasAttributes)
			{
				string attribute = reader.GetAttribute("schemafragmentcount", "urn:schemas-microsoft-com:xml-msdata");
				if (!string.IsNullOrEmpty(attribute))
				{
					num = int.Parse(attribute, null);
				}
			}
			while (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
			{
				XmlSchema schema = XmlSchema.Read(reader, null);
				xmlSchemaSet.Add(schema);
				this.ReadEndElement(reader);
				if (--num > 0)
				{
					DataSet.MoveToElement(reader);
				}
				while (reader.NodeType == XmlNodeType.Whitespace)
				{
					reader.Skip();
				}
			}
			xmlSchemaSet.Compile();
			new XSDSchema().LoadSchema(xmlSchemaSet, this);
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0001E1B0 File Offset: 0x0001C3B0
		internal void ReadXDRSchema(XmlReader reader)
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlNode xmlNode = xmlDocument.ReadNode(reader);
			xmlDocument.AppendChild(xmlNode);
			XDRSchema xdrschema = new XDRSchema(this, false);
			this.DataSetName = xmlDocument.DocumentElement.LocalName;
			xdrschema.LoadSchema((XmlElement)xmlNode, this);
		}

		/// <summary>Reads the XML schema from the specified <see cref="T:System.IO.Stream" /> into the <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> from which to read.</param>
		// Token: 0x0600087D RID: 2173 RVA: 0x0001E1F7 File Offset: 0x0001C3F7
		public void ReadXmlSchema(Stream stream)
		{
			if (stream == null)
			{
				return;
			}
			this.ReadXmlSchema(new XmlTextReader(stream), false);
		}

		/// <summary>Reads the XML schema from the specified <see cref="T:System.IO.TextReader" /> into the <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="reader">The <see cref="T:System.IO.TextReader" /> from which to read.</param>
		// Token: 0x0600087E RID: 2174 RVA: 0x0001E20A File Offset: 0x0001C40A
		public void ReadXmlSchema(TextReader reader)
		{
			if (reader == null)
			{
				return;
			}
			this.ReadXmlSchema(new XmlTextReader(reader), false);
		}

		/// <summary>Reads the XML schema from the specified file into the <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="fileName">The file name (including the path) from which to read.</param>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="T:System.Security.Permissions.FileIOPermission" /> is not set to <see cref="F:System.Security.Permissions.FileIOPermissionAccess.Read" />.</exception>
		// Token: 0x0600087F RID: 2175 RVA: 0x0001E220 File Offset: 0x0001C420
		public void ReadXmlSchema(string fileName)
		{
			XmlTextReader xmlTextReader = new XmlTextReader(fileName);
			try
			{
				this.ReadXmlSchema(xmlTextReader, false);
			}
			finally
			{
				xmlTextReader.Close();
			}
		}

		/// <summary>Writes the <see cref="T:System.Data.DataSet" /> structure as an XML schema to the specified <see cref="T:System.IO.Stream" /> object.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> object used to write to a file.</param>
		// Token: 0x06000880 RID: 2176 RVA: 0x0001E258 File Offset: 0x0001C458
		public void WriteXmlSchema(Stream stream)
		{
			this.WriteXmlSchema(stream, SchemaFormat.Public, null);
		}

		/// <summary>Writes the <see cref="T:System.Data.DataSet" /> structure as an XML schema to the specified <see cref="T:System.IO.Stream" /> object.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> object to write to.</param>
		/// <param name="multipleTargetConverter">A delegate used to convert <see cref="T:System.Type" /> to string.</param>
		// Token: 0x06000881 RID: 2177 RVA: 0x0001E263 File Offset: 0x0001C463
		public void WriteXmlSchema(Stream stream, Converter<Type, string> multipleTargetConverter)
		{
			ADP.CheckArgumentNull(multipleTargetConverter, "multipleTargetConverter");
			this.WriteXmlSchema(stream, SchemaFormat.Public, multipleTargetConverter);
		}

		/// <summary>Writes the <see cref="T:System.Data.DataSet" /> structure as an XML schema to a file.</summary>
		/// <param name="fileName">The file name (including the path) to which to write.</param>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="T:System.Security.Permissions.FileIOPermission" /> is not set to <see cref="F:System.Security.Permissions.FileIOPermissionAccess.Write" />.</exception>
		// Token: 0x06000882 RID: 2178 RVA: 0x0001E279 File Offset: 0x0001C479
		public void WriteXmlSchema(string fileName)
		{
			this.WriteXmlSchema(fileName, SchemaFormat.Public, null);
		}

		/// <summary>Writes the <see cref="T:System.Data.DataSet" /> structure as an XML schema to a file.</summary>
		/// <param name="fileName">The name of the file to write to.</param>
		/// <param name="multipleTargetConverter">A delegate used to convert <see cref="T:System.Type" /> to string.</param>
		// Token: 0x06000883 RID: 2179 RVA: 0x0001E284 File Offset: 0x0001C484
		public void WriteXmlSchema(string fileName, Converter<Type, string> multipleTargetConverter)
		{
			ADP.CheckArgumentNull(multipleTargetConverter, "multipleTargetConverter");
			this.WriteXmlSchema(fileName, SchemaFormat.Public, multipleTargetConverter);
		}

		/// <summary>Writes the <see cref="T:System.Data.DataSet" /> structure as an XML schema to the specified <see cref="T:System.IO.TextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> object with which to write.</param>
		// Token: 0x06000884 RID: 2180 RVA: 0x0001E29A File Offset: 0x0001C49A
		public void WriteXmlSchema(TextWriter writer)
		{
			this.WriteXmlSchema(writer, SchemaFormat.Public, null);
		}

		/// <summary>Writes the <see cref="T:System.Data.DataSet" /> structure as an XML schema to the specified <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="writer">A <see cref="T:System.IO.TextWriter" /> object to write to.</param>
		/// <param name="multipleTargetConverter">A delegate used to convert <see cref="T:System.Type" /> to string.</param>
		// Token: 0x06000885 RID: 2181 RVA: 0x0001E2A5 File Offset: 0x0001C4A5
		public void WriteXmlSchema(TextWriter writer, Converter<Type, string> multipleTargetConverter)
		{
			ADP.CheckArgumentNull(multipleTargetConverter, "multipleTargetConverter");
			this.WriteXmlSchema(writer, SchemaFormat.Public, multipleTargetConverter);
		}

		/// <summary>Writes the <see cref="T:System.Data.DataSet" /> structure as an XML schema to an <see cref="T:System.Xml.XmlWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> to write to.</param>
		// Token: 0x06000886 RID: 2182 RVA: 0x0001E2BB File Offset: 0x0001C4BB
		public void WriteXmlSchema(XmlWriter writer)
		{
			this.WriteXmlSchema(writer, SchemaFormat.Public, null);
		}

		/// <summary>Writes the <see cref="T:System.Data.DataSet" /> structure as an XML schema to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">A <see cref="T:System.Xml.XmlWriter" /> object to write to.</param>
		/// <param name="multipleTargetConverter">A delegate used to convert <see cref="T:System.Type" /> to string.</param>
		// Token: 0x06000887 RID: 2183 RVA: 0x0001E2C6 File Offset: 0x0001C4C6
		public void WriteXmlSchema(XmlWriter writer, Converter<Type, string> multipleTargetConverter)
		{
			ADP.CheckArgumentNull(multipleTargetConverter, "multipleTargetConverter");
			this.WriteXmlSchema(writer, SchemaFormat.Public, multipleTargetConverter);
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0001E2DC File Offset: 0x0001C4DC
		private void WriteXmlSchema(string fileName, SchemaFormat schemaFormat, Converter<Type, string> multipleTargetConverter)
		{
			XmlTextWriter xmlTextWriter = new XmlTextWriter(fileName, null);
			try
			{
				xmlTextWriter.Formatting = Formatting.Indented;
				xmlTextWriter.WriteStartDocument(true);
				this.WriteXmlSchema(xmlTextWriter, schemaFormat, multipleTargetConverter);
				xmlTextWriter.WriteEndDocument();
			}
			finally
			{
				xmlTextWriter.Close();
			}
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0001E328 File Offset: 0x0001C528
		private void WriteXmlSchema(Stream stream, SchemaFormat schemaFormat, Converter<Type, string> multipleTargetConverter)
		{
			if (stream == null)
			{
				return;
			}
			this.WriteXmlSchema(new XmlTextWriter(stream, null)
			{
				Formatting = Formatting.Indented
			}, schemaFormat, multipleTargetConverter);
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0001E354 File Offset: 0x0001C554
		private void WriteXmlSchema(TextWriter writer, SchemaFormat schemaFormat, Converter<Type, string> multipleTargetConverter)
		{
			if (writer == null)
			{
				return;
			}
			this.WriteXmlSchema(new XmlTextWriter(writer)
			{
				Formatting = Formatting.Indented
			}, schemaFormat, multipleTargetConverter);
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0001E37C File Offset: 0x0001C57C
		private void WriteXmlSchema(XmlWriter writer, SchemaFormat schemaFormat, Converter<Type, string> multipleTargetConverter)
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int, SchemaFormat>("<ds.DataSet.WriteXmlSchema|INFO> {0}, schemaFormat={1}", this.ObjectID, schemaFormat);
			try
			{
				if (writer != null)
				{
					XmlTreeGen xmlTreeGen;
					if (schemaFormat == SchemaFormat.WebService && this.SchemaSerializationMode == SchemaSerializationMode.ExcludeSchema && writer.WriteState == WriteState.Element)
					{
						xmlTreeGen = new XmlTreeGen(SchemaFormat.WebServiceSkipSchema);
					}
					else
					{
						xmlTreeGen = new XmlTreeGen(schemaFormat);
					}
					xmlTreeGen.Save(this, null, writer, false, multipleTargetConverter);
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
		}

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> from which to read.</param>
		/// <returns>The <see langword="XmlReadMode" /> used to read the data.</returns>
		// Token: 0x0600088C RID: 2188 RVA: 0x0001E3F4 File Offset: 0x0001C5F4
		public XmlReadMode ReadXml(XmlReader reader)
		{
			return this.ReadXml(reader, false);
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0001E400 File Offset: 0x0001C600
		internal XmlReadMode ReadXml(XmlReader reader, bool denyResolving)
		{
			IDisposable disposable = null;
			long scopeId = DataCommonEventSource.Log.EnterScope<int, bool>("<ds.DataSet.ReadXml|INFO> {0}, denyResolving={1}", this.ObjectID, denyResolving);
			XmlReadMode result;
			try
			{
				disposable = TypeLimiter.EnterRestrictedScope(this);
				DataTable.DSRowDiffIdUsageSection dsrowDiffIdUsageSection = default(DataTable.DSRowDiffIdUsageSection);
				try
				{
					bool flag = false;
					bool flag2 = false;
					bool flag3 = false;
					bool isXdr = false;
					int depth = -1;
					XmlReadMode xmlReadMode = XmlReadMode.Auto;
					bool flag4 = false;
					bool flag5 = false;
					dsrowDiffIdUsageSection.Prepare(this);
					if (reader == null)
					{
						result = xmlReadMode;
					}
					else
					{
						if (this.Tables.Count == 0)
						{
							flag4 = true;
						}
						if (reader is XmlTextReader)
						{
							((XmlTextReader)reader).WhitespaceHandling = WhitespaceHandling.Significant;
						}
						XmlDocument xmlDocument = new XmlDocument();
						XmlDataLoader xmlDataLoader = null;
						reader.MoveToContent();
						if (reader.NodeType == XmlNodeType.Element)
						{
							depth = reader.Depth;
						}
						if (reader.NodeType == XmlNodeType.Element)
						{
							if (reader.LocalName == "diffgram" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
							{
								this.ReadXmlDiffgram(reader);
								this.ReadEndElement(reader);
								return XmlReadMode.DiffGram;
							}
							if (reader.LocalName == "Schema" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-data")
							{
								this.ReadXDRSchema(reader);
								return XmlReadMode.ReadSchema;
							}
							if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
							{
								this.ReadXSDSchema(reader, denyResolving);
								return XmlReadMode.ReadSchema;
							}
							if (reader.LocalName == "schema" && reader.NamespaceURI.StartsWith("http://www.w3.org/", StringComparison.Ordinal))
							{
								throw ExceptionBuilder.DataSetUnsupportedSchema("http://www.w3.org/2001/XMLSchema");
							}
							XmlElement xmlElement = xmlDocument.CreateElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
							if (reader.HasAttributes)
							{
								int attributeCount = reader.AttributeCount;
								for (int i = 0; i < attributeCount; i++)
								{
									reader.MoveToAttribute(i);
									if (reader.NamespaceURI.Equals("http://www.w3.org/2000/xmlns/"))
									{
										xmlElement.SetAttribute(reader.Name, reader.GetAttribute(i));
									}
									else
									{
										XmlAttribute xmlAttribute = xmlElement.SetAttributeNode(reader.LocalName, reader.NamespaceURI);
										xmlAttribute.Prefix = reader.Prefix;
										xmlAttribute.Value = reader.GetAttribute(i);
									}
								}
							}
							reader.Read();
							string value = reader.Value;
							while (this.MoveToElement(reader, depth))
							{
								if (reader.LocalName == "diffgram" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
								{
									this.ReadXmlDiffgram(reader);
									xmlReadMode = XmlReadMode.DiffGram;
								}
								if (!flag2 && !flag && reader.LocalName == "Schema" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-data")
								{
									this.ReadXDRSchema(reader);
									flag2 = true;
									isXdr = true;
								}
								else if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
								{
									this.ReadXSDSchema(reader, denyResolving);
									flag2 = true;
								}
								else
								{
									if (reader.LocalName == "schema" && reader.NamespaceURI.StartsWith("http://www.w3.org/", StringComparison.Ordinal))
									{
										throw ExceptionBuilder.DataSetUnsupportedSchema("http://www.w3.org/2001/XMLSchema");
									}
									if (reader.LocalName == "diffgram" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
									{
										this.ReadXmlDiffgram(reader);
										flag3 = true;
										xmlReadMode = XmlReadMode.DiffGram;
									}
									else
									{
										while (!reader.EOF && reader.NodeType == XmlNodeType.Whitespace)
										{
											reader.Read();
										}
										if (reader.NodeType == XmlNodeType.Element)
										{
											flag = true;
											if (!flag2 && this.Tables.Count == 0)
											{
												XmlNode newChild = xmlDocument.ReadNode(reader);
												xmlElement.AppendChild(newChild);
											}
											else
											{
												if (xmlDataLoader == null)
												{
													xmlDataLoader = new XmlDataLoader(this, isXdr, xmlElement, false);
												}
												xmlDataLoader.LoadData(reader);
												flag5 = true;
												if (flag2)
												{
													xmlReadMode = XmlReadMode.ReadSchema;
												}
												else
												{
													xmlReadMode = XmlReadMode.IgnoreSchema;
												}
											}
										}
									}
								}
							}
							this.ReadEndElement(reader);
							bool flag6 = false;
							bool fTopLevelTable = this._fTopLevelTable;
							if (!flag2 && this.Tables.Count == 0 && !xmlElement.HasChildNodes)
							{
								this._fTopLevelTable = true;
								flag6 = true;
								if (value != null && value.Length > 0)
								{
									xmlElement.InnerText = value;
								}
							}
							if (!flag4 && value != null && value.Length > 0)
							{
								xmlElement.InnerText = value;
							}
							xmlDocument.AppendChild(xmlElement);
							if (xmlDataLoader == null)
							{
								xmlDataLoader = new XmlDataLoader(this, isXdr, xmlElement, false);
							}
							if (!flag4 && !flag5)
							{
								XmlElement documentElement = xmlDocument.DocumentElement;
								if (documentElement.ChildNodes.Count == 0 || (documentElement.ChildNodes.Count == 1 && documentElement.FirstChild.GetType() == typeof(XmlText)))
								{
									bool fTopLevelTable2 = this._fTopLevelTable;
									if (this.DataSetName != documentElement.Name && this._namespaceURI != documentElement.NamespaceURI && this.Tables.Contains(documentElement.Name, (documentElement.NamespaceURI.Length == 0) ? null : documentElement.NamespaceURI, false, true))
									{
										this._fTopLevelTable = true;
									}
									try
									{
										xmlDataLoader.LoadData(xmlDocument);
									}
									finally
									{
										this._fTopLevelTable = fTopLevelTable2;
									}
								}
							}
							if (!flag3)
							{
								if (!flag2 && this.Tables.Count == 0)
								{
									this.InferSchema(xmlDocument, null, XmlReadMode.Auto);
									xmlReadMode = XmlReadMode.InferSchema;
									xmlDataLoader.FromInference = true;
									try
									{
										xmlDataLoader.LoadData(xmlDocument);
									}
									finally
									{
										xmlDataLoader.FromInference = false;
									}
								}
								if (flag6)
								{
									this._fTopLevelTable = fTopLevelTable;
								}
							}
						}
						result = xmlReadMode;
					}
				}
				finally
				{
				}
			}
			finally
			{
				if (disposable != null)
				{
					disposable.Dispose();
				}
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
			return result;
		}

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="stream">An object that derives from <see cref="T:System.IO.Stream" />.</param>
		/// <returns>The <see cref="T:System.Data.XmlReadMode" /> used to read the data.</returns>
		// Token: 0x0600088E RID: 2190 RVA: 0x0001E9D0 File Offset: 0x0001CBD0
		public XmlReadMode ReadXml(Stream stream)
		{
			if (stream == null)
			{
				return XmlReadMode.Auto;
			}
			return this.ReadXml(new XmlTextReader(stream)
			{
				XmlResolver = null
			}, false);
		}

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.IO.TextReader" />.</summary>
		/// <param name="reader">The <see langword="TextReader" /> from which to read the schema and data.</param>
		/// <returns>The <see cref="T:System.Data.XmlReadMode" /> used to read the data.</returns>
		// Token: 0x0600088F RID: 2191 RVA: 0x0001E9F8 File Offset: 0x0001CBF8
		public XmlReadMode ReadXml(TextReader reader)
		{
			if (reader == null)
			{
				return XmlReadMode.Auto;
			}
			return this.ReadXml(new XmlTextReader(reader)
			{
				XmlResolver = null
			}, false);
		}

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataSet" /> using the specified file.</summary>
		/// <param name="fileName">The filename (including the path) from which to read.</param>
		/// <returns>The <see langword="XmlReadMode" /> used to read the data.</returns>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="T:System.Security.Permissions.FileIOPermission" /> is not set to <see cref="F:System.Security.Permissions.FileIOPermissionAccess.Read" />.</exception>
		// Token: 0x06000890 RID: 2192 RVA: 0x0001EA20 File Offset: 0x0001CC20
		public XmlReadMode ReadXml(string fileName)
		{
			XmlTextReader xmlTextReader = new XmlTextReader(fileName);
			xmlTextReader.XmlResolver = null;
			XmlReadMode result;
			try
			{
				result = this.ReadXml(xmlTextReader, false);
			}
			finally
			{
				xmlTextReader.Close();
			}
			return result;
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0001EA60 File Offset: 0x0001CC60
		internal void InferSchema(XmlDocument xdoc, string[] excludedNamespaces, XmlReadMode mode)
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int, XmlReadMode>("<ds.DataSet.InferSchema|INFO> {0}, mode={1}", this.ObjectID, mode);
			try
			{
				string namespaceURI = xdoc.DocumentElement.NamespaceURI;
				if (excludedNamespaces == null)
				{
					excludedNamespaces = Array.Empty<string>();
				}
				XmlNodeReader instanceDocument = new XmlIgnoreNamespaceReader(xdoc, excludedNamespaces);
				XmlSchemaSet xmlSchemaSet = new XmlSchemaInference
				{
					Occurrence = XmlSchemaInference.InferenceOption.Relaxed,
					TypeInference = ((mode == XmlReadMode.InferTypedSchema) ? XmlSchemaInference.InferenceOption.Restricted : XmlSchemaInference.InferenceOption.Relaxed)
				}.InferSchema(instanceDocument);
				xmlSchemaSet.Compile();
				XSDSchema xsdschema = new XSDSchema();
				xsdschema.FromInference = true;
				try
				{
					xsdschema.LoadSchema(xmlSchemaSet, this);
				}
				finally
				{
					xsdschema.FromInference = false;
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0001EB10 File Offset: 0x0001CD10
		private bool IsEmpty()
		{
			using (IEnumerator enumerator = this.Tables.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((DataTable)enumerator.Current).Rows.Count > 0)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0001EB78 File Offset: 0x0001CD78
		private void ReadXmlDiffgram(XmlReader reader)
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.ReadXmlDiffgram|INFO> {0}", this.ObjectID);
			try
			{
				int depth = reader.Depth;
				bool enforceConstraints = this.EnforceConstraints;
				this.EnforceConstraints = false;
				bool flag = this.IsEmpty();
				DataSet dataSet;
				if (flag)
				{
					dataSet = this;
				}
				else
				{
					dataSet = this.Clone();
					dataSet.EnforceConstraints = false;
				}
				foreach (object obj in dataSet.Tables)
				{
					((DataTable)obj).Rows._nullInList = 0;
				}
				reader.MoveToContent();
				if (!(reader.LocalName != "diffgram") || !(reader.NamespaceURI != "urn:schemas-microsoft-com:xml-diffgram-v1"))
				{
					reader.Read();
					if (reader.NodeType == XmlNodeType.Whitespace)
					{
						this.MoveToElement(reader, reader.Depth - 1);
					}
					dataSet._fInLoadDiffgram = true;
					if (reader.Depth > depth)
					{
						if (reader.NamespaceURI != "urn:schemas-microsoft-com:xml-diffgram-v1" && reader.NamespaceURI != "urn:schemas-microsoft-com:xml-msdata")
						{
							XmlElement topNode = new XmlDocument().CreateElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
							reader.Read();
							if (reader.NodeType == XmlNodeType.Whitespace)
							{
								this.MoveToElement(reader, reader.Depth - 1);
							}
							if (reader.Depth - 1 > depth)
							{
								new XmlDataLoader(dataSet, false, topNode, false)
								{
									_isDiffgram = true
								}.LoadData(reader);
							}
							this.ReadEndElement(reader);
							if (reader.NodeType == XmlNodeType.Whitespace)
							{
								this.MoveToElement(reader, reader.Depth - 1);
							}
						}
						if ((reader.LocalName == "before" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1") || (reader.LocalName == "errors" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1"))
						{
							new XMLDiffLoader().LoadDiffGram(dataSet, reader);
						}
						while (reader.Depth > depth)
						{
							reader.Read();
						}
						this.ReadEndElement(reader);
					}
					foreach (object obj2 in dataSet.Tables)
					{
						DataTable dataTable = (DataTable)obj2;
						if (dataTable.Rows._nullInList > 0)
						{
							throw ExceptionBuilder.RowInsertMissing(dataTable.TableName);
						}
					}
					dataSet._fInLoadDiffgram = false;
					foreach (object obj3 in dataSet.Tables)
					{
						DataTable dataTable2 = (DataTable)obj3;
						DataRelation[] nestedParentRelations = dataTable2.NestedParentRelations;
						DataRelation[] array = nestedParentRelations;
						for (int i = 0; i < array.Length; i++)
						{
							if (array[i].ParentTable == dataTable2)
							{
								foreach (object obj4 in dataTable2.Rows)
								{
									DataRow dataRow = (DataRow)obj4;
									foreach (DataRelation rel in nestedParentRelations)
									{
										dataRow.CheckForLoops(rel);
									}
								}
							}
						}
					}
					if (!flag)
					{
						this.Merge(dataSet);
						if (this._dataSetName == "NewDataSet")
						{
							this._dataSetName = dataSet._dataSetName;
						}
						dataSet.EnforceConstraints = enforceConstraints;
					}
					this.EnforceConstraints = enforceConstraints;
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
		}

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.Xml.XmlReader" /> and <see cref="T:System.Data.XmlReadMode" />.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> from which to read.</param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlReadMode" /> values.</param>
		/// <returns>The <see langword="XmlReadMode" /> used to read the data.</returns>
		// Token: 0x06000894 RID: 2196 RVA: 0x0001EF8C File Offset: 0x0001D18C
		public XmlReadMode ReadXml(XmlReader reader, XmlReadMode mode)
		{
			return this.ReadXml(reader, mode, false);
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0001EF98 File Offset: 0x0001D198
		internal XmlReadMode ReadXml(XmlReader reader, XmlReadMode mode, bool denyResolving)
		{
			IDisposable disposable = null;
			long scopeId = DataCommonEventSource.Log.EnterScope<int, XmlReadMode, bool>("<ds.DataSet.ReadXml|INFO> {0}, mode={1}, denyResolving={2}", this.ObjectID, mode, denyResolving);
			XmlReadMode result;
			try
			{
				disposable = TypeLimiter.EnterRestrictedScope(this);
				XmlReadMode xmlReadMode = mode;
				if (reader == null)
				{
					result = xmlReadMode;
				}
				else if (mode == XmlReadMode.Auto)
				{
					result = this.ReadXml(reader);
				}
				else
				{
					DataTable.DSRowDiffIdUsageSection dsrowDiffIdUsageSection = default(DataTable.DSRowDiffIdUsageSection);
					try
					{
						bool flag = false;
						bool flag2 = false;
						bool isXdr = false;
						int depth = -1;
						dsrowDiffIdUsageSection.Prepare(this);
						if (reader is XmlTextReader)
						{
							((XmlTextReader)reader).WhitespaceHandling = WhitespaceHandling.Significant;
						}
						XmlDocument xmlDocument = new XmlDocument();
						if (mode != XmlReadMode.Fragment && reader.NodeType == XmlNodeType.Element)
						{
							depth = reader.Depth;
						}
						reader.MoveToContent();
						XmlDataLoader xmlDataLoader = null;
						if (reader.NodeType == XmlNodeType.Element)
						{
							XmlElement xmlElement;
							if (mode == XmlReadMode.Fragment)
							{
								xmlDocument.AppendChild(xmlDocument.CreateElement("ds_sqlXmlWraPPeR"));
								xmlElement = xmlDocument.DocumentElement;
							}
							else
							{
								if (reader.LocalName == "diffgram" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
								{
									if (mode == XmlReadMode.DiffGram || mode == XmlReadMode.IgnoreSchema)
									{
										this.ReadXmlDiffgram(reader);
										this.ReadEndElement(reader);
									}
									else
									{
										reader.Skip();
									}
									return xmlReadMode;
								}
								if (reader.LocalName == "Schema" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-data")
								{
									if (mode != XmlReadMode.IgnoreSchema && mode != XmlReadMode.InferSchema && mode != XmlReadMode.InferTypedSchema)
									{
										this.ReadXDRSchema(reader);
									}
									else
									{
										reader.Skip();
									}
									return xmlReadMode;
								}
								if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
								{
									if (mode != XmlReadMode.IgnoreSchema && mode != XmlReadMode.InferSchema && mode != XmlReadMode.InferTypedSchema)
									{
										this.ReadXSDSchema(reader, denyResolving);
									}
									else
									{
										reader.Skip();
									}
									return xmlReadMode;
								}
								if (reader.LocalName == "schema" && reader.NamespaceURI.StartsWith("http://www.w3.org/", StringComparison.Ordinal))
								{
									throw ExceptionBuilder.DataSetUnsupportedSchema("http://www.w3.org/2001/XMLSchema");
								}
								xmlElement = xmlDocument.CreateElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
								if (reader.HasAttributes)
								{
									int attributeCount = reader.AttributeCount;
									for (int i = 0; i < attributeCount; i++)
									{
										reader.MoveToAttribute(i);
										if (reader.NamespaceURI.Equals("http://www.w3.org/2000/xmlns/"))
										{
											xmlElement.SetAttribute(reader.Name, reader.GetAttribute(i));
										}
										else
										{
											XmlAttribute xmlAttribute = xmlElement.SetAttributeNode(reader.LocalName, reader.NamespaceURI);
											xmlAttribute.Prefix = reader.Prefix;
											xmlAttribute.Value = reader.GetAttribute(i);
										}
									}
								}
								reader.Read();
							}
							while (this.MoveToElement(reader, depth))
							{
								if (reader.LocalName == "Schema" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-data")
								{
									if (!flag && !flag2 && mode != XmlReadMode.IgnoreSchema && mode != XmlReadMode.InferSchema && mode != XmlReadMode.InferTypedSchema)
									{
										this.ReadXDRSchema(reader);
										flag = true;
										isXdr = true;
									}
									else
									{
										reader.Skip();
									}
								}
								else if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
								{
									if (mode != XmlReadMode.IgnoreSchema && mode != XmlReadMode.InferSchema && mode != XmlReadMode.InferTypedSchema)
									{
										this.ReadXSDSchema(reader, denyResolving);
										flag = true;
									}
									else
									{
										reader.Skip();
									}
								}
								else if (reader.LocalName == "diffgram" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
								{
									if (mode == XmlReadMode.DiffGram || mode == XmlReadMode.IgnoreSchema)
									{
										this.ReadXmlDiffgram(reader);
										xmlReadMode = XmlReadMode.DiffGram;
									}
									else
									{
										reader.Skip();
									}
								}
								else
								{
									if (reader.LocalName == "schema" && reader.NamespaceURI.StartsWith("http://www.w3.org/", StringComparison.Ordinal))
									{
										throw ExceptionBuilder.DataSetUnsupportedSchema("http://www.w3.org/2001/XMLSchema");
									}
									if (mode == XmlReadMode.DiffGram)
									{
										reader.Skip();
									}
									else
									{
										flag2 = true;
										if (mode == XmlReadMode.InferSchema || mode == XmlReadMode.InferTypedSchema)
										{
											XmlNode newChild = xmlDocument.ReadNode(reader);
											xmlElement.AppendChild(newChild);
										}
										else
										{
											if (xmlDataLoader == null)
											{
												xmlDataLoader = new XmlDataLoader(this, isXdr, xmlElement, mode == XmlReadMode.IgnoreSchema);
											}
											xmlDataLoader.LoadData(reader);
										}
									}
								}
							}
							this.ReadEndElement(reader);
							xmlDocument.AppendChild(xmlElement);
							if (xmlDataLoader == null)
							{
								xmlDataLoader = new XmlDataLoader(this, isXdr, mode == XmlReadMode.IgnoreSchema);
							}
							if (mode == XmlReadMode.DiffGram)
							{
								return xmlReadMode;
							}
							if (mode == XmlReadMode.InferSchema || mode == XmlReadMode.InferTypedSchema)
							{
								this.InferSchema(xmlDocument, null, mode);
								xmlReadMode = XmlReadMode.InferSchema;
								xmlDataLoader.FromInference = true;
								try
								{
									xmlDataLoader.LoadData(xmlDocument);
								}
								finally
								{
									xmlDataLoader.FromInference = false;
								}
							}
						}
						result = xmlReadMode;
					}
					finally
					{
					}
				}
			}
			finally
			{
				if (disposable != null)
				{
					disposable.Dispose();
				}
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
			return result;
		}

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.IO.Stream" /> and <see cref="T:System.Data.XmlReadMode" />.</summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> from which to read.</param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlReadMode" /> values.</param>
		/// <returns>The <see langword="XmlReadMode" /> used to read the data.</returns>
		// Token: 0x06000896 RID: 2198 RVA: 0x0001F44C File Offset: 0x0001D64C
		public XmlReadMode ReadXml(Stream stream, XmlReadMode mode)
		{
			if (stream == null)
			{
				return XmlReadMode.Auto;
			}
			XmlTextReader xmlTextReader = (mode == XmlReadMode.Fragment) ? new XmlTextReader(stream, XmlNodeType.Element, null) : new XmlTextReader(stream);
			xmlTextReader.XmlResolver = null;
			return this.ReadXml(xmlTextReader, mode, false);
		}

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.IO.TextReader" /> and <see cref="T:System.Data.XmlReadMode" />.</summary>
		/// <param name="reader">The <see cref="T:System.IO.TextReader" /> from which to read.</param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlReadMode" /> values.</param>
		/// <returns>The <see langword="XmlReadMode" /> used to read the data.</returns>
		// Token: 0x06000897 RID: 2199 RVA: 0x0001F484 File Offset: 0x0001D684
		public XmlReadMode ReadXml(TextReader reader, XmlReadMode mode)
		{
			if (reader == null)
			{
				return XmlReadMode.Auto;
			}
			XmlTextReader xmlTextReader = (mode == XmlReadMode.Fragment) ? new XmlTextReader(reader.ReadToEnd(), XmlNodeType.Element, null) : new XmlTextReader(reader);
			xmlTextReader.XmlResolver = null;
			return this.ReadXml(xmlTextReader, mode, false);
		}

		/// <summary>Reads XML schema and data into the <see cref="T:System.Data.DataSet" /> using the specified file and <see cref="T:System.Data.XmlReadMode" />.</summary>
		/// <param name="fileName">The filename (including the path) from which to read.</param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlReadMode" /> values.</param>
		/// <returns>The <see langword="XmlReadMode" /> used to read the data.</returns>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="T:System.Security.Permissions.FileIOPermission" /> is not set to <see cref="F:System.Security.Permissions.FileIOPermissionAccess.Read" />.</exception>
		// Token: 0x06000898 RID: 2200 RVA: 0x0001F4C0 File Offset: 0x0001D6C0
		public XmlReadMode ReadXml(string fileName, XmlReadMode mode)
		{
			XmlTextReader xmlTextReader = null;
			if (mode == XmlReadMode.Fragment)
			{
				xmlTextReader = new XmlTextReader(new FileStream(fileName, FileMode.Open), XmlNodeType.Element, null);
			}
			else
			{
				xmlTextReader = new XmlTextReader(fileName);
			}
			xmlTextReader.XmlResolver = null;
			XmlReadMode result;
			try
			{
				result = this.ReadXml(xmlTextReader, mode, false);
			}
			finally
			{
				xmlTextReader.Close();
			}
			return result;
		}

		/// <summary>Writes the current data for the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> object used to write to a file.</param>
		// Token: 0x06000899 RID: 2201 RVA: 0x0001F518 File Offset: 0x0001D718
		public void WriteXml(Stream stream)
		{
			this.WriteXml(stream, XmlWriteMode.IgnoreSchema);
		}

		/// <summary>Writes the current data for the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.IO.TextWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> object with which to write.</param>
		// Token: 0x0600089A RID: 2202 RVA: 0x0001F522 File Offset: 0x0001D722
		public void WriteXml(TextWriter writer)
		{
			this.WriteXml(writer, XmlWriteMode.IgnoreSchema);
		}

		/// <summary>Writes the current data for the <see cref="T:System.Data.DataSet" /> to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> with which to write.</param>
		// Token: 0x0600089B RID: 2203 RVA: 0x0001F52C File Offset: 0x0001D72C
		public void WriteXml(XmlWriter writer)
		{
			this.WriteXml(writer, XmlWriteMode.IgnoreSchema);
		}

		/// <summary>Writes the current data for the <see cref="T:System.Data.DataSet" /> to the specified file.</summary>
		/// <param name="fileName">The file name (including the path) to which to write.</param>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="T:System.Security.Permissions.FileIOPermission" /> is not set to <see cref="F:System.Security.Permissions.FileIOPermissionAccess.Write" />.</exception>
		// Token: 0x0600089C RID: 2204 RVA: 0x0001F536 File Offset: 0x0001D736
		public void WriteXml(string fileName)
		{
			this.WriteXml(fileName, XmlWriteMode.IgnoreSchema);
		}

		/// <summary>Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.IO.Stream" /> and <see cref="T:System.Data.XmlWriteMode" />. To write the schema, set the value for the <paramref name="mode" /> parameter to <see langword="WriteSchema" />.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> object used to write to a file.</param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlWriteMode" /> values.</param>
		// Token: 0x0600089D RID: 2205 RVA: 0x0001F540 File Offset: 0x0001D740
		public void WriteXml(Stream stream, XmlWriteMode mode)
		{
			if (stream != null)
			{
				this.WriteXml(new XmlTextWriter(stream, null)
				{
					Formatting = Formatting.Indented
				}, mode);
			}
		}

		/// <summary>Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.IO.TextWriter" /> and <see cref="T:System.Data.XmlWriteMode" />. To write the schema, set the value for the <paramref name="mode" /> parameter to <see langword="WriteSchema" />.</summary>
		/// <param name="writer">A <see cref="T:System.IO.TextWriter" /> object used to write the document.</param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlWriteMode" /> values.</param>
		// Token: 0x0600089E RID: 2206 RVA: 0x0001F568 File Offset: 0x0001D768
		public void WriteXml(TextWriter writer, XmlWriteMode mode)
		{
			if (writer != null)
			{
				this.WriteXml(new XmlTextWriter(writer)
				{
					Formatting = Formatting.Indented
				}, mode);
			}
		}

		/// <summary>Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataSet" /> using the specified <see cref="T:System.Xml.XmlWriter" /> and <see cref="T:System.Data.XmlWriteMode" />. To write the schema, set the value for the <paramref name="mode" /> parameter to <see langword="WriteSchema" />.</summary>
		/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> with which to write.</param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlWriteMode" /> values.</param>
		// Token: 0x0600089F RID: 2207 RVA: 0x0001F590 File Offset: 0x0001D790
		public void WriteXml(XmlWriter writer, XmlWriteMode mode)
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int, XmlWriteMode>("<ds.DataSet.WriteXml|API> {0}, mode={1}", this.ObjectID, mode);
			try
			{
				if (writer != null)
				{
					if (mode == XmlWriteMode.DiffGram)
					{
						new NewDiffgramGen(this).Save(writer);
					}
					else
					{
						new XmlDataTreeWriter(this).Save(writer, mode == XmlWriteMode.WriteSchema);
					}
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
		}

		/// <summary>Writes the current data, and optionally the schema, for the <see cref="T:System.Data.DataSet" /> to the specified file using the specified <see cref="T:System.Data.XmlWriteMode" />. To write the schema, set the value for the <paramref name="mode" /> parameter to <see langword="WriteSchema" />.</summary>
		/// <param name="fileName">The file name (including the path) to which to write.</param>
		/// <param name="mode">One of the <see cref="T:System.Data.XmlWriteMode" /> values.</param>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="T:System.Security.Permissions.FileIOPermission" /> is not set to <see cref="F:System.Security.Permissions.FileIOPermissionAccess.Write" />.</exception>
		// Token: 0x060008A0 RID: 2208 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
		public void WriteXml(string fileName, XmlWriteMode mode)
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int, string, int>("<ds.DataSet.WriteXml|API> {0}, fileName='{1}', mode={2}", this.ObjectID, fileName, (int)mode);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(fileName, null);
			try
			{
				xmlTextWriter.Formatting = Formatting.Indented;
				xmlTextWriter.WriteStartDocument(true);
				if (xmlTextWriter != null)
				{
					if (mode == XmlWriteMode.DiffGram)
					{
						new NewDiffgramGen(this).Save(xmlTextWriter);
					}
					else
					{
						new XmlDataTreeWriter(this).Save(xmlTextWriter, mode == XmlWriteMode.WriteSchema);
					}
				}
				xmlTextWriter.WriteEndDocument();
			}
			finally
			{
				xmlTextWriter.Close();
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0001F684 File Offset: 0x0001D884
		internal DataRelationCollection GetParentRelations(DataTable table)
		{
			return table.ParentRelations;
		}

		/// <summary>Merges a specified <see cref="T:System.Data.DataSet" /> and its schema into the current <see langword="DataSet" />.</summary>
		/// <param name="dataSet">The <see langword="DataSet" /> whose data and schema will be merged.</param>
		/// <exception cref="T:System.Data.ConstraintException">One or more constraints cannot be enabled.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="dataSet" /> is <see langword="null" />.</exception>
		// Token: 0x060008A2 RID: 2210 RVA: 0x0001F68C File Offset: 0x0001D88C
		public void Merge(DataSet dataSet)
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int, int>("<ds.DataSet.Merge|API> {0}, dataSet={1}", this.ObjectID, (dataSet != null) ? dataSet.ObjectID : 0);
			try
			{
				this.Merge(dataSet, false, MissingSchemaAction.Add);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
		}

		/// <summary>Merges a specified <see cref="T:System.Data.DataSet" /> and its schema into the current <see langword="DataSet" />, preserving or discarding any changes in this <see langword="DataSet" /> according to the given argument.</summary>
		/// <param name="dataSet">The <see langword="DataSet" /> whose data and schema will be merged.</param>
		/// <param name="preserveChanges">
		///   <see langword="true" /> to preserve changes in the current <see langword="DataSet" />; otherwise, <see langword="false" />.</param>
		// Token: 0x060008A3 RID: 2211 RVA: 0x0001F6E4 File Offset: 0x0001D8E4
		public void Merge(DataSet dataSet, bool preserveChanges)
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int, int, bool>("<ds.DataSet.Merge|API> {0}, dataSet={1}, preserveChanges={2}", this.ObjectID, (dataSet != null) ? dataSet.ObjectID : 0, preserveChanges);
			try
			{
				this.Merge(dataSet, preserveChanges, MissingSchemaAction.Add);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
		}

		/// <summary>Merges a specified <see cref="T:System.Data.DataSet" /> and its schema with the current <see langword="DataSet" />, preserving or discarding changes in the current <see langword="DataSet" /> and handling an incompatible schema according to the given arguments.</summary>
		/// <param name="dataSet">The <see langword="DataSet" /> whose data and schema will be merged.</param>
		/// <param name="preserveChanges">
		///   <see langword="true" /> to preserve changes in the current <see langword="DataSet" />; otherwise, <see langword="false" />.</param>
		/// <param name="missingSchemaAction">One of the <see cref="T:System.Data.MissingSchemaAction" /> values.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="dataSet" /> is <see langword="null" />.</exception>
		// Token: 0x060008A4 RID: 2212 RVA: 0x0001F73C File Offset: 0x0001D93C
		public void Merge(DataSet dataSet, bool preserveChanges, MissingSchemaAction missingSchemaAction)
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int, int, bool, MissingSchemaAction>("<ds.DataSet.Merge|API> {0}, dataSet={1}, preserveChanges={2}, missingSchemaAction={3}", this.ObjectID, (dataSet != null) ? dataSet.ObjectID : 0, preserveChanges, missingSchemaAction);
			try
			{
				if (dataSet == null)
				{
					throw ExceptionBuilder.ArgumentNull("dataSet");
				}
				if (missingSchemaAction - MissingSchemaAction.Add > 3)
				{
					throw ADP.InvalidMissingSchemaAction(missingSchemaAction);
				}
				new Merger(this, preserveChanges, missingSchemaAction).MergeDataSet(dataSet);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
		}

		/// <summary>Merges a specified <see cref="T:System.Data.DataTable" /> and its schema into the current <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="table">The <see cref="T:System.Data.DataTable" /> whose data and schema will be merged.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="table" /> is <see langword="null" />.</exception>
		// Token: 0x060008A5 RID: 2213 RVA: 0x0001F7B4 File Offset: 0x0001D9B4
		public void Merge(DataTable table)
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int, int>("<ds.DataSet.Merge|API> {0}, table={1}", this.ObjectID, (table != null) ? table.ObjectID : 0);
			try
			{
				this.Merge(table, false, MissingSchemaAction.Add);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
		}

		/// <summary>Merges a specified <see cref="T:System.Data.DataTable" /> and its schema into the current <see langword="DataSet" />, preserving or discarding changes in the <see langword="DataSet" /> and handling an incompatible schema according to the given arguments.</summary>
		/// <param name="table">The <see langword="DataTable" /> whose data and schema will be merged.</param>
		/// <param name="preserveChanges">One of the <see cref="T:System.Data.MissingSchemaAction" /> values.</param>
		/// <param name="missingSchemaAction">
		///   <see langword="true" /> to preserve changes in the <see langword="DataSet" />; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="dataSet" /> is <see langword="null" />.</exception>
		// Token: 0x060008A6 RID: 2214 RVA: 0x0001F80C File Offset: 0x0001DA0C
		public void Merge(DataTable table, bool preserveChanges, MissingSchemaAction missingSchemaAction)
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int, int, bool, MissingSchemaAction>("<ds.DataSet.Merge|API> {0}, table={1}, preserveChanges={2}, missingSchemaAction={3}", this.ObjectID, (table != null) ? table.ObjectID : 0, preserveChanges, missingSchemaAction);
			try
			{
				if (table == null)
				{
					throw ExceptionBuilder.ArgumentNull("table");
				}
				if (missingSchemaAction - MissingSchemaAction.Add > 3)
				{
					throw ADP.InvalidMissingSchemaAction(missingSchemaAction);
				}
				new Merger(this, preserveChanges, missingSchemaAction).MergeTable(table);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
		}

		/// <summary>Merges an array of <see cref="T:System.Data.DataRow" /> objects into the current <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="rows">The array of <see langword="DataRow" /> objects to be merged into the <see langword="DataSet" />.</param>
		// Token: 0x060008A7 RID: 2215 RVA: 0x0001F884 File Offset: 0x0001DA84
		public void Merge(DataRow[] rows)
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.Merge|API> {0}, rows", this.ObjectID);
			try
			{
				this.Merge(rows, false, MissingSchemaAction.Add);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
		}

		/// <summary>Merges an array of <see cref="T:System.Data.DataRow" /> objects into the current <see cref="T:System.Data.DataSet" />, preserving or discarding changes in the <see langword="DataSet" /> and handling an incompatible schema according to the given arguments.</summary>
		/// <param name="rows">The array of <see cref="T:System.Data.DataRow" /> objects to be merged into the <see langword="DataSet" />.</param>
		/// <param name="preserveChanges">
		///   <see langword="true" /> to preserve changes in the <see langword="DataSet" />; otherwise, <see langword="false" />.</param>
		/// <param name="missingSchemaAction">One of the <see cref="T:System.Data.MissingSchemaAction" /> values.</param>
		// Token: 0x060008A8 RID: 2216 RVA: 0x0001F8D0 File Offset: 0x0001DAD0
		public void Merge(DataRow[] rows, bool preserveChanges, MissingSchemaAction missingSchemaAction)
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int, bool, MissingSchemaAction>("<ds.DataSet.Merge|API> {0}, preserveChanges={1}, missingSchemaAction={2}", this.ObjectID, preserveChanges, missingSchemaAction);
			try
			{
				if (rows == null)
				{
					throw ExceptionBuilder.ArgumentNull("rows");
				}
				if (missingSchemaAction - MissingSchemaAction.Add > 3)
				{
					throw ADP.InvalidMissingSchemaAction(missingSchemaAction);
				}
				new Merger(this, preserveChanges, missingSchemaAction).MergeRows(rows);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
		}

		/// <summary>Raises the <see cref="M:System.Data.DataSet.OnPropertyChanging(System.ComponentModel.PropertyChangedEventArgs)" /> event.</summary>
		/// <param name="pcevent">A <see cref="T:System.ComponentModel.PropertyChangedEventArgs" /> that contains the event data.</param>
		// Token: 0x060008A9 RID: 2217 RVA: 0x0001F93C File Offset: 0x0001DB3C
		protected virtual void OnPropertyChanging(PropertyChangedEventArgs pcevent)
		{
			PropertyChangedEventHandler propertyChanging = this.PropertyChanging;
			if (propertyChanging == null)
			{
				return;
			}
			propertyChanging(this, pcevent);
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0001F950 File Offset: 0x0001DB50
		internal void OnMergeFailed(MergeFailedEventArgs mfevent)
		{
			if (this.MergeFailed != null)
			{
				this.MergeFailed(this, mfevent);
				return;
			}
			throw ExceptionBuilder.MergeFailed(mfevent.Conflict);
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0001F973 File Offset: 0x0001DB73
		internal void RaiseMergeFailed(DataTable table, string conflict, MissingSchemaAction missingSchemaAction)
		{
			if (MissingSchemaAction.Error == missingSchemaAction)
			{
				throw ExceptionBuilder.MergeFailed(conflict);
			}
			this.OnMergeFailed(new MergeFailedEventArgs(table, conflict));
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0001F98D File Offset: 0x0001DB8D
		internal void OnDataRowCreated(DataRow row)
		{
			DataRowCreatedEventHandler dataRowCreated = this.DataRowCreated;
			if (dataRowCreated == null)
			{
				return;
			}
			dataRowCreated(this, row);
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0001F9A1 File Offset: 0x0001DBA1
		internal void OnClearFunctionCalled(DataTable table)
		{
			DataSetClearEventhandler clearFunctionCalled = this.ClearFunctionCalled;
			if (clearFunctionCalled == null)
			{
				return;
			}
			clearFunctionCalled(this, table);
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0001F9B5 File Offset: 0x0001DBB5
		private void OnInitialized()
		{
			EventHandler initialized = this.Initialized;
			if (initialized == null)
			{
				return;
			}
			initialized(this, EventArgs.Empty);
		}

		/// <summary>Occurs when a <see cref="T:System.Data.DataTable" /> is removed from a <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="table">The <see cref="T:System.Data.DataTable" /> being removed.</param>
		// Token: 0x060008AF RID: 2223 RVA: 0x00007EED File Offset: 0x000060ED
		protected internal virtual void OnRemoveTable(DataTable table)
		{
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x0001F9D0 File Offset: 0x0001DBD0
		internal void OnRemovedTable(DataTable table)
		{
			DataViewManager defaultViewManager = this._defaultViewManager;
			if (defaultViewManager != null)
			{
				defaultViewManager.DataViewSettings.Remove(table);
			}
		}

		/// <summary>Occurs when a <see cref="T:System.Data.DataRelation" /> object is removed from a <see cref="T:System.Data.DataTable" />.</summary>
		/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> being removed.</param>
		// Token: 0x060008B1 RID: 2225 RVA: 0x00007EED File Offset: 0x000060ED
		protected virtual void OnRemoveRelation(DataRelation relation)
		{
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x0001F9F3 File Offset: 0x0001DBF3
		internal void OnRemoveRelationHack(DataRelation relation)
		{
			this.OnRemoveRelation(relation);
		}

		/// <summary>Sends a notification that the specified <see cref="T:System.Data.DataSet" /> property is about to change.</summary>
		/// <param name="name">The name of the property that is about to change.</param>
		// Token: 0x060008B3 RID: 2227 RVA: 0x0001F9FC File Offset: 0x0001DBFC
		protected internal void RaisePropertyChanging(string name)
		{
			this.OnPropertyChanging(new PropertyChangedEventArgs(name));
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x0001FA0A File Offset: 0x0001DC0A
		internal DataTable[] TopLevelTables()
		{
			return this.TopLevelTables(false);
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x0001FA14 File Offset: 0x0001DC14
		internal DataTable[] TopLevelTables(bool forSchema)
		{
			List<DataTable> list = new List<DataTable>();
			if (forSchema)
			{
				for (int i = 0; i < this.Tables.Count; i++)
				{
					DataTable dataTable = this.Tables[i];
					if (dataTable.NestedParentsCount > 1 || dataTable.SelfNested)
					{
						list.Add(dataTable);
					}
				}
			}
			for (int j = 0; j < this.Tables.Count; j++)
			{
				DataTable dataTable2 = this.Tables[j];
				if (dataTable2.NestedParentsCount == 0 && !list.Contains(dataTable2))
				{
					list.Add(dataTable2);
				}
			}
			if (list.Count != 0)
			{
				return list.ToArray();
			}
			return Array.Empty<DataTable>();
		}

		/// <summary>Rolls back all the changes made to the <see cref="T:System.Data.DataSet" /> since it was created, or since the last time <see cref="M:System.Data.DataSet.AcceptChanges" /> was called.</summary>
		// Token: 0x060008B6 RID: 2230 RVA: 0x0001FAB8 File Offset: 0x0001DCB8
		public virtual void RejectChanges()
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.RejectChanges|API> {0}", this.ObjectID);
			try
			{
				bool enforceConstraints = this.EnforceConstraints;
				this.EnforceConstraints = false;
				for (int i = 0; i < this.Tables.Count; i++)
				{
					this.Tables[i].RejectChanges();
				}
				this.EnforceConstraints = enforceConstraints;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
		}

		/// <summary>Clears all tables and removes all relations, foreign constraints, and tables from the <see cref="T:System.Data.DataSet" />. Subclasses should override <see cref="M:System.Data.DataSet.Reset" /> to restore a <see cref="T:System.Data.DataSet" /> to its original state.</summary>
		// Token: 0x060008B7 RID: 2231 RVA: 0x0001FB38 File Offset: 0x0001DD38
		public virtual void Reset()
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.Reset|API> {0}", this.ObjectID);
			try
			{
				for (int i = 0; i < this.Tables.Count; i++)
				{
					ConstraintCollection constraints = this.Tables[i].Constraints;
					int j = 0;
					while (j < constraints.Count)
					{
						if (constraints[j] is ForeignKeyConstraint)
						{
							constraints.Remove(constraints[j]);
						}
						else
						{
							j++;
						}
					}
				}
				this.Clear();
				this.Relations.Clear();
				this.Tables.Clear();
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0001FBEC File Offset: 0x0001DDEC
		internal bool ValidateCaseConstraint()
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.ValidateCaseConstraint|INFO> {0}", this.ObjectID);
			bool result;
			try
			{
				for (int i = 0; i < this.Relations.Count; i++)
				{
					DataRelation dataRelation = this.Relations[i];
					if (dataRelation.ChildTable.CaseSensitive != dataRelation.ParentTable.CaseSensitive)
					{
						return false;
					}
				}
				for (int j = 0; j < this.Tables.Count; j++)
				{
					ConstraintCollection constraints = this.Tables[j].Constraints;
					for (int k = 0; k < constraints.Count; k++)
					{
						if (constraints[k] is ForeignKeyConstraint)
						{
							ForeignKeyConstraint foreignKeyConstraint = (ForeignKeyConstraint)constraints[k];
							if (foreignKeyConstraint.Table.CaseSensitive != foreignKeyConstraint.RelatedTable.CaseSensitive)
							{
								return false;
							}
						}
					}
				}
				result = true;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
			return result;
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0001FCFC File Offset: 0x0001DEFC
		internal bool ValidateLocaleConstraint()
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.ValidateLocaleConstraint|INFO> {0}", this.ObjectID);
			bool result;
			try
			{
				for (int i = 0; i < this.Relations.Count; i++)
				{
					DataRelation dataRelation = this.Relations[i];
					if (dataRelation.ChildTable.Locale.LCID != dataRelation.ParentTable.Locale.LCID)
					{
						return false;
					}
				}
				for (int j = 0; j < this.Tables.Count; j++)
				{
					ConstraintCollection constraints = this.Tables[j].Constraints;
					for (int k = 0; k < constraints.Count; k++)
					{
						if (constraints[k] is ForeignKeyConstraint)
						{
							ForeignKeyConstraint foreignKeyConstraint = (ForeignKeyConstraint)constraints[k];
							if (foreignKeyConstraint.Table.Locale.LCID != foreignKeyConstraint.RelatedTable.Locale.LCID)
							{
								return false;
							}
						}
					}
				}
				result = true;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
			return result;
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0001FE24 File Offset: 0x0001E024
		internal DataTable FindTable(DataTable baseTable, PropertyDescriptor[] props, int propStart)
		{
			if (props.Length < propStart + 1)
			{
				return baseTable;
			}
			PropertyDescriptor propertyDescriptor = props[propStart];
			if (baseTable == null)
			{
				if (propertyDescriptor is DataTablePropertyDescriptor)
				{
					return this.FindTable(((DataTablePropertyDescriptor)propertyDescriptor).Table, props, propStart + 1);
				}
				return null;
			}
			else
			{
				if (propertyDescriptor is DataRelationPropertyDescriptor)
				{
					return this.FindTable(((DataRelationPropertyDescriptor)propertyDescriptor).Relation.ChildTable, props, propStart + 1);
				}
				return null;
			}
		}

		/// <summary>Ignores attributes and returns an empty DataSet.</summary>
		/// <param name="reader">The specified XML reader.</param>
		// Token: 0x060008BB RID: 2235 RVA: 0x0001FE88 File Offset: 0x0001E088
		protected virtual void ReadXmlSerializable(XmlReader reader)
		{
			this._useDataSetSchemaOnly = false;
			this._udtIsWrapped = false;
			if (reader.HasAttributes)
			{
				if (reader.MoveToAttribute("xsi:nil") && string.Equals(reader.GetAttribute("xsi:nil"), "true", StringComparison.Ordinal))
				{
					this.MoveToElement(reader, 1);
					return;
				}
				if (reader.MoveToAttribute("msdata:UseDataSetSchemaOnly"))
				{
					string attribute = reader.GetAttribute("msdata:UseDataSetSchemaOnly");
					if (string.Equals(attribute, "true", StringComparison.Ordinal) || string.Equals(attribute, "1", StringComparison.Ordinal))
					{
						this._useDataSetSchemaOnly = true;
					}
					else if (!string.Equals(attribute, "false", StringComparison.Ordinal) && !string.Equals(attribute, "0", StringComparison.Ordinal))
					{
						throw ExceptionBuilder.InvalidAttributeValue("UseDataSetSchemaOnly", attribute);
					}
				}
				if (reader.MoveToAttribute("msdata:UDTColumnValueWrapped"))
				{
					string attribute2 = reader.GetAttribute("msdata:UDTColumnValueWrapped");
					if (string.Equals(attribute2, "true", StringComparison.Ordinal) || string.Equals(attribute2, "1", StringComparison.Ordinal))
					{
						this._udtIsWrapped = true;
					}
					else if (!string.Equals(attribute2, "false", StringComparison.Ordinal) && !string.Equals(attribute2, "0", StringComparison.Ordinal))
					{
						throw ExceptionBuilder.InvalidAttributeValue("UDTColumnValueWrapped", attribute2);
					}
				}
			}
			this.ReadXml(reader, XmlReadMode.DiffGram, true);
		}

		/// <summary>Returns a serializable <see cref="T:System.Xml.Schema.XmlSchema" /> instance.</summary>
		/// <returns>The <see cref="T:System.Xml.Schema.XmlSchema" /> instance.</returns>
		// Token: 0x060008BC RID: 2236 RVA: 0x00003E32 File Offset: 0x00002032
		protected virtual XmlSchema GetSchemaSerializable()
		{
			return null;
		}

		/// <summary>Gets a copy of <see cref="T:System.Xml.Schema.XmlSchemaSet" /> for the DataSet.</summary>
		/// <param name="schemaSet">The specified schema set.</param>
		/// <returns>A copy of <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		// Token: 0x060008BD RID: 2237 RVA: 0x0001FFB4 File Offset: 0x0001E1B4
		public static XmlSchemaComplexType GetDataSetSchema(XmlSchemaSet schemaSet)
		{
			if (DataSet.s_schemaTypeForWSDL == null)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
				xmlSchemaAny.Namespace = "http://www.w3.org/2001/XMLSchema";
				xmlSchemaAny.MinOccurs = 0m;
				xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny);
				xmlSchemaAny = new XmlSchemaAny();
				xmlSchemaAny.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
				xmlSchemaAny.MinOccurs = 0m;
				xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny);
				xmlSchemaSequence.MaxOccurs = decimal.MaxValue;
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				DataSet.s_schemaTypeForWSDL = xmlSchemaComplexType;
			}
			return DataSet.s_schemaTypeForWSDL;
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x00006D64 File Offset: 0x00004F64
		private static bool PublishLegacyWSDL()
		{
			return false;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Serialization.IXmlSerializable.GetSchema" />.</summary>
		/// <returns>For a description of this member, see <see cref="M:System.Xml.Serialization.IXmlSerializable.GetSchema" />.</returns>
		// Token: 0x060008BF RID: 2239 RVA: 0x00020058 File Offset: 0x0001E258
		XmlSchema IXmlSerializable.GetSchema()
		{
			if (base.GetType() == typeof(DataSet))
			{
				return null;
			}
			MemoryStream memoryStream = new MemoryStream();
			XmlWriter xmlWriter = new XmlTextWriter(memoryStream, null);
			if (xmlWriter != null)
			{
				new XmlTreeGen(SchemaFormat.WebService).Save(this, xmlWriter);
			}
			memoryStream.Position = 0L;
			return XmlSchema.Read(new XmlTextReader(memoryStream), null);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)" />.</summary>
		/// <param name="reader">A <see cref="T:System.Xml.XmlReader" />.</param>
		// Token: 0x060008C0 RID: 2240 RVA: 0x000200B0 File Offset: 0x0001E2B0
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			bool flag = true;
			XmlTextReader xmlTextReader = null;
			IXmlTextParser xmlTextParser = reader as IXmlTextParser;
			if (xmlTextParser != null)
			{
				flag = xmlTextParser.Normalized;
				xmlTextParser.Normalized = false;
			}
			else
			{
				xmlTextReader = (reader as XmlTextReader);
				if (xmlTextReader != null)
				{
					flag = xmlTextReader.Normalization;
					xmlTextReader.Normalization = false;
				}
			}
			this.ReadXmlSerializable(reader);
			if (xmlTextParser != null)
			{
				xmlTextParser.Normalized = flag;
				return;
			}
			if (xmlTextReader != null)
			{
				xmlTextReader.Normalization = flag;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)" />.</summary>
		/// <param name="writer">A <see cref="T:System.Xml.XmlWriter" />.</param>
		// Token: 0x060008C1 RID: 2241 RVA: 0x0002010F File Offset: 0x0001E30F
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.WriteXmlSchema(writer, SchemaFormat.WebService, null);
			this.WriteXml(writer, XmlWriteMode.DiffGram);
		}

		/// <summary>Fills a <see cref="T:System.Data.DataSet" /> with values from a data source using the supplied <see cref="T:System.Data.IDataReader" />, using an array of <see cref="T:System.Data.DataTable" /> instances to supply the schema and namespace information.</summary>
		/// <param name="reader">An <see cref="T:System.Data.IDataReader" /> that provides one or more result sets.</param>
		/// <param name="loadOption">A value from the <see cref="T:System.Data.LoadOption" /> enumeration that indicates how rows already in the <see cref="T:System.Data.DataTable" /> instances within the <see cref="T:System.Data.DataSet" /> will be combined with incoming rows that share the same primary key.</param>
		/// <param name="errorHandler">A <see cref="T:System.Data.FillErrorEventHandler" /> delegate to call when an error occurs while loading data.</param>
		/// <param name="tables">An array of <see cref="T:System.Data.DataTable" /> instances, from which the <see cref="M:System.Data.DataSet.Load(System.Data.IDataReader,System.Data.LoadOption,System.Data.FillErrorEventHandler,System.Data.DataTable[])" /> method retrieves name and namespace information.</param>
		// Token: 0x060008C2 RID: 2242 RVA: 0x00020124 File Offset: 0x0001E324
		public virtual void Load(IDataReader reader, LoadOption loadOption, FillErrorEventHandler errorHandler, params DataTable[] tables)
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<LoadOption>("<ds.DataSet.Load|API> reader, loadOption={0}", loadOption);
			try
			{
				foreach (DataTable dataTable in tables)
				{
					ADP.CheckArgumentNull(dataTable, "tables");
					if (dataTable.DataSet != this)
					{
						throw ExceptionBuilder.TableNotInTheDataSet(dataTable.TableName);
					}
				}
				LoadAdapter loadAdapter = new LoadAdapter();
				loadAdapter.FillLoadOption = loadOption;
				loadAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
				if (errorHandler != null)
				{
					loadAdapter.FillError += errorHandler;
				}
				loadAdapter.FillFromReader(tables, reader, 0, 0);
				if (!reader.IsClosed && !reader.NextResult())
				{
					reader.Close();
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
		}

		/// <summary>Fills a <see cref="T:System.Data.DataSet" /> with values from a data source using the supplied <see cref="T:System.Data.IDataReader" />, using an array of <see cref="T:System.Data.DataTable" /> instances to supply the schema and namespace information.</summary>
		/// <param name="reader">An <see cref="T:System.Data.IDataReader" /> that provides one or more result sets.</param>
		/// <param name="loadOption">A value from the <see cref="T:System.Data.LoadOption" /> enumeration that indicates how rows already in the <see cref="T:System.Data.DataTable" /> instances within the <see cref="T:System.Data.DataSet" /> will be combined with incoming rows that share the same primary key.</param>
		/// <param name="tables">An array of <see cref="T:System.Data.DataTable" /> instances, from which the <see cref="M:System.Data.DataSet.Load(System.Data.IDataReader,System.Data.LoadOption,System.Data.DataTable[])" /> method retrieves name and namespace information. Each of these tables must be a member of the <see cref="T:System.Data.DataTableCollection" /> contained by this <see cref="T:System.Data.DataSet" />.</param>
		// Token: 0x060008C3 RID: 2243 RVA: 0x000201DC File Offset: 0x0001E3DC
		public void Load(IDataReader reader, LoadOption loadOption, params DataTable[] tables)
		{
			this.Load(reader, loadOption, null, tables);
		}

		/// <summary>Fills a <see cref="T:System.Data.DataSet" /> with values from a data source using the supplied <see cref="T:System.Data.IDataReader" />, using an array of strings to supply the names for the tables within the <see langword="DataSet" />.</summary>
		/// <param name="reader">An <see cref="T:System.Data.IDataReader" /> that provides one or more result sets.</param>
		/// <param name="loadOption">A value from the <see cref="T:System.Data.LoadOption" /> enumeration that indicates how rows already in the <see cref="T:System.Data.DataTable" /> instances within the <see langword="DataSet" /> will be combined with incoming rows that share the same primary key.</param>
		/// <param name="tables">An array of strings, from which the <see langword="Load" /> method retrieves table name information.</param>
		// Token: 0x060008C4 RID: 2244 RVA: 0x000201E8 File Offset: 0x0001E3E8
		public void Load(IDataReader reader, LoadOption loadOption, params string[] tables)
		{
			ADP.CheckArgumentNull(tables, "tables");
			DataTable[] array = new DataTable[tables.Length];
			for (int i = 0; i < tables.Length; i++)
			{
				DataTable dataTable = this.Tables[tables[i]];
				if (dataTable == null)
				{
					dataTable = new DataTable(tables[i]);
					this.Tables.Add(dataTable);
				}
				array[i] = dataTable;
			}
			this.Load(reader, loadOption, null, array);
		}

		/// <summary>Returns a <see cref="T:System.Data.DataTableReader" /> with one result set per <see cref="T:System.Data.DataTable" />, in the same sequence as the tables appear in the <see cref="P:System.Data.DataSet.Tables" /> collection.</summary>
		/// <returns>A <see cref="T:System.Data.DataTableReader" /> containing one or more result sets, corresponding to the <see cref="T:System.Data.DataTable" /> instances contained within the source <see cref="T:System.Data.DataSet" />.</returns>
		// Token: 0x060008C5 RID: 2245 RVA: 0x0002024C File Offset: 0x0001E44C
		public DataTableReader CreateDataReader()
		{
			if (this.Tables.Count == 0)
			{
				throw ExceptionBuilder.CannotCreateDataReaderOnEmptyDataSet();
			}
			DataTable[] array = new DataTable[this.Tables.Count];
			for (int i = 0; i < this.Tables.Count; i++)
			{
				array[i] = this.Tables[i];
			}
			return this.CreateDataReader(array);
		}

		/// <summary>Returns a <see cref="T:System.Data.DataTableReader" /> with one result set per <see cref="T:System.Data.DataTable" />.</summary>
		/// <param name="dataTables">An array of DataTables providing the order of the result sets to be returned in the <see cref="T:System.Data.DataTableReader" />.</param>
		/// <returns>A <see cref="T:System.Data.DataTableReader" /> containing one or more result sets, corresponding to the <see cref="T:System.Data.DataTable" /> instances contained within the source <see cref="T:System.Data.DataSet" />. The returned result sets are in the order specified by the <paramref name="dataTables" /> parameter.</returns>
		// Token: 0x060008C6 RID: 2246 RVA: 0x000202AC File Offset: 0x0001E4AC
		public DataTableReader CreateDataReader(params DataTable[] dataTables)
		{
			long scopeId = DataCommonEventSource.Log.EnterScope<int>("<ds.DataSet.GetDataReader|API> {0}", this.ObjectID);
			DataTableReader result;
			try
			{
				if (dataTables.Length == 0)
				{
					throw ExceptionBuilder.DataTableReaderArgumentIsEmpty();
				}
				for (int i = 0; i < dataTables.Length; i++)
				{
					if (dataTables[i] == null)
					{
						throw ExceptionBuilder.ArgumentContainsNullValue();
					}
				}
				result = new DataTableReader(dataTables);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(scopeId);
			}
			return result;
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x00020318 File Offset: 0x0001E518
		// (set) Token: 0x060008C8 RID: 2248 RVA: 0x00020320 File Offset: 0x0001E520
		internal string MainTableName
		{
			get
			{
				return this._mainTableName;
			}
			set
			{
				this._mainTableName = value;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x00020329 File Offset: 0x0001E529
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x0400069A RID: 1690
		private const string KEY_XMLSCHEMA = "XmlSchema";

		// Token: 0x0400069B RID: 1691
		private const string KEY_XMLDIFFGRAM = "XmlDiffGram";

		// Token: 0x0400069C RID: 1692
		private DataViewManager _defaultViewManager;

		// Token: 0x0400069D RID: 1693
		private readonly DataTableCollection _tableCollection;

		// Token: 0x0400069E RID: 1694
		private readonly DataRelationCollection _relationCollection;

		// Token: 0x0400069F RID: 1695
		internal PropertyCollection _extendedProperties;

		// Token: 0x040006A0 RID: 1696
		private string _dataSetName = "NewDataSet";

		// Token: 0x040006A1 RID: 1697
		private string _datasetPrefix = string.Empty;

		// Token: 0x040006A2 RID: 1698
		internal string _namespaceURI = string.Empty;

		// Token: 0x040006A3 RID: 1699
		private bool _enforceConstraints = true;

		// Token: 0x040006A4 RID: 1700
		private bool _caseSensitive;

		// Token: 0x040006A5 RID: 1701
		private CultureInfo _culture;

		// Token: 0x040006A6 RID: 1702
		private bool _cultureUserSet;

		// Token: 0x040006A7 RID: 1703
		internal bool _fInReadXml;

		// Token: 0x040006A8 RID: 1704
		internal bool _fInLoadDiffgram;

		// Token: 0x040006A9 RID: 1705
		internal bool _fTopLevelTable;

		// Token: 0x040006AA RID: 1706
		internal bool _fInitInProgress;

		// Token: 0x040006AB RID: 1707
		internal bool _fEnableCascading = true;

		// Token: 0x040006AC RID: 1708
		internal bool _fIsSchemaLoading;

		// Token: 0x040006AD RID: 1709
		private bool _fBoundToDocument;

		// Token: 0x040006AE RID: 1710
		internal string _mainTableName = string.Empty;

		// Token: 0x040006AF RID: 1711
		private SerializationFormat _remotingFormat;

		// Token: 0x040006B0 RID: 1712
		private object _defaultViewManagerLock = new object();

		// Token: 0x040006B1 RID: 1713
		private static int s_objectTypeCount;

		// Token: 0x040006B2 RID: 1714
		private readonly int _objectID = Interlocked.Increment(ref DataSet.s_objectTypeCount);

		// Token: 0x040006B3 RID: 1715
		private static XmlSchemaComplexType s_schemaTypeForWSDL;

		// Token: 0x040006B4 RID: 1716
		internal bool _useDataSetSchemaOnly;

		// Token: 0x040006B5 RID: 1717
		internal bool _udtIsWrapped;

		// Token: 0x040006B6 RID: 1718
		[CompilerGenerated]
		private PropertyChangedEventHandler PropertyChanging;

		// Token: 0x040006B7 RID: 1719
		[CompilerGenerated]
		private MergeFailedEventHandler MergeFailed;

		// Token: 0x040006B8 RID: 1720
		[CompilerGenerated]
		private DataRowCreatedEventHandler DataRowCreated;

		// Token: 0x040006B9 RID: 1721
		[CompilerGenerated]
		private DataSetClearEventhandler ClearFunctionCalled;

		// Token: 0x040006BA RID: 1722
		[CompilerGenerated]
		private EventHandler Initialized;

		// Token: 0x02000095 RID: 149
		private struct TableChanges
		{
			// Token: 0x060008CA RID: 2250 RVA: 0x00020331 File Offset: 0x0001E531
			internal TableChanges(int rowCount)
			{
				this._rowChanges = new BitArray(rowCount);
				this.HasChanges = 0;
			}

			// Token: 0x17000180 RID: 384
			// (get) Token: 0x060008CB RID: 2251 RVA: 0x00020346 File Offset: 0x0001E546
			// (set) Token: 0x060008CC RID: 2252 RVA: 0x0002034E File Offset: 0x0001E54E
			internal int HasChanges
			{
				[CompilerGenerated]
				readonly get
				{
					return this.<HasChanges>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<HasChanges>k__BackingField = value;
				}
			}

			// Token: 0x17000181 RID: 385
			internal bool this[int index]
			{
				get
				{
					return this._rowChanges[index];
				}
				set
				{
					this._rowChanges[index] = value;
					int hasChanges = this.HasChanges;
					this.HasChanges = hasChanges + 1;
				}
			}

			// Token: 0x040006BB RID: 1723
			private BitArray _rowChanges;

			// Token: 0x040006BC RID: 1724
			[CompilerGenerated]
			private int <HasChanges>k__BackingField;
		}
	}
}
