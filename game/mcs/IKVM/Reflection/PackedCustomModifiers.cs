using System;

namespace IKVM.Reflection
{
	// Token: 0x0200003F RID: 63
	internal struct PackedCustomModifiers
	{
		// Token: 0x0600028C RID: 652 RVA: 0x000098FF File Offset: 0x00007AFF
		private PackedCustomModifiers(CustomModifiers[] customModifiers)
		{
			this.customModifiers = customModifiers;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x00009908 File Offset: 0x00007B08
		public override int GetHashCode()
		{
			return Util.GetHashCode(this.customModifiers);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00009918 File Offset: 0x00007B18
		public override bool Equals(object obj)
		{
			PackedCustomModifiers? packedCustomModifiers = obj as PackedCustomModifiers?;
			return packedCustomModifiers != null && this.Equals(packedCustomModifiers.Value);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00009949 File Offset: 0x00007B49
		internal bool Equals(PackedCustomModifiers other)
		{
			return Util.ArrayEquals(this.customModifiers, other.customModifiers);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000995C File Offset: 0x00007B5C
		internal CustomModifiers GetReturnTypeCustomModifiers()
		{
			if (this.customModifiers == null)
			{
				return default(CustomModifiers);
			}
			return this.customModifiers[0];
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00009988 File Offset: 0x00007B88
		internal CustomModifiers GetParameterCustomModifiers(int index)
		{
			if (this.customModifiers == null)
			{
				return default(CustomModifiers);
			}
			return this.customModifiers[index + 1];
		}

		// Token: 0x06000292 RID: 658 RVA: 0x000099B8 File Offset: 0x00007BB8
		internal PackedCustomModifiers Bind(IGenericBinder binder)
		{
			if (this.customModifiers == null)
			{
				return default(PackedCustomModifiers);
			}
			CustomModifiers[] array = new CustomModifiers[this.customModifiers.Length];
			for (int i = 0; i < this.customModifiers.Length; i++)
			{
				array[i] = this.customModifiers[i].Bind(binder);
			}
			return new PackedCustomModifiers(array);
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00009A18 File Offset: 0x00007C18
		internal bool ContainsMissingType
		{
			get
			{
				if (this.customModifiers != null)
				{
					for (int i = 0; i < this.customModifiers.Length; i++)
					{
						if (this.customModifiers[i].ContainsMissingType)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00009A58 File Offset: 0x00007C58
		internal static PackedCustomModifiers CreateFromExternal(Type[] returnOptional, Type[] returnRequired, Type[][] parameterOptional, Type[][] parameterRequired, int parameterCount)
		{
			CustomModifiers[] array = null;
			PackedCustomModifiers.Pack(ref array, 0, CustomModifiers.FromReqOpt(returnRequired, returnOptional), parameterCount + 1);
			for (int i = 0; i < parameterCount; i++)
			{
				PackedCustomModifiers.Pack(ref array, i + 1, CustomModifiers.FromReqOpt(Util.NullSafeElementAt<Type[]>(parameterRequired, i), Util.NullSafeElementAt<Type[]>(parameterOptional, i)), parameterCount + 1);
			}
			return new PackedCustomModifiers(array);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00009AB0 File Offset: 0x00007CB0
		internal static PackedCustomModifiers CreateFromExternal(CustomModifiers returnTypeCustomModifiers, CustomModifiers[] parameterTypeCustomModifiers, int parameterCount)
		{
			CustomModifiers[] array = null;
			PackedCustomModifiers.Pack(ref array, 0, returnTypeCustomModifiers, parameterCount + 1);
			if (parameterTypeCustomModifiers != null)
			{
				for (int i = 0; i < parameterCount; i++)
				{
					PackedCustomModifiers.Pack(ref array, i + 1, parameterTypeCustomModifiers[i], parameterCount + 1);
				}
			}
			return new PackedCustomModifiers(array);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00009AF4 File Offset: 0x00007CF4
		internal static PackedCustomModifiers Wrap(CustomModifiers[] modifiers)
		{
			return new PackedCustomModifiers(modifiers);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00009AFC File Offset: 0x00007CFC
		internal static void Pack(ref CustomModifiers[] array, int index, CustomModifiers mods, int count)
		{
			if (!mods.IsEmpty)
			{
				if (array == null)
				{
					array = new CustomModifiers[count];
				}
				array[index] = mods;
			}
		}

		// Token: 0x0400016B RID: 363
		private readonly CustomModifiers[] customModifiers;
	}
}
