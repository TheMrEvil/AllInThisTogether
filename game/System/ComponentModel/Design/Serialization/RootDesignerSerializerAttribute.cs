using System;
using System.Runtime.CompilerServices;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Indicates the base serializer to use for a root designer object. This class cannot be inherited.</summary>
	// Token: 0x02000492 RID: 1170
	[Obsolete("This attribute has been deprecated. Use DesignerSerializerAttribute instead.  For example, to specify a root designer for CodeDom, use DesignerSerializerAttribute(...,typeof(TypeCodeDomSerializer)).  https://go.microsoft.com/fwlink/?linkid=14202")]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
	public sealed class RootDesignerSerializerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.RootDesignerSerializerAttribute" /> class using the specified attributes.</summary>
		/// <param name="serializerType">The data type of the serializer.</param>
		/// <param name="baseSerializerType">The base type of the serializer. A class can include multiple serializers as they all have different base types.</param>
		/// <param name="reloadable">
		///   <see langword="true" /> if this serializer supports dynamic reloading of the document; otherwise, <see langword="false" />.</param>
		// Token: 0x06002557 RID: 9559 RVA: 0x000832A7 File Offset: 0x000814A7
		public RootDesignerSerializerAttribute(Type serializerType, Type baseSerializerType, bool reloadable)
		{
			this.SerializerTypeName = serializerType.AssemblyQualifiedName;
			this.SerializerBaseTypeName = baseSerializerType.AssemblyQualifiedName;
			this.Reloadable = reloadable;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.RootDesignerSerializerAttribute" /> class using the specified attributes.</summary>
		/// <param name="serializerTypeName">The fully qualified name of the data type of the serializer.</param>
		/// <param name="baseSerializerType">The name of the base type of the serializer. A class can include multiple serializers, as they all have different base types.</param>
		/// <param name="reloadable">
		///   <see langword="true" /> if this serializer supports dynamic reloading of the document; otherwise, <see langword="false" />.</param>
		// Token: 0x06002558 RID: 9560 RVA: 0x000832CE File Offset: 0x000814CE
		public RootDesignerSerializerAttribute(string serializerTypeName, Type baseSerializerType, bool reloadable)
		{
			this.SerializerTypeName = serializerTypeName;
			this.SerializerBaseTypeName = baseSerializerType.AssemblyQualifiedName;
			this.Reloadable = reloadable;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.RootDesignerSerializerAttribute" /> class using the specified attributes.</summary>
		/// <param name="serializerTypeName">The fully qualified name of the data type of the serializer.</param>
		/// <param name="baseSerializerTypeName">The name of the base type of the serializer. A class can include multiple serializers as they all have different base types.</param>
		/// <param name="reloadable">
		///   <see langword="true" /> if this serializer supports dynamic reloading of the document; otherwise, <see langword="false" />.</param>
		// Token: 0x06002559 RID: 9561 RVA: 0x000832F0 File Offset: 0x000814F0
		public RootDesignerSerializerAttribute(string serializerTypeName, string baseSerializerTypeName, bool reloadable)
		{
			this.SerializerTypeName = serializerTypeName;
			this.SerializerBaseTypeName = baseSerializerTypeName;
			this.Reloadable = reloadable;
		}

		/// <summary>Gets a value indicating whether the root serializer supports reloading of the design document without first disposing the designer host.</summary>
		/// <returns>
		///   <see langword="true" /> if the root serializer supports reloading; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x0600255A RID: 9562 RVA: 0x0008330D File Offset: 0x0008150D
		public bool Reloadable
		{
			[CompilerGenerated]
			get
			{
				return this.<Reloadable>k__BackingField;
			}
		}

		/// <summary>Gets the fully qualified type name of the serializer.</summary>
		/// <returns>The name of the type of the serializer.</returns>
		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x0600255B RID: 9563 RVA: 0x00083315 File Offset: 0x00081515
		public string SerializerTypeName
		{
			[CompilerGenerated]
			get
			{
				return this.<SerializerTypeName>k__BackingField;
			}
		}

		/// <summary>Gets the fully qualified type name of the base type of the serializer.</summary>
		/// <returns>The name of the base type of the serializer.</returns>
		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x0600255C RID: 9564 RVA: 0x0008331D File Offset: 0x0008151D
		public string SerializerBaseTypeName
		{
			[CompilerGenerated]
			get
			{
				return this.<SerializerBaseTypeName>k__BackingField;
			}
		}

		/// <summary>Gets a unique ID for this attribute type.</summary>
		/// <returns>An object containing a unique ID for this attribute type.</returns>
		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x0600255D RID: 9565 RVA: 0x00083328 File Offset: 0x00081528
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

		// Token: 0x040014A3 RID: 5283
		private string _typeId;

		// Token: 0x040014A4 RID: 5284
		[CompilerGenerated]
		private readonly bool <Reloadable>k__BackingField;

		// Token: 0x040014A5 RID: 5285
		[CompilerGenerated]
		private readonly string <SerializerTypeName>k__BackingField;

		// Token: 0x040014A6 RID: 5286
		[CompilerGenerated]
		private readonly string <SerializerBaseTypeName>k__BackingField;
	}
}
