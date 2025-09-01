using System;
using System.Collections.Generic;

namespace IKVM.Reflection
{
	// Token: 0x02000025 RID: 37
	public abstract class EventInfo : MemberInfo
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x00005501 File Offset: 0x00003701
		internal EventInfo()
		{
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00005509 File Offset: 0x00003709
		public sealed override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Event;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000FA RID: 250
		public abstract EventAttributes Attributes { get; }

		// Token: 0x060000FB RID: 251
		public abstract MethodInfo GetAddMethod(bool nonPublic);

		// Token: 0x060000FC RID: 252
		public abstract MethodInfo GetRaiseMethod(bool nonPublic);

		// Token: 0x060000FD RID: 253
		public abstract MethodInfo GetRemoveMethod(bool nonPublic);

		// Token: 0x060000FE RID: 254
		public abstract MethodInfo[] GetOtherMethods(bool nonPublic);

		// Token: 0x060000FF RID: 255
		public abstract MethodInfo[] __GetMethods();

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000100 RID: 256
		public abstract Type EventHandlerType { get; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000101 RID: 257
		internal abstract bool IsPublic { get; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000102 RID: 258
		internal abstract bool IsNonPrivate { get; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000103 RID: 259
		internal abstract bool IsStatic { get; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000104 RID: 260 RVA: 0x0000550C File Offset: 0x0000370C
		public bool IsSpecialName
		{
			get
			{
				return (this.Attributes & EventAttributes.SpecialName) > EventAttributes.None;
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000551D File Offset: 0x0000371D
		public MethodInfo GetAddMethod()
		{
			return this.GetAddMethod(false);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00005526 File Offset: 0x00003726
		public MethodInfo GetRaiseMethod()
		{
			return this.GetRaiseMethod(false);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000552F File Offset: 0x0000372F
		public MethodInfo GetRemoveMethod()
		{
			return this.GetRemoveMethod(false);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00005538 File Offset: 0x00003738
		public MethodInfo[] GetOtherMethods()
		{
			return this.GetOtherMethods(false);
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00005541 File Offset: 0x00003741
		public MethodInfo AddMethod
		{
			get
			{
				return this.GetAddMethod(true);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600010A RID: 266 RVA: 0x0000554A File Offset: 0x0000374A
		public MethodInfo RaiseMethod
		{
			get
			{
				return this.GetRaiseMethod(true);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00005553 File Offset: 0x00003753
		public MethodInfo RemoveMethod
		{
			get
			{
				return this.GetRemoveMethod(true);
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000555C File Offset: 0x0000375C
		internal virtual EventInfo BindTypeParameters(Type type)
		{
			return new GenericEventInfo(this.DeclaringType.BindTypeParameters(type), this);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005570 File Offset: 0x00003770
		public override string ToString()
		{
			return this.DeclaringType.ToString() + " " + this.Name;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000558D File Offset: 0x0000378D
		internal sealed override bool BindingFlagsMatch(BindingFlags flags)
		{
			return MemberInfo.BindingFlagsMatch(this.IsPublic, flags, BindingFlags.Public, BindingFlags.NonPublic) && MemberInfo.BindingFlagsMatch(this.IsStatic, flags, BindingFlags.Static, BindingFlags.Instance);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000055B1 File Offset: 0x000037B1
		internal sealed override bool BindingFlagsMatchInherited(BindingFlags flags)
		{
			return this.IsNonPrivate && MemberInfo.BindingFlagsMatch(this.IsPublic, flags, BindingFlags.Public, BindingFlags.NonPublic) && MemberInfo.BindingFlagsMatch(this.IsStatic, flags, BindingFlags.Static | BindingFlags.FlattenHierarchy, BindingFlags.Instance);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000055DE File Offset: 0x000037DE
		internal sealed override MemberInfo SetReflectedType(Type type)
		{
			return new EventInfoWithReflectedType(type, this);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000055E7 File Offset: 0x000037E7
		internal sealed override List<CustomAttributeData> GetPseudoCustomAttributes(Type attributeType)
		{
			return null;
		}
	}
}
