using System;
using System.Reflection;
using System.Text;

namespace Mono.CSharp
{
	// Token: 0x0200026E RID: 622
	public abstract class AParametersCollection
	{
		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06001E90 RID: 7824 RVA: 0x00096753 File Offset: 0x00094953
		public CallingConventions CallingConvention
		{
			get
			{
				if (!this.has_arglist)
				{
					return CallingConventions.Standard;
				}
				return CallingConventions.VarArgs;
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06001E91 RID: 7825 RVA: 0x00096760 File Offset: 0x00094960
		public int Count
		{
			get
			{
				return this.parameters.Length;
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06001E92 RID: 7826 RVA: 0x0009676A File Offset: 0x0009496A
		public TypeSpec ExtensionMethodType
		{
			get
			{
				if (this.Count == 0)
				{
					return null;
				}
				if (!this.FixedParameters[0].HasExtensionMethodModifier)
				{
					return null;
				}
				return this.types[0];
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06001E93 RID: 7827 RVA: 0x0009678F File Offset: 0x0009498F
		public IParameterData[] FixedParameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x06001E94 RID: 7828 RVA: 0x00096797 File Offset: 0x00094997
		public static ParameterAttributes GetParameterAttribute(Parameter.Modifier modFlags)
		{
			if ((modFlags & Parameter.Modifier.OUT) == Parameter.Modifier.NONE)
			{
				return ParameterAttributes.None;
			}
			return ParameterAttributes.Out;
		}

		// Token: 0x06001E95 RID: 7829 RVA: 0x000967A4 File Offset: 0x000949A4
		public Type[] GetMetaInfo()
		{
			Type[] array;
			if (this.has_arglist)
			{
				if (this.Count == 1)
				{
					return Type.EmptyTypes;
				}
				array = new Type[this.Count - 1];
			}
			else
			{
				if (this.Count == 0)
				{
					return Type.EmptyTypes;
				}
				array = new Type[this.Count];
			}
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.Types[i].GetMetaInfo();
				if ((this.FixedParameters[i].ModFlags & Parameter.Modifier.RefOutMask) != Parameter.Modifier.NONE)
				{
					array[i] = array[i].MakeByRefType();
				}
			}
			return array;
		}

		// Token: 0x06001E96 RID: 7830 RVA: 0x00096830 File Offset: 0x00094A30
		public int GetParameterIndexByName(string name)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (this.parameters[i].Name == name)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001E97 RID: 7831 RVA: 0x00096868 File Offset: 0x00094A68
		public string GetSignatureForDocumentation()
		{
			if (this.IsEmpty)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder("(");
			for (int i = 0; i < this.Count; i++)
			{
				if (i != 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(this.types[i].GetSignatureForDocumentation());
				if ((this.parameters[i].ModFlags & Parameter.Modifier.RefOutMask) != Parameter.Modifier.NONE)
				{
					stringBuilder.Append("@");
				}
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x06001E98 RID: 7832 RVA: 0x000968F1 File Offset: 0x00094AF1
		public string GetSignatureForError()
		{
			return this.GetSignatureForError("(", ")", this.Count);
		}

		// Token: 0x06001E99 RID: 7833 RVA: 0x0009690C File Offset: 0x00094B0C
		public string GetSignatureForError(string start, string end, int count)
		{
			StringBuilder stringBuilder = new StringBuilder(start);
			for (int i = 0; i < count; i++)
			{
				if (i != 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(this.ParameterDesc(i));
			}
			stringBuilder.Append(end);
			return stringBuilder.ToString();
		}

		// Token: 0x06001E9A RID: 7834 RVA: 0x00096958 File Offset: 0x00094B58
		public static bool HasSameParameterDefaults(AParametersCollection a, AParametersCollection b)
		{
			if (a == null)
			{
				return b == null;
			}
			for (int i = 0; i < a.Count; i++)
			{
				if (a.FixedParameters[i].HasDefaultValue != b.FixedParameters[i].HasDefaultValue)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06001E9B RID: 7835 RVA: 0x0009699D File Offset: 0x00094B9D
		public bool HasArglist
		{
			get
			{
				return this.has_arglist;
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06001E9C RID: 7836 RVA: 0x000969A5 File Offset: 0x00094BA5
		public bool HasExtensionMethodType
		{
			get
			{
				return this.Count != 0 && this.FixedParameters[0].HasExtensionMethodModifier;
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06001E9D RID: 7837 RVA: 0x000969BE File Offset: 0x00094BBE
		public bool HasParams
		{
			get
			{
				return this.has_params;
			}
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06001E9E RID: 7838 RVA: 0x000969C6 File Offset: 0x00094BC6
		public bool IsEmpty
		{
			get
			{
				return this.parameters.Length == 0;
			}
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x000969D4 File Offset: 0x00094BD4
		public AParametersCollection Inflate(TypeParameterInflator inflator)
		{
			TypeSpec[] array = null;
			bool flag = false;
			int i = 0;
			while (i < this.Count)
			{
				TypeSpec typeSpec = inflator.Inflate(this.types[i]);
				if (array == null)
				{
					if (typeSpec != this.types[i])
					{
						flag |= this.FixedParameters[i].HasDefaultValue;
						array = new TypeSpec[this.types.Length];
						Array.Copy(this.types, array, this.types.Length);
						goto IL_78;
					}
				}
				else if (typeSpec != this.types[i])
				{
					flag |= this.FixedParameters[i].HasDefaultValue;
					goto IL_78;
				}
				IL_7D:
				i++;
				continue;
				IL_78:
				array[i] = typeSpec;
				goto IL_7D;
			}
			if (array == null)
			{
				return this;
			}
			AParametersCollection aparametersCollection = (AParametersCollection)base.MemberwiseClone();
			aparametersCollection.types = array;
			if (flag)
			{
				aparametersCollection.parameters = new IParameterData[this.Count];
				for (int j = 0; j < this.Count; j++)
				{
					IParameterData parameterData = this.FixedParameters[j];
					aparametersCollection.FixedParameters[j] = parameterData;
					if (parameterData.HasDefaultValue)
					{
						Expression expression = parameterData.DefaultValue;
						if (array[j] != expression.Type)
						{
							Constant constant = expression as Constant;
							if (constant != null)
							{
								constant = Constant.ExtractConstantFromValue(array[j], constant.GetValue(), expression.Location);
								if (constant == null)
								{
									expression = new DefaultValueExpression(new TypeExpression(array[j], expression.Location), expression.Location);
								}
								else
								{
									expression = constant;
								}
							}
							else if (expression is DefaultValueExpression)
							{
								expression = new DefaultValueExpression(new TypeExpression(array[j], expression.Location), expression.Location);
							}
							aparametersCollection.FixedParameters[j] = new ParameterData(parameterData.Name, parameterData.ModFlags, expression);
						}
					}
				}
			}
			return aparametersCollection;
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x00096B8C File Offset: 0x00094D8C
		public string ParameterDesc(int pos)
		{
			if (this.types == null || this.types[pos] == null)
			{
				return ((Parameter)this.FixedParameters[pos]).GetSignatureForError();
			}
			string signatureForError = this.types[pos].GetSignatureForError();
			if (this.FixedParameters[pos].HasExtensionMethodModifier)
			{
				return "this " + signatureForError;
			}
			Parameter.Modifier modifier = this.FixedParameters[pos].ModFlags & Parameter.Modifier.ModifierMask;
			if (modifier == Parameter.Modifier.NONE)
			{
				return signatureForError;
			}
			return Parameter.GetModifierSignature(modifier) + " " + signatureForError;
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06001EA1 RID: 7841 RVA: 0x00096C0E File Offset: 0x00094E0E
		// (set) Token: 0x06001EA2 RID: 7842 RVA: 0x00096C16 File Offset: 0x00094E16
		public TypeSpec[] Types
		{
			get
			{
				return this.types;
			}
			set
			{
				this.types = value;
			}
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x00002CCC File Offset: 0x00000ECC
		protected AParametersCollection()
		{
		}

		// Token: 0x04000B4E RID: 2894
		protected bool has_arglist;

		// Token: 0x04000B4F RID: 2895
		protected bool has_params;

		// Token: 0x04000B50 RID: 2896
		protected IParameterData[] parameters;

		// Token: 0x04000B51 RID: 2897
		protected TypeSpec[] types;
	}
}
