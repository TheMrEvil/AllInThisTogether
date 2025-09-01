using System;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;

namespace System.Data
{
	// Token: 0x02000093 RID: 147
	internal static class ExceptionBuilder
	{
		// Token: 0x06000700 RID: 1792 RVA: 0x0001A853 File Offset: 0x00018A53
		private static void TraceException(string trace, Exception e)
		{
			if (e != null)
			{
				DataCommonEventSource.Log.Trace<Exception>(trace, e);
			}
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0001A864 File Offset: 0x00018A64
		internal static Exception TraceExceptionAsReturnValue(Exception e)
		{
			ExceptionBuilder.TraceException("<comm.ADP.TraceException|ERR|THROW> '{0}'", e);
			return e;
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0001A872 File Offset: 0x00018A72
		internal static Exception TraceExceptionForCapture(Exception e)
		{
			ExceptionBuilder.TraceException("<comm.ADP.TraceException|ERR|CATCH> '{0}'", e);
			return e;
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0001A872 File Offset: 0x00018A72
		internal static Exception TraceExceptionWithoutRethrow(Exception e)
		{
			ExceptionBuilder.TraceException("<comm.ADP.TraceException|ERR|CATCH> '{0}'", e);
			return e;
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0001A880 File Offset: 0x00018A80
		internal static Exception _Argument(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new ArgumentException(error));
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0001A88D File Offset: 0x00018A8D
		internal static Exception _Argument(string paramName, string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new ArgumentException(error));
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0001A89A File Offset: 0x00018A9A
		internal static Exception _Argument(string error, Exception innerException)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new ArgumentException(error, innerException));
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0001A8A8 File Offset: 0x00018AA8
		private static Exception _ArgumentNull(string paramName, string msg)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new ArgumentNullException(paramName, msg));
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0001A8B6 File Offset: 0x00018AB6
		internal static Exception _ArgumentOutOfRange(string paramName, string msg)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new ArgumentOutOfRangeException(paramName, msg));
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0001A8C4 File Offset: 0x00018AC4
		private static Exception _IndexOutOfRange(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new IndexOutOfRangeException(error));
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0001A8D1 File Offset: 0x00018AD1
		private static Exception _InvalidOperation(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new InvalidOperationException(error));
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0001A8DE File Offset: 0x00018ADE
		private static Exception _InvalidEnumArgumentException(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new InvalidEnumArgumentException(error));
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001A8EB File Offset: 0x00018AEB
		private static Exception _InvalidEnumArgumentException<T>(T value)
		{
			return ExceptionBuilder._InvalidEnumArgumentException(SR.Format("The {0} enumeration value, {1}, is invalid.", typeof(T).Name, value.ToString()));
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0001A918 File Offset: 0x00018B18
		private static void ThrowDataException(string error, Exception innerException)
		{
			throw ExceptionBuilder.TraceExceptionAsReturnValue(new DataException(error, innerException));
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0001A926 File Offset: 0x00018B26
		private static Exception _Data(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new DataException(error));
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0001A933 File Offset: 0x00018B33
		private static Exception _Constraint(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new ConstraintException(error));
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0001A940 File Offset: 0x00018B40
		private static Exception _InvalidConstraint(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new InvalidConstraintException(error));
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0001A94D File Offset: 0x00018B4D
		private static Exception _DeletedRowInaccessible(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new DeletedRowInaccessibleException(error));
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0001A95A File Offset: 0x00018B5A
		private static Exception _DuplicateName(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new DuplicateNameException(error));
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0001A967 File Offset: 0x00018B67
		private static Exception _InRowChangingEvent(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new InRowChangingEventException(error));
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0001A974 File Offset: 0x00018B74
		private static Exception _MissingPrimaryKey(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new MissingPrimaryKeyException(error));
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0001A981 File Offset: 0x00018B81
		private static Exception _NoNullAllowed(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new NoNullAllowedException(error));
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0001A98E File Offset: 0x00018B8E
		private static Exception _ReadOnly(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new ReadOnlyException(error));
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0001A99B File Offset: 0x00018B9B
		private static Exception _RowNotInTable(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new RowNotInTableException(error));
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0001A9A8 File Offset: 0x00018BA8
		private static Exception _VersionNotFound(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new VersionNotFoundException(error));
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0001A9B5 File Offset: 0x00018BB5
		public static Exception ArgumentNull(string paramName)
		{
			return ExceptionBuilder._ArgumentNull(paramName, SR.Format("'{0}' argument cannot be null.", paramName));
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0001A9C8 File Offset: 0x00018BC8
		public static Exception ArgumentOutOfRange(string paramName)
		{
			return ExceptionBuilder._ArgumentOutOfRange(paramName, SR.Format("'{0}' argument is out of range.", paramName));
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0001A9DB File Offset: 0x00018BDB
		public static Exception BadObjectPropertyAccess(string error)
		{
			return ExceptionBuilder._InvalidOperation(SR.Format("Property not accessible because '{0}'.", error));
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0001A9ED File Offset: 0x00018BED
		public static Exception ArgumentContainsNull(string paramName)
		{
			return ExceptionBuilder._Argument(paramName, SR.Format("'{0}' argument contains null value.", paramName));
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0001AA00 File Offset: 0x00018C00
		public static Exception TypeNotAllowed(Type type)
		{
			return ExceptionBuilder._InvalidOperation(SR.Format("Type '{0}' is not allowed here. See https://go.microsoft.com/fwlink/?linkid=2132227 for more details.", type.AssemblyQualifiedName));
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0001AA17 File Offset: 0x00018C17
		public static Exception CannotModifyCollection()
		{
			return ExceptionBuilder._Argument("Collection itself is not modifiable.");
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0001AA23 File Offset: 0x00018C23
		public static Exception CaseInsensitiveNameConflict(string name)
		{
			return ExceptionBuilder._Argument(SR.Format("The given name '{0}' matches at least two names in the collection object with different cases, but does not match either of them with the same case.", name));
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0001AA35 File Offset: 0x00018C35
		public static Exception NamespaceNameConflict(string name)
		{
			return ExceptionBuilder._Argument(SR.Format("The given name '{0}' matches at least two names in the collection object with different namespaces.", name));
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0001AA47 File Offset: 0x00018C47
		public static Exception InvalidOffsetLength()
		{
			return ExceptionBuilder._Argument("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0001AA53 File Offset: 0x00018C53
		public static Exception ColumnNotInTheTable(string column, string table)
		{
			return ExceptionBuilder._Argument(SR.Format("Column '{0}' does not belong to table {1}.", column, table));
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0001AA66 File Offset: 0x00018C66
		public static Exception ColumnNotInAnyTable()
		{
			return ExceptionBuilder._Argument("Column must belong to a table.");
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0001AA72 File Offset: 0x00018C72
		public static Exception ColumnOutOfRange(int index)
		{
			return ExceptionBuilder._IndexOutOfRange(SR.Format("Cannot find column {0}.", index.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0001AA8F File Offset: 0x00018C8F
		public static Exception ColumnOutOfRange(string column)
		{
			return ExceptionBuilder._IndexOutOfRange(SR.Format("Cannot find column {0}.", column));
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0001AAA1 File Offset: 0x00018CA1
		public static Exception CannotAddColumn1(string column)
		{
			return ExceptionBuilder._Argument(SR.Format("Column '{0}' already belongs to this DataTable.", column));
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0001AAB3 File Offset: 0x00018CB3
		public static Exception CannotAddColumn2(string column)
		{
			return ExceptionBuilder._Argument(SR.Format("Column '{0}' already belongs to another DataTable.", column));
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0001AAC5 File Offset: 0x00018CC5
		public static Exception CannotAddColumn3()
		{
			return ExceptionBuilder._Argument("Cannot have more than one SimpleContent columns in a DataTable.");
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0001AAD1 File Offset: 0x00018CD1
		public static Exception CannotAddColumn4(string column)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot add a SimpleContent column to a table containing element columns or nested relations.", column));
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0001AAE3 File Offset: 0x00018CE3
		public static Exception CannotAddDuplicate(string column)
		{
			return ExceptionBuilder._DuplicateName(SR.Format("A column named '{0}' already belongs to this DataTable.", column));
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0001AAF5 File Offset: 0x00018CF5
		public static Exception CannotAddDuplicate2(string table)
		{
			return ExceptionBuilder._DuplicateName(SR.Format("Cannot add a column named '{0}': a nested table with the same name already belongs to this DataTable.", table));
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0001AB07 File Offset: 0x00018D07
		public static Exception CannotAddDuplicate3(string table)
		{
			return ExceptionBuilder._DuplicateName(SR.Format("A column named '{0}' already belongs to this DataTable: cannot set a nested table name to the same name.", table));
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0001AB19 File Offset: 0x00018D19
		public static Exception CannotRemoveColumn()
		{
			return ExceptionBuilder._Argument("Cannot remove a column that doesn't belong to this table.");
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0001AB25 File Offset: 0x00018D25
		public static Exception CannotRemovePrimaryKey()
		{
			return ExceptionBuilder._Argument("Cannot remove this column, because it's part of the primary key.");
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0001AB31 File Offset: 0x00018D31
		public static Exception CannotRemoveChildKey(string relation)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot remove this column, because it is part of the parent key for relationship {0}.", relation));
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0001AB43 File Offset: 0x00018D43
		public static Exception CannotRemoveConstraint(string constraint, string table)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot remove this column, because it is a part of the constraint {0} on the table {1}.", constraint, table));
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0001AB56 File Offset: 0x00018D56
		public static Exception CannotRemoveExpression(string column, string expression)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot remove this column, because it is part of an expression: {0} = {1}.", column, expression));
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0001AB69 File Offset: 0x00018D69
		public static Exception ColumnNotInTheUnderlyingTable(string column, string table)
		{
			return ExceptionBuilder._Argument(SR.Format("Column '{0}' does not belong to underlying table '{1}'.", column, table));
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0001AB7C File Offset: 0x00018D7C
		public static Exception InvalidOrdinal(string name, int ordinal)
		{
			return ExceptionBuilder._ArgumentOutOfRange(name, SR.Format("Ordinal '{0}' exceeds the maximum number.", ordinal.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0001AB9A File Offset: 0x00018D9A
		public static Exception AddPrimaryKeyConstraint()
		{
			return ExceptionBuilder._Argument("Cannot add primary key constraint since primary key is already set for the table.");
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0001ABA6 File Offset: 0x00018DA6
		public static Exception NoConstraintName()
		{
			return ExceptionBuilder._Argument("Cannot change the name of a constraint to empty string when it is in the ConstraintCollection.");
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0001ABB2 File Offset: 0x00018DB2
		public static Exception ConstraintViolation(string constraint)
		{
			return ExceptionBuilder._Constraint(SR.Format("Cannot enforce constraints on constraint {0}.", constraint));
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0001ABC4 File Offset: 0x00018DC4
		public static Exception ConstraintNotInTheTable(string constraint)
		{
			return ExceptionBuilder._Argument(SR.Format("Constraint '{0}' does not belong to this DataTable.", constraint));
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0001ABD8 File Offset: 0x00018DD8
		public static string KeysToString(object[] keys)
		{
			string text = string.Empty;
			for (int i = 0; i < keys.Length; i++)
			{
				text = text + Convert.ToString(keys[i], null) + ((i < keys.Length - 1) ? ", " : string.Empty);
			}
			return text;
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0001AC20 File Offset: 0x00018E20
		public static string UniqueConstraintViolationText(DataColumn[] columns, object[] values)
		{
			if (columns.Length > 1)
			{
				string text = string.Empty;
				for (int i = 0; i < columns.Length; i++)
				{
					text = text + columns[i].ColumnName + ((i < columns.Length - 1) ? ", " : "");
				}
				return SR.Format("Column '{0}' is constrained to be unique.  Value '{1}' is already present.", text, ExceptionBuilder.KeysToString(values));
			}
			return SR.Format("Column '{0}' is constrained to be unique.  Value '{1}' is already present.", columns[0].ColumnName, Convert.ToString(values[0], null));
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0001AC97 File Offset: 0x00018E97
		public static Exception ConstraintViolation(DataColumn[] columns, object[] values)
		{
			return ExceptionBuilder._Constraint(ExceptionBuilder.UniqueConstraintViolationText(columns, values));
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0001ACA5 File Offset: 0x00018EA5
		public static Exception ConstraintOutOfRange(int index)
		{
			return ExceptionBuilder._IndexOutOfRange(SR.Format("Cannot find constraint {0}.", index.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0001ACC2 File Offset: 0x00018EC2
		public static Exception DuplicateConstraint(string constraint)
		{
			return ExceptionBuilder._Data(SR.Format("Constraint matches constraint named {0} already in collection.", constraint));
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0001ACD4 File Offset: 0x00018ED4
		public static Exception DuplicateConstraintName(string constraint)
		{
			return ExceptionBuilder._DuplicateName(SR.Format("A Constraint named '{0}' already belongs to this DataTable.", constraint));
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0001ACE6 File Offset: 0x00018EE6
		public static Exception NeededForForeignKeyConstraint(UniqueConstraint key, ForeignKeyConstraint fk)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot remove unique constraint '{0}'. Remove foreign key constraint '{1}' first.", key.ConstraintName, fk.ConstraintName));
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0001AD03 File Offset: 0x00018F03
		public static Exception UniqueConstraintViolation()
		{
			return ExceptionBuilder._Argument("These columns don't currently have unique values.");
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0001AD0F File Offset: 0x00018F0F
		public static Exception ConstraintForeignTable()
		{
			return ExceptionBuilder._Argument("These columns don't point to this table.");
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0001AD1B File Offset: 0x00018F1B
		public static Exception ConstraintParentValues()
		{
			return ExceptionBuilder._Argument("This constraint cannot be enabled as not all values have corresponding parent values.");
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0001AD27 File Offset: 0x00018F27
		public static Exception ConstraintAddFailed(DataTable table)
		{
			return ExceptionBuilder._InvalidConstraint(SR.Format("This constraint cannot be added since ForeignKey doesn't belong to table {0}.", table.TableName));
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0001AD3E File Offset: 0x00018F3E
		public static Exception ConstraintRemoveFailed()
		{
			return ExceptionBuilder._Argument("Cannot remove a constraint that doesn't belong to this table.");
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0001AD4A File Offset: 0x00018F4A
		public static Exception FailedCascadeDelete(string constraint)
		{
			return ExceptionBuilder._InvalidConstraint(SR.Format("Cannot delete this row because constraints are enforced on relation {0}, and deleting this row will strand child rows.", constraint));
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0001AD5C File Offset: 0x00018F5C
		public static Exception FailedCascadeUpdate(string constraint)
		{
			return ExceptionBuilder._InvalidConstraint(SR.Format("Cannot make this change because constraints are enforced on relation {0}, and changing this value will strand child rows.", constraint));
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0001AD6E File Offset: 0x00018F6E
		public static Exception FailedClearParentTable(string table, string constraint, string childTable)
		{
			return ExceptionBuilder._InvalidConstraint(SR.Format("Cannot clear table {0} because ForeignKeyConstraint {1} enforces constraints and there are child rows in {2}.", table, constraint, childTable));
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0001AD82 File Offset: 0x00018F82
		public static Exception ForeignKeyViolation(string constraint, object[] keys)
		{
			return ExceptionBuilder._InvalidConstraint(SR.Format("ForeignKeyConstraint {0} requires the child key values ({1}) to exist in the parent table.", constraint, ExceptionBuilder.KeysToString(keys)));
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0001AD9A File Offset: 0x00018F9A
		public static Exception RemoveParentRow(ForeignKeyConstraint constraint)
		{
			return ExceptionBuilder._InvalidConstraint(SR.Format("Cannot remove this row because it has child rows, and constraints on relation {0} are enforced.", constraint.ConstraintName));
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0001ADB1 File Offset: 0x00018FB1
		public static string MaxLengthViolationText(string columnName)
		{
			return SR.Format("Column '{0}' exceeds the MaxLength limit.", columnName);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0001ADBE File Offset: 0x00018FBE
		public static string NotAllowDBNullViolationText(string columnName)
		{
			return SR.Format("Column '{0}' does not allow DBNull.Value.", columnName);
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0001ADCB File Offset: 0x00018FCB
		public static Exception CantAddConstraintToMultipleNestedTable(string tableName)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot add constraint to DataTable '{0}' which is a child table in two nested relations.", tableName));
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0001ADDD File Offset: 0x00018FDD
		public static Exception AutoIncrementAndExpression()
		{
			return ExceptionBuilder._Argument("Cannot set AutoIncrement property for a computed column.");
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0001ADE9 File Offset: 0x00018FE9
		public static Exception AutoIncrementAndDefaultValue()
		{
			return ExceptionBuilder._Argument("Cannot set AutoIncrement property for a column with DefaultValue set.");
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0001ADF5 File Offset: 0x00018FF5
		public static Exception AutoIncrementSeed()
		{
			return ExceptionBuilder._Argument("AutoIncrementStep must be a non-zero value.");
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0001AE01 File Offset: 0x00019001
		public static Exception CantChangeDataType()
		{
			return ExceptionBuilder._Argument("Cannot change DataType of a column once it has data.");
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0001AE0D File Offset: 0x0001900D
		public static Exception NullDataType()
		{
			return ExceptionBuilder._Argument("Column requires a valid DataType.");
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0001AE19 File Offset: 0x00019019
		public static Exception ColumnNameRequired()
		{
			return ExceptionBuilder._Argument("ColumnName is required when it is part of a DataTable.");
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0001AE25 File Offset: 0x00019025
		public static Exception DefaultValueAndAutoIncrement()
		{
			return ExceptionBuilder._Argument("Cannot set a DefaultValue on an AutoIncrement column.");
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0001AE34 File Offset: 0x00019034
		public static Exception DefaultValueDataType(string column, Type defaultType, Type columnType, Exception inner)
		{
			if (column.Length != 0)
			{
				return ExceptionBuilder._Argument(SR.Format("The DefaultValue for column {0} is of type {1} and cannot be converted to {2}.", column, defaultType.FullName, columnType.FullName), inner);
			}
			return ExceptionBuilder._Argument(SR.Format("The DefaultValue for the column is of type {0} and cannot be converted to {1}.", defaultType.FullName, columnType.FullName), inner);
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0001AE83 File Offset: 0x00019083
		public static Exception DefaultValueColumnDataType(string column, Type defaultType, Type columnType, Exception inner)
		{
			return ExceptionBuilder._Argument(SR.Format("The DefaultValue for column {0} is of type {1}, but the column is of type {2}.", column, defaultType.FullName, columnType.FullName), inner);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0001AEA2 File Offset: 0x000190A2
		public static Exception ExpressionAndUnique()
		{
			return ExceptionBuilder._Argument("Cannot create an expression on a column that has AutoIncrement or Unique.");
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0001AEAE File Offset: 0x000190AE
		public static Exception ExpressionAndReadOnly()
		{
			return ExceptionBuilder._Argument("Cannot set expression because column cannot be made ReadOnly.");
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0001AEBA File Offset: 0x000190BA
		public static Exception ExpressionAndConstraint(DataColumn column, Constraint constraint)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot set Expression property on column {0}, because it is a part of a constraint.", column.ColumnName, constraint.ConstraintName));
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0001AED7 File Offset: 0x000190D7
		public static Exception ExpressionInConstraint(DataColumn column)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot create a constraint based on Expression column {0}.", column.ColumnName));
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0001AEEE File Offset: 0x000190EE
		public static Exception ExpressionCircular()
		{
			return ExceptionBuilder._Argument("Cannot set Expression property due to circular reference in the expression.");
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0001AEFA File Offset: 0x000190FA
		public static Exception NonUniqueValues(string column)
		{
			return ExceptionBuilder._InvalidConstraint(SR.Format("Column '{0}' contains non-unique values.", column));
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0001AF0C File Offset: 0x0001910C
		public static Exception NullKeyValues(string column)
		{
			return ExceptionBuilder._Data(SR.Format("Column '{0}' has null values in it.", column));
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0001AF1E File Offset: 0x0001911E
		public static Exception NullValues(string column)
		{
			return ExceptionBuilder._NoNullAllowed(SR.Format("Column '{0}' does not allow nulls.", column));
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x0001AF30 File Offset: 0x00019130
		public static Exception ReadOnlyAndExpression()
		{
			return ExceptionBuilder._ReadOnly("Cannot change ReadOnly property for the expression column.");
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0001AF3C File Offset: 0x0001913C
		public static Exception ReadOnly(string column)
		{
			return ExceptionBuilder._ReadOnly(SR.Format("Column '{0}' is read only.", column));
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0001AF4E File Offset: 0x0001914E
		public static Exception UniqueAndExpression()
		{
			return ExceptionBuilder._Argument("Cannot change Unique property for the expression column.");
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0001AF5A File Offset: 0x0001915A
		public static Exception SetFailed(object value, DataColumn column, Type type, Exception innerException)
		{
			return ExceptionBuilder._Argument(innerException.Message + SR.Format("Couldn't store <{0}> in {1} Column.  Expected type is {2}.", value.ToString(), column.ColumnName, type.Name), innerException);
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0001AF89 File Offset: 0x00019189
		public static Exception CannotSetToNull(DataColumn column)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot set Column '{0}' to be null. Please use DBNull instead.", column.ColumnName));
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0001AFA0 File Offset: 0x000191A0
		public static Exception LongerThanMaxLength(DataColumn column)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot set column '{0}'. The value violates the MaxLength limit of this column.", column.ColumnName));
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0001AFB7 File Offset: 0x000191B7
		public static Exception CannotSetMaxLength(DataColumn column, int value)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot set Column '{0}' property MaxLength to '{1}'. There is at least one string in the table longer than the new limit.", column.ColumnName, value.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0001AFDA File Offset: 0x000191DA
		public static Exception CannotSetMaxLength2(DataColumn column)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot set Column '{0}' property MaxLength. The Column is SimpleContent.", column.ColumnName));
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0001AFF1 File Offset: 0x000191F1
		public static Exception CannotSetSimpleContentType(string columnName, Type type)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot set Column '{0}' property DataType to {1}. The Column is SimpleContent.", columnName, type));
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0001B004 File Offset: 0x00019204
		public static Exception CannotSetSimpleContent(string columnName, Type type)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot set Column '{0}' property MappingType to SimpleContent. The Column DataType is {1}.", columnName, type));
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0001B017 File Offset: 0x00019217
		public static Exception CannotChangeNamespace(string columnName)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot change the Column '{0}' property Namespace. The Column is SimpleContent.", columnName));
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0001B029 File Offset: 0x00019229
		public static Exception HasToBeStringType(DataColumn column)
		{
			return ExceptionBuilder._Argument(SR.Format("MaxLength applies to string data type only. You cannot set Column '{0}' property MaxLength to be non-negative number.", column.ColumnName));
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0001B040 File Offset: 0x00019240
		public static Exception AutoIncrementCannotSetIfHasData(string typeName)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot change AutoIncrement of a DataColumn with type '{0}' once it has data.", typeName));
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0001B052 File Offset: 0x00019252
		public static Exception INullableUDTwithoutStaticNull(string typeName)
		{
			return ExceptionBuilder._Argument(SR.Format("Type '{0}' does not contain static Null property or field.", typeName));
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0001B064 File Offset: 0x00019264
		public static Exception IComparableNotImplemented(string typeName)
		{
			return ExceptionBuilder._Data(SR.Format(" Type '{0}' does not implement IComparable interface. Comparison cannot be done.", typeName));
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0001B076 File Offset: 0x00019276
		public static Exception UDTImplementsIChangeTrackingButnotIRevertible(string typeName)
		{
			return ExceptionBuilder._InvalidOperation(SR.Format("Type '{0}' does not implement IRevertibleChangeTracking; therefore can not proceed with RejectChanges().", typeName));
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0001B088 File Offset: 0x00019288
		public static Exception SetAddedAndModifiedCalledOnnonUnchanged()
		{
			return ExceptionBuilder._InvalidOperation("SetAdded and SetModified can only be called on DataRows with Unchanged DataRowState.");
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0001B094 File Offset: 0x00019294
		public static Exception InvalidDataColumnMapping(Type type)
		{
			return ExceptionBuilder._Argument(SR.Format("DataColumn with type '{0}' is a complexType. Can not serialize value of a complex type as Attribute", type.AssemblyQualifiedName));
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0001B0AB File Offset: 0x000192AB
		public static Exception CannotSetDateTimeModeForNonDateTimeColumns()
		{
			return ExceptionBuilder._InvalidOperation("The DateTimeMode can be set only on DataColumns of type DateTime.");
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0001B0B7 File Offset: 0x000192B7
		public static Exception InvalidDateTimeMode(DataSetDateTime mode)
		{
			return ExceptionBuilder._InvalidEnumArgumentException<DataSetDateTime>(mode);
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0001B0BF File Offset: 0x000192BF
		public static Exception CantChangeDateTimeMode(DataSetDateTime oldValue, DataSetDateTime newValue)
		{
			return ExceptionBuilder._InvalidOperation(SR.Format("Cannot change DateTimeMode from '{0}' to '{1}' once the table has data.", oldValue.ToString(), newValue.ToString()));
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0001B0EA File Offset: 0x000192EA
		public static Exception ColumnTypeNotSupported()
		{
			return ADP.NotSupported("DataSet does not support System.Nullable<>.");
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0001B0F6 File Offset: 0x000192F6
		public static Exception SetFailed(string name)
		{
			return ExceptionBuilder._Data(SR.Format("Cannot set {0}.", name));
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0001B108 File Offset: 0x00019308
		public static Exception SetDataSetFailed()
		{
			return ExceptionBuilder._Data("Cannot change DataSet on a DataViewManager that's already the default view for a DataSet.");
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0001B114 File Offset: 0x00019314
		public static Exception SetRowStateFilter()
		{
			return ExceptionBuilder._Data("RowStateFilter cannot show ModifiedOriginals and ModifiedCurrents at the same time.");
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0001B120 File Offset: 0x00019320
		public static Exception CanNotSetDataSet()
		{
			return ExceptionBuilder._Data("Cannot change DataSet property once it is set.");
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0001B12C File Offset: 0x0001932C
		public static Exception CanNotUseDataViewManager()
		{
			return ExceptionBuilder._Data("DataSet must be set prior to using DataViewManager.");
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0001B138 File Offset: 0x00019338
		public static Exception CanNotSetTable()
		{
			return ExceptionBuilder._Data("Cannot change Table property once it is set.");
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0001B144 File Offset: 0x00019344
		public static Exception CanNotUse()
		{
			return ExceptionBuilder._Data("DataTable must be set prior to using DataView.");
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0001B150 File Offset: 0x00019350
		public static Exception CanNotBindTable()
		{
			return ExceptionBuilder._Data("Cannot bind to DataTable with no name.");
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0001B15C File Offset: 0x0001935C
		public static Exception SetTable()
		{
			return ExceptionBuilder._Data("Cannot change Table property on a DefaultView or a DataView coming from a DataViewManager.");
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0001B168 File Offset: 0x00019368
		public static Exception SetIListObject()
		{
			return ExceptionBuilder._Argument("Cannot set an object into this list.");
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0001B174 File Offset: 0x00019374
		public static Exception AddNewNotAllowNull()
		{
			return ExceptionBuilder._Data("Cannot call AddNew on a DataView where AllowNew is false.");
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0001B180 File Offset: 0x00019380
		public static Exception NotOpen()
		{
			return ExceptionBuilder._Data("DataView is not open.");
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0001B18C File Offset: 0x0001938C
		public static Exception CreateChildView()
		{
			return ExceptionBuilder._Argument("The relation is not parented to the table to which this DataView points.");
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0001B198 File Offset: 0x00019398
		public static Exception CanNotDelete()
		{
			return ExceptionBuilder._Data("Cannot delete on a DataSource where AllowDelete is false.");
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0001B1A4 File Offset: 0x000193A4
		public static Exception CanNotEdit()
		{
			return ExceptionBuilder._Data("Cannot edit on a DataSource where AllowEdit is false.");
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0001B1B0 File Offset: 0x000193B0
		public static Exception GetElementIndex(int index)
		{
			return ExceptionBuilder._IndexOutOfRange(SR.Format("Index {0} is either negative or above rows count.", index.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0001B1CD File Offset: 0x000193CD
		public static Exception AddExternalObject()
		{
			return ExceptionBuilder._Argument("Cannot add external objects to this list.");
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0001B1D9 File Offset: 0x000193D9
		public static Exception CanNotClear()
		{
			return ExceptionBuilder._Argument("Cannot clear this list.");
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0001B1E5 File Offset: 0x000193E5
		public static Exception InsertExternalObject()
		{
			return ExceptionBuilder._Argument("Cannot insert external objects to this list.");
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0001B1F1 File Offset: 0x000193F1
		public static Exception RemoveExternalObject()
		{
			return ExceptionBuilder._Argument("Cannot remove objects not in the list.");
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0001B1FD File Offset: 0x000193FD
		public static Exception PropertyNotFound(string property, string table)
		{
			return ExceptionBuilder._Argument(SR.Format("{0} is neither a DataColumn nor a DataRelation for table {1}.", property, table));
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0001B210 File Offset: 0x00019410
		public static Exception ColumnToSortIsOutOfRange(string column)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot find column {0}.", column));
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0001B222 File Offset: 0x00019422
		public static Exception KeyTableMismatch()
		{
			return ExceptionBuilder._InvalidConstraint("Cannot create a Key from Columns that belong to different tables.");
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0001B22E File Offset: 0x0001942E
		public static Exception KeyNoColumns()
		{
			return ExceptionBuilder._InvalidConstraint("Cannot have 0 columns.");
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0001B23A File Offset: 0x0001943A
		public static Exception KeyTooManyColumns(int cols)
		{
			return ExceptionBuilder._InvalidConstraint(SR.Format("Cannot have more than {0} columns.", cols.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0001B257 File Offset: 0x00019457
		public static Exception KeyDuplicateColumns(string columnName)
		{
			return ExceptionBuilder._InvalidConstraint(SR.Format("Cannot create a Key when the same column is listed more than once: '{0}'", columnName));
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0001B269 File Offset: 0x00019469
		public static Exception RelationDataSetMismatch()
		{
			return ExceptionBuilder._InvalidConstraint("Cannot have a relationship between tables in different DataSets.");
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0001B275 File Offset: 0x00019475
		public static Exception NoRelationName()
		{
			return ExceptionBuilder._Argument("RelationName is required when it is part of a DataSet.");
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0001B281 File Offset: 0x00019481
		public static Exception ColumnsTypeMismatch()
		{
			return ExceptionBuilder._InvalidConstraint("Parent Columns and Child Columns don't have type-matching columns.");
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0001B28D File Offset: 0x0001948D
		public static Exception KeyLengthMismatch()
		{
			return ExceptionBuilder._Argument("ParentColumns and ChildColumns should be the same length.");
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0001B299 File Offset: 0x00019499
		public static Exception KeyLengthZero()
		{
			return ExceptionBuilder._Argument("ParentColumns and ChildColumns must not be zero length.");
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0001B2A5 File Offset: 0x000194A5
		public static Exception ForeignRelation()
		{
			return ExceptionBuilder._Argument("This relation should connect two tables in this DataSet to be added to this DataSet.");
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0001B2B1 File Offset: 0x000194B1
		public static Exception KeyColumnsIdentical()
		{
			return ExceptionBuilder._InvalidConstraint("ParentKey and ChildKey are identical.");
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0001B2BD File Offset: 0x000194BD
		public static Exception RelationForeignTable(string t1, string t2)
		{
			return ExceptionBuilder._InvalidConstraint(SR.Format("GetChildRows requires a row whose Table is {0}, but the specified row's Table is {1}.", t1, t2));
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0001B2D0 File Offset: 0x000194D0
		public static Exception GetParentRowTableMismatch(string t1, string t2)
		{
			return ExceptionBuilder._InvalidConstraint(SR.Format("GetParentRow requires a row whose Table is {0}, but the specified row's Table is {1}.", t1, t2));
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0001B2E3 File Offset: 0x000194E3
		public static Exception SetParentRowTableMismatch(string t1, string t2)
		{
			return ExceptionBuilder._InvalidConstraint(SR.Format("SetParentRow requires a child row whose Table is {0}, but the specified row's Table is {1}.", t1, t2));
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0001B2F6 File Offset: 0x000194F6
		public static Exception RelationForeignRow()
		{
			return ExceptionBuilder._Argument("The row doesn't belong to the same DataSet as this relation.");
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0001B302 File Offset: 0x00019502
		public static Exception RelationNestedReadOnly()
		{
			return ExceptionBuilder._Argument("Cannot set the 'Nested' property to false for this relation.");
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0001B30E File Offset: 0x0001950E
		public static Exception TableCantBeNestedInTwoTables(string tableName)
		{
			return ExceptionBuilder._Argument(SR.Format("The same table '{0}' cannot be the child table in two nested relations.", tableName));
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0001B320 File Offset: 0x00019520
		public static Exception LoopInNestedRelations(string tableName)
		{
			return ExceptionBuilder._Argument(SR.Format("The table ({0}) cannot be the child table to itself in nested relations.", tableName));
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0001B332 File Offset: 0x00019532
		public static Exception RelationDoesNotExist()
		{
			return ExceptionBuilder._Argument("This relation doesn't belong to this relation collection.");
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0001B33E File Offset: 0x0001953E
		public static Exception ParentRowNotInTheDataSet()
		{
			return ExceptionBuilder._Argument("This relation and child row don't belong to same DataSet.");
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0001B34A File Offset: 0x0001954A
		public static Exception ParentOrChildColumnsDoNotHaveDataSet()
		{
			return ExceptionBuilder._InvalidConstraint("Cannot create a DataRelation if Parent or Child Columns are not in a DataSet.");
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0001B356 File Offset: 0x00019556
		public static Exception InValidNestedRelation(string childTableName)
		{
			return ExceptionBuilder._InvalidOperation(SR.Format("Nested table '{0}' which inherits its namespace cannot have multiple parent tables in different namespaces.", childTableName));
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0001B368 File Offset: 0x00019568
		public static Exception InvalidParentNamespaceinNestedRelation(string childTableName)
		{
			return ExceptionBuilder._InvalidOperation(SR.Format("Nested table '{0}' with empty namespace cannot have multiple parent tables in different namespaces.", childTableName));
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0001B2F6 File Offset: 0x000194F6
		public static Exception RowNotInTheDataSet()
		{
			return ExceptionBuilder._Argument("The row doesn't belong to the same DataSet as this relation.");
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0001B37A File Offset: 0x0001957A
		public static Exception RowNotInTheTable()
		{
			return ExceptionBuilder._RowNotInTable("Cannot perform this operation on a row not in the table.");
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0001B386 File Offset: 0x00019586
		public static Exception EditInRowChanging()
		{
			return ExceptionBuilder._InRowChangingEvent("Cannot change a proposed value in the RowChanging event.");
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0001B392 File Offset: 0x00019592
		public static Exception EndEditInRowChanging()
		{
			return ExceptionBuilder._InRowChangingEvent("Cannot call EndEdit() inside an OnRowChanging event.");
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0001B39E File Offset: 0x0001959E
		public static Exception BeginEditInRowChanging()
		{
			return ExceptionBuilder._InRowChangingEvent("Cannot call BeginEdit() inside the RowChanging event.");
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0001B3AA File Offset: 0x000195AA
		public static Exception CancelEditInRowChanging()
		{
			return ExceptionBuilder._InRowChangingEvent("Cannot call CancelEdit() inside an OnRowChanging event.  Throw an exception to cancel this update.");
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0001B3B6 File Offset: 0x000195B6
		public static Exception DeleteInRowDeleting()
		{
			return ExceptionBuilder._InRowChangingEvent("Cannot call Delete inside an OnRowDeleting event.  Throw an exception to cancel this delete.");
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0001B3C2 File Offset: 0x000195C2
		public static Exception ValueArrayLength()
		{
			return ExceptionBuilder._Argument("Input array is longer than the number of columns in this table.");
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0001B3CE File Offset: 0x000195CE
		public static Exception NoCurrentData()
		{
			return ExceptionBuilder._VersionNotFound("There is no Current data to access.");
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0001B3DA File Offset: 0x000195DA
		public static Exception NoOriginalData()
		{
			return ExceptionBuilder._VersionNotFound("There is no Original data to access.");
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0001B3E6 File Offset: 0x000195E6
		public static Exception NoProposedData()
		{
			return ExceptionBuilder._VersionNotFound("There is no Proposed data to access.");
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0001B3F2 File Offset: 0x000195F2
		public static Exception RowRemovedFromTheTable()
		{
			return ExceptionBuilder._RowNotInTable("This row has been removed from a table and does not have any data.  BeginEdit() will allow creation of new data in this row.");
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0001B3FE File Offset: 0x000195FE
		public static Exception DeletedRowInaccessible()
		{
			return ExceptionBuilder._DeletedRowInaccessible("Deleted row information cannot be accessed through the row.");
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0001B40A File Offset: 0x0001960A
		public static Exception RowAlreadyDeleted()
		{
			return ExceptionBuilder._DeletedRowInaccessible("Cannot delete this row since it's already deleted.");
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0001B416 File Offset: 0x00019616
		public static Exception RowEmpty()
		{
			return ExceptionBuilder._Argument("This row is empty.");
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0001B422 File Offset: 0x00019622
		public static Exception InvalidRowVersion()
		{
			return ExceptionBuilder._Data("Version must be Original, Current, or Proposed.");
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0001B42E File Offset: 0x0001962E
		public static Exception RowOutOfRange()
		{
			return ExceptionBuilder._IndexOutOfRange("The given DataRow is not in the current DataRowCollection.");
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0001B43A File Offset: 0x0001963A
		public static Exception RowOutOfRange(int index)
		{
			return ExceptionBuilder._IndexOutOfRange(SR.Format("There is no row at position {0}.", index.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0001B457 File Offset: 0x00019657
		public static Exception RowInsertOutOfRange(int index)
		{
			return ExceptionBuilder._IndexOutOfRange(SR.Format("The row insert position {0} is invalid.", index.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0001B474 File Offset: 0x00019674
		public static Exception RowInsertTwice(int index, string tableName)
		{
			return ExceptionBuilder._IndexOutOfRange(SR.Format("The rowOrder value={0} has been found twice for table named '{1}'.", index.ToString(CultureInfo.InvariantCulture), tableName));
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0001B492 File Offset: 0x00019692
		public static Exception RowInsertMissing(string tableName)
		{
			return ExceptionBuilder._IndexOutOfRange(SR.Format("Values are missing in the rowOrder sequence for table '{0}'.", tableName));
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0001B4A4 File Offset: 0x000196A4
		public static Exception RowAlreadyRemoved()
		{
			return ExceptionBuilder._Data("Cannot remove a row that's already been removed.");
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0001B4B0 File Offset: 0x000196B0
		public static Exception MultipleParents()
		{
			return ExceptionBuilder._Data("A child row has multiple parents.");
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0001B4BC File Offset: 0x000196BC
		public static Exception InvalidRowState(DataRowState state)
		{
			return ExceptionBuilder._InvalidEnumArgumentException<DataRowState>(state);
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0001B4C4 File Offset: 0x000196C4
		public static Exception InvalidRowBitPattern()
		{
			return ExceptionBuilder._Argument("Unrecognized row state bit pattern.");
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0001B4D0 File Offset: 0x000196D0
		internal static Exception SetDataSetNameToEmpty()
		{
			return ExceptionBuilder._Argument("Cannot change the name of the DataSet to an empty string.");
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0001B4DC File Offset: 0x000196DC
		internal static Exception SetDataSetNameConflicting(string name)
		{
			return ExceptionBuilder._Argument(SR.Format("The name '{0}' is invalid. A DataSet cannot have the same name of the DataTable.", name));
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0001B4EE File Offset: 0x000196EE
		public static Exception DataSetUnsupportedSchema(string ns)
		{
			return ExceptionBuilder._Argument(SR.Format("The schema namespace is invalid. Please use this one instead: {0}.", ns));
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0001B500 File Offset: 0x00019700
		public static Exception MergeMissingDefinition(string obj)
		{
			return ExceptionBuilder._Argument(SR.Format("Target DataSet missing definition for {0}.", obj));
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0001B512 File Offset: 0x00019712
		public static Exception TablesInDifferentSets()
		{
			return ExceptionBuilder._Argument("Cannot create a relation between tables in different DataSets.");
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0001B51E File Offset: 0x0001971E
		public static Exception RelationAlreadyExists()
		{
			return ExceptionBuilder._Argument("A relation already exists for these child columns.");
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0001B52A File Offset: 0x0001972A
		public static Exception RowAlreadyInOtherCollection()
		{
			return ExceptionBuilder._Argument("This row already belongs to another table.");
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0001B536 File Offset: 0x00019736
		public static Exception RowAlreadyInTheCollection()
		{
			return ExceptionBuilder._Argument("This row already belongs to this table.");
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0001B542 File Offset: 0x00019742
		public static Exception TableMissingPrimaryKey()
		{
			return ExceptionBuilder._MissingPrimaryKey("Table doesn't have a primary key.");
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0001B54E File Offset: 0x0001974E
		public static Exception RecordStateRange()
		{
			return ExceptionBuilder._Argument("The RowStates parameter must be set to a valid combination of values from the DataViewRowState enumeration.");
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0001B55A File Offset: 0x0001975A
		public static Exception IndexKeyLength(int length, int keyLength)
		{
			if (length != 0)
			{
				return ExceptionBuilder._Argument(SR.Format("Expecting {0} value(s) for the key being indexed, but received {1} value(s).", length.ToString(CultureInfo.InvariantCulture), keyLength.ToString(CultureInfo.InvariantCulture)));
			}
			return ExceptionBuilder._Argument("Find finds a row based on a Sort order, and no Sort order is specified.");
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0001B591 File Offset: 0x00019791
		public static Exception RemovePrimaryKey(DataTable table)
		{
			if (table.TableName.Length != 0)
			{
				return ExceptionBuilder._Argument(SR.Format("Cannot remove unique constraint since it's the primary key of table {0}.", table.TableName));
			}
			return ExceptionBuilder._Argument("Cannot remove unique constraint since it's the primary key of a table.");
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0001B5C0 File Offset: 0x000197C0
		public static Exception RelationAlreadyInOtherDataSet()
		{
			return ExceptionBuilder._Argument("This relation already belongs to another DataSet.");
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0001B5CC File Offset: 0x000197CC
		public static Exception RelationAlreadyInTheDataSet()
		{
			return ExceptionBuilder._Argument("This relation already belongs to this DataSet.");
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0001B5D8 File Offset: 0x000197D8
		public static Exception RelationNotInTheDataSet(string relation)
		{
			return ExceptionBuilder._Argument(SR.Format("Relation {0} does not belong to this DataSet.", relation));
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0001B5EA File Offset: 0x000197EA
		public static Exception RelationOutOfRange(object index)
		{
			return ExceptionBuilder._IndexOutOfRange(SR.Format("Cannot find relation {0}.", Convert.ToString(index, null)));
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0001B602 File Offset: 0x00019802
		public static Exception DuplicateRelation(string relation)
		{
			return ExceptionBuilder._DuplicateName(SR.Format("A Relation named '{0}' already belongs to this DataSet.", relation));
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0001B614 File Offset: 0x00019814
		public static Exception RelationTableNull()
		{
			return ExceptionBuilder._Argument("Cannot create a collection on a null table.");
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0001B614 File Offset: 0x00019814
		public static Exception RelationDataSetNull()
		{
			return ExceptionBuilder._Argument("Cannot create a collection on a null table.");
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0001B620 File Offset: 0x00019820
		public static Exception RelationTableWasRemoved()
		{
			return ExceptionBuilder._Argument("The table this collection displays relations for has been removed from its DataSet.");
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0001B62C File Offset: 0x0001982C
		public static Exception ParentTableMismatch()
		{
			return ExceptionBuilder._Argument("Cannot add a relation to this table's ChildRelation collection where this table isn't the parent table.");
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0001B638 File Offset: 0x00019838
		public static Exception ChildTableMismatch()
		{
			return ExceptionBuilder._Argument("Cannot add a relation to this table's ParentRelation collection where this table isn't the child table.");
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0001B644 File Offset: 0x00019844
		public static Exception EnforceConstraint()
		{
			return ExceptionBuilder._Constraint("Failed to enable constraints. One or more rows contain values violating non-null, unique, or foreign-key constraints.");
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0001B650 File Offset: 0x00019850
		public static Exception CaseLocaleMismatch()
		{
			return ExceptionBuilder._Argument("Cannot add a DataRelation or Constraint that has different Locale or CaseSensitive settings between its parent and child tables.");
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0001B65C File Offset: 0x0001985C
		public static Exception CannotChangeCaseLocale()
		{
			return ExceptionBuilder.CannotChangeCaseLocale(null);
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0001B664 File Offset: 0x00019864
		public static Exception CannotChangeCaseLocale(Exception innerException)
		{
			return ExceptionBuilder._Argument("Cannot change CaseSensitive or Locale property. This change would lead to at least one DataRelation or Constraint to have different Locale or CaseSensitive settings between its related tables.", innerException);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0001B671 File Offset: 0x00019871
		public static Exception CannotChangeSchemaSerializationMode()
		{
			return ExceptionBuilder._InvalidOperation("SchemaSerializationMode property can be set only if it is overridden by derived DataSet.");
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0001B67D File Offset: 0x0001987D
		public static Exception InvalidSchemaSerializationMode(Type enumType, string mode)
		{
			return ExceptionBuilder._InvalidEnumArgumentException(SR.Format("The {0} enumeration value, {1}, is invalid.", enumType.Name, mode));
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0001B695 File Offset: 0x00019895
		public static Exception InvalidRemotingFormat(SerializationFormat mode)
		{
			return ExceptionBuilder._InvalidEnumArgumentException<SerializationFormat>(mode);
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0001B69D File Offset: 0x0001989D
		public static Exception TableForeignPrimaryKey()
		{
			return ExceptionBuilder._Argument("PrimaryKey columns do not belong to this table.");
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0001B6A9 File Offset: 0x000198A9
		public static Exception TableCannotAddToSimpleContent()
		{
			return ExceptionBuilder._Argument("Cannot add a nested relation or an element column to a table containing a SimpleContent column.");
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0001B6B5 File Offset: 0x000198B5
		public static Exception NoTableName()
		{
			return ExceptionBuilder._Argument("TableName is required when it is part of a DataSet.");
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0001B6C1 File Offset: 0x000198C1
		public static Exception MultipleTextOnlyColumns()
		{
			return ExceptionBuilder._Argument("DataTable already has a simple content column.");
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0001B6CD File Offset: 0x000198CD
		public static Exception InvalidSortString(string sort)
		{
			return ExceptionBuilder._Argument(SR.Format(" {0} isn't a valid Sort string entry.", sort));
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0001B6DF File Offset: 0x000198DF
		public static Exception DuplicateTableName(string table)
		{
			return ExceptionBuilder._DuplicateName(SR.Format("A DataTable named '{0}' already belongs to this DataSet.", table));
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0001B6F1 File Offset: 0x000198F1
		public static Exception DuplicateTableName2(string table, string ns)
		{
			return ExceptionBuilder._DuplicateName(SR.Format("A DataTable named '{0}' with the same Namespace '{1}' already belongs to this DataSet.", table, ns));
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0001B704 File Offset: 0x00019904
		public static Exception SelfnestedDatasetConflictingName(string table)
		{
			return ExceptionBuilder._DuplicateName(SR.Format("The table ({0}) cannot be the child table to itself in a nested relation: the DataSet name conflicts with the table name.", table));
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0001B716 File Offset: 0x00019916
		public static Exception DatasetConflictingName(string table)
		{
			return ExceptionBuilder._DuplicateName(SR.Format("The name '{0}' is invalid. A DataTable cannot have the same name of the DataSet.", table));
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0001B728 File Offset: 0x00019928
		public static Exception TableAlreadyInOtherDataSet()
		{
			return ExceptionBuilder._Argument("DataTable already belongs to another DataSet.");
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0001B734 File Offset: 0x00019934
		public static Exception TableAlreadyInTheDataSet()
		{
			return ExceptionBuilder._Argument("DataTable already belongs to this DataSet.");
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0001B740 File Offset: 0x00019940
		public static Exception TableOutOfRange(int index)
		{
			return ExceptionBuilder._IndexOutOfRange(SR.Format("Cannot find table {0}.", index.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0001B75D File Offset: 0x0001995D
		public static Exception TableNotInTheDataSet(string table)
		{
			return ExceptionBuilder._Argument(SR.Format("Table {0} does not belong to this DataSet.", table));
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0001B76F File Offset: 0x0001996F
		public static Exception TableInRelation()
		{
			return ExceptionBuilder._Argument("Cannot remove a table that has existing relations.  Remove relations first.");
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0001B77B File Offset: 0x0001997B
		public static Exception TableInConstraint(DataTable table, Constraint constraint)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot remove table {0}, because it referenced in ForeignKeyConstraint {1}.  Remove the constraint first.", table.TableName, constraint.ConstraintName));
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0001B798 File Offset: 0x00019998
		public static Exception CanNotSerializeDataTableHierarchy()
		{
			return ExceptionBuilder._InvalidOperation("Cannot serialize the DataTable. A DataTable being used in one or more DataColumn expressions is not a descendant of current DataTable.");
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0001B7A4 File Offset: 0x000199A4
		public static Exception CanNotRemoteDataTable()
		{
			return ExceptionBuilder._InvalidOperation("This DataTable can only be remoted as part of DataSet. One or more Expression Columns has reference to other DataTable(s).");
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0001B7B0 File Offset: 0x000199B0
		public static Exception CanNotSetRemotingFormat()
		{
			return ExceptionBuilder._Argument("Cannot have different remoting format property value for DataSet and DataTable.");
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0001B7BC File Offset: 0x000199BC
		public static Exception CanNotSerializeDataTableWithEmptyName()
		{
			return ExceptionBuilder._InvalidOperation("Cannot serialize the DataTable. DataTable name is not set.");
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0001B7C8 File Offset: 0x000199C8
		public static Exception TableNotFound(string tableName)
		{
			return ExceptionBuilder._Argument(SR.Format("DataTable '{0}' does not match to any DataTable in source.", tableName));
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0001B7DA File Offset: 0x000199DA
		public static Exception AggregateException(AggregateType aggregateType, Type type)
		{
			return ExceptionBuilder._Data(SR.Format("Invalid usage of aggregate function {0}() and Type: {1}.", aggregateType.ToString(), type.Name));
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x0001B7FE File Offset: 0x000199FE
		public static Exception InvalidStorageType(TypeCode typecode)
		{
			return ExceptionBuilder._Data(SR.Format("Invalid storage type: {0}.", typecode.ToString()));
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x0001B81C File Offset: 0x00019A1C
		public static Exception RangeArgument(int min, int max)
		{
			return ExceptionBuilder._Argument(SR.Format("Min ({0}) must be less than or equal to max ({1}) in a Range object.", min.ToString(CultureInfo.InvariantCulture), max.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x0001B845 File Offset: 0x00019A45
		public static Exception NullRange()
		{
			return ExceptionBuilder._Data("This is a null range.");
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x0001B851 File Offset: 0x00019A51
		public static Exception NegativeMinimumCapacity()
		{
			return ExceptionBuilder._Argument("MinimumCapacity must be non-negative.");
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x0001B860 File Offset: 0x00019A60
		public static Exception ProblematicChars(char charValue)
		{
			string resourceFormat = "The DataSet Xml persistency does not support the value '{0}' as Char value, please use Byte storage instead.";
			string str = "0x";
			ushort num = (ushort)charValue;
			return ExceptionBuilder._Argument(SR.Format(resourceFormat, str + num.ToString("X", CultureInfo.InvariantCulture)));
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x0001B899 File Offset: 0x00019A99
		public static Exception StorageSetFailed()
		{
			return ExceptionBuilder._Argument("Type of value has a mismatch with column type");
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x0001B8A5 File Offset: 0x00019AA5
		public static Exception SimpleTypeNotSupported()
		{
			return ExceptionBuilder._Data("DataSet doesn't support 'union' or 'list' as simpleType.");
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0001B8B1 File Offset: 0x00019AB1
		public static Exception MissingAttribute(string attribute)
		{
			return ExceptionBuilder.MissingAttribute(string.Empty, attribute);
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0001B8BE File Offset: 0x00019ABE
		public static Exception MissingAttribute(string element, string attribute)
		{
			return ExceptionBuilder._Data(SR.Format("Invalid {0} syntax: missing required '{1}' attribute.", element, attribute));
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0001B8D1 File Offset: 0x00019AD1
		public static Exception InvalidAttributeValue(string name, string value)
		{
			return ExceptionBuilder._Data(SR.Format("Value '{1}' is invalid for attribute '{0}'.", name, value));
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0001B8E4 File Offset: 0x00019AE4
		public static Exception AttributeValues(string name, string value1, string value2)
		{
			return ExceptionBuilder._Data(SR.Format("The value of attribute '{0}' should be '{1}' or '{2}'.", name, value1, value2));
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0001B8F8 File Offset: 0x00019AF8
		public static Exception ElementTypeNotFound(string name)
		{
			return ExceptionBuilder._Data(SR.Format("Cannot find ElementType name='{0}'.", name));
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0001B90A File Offset: 0x00019B0A
		public static Exception RelationParentNameMissing(string rel)
		{
			return ExceptionBuilder._Data(SR.Format("Parent table name is missing in relation '{0}'.", rel));
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0001B91C File Offset: 0x00019B1C
		public static Exception RelationChildNameMissing(string rel)
		{
			return ExceptionBuilder._Data(SR.Format("Child table name is missing in relation '{0}'.", rel));
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x0001B92E File Offset: 0x00019B2E
		public static Exception RelationTableKeyMissing(string rel)
		{
			return ExceptionBuilder._Data(SR.Format("Parent table key is missing in relation '{0}'.", rel));
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0001B940 File Offset: 0x00019B40
		public static Exception RelationChildKeyMissing(string rel)
		{
			return ExceptionBuilder._Data(SR.Format("Child table key is missing in relation '{0}'.", rel));
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0001B952 File Offset: 0x00019B52
		public static Exception UndefinedDatatype(string name)
		{
			return ExceptionBuilder._Data(SR.Format("Undefined data type: '{0}'.", name));
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x0001B964 File Offset: 0x00019B64
		public static Exception DatatypeNotDefined()
		{
			return ExceptionBuilder._Data("Data type not defined.");
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0001B970 File Offset: 0x00019B70
		public static Exception MismatchKeyLength()
		{
			return ExceptionBuilder._Data("Invalid Relation definition: different length keys.");
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0001B97C File Offset: 0x00019B7C
		public static Exception InvalidField(string name)
		{
			return ExceptionBuilder._Data(SR.Format("Invalid XPath selection inside field node. Cannot find: {0}.", name));
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0001B98E File Offset: 0x00019B8E
		public static Exception InvalidSelector(string name)
		{
			return ExceptionBuilder._Data(SR.Format("Invalid XPath selection inside selector node: {0}.", name));
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0001B9A0 File Offset: 0x00019BA0
		public static Exception CircularComplexType(string name)
		{
			return ExceptionBuilder._Data(SR.Format("DataSet doesn't allow the circular reference in the ComplexType named '{0}'.", name));
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0001B9B2 File Offset: 0x00019BB2
		public static Exception CannotInstantiateAbstract(string name)
		{
			return ExceptionBuilder._Data(SR.Format("DataSet cannot instantiate an abstract ComplexType for the node {0}.", name));
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0001B9C4 File Offset: 0x00019BC4
		public static Exception InvalidKey(string name)
		{
			return ExceptionBuilder._Data(SR.Format("Invalid 'Key' node inside constraint named: {0}.", name));
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0001B9D6 File Offset: 0x00019BD6
		public static Exception DiffgramMissingTable(string name)
		{
			return ExceptionBuilder._Data(SR.Format("Cannot load diffGram. Table '{0}' is missing in the destination dataset.", name));
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0001B9E8 File Offset: 0x00019BE8
		public static Exception DiffgramMissingSQL()
		{
			return ExceptionBuilder._Data("Cannot load diffGram. The 'sql' node is missing.");
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0001B9F4 File Offset: 0x00019BF4
		public static Exception DuplicateConstraintRead(string str)
		{
			return ExceptionBuilder._Data(SR.Format("The constraint name {0} is already used in the schema.", str));
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0001BA06 File Offset: 0x00019C06
		public static Exception ColumnTypeConflict(string name)
		{
			return ExceptionBuilder._Data(SR.Format("Column name '{0}' is defined for different mapping types.", name));
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0001BA18 File Offset: 0x00019C18
		public static Exception CannotConvert(string name, string type)
		{
			return ExceptionBuilder._Data(SR.Format(" Cannot convert '{0}' to type '{1}'.", name, type));
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0001BA2B File Offset: 0x00019C2B
		public static Exception MissingRefer(string name)
		{
			return ExceptionBuilder._Data(SR.Format("Missing '{0}' part in '{1}' constraint named '{2}'.", "refer", "keyref", name));
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0001BA47 File Offset: 0x00019C47
		public static Exception InvalidPrefix(string name)
		{
			return ExceptionBuilder._Data(SR.Format("Prefix '{0}' is not valid, because it contains special characters.", name));
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0001BA59 File Offset: 0x00019C59
		public static Exception CanNotDeserializeObjectType()
		{
			return ExceptionBuilder._InvalidOperation("Unable to proceed with deserialization. Data does not implement IXMLSerializable, therefore polymorphism is not supported.");
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0001BA65 File Offset: 0x00019C65
		public static Exception IsDataSetAttributeMissingInSchema()
		{
			return ExceptionBuilder._Data("IsDataSet attribute is missing in input Schema.");
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0001BA71 File Offset: 0x00019C71
		public static Exception TooManyIsDataSetAtributeInSchema()
		{
			return ExceptionBuilder._Data("Cannot determine the DataSet Element. IsDataSet attribute exist more than once.");
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0001BA7D File Offset: 0x00019C7D
		public static Exception NestedCircular(string name)
		{
			return ExceptionBuilder._Data(SR.Format("Circular reference in self-nested table '{0}'.", name));
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0001BA8F File Offset: 0x00019C8F
		public static Exception MultipleParentRows(string tableQName)
		{
			return ExceptionBuilder._Data(SR.Format("Cannot proceed with serializing DataTable '{0}'. It contains a DataRow which has multiple parent rows on the same Foreign Key.", tableQName));
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0001BAA1 File Offset: 0x00019CA1
		public static Exception PolymorphismNotSupported(string typeName)
		{
			return ExceptionBuilder._InvalidOperation(SR.Format("Type '{0}' does not implement IXmlSerializable interface therefore can not proceed with serialization.", typeName));
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0001BAB3 File Offset: 0x00019CB3
		public static Exception DataTableInferenceNotSupported()
		{
			return ExceptionBuilder._InvalidOperation("DataTable does not support schema inference from Xml.");
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0001BABF File Offset: 0x00019CBF
		internal static void ThrowMultipleTargetConverter(Exception innerException)
		{
			ExceptionBuilder.ThrowDataException((innerException != null) ? "An error occurred with the multiple target converter while writing an Xml Schema.  See the inner exception for details." : "An error occurred with the multiple target converter while writing an Xml Schema.  A null or empty string was returned.", innerException);
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0001BAD6 File Offset: 0x00019CD6
		public static Exception DuplicateDeclaration(string name)
		{
			return ExceptionBuilder._Data(SR.Format("Duplicated declaration '{0}'.", name));
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0001BAE8 File Offset: 0x00019CE8
		public static Exception FoundEntity()
		{
			return ExceptionBuilder._Data("DataSet cannot expand entities. Use XmlValidatingReader and set the EntityHandling property accordingly.");
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0001BAF4 File Offset: 0x00019CF4
		public static Exception MergeFailed(string name)
		{
			return ExceptionBuilder._Data(name);
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0001BAFC File Offset: 0x00019CFC
		public static Exception ConvertFailed(Type type1, Type type2)
		{
			return ExceptionBuilder._Data(SR.Format(" Cannot convert object of type '{0}' to object of type '{1}'.", type1.FullName, type2.FullName));
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0001BB19 File Offset: 0x00019D19
		public static Exception InvalidDataTableReader(string tableName)
		{
			return ExceptionBuilder._InvalidOperation(SR.Format("DataTableReader is invalid for current DataTable '{0}'.", tableName));
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0001BB2B File Offset: 0x00019D2B
		public static Exception DataTableReaderSchemaIsInvalid(string tableName)
		{
			return ExceptionBuilder._InvalidOperation(SR.Format("Schema of current DataTable '{0}' in DataTableReader has changed, DataTableReader is invalid.", tableName));
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0001BB3D File Offset: 0x00019D3D
		public static Exception CannotCreateDataReaderOnEmptyDataSet()
		{
			return ExceptionBuilder._Argument("DataTableReader Cannot be created. There is no DataTable in DataSet.");
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0001BB49 File Offset: 0x00019D49
		public static Exception DataTableReaderArgumentIsEmpty()
		{
			return ExceptionBuilder._Argument("Cannot create DataTableReader. Argument is Empty.");
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0001BB55 File Offset: 0x00019D55
		public static Exception ArgumentContainsNullValue()
		{
			return ExceptionBuilder._Argument("Cannot create DataTableReader. Arguments contain null value.");
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0001BB61 File Offset: 0x00019D61
		public static Exception InvalidCurrentRowInDataTableReader()
		{
			return ExceptionBuilder._DeletedRowInaccessible("Current DataRow is either in Deleted or Detached state.");
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0001BB6D File Offset: 0x00019D6D
		public static Exception EmptyDataTableReader(string tableName)
		{
			return ExceptionBuilder._DeletedRowInaccessible(SR.Format("Current DataTable '{0}' is empty. There is no DataRow in DataTable.", tableName));
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0001BB7F File Offset: 0x00019D7F
		internal static Exception InvalidDuplicateNamedSimpleTypeDelaration(string stName, string errorStr)
		{
			return ExceptionBuilder._Argument(SR.Format("Simple type '{0}' has already be declared with different '{1}'.", stName, errorStr));
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x0001BB92 File Offset: 0x00019D92
		internal static Exception InternalRBTreeError(RBTreeError internalError)
		{
			return ExceptionBuilder._InvalidOperation(SR.Format("DataTable internal index is corrupted: '{0}'.", (int)internalError));
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0001BBA9 File Offset: 0x00019DA9
		public static Exception EnumeratorModified()
		{
			return ExceptionBuilder._InvalidOperation("Collection was modified; enumeration operation might not execute.");
		}
	}
}
