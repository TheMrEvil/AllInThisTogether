using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Indicates a serializer for the serialization manager to use to serialize the values of the type this attribute is applied to. This class cannot be inherited.</summary>
	// Token: 0x02000484 RID: 1156
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
	public sealed class DesignerSerializerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.DesignerSerializerAttribute" /> class.</summary>
		/// <param name="serializerType">The data type of the serializer.</param>
		/// <param name="baseSerializerType">The base data type of the serializer. Multiple serializers can be supplied for a class as long as the serializers have different base types.</param>
		// Token: 0x0600250F RID: 9487 RVA: 0x00082BD2 File Offset: 0x00080DD2
		public DesignerSerializerAttribute(Type serializerType, Type baseSerializerType)
		{
			this.SerializerTypeName = serializerType.AssemblyQualifiedName;
			this.SerializerBaseTypeName = baseSerializerType.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.DesignerSerializerAttribute" /> class.</summary>
		/// <param name="serializerTypeName">The fully qualified name of the data type of the serializer.</param>
		/// <param name="baseSerializerType">The base data type of the serializer. Multiple serializers can be supplied for a class as long as the serializers have different base types.</param>
		// Token: 0x06002510 RID: 9488 RVA: 0x00082BF2 File Offset: 0x00080DF2
		public DesignerSerializerAttribute(string serializerTypeName, Type baseSerializerType)
		{
			this.SerializerTypeName = serializerTypeName;
			this.SerializerBaseTypeName = baseSerializerType.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.DesignerSerializerAttribute" /> class.</summary>
		/// <param name="serializerTypeName">The fully qualified name of the data type of the serializer.</param>
		/// <param name="baseSerializerTypeName">The fully qualified name of the base data type of the serializer. Multiple serializers can be supplied for a class as long as the serializers have different base types.</param>
		// Token: 0x06002511 RID: 9489 RVA: 0x00082C0D File Offset: 0x00080E0D
		public DesignerSerializerAttribute(string serializerTypeName, string baseSerializerTypeName)
		{
			this.SerializerTypeName = serializerTypeName;
			this.SerializerBaseTypeName = baseSerializerTypeName;
		}

		/// <summary>Gets the fully qualified type name of the serializer.</summary>
		/// <returns>The fully qualified type name of the serializer.</returns>
		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06002512 RID: 9490 RVA: 0x00082C23 File Offset: 0x00080E23
		public string SerializerTypeName
		{
			[CompilerGenerated]
			get
			{
				return this.<SerializerTypeName>k__BackingField;
			}
		}

		/// <summary>Gets the fully qualified type name of the serializer base type.</summary>
		/// <returns>The fully qualified type name of the serializer base type.</returns>
		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06002513 RID: 9491 RVA: 0x00082C2B File Offset: 0x00080E2B
		public string SerializerBaseTypeName
		{
			[CompilerGenerated]
			get
			{
				return this.<SerializerBaseTypeName>k__BackingField;
			}
		}

		/// <summary>Indicates a unique ID for this attribute type.</summary>
		/// <returns>A unique ID for this attribute type.</returns>
		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06002514 RID: 9492 RVA: 0x00082C34 File Offset: 0x00080E34
		public override object TypeId
		{
			get
			{
				if (this._typeId == null)
				{
					string text = this.SerializerBaseTypeName;
					int num = text.IndexOf(',');
					if (num != -1)
					{
						text = text.Substring(0, num);
					}
					this._typeId = base.GetType().FullName + text;
				}
				return this._typeId;
			}
		}

		// Token: 0x04001494 RID: 5268
		private string _typeId;

		// Token: 0x04001495 RID: 5269
		[CompilerGenerated]
		private readonly string <SerializerTypeName>k__BackingField;

		// Token: 0x04001496 RID: 5270
		[CompilerGenerated]
		private readonly string <SerializerBaseTypeName>k__BackingField;
	}
}
