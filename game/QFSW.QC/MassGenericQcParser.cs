using System;
using System.Collections.Generic;

namespace QFSW.QC
{
	// Token: 0x02000024 RID: 36
	public abstract class MassGenericQcParser : IQcParser
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600007C RID: 124
		protected abstract HashSet<Type> GenericTypes { get; }

		// Token: 0x0600007D RID: 125 RVA: 0x000033AC File Offset: 0x000015AC
		protected MassGenericQcParser()
		{
			foreach (Type type in this.GenericTypes)
			{
				if (!type.IsGenericType)
				{
					throw new ArgumentException("Generic Parsers must use a generic type as their base");
				}
				if (type.IsConstructedGenericType)
				{
					throw new ArgumentException("Generic Parsers must use an incomplete generic type as their base");
				}
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003424 File Offset: 0x00001624
		public virtual int Priority
		{
			get
			{
				return -2000;
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000342B File Offset: 0x0000162B
		public bool CanParse(Type type)
		{
			return type.IsGenericType && this.GenericTypes.Contains(type.GetGenericTypeDefinition());
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003448 File Offset: 0x00001648
		public virtual object Parse(string value, Type type, Func<string, Type, object> recursiveParser)
		{
			this._recursiveParser = recursiveParser;
			return this.Parse(value, type);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003459 File Offset: 0x00001659
		protected object ParseRecursive(string value, Type type)
		{
			return this._recursiveParser(value, type);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003468 File Offset: 0x00001668
		protected TElement ParseRecursive<TElement>(string value)
		{
			return (TElement)((object)this._recursiveParser(value, typeof(TElement)));
		}

		// Token: 0x06000083 RID: 131
		public abstract object Parse(string value, Type type);

		// Token: 0x04000043 RID: 67
		private Func<string, Type, object> _recursiveParser;
	}
}
