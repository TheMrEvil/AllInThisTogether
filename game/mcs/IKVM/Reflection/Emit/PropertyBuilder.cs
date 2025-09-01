using System;
using System.Collections.Generic;
using IKVM.Reflection.Metadata;

namespace IKVM.Reflection.Emit
{
	// Token: 0x020000F0 RID: 240
	public sealed class PropertyBuilder : PropertyInfo
	{
		// Token: 0x06000BAA RID: 2986 RVA: 0x0002A948 File Offset: 0x00028B48
		internal PropertyBuilder(TypeBuilder typeBuilder, string name, PropertyAttributes attributes, PropertySignature sig, bool patchCallingConvention)
		{
			this.typeBuilder = typeBuilder;
			this.name = name;
			this.attributes = attributes;
			this.sig = sig;
			this.patchCallingConvention = patchCallingConvention;
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x0002A980 File Offset: 0x00028B80
		internal override PropertySignature PropertySignature
		{
			get
			{
				return this.sig;
			}
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0002A988 File Offset: 0x00028B88
		public void SetGetMethod(MethodBuilder mdBuilder)
		{
			this.getter = mdBuilder;
			PropertyBuilder.Accessor item;
			item.Semantics = 2;
			item.Method = mdBuilder;
			this.accessors.Add(item);
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0002A9B8 File Offset: 0x00028BB8
		public void SetSetMethod(MethodBuilder mdBuilder)
		{
			this.setter = mdBuilder;
			PropertyBuilder.Accessor item;
			item.Semantics = 1;
			item.Method = mdBuilder;
			this.accessors.Add(item);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0002A9E8 File Offset: 0x00028BE8
		public void AddOtherMethod(MethodBuilder mdBuilder)
		{
			PropertyBuilder.Accessor item;
			item.Semantics = 4;
			item.Method = mdBuilder;
			this.accessors.Add(item);
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0002AA11 File Offset: 0x00028C11
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0002AA20 File Offset: 0x00028C20
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder.KnownCA == KnownCA.SpecialNameAttribute)
			{
				this.attributes |= PropertyAttributes.SpecialName;
				return;
			}
			if (this.lazyPseudoToken == 0)
			{
				this.lazyPseudoToken = this.typeBuilder.ModuleBuilder.AllocPseudoToken();
			}
			this.typeBuilder.ModuleBuilder.SetCustomAttribute(this.lazyPseudoToken, customBuilder);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0002AA7F File Offset: 0x00028C7F
		public override object GetRawConstantValue()
		{
			if (this.lazyPseudoToken != 0)
			{
				return this.typeBuilder.ModuleBuilder.Constant.GetRawConstantValue(this.typeBuilder.ModuleBuilder, this.lazyPseudoToken);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x0002AAB5 File Offset: 0x00028CB5
		public override PropertyAttributes Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000BB3 RID: 2995 RVA: 0x0002AABD File Offset: 0x00028CBD
		public override bool CanRead
		{
			get
			{
				return this.getter != null;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x0002AACB File Offset: 0x00028CCB
		public override bool CanWrite
		{
			get
			{
				return this.setter != null;
			}
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0002AAD9 File Offset: 0x00028CD9
		public override MethodInfo GetGetMethod(bool nonPublic)
		{
			if (!nonPublic && (!(this.getter != null) || !this.getter.IsPublic))
			{
				return null;
			}
			return this.getter;
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0002AB01 File Offset: 0x00028D01
		public override MethodInfo GetSetMethod(bool nonPublic)
		{
			if (!nonPublic && (!(this.setter != null) || !this.setter.IsPublic))
			{
				return null;
			}
			return this.setter;
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0002AB2C File Offset: 0x00028D2C
		public override MethodInfo[] GetAccessors(bool nonPublic)
		{
			List<MethodInfo> list = new List<MethodInfo>();
			foreach (PropertyBuilder.Accessor accessor in this.accessors)
			{
				PropertyBuilder.AddAccessor(list, nonPublic, accessor.Method);
			}
			return list.ToArray();
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0002AB94 File Offset: 0x00028D94
		private static void AddAccessor(List<MethodInfo> list, bool nonPublic, MethodInfo method)
		{
			if (method != null && (nonPublic || method.IsPublic))
			{
				list.Add(method);
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x0002ABB1 File Offset: 0x00028DB1
		public override Type DeclaringType
		{
			get
			{
				return this.typeBuilder;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x0002ABB9 File Offset: 0x00028DB9
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x0002ABC1 File Offset: 0x00028DC1
		public override Module Module
		{
			get
			{
				return this.typeBuilder.Module;
			}
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0002ABD0 File Offset: 0x00028DD0
		public void SetConstant(object defaultValue)
		{
			if (this.lazyPseudoToken == 0)
			{
				this.lazyPseudoToken = this.typeBuilder.ModuleBuilder.AllocPseudoToken();
			}
			this.attributes |= PropertyAttributes.HasDefault;
			this.typeBuilder.ModuleBuilder.AddConstant(this.lazyPseudoToken, defaultValue);
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0002AC24 File Offset: 0x00028E24
		internal void Bake()
		{
			if (this.patchCallingConvention)
			{
				this.sig.HasThis = !this.IsStatic;
			}
			PropertyTable.Record newRecord = default(PropertyTable.Record);
			newRecord.Flags = (short)this.attributes;
			newRecord.Name = this.typeBuilder.ModuleBuilder.Strings.Add(this.name);
			newRecord.Type = this.typeBuilder.ModuleBuilder.GetSignatureBlobIndex(this.sig);
			int num = 385875968 | this.typeBuilder.ModuleBuilder.Property.AddRecord(newRecord);
			if (this.lazyPseudoToken == 0)
			{
				this.lazyPseudoToken = num;
			}
			else
			{
				this.typeBuilder.ModuleBuilder.RegisterTokenFixup(this.lazyPseudoToken, num);
			}
			foreach (PropertyBuilder.Accessor accessor in this.accessors)
			{
				this.AddMethodSemantics(accessor.Semantics, accessor.Method.MetadataToken, num);
			}
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0002AD40 File Offset: 0x00028F40
		private void AddMethodSemantics(short semantics, int methodToken, int propertyToken)
		{
			MethodSemanticsTable.Record newRecord = default(MethodSemanticsTable.Record);
			newRecord.Semantics = semantics;
			newRecord.Method = methodToken;
			newRecord.Association = propertyToken;
			this.typeBuilder.ModuleBuilder.MethodSemantics.AddRecord(newRecord);
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000BBF RID: 3007 RVA: 0x0002AD84 File Offset: 0x00028F84
		internal override bool IsPublic
		{
			get
			{
				using (List<PropertyBuilder.Accessor>.Enumerator enumerator = this.accessors.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Method.IsPublic)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x0002ADE4 File Offset: 0x00028FE4
		internal override bool IsNonPrivate
		{
			get
			{
				using (List<PropertyBuilder.Accessor>.Enumerator enumerator = this.accessors.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if ((enumerator.Current.Method.Attributes & MethodAttributes.MemberAccessMask) > MethodAttributes.Private)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x0002AE48 File Offset: 0x00029048
		internal override bool IsStatic
		{
			get
			{
				using (List<PropertyBuilder.Accessor>.Enumerator enumerator = this.accessors.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Method.IsStatic)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x0002AEA8 File Offset: 0x000290A8
		internal override bool IsBaked
		{
			get
			{
				return this.typeBuilder.IsBaked;
			}
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0002AEB5 File Offset: 0x000290B5
		internal override int GetCurrentToken()
		{
			if (this.typeBuilder.ModuleBuilder.IsSaved && ModuleBuilder.IsPseudoToken(this.lazyPseudoToken))
			{
				return this.typeBuilder.ModuleBuilder.ResolvePseudoToken(this.lazyPseudoToken);
			}
			return this.lazyPseudoToken;
		}

		// Token: 0x040005E3 RID: 1507
		private readonly TypeBuilder typeBuilder;

		// Token: 0x040005E4 RID: 1508
		private readonly string name;

		// Token: 0x040005E5 RID: 1509
		private PropertyAttributes attributes;

		// Token: 0x040005E6 RID: 1510
		private PropertySignature sig;

		// Token: 0x040005E7 RID: 1511
		private MethodBuilder getter;

		// Token: 0x040005E8 RID: 1512
		private MethodBuilder setter;

		// Token: 0x040005E9 RID: 1513
		private readonly List<PropertyBuilder.Accessor> accessors = new List<PropertyBuilder.Accessor>();

		// Token: 0x040005EA RID: 1514
		private int lazyPseudoToken;

		// Token: 0x040005EB RID: 1515
		private bool patchCallingConvention;

		// Token: 0x02000372 RID: 882
		private struct Accessor
		{
			// Token: 0x04000F32 RID: 3890
			internal short Semantics;

			// Token: 0x04000F33 RID: 3891
			internal MethodBuilder Method;
		}
	}
}
