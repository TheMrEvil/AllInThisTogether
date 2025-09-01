using System;
using System.Reflection;

namespace System.Security.Cryptography.Asn1
{
	// Token: 0x020000CC RID: 204
	internal class AsnAmbiguousFieldTypeException : AsnSerializationConstraintException
	{
		// Token: 0x06000524 RID: 1316 RVA: 0x00014D10 File Offset: 0x00012F10
		public AsnAmbiguousFieldTypeException(FieldInfo fieldInfo, Type ambiguousType) : base(SR.Format("Field '{0}' of type '{1}' has ambiguous type '{2}', an attribute derived from AsnTypeAttribute is required.", fieldInfo.Name, fieldInfo.DeclaringType.FullName, ambiguousType.Namespace))
		{
		}
	}
}
