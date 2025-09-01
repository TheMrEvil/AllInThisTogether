using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Specifies the custom type description provider for a class. This class cannot be inherited.</summary>
	// Token: 0x020003F5 RID: 1013
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	public sealed class TypeDescriptionProviderAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeDescriptionProviderAttribute" /> class using the specified type name.</summary>
		/// <param name="typeName">The qualified name of the type.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is <see langword="null" />.</exception>
		// Token: 0x06002112 RID: 8466 RVA: 0x00071E4F File Offset: 0x0007004F
		public TypeDescriptionProviderAttribute(string typeName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			this.TypeName = typeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeDescriptionProviderAttribute" /> class using the specified type.</summary>
		/// <param name="type">The type to store in the attribute.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		// Token: 0x06002113 RID: 8467 RVA: 0x00071E6C File Offset: 0x0007006C
		public TypeDescriptionProviderAttribute(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.TypeName = type.AssemblyQualifiedName;
		}

		/// <summary>Gets the type name for the type description provider.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the qualified type name for the <see cref="T:System.ComponentModel.TypeDescriptionProvider" />.</returns>
		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06002114 RID: 8468 RVA: 0x00071E94 File Offset: 0x00070094
		public string TypeName
		{
			[CompilerGenerated]
			get
			{
				return this.<TypeName>k__BackingField;
			}
		}

		// Token: 0x04000FF5 RID: 4085
		[CompilerGenerated]
		private readonly string <TypeName>k__BackingField;
	}
}
