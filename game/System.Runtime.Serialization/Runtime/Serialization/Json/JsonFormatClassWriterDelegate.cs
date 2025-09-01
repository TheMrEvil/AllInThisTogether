using System;
using System.Xml;

namespace System.Runtime.Serialization.Json
{
	// Token: 0x0200017C RID: 380
	// (Invoke) Token: 0x0600137E RID: 4990
	internal delegate void JsonFormatClassWriterDelegate(XmlWriterDelegator xmlWriter, object obj, XmlObjectSerializerWriteContextComplexJson context, ClassDataContract dataContract, XmlDictionaryString[] memberNames);
}
