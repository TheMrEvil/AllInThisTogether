using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.XPath;
using System.Xml.Xsl.Runtime;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x02000496 RID: 1174
	internal class XmlILStorageMethods
	{
		// Token: 0x06002DD8 RID: 11736 RVA: 0x0010C07C File Offset: 0x0010A27C
		public XmlILStorageMethods(Type storageType)
		{
			if (storageType == typeof(int) || storageType == typeof(long) || storageType == typeof(decimal) || storageType == typeof(double))
			{
				Type type = Type.GetType("System.Xml.Xsl.Runtime." + storageType.Name + "Aggregator");
				this.AggAvg = XmlILMethods.GetMethod(type, "Average");
				this.AggAvgResult = XmlILMethods.GetMethod(type, "get_AverageResult");
				this.AggCreate = XmlILMethods.GetMethod(type, "Create");
				this.AggIsEmpty = XmlILMethods.GetMethod(type, "get_IsEmpty");
				this.AggMax = XmlILMethods.GetMethod(type, "Maximum");
				this.AggMaxResult = XmlILMethods.GetMethod(type, "get_MaximumResult");
				this.AggMin = XmlILMethods.GetMethod(type, "Minimum");
				this.AggMinResult = XmlILMethods.GetMethod(type, "get_MinimumResult");
				this.AggSum = XmlILMethods.GetMethod(type, "Sum");
				this.AggSumResult = XmlILMethods.GetMethod(type, "get_SumResult");
			}
			if (storageType == typeof(XPathNavigator))
			{
				this.SeqType = typeof(XmlQueryNodeSequence);
				this.SeqAdd = XmlILMethods.GetMethod(this.SeqType, "AddClone");
			}
			else if (storageType == typeof(XPathItem))
			{
				this.SeqType = typeof(XmlQueryItemSequence);
				this.SeqAdd = XmlILMethods.GetMethod(this.SeqType, "AddClone");
			}
			else
			{
				this.SeqType = typeof(XmlQuerySequence<>).MakeGenericType(new Type[]
				{
					storageType
				});
				this.SeqAdd = XmlILMethods.GetMethod(this.SeqType, "Add");
			}
			this.SeqEmpty = this.SeqType.GetField("Empty");
			this.SeqReuse = XmlILMethods.GetMethod(this.SeqType, "CreateOrReuse", new Type[]
			{
				this.SeqType
			});
			this.SeqReuseSgl = XmlILMethods.GetMethod(this.SeqType, "CreateOrReuse", new Type[]
			{
				this.SeqType,
				storageType
			});
			this.SeqSortByKeys = XmlILMethods.GetMethod(this.SeqType, "SortByKeys");
			this.IListType = typeof(IList<>).MakeGenericType(new Type[]
			{
				storageType
			});
			this.IListItem = XmlILMethods.GetMethod(this.IListType, "get_Item");
			this.IListCount = XmlILMethods.GetMethod(typeof(ICollection<>).MakeGenericType(new Type[]
			{
				storageType
			}), "get_Count");
			if (storageType == typeof(string))
			{
				this.ValueAs = XmlILMethods.GetMethod(typeof(XPathItem), "get_Value");
			}
			else if (storageType == typeof(int))
			{
				this.ValueAs = XmlILMethods.GetMethod(typeof(XPathItem), "get_ValueAsInt");
			}
			else if (storageType == typeof(long))
			{
				this.ValueAs = XmlILMethods.GetMethod(typeof(XPathItem), "get_ValueAsLong");
			}
			else if (storageType == typeof(DateTime))
			{
				this.ValueAs = XmlILMethods.GetMethod(typeof(XPathItem), "get_ValueAsDateTime");
			}
			else if (storageType == typeof(double))
			{
				this.ValueAs = XmlILMethods.GetMethod(typeof(XPathItem), "get_ValueAsDouble");
			}
			else if (storageType == typeof(bool))
			{
				this.ValueAs = XmlILMethods.GetMethod(typeof(XPathItem), "get_ValueAsBoolean");
			}
			if (storageType == typeof(byte[]))
			{
				this.ToAtomicValue = XmlILMethods.GetMethod(typeof(XmlILStorageConverter), "BytesToAtomicValue");
				return;
			}
			if (storageType != typeof(XPathItem) && storageType != typeof(XPathNavigator))
			{
				this.ToAtomicValue = XmlILMethods.GetMethod(typeof(XmlILStorageConverter), storageType.Name + "ToAtomicValue");
			}
		}

		// Token: 0x04002390 RID: 9104
		public MethodInfo AggAvg;

		// Token: 0x04002391 RID: 9105
		public MethodInfo AggAvgResult;

		// Token: 0x04002392 RID: 9106
		public MethodInfo AggCreate;

		// Token: 0x04002393 RID: 9107
		public MethodInfo AggIsEmpty;

		// Token: 0x04002394 RID: 9108
		public MethodInfo AggMax;

		// Token: 0x04002395 RID: 9109
		public MethodInfo AggMaxResult;

		// Token: 0x04002396 RID: 9110
		public MethodInfo AggMin;

		// Token: 0x04002397 RID: 9111
		public MethodInfo AggMinResult;

		// Token: 0x04002398 RID: 9112
		public MethodInfo AggSum;

		// Token: 0x04002399 RID: 9113
		public MethodInfo AggSumResult;

		// Token: 0x0400239A RID: 9114
		public Type SeqType;

		// Token: 0x0400239B RID: 9115
		public FieldInfo SeqEmpty;

		// Token: 0x0400239C RID: 9116
		public MethodInfo SeqReuse;

		// Token: 0x0400239D RID: 9117
		public MethodInfo SeqReuseSgl;

		// Token: 0x0400239E RID: 9118
		public MethodInfo SeqAdd;

		// Token: 0x0400239F RID: 9119
		public MethodInfo SeqSortByKeys;

		// Token: 0x040023A0 RID: 9120
		public Type IListType;

		// Token: 0x040023A1 RID: 9121
		public MethodInfo IListCount;

		// Token: 0x040023A2 RID: 9122
		public MethodInfo IListItem;

		// Token: 0x040023A3 RID: 9123
		public MethodInfo ValueAs;

		// Token: 0x040023A4 RID: 9124
		public MethodInfo ToAtomicValue;
	}
}
