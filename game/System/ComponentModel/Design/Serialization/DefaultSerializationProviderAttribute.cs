using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>The <see cref="T:System.ComponentModel.Design.Serialization.DefaultSerializationProviderAttribute" /> attribute is placed on a serializer to indicate the class to use as a default provider of that type of serializer.</summary>
	// Token: 0x02000482 RID: 1154
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public sealed class DefaultSerializationProviderAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.DefaultSerializationProviderAttribute" /> class with the given provider type.</summary>
		/// <param name="providerType">The <see cref="T:System.Type" /> of the serialization provider.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="providerType" /> is <see langword="null" />.</exception>
		// Token: 0x06002507 RID: 9479 RVA: 0x00082B85 File Offset: 0x00080D85
		public DefaultSerializationProviderAttribute(Type providerType)
		{
			if (providerType == null)
			{
				throw new ArgumentNullException("providerType");
			}
			this.ProviderTypeName = providerType.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.DefaultSerializationProviderAttribute" /> class with the named provider type.</summary>
		/// <param name="providerTypeName">The name of the serialization provider type.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="providerTypeName" /> is <see langword="null" />.</exception>
		// Token: 0x06002508 RID: 9480 RVA: 0x00082BAD File Offset: 0x00080DAD
		public DefaultSerializationProviderAttribute(string providerTypeName)
		{
			if (providerTypeName == null)
			{
				throw new ArgumentNullException("providerTypeName");
			}
			this.ProviderTypeName = providerTypeName;
		}

		/// <summary>Gets the type name of the serialization provider.</summary>
		/// <returns>A string containing the name of the provider.</returns>
		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06002509 RID: 9481 RVA: 0x00082BCA File Offset: 0x00080DCA
		public string ProviderTypeName
		{
			[CompilerGenerated]
			get
			{
				return this.<ProviderTypeName>k__BackingField;
			}
		}

		// Token: 0x04001493 RID: 5267
		[CompilerGenerated]
		private readonly string <ProviderTypeName>k__BackingField;
	}
}
