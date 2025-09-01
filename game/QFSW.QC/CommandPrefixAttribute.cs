using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace QFSW.QC
{
	// Token: 0x0200000A RID: 10
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = false)]
	public sealed class CommandPrefixAttribute : Attribute
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002264 File Offset: 0x00000464
		public CommandPrefixAttribute([CallerMemberName] string prefixName = "")
		{
			this.Prefix = prefixName;
			foreach (char c in CommandPrefixAttribute._bannedAliasChars)
			{
				if (this.Prefix.Contains(c))
				{
					string message = string.Format("Development Processor Error: Command prefix '{0}' contains the char '{1}' which is banned. Unexpected behaviour may occurr.", this.Prefix, c);
					Debug.LogError(message);
					this.Valid = false;
					throw new ArgumentException(message, "prefixName");
				}
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022D9 File Offset: 0x000004D9
		// Note: this type is marked as 'beforefieldinit'.
		static CommandPrefixAttribute()
		{
		}

		// Token: 0x04000011 RID: 17
		public readonly string Prefix;

		// Token: 0x04000012 RID: 18
		public readonly bool Valid = true;

		// Token: 0x04000013 RID: 19
		private static readonly char[] _bannedAliasChars = new char[]
		{
			' ',
			'(',
			')',
			'{',
			'}',
			'[',
			']',
			'<',
			'>'
		};
	}
}
