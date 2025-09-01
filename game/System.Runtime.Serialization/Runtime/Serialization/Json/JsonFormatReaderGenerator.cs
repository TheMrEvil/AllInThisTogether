using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Diagnostics.Application;
using System.Security;
using System.Xml;

namespace System.Runtime.Serialization.Json
{
	// Token: 0x02000177 RID: 375
	internal sealed class JsonFormatReaderGenerator
	{
		// Token: 0x0600136F RID: 4975 RVA: 0x0004B4E4 File Offset: 0x000496E4
		[SecurityCritical]
		public JsonFormatReaderGenerator()
		{
			this.helper = new JsonFormatReaderGenerator.CriticalHelper();
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x0004B4F8 File Offset: 0x000496F8
		[SecurityCritical]
		public JsonFormatClassReaderDelegate GenerateClassReader(ClassDataContract classContract)
		{
			JsonFormatClassReaderDelegate result;
			try
			{
				if (TD.DCJsonGenReaderStartIsEnabled())
				{
					TD.DCJsonGenReaderStart("Class", classContract.UnderlyingType.FullName);
				}
				result = this.helper.GenerateClassReader(classContract);
			}
			finally
			{
				if (TD.DCJsonGenReaderStopIsEnabled())
				{
					TD.DCJsonGenReaderStop();
				}
			}
			return result;
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x0004B550 File Offset: 0x00049750
		[SecurityCritical]
		public JsonFormatCollectionReaderDelegate GenerateCollectionReader(CollectionDataContract collectionContract)
		{
			JsonFormatCollectionReaderDelegate result;
			try
			{
				if (TD.DCJsonGenReaderStartIsEnabled())
				{
					TD.DCJsonGenReaderStart("Collection", collectionContract.StableName.Name);
				}
				result = this.helper.GenerateCollectionReader(collectionContract);
			}
			finally
			{
				if (TD.DCJsonGenReaderStopIsEnabled())
				{
					TD.DCJsonGenReaderStop();
				}
			}
			return result;
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x0004B5A8 File Offset: 0x000497A8
		[SecurityCritical]
		public JsonFormatGetOnlyCollectionReaderDelegate GenerateGetOnlyCollectionReader(CollectionDataContract collectionContract)
		{
			JsonFormatGetOnlyCollectionReaderDelegate result;
			try
			{
				if (TD.DCJsonGenReaderStartIsEnabled())
				{
					TD.DCJsonGenReaderStart("GetOnlyCollection", collectionContract.UnderlyingType.FullName);
				}
				result = this.helper.GenerateGetOnlyCollectionReader(collectionContract);
			}
			finally
			{
				if (TD.DCJsonGenReaderStopIsEnabled())
				{
					TD.DCJsonGenReaderStop();
				}
			}
			return result;
		}

		// Token: 0x040009B6 RID: 2486
		[SecurityCritical]
		private JsonFormatReaderGenerator.CriticalHelper helper;

		// Token: 0x02000178 RID: 376
		private class CriticalHelper
		{
			// Token: 0x06001373 RID: 4979 RVA: 0x0004B600 File Offset: 0x00049800
			internal JsonFormatClassReaderDelegate GenerateClassReader(ClassDataContract classContract)
			{
				return (XmlReaderDelegator xr, XmlObjectSerializerReadContextComplexJson ctx, XmlDictionaryString emptyDictionaryString, XmlDictionaryString[] memberNames) => new JsonFormatReaderInterpreter(classContract).ReadFromJson(xr, ctx, emptyDictionaryString, memberNames);
			}

			// Token: 0x06001374 RID: 4980 RVA: 0x0004B619 File Offset: 0x00049819
			internal JsonFormatCollectionReaderDelegate GenerateCollectionReader(CollectionDataContract collectionContract)
			{
				return (XmlReaderDelegator xr, XmlObjectSerializerReadContextComplexJson ctx, XmlDictionaryString emptyDS, XmlDictionaryString inm, CollectionDataContract cc) => new JsonFormatReaderInterpreter(collectionContract, false).ReadCollectionFromJson(xr, ctx, emptyDS, inm, cc);
			}

			// Token: 0x06001375 RID: 4981 RVA: 0x0004B632 File Offset: 0x00049832
			internal JsonFormatGetOnlyCollectionReaderDelegate GenerateGetOnlyCollectionReader(CollectionDataContract collectionContract)
			{
				return delegate(XmlReaderDelegator xr, XmlObjectSerializerReadContextComplexJson ctx, XmlDictionaryString emptyDS, XmlDictionaryString inm, CollectionDataContract cc)
				{
					new JsonFormatReaderInterpreter(collectionContract, true).ReadGetOnlyCollectionFromJson(xr, ctx, emptyDS, inm, cc);
				};
			}

			// Token: 0x06001376 RID: 4982 RVA: 0x0000222F File Offset: 0x0000042F
			public CriticalHelper()
			{
			}

			// Token: 0x02000179 RID: 377
			[CompilerGenerated]
			private sealed class <>c__DisplayClass0_0
			{
				// Token: 0x06001377 RID: 4983 RVA: 0x0000222F File Offset: 0x0000042F
				public <>c__DisplayClass0_0()
				{
				}

				// Token: 0x06001378 RID: 4984 RVA: 0x0004B64B File Offset: 0x0004984B
				internal object <GenerateClassReader>b__0(XmlReaderDelegator xr, XmlObjectSerializerReadContextComplexJson ctx, XmlDictionaryString emptyDictionaryString, XmlDictionaryString[] memberNames)
				{
					return new JsonFormatReaderInterpreter(this.classContract).ReadFromJson(xr, ctx, emptyDictionaryString, memberNames);
				}

				// Token: 0x040009B7 RID: 2487
				public ClassDataContract classContract;
			}

			// Token: 0x0200017A RID: 378
			[CompilerGenerated]
			private sealed class <>c__DisplayClass1_0
			{
				// Token: 0x06001379 RID: 4985 RVA: 0x0000222F File Offset: 0x0000042F
				public <>c__DisplayClass1_0()
				{
				}

				// Token: 0x0600137A RID: 4986 RVA: 0x0004B662 File Offset: 0x00049862
				internal object <GenerateCollectionReader>b__0(XmlReaderDelegator xr, XmlObjectSerializerReadContextComplexJson ctx, XmlDictionaryString emptyDS, XmlDictionaryString inm, CollectionDataContract cc)
				{
					return new JsonFormatReaderInterpreter(this.collectionContract, false).ReadCollectionFromJson(xr, ctx, emptyDS, inm, cc);
				}

				// Token: 0x040009B8 RID: 2488
				public CollectionDataContract collectionContract;
			}

			// Token: 0x0200017B RID: 379
			[CompilerGenerated]
			private sealed class <>c__DisplayClass2_0
			{
				// Token: 0x0600137B RID: 4987 RVA: 0x0000222F File Offset: 0x0000042F
				public <>c__DisplayClass2_0()
				{
				}

				// Token: 0x0600137C RID: 4988 RVA: 0x0004B67C File Offset: 0x0004987C
				internal void <GenerateGetOnlyCollectionReader>b__0(XmlReaderDelegator xr, XmlObjectSerializerReadContextComplexJson ctx, XmlDictionaryString emptyDS, XmlDictionaryString inm, CollectionDataContract cc)
				{
					new JsonFormatReaderInterpreter(this.collectionContract, true).ReadGetOnlyCollectionFromJson(xr, ctx, emptyDS, inm, cc);
				}

				// Token: 0x040009B9 RID: 2489
				public CollectionDataContract collectionContract;
			}
		}
	}
}
