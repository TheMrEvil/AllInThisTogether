using System;

namespace System.Xml.Schema
{
	/// <summary>Specifies schema validation options used by the <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> and <see cref="T:System.Xml.XmlReader" /> classes.</summary>
	// Token: 0x020005E8 RID: 1512
	[Flags]
	public enum XmlSchemaValidationFlags
	{
		/// <summary>Do not process identity constraints, inline schemas, schema location hints, or report schema validation warnings.</summary>
		// Token: 0x04002BE3 RID: 11235
		None = 0,
		/// <summary>Process inline schemas encountered during validation.</summary>
		// Token: 0x04002BE4 RID: 11236
		ProcessInlineSchema = 1,
		/// <summary>Process schema location hints (xsi:schemaLocation, xsi:noNamespaceSchemaLocation) encountered during validation.</summary>
		// Token: 0x04002BE5 RID: 11237
		ProcessSchemaLocation = 2,
		/// <summary>Report schema validation warnings encountered during validation.</summary>
		// Token: 0x04002BE6 RID: 11238
		ReportValidationWarnings = 4,
		/// <summary>Process identity constraints (xs:ID, xs:IDREF, xs:key, xs:keyref, xs:unique) encountered during validation.</summary>
		// Token: 0x04002BE7 RID: 11239
		ProcessIdentityConstraints = 8,
		/// <summary>Allow xml:* attributes even if they are not defined in the schema. The attributes will be validated based on their data type.</summary>
		// Token: 0x04002BE8 RID: 11240
		AllowXmlAttributes = 16
	}
}
