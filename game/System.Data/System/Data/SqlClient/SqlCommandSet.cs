using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Data.SqlClient
{
	// Token: 0x020001C6 RID: 454
	internal sealed class SqlCommandSet
	{
		// Token: 0x06001622 RID: 5666 RVA: 0x00065906 File Offset: 0x00063B06
		internal SqlCommandSet()
		{
			this._batchCommand = new SqlCommand();
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06001623 RID: 5667 RVA: 0x00065924 File Offset: 0x00063B24
		private SqlCommand BatchCommand
		{
			get
			{
				SqlCommand batchCommand = this._batchCommand;
				if (batchCommand == null)
				{
					throw ADP.ObjectDisposed(this);
				}
				return batchCommand;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001624 RID: 5668 RVA: 0x00065943 File Offset: 0x00063B43
		internal int CommandCount
		{
			get
			{
				return this.CommandList.Count;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001625 RID: 5669 RVA: 0x00065950 File Offset: 0x00063B50
		private List<SqlCommandSet.LocalCommand> CommandList
		{
			get
			{
				List<SqlCommandSet.LocalCommand> commandList = this._commandList;
				if (commandList == null)
				{
					throw ADP.ObjectDisposed(this);
				}
				return commandList;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (set) Token: 0x06001626 RID: 5670 RVA: 0x0006596F File Offset: 0x00063B6F
		internal int CommandTimeout
		{
			set
			{
				this.BatchCommand.CommandTimeout = value;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06001627 RID: 5671 RVA: 0x0006597D File Offset: 0x00063B7D
		// (set) Token: 0x06001628 RID: 5672 RVA: 0x0006598A File Offset: 0x00063B8A
		internal SqlConnection Connection
		{
			get
			{
				return this.BatchCommand.Connection;
			}
			set
			{
				this.BatchCommand.Connection = value;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (set) Token: 0x06001629 RID: 5673 RVA: 0x00065998 File Offset: 0x00063B98
		internal SqlTransaction Transaction
		{
			set
			{
				this.BatchCommand.Transaction = value;
			}
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x000659A8 File Offset: 0x00063BA8
		internal void Append(SqlCommand command)
		{
			ADP.CheckArgumentNull(command, "command");
			string commandText = command.CommandText;
			if (string.IsNullOrEmpty(commandText))
			{
				throw ADP.CommandTextRequired("Append");
			}
			CommandType commandType = command.CommandType;
			if (commandType == CommandType.Text || commandType == CommandType.StoredProcedure)
			{
				SqlParameterCollection sqlParameterCollection = null;
				SqlParameterCollection parameters = command.Parameters;
				if (0 < parameters.Count)
				{
					sqlParameterCollection = new SqlParameterCollection();
					for (int i = 0; i < parameters.Count; i++)
					{
						SqlParameter sqlParameter = new SqlParameter();
						parameters[i].CopyTo(sqlParameter);
						sqlParameterCollection.Add(sqlParameter);
						if (!SqlCommandSet.s_sqlIdentifierParser.IsMatch(sqlParameter.ParameterName))
						{
							throw ADP.BadParameterName(sqlParameter.ParameterName);
						}
					}
					foreach (object obj in sqlParameterCollection)
					{
						SqlParameter sqlParameter2 = (SqlParameter)obj;
						object value = sqlParameter2.Value;
						byte[] array = value as byte[];
						if (array != null)
						{
							int offset = sqlParameter2.Offset;
							int size = sqlParameter2.Size;
							int num = array.Length - offset;
							if (size != 0 && size < num)
							{
								num = size;
							}
							byte[] array2 = new byte[Math.Max(num, 0)];
							Buffer.BlockCopy(array, offset, array2, 0, array2.Length);
							sqlParameter2.Offset = 0;
							sqlParameter2.Value = array2;
						}
						else
						{
							char[] array3 = value as char[];
							if (array3 != null)
							{
								int offset2 = sqlParameter2.Offset;
								int size2 = sqlParameter2.Size;
								int num2 = array3.Length - offset2;
								if (size2 != 0 && size2 < num2)
								{
									num2 = size2;
								}
								char[] array4 = new char[Math.Max(num2, 0)];
								Buffer.BlockCopy(array3, offset2, array4, 0, array4.Length * 2);
								sqlParameter2.Offset = 0;
								sqlParameter2.Value = array4;
							}
							else
							{
								ICloneable cloneable = value as ICloneable;
								if (cloneable != null)
								{
									sqlParameter2.Value = cloneable.Clone();
								}
							}
						}
					}
				}
				int returnParameterIndex = -1;
				if (sqlParameterCollection != null)
				{
					for (int j = 0; j < sqlParameterCollection.Count; j++)
					{
						if (ParameterDirection.ReturnValue == sqlParameterCollection[j].Direction)
						{
							returnParameterIndex = j;
							break;
						}
					}
				}
				SqlCommandSet.LocalCommand item = new SqlCommandSet.LocalCommand(commandText, sqlParameterCollection, returnParameterIndex, command.CommandType);
				this.CommandList.Add(item);
				return;
			}
			if (commandType == CommandType.TableDirect)
			{
				throw SQL.NotSupportedCommandType(commandType);
			}
			throw ADP.InvalidCommandType(commandType);
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x00065C10 File Offset: 0x00063E10
		internal static void BuildStoredProcedureName(StringBuilder builder, string part)
		{
			if (part != null && 0 < part.Length)
			{
				if ('[' == part[0])
				{
					int num = 0;
					foreach (char c in part)
					{
						if (']' == c)
						{
							num++;
						}
					}
					if (1 == num % 2)
					{
						builder.Append(part);
						return;
					}
				}
				SqlServerEscapeHelper.EscapeIdentifier(builder, part);
			}
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x00065C70 File Offset: 0x00063E70
		internal void Clear()
		{
			DbCommand batchCommand = this.BatchCommand;
			if (batchCommand != null)
			{
				batchCommand.Parameters.Clear();
				batchCommand.CommandText = null;
			}
			List<SqlCommandSet.LocalCommand> commandList = this._commandList;
			if (commandList != null)
			{
				commandList.Clear();
			}
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x00065CAC File Offset: 0x00063EAC
		internal void Dispose()
		{
			SqlCommand batchCommand = this._batchCommand;
			this._commandList = null;
			this._batchCommand = null;
			if (batchCommand != null)
			{
				batchCommand.Dispose();
			}
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x00065CD8 File Offset: 0x00063ED8
		internal int ExecuteNonQuery()
		{
			this.ValidateCommandBehavior("ExecuteNonQuery", CommandBehavior.Default);
			this.BatchCommand.BatchRPCMode = true;
			this.BatchCommand.ClearBatchCommand();
			this.BatchCommand.Parameters.Clear();
			for (int i = 0; i < this._commandList.Count; i++)
			{
				SqlCommandSet.LocalCommand localCommand = this._commandList[i];
				this.BatchCommand.AddBatchCommand(localCommand.CommandText, localCommand.Parameters, localCommand.CmdType);
			}
			return this.BatchCommand.ExecuteBatchRPCCommand();
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x00065D63 File Offset: 0x00063F63
		internal SqlParameter GetParameter(int commandIndex, int parameterIndex)
		{
			return this.CommandList[commandIndex].Parameters[parameterIndex];
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x00065D7C File Offset: 0x00063F7C
		internal bool GetBatchedAffected(int commandIdentifier, out int recordsAffected, out Exception error)
		{
			error = this.BatchCommand.GetErrors(commandIdentifier);
			int? recordsAffected2 = this.BatchCommand.GetRecordsAffected(commandIdentifier);
			recordsAffected = recordsAffected2.GetValueOrDefault();
			return recordsAffected2 != null;
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x00065DB4 File Offset: 0x00063FB4
		internal int GetParameterCount(int commandIndex)
		{
			return this.CommandList[commandIndex].Parameters.Count;
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x00065DCC File Offset: 0x00063FCC
		private void ValidateCommandBehavior(string method, CommandBehavior behavior)
		{
			if ((behavior & ~(CommandBehavior.SequentialAccess | CommandBehavior.CloseConnection)) != CommandBehavior.Default)
			{
				ADP.ValidateCommandBehavior(behavior);
				throw ADP.NotSupportedCommandBehavior(behavior & ~(CommandBehavior.SequentialAccess | CommandBehavior.CloseConnection), method);
			}
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x00065DE5 File Offset: 0x00063FE5
		// Note: this type is marked as 'beforefieldinit'.
		static SqlCommandSet()
		{
		}

		// Token: 0x04000E0A RID: 3594
		private const string SqlIdentifierPattern = "^@[\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}_@#][\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}\\p{Nd}＿_@#\\$]*$";

		// Token: 0x04000E0B RID: 3595
		private static readonly Regex s_sqlIdentifierParser = new Regex("^@[\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}_@#][\\p{Lo}\\p{Lu}\\p{Ll}\\p{Lm}\\p{Nd}＿_@#\\$]*$", RegexOptions.ExplicitCapture | RegexOptions.Singleline);

		// Token: 0x04000E0C RID: 3596
		private List<SqlCommandSet.LocalCommand> _commandList = new List<SqlCommandSet.LocalCommand>();

		// Token: 0x04000E0D RID: 3597
		private SqlCommand _batchCommand;

		// Token: 0x020001C7 RID: 455
		private sealed class LocalCommand
		{
			// Token: 0x06001634 RID: 5684 RVA: 0x00065DF8 File Offset: 0x00063FF8
			internal LocalCommand(string commandText, SqlParameterCollection parameters, int returnParameterIndex, CommandType cmdType)
			{
				this.CommandText = commandText;
				this.Parameters = parameters;
				this.ReturnParameterIndex = returnParameterIndex;
				this.CmdType = cmdType;
			}

			// Token: 0x04000E0E RID: 3598
			internal readonly string CommandText;

			// Token: 0x04000E0F RID: 3599
			internal readonly SqlParameterCollection Parameters;

			// Token: 0x04000E10 RID: 3600
			internal readonly int ReturnParameterIndex;

			// Token: 0x04000E11 RID: 3601
			internal readonly CommandType CmdType;
		}
	}
}
