using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Diagnostics.Application;
using System.Security;
using System.Xml;

namespace System.Runtime.Serialization.Json
{
	// Token: 0x0200017E RID: 382
	internal class JsonFormatWriterGenerator
	{
		// Token: 0x06001385 RID: 4997 RVA: 0x0004B696 File Offset: 0x00049896
		[SecurityCritical]
		public JsonFormatWriterGenerator()
		{
			this.helper = new JsonFormatWriterGenerator.CriticalHelper();
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x0004B6AC File Offset: 0x000498AC
		[SecurityCritical]
		internal JsonFormatClassWriterDelegate GenerateClassWriter(ClassDataContract classContract)
		{
			JsonFormatClassWriterDelegate result;
			try
			{
				if (TD.DCJsonGenWriterStartIsEnabled())
				{
					TD.DCJsonGenWriterStart("Class", classContract.UnderlyingType.FullName);
				}
				result = this.helper.GenerateClassWriter(classContract);
			}
			finally
			{
				if (TD.DCJsonGenWriterStopIsEnabled())
				{
					TD.DCJsonGenWriterStop();
				}
			}
			return result;
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x0004B704 File Offset: 0x00049904
		[SecurityCritical]
		internal JsonFormatCollectionWriterDelegate GenerateCollectionWriter(CollectionDataContract collectionContract)
		{
			JsonFormatCollectionWriterDelegate result;
			try
			{
				if (TD.DCJsonGenWriterStartIsEnabled())
				{
					TD.DCJsonGenWriterStart("Collection", collectionContract.UnderlyingType.FullName);
				}
				result = this.helper.GenerateCollectionWriter(collectionContract);
			}
			finally
			{
				if (TD.DCJsonGenWriterStopIsEnabled())
				{
					TD.DCJsonGenWriterStop();
				}
			}
			return result;
		}

		// Token: 0x040009BA RID: 2490
		[SecurityCritical]
		private JsonFormatWriterGenerator.CriticalHelper helper;

		// Token: 0x0200017F RID: 383
		private class CriticalHelper
		{
			// Token: 0x06001388 RID: 5000 RVA: 0x0004B75C File Offset: 0x0004995C
			internal JsonFormatClassWriterDelegate GenerateClassWriter(ClassDataContract classContract)
			{
				return delegate(XmlWriterDelegator xmlWriter, object obj, XmlObjectSerializerWriteContextComplexJson context, ClassDataContract dataContract, XmlDictionaryString[] memberNames)
				{
					new JsonFormatWriterInterpreter(classContract).WriteToJson(xmlWriter, obj, context, dataContract, memberNames);
				};
			}

			// Token: 0x06001389 RID: 5001 RVA: 0x0004B775 File Offset: 0x00049975
			internal JsonFormatCollectionWriterDelegate GenerateCollectionWriter(CollectionDataContract collectionContract)
			{
				return delegate(XmlWriterDelegator xmlWriter, object obj, XmlObjectSerializerWriteContextComplexJson context, CollectionDataContract dataContract)
				{
					new JsonFormatWriterInterpreter(collectionContract).WriteCollectionToJson(xmlWriter, obj, context, dataContract);
				};
			}

			// Token: 0x0600138A RID: 5002 RVA: 0x0000222F File Offset: 0x0000042F
			public CriticalHelper()
			{
			}

			// Token: 0x02000180 RID: 384
			[CompilerGenerated]
			private sealed class <>c__DisplayClass0_0
			{
				// Token: 0x0600138B RID: 5003 RVA: 0x0000222F File Offset: 0x0000042F
				public <>c__DisplayClass0_0()
				{
				}

				// Token: 0x0600138C RID: 5004 RVA: 0x0004B78E File Offset: 0x0004998E
				internal void <GenerateClassWriter>b__0(XmlWriterDelegator xmlWriter, object obj, XmlObjectSerializerWriteContextComplexJson context, ClassDataContract dataContract, XmlDictionaryString[] memberNames)
				{
					new JsonFormatWriterInterpreter(this.classContract).WriteToJson(xmlWriter, obj, context, dataContract, memberNames);
				}

				// Token: 0x040009BB RID: 2491
				public ClassDataContract classContract;
			}

			// Token: 0x02000181 RID: 385
			[CompilerGenerated]
			private sealed class <>c__DisplayClass1_0
			{
				// Token: 0x0600138D RID: 5005 RVA: 0x0000222F File Offset: 0x0000042F
				public <>c__DisplayClass1_0()
				{
				}

				// Token: 0x0600138E RID: 5006 RVA: 0x0004B7A7 File Offset: 0x000499A7
				internal void <GenerateCollectionWriter>b__0(XmlWriterDelegator xmlWriter, object obj, XmlObjectSerializerWriteContextComplexJson context, CollectionDataContract dataContract)
				{
					new JsonFormatWriterInterpreter(this.collectionContract).WriteCollectionToJson(xmlWriter, obj, context, dataContract);
				}

				// Token: 0x040009BC RID: 2492
				public CollectionDataContract collectionContract;
			}
		}
	}
}
