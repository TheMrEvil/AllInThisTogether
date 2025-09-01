using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
	/// <summary>Specifies a property that is offered by an extender provider. This class cannot be inherited.</summary>
	// Token: 0x020003AB RID: 939
	[AttributeUsage(AttributeTargets.All)]
	public sealed class ExtenderProvidedPropertyAttribute : Attribute
	{
		// Token: 0x06001EC6 RID: 7878 RVA: 0x0006CB0F File Offset: 0x0006AD0F
		internal static ExtenderProvidedPropertyAttribute Create(PropertyDescriptor extenderProperty, Type receiverType, IExtenderProvider provider)
		{
			return new ExtenderProvidedPropertyAttribute
			{
				ExtenderProperty = extenderProperty,
				ReceiverType = receiverType,
				Provider = provider
			};
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ExtenderProvidedPropertyAttribute" /> class.</summary>
		// Token: 0x06001EC7 RID: 7879 RVA: 0x00003D9F File Offset: 0x00001F9F
		public ExtenderProvidedPropertyAttribute()
		{
		}

		/// <summary>Gets the property that is being provided.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> encapsulating the property that is being provided.</returns>
		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001EC8 RID: 7880 RVA: 0x0006CB2B File Offset: 0x0006AD2B
		// (set) Token: 0x06001EC9 RID: 7881 RVA: 0x0006CB33 File Offset: 0x0006AD33
		public PropertyDescriptor ExtenderProperty
		{
			[CompilerGenerated]
			get
			{
				return this.<ExtenderProperty>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ExtenderProperty>k__BackingField = value;
			}
		}

		/// <summary>Gets the extender provider that is providing the property.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IExtenderProvider" /> that is providing the property.</returns>
		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001ECA RID: 7882 RVA: 0x0006CB3C File Offset: 0x0006AD3C
		// (set) Token: 0x06001ECB RID: 7883 RVA: 0x0006CB44 File Offset: 0x0006AD44
		public IExtenderProvider Provider
		{
			[CompilerGenerated]
			get
			{
				return this.<Provider>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Provider>k__BackingField = value;
			}
		}

		/// <summary>Gets the type of object that can receive the property.</summary>
		/// <returns>A <see cref="T:System.Type" /> describing the type of object that can receive the property.</returns>
		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001ECC RID: 7884 RVA: 0x0006CB4D File Offset: 0x0006AD4D
		// (set) Token: 0x06001ECD RID: 7885 RVA: 0x0006CB55 File Offset: 0x0006AD55
		public Type ReceiverType
		{
			[CompilerGenerated]
			get
			{
				return this.<ReceiverType>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ReceiverType>k__BackingField = value;
			}
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance or a null reference (<see langword="Nothing" /> in Visual Basic).</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> equals the type and value of this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001ECE RID: 7886 RVA: 0x0006CB60 File Offset: 0x0006AD60
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ExtenderProvidedPropertyAttribute extenderProvidedPropertyAttribute = obj as ExtenderProvidedPropertyAttribute;
			return extenderProvidedPropertyAttribute != null && extenderProvidedPropertyAttribute.ExtenderProperty.Equals(this.ExtenderProperty) && extenderProvidedPropertyAttribute.Provider.Equals(this.Provider) && extenderProvidedPropertyAttribute.ReceiverType.Equals(this.ReceiverType);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001ECF RID: 7887 RVA: 0x000678FA File Offset: 0x00065AFA
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Provides an indication whether the value of this instance is the default value for the derived class.</summary>
		/// <returns>
		///   <see langword="true" /> if this instance is the default attribute for the class; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001ED0 RID: 7888 RVA: 0x0006CBB6 File Offset: 0x0006ADB6
		public override bool IsDefaultAttribute()
		{
			return this.ReceiverType == null;
		}

		// Token: 0x04000F46 RID: 3910
		[CompilerGenerated]
		private PropertyDescriptor <ExtenderProperty>k__BackingField;

		// Token: 0x04000F47 RID: 3911
		[CompilerGenerated]
		private IExtenderProvider <Provider>k__BackingField;

		// Token: 0x04000F48 RID: 3912
		[CompilerGenerated]
		private Type <ReceiverType>k__BackingField;
	}
}
