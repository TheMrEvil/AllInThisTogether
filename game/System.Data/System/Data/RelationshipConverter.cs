using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Data
{
	// Token: 0x02000121 RID: 289
	internal sealed class RelationshipConverter : ExpandableObjectConverter
	{
		// Token: 0x0600100F RID: 4111 RVA: 0x0002C704 File Offset: 0x0002A904
		public RelationshipConverter()
		{
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x000425E3 File Offset: 0x000407E3
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x00042604 File Offset: 0x00040804
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(InstanceDescriptor) && value is DataRelation)
			{
				DataRelation dataRelation = (DataRelation)value;
				DataTable table = dataRelation.ParentKey.Table;
				DataTable table2 = dataRelation.ChildKey.Table;
				ConstructorInfo constructor;
				object[] arguments;
				if (string.IsNullOrEmpty(table.Namespace) && string.IsNullOrEmpty(table2.Namespace))
				{
					constructor = typeof(DataRelation).GetConstructor(new Type[]
					{
						typeof(string),
						typeof(string),
						typeof(string),
						typeof(string[]),
						typeof(string[]),
						typeof(bool)
					});
					arguments = new object[]
					{
						dataRelation.RelationName,
						dataRelation.ParentKey.Table.TableName,
						dataRelation.ChildKey.Table.TableName,
						dataRelation.ParentColumnNames,
						dataRelation.ChildColumnNames,
						dataRelation.Nested
					};
				}
				else
				{
					constructor = typeof(DataRelation).GetConstructor(new Type[]
					{
						typeof(string),
						typeof(string),
						typeof(string),
						typeof(string),
						typeof(string),
						typeof(string[]),
						typeof(string[]),
						typeof(bool)
					});
					arguments = new object[]
					{
						dataRelation.RelationName,
						dataRelation.ParentKey.Table.TableName,
						dataRelation.ParentKey.Table.Namespace,
						dataRelation.ChildKey.Table.TableName,
						dataRelation.ChildKey.Table.Namespace,
						dataRelation.ParentColumnNames,
						dataRelation.ChildColumnNames,
						dataRelation.Nested
					};
				}
				return new InstanceDescriptor(constructor, arguments);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
