using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using QFSW.QC.Utilities;

namespace QFSW.QC
{
	// Token: 0x0200000D RID: 13
	public class CommandData
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000022FA File Offset: 0x000004FA
		public bool IsGeneric
		{
			get
			{
				return this.GenericParamTypes.Length != 0;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002306 File Offset: 0x00000506
		public bool IsStatic
		{
			get
			{
				return this.MethodData.IsStatic;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002313 File Offset: 0x00000513
		public bool HasDescription
		{
			get
			{
				return !string.IsNullOrWhiteSpace(this.CommandDescription);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002323 File Offset: 0x00000523
		public int ParamCount
		{
			get
			{
				return this.ParamTypes.Length - this._defaultParameters.Length;
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002338 File Offset: 0x00000538
		public Type[] MakeGenericArguments(params Type[] genericTypeArguments)
		{
			if (genericTypeArguments.Length != this.GenericParamTypes.Length)
			{
				throw new ArgumentException("Incorrect number of generic substitution types were supplied.");
			}
			Dictionary<string, Type> dictionary = new Dictionary<string, Type>();
			for (int i = 0; i < genericTypeArguments.Length; i++)
			{
				dictionary.Add(this.GenericParamTypes[i].Name, genericTypeArguments[i]);
			}
			Type[] array = new Type[this.ParamTypes.Length];
			for (int j = 0; j < array.Length; j++)
			{
				if (this.ParamTypes[j].ContainsGenericParameters)
				{
					Type type = this.ConstructGenericType(this.ParamTypes[j], dictionary);
					array[j] = type;
				}
				else
				{
					array[j] = this.ParamTypes[j];
				}
			}
			return array;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023D8 File Offset: 0x000005D8
		private Type ConstructGenericType(Type genericType, Dictionary<string, Type> substitutionTable)
		{
			if (!genericType.ContainsGenericParameters)
			{
				return genericType;
			}
			if (substitutionTable.ContainsKey(genericType.Name))
			{
				return substitutionTable[genericType.Name];
			}
			if (genericType.IsArray)
			{
				return this.ConstructGenericType(genericType.GetElementType(), substitutionTable).MakeArrayType();
			}
			if (genericType.IsGenericType)
			{
				Type genericTypeDefinition = genericType.GetGenericTypeDefinition();
				Type[] genericArguments = genericType.GetGenericArguments();
				for (int i = 0; i < genericArguments.Length; i++)
				{
					genericArguments[i] = this.ConstructGenericType(genericArguments[i], substitutionTable);
				}
				return genericTypeDefinition.MakeGenericType(genericArguments);
			}
			throw new ArgumentException(string.Format("Could not construct the generic type {0}", genericType));
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002470 File Offset: 0x00000670
		public object Invoke(object[] paramData, Type[] genericTypeArguments)
		{
			object[] array = new object[paramData.Length + this._defaultParameters.Length];
			Array.Copy(paramData, 0, array, 0, paramData.Length);
			Array.Copy(this._defaultParameters, 0, array, paramData.Length, this._defaultParameters.Length);
			MethodInfo invokingMethod = this.GetInvokingMethod(genericTypeArguments);
			if (this.IsStatic)
			{
				return invokingMethod.Invoke(null, array);
			}
			IEnumerable<object> invocationTargets = this.GetInvocationTargets(invokingMethod);
			return InvocationTargetFactory.InvokeOnTargets(invokingMethod, invocationTargets, array);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000024DC File Offset: 0x000006DC
		protected virtual IEnumerable<object> GetInvocationTargets(MethodInfo invokingMethod)
		{
			return InvocationTargetFactory.FindTargets(invokingMethod.DeclaringType, this.MonoTarget);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000024F0 File Offset: 0x000006F0
		private MethodInfo GetInvokingMethod(Type[] genericTypeArguments)
		{
			CommandData.<>c__DisplayClass23_0 CS$<>8__locals1 = new CommandData.<>c__DisplayClass23_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.genericTypeArguments = genericTypeArguments;
			if (!this.IsGeneric)
			{
				return this.MethodData;
			}
			CS$<>8__locals1.declaringType = this.MethodData.DeclaringType;
			CS$<>8__locals1.method = this.MethodData;
			if (CS$<>8__locals1.declaringType.IsGenericTypeDefinition)
			{
				int count = CS$<>8__locals1.declaringType.GetGenericArguments().Length;
				Type[] genericTypes = CS$<>8__locals1.genericTypeArguments.Take(count).ToArray<Type>();
				CS$<>8__locals1.genericTypeArguments = CS$<>8__locals1.genericTypeArguments.Skip(count).ToArray<Type>();
				CS$<>8__locals1.declaringType = CS$<>8__locals1.<GetInvokingMethod>g__WrapConstruction|0<Type>(() => CS$<>8__locals1.declaringType.MakeGenericType(genericTypes));
				CS$<>8__locals1.method = CS$<>8__locals1.method.RebaseMethod(CS$<>8__locals1.declaringType);
			}
			if (CS$<>8__locals1.genericTypeArguments.Length != 0)
			{
				return CS$<>8__locals1.<GetInvokingMethod>g__WrapConstruction|0<MethodInfo>(() => CS$<>8__locals1.method.MakeGenericMethod(CS$<>8__locals1.genericTypeArguments));
			}
			return CS$<>8__locals1.method;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002614 File Offset: 0x00000814
		private string BuildPrefix(Type declaringType)
		{
			CommandData.<>c__DisplayClass24_0 CS$<>8__locals1;
			CS$<>8__locals1.prefixes = new List<string>();
			Assembly assembly = declaringType.Assembly;
			while (declaringType != null)
			{
				CommandData.<BuildPrefix>g__AddPrefixes|24_0(declaringType.GetCustomAttributes<CommandPrefixAttribute>(), declaringType.Name, ref CS$<>8__locals1);
				declaringType = declaringType.DeclaringType;
			}
			CommandData.<BuildPrefix>g__AddPrefixes|24_0(assembly.GetCustomAttributes<CommandPrefixAttribute>(), assembly.GetName().Name, ref CS$<>8__locals1);
			return string.Join("", CS$<>8__locals1.prefixes.Reversed<string>());
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002688 File Offset: 0x00000888
		private string BuildGenericSignature(Type[] genericParamTypes)
		{
			if (genericParamTypes.Length == 0)
			{
				return string.Empty;
			}
			IEnumerable<string> values = from x in genericParamTypes
			select x.Name;
			return "<" + string.Join(", ", values) + ">";
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000026E0 File Offset: 0x000008E0
		private string BuildParameterSignature(ParameterInfo[] methodParams, int defaultParameterCount)
		{
			string text = string.Empty;
			for (int i = 0; i < methodParams.Length - defaultParameterCount; i++)
			{
				text = text + ((i == 0) ? string.Empty : " ") + methodParams[i].Name;
			}
			return text;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002724 File Offset: 0x00000924
		private Type[] BuildGenericParamTypes(MethodInfo method, Type declaringType)
		{
			List<Type> list = new List<Type>();
			if (declaringType.IsGenericTypeDefinition)
			{
				list.AddRange(declaringType.GetGenericArguments());
			}
			if (method.IsGenericMethodDefinition)
			{
				list.AddRange(method.GetGenericArguments());
			}
			return list.ToArray();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002765 File Offset: 0x00000965
		public CommandData(MethodInfo methodData, int defaultParameterCount = 0) : this(methodData, methodData.Name, defaultParameterCount)
		{
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002778 File Offset: 0x00000978
		public CommandData(MethodInfo methodData, string commandName, int defaultParameterCount = 0)
		{
			this.CommandName = commandName;
			this.MethodData = methodData;
			if (string.IsNullOrWhiteSpace(commandName))
			{
				this.CommandName = methodData.Name;
			}
			Type declaringType = methodData.DeclaringType;
			string str = this.BuildPrefix(declaringType);
			this.CommandName = str + this.CommandName;
			this.MethodParamData = methodData.GetParameters();
			this.ParamTypes = (from x in this.MethodParamData
			select x.ParameterType).ToArray<Type>();
			this._defaultParameters = new object[defaultParameterCount];
			for (int i = 0; i < defaultParameterCount; i++)
			{
				int num = this.MethodParamData.Length - defaultParameterCount + i;
				this._defaultParameters[i] = this.MethodParamData[num].DefaultValue;
			}
			this.GenericParamTypes = this.BuildGenericParamTypes(methodData, declaringType);
			this.ParameterSignature = this.BuildParameterSignature(this.MethodParamData, defaultParameterCount);
			this.GenericSignature = this.BuildGenericSignature(this.GenericParamTypes);
			this.CommandSignature = ((this.ParamCount > 0) ? (this.CommandName + this.GenericSignature + " " + this.ParameterSignature) : (this.CommandName + this.GenericSignature));
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000028BA File Offset: 0x00000ABA
		public CommandData(MethodInfo methodData, CommandAttribute commandAttribute, int defaultParameterCount = 0) : this(methodData, commandAttribute.Alias, defaultParameterCount)
		{
			this.CommandDescription = commandAttribute.Description;
			this.MonoTarget = commandAttribute.MonoTarget;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000028E2 File Offset: 0x00000AE2
		public CommandData(MethodInfo methodData, CommandAttribute commandAttribute, CommandDescriptionAttribute descriptionAttribute, int defaultParameterCount = 0) : this(methodData, commandAttribute, defaultParameterCount)
		{
			if (descriptionAttribute != null && descriptionAttribute.Valid && string.IsNullOrWhiteSpace(commandAttribute.Description))
			{
				this.CommandDescription = descriptionAttribute.Description;
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002914 File Offset: 0x00000B14
		[CompilerGenerated]
		internal static void <BuildPrefix>g__AddPrefixes|24_0(IEnumerable<CommandPrefixAttribute> prefixAttributes, string defaultName, ref CommandData.<>c__DisplayClass24_0 A_2)
		{
			foreach (CommandPrefixAttribute commandPrefixAttribute in prefixAttributes.Reverse<CommandPrefixAttribute>())
			{
				if (commandPrefixAttribute.Valid)
				{
					string text = commandPrefixAttribute.Prefix;
					if (string.IsNullOrWhiteSpace(text))
					{
						text = defaultName;
					}
					A_2.prefixes.Add(text);
				}
			}
		}

		// Token: 0x04000018 RID: 24
		public readonly string CommandName;

		// Token: 0x04000019 RID: 25
		public readonly string CommandDescription;

		// Token: 0x0400001A RID: 26
		public readonly string CommandSignature;

		// Token: 0x0400001B RID: 27
		public readonly string ParameterSignature;

		// Token: 0x0400001C RID: 28
		public readonly string GenericSignature;

		// Token: 0x0400001D RID: 29
		public readonly ParameterInfo[] MethodParamData;

		// Token: 0x0400001E RID: 30
		public readonly Type[] ParamTypes;

		// Token: 0x0400001F RID: 31
		public readonly Type[] GenericParamTypes;

		// Token: 0x04000020 RID: 32
		public readonly MethodInfo MethodData;

		// Token: 0x04000021 RID: 33
		public readonly MonoTargetType MonoTarget;

		// Token: 0x04000022 RID: 34
		private readonly object[] _defaultParameters;

		// Token: 0x02000081 RID: 129
		[CompilerGenerated]
		private sealed class <>c__DisplayClass23_0
		{
			// Token: 0x06000290 RID: 656 RVA: 0x0000AA53 File Offset: 0x00008C53
			public <>c__DisplayClass23_0()
			{
			}

			// Token: 0x06000291 RID: 657 RVA: 0x0000AA5C File Offset: 0x00008C5C
			internal T <GetInvokingMethod>g__WrapConstruction|0<T>(Func<T> f)
			{
				T result;
				try
				{
					result = f();
				}
				catch (ArgumentException)
				{
					throw new ArgumentException("Supplied generic parameters did not satisfy the generic constraints imposed by '" + this.<>4__this.CommandName + "'");
				}
				return result;
			}

			// Token: 0x06000292 RID: 658 RVA: 0x0000AAA4 File Offset: 0x00008CA4
			internal MethodInfo <GetInvokingMethod>b__1()
			{
				return this.method.MakeGenericMethod(this.genericTypeArguments);
			}

			// Token: 0x0400016E RID: 366
			public CommandData <>4__this;

			// Token: 0x0400016F RID: 367
			public Type declaringType;

			// Token: 0x04000170 RID: 368
			public MethodInfo method;

			// Token: 0x04000171 RID: 369
			public Type[] genericTypeArguments;
		}

		// Token: 0x02000082 RID: 130
		[CompilerGenerated]
		private sealed class <>c__DisplayClass23_1
		{
			// Token: 0x06000293 RID: 659 RVA: 0x0000AAB7 File Offset: 0x00008CB7
			public <>c__DisplayClass23_1()
			{
			}

			// Token: 0x06000294 RID: 660 RVA: 0x0000AABF File Offset: 0x00008CBF
			internal Type <GetInvokingMethod>b__2()
			{
				return this.CS$<>8__locals1.declaringType.MakeGenericType(this.genericTypes);
			}

			// Token: 0x04000172 RID: 370
			public Type[] genericTypes;

			// Token: 0x04000173 RID: 371
			public CommandData.<>c__DisplayClass23_0 CS$<>8__locals1;
		}

		// Token: 0x02000083 RID: 131
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <>c__DisplayClass24_0
		{
			// Token: 0x04000174 RID: 372
			public List<string> prefixes;
		}

		// Token: 0x02000084 RID: 132
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000295 RID: 661 RVA: 0x0000AAD7 File Offset: 0x00008CD7
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000296 RID: 662 RVA: 0x0000AAE3 File Offset: 0x00008CE3
			public <>c()
			{
			}

			// Token: 0x06000297 RID: 663 RVA: 0x0000AAEB File Offset: 0x00008CEB
			internal string <BuildGenericSignature>b__25_0(Type x)
			{
				return x.Name;
			}

			// Token: 0x06000298 RID: 664 RVA: 0x0000AAF3 File Offset: 0x00008CF3
			internal Type <.ctor>b__29_0(ParameterInfo x)
			{
				return x.ParameterType;
			}

			// Token: 0x04000175 RID: 373
			public static readonly CommandData.<>c <>9 = new CommandData.<>c();

			// Token: 0x04000176 RID: 374
			public static Func<Type, string> <>9__25_0;

			// Token: 0x04000177 RID: 375
			public static Func<ParameterInfo, Type> <>9__29_0;
		}
	}
}
