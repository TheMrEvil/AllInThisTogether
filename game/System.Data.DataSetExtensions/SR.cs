using System;

// Token: 0x02000004 RID: 4
internal class SR
{
	// Token: 0x0600000F RID: 15 RVA: 0x000021DF File Offset: 0x000003DF
	public SR()
	{
	}

	// Token: 0x04000030 RID: 48
	public const string DataSetLinq_InvalidEnumerationValue = "The {0} enumeration value, {1}, is not valid.";

	// Token: 0x04000031 RID: 49
	public const string DataSetLinq_EmptyDataRowSource = "The source contains no DataRows.";

	// Token: 0x04000032 RID: 50
	public const string DataSetLinq_NullDataRow = "The source contains a DataRow reference that is null.";

	// Token: 0x04000033 RID: 51
	public const string DataSetLinq_CannotLoadDetachedRow = "The source contains a detached DataRow that cannot be copied to the DataTable.";

	// Token: 0x04000034 RID: 52
	public const string DataSetLinq_CannotCompareDeletedRow = "The DataRowComparer does not work with DataRows that have been deleted since it only compares current values.";

	// Token: 0x04000035 RID: 53
	public const string DataSetLinq_CannotLoadDeletedRow = "The source contains a deleted DataRow that cannot be copied to the DataTable.";

	// Token: 0x04000036 RID: 54
	public const string DataSetLinq_NonNullableCast = "Cannot cast DBNull. Value to type '{0}'. Please use a nullable type.";
}
