using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Diagnostics.Application;
using System.Security;
using System.Xml;

namespace System.Runtime.Serialization
{
	// Token: 0x0200013A RID: 314
	internal sealed class XmlFormatReaderGenerator
	{
		// Token: 0x06000FA6 RID: 4006 RVA: 0x0003E961 File Offset: 0x0003CB61
		[SecurityCritical]
		public XmlFormatReaderGenerator()
		{
			this.helper = new XmlFormatReaderGenerator.CriticalHelper();
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x0003E974 File Offset: 0x0003CB74
		[SecurityCritical]
		public XmlFormatClassReaderDelegate GenerateClassReader(ClassDataContract classContract)
		{
			XmlFormatClassReaderDelegate result;
			try
			{
				if (TD.DCGenReaderStartIsEnabled())
				{
					TD.DCGenReaderStart("Class", classContract.UnderlyingType.FullName);
				}
				result = this.helper.GenerateClassReader(classContract);
			}
			finally
			{
				if (TD.DCGenReaderStopIsEnabled())
				{
					TD.DCGenReaderStop();
				}
			}
			return result;
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x0003E9CC File Offset: 0x0003CBCC
		[SecurityCritical]
		public XmlFormatCollectionReaderDelegate GenerateCollectionReader(CollectionDataContract collectionContract)
		{
			XmlFormatCollectionReaderDelegate result;
			try
			{
				if (TD.DCGenReaderStartIsEnabled())
				{
					TD.DCGenReaderStart("Collection", collectionContract.UnderlyingType.FullName);
				}
				result = this.helper.GenerateCollectionReader(collectionContract);
			}
			finally
			{
				if (TD.DCGenReaderStopIsEnabled())
				{
					TD.DCGenReaderStop();
				}
			}
			return result;
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x0003EA24 File Offset: 0x0003CC24
		[SecurityCritical]
		public XmlFormatGetOnlyCollectionReaderDelegate GenerateGetOnlyCollectionReader(CollectionDataContract collectionContract)
		{
			XmlFormatGetOnlyCollectionReaderDelegate result;
			try
			{
				if (TD.DCGenReaderStartIsEnabled())
				{
					TD.DCGenReaderStart("GetOnlyCollection", collectionContract.UnderlyingType.FullName);
				}
				result = this.helper.GenerateGetOnlyCollectionReader(collectionContract);
			}
			finally
			{
				if (TD.DCGenReaderStopIsEnabled())
				{
					TD.DCGenReaderStop();
				}
			}
			return result;
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x0003EA7C File Offset: 0x0003CC7C
		[SecuritySafeCritical]
		internal static object UnsafeGetUninitializedObject(int id)
		{
			return FormatterServices.GetUninitializedObject(DataContract.GetDataContractForInitialization(id).TypeForInitialization);
		}

		// Token: 0x040006EB RID: 1771
		[SecurityCritical]
		private XmlFormatReaderGenerator.CriticalHelper helper;

		// Token: 0x0200013B RID: 315
		private class CriticalHelper
		{
			// Token: 0x06000FAB RID: 4011 RVA: 0x0003EA8E File Offset: 0x0003CC8E
			internal XmlFormatClassReaderDelegate GenerateClassReader(ClassDataContract classContract)
			{
				return (XmlReaderDelegator xr, XmlObjectSerializerReadContext ctx, XmlDictionaryString[] memberNames, XmlDictionaryString[] memberNamespaces) => new XmlFormatReaderInterpreter(classContract).ReadFromXml(xr, ctx, memberNames, memberNamespaces);
			}

			// Token: 0x06000FAC RID: 4012 RVA: 0x0003EAA7 File Offset: 0x0003CCA7
			internal XmlFormatCollectionReaderDelegate GenerateCollectionReader(CollectionDataContract collectionContract)
			{
				return (XmlReaderDelegator xr, XmlObjectSerializerReadContext ctx, XmlDictionaryString inm, XmlDictionaryString ins, CollectionDataContract cc) => new XmlFormatReaderInterpreter(collectionContract, false).ReadCollectionFromXml(xr, ctx, inm, ins, cc);
			}

			// Token: 0x06000FAD RID: 4013 RVA: 0x0003EAC0 File Offset: 0x0003CCC0
			internal XmlFormatGetOnlyCollectionReaderDelegate GenerateGetOnlyCollectionReader(CollectionDataContract collectionContract)
			{
				return delegate(XmlReaderDelegator xr, XmlObjectSerializerReadContext ctx, XmlDictionaryString inm, XmlDictionaryString ins, CollectionDataContract cc)
				{
					new XmlFormatReaderInterpreter(collectionContract, true).ReadGetOnlyCollectionFromXml(xr, ctx, inm, ins, cc);
				};
			}

			// Token: 0x06000FAE RID: 4014 RVA: 0x0000222F File Offset: 0x0000042F
			public CriticalHelper()
			{
			}

			// Token: 0x0200013C RID: 316
			[CompilerGenerated]
			private sealed class <>c__DisplayClass0_0
			{
				// Token: 0x06000FAF RID: 4015 RVA: 0x0000222F File Offset: 0x0000042F
				public <>c__DisplayClass0_0()
				{
				}

				// Token: 0x06000FB0 RID: 4016 RVA: 0x0003EAD9 File Offset: 0x0003CCD9
				internal object <GenerateClassReader>b__0(XmlReaderDelegator xr, XmlObjectSerializerReadContext ctx, XmlDictionaryString[] memberNames, XmlDictionaryString[] memberNamespaces)
				{
					return new XmlFormatReaderInterpreter(this.classContract).ReadFromXml(xr, ctx, memberNames, memberNamespaces);
				}

				// Token: 0x040006EC RID: 1772
				public ClassDataContract classContract;
			}

			// Token: 0x0200013D RID: 317
			[CompilerGenerated]
			private sealed class <>c__DisplayClass1_0
			{
				// Token: 0x06000FB1 RID: 4017 RVA: 0x0000222F File Offset: 0x0000042F
				public <>c__DisplayClass1_0()
				{
				}

				// Token: 0x06000FB2 RID: 4018 RVA: 0x0003EAF0 File Offset: 0x0003CCF0
				internal object <GenerateCollectionReader>b__0(XmlReaderDelegator xr, XmlObjectSerializerReadContext ctx, XmlDictionaryString inm, XmlDictionaryString ins, CollectionDataContract cc)
				{
					return new XmlFormatReaderInterpreter(this.collectionContract, false).ReadCollectionFromXml(xr, ctx, inm, ins, cc);
				}

				// Token: 0x040006ED RID: 1773
				public CollectionDataContract collectionContract;
			}

			// Token: 0x0200013E RID: 318
			[CompilerGenerated]
			private sealed class <>c__DisplayClass2_0
			{
				// Token: 0x06000FB3 RID: 4019 RVA: 0x0000222F File Offset: 0x0000042F
				public <>c__DisplayClass2_0()
				{
				}

				// Token: 0x06000FB4 RID: 4020 RVA: 0x0003EB0A File Offset: 0x0003CD0A
				internal void <GenerateGetOnlyCollectionReader>b__0(XmlReaderDelegator xr, XmlObjectSerializerReadContext ctx, XmlDictionaryString inm, XmlDictionaryString ins, CollectionDataContract cc)
				{
					new XmlFormatReaderInterpreter(this.collectionContract, true).ReadGetOnlyCollectionFromXml(xr, ctx, inm, ins, cc);
				}

				// Token: 0x040006EE RID: 1774
				public CollectionDataContract collectionContract;
			}
		}
	}
}
