using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace System.Data
{
	// Token: 0x0200009C RID: 156
	internal sealed class TypeLimiter
	{
		// Token: 0x06000A23 RID: 2595 RVA: 0x0002A9AF File Offset: 0x00028BAF
		private TypeLimiter(TypeLimiter.Scope scope)
		{
			this.m_instanceScope = scope;
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x0002A9BE File Offset: 0x00028BBE
		private static bool IsTypeLimitingDisabled
		{
			get
			{
				return LocalAppContextSwitches.AllowArbitraryTypeInstantiation;
			}
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0002A9C8 File Offset: 0x00028BC8
		[NullableContext(2)]
		public static TypeLimiter Capture()
		{
			TypeLimiter.Scope scope = TypeLimiter.s_activeScope;
			if (scope == null)
			{
				return null;
			}
			return new TypeLimiter(scope);
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0002A9E8 File Offset: 0x00028BE8
		[NullableContext(2)]
		public static void EnsureTypeIsAllowed(Type type, TypeLimiter capturedLimiter = null)
		{
			if (type == null)
			{
				return;
			}
			TypeLimiter.Scope scope = ((capturedLimiter != null) ? capturedLimiter.m_instanceScope : null) ?? TypeLimiter.s_activeScope;
			if (scope == null)
			{
				return;
			}
			if (scope.IsAllowedType(type))
			{
				return;
			}
			throw ExceptionBuilder.TypeNotAllowed(type);
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0002AA23 File Offset: 0x00028C23
		[return: Nullable(2)]
		public static IDisposable EnterRestrictedScope(DataSet dataSet)
		{
			if (TypeLimiter.IsTypeLimitingDisabled)
			{
				return null;
			}
			return TypeLimiter.s_activeScope = new TypeLimiter.Scope(TypeLimiter.s_activeScope, TypeLimiter.GetPreviouslyDeclaredDataTypes(dataSet));
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0002AA44 File Offset: 0x00028C44
		[return: Nullable(2)]
		public static IDisposable EnterRestrictedScope(DataTable dataTable)
		{
			if (TypeLimiter.IsTypeLimitingDisabled)
			{
				return null;
			}
			return TypeLimiter.s_activeScope = new TypeLimiter.Scope(TypeLimiter.s_activeScope, TypeLimiter.GetPreviouslyDeclaredDataTypes(dataTable));
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0002AA65 File Offset: 0x00028C65
		private static IEnumerable<Type> GetPreviouslyDeclaredDataTypes(DataTable dataTable)
		{
			if (dataTable == null)
			{
				return Enumerable.Empty<Type>();
			}
			return from DataColumn column in dataTable.Columns
			select column.DataType;
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0002AA9F File Offset: 0x00028C9F
		private static IEnumerable<Type> GetPreviouslyDeclaredDataTypes(DataSet dataSet)
		{
			if (dataSet == null)
			{
				return Enumerable.Empty<Type>();
			}
			return dataSet.Tables.Cast<DataTable>().SelectMany((DataTable table) => TypeLimiter.GetPreviouslyDeclaredDataTypes(table));
		}

		// Token: 0x04000746 RID: 1862
		[Nullable(2)]
		[ThreadStatic]
		private static TypeLimiter.Scope s_activeScope;

		// Token: 0x04000747 RID: 1863
		private TypeLimiter.Scope m_instanceScope;

		// Token: 0x04000748 RID: 1864
		private const string AppDomainDataSetDefaultAllowedTypesKey = "System.Data.DataSetDefaultAllowedTypes";

		// Token: 0x0200009D RID: 157
		private sealed class Scope : IDisposable
		{
			// Token: 0x06000A2B RID: 2603 RVA: 0x0002AAD9 File Offset: 0x00028CD9
			internal Scope([Nullable(2)] TypeLimiter.Scope previousScope, IEnumerable<Type> allowedTypes)
			{
				this.m_previousScope = previousScope;
				this.m_allowedTypes = new HashSet<Type>(from type in allowedTypes
				where type != null
				select type);
			}

			// Token: 0x06000A2C RID: 2604 RVA: 0x0002AB18 File Offset: 0x00028D18
			public void Dispose()
			{
				if (this != TypeLimiter.s_activeScope)
				{
					throw new ObjectDisposedException(base.GetType().FullName);
				}
				TypeLimiter.s_activeScope = this.m_previousScope;
			}

			// Token: 0x06000A2D RID: 2605 RVA: 0x0002AB40 File Offset: 0x00028D40
			public bool IsAllowedType(Type type)
			{
				if (TypeLimiter.Scope.IsTypeUnconditionallyAllowed(type))
				{
					return true;
				}
				for (TypeLimiter.Scope scope = this; scope != null; scope = scope.m_previousScope)
				{
					if (scope.m_allowedTypes.Contains(type))
					{
						return true;
					}
				}
				Type[] array = (Type[])AppDomain.CurrentDomain.GetData("System.Data.DataSetDefaultAllowedTypes");
				if (array != null)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (type == array[i])
						{
							return true;
						}
					}
				}
				return false;
			}

			// Token: 0x06000A2E RID: 2606 RVA: 0x0002ABAC File Offset: 0x00028DAC
			private static bool IsTypeUnconditionallyAllowed(Type type)
			{
				while (!TypeLimiter.Scope.s_allowedTypes.Contains(type))
				{
					if (type.IsEnum)
					{
						return true;
					}
					if (type.IsSZArray)
					{
						type = type.GetElementType();
					}
					else
					{
						if (!type.IsGenericType || type.IsGenericTypeDefinition || !(type.GetGenericTypeDefinition() == typeof(List<>)))
						{
							return false;
						}
						type = type.GetGenericArguments()[0];
					}
				}
				return true;
			}

			// Token: 0x06000A2F RID: 2607 RVA: 0x0002AC18 File Offset: 0x00028E18
			// Note: this type is marked as 'beforefieldinit'.
			static Scope()
			{
			}

			// Token: 0x04000749 RID: 1865
			private static readonly HashSet<Type> s_allowedTypes = new HashSet<Type>
			{
				typeof(bool),
				typeof(char),
				typeof(sbyte),
				typeof(byte),
				typeof(short),
				typeof(ushort),
				typeof(int),
				typeof(uint),
				typeof(long),
				typeof(ulong),
				typeof(float),
				typeof(double),
				typeof(decimal),
				typeof(DateTime),
				typeof(DateTimeOffset),
				typeof(TimeSpan),
				typeof(string),
				typeof(Guid),
				typeof(SqlBinary),
				typeof(SqlBoolean),
				typeof(SqlByte),
				typeof(SqlBytes),
				typeof(SqlChars),
				typeof(SqlDateTime),
				typeof(SqlDecimal),
				typeof(SqlDouble),
				typeof(SqlGuid),
				typeof(SqlInt16),
				typeof(SqlInt32),
				typeof(SqlInt64),
				typeof(SqlMoney),
				typeof(SqlSingle),
				typeof(SqlString),
				typeof(object),
				typeof(Type),
				typeof(BigInteger),
				typeof(Uri),
				typeof(Color),
				typeof(Point),
				typeof(PointF),
				typeof(Rectangle),
				typeof(RectangleF),
				typeof(Size),
				typeof(SizeF)
			};

			// Token: 0x0400074A RID: 1866
			private HashSet<Type> m_allowedTypes;

			// Token: 0x0400074B RID: 1867
			[Nullable(2)]
			private readonly TypeLimiter.Scope m_previousScope;

			// Token: 0x0200009E RID: 158
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x06000A30 RID: 2608 RVA: 0x0002AF1B File Offset: 0x0002911B
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x06000A31 RID: 2609 RVA: 0x00003D93 File Offset: 0x00001F93
				public <>c()
				{
				}

				// Token: 0x06000A32 RID: 2610 RVA: 0x0002AF27 File Offset: 0x00029127
				internal bool <.ctor>b__3_0(Type type)
				{
					return type != null;
				}

				// Token: 0x0400074C RID: 1868
				public static readonly TypeLimiter.Scope.<>c <>9 = new TypeLimiter.Scope.<>c();

				// Token: 0x0400074D RID: 1869
				public static Func<Type, bool> <>9__3_0;
			}
		}

		// Token: 0x0200009F RID: 159
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000A33 RID: 2611 RVA: 0x0002AF30 File Offset: 0x00029130
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000A34 RID: 2612 RVA: 0x00003D93 File Offset: 0x00001F93
			public <>c()
			{
			}

			// Token: 0x06000A35 RID: 2613 RVA: 0x0002AF3C File Offset: 0x0002913C
			internal Type <GetPreviouslyDeclaredDataTypes>b__10_0(DataColumn column)
			{
				return column.DataType;
			}

			// Token: 0x06000A36 RID: 2614 RVA: 0x0002AF44 File Offset: 0x00029144
			internal IEnumerable<Type> <GetPreviouslyDeclaredDataTypes>b__11_0(DataTable table)
			{
				return TypeLimiter.GetPreviouslyDeclaredDataTypes(table);
			}

			// Token: 0x0400074E RID: 1870
			public static readonly TypeLimiter.<>c <>9 = new TypeLimiter.<>c();

			// Token: 0x0400074F RID: 1871
			public static Func<DataColumn, Type> <>9__10_0;

			// Token: 0x04000750 RID: 1872
			public static Func<DataTable, IEnumerable<Type>> <>9__11_0;
		}
	}
}
