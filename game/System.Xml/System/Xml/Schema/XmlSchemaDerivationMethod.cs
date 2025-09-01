using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Provides different methods for preventing derivation.</summary>
	// Token: 0x020005A9 RID: 1449
	[Flags]
	public enum XmlSchemaDerivationMethod
	{
		/// <summary>Override default derivation method to allow any derivation.</summary>
		// Token: 0x04002B1F RID: 11039
		[XmlEnum("")]
		Empty = 0,
		/// <summary>Refers to derivations by <see langword="Substitution" />.</summary>
		// Token: 0x04002B20 RID: 11040
		[XmlEnum("substitution")]
		Substitution = 1,
		/// <summary>Refers to derivations by <see langword="Extension" />.</summary>
		// Token: 0x04002B21 RID: 11041
		[XmlEnum("extension")]
		Extension = 2,
		/// <summary>Refers to derivations by <see langword="Restriction" />.</summary>
		// Token: 0x04002B22 RID: 11042
		[XmlEnum("restriction")]
		Restriction = 4,
		/// <summary>Refers to derivations by <see langword="List" />.</summary>
		// Token: 0x04002B23 RID: 11043
		[XmlEnum("list")]
		List = 8,
		/// <summary>Refers to derivations by <see langword="Union" />.</summary>
		// Token: 0x04002B24 RID: 11044
		[XmlEnum("union")]
		Union = 16,
		/// <summary>
		///     <see langword="#all" />. Refers to all derivation methods.</summary>
		// Token: 0x04002B25 RID: 11045
		[XmlEnum("#all")]
		All = 255,
		/// <summary>Accepts the default derivation method.</summary>
		// Token: 0x04002B26 RID: 11046
		[XmlIgnore]
		None = 256
	}
}
