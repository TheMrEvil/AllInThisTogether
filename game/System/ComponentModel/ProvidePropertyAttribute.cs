using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Specifies the name of the property that an implementer of <see cref="T:System.ComponentModel.IExtenderProvider" /> offers to other components. This class cannot be inherited</summary>
	// Token: 0x020003E2 RID: 994
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class ProvidePropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ProvidePropertyAttribute" /> class with the name of the property and its <see cref="T:System.Type" />.</summary>
		/// <param name="propertyName">The name of the property extending to an object of the specified type.</param>
		/// <param name="receiverType">The <see cref="T:System.Type" /> of the data type of the object that can receive the property.</param>
		// Token: 0x060020A5 RID: 8357 RVA: 0x00070C28 File Offset: 0x0006EE28
		public ProvidePropertyAttribute(string propertyName, Type receiverType)
		{
			this.PropertyName = propertyName;
			this.ReceiverTypeName = receiverType.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ProvidePropertyAttribute" /> class with the name of the property and the type of its receiver.</summary>
		/// <param name="propertyName">The name of the property extending to an object of the specified type.</param>
		/// <param name="receiverTypeName">The name of the data type this property can extend.</param>
		// Token: 0x060020A6 RID: 8358 RVA: 0x00070C43 File Offset: 0x0006EE43
		public ProvidePropertyAttribute(string propertyName, string receiverTypeName)
		{
			this.PropertyName = propertyName;
			this.ReceiverTypeName = receiverTypeName;
		}

		/// <summary>Gets the name of a property that this class provides.</summary>
		/// <returns>The name of a property that this class provides.</returns>
		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x060020A7 RID: 8359 RVA: 0x00070C59 File Offset: 0x0006EE59
		public string PropertyName
		{
			[CompilerGenerated]
			get
			{
				return this.<PropertyName>k__BackingField;
			}
		}

		/// <summary>Gets the name of the data type this property can extend.</summary>
		/// <returns>The name of the data type this property can extend.</returns>
		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x060020A8 RID: 8360 RVA: 0x00070C61 File Offset: 0x0006EE61
		public string ReceiverTypeName
		{
			[CompilerGenerated]
			get
			{
				return this.<ReceiverTypeName>k__BackingField;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.ProvidePropertyAttribute" />.</summary>
		/// <param name="obj">The object to test the value equality of.</param>
		/// <returns>
		///   <see langword="true" /> if the value of the given object is equal to that of the current; otherwise, <see langword="false" />.</returns>
		// Token: 0x060020A9 RID: 8361 RVA: 0x00070C6C File Offset: 0x0006EE6C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ProvidePropertyAttribute providePropertyAttribute = obj as ProvidePropertyAttribute;
			return providePropertyAttribute != null && providePropertyAttribute.PropertyName == this.PropertyName && providePropertyAttribute.ReceiverTypeName == this.ReceiverTypeName;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.ProvidePropertyAttribute" />.</returns>
		// Token: 0x060020AA RID: 8362 RVA: 0x00070CAF File Offset: 0x0006EEAF
		public override int GetHashCode()
		{
			return this.PropertyName.GetHashCode() ^ this.ReceiverTypeName.GetHashCode();
		}

		/// <summary>Gets a unique identifier for this attribute.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is a unique identifier for the attribute.</returns>
		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x060020AB RID: 8363 RVA: 0x00070CC8 File Offset: 0x0006EEC8
		public override object TypeId
		{
			get
			{
				return base.GetType().FullName + this.PropertyName;
			}
		}

		// Token: 0x04000FD1 RID: 4049
		[CompilerGenerated]
		private readonly string <PropertyName>k__BackingField;

		// Token: 0x04000FD2 RID: 4050
		[CompilerGenerated]
		private readonly string <ReceiverTypeName>k__BackingField;
	}
}
