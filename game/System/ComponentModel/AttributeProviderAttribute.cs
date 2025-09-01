using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Enables attribute redirection. This class cannot be inherited.</summary>
	// Token: 0x0200037F RID: 895
	[AttributeUsage(AttributeTargets.Property)]
	public class AttributeProviderAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AttributeProviderAttribute" /> class with the given type name.</summary>
		/// <param name="typeName">The name of the type to specify.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is <see langword="null" />.</exception>
		// Token: 0x06001D6F RID: 7535 RVA: 0x00068F18 File Offset: 0x00067118
		public AttributeProviderAttribute(string typeName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			this.TypeName = typeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AttributeProviderAttribute" /> class with the given type name and property name.</summary>
		/// <param name="typeName">The name of the type to specify.</param>
		/// <param name="propertyName">The name of the property for which attributes will be retrieved.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="propertyName" /> is <see langword="null" />.</exception>
		// Token: 0x06001D70 RID: 7536 RVA: 0x00068F35 File Offset: 0x00067135
		public AttributeProviderAttribute(string typeName, string propertyName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			if (propertyName == null)
			{
				throw new ArgumentNullException("propertyName");
			}
			this.TypeName = typeName;
			this.PropertyName = propertyName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AttributeProviderAttribute" /> class with the given type.</summary>
		/// <param name="type">The type to specify.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		// Token: 0x06001D71 RID: 7537 RVA: 0x00068F67 File Offset: 0x00067167
		public AttributeProviderAttribute(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.TypeName = type.AssemblyQualifiedName;
		}

		/// <summary>Gets the assembly qualified type name passed into the constructor.</summary>
		/// <returns>The assembly qualified name of the type specified in the constructor.</returns>
		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001D72 RID: 7538 RVA: 0x00068F8F File Offset: 0x0006718F
		public string TypeName
		{
			[CompilerGenerated]
			get
			{
				return this.<TypeName>k__BackingField;
			}
		}

		/// <summary>Gets the name of the property for which attributes will be retrieved.</summary>
		/// <returns>The name of the property for which attributes will be retrieved.</returns>
		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06001D73 RID: 7539 RVA: 0x00068F97 File Offset: 0x00067197
		public string PropertyName
		{
			[CompilerGenerated]
			get
			{
				return this.<PropertyName>k__BackingField;
			}
		}

		// Token: 0x04000EDA RID: 3802
		[CompilerGenerated]
		private readonly string <TypeName>k__BackingField;

		// Token: 0x04000EDB RID: 3803
		[CompilerGenerated]
		private readonly string <PropertyName>k__BackingField;
	}
}
