using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x02000004 RID: 4
[CompilerGenerated]
internal sealed class <>f__AnonymousType2<<obj>j__TPar, <result>j__TPar>
{
	// Token: 0x17000003 RID: 3
	// (get) Token: 0x0600000B RID: 11 RVA: 0x0000217E File Offset: 0x0000037E
	public <obj>j__TPar obj
	{
		get
		{
			return this.<obj>i__Field;
		}
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x0600000C RID: 12 RVA: 0x00002186 File Offset: 0x00000386
	public <result>j__TPar result
	{
		get
		{
			return this.<result>i__Field;
		}
	}

	// Token: 0x0600000D RID: 13 RVA: 0x0000218E File Offset: 0x0000038E
	[DebuggerHidden]
	public <>f__AnonymousType2(<obj>j__TPar obj, <result>j__TPar result)
	{
		this.<obj>i__Field = obj;
		this.<result>i__Field = result;
	}

	// Token: 0x0600000E RID: 14 RVA: 0x000021A4 File Offset: 0x000003A4
	[DebuggerHidden]
	public override bool Equals(object value)
	{
		var <>f__AnonymousType = value as <>f__AnonymousType2<<obj>j__TPar, <result>j__TPar>;
		return <>f__AnonymousType != null && EqualityComparer<<obj>j__TPar>.Default.Equals(this.<obj>i__Field, <>f__AnonymousType.<obj>i__Field) && EqualityComparer<<result>j__TPar>.Default.Equals(this.<result>i__Field, <>f__AnonymousType.<result>i__Field);
	}

	// Token: 0x0600000F RID: 15 RVA: 0x000021EB File Offset: 0x000003EB
	[DebuggerHidden]
	public override int GetHashCode()
	{
		return (-1183400040 * -1521134295 + EqualityComparer<<obj>j__TPar>.Default.GetHashCode(this.<obj>i__Field)) * -1521134295 + EqualityComparer<<result>j__TPar>.Default.GetHashCode(this.<result>i__Field);
	}

	// Token: 0x06000010 RID: 16 RVA: 0x00002220 File Offset: 0x00000420
	[DebuggerHidden]
	public override string ToString()
	{
		IFormatProvider provider = null;
		string format = "{{ obj = {0}, result = {1} }}";
		object[] array = new object[2];
		int num = 0;
		<obj>j__TPar <obj>j__TPar = this.<obj>i__Field;
		array[num] = ((<obj>j__TPar != null) ? <obj>j__TPar.ToString() : null);
		int num2 = 1;
		<result>j__TPar <result>j__TPar = this.<result>i__Field;
		array[num2] = ((<result>j__TPar != null) ? <result>j__TPar.ToString() : null);
		return string.Format(provider, format, array);
	}

	// Token: 0x04000003 RID: 3
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <obj>j__TPar <obj>i__Field;

	// Token: 0x04000004 RID: 4
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <result>j__TPar <result>i__Field;
}
