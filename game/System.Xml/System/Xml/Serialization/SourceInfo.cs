using System;
using System.Collections;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace System.Xml.Serialization
{
	// Token: 0x020002B8 RID: 696
	internal class SourceInfo
	{
		// Token: 0x06001A60 RID: 6752 RVA: 0x00098893 File Offset: 0x00096A93
		public SourceInfo(string source, string arg, MemberInfo memberInfo, Type type, CodeGenerator ilg)
		{
			this.Source = source;
			this.Arg = (arg ?? source);
			this.MemberInfo = memberInfo;
			this.Type = type;
			this.ILG = ilg;
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x000988C8 File Offset: 0x00096AC8
		public SourceInfo CastTo(TypeDesc td)
		{
			return new SourceInfo(string.Concat(new string[]
			{
				"((",
				td.CSharpName,
				")",
				this.Source,
				")"
			}), this.Arg, this.MemberInfo, td.Type, this.ILG);
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x00098927 File Offset: 0x00096B27
		public void LoadAddress(Type elementType)
		{
			this.InternalLoad(elementType, true);
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x00098931 File Offset: 0x00096B31
		public void Load(Type elementType)
		{
			this.InternalLoad(elementType, false);
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x0009893C File Offset: 0x00096B3C
		private void InternalLoad(Type elementType, bool asAddress = false)
		{
			Match match = SourceInfo.regex.Match(this.Arg);
			if (match.Success)
			{
				object variable = this.ILG.GetVariable(match.Groups["a"].Value);
				Type variableType = this.ILG.GetVariableType(variable);
				object variable2 = this.ILG.GetVariable(match.Groups["ia"].Value);
				if (variableType.IsArray)
				{
					this.ILG.Load(variable);
					this.ILG.Load(variable2);
					Type elementType2 = variableType.GetElementType();
					if (CodeGenerator.IsNullableGenericType(elementType2))
					{
						this.ILG.Ldelema(elementType2);
						this.ConvertNullableValue(elementType2, elementType);
						return;
					}
					if (elementType2.IsValueType)
					{
						this.ILG.Ldelema(elementType2);
						if (!asAddress)
						{
							this.ILG.Ldobj(elementType2);
						}
					}
					else
					{
						this.ILG.Ldelem(elementType2);
					}
					if (elementType != null)
					{
						this.ILG.ConvertValue(elementType2, elementType);
						return;
					}
				}
				else
				{
					this.ILG.Load(variable);
					this.ILG.Load(variable2);
					MethodInfo methodInfo = variableType.GetMethod("get_Item", CodeGenerator.InstanceBindingFlags, null, new Type[]
					{
						typeof(int)
					}, null);
					if (methodInfo == null && typeof(IList).IsAssignableFrom(variableType))
					{
						methodInfo = SourceInfo.iListGetItemMethod.Value;
					}
					this.ILG.Call(methodInfo);
					Type returnType = methodInfo.ReturnType;
					if (CodeGenerator.IsNullableGenericType(returnType))
					{
						LocalBuilder tempLocal = this.ILG.GetTempLocal(returnType);
						this.ILG.Stloc(tempLocal);
						this.ILG.Ldloca(tempLocal);
						this.ConvertNullableValue(returnType, elementType);
						return;
					}
					if (elementType != null && !returnType.IsAssignableFrom(elementType) && !elementType.IsAssignableFrom(returnType))
					{
						throw new CodeGeneratorConversionException(returnType, elementType, asAddress, "IsNotAssignableFrom");
					}
					this.Convert(returnType, elementType, asAddress);
					return;
				}
			}
			else
			{
				if (this.Source == "null")
				{
					this.ILG.Load(null);
					return;
				}
				Type type;
				if (this.Arg.StartsWith("o.@", StringComparison.Ordinal) || this.MemberInfo != null)
				{
					object variable3 = this.ILG.GetVariable(this.Arg.StartsWith("o.@", StringComparison.Ordinal) ? "o" : this.Arg);
					type = this.ILG.GetVariableType(variable3);
					if (type.IsValueType)
					{
						this.ILG.LoadAddress(variable3);
					}
					else
					{
						this.ILG.Load(variable3);
					}
				}
				else
				{
					object variable3 = this.ILG.GetVariable(this.Arg);
					type = this.ILG.GetVariableType(variable3);
					if (CodeGenerator.IsNullableGenericType(type) && type.GetGenericArguments()[0] == elementType)
					{
						this.ILG.LoadAddress(variable3);
						this.ConvertNullableValue(type, elementType);
					}
					else if (asAddress)
					{
						this.ILG.LoadAddress(variable3);
					}
					else
					{
						this.ILG.Load(variable3);
					}
				}
				if (this.MemberInfo != null)
				{
					Type type2 = (this.MemberInfo is FieldInfo) ? ((FieldInfo)this.MemberInfo).FieldType : ((PropertyInfo)this.MemberInfo).PropertyType;
					if (CodeGenerator.IsNullableGenericType(type2))
					{
						this.ILG.LoadMemberAddress(this.MemberInfo);
						this.ConvertNullableValue(type2, elementType);
						return;
					}
					this.ILG.LoadMember(this.MemberInfo);
					this.Convert(type2, elementType, asAddress);
					return;
				}
				else
				{
					match = SourceInfo.regex2.Match(this.Source);
					if (match.Success)
					{
						if (asAddress)
						{
							this.ILG.ConvertAddress(type, this.Type);
						}
						else
						{
							this.ILG.ConvertValue(type, this.Type);
						}
						type = this.Type;
					}
					this.Convert(type, elementType, asAddress);
				}
			}
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x00098D2F File Offset: 0x00096F2F
		private void Convert(Type sourceType, Type targetType, bool asAddress)
		{
			if (targetType != null)
			{
				if (asAddress)
				{
					this.ILG.ConvertAddress(sourceType, targetType);
					return;
				}
				this.ILG.ConvertValue(sourceType, targetType);
			}
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x00098D58 File Offset: 0x00096F58
		private void ConvertNullableValue(Type nullableType, Type targetType)
		{
			if (targetType != nullableType)
			{
				MethodInfo method = nullableType.GetMethod("get_Value", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
				this.ILG.Call(method);
				if (targetType != null)
				{
					this.ILG.ConvertValue(method.ReturnType, targetType);
				}
			}
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x00098DAD File Offset: 0x00096FAD
		public static implicit operator string(SourceInfo source)
		{
			return source.Source;
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x00098DB5 File Offset: 0x00096FB5
		public static bool operator !=(SourceInfo a, SourceInfo b)
		{
			if (a != null)
			{
				return !a.Equals(b);
			}
			return b != null;
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x00098DC9 File Offset: 0x00096FC9
		public static bool operator ==(SourceInfo a, SourceInfo b)
		{
			if (a != null)
			{
				return a.Equals(b);
			}
			return b == null;
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x00098DDC File Offset: 0x00096FDC
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return this.Source == null;
			}
			SourceInfo sourceInfo = obj as SourceInfo;
			return sourceInfo != null && this.Source == sourceInfo.Source;
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x00098E19 File Offset: 0x00097019
		public override int GetHashCode()
		{
			if (this.Source != null)
			{
				return this.Source.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x00098E30 File Offset: 0x00097030
		// Note: this type is marked as 'beforefieldinit'.
		static SourceInfo()
		{
		}

		// Token: 0x04001966 RID: 6502
		private static Regex regex = new Regex("([(][(](?<t>[^)]+)[)])?(?<a>[^[]+)[[](?<ia>.+)[]][)]?");

		// Token: 0x04001967 RID: 6503
		private static Regex regex2 = new Regex("[(][(](?<cast>[^)]+)[)](?<arg>[^)]+)[)]");

		// Token: 0x04001968 RID: 6504
		private static readonly Lazy<MethodInfo> iListGetItemMethod = new Lazy<MethodInfo>(() => typeof(IList).GetMethod("get_Item", CodeGenerator.InstanceBindingFlags, null, new Type[]
		{
			typeof(int)
		}, null));

		// Token: 0x04001969 RID: 6505
		public string Source;

		// Token: 0x0400196A RID: 6506
		public readonly string Arg;

		// Token: 0x0400196B RID: 6507
		public readonly MemberInfo MemberInfo;

		// Token: 0x0400196C RID: 6508
		public readonly Type Type;

		// Token: 0x0400196D RID: 6509
		public readonly CodeGenerator ILG;

		// Token: 0x020002B9 RID: 697
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06001A6D RID: 6765 RVA: 0x00098E6A File Offset: 0x0009706A
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06001A6E RID: 6766 RVA: 0x0000216B File Offset: 0x0000036B
			public <>c()
			{
			}

			// Token: 0x06001A6F RID: 6767 RVA: 0x00098E76 File Offset: 0x00097076
			internal MethodInfo <.cctor>b__20_0()
			{
				return typeof(IList).GetMethod("get_Item", CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					typeof(int)
				}, null);
			}

			// Token: 0x0400196E RID: 6510
			public static readonly SourceInfo.<>c <>9 = new SourceInfo.<>c();
		}
	}
}
