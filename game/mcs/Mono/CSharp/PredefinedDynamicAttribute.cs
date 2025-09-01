using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x0200012A RID: 298
	public class PredefinedDynamicAttribute : PredefinedAttribute
	{
		// Token: 0x06000E8B RID: 3723 RVA: 0x0003742F File Offset: 0x0003562F
		public PredefinedDynamicAttribute(ModuleContainer module, string ns, string name) : base(module, ns, name)
		{
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x000376CC File Offset: 0x000358CC
		public void EmitAttribute(FieldBuilder builder, TypeSpec type, Location loc)
		{
			if (this.ResolveTransformationCtor(loc))
			{
				CustomAttributeBuilder customAttribute = new CustomAttributeBuilder((ConstructorInfo)this.tctor.GetMetaInfo(), new object[]
				{
					PredefinedDynamicAttribute.GetTransformationFlags(type)
				});
				builder.SetCustomAttribute(customAttribute);
			}
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x00037710 File Offset: 0x00035910
		public void EmitAttribute(ParameterBuilder builder, TypeSpec type, Location loc)
		{
			if (this.ResolveTransformationCtor(loc))
			{
				CustomAttributeBuilder customAttribute = new CustomAttributeBuilder((ConstructorInfo)this.tctor.GetMetaInfo(), new object[]
				{
					PredefinedDynamicAttribute.GetTransformationFlags(type)
				});
				builder.SetCustomAttribute(customAttribute);
			}
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x00037754 File Offset: 0x00035954
		public void EmitAttribute(PropertyBuilder builder, TypeSpec type, Location loc)
		{
			if (this.ResolveTransformationCtor(loc))
			{
				CustomAttributeBuilder customAttribute = new CustomAttributeBuilder((ConstructorInfo)this.tctor.GetMetaInfo(), new object[]
				{
					PredefinedDynamicAttribute.GetTransformationFlags(type)
				});
				builder.SetCustomAttribute(customAttribute);
			}
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x00037798 File Offset: 0x00035998
		public void EmitAttribute(TypeBuilder builder, TypeSpec type, Location loc)
		{
			if (this.ResolveTransformationCtor(loc))
			{
				CustomAttributeBuilder customAttribute = new CustomAttributeBuilder((ConstructorInfo)this.tctor.GetMetaInfo(), new object[]
				{
					PredefinedDynamicAttribute.GetTransformationFlags(type)
				});
				builder.SetCustomAttribute(customAttribute);
			}
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x000377DC File Offset: 0x000359DC
		private static bool[] GetTransformationFlags(TypeSpec t)
		{
			ArrayContainer arrayContainer = t as ArrayContainer;
			if (arrayContainer != null)
			{
				bool[] transformationFlags = PredefinedDynamicAttribute.GetTransformationFlags(arrayContainer.Element);
				if (transformationFlags == null)
				{
					return new bool[2];
				}
				bool[] array = new bool[transformationFlags.Length + 1];
				array[0] = false;
				Array.Copy(transformationFlags, 0, array, 1, transformationFlags.Length);
				return array;
			}
			else
			{
				if (t == null)
				{
					return null;
				}
				if (t.IsGeneric)
				{
					List<bool> list = null;
					TypeSpec[] typeArguments = t.TypeArguments;
					for (int i = 0; i < typeArguments.Length; i++)
					{
						bool[] transformationFlags = PredefinedDynamicAttribute.GetTransformationFlags(typeArguments[i]);
						if (transformationFlags != null)
						{
							if (list == null)
							{
								list = new List<bool>();
								for (int j = 0; j <= i; j++)
								{
									list.Add(false);
								}
							}
							list.AddRange(transformationFlags);
						}
						else if (list != null)
						{
							list.Add(false);
						}
					}
					if (list != null)
					{
						return list.ToArray();
					}
				}
				if (t.BuiltinType == BuiltinTypeSpec.Type.Dynamic)
				{
					return new bool[]
					{
						true
					};
				}
				return null;
			}
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x000378B2 File Offset: 0x00035AB2
		private bool ResolveTransformationCtor(Location loc)
		{
			if (this.tctor != null)
			{
				return true;
			}
			this.tctor = this.module.PredefinedMembers.DynamicAttributeCtor.Resolve(loc);
			return this.tctor != null;
		}

		// Token: 0x040006CA RID: 1738
		private MethodSpec tctor;
	}
}
