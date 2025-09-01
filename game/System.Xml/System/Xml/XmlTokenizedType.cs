using System;

namespace System.Xml
{
	/// <summary>Represents the XML type for the string. This allows the string to be read as a particular XML type, for example a CDATA section type.</summary>
	// Token: 0x02000221 RID: 545
	public enum XmlTokenizedType
	{
		/// <summary>CDATA type.</summary>
		// Token: 0x0400127F RID: 4735
		CDATA,
		/// <summary>ID type.</summary>
		// Token: 0x04001280 RID: 4736
		ID,
		/// <summary>IDREF type.</summary>
		// Token: 0x04001281 RID: 4737
		IDREF,
		/// <summary>IDREFS type.</summary>
		// Token: 0x04001282 RID: 4738
		IDREFS,
		/// <summary>ENTITY type.</summary>
		// Token: 0x04001283 RID: 4739
		ENTITY,
		/// <summary>ENTITIES type.</summary>
		// Token: 0x04001284 RID: 4740
		ENTITIES,
		/// <summary>NMTOKEN type.</summary>
		// Token: 0x04001285 RID: 4741
		NMTOKEN,
		/// <summary>NMTOKENS type.</summary>
		// Token: 0x04001286 RID: 4742
		NMTOKENS,
		/// <summary>NOTATION type.</summary>
		// Token: 0x04001287 RID: 4743
		NOTATION,
		/// <summary>ENUMERATION type.</summary>
		// Token: 0x04001288 RID: 4744
		ENUMERATION,
		/// <summary>QName type.</summary>
		// Token: 0x04001289 RID: 4745
		QName,
		/// <summary>NCName type.</summary>
		// Token: 0x0400128A RID: 4746
		NCName,
		/// <summary>No type.</summary>
		// Token: 0x0400128B RID: 4747
		None
	}
}
