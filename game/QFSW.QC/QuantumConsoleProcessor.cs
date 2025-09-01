using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using QFSW.QC.Internal;
using QFSW.QC.Suggestors.Tags;
using QFSW.QC.Utilities;
using UnityEngine;

namespace QFSW.QC
{
	// Token: 0x0200002D RID: 45
	public static class QuantumConsoleProcessor
	{
		// Token: 0x0600010A RID: 266 RVA: 0x00005C53 File Offset: 0x00003E53
		private static string GetHelp()
		{
			return "Welcome to Quantum Console! In order to see specific help about any specific command, please use the 'man' command. Use 'man man' to see more about the man command. To see a full list of all commands, use 'all-commands'.\n\nmono-targets\nVarious commands may show a mono-target in their command signature.\nThis means they are not static commands, and instead requires instance(s) of the class in order to invoke the command.\nEach mono-target works differently as follows:\n - single: uses the first instance of the type found in the scene\n - all: uses all instances of the type found in the scene\n - registry: uses all instances of the type found in the registry\n - singleton: creates and manages a single instance automatically\n\nThe registry is a part of the Quantum Registry that allows you to decide which specific instances of the class should be used when invoking the command. In order to add an object to the registry, either use QFSW.QC.QuantumRegistry.RegisterObject<T> or the runtime command 'register-object<T>'.";
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005C5A File Offset: 0x00003E5A
		private static string ManualHelp()
		{
			return "To use the man command, simply put the desired command name in front of it. For example, 'man my-command' will generate the manual for 'my-command'";
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005C64 File Offset: 0x00003E64
		private static string GenerateCommandManual([CommandName] string commandName)
		{
			string[] array = (from key in QuantumConsoleProcessor._commandTable.Keys
			where key.Split('(', StringSplitOptions.None)[0] == commandName
			orderby key
			select key).ToArray<string>();
			if (array.Length == 0)
			{
				throw new ArgumentException("No command with the name " + commandName + " was found.");
			}
			Dictionary<string, ParameterInfo> dictionary = new Dictionary<string, ParameterInfo>();
			Dictionary<string, Type> dictionary2 = new Dictionary<string, Type>();
			Dictionary<string, CommandParameterDescriptionAttribute> dictionary3 = new Dictionary<string, CommandParameterDescriptionAttribute>();
			List<Type> list = new List<Type>(1);
			string text = "Generated user manual for " + commandName + "\nAvailable command signatures:";
			for (int i = 0; i < array.Length; i++)
			{
				CommandData commandData = QuantumConsoleProcessor._commandTable[array[i]];
				list.Add(commandData.MethodData.DeclaringType);
				text = text + "\n   - " + commandData.CommandSignature;
				if (!commandData.IsStatic)
				{
					text = text + " (mono-target = " + commandData.MonoTarget.ToString().ToLower() + ")";
				}
				for (int j = 0; j < commandData.ParamCount; j++)
				{
					ParameterInfo parameterInfo = commandData.MethodParamData[j];
					if (!dictionary.ContainsKey(parameterInfo.Name))
					{
						dictionary.Add(parameterInfo.Name, parameterInfo);
					}
					if (!dictionary3.ContainsKey(parameterInfo.Name))
					{
						CommandParameterDescriptionAttribute customAttribute = parameterInfo.GetCustomAttribute<CommandParameterDescriptionAttribute>();
						if (customAttribute != null && customAttribute.Valid)
						{
							dictionary3.Add(parameterInfo.Name, customAttribute);
						}
					}
				}
				if (commandData.IsGeneric)
				{
					foreach (Type type in commandData.GenericParamTypes)
					{
						if (!dictionary2.ContainsKey(type.Name))
						{
							dictionary2.Add(type.Name, type);
						}
					}
				}
			}
			if (dictionary.Count > 0)
			{
				text += "\nParameter info:";
				foreach (ParameterInfo parameterInfo2 in dictionary.Values.ToArray<ParameterInfo>())
				{
					text = string.Concat(new string[]
					{
						text,
						"\n   - ",
						parameterInfo2.Name,
						": ",
						parameterInfo2.ParameterType.GetDisplayName(false)
					});
				}
			}
			string text2 = "";
			if (dictionary2.Count > 0)
			{
				Type[] array3 = dictionary2.Values.ToArray<Type>();
				for (int m = 0; m < array3.Length; m++)
				{
					Type type2 = array3[m];
					Type[] genericParameterConstraints = type2.GetGenericParameterConstraints();
					GenericParameterAttributes genericParameterAttributes = type2.GenericParameterAttributes;
					List<string> list2 = new List<string>();
					if (genericParameterAttributes.HasFlag(GenericParameterAttributes.NotNullableValueTypeConstraint))
					{
						list2.Add("struct");
					}
					if (genericParameterAttributes.HasFlag(GenericParameterAttributes.ReferenceTypeConstraint))
					{
						list2.Add("class");
					}
					for (int n = 0; n < genericParameterConstraints.Length; n++)
					{
						list2.Add(genericParameterConstraints[m].GetDisplayName(false));
					}
					if (genericParameterAttributes.HasFlag(GenericParameterAttributes.DefaultConstructorConstraint))
					{
						list2.Add("new()");
					}
					if (list2.Count > 0)
					{
						text2 = string.Concat(new string[]
						{
							text2,
							"\n   - ",
							type2.Name,
							": ",
							string.Join(", ", list2)
						});
					}
				}
			}
			if (!string.IsNullOrWhiteSpace(text2))
			{
				text = text + "\nGeneric constraints:" + text2;
			}
			for (int num = 0; num < array.Length; num++)
			{
				CommandData commandData2 = QuantumConsoleProcessor._commandTable[array[num]];
				if (commandData2.HasDescription)
				{
					text = text + "\n\nCommand description:\n" + commandData2.CommandDescription;
					num = array.Length;
				}
			}
			if (dictionary3.Count > 0)
			{
				text += "\n\nParameter descriptions:";
				foreach (ParameterInfo parameterInfo3 in dictionary.Values.ToArray<ParameterInfo>())
				{
					if (dictionary3.ContainsKey(parameterInfo3.Name))
					{
						text = string.Concat(new string[]
						{
							text,
							"\n - ",
							parameterInfo3.Name,
							": ",
							dictionary3[parameterInfo3.Name].Description
						});
					}
				}
			}
			list = list.Distinct<Type>().ToList<Type>();
			text += "\n\nDeclared in";
			if (list.Count == 1)
			{
				text = text + " " + list[0].GetDisplayName(true);
			}
			else
			{
				text += ":";
				foreach (Type type3 in list)
				{
					text = text + "\n   - " + type3.GetDisplayName(true);
				}
			}
			return text;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00006190 File Offset: 0x00004390
		public static IEnumerable<CommandData> GetUniqueCommands()
		{
			return from x in QuantumConsoleProcessor.GetAllCommands().DistinctBy((CommandData x) => x.CommandName)
			orderby x.CommandName
			select x;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000061EC File Offset: 0x000043EC
		[CommandDescription("Generates a list of all commands currently loaded by the Quantum Console Processor")]
		[Command("commands", Platform.AllPlatforms, MonoTargetType.Single)]
		private static string GenerateCommandList()
		{
			string text = "List of all commands loaded by the Quantum Processor. Use 'man' on any command to see more:";
			foreach (CommandData commandData in QuantumConsoleProcessor.GetUniqueCommands())
			{
				text = text + "\n   - " + commandData.CommandName;
			}
			return text;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000624C File Offset: 0x0000444C
		private static IEnumerable<string> GenerateUserCommandList()
		{
			return from x in QuantumConsoleProcessor.GetUniqueCommands()
			where !x.MethodData.DeclaringType.Assembly.FullName.StartsWith("QFSW.QC")
			select "   - " + x.CommandName;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000110 RID: 272 RVA: 0x000062A6 File Offset: 0x000044A6
		// (set) Token: 0x06000111 RID: 273 RVA: 0x000062AD File Offset: 0x000044AD
		public static bool TableGenerated
		{
			[CompilerGenerated]
			get
			{
				return QuantumConsoleProcessor.<TableGenerated>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				QuantumConsoleProcessor.<TableGenerated>k__BackingField = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000112 RID: 274 RVA: 0x000062B5 File Offset: 0x000044B5
		// (set) Token: 0x06000113 RID: 275 RVA: 0x000062BC File Offset: 0x000044BC
		public static bool TableIsGenerating
		{
			[CompilerGenerated]
			get
			{
				return QuantumConsoleProcessor.<TableIsGenerating>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				QuantumConsoleProcessor.<TableIsGenerating>k__BackingField = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000114 RID: 276 RVA: 0x000062C4 File Offset: 0x000044C4
		public static int LoadedCommandCount
		{
			get
			{
				return QuantumConsoleProcessor._loadedCommandCount;
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000062CC File Offset: 0x000044CC
		public static IEnumerable<CommandData> GetAllCommands()
		{
			if (QuantumConsoleProcessor._commandCacheDirty)
			{
				List<CommandData> commandCache = QuantumConsoleProcessor._commandCache;
				lock (commandCache)
				{
					QuantumConsoleProcessor._commandCache.Clear();
					QuantumConsoleProcessor._commandCache.AddRange(QuantumConsoleProcessor._commandTable.Values);
					QuantumConsoleProcessor._commandCacheDirty = false;
				}
			}
			return QuantumConsoleProcessor._commandCache;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00006338 File Offset: 0x00004538
		public static void GenerateCommandTable(bool deployThread = false, bool forceReload = false)
		{
			if (deployThread)
			{
				ThreadPool.QueueUserWorkItem(delegate(object state)
				{
					try
					{
						QuantumConsoleProcessor.GenerateCommandTable(false, forceReload);
					}
					catch (Exception exception)
					{
						UnityEngine.Debug.LogException(exception);
					}
				});
				return;
			}
			ConcurrentDictionary<string, CommandData> commandTable = QuantumConsoleProcessor._commandTable;
			lock (commandTable)
			{
				if (!QuantumConsoleProcessor.TableGenerated | forceReload)
				{
					QuantumConsoleProcessor.TableIsGenerating = true;
					if (forceReload && QuantumConsoleProcessor.TableGenerated)
					{
						QuantumConsoleProcessor._commandTable.Clear();
						QuantumConsoleProcessor._loadedCommandCount = 0;
					}
					Parallel.ForEach<Assembly>(QuantumConsoleProcessor._loadedAssemblies, new Action<Assembly>(QuantumConsoleProcessor.LoadCommandsFromAssembly));
					QuantumConsoleProcessor.TableIsGenerating = false;
					QuantumConsoleProcessor.TableGenerated = true;
					GC.Collect(3, GCCollectionMode.Forced, false, true);
				}
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000063F8 File Offset: 0x000045F8
		[return: TupleElementNames(new string[]
		{
			"method",
			"member"
		})]
		private static IEnumerable<ValueTuple<MethodInfo, MemberInfo>> ExtractCommandMethods(Type type)
		{
			MethodInfo[] methods = type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			PropertyInfo[] properties = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			FieldInfo[] fields = type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			foreach (MethodInfo methodInfo in methods)
			{
				yield return new ValueTuple<MethodInfo, MemberInfo>(methodInfo, methodInfo);
			}
			MethodInfo[] array = null;
			foreach (PropertyInfo property in properties)
			{
				if (property.CanWrite)
				{
					yield return new ValueTuple<MethodInfo, MemberInfo>(property.SetMethod, property);
				}
				if (property.CanRead)
				{
					yield return new ValueTuple<MethodInfo, MemberInfo>(property.GetMethod, property);
				}
				property = null;
			}
			PropertyInfo[] array2 = null;
			foreach (FieldInfo field in fields)
			{
				if (field.HasAttribute(true))
				{
					if (field.IsDelegate())
					{
						if (field.IsStrongDelegate())
						{
							FieldDelegateMethod item = new FieldDelegateMethod(field);
							yield return new ValueTuple<MethodInfo, MemberInfo>(item, field);
						}
						else if (QuantumConsoleProcessor.loggingLevel >= LoggingLevel.Warnings)
						{
							UnityEngine.Debug.LogWarning(string.Format("Quantum Processor Warning: Could not add '{0}' from {1} to the table as it is an invalid delegate type.", field.Name, field.DeclaringType));
						}
					}
					else
					{
						FieldAutoMethod item2 = new FieldAutoMethod(field, FieldAutoMethod.AccessType.Read);
						yield return new ValueTuple<MethodInfo, MemberInfo>(item2, field);
						if (!field.IsLiteral && !field.IsInitOnly)
						{
							FieldAutoMethod item3 = new FieldAutoMethod(field, FieldAutoMethod.AccessType.Write);
							yield return new ValueTuple<MethodInfo, MemberInfo>(item3, field);
						}
					}
				}
				field = null;
			}
			FieldInfo[] array3 = null;
			yield break;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00006408 File Offset: 0x00004608
		private static bool GetCommandSupported(CommandData command, out string unsupportedReason)
		{
			for (int i = 0; i < command.ParamCount; i++)
			{
				Type parameterType = command.MethodParamData[i].ParameterType;
				if (!QuantumConsoleProcessor._parser.CanParse(parameterType) && !parameterType.IsGenericParameter)
				{
					unsupportedReason = string.Format("Parameter type {0} is not supported by the Quantum Parser.", parameterType);
					return false;
				}
			}
			if (command.MonoTarget != MonoTargetType.Registry && !command.MethodData.IsStatic && !command.MethodData.DeclaringType.IsDerivedTypeOf(typeof(MonoBehaviour)))
			{
				unsupportedReason = string.Format("Non static non MonoBehaviour commands are incompatible with MonoTargetType.{0}.", command.MonoTarget);
				return false;
			}
			unsupportedReason = string.Empty;
			return true;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000064AC File Offset: 0x000046AC
		private static void LoadCommandsFromAssembly(Assembly assembly)
		{
			if (!QuantumConsoleProcessor._scanRuleset.ShouldScan<Assembly>(assembly))
			{
				return;
			}
			foreach (Type type in assembly.GetTypes())
			{
				try
				{
					QuantumConsoleProcessor.LoadCommandsFromType(type);
				}
				catch (TypeLoadException)
				{
				}
				catch (BadImageFormatException)
				{
				}
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000650C File Offset: 0x0000470C
		private static void LoadCommandsFromType(Type type)
		{
			if (!QuantumConsoleProcessor._scanRuleset.ShouldScan<Type>(type))
			{
				return;
			}
			foreach (ValueTuple<MethodInfo, MemberInfo> valueTuple in QuantumConsoleProcessor.ExtractCommandMethods(type))
			{
				MethodInfo item = valueTuple.Item1;
				MemberInfo item2 = valueTuple.Item2;
				if (item2.DeclaringType == type)
				{
					QuantumConsoleProcessor.LoadCommandsFromMember(item2, item);
				}
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00006584 File Offset: 0x00004784
		private static void LoadCommandsFromMember(MemberInfo member, MethodInfo method)
		{
			if (!QuantumConsoleProcessor._scanRuleset.ShouldScan<MemberInfo>(member))
			{
				return;
			}
			IEnumerable<CommandAttribute> customAttributes = member.GetCustomAttributes<CommandAttribute>();
			CommandDescriptionAttribute customAttribute = member.GetCustomAttribute<CommandDescriptionAttribute>();
			foreach (CommandAttribute commandAttribute in customAttributes)
			{
				if (!commandAttribute.Valid)
				{
					if (QuantumConsoleProcessor.loggingLevel >= LoggingLevel.Warnings)
					{
						UnityEngine.Debug.LogWarning("Quantum Processor Warning: Could not add '" + commandAttribute.Alias + "' to the table as it is invalid.");
					}
				}
				else
				{
					CommandPlatformAttribute customAttribute2 = member.GetCustomAttribute<CommandPlatformAttribute>();
					Platform platform = (customAttribute2 != null) ? customAttribute2.SupportedPlatforms : commandAttribute.SupportedPlatforms;
					if (platform.HasFlag(Application.platform.ToPlatform()))
					{
						foreach (CommandData command in QuantumConsoleProcessor.CreateCommandOverloads(method, commandAttribute, customAttribute))
						{
							QuantumConsoleProcessor.TryAddCommand(command);
						}
					}
				}
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000668C File Offset: 0x0000488C
		private static IEnumerable<CommandData> CreateCommandOverloads(MethodInfo method, CommandAttribute commandAttribute, CommandDescriptionAttribute descriptionAttribute)
		{
			int defaultParameters = method.GetParameters().Count((ParameterInfo x) => x.HasDefaultValue);
			int num;
			for (int i = 0; i < defaultParameters + 1; i = num + 1)
			{
				CommandData commandData = new CommandData(method, commandAttribute, descriptionAttribute, i);
				yield return commandData;
				num = i;
			}
			yield break;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000066AA File Offset: 0x000048AA
		private static string GenerateCommandKey(CommandData command)
		{
			return string.Format("{0}({1})", command.CommandName, command.ParamCount);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000066C8 File Offset: 0x000048C8
		public static bool TryAddCommand(CommandData command)
		{
			string text;
			if (!QuantumConsoleProcessor.GetCommandSupported(command, out text))
			{
				if (QuantumConsoleProcessor.loggingLevel >= LoggingLevel.Warnings)
				{
					UnityEngine.Debug.LogWarning(string.Concat(new string[]
					{
						"Quantum Processor Warning: Could not add '",
						command.CommandSignature,
						"' from ",
						command.MethodData.DeclaringType.GetDisplayName(false),
						" to the table as it is not supported. ",
						text
					}));
				}
				return false;
			}
			string text2 = QuantumConsoleProcessor.GenerateCommandKey(command);
			if (!QuantumConsoleProcessor._commandTable.TryAdd(text2, command))
			{
				if (QuantumConsoleProcessor.loggingLevel >= LoggingLevel.Warnings)
				{
					string text3 = command.MethodData.DeclaringType.FullName + "." + command.MethodData.Name;
					UnityEngine.Debug.LogWarning(string.Concat(new string[]
					{
						"Quantum Processor Warning: Could not add ",
						text3,
						" to the table as another method with the same alias and parameter count, ",
						text2,
						", already exists."
					}));
				}
				return false;
			}
			QuantumConsoleProcessor._commandCacheDirty = true;
			Interlocked.Increment(ref QuantumConsoleProcessor._loadedCommandCount);
			return true;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000067BC File Offset: 0x000049BC
		public static bool TryRemoveCommand(CommandData command)
		{
			string key = QuantumConsoleProcessor.GenerateCommandKey(command);
			CommandData commandData;
			if (QuantumConsoleProcessor._commandTable.TryRemove(key, out commandData))
			{
				QuantumConsoleProcessor._commandCacheDirty = true;
				Interlocked.Decrement(ref QuantumConsoleProcessor._loadedCommandCount);
				return true;
			}
			return false;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000067F4 File Offset: 0x000049F4
		public static object InvokeCommand(string commandString)
		{
			QuantumConsoleProcessor.GenerateCommandTable(false, false);
			commandString = commandString.Trim();
			commandString = QuantumConsoleProcessor._preprocessor.Process(commandString);
			if (string.IsNullOrWhiteSpace(commandString))
			{
				throw new ArgumentException("Cannot parse an empty string.");
			}
			string[] array = commandString.SplitScoped(' ');
			array = (from x in array
			where !string.IsNullOrWhiteSpace(x)
			select x).ToArray<string>();
			string commandName = array[0];
			string[] array2 = array.SubArray(1, array.Length - 1);
			int num = array2.Length;
			string[] array3 = commandName.Split(new char[]
			{
				'<'
			}, 2);
			string text = (array3.Length > 1) ? ("<" + array3[1]) : "";
			commandName = array3[0];
			string key2 = string.Format("{0}({1})", commandName, num);
			if (QuantumConsoleProcessor._commandTable.ContainsKey(key2))
			{
				CommandData commandData = QuantumConsoleProcessor._commandTable[key2];
				Type[] array4 = Array.Empty<Type>();
				if (commandData.IsGeneric)
				{
					int num2 = commandData.GenericParamTypes.Length;
					string[] array5 = text.ReduceScope('<', '>').SplitScoped(',');
					if (array5.Length != num2)
					{
						throw new ArgumentException(string.Format("Generic command '{0}' requires {1} generic parameter{2} but was supplied with {3}.", new object[]
						{
							commandName,
							num2,
							(num2 == 1) ? "" : "s",
							array5.Length
						}));
					}
					array4 = new Type[array5.Length];
					for (int i = 0; i < array4.Length; i++)
					{
						array4[i] = QuantumParser.ParseType(array5[i]);
					}
				}
				else if (text != string.Empty)
				{
					throw new ArgumentException("Command '" + commandName + "' is not a generic command and cannot be invoked as such.");
				}
				object[] paramData = QuantumConsoleProcessor.ParseParamData(commandData.MakeGenericArguments(array4), array2);
				return commandData.Invoke(paramData, array4);
			}
			if (QuantumConsoleProcessor._commandTable.Keys.Any((string key) => key.Contains(commandName + "(") && QuantumConsoleProcessor._commandTable[key].CommandName == commandName))
			{
				throw new ArgumentException(string.Format("No overload of '{0}' with {1} parameters could be found.", commandName, num));
			}
			throw new ArgumentException("Command '" + commandName + "' could not be found.");
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00006A44 File Offset: 0x00004C44
		private static object[] ParseParamData(Type[] paramTypes, string[] paramData)
		{
			object[] array = new object[paramData.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = QuantumConsoleProcessor._parser.Parse(paramData[i], paramTypes[i]);
			}
			return array;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00006A7C File Offset: 0x00004C7C
		// Note: this type is marked as 'beforefieldinit'.
		static QuantumConsoleProcessor()
		{
		}

		// Token: 0x040000C6 RID: 198
		private const string helpStr = "Welcome to Quantum Console! In order to see specific help about any specific command, please use the 'man' command. Use 'man man' to see more about the man command. To see a full list of all commands, use 'all-commands'.\n\nmono-targets\nVarious commands may show a mono-target in their command signature.\nThis means they are not static commands, and instead requires instance(s) of the class in order to invoke the command.\nEach mono-target works differently as follows:\n - single: uses the first instance of the type found in the scene\n - all: uses all instances of the type found in the scene\n - registry: uses all instances of the type found in the registry\n - singleton: creates and manages a single instance automatically\n\nThe registry is a part of the Quantum Registry that allows you to decide which specific instances of the class should be used when invoking the command. In order to add an object to the registry, either use QFSW.QC.QuantumRegistry.RegisterObject<T> or the runtime command 'register-object<T>'.";

		// Token: 0x040000C7 RID: 199
		public static LoggingLevel loggingLevel = LoggingLevel.Full;

		// Token: 0x040000C8 RID: 200
		private static readonly QuantumParser _parser = new QuantumParser();

		// Token: 0x040000C9 RID: 201
		private static readonly QuantumPreprocessor _preprocessor = new QuantumPreprocessor();

		// Token: 0x040000CA RID: 202
		private static readonly QuantumScanRuleset _scanRuleset = new QuantumScanRuleset();

		// Token: 0x040000CB RID: 203
		private static readonly ConcurrentDictionary<string, CommandData> _commandTable = new ConcurrentDictionary<string, CommandData>();

		// Token: 0x040000CC RID: 204
		private static readonly List<CommandData> _commandCache = new List<CommandData>();

		// Token: 0x040000CD RID: 205
		[CompilerGenerated]
		private static bool <TableGenerated>k__BackingField;

		// Token: 0x040000CE RID: 206
		[CompilerGenerated]
		private static bool <TableIsGenerating>k__BackingField;

		// Token: 0x040000CF RID: 207
		private static int _loadedCommandCount = 0;

		// Token: 0x040000D0 RID: 208
		private static bool _commandCacheDirty = true;

		// Token: 0x040000D1 RID: 209
		private static readonly Assembly[] _loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();

		// Token: 0x0200008E RID: 142
		[CompilerGenerated]
		private sealed class <>c__DisplayClass3_0
		{
			// Token: 0x060002CB RID: 715 RVA: 0x0000B336 File Offset: 0x00009536
			public <>c__DisplayClass3_0()
			{
			}

			// Token: 0x060002CC RID: 716 RVA: 0x0000B33E File Offset: 0x0000953E
			internal bool <GenerateCommandManual>b__0(string key)
			{
				return key.Split('(', StringSplitOptions.None)[0] == this.commandName;
			}

			// Token: 0x040001A5 RID: 421
			public string commandName;
		}

		// Token: 0x0200008F RID: 143
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060002CD RID: 717 RVA: 0x0000B356 File Offset: 0x00009556
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060002CE RID: 718 RVA: 0x0000B362 File Offset: 0x00009562
			public <>c()
			{
			}

			// Token: 0x060002CF RID: 719 RVA: 0x0000B36A File Offset: 0x0000956A
			internal string <GenerateCommandManual>b__3_1(string key)
			{
				return key;
			}

			// Token: 0x060002D0 RID: 720 RVA: 0x0000B36D File Offset: 0x0000956D
			internal string <GetUniqueCommands>b__4_0(CommandData x)
			{
				return x.CommandName;
			}

			// Token: 0x060002D1 RID: 721 RVA: 0x0000B375 File Offset: 0x00009575
			internal string <GetUniqueCommands>b__4_1(CommandData x)
			{
				return x.CommandName;
			}

			// Token: 0x060002D2 RID: 722 RVA: 0x0000B37D File Offset: 0x0000957D
			internal bool <GenerateUserCommandList>b__6_0(CommandData x)
			{
				return !x.MethodData.DeclaringType.Assembly.FullName.StartsWith("QFSW.QC");
			}

			// Token: 0x060002D3 RID: 723 RVA: 0x0000B3A1 File Offset: 0x000095A1
			internal string <GenerateUserCommandList>b__6_1(CommandData x)
			{
				return "   - " + x.CommandName;
			}

			// Token: 0x060002D4 RID: 724 RVA: 0x0000B3B3 File Offset: 0x000095B3
			internal bool <CreateCommandOverloads>b__33_0(ParameterInfo x)
			{
				return x.HasDefaultValue;
			}

			// Token: 0x060002D5 RID: 725 RVA: 0x0000B3BB File Offset: 0x000095BB
			internal bool <InvokeCommand>b__37_0(string x)
			{
				return !string.IsNullOrWhiteSpace(x);
			}

			// Token: 0x040001A6 RID: 422
			public static readonly QuantumConsoleProcessor.<>c <>9 = new QuantumConsoleProcessor.<>c();

			// Token: 0x040001A7 RID: 423
			public static Func<string, string> <>9__3_1;

			// Token: 0x040001A8 RID: 424
			public static Func<CommandData, string> <>9__4_0;

			// Token: 0x040001A9 RID: 425
			public static Func<CommandData, string> <>9__4_1;

			// Token: 0x040001AA RID: 426
			public static Func<CommandData, bool> <>9__6_0;

			// Token: 0x040001AB RID: 427
			public static Func<CommandData, string> <>9__6_1;

			// Token: 0x040001AC RID: 428
			public static Func<ParameterInfo, bool> <>9__33_0;

			// Token: 0x040001AD RID: 429
			public static Func<string, bool> <>9__37_0;
		}

		// Token: 0x02000090 RID: 144
		[CompilerGenerated]
		private sealed class <>c__DisplayClass27_0
		{
			// Token: 0x060002D6 RID: 726 RVA: 0x0000B3C6 File Offset: 0x000095C6
			public <>c__DisplayClass27_0()
			{
			}

			// Token: 0x060002D7 RID: 727 RVA: 0x0000B3D0 File Offset: 0x000095D0
			internal void <GenerateCommandTable>b__0(object state)
			{
				try
				{
					QuantumConsoleProcessor.GenerateCommandTable(false, this.forceReload);
				}
				catch (Exception exception)
				{
					UnityEngine.Debug.LogException(exception);
				}
			}

			// Token: 0x040001AE RID: 430
			public bool forceReload;
		}

		// Token: 0x02000091 RID: 145
		[CompilerGenerated]
		private sealed class <ExtractCommandMethods>d__28 : IEnumerable<ValueTuple<MethodInfo, MemberInfo>>, IEnumerable, IEnumerator<ValueTuple<MethodInfo, MemberInfo>>, IEnumerator, IDisposable
		{
			// Token: 0x060002D8 RID: 728 RVA: 0x0000B404 File Offset: 0x00009604
			[DebuggerHidden]
			public <ExtractCommandMethods>d__28(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x060002D9 RID: 729 RVA: 0x0000B41E File Offset: 0x0000961E
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060002DA RID: 730 RVA: 0x0000B420 File Offset: 0x00009620
			bool IEnumerator.MoveNext()
			{
				switch (this.<>1__state)
				{
				case 0:
				{
					this.<>1__state = -1;
					MethodInfo[] methods = type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
					properties = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
					fields = type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
					array = methods;
					i = 0;
					break;
				}
				case 1:
					this.<>1__state = -1;
					i++;
					break;
				case 2:
					this.<>1__state = -1;
					goto IL_12A;
				case 3:
					this.<>1__state = -1;
					goto IL_163;
				case 4:
					this.<>1__state = -1;
					goto IL_2C9;
				case 5:
					this.<>1__state = -1;
					if (!field.IsLiteral && !field.IsInitOnly)
					{
						FieldAutoMethod item = new FieldAutoMethod(field, FieldAutoMethod.AccessType.Write);
						this.<>2__current = new ValueTuple<MethodInfo, MemberInfo>(item, field);
						this.<>1__state = 6;
						return true;
					}
					goto IL_2C9;
				case 6:
					this.<>1__state = -1;
					goto IL_2C9;
				default:
					return false;
				}
				if (i >= array.Length)
				{
					array = null;
					array2 = properties;
					i = 0;
					goto IL_178;
				}
				MethodInfo methodInfo = array[i];
				this.<>2__current = new ValueTuple<MethodInfo, MemberInfo>(methodInfo, methodInfo);
				this.<>1__state = 1;
				return true;
				IL_12A:
				if (property.CanRead)
				{
					this.<>2__current = new ValueTuple<MethodInfo, MemberInfo>(property.GetMethod, property);
					this.<>1__state = 3;
					return true;
				}
				IL_163:
				property = null;
				i++;
				IL_178:
				if (i >= array2.Length)
				{
					array2 = null;
					array3 = fields;
					i = 0;
					goto IL_2DE;
				}
				property = array2[i];
				if (property.CanWrite)
				{
					this.<>2__current = new ValueTuple<MethodInfo, MemberInfo>(property.SetMethod, property);
					this.<>1__state = 2;
					return true;
				}
				goto IL_12A;
				IL_2C9:
				field = null;
				i++;
				IL_2DE:
				if (i >= array3.Length)
				{
					array3 = null;
					return false;
				}
				field = array3[i];
				if (!field.HasAttribute(true))
				{
					goto IL_2C9;
				}
				if (!field.IsDelegate())
				{
					FieldAutoMethod item2 = new FieldAutoMethod(field, FieldAutoMethod.AccessType.Read);
					this.<>2__current = new ValueTuple<MethodInfo, MemberInfo>(item2, field);
					this.<>1__state = 5;
					return true;
				}
				if (field.IsStrongDelegate())
				{
					FieldDelegateMethod item3 = new FieldDelegateMethod(field);
					this.<>2__current = new ValueTuple<MethodInfo, MemberInfo>(item3, field);
					this.<>1__state = 4;
					return true;
				}
				if (QuantumConsoleProcessor.loggingLevel >= LoggingLevel.Warnings)
				{
					UnityEngine.Debug.LogWarning(string.Format("Quantum Processor Warning: Could not add '{0}' from {1} to the table as it is an invalid delegate type.", field.Name, field.DeclaringType));
					goto IL_2C9;
				}
				goto IL_2C9;
			}

			// Token: 0x1700006D RID: 109
			// (get) Token: 0x060002DB RID: 731 RVA: 0x0000B726 File Offset: 0x00009926
			ValueTuple<MethodInfo, MemberInfo> IEnumerator<ValueTuple<MethodInfo, MemberInfo>>.Current
			{
				[DebuggerHidden]
				[return: TupleElementNames(new string[]
				{
					"method",
					"member"
				})]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060002DC RID: 732 RVA: 0x0000B72E File Offset: 0x0000992E
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700006E RID: 110
			// (get) Token: 0x060002DD RID: 733 RVA: 0x0000B735 File Offset: 0x00009935
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060002DE RID: 734 RVA: 0x0000B744 File Offset: 0x00009944
			[DebuggerHidden]
			[return: TupleElementNames(new string[]
			{
				"method",
				"member"
			})]
			IEnumerator<ValueTuple<MethodInfo, MemberInfo>> IEnumerable<ValueTuple<MethodInfo, MemberInfo>>.GetEnumerator()
			{
				QuantumConsoleProcessor.<ExtractCommandMethods>d__28 <ExtractCommandMethods>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<ExtractCommandMethods>d__ = this;
				}
				else
				{
					<ExtractCommandMethods>d__ = new QuantumConsoleProcessor.<ExtractCommandMethods>d__28(0);
				}
				<ExtractCommandMethods>d__.type = type;
				return <ExtractCommandMethods>d__;
			}

			// Token: 0x060002DF RID: 735 RVA: 0x0000B787 File Offset: 0x00009987
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<(System.Reflection.MethodInfomethod,System.Reflection.MemberInfomember)>.GetEnumerator();
			}

			// Token: 0x040001AF RID: 431
			private int <>1__state;

			// Token: 0x040001B0 RID: 432
			[TupleElementNames(new string[]
			{
				"method",
				"member"
			})]
			private ValueTuple<MethodInfo, MemberInfo> <>2__current;

			// Token: 0x040001B1 RID: 433
			private int <>l__initialThreadId;

			// Token: 0x040001B2 RID: 434
			private Type type;

			// Token: 0x040001B3 RID: 435
			public Type <>3__type;

			// Token: 0x040001B4 RID: 436
			private PropertyInfo[] <properties>5__2;

			// Token: 0x040001B5 RID: 437
			private FieldInfo[] <fields>5__3;

			// Token: 0x040001B6 RID: 438
			private MethodInfo[] <>7__wrap3;

			// Token: 0x040001B7 RID: 439
			private int <>7__wrap4;

			// Token: 0x040001B8 RID: 440
			private PropertyInfo[] <>7__wrap5;

			// Token: 0x040001B9 RID: 441
			private PropertyInfo <property>5__7;

			// Token: 0x040001BA RID: 442
			private FieldInfo[] <>7__wrap7;

			// Token: 0x040001BB RID: 443
			private FieldInfo <field>5__9;
		}

		// Token: 0x02000092 RID: 146
		[CompilerGenerated]
		private sealed class <CreateCommandOverloads>d__33 : IEnumerable<CommandData>, IEnumerable, IEnumerator<CommandData>, IEnumerator, IDisposable
		{
			// Token: 0x060002E0 RID: 736 RVA: 0x0000B78F File Offset: 0x0000998F
			[DebuggerHidden]
			public <CreateCommandOverloads>d__33(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x060002E1 RID: 737 RVA: 0x0000B7A9 File Offset: 0x000099A9
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060002E2 RID: 738 RVA: 0x0000B7AC File Offset: 0x000099AC
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					int num2 = i;
					i = num2 + 1;
				}
				else
				{
					this.<>1__state = -1;
					defaultParameters = method.GetParameters().Count(new Func<ParameterInfo, bool>(QuantumConsoleProcessor.<>c.<>9.<CreateCommandOverloads>b__33_0));
					i = 0;
				}
				if (i >= defaultParameters + 1)
				{
					return false;
				}
				CommandData commandData = new CommandData(method, commandAttribute, descriptionAttribute, i);
				this.<>2__current = commandData;
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x1700006F RID: 111
			// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000B864 File Offset: 0x00009A64
			CommandData IEnumerator<CommandData>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060002E4 RID: 740 RVA: 0x0000B86C File Offset: 0x00009A6C
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000070 RID: 112
			// (get) Token: 0x060002E5 RID: 741 RVA: 0x0000B873 File Offset: 0x00009A73
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060002E6 RID: 742 RVA: 0x0000B87C File Offset: 0x00009A7C
			[DebuggerHidden]
			IEnumerator<CommandData> IEnumerable<CommandData>.GetEnumerator()
			{
				QuantumConsoleProcessor.<CreateCommandOverloads>d__33 <CreateCommandOverloads>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<CreateCommandOverloads>d__ = this;
				}
				else
				{
					<CreateCommandOverloads>d__ = new QuantumConsoleProcessor.<CreateCommandOverloads>d__33(0);
				}
				<CreateCommandOverloads>d__.method = method;
				<CreateCommandOverloads>d__.commandAttribute = commandAttribute;
				<CreateCommandOverloads>d__.descriptionAttribute = descriptionAttribute;
				return <CreateCommandOverloads>d__;
			}

			// Token: 0x060002E7 RID: 743 RVA: 0x0000B8D7 File Offset: 0x00009AD7
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<QFSW.QC.CommandData>.GetEnumerator();
			}

			// Token: 0x040001BC RID: 444
			private int <>1__state;

			// Token: 0x040001BD RID: 445
			private CommandData <>2__current;

			// Token: 0x040001BE RID: 446
			private int <>l__initialThreadId;

			// Token: 0x040001BF RID: 447
			private MethodInfo method;

			// Token: 0x040001C0 RID: 448
			public MethodInfo <>3__method;

			// Token: 0x040001C1 RID: 449
			private CommandAttribute commandAttribute;

			// Token: 0x040001C2 RID: 450
			public CommandAttribute <>3__commandAttribute;

			// Token: 0x040001C3 RID: 451
			private CommandDescriptionAttribute descriptionAttribute;

			// Token: 0x040001C4 RID: 452
			public CommandDescriptionAttribute <>3__descriptionAttribute;

			// Token: 0x040001C5 RID: 453
			private int <defaultParameters>5__2;

			// Token: 0x040001C6 RID: 454
			private int <i>5__3;
		}

		// Token: 0x02000093 RID: 147
		[CompilerGenerated]
		private sealed class <>c__DisplayClass37_0
		{
			// Token: 0x060002E8 RID: 744 RVA: 0x0000B8DF File Offset: 0x00009ADF
			public <>c__DisplayClass37_0()
			{
			}

			// Token: 0x060002E9 RID: 745 RVA: 0x0000B8E7 File Offset: 0x00009AE7
			internal bool <InvokeCommand>b__1(string key)
			{
				return key.Contains(this.commandName + "(") && QuantumConsoleProcessor._commandTable[key].CommandName == this.commandName;
			}

			// Token: 0x040001C7 RID: 455
			public string commandName;
		}
	}
}
