using System;
using System.ComponentModel.Composition.Primitives;
using System.Runtime.CompilerServices;
using Microsoft.Internal;
using Microsoft.Internal.Collections;

namespace System.ComponentModel.Composition.ReflectionModel
{
	// Token: 0x02000067 RID: 103
	internal class ImportType
	{
		// Token: 0x06000289 RID: 649 RVA: 0x00007E40 File Offset: 0x00006040
		public ImportType(Type type, ImportCardinality cardinality)
		{
			Assumes.NotNull<Type>(type);
			this._type = type;
			this._contractType = type;
			if (cardinality == ImportCardinality.ZeroOrMore)
			{
				this._isAssignableCollectionType = ImportType.IsTypeAssignableCollectionType(type);
				this._contractType = this.CheckForCollection(type);
			}
			this._isOpenGeneric = type.ContainsGenericParameters;
			this._contractType = this.CheckForLazyAndPartCreator(this._contractType);
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600028A RID: 650 RVA: 0x00007EA2 File Offset: 0x000060A2
		public bool IsAssignableCollectionType
		{
			get
			{
				return this._isAssignableCollectionType;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00007EAA File Offset: 0x000060AA
		// (set) Token: 0x0600028C RID: 652 RVA: 0x00007EB2 File Offset: 0x000060B2
		public Type ElementType
		{
			[CompilerGenerated]
			get
			{
				return this.<ElementType>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ElementType>k__BackingField = value;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00007EBB File Offset: 0x000060BB
		public Type ActualType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00007EC3 File Offset: 0x000060C3
		// (set) Token: 0x0600028F RID: 655 RVA: 0x00007ECB File Offset: 0x000060CB
		public bool IsPartCreator
		{
			[CompilerGenerated]
			get
			{
				return this.<IsPartCreator>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<IsPartCreator>k__BackingField = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00007ED4 File Offset: 0x000060D4
		public Type ContractType
		{
			get
			{
				return this._contractType;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00007EDC File Offset: 0x000060DC
		public Func<Export, object> CastExport
		{
			get
			{
				Assumes.IsTrue(!this._isOpenGeneric);
				return this._castSingleValue;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00007EF2 File Offset: 0x000060F2
		// (set) Token: 0x06000293 RID: 659 RVA: 0x00007EFA File Offset: 0x000060FA
		public Type MetadataViewType
		{
			[CompilerGenerated]
			get
			{
				return this.<MetadataViewType>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<MetadataViewType>k__BackingField = value;
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00007F03 File Offset: 0x00006103
		private Type CheckForCollection(Type type)
		{
			this.ElementType = CollectionServices.GetEnumerableElementType(type);
			if (this.ElementType != null)
			{
				return this.ElementType;
			}
			return type;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00007F28 File Offset: 0x00006128
		private static bool IsGenericDescendentOf(Type type, Type baseGenericTypeDefinition)
		{
			return !(type == typeof(object)) && !(type == null) && ((type.IsGenericType && type.GetGenericTypeDefinition() == baseGenericTypeDefinition) || ImportType.IsGenericDescendentOf(type.BaseType, baseGenericTypeDefinition));
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00007F76 File Offset: 0x00006176
		public static bool IsDescendentOf(Type type, Type baseType)
		{
			Assumes.NotNull<Type>(type);
			Assumes.NotNull<Type>(baseType);
			if (!baseType.IsGenericTypeDefinition)
			{
				return baseType.IsAssignableFrom(type);
			}
			return ImportType.IsGenericDescendentOf(type, baseType.GetGenericTypeDefinition());
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00007FA0 File Offset: 0x000061A0
		private Type CheckForLazyAndPartCreator(Type type)
		{
			if (type.IsGenericType)
			{
				Type underlyingSystemType = type.GetGenericTypeDefinition().UnderlyingSystemType;
				Type[] genericArguments = type.GetGenericArguments();
				if (underlyingSystemType == ImportType.LazyOfTType)
				{
					if (!this._isOpenGeneric)
					{
						this._castSingleValue = ExportServices.CreateStronglyTypedLazyFactory(genericArguments[0].UnderlyingSystemType, null);
					}
					return genericArguments[0];
				}
				if (underlyingSystemType == ImportType.LazyOfTMType)
				{
					this.MetadataViewType = genericArguments[1];
					if (!this._isOpenGeneric)
					{
						this._castSingleValue = ExportServices.CreateStronglyTypedLazyFactory(genericArguments[0].UnderlyingSystemType, genericArguments[1].UnderlyingSystemType);
					}
					return genericArguments[0];
				}
				if (underlyingSystemType != null && ImportType.IsDescendentOf(underlyingSystemType, ImportType.ExportFactoryOfTType))
				{
					this.IsPartCreator = true;
					if (genericArguments.Length == 1)
					{
						if (!this._isOpenGeneric)
						{
							this._castSingleValue = new ExportFactoryCreator(underlyingSystemType).CreateStronglyTypedExportFactoryFactory(genericArguments[0].UnderlyingSystemType, null);
						}
					}
					else
					{
						if (genericArguments.Length != 2)
						{
							throw ExceptionBuilder.ExportFactory_TooManyGenericParameters(underlyingSystemType.FullName);
						}
						if (!this._isOpenGeneric)
						{
							this._castSingleValue = new ExportFactoryCreator(underlyingSystemType).CreateStronglyTypedExportFactoryFactory(genericArguments[0].UnderlyingSystemType, genericArguments[1].UnderlyingSystemType);
						}
						this.MetadataViewType = genericArguments[1];
					}
					return genericArguments[0];
				}
			}
			return type;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x000080CA File Offset: 0x000062CA
		private static bool IsTypeAssignableCollectionType(Type type)
		{
			return type.IsArray || CollectionServices.IsEnumerableOfT(type);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x000080DF File Offset: 0x000062DF
		// Note: this type is marked as 'beforefieldinit'.
		static ImportType()
		{
		}

		// Token: 0x04000115 RID: 277
		private static readonly Type LazyOfTType = typeof(Lazy<>);

		// Token: 0x04000116 RID: 278
		private static readonly Type LazyOfTMType = typeof(Lazy<, >);

		// Token: 0x04000117 RID: 279
		private static readonly Type ExportFactoryOfTType = typeof(ExportFactory<>);

		// Token: 0x04000118 RID: 280
		private static readonly Type ExportFactoryOfTMType = typeof(ExportFactory<, >);

		// Token: 0x04000119 RID: 281
		private readonly Type _type;

		// Token: 0x0400011A RID: 282
		private readonly bool _isAssignableCollectionType;

		// Token: 0x0400011B RID: 283
		private readonly Type _contractType;

		// Token: 0x0400011C RID: 284
		private Func<Export, object> _castSingleValue;

		// Token: 0x0400011D RID: 285
		private bool _isOpenGeneric;

		// Token: 0x0400011E RID: 286
		[CompilerGenerated]
		private Type <ElementType>k__BackingField;

		// Token: 0x0400011F RID: 287
		[CompilerGenerated]
		private bool <IsPartCreator>k__BackingField;

		// Token: 0x04000120 RID: 288
		[CompilerGenerated]
		private Type <MetadataViewType>k__BackingField;
	}
}
