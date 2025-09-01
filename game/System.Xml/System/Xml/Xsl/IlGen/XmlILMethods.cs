using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl.Runtime;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x02000498 RID: 1176
	internal static class XmlILMethods
	{
		// Token: 0x06002DDC RID: 11740 RVA: 0x0010C618 File Offset: 0x0010A818
		static XmlILMethods()
		{
			XmlILMethods.StorageMethods[typeof(string)] = new XmlILStorageMethods(typeof(string));
			XmlILMethods.StorageMethods[typeof(bool)] = new XmlILStorageMethods(typeof(bool));
			XmlILMethods.StorageMethods[typeof(int)] = new XmlILStorageMethods(typeof(int));
			XmlILMethods.StorageMethods[typeof(long)] = new XmlILStorageMethods(typeof(long));
			XmlILMethods.StorageMethods[typeof(decimal)] = new XmlILStorageMethods(typeof(decimal));
			XmlILMethods.StorageMethods[typeof(double)] = new XmlILStorageMethods(typeof(double));
			XmlILMethods.StorageMethods[typeof(float)] = new XmlILStorageMethods(typeof(float));
			XmlILMethods.StorageMethods[typeof(DateTime)] = new XmlILStorageMethods(typeof(DateTime));
			XmlILMethods.StorageMethods[typeof(byte[])] = new XmlILStorageMethods(typeof(byte[]));
			XmlILMethods.StorageMethods[typeof(XmlQualifiedName)] = new XmlILStorageMethods(typeof(XmlQualifiedName));
			XmlILMethods.StorageMethods[typeof(TimeSpan)] = new XmlILStorageMethods(typeof(TimeSpan));
			XmlILMethods.StorageMethods[typeof(XPathItem)] = new XmlILStorageMethods(typeof(XPathItem));
			XmlILMethods.StorageMethods[typeof(XPathNavigator)] = new XmlILStorageMethods(typeof(XPathNavigator));
		}

		// Token: 0x06002DDD RID: 11741 RVA: 0x0010B4BC File Offset: 0x001096BC
		public static MethodInfo GetMethod(Type className, string methName)
		{
			return className.GetMethod(methName);
		}

		// Token: 0x06002DDE RID: 11742 RVA: 0x0010B4C5 File Offset: 0x001096C5
		public static MethodInfo GetMethod(Type className, string methName, params Type[] args)
		{
			return className.GetMethod(methName, args);
		}

		// Token: 0x040023AD RID: 9133
		public static readonly MethodInfo AncCreate = XmlILMethods.GetMethod(typeof(AncestorIterator), "Create");

		// Token: 0x040023AE RID: 9134
		public static readonly MethodInfo AncNext = XmlILMethods.GetMethod(typeof(AncestorIterator), "MoveNext");

		// Token: 0x040023AF RID: 9135
		public static readonly MethodInfo AncDOCreate = XmlILMethods.GetMethod(typeof(AncestorDocOrderIterator), "Create");

		// Token: 0x040023B0 RID: 9136
		public static readonly MethodInfo AncDONext = XmlILMethods.GetMethod(typeof(AncestorDocOrderIterator), "MoveNext");

		// Token: 0x040023B1 RID: 9137
		public static readonly MethodInfo AttrContentCreate = XmlILMethods.GetMethod(typeof(AttributeContentIterator), "Create");

		// Token: 0x040023B2 RID: 9138
		public static readonly MethodInfo AttrContentNext = XmlILMethods.GetMethod(typeof(AttributeContentIterator), "MoveNext");

		// Token: 0x040023B3 RID: 9139
		public static readonly MethodInfo AttrCreate = XmlILMethods.GetMethod(typeof(AttributeIterator), "Create");

		// Token: 0x040023B4 RID: 9140
		public static readonly MethodInfo AttrNext = XmlILMethods.GetMethod(typeof(AttributeIterator), "MoveNext");

		// Token: 0x040023B5 RID: 9141
		public static readonly MethodInfo ContentCreate = XmlILMethods.GetMethod(typeof(ContentIterator), "Create");

		// Token: 0x040023B6 RID: 9142
		public static readonly MethodInfo ContentNext = XmlILMethods.GetMethod(typeof(ContentIterator), "MoveNext");

		// Token: 0x040023B7 RID: 9143
		public static readonly MethodInfo ContentMergeCreate = XmlILMethods.GetMethod(typeof(ContentMergeIterator), "Create");

		// Token: 0x040023B8 RID: 9144
		public static readonly MethodInfo ContentMergeNext = XmlILMethods.GetMethod(typeof(ContentMergeIterator), "MoveNext");

		// Token: 0x040023B9 RID: 9145
		public static readonly MethodInfo DescCreate = XmlILMethods.GetMethod(typeof(DescendantIterator), "Create");

		// Token: 0x040023BA RID: 9146
		public static readonly MethodInfo DescNext = XmlILMethods.GetMethod(typeof(DescendantIterator), "MoveNext");

		// Token: 0x040023BB RID: 9147
		public static readonly MethodInfo DescMergeCreate = XmlILMethods.GetMethod(typeof(DescendantMergeIterator), "Create");

		// Token: 0x040023BC RID: 9148
		public static readonly MethodInfo DescMergeNext = XmlILMethods.GetMethod(typeof(DescendantMergeIterator), "MoveNext");

		// Token: 0x040023BD RID: 9149
		public static readonly MethodInfo DiffCreate = XmlILMethods.GetMethod(typeof(DifferenceIterator), "Create");

		// Token: 0x040023BE RID: 9150
		public static readonly MethodInfo DiffNext = XmlILMethods.GetMethod(typeof(DifferenceIterator), "MoveNext");

		// Token: 0x040023BF RID: 9151
		public static readonly MethodInfo DodMergeCreate = XmlILMethods.GetMethod(typeof(DodSequenceMerge), "Create");

		// Token: 0x040023C0 RID: 9152
		public static readonly MethodInfo DodMergeAdd = XmlILMethods.GetMethod(typeof(DodSequenceMerge), "AddSequence");

		// Token: 0x040023C1 RID: 9153
		public static readonly MethodInfo DodMergeSeq = XmlILMethods.GetMethod(typeof(DodSequenceMerge), "MergeSequences");

		// Token: 0x040023C2 RID: 9154
		public static readonly MethodInfo ElemContentCreate = XmlILMethods.GetMethod(typeof(ElementContentIterator), "Create");

		// Token: 0x040023C3 RID: 9155
		public static readonly MethodInfo ElemContentNext = XmlILMethods.GetMethod(typeof(ElementContentIterator), "MoveNext");

		// Token: 0x040023C4 RID: 9156
		public static readonly MethodInfo FollSibCreate = XmlILMethods.GetMethod(typeof(FollowingSiblingIterator), "Create");

		// Token: 0x040023C5 RID: 9157
		public static readonly MethodInfo FollSibNext = XmlILMethods.GetMethod(typeof(FollowingSiblingIterator), "MoveNext");

		// Token: 0x040023C6 RID: 9158
		public static readonly MethodInfo FollSibMergeCreate = XmlILMethods.GetMethod(typeof(FollowingSiblingMergeIterator), "Create");

		// Token: 0x040023C7 RID: 9159
		public static readonly MethodInfo FollSibMergeNext = XmlILMethods.GetMethod(typeof(FollowingSiblingMergeIterator), "MoveNext");

		// Token: 0x040023C8 RID: 9160
		public static readonly MethodInfo IdCreate = XmlILMethods.GetMethod(typeof(IdIterator), "Create");

		// Token: 0x040023C9 RID: 9161
		public static readonly MethodInfo IdNext = XmlILMethods.GetMethod(typeof(IdIterator), "MoveNext");

		// Token: 0x040023CA RID: 9162
		public static readonly MethodInfo InterCreate = XmlILMethods.GetMethod(typeof(IntersectIterator), "Create");

		// Token: 0x040023CB RID: 9163
		public static readonly MethodInfo InterNext = XmlILMethods.GetMethod(typeof(IntersectIterator), "MoveNext");

		// Token: 0x040023CC RID: 9164
		public static readonly MethodInfo KindContentCreate = XmlILMethods.GetMethod(typeof(NodeKindContentIterator), "Create");

		// Token: 0x040023CD RID: 9165
		public static readonly MethodInfo KindContentNext = XmlILMethods.GetMethod(typeof(NodeKindContentIterator), "MoveNext");

		// Token: 0x040023CE RID: 9166
		public static readonly MethodInfo NmspCreate = XmlILMethods.GetMethod(typeof(NamespaceIterator), "Create");

		// Token: 0x040023CF RID: 9167
		public static readonly MethodInfo NmspNext = XmlILMethods.GetMethod(typeof(NamespaceIterator), "MoveNext");

		// Token: 0x040023D0 RID: 9168
		public static readonly MethodInfo NodeRangeCreate = XmlILMethods.GetMethod(typeof(NodeRangeIterator), "Create");

		// Token: 0x040023D1 RID: 9169
		public static readonly MethodInfo NodeRangeNext = XmlILMethods.GetMethod(typeof(NodeRangeIterator), "MoveNext");

		// Token: 0x040023D2 RID: 9170
		public static readonly MethodInfo ParentCreate = XmlILMethods.GetMethod(typeof(ParentIterator), "Create");

		// Token: 0x040023D3 RID: 9171
		public static readonly MethodInfo ParentNext = XmlILMethods.GetMethod(typeof(ParentIterator), "MoveNext");

		// Token: 0x040023D4 RID: 9172
		public static readonly MethodInfo PrecCreate = XmlILMethods.GetMethod(typeof(PrecedingIterator), "Create");

		// Token: 0x040023D5 RID: 9173
		public static readonly MethodInfo PrecNext = XmlILMethods.GetMethod(typeof(PrecedingIterator), "MoveNext");

		// Token: 0x040023D6 RID: 9174
		public static readonly MethodInfo PreSibCreate = XmlILMethods.GetMethod(typeof(PrecedingSiblingIterator), "Create");

		// Token: 0x040023D7 RID: 9175
		public static readonly MethodInfo PreSibNext = XmlILMethods.GetMethod(typeof(PrecedingSiblingIterator), "MoveNext");

		// Token: 0x040023D8 RID: 9176
		public static readonly MethodInfo PreSibDOCreate = XmlILMethods.GetMethod(typeof(PrecedingSiblingDocOrderIterator), "Create");

		// Token: 0x040023D9 RID: 9177
		public static readonly MethodInfo PreSibDONext = XmlILMethods.GetMethod(typeof(PrecedingSiblingDocOrderIterator), "MoveNext");

		// Token: 0x040023DA RID: 9178
		public static readonly MethodInfo SortKeyCreate = XmlILMethods.GetMethod(typeof(XmlSortKeyAccumulator), "Create");

		// Token: 0x040023DB RID: 9179
		public static readonly MethodInfo SortKeyDateTime = XmlILMethods.GetMethod(typeof(XmlSortKeyAccumulator), "AddDateTimeSortKey");

		// Token: 0x040023DC RID: 9180
		public static readonly MethodInfo SortKeyDecimal = XmlILMethods.GetMethod(typeof(XmlSortKeyAccumulator), "AddDecimalSortKey");

		// Token: 0x040023DD RID: 9181
		public static readonly MethodInfo SortKeyDouble = XmlILMethods.GetMethod(typeof(XmlSortKeyAccumulator), "AddDoubleSortKey");

		// Token: 0x040023DE RID: 9182
		public static readonly MethodInfo SortKeyEmpty = XmlILMethods.GetMethod(typeof(XmlSortKeyAccumulator), "AddEmptySortKey");

		// Token: 0x040023DF RID: 9183
		public static readonly MethodInfo SortKeyFinish = XmlILMethods.GetMethod(typeof(XmlSortKeyAccumulator), "FinishSortKeys");

		// Token: 0x040023E0 RID: 9184
		public static readonly MethodInfo SortKeyInt = XmlILMethods.GetMethod(typeof(XmlSortKeyAccumulator), "AddIntSortKey");

		// Token: 0x040023E1 RID: 9185
		public static readonly MethodInfo SortKeyInteger = XmlILMethods.GetMethod(typeof(XmlSortKeyAccumulator), "AddIntegerSortKey");

		// Token: 0x040023E2 RID: 9186
		public static readonly MethodInfo SortKeyKeys = XmlILMethods.GetMethod(typeof(XmlSortKeyAccumulator), "get_Keys");

		// Token: 0x040023E3 RID: 9187
		public static readonly MethodInfo SortKeyString = XmlILMethods.GetMethod(typeof(XmlSortKeyAccumulator), "AddStringSortKey");

		// Token: 0x040023E4 RID: 9188
		public static readonly MethodInfo UnionCreate = XmlILMethods.GetMethod(typeof(UnionIterator), "Create");

		// Token: 0x040023E5 RID: 9189
		public static readonly MethodInfo UnionNext = XmlILMethods.GetMethod(typeof(UnionIterator), "MoveNext");

		// Token: 0x040023E6 RID: 9190
		public static readonly MethodInfo XPFollCreate = XmlILMethods.GetMethod(typeof(XPathFollowingIterator), "Create");

		// Token: 0x040023E7 RID: 9191
		public static readonly MethodInfo XPFollNext = XmlILMethods.GetMethod(typeof(XPathFollowingIterator), "MoveNext");

		// Token: 0x040023E8 RID: 9192
		public static readonly MethodInfo XPFollMergeCreate = XmlILMethods.GetMethod(typeof(XPathFollowingMergeIterator), "Create");

		// Token: 0x040023E9 RID: 9193
		public static readonly MethodInfo XPFollMergeNext = XmlILMethods.GetMethod(typeof(XPathFollowingMergeIterator), "MoveNext");

		// Token: 0x040023EA RID: 9194
		public static readonly MethodInfo XPPrecCreate = XmlILMethods.GetMethod(typeof(XPathPrecedingIterator), "Create");

		// Token: 0x040023EB RID: 9195
		public static readonly MethodInfo XPPrecNext = XmlILMethods.GetMethod(typeof(XPathPrecedingIterator), "MoveNext");

		// Token: 0x040023EC RID: 9196
		public static readonly MethodInfo XPPrecDOCreate = XmlILMethods.GetMethod(typeof(XPathPrecedingDocOrderIterator), "Create");

		// Token: 0x040023ED RID: 9197
		public static readonly MethodInfo XPPrecDONext = XmlILMethods.GetMethod(typeof(XPathPrecedingDocOrderIterator), "MoveNext");

		// Token: 0x040023EE RID: 9198
		public static readonly MethodInfo XPPrecMergeCreate = XmlILMethods.GetMethod(typeof(XPathPrecedingMergeIterator), "Create");

		// Token: 0x040023EF RID: 9199
		public static readonly MethodInfo XPPrecMergeNext = XmlILMethods.GetMethod(typeof(XPathPrecedingMergeIterator), "MoveNext");

		// Token: 0x040023F0 RID: 9200
		public static readonly MethodInfo AddNewIndex = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "AddNewIndex");

		// Token: 0x040023F1 RID: 9201
		public static readonly MethodInfo ChangeTypeXsltArg = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "ChangeTypeXsltArgument", new Type[]
		{
			typeof(int),
			typeof(object),
			typeof(Type)
		});

		// Token: 0x040023F2 RID: 9202
		public static readonly MethodInfo ChangeTypeXsltResult = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "ChangeTypeXsltResult");

		// Token: 0x040023F3 RID: 9203
		public static readonly MethodInfo CompPos = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "ComparePosition");

		// Token: 0x040023F4 RID: 9204
		public static readonly MethodInfo Context = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "get_ExternalContext");

		// Token: 0x040023F5 RID: 9205
		public static readonly MethodInfo CreateCollation = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "CreateCollation");

		// Token: 0x040023F6 RID: 9206
		public static readonly MethodInfo DocOrder = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "DocOrderDistinct");

		// Token: 0x040023F7 RID: 9207
		public static readonly MethodInfo EndRtfConstr = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "EndRtfConstruction");

		// Token: 0x040023F8 RID: 9208
		public static readonly MethodInfo EndSeqConstr = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "EndSequenceConstruction");

		// Token: 0x040023F9 RID: 9209
		public static readonly MethodInfo FindIndex = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "FindIndex");

		// Token: 0x040023FA RID: 9210
		public static readonly MethodInfo GenId = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "GenerateId");

		// Token: 0x040023FB RID: 9211
		public static readonly MethodInfo GetAtomizedName = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "GetAtomizedName");

		// Token: 0x040023FC RID: 9212
		public static readonly MethodInfo GetCollation = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "GetCollation");

		// Token: 0x040023FD RID: 9213
		public static readonly MethodInfo GetEarly = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "GetEarlyBoundObject");

		// Token: 0x040023FE RID: 9214
		public static readonly MethodInfo GetNameFilter = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "GetNameFilter");

		// Token: 0x040023FF RID: 9215
		public static readonly MethodInfo GetOutput = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "get_Output");

		// Token: 0x04002400 RID: 9216
		public static readonly MethodInfo GetGlobalValue = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "GetGlobalValue");

		// Token: 0x04002401 RID: 9217
		public static readonly MethodInfo GetTypeFilter = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "GetTypeFilter");

		// Token: 0x04002402 RID: 9218
		public static readonly MethodInfo GlobalComputed = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "IsGlobalComputed");

		// Token: 0x04002403 RID: 9219
		public static readonly MethodInfo ItemMatchesCode = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "MatchesXmlType", new Type[]
		{
			typeof(XPathItem),
			typeof(XmlTypeCode)
		});

		// Token: 0x04002404 RID: 9220
		public static readonly MethodInfo ItemMatchesType = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "MatchesXmlType", new Type[]
		{
			typeof(XPathItem),
			typeof(int)
		});

		// Token: 0x04002405 RID: 9221
		public static readonly MethodInfo QNameEqualLit = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "IsQNameEqual", new Type[]
		{
			typeof(XPathNavigator),
			typeof(int),
			typeof(int)
		});

		// Token: 0x04002406 RID: 9222
		public static readonly MethodInfo QNameEqualNav = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "IsQNameEqual", new Type[]
		{
			typeof(XPathNavigator),
			typeof(XPathNavigator)
		});

		// Token: 0x04002407 RID: 9223
		public static readonly MethodInfo RtfConstr = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "TextRtfConstruction");

		// Token: 0x04002408 RID: 9224
		public static readonly MethodInfo SendMessage = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "SendMessage");

		// Token: 0x04002409 RID: 9225
		public static readonly MethodInfo SeqMatchesCode = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "MatchesXmlType", new Type[]
		{
			typeof(IList<XPathItem>),
			typeof(XmlTypeCode)
		});

		// Token: 0x0400240A RID: 9226
		public static readonly MethodInfo SeqMatchesType = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "MatchesXmlType", new Type[]
		{
			typeof(IList<XPathItem>),
			typeof(int)
		});

		// Token: 0x0400240B RID: 9227
		public static readonly MethodInfo SetGlobalValue = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "SetGlobalValue");

		// Token: 0x0400240C RID: 9228
		public static readonly MethodInfo StartRtfConstr = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "StartRtfConstruction");

		// Token: 0x0400240D RID: 9229
		public static readonly MethodInfo StartSeqConstr = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "StartSequenceConstruction");

		// Token: 0x0400240E RID: 9230
		public static readonly MethodInfo TagAndMappings = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "ParseTagName", new Type[]
		{
			typeof(string),
			typeof(int)
		});

		// Token: 0x0400240F RID: 9231
		public static readonly MethodInfo TagAndNamespace = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "ParseTagName", new Type[]
		{
			typeof(string),
			typeof(string)
		});

		// Token: 0x04002410 RID: 9232
		public static readonly MethodInfo ThrowException = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "ThrowException");

		// Token: 0x04002411 RID: 9233
		public static readonly MethodInfo XsltLib = XmlILMethods.GetMethod(typeof(XmlQueryRuntime), "get_XsltFunctions");

		// Token: 0x04002412 RID: 9234
		public static readonly MethodInfo GetDataSource = XmlILMethods.GetMethod(typeof(XmlQueryContext), "GetDataSource");

		// Token: 0x04002413 RID: 9235
		public static readonly MethodInfo GetDefaultDataSource = XmlILMethods.GetMethod(typeof(XmlQueryContext), "get_DefaultDataSource");

		// Token: 0x04002414 RID: 9236
		public static readonly MethodInfo GetParam = XmlILMethods.GetMethod(typeof(XmlQueryContext), "GetParameter");

		// Token: 0x04002415 RID: 9237
		public static readonly MethodInfo InvokeXsltLate = XmlILMethods.GetMethod(typeof(XmlQueryContext), "InvokeXsltLateBoundFunction");

		// Token: 0x04002416 RID: 9238
		public static readonly MethodInfo IndexAdd = XmlILMethods.GetMethod(typeof(XmlILIndex), "Add");

		// Token: 0x04002417 RID: 9239
		public static readonly MethodInfo IndexLookup = XmlILMethods.GetMethod(typeof(XmlILIndex), "Lookup");

		// Token: 0x04002418 RID: 9240
		public static readonly MethodInfo ItemIsNode = XmlILMethods.GetMethod(typeof(XPathItem), "get_IsNode");

		// Token: 0x04002419 RID: 9241
		public static readonly MethodInfo Value = XmlILMethods.GetMethod(typeof(XPathItem), "get_Value");

		// Token: 0x0400241A RID: 9242
		public static readonly MethodInfo ValueAsAny = XmlILMethods.GetMethod(typeof(XPathItem), "ValueAs", new Type[]
		{
			typeof(Type),
			typeof(IXmlNamespaceResolver)
		});

		// Token: 0x0400241B RID: 9243
		public static readonly MethodInfo NavClone = XmlILMethods.GetMethod(typeof(XPathNavigator), "Clone");

		// Token: 0x0400241C RID: 9244
		public static readonly MethodInfo NavLocalName = XmlILMethods.GetMethod(typeof(XPathNavigator), "get_LocalName");

		// Token: 0x0400241D RID: 9245
		public static readonly MethodInfo NavMoveAttr = XmlILMethods.GetMethod(typeof(XPathNavigator), "MoveToAttribute", new Type[]
		{
			typeof(string),
			typeof(string)
		});

		// Token: 0x0400241E RID: 9246
		public static readonly MethodInfo NavMoveId = XmlILMethods.GetMethod(typeof(XPathNavigator), "MoveToId");

		// Token: 0x0400241F RID: 9247
		public static readonly MethodInfo NavMoveParent = XmlILMethods.GetMethod(typeof(XPathNavigator), "MoveToParent");

		// Token: 0x04002420 RID: 9248
		public static readonly MethodInfo NavMoveRoot = XmlILMethods.GetMethod(typeof(XPathNavigator), "MoveToRoot");

		// Token: 0x04002421 RID: 9249
		public static readonly MethodInfo NavMoveTo = XmlILMethods.GetMethod(typeof(XPathNavigator), "MoveTo");

		// Token: 0x04002422 RID: 9250
		public static readonly MethodInfo NavNmsp = XmlILMethods.GetMethod(typeof(XPathNavigator), "get_NamespaceURI");

		// Token: 0x04002423 RID: 9251
		public static readonly MethodInfo NavPrefix = XmlILMethods.GetMethod(typeof(XPathNavigator), "get_Prefix");

		// Token: 0x04002424 RID: 9252
		public static readonly MethodInfo NavSamePos = XmlILMethods.GetMethod(typeof(XPathNavigator), "IsSamePosition");

		// Token: 0x04002425 RID: 9253
		public static readonly MethodInfo NavType = XmlILMethods.GetMethod(typeof(XPathNavigator), "get_NodeType");

		// Token: 0x04002426 RID: 9254
		public static readonly MethodInfo StartElemLitName = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStartElement", new Type[]
		{
			typeof(string),
			typeof(string),
			typeof(string)
		});

		// Token: 0x04002427 RID: 9255
		public static readonly MethodInfo StartElemLocName = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStartElementLocalName", new Type[]
		{
			typeof(string)
		});

		// Token: 0x04002428 RID: 9256
		public static readonly MethodInfo EndElemStackName = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteEndElement");

		// Token: 0x04002429 RID: 9257
		public static readonly MethodInfo StartAttrLitName = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStartAttribute", new Type[]
		{
			typeof(string),
			typeof(string),
			typeof(string)
		});

		// Token: 0x0400242A RID: 9258
		public static readonly MethodInfo StartAttrLocName = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStartAttributeLocalName", new Type[]
		{
			typeof(string)
		});

		// Token: 0x0400242B RID: 9259
		public static readonly MethodInfo EndAttr = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteEndAttribute");

		// Token: 0x0400242C RID: 9260
		public static readonly MethodInfo Text = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteString");

		// Token: 0x0400242D RID: 9261
		public static readonly MethodInfo NoEntText = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteRaw", new Type[]
		{
			typeof(string)
		});

		// Token: 0x0400242E RID: 9262
		public static readonly MethodInfo StartTree = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "StartTree");

		// Token: 0x0400242F RID: 9263
		public static readonly MethodInfo EndTree = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "EndTree");

		// Token: 0x04002430 RID: 9264
		public static readonly MethodInfo StartElemLitNameUn = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStartElementUnchecked", new Type[]
		{
			typeof(string),
			typeof(string),
			typeof(string)
		});

		// Token: 0x04002431 RID: 9265
		public static readonly MethodInfo StartElemLocNameUn = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStartElementUnchecked", new Type[]
		{
			typeof(string)
		});

		// Token: 0x04002432 RID: 9266
		public static readonly MethodInfo StartContentUn = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "StartElementContentUnchecked");

		// Token: 0x04002433 RID: 9267
		public static readonly MethodInfo EndElemLitNameUn = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteEndElementUnchecked", new Type[]
		{
			typeof(string),
			typeof(string),
			typeof(string)
		});

		// Token: 0x04002434 RID: 9268
		public static readonly MethodInfo EndElemLocNameUn = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteEndElementUnchecked", new Type[]
		{
			typeof(string)
		});

		// Token: 0x04002435 RID: 9269
		public static readonly MethodInfo StartAttrLitNameUn = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStartAttributeUnchecked", new Type[]
		{
			typeof(string),
			typeof(string),
			typeof(string)
		});

		// Token: 0x04002436 RID: 9270
		public static readonly MethodInfo StartAttrLocNameUn = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStartAttributeUnchecked", new Type[]
		{
			typeof(string)
		});

		// Token: 0x04002437 RID: 9271
		public static readonly MethodInfo EndAttrUn = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteEndAttributeUnchecked");

		// Token: 0x04002438 RID: 9272
		public static readonly MethodInfo NamespaceDeclUn = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteNamespaceDeclarationUnchecked");

		// Token: 0x04002439 RID: 9273
		public static readonly MethodInfo TextUn = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStringUnchecked");

		// Token: 0x0400243A RID: 9274
		public static readonly MethodInfo NoEntTextUn = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteRawUnchecked");

		// Token: 0x0400243B RID: 9275
		public static readonly MethodInfo StartRoot = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStartRoot");

		// Token: 0x0400243C RID: 9276
		public static readonly MethodInfo EndRoot = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteEndRoot");

		// Token: 0x0400243D RID: 9277
		public static readonly MethodInfo StartElemCopyName = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStartElementComputed", new Type[]
		{
			typeof(XPathNavigator)
		});

		// Token: 0x0400243E RID: 9278
		public static readonly MethodInfo StartElemMapName = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStartElementComputed", new Type[]
		{
			typeof(string),
			typeof(int)
		});

		// Token: 0x0400243F RID: 9279
		public static readonly MethodInfo StartElemNmspName = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStartElementComputed", new Type[]
		{
			typeof(string),
			typeof(string)
		});

		// Token: 0x04002440 RID: 9280
		public static readonly MethodInfo StartElemQName = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStartElementComputed", new Type[]
		{
			typeof(XmlQualifiedName)
		});

		// Token: 0x04002441 RID: 9281
		public static readonly MethodInfo StartAttrCopyName = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStartAttributeComputed", new Type[]
		{
			typeof(XPathNavigator)
		});

		// Token: 0x04002442 RID: 9282
		public static readonly MethodInfo StartAttrMapName = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStartAttributeComputed", new Type[]
		{
			typeof(string),
			typeof(int)
		});

		// Token: 0x04002443 RID: 9283
		public static readonly MethodInfo StartAttrNmspName = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStartAttributeComputed", new Type[]
		{
			typeof(string),
			typeof(string)
		});

		// Token: 0x04002444 RID: 9284
		public static readonly MethodInfo StartAttrQName = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStartAttributeComputed", new Type[]
		{
			typeof(XmlQualifiedName)
		});

		// Token: 0x04002445 RID: 9285
		public static readonly MethodInfo NamespaceDecl = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteNamespaceDeclaration");

		// Token: 0x04002446 RID: 9286
		public static readonly MethodInfo StartComment = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStartComment");

		// Token: 0x04002447 RID: 9287
		public static readonly MethodInfo CommentText = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteCommentString");

		// Token: 0x04002448 RID: 9288
		public static readonly MethodInfo EndComment = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteEndComment");

		// Token: 0x04002449 RID: 9289
		public static readonly MethodInfo StartPI = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteStartProcessingInstruction");

		// Token: 0x0400244A RID: 9290
		public static readonly MethodInfo PIText = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteProcessingInstructionString");

		// Token: 0x0400244B RID: 9291
		public static readonly MethodInfo EndPI = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteEndProcessingInstruction");

		// Token: 0x0400244C RID: 9292
		public static readonly MethodInfo WriteItem = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "WriteItem");

		// Token: 0x0400244D RID: 9293
		public static readonly MethodInfo CopyOf = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "XsltCopyOf");

		// Token: 0x0400244E RID: 9294
		public static readonly MethodInfo StartCopy = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "StartCopy");

		// Token: 0x0400244F RID: 9295
		public static readonly MethodInfo EndCopy = XmlILMethods.GetMethod(typeof(XmlQueryOutput), "EndCopy");

		// Token: 0x04002450 RID: 9296
		public static readonly MethodInfo DecAdd = XmlILMethods.GetMethod(typeof(decimal), "Add");

		// Token: 0x04002451 RID: 9297
		public static readonly MethodInfo DecCmp = XmlILMethods.GetMethod(typeof(decimal), "Compare", new Type[]
		{
			typeof(decimal),
			typeof(decimal)
		});

		// Token: 0x04002452 RID: 9298
		public static readonly MethodInfo DecEq = XmlILMethods.GetMethod(typeof(decimal), "Equals", new Type[]
		{
			typeof(decimal),
			typeof(decimal)
		});

		// Token: 0x04002453 RID: 9299
		public static readonly MethodInfo DecSub = XmlILMethods.GetMethod(typeof(decimal), "Subtract");

		// Token: 0x04002454 RID: 9300
		public static readonly MethodInfo DecMul = XmlILMethods.GetMethod(typeof(decimal), "Multiply");

		// Token: 0x04002455 RID: 9301
		public static readonly MethodInfo DecDiv = XmlILMethods.GetMethod(typeof(decimal), "Divide");

		// Token: 0x04002456 RID: 9302
		public static readonly MethodInfo DecRem = XmlILMethods.GetMethod(typeof(decimal), "Remainder");

		// Token: 0x04002457 RID: 9303
		public static readonly MethodInfo DecNeg = XmlILMethods.GetMethod(typeof(decimal), "Negate");

		// Token: 0x04002458 RID: 9304
		public static readonly MethodInfo QNameEq = XmlILMethods.GetMethod(typeof(XmlQualifiedName), "Equals");

		// Token: 0x04002459 RID: 9305
		public static readonly MethodInfo StrEq = XmlILMethods.GetMethod(typeof(string), "Equals", new Type[]
		{
			typeof(string),
			typeof(string)
		});

		// Token: 0x0400245A RID: 9306
		public static readonly MethodInfo StrCat2 = XmlILMethods.GetMethod(typeof(string), "Concat", new Type[]
		{
			typeof(string),
			typeof(string)
		});

		// Token: 0x0400245B RID: 9307
		public static readonly MethodInfo StrCat3 = XmlILMethods.GetMethod(typeof(string), "Concat", new Type[]
		{
			typeof(string),
			typeof(string),
			typeof(string)
		});

		// Token: 0x0400245C RID: 9308
		public static readonly MethodInfo StrCat4 = XmlILMethods.GetMethod(typeof(string), "Concat", new Type[]
		{
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string)
		});

		// Token: 0x0400245D RID: 9309
		public static readonly MethodInfo StrCmp = XmlILMethods.GetMethod(typeof(string), "CompareOrdinal", new Type[]
		{
			typeof(string),
			typeof(string)
		});

		// Token: 0x0400245E RID: 9310
		public static readonly MethodInfo StrLen = XmlILMethods.GetMethod(typeof(string), "get_Length");

		// Token: 0x0400245F RID: 9311
		public static readonly MethodInfo DblToDec = XmlILMethods.GetMethod(typeof(XsltConvert), "ToDecimal", new Type[]
		{
			typeof(double)
		});

		// Token: 0x04002460 RID: 9312
		public static readonly MethodInfo DblToInt = XmlILMethods.GetMethod(typeof(XsltConvert), "ToInt", new Type[]
		{
			typeof(double)
		});

		// Token: 0x04002461 RID: 9313
		public static readonly MethodInfo DblToLng = XmlILMethods.GetMethod(typeof(XsltConvert), "ToLong", new Type[]
		{
			typeof(double)
		});

		// Token: 0x04002462 RID: 9314
		public static readonly MethodInfo DblToStr = XmlILMethods.GetMethod(typeof(XsltConvert), "ToString", new Type[]
		{
			typeof(double)
		});

		// Token: 0x04002463 RID: 9315
		public static readonly MethodInfo DecToDbl = XmlILMethods.GetMethod(typeof(XsltConvert), "ToDouble", new Type[]
		{
			typeof(decimal)
		});

		// Token: 0x04002464 RID: 9316
		public static readonly MethodInfo DTToStr = XmlILMethods.GetMethod(typeof(XsltConvert), "ToString", new Type[]
		{
			typeof(DateTime)
		});

		// Token: 0x04002465 RID: 9317
		public static readonly MethodInfo IntToDbl = XmlILMethods.GetMethod(typeof(XsltConvert), "ToDouble", new Type[]
		{
			typeof(int)
		});

		// Token: 0x04002466 RID: 9318
		public static readonly MethodInfo LngToDbl = XmlILMethods.GetMethod(typeof(XsltConvert), "ToDouble", new Type[]
		{
			typeof(long)
		});

		// Token: 0x04002467 RID: 9319
		public static readonly MethodInfo StrToDbl = XmlILMethods.GetMethod(typeof(XsltConvert), "ToDouble", new Type[]
		{
			typeof(string)
		});

		// Token: 0x04002468 RID: 9320
		public static readonly MethodInfo StrToDT = XmlILMethods.GetMethod(typeof(XsltConvert), "ToDateTime", new Type[]
		{
			typeof(string)
		});

		// Token: 0x04002469 RID: 9321
		public static readonly MethodInfo ItemToBool = XmlILMethods.GetMethod(typeof(XsltConvert), "ToBoolean", new Type[]
		{
			typeof(XPathItem)
		});

		// Token: 0x0400246A RID: 9322
		public static readonly MethodInfo ItemToDbl = XmlILMethods.GetMethod(typeof(XsltConvert), "ToDouble", new Type[]
		{
			typeof(XPathItem)
		});

		// Token: 0x0400246B RID: 9323
		public static readonly MethodInfo ItemToStr = XmlILMethods.GetMethod(typeof(XsltConvert), "ToString", new Type[]
		{
			typeof(XPathItem)
		});

		// Token: 0x0400246C RID: 9324
		public static readonly MethodInfo ItemToNode = XmlILMethods.GetMethod(typeof(XsltConvert), "ToNode", new Type[]
		{
			typeof(XPathItem)
		});

		// Token: 0x0400246D RID: 9325
		public static readonly MethodInfo ItemToNodes = XmlILMethods.GetMethod(typeof(XsltConvert), "ToNodeSet", new Type[]
		{
			typeof(XPathItem)
		});

		// Token: 0x0400246E RID: 9326
		public static readonly MethodInfo ItemsToBool = XmlILMethods.GetMethod(typeof(XsltConvert), "ToBoolean", new Type[]
		{
			typeof(IList<XPathItem>)
		});

		// Token: 0x0400246F RID: 9327
		public static readonly MethodInfo ItemsToDbl = XmlILMethods.GetMethod(typeof(XsltConvert), "ToDouble", new Type[]
		{
			typeof(IList<XPathItem>)
		});

		// Token: 0x04002470 RID: 9328
		public static readonly MethodInfo ItemsToNode = XmlILMethods.GetMethod(typeof(XsltConvert), "ToNode", new Type[]
		{
			typeof(IList<XPathItem>)
		});

		// Token: 0x04002471 RID: 9329
		public static readonly MethodInfo ItemsToNodes = XmlILMethods.GetMethod(typeof(XsltConvert), "ToNodeSet", new Type[]
		{
			typeof(IList<XPathItem>)
		});

		// Token: 0x04002472 RID: 9330
		public static readonly MethodInfo ItemsToStr = XmlILMethods.GetMethod(typeof(XsltConvert), "ToString", new Type[]
		{
			typeof(IList<XPathItem>)
		});

		// Token: 0x04002473 RID: 9331
		public static readonly MethodInfo StrCatCat = XmlILMethods.GetMethod(typeof(StringConcat), "Concat");

		// Token: 0x04002474 RID: 9332
		public static readonly MethodInfo StrCatClear = XmlILMethods.GetMethod(typeof(StringConcat), "Clear");

		// Token: 0x04002475 RID: 9333
		public static readonly MethodInfo StrCatResult = XmlILMethods.GetMethod(typeof(StringConcat), "GetResult");

		// Token: 0x04002476 RID: 9334
		public static readonly MethodInfo StrCatDelim = XmlILMethods.GetMethod(typeof(StringConcat), "set_Delimiter");

		// Token: 0x04002477 RID: 9335
		public static readonly MethodInfo NavsToItems = XmlILMethods.GetMethod(typeof(XmlILStorageConverter), "NavigatorsToItems");

		// Token: 0x04002478 RID: 9336
		public static readonly MethodInfo ItemsToNavs = XmlILMethods.GetMethod(typeof(XmlILStorageConverter), "ItemsToNavigators");

		// Token: 0x04002479 RID: 9337
		public static readonly MethodInfo SetDod = XmlILMethods.GetMethod(typeof(XmlQueryNodeSequence), "set_IsDocOrderDistinct");

		// Token: 0x0400247A RID: 9338
		public static readonly MethodInfo GetTypeFromHandle = XmlILMethods.GetMethod(typeof(Type), "GetTypeFromHandle");

		// Token: 0x0400247B RID: 9339
		public static readonly MethodInfo InitializeArray = XmlILMethods.GetMethod(typeof(RuntimeHelpers), "InitializeArray");

		// Token: 0x0400247C RID: 9340
		public static readonly Dictionary<Type, XmlILStorageMethods> StorageMethods = new Dictionary<Type, XmlILStorageMethods>();
	}
}
