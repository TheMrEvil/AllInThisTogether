using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x02000006 RID: 6
[CompilerGenerated]
internal sealed class <>f__AnonymousType4<<OperationId>j__TPar, <Operation>j__TPar, <ConnectionId>j__TPar, <Connection>j__TPar, <Statistics>j__TPar, <Timestamp>j__TPar>
{
	// Token: 0x17000015 RID: 21
	// (get) Token: 0x06000025 RID: 37 RVA: 0x000029AA File Offset: 0x00000BAA
	public <OperationId>j__TPar OperationId
	{
		get
		{
			return this.<OperationId>i__Field;
		}
	}

	// Token: 0x17000016 RID: 22
	// (get) Token: 0x06000026 RID: 38 RVA: 0x000029B2 File Offset: 0x00000BB2
	public <Operation>j__TPar Operation
	{
		get
		{
			return this.<Operation>i__Field;
		}
	}

	// Token: 0x17000017 RID: 23
	// (get) Token: 0x06000027 RID: 39 RVA: 0x000029BA File Offset: 0x00000BBA
	public <ConnectionId>j__TPar ConnectionId
	{
		get
		{
			return this.<ConnectionId>i__Field;
		}
	}

	// Token: 0x17000018 RID: 24
	// (get) Token: 0x06000028 RID: 40 RVA: 0x000029C2 File Offset: 0x00000BC2
	public <Connection>j__TPar Connection
	{
		get
		{
			return this.<Connection>i__Field;
		}
	}

	// Token: 0x17000019 RID: 25
	// (get) Token: 0x06000029 RID: 41 RVA: 0x000029CA File Offset: 0x00000BCA
	public <Statistics>j__TPar Statistics
	{
		get
		{
			return this.<Statistics>i__Field;
		}
	}

	// Token: 0x1700001A RID: 26
	// (get) Token: 0x0600002A RID: 42 RVA: 0x000029D2 File Offset: 0x00000BD2
	public <Timestamp>j__TPar Timestamp
	{
		get
		{
			return this.<Timestamp>i__Field;
		}
	}

	// Token: 0x0600002B RID: 43 RVA: 0x000029DA File Offset: 0x00000BDA
	[DebuggerHidden]
	public <>f__AnonymousType4(<OperationId>j__TPar OperationId, <Operation>j__TPar Operation, <ConnectionId>j__TPar ConnectionId, <Connection>j__TPar Connection, <Statistics>j__TPar Statistics, <Timestamp>j__TPar Timestamp)
	{
		this.<OperationId>i__Field = OperationId;
		this.<Operation>i__Field = Operation;
		this.<ConnectionId>i__Field = ConnectionId;
		this.<Connection>i__Field = Connection;
		this.<Statistics>i__Field = Statistics;
		this.<Timestamp>i__Field = Timestamp;
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00002A10 File Offset: 0x00000C10
	[DebuggerHidden]
	public override bool Equals(object value)
	{
		var <>f__AnonymousType = value as <>f__AnonymousType4<<OperationId>j__TPar, <Operation>j__TPar, <ConnectionId>j__TPar, <Connection>j__TPar, <Statistics>j__TPar, <Timestamp>j__TPar>;
		return <>f__AnonymousType != null && EqualityComparer<<OperationId>j__TPar>.Default.Equals(this.<OperationId>i__Field, <>f__AnonymousType.<OperationId>i__Field) && EqualityComparer<<Operation>j__TPar>.Default.Equals(this.<Operation>i__Field, <>f__AnonymousType.<Operation>i__Field) && EqualityComparer<<ConnectionId>j__TPar>.Default.Equals(this.<ConnectionId>i__Field, <>f__AnonymousType.<ConnectionId>i__Field) && EqualityComparer<<Connection>j__TPar>.Default.Equals(this.<Connection>i__Field, <>f__AnonymousType.<Connection>i__Field) && EqualityComparer<<Statistics>j__TPar>.Default.Equals(this.<Statistics>i__Field, <>f__AnonymousType.<Statistics>i__Field) && EqualityComparer<<Timestamp>j__TPar>.Default.Equals(this.<Timestamp>i__Field, <>f__AnonymousType.<Timestamp>i__Field);
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00002ABC File Offset: 0x00000CBC
	[DebuggerHidden]
	public override int GetHashCode()
	{
		return (((((682260145 * -1521134295 + EqualityComparer<<OperationId>j__TPar>.Default.GetHashCode(this.<OperationId>i__Field)) * -1521134295 + EqualityComparer<<Operation>j__TPar>.Default.GetHashCode(this.<Operation>i__Field)) * -1521134295 + EqualityComparer<<ConnectionId>j__TPar>.Default.GetHashCode(this.<ConnectionId>i__Field)) * -1521134295 + EqualityComparer<<Connection>j__TPar>.Default.GetHashCode(this.<Connection>i__Field)) * -1521134295 + EqualityComparer<<Statistics>j__TPar>.Default.GetHashCode(this.<Statistics>i__Field)) * -1521134295 + EqualityComparer<<Timestamp>j__TPar>.Default.GetHashCode(this.<Timestamp>i__Field);
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00002B58 File Offset: 0x00000D58
	[DebuggerHidden]
	[return: Nullable(1)]
	public override string ToString()
	{
		IFormatProvider provider = null;
		string format = "{{ OperationId = {0}, Operation = {1}, ConnectionId = {2}, Connection = {3}, Statistics = {4}, Timestamp = {5} }}";
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
		<Connection>j__TPar <Connection>j__TPar = this.<Connection>i__Field;
		array[num4] = ((<Connection>j__TPar != null) ? <Connection>j__TPar.ToString() : null);
		int num5 = 4;
		<Statistics>j__TPar <Statistics>j__TPar = this.<Statistics>i__Field;
		array[num5] = ((<Statistics>j__TPar != null) ? <Statistics>j__TPar.ToString() : null);
		int num6 = 5;
		<Timestamp>j__TPar <Timestamp>j__TPar = this.<Timestamp>i__Field;
		array[num6] = ((<Timestamp>j__TPar != null) ? <Timestamp>j__TPar.ToString() : null);
		return string.Format(provider, format, array);
	}

	// Token: 0x04000015 RID: 21
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <OperationId>j__TPar <OperationId>i__Field;

	// Token: 0x04000016 RID: 22
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Operation>j__TPar <Operation>i__Field;

	// Token: 0x04000017 RID: 23
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <ConnectionId>j__TPar <ConnectionId>i__Field;

	// Token: 0x04000018 RID: 24
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Connection>j__TPar <Connection>i__Field;

	// Token: 0x04000019 RID: 25
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Statistics>j__TPar <Statistics>i__Field;

	// Token: 0x0400001A RID: 26
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Timestamp>j__TPar <Timestamp>i__Field;
}
