using System;
using IKVM.Reflection.Emit;

namespace IKVM.Reflection
{
	// Token: 0x0200005D RID: 93
	internal abstract class ElementHolderType : TypeInfo
	{
		// Token: 0x06000555 RID: 1365 RVA: 0x00010882 File Offset: 0x0000EA82
		protected ElementHolderType(Type elementType, CustomModifiers mods, byte sigElementType) : base(sigElementType)
		{
			this.elementType = elementType;
			this.mods = mods;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0001089C File Offset: 0x0000EA9C
		protected bool EqualsHelper(ElementHolderType other)
		{
			return other != null && other.elementType.Equals(this.elementType) && other.mods.Equals(this.mods);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x000108DB File Offset: 0x0000EADB
		public override CustomModifiers __GetCustomModifiers()
		{
			return this.mods;
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x000108E3 File Offset: 0x0000EAE3
		public sealed override string Name
		{
			get
			{
				return this.elementType.Name + this.GetSuffix();
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x000108FB File Offset: 0x0000EAFB
		public sealed override string Namespace
		{
			get
			{
				return this.elementType.Namespace;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x00010908 File Offset: 0x0000EB08
		public sealed override string FullName
		{
			get
			{
				return this.elementType.FullName + this.GetSuffix();
			}
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00010920 File Offset: 0x0000EB20
		public sealed override string ToString()
		{
			return this.elementType.ToString() + this.GetSuffix();
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00010938 File Offset: 0x0000EB38
		public sealed override Type GetElementType()
		{
			return this.elementType;
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x00010940 File Offset: 0x0000EB40
		public sealed override Module Module
		{
			get
			{
				return this.elementType.Module;
			}
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x0001094D File Offset: 0x0000EB4D
		internal sealed override int GetModuleBuilderToken()
		{
			if (this.token == 0)
			{
				this.token = ((ModuleBuilder)this.elementType.Module).ImportType(this);
			}
			return this.token;
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x0001097C File Offset: 0x0000EB7C
		public sealed override bool ContainsGenericParameters
		{
			get
			{
				Type type = this.elementType;
				while (type.HasElementType)
				{
					type = type.GetElementType();
				}
				return type.ContainsGenericParameters;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x000109A8 File Offset: 0x0000EBA8
		protected sealed override bool ContainsMissingTypeImpl
		{
			get
			{
				Type type = this.elementType;
				while (type.HasElementType)
				{
					type = type.GetElementType();
				}
				return type.__ContainsMissingType || this.mods.ContainsMissingType;
			}
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x000109E8 File Offset: 0x0000EBE8
		internal sealed override Type BindTypeParameters(IGenericBinder binder)
		{
			Type type = this.elementType.BindTypeParameters(binder);
			CustomModifiers customModifiers = this.mods.Bind(binder);
			if (type == this.elementType && customModifiers.Equals(this.mods))
			{
				return this;
			}
			return this.Wrap(type, customModifiers);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00010A34 File Offset: 0x0000EC34
		internal override void CheckBaked()
		{
			this.elementType.CheckBaked();
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x00010A41 File Offset: 0x0000EC41
		internal sealed override Universe Universe
		{
			get
			{
				return this.elementType.Universe;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x00010A4E File Offset: 0x0000EC4E
		internal sealed override bool IsBaked
		{
			get
			{
				return this.elementType.IsBaked;
			}
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x000022F4 File Offset: 0x000004F4
		internal sealed override int GetCurrentToken()
		{
			return 0;
		}

		// Token: 0x06000566 RID: 1382
		internal abstract string GetSuffix();

		// Token: 0x06000567 RID: 1383
		protected abstract Type Wrap(Type type, CustomModifiers mods);

		// Token: 0x040001FF RID: 511
		protected readonly Type elementType;

		// Token: 0x04000200 RID: 512
		private int token;

		// Token: 0x04000201 RID: 513
		private readonly CustomModifiers mods;
	}
}
