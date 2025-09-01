using System;
using System.Globalization;

namespace System.Xml.Schema
{
	// Token: 0x020004F1 RID: 1265
	internal class TypedObject
	{
		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x060033D8 RID: 13272 RVA: 0x00126B7E File Offset: 0x00124D7E
		public int Dim
		{
			get
			{
				return this.dim;
			}
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x060033D9 RID: 13273 RVA: 0x00126B86 File Offset: 0x00124D86
		public bool IsList
		{
			get
			{
				return this.isList;
			}
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x060033DA RID: 13274 RVA: 0x00126B8E File Offset: 0x00124D8E
		public bool IsDecimal
		{
			get
			{
				return this.dstruct.IsDecimal;
			}
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x060033DB RID: 13275 RVA: 0x00126B9B File Offset: 0x00124D9B
		public decimal[] Dvalue
		{
			get
			{
				return this.dstruct.Dvalue;
			}
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x060033DC RID: 13276 RVA: 0x00126BA8 File Offset: 0x00124DA8
		// (set) Token: 0x060033DD RID: 13277 RVA: 0x00126BB0 File Offset: 0x00124DB0
		public object Value
		{
			get
			{
				return this.ovalue;
			}
			set
			{
				this.ovalue = value;
			}
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x060033DE RID: 13278 RVA: 0x00126BB9 File Offset: 0x00124DB9
		// (set) Token: 0x060033DF RID: 13279 RVA: 0x00126BC1 File Offset: 0x00124DC1
		public XmlSchemaDatatype Type
		{
			get
			{
				return this.xsdtype;
			}
			set
			{
				this.xsdtype = value;
			}
		}

		// Token: 0x060033E0 RID: 13280 RVA: 0x00126BCC File Offset: 0x00124DCC
		public TypedObject(object obj, string svalue, XmlSchemaDatatype xsdtype)
		{
			this.ovalue = obj;
			this.svalue = svalue;
			this.xsdtype = xsdtype;
			if (xsdtype.Variety == XmlSchemaDatatypeVariety.List || xsdtype is Datatype_base64Binary || xsdtype is Datatype_hexBinary)
			{
				this.isList = true;
				this.dim = ((Array)obj).Length;
			}
		}

		// Token: 0x060033E1 RID: 13281 RVA: 0x00126C2C File Offset: 0x00124E2C
		public override string ToString()
		{
			return this.svalue;
		}

		// Token: 0x060033E2 RID: 13282 RVA: 0x00126C34 File Offset: 0x00124E34
		public void SetDecimal()
		{
			if (this.dstruct != null)
			{
				return;
			}
			XmlTypeCode typeCode = this.xsdtype.TypeCode;
			if (typeCode == XmlTypeCode.Decimal || typeCode - XmlTypeCode.Integer <= 12)
			{
				if (this.isList)
				{
					this.dstruct = new TypedObject.DecimalStruct(this.dim);
					for (int i = 0; i < this.dim; i++)
					{
						this.dstruct.Dvalue[i] = Convert.ToDecimal(((Array)this.ovalue).GetValue(i), NumberFormatInfo.InvariantInfo);
					}
				}
				else
				{
					this.dstruct = new TypedObject.DecimalStruct();
					this.dstruct.Dvalue[0] = Convert.ToDecimal(this.ovalue, NumberFormatInfo.InvariantInfo);
				}
				this.dstruct.IsDecimal = true;
				return;
			}
			if (this.isList)
			{
				this.dstruct = new TypedObject.DecimalStruct(this.dim);
				return;
			}
			this.dstruct = new TypedObject.DecimalStruct();
		}

		// Token: 0x060033E3 RID: 13283 RVA: 0x00126D1C File Offset: 0x00124F1C
		private bool ListDValueEquals(TypedObject other)
		{
			for (int i = 0; i < this.Dim; i++)
			{
				if (this.Dvalue[i] != other.Dvalue[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060033E4 RID: 13284 RVA: 0x00126D5C File Offset: 0x00124F5C
		public bool Equals(TypedObject other)
		{
			if (this.Dim != other.Dim)
			{
				return false;
			}
			if (this.Type != other.Type)
			{
				if (!this.Type.IsComparable(other.Type))
				{
					return false;
				}
				other.SetDecimal();
				this.SetDecimal();
				if (this.IsDecimal && other.IsDecimal)
				{
					return this.ListDValueEquals(other);
				}
			}
			if (this.IsList)
			{
				if (other.IsList)
				{
					return this.Type.Compare(this.Value, other.Value) == 0;
				}
				Array array = this.Value as Array;
				XmlAtomicValue[] array2 = array as XmlAtomicValue[];
				if (array2 != null)
				{
					return array2.Length == 1 && array2.GetValue(0).Equals(other.Value);
				}
				return array.Length == 1 && array.GetValue(0).Equals(other.Value);
			}
			else
			{
				if (!other.IsList)
				{
					return this.Value.Equals(other.Value);
				}
				Array array3 = other.Value as Array;
				XmlAtomicValue[] array4 = array3 as XmlAtomicValue[];
				if (array4 != null)
				{
					return array4.Length == 1 && array4.GetValue(0).Equals(this.Value);
				}
				return array3.Length == 1 && array3.GetValue(0).Equals(this.Value);
			}
		}

		// Token: 0x040026BC RID: 9916
		private TypedObject.DecimalStruct dstruct;

		// Token: 0x040026BD RID: 9917
		private object ovalue;

		// Token: 0x040026BE RID: 9918
		private string svalue;

		// Token: 0x040026BF RID: 9919
		private XmlSchemaDatatype xsdtype;

		// Token: 0x040026C0 RID: 9920
		private int dim = 1;

		// Token: 0x040026C1 RID: 9921
		private bool isList;

		// Token: 0x020004F2 RID: 1266
		private class DecimalStruct
		{
			// Token: 0x17000930 RID: 2352
			// (get) Token: 0x060033E5 RID: 13285 RVA: 0x00126EA2 File Offset: 0x001250A2
			// (set) Token: 0x060033E6 RID: 13286 RVA: 0x00126EAA File Offset: 0x001250AA
			public bool IsDecimal
			{
				get
				{
					return this.isDecimal;
				}
				set
				{
					this.isDecimal = value;
				}
			}

			// Token: 0x17000931 RID: 2353
			// (get) Token: 0x060033E7 RID: 13287 RVA: 0x00126EB3 File Offset: 0x001250B3
			public decimal[] Dvalue
			{
				get
				{
					return this.dvalue;
				}
			}

			// Token: 0x060033E8 RID: 13288 RVA: 0x00126EBB File Offset: 0x001250BB
			public DecimalStruct()
			{
				this.dvalue = new decimal[1];
			}

			// Token: 0x060033E9 RID: 13289 RVA: 0x00126ECF File Offset: 0x001250CF
			public DecimalStruct(int dim)
			{
				this.dvalue = new decimal[dim];
			}

			// Token: 0x040026C2 RID: 9922
			private bool isDecimal;

			// Token: 0x040026C3 RID: 9923
			private decimal[] dvalue;
		}
	}
}
