using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000509 RID: 1289
	internal sealed class AllElementsContentValidator : ContentValidator
	{
		// Token: 0x0600347C RID: 13436 RVA: 0x001292BA File Offset: 0x001274BA
		public AllElementsContentValidator(XmlSchemaContentType contentType, int size, bool isEmptiable) : base(contentType, false, isEmptiable)
		{
			this.elements = new Hashtable(size);
			this.particles = new object[size];
			this.isRequired = new BitSet(size);
		}

		// Token: 0x0600347D RID: 13437 RVA: 0x001292EC File Offset: 0x001274EC
		public bool AddElement(XmlQualifiedName name, object particle, bool isEmptiable)
		{
			if (this.elements[name] != null)
			{
				return false;
			}
			int count = this.elements.Count;
			this.elements.Add(name, count);
			this.particles[count] = particle;
			if (!isEmptiable)
			{
				this.isRequired.Set(count);
				this.countRequired++;
			}
			return true;
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x0600347E RID: 13438 RVA: 0x0012934E File Offset: 0x0012754E
		public override bool IsEmptiable
		{
			get
			{
				return base.IsEmptiable || this.countRequired == 0;
			}
		}

		// Token: 0x0600347F RID: 13439 RVA: 0x00129363 File Offset: 0x00127563
		public override void InitValidation(ValidationState context)
		{
			context.AllElementsSet = new BitSet(this.elements.Count);
			context.CurrentState.AllElementsRequired = -1;
		}

		// Token: 0x06003480 RID: 13440 RVA: 0x00129388 File Offset: 0x00127588
		public override object ValidateElement(XmlQualifiedName name, ValidationState context, out int errorCode)
		{
			object obj = this.elements[name];
			errorCode = 0;
			if (obj == null)
			{
				context.NeedValidateChildren = false;
				return null;
			}
			int num = (int)obj;
			if (context.AllElementsSet[num])
			{
				errorCode = -2;
				return null;
			}
			if (context.CurrentState.AllElementsRequired == -1)
			{
				context.CurrentState.AllElementsRequired = 0;
			}
			context.AllElementsSet.Set(num);
			if (this.isRequired[num])
			{
				context.CurrentState.AllElementsRequired = context.CurrentState.AllElementsRequired + 1;
			}
			return this.particles[num];
		}

		// Token: 0x06003481 RID: 13441 RVA: 0x00129418 File Offset: 0x00127618
		public override bool CompleteValidation(ValidationState context)
		{
			return context.CurrentState.AllElementsRequired == this.countRequired || (this.IsEmptiable && context.CurrentState.AllElementsRequired == -1);
		}

		// Token: 0x06003482 RID: 13442 RVA: 0x00129448 File Offset: 0x00127648
		public override ArrayList ExpectedElements(ValidationState context, bool isRequiredOnly)
		{
			ArrayList arrayList = null;
			foreach (object obj in this.elements)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (!context.AllElementsSet[(int)dictionaryEntry.Value] && (!isRequiredOnly || this.isRequired[(int)dictionaryEntry.Value]))
				{
					if (arrayList == null)
					{
						arrayList = new ArrayList();
					}
					arrayList.Add(dictionaryEntry.Key);
				}
			}
			return arrayList;
		}

		// Token: 0x06003483 RID: 13443 RVA: 0x001294EC File Offset: 0x001276EC
		public override ArrayList ExpectedParticles(ValidationState context, bool isRequiredOnly, XmlSchemaSet schemaSet)
		{
			ArrayList result = new ArrayList();
			foreach (object obj in this.elements)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (!context.AllElementsSet[(int)dictionaryEntry.Value] && (!isRequiredOnly || this.isRequired[(int)dictionaryEntry.Value]))
				{
					ContentValidator.AddParticleToExpected(this.particles[(int)dictionaryEntry.Value] as XmlSchemaParticle, schemaSet, result);
				}
			}
			return result;
		}

		// Token: 0x040026FF RID: 9983
		private Hashtable elements;

		// Token: 0x04002700 RID: 9984
		private object[] particles;

		// Token: 0x04002701 RID: 9985
		private BitSet isRequired;

		// Token: 0x04002702 RID: 9986
		private int countRequired;
	}
}
