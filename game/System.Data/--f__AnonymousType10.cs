using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

// Token: 0x0200000C RID: 12
[CompilerGenerated]
internal sealed class <>f__AnonymousType10<<OperationId>j__TPar, <Operation>j__TPar, <IsolationLevel>j__TPar, <Connection>j__TPar, <TransactionName>j__TPar, <Exception>j__TPar, <Timestamp>j__TPar>
{
	// Token: 0x17000039 RID: 57
	// (get) Token: 0x06000061 RID: 97 RVA: 0x00003A28 File Offset: 0x00001C28
	public <OperationId>j__TPar OperationId
	{
		get
		{
			return this.<OperationId>i__Field;
		}
	}

	// Token: 0x1700003A RID: 58
	// (get) Token: 0x06000062 RID: 98 RVA: 0x00003A30 File Offset: 0x00001C30
	public <Operation>j__TPar Operation
	{
		get
		{
			return this.<Operation>i__Field;
		}
	}

	// Token: 0x1700003B RID: 59
	// (get) Token: 0x06000063 RID: 99 RVA: 0x00003A38 File Offset: 0x00001C38
	public <IsolationLevel>j__TPar IsolationLevel
	{
		get
		{
			return this.<IsolationLevel>i__Field;
		}
	}

	// Token: 0x1700003C RID: 60
	// (get) Token: 0x06000064 RID: 100 RVA: 0x00003A40 File Offset: 0x00001C40
	public <Connection>j__TPar Connection
	{
		get
		{
			return this.<Connection>i__Field;
		}
	}

	// Token: 0x1700003D RID: 61
	// (get) Token: 0x06000065 RID: 101 RVA: 0x00003A48 File Offset: 0x00001C48
	public <TransactionName>j__TPar TransactionName
	{
		get
		{
			return this.<TransactionName>i__Field;
		}
	}

	// Token: 0x1700003E RID: 62
	// (get) Token: 0x06000066 RID: 102 RVA: 0x00003A50 File Offset: 0x00001C50
	public <Exception>j__TPar Exception
	{
		get
		{
			return this.<Exception>i__Field;
		}
	}

	// Token: 0x1700003F RID: 63
	// (get) Token: 0x06000067 RID: 103 RVA: 0x00003A58 File Offset: 0x00001C58
	public <Timestamp>j__TPar Timestamp
	{
		get
		{
			return this.<Timestamp>i__Field;
		}
	}

	// Token: 0x06000068 RID: 104 RVA: 0x00003A60 File Offset: 0x00001C60
	[DebuggerHidden]
	public <>f__AnonymousType10(<OperationId>j__TPar OperationId, <Operation>j__TPar Operation, <IsolationLevel>j__TPar IsolationLevel, <Connection>j__TPar Connection, <TransactionName>j__TPar TransactionName, <Exception>j__TPar Exception, <Timestamp>j__TPar Timestamp)
	{
		this.<OperationId>i__Field = OperationId;
		this.<Operation>i__Field = Operation;
		this.<IsolationLevel>i__Field = IsolationLevel;
		this.<Connection>i__Field = Connection;
		this.<TransactionName>i__Field = TransactionName;
		this.<Exception>i__Field = Exception;
		this.<Timestamp>i__Field = Timestamp;
	}

	// Token: 0x06000069 RID: 105 RVA: 0x00003AA0 File Offset: 0x00001CA0
	[DebuggerHidden]
	public override bool Equals(object value)
	{
		var <>f__AnonymousType = value as <>f__AnonymousType10<<OperationId>j__TPar, <Operation>j__TPar, <IsolationLevel>j__TPar, <Connection>j__TPar, <TransactionName>j__TPar, <Exception>j__TPar, <Timestamp>j__TPar>;
		return <>f__AnonymousType != null && EqualityComparer<<OperationId>j__TPar>.Default.Equals(this.<OperationId>i__Field, <>f__AnonymousType.<OperationId>i__Field) && EqualityComparer<<Operation>j__TPar>.Default.Equals(this.<Operation>i__Field, <>f__AnonymousType.<Operation>i__Field) && EqualityComparer<<IsolationLevel>j__TPar>.Default.Equals(this.<IsolationLevel>i__Field, <>f__AnonymousType.<IsolationLevel>i__Field) && EqualityComparer<<Connection>j__TPar>.Default.Equals(this.<Connection>i__Field, <>f__AnonymousType.<Connection>i__Field) && EqualityComparer<<TransactionName>j__TPar>.Default.Equals(this.<TransactionName>i__Field, <>f__AnonymousType.<TransactionName>i__Field) && EqualityComparer<<Exception>j__TPar>.Default.Equals(this.<Exception>i__Field, <>f__AnonymousType.<Exception>i__Field) && EqualityComparer<<Timestamp>j__TPar>.Default.Equals(this.<Timestamp>i__Field, <>f__AnonymousType.<Timestamp>i__Field);
	}

	// Token: 0x0600006A RID: 106 RVA: 0x00003B68 File Offset: 0x00001D68
	[DebuggerHidden]
	public override int GetHashCode()
	{
		return ((((((1756095211 * -1521134295 + EqualityComparer<<OperationId>j__TPar>.Default.GetHashCode(this.<OperationId>i__Field)) * -1521134295 + EqualityComparer<<Operation>j__TPar>.Default.GetHashCode(this.<Operation>i__Field)) * -1521134295 + EqualityComparer<<IsolationLevel>j__TPar>.Default.GetHashCode(this.<IsolationLevel>i__Field)) * -1521134295 + EqualityComparer<<Connection>j__TPar>.Default.GetHashCode(this.<Connection>i__Field)) * -1521134295 + EqualityComparer<<TransactionName>j__TPar>.Default.GetHashCode(this.<TransactionName>i__Field)) * -1521134295 + EqualityComparer<<Exception>j__TPar>.Default.GetHashCode(this.<Exception>i__Field)) * -1521134295 + EqualityComparer<<Timestamp>j__TPar>.Default.GetHashCode(this.<Timestamp>i__Field);
	}

	// Token: 0x0600006B RID: 107 RVA: 0x00003C1C File Offset: 0x00001E1C
	[DebuggerHidden]
	[return: Nullable(1)]
	public override string ToString()
	{
		IFormatProvider provider = null;
		string format = "{{ OperationId = {0}, Operation = {1}, IsolationLevel = {2}, Connection = {3}, TransactionName = {4}, Exception = {5}, Timestamp = {6} }}";
		object[] array = new object[7];
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
		<Exception>j__TPar <Exception>j__TPar = this.<Exception>i__Field;
		array[num6] = ((<Exception>j__TPar != null) ? <Exception>j__TPar.ToString() : null);
		int num7 = 6;
		<Timestamp>j__TPar <Timestamp>j__TPar = this.<Timestamp>i__Field;
		array[num7] = ((<Timestamp>j__TPar != null) ? <Timestamp>j__TPar.ToString() : null);
		return string.Format(provider, format, array);
	}

	// Token: 0x04000039 RID: 57
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <OperationId>j__TPar <OperationId>i__Field;

	// Token: 0x0400003A RID: 58
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Operation>j__TPar <Operation>i__Field;

	// Token: 0x0400003B RID: 59
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <IsolationLevel>j__TPar <IsolationLevel>i__Field;

	// Token: 0x0400003C RID: 60
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Connection>j__TPar <Connection>i__Field;

	// Token: 0x0400003D RID: 61
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <TransactionName>j__TPar <TransactionName>i__Field;

	// Token: 0x0400003E RID: 62
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Exception>j__TPar <Exception>i__Field;

	// Token: 0x0400003F RID: 63
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private readonly <Timestamp>j__TPar <Timestamp>i__Field;
}
