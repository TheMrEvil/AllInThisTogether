using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Specifies what type to use as a converter for the object this attribute is bound to.</summary>
	// Token: 0x020003F2 RID: 1010
	[AttributeUsage(AttributeTargets.All)]
	public sealed class TypeConverterAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeConverterAttribute" /> class with the default type converter, which is an empty string ("").</summary>
		// Token: 0x060020FB RID: 8443 RVA: 0x00071BAF File Offset: 0x0006FDAF
		public TypeConverterAttribute()
		{
			this.ConverterTypeName = string.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeConverterAttribute" /> class, using the specified type as the data converter for the object this attribute is bound to.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of the converter class to use for data conversion for the object this attribute is bound to.</param>
		// Token: 0x060020FC RID: 8444 RVA: 0x00071BC2 File Offset: 0x0006FDC2
		public TypeConverterAttribute(Type type)
		{
			this.ConverterTypeName = type.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeConverterAttribute" /> class, using the specified type name as the data converter for the object this attribute is bound to.</summary>
		/// <param name="typeName">The fully qualified name of the class to use for data conversion for the object this attribute is bound to.</param>
		// Token: 0x060020FD RID: 8445 RVA: 0x00071BD6 File Offset: 0x0006FDD6
		public TypeConverterAttribute(string typeName)
		{
			this.ConverterTypeName = typeName;
		}

		/// <summary>Gets the fully qualified type name of the <see cref="T:System.Type" /> to use as a converter for the object this attribute is bound to.</summary>
		/// <returns>The fully qualified type name of the <see cref="T:System.Type" /> to use as a converter for the object this attribute is bound to, or an empty string ("") if none exists. The default value is an empty string ("").</returns>
		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x060020FE RID: 8446 RVA: 0x00071BE5 File Offset: 0x0006FDE5
		public string ConverterTypeName
		{
			[CompilerGenerated]
			get
			{
				return this.<ConverterTypeName>k__BackingField;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.TypeConverterAttribute" />.</summary>
		/// <param name="obj">The object to test the value equality of.</param>
		/// <returns>
		///   <see langword="true" /> if the value of the given object is equal to that of the current <see cref="T:System.ComponentModel.TypeConverterAttribute" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x060020FF RID: 8447 RVA: 0x00071BF0 File Offset: 0x0006FDF0
		public override bool Equals(object obj)
		{
			TypeConverterAttribute typeConverterAttribute = obj as TypeConverterAttribute;
			return typeConverterAttribute != null && typeConverterAttribute.ConverterTypeName == this.ConverterTypeName;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.TypeConverterAttribute" />.</returns>
		// Token: 0x06002100 RID: 8448 RVA: 0x00071C1A File Offset: 0x0006FE1A
		public override int GetHashCode()
		{
			return this.ConverterTypeName.GetHashCode();
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x00071C27 File Offset: 0x0006FE27
		// Note: this type is marked as 'beforefieldinit'.
		static TypeConverterAttribute()
		{
		}

		/// <summary>Specifies the type to use as a converter for the object this attribute is bound to.</summary>
		// Token: 0x04000FF1 RID: 4081
		public static readonly TypeConverterAttribute Default = new TypeConverterAttribute();

		// Token: 0x04000FF2 RID: 4082
		[CompilerGenerated]
		private readonly string <ConverterTypeName>k__BackingField;
	}
}
