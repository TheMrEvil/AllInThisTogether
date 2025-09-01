using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x02000003 RID: 3
[CompilerGenerated]
internal sealed class <>f__AnonymousType1<<OperationId>j__TPar, <Operation>j__TPar, <ConnectionId>j__TPar, <Command>j__TPar, <Statistics>j__TPar, <Timestamp>j__TPar>
{
	// Token: 0x17000005 RID: 5
	// (get) Token: 0x06000009 RID: 9 RVA: 0x0000223E File Offset: 0x0000043E
	public <OperationId>j__TPar OperationId
	{
		get
		{
			return this.<OperationId>i__Field;
		}
	}

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x0600000A RID: 10 RVA: 0x00002246 File Offset: 0x00000446
	public <Operation>j__TPar Operation
	{
		get
		{
			return this.<Operation>i__Field;
		}
	}

	// Token: 0x17000007 RID: 7
	// (get) Token: 0x0600000B RID: 11 RVA: 0x0000224E File Offset: 0x0000044E
	public <ConnectionId>j__TPar ConnectionId
	{
		get
		{
			return this.<ConnectionId>i__Field;
		}
	}

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x0600000C RID: 12 RVA: 0x00002256 File Offset: 0x00000456
	public <Command>j__TPar Command
	{
		get
		{
			return this.<Command>i__Field;
		}
	}

	// Token: 0x17000009 RID: 9
	// (get) Token: 0x0600000D RID: 13 RVA: 0x0000225E File Offset: 0x0000045E
	public <Statistics>j__TPar Statistics
	{
		get
		{
			return this.<Statistics>i__Field;
		}
	}

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x0600000E RID: 14 RVA: 0x00002266 File Offset: 0x00000466
	public <Timestamp>j__TPar Timestamp
	{
		get
		{
			return this.<Timestamp>i__Field;
		}
	}

	// Token: 0x0600000F RID: 15 RVA: 0x0000226E File Offset: 0x0000046E
	[DebuggerHidden]
	public <>f__AnonymousType1(<OperationId>j__TPar OperationId, <Operation>j__TPar Operation, <ConnectionId>j__TPar ConnectionId, <Command>j__TPar Command, <Statistics>j__TPar Statistics, <Timestamp>j__TPar Timestamp)
	{
		this.<OperationId>i__Field = OperationId;
		this.<Operation>i__Field = Operation;
		this.<ConnectionId>i__Field = ConnectionId;
		this.<Command>i__Field = Command;
		this.<Statistics>i__Field = Statistics;
		this.<Timestamp>i__Field = Timestamp;
	}

	// Token: 0x06000010 RID: 16 RVA: 0x000022A4 File Offset: 0x000004A4
	[DebuggerHidden]
	public override bool Equals(object value)
	{
		var <>f__AnonymousType = value as <>f__AnonymousType1<<OperationId>j__TPar, <Operation>j__TPar, <ConnectionId>j__TPar, <Command>j__TPar, <Statistics>j__TPar, <Timestamp>j__TPar>;
		return <>f__AnonymousType != null && EqualityComparer<<OperationId>j__TPar>.Default.Equals(this.<OperationId>i__Field, <>f__AnonymousType.<OperationId>i__Field) && EqualityComparer<<Operation>j__TPar>.Default.Equals(this.<Operation>i__Field, <>f__AnonymousType.<Operation>i__Field) && EqualityComparer<<ConnectionId>j__TPar>.Default.Equals(this.<ConnectionId>i__Field, <>f__AnonymousType.<ConnectionId>i__Field) && EqualityComparer<<Command>j__TPar>.Default.Equals(this.<Command>i__Field, <>f__AnonymousType.<Command>i__Field) && EqualityComparer<<Statistics>j__TPar>.Default.Equals(this.<Statistics>i__Field, <>f__AnonymousType.<Statistics>i__Field) && EqualityComparer<<Timestamp>j__TPar>.Default.Equals(this.<Timestamp>i__Field, <>f__AnonymousType.<Timestamp>i__Field);
	}

	// Token: 0x06000011 RID: 17 RVA: 0x00002350 File Offset: 0x00000550
	[DebuggerHidden]
	public override int GetHashCode()
	{
		return (((((439063918 * -1521134295 + EqualityComparer<<OperationId>j__TPar>.Default.GetHashCode(this.<OperationId>i__Field)) * -1521134295 + EqualityComparer<<Operation>j__TPar>.Default.GetHashCode(this.<Operation>i__Field)) * -1521134295 + EqualityComparer<<ConnectionId>j__TPar>.Default.GetHashCode(this.<ConnectionId>i__Field)) * -1521134295 + EqualityComparer<<Command>j__TPar>.Default.GetHashCode(this.<Command>i__Field)) * -1521134295 + EqualityComparer<<Statistics>j__TPar>.Default.GetHashCode(this.<Statistics>i__Field)) * -1521134295 + EqualityComparer<<Timestamp>j__TPar>.Default.GetHashCode(this.<Timestamp>i__Field);
	}

	// Token: 0x06000012 RID: 18 RVA: 0x000023EC File Offset: 0x000005EC
	[DebuggerHidden]
	[return: Nullable(1)]
	public override string ToString()
	{
		IFormatProvider provider = null;
		string format = "{{ OperationId = {0}, Operation = {1}, ConnectionId = {2}, Command = {3}, Statistics = {4}, Timestamp = {5} }}";
		object[] array = new object[6];
		int num = 0;
		<OperationId>j__TPar <OperationId>j__TPar = this.<OperationId>i__Field;
		array[num] = ((<OperationId>j__TPar != null) ? <OperationId>j__TPar.ToString() : null);
		int num2 = 1;
		<Operation>j__TPar <Operation>j__TPar = this.<Operation>i__Field;
		array[num2] = ((<Operation>j__TPar != null) ? <Operation>j__TPar.ToString() : null);
		int num3 = 2;
		<ConnectionId>j__TPar <ConnectionId>j__TPar = this.<ConnectionId>i__Field;
		array[num3] = ((<ConnectionId>j__TPar != null) ? <ConnectionId>j__TPar.ToString() : null);
		int num4 = 3;
		<Command>j__TPar <Command>j__TPar = this.<Command>i__Field;
		array[num4] = ((<Command>j__TPar != null) ? <Command>j__TPar.ToString() : null);
		int num5 = 4;
		<Statistics>j__TPar <Statistics>j__TPar = this.<Statistics>i__Field;
		array[num5] = ((<Statistics>j__TPar != null) ? <Statistics>j__TPar.ToString() : null);
		int num6 = 5;
		<Timestamp>j__TPar <Timestamp>j__TPar = this.<Timestamp>i__Field;
		array[num6] = ((<Timestamp>j__TPar != null) ? <Timestamp>j__TPar.ToString() : null);
		return string.Format(provider, format, array);
	}

	// Token: 0x04000005 RID: 5
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <OperationId>j__TPar <OperationId>i__Field;

	// Token: 0x04000006 RID: 6
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Operation>j__TPar <Operation>i__Field;

	// Token: 0x04000007 RID: 7
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <ConnectionId>j__TPar <ConnectionId>i__Field;

	// Token: 0x04000008 RID: 8
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Command>j__TPar <Command>i__Field;

	// Token: 0x04000009 RID: 9
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Statistics>j__TPar <Statistics>i__Field;

	// Token: 0x0400000A RID: 10
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Timestamp>j__TPar <Timestamp>i__Field;
}
