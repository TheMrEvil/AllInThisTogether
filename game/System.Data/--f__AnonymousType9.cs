using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x0200000B RID: 11
[CompilerGenerated]
internal sealed class <>f__AnonymousType9<<OperationId>j__TPar, <Operation>j__TPar, <IsolationLevel>j__TPar, <Connection>j__TPar, <TransactionName>j__TPar, <Timestamp>j__TPar>
{
	// Token: 0x17000033 RID: 51
	// (get) Token: 0x06000057 RID: 87 RVA: 0x00003768 File Offset: 0x00001968
	public <OperationId>j__TPar OperationId
	{
		get
		{
			return this.<OperationId>i__Field;
		}
	}

	// Token: 0x17000034 RID: 52
	// (get) Token: 0x06000058 RID: 88 RVA: 0x00003770 File Offset: 0x00001970
	public <Operation>j__TPar Operation
	{
		get
		{
			return this.<Operation>i__Field;
		}
	}

	// Token: 0x17000035 RID: 53
	// (get) Token: 0x06000059 RID: 89 RVA: 0x00003778 File Offset: 0x00001978
	public <IsolationLevel>j__TPar IsolationLevel
	{
		get
		{
			return this.<IsolationLevel>i__Field;
		}
	}

	// Token: 0x17000036 RID: 54
	// (get) Token: 0x0600005A RID: 90 RVA: 0x00003780 File Offset: 0x00001980
	public <Connection>j__TPar Connection
	{
		get
		{
			return this.<Connection>i__Field;
		}
	}

	// Token: 0x17000037 RID: 55
	// (get) Token: 0x0600005B RID: 91 RVA: 0x00003788 File Offset: 0x00001988
	public <TransactionName>j__TPar TransactionName
	{
		get
		{
			return this.<TransactionName>i__Field;
		}
	}

	// Token: 0x17000038 RID: 56
	// (get) Token: 0x0600005C RID: 92 RVA: 0x00003790 File Offset: 0x00001990
	public <Timestamp>j__TPar Timestamp
	{
		get
		{
			return this.<Timestamp>i__Field;
		}
	}

	// Token: 0x0600005D RID: 93 RVA: 0x00003798 File Offset: 0x00001998
	[DebuggerHidden]
	public <>f__AnonymousType9(<OperationId>j__TPar OperationId, <Operation>j__TPar Operation, <IsolationLevel>j__TPar IsolationLevel, <Connection>j__TPar Connection, <TransactionName>j__TPar TransactionName, <Timestamp>j__TPar Timestamp)
	{
		this.<OperationId>i__Field = OperationId;
		this.<Operation>i__Field = Operation;
		this.<IsolationLevel>i__Field = IsolationLevel;
		this.<Connection>i__Field = Connection;
		this.<TransactionName>i__Field = TransactionName;
		this.<Timestamp>i__Field = Timestamp;
	}

	// Token: 0x0600005E RID: 94 RVA: 0x000037D0 File Offset: 0x000019D0
	[DebuggerHidden]
	public override bool Equals(object value)
	{
		var <>f__AnonymousType = value as <>f__AnonymousType9<<OperationId>j__TPar, <Operation>j__TPar, <IsolationLevel>j__TPar, <Connection>j__TPar, <TransactionName>j__TPar, <Timestamp>j__TPar>;
		return <>f__AnonymousType != null && EqualityComparer<<OperationId>j__TPar>.Default.Equals(this.<OperationId>i__Field, <>f__AnonymousType.<OperationId>i__Field) && EqualityComparer<<Operation>j__TPar>.Default.Equals(this.<Operation>i__Field, <>f__AnonymousType.<Operation>i__Field) && EqualityComparer<<IsolationLevel>j__TPar>.Default.Equals(this.<IsolationLevel>i__Field, <>f__AnonymousType.<IsolationLevel>i__Field) && EqualityComparer<<Connection>j__TPar>.Default.Equals(this.<Connection>i__Field, <>f__AnonymousType.<Connection>i__Field) && EqualityComparer<<TransactionName>j__TPar>.Default.Equals(this.<TransactionName>i__Field, <>f__AnonymousType.<TransactionName>i__Field) && EqualityComparer<<Timestamp>j__TPar>.Default.Equals(this.<Timestamp>i__Field, <>f__AnonymousType.<Timestamp>i__Field);
	}

	// Token: 0x0600005F RID: 95 RVA: 0x0000387C File Offset: 0x00001A7C
	[DebuggerHidden]
	public override int GetHashCode()
	{
		return (((((326743558 * -1521134295 + EqualityComparer<<OperationId>j__TPar>.Default.GetHashCode(this.<OperationId>i__Field)) * -1521134295 + EqualityComparer<<Operation>j__TPar>.Default.GetHashCode(this.<Operation>i__Field)) * -1521134295 + EqualityComparer<<IsolationLevel>j__TPar>.Default.GetHashCode(this.<IsolationLevel>i__Field)) * -1521134295 + EqualityComparer<<Connection>j__TPar>.Default.GetHashCode(this.<Connection>i__Field)) * -1521134295 + EqualityComparer<<TransactionName>j__TPar>.Default.GetHashCode(this.<TransactionName>i__Field)) * -1521134295 + EqualityComparer<<Timestamp>j__TPar>.Default.GetHashCode(this.<Timestamp>i__Field);
	}

	// Token: 0x06000060 RID: 96 RVA: 0x00003918 File Offset: 0x00001B18
	[DebuggerHidden]
	[return: Nullable(1)]
	public override string ToString()
	{
		IFormatProvider provider = null;
		string format = "{{ OperationId = {0}, Operation = {1}, IsolationLevel = {2}, Connection = {3}, TransactionName = {4}, Timestamp = {5} }}";
		object[] array = new object[6];
		int num = 0;
		<OperationId>j__TPar <OperationId>j__TPar = this.<OperationId>i__Field;
		array[num] = ((<OperationId>j__TPar != null) ? <OperationId>j__TPar.ToString() : null);
		int num2 = 1;
		<Operation>j__TPar <Operation>j__TPar = this.<Operation>i__Field;
		array[num2] = ((<Operation>j__TPar != null) ? <Operation>j__TPar.ToString() : null);
		int num3 = 2;
		<IsolationLevel>j__TPar <IsolationLevel>j__TPar = this.<IsolationLevel>i__Field;
		array[num3] = ((<IsolationLevel>j__TPar != null) ? <IsolationLevel>j__TPar.ToString() : null);
		int num4 = 3;
		<Connection>j__TPar <Connection>j__TPar = this.<Connection>i__Field;
		array[num4] = ((<Connection>j__TPar != null) ? <Connection>j__TPar.ToString() : null);
		int num5 = 4;
		<TransactionName>j__TPar <TransactionName>j__TPar = this.<TransactionName>i__Field;
		array[num5] = ((<TransactionName>j__TPar != null) ? <TransactionName>j__TPar.ToString() : null);
		int num6 = 5;
		<Timestamp>j__TPar <Timestamp>j__TPar = this.<Timestamp>i__Field;
		array[num6] = ((<Timestamp>j__TPar != null) ? <Timestamp>j__TPar.ToString() : null);
		return string.Format(provider, format, array);
	}

	// Token: 0x04000033 RID: 51
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <OperationId>j__TPar <OperationId>i__Field;

	// Token: 0x04000034 RID: 52
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Operation>j__TPar <Operation>i__Field;

	// Token: 0x04000035 RID: 53
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <IsolationLevel>j__TPar <IsolationLevel>i__Field;

	// Token: 0x04000036 RID: 54
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Connection>j__TPar <Connection>i__Field;

	// Token: 0x04000037 RID: 55
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <TransactionName>j__TPar <TransactionName>i__Field;

	// Token: 0x04000038 RID: 56
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Timestamp>j__TPar <Timestamp>i__Field;
}
