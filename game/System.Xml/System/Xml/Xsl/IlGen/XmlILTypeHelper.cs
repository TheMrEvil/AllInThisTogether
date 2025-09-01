using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace System.Xml.Xsl.IlGen
{
	// Token: 0x020004B4 RID: 1204
	internal class XmlILTypeHelper
	{
		// Token: 0x06002F5C RID: 12124 RVA: 0x0000216B File Offset: 0x0000036B
		private XmlILTypeHelper()
		{
		}

		// Token: 0x06002F5D RID: 12125 RVA: 0x00119564 File Offset: 0x00117764
		public static Type GetStorageType(XmlQueryType qyTyp)
		{
			Type type;
			if (qyTyp.IsSingleton)
			{
				type = XmlILTypeHelper.TypeCodeToStorage[(int)qyTyp.TypeCode];
				if (!qyTyp.IsStrict && type != typeof(XPathNavigator))
				{
					return typeof(XPathItem);
				}
			}
			else
			{
				type = XmlILTypeHelper.TypeCodeToCachedStorage[(int)qyTyp.TypeCode];
				if (!qyTyp.IsStrict && type != typeof(IList<XPathNavigator>))
				{
					return typeof(IList<XPathItem>);
				}
			}
			return type;
		}

		// Token: 0x06002F5E RID: 12126 RVA: 0x001195E0 File Offset: 0x001177E0
		// Note: this type is marked as 'beforefieldinit'.
		static XmlILTypeHelper()
		{
		}

		// Token: 0x040025B2 RID: 9650
		private static readonly Type[] TypeCodeToStorage = new Type[]
		{
			typeof(XPathItem),
			typeof(XPathItem),
			typeof(XPathNavigator),
			typeof(XPathNavigator),
			typeof(XPathNavigator),
			typeof(XPathNavigator),
			typeof(XPathNavigator),
			typeof(XPathNavigator),
			typeof(XPathNavigator),
			typeof(XPathNavigator),
			typeof(XPathItem),
			typeof(string),
			typeof(string),
			typeof(bool),
			typeof(decimal),
			typeof(float),
			typeof(double),
			typeof(string),
			typeof(DateTime),
			typeof(DateTime),
			typeof(DateTime),
			typeof(DateTime),
			typeof(DateTime),
			typeof(DateTime),
			typeof(DateTime),
			typeof(DateTime),
			typeof(byte[]),
			typeof(byte[]),
			typeof(string),
			typeof(XmlQualifiedName),
			typeof(XmlQualifiedName),
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(string),
			typeof(long),
			typeof(decimal),
			typeof(decimal),
			typeof(long),
			typeof(int),
			typeof(int),
			typeof(int),
			typeof(decimal),
			typeof(decimal),
			typeof(long),
			typeof(int),
			typeof(int),
			typeof(decimal),
			typeof(TimeSpan),
			typeof(TimeSpan)
		};

		// Token: 0x040025B3 RID: 9651
		private static readonly Type[] TypeCodeToCachedStorage = new Type[]
		{
			typeof(IList<XPathItem>),
			typeof(IList<XPathItem>),
			typeof(IList<XPathNavigator>),
			typeof(IList<XPathNavigator>),
			typeof(IList<XPathNavigator>),
			typeof(IList<XPathNavigator>),
			typeof(IList<XPathNavigator>),
			typeof(IList<XPathNavigator>),
			typeof(IList<XPathNavigator>),
			typeof(IList<XPathNavigator>),
			typeof(IList<XPathItem>),
			typeof(IList<string>),
			typeof(IList<string>),
			typeof(IList<bool>),
			typeof(IList<decimal>),
			typeof(IList<float>),
			typeof(IList<double>),
			typeof(IList<string>),
			typeof(IList<DateTime>),
			typeof(IList<DateTime>),
			typeof(IList<DateTime>),
			typeof(IList<DateTime>),
			typeof(IList<DateTime>),
			typeof(IList<DateTime>),
			typeof(IList<DateTime>),
			typeof(IList<DateTime>),
			typeof(IList<byte[]>),
			typeof(IList<byte[]>),
			typeof(IList<string>),
			typeof(IList<XmlQualifiedName>),
			typeof(IList<XmlQualifiedName>),
			typeof(IList<string>),
			typeof(IList<string>),
			typeof(IList<string>),
			typeof(IList<string>),
			typeof(IList<string>),
			typeof(IList<string>),
			typeof(IList<string>),
			typeof(IList<string>),
			typeof(IList<string>),
			typeof(IList<long>),
			typeof(IList<decimal>),
			typeof(IList<decimal>),
			typeof(IList<long>),
			typeof(IList<int>),
			typeof(IList<int>),
			typeof(IList<int>),
			typeof(IList<decimal>),
			typeof(IList<decimal>),
			typeof(IList<long>),
			typeof(IList<int>),
			typeof(IList<int>),
			typeof(IList<decimal>),
			typeof(IList<TimeSpan>),
			typeof(IList<TimeSpan>)
		};
	}
}
