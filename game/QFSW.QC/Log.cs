using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace QFSW.QC
{
	// Token: 0x02000016 RID: 22
	public readonly struct Log : ILog
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002DD8 File Offset: 0x00000FD8
		public string Text
		{
			[CompilerGenerated]
			get
			{
				return this.<Text>k__BackingField;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002DE0 File Offset: 0x00000FE0
		public LogType Type
		{
			[CompilerGenerated]
			get
			{
				return this.<Type>k__BackingField;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002DE8 File Offset: 0x00000FE8
		public bool NewLine
		{
			[CompilerGenerated]
			get
			{
				return this.<NewLine>k__BackingField;
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002DF0 File Offset: 0x00000FF0
		public Log(string text, LogType type = LogType.Log, bool newLine = true)
		{
			this.Text = text;
			this.Type = type;
			this.NewLine = newLine;
		}

		// Token: 0x04000026 RID: 38
		[CompilerGenerated]
		private readonly string <Text>k__BackingField;

		// Token: 0x04000027 RID: 39
		[CompilerGenerated]
		private readonly LogType <Type>k__BackingField;

		// Token: 0x04000028 RID: 40
		[CompilerGenerated]
		private readonly bool <NewLine>k__BackingField;
	}
}
