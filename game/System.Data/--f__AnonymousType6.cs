using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x02000008 RID: 8
[CompilerGenerated]
internal sealed class <>f__AnonymousType6<<OperationId>j__TPar, <Operation>j__TPar, <ConnectionId>j__TPar, <Connection>j__TPar, <Statistics>j__TPar, <Exception>j__TPar, <Timestamp>j__TPar>
{
	// Token: 0x17000021 RID: 33
	// (get) Token: 0x06000039 RID: 57 RVA: 0x00002F28 File Offset: 0x00001128
	public <OperationId>j__TPar OperationId
	{
		get
		{
			return this.<OperationId>i__Field;
		}
	}

	// Token: 0x17000022 RID: 34
	// (get) Token: 0x0600003A RID: 58 RVA: 0x00002F30 File Offset: 0x00001130
	public <Operation>j__TPar Operation
	{
		get
		{
			return this.<Operation>i__Field;
		}
	}

	// Token: 0x17000023 RID: 35
	// (get) Token: 0x0600003B RID: 59 RVA: 0x00002F38 File Offset: 0x00001138
	public <ConnectionId>j__TPar ConnectionId
	{
		get
		{
			return this.<ConnectionId>i__Field;
		}
	}

	// Token: 0x17000024 RID: 36
	// (get) Token: 0x0600003C RID: 60 RVA: 0x00002F40 File Offset: 0x00001140
	public <Connection>j__TPar Connection
	{
		get
		{
			return this.<Connection>i__Field;
		}
	}

	// Token: 0x17000025 RID: 37
	// (get) Token: 0x0600003D RID: 61 RVA: 0x00002F48 File Offset: 0x00001148
	public <Statistics>j__TPar Statistics
	{
		get
		{
			return this.<Statistics>i__Field;
		}
	}

	// Token: 0x17000026 RID: 38
	// (get) Token: 0x0600003E RID: 62 RVA: 0x00002F50 File Offset: 0x00001150
	public <Exception>j__TPar Exception
	{
		get
		{
			return this.<Exception>i__Field;
		}
	}

	// Token: 0x17000027 RID: 39
	// (get) Token: 0x0600003F RID: 63 RVA: 0x00002F58 File Offset: 0x00001158
	public <Timestamp>j__TPar Timestamp
	{
		get
		{
			return this.<Timestamp>i__Field;
		}
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00002F60 File Offset: 0x00001160
	[DebuggerHidden]
	public <>f__AnonymousType6(<OperationId>j__TPar OperationId, <Operation>j__TPar Operation, <ConnectionId>j__TPar ConnectionId, <Connection>j__TPar Connection, <Statistics>j__TPar Statistics, <Exception>j__TPar Exception, <Timestamp>j__TPar Timestamp)
	{
		this.<OperationId>i__Field = OperationId;
		this.<Operation>i__Field = Operation;
		this.<ConnectionId>i__Field = ConnectionId;
		this.<Connection>i__Field = Connection;
		this.<Statistics>i__Field = Statistics;
		this.<Exception>i__Field = Exception;
		this.<Timestamp>i__Field = Timestamp;
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00002FA0 File Offset: 0x000011A0
	[DebuggerHidden]
	public override bool Equals(object value)
	{
		var <>f__AnonymousType = value as <>f__AnonymousType6<<OperationId>j__TPar, <Operation>j__TPar, <ConnectionId>j__TPar, <Connection>j__TPar, <Statistics>j__TPar, <Exception>j__TPar, <Timestamp>j__TPar>;
		return <>f__AnonymousType != null && EqualityComparer<<OperationId>j__TPar>.Default.Equals(this.<OperationId>i__Field, <>f__AnonymousType.<OperationId>i__Field) && EqualityComparer<<Operation>j__TPar>.Default.Equals(this.<Operation>i__Field, <>f__AnonymousType.<Operation>i__Field) && EqualityComparer<<ConnectionId>j__TPar>.Default.Equals(this.<ConnectionId>i__Field, <>f__AnonymousType.<ConnectionId>i__Field) && EqualityComparer<<Connection>j__TPar>.Default.Equals(this.<Connection>i__Field, <>f__AnonymousType.<Connection>i__Field) && EqualityComparer<<Statistics>j__TPar>.Default.Equals(this.<Statistics>i__Field, <>f__AnonymousType.<Statistics>i__Field) && EqualityComparer<<Exception>j__TPar>.Default.Equals(this.<Exception>i__Field, <>f__AnonymousType.<Exception>i__Field) && EqualityComparer<<Timestamp>j__TPar>.Default.Equals(this.<Timestamp>i__Field, <>f__AnonymousType.<Timestamp>i__Field);
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00003068 File Offset: 0x00001268
	[DebuggerHidden]
	public override int GetHashCode()
	{
		return ((((((758809934 * -1521134295 + EqualityComparer<<OperationId>j__TPar>.Default.GetHashCode(this.<OperationId>i__Field)) * -1521134295 + EqualityComparer<<Operation>j__TPar>.Default.GetHashCode(this.<Operation>i__Field)) * -1521134295 + EqualityComparer<<ConnectionId>j__TPar>.Default.GetHashCode(this.<ConnectionId>i__Field)) * -1521134295 + EqualityComparer<<Connection>j__TPar>.Default.GetHashCode(this.<Connection>i__Field)) * -1521134295 + EqualityComparer<<Statistics>j__TPar>.Default.GetHashCode(this.<Statistics>i__Field)) * -1521134295 + EqualityComparer<<Exception>j__TPar>.Default.GetHashCode(this.<Exception>i__Field)) * -1521134295 + EqualityComparer<<Timestamp>j__TPar>.Default.GetHashCode(this.<Timestamp>i__Field);
	}

	// Token: 0x06000043 RID: 67 RVA: 0x0000311C File Offset: 0x0000131C
	[DebuggerHidden]
	[return: Nullable(1)]
	public override string ToString()
	{
		IFormatProvider provider = null;
		string format = "{{ OperationId = {0}, Operation = {1}, ConnectionId = {2}, Connection = {3}, Statistics = {4}, Exception = {5}, Timestamp = {6} }}";
		object[] array = new object[7];
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
		<Exception>j__TPar <Exception>j__TPar = this.<Exception>i__Field;
		array[num6] = ((<Exception>j__TPar != null) ? <Exception>j__TPar.ToString() : null);
		int num7 = 6;
		<Timestamp>j__TPar <Timestamp>j__TPar = this.<Timestamp>i__Field;
		array[num7] = ((<Timestamp>j__TPar != null) ? <Timestamp>j__TPar.ToString() : null);
		return string.Format(provider, format, array);
	}

	// Token: 0x04000021 RID: 33
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <OperationId>j__TPar <OperationId>i__Field;

	// Token: 0x04000022 RID: 34
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Operation>j__TPar <Operation>i__Field;

	// Token: 0x04000023 RID: 35
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <ConnectionId>j__TPar <ConnectionId>i__Field;

	// Token: 0x04000024 RID: 36
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Connection>j__TPar <Connection>i__Field;

	// Token: 0x04000025 RID: 37
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Statistics>j__TPar <Statistics>i__Field;

	// Token: 0x04000026 RID: 38
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Exception>j__TPar <Exception>i__Field;

	// Token: 0x04000027 RID: 39
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Timestamp>j__TPar <Timestamp>i__Field;
}
