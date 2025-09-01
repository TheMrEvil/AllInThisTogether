using System;

namespace IKVM.Reflection
{
	// Token: 0x0200000C RID: 12
	internal sealed class ConstructorInfoWithReflectedType : ConstructorInfo
	{
		// Token: 0x0600009C RID: 156 RVA: 0x000033E2 File Offset: 0x000015E2
		internal ConstructorInfoWithReflectedType(Type reflectedType, ConstructorInfo ctor)
		{
			this.reflectedType = reflectedType;
			this.ctor = ctor;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000033F8 File Offset: 0x000015F8
		public override bool Equals(object obj)
		{
			ConstructorInfoWithReflectedType constructorInfoWithReflectedType = obj as ConstructorInfoWithReflectedType;
			return constructorInfoWithReflectedType != null && constructorInfoWithReflectedType.reflectedType == this.reflectedType && constructorInfoWithReflectedType.ctor == this.ctor;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000343B File Offset: 0x0000163B
		public override int GetHashCode()
		{
			return this.reflectedType.GetHashCode() ^ this.ctor.GetHashCode();
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00003454 File Offset: 0x00001654
		public override Type ReflectedType
		{
			get
			{
				return this.reflectedType;
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000345C File Offset: 0x0000165C
		internal override MethodInfo GetMethodInfo()
		{
			return this.ctor.GetMethodInfo();
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003469 File Offset: 0x00001669
		internal override MethodInfo GetMethodOnTypeDefinition()
		{
			return this.ctor.GetMethodOnTypeDefinition();
		}

		// Token: 0x04000031 RID: 49
		private readonly Type reflectedType;

		// Token: 0x04000032 RID: 50
		private readonly ConstructorInfo ctor;
	}
}
