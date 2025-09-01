using System;

namespace System.Configuration
{
	/// <summary>Specifies the type of a <see cref="T:System.Configuration.ConfigurationElementCollectionType" /> object.</summary>
	// Token: 0x0200001E RID: 30
	public enum ConfigurationElementCollectionType
	{
		/// <summary>Collections of this type contain elements that apply to the level at which they are specified, and to all child levels. A child level cannot modify the properties specified by a parent element of this type.</summary>
		// Token: 0x04000081 RID: 129
		BasicMap,
		/// <summary>The default type of <see cref="T:System.Configuration.ConfigurationElementCollection" />. Collections of this type contain elements that can be merged across a hierarchy of configuration files. At any particular level within such a hierarchy, <see langword="add" />, <see langword="remove" />, and <see langword="clear" /> directives are used to modify any inherited properties and specify new ones.</summary>
		// Token: 0x04000082 RID: 130
		AddRemoveClearMap,
		/// <summary>Same as <see cref="F:System.Configuration.ConfigurationElementCollectionType.BasicMap" />, except that this type causes the <see cref="T:System.Configuration.ConfigurationElementCollection" /> object to sort its contents such that inherited elements are listed last.</summary>
		// Token: 0x04000083 RID: 131
		BasicMapAlternate,
		/// <summary>Same as <see cref="F:System.Configuration.ConfigurationElementCollectionType.AddRemoveClearMap" />, except that this type causes the <see cref="T:System.Configuration.ConfigurationElementCollection" /> object to sort its contents such that inherited elements are listed last.</summary>
		// Token: 0x04000084 RID: 132
		AddRemoveClearMapAlternate
	}
}
