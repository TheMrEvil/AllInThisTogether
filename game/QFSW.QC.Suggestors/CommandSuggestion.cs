using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

namespace QFSW.QC.Suggestors
{
	// Token: 0x02000004 RID: 4
	public class CommandSuggestion : IQcSuggestion
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002108 File Offset: 0x00000308
		public string FullSignature
		{
			get
			{
				return this._command.CommandSignature;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002115 File Offset: 0x00000315
		public string PrimarySignature
		{
			get
			{
				return this._command.CommandName;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002122 File Offset: 0x00000322
		public string SecondarySignature
		{
			[CompilerGenerated]
			get
			{
				return this.<SecondarySignature>k__BackingField;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000212A File Offset: 0x0000032A
		public CommandData Command
		{
			get
			{
				return this._command;
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002134 File Offset: 0x00000334
		public CommandSuggestion(CommandData command, int numOptionalParams = 0)
		{
			this._command = command;
			this._paramNames = this._command.ParameterSignature.Split(' ', StringSplitOptions.None);
			this._numOptionalParams = numOptionalParams;
			for (int i = this._paramNames.Length - this._numOptionalParams; i < this._paramNames.Length; i++)
			{
				this._paramNames[i] = "[" + this._paramNames[i] + "]";
			}
			this.SecondarySignature = this._command.GenericSignature + " " + string.Join(" ", this._paramNames);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021FA File Offset: 0x000003FA
		public bool MatchesPrompt(string prompt)
		{
			this.UpdateCurrentCache(prompt);
			return this._currentCommandNameCache.CommandName == this._command.CommandName;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000221E File Offset: 0x0000041E
		public string GetCompletion(string prompt)
		{
			return this._command.CommandName;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000222C File Offset: 0x0000042C
		public string GetCompletionTail(string prompt)
		{
			this.UpdateCurrentCache(prompt);
			this._stringBuilder.Clear();
			int num = prompt.SplitScoped(' ').Count((string x) => !string.IsNullOrWhiteSpace(x)) - 1;
			num = Mathf.Max(num, 0);
			int num2 = this._command.ParamCount - num;
			if (prompt == this._currentCommandNameCache.CommandName)
			{
				this._stringBuilder.Append(this._command.GenericSignature);
			}
			for (int i = 0; i < num2; i++)
			{
				if (i > 0 || !prompt.EndsWith(" "))
				{
					this._stringBuilder.Append(' ');
				}
				int num3 = i + num;
				this._stringBuilder.Append(this._paramNames[num3]);
			}
			return this._stringBuilder.ToString();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000230C File Offset: 0x0000050C
		public SuggestionContext? GetInnerSuggestionContext(SuggestionContext context)
		{
			this.UpdateCurrentCache(context.Prompt);
			bool flag = context.Prompt.EndsWith(" ") && context.Prompt.GetMaxScopeDepthAtEnd() == 0;
			string[] array = (from x in context.Prompt.SplitScoped(' ')
			where !string.IsNullOrWhiteSpace(x)
			select x).ToArray<string>();
			int num = array.Length - 1;
			if (flag)
			{
				num++;
			}
			if (num <= 0 || num > this._command.ParamCount)
			{
				return null;
			}
			int paramIndex = num - 1;
			SuggestionContext value = context;
			value.Depth++;
			value.TargetType = this.GetParameterType(paramIndex);
			value.Tags = this.GetParameterTags(paramIndex);
			value.Prompt = (flag ? string.Empty : array.LastOrDefault<string>());
			return new SuggestionContext?(value);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000023F8 File Offset: 0x000005F8
		private void UpdateCurrentCache(string prompt)
		{
			string text = prompt.SplitScopedFirst(' ');
			if (text != this._currentCommandNameCache.RawName)
			{
				this._currentCommandNameCache = this.ParseCommandNameInfo(text);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002430 File Offset: 0x00000630
		private CommandSuggestion.ParsedCommandNameInfo ParseCommandNameInfo(string rawName)
		{
			string[] array = rawName.Split(new char[]
			{
				'<'
			}, 2);
			CommandSuggestion.ParsedCommandNameInfo parsedCommandNameInfo = default(CommandSuggestion.ParsedCommandNameInfo);
			parsedCommandNameInfo.RawName = rawName;
			parsedCommandNameInfo.CommandName = array[0];
			if (this._command.IsGeneric)
			{
				parsedCommandNameInfo.GenericSignature = ((array.Length > 1) ? ("<" + array[1]) : "");
				parsedCommandNameInfo.GenericArgNames = parsedCommandNameInfo.GenericSignature.ReduceScope('<', '>').SplitScoped(',');
			}
			return parsedCommandNameInfo;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000024B6 File Offset: 0x000006B6
		private Type[] ParseGenericTypes(CommandSuggestion.ParsedCommandNameInfo commandNameInfo)
		{
			return commandNameInfo.GenericArgNames.Select(new Func<string, Type>(QuantumParser.ParseType)).ToArray<Type>();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000024D4 File Offset: 0x000006D4
		private Type[] GetParameterTypes(CommandSuggestion.ParsedCommandNameInfo commandNameInfo)
		{
			if (!this._command.IsGeneric)
			{
				return this._command.ParamTypes;
			}
			Type[] array;
			if (this._genericSignatureCache.TryGetValue(commandNameInfo.GenericSignature, out array))
			{
				return array;
			}
			try
			{
				Type[] genericTypeArguments = this.ParseGenericTypes(this._currentCommandNameCache);
				array = this._command.MakeGenericArguments(genericTypeArguments);
			}
			catch
			{
				array = this._command.ParamTypes;
			}
			return this._genericSignatureCache[commandNameInfo.GenericSignature] = array;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002564 File Offset: 0x00000764
		private Type GetParameterType(int paramIndex)
		{
			return this.GetParameterTypes(this._currentCommandNameCache)[paramIndex];
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002574 File Offset: 0x00000774
		private IQcSuggestorTag[] GetParameterTags(int paramIndex)
		{
			ParameterInfo parameterInfo = this._command.MethodParamData[paramIndex];
			IQcSuggestorTag[] result;
			if (this._parameterTagsCache.TryGetValue(parameterInfo, out result))
			{
				return result;
			}
			return this._parameterTagsCache[parameterInfo] = parameterInfo.GetCustomAttributes<SuggestorTagAttribute>().SelectMany((SuggestorTagAttribute x) => x.GetSuggestorTags()).ToArray<IQcSuggestorTag>();
		}

		// Token: 0x04000002 RID: 2
		private readonly CommandData _command;

		// Token: 0x04000003 RID: 3
		private readonly string[] _paramNames;

		// Token: 0x04000004 RID: 4
		private readonly int _numOptionalParams;

		// Token: 0x04000005 RID: 5
		private readonly Dictionary<string, Type[]> _genericSignatureCache = new Dictionary<string, Type[]>();

		// Token: 0x04000006 RID: 6
		private readonly Dictionary<ParameterInfo, IQcSuggestorTag[]> _parameterTagsCache = new Dictionary<ParameterInfo, IQcSuggestorTag[]>();

		// Token: 0x04000007 RID: 7
		private readonly StringBuilder _stringBuilder = new StringBuilder();

		// Token: 0x04000008 RID: 8
		private CommandSuggestion.ParsedCommandNameInfo _currentCommandNameCache;

		// Token: 0x04000009 RID: 9
		[CompilerGenerated]
		private readonly string <SecondarySignature>k__BackingField;

		// Token: 0x0200000E RID: 14
		private struct ParsedCommandNameInfo
		{
			// Token: 0x04000011 RID: 17
			public string RawName;

			// Token: 0x04000012 RID: 18
			public string CommandName;

			// Token: 0x04000013 RID: 19
			public string GenericSignature;

			// Token: 0x04000014 RID: 20
			public string[] GenericArgNames;
		}

		// Token: 0x0200000F RID: 15
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600003A RID: 58 RVA: 0x00002986 File Offset: 0x00000B86
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600003B RID: 59 RVA: 0x00002992 File Offset: 0x00000B92
			public <>c()
			{
			}

			// Token: 0x0600003C RID: 60 RVA: 0x0000299A File Offset: 0x00000B9A
			internal bool <GetCompletionTail>b__20_0(string x)
			{
				return !string.IsNullOrWhiteSpace(x);
			}

			// Token: 0x0600003D RID: 61 RVA: 0x000029A5 File Offset: 0x00000BA5
			internal bool <GetInnerSuggestionContext>b__21_0(string x)
			{
				return !string.IsNullOrWhiteSpace(x);
			}

			// Token: 0x0600003E RID: 62 RVA: 0x000029B0 File Offset: 0x00000BB0
			internal IEnumerable<IQcSuggestorTag> <GetParameterTags>b__27_0(SuggestorTagAttribute x)
			{
				return x.GetSuggestorTags();
			}

			// Token: 0x04000015 RID: 21
			public static readonly CommandSuggestion.<>c <>9 = new CommandSuggestion.<>c();

			// Token: 0x04000016 RID: 22
			public static Func<string, bool> <>9__20_0;

			// Token: 0x04000017 RID: 23
			public static Func<string, bool> <>9__21_0;

			// Token: 0x04000018 RID: 24
			public static Func<SuggestorTagAttribute, IEnumerable<IQcSuggestorTag>> <>9__27_0;
		}
	}
}
