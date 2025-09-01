using System;
using System.Reflection.Emit;

namespace Mono.CSharp
{
	// Token: 0x020001AD RID: 429
	public class ConvCast : TypeCast
	{
		// Token: 0x060016BA RID: 5818 RVA: 0x0006C8AF File Offset: 0x0006AAAF
		public ConvCast(Expression child, TypeSpec return_type, ConvCast.Mode m) : base(child, return_type)
		{
			this.mode = m;
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x00005936 File Offset: 0x00003B36
		protected override Expression DoResolve(ResolveContext ec)
		{
			return this;
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x0006C8C0 File Offset: 0x0006AAC0
		public override string ToString()
		{
			return string.Format("ConvCast ({0}, {1})", this.mode, this.child);
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x0006C8DD File Offset: 0x0006AADD
		public override void Emit(EmitContext ec)
		{
			base.Emit(ec);
			ConvCast.Emit(ec, this.mode);
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x0006C8F4 File Offset: 0x0006AAF4
		public static void Emit(EmitContext ec, ConvCast.Mode mode)
		{
			if (ec.HasSet(BuilderContext.Options.CheckedScope))
			{
				switch (mode)
				{
				case ConvCast.Mode.I1_U1:
					ec.Emit(OpCodes.Conv_Ovf_U1);
					return;
				case ConvCast.Mode.I1_U2:
					ec.Emit(OpCodes.Conv_Ovf_U2);
					return;
				case ConvCast.Mode.I1_U4:
					ec.Emit(OpCodes.Conv_Ovf_U4);
					return;
				case ConvCast.Mode.I1_U8:
					ec.Emit(OpCodes.Conv_Ovf_U8);
					return;
				case ConvCast.Mode.I1_CH:
					ec.Emit(OpCodes.Conv_Ovf_U2);
					return;
				case ConvCast.Mode.U1_I1:
					ec.Emit(OpCodes.Conv_Ovf_I1_Un);
					return;
				case ConvCast.Mode.U1_CH:
				case ConvCast.Mode.U2_CH:
					break;
				case ConvCast.Mode.I2_I1:
					ec.Emit(OpCodes.Conv_Ovf_I1);
					return;
				case ConvCast.Mode.I2_U1:
					ec.Emit(OpCodes.Conv_Ovf_U1);
					return;
				case ConvCast.Mode.I2_U2:
					ec.Emit(OpCodes.Conv_Ovf_U2);
					return;
				case ConvCast.Mode.I2_U4:
					ec.Emit(OpCodes.Conv_Ovf_U4);
					return;
				case ConvCast.Mode.I2_U8:
					ec.Emit(OpCodes.Conv_Ovf_U8);
					return;
				case ConvCast.Mode.I2_CH:
					ec.Emit(OpCodes.Conv_Ovf_U2);
					return;
				case ConvCast.Mode.U2_I1:
					ec.Emit(OpCodes.Conv_Ovf_I1_Un);
					return;
				case ConvCast.Mode.U2_U1:
					ec.Emit(OpCodes.Conv_Ovf_U1_Un);
					return;
				case ConvCast.Mode.U2_I2:
					ec.Emit(OpCodes.Conv_Ovf_I2_Un);
					return;
				case ConvCast.Mode.I4_I1:
					ec.Emit(OpCodes.Conv_Ovf_I1);
					return;
				case ConvCast.Mode.I4_U1:
					ec.Emit(OpCodes.Conv_Ovf_U1);
					return;
				case ConvCast.Mode.I4_I2:
					ec.Emit(OpCodes.Conv_Ovf_I2);
					return;
				case ConvCast.Mode.I4_U2:
					ec.Emit(OpCodes.Conv_Ovf_U2);
					return;
				case ConvCast.Mode.I4_U4:
					ec.Emit(OpCodes.Conv_Ovf_U4);
					return;
				case ConvCast.Mode.I4_U8:
					ec.Emit(OpCodes.Conv_Ovf_U8);
					return;
				case ConvCast.Mode.I4_CH:
					ec.Emit(OpCodes.Conv_Ovf_U2);
					return;
				case ConvCast.Mode.U4_I1:
					ec.Emit(OpCodes.Conv_Ovf_I1_Un);
					return;
				case ConvCast.Mode.U4_U1:
					ec.Emit(OpCodes.Conv_Ovf_U1_Un);
					return;
				case ConvCast.Mode.U4_I2:
					ec.Emit(OpCodes.Conv_Ovf_I2_Un);
					return;
				case ConvCast.Mode.U4_U2:
					ec.Emit(OpCodes.Conv_Ovf_U2_Un);
					return;
				case ConvCast.Mode.U4_I4:
					ec.Emit(OpCodes.Conv_Ovf_I4_Un);
					return;
				case ConvCast.Mode.U4_CH:
					ec.Emit(OpCodes.Conv_Ovf_U2_Un);
					return;
				case ConvCast.Mode.I8_I1:
					ec.Emit(OpCodes.Conv_Ovf_I1);
					return;
				case ConvCast.Mode.I8_U1:
					ec.Emit(OpCodes.Conv_Ovf_U1);
					return;
				case ConvCast.Mode.I8_I2:
					ec.Emit(OpCodes.Conv_Ovf_I2);
					return;
				case ConvCast.Mode.I8_U2:
					ec.Emit(OpCodes.Conv_Ovf_U2);
					return;
				case ConvCast.Mode.I8_I4:
					ec.Emit(OpCodes.Conv_Ovf_I4);
					return;
				case ConvCast.Mode.I8_U4:
					ec.Emit(OpCodes.Conv_Ovf_U4);
					return;
				case ConvCast.Mode.I8_U8:
					ec.Emit(OpCodes.Conv_Ovf_U8);
					return;
				case ConvCast.Mode.I8_CH:
					ec.Emit(OpCodes.Conv_Ovf_U2);
					return;
				case ConvCast.Mode.I8_I:
					ec.Emit(OpCodes.Conv_Ovf_U);
					return;
				case ConvCast.Mode.U8_I1:
					ec.Emit(OpCodes.Conv_Ovf_I1_Un);
					return;
				case ConvCast.Mode.U8_U1:
					ec.Emit(OpCodes.Conv_Ovf_U1_Un);
					return;
				case ConvCast.Mode.U8_I2:
					ec.Emit(OpCodes.Conv_Ovf_I2_Un);
					return;
				case ConvCast.Mode.U8_U2:
					ec.Emit(OpCodes.Conv_Ovf_U2_Un);
					return;
				case ConvCast.Mode.U8_I4:
					ec.Emit(OpCodes.Conv_Ovf_I4_Un);
					return;
				case ConvCast.Mode.U8_U4:
					ec.Emit(OpCodes.Conv_Ovf_U4_Un);
					return;
				case ConvCast.Mode.U8_I8:
					ec.Emit(OpCodes.Conv_Ovf_I8_Un);
					return;
				case ConvCast.Mode.U8_CH:
					ec.Emit(OpCodes.Conv_Ovf_U2_Un);
					return;
				case ConvCast.Mode.U8_I:
					ec.Emit(OpCodes.Conv_Ovf_U_Un);
					return;
				case ConvCast.Mode.CH_I1:
					ec.Emit(OpCodes.Conv_Ovf_I1_Un);
					return;
				case ConvCast.Mode.CH_U1:
					ec.Emit(OpCodes.Conv_Ovf_U1_Un);
					return;
				case ConvCast.Mode.CH_I2:
					ec.Emit(OpCodes.Conv_Ovf_I2_Un);
					return;
				case ConvCast.Mode.R4_I1:
					ec.Emit(OpCodes.Conv_Ovf_I1);
					return;
				case ConvCast.Mode.R4_U1:
					ec.Emit(OpCodes.Conv_Ovf_U1);
					return;
				case ConvCast.Mode.R4_I2:
					ec.Emit(OpCodes.Conv_Ovf_I2);
					return;
				case ConvCast.Mode.R4_U2:
					ec.Emit(OpCodes.Conv_Ovf_U2);
					return;
				case ConvCast.Mode.R4_I4:
					ec.Emit(OpCodes.Conv_Ovf_I4);
					return;
				case ConvCast.Mode.R4_U4:
					ec.Emit(OpCodes.Conv_Ovf_U4);
					return;
				case ConvCast.Mode.R4_I8:
					ec.Emit(OpCodes.Conv_Ovf_I8);
					return;
				case ConvCast.Mode.R4_U8:
					ec.Emit(OpCodes.Conv_Ovf_U8);
					return;
				case ConvCast.Mode.R4_CH:
					ec.Emit(OpCodes.Conv_Ovf_U2);
					return;
				case ConvCast.Mode.R8_I1:
					ec.Emit(OpCodes.Conv_Ovf_I1);
					return;
				case ConvCast.Mode.R8_U1:
					ec.Emit(OpCodes.Conv_Ovf_U1);
					return;
				case ConvCast.Mode.R8_I2:
					ec.Emit(OpCodes.Conv_Ovf_I2);
					return;
				case ConvCast.Mode.R8_U2:
					ec.Emit(OpCodes.Conv_Ovf_U2);
					return;
				case ConvCast.Mode.R8_I4:
					ec.Emit(OpCodes.Conv_Ovf_I4);
					return;
				case ConvCast.Mode.R8_U4:
					ec.Emit(OpCodes.Conv_Ovf_U4);
					return;
				case ConvCast.Mode.R8_I8:
					ec.Emit(OpCodes.Conv_Ovf_I8);
					return;
				case ConvCast.Mode.R8_U8:
					ec.Emit(OpCodes.Conv_Ovf_U8);
					return;
				case ConvCast.Mode.R8_CH:
					ec.Emit(OpCodes.Conv_Ovf_U2);
					return;
				case ConvCast.Mode.R8_R4:
					ec.Emit(OpCodes.Conv_R4);
					return;
				case ConvCast.Mode.I_I8:
					ec.Emit(OpCodes.Conv_Ovf_I8_Un);
					return;
				default:
					return;
				}
			}
			else
			{
				switch (mode)
				{
				case ConvCast.Mode.I1_U1:
					ec.Emit(OpCodes.Conv_U1);
					return;
				case ConvCast.Mode.I1_U2:
					ec.Emit(OpCodes.Conv_U2);
					return;
				case ConvCast.Mode.I1_U4:
					ec.Emit(OpCodes.Conv_U4);
					return;
				case ConvCast.Mode.I1_U8:
					ec.Emit(OpCodes.Conv_I8);
					return;
				case ConvCast.Mode.I1_CH:
					ec.Emit(OpCodes.Conv_U2);
					return;
				case ConvCast.Mode.U1_I1:
					ec.Emit(OpCodes.Conv_I1);
					return;
				case ConvCast.Mode.U1_CH:
					ec.Emit(OpCodes.Conv_U2);
					return;
				case ConvCast.Mode.I2_I1:
					ec.Emit(OpCodes.Conv_I1);
					return;
				case ConvCast.Mode.I2_U1:
					ec.Emit(OpCodes.Conv_U1);
					return;
				case ConvCast.Mode.I2_U2:
					ec.Emit(OpCodes.Conv_U2);
					return;
				case ConvCast.Mode.I2_U4:
					ec.Emit(OpCodes.Conv_U4);
					return;
				case ConvCast.Mode.I2_U8:
					ec.Emit(OpCodes.Conv_I8);
					return;
				case ConvCast.Mode.I2_CH:
					ec.Emit(OpCodes.Conv_U2);
					return;
				case ConvCast.Mode.U2_I1:
					ec.Emit(OpCodes.Conv_I1);
					return;
				case ConvCast.Mode.U2_U1:
					ec.Emit(OpCodes.Conv_U1);
					return;
				case ConvCast.Mode.U2_I2:
					ec.Emit(OpCodes.Conv_I2);
					return;
				case ConvCast.Mode.U2_CH:
				case ConvCast.Mode.I4_U4:
				case ConvCast.Mode.U4_I4:
				case ConvCast.Mode.I8_U8:
				case ConvCast.Mode.U8_I8:
					break;
				case ConvCast.Mode.I4_I1:
					ec.Emit(OpCodes.Conv_I1);
					return;
				case ConvCast.Mode.I4_U1:
					ec.Emit(OpCodes.Conv_U1);
					return;
				case ConvCast.Mode.I4_I2:
					ec.Emit(OpCodes.Conv_I2);
					return;
				case ConvCast.Mode.I4_U2:
					ec.Emit(OpCodes.Conv_U2);
					return;
				case ConvCast.Mode.I4_U8:
					ec.Emit(OpCodes.Conv_I8);
					return;
				case ConvCast.Mode.I4_CH:
					ec.Emit(OpCodes.Conv_U2);
					return;
				case ConvCast.Mode.U4_I1:
					ec.Emit(OpCodes.Conv_I1);
					return;
				case ConvCast.Mode.U4_U1:
					ec.Emit(OpCodes.Conv_U1);
					return;
				case ConvCast.Mode.U4_I2:
					ec.Emit(OpCodes.Conv_I2);
					return;
				case ConvCast.Mode.U4_U2:
					ec.Emit(OpCodes.Conv_U2);
					return;
				case ConvCast.Mode.U4_CH:
					ec.Emit(OpCodes.Conv_U2);
					return;
				case ConvCast.Mode.I8_I1:
					ec.Emit(OpCodes.Conv_I1);
					return;
				case ConvCast.Mode.I8_U1:
					ec.Emit(OpCodes.Conv_U1);
					return;
				case ConvCast.Mode.I8_I2:
					ec.Emit(OpCodes.Conv_I2);
					return;
				case ConvCast.Mode.I8_U2:
					ec.Emit(OpCodes.Conv_U2);
					return;
				case ConvCast.Mode.I8_I4:
					ec.Emit(OpCodes.Conv_I4);
					return;
				case ConvCast.Mode.I8_U4:
					ec.Emit(OpCodes.Conv_U4);
					return;
				case ConvCast.Mode.I8_CH:
					ec.Emit(OpCodes.Conv_U2);
					return;
				case ConvCast.Mode.I8_I:
					ec.Emit(OpCodes.Conv_U);
					return;
				case ConvCast.Mode.U8_I1:
					ec.Emit(OpCodes.Conv_I1);
					return;
				case ConvCast.Mode.U8_U1:
					ec.Emit(OpCodes.Conv_U1);
					return;
				case ConvCast.Mode.U8_I2:
					ec.Emit(OpCodes.Conv_I2);
					return;
				case ConvCast.Mode.U8_U2:
					ec.Emit(OpCodes.Conv_U2);
					return;
				case ConvCast.Mode.U8_I4:
					ec.Emit(OpCodes.Conv_I4);
					return;
				case ConvCast.Mode.U8_U4:
					ec.Emit(OpCodes.Conv_U4);
					return;
				case ConvCast.Mode.U8_CH:
					ec.Emit(OpCodes.Conv_U2);
					return;
				case ConvCast.Mode.U8_I:
					ec.Emit(OpCodes.Conv_U);
					return;
				case ConvCast.Mode.CH_I1:
					ec.Emit(OpCodes.Conv_I1);
					return;
				case ConvCast.Mode.CH_U1:
					ec.Emit(OpCodes.Conv_U1);
					return;
				case ConvCast.Mode.CH_I2:
					ec.Emit(OpCodes.Conv_I2);
					return;
				case ConvCast.Mode.R4_I1:
					ec.Emit(OpCodes.Conv_I1);
					return;
				case ConvCast.Mode.R4_U1:
					ec.Emit(OpCodes.Conv_U1);
					return;
				case ConvCast.Mode.R4_I2:
					ec.Emit(OpCodes.Conv_I2);
					return;
				case ConvCast.Mode.R4_U2:
					ec.Emit(OpCodes.Conv_U2);
					return;
				case ConvCast.Mode.R4_I4:
					ec.Emit(OpCodes.Conv_I4);
					return;
				case ConvCast.Mode.R4_U4:
					ec.Emit(OpCodes.Conv_U4);
					return;
				case ConvCast.Mode.R4_I8:
					ec.Emit(OpCodes.Conv_I8);
					return;
				case ConvCast.Mode.R4_U8:
					ec.Emit(OpCodes.Conv_U8);
					return;
				case ConvCast.Mode.R4_CH:
					ec.Emit(OpCodes.Conv_U2);
					return;
				case ConvCast.Mode.R8_I1:
					ec.Emit(OpCodes.Conv_I1);
					return;
				case ConvCast.Mode.R8_U1:
					ec.Emit(OpCodes.Conv_U1);
					return;
				case ConvCast.Mode.R8_I2:
					ec.Emit(OpCodes.Conv_I2);
					return;
				case ConvCast.Mode.R8_U2:
					ec.Emit(OpCodes.Conv_U2);
					return;
				case ConvCast.Mode.R8_I4:
					ec.Emit(OpCodes.Conv_I4);
					return;
				case ConvCast.Mode.R8_U4:
					ec.Emit(OpCodes.Conv_U4);
					return;
				case ConvCast.Mode.R8_I8:
					ec.Emit(OpCodes.Conv_I8);
					return;
				case ConvCast.Mode.R8_U8:
					ec.Emit(OpCodes.Conv_U8);
					return;
				case ConvCast.Mode.R8_CH:
					ec.Emit(OpCodes.Conv_U2);
					return;
				case ConvCast.Mode.R8_R4:
					ec.Emit(OpCodes.Conv_R4);
					return;
				case ConvCast.Mode.I_I8:
					ec.Emit(OpCodes.Conv_U8);
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x04000957 RID: 2391
		private ConvCast.Mode mode;

		// Token: 0x020003A4 RID: 932
		public enum Mode : byte
		{
			// Token: 0x0400100C RID: 4108
			I1_U1,
			// Token: 0x0400100D RID: 4109
			I1_U2,
			// Token: 0x0400100E RID: 4110
			I1_U4,
			// Token: 0x0400100F RID: 4111
			I1_U8,
			// Token: 0x04001010 RID: 4112
			I1_CH,
			// Token: 0x04001011 RID: 4113
			U1_I1,
			// Token: 0x04001012 RID: 4114
			U1_CH,
			// Token: 0x04001013 RID: 4115
			I2_I1,
			// Token: 0x04001014 RID: 4116
			I2_U1,
			// Token: 0x04001015 RID: 4117
			I2_U2,
			// Token: 0x04001016 RID: 4118
			I2_U4,
			// Token: 0x04001017 RID: 4119
			I2_U8,
			// Token: 0x04001018 RID: 4120
			I2_CH,
			// Token: 0x04001019 RID: 4121
			U2_I1,
			// Token: 0x0400101A RID: 4122
			U2_U1,
			// Token: 0x0400101B RID: 4123
			U2_I2,
			// Token: 0x0400101C RID: 4124
			U2_CH,
			// Token: 0x0400101D RID: 4125
			I4_I1,
			// Token: 0x0400101E RID: 4126
			I4_U1,
			// Token: 0x0400101F RID: 4127
			I4_I2,
			// Token: 0x04001020 RID: 4128
			I4_U2,
			// Token: 0x04001021 RID: 4129
			I4_U4,
			// Token: 0x04001022 RID: 4130
			I4_U8,
			// Token: 0x04001023 RID: 4131
			I4_CH,
			// Token: 0x04001024 RID: 4132
			U4_I1,
			// Token: 0x04001025 RID: 4133
			U4_U1,
			// Token: 0x04001026 RID: 4134
			U4_I2,
			// Token: 0x04001027 RID: 4135
			U4_U2,
			// Token: 0x04001028 RID: 4136
			U4_I4,
			// Token: 0x04001029 RID: 4137
			U4_CH,
			// Token: 0x0400102A RID: 4138
			I8_I1,
			// Token: 0x0400102B RID: 4139
			I8_U1,
			// Token: 0x0400102C RID: 4140
			I8_I2,
			// Token: 0x0400102D RID: 4141
			I8_U2,
			// Token: 0x0400102E RID: 4142
			I8_I4,
			// Token: 0x0400102F RID: 4143
			I8_U4,
			// Token: 0x04001030 RID: 4144
			I8_U8,
			// Token: 0x04001031 RID: 4145
			I8_CH,
			// Token: 0x04001032 RID: 4146
			I8_I,
			// Token: 0x04001033 RID: 4147
			U8_I1,
			// Token: 0x04001034 RID: 4148
			U8_U1,
			// Token: 0x04001035 RID: 4149
			U8_I2,
			// Token: 0x04001036 RID: 4150
			U8_U2,
			// Token: 0x04001037 RID: 4151
			U8_I4,
			// Token: 0x04001038 RID: 4152
			U8_U4,
			// Token: 0x04001039 RID: 4153
			U8_I8,
			// Token: 0x0400103A RID: 4154
			U8_CH,
			// Token: 0x0400103B RID: 4155
			U8_I,
			// Token: 0x0400103C RID: 4156
			CH_I1,
			// Token: 0x0400103D RID: 4157
			CH_U1,
			// Token: 0x0400103E RID: 4158
			CH_I2,
			// Token: 0x0400103F RID: 4159
			R4_I1,
			// Token: 0x04001040 RID: 4160
			R4_U1,
			// Token: 0x04001041 RID: 4161
			R4_I2,
			// Token: 0x04001042 RID: 4162
			R4_U2,
			// Token: 0x04001043 RID: 4163
			R4_I4,
			// Token: 0x04001044 RID: 4164
			R4_U4,
			// Token: 0x04001045 RID: 4165
			R4_I8,
			// Token: 0x04001046 RID: 4166
			R4_U8,
			// Token: 0x04001047 RID: 4167
			R4_CH,
			// Token: 0x04001048 RID: 4168
			R8_I1,
			// Token: 0x04001049 RID: 4169
			R8_U1,
			// Token: 0x0400104A RID: 4170
			R8_I2,
			// Token: 0x0400104B RID: 4171
			R8_U2,
			// Token: 0x0400104C RID: 4172
			R8_I4,
			// Token: 0x0400104D RID: 4173
			R8_U4,
			// Token: 0x0400104E RID: 4174
			R8_I8,
			// Token: 0x0400104F RID: 4175
			R8_U8,
			// Token: 0x04001050 RID: 4176
			R8_CH,
			// Token: 0x04001051 RID: 4177
			R8_R4,
			// Token: 0x04001052 RID: 4178
			I_I8
		}
	}
}
