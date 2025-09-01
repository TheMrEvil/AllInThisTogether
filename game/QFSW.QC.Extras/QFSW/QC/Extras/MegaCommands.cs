using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using QFSW.QC.Utilities;

namespace QFSW.QC.Extras
{
	// Token: 0x0200000D RID: 13
	public static class MegaCommands
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00003020 File Offset: 0x00001220
		private static MethodInfo[] ExtractMethods(Type type, string name)
		{
			MethodInfo[] array = (from x in type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.InvokeMethod)
			where x.Name == name
			select x).ToArray<MethodInfo>();
			if (!array.Any<MethodInfo>())
			{
				PropertyInfo property = type.GetProperty(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy | BindingFlags.InvokeMethod);
				if (property != null)
				{
					array = (from x in new MethodInfo[]
					{
						property.GetMethod,
						property.SetMethod
					}
					where x != null
					select x).ToArray<MethodInfo>();
					if (array.Length != 0)
					{
						return array;
					}
				}
				throw new ArgumentException("No method or property named " + name + " could be found in class " + MegaCommands.Serializer.SerializeFormatted(type, null));
			}
			return array;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000030F4 File Offset: 0x000012F4
		private static string GenerateSignature(MethodInfo method)
		{
			IEnumerable<string> values = from x in method.GetParameters()
			select new ValueTuple<string, Type>(x.Name, x.ParameterType) into x
			select x.Item2.GetDisplayName(false) + " " + x.Item1;
			string str = string.Join(", ", values);
			return method.Name + "(" + str + ")";
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003174 File Offset: 0x00001374
		private static MethodInfo GetIdealOverload(MethodInfo[] methods, bool isStatic, int argc)
		{
			methods = (from x in methods
			where x.IsStatic == isStatic
			select x).ToArray<MethodInfo>();
			if (methods.Length == 0)
			{
				throw new ArgumentException("No " + (isStatic ? "static" : "non-static") + " overloads could be found.");
			}
			if (methods.Length == 1)
			{
				return methods[0];
			}
			methods = (from x in methods
			where !x.IsGenericMethod
			select x).ToArray<MethodInfo>();
			if (methods.Length == 0)
			{
				throw new ArgumentException("Generic methods are not supported.");
			}
			MethodInfo[] array = (from x in methods
			where x.GetParameters().Length == argc
			select x).ToArray<MethodInfo>();
			if (array.Length == 1)
			{
				return array[0];
			}
			if (array.Length == 0)
			{
				IEnumerable<string> values = methods.Select(new Func<MethodInfo, string>(MegaCommands.GenerateSignature));
				string arg = string.Join("\n", values);
				throw new ArgumentException(string.Format("No overloads with {0} arguments were found. the following overloads are available:\n{1}", argc, arg));
			}
			IEnumerable<string> values2 = array.Select(new Func<MethodInfo, string>(MegaCommands.GenerateSignature));
			string str = string.Join("\n", values2);
			throw new ArgumentException("Multiple overloads with the same argument count were found: please specify the types explicitly.\n" + str);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000032B4 File Offset: 0x000014B4
		private static MethodInfo GetIdealOverload(MethodInfo[] methods, bool isStatic, Type[] argTypes)
		{
			foreach (MethodInfo methodInfo in methods)
			{
				if (methodInfo.IsStatic == isStatic)
				{
					if ((from x in methodInfo.GetParameters()
					select x.ParameterType).SequenceEqual(argTypes))
					{
						return methodInfo;
					}
				}
			}
			foreach (MethodInfo methodInfo2 in methods)
			{
				if (methodInfo2.IsStatic == isStatic)
				{
					ParameterInfo[] parameters = methodInfo2.GetParameters();
					if (parameters.Length == argTypes.Length)
					{
						if ((from x in parameters
						select x.ParameterType).Zip(argTypes, (Type x, Type y) => new ValueTuple<Type, Type>(x, y)).All(([TupleElementNames(new string[]
						{
							"x",
							"y"
						})] ValueTuple<Type, Type> pair) => pair.Item1.IsAssignableFrom(pair.Item2)))
						{
							return methodInfo2;
						}
					}
				}
			}
			throw new ArgumentException("No overload with the supplied argument types could be found.");
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000033C8 File Offset: 0x000015C8
		private static object[] CreateArgs(MethodInfo method, string[] rawArgs)
		{
			Type[] argTypes = (from x in method.GetParameters()
			select x.ParameterType).ToArray<Type>();
			return MegaCommands.CreateArgs(method, argTypes, rawArgs);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003410 File Offset: 0x00001610
		private static object[] CreateArgs(MethodInfo method, Type[] argTypes, string[] rawArgs)
		{
			ParameterInfo[] parameters = method.GetParameters();
			int num = parameters.Count((ParameterInfo x) => x.HasDefaultValue);
			if (rawArgs.Length < argTypes.Length - num || rawArgs.Length > argTypes.Length)
			{
				throw new ArgumentException(string.Format("Incorrect number ({0}) of arguments supplied for {1}.{2}", rawArgs.Length, MegaCommands.Serializer.SerializeFormatted(method.DeclaringType, null), method.Name) + string.Format(", expected {0}", argTypes.Length));
			}
			object[] array = new object[argTypes.Length];
			for (int i = 0; i < array.Length; i++)
			{
				if (i < rawArgs.Length)
				{
					array[i] = MegaCommands.Parser.Parse(rawArgs[i], argTypes[i]);
				}
				else
				{
					array[i] = parameters[i].DefaultValue;
				}
			}
			return array;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000034E0 File Offset: 0x000016E0
		private static object InvokeAndUnwrapException(this MethodInfo method, object[] args)
		{
			object result;
			try
			{
				result = method.Invoke(null, args);
			}
			catch (TargetInvocationException ex)
			{
				throw ex.InnerException;
			}
			return result;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003510 File Offset: 0x00001710
		private static object InvokeAndUnwrapException(this MethodInfo method, IEnumerable<object> targets, object[] args)
		{
			object result;
			try
			{
				result = InvocationTargetFactory.InvokeOnTargets(method, targets, args);
			}
			catch (TargetInvocationException ex)
			{
				throw ex.InnerException;
			}
			return result;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003540 File Offset: 0x00001740
		private static object CallStatic(Type classType, string funcName)
		{
			return MegaCommands.CallStatic(classType, funcName, Array.Empty<string>());
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003550 File Offset: 0x00001750
		private static object CallStatic(Type classType, string funcName, string[] args)
		{
			MethodInfo idealOverload = MegaCommands.GetIdealOverload(MegaCommands.ExtractMethods(classType, funcName), true, args.Length);
			object[] args2 = MegaCommands.CreateArgs(idealOverload, args);
			return idealOverload.InvokeAndUnwrapException(args2);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000357C File Offset: 0x0000177C
		private static object CallStatic([CommandParameterDescription("Namespace qualified typename of the class.")] Type classType, [CommandParameterDescription("Name of the method or property.")] string funcName, [CommandParameterDescription("The arguments for the function call.")] string[] args, [CommandParameterDescription("The types of the arguments to resolve ambiguous overloads.")] Type[] argTypes)
		{
			MethodInfo idealOverload = MegaCommands.GetIdealOverload(MegaCommands.ExtractMethods(classType, funcName), true, argTypes);
			object[] args2 = MegaCommands.CreateArgs(idealOverload, argTypes, args);
			return idealOverload.InvokeAndUnwrapException(args2);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000035A6 File Offset: 0x000017A6
		private static object CallInstance(Type classType, string funcName, MonoTargetType targetType)
		{
			return MegaCommands.CallInstance(classType, funcName, targetType, Array.Empty<string>());
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000035B8 File Offset: 0x000017B8
		private static object CallInstance(Type classType, string funcName, MonoTargetType targetType, string[] args)
		{
			MethodInfo idealOverload = MegaCommands.GetIdealOverload(MegaCommands.ExtractMethods(classType, funcName), false, args.Length);
			object[] args2 = MegaCommands.CreateArgs(idealOverload, args);
			IEnumerable<object> targets = InvocationTargetFactory.FindTargets(classType, targetType);
			return idealOverload.InvokeAndUnwrapException(targets, args2);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000035EC File Offset: 0x000017EC
		private static object CallInstance([CommandParameterDescription("Namespace qualified typename of the class.")] Type classType, [CommandParameterDescription("Name of the method or property.")] string funcName, [CommandParameterDescription("The MonoTargetType used to find the target instances.")] MonoTargetType targetType, [CommandParameterDescription("The arguments for the function call.")] string[] args, [CommandParameterDescription("The types of the arguments to resolve ambiguous overloads.")] Type[] argTypes)
		{
			MethodInfo idealOverload = MegaCommands.GetIdealOverload(MegaCommands.ExtractMethods(classType, funcName), false, argTypes);
			object[] args2 = MegaCommands.CreateArgs(idealOverload, argTypes, args);
			IEnumerable<object> targets = InvocationTargetFactory.FindTargets(classType, targetType);
			return idealOverload.InvokeAndUnwrapException(targets, args2);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003621 File Offset: 0x00001821
		// Note: this type is marked as 'beforefieldinit'.
		static MegaCommands()
		{
		}

		// Token: 0x04000011 RID: 17
		private static readonly QuantumSerializer Serializer = new QuantumSerializer();

		// Token: 0x04000012 RID: 18
		private static readonly QuantumParser Parser = new QuantumParser();

		// Token: 0x0200001E RID: 30
		[CompilerGenerated]
		private sealed class <>c__DisplayClass2_0
		{
			// Token: 0x0600007E RID: 126 RVA: 0x000041BF File Offset: 0x000023BF
			public <>c__DisplayClass2_0()
			{
			}

			// Token: 0x0600007F RID: 127 RVA: 0x000041C7 File Offset: 0x000023C7
			internal bool <ExtractMethods>b__0(MethodInfo x)
			{
				return x.Name == this.name;
			}

			// Token: 0x0400003B RID: 59
			public string name;
		}

		// Token: 0x0200001F RID: 31
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000080 RID: 128 RVA: 0x000041DA File Offset: 0x000023DA
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000081 RID: 129 RVA: 0x000041E6 File Offset: 0x000023E6
			public <>c()
			{
			}

			// Token: 0x06000082 RID: 130 RVA: 0x000041EE File Offset: 0x000023EE
			internal bool <ExtractMethods>b__2_1(MethodInfo x)
			{
				return x != null;
			}

			// Token: 0x06000083 RID: 131 RVA: 0x000041F7 File Offset: 0x000023F7
			[return: TupleElementNames(new string[]
			{
				"Name",
				"ParameterType"
			})]
			internal ValueTuple<string, Type> <GenerateSignature>b__3_0(ParameterInfo x)
			{
				return new ValueTuple<string, Type>(x.Name, x.ParameterType);
			}

			// Token: 0x06000084 RID: 132 RVA: 0x0000420A File Offset: 0x0000240A
			internal string <GenerateSignature>b__3_1([TupleElementNames(new string[]
			{
				"Name",
				"ParameterType"
			})] ValueTuple<string, Type> x)
			{
				return x.Item2.GetDisplayName(false) + " " + x.Item1;
			}

			// Token: 0x06000085 RID: 133 RVA: 0x00004228 File Offset: 0x00002428
			internal bool <GetIdealOverload>b__4_1(MethodInfo x)
			{
				return !x.IsGenericMethod;
			}

			// Token: 0x06000086 RID: 134 RVA: 0x00004233 File Offset: 0x00002433
			internal Type <GetIdealOverload>b__5_0(ParameterInfo x)
			{
				return x.ParameterType;
			}

			// Token: 0x06000087 RID: 135 RVA: 0x0000423B File Offset: 0x0000243B
			internal Type <GetIdealOverload>b__5_1(ParameterInfo x)
			{
				return x.ParameterType;
			}

			// Token: 0x06000088 RID: 136 RVA: 0x00004243 File Offset: 0x00002443
			[return: TupleElementNames(new string[]
			{
				"x",
				"y"
			})]
			internal ValueTuple<Type, Type> <GetIdealOverload>b__5_2(Type x, Type y)
			{
				return new ValueTuple<Type, Type>(x, y);
			}

			// Token: 0x06000089 RID: 137 RVA: 0x0000424C File Offset: 0x0000244C
			internal bool <GetIdealOverload>b__5_3([TupleElementNames(new string[]
			{
				"x",
				"y"
			})] ValueTuple<Type, Type> pair)
			{
				return pair.Item1.IsAssignableFrom(pair.Item2);
			}

			// Token: 0x0600008A RID: 138 RVA: 0x0000425F File Offset: 0x0000245F
			internal Type <CreateArgs>b__6_0(ParameterInfo x)
			{
				return x.ParameterType;
			}

			// Token: 0x0600008B RID: 139 RVA: 0x00004267 File Offset: 0x00002467
			internal bool <CreateArgs>b__7_0(ParameterInfo x)
			{
				return x.HasDefaultValue;
			}

			// Token: 0x0400003C RID: 60
			public static readonly MegaCommands.<>c <>9 = new MegaCommands.<>c();

			// Token: 0x0400003D RID: 61
			public static Func<MethodInfo, bool> <>9__2_1;

			// Token: 0x0400003E RID: 62
			[TupleElementNames(new string[]
			{
				"Name",
				"ParameterType"
			})]
			public static Func<ParameterInfo, ValueTuple<string, Type>> <>9__3_0;

			// Token: 0x0400003F RID: 63
			[TupleElementNames(new string[]
			{
				"Name",
				"ParameterType"
			})]
			public static Func<ValueTuple<string, Type>, string> <>9__3_1;

			// Token: 0x04000040 RID: 64
			public static Func<MethodInfo, bool> <>9__4_1;

			// Token: 0x04000041 RID: 65
			public static Func<ParameterInfo, Type> <>9__5_0;

			// Token: 0x04000042 RID: 66
			public static Func<ParameterInfo, Type> <>9__5_1;

			// Token: 0x04000043 RID: 67
			[TupleElementNames(new string[]
			{
				"x",
				"y"
			})]
			public static Func<Type, Type, ValueTuple<Type, Type>> <>9__5_2;

			// Token: 0x04000044 RID: 68
			[TupleElementNames(new string[]
			{
				"x",
				"y"
			})]
			public static Func<ValueTuple<Type, Type>, bool> <>9__5_3;

			// Token: 0x04000045 RID: 69
			public static Func<ParameterInfo, Type> <>9__6_0;

			// Token: 0x04000046 RID: 70
			public static Func<ParameterInfo, bool> <>9__7_0;
		}

		// Token: 0x02000020 RID: 32
		[CompilerGenerated]
		private sealed class <>c__DisplayClass4_0
		{
			// Token: 0x0600008C RID: 140 RVA: 0x0000426F File Offset: 0x0000246F
			public <>c__DisplayClass4_0()
			{
			}

			// Token: 0x0600008D RID: 141 RVA: 0x00004277 File Offset: 0x00002477
			internal bool <GetIdealOverload>b__0(MethodInfo x)
			{
				return x.IsStatic == this.isStatic;
			}

			// Token: 0x0600008E RID: 142 RVA: 0x00004287 File Offset: 0x00002487
			internal bool <GetIdealOverload>b__2(MethodInfo x)
			{
				return x.GetParameters().Length == this.argc;
			}

			// Token: 0x04000047 RID: 71
			public bool isStatic;

			// Token: 0x04000048 RID: 72
			public int argc;
		}
	}
}
