using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace QFSW.QC
{
	// Token: 0x02000006 RID: 6
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
	public sealed class CommandAttribute : Attribute
	{
		// Token: 0x06000007 RID: 7 RVA: 0x0000214C File Offset: 0x0000034C
		public CommandAttribute([CallerMemberName] string aliasOverride = "", Platform supportedPlatforms = Platform.AllPlatforms, MonoTargetType targetType = MonoTargetType.Single)
		{
			this.Alias = aliasOverride;
			this.MonoTarget = targetType;
			this.SupportedPlatforms = supportedPlatforms;
			for (int i = 0; i < CommandAttribute._bannedAliasChars.Length; i++)
			{
				if (this.Alias.Contains(CommandAttribute._bannedAliasChars[i]))
				{
					string message = string.Format("Development Processor Error: Command with alias '{0}' contains the char '{1}' which is banned. Unexpected behaviour may occur.", this.Alias, CommandAttribute._bannedAliasChars[i]);
					Debug.LogError(message);
					this.Valid = false;
					throw new ArgumentException(message, "aliasOverride");
				}
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021D5 File Offset: 0x000003D5
		public CommandAttribute(string aliasOverride, MonoTargetType targetType, Platform supportedPlatforms = Platform.AllPlatforms) : this(aliasOverride, supportedPlatforms, targetType)
		{
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021E0 File Offset: 0x000003E0
		public CommandAttribute(string aliasOverride, string description, Platform supportedPlatforms = Platform.AllPlatforms, MonoTargetType targetType = MonoTargetType.Single) : this(aliasOverride, supportedPlatforms, targetType)
		{
			this.Description = description;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021F3 File Offset: 0x000003F3
		public CommandAttribute(string aliasOverride, string description, MonoTargetType targetType, Platform supportedPlatforms = Platform.AllPlatforms) : this(aliasOverride, description, supportedPlatforms, targetType)
		{
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002200 File Offset: 0x00000400
		// Note: this type is marked as 'beforefieldinit'.
		static CommandAttribute()
		{
		}

		// Token: 0x04000006 RID: 6
		public readonly string Alias;

		// Token: 0x04000007 RID: 7
		public readonly string Description;

		// Token: 0x04000008 RID: 8
		public readonly Platform SupportedPlatforms;

		// Token: 0x04000009 RID: 9
		public readonly MonoTargetType MonoTarget;

		// Token: 0x0400000A RID: 10
		public readonly bool Valid = true;

		// Token: 0x0400000B RID: 11
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
