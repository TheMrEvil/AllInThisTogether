using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Allows structured data to be stored, retrieved, and manipulated through a relational <see cref="T:System.Data.DataSet" />.</summary>
	// Token: 0x0200007F RID: 127
	[Obsolete("XmlDataDocument class will be removed in a future release.")]
	public class XmlDataDocument : XmlDocument
	{
		// Token: 0x0600059C RID: 1436 RVA: 0x000149EC File Offset: 0x00012BEC
		internal void AddPointer(IXmlDataVirtualNode pointer)
		{
			Hashtable pointers = this._pointers;
			lock (pointers)
			{
				this._countAddPointer++;
				if (this._countAddPointer >= 5)
				{
					ArrayList arrayList = new ArrayList();
					foreach (object obj in this._pointers)
					{
						IXmlDataVirtualNode xmlDataVirtualNode = (IXmlDataVirtualNode)((DictionaryEntry)obj).Value;
						if (!xmlDataVirtualNode.IsInUse())
						{
							arrayList.Add(xmlDataVirtualNode);
						}
					}
					for (int i = 0; i < arrayList.Count; i++)
					{
						this._pointers.Remove(arrayList[i]);
					}
					this._countAddPointer = 0;
				}
				this._pointers[pointer] = pointer;
			}
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00007EED File Offset: 0x000060ED
		[Conditional("DEBUG")]
		internal void AssertPointerPresent(IXmlDataVirtualNode pointer)
		{
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00014AE8 File Offset: 0x00012CE8
		private void AttachDataSet(DataSet ds)
		{
			if (ds.FBoundToDocument)
			{
				throw new ArgumentException("DataSet can be associated with at most one XmlDataDocument. Cannot associate the DataSet with the current XmlDataDocument because the DataSet is already associated with another XmlDataDocument.");
			}
			ds.FBoundToDocument = true;
			this._dataSet = ds;
			this.BindSpecialListeners();
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00014B14 File Offset: 0x00012D14
		internal void SyncRows(DataRow parentRow, XmlNode node, bool fAddRowsToTable)
		{
			XmlBoundElement xmlBoundElement = node as XmlBoundElement;
			if (xmlBoundElement != null)
			{
				DataRow row = xmlBoundElement.Row;
				if (row != null && xmlBoundElement.ElementState == ElementState.Defoliated)
				{
					return;
				}
				if (row != null)
				{
					this.SynchronizeRowFromRowElement(xmlBoundElement);
					xmlBoundElement.ElementState = ElementState.WeakFoliation;
					this.DefoliateRegion(xmlBoundElement);
					if (parentRow != null)
					{
						XmlDataDocument.SetNestedParentRow(row, parentRow);
					}
					if (fAddRowsToTable && row.RowState == DataRowState.Detached)
					{
						row.Table.Rows.Add(row);
					}
					parentRow = row;
				}
			}
			for (XmlNode xmlNode = node.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				this.SyncRows(parentRow, xmlNode, fAddRowsToTable);
			}
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00014BA0 File Offset: 0x00012DA0
		internal void SyncTree(XmlNode node)
		{
			XmlBoundElement xmlBoundElement = null;
			this._mapper.GetRegion(node, out xmlBoundElement);
			DataRow parentRow = null;
			bool flag = this.IsConnected(node);
			if (xmlBoundElement != null)
			{
				DataRow row = xmlBoundElement.Row;
				if (row != null && xmlBoundElement.ElementState == ElementState.Defoliated)
				{
					return;
				}
				if (row != null)
				{
					this.SynchronizeRowFromRowElement(xmlBoundElement);
					if (node == xmlBoundElement)
					{
						xmlBoundElement.ElementState = ElementState.WeakFoliation;
						this.DefoliateRegion(xmlBoundElement);
					}
					if (flag && row.RowState == DataRowState.Detached)
					{
						row.Table.Rows.Add(row);
					}
					parentRow = row;
				}
			}
			for (XmlNode xmlNode = node.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				this.SyncRows(parentRow, xmlNode, flag);
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00014C3B File Offset: 0x00012E3B
		// (set) Token: 0x060005A2 RID: 1442 RVA: 0x00014C43 File Offset: 0x00012E43
		internal ElementState AutoFoliationState
		{
			get
			{
				return this._autoFoliationState;
			}
			set
			{
				this._autoFoliationState = value;
			}
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00014C4C File Offset: 0x00012E4C
		private void BindForLoad()
		{
			this._ignoreDataSetEvents = true;
			this._mapper.SetupMapping(this, this._dataSet);
			if (this._dataSet.Tables.Count > 0)
			{
				this.LoadDataSetFromTree();
			}
			this.BindListeners();
			this._ignoreDataSetEvents = false;
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00014C98 File Offset: 0x00012E98
		private void Bind(bool fLoadFromDataSet)
		{
			this._ignoreDataSetEvents = true;
			this._ignoreXmlEvents = true;
			this._mapper.SetupMapping(this, this._dataSet);
			if (base.DocumentElement != null)
			{
				this.LoadDataSetFromTree();
				this.BindListeners();
			}
			else if (fLoadFromDataSet)
			{
				this._bLoadFromDataSet = true;
				this.LoadTreeFromDataSet(this.DataSet);
				this.BindListeners();
			}
			this._ignoreDataSetEvents = false;
			this._ignoreXmlEvents = false;
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00014D05 File Offset: 0x00012F05
		internal void Bind(DataRow r, XmlBoundElement e)
		{
			r.Element = e;
			e.Row = r;
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00014D15 File Offset: 0x00012F15
		private void BindSpecialListeners()
		{
			this._dataSet.DataRowCreated += this.OnDataRowCreatedSpecial;
			this._fDataRowCreatedSpecial = true;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00014D35 File Offset: 0x00012F35
		private void UnBindSpecialListeners()
		{
			this._dataSet.DataRowCreated -= this.OnDataRowCreatedSpecial;
			this._fDataRowCreatedSpecial = false;
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00014D55 File Offset: 0x00012F55
		private void BindListeners()
		{
			this.BindToDocument();
			this.BindToDataSet();
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00014D64 File Offset: 0x00012F64
		private void BindToDataSet()
		{
			if (this._fBoundToDataSet)
			{
				return;
			}
			if (this._fDataRowCreatedSpecial)
			{
				this.UnBindSpecialListeners();
			}
			this._dataSet.Tables.CollectionChanging += this.OnDataSetTablesChanging;
			this._dataSet.Relations.CollectionChanging += this.OnDataSetRelationsChanging;
			this._dataSet.DataRowCreated += this.OnDataRowCreated;
			this._dataSet.PropertyChanging += this.OnDataSetPropertyChanging;
			this._dataSet.ClearFunctionCalled += this.OnClearCalled;
			if (this._dataSet.Tables.Count > 0)
			{
				foreach (object obj in this._dataSet.Tables)
				{
					DataTable t = (DataTable)obj;
					this.BindToTable(t);
				}
			}
			foreach (object obj2 in this._dataSet.Relations)
			{
				((DataRelation)obj2).PropertyChanging += this.OnRelationPropertyChanging;
			}
			this._fBoundToDataSet = true;
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00014EC8 File Offset: 0x000130C8
		private void BindToDocument()
		{
			if (!this._fBoundToDocument)
			{
				base.NodeInserting += this.OnNodeInserting;
				base.NodeInserted += this.OnNodeInserted;
				base.NodeRemoving += this.OnNodeRemoving;
				base.NodeRemoved += this.OnNodeRemoved;
				base.NodeChanging += this.OnNodeChanging;
				base.NodeChanged += this.OnNodeChanged;
				this._fBoundToDocument = true;
			}
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00014F50 File Offset: 0x00013150
		private void BindToTable(DataTable t)
		{
			t.ColumnChanged += this.OnColumnChanged;
			t.RowChanging += this.OnRowChanging;
			t.RowChanged += this.OnRowChanged;
			t.RowDeleting += this.OnRowChanging;
			t.RowDeleted += this.OnRowChanged;
			t.PropertyChanging += this.OnTablePropertyChanging;
			t.Columns.CollectionChanging += this.OnTableColumnsChanging;
			foreach (object obj in t.Columns)
			{
				((DataColumn)obj).PropertyChanging += this.OnColumnPropertyChanging;
			}
		}

		/// <summary>Creates an element with the specified <see cref="P:System.Xml.XmlNode.Prefix" />, <see cref="P:System.Xml.XmlDocument.LocalName" /> , and <see cref="P:System.Xml.XmlNode.NamespaceURI" />.</summary>
		/// <param name="prefix">The prefix of the new element. If String.Empty or <see langword="null" />, there is no prefix.</param>
		/// <param name="localName">The local name of the new element.</param>
		/// <param name="namespaceURI">The namespace Uniform Resource Identifier (URI) of the new element. If String.Empty or <see langword="null" />, there is no namespaceURI.</param>
		/// <returns>A new <see cref="T:System.Xml.XmlElement" />.</returns>
		// Token: 0x060005AC RID: 1452 RVA: 0x00015038 File Offset: 0x00013238
		public override XmlElement CreateElement(string prefix, string localName, string namespaceURI)
		{
			if (prefix == null)
			{
				prefix = string.Empty;
			}
			if (namespaceURI == null)
			{
				namespaceURI = string.Empty;
			}
			if (!this._fAssociateDataRow)
			{
				return new XmlBoundElement(prefix, localName, namespaceURI, this);
			}
			this.EnsurePopulatedMode();
			DataTable dataTable = this._mapper.SearchMatchingTableSchema(localName, namespaceURI);
			if (dataTable != null)
			{
				DataRow dataRow = dataTable.CreateEmptyRow();
				foreach (object obj in dataTable.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					if (dataColumn.ColumnMapping != MappingType.Hidden)
					{
						XmlDataDocument.SetRowValueToNull(dataRow, dataColumn);
					}
				}
				XmlBoundElement element = dataRow.Element;
				element.Prefix = prefix;
				return element;
			}
			return new XmlBoundElement(prefix, localName, namespaceURI, this);
		}

		/// <summary>Creates an <see cref="T:System.Xml.XmlEntityReference" /> with the specified name.</summary>
		/// <param name="name">The name of the entity reference.</param>
		/// <returns>An <see cref="T:System.Xml.XmlEntityReference" /> with the specified name.</returns>
		/// <exception cref="T:System.NotSupportedException">Calling this method.</exception>
		// Token: 0x060005AD RID: 1453 RVA: 0x000150F8 File Offset: 0x000132F8
		public override XmlEntityReference CreateEntityReference(string name)
		{
			throw new NotSupportedException("Cannot create entity references on DataDocument.");
		}

		/// <summary>Gets a <see cref="T:System.Data.DataSet" /> that provides a relational representation of the data in the <see langword="XmlDataDocument" />.</summary>
		/// <returns>A <see langword="DataSet" /> that can be used to access the data in the <see langword="XmlDataDocument" /> using a relational model.</returns>
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x00015104 File Offset: 0x00013304
		public DataSet DataSet
		{
			get
			{
				return this._dataSet;
			}
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0001510C File Offset: 0x0001330C
		private void DefoliateRegion(XmlBoundElement rowElem)
		{
			if (!this._optimizeStorage)
			{
				return;
			}
			if (rowElem.ElementState != ElementState.WeakFoliation)
			{
				return;
			}
			if (!this._mapper.IsRegionRadical(rowElem))
			{
				return;
			}
			bool ignoreXmlEvents = this.IgnoreXmlEvents;
			this.IgnoreXmlEvents = true;
			rowElem.ElementState = ElementState.Defoliating;
			try
			{
				rowElem.RemoveAllAttributes();
				XmlNode nextSibling;
				for (XmlNode xmlNode = rowElem.FirstChild; xmlNode != null; xmlNode = nextSibling)
				{
					nextSibling = xmlNode.NextSibling;
					XmlBoundElement xmlBoundElement = xmlNode as XmlBoundElement;
					if (xmlBoundElement != null && xmlBoundElement.Row != null)
					{
						break;
					}
					rowElem.RemoveChild(xmlNode);
				}
				rowElem.ElementState = ElementState.Defoliated;
			}
			finally
			{
				this.IgnoreXmlEvents = ignoreXmlEvents;
			}
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x000151A8 File Offset: 0x000133A8
		private XmlElement EnsureDocumentElement()
		{
			XmlElement xmlElement = base.DocumentElement;
			if (xmlElement == null)
			{
				string text = XmlConvert.EncodeLocalName(this.DataSet.DataSetName);
				if (text == null || text.Length == 0)
				{
					text = "Xml";
				}
				string text2 = this.DataSet.Namespace;
				if (text2 == null)
				{
					text2 = string.Empty;
				}
				xmlElement = new XmlBoundElement(string.Empty, text, text2, this);
				this.AppendChild(xmlElement);
			}
			return xmlElement;
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00015210 File Offset: 0x00013410
		private XmlElement EnsureNonRowDocumentElement()
		{
			XmlElement documentElement = base.DocumentElement;
			if (documentElement == null)
			{
				return this.EnsureDocumentElement();
			}
			if (this.GetRowFromElement(documentElement) == null)
			{
				return documentElement;
			}
			return this.DemoteDocumentElement();
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00015240 File Offset: 0x00013440
		private XmlElement DemoteDocumentElement()
		{
			XmlElement documentElement = base.DocumentElement;
			this.RemoveChild(documentElement);
			XmlElement xmlElement = this.EnsureDocumentElement();
			xmlElement.AppendChild(documentElement);
			return xmlElement;
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0001526A File Offset: 0x0001346A
		private void EnsurePopulatedMode()
		{
			if (this._fDataRowCreatedSpecial)
			{
				this.UnBindSpecialListeners();
				this._mapper.SetupMapping(this, this._dataSet);
				this.BindListeners();
				this._fAssociateDataRow = true;
			}
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0001529C File Offset: 0x0001349C
		private void FixNestedChildren(DataRow row, XmlElement rowElement)
		{
			foreach (object obj in this.GetNestedChildRelations(row))
			{
				DataRelation relation = (DataRelation)obj;
				DataRow[] childRows = row.GetChildRows(relation);
				for (int i = 0; i < childRows.Length; i++)
				{
					XmlElement element = childRows[i].Element;
					if (element != null && element.ParentNode != rowElement)
					{
						element.ParentNode.RemoveChild(element);
						rowElement.AppendChild(element);
					}
				}
			}
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00015338 File Offset: 0x00013538
		internal void Foliate(XmlBoundElement node, ElementState newState)
		{
			if (this.IsFoliationEnabled)
			{
				if (node.ElementState == ElementState.Defoliated)
				{
					this.ForceFoliation(node, newState);
					return;
				}
				if (node.ElementState == ElementState.WeakFoliation && newState == ElementState.StrongFoliation)
				{
					node.ElementState = newState;
				}
			}
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00015368 File Offset: 0x00013568
		private void Foliate(XmlElement element)
		{
			if (element is XmlBoundElement)
			{
				((XmlBoundElement)element).Foliate(ElementState.WeakFoliation);
			}
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00015380 File Offset: 0x00013580
		private void FoliateIfDataPointers(DataRow row, XmlElement rowElement)
		{
			if (!this.IsFoliated(rowElement) && this.HasPointers(rowElement))
			{
				bool isFoliationEnabled = this.IsFoliationEnabled;
				this.IsFoliationEnabled = true;
				try
				{
					this.Foliate(rowElement);
				}
				finally
				{
					this.IsFoliationEnabled = isFoliationEnabled;
				}
			}
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x000153D0 File Offset: 0x000135D0
		private void EnsureFoliation(XmlBoundElement rowElem, ElementState foliation)
		{
			if (rowElem.IsFoliated)
			{
				return;
			}
			this.ForceFoliation(rowElem, foliation);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x000153E4 File Offset: 0x000135E4
		private void ForceFoliation(XmlBoundElement node, ElementState newState)
		{
			object foliationLock = this._foliationLock;
			lock (foliationLock)
			{
				if (node.ElementState == ElementState.Defoliated)
				{
					node.ElementState = ElementState.Foliating;
					bool ignoreXmlEvents = this.IgnoreXmlEvents;
					this.IgnoreXmlEvents = true;
					try
					{
						XmlNode xmlNode = null;
						DataRow row = node.Row;
						DataRowVersion version = (row.RowState == DataRowState.Detached) ? DataRowVersion.Proposed : DataRowVersion.Current;
						foreach (object obj in row.Table.Columns)
						{
							DataColumn dataColumn = (DataColumn)obj;
							if (!this.IsNotMapped(dataColumn))
							{
								object value = row[dataColumn, version];
								if (!Convert.IsDBNull(value))
								{
									if (dataColumn.ColumnMapping == MappingType.Attribute)
									{
										node.SetAttribute(dataColumn.EncodedColumnName, dataColumn.Namespace, dataColumn.ConvertObjectToXml(value));
									}
									else if (dataColumn.ColumnMapping == MappingType.Element)
									{
										XmlNode xmlNode2 = new XmlBoundElement(string.Empty, dataColumn.EncodedColumnName, dataColumn.Namespace, this);
										xmlNode2.AppendChild(this.CreateTextNode(dataColumn.ConvertObjectToXml(value)));
										if (xmlNode != null)
										{
											node.InsertAfter(xmlNode2, xmlNode);
										}
										else if (node.FirstChild != null)
										{
											node.InsertBefore(xmlNode2, node.FirstChild);
										}
										else
										{
											node.AppendChild(xmlNode2);
										}
										xmlNode = xmlNode2;
									}
									else
									{
										XmlNode xmlNode2 = this.CreateTextNode(dataColumn.ConvertObjectToXml(value));
										if (node.FirstChild != null)
										{
											node.InsertBefore(xmlNode2, node.FirstChild);
										}
										else
										{
											node.AppendChild(xmlNode2);
										}
										if (xmlNode == null)
										{
											xmlNode = xmlNode2;
										}
									}
								}
								else if (dataColumn.ColumnMapping == MappingType.SimpleContent)
								{
									XmlAttribute xmlAttribute = this.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
									xmlAttribute.Value = "true";
									node.SetAttributeNode(xmlAttribute);
									this._bHasXSINIL = true;
								}
							}
						}
					}
					finally
					{
						this.IgnoreXmlEvents = ignoreXmlEvents;
						node.ElementState = newState;
					}
					this.OnFoliated(node);
				}
			}
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00015640 File Offset: 0x00013840
		private XmlNode GetColumnInsertAfterLocation(DataRow row, DataColumn col, XmlBoundElement rowElement)
		{
			XmlNode result = null;
			if (this.IsTextOnly(col))
			{
				return null;
			}
			for (XmlNode xmlNode = rowElement.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if (!XmlDataDocument.IsTextLikeNode(xmlNode))
				{
					IL_81:
					while (xmlNode != null && xmlNode.NodeType == XmlNodeType.Element)
					{
						XmlElement e = xmlNode as XmlElement;
						if (this._mapper.GetRowFromElement(e) != null)
						{
							break;
						}
						object columnSchemaForNode = this._mapper.GetColumnSchemaForNode(rowElement, xmlNode);
						if (columnSchemaForNode == null || !(columnSchemaForNode is DataColumn) || ((DataColumn)columnSchemaForNode).Ordinal > col.Ordinal)
						{
							break;
						}
						result = xmlNode;
						xmlNode = xmlNode.NextSibling;
					}
					return result;
				}
				result = xmlNode;
			}
			goto IL_81;
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x000156D4 File Offset: 0x000138D4
		private ArrayList GetNestedChildRelations(DataRow row)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in row.Table.ChildRelations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				if (dataRelation.Nested)
				{
					arrayList.Add(dataRelation);
				}
			}
			return arrayList;
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00015744 File Offset: 0x00013944
		private DataRow GetNestedParent(DataRow row)
		{
			DataRelation nestedParentRelation = XmlDataDocument.GetNestedParentRelation(row);
			if (nestedParentRelation != null)
			{
				return row.GetParentRow(nestedParentRelation);
			}
			return null;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00015764 File Offset: 0x00013964
		private static DataRelation GetNestedParentRelation(DataRow row)
		{
			DataRelation[] nestedParentRelations = row.Table.NestedParentRelations;
			if (nestedParentRelations.Length == 0)
			{
				return null;
			}
			return nestedParentRelations[0];
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00015786 File Offset: 0x00013986
		private DataColumn GetTextOnlyColumn(DataRow row)
		{
			return row.Table.XmlText;
		}

		/// <summary>Retrieves the <see cref="T:System.Data.DataRow" /> associated with the specified <see cref="T:System.Xml.XmlElement" />.</summary>
		/// <param name="e">The <see langword="XmlElement" /> whose associated <see langword="DataRow" /> you want to retrieve.</param>
		/// <returns>The <see langword="DataRow" /> containing a representation of the <see langword="XmlElement" />; <see langword="null" /> if there is no <see langword="DataRow" /> associated with the <see langword="XmlElement" />.</returns>
		// Token: 0x060005BF RID: 1471 RVA: 0x00015793 File Offset: 0x00013993
		public DataRow GetRowFromElement(XmlElement e)
		{
			return this._mapper.GetRowFromElement(e);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x000157A4 File Offset: 0x000139A4
		private XmlNode GetRowInsertBeforeLocation(DataRow row, XmlElement rowElement, XmlNode parentElement)
		{
			DataRow dataRow = row;
			int i = 0;
			while (i < row.Table.Rows.Count && row != row.Table.Rows[i])
			{
				i++;
			}
			int num = i;
			DataRow nestedParent = this.GetNestedParent(row);
			for (i = num + 1; i < row.Table.Rows.Count; i++)
			{
				dataRow = row.Table.Rows[i];
				if (this.GetNestedParent(dataRow) == nestedParent && this.GetElementFromRow(dataRow).ParentNode == parentElement)
				{
					break;
				}
			}
			if (i < row.Table.Rows.Count)
			{
				return this.GetElementFromRow(dataRow);
			}
			return null;
		}

		/// <summary>Retrieves the <see cref="T:System.Xml.XmlElement" /> associated with the specified <see cref="T:System.Data.DataRow" />.</summary>
		/// <param name="r">The <see langword="DataRow" /> whose associated <see langword="XmlElement" /> you want to retrieve.</param>
		/// <returns>The <see langword="XmlElement" /> containing a representation of the specified <see langword="DataRow" />.</returns>
		// Token: 0x060005C1 RID: 1473 RVA: 0x00015850 File Offset: 0x00013A50
		public XmlElement GetElementFromRow(DataRow r)
		{
			return r.Element;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00015858 File Offset: 0x00013A58
		internal bool HasPointers(XmlNode node)
		{
			bool result;
			for (;;)
			{
				try
				{
					if (this._pointers.Count > 0)
					{
						foreach (object obj in this._pointers)
						{
							if (((IXmlDataVirtualNode)((DictionaryEntry)obj).Value).IsOnNode(node))
							{
								return true;
							}
						}
					}
					result = false;
				}
				catch (Exception e) when (ADP.IsCatchableExceptionType(e))
				{
					continue;
				}
				break;
			}
			return result;
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x000158FC File Offset: 0x00013AFC
		// (set) Token: 0x060005C4 RID: 1476 RVA: 0x00015904 File Offset: 0x00013B04
		internal bool IgnoreXmlEvents
		{
			get
			{
				return this._ignoreXmlEvents;
			}
			set
			{
				this._ignoreXmlEvents = value;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x0001590D File Offset: 0x00013B0D
		// (set) Token: 0x060005C6 RID: 1478 RVA: 0x00015915 File Offset: 0x00013B15
		internal bool IgnoreDataSetEvents
		{
			get
			{
				return this._ignoreDataSetEvents;
			}
			set
			{
				this._ignoreDataSetEvents = value;
			}
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0001591E File Offset: 0x00013B1E
		private bool IsFoliated(XmlElement element)
		{
			return !(element is XmlBoundElement) || ((XmlBoundElement)element).IsFoliated;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00015935 File Offset: 0x00013B35
		private bool IsFoliated(XmlBoundElement be)
		{
			return be.IsFoliated;
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x0001593D File Offset: 0x00013B3D
		// (set) Token: 0x060005CA RID: 1482 RVA: 0x00015945 File Offset: 0x00013B45
		internal bool IsFoliationEnabled
		{
			get
			{
				return this._isFoliationEnabled;
			}
			set
			{
				this._isFoliationEnabled = value;
			}
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00015950 File Offset: 0x00013B50
		internal XmlNode CloneTree(DataPointer other)
		{
			this.EnsurePopulatedMode();
			bool ignoreDataSetEvents = this._ignoreDataSetEvents;
			bool ignoreXmlEvents = this._ignoreXmlEvents;
			bool isFoliationEnabled = this.IsFoliationEnabled;
			bool fAssociateDataRow = this._fAssociateDataRow;
			XmlNode xmlNode;
			try
			{
				this._ignoreDataSetEvents = true;
				this._ignoreXmlEvents = true;
				this.IsFoliationEnabled = false;
				this._fAssociateDataRow = false;
				xmlNode = this.CloneTreeInternal(other);
				this.LoadRows(null, xmlNode);
				this.SyncRows(null, xmlNode, false);
			}
			finally
			{
				this._ignoreDataSetEvents = ignoreDataSetEvents;
				this._ignoreXmlEvents = ignoreXmlEvents;
				this.IsFoliationEnabled = isFoliationEnabled;
				this._fAssociateDataRow = fAssociateDataRow;
			}
			return xmlNode;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x000159E8 File Offset: 0x00013BE8
		private XmlNode CloneTreeInternal(DataPointer other)
		{
			XmlNode xmlNode = this.CloneNode(other);
			DataPointer dataPointer = new DataPointer(other);
			try
			{
				dataPointer.AddPointer();
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					int attributeCount = dataPointer.AttributeCount;
					for (int i = 0; i < attributeCount; i++)
					{
						dataPointer.MoveToOwnerElement();
						if (dataPointer.MoveToAttribute(i))
						{
							xmlNode.Attributes.Append((XmlAttribute)this.CloneTreeInternal(dataPointer));
						}
					}
					dataPointer.MoveTo(other);
				}
				bool flag = dataPointer.MoveToFirstChild();
				while (flag)
				{
					xmlNode.AppendChild(this.CloneTreeInternal(dataPointer));
					flag = dataPointer.MoveToNextSibling();
				}
			}
			finally
			{
				dataPointer.SetNoLongerUse();
			}
			return xmlNode;
		}

		/// <summary>Creates a duplicate of the current node.</summary>
		/// <param name="deep">
		///   <see langword="true" /> to recursively clone the subtree under the specified node; <see langword="false" /> to clone only the node itself.</param>
		/// <returns>The cloned node.</returns>
		// Token: 0x060005CD RID: 1485 RVA: 0x00015A94 File Offset: 0x00013C94
		public override XmlNode CloneNode(bool deep)
		{
			XmlDataDocument xmlDataDocument = (XmlDataDocument)base.CloneNode(false);
			xmlDataDocument.Init(this.DataSet.Clone());
			xmlDataDocument._dataSet.EnforceConstraints = this._dataSet.EnforceConstraints;
			if (deep)
			{
				DataPointer dataPointer = new DataPointer(this, this);
				try
				{
					dataPointer.AddPointer();
					bool flag = dataPointer.MoveToFirstChild();
					while (flag)
					{
						XmlNode newChild;
						if (dataPointer.NodeType == XmlNodeType.Element)
						{
							newChild = xmlDataDocument.CloneTree(dataPointer);
						}
						else
						{
							newChild = xmlDataDocument.CloneNode(dataPointer);
						}
						xmlDataDocument.AppendChild(newChild);
						flag = dataPointer.MoveToNextSibling();
					}
				}
				finally
				{
					dataPointer.SetNoLongerUse();
				}
			}
			return xmlDataDocument;
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00015B38 File Offset: 0x00013D38
		private XmlNode CloneNode(DataPointer dp)
		{
			switch (dp.NodeType)
			{
			case XmlNodeType.Element:
				return this.CreateElement(dp.Prefix, dp.LocalName, dp.NamespaceURI);
			case XmlNodeType.Attribute:
				return this.CreateAttribute(dp.Prefix, dp.LocalName, dp.NamespaceURI);
			case XmlNodeType.Text:
				return this.CreateTextNode(dp.Value);
			case XmlNodeType.CDATA:
				return this.CreateCDataSection(dp.Value);
			case XmlNodeType.EntityReference:
				return this.CreateEntityReference(dp.Name);
			case XmlNodeType.ProcessingInstruction:
				return this.CreateProcessingInstruction(dp.Name, dp.Value);
			case XmlNodeType.Comment:
				return this.CreateComment(dp.Value);
			case XmlNodeType.DocumentType:
				return this.CreateDocumentType(dp.Name, dp.PublicId, dp.SystemId, dp.InternalSubset);
			case XmlNodeType.DocumentFragment:
				return this.CreateDocumentFragment();
			case XmlNodeType.Whitespace:
				return this.CreateWhitespace(dp.Value);
			case XmlNodeType.SignificantWhitespace:
				return this.CreateSignificantWhitespace(dp.Value);
			case XmlNodeType.XmlDeclaration:
				return this.CreateXmlDeclaration(dp.Version, dp.Encoding, dp.Standalone);
			}
			throw new InvalidOperationException(SR.Format("This type of node cannot be cloned: {0}.", dp.NodeType.ToString()));
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00015C94 File Offset: 0x00013E94
		internal static bool IsTextLikeNode(XmlNode n)
		{
			XmlNodeType nodeType = n.NodeType;
			if (nodeType - XmlNodeType.Text > 1)
			{
				if (nodeType == XmlNodeType.EntityReference)
				{
					return false;
				}
				if (nodeType - XmlNodeType.Whitespace > 1)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00015CBE File Offset: 0x00013EBE
		internal bool IsNotMapped(DataColumn c)
		{
			return DataSetMapper.IsNotMapped(c);
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00015CC6 File Offset: 0x00013EC6
		private bool IsSame(DataColumn c, int recNo1, int recNo2)
		{
			return c.Compare(recNo1, recNo2) == 0;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00015CD5 File Offset: 0x00013ED5
		internal bool IsTextOnly(DataColumn c)
		{
			return c.ColumnMapping == MappingType.SimpleContent;
		}

		/// <summary>Loads the <see langword="XmlDataDocument" /> using the specified URL.</summary>
		/// <param name="filename">The URL of the file containing the XML document to load.</param>
		// Token: 0x060005D3 RID: 1491 RVA: 0x00015CE0 File Offset: 0x00013EE0
		public override void Load(string filename)
		{
			this._bForceExpandEntity = true;
			base.Load(filename);
			this._bForceExpandEntity = false;
		}

		/// <summary>Loads the <see langword="XmlDataDocument" /> from the specified stream.</summary>
		/// <param name="inStream">The stream containing the XML document to load.</param>
		// Token: 0x060005D4 RID: 1492 RVA: 0x00015CF7 File Offset: 0x00013EF7
		public override void Load(Stream inStream)
		{
			this._bForceExpandEntity = true;
			base.Load(inStream);
			this._bForceExpandEntity = false;
		}

		/// <summary>Loads the <see langword="XmlDataDocument" /> from the specified <see cref="T:System.IO.TextReader" />.</summary>
		/// <param name="txtReader">The <see langword="TextReader" /> used to feed the XML data into the document.</param>
		// Token: 0x060005D5 RID: 1493 RVA: 0x00015D0E File Offset: 0x00013F0E
		public override void Load(TextReader txtReader)
		{
			this._bForceExpandEntity = true;
			base.Load(txtReader);
			this._bForceExpandEntity = false;
		}

		/// <summary>Loads the <see langword="XmlDataDocument" /> from the specified <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="reader">The <see langword="XmlReader" /> containing the XML document to load.</param>
		/// <exception cref="T:System.NotSupportedException">The XML being loaded contains entity references, and the reader cannot resolve entities.</exception>
		// Token: 0x060005D6 RID: 1494 RVA: 0x00015D28 File Offset: 0x00013F28
		public override void Load(XmlReader reader)
		{
			if (this.FirstChild != null)
			{
				throw new InvalidOperationException("Cannot load XmlDataDocument if it already contains data. Please use a new XmlDataDocument.");
			}
			try
			{
				this._ignoreXmlEvents = true;
				if (this._fDataRowCreatedSpecial)
				{
					this.UnBindSpecialListeners();
				}
				this._fAssociateDataRow = false;
				this._isFoliationEnabled = false;
				if (this._bForceExpandEntity)
				{
					((XmlTextReader)reader).EntityHandling = EntityHandling.ExpandEntities;
				}
				base.Load(reader);
				this.BindForLoad();
			}
			finally
			{
				this._ignoreXmlEvents = false;
				this._isFoliationEnabled = true;
				this._autoFoliationState = ElementState.StrongFoliation;
				this._fAssociateDataRow = true;
			}
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00015DBC File Offset: 0x00013FBC
		private void LoadDataSetFromTree()
		{
			this._ignoreDataSetEvents = true;
			this._ignoreXmlEvents = true;
			bool isFoliationEnabled = this.IsFoliationEnabled;
			this.IsFoliationEnabled = false;
			bool enforceConstraints = this._dataSet.EnforceConstraints;
			this._dataSet.EnforceConstraints = false;
			try
			{
				this.LoadRows(null, base.DocumentElement);
				this.SyncRows(null, base.DocumentElement, true);
				this._dataSet.EnforceConstraints = enforceConstraints;
			}
			finally
			{
				this._ignoreDataSetEvents = false;
				this._ignoreXmlEvents = false;
				this.IsFoliationEnabled = isFoliationEnabled;
			}
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00015E4C File Offset: 0x0001404C
		private void LoadTreeFromDataSet(DataSet ds)
		{
			this._ignoreDataSetEvents = true;
			this._ignoreXmlEvents = true;
			bool isFoliationEnabled = this.IsFoliationEnabled;
			this.IsFoliationEnabled = false;
			this._fAssociateDataRow = false;
			DataTable[] array = this.OrderTables(ds);
			try
			{
				for (int i = 0; i < array.Length; i++)
				{
					foreach (object obj in array[i].Rows)
					{
						DataRow dataRow = (DataRow)obj;
						this.AttachBoundElementToDataRow(dataRow);
						DataRowState rowState = dataRow.RowState;
						switch (rowState)
						{
						case DataRowState.Detached:
						case DataRowState.Detached | DataRowState.Unchanged:
							continue;
						case DataRowState.Unchanged:
						case DataRowState.Added:
							break;
						default:
							if (rowState == DataRowState.Deleted || rowState != DataRowState.Modified)
							{
								continue;
							}
							break;
						}
						this.OnAddRow(dataRow);
					}
				}
			}
			finally
			{
				this._ignoreDataSetEvents = false;
				this._ignoreXmlEvents = false;
				this.IsFoliationEnabled = isFoliationEnabled;
				this._fAssociateDataRow = true;
			}
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00015F4C File Offset: 0x0001414C
		private void LoadRows(XmlBoundElement rowElem, XmlNode node)
		{
			XmlBoundElement xmlBoundElement = node as XmlBoundElement;
			if (xmlBoundElement != null)
			{
				DataTable dataTable = this._mapper.SearchMatchingTableSchema(rowElem, xmlBoundElement);
				if (dataTable != null)
				{
					DataRow r = this.GetRowFromElement(xmlBoundElement);
					if (xmlBoundElement.ElementState == ElementState.None)
					{
						xmlBoundElement.ElementState = ElementState.WeakFoliation;
					}
					r = dataTable.CreateEmptyRow();
					this.Bind(r, xmlBoundElement);
					rowElem = xmlBoundElement;
				}
			}
			for (XmlNode xmlNode = node.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				this.LoadRows(rowElem, xmlNode);
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x00015FB8 File Offset: 0x000141B8
		internal DataSetMapper Mapper
		{
			get
			{
				return this._mapper;
			}
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00015FC0 File Offset: 0x000141C0
		internal void OnDataRowCreated(object oDataSet, DataRow row)
		{
			this.OnNewRow(row);
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00015FC9 File Offset: 0x000141C9
		internal void OnClearCalled(object oDataSet, DataTable table)
		{
			throw new NotSupportedException("Clear function on DateSet and DataTable is not supported on XmlDataDocument.");
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00015FD5 File Offset: 0x000141D5
		internal void OnDataRowCreatedSpecial(object oDataSet, DataRow row)
		{
			this.Bind(true);
			this.OnNewRow(row);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00015FE5 File Offset: 0x000141E5
		internal void OnNewRow(DataRow row)
		{
			this.AttachBoundElementToDataRow(row);
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00015FF0 File Offset: 0x000141F0
		private XmlBoundElement AttachBoundElementToDataRow(DataRow row)
		{
			DataTable table = row.Table;
			XmlBoundElement xmlBoundElement = new XmlBoundElement(string.Empty, table.EncodedTableName, table.Namespace, this);
			xmlBoundElement.IsEmpty = false;
			this.Bind(row, xmlBoundElement);
			xmlBoundElement.ElementState = ElementState.Defoliated;
			return xmlBoundElement;
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00016034 File Offset: 0x00014234
		private bool NeedXSI_NilAttr(DataRow row)
		{
			DataTable table = row.Table;
			return table._xmlText != null && Convert.IsDBNull(row[table._xmlText]);
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00016064 File Offset: 0x00014264
		private void OnAddRow(DataRow row)
		{
			XmlBoundElement xmlBoundElement = (XmlBoundElement)this.GetElementFromRow(row);
			if (this.NeedXSI_NilAttr(row) && !xmlBoundElement.IsFoliated)
			{
				this.ForceFoliation(xmlBoundElement, this.AutoFoliationState);
			}
			if (this.GetRowFromElement(base.DocumentElement) != null && this.GetNestedParent(row) == null)
			{
				this.DemoteDocumentElement();
			}
			this.EnsureDocumentElement().AppendChild(xmlBoundElement);
			this.FixNestedChildren(row, xmlBoundElement);
			this.OnNestedParentChange(row, xmlBoundElement, null);
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x000160D8 File Offset: 0x000142D8
		private void OnColumnValueChanged(DataRow row, DataColumn col, XmlBoundElement rowElement)
		{
			if (!this.IsNotMapped(col))
			{
				object value = row[col];
				if (col.ColumnMapping == MappingType.SimpleContent && Convert.IsDBNull(value) && !rowElement.IsFoliated)
				{
					this.ForceFoliation(rowElement, ElementState.WeakFoliation);
				}
				else if (!this.IsFoliated(rowElement))
				{
					goto IL_318;
				}
				if (this.IsTextOnly(col))
				{
					if (Convert.IsDBNull(value))
					{
						value = string.Empty;
						XmlAttribute xmlAttribute = rowElement.GetAttributeNode("xsi:nil");
						if (xmlAttribute == null)
						{
							xmlAttribute = this.CreateAttribute("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance");
							xmlAttribute.Value = "true";
							rowElement.SetAttributeNode(xmlAttribute);
							this._bHasXSINIL = true;
						}
						else
						{
							xmlAttribute.Value = "true";
						}
					}
					else
					{
						XmlAttribute attributeNode = rowElement.GetAttributeNode("xsi:nil");
						if (attributeNode != null)
						{
							attributeNode.Value = "false";
						}
					}
					this.ReplaceInitialChildText(rowElement, col.ConvertObjectToXml(value));
				}
				else
				{
					bool flag = false;
					if (col.ColumnMapping == MappingType.Attribute)
					{
						foreach (object obj in rowElement.Attributes)
						{
							XmlAttribute xmlAttribute2 = (XmlAttribute)obj;
							if (xmlAttribute2.LocalName == col.EncodedColumnName && xmlAttribute2.NamespaceURI == col.Namespace)
							{
								if (Convert.IsDBNull(value))
								{
									xmlAttribute2.OwnerElement.Attributes.Remove(xmlAttribute2);
								}
								else
								{
									xmlAttribute2.Value = col.ConvertObjectToXml(value);
								}
								flag = true;
								break;
							}
						}
						if (!flag && !Convert.IsDBNull(value))
						{
							rowElement.SetAttribute(col.EncodedColumnName, col.Namespace, col.ConvertObjectToXml(value));
						}
					}
					else
					{
						RegionIterator regionIterator = new RegionIterator(rowElement);
						bool flag2 = regionIterator.Next();
						while (flag2)
						{
							if (regionIterator.CurrentNode.NodeType == XmlNodeType.Element)
							{
								XmlElement xmlElement = (XmlElement)regionIterator.CurrentNode;
								XmlBoundElement xmlBoundElement = xmlElement as XmlBoundElement;
								if (xmlBoundElement != null && xmlBoundElement.Row != null)
								{
									flag2 = regionIterator.NextRight();
									continue;
								}
								if (xmlElement.LocalName == col.EncodedColumnName && xmlElement.NamespaceURI == col.Namespace)
								{
									flag = true;
									if (Convert.IsDBNull(value))
									{
										this.PromoteNonValueChildren(xmlElement);
										flag2 = regionIterator.NextRight();
										xmlElement.ParentNode.RemoveChild(xmlElement);
										continue;
									}
									this.ReplaceInitialChildText(xmlElement, col.ConvertObjectToXml(value));
									XmlAttribute attributeNode2 = xmlElement.GetAttributeNode("xsi:nil");
									if (attributeNode2 != null)
									{
										attributeNode2.Value = "false";
										goto IL_318;
									}
									goto IL_318;
								}
							}
							flag2 = regionIterator.Next();
						}
						if (!flag && !Convert.IsDBNull(value))
						{
							XmlElement xmlElement2 = new XmlBoundElement(string.Empty, col.EncodedColumnName, col.Namespace, this);
							xmlElement2.AppendChild(this.CreateTextNode(col.ConvertObjectToXml(value)));
							XmlNode columnInsertAfterLocation = this.GetColumnInsertAfterLocation(row, col, rowElement);
							if (columnInsertAfterLocation != null)
							{
								rowElement.InsertAfter(xmlElement2, columnInsertAfterLocation);
							}
							else if (rowElement.FirstChild != null)
							{
								rowElement.InsertBefore(xmlElement2, rowElement.FirstChild);
							}
							else
							{
								rowElement.AppendChild(xmlElement2);
							}
						}
					}
				}
			}
			IL_318:
			DataRelation nestedParentRelation = XmlDataDocument.GetNestedParentRelation(row);
			if (nestedParentRelation != null && nestedParentRelation.ChildKey.ContainsColumn(col))
			{
				this.OnNestedParentChange(row, rowElement, col);
			}
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00016434 File Offset: 0x00014634
		private void OnColumnChanged(object sender, DataColumnChangeEventArgs args)
		{
			if (this._ignoreDataSetEvents)
			{
				return;
			}
			bool ignoreXmlEvents = this._ignoreXmlEvents;
			this._ignoreXmlEvents = true;
			bool isFoliationEnabled = this.IsFoliationEnabled;
			this.IsFoliationEnabled = false;
			try
			{
				DataRow row = args.Row;
				DataColumn column = args.Column;
				object proposedValue = args.ProposedValue;
				if (row.RowState == DataRowState.Detached)
				{
					XmlBoundElement element = row.Element;
					if (element.IsFoliated)
					{
						this.OnColumnValueChanged(row, column, element);
					}
				}
			}
			finally
			{
				this.IsFoliationEnabled = isFoliationEnabled;
				this._ignoreXmlEvents = ignoreXmlEvents;
			}
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x000164C0 File Offset: 0x000146C0
		private void OnColumnValuesChanged(DataRow row, XmlBoundElement rowElement)
		{
			if (this._columnChangeList.Count > 0)
			{
				if (((DataColumn)this._columnChangeList[0]).Table == row.Table)
				{
					using (IEnumerator enumerator = this._columnChangeList.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							DataColumn col = (DataColumn)obj;
							this.OnColumnValueChanged(row, col, rowElement);
						}
						goto IL_F8;
					}
				}
				using (IEnumerator enumerator = row.Table.Columns.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj2 = enumerator.Current;
						DataColumn col2 = (DataColumn)obj2;
						this.OnColumnValueChanged(row, col2, rowElement);
					}
					goto IL_F8;
				}
			}
			foreach (object obj3 in row.Table.Columns)
			{
				DataColumn col3 = (DataColumn)obj3;
				this.OnColumnValueChanged(row, col3, rowElement);
			}
			IL_F8:
			this._columnChangeList.Clear();
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x000165F8 File Offset: 0x000147F8
		private void OnDeleteRow(DataRow row, XmlBoundElement rowElement)
		{
			if (rowElement == base.DocumentElement)
			{
				this.DemoteDocumentElement();
			}
			this.PromoteInnerRegions(rowElement);
			rowElement.ParentNode.RemoveChild(rowElement);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00016620 File Offset: 0x00014820
		private void OnDeletingRow(DataRow row, XmlBoundElement rowElement)
		{
			if (this.IsFoliated(rowElement))
			{
				return;
			}
			bool ignoreXmlEvents = this.IgnoreXmlEvents;
			this.IgnoreXmlEvents = true;
			bool isFoliationEnabled = this.IsFoliationEnabled;
			this.IsFoliationEnabled = true;
			try
			{
				this.Foliate(rowElement);
			}
			finally
			{
				this.IsFoliationEnabled = isFoliationEnabled;
				this.IgnoreXmlEvents = ignoreXmlEvents;
			}
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0001667C File Offset: 0x0001487C
		private void OnFoliated(XmlNode node)
		{
			for (;;)
			{
				try
				{
					if (this._pointers.Count > 0)
					{
						foreach (object obj in this._pointers)
						{
							((IXmlDataVirtualNode)((DictionaryEntry)obj).Value).OnFoliated(node);
						}
					}
				}
				catch (Exception e) when (ADP.IsCatchableExceptionType(e))
				{
					continue;
				}
				break;
			}
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00016718 File Offset: 0x00014918
		private DataColumn FindAssociatedParentColumn(DataRelation relation, DataColumn childCol)
		{
			DataColumn[] columnsReference = relation.ChildKey.ColumnsReference;
			for (int i = 0; i < columnsReference.Length; i++)
			{
				if (childCol == columnsReference[i])
				{
					return relation.ParentKey.ColumnsReference[i];
				}
			}
			return null;
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0001675C File Offset: 0x0001495C
		private void OnNestedParentChange(DataRow child, XmlBoundElement childElement, DataColumn childCol)
		{
			DataRow dataRow;
			if (childElement == base.DocumentElement || childElement.ParentNode == null)
			{
				dataRow = null;
			}
			else
			{
				dataRow = this.GetRowFromElement((XmlElement)childElement.ParentNode);
			}
			DataRow nestedParent = this.GetNestedParent(child);
			if (dataRow != nestedParent)
			{
				if (nestedParent != null)
				{
					this.GetElementFromRow(nestedParent).AppendChild(childElement);
					return;
				}
				DataRelation nestedParentRelation = XmlDataDocument.GetNestedParentRelation(child);
				if (childCol == null || nestedParentRelation == null || Convert.IsDBNull(child[childCol]))
				{
					this.EnsureNonRowDocumentElement().AppendChild(childElement);
					return;
				}
				DataColumn dataColumn = this.FindAssociatedParentColumn(nestedParentRelation, childCol);
				object value = dataColumn.ConvertValue(child[childCol]);
				if (dataRow._tempRecord != -1 && dataColumn.CompareValueTo(dataRow._tempRecord, value) != 0)
				{
					this.EnsureNonRowDocumentElement().AppendChild(childElement);
				}
			}
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00016818 File Offset: 0x00014A18
		private void OnNodeChanged(object sender, XmlNodeChangedEventArgs args)
		{
			if (this._ignoreXmlEvents)
			{
				return;
			}
			bool ignoreDataSetEvents = this._ignoreDataSetEvents;
			bool ignoreXmlEvents = this._ignoreXmlEvents;
			bool isFoliationEnabled = this.IsFoliationEnabled;
			this._ignoreDataSetEvents = true;
			this._ignoreXmlEvents = true;
			this.IsFoliationEnabled = false;
			bool fEnableCascading = this.DataSet._fEnableCascading;
			this.DataSet._fEnableCascading = false;
			try
			{
				XmlBoundElement rowElement = null;
				if (this._mapper.GetRegion(args.Node, out rowElement))
				{
					this.SynchronizeRowFromRowElement(rowElement);
				}
			}
			finally
			{
				this._ignoreDataSetEvents = ignoreDataSetEvents;
				this._ignoreXmlEvents = ignoreXmlEvents;
				this.IsFoliationEnabled = isFoliationEnabled;
				this.DataSet._fEnableCascading = fEnableCascading;
			}
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x000168C4 File Offset: 0x00014AC4
		private void OnNodeChanging(object sender, XmlNodeChangedEventArgs args)
		{
			if (this._ignoreXmlEvents)
			{
				return;
			}
			if (this.DataSet.EnforceConstraints)
			{
				throw new InvalidOperationException("Please set DataSet.EnforceConstraints == false before trying to edit XmlDataDocument using XML operations.");
			}
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x000168E8 File Offset: 0x00014AE8
		private void OnNodeInserted(object sender, XmlNodeChangedEventArgs args)
		{
			if (this._ignoreXmlEvents)
			{
				return;
			}
			bool ignoreDataSetEvents = this._ignoreDataSetEvents;
			bool ignoreXmlEvents = this._ignoreXmlEvents;
			bool isFoliationEnabled = this.IsFoliationEnabled;
			this._ignoreDataSetEvents = true;
			this._ignoreXmlEvents = true;
			this.IsFoliationEnabled = false;
			bool fEnableCascading = this.DataSet._fEnableCascading;
			this.DataSet._fEnableCascading = false;
			try
			{
				XmlNode node = args.Node;
				XmlNode oldParent = args.OldParent;
				XmlNode newParent = args.NewParent;
				if (this.IsConnected(newParent))
				{
					this.OnNodeInsertedInTree(node);
				}
				else
				{
					this.OnNodeInsertedInFragment(node);
				}
			}
			finally
			{
				this._ignoreDataSetEvents = ignoreDataSetEvents;
				this._ignoreXmlEvents = ignoreXmlEvents;
				this.IsFoliationEnabled = isFoliationEnabled;
				this.DataSet._fEnableCascading = fEnableCascading;
			}
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x000168C4 File Offset: 0x00014AC4
		private void OnNodeInserting(object sender, XmlNodeChangedEventArgs args)
		{
			if (this._ignoreXmlEvents)
			{
				return;
			}
			if (this.DataSet.EnforceConstraints)
			{
				throw new InvalidOperationException("Please set DataSet.EnforceConstraints == false before trying to edit XmlDataDocument using XML operations.");
			}
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x000169A8 File Offset: 0x00014BA8
		private void OnNodeRemoved(object sender, XmlNodeChangedEventArgs args)
		{
			if (this._ignoreXmlEvents)
			{
				return;
			}
			bool ignoreDataSetEvents = this._ignoreDataSetEvents;
			bool ignoreXmlEvents = this._ignoreXmlEvents;
			bool isFoliationEnabled = this.IsFoliationEnabled;
			this._ignoreDataSetEvents = true;
			this._ignoreXmlEvents = true;
			this.IsFoliationEnabled = false;
			bool fEnableCascading = this.DataSet._fEnableCascading;
			this.DataSet._fEnableCascading = false;
			try
			{
				XmlNode node = args.Node;
				XmlNode oldParent = args.OldParent;
				if (this.IsConnected(oldParent))
				{
					this.OnNodeRemovedFromTree(node, oldParent);
				}
				else
				{
					this.OnNodeRemovedFromFragment(node, oldParent);
				}
			}
			finally
			{
				this._ignoreDataSetEvents = ignoreDataSetEvents;
				this._ignoreXmlEvents = ignoreXmlEvents;
				this.IsFoliationEnabled = isFoliationEnabled;
				this.DataSet._fEnableCascading = fEnableCascading;
			}
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x000168C4 File Offset: 0x00014AC4
		private void OnNodeRemoving(object sender, XmlNodeChangedEventArgs args)
		{
			if (this._ignoreXmlEvents)
			{
				return;
			}
			if (this.DataSet.EnforceConstraints)
			{
				throw new InvalidOperationException("Please set DataSet.EnforceConstraints == false before trying to edit XmlDataDocument using XML operations.");
			}
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00016A64 File Offset: 0x00014C64
		private void OnNodeRemovedFromTree(XmlNode node, XmlNode oldParent)
		{
			XmlBoundElement rowElement;
			if (this._mapper.GetRegion(oldParent, out rowElement))
			{
				this.SynchronizeRowFromRowElement(rowElement);
			}
			XmlBoundElement xmlBoundElement = node as XmlBoundElement;
			if (xmlBoundElement != null && xmlBoundElement.Row != null)
			{
				this.EnsureDisconnectedDataRow(xmlBoundElement);
			}
			TreeIterator treeIterator = new TreeIterator(node);
			bool flag = treeIterator.NextRowElement();
			while (flag)
			{
				xmlBoundElement = (XmlBoundElement)treeIterator.CurrentNode;
				this.EnsureDisconnectedDataRow(xmlBoundElement);
				flag = treeIterator.NextRowElement();
			}
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00016AD0 File Offset: 0x00014CD0
		private void OnNodeRemovedFromFragment(XmlNode node, XmlNode oldParent)
		{
			XmlBoundElement xmlBoundElement;
			if (this._mapper.GetRegion(oldParent, out xmlBoundElement))
			{
				DataRow row = xmlBoundElement.Row;
				if (xmlBoundElement.Row.RowState == DataRowState.Detached)
				{
					this.SynchronizeRowFromRowElement(xmlBoundElement);
				}
			}
			XmlBoundElement xmlBoundElement2 = node as XmlBoundElement;
			if (xmlBoundElement2 != null && xmlBoundElement2.Row != null)
			{
				this.SetNestedParentRegion(xmlBoundElement2, null);
				return;
			}
			TreeIterator treeIterator = new TreeIterator(node);
			bool flag = treeIterator.NextRowElement();
			while (flag)
			{
				XmlBoundElement childRowElem = (XmlBoundElement)treeIterator.CurrentNode;
				this.SetNestedParentRegion(childRowElem, null);
				flag = treeIterator.NextRightRowElement();
			}
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00016B54 File Offset: 0x00014D54
		private void OnRowChanged(object sender, DataRowChangeEventArgs args)
		{
			if (this._ignoreDataSetEvents)
			{
				return;
			}
			this._ignoreXmlEvents = true;
			bool isFoliationEnabled = this.IsFoliationEnabled;
			this.IsFoliationEnabled = false;
			try
			{
				DataRow row = args.Row;
				XmlBoundElement element = row.Element;
				DataRowAction action = args.Action;
				switch (action)
				{
				case DataRowAction.Delete:
					this.OnDeleteRow(row, element);
					break;
				case DataRowAction.Change:
					this.OnColumnValuesChanged(row, element);
					break;
				case DataRowAction.Delete | DataRowAction.Change:
					break;
				case DataRowAction.Rollback:
				{
					DataRowState rollbackState = this._rollbackState;
					if (rollbackState != DataRowState.Added)
					{
						if (rollbackState != DataRowState.Deleted)
						{
							if (rollbackState == DataRowState.Modified)
							{
								this.OnColumnValuesChanged(row, element);
							}
						}
						else
						{
							this.OnUndeleteRow(row, element);
							this.UpdateAllColumns(row, element);
						}
					}
					else
					{
						element.ParentNode.RemoveChild(element);
					}
					break;
				}
				default:
					if (action != DataRowAction.Commit)
					{
						if (action == DataRowAction.Add)
						{
							this.OnAddRow(row);
						}
					}
					else if (row.RowState == DataRowState.Detached)
					{
						element.RemoveAll();
					}
					break;
				}
			}
			finally
			{
				this.IsFoliationEnabled = isFoliationEnabled;
				this._ignoreXmlEvents = false;
			}
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00016C48 File Offset: 0x00014E48
		private void OnRowChanging(object sender, DataRowChangeEventArgs args)
		{
			DataRow row = args.Row;
			if (args.Action == DataRowAction.Delete && row.Element != null)
			{
				this.OnDeletingRow(row, row.Element);
				return;
			}
			if (this._ignoreDataSetEvents)
			{
				return;
			}
			bool isFoliationEnabled = this.IsFoliationEnabled;
			this.IsFoliationEnabled = false;
			try
			{
				this._ignoreXmlEvents = true;
				XmlElement elementFromRow = this.GetElementFromRow(row);
				if (elementFromRow != null)
				{
					DataRowAction action = args.Action;
					int recordFromVersion;
					int recordFromVersion2;
					switch (action)
					{
					case DataRowAction.Delete:
					case DataRowAction.Delete | DataRowAction.Change:
						goto IL_212;
					case DataRowAction.Change:
						break;
					case DataRowAction.Rollback:
					{
						this._rollbackState = row.RowState;
						DataRowState rollbackState = this._rollbackState;
						if (rollbackState <= DataRowState.Added)
						{
							if (rollbackState != DataRowState.Detached && rollbackState != DataRowState.Added)
							{
								return;
							}
							goto IL_212;
						}
						else
						{
							if (rollbackState == DataRowState.Deleted)
							{
								goto IL_212;
							}
							if (rollbackState != DataRowState.Modified)
							{
								return;
							}
							this._columnChangeList.Clear();
							recordFromVersion = row.GetRecordFromVersion(DataRowVersion.Original);
							recordFromVersion2 = row.GetRecordFromVersion(DataRowVersion.Current);
							using (IEnumerator enumerator = row.Table.Columns.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									object obj = enumerator.Current;
									DataColumn dataColumn = (DataColumn)obj;
									if (!this.IsSame(dataColumn, recordFromVersion, recordFromVersion2))
									{
										this._columnChangeList.Add(dataColumn);
									}
								}
								return;
							}
						}
						break;
					}
					default:
						if (action != DataRowAction.Commit && action != DataRowAction.Add)
						{
							goto IL_212;
						}
						goto IL_212;
					}
					this._columnChangeList.Clear();
					recordFromVersion = row.GetRecordFromVersion(DataRowVersion.Proposed);
					recordFromVersion2 = row.GetRecordFromVersion(DataRowVersion.Current);
					foreach (object obj2 in row.Table.Columns)
					{
						DataColumn dataColumn2 = (DataColumn)obj2;
						object value = row[dataColumn2, DataRowVersion.Proposed];
						object value2 = row[dataColumn2, DataRowVersion.Current];
						if (Convert.IsDBNull(value) && !Convert.IsDBNull(value2) && dataColumn2.ColumnMapping != MappingType.Hidden)
						{
							this.FoliateIfDataPointers(row, elementFromRow);
						}
						if (!this.IsSame(dataColumn2, recordFromVersion, recordFromVersion2))
						{
							this._columnChangeList.Add(dataColumn2);
						}
					}
				}
				IL_212:;
			}
			finally
			{
				this._ignoreXmlEvents = false;
				this.IsFoliationEnabled = isFoliationEnabled;
			}
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00016EC4 File Offset: 0x000150C4
		private void OnDataSetPropertyChanging(object oDataSet, PropertyChangedEventArgs args)
		{
			if (args.PropertyName == "DataSetName")
			{
				throw new InvalidOperationException("Cannot change the DataSet name once the DataSet is mapped to a loaded XML document.");
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00016EE4 File Offset: 0x000150E4
		private void OnColumnPropertyChanging(object oColumn, PropertyChangedEventArgs args)
		{
			if (args.PropertyName == "ColumnName")
			{
				throw new InvalidOperationException("Cannot change the column name once the associated DataSet is mapped to a loaded XML document.");
			}
			if (args.PropertyName == "Namespace")
			{
				throw new InvalidOperationException("Cannot change the column namespace once the associated DataSet is mapped to a loaded XML document.");
			}
			if (args.PropertyName == "ColumnMapping")
			{
				throw new InvalidOperationException("Cannot change the ColumnMapping property once the associated DataSet is mapped to a loaded XML document.");
			}
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00016F48 File Offset: 0x00015148
		private void OnTablePropertyChanging(object oTable, PropertyChangedEventArgs args)
		{
			if (args.PropertyName == "TableName")
			{
				throw new InvalidOperationException("Cannot change the table name once the associated DataSet is mapped to a loaded XML document.");
			}
			if (args.PropertyName == "Namespace")
			{
				throw new InvalidOperationException("Cannot change the table namespace once the associated DataSet is mapped to a loaded XML document.");
			}
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00016F84 File Offset: 0x00015184
		private void OnTableColumnsChanging(object oColumnsCollection, CollectionChangeEventArgs args)
		{
			throw new InvalidOperationException("Cannot add or remove columns from the table once the DataSet is mapped to a loaded XML document.");
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00016F90 File Offset: 0x00015190
		private void OnDataSetTablesChanging(object oTablesCollection, CollectionChangeEventArgs args)
		{
			throw new InvalidOperationException("Cannot add or remove tables from the DataSet once the DataSet is mapped to a loaded XML document.");
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00016F9C File Offset: 0x0001519C
		private void OnDataSetRelationsChanging(object oRelationsCollection, CollectionChangeEventArgs args)
		{
			DataRelation dataRelation = (DataRelation)args.Element;
			if (dataRelation != null && dataRelation.Nested)
			{
				throw new InvalidOperationException("Cannot add, remove, or change Nested relations from the DataSet once the DataSet is mapped to a loaded XML document.");
			}
			if (args.Action == CollectionChangeAction.Refresh)
			{
				using (IEnumerator enumerator = ((DataRelationCollection)oRelationsCollection).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (((DataRelation)enumerator.Current).Nested)
						{
							throw new InvalidOperationException("Cannot add, remove, or change Nested relations from the DataSet once the DataSet is mapped to a loaded XML document.");
						}
					}
				}
			}
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0001702C File Offset: 0x0001522C
		private void OnRelationPropertyChanging(object oRelationsCollection, PropertyChangedEventArgs args)
		{
			if (args.PropertyName == "Nested")
			{
				throw new InvalidOperationException("Cannot add, remove, or change Nested relations from the DataSet once the DataSet is mapped to a loaded XML document.");
			}
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0001704C File Offset: 0x0001524C
		private void OnUndeleteRow(DataRow row, XmlElement rowElement)
		{
			if (rowElement.ParentNode != null)
			{
				rowElement.ParentNode.RemoveChild(rowElement);
			}
			DataRow nestedParent = this.GetNestedParent(row);
			XmlElement xmlElement;
			if (nestedParent == null)
			{
				xmlElement = this.EnsureNonRowDocumentElement();
			}
			else
			{
				xmlElement = this.GetElementFromRow(nestedParent);
			}
			XmlNode rowInsertBeforeLocation;
			if ((rowInsertBeforeLocation = this.GetRowInsertBeforeLocation(row, rowElement, xmlElement)) != null)
			{
				xmlElement.InsertBefore(rowElement, rowInsertBeforeLocation);
			}
			else
			{
				xmlElement.AppendChild(rowElement);
			}
			this.FixNestedChildren(row, rowElement);
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x000170B2 File Offset: 0x000152B2
		private void PromoteChild(XmlNode child, XmlNode prevSibling)
		{
			if (child.ParentNode != null)
			{
				child.ParentNode.RemoveChild(child);
			}
			prevSibling.ParentNode.InsertAfter(child, prevSibling);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x000170D8 File Offset: 0x000152D8
		private void PromoteInnerRegions(XmlNode parent)
		{
			XmlBoundElement parentRowElem;
			this._mapper.GetRegion(parent.ParentNode, out parentRowElem);
			TreeIterator treeIterator = new TreeIterator(parent);
			bool flag = treeIterator.NextRowElement();
			while (flag)
			{
				XmlBoundElement xmlBoundElement = (XmlBoundElement)treeIterator.CurrentNode;
				flag = treeIterator.NextRightRowElement();
				this.PromoteChild(xmlBoundElement, parent);
				this.SetNestedParentRegion(xmlBoundElement, parentRowElem);
			}
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00017134 File Offset: 0x00015334
		private void PromoteNonValueChildren(XmlNode parent)
		{
			XmlNode prevSibling = parent;
			XmlNode xmlNode = parent.FirstChild;
			bool flag = true;
			while (xmlNode != null)
			{
				XmlNode nextSibling = xmlNode.NextSibling;
				if (!flag || !XmlDataDocument.IsTextLikeNode(xmlNode))
				{
					flag = false;
					nextSibling = xmlNode.NextSibling;
					this.PromoteChild(xmlNode, prevSibling);
					prevSibling = xmlNode;
				}
				xmlNode = nextSibling;
			}
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0001717A File Offset: 0x0001537A
		private void RemoveInitialTextNodes(XmlNode node)
		{
			while (node != null && XmlDataDocument.IsTextLikeNode(node))
			{
				XmlNode nextSibling = node.NextSibling;
				node.ParentNode.RemoveChild(node);
				node = nextSibling;
			}
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x000171A0 File Offset: 0x000153A0
		private void ReplaceInitialChildText(XmlNode parent, string value)
		{
			XmlNode xmlNode = parent.FirstChild;
			while (xmlNode != null && xmlNode.NodeType == XmlNodeType.Whitespace)
			{
				xmlNode = xmlNode.NextSibling;
			}
			if (xmlNode != null)
			{
				if (xmlNode.NodeType == XmlNodeType.Text)
				{
					xmlNode.Value = value;
				}
				else
				{
					xmlNode = parent.InsertBefore(this.CreateTextNode(value), xmlNode);
				}
				this.RemoveInitialTextNodes(xmlNode.NextSibling);
				return;
			}
			parent.AppendChild(this.CreateTextNode(value));
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0001720C File Offset: 0x0001540C
		internal XmlNode SafeFirstChild(XmlNode n)
		{
			XmlBoundElement xmlBoundElement = n as XmlBoundElement;
			if (xmlBoundElement != null)
			{
				return xmlBoundElement.SafeFirstChild;
			}
			return n.FirstChild;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00017230 File Offset: 0x00015430
		internal XmlNode SafeNextSibling(XmlNode n)
		{
			XmlBoundElement xmlBoundElement = n as XmlBoundElement;
			if (xmlBoundElement != null)
			{
				return xmlBoundElement.SafeNextSibling;
			}
			return n.NextSibling;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00017254 File Offset: 0x00015454
		internal XmlNode SafePreviousSibling(XmlNode n)
		{
			XmlBoundElement xmlBoundElement = n as XmlBoundElement;
			if (xmlBoundElement != null)
			{
				return xmlBoundElement.SafePreviousSibling;
			}
			return n.PreviousSibling;
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00017278 File Offset: 0x00015478
		internal static void SetRowValueToNull(DataRow row, DataColumn col)
		{
			if (!row.IsNull(col))
			{
				row[col] = DBNull.Value;
			}
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00017290 File Offset: 0x00015490
		internal static void SetRowValueFromXmlText(DataRow row, DataColumn col, string xmlText)
		{
			object obj;
			try
			{
				obj = col.ConvertXmlToObject(xmlText);
			}
			catch (Exception e) when (ADP.IsCatchableExceptionType(e))
			{
				XmlDataDocument.SetRowValueToNull(row, col);
				return;
			}
			if (!obj.Equals(row[col]))
			{
				row[col] = obj;
			}
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x000172F0 File Offset: 0x000154F0
		private void SynchronizeRowFromRowElement(XmlBoundElement rowElement)
		{
			this.SynchronizeRowFromRowElement(rowElement, null);
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x000172FC File Offset: 0x000154FC
		private void SynchronizeRowFromRowElement(XmlBoundElement rowElement, ArrayList rowElemList)
		{
			DataRow row = rowElement.Row;
			if (row.RowState == DataRowState.Deleted)
			{
				return;
			}
			row.BeginEdit();
			this.SynchronizeRowFromRowElementEx(rowElement, rowElemList);
			row.EndEdit();
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00017330 File Offset: 0x00015530
		private void SynchronizeRowFromRowElementEx(XmlBoundElement rowElement, ArrayList rowElemList)
		{
			DataRow row = rowElement.Row;
			DataTable table = row.Table;
			Hashtable hashtable = new Hashtable();
			string a = string.Empty;
			RegionIterator regionIterator = new RegionIterator(rowElement);
			DataColumn textOnlyColumn = this.GetTextOnlyColumn(row);
			bool flag;
			if (textOnlyColumn != null)
			{
				hashtable[textOnlyColumn] = textOnlyColumn;
				string text;
				flag = regionIterator.NextInitialTextLikeNodes(out text);
				if (text.Length == 0 && ((a = rowElement.GetAttribute("xsi:nil")) == "1" || a == "true"))
				{
					row[textOnlyColumn] = DBNull.Value;
				}
				else
				{
					XmlDataDocument.SetRowValueFromXmlText(row, textOnlyColumn, text);
				}
			}
			else
			{
				flag = regionIterator.Next();
			}
			while (flag)
			{
				XmlElement xmlElement = regionIterator.CurrentNode as XmlElement;
				if (xmlElement == null)
				{
					flag = regionIterator.Next();
				}
				else
				{
					XmlBoundElement xmlBoundElement = xmlElement as XmlBoundElement;
					if (xmlBoundElement != null && xmlBoundElement.Row != null)
					{
						if (rowElemList != null)
						{
							rowElemList.Add(xmlElement);
						}
						flag = regionIterator.NextRight();
					}
					else
					{
						DataColumn columnSchemaForNode = this._mapper.GetColumnSchemaForNode(rowElement, xmlElement);
						if (columnSchemaForNode != null && hashtable[columnSchemaForNode] == null)
						{
							hashtable[columnSchemaForNode] = columnSchemaForNode;
							string text2;
							flag = regionIterator.NextInitialTextLikeNodes(out text2);
							if (text2.Length == 0 && ((a = xmlElement.GetAttribute("xsi:nil")) == "1" || a == "true"))
							{
								row[columnSchemaForNode] = DBNull.Value;
							}
							else
							{
								XmlDataDocument.SetRowValueFromXmlText(row, columnSchemaForNode, text2);
							}
						}
						else
						{
							flag = regionIterator.Next();
						}
					}
				}
			}
			foreach (object obj in rowElement.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				DataColumn columnSchemaForNode2 = this._mapper.GetColumnSchemaForNode(rowElement, xmlAttribute);
				if (columnSchemaForNode2 != null && hashtable[columnSchemaForNode2] == null)
				{
					hashtable[columnSchemaForNode2] = columnSchemaForNode2;
					XmlDataDocument.SetRowValueFromXmlText(row, columnSchemaForNode2, xmlAttribute.Value);
				}
			}
			foreach (object obj2 in row.Table.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj2;
				if (hashtable[dataColumn] == null && !this.IsNotMapped(dataColumn))
				{
					if (!dataColumn.AutoIncrement)
					{
						XmlDataDocument.SetRowValueToNull(row, dataColumn);
					}
					else
					{
						dataColumn.Init(row._tempRecord);
					}
				}
			}
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x000175BC File Offset: 0x000157BC
		private void UpdateAllColumns(DataRow row, XmlBoundElement rowElement)
		{
			foreach (object obj in row.Table.Columns)
			{
				DataColumn col = (DataColumn)obj;
				this.OnColumnValueChanged(row, col, rowElement);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlDataDocument" /> class.</summary>
		// Token: 0x0600060A RID: 1546 RVA: 0x0001761C File Offset: 0x0001581C
		public XmlDataDocument() : base(new XmlDataImplementation())
		{
			this.Init();
			this.AttachDataSet(new DataSet());
			this._dataSet.EnforceConstraints = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlDataDocument" /> class with the specified <see cref="T:System.Data.DataSet" />.</summary>
		/// <param name="dataset">The <see langword="DataSet" /> to load into <see langword="XmlDataDocument" />.</param>
		// Token: 0x0600060B RID: 1547 RVA: 0x00017646 File Offset: 0x00015846
		public XmlDataDocument(DataSet dataset) : base(new XmlDataImplementation())
		{
			this.Init(dataset);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0001765A File Offset: 0x0001585A
		internal XmlDataDocument(XmlImplementation imp) : base(imp)
		{
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00017664 File Offset: 0x00015864
		private void Init()
		{
			this._pointers = new Hashtable();
			this._countAddPointer = 0;
			this._columnChangeList = new ArrayList();
			this._ignoreDataSetEvents = false;
			this._isFoliationEnabled = true;
			this._optimizeStorage = true;
			this._fDataRowCreatedSpecial = false;
			this._autoFoliationState = ElementState.StrongFoliation;
			this._fAssociateDataRow = true;
			this._mapper = new DataSetMapper();
			this._foliationLock = new object();
			this._ignoreXmlEvents = true;
			this._attrXml = this.CreateAttribute("xmlns", "xml", "http://www.w3.org/2000/xmlns/");
			this._attrXml.Value = "http://www.w3.org/XML/1998/namespace";
			this._ignoreXmlEvents = false;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00017707 File Offset: 0x00015907
		private void Init(DataSet ds)
		{
			if (ds == null)
			{
				throw new ArgumentException("The DataSet parameter is invalid. It cannot be null.");
			}
			this.Init();
			if (ds.FBoundToDocument)
			{
				throw new ArgumentException("DataSet can be associated with at most one XmlDataDocument. Cannot associate the DataSet with the current XmlDataDocument because the DataSet is already associated with another XmlDataDocument.");
			}
			ds.FBoundToDocument = true;
			this._dataSet = ds;
			this.Bind(true);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00017748 File Offset: 0x00015948
		private bool IsConnected(XmlNode node)
		{
			while (node != null)
			{
				if (node == this)
				{
					return true;
				}
				XmlAttribute xmlAttribute = node as XmlAttribute;
				if (xmlAttribute != null)
				{
					node = xmlAttribute.OwnerElement;
				}
				else
				{
					node = node.ParentNode;
				}
			}
			return false;
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0001777D File Offset: 0x0001597D
		private bool IsRowLive(DataRow row)
		{
			return (row.RowState & (DataRowState.Unchanged | DataRowState.Added | DataRowState.Modified)) > (DataRowState)0;
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0001778C File Offset: 0x0001598C
		private static void SetNestedParentRow(DataRow childRow, DataRow parentRow)
		{
			DataRelation nestedParentRelation = XmlDataDocument.GetNestedParentRelation(childRow);
			if (nestedParentRelation != null)
			{
				if (parentRow == null || nestedParentRelation.ParentKey.Table != parentRow.Table)
				{
					childRow.SetParentRow(null, nestedParentRelation);
					return;
				}
				childRow.SetParentRow(parentRow, nestedParentRelation);
			}
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x000177D0 File Offset: 0x000159D0
		private void OnNodeInsertedInTree(XmlNode node)
		{
			ArrayList arrayList = new ArrayList();
			XmlBoundElement xmlBoundElement;
			if (this._mapper.GetRegion(node, out xmlBoundElement))
			{
				if (xmlBoundElement == node)
				{
					this.OnRowElementInsertedInTree(xmlBoundElement, arrayList);
				}
				else
				{
					this.OnNonRowElementInsertedInTree(node, xmlBoundElement, arrayList);
				}
			}
			else
			{
				TreeIterator treeIterator = new TreeIterator(node);
				bool flag = treeIterator.NextRowElement();
				while (flag)
				{
					arrayList.Add(treeIterator.CurrentNode);
					flag = treeIterator.NextRightRowElement();
				}
			}
			while (arrayList.Count > 0)
			{
				XmlBoundElement rowElem = (XmlBoundElement)arrayList[0];
				arrayList.RemoveAt(0);
				this.OnRowElementInsertedInTree(rowElem, arrayList);
			}
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0001785C File Offset: 0x00015A5C
		private void OnNodeInsertedInFragment(XmlNode node)
		{
			XmlBoundElement xmlBoundElement;
			if (!this._mapper.GetRegion(node, out xmlBoundElement))
			{
				return;
			}
			if (xmlBoundElement == node)
			{
				this.SetNestedParentRegion(xmlBoundElement);
				return;
			}
			ArrayList arrayList = new ArrayList();
			this.OnNonRowElementInsertedInFragment(node, xmlBoundElement, arrayList);
			while (arrayList.Count > 0)
			{
				XmlBoundElement childRowElem = (XmlBoundElement)arrayList[0];
				arrayList.RemoveAt(0);
				this.SetNestedParentRegion(childRowElem, xmlBoundElement);
			}
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x000178BC File Offset: 0x00015ABC
		private void OnRowElementInsertedInTree(XmlBoundElement rowElem, ArrayList rowElemList)
		{
			DataRow row = rowElem.Row;
			DataRowState rowState = row.RowState;
			if (rowState != DataRowState.Detached)
			{
				if (rowState != DataRowState.Deleted)
				{
					return;
				}
				row.RejectChanges();
				this.SynchronizeRowFromRowElement(rowElem, rowElemList);
				this.SetNestedParentRegion(rowElem);
			}
			else
			{
				row.Table.Rows.Add(row);
				this.SetNestedParentRegion(rowElem);
				if (rowElemList != null)
				{
					RegionIterator regionIterator = new RegionIterator(rowElem);
					bool flag = regionIterator.NextRowElement();
					while (flag)
					{
						rowElemList.Add(regionIterator.CurrentNode);
						flag = regionIterator.NextRightRowElement();
					}
					return;
				}
			}
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00017938 File Offset: 0x00015B38
		private void EnsureDisconnectedDataRow(XmlBoundElement rowElem)
		{
			DataRow row = rowElem.Row;
			DataRowState rowState = row.RowState;
			switch (rowState)
			{
			case DataRowState.Detached:
				this.SetNestedParentRegion(rowElem);
				return;
			case DataRowState.Unchanged:
				break;
			case DataRowState.Detached | DataRowState.Unchanged:
				return;
			case DataRowState.Added:
				this.EnsureFoliation(rowElem, ElementState.WeakFoliation);
				row.Delete();
				this.SetNestedParentRegion(rowElem);
				return;
			default:
				if (rowState == DataRowState.Deleted)
				{
					return;
				}
				if (rowState != DataRowState.Modified)
				{
					return;
				}
				break;
			}
			this.EnsureFoliation(rowElem, ElementState.WeakFoliation);
			row.Delete();
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x000179A4 File Offset: 0x00015BA4
		private void OnNonRowElementInsertedInTree(XmlNode node, XmlBoundElement rowElement, ArrayList rowElemList)
		{
			DataRow row = rowElement.Row;
			this.SynchronizeRowFromRowElement(rowElement);
			if (rowElemList != null)
			{
				TreeIterator treeIterator = new TreeIterator(node);
				bool flag = treeIterator.NextRowElement();
				while (flag)
				{
					rowElemList.Add(treeIterator.CurrentNode);
					flag = treeIterator.NextRightRowElement();
				}
			}
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x000179E9 File Offset: 0x00015BE9
		private void OnNonRowElementInsertedInFragment(XmlNode node, XmlBoundElement rowElement, ArrayList rowElemList)
		{
			if (rowElement.Row.RowState == DataRowState.Detached)
			{
				this.SynchronizeRowFromRowElementEx(rowElement, rowElemList);
			}
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00017A04 File Offset: 0x00015C04
		private void SetNestedParentRegion(XmlBoundElement childRowElem)
		{
			XmlBoundElement parentRowElem;
			this._mapper.GetRegion(childRowElem.ParentNode, out parentRowElem);
			this.SetNestedParentRegion(childRowElem, parentRowElem);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00017A30 File Offset: 0x00015C30
		private void SetNestedParentRegion(XmlBoundElement childRowElem, XmlBoundElement parentRowElem)
		{
			DataRow row = childRowElem.Row;
			if (parentRowElem == null)
			{
				XmlDataDocument.SetNestedParentRow(row, null);
				return;
			}
			DataRow row2 = parentRowElem.Row;
			DataRelation[] nestedParentRelations = row.Table.NestedParentRelations;
			if (nestedParentRelations.Length != 0 && nestedParentRelations[0].ParentTable == row2.Table)
			{
				XmlDataDocument.SetNestedParentRow(row, row2);
				return;
			}
			XmlDataDocument.SetNestedParentRow(row, null);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00017A85 File Offset: 0x00015C85
		internal static bool IsTextNode(XmlNodeType nt)
		{
			return nt - XmlNodeType.Text <= 1 || nt - XmlNodeType.Whitespace <= 1;
		}

		/// <summary>Creates a new <see cref="T:System.Xml.XPath.XPathNavigator" /> object for navigating this document. The <see langword="XPathNavigator" /> is positioned on the node specified in the <paramref name="node" /> parameter.</summary>
		/// <param name="node">The <see cref="T:System.Xml.XmlNode" /> you want the navigator initially positioned on.</param>
		/// <returns>An <see langword="XPathNavigator" /> used to navigate the document.</returns>
		// Token: 0x0600061B RID: 1563 RVA: 0x00017A98 File Offset: 0x00015C98
		protected override XPathNavigator CreateNavigator(XmlNode node)
		{
			if (XPathNodePointer.s_xmlNodeType_To_XpathNodeType_Map[(int)node.NodeType] == -1)
			{
				return null;
			}
			if (XmlDataDocument.IsTextNode(node.NodeType))
			{
				XmlNode parentNode = node.ParentNode;
				if (parentNode != null && parentNode.NodeType == XmlNodeType.Attribute)
				{
					return null;
				}
				XmlNode xmlNode = node.PreviousSibling;
				while (xmlNode != null && XmlDataDocument.IsTextNode(xmlNode.NodeType))
				{
					node = xmlNode;
					xmlNode = this.SafePreviousSibling(node);
				}
			}
			return new DataDocumentXPathNavigator(this, node);
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00017B04 File Offset: 0x00015D04
		[Conditional("DEBUG")]
		private void AssertLiveRows(XmlNode node)
		{
			bool isFoliationEnabled = this.IsFoliationEnabled;
			this.IsFoliationEnabled = false;
			try
			{
				XmlBoundElement xmlBoundElement = node as XmlBoundElement;
				if (xmlBoundElement != null)
				{
					DataRow row = xmlBoundElement.Row;
				}
				TreeIterator treeIterator = new TreeIterator(node);
				bool flag = treeIterator.NextRowElement();
				while (flag)
				{
					xmlBoundElement = (treeIterator.CurrentNode as XmlBoundElement);
					flag = treeIterator.NextRowElement();
				}
			}
			finally
			{
				this.IsFoliationEnabled = isFoliationEnabled;
			}
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00017B70 File Offset: 0x00015D70
		[Conditional("DEBUG")]
		private void AssertNonLiveRows(XmlNode node)
		{
			bool isFoliationEnabled = this.IsFoliationEnabled;
			this.IsFoliationEnabled = false;
			try
			{
				XmlBoundElement xmlBoundElement = node as XmlBoundElement;
				if (xmlBoundElement != null)
				{
					DataRow row = xmlBoundElement.Row;
				}
				TreeIterator treeIterator = new TreeIterator(node);
				bool flag = treeIterator.NextRowElement();
				while (flag)
				{
					xmlBoundElement = (treeIterator.CurrentNode as XmlBoundElement);
					flag = treeIterator.NextRowElement();
				}
			}
			finally
			{
				this.IsFoliationEnabled = isFoliationEnabled;
			}
		}

		/// <summary>Gets the <see cref="T:System.Xml.XmlElement" /> with the specified ID. This method is not supported by the <see cref="T:System.Xml.XmlDataDocument" /> class. Calling this method throws an exception.</summary>
		/// <param name="elemId">The attribute ID to match.</param>
		/// <returns>An <see cref="T:System.Xml.XmlElement" /> with the specified ID.</returns>
		/// <exception cref="T:System.NotSupportedException">Calling this method.</exception>
		// Token: 0x0600061E RID: 1566 RVA: 0x00017BDC File Offset: 0x00015DDC
		public override XmlElement GetElementById(string elemId)
		{
			throw new NotSupportedException("GetElementById() is not supported on DataDocument.");
		}

		/// <summary>Returns an <see cref="T:System.Xml.XmlNodeList" /> containing a list of all descendant elements that match the specified <see cref="P:System.Xml.XmlDocument.Name" />.</summary>
		/// <param name="name">The qualified name to match. It is matched against the <see cref="P:System.Xml.XmlDocument.Name" /> property of the matching node. The special value "*" matches all tags.</param>
		/// <returns>An <see cref="T:System.Xml.XmlNodeList" /> containing a list of all matching nodes.</returns>
		// Token: 0x0600061F RID: 1567 RVA: 0x00017BE8 File Offset: 0x00015DE8
		public override XmlNodeList GetElementsByTagName(string name)
		{
			XmlNodeList elementsByTagName = base.GetElementsByTagName(name);
			int count = elementsByTagName.Count;
			return elementsByTagName;
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00017BF8 File Offset: 0x00015DF8
		private DataTable[] OrderTables(DataSet ds)
		{
			DataTable[] array = null;
			if (ds == null || ds.Tables.Count == 0)
			{
				array = Array.Empty<DataTable>();
			}
			else if (this.TablesAreOrdered(ds))
			{
				array = new DataTable[ds.Tables.Count];
				ds.Tables.CopyTo(array, 0);
			}
			if (array == null)
			{
				array = new DataTable[ds.Tables.Count];
				List<DataTable> list = new List<DataTable>();
				foreach (object obj in ds.Tables)
				{
					DataTable dataTable = (DataTable)obj;
					if (dataTable.ParentRelations.Count == 0)
					{
						list.Add(dataTable);
					}
				}
				if (list.Count > 0)
				{
					foreach (object obj2 in ds.Tables)
					{
						DataTable dataTable2 = (DataTable)obj2;
						if (this.IsSelfRelatedDataTable(dataTable2))
						{
							list.Add(dataTable2);
						}
					}
					for (int i = 0; i < list.Count; i++)
					{
						foreach (object obj3 in list[i].ChildRelations)
						{
							DataTable childTable = ((DataRelation)obj3).ChildTable;
							if (!list.Contains(childTable))
							{
								list.Add(childTable);
							}
						}
					}
					list.CopyTo(array);
				}
				else
				{
					ds.Tables.CopyTo(array, 0);
				}
			}
			return array;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00017DB4 File Offset: 0x00015FB4
		private bool IsSelfRelatedDataTable(DataTable rootTable)
		{
			List<DataTable> list = new List<DataTable>();
			bool flag = false;
			foreach (object obj in rootTable.ChildRelations)
			{
				DataTable childTable = ((DataRelation)obj).ChildTable;
				if (childTable == rootTable)
				{
					flag = true;
					break;
				}
				if (!list.Contains(childTable))
				{
					list.Add(childTable);
				}
			}
			if (!flag)
			{
				for (int i = 0; i < list.Count; i++)
				{
					foreach (object obj2 in list[i].ChildRelations)
					{
						DataTable childTable2 = ((DataRelation)obj2).ChildTable;
						if (childTable2 == rootTable)
						{
							flag = true;
							break;
						}
						if (!list.Contains(childTable2))
						{
							list.Add(childTable2);
						}
					}
					if (flag)
					{
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00017EB8 File Offset: 0x000160B8
		private bool TablesAreOrdered(DataSet ds)
		{
			using (IEnumerator enumerator = ds.Tables.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((DataTable)enumerator.Current).Namespace != ds.Namespace)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x04000655 RID: 1621
		private DataSet _dataSet;

		// Token: 0x04000656 RID: 1622
		private DataSetMapper _mapper;

		// Token: 0x04000657 RID: 1623
		internal Hashtable _pointers;

		// Token: 0x04000658 RID: 1624
		private int _countAddPointer;

		// Token: 0x04000659 RID: 1625
		private ArrayList _columnChangeList;

		// Token: 0x0400065A RID: 1626
		private DataRowState _rollbackState;

		// Token: 0x0400065B RID: 1627
		private bool _fBoundToDataSet;

		// Token: 0x0400065C RID: 1628
		private bool _fBoundToDocument;

		// Token: 0x0400065D RID: 1629
		private bool _fDataRowCreatedSpecial;

		// Token: 0x0400065E RID: 1630
		private bool _ignoreXmlEvents;

		// Token: 0x0400065F RID: 1631
		private bool _ignoreDataSetEvents;

		// Token: 0x04000660 RID: 1632
		private bool _isFoliationEnabled;

		// Token: 0x04000661 RID: 1633
		private bool _optimizeStorage;

		// Token: 0x04000662 RID: 1634
		private ElementState _autoFoliationState;

		// Token: 0x04000663 RID: 1635
		private bool _fAssociateDataRow;

		// Token: 0x04000664 RID: 1636
		private object _foliationLock;

		// Token: 0x04000665 RID: 1637
		internal const string XSI_NIL = "xsi:nil";

		// Token: 0x04000666 RID: 1638
		internal const string XSI = "xsi";

		// Token: 0x04000667 RID: 1639
		private bool _bForceExpandEntity;

		// Token: 0x04000668 RID: 1640
		internal XmlAttribute _attrXml;

		// Token: 0x04000669 RID: 1641
		internal bool _bLoadFromDataSet;

		// Token: 0x0400066A RID: 1642
		internal bool _bHasXSINIL;
	}
}
