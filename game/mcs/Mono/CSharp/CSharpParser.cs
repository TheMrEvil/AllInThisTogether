using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Mono.CSharp.Linq;
using Mono.CSharp.Nullable;
using Mono.CSharp.yydebug;
using Mono.CSharp.yyParser;

namespace Mono.CSharp
{
	// Token: 0x02000186 RID: 390
	public class CSharpParser
	{
		// Token: 0x06001270 RID: 4720 RVA: 0x0004D321 File Offset: 0x0004B521
		public void yyerror(string message)
		{
			this.yyerror(message, null);
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x0004D32C File Offset: 0x0004B52C
		public void yyerror(string message, string[] expected)
		{
			if (this.yacc_verbose_flag > 0 && expected != null && expected.Length != 0)
			{
				this.ErrorOutput.Write(message + ", expecting");
				for (int i = 0; i < expected.Length; i++)
				{
					this.ErrorOutput.Write(" " + expected[i]);
				}
				this.ErrorOutput.WriteLine();
				return;
			}
			this.ErrorOutput.WriteLine(message);
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x0004D3A0 File Offset: 0x0004B5A0
		public static string yyname(int token)
		{
			if (token < 0 || token > CSharpParser.yyNames.Length)
			{
				return "[illegal]";
			}
			string result;
			if ((result = CSharpParser.yyNames[token]) != null)
			{
				return result;
			}
			return "[unknown]";
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x0004D3D4 File Offset: 0x0004B5D4
		protected int[] yyExpectingTokens(int state)
		{
			int num = 0;
			bool[] array = new bool[CSharpParser.yyNames.Length];
			int i;
			int num2;
			if ((i = (int)CSharpParser.yySindex[state]) != 0)
			{
				num2 = ((i < 0) ? (-i) : 0);
				while (num2 < CSharpParser.yyNames.Length && i + num2 < CSharpParser.yyTable.Length)
				{
					if ((int)CSharpParser.yyCheck[i + num2] == num2 && !array[num2] && CSharpParser.yyNames[num2] != null)
					{
						num++;
						array[num2] = true;
					}
					num2++;
				}
			}
			if ((i = (int)CSharpParser.yyRindex[state]) != 0)
			{
				num2 = ((i < 0) ? (-i) : 0);
				while (num2 < CSharpParser.yyNames.Length && i + num2 < CSharpParser.yyTable.Length)
				{
					if ((int)CSharpParser.yyCheck[i + num2] == num2 && !array[num2] && CSharpParser.yyNames[num2] != null)
					{
						num++;
						array[num2] = true;
					}
					num2++;
				}
			}
			int[] array2 = new int[num];
			num2 = (i = 0);
			while (i < num)
			{
				if (array[num2])
				{
					array2[i++] = num2;
				}
				num2++;
			}
			return array2;
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x0004D4BC File Offset: 0x0004B6BC
		protected string[] yyExpecting(int state)
		{
			int[] array = this.yyExpectingTokens(state);
			string[] array2 = new string[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i++] = CSharpParser.yyNames[array[i]];
			}
			return array2;
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x0004D4F9 File Offset: 0x0004B6F9
		public object yyparse(yyInput yyLex, object yyd)
		{
			this.debug = (yyDebug)yyd;
			return this.yyparse(yyLex);
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x0004D50E File Offset: 0x0004B70E
		protected object yyDefault(object first)
		{
			return first;
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x0004D514 File Offset: 0x0004B714
		public object yyparse(yyInput yyLex)
		{
			if (this.yyMax <= 0)
			{
				this.yyMax = 256;
			}
			int num = 0;
			this.yyVal = null;
			this.yyToken = -1;
			int num2 = 0;
			int[] array;
			if (this.use_global_stacks && CSharpParser.global_yyStates != null)
			{
				this.yyVals = CSharpParser.global_yyVals;
				array = CSharpParser.global_yyStates;
			}
			else
			{
				this.yyVals = new object[this.yyMax];
				array = new int[this.yyMax];
				if (this.use_global_stacks)
				{
					CSharpParser.global_yyVals = this.yyVals;
					CSharpParser.global_yyStates = array;
				}
			}
			this.yyTop = 0;
			for (;;)
			{
				IL_85:
				if (this.yyTop >= array.Length)
				{
					Array.Resize<int>(ref array, array.Length + this.yyMax);
					Array.Resize<object>(ref this.yyVals, this.yyVals.Length + this.yyMax);
				}
				array[this.yyTop] = num;
				this.yyVals[this.yyTop] = this.yyVal;
				if (this.debug != null)
				{
					this.debug.push(num, this.yyVal);
				}
				int num3;
				while ((num3 = (int)CSharpParser.yyDefRed[num]) == 0)
				{
					if (this.yyToken < 0)
					{
						this.yyToken = (yyLex.advance() ? yyLex.token() : 0);
						if (this.debug != null)
						{
							this.debug.lex(num, this.yyToken, CSharpParser.yyname(this.yyToken), yyLex.value());
						}
					}
					if ((num3 = (int)CSharpParser.yySindex[num]) != 0 && (num3 += this.yyToken) >= 0 && num3 < CSharpParser.yyTable.Length && (int)CSharpParser.yyCheck[num3] == this.yyToken)
					{
						if (this.debug != null)
						{
							this.debug.shift(num, (int)CSharpParser.yyTable[num3], num2 - 1);
						}
						num = (int)CSharpParser.yyTable[num3];
						this.yyVal = yyLex.value();
						this.yyToken = -1;
						if (num2 > 0)
						{
							num2--;
						}
					}
					else
					{
						if ((num3 = (int)CSharpParser.yyRindex[num]) != 0 && (num3 += this.yyToken) >= 0 && num3 < CSharpParser.yyTable.Length && (int)CSharpParser.yyCheck[num3] == this.yyToken)
						{
							num3 = (int)CSharpParser.yyTable[num3];
							break;
						}
						switch (num2)
						{
						case 0:
							this.yyExpectingState = num;
							if (this.debug != null)
							{
								this.debug.error("syntax error");
							}
							if (this.yyToken == 0 || this.yyToken == this.eof_token)
							{
								goto IL_253;
							}
							break;
						case 1:
						case 2:
							break;
						case 3:
							if (this.yyToken == 0)
							{
								goto Block_32;
							}
							if (this.debug != null)
							{
								this.debug.discard(num, this.yyToken, CSharpParser.yyname(this.yyToken), yyLex.value());
							}
							this.yyToken = -1;
							continue;
						default:
							goto IL_37D;
						}
						num2 = 3;
						while ((num3 = (int)CSharpParser.yySindex[array[this.yyTop]]) == 0 || (num3 += 256) < 0 || num3 >= CSharpParser.yyTable.Length || CSharpParser.yyCheck[num3] != 256)
						{
							if (this.debug != null)
							{
								this.debug.pop(array[this.yyTop]);
							}
							int num4 = this.yyTop - 1;
							this.yyTop = num4;
							if (num4 < 0)
							{
								goto Block_30;
							}
						}
						if (this.debug != null)
						{
							this.debug.shift(array[this.yyTop], (int)CSharpParser.yyTable[num3], 3);
						}
						num = (int)CSharpParser.yyTable[num3];
						this.yyVal = yyLex.value();
					}
					IL_4C34:
					this.yyTop++;
					goto IL_85;
				}
				IL_37D:
				int num5 = this.yyTop + 1 - (int)CSharpParser.yyLen[num3];
				if (this.debug != null)
				{
					this.debug.reduce(num, array[num5 - 1], num3, CSharpParser.YYRules.getRule(num3), (int)CSharpParser.yyLen[num3]);
				}
				this.yyVal = ((num5 > this.yyTop) ? null : this.yyVals[num5]);
				switch (num3)
				{
				case 1:
					this.Lexer.check_incorrect_doc_comment();
					break;
				case 2:
					this.Lexer.CompleteOnEOF = false;
					break;
				case 6:
					this.case_6();
					break;
				case 7:
					this.module.AddAttributes((Attributes)this.yyVals[0 + this.yyTop], this.current_namespace);
					break;
				case 8:
					this.case_8();
					break;
				case 13:
					this.case_13();
					break;
				case 14:
					this.Error_SyntaxError(this.yyToken);
					break;
				case 17:
					this.case_17();
					break;
				case 18:
					this.case_18();
					break;
				case 19:
					this.case_19();
					break;
				case 20:
					this.case_20();
					break;
				case 23:
					this.case_23();
					break;
				case 24:
					this.case_24();
					break;
				case 25:
					this.case_25();
					break;
				case 26:
					this.case_26();
					break;
				case 29:
					this.case_29();
					break;
				case 30:
					this.case_30();
					break;
				case 31:
					this.case_31();
					break;
				case 32:
					this.case_32();
					break;
				case 45:
					this.case_45();
					break;
				case 46:
					this.current_namespace.DeclarationFound = true;
					break;
				case 47:
					this.case_47();
					break;
				case 55:
					this.case_55();
					break;
				case 56:
					this.case_56();
					break;
				case 57:
					this.case_57();
					break;
				case 58:
					this.case_58();
					break;
				case 59:
					this.case_59();
					break;
				case 60:
					this.case_60();
					break;
				case 61:
					this.case_61();
					break;
				case 62:
					this.case_62();
					break;
				case 63:
					this.case_63();
					break;
				case 64:
					this.case_64();
					break;
				case 65:
					this.yyVal = "event";
					break;
				case 66:
					this.yyVal = "return";
					break;
				case 67:
					this.yyVal = new List<Attribute>(4)
					{
						(Attribute)this.yyVals[0 + this.yyTop]
					};
					break;
				case 68:
					this.case_68();
					break;
				case 69:
					this.lexer.parsing_block++;
					break;
				case 70:
					this.case_70();
					break;
				case 72:
					this.yyVal = null;
					break;
				case 73:
					this.yyVal = this.yyVals[-1 + this.yyTop];
					break;
				case 74:
					this.yyVal = null;
					break;
				case 75:
					this.case_75();
					break;
				case 76:
					this.case_76();
					break;
				case 77:
					this.case_77();
					break;
				case 78:
					this.case_78();
					break;
				case 79:
					this.yyVal = new Argument((Expression)this.yyVals[0 + this.yyTop]);
					break;
				case 81:
					this.case_81();
					break;
				case 82:
					this.lexer.parsing_block++;
					break;
				case 83:
					this.case_83();
					break;
				case 84:
					this.case_84();
					break;
				case 86:
					this.yyVal = null;
					break;
				case 87:
					this.yyVal = Argument.AType.Ref;
					break;
				case 88:
					this.yyVal = Argument.AType.Out;
					break;
				case 91:
					this.case_91();
					break;
				case 92:
					this.case_92();
					break;
				case 106:
					this.case_106();
					break;
				case 107:
					this.case_107();
					break;
				case 108:
					this.case_108();
					break;
				case 110:
					this.case_110();
					break;
				case 111:
					this.case_111();
					break;
				case 112:
					this.case_112();
					break;
				case 113:
					this.case_113();
					break;
				case 114:
					this.case_114();
					break;
				case 115:
					this.Error_SyntaxError(this.yyToken);
					break;
				case 116:
					this.case_116();
					break;
				case 117:
					this.case_117();
					break;
				case 118:
					this.case_118();
					break;
				case 121:
					this.current_field.AddDeclarator((FieldDeclarator)this.yyVals[0 + this.yyTop]);
					break;
				case 122:
					this.current_field.AddDeclarator((FieldDeclarator)this.yyVals[0 + this.yyTop]);
					break;
				case 123:
					this.case_123();
					break;
				case 124:
					this.lexer.parsing_block++;
					break;
				case 125:
					this.case_125();
					break;
				case 126:
					this.case_126();
					break;
				case 129:
					this.case_129();
					break;
				case 130:
					this.case_130();
					break;
				case 131:
					this.case_131();
					break;
				case 132:
					this.case_132();
					break;
				case 133:
					this.report.Error(1641, this.GetLocation(this.yyVals[-1 + this.yyTop]), "A fixed size buffer field must have the array size specifier after the field name");
					break;
				case 135:
					this.case_135();
					break;
				case 136:
					this.case_136();
					break;
				case 139:
					this.current_field.AddDeclarator((FieldDeclarator)this.yyVals[0 + this.yyTop]);
					break;
				case 140:
					this.current_field.AddDeclarator((FieldDeclarator)this.yyVals[0 + this.yyTop]);
					break;
				case 141:
					this.case_141();
					break;
				case 142:
					this.lexer.parsing_block++;
					break;
				case 143:
					this.case_143();
					break;
				case 146:
					this.current_field.AddDeclarator((FieldDeclarator)this.yyVals[0 + this.yyTop]);
					break;
				case 147:
					this.current_field.AddDeclarator((FieldDeclarator)this.yyVals[0 + this.yyTop]);
					break;
				case 148:
					this.case_148();
					break;
				case 149:
					this.lexer.parsing_block++;
					break;
				case 150:
					this.case_150();
					break;
				case 151:
					this.case_151();
					break;
				case 154:
					this.case_154();
					break;
				case 155:
					this.case_155();
					break;
				case 156:
					this.case_156();
					break;
				case 157:
					this.valid_param_mod = CSharpParser.ParameterModifierType.All;
					break;
				case 158:
					this.case_158();
					break;
				case 159:
					this.case_159();
					break;
				case 160:
					this.lexer.parsing_generic_declaration = true;
					break;
				case 161:
					this.case_161();
					break;
				case 162:
					this.lexer.ConstraintsParsing = true;
					break;
				case 163:
					this.case_163();
					break;
				case 164:
					this.case_164();
					break;
				case 165:
					this.case_165();
					break;
				case 169:
					this.yyVal = null;
					break;
				case 170:
					this.case_170();
					break;
				case 171:
					this.case_171();
					break;
				case 172:
					this.yyVal = ParametersCompiled.EmptyReadOnlyParameters;
					break;
				case 174:
					this.case_174();
					break;
				case 175:
					this.case_175();
					break;
				case 176:
					this.case_176();
					break;
				case 177:
					this.case_177();
					break;
				case 178:
					this.case_178();
					break;
				case 179:
					this.case_179();
					break;
				case 180:
					this.case_180();
					break;
				case 181:
					this.yyVal = new ParametersCompiled(new Parameter[]
					{
						(Parameter)this.yyVals[0 + this.yyTop]
					});
					break;
				case 182:
					this.yyVal = new ParametersCompiled(new Parameter[]
					{
						new ArglistParameter(this.GetLocation(this.yyVals[0 + this.yyTop]))
					}, true);
					break;
				case 183:
					this.case_183();
					break;
				case 184:
					this.case_184();
					break;
				case 185:
					this.case_185();
					break;
				case 186:
					this.case_186();
					break;
				case 187:
					this.case_187();
					break;
				case 188:
					this.case_188();
					break;
				case 189:
					this.case_189();
					break;
				case 190:
					this.lexer.parsing_block++;
					break;
				case 191:
					this.case_191();
					break;
				case 192:
					this.yyVal = Parameter.Modifier.NONE;
					break;
				case 194:
					this.yyVal = this.yyVals[0 + this.yyTop];
					break;
				case 195:
					this.case_195();
					break;
				case 196:
					this.case_196();
					break;
				case 197:
					this.case_197();
					break;
				case 198:
					this.case_198();
					break;
				case 199:
					this.case_199();
					break;
				case 200:
					this.case_200();
					break;
				case 201:
					this.case_201();
					break;
				case 202:
					this.case_202();
					break;
				case 203:
					this.case_203();
					break;
				case 204:
					this.Error_DuplicateParameterModifier(this.GetLocation(this.yyVals[-1 + this.yyTop]), Parameter.Modifier.PARAMS);
					break;
				case 205:
					this.case_205();
					break;
				case 206:
					this.case_206();
					break;
				case 207:
					this.case_207();
					break;
				case 208:
					this.case_208();
					break;
				case 209:
					this.case_209();
					break;
				case 210:
					this.current_property = null;
					break;
				case 211:
					this.case_211();
					break;
				case 212:
					this.case_212();
					break;
				case 214:
					this.case_214();
					break;
				case 215:
					this.case_215();
					break;
				case 218:
					this.valid_param_mod = (CSharpParser.ParameterModifierType.Params | CSharpParser.ParameterModifierType.DefaultValue);
					break;
				case 219:
					this.case_219();
					break;
				case 220:
					this.case_220();
					break;
				case 222:
					this.case_222();
					break;
				case 227:
					this.case_227();
					break;
				case 228:
					this.case_228();
					break;
				case 229:
					this.case_229();
					break;
				case 230:
					this.case_230();
					break;
				case 231:
					this.case_231();
					break;
				case 233:
					this.case_233();
					break;
				case 234:
					this.case_234();
					break;
				case 236:
					this.case_236();
					break;
				case 237:
					this.case_237();
					break;
				case 238:
					this.case_238();
					break;
				case 239:
					this.case_239();
					break;
				case 240:
					this.Error_SyntaxError(this.yyToken);
					break;
				case 243:
					this.case_243();
					break;
				case 244:
					this.case_244();
					break;
				case 245:
					this.report.Error(525, this.GetLocation(this.yyVals[0 + this.yyTop]), "Interfaces cannot contain fields or constants");
					break;
				case 246:
					this.report.Error(525, this.GetLocation(this.yyVals[0 + this.yyTop]), "Interfaces cannot contain fields or constants");
					break;
				case 251:
					this.report.Error(567, this.GetLocation(this.yyVals[0 + this.yyTop]), "Interfaces cannot contain operators");
					break;
				case 252:
					this.report.Error(526, this.GetLocation(this.yyVals[0 + this.yyTop]), "Interfaces cannot contain contructors");
					break;
				case 253:
					this.report.Error(524, this.GetLocation(this.yyVals[0 + this.yyTop]), "Interfaces cannot declare classes, structs, interfaces, delegates, or enumerations");
					break;
				case 255:
					this.case_255();
					break;
				case 257:
					this.case_257();
					break;
				case 258:
					this.case_258();
					break;
				case 259:
					this.case_259();
					break;
				case 261:
					this.yyVal = Operator.OpType.LogicalNot;
					break;
				case 262:
					this.yyVal = Operator.OpType.OnesComplement;
					break;
				case 263:
					this.yyVal = Operator.OpType.Increment;
					break;
				case 264:
					this.yyVal = Operator.OpType.Decrement;
					break;
				case 265:
					this.yyVal = Operator.OpType.True;
					break;
				case 266:
					this.yyVal = Operator.OpType.False;
					break;
				case 267:
					this.yyVal = Operator.OpType.Addition;
					break;
				case 268:
					this.yyVal = Operator.OpType.Subtraction;
					break;
				case 269:
					this.yyVal = Operator.OpType.Multiply;
					break;
				case 270:
					this.yyVal = Operator.OpType.Division;
					break;
				case 271:
					this.yyVal = Operator.OpType.Modulus;
					break;
				case 272:
					this.yyVal = Operator.OpType.BitwiseAnd;
					break;
				case 273:
					this.yyVal = Operator.OpType.BitwiseOr;
					break;
				case 274:
					this.yyVal = Operator.OpType.ExclusiveOr;
					break;
				case 275:
					this.yyVal = Operator.OpType.LeftShift;
					break;
				case 276:
					this.yyVal = Operator.OpType.RightShift;
					break;
				case 277:
					this.yyVal = Operator.OpType.Equality;
					break;
				case 278:
					this.yyVal = Operator.OpType.Inequality;
					break;
				case 279:
					this.yyVal = Operator.OpType.GreaterThan;
					break;
				case 280:
					this.yyVal = Operator.OpType.LessThan;
					break;
				case 281:
					this.yyVal = Operator.OpType.GreaterThanOrEqual;
					break;
				case 282:
					this.yyVal = Operator.OpType.LessThanOrEqual;
					break;
				case 283:
					this.case_283();
					break;
				case 284:
					this.valid_param_mod = CSharpParser.ParameterModifierType.DefaultValue;
					break;
				case 285:
					this.case_285();
					break;
				case 286:
					this.valid_param_mod = CSharpParser.ParameterModifierType.DefaultValue;
					break;
				case 287:
					this.case_287();
					break;
				case 288:
					this.case_288();
					break;
				case 289:
					this.case_289();
					break;
				case 290:
					this.case_290();
					break;
				case 291:
					this.case_291();
					break;
				case 292:
					this.case_292();
					break;
				case 293:
					this.case_293();
					break;
				case 295:
					this.current_block = null;
					this.yyVal = null;
					break;
				case 298:
					this.lexer.parsing_block++;
					break;
				case 299:
					this.case_299();
					break;
				case 300:
					this.lexer.parsing_block++;
					break;
				case 301:
					this.case_301();
					break;
				case 302:
					this.case_302();
					break;
				case 303:
					this.case_303();
					break;
				case 304:
					this.case_304();
					break;
				case 305:
					this.case_305();
					break;
				case 306:
					this.case_306();
					break;
				case 307:
					this.case_307();
					break;
				case 308:
					this.case_308();
					break;
				case 309:
					this.case_309();
					break;
				case 310:
					this.case_310();
					break;
				case 311:
					this.case_311();
					break;
				case 313:
					this.lexer.parsing_block++;
					break;
				case 314:
					this.case_314();
					break;
				case 317:
					this.current_event_field.AddDeclarator((FieldDeclarator)this.yyVals[0 + this.yyTop]);
					break;
				case 318:
					this.current_event_field.AddDeclarator((FieldDeclarator)this.yyVals[0 + this.yyTop]);
					break;
				case 319:
					this.case_319();
					break;
				case 320:
					this.lexer.parsing_block++;
					break;
				case 321:
					this.case_321();
					break;
				case 322:
					this.case_322();
					break;
				case 323:
					this.yyVal = this.yyVals[0 + this.yyTop];
					break;
				case 326:
					this.case_326();
					break;
				case 327:
					this.case_327();
					break;
				case 328:
					this.case_328();
					break;
				case 329:
					this.case_329();
					break;
				case 330:
					this.case_330();
					break;
				case 331:
					this.case_331();
					break;
				case 332:
					this.case_332();
					break;
				case 333:
					this.case_333();
					break;
				case 335:
					this.case_335();
					break;
				case 336:
					this.case_336();
					break;
				case 337:
					this.case_337();
					break;
				case 338:
					this.case_338();
					break;
				case 339:
					this.case_339();
					break;
				case 340:
					this.case_340();
					break;
				case 342:
					this.yyVal = this.yyVals[0 + this.yyTop];
					break;
				case 343:
					this.case_343();
					break;
				case 348:
					this.case_348();
					break;
				case 349:
					this.case_349();
					break;
				case 350:
					this.case_350();
					break;
				case 351:
					this.case_351();
					break;
				case 352:
					this.case_352();
					break;
				case 354:
					this.valid_param_mod = CSharpParser.ParameterModifierType.PrimaryConstructor;
					break;
				case 355:
					this.case_355();
					break;
				case 356:
					this.lexer.ConstraintsParsing = false;
					break;
				case 357:
					this.case_357();
					break;
				case 359:
					this.case_359();
					break;
				case 361:
					this.case_361();
					break;
				case 362:
					this.case_362();
					break;
				case 364:
					this.case_364();
					break;
				case 365:
					this.case_365();
					break;
				case 366:
					this.case_366();
					break;
				case 367:
					this.case_367();
					break;
				case 369:
					this.case_369();
					break;
				case 370:
					this.case_370();
					break;
				case 371:
					this.case_371();
					break;
				case 372:
					this.case_372();
					break;
				case 373:
					this.lexer.parsing_generic_declaration = true;
					break;
				case 374:
					this.case_374();
					break;
				case 375:
					this.case_375();
					break;
				case 377:
					this.case_377();
					break;
				case 378:
					this.case_378();
					break;
				case 379:
					this.case_379();
					break;
				case 380:
					this.case_380();
					break;
				case 381:
					this.case_381();
					break;
				case 382:
					this.case_382();
					break;
				case 384:
					this.case_384();
					break;
				case 385:
					this.case_385();
					break;
				case 386:
					this.case_386();
					break;
				case 387:
					this.case_387();
					break;
				case 388:
					this.case_388();
					break;
				case 390:
					this.yyVal = new TypeExpression(this.compiler.BuiltinTypes.Void, this.GetLocation(this.yyVals[0 + this.yyTop]));
					break;
				case 391:
					this.lexer.parsing_generic_declaration = true;
					break;
				case 397:
					this.case_397();
					break;
				case 399:
					this.yyVal = new ComposedCast((FullNamedExpression)this.yyVals[-1 + this.yyTop], (ComposedTypeSpecifier)this.yyVals[0 + this.yyTop]);
					break;
				case 400:
					this.case_400();
					break;
				case 401:
					this.yyVal = new ComposedCast((ATypeNameExpression)this.yyVals[-1 + this.yyTop], (ComposedTypeSpecifier)this.yyVals[0 + this.yyTop]);
					break;
				case 403:
					this.case_403();
					break;
				case 404:
					this.case_404();
					break;
				case 405:
					this.yyVal = new ComposedCast((FullNamedExpression)this.yyVals[-1 + this.yyTop], (ComposedTypeSpecifier)this.yyVals[0 + this.yyTop]);
					break;
				case 406:
					this.yyVal = new ComposedCast(new TypeExpression(this.compiler.BuiltinTypes.Void, this.GetLocation(this.yyVals[-1 + this.yyTop])), (ComposedTypeSpecifier)this.yyVals[0 + this.yyTop]);
					break;
				case 407:
					this.case_407();
					break;
				case 408:
					this.case_408();
					break;
				case 409:
					this.case_409();
					break;
				case 410:
					this.yyVal = new TypeExpression(this.compiler.BuiltinTypes.Object, this.GetLocation(this.yyVals[0 + this.yyTop]));
					break;
				case 411:
					this.yyVal = new TypeExpression(this.compiler.BuiltinTypes.String, this.GetLocation(this.yyVals[0 + this.yyTop]));
					break;
				case 412:
					this.yyVal = new TypeExpression(this.compiler.BuiltinTypes.Bool, this.GetLocation(this.yyVals[0 + this.yyTop]));
					break;
				case 413:
					this.yyVal = new TypeExpression(this.compiler.BuiltinTypes.Decimal, this.GetLocation(this.yyVals[0 + this.yyTop]));
					break;
				case 414:
					this.yyVal = new TypeExpression(this.compiler.BuiltinTypes.Float, this.GetLocation(this.yyVals[0 + this.yyTop]));
					break;
				case 415:
					this.yyVal = new TypeExpression(this.compiler.BuiltinTypes.Double, this.GetLocation(this.yyVals[0 + this.yyTop]));
					break;
				case 417:
					this.yyVal = new TypeExpression(this.compiler.BuiltinTypes.SByte, this.GetLocation(this.yyVals[0 + this.yyTop]));
					break;
				case 418:
					this.yyVal = new TypeExpression(this.compiler.BuiltinTypes.Byte, this.GetLocation(this.yyVals[0 + this.yyTop]));
					break;
				case 419:
					this.yyVal = new TypeExpression(this.compiler.BuiltinTypes.Short, this.GetLocation(this.yyVals[0 + this.yyTop]));
					break;
				case 420:
					this.yyVal = new TypeExpression(this.compiler.BuiltinTypes.UShort, this.GetLocation(this.yyVals[0 + this.yyTop]));
					break;
				case 421:
					this.yyVal = new TypeExpression(this.compiler.BuiltinTypes.Int, this.GetLocation(this.yyVals[0 + this.yyTop]));
					break;
				case 422:
					this.yyVal = new TypeExpression(this.compiler.BuiltinTypes.UInt, this.GetLocation(this.yyVals[0 + this.yyTop]));
					break;
				case 423:
					this.yyVal = new TypeExpression(this.compiler.BuiltinTypes.Long, this.GetLocation(this.yyVals[0 + this.yyTop]));
					break;
				case 424:
					this.yyVal = new TypeExpression(this.compiler.BuiltinTypes.ULong, this.GetLocation(this.yyVals[0 + this.yyTop]));
					break;
				case 425:
					this.yyVal = new TypeExpression(this.compiler.BuiltinTypes.Char, this.GetLocation(this.yyVals[0 + this.yyTop]));
					break;
				case 448:
					this.case_448();
					break;
				case 452:
					this.yyVal = new NullLiteral(this.GetLocation(this.yyVals[0 + this.yyTop]));
					break;
				case 453:
					this.yyVal = new BoolLiteral(this.compiler.BuiltinTypes, true, this.GetLocation(this.yyVals[0 + this.yyTop]));
					break;
				case 454:
					this.yyVal = new BoolLiteral(this.compiler.BuiltinTypes, false, this.GetLocation(this.yyVals[0 + this.yyTop]));
					break;
				case 455:
					this.yyVal = new InterpolatedString((StringLiteral)this.yyVals[-2 + this.yyTop], (List<Expression>)this.yyVals[-1 + this.yyTop], (StringLiteral)this.yyVals[0 + this.yyTop]);
					break;
				case 456:
					this.yyVal = new InterpolatedString((StringLiteral)this.yyVals[0 + this.yyTop], null, null);
					break;
				case 457:
					this.case_457();
					break;
				case 458:
					this.case_458();
					break;
				case 459:
					this.yyVal = new InterpolatedStringInsert((Expression)this.yyVals[0 + this.yyTop]);
					break;
				case 460:
					this.case_460();
					break;
				case 461:
					this.lexer.parsing_interpolation_format = true;
					break;
				case 462:
					this.case_462();
					break;
				case 463:
					this.lexer.parsing_interpolation_format = true;
					break;
				case 464:
					this.case_464();
					break;
				case 469:
					this.case_469();
					break;
				case 470:
					this.yyVal = new ParenthesizedExpression((Expression)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-2 + this.yyTop]));
					break;
				case 471:
					this.case_471();
					break;
				case 472:
					this.case_472();
					break;
				case 473:
					this.case_473();
					break;
				case 474:
					this.case_474();
					break;
				case 475:
					this.case_475();
					break;
				case 476:
					this.case_476();
					break;
				case 477:
					this.case_477();
					break;
				case 478:
					this.case_478();
					break;
				case 479:
					this.yyVal = new CompletionMemberAccess((Expression)this.yyVals[-2 + this.yyTop], null, this.GetLocation(this.yyVals[0 + this.yyTop]));
					break;
				case 480:
					this.case_480();
					break;
				case 481:
					this.yyVal = new CompletionMemberAccess((Expression)this.yyVals[-2 + this.yyTop], null, this.lexer.Location);
					break;
				case 482:
					this.case_482();
					break;
				case 483:
					this.case_483();
					break;
				case 484:
					this.case_484();
					break;
				case 485:
					this.case_485();
					break;
				case 486:
					this.yyVal = null;
					break;
				case 488:
					this.case_488();
					break;
				case 489:
					this.case_489();
					break;
				case 490:
					this.yyVal = null;
					break;
				case 491:
					this.yyVal = this.yyVals[0 + this.yyTop];
					break;
				case 492:
					this.case_492();
					break;
				case 493:
					this.case_493();
					break;
				case 494:
					this.case_494();
					break;
				case 495:
					this.case_495();
					break;
				case 496:
					this.case_496();
					break;
				case 497:
					this.yyVal = new CompletionElementInitializer(null, this.GetLocation(this.yyVals[0 + this.yyTop]));
					break;
				case 498:
					this.case_498();
					break;
				case 499:
					this.case_499();
					break;
				case 500:
					this.case_500();
					break;
				case 501:
					this.case_501();
					break;
				case 504:
					this.yyVal = null;
					break;
				case 506:
					this.case_506();
					break;
				case 507:
					this.case_507();
					break;
				case 508:
					this.case_508();
					break;
				case 509:
					this.case_509();
					break;
				case 510:
					this.case_510();
					break;
				case 511:
					this.yyVal = new Argument((Expression)this.yyVals[0 + this.yyTop]);
					break;
				case 515:
					this.case_515();
					break;
				case 516:
					this.yyVal = new Argument((Expression)this.yyVals[0 + this.yyTop], Argument.AType.Ref);
					break;
				case 517:
					this.case_517();
					break;
				case 518:
					this.yyVal = new Argument((Expression)this.yyVals[0 + this.yyTop], Argument.AType.Out);
					break;
				case 519:
					this.case_519();
					break;
				case 520:
					this.case_520();
					break;
				case 521:
					this.case_521();
					break;
				case 522:
					this.case_522();
					break;
				case 523:
					this.case_523();
					break;
				case 525:
					this.case_525();
					break;
				case 526:
					this.case_526();
					break;
				case 527:
					this.case_527();
					break;
				case 528:
					this.case_528();
					break;
				case 529:
					this.case_529();
					break;
				case 530:
					this.case_530();
					break;
				case 531:
					this.case_531();
					break;
				case 532:
					this.case_532();
					break;
				case 533:
					this.yyVal = new Argument((Expression)this.yyVals[0 + this.yyTop]);
					break;
				case 535:
					this.yyVal = new This(this.GetLocation(this.yyVals[0 + this.yyTop]));
					break;
				case 536:
					this.case_536();
					break;
				case 537:
					this.case_537();
					break;
				case 538:
					this.yyVal = new UnaryMutator(UnaryMutator.Mode.IsPost, (Expression)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[0 + this.yyTop]));
					break;
				case 539:
					this.yyVal = new UnaryMutator(UnaryMutator.Mode.PostDecrement, (Expression)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[0 + this.yyTop]));
					break;
				case 540:
					this.case_540();
					break;
				case 541:
					this.case_541();
					break;
				case 542:
					this.case_542();
					break;
				case 543:
					this.case_543();
					break;
				case 544:
					this.case_544();
					break;
				case 545:
					this.case_545();
					break;
				case 546:
					this.case_546();
					break;
				case 547:
					this.lexer.parsing_type++;
					break;
				case 548:
					this.case_548();
					break;
				case 549:
					this.case_549();
					break;
				case 550:
					this.yyVal = new EmptyCompletion();
					break;
				case 553:
					this.yyVal = null;
					break;
				case 555:
					this.case_555();
					break;
				case 556:
					this.case_556();
					break;
				case 557:
					this.yyVal = new EmptyCompletion();
					break;
				case 558:
					this.yyVal = this.yyVals[-1 + this.yyTop];
					break;
				case 559:
					this.case_559();
					break;
				case 560:
					this.case_560();
					break;
				case 561:
					this.case_561();
					break;
				case 562:
					this.case_562();
					break;
				case 566:
					this.case_566();
					break;
				case 567:
					this.case_567();
					break;
				case 568:
					this.case_568();
					break;
				case 569:
					this.yyVal = 2;
					break;
				case 570:
					this.yyVal = (int)this.yyVals[-1 + this.yyTop] + 1;
					break;
				case 571:
					this.yyVal = null;
					break;
				case 572:
					this.yyVal = this.yyVals[0 + this.yyTop];
					break;
				case 573:
					this.case_573();
					break;
				case 574:
					this.case_574();
					break;
				case 575:
					this.case_575();
					break;
				case 576:
					this.case_576();
					break;
				case 577:
					this.case_577();
					break;
				case 579:
					this.case_579();
					break;
				case 580:
					this.case_580();
					break;
				case 581:
					this.case_581();
					break;
				case 582:
					this.case_582();
					break;
				case 583:
					this.case_583();
					break;
				case 584:
					this.case_584();
					break;
				case 585:
					this.case_585();
					break;
				case 586:
					this.case_586();
					break;
				case 587:
					this.case_587();
					break;
				case 588:
					this.case_588();
					break;
				case 589:
					this.start_anonymous(false, (ParametersCompiled)this.yyVals[0 + this.yyTop], false, this.GetLocation(this.yyVals[-1 + this.yyTop]));
					break;
				case 590:
					this.yyVal = this.end_anonymous((ParametersBlock)this.yyVals[0 + this.yyTop]);
					break;
				case 591:
					this.start_anonymous(false, (ParametersCompiled)this.yyVals[0 + this.yyTop], true, this.GetLocation(this.yyVals[-2 + this.yyTop]));
					break;
				case 592:
					this.yyVal = this.end_anonymous((ParametersBlock)this.yyVals[0 + this.yyTop]);
					break;
				case 593:
					this.yyVal = ParametersCompiled.Undefined;
					break;
				case 595:
					this.valid_param_mod = (CSharpParser.ParameterModifierType.Ref | CSharpParser.ParameterModifierType.Out);
					break;
				case 596:
					this.case_596();
					break;
				case 597:
					this.case_597();
					break;
				case 599:
					this.yyVal = new Unary(Unary.Operator.LogicalNot, (Expression)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-1 + this.yyTop]));
					break;
				case 600:
					this.yyVal = new Unary(Unary.Operator.OnesComplement, (Expression)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-1 + this.yyTop]));
					break;
				case 601:
					this.case_601();
					break;
				case 602:
					this.case_602();
					break;
				case 603:
					this.case_603();
					break;
				case 604:
					this.case_604();
					break;
				case 605:
					this.case_605();
					break;
				case 606:
					this.case_606();
					break;
				case 608:
					this.yyVal = new Unary(Unary.Operator.UnaryPlus, (Expression)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-1 + this.yyTop]));
					break;
				case 609:
					this.yyVal = new Unary(Unary.Operator.UnaryNegation, (Expression)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-1 + this.yyTop]));
					break;
				case 610:
					this.yyVal = new UnaryMutator(UnaryMutator.Mode.IsIncrement, (Expression)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-1 + this.yyTop]));
					break;
				case 611:
					this.yyVal = new UnaryMutator(UnaryMutator.Mode.IsDecrement, (Expression)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-1 + this.yyTop]));
					break;
				case 612:
					this.yyVal = new Indirection((Expression)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-1 + this.yyTop]));
					break;
				case 613:
					this.yyVal = new Unary(Unary.Operator.AddressOf, (Expression)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-1 + this.yyTop]));
					break;
				case 614:
					this.case_614();
					break;
				case 615:
					this.case_615();
					break;
				case 616:
					this.case_616();
					break;
				case 617:
					this.case_617();
					break;
				case 618:
					this.case_618();
					break;
				case 619:
					this.case_619();
					break;
				case 621:
					this.case_621();
					break;
				case 622:
					this.case_622();
					break;
				case 623:
					this.case_623();
					break;
				case 624:
					this.case_624();
					break;
				case 625:
					this.case_625();
					break;
				case 626:
					this.case_626();
					break;
				case 628:
					this.case_628();
					break;
				case 629:
					this.case_629();
					break;
				case 630:
					this.case_630();
					break;
				case 631:
					this.case_631();
					break;
				case 632:
					this.yyVal = new As((Expression)this.yyVals[-2 + this.yyTop], (Expression)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-1 + this.yyTop]));
					break;
				case 633:
					this.case_633();
					break;
				case 634:
					this.case_634();
					break;
				case 635:
					this.case_635();
					break;
				case 636:
					this.case_636();
					break;
				case 637:
					this.case_637();
					break;
				case 638:
					this.case_638();
					break;
				case 641:
					this.yyVal = new Unary(Unary.Operator.UnaryPlus, (Expression)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-1 + this.yyTop]));
					break;
				case 642:
					this.yyVal = new Unary(Unary.Operator.UnaryNegation, (Expression)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-1 + this.yyTop]));
					break;
				case 645:
					this.case_645();
					break;
				case 646:
					this.yyVal = new WildcardPattern(this.GetLocation(this.yyVals[0 + this.yyTop]));
					break;
				case 649:
					this.yyVal = new RecursivePattern((ATypeNameExpression)this.yyVals[-3 + this.yyTop], (Arguments)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-2 + this.yyTop]));
					break;
				case 650:
					this.yyVal = new PropertyPattern((ATypeNameExpression)this.yyVals[-3 + this.yyTop], (List<PropertyPatternMember>)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-2 + this.yyTop]));
					break;
				case 651:
					this.case_651();
					break;
				case 652:
					this.case_652();
					break;
				case 653:
					this.case_653();
					break;
				case 655:
					this.case_655();
					break;
				case 656:
					this.yyVal = new Arguments(0);
					break;
				case 658:
					this.case_658();
					break;
				case 659:
					this.case_659();
					break;
				case 660:
					this.yyVal = new Argument((Expression)this.yyVals[0 + this.yyTop]);
					break;
				case 661:
					this.case_661();
					break;
				case 663:
					this.case_663();
					break;
				case 664:
					this.case_664();
					break;
				case 665:
					this.case_665();
					break;
				case 666:
					this.case_666();
					break;
				case 668:
					this.case_668();
					break;
				case 669:
					this.case_669();
					break;
				case 670:
					this.case_670();
					break;
				case 671:
					this.case_671();
					break;
				case 672:
					this.case_672();
					break;
				case 673:
					this.case_673();
					break;
				case 674:
					this.case_674();
					break;
				case 675:
					this.case_675();
					break;
				case 677:
					this.case_677();
					break;
				case 678:
					this.case_678();
					break;
				case 679:
					this.case_679();
					break;
				case 680:
					this.case_680();
					break;
				case 682:
					this.case_682();
					break;
				case 683:
					this.case_683();
					break;
				case 685:
					this.case_685();
					break;
				case 686:
					this.case_686();
					break;
				case 688:
					this.case_688();
					break;
				case 689:
					this.case_689();
					break;
				case 691:
					this.case_691();
					break;
				case 692:
					this.case_692();
					break;
				case 694:
					this.case_694();
					break;
				case 695:
					this.case_695();
					break;
				case 697:
					this.case_697();
					break;
				case 699:
					this.case_699();
					break;
				case 700:
					this.case_700();
					break;
				case 701:
					this.case_701();
					break;
				case 702:
					this.case_702();
					break;
				case 703:
					this.case_703();
					break;
				case 704:
					this.case_704();
					break;
				case 705:
					this.case_705();
					break;
				case 706:
					this.case_706();
					break;
				case 707:
					this.case_707();
					break;
				case 708:
					this.case_708();
					break;
				case 709:
					this.case_709();
					break;
				case 710:
					this.case_710();
					break;
				case 711:
					this.case_711();
					break;
				case 712:
					this.case_712();
					break;
				case 713:
					this.case_713();
					break;
				case 714:
					this.case_714();
					break;
				case 715:
					this.case_715();
					break;
				case 716:
					this.case_716();
					break;
				case 717:
					this.case_717();
					break;
				case 718:
					this.case_718();
					break;
				case 719:
					this.case_719();
					break;
				case 720:
					this.yyVal = ParametersCompiled.EmptyReadOnlyParameters;
					break;
				case 721:
					this.case_721();
					break;
				case 722:
					this.start_block(Location.Null);
					break;
				case 723:
					this.case_723();
					break;
				case 725:
					this.case_725();
					break;
				case 727:
					this.case_727();
					break;
				case 728:
					this.case_728();
					break;
				case 729:
					this.case_729();
					break;
				case 730:
					this.case_730();
					break;
				case 731:
					this.case_731();
					break;
				case 732:
					this.case_732();
					break;
				case 733:
					this.case_733();
					break;
				case 734:
					this.valid_param_mod = (CSharpParser.ParameterModifierType.Ref | CSharpParser.ParameterModifierType.Out);
					break;
				case 735:
					this.case_735();
					break;
				case 736:
					this.case_736();
					break;
				case 737:
					this.valid_param_mod = (CSharpParser.ParameterModifierType.Ref | CSharpParser.ParameterModifierType.Out);
					break;
				case 738:
					this.case_738();
					break;
				case 739:
					this.case_739();
					break;
				case 745:
					this.yyVal = new ArglistAccess(this.GetLocation(this.yyVals[0 + this.yyTop]));
					break;
				case 746:
					this.case_746();
					break;
				case 747:
					this.case_747();
					break;
				case 748:
					this.case_748();
					break;
				case 750:
					this.yyVal = new BooleanExpression((Expression)this.yyVals[0 + this.yyTop]);
					break;
				case 751:
					this.yyVal = null;
					break;
				case 753:
					this.case_753();
					break;
				case 754:
					this.yyVal = null;
					break;
				case 755:
					this.yyVal = null;
					break;
				case 756:
					this.yyVal = this.yyVals[0 + this.yyTop];
					break;
				case 757:
					this.yyVal = this.yyVals[-1 + this.yyTop];
					break;
				case 758:
					this.case_758();
					break;
				case 759:
					this.case_759();
					break;
				case 761:
					this.case_761();
					break;
				case 762:
					this.case_762();
					break;
				case 763:
					this.case_763();
					break;
				case 764:
					this.case_764();
					break;
				case 765:
					this.yyVal = null;
					break;
				case 766:
					this.yyVal = this.yyVals[0 + this.yyTop];
					break;
				case 767:
					this.case_767();
					break;
				case 768:
					this.lexer.parsing_modifiers = false;
					break;
				case 770:
					this.case_770();
					break;
				case 771:
					this.case_771();
					break;
				case 772:
					this.case_772();
					break;
				case 773:
					this.case_773();
					break;
				case 774:
					this.case_774();
					break;
				case 775:
					this.case_775();
					break;
				case 776:
					this.case_776();
					break;
				case 777:
					this.case_777();
					break;
				case 778:
					this.case_778();
					break;
				case 779:
					this.case_779();
					break;
				case 780:
					this.case_780();
					break;
				case 781:
					this.case_781();
					break;
				case 782:
					this.case_782();
					break;
				case 783:
					this.case_783();
					break;
				case 784:
					this.case_784();
					break;
				case 785:
					this.case_785();
					break;
				case 788:
					this.current_type.SetBaseTypes((List<FullNamedExpression>)this.yyVals[0 + this.yyTop]);
					break;
				case 789:
					this.case_789();
					break;
				case 791:
					this.yyVal = this.yyVals[0 + this.yyTop];
					break;
				case 792:
					this.case_792();
					break;
				case 793:
					this.case_793();
					break;
				case 794:
					this.case_794();
					break;
				case 795:
					this.case_795();
					break;
				case 796:
					this.case_796();
					break;
				case 797:
					this.case_797();
					break;
				case 798:
					this.case_798();
					break;
				case 799:
					this.case_799();
					break;
				case 800:
					this.yyVal = new SpecialContraintExpr(SpecialConstraint.Class, this.GetLocation(this.yyVals[0 + this.yyTop]));
					break;
				case 801:
					this.yyVal = new SpecialContraintExpr(SpecialConstraint.Struct, this.GetLocation(this.yyVals[0 + this.yyTop]));
					break;
				case 802:
					this.yyVal = null;
					break;
				case 803:
					this.case_803();
					break;
				case 804:
					this.yyVal = new VarianceDecl(Variance.Covariant, this.GetLocation(this.yyVals[0 + this.yyTop]));
					break;
				case 805:
					this.yyVal = new VarianceDecl(Variance.Contravariant, this.GetLocation(this.yyVals[0 + this.yyTop]));
					break;
				case 806:
					this.case_806();
					break;
				case 807:
					this.yyVal = this.yyVals[0 + this.yyTop];
					break;
				case 808:
					this.case_808();
					break;
				case 809:
					this.case_809();
					break;
				case 810:
					this.case_810();
					break;
				case 811:
					this.case_811();
					break;
				case 816:
					this.current_block.AddStatement((Statement)this.yyVals[0 + this.yyTop]);
					break;
				case 817:
					this.current_block.AddStatement((Statement)this.yyVals[0 + this.yyTop]);
					break;
				case 819:
					this.case_819();
					break;
				case 822:
					this.current_block.AddStatement((Statement)this.yyVals[0 + this.yyTop]);
					break;
				case 823:
					this.current_block.AddStatement((Statement)this.yyVals[0 + this.yyTop]);
					break;
				case 852:
					this.case_852();
					break;
				case 853:
					this.case_853();
					break;
				case 854:
					this.case_854();
					break;
				case 855:
					this.case_855();
					break;
				case 856:
					this.case_856();
					break;
				case 859:
					this.case_859();
					break;
				case 860:
					this.case_860();
					break;
				case 861:
					this.case_861();
					break;
				case 865:
					this.case_865();
					break;
				case 866:
					this.yyVal = ComposedTypeSpecifier.CreatePointer(this.GetLocation(this.yyVals[0 + this.yyTop]));
					break;
				case 868:
					this.yyVal = this.Error_AwaitAsIdentifier(this.yyVals[0 + this.yyTop]);
					break;
				case 869:
					this.case_869();
					break;
				case 870:
					this.case_870();
					break;
				case 871:
					this.case_871();
					break;
				case 872:
					this.case_872();
					break;
				case 874:
					this.case_874();
					break;
				case 875:
					this.case_875();
					break;
				case 879:
					this.case_879();
					break;
				case 882:
					this.case_882();
					break;
				case 883:
					this.case_883();
					break;
				case 884:
					this.report.Error(145, this.lexer.Location, "A const field requires a value to be provided");
					break;
				case 885:
					this.current_variable.Initializer = (Expression)this.yyVals[0 + this.yyTop];
					break;
				case 890:
					this.case_890();
					break;
				case 892:
					this.case_892();
					break;
				case 893:
					this.case_893();
					break;
				case 894:
					this.case_894();
					break;
				case 895:
					this.yyVal = this.yyVals[-1 + this.yyTop];
					break;
				case 896:
					this.case_896();
					break;
				case 897:
					this.yyVal = this.yyVals[-1 + this.yyTop];
					break;
				case 898:
					this.yyVal = this.yyVals[-1 + this.yyTop];
					break;
				case 899:
					this.case_899();
					break;
				case 900:
					this.case_900();
					break;
				case 901:
					this.case_901();
					break;
				case 904:
					this.case_904();
					break;
				case 905:
					this.case_905();
					break;
				case 906:
					this.case_906();
					break;
				case 907:
					this.start_block(this.GetLocation(this.yyVals[0 + this.yyTop]));
					break;
				case 908:
					this.case_908();
					break;
				case 909:
					this.case_909();
					break;
				case 910:
					this.report.Warning(1522, 1, this.current_block.StartLocation, "Empty switch block");
					break;
				case 914:
					this.Error_SyntaxError(this.yyToken);
					break;
				case 916:
					this.case_916();
					break;
				case 917:
					this.current_block.AddStatement((Statement)this.yyVals[0 + this.yyTop]);
					break;
				case 918:
					this.case_918();
					break;
				case 919:
					this.case_919();
					break;
				case 920:
					this.yyVal = new SwitchLabel(null, this.GetLocation(this.yyVals[0 + this.yyTop]));
					break;
				case 925:
					this.case_925();
					break;
				case 926:
					this.case_926();
					break;
				case 927:
					this.case_927();
					break;
				case 928:
					this.case_928();
					break;
				case 929:
					this.case_929();
					break;
				case 930:
					this.case_930();
					break;
				case 931:
					this.yyVal = this.yyVals[0 + this.yyTop];
					break;
				case 932:
					this.case_932();
					break;
				case 933:
					this.case_933();
					break;
				case 934:
					this.case_934();
					break;
				case 935:
					this.case_935();
					break;
				case 936:
					this.yyVal = new Tuple<Location, Location>(this.GetLocation(this.yyVals[-2 + this.yyTop]), (Location)this.yyVals[0 + this.yyTop]);
					break;
				case 937:
					this.case_937();
					break;
				case 938:
					this.case_938();
					break;
				case 939:
					this.case_939();
					break;
				case 941:
					this.lexer.putback(125);
					break;
				case 942:
					this.yyVal = new EmptyStatement(this.lexer.Location);
					break;
				case 944:
					this.case_944();
					break;
				case 945:
					this.case_945();
					break;
				case 947:
					this.yyVal = null;
					break;
				case 949:
					this.yyVal = new EmptyStatement(this.lexer.Location);
					break;
				case 953:
					this.case_953();
					break;
				case 954:
					this.case_954();
					break;
				case 955:
					this.case_955();
					break;
				case 956:
					this.case_956();
					break;
				case 957:
					this.case_957();
					break;
				case 964:
					this.case_964();
					break;
				case 965:
					this.case_965();
					break;
				case 966:
					this.case_966();
					break;
				case 967:
					this.case_967();
					break;
				case 968:
					this.case_968();
					break;
				case 969:
					this.case_969();
					break;
				case 970:
					this.case_970();
					break;
				case 971:
					this.case_971();
					break;
				case 972:
					this.case_972();
					break;
				case 973:
					this.case_973();
					break;
				case 974:
					this.case_974();
					break;
				case 975:
					this.case_975();
					break;
				case 976:
					this.case_976();
					break;
				case 977:
					this.case_977();
					break;
				case 978:
					this.case_978();
					break;
				case 981:
					this.yyVal = new TryCatch((Block)this.yyVals[-1 + this.yyTop], (List<Catch>)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-2 + this.yyTop]), false);
					break;
				case 982:
					this.case_982();
					break;
				case 983:
					this.case_983();
					break;
				case 984:
					this.case_984();
					break;
				case 985:
					this.case_985();
					break;
				case 986:
					this.case_986();
					break;
				case 989:
					this.case_989();
					break;
				case 990:
					this.case_990();
					break;
				case 991:
					this.case_991();
					break;
				case 992:
					this.case_992();
					break;
				case 993:
					this.yyVal = this.yyVals[-1 + this.yyTop];
					break;
				case 994:
					this.case_994();
					break;
				case 995:
					this.lexer.parsing_catch_when = false;
					break;
				case 996:
					this.lexer.parsing_catch_when = false;
					break;
				case 997:
					this.case_997();
					break;
				case 998:
					this.yyVal = new Checked((Block)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-1 + this.yyTop]));
					break;
				case 999:
					this.yyVal = new Unchecked((Block)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-1 + this.yyTop]));
					break;
				case 1000:
					this.case_1000();
					break;
				case 1001:
					this.yyVal = new Unsafe((Block)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-2 + this.yyTop]));
					break;
				case 1002:
					this.case_1002();
					break;
				case 1003:
					this.case_1003();
					break;
				case 1004:
					this.case_1004();
					break;
				case 1005:
					this.case_1005();
					break;
				case 1006:
					this.case_1006();
					break;
				case 1007:
					this.case_1007();
					break;
				case 1008:
					this.case_1008();
					break;
				case 1009:
					this.case_1009();
					break;
				case 1010:
					this.case_1010();
					break;
				case 1011:
					this.case_1011();
					break;
				case 1013:
					this.case_1013();
					break;
				case 1014:
					this.Error_MissingInitializer(this.lexer.Location);
					break;
				case 1015:
					this.case_1015();
					break;
				case 1016:
					this.case_1016();
					break;
				case 1017:
					this.case_1017();
					break;
				case 1018:
					this.case_1018();
					break;
				case 1019:
					this.case_1019();
					break;
				case 1020:
					this.case_1020();
					break;
				case 1021:
					this.case_1021();
					break;
				case 1022:
					this.case_1022();
					break;
				case 1023:
					this.case_1023();
					break;
				case 1024:
					this.current_block = new QueryBlock(this.current_block, this.lexer.Location);
					break;
				case 1025:
					this.case_1025();
					break;
				case 1026:
					this.current_block = new QueryBlock(this.current_block, this.lexer.Location);
					break;
				case 1027:
					this.case_1027();
					break;
				case 1028:
					this.case_1028();
					break;
				case 1029:
					this.case_1029();
					break;
				case 1031:
					this.case_1031();
					break;
				case 1032:
					this.case_1032();
					break;
				case 1033:
					this.current_block = new QueryBlock(this.current_block, this.lexer.Location);
					break;
				case 1034:
					this.case_1034();
					break;
				case 1035:
					this.case_1035();
					break;
				case 1036:
					this.case_1036();
					break;
				case 1037:
					this.case_1037();
					break;
				case 1038:
					this.yyVal = new object[]
					{
						this.yyVals[0 + this.yyTop],
						this.GetLocation(this.yyVals[-1 + this.yyTop])
					};
					break;
				case 1039:
					this.case_1039();
					break;
				case 1041:
					this.case_1041();
					break;
				case 1047:
					this.current_block = new QueryBlock(this.current_block, this.lexer.Location);
					break;
				case 1048:
					this.case_1048();
					break;
				case 1049:
					this.current_block = new QueryBlock(this.current_block, this.lexer.Location);
					break;
				case 1050:
					this.case_1050();
					break;
				case 1051:
					this.case_1051();
					break;
				case 1052:
					this.case_1052();
					break;
				case 1053:
					this.case_1053();
					break;
				case 1054:
					this.case_1054();
					break;
				case 1055:
					this.case_1055();
					break;
				case 1056:
					this.case_1056();
					break;
				case 1057:
					this.case_1057();
					break;
				case 1058:
					this.case_1058();
					break;
				case 1060:
					this.yyVal = this.yyVals[0 + this.yyTop];
					break;
				case 1061:
					this.current_block = new QueryBlock(this.current_block, this.lexer.Location);
					break;
				case 1062:
					this.case_1062();
					break;
				case 1064:
					this.case_1064();
					break;
				case 1065:
					this.case_1065();
					break;
				case 1067:
					this.case_1067();
					break;
				case 1068:
					this.case_1068();
					break;
				case 1069:
					this.yyVal = new OrderByAscending((QueryBlock)this.current_block, (Expression)this.yyVals[0 + this.yyTop]);
					break;
				case 1070:
					this.case_1070();
					break;
				case 1071:
					this.case_1071();
					break;
				case 1072:
					this.yyVal = new ThenByAscending((QueryBlock)this.current_block, (Expression)this.yyVals[0 + this.yyTop]);
					break;
				case 1073:
					this.case_1073();
					break;
				case 1074:
					this.case_1074();
					break;
				case 1076:
					this.case_1076();
					break;
				case 1077:
					this.case_1077();
					break;
				case 1080:
					this.case_1080();
					break;
				case 1081:
					this.case_1081();
					break;
				case 1089:
					this.module.DocumentationBuilder.ParsedName = (MemberName)this.yyVals[0 + this.yyTop];
					break;
				case 1090:
					this.module.DocumentationBuilder.ParsedParameters = (List<DocumentationParameter>)this.yyVals[0 + this.yyTop];
					break;
				case 1091:
					this.case_1091();
					break;
				case 1092:
					this.case_1092();
					break;
				case 1093:
					this.case_1093();
					break;
				case 1094:
					this.yyVal = new MemberName((MemberName)this.yyVals[-2 + this.yyTop], MemberCache.IndexerNameAlias, Location.Null);
					break;
				case 1095:
					this.valid_param_mod = (CSharpParser.ParameterModifierType.Ref | CSharpParser.ParameterModifierType.Out);
					break;
				case 1096:
					this.case_1096();
					break;
				case 1097:
					this.case_1097();
					break;
				case 1098:
					this.case_1098();
					break;
				case 1099:
					this.case_1099();
					break;
				case 1101:
					this.yyVal = new MemberName((MemberName)this.yyVals[-2 + this.yyTop], (MemberName)this.yyVals[0 + this.yyTop]);
					break;
				case 1103:
					this.valid_param_mod = (CSharpParser.ParameterModifierType.Ref | CSharpParser.ParameterModifierType.Out);
					break;
				case 1104:
					this.yyVal = this.yyVals[-1 + this.yyTop];
					break;
				case 1105:
					this.yyVal = new List<DocumentationParameter>(0);
					break;
				case 1107:
					this.case_1107();
					break;
				case 1108:
					this.case_1108();
					break;
				case 1109:
					this.case_1109();
					break;
				}
				this.yyTop -= (int)CSharpParser.yyLen[num3];
				num = array[this.yyTop];
				int num6 = (int)CSharpParser.yyLhs[num3];
				if (num == 0 && num6 == 0)
				{
					if (this.debug != null)
					{
						this.debug.shift(0, 7);
					}
					num = 7;
					if (this.yyToken < 0)
					{
						this.yyToken = (yyLex.advance() ? yyLex.token() : 0);
						if (this.debug != null)
						{
							this.debug.lex(num, this.yyToken, CSharpParser.yyname(this.yyToken), yyLex.value());
						}
					}
					if (this.yyToken == 0)
					{
						goto Block_44;
					}
					goto IL_4C34;
				}
				else
				{
					if ((num3 = (int)CSharpParser.yyGindex[num6]) != 0 && (num3 += num) >= 0 && num3 < CSharpParser.yyTable.Length && (int)CSharpParser.yyCheck[num3] == num)
					{
						num = (int)CSharpParser.yyTable[num3];
					}
					else
					{
						num = (int)CSharpParser.yyDgoto[num6];
					}
					if (this.debug != null)
					{
						this.debug.shift(array[this.yyTop], num);
						goto IL_4C34;
					}
					goto IL_4C34;
				}
			}
			IL_253:
			throw new yyUnexpectedEof();
			Block_30:
			if (this.debug != null)
			{
				this.debug.reject();
			}
			throw new yyException("irrecoverable syntax error");
			Block_32:
			if (this.debug != null)
			{
				this.debug.reject();
			}
			throw new yyException("irrecoverable syntax error at end-of-file");
			Block_44:
			if (this.debug != null)
			{
				this.debug.accept(this.yyVal);
			}
			return this.yyVal;
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x00052168 File Offset: 0x00050368
		private void case_6()
		{
			if (this.yyVals[0 + this.yyTop] != null)
			{
				Attributes attributes = (Attributes)this.yyVals[0 + this.yyTop];
				this.report.Error(1730, attributes.Attrs[0].Location, "Assembly and module attributes must precede all other elements except using clauses and extern alias declarations");
				this.current_namespace.UnattachedAttributes = attributes;
			}
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x000521CD File Offset: 0x000503CD
		private void case_8()
		{
			if (this.yyToken == 358)
			{
				this.report.Error(439, this.lexer.Location, "An extern alias declaration must precede all other elements");
				return;
			}
			this.Error_SyntaxError(this.yyToken);
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x0005220C File Offset: 0x0005040C
		private void case_13()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-2 + this.yyTop];
			if (locatedToken.Value != "alias")
			{
				this.syntax_error(locatedToken.Location, "`alias' expected");
				return;
			}
			if (this.lang_version == LanguageVersion.ISO_1)
			{
				this.FeatureIsNotAvailable(locatedToken.Location, "external alias");
			}
			locatedToken = (LocatedToken)this.yyVals[-1 + this.yyTop];
			if (locatedToken.Value == QualifiedAliasMember.GlobalAlias)
			{
				RootNamespace.Error_GlobalNamespaceRedefined(this.report, locatedToken.Location);
			}
			UsingExternAlias un = new UsingExternAlias(new SimpleMemberName(locatedToken.Value, locatedToken.Location), this.GetLocation(this.yyVals[-3 + this.yyTop]));
			this.current_namespace.AddUsing(un);
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x000522DE File Offset: 0x000504DE
		private void case_17()
		{
			if (this.doc_support)
			{
				this.Lexer.doc_state = XmlCommentState.Allowed;
			}
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x000522F4 File Offset: 0x000504F4
		private void case_18()
		{
			UsingClause un;
			if (this.yyVals[-2 + this.yyTop] != null)
			{
				if (this.lang_version <= LanguageVersion.V_5)
				{
					this.FeatureIsNotAvailable(this.GetLocation(this.yyVals[-2 + this.yyTop]), "using static");
				}
				un = new UsingType((ATypeNameExpression)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-3 + this.yyTop]));
			}
			else
			{
				un = new UsingNamespace((ATypeNameExpression)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-3 + this.yyTop]));
			}
			this.current_namespace.AddUsing(un);
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x000523AC File Offset: 0x000505AC
		private void case_19()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-3 + this.yyTop];
			if (this.lang_version != LanguageVersion.ISO_1 && locatedToken.Value == "global")
			{
				this.report.Warning(440, 2, locatedToken.Location, "An alias named `global' will not be used when resolving `global::'. The global namespace will be used instead");
			}
			if (this.yyVals[-4 + this.yyTop] != null)
			{
				this.report.Error(8085, this.GetLocation(this.yyVals[-4 + this.yyTop]), "A `using static' directive cannot be used to declare an alias");
			}
			UsingAliasNamespace un = new UsingAliasNamespace(new SimpleMemberName(locatedToken.Value, locatedToken.Location), (ATypeNameExpression)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-5 + this.yyTop]));
			this.current_namespace.AddUsing(un);
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x00052490 File Offset: 0x00050690
		private void case_20()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = null;
		}

		// Token: 0x0600127F RID: 4735 RVA: 0x000524A8 File Offset: 0x000506A8
		private void case_23()
		{
			Attributes attributes = (Attributes)this.yyVals[-2 + this.yyTop];
			MemberName memberName = (MemberName)this.yyVals[0 + this.yyTop];
			if (attributes != null)
			{
				bool flag = true;
				if (this.current_namespace.DeclarationFound || this.current_namespace != this.file)
				{
					flag = false;
				}
				else
				{
					foreach (Attribute attribute in attributes.Attrs)
					{
						if (!(attribute.ExplicitTarget == "assembly") && !(attribute.ExplicitTarget == "module"))
						{
							flag = false;
							break;
						}
					}
				}
				if (!flag)
				{
					this.report.Error(1671, memberName.Location, "A namespace declaration cannot have modifiers or attributes");
				}
			}
			this.module.AddAttributes(attributes, this.current_namespace);
			NamespaceContainer tc = new NamespaceContainer(memberName, this.current_namespace);
			this.current_namespace.AddTypeContainer(tc);
			this.current_container = (this.current_namespace = tc);
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x000522DE File Offset: 0x000504DE
		private void case_24()
		{
			if (this.doc_support)
			{
				this.Lexer.doc_state = XmlCommentState.Allowed;
			}
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x000525D0 File Offset: 0x000507D0
		private void case_25()
		{
			object obj = this.yyVals[0 + this.yyTop];
			this.current_container = (this.current_namespace = this.current_namespace.Parent);
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x00052608 File Offset: 0x00050808
		private void case_26()
		{
			this.report.Error(1514, this.lexer.Location, "Unexpected symbol `{0}', expecting `.' or `{{'", this.GetSymbolName(this.yyToken));
			NamespaceContainer tc = new NamespaceContainer((MemberName)this.yyVals[0 + this.yyTop], this.current_namespace);
			this.current_namespace.AddTypeContainer(tc);
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x00052490 File Offset: 0x00050690
		private void case_29()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = null;
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x00052670 File Offset: 0x00050870
		private void case_30()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[0 + this.yyTop];
			this.yyVal = new MemberName(locatedToken.Value, locatedToken.Location);
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x000526AC File Offset: 0x000508AC
		private void case_31()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[0 + this.yyTop];
			this.yyVal = new MemberName((MemberName)this.yyVals[-2 + this.yyTop], locatedToken.Value, locatedToken.Location);
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x000526FA File Offset: 0x000508FA
		private void case_32()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new MemberName("<invalid>", this.lexer.Location);
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x00052724 File Offset: 0x00050924
		private void case_45()
		{
			if (this.yyVals[0 + this.yyTop] != null)
			{
				TypeContainer typeContainer = (TypeContainer)this.yyVals[0 + this.yyTop];
				if ((typeContainer.ModFlags & (Modifiers.PROTECTED | Modifiers.PRIVATE)) != (Modifiers)0)
				{
					this.report.Error(1527, typeContainer.Location, "Namespace elements cannot be explicitly declared as private, protected or protected internal");
				}
				if (typeContainer.OptAttributes != null)
				{
					typeContainer.OptAttributes.ConvertGlobalAttributes(typeContainer, this.current_namespace, !this.current_namespace.DeclarationFound && this.current_namespace == this.file);
				}
			}
			this.current_namespace.DeclarationFound = true;
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x000527C0 File Offset: 0x000509C0
		private void case_47()
		{
			this.current_namespace.UnattachedAttributes = (Attributes)this.yyVals[-1 + this.yyTop];
			this.report.Error(1518, this.lexer.Location, "Attributes must be attached to class, delegate, enum, interface or struct");
			this.lexer.putback(125);
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x0005281C File Offset: 0x00050A1C
		private void case_55()
		{
			List<Attribute> attrs = (List<Attribute>)this.yyVals[0 + this.yyTop];
			this.yyVal = new Attributes(attrs);
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x0005284C File Offset: 0x00050A4C
		private void case_56()
		{
			Attributes attributes = this.yyVals[-1 + this.yyTop] as Attributes;
			List<Attribute> list = (List<Attribute>)this.yyVals[0 + this.yyTop];
			if (attributes == null)
			{
				attributes = new Attributes(list);
			}
			else if (list != null)
			{
				attributes.AddAttributes(list);
			}
			this.yyVal = attributes;
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x000528A0 File Offset: 0x00050AA0
		private void case_57()
		{
			this.lexer.parsing_attribute_section = true;
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x000528AE File Offset: 0x00050AAE
		private void case_58()
		{
			this.lexer.parsing_attribute_section = false;
			this.yyVal = this.yyVals[0 + this.yyTop];
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x000528D4 File Offset: 0x00050AD4
		private void case_59()
		{
			this.current_attr_target = (string)this.yyVals[-1 + this.yyTop];
			if (this.current_attr_target == "assembly" || this.current_attr_target == "module")
			{
				this.Lexer.check_incorrect_doc_comment();
			}
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x0005292C File Offset: 0x00050B2C
		private void case_60()
		{
			if (this.current_attr_target == string.Empty)
			{
				this.yyVal = new List<Attribute>(0);
			}
			else
			{
				this.yyVal = this.yyVals[-2 + this.yyTop];
			}
			object obj = this.yyVals[-1 + this.yyTop];
			this.current_attr_target = null;
			this.lexer.parsing_attribute_section = false;
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x00052992 File Offset: 0x00050B92
		private void case_61()
		{
			this.yyVal = this.yyVals[-2 + this.yyTop];
			object obj = this.yyVals[-1 + this.yyTop];
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x000529BC File Offset: 0x00050BBC
		private void case_62()
		{
			this.Error_SyntaxError(this.yyToken);
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-1 + this.yyTop];
			SimpleName expr = new SimpleName(locatedToken.Value, null, locatedToken.Location);
			this.yyVal = new List<Attribute>
			{
				new Attribute(null, expr, null, this.GetLocation(this.yyVals[-1 + this.yyTop]), false)
			};
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x00052A2C File Offset: 0x00050C2C
		private void case_63()
		{
			if (this.CheckAttributeTarget(this.yyToken, CSharpParser.GetTokenName(this.yyToken), this.GetLocation(this.yyVals[0 + this.yyTop])).Length > 0)
			{
				this.Error_SyntaxError(this.yyToken);
			}
			this.yyVal = null;
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x00052A80 File Offset: 0x00050C80
		private void case_64()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[0 + this.yyTop];
			this.yyVal = this.CheckAttributeTarget(this.yyToken, locatedToken.Value, locatedToken.Location);
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x00052AC0 File Offset: 0x00050CC0
		private void case_68()
		{
			List<Attribute> list = (List<Attribute>)this.yyVals[-2 + this.yyTop];
			if (list != null)
			{
				list.Add((Attribute)this.yyVals[0 + this.yyTop]);
			}
			this.yyVal = list;
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x00052B08 File Offset: 0x00050D08
		private void case_70()
		{
			this.lexer.parsing_block--;
			ATypeNameExpression atypeNameExpression = (ATypeNameExpression)this.yyVals[-2 + this.yyTop];
			if (atypeNameExpression.HasTypeArguments)
			{
				this.report.Error(404, atypeNameExpression.Location, "Attributes cannot be generic");
			}
			this.yyVal = new Attribute(this.current_attr_target, atypeNameExpression, (Arguments[])this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-2 + this.yyTop]), this.lexer.IsEscapedIdentifier(atypeNameExpression));
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x00052BAC File Offset: 0x00050DAC
		private void case_75()
		{
			Arguments arguments = new Arguments(4);
			arguments.Add((Argument)this.yyVals[0 + this.yyTop]);
			Arguments[] array = new Arguments[2];
			array[0] = arguments;
			this.yyVal = array;
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x00052BEC File Offset: 0x00050DEC
		private void case_76()
		{
			Arguments arguments = new Arguments(4);
			arguments.Add((Argument)this.yyVals[0 + this.yyTop]);
			this.yyVal = new Arguments[]
			{
				null,
				arguments
			};
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x00052C2C File Offset: 0x00050E2C
		private void case_77()
		{
			Arguments[] array = (Arguments[])this.yyVals[-2 + this.yyTop];
			if (array[1] != null)
			{
				this.report.Error(1016, ((Argument)this.yyVals[0 + this.yyTop]).Expr.Location, "Named attribute arguments must appear after the positional arguments");
				array[0] = new Arguments(4);
			}
			Arguments arguments = array[0];
			if (arguments.Count > 0 && !(this.yyVals[0 + this.yyTop] is NamedArgument))
			{
				Arguments arguments2 = arguments;
				if (arguments2[arguments2.Count - 1] is NamedArgument)
				{
					Arguments arguments3 = arguments;
					this.Error_NamedArgumentExpected((NamedArgument)arguments3[arguments3.Count - 1]);
				}
			}
			arguments.Add((Argument)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x00052CFC File Offset: 0x00050EFC
		private void case_78()
		{
			Arguments[] array = (Arguments[])this.yyVals[-2 + this.yyTop];
			if (array[1] == null)
			{
				array[1] = new Arguments(4);
			}
			array[1].Add((Argument)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x00052490 File Offset: 0x00050690
		private void case_81()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = null;
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x00052D4C File Offset: 0x00050F4C
		private void case_83()
		{
			this.lexer.parsing_block--;
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-3 + this.yyTop];
			this.yyVal = new NamedArgument(locatedToken.Value, locatedToken.Location, (Expression)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x00052DB0 File Offset: 0x00050FB0
		private void case_84()
		{
			if (this.lang_version <= LanguageVersion.V_3)
			{
				this.FeatureIsNotAvailable(this.GetLocation(this.yyVals[-3 + this.yyTop]), "named argument");
			}
			Argument.AType modifier = (this.yyVals[-1 + this.yyTop] == null) ? Argument.AType.None : ((Argument.AType)this.yyVals[-1 + this.yyTop]);
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-3 + this.yyTop];
			this.yyVal = new NamedArgument(locatedToken.Value, locatedToken.Location, (Expression)this.yyVals[0 + this.yyTop], modifier);
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x00052E52 File Offset: 0x00051052
		private void case_91()
		{
			this.lexer.parsing_modifiers = true;
			this.lexer.parsing_block = 0;
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x00052E52 File Offset: 0x00051052
		private void case_92()
		{
			this.lexer.parsing_modifiers = true;
			this.lexer.parsing_block = 0;
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x00052E6C File Offset: 0x0005106C
		private void case_106()
		{
			this.report.Error(1519, this.lexer.Location, "Unexpected symbol `{0}' in class, struct, or interface member declaration", this.GetSymbolName(this.yyToken));
			this.yyVal = null;
			this.lexer.parsing_generic_declaration = false;
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x00052EB8 File Offset: 0x000510B8
		private void case_107()
		{
			this.current_local_parameters = this.current_type.PrimaryConstructorParameters;
			if (this.current_local_parameters == null)
			{
				this.report.Error(9010, this.GetLocation(this.yyVals[0 + this.yyTop]), "Primary constructor body is not allowed");
				this.current_local_parameters = ParametersCompiled.EmptyReadOnlyParameters;
			}
			this.lexer.parsing_block++;
			this.start_block(this.GetLocation(this.yyVals[0 + this.yyTop]));
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x00052F44 File Offset: 0x00051144
		private void case_108()
		{
			this.current_local_parameters = null;
			ClassOrStruct classOrStruct = this.current_type as ClassOrStruct;
			if (classOrStruct != null)
			{
				ToplevelBlock toplevelBlock = (ToplevelBlock)this.yyVals[0 + this.yyTop];
				if (classOrStruct.PrimaryConstructorBlock != null)
				{
					this.report.Error(8041, toplevelBlock.StartLocation, "Primary constructor already has a body");
					return;
				}
				classOrStruct.PrimaryConstructorBlock = toplevelBlock;
			}
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x00052FA8 File Offset: 0x000511A8
		private void case_110()
		{
			this.lexer.ConstraintsParsing = true;
			this.valid_param_mod = CSharpParser.ParameterModifierType.PrimaryConstructor;
			this.push_current_container(new Struct(this.current_container, (MemberName)this.yyVals[0 + this.yyTop], (Modifiers)this.yyVals[-4 + this.yyTop], (Attributes)this.yyVals[-5 + this.yyTop]), this.yyVals[-3 + this.yyTop]);
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x00053028 File Offset: 0x00051228
		private void case_111()
		{
			this.valid_param_mod = (CSharpParser.ParameterModifierType)0;
			this.lexer.ConstraintsParsing = false;
			if (this.yyVals[-2 + this.yyTop] != null)
			{
				this.current_type.PrimaryConstructorParameters = (ParametersCompiled)this.yyVals[-2 + this.yyTop];
			}
			if (this.yyVals[0 + this.yyTop] != null)
			{
				this.current_container.SetConstraints((List<Constraints>)this.yyVals[0 + this.yyTop]);
			}
			if (this.doc_support)
			{
				this.current_container.PartialContainer.DocComment = this.Lexer.consume_doc_comment();
			}
			this.lexer.parsing_modifiers = true;
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x000522DE File Offset: 0x000504DE
		private void case_112()
		{
			if (this.doc_support)
			{
				this.Lexer.doc_state = XmlCommentState.Allowed;
			}
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x000530D9 File Offset: 0x000512D9
		private void case_113()
		{
			this.lexer.parsing_declaration--;
			if (this.doc_support)
			{
				this.Lexer.doc_state = XmlCommentState.Allowed;
			}
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x00053102 File Offset: 0x00051302
		private void case_114()
		{
			object obj = this.yyVals[-1 + this.yyTop];
			this.yyVal = this.pop_current_class();
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x00053120 File Offset: 0x00051320
		private void case_116()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[0 + this.yyTop];
			Modifiers modifiers = (Modifiers)this.yyVals[-3 + this.yyTop];
			this.current_field = new Const(this.current_type, (FullNamedExpression)this.yyVals[-1 + this.yyTop], modifiers, new MemberName(locatedToken.Value, locatedToken.Location), (Attributes)this.yyVals[-4 + this.yyTop]);
			this.current_type.AddMember(this.current_field);
			if ((modifiers & Modifiers.STATIC) != (Modifiers)0)
			{
				this.report.Error(504, this.current_field.Location, "The constant `{0}' cannot be marked static", this.current_field.GetSignatureForError());
			}
			this.yyVal = this.current_field;
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x000531F8 File Offset: 0x000513F8
		private void case_117()
		{
			if (this.doc_support)
			{
				this.current_field.DocComment = this.Lexer.consume_doc_comment();
				this.Lexer.doc_state = XmlCommentState.Allowed;
			}
			this.current_field.Initializer = (ConstInitializer)this.yyVals[-2 + this.yyTop];
			this.current_field = null;
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x00053258 File Offset: 0x00051458
		private void case_118()
		{
			this.Error_SyntaxError(this.yyToken);
			this.current_type.AddMember(new Const(this.current_type, (FullNamedExpression)this.yyVals[-1 + this.yyTop], (Modifiers)this.yyVals[-3 + this.yyTop], MemberName.Null, (Attributes)this.yyVals[-4 + this.yyTop]));
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x000532CC File Offset: 0x000514CC
		private void case_123()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-1 + this.yyTop];
			this.yyVal = new FieldDeclarator(new SimpleMemberName(locatedToken.Value, locatedToken.Location), (ConstInitializer)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x00053320 File Offset: 0x00051520
		private void case_125()
		{
			this.lexer.parsing_block--;
			this.yyVal = new ConstInitializer(this.current_field, (Expression)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-2 + this.yyTop]));
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x0005337B File Offset: 0x0005157B
		private void case_126()
		{
			this.report.Error(145, this.lexer.Location, "A const field requires a value to be provided");
			this.yyVal = null;
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x000533A4 File Offset: 0x000515A4
		private void case_129()
		{
			this.lexer.parsing_generic_declaration = false;
			FullNamedExpression fullNamedExpression = (FullNamedExpression)this.yyVals[-1 + this.yyTop];
			if (fullNamedExpression.Type != null && fullNamedExpression.Type.Kind == MemberKind.Void)
			{
				this.report.Error(670, this.GetLocation(this.yyVals[-1 + this.yyTop]), "Fields cannot have void type");
			}
			LocatedToken locatedToken = (LocatedToken)this.yyVals[0 + this.yyTop];
			this.current_field = new Field(this.current_type, fullNamedExpression, (Modifiers)this.yyVals[-2 + this.yyTop], new MemberName(locatedToken.Value, locatedToken.Location), (Attributes)this.yyVals[-3 + this.yyTop]);
			this.current_type.AddField(this.current_field);
			this.yyVal = this.current_field;
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x00053496 File Offset: 0x00051696
		private void case_130()
		{
			if (this.doc_support)
			{
				this.current_field.DocComment = this.Lexer.consume_doc_comment();
				this.Lexer.doc_state = XmlCommentState.Allowed;
			}
			this.yyVal = this.current_field;
			this.current_field = null;
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x000534D8 File Offset: 0x000516D8
		private void case_131()
		{
			if (this.lang_version < LanguageVersion.ISO_2)
			{
				this.FeatureIsNotAvailable(this.GetLocation(this.yyVals[-2 + this.yyTop]), "fixed size buffers");
			}
			LocatedToken locatedToken = (LocatedToken)this.yyVals[0 + this.yyTop];
			this.current_field = new FixedField(this.current_type, (FullNamedExpression)this.yyVals[-1 + this.yyTop], (Modifiers)this.yyVals[-3 + this.yyTop], new MemberName(locatedToken.Value, locatedToken.Location), (Attributes)this.yyVals[-4 + this.yyTop]);
			this.current_type.AddField(this.current_field);
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x00053598 File Offset: 0x00051798
		private void case_132()
		{
			if (this.doc_support)
			{
				this.current_field.DocComment = this.Lexer.consume_doc_comment();
				this.Lexer.doc_state = XmlCommentState.Allowed;
			}
			this.current_field.Initializer = (ConstInitializer)this.yyVals[-2 + this.yyTop];
			this.yyVal = this.current_field;
			this.current_field = null;
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x00053602 File Offset: 0x00051802
		private void case_135()
		{
			this.lexer.parsing_block++;
			this.current_local_parameters = ParametersCompiled.EmptyReadOnlyParameters;
			this.start_block(this.GetLocation(this.yyVals[0 + this.yyTop]));
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x00053640 File Offset: 0x00051840
		private void case_136()
		{
			this.lexer.parsing_block--;
			this.current_field.Initializer = (Expression)this.yyVals[0 + this.yyTop];
			this.end_block(this.lexer.Location);
			this.current_local_parameters = null;
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x00053698 File Offset: 0x00051898
		private void case_141()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[0 + this.yyTop];
			this.yyVal = new FieldDeclarator(new SimpleMemberName(locatedToken.Value, locatedToken.Location), null);
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x000536D8 File Offset: 0x000518D8
		private void case_143()
		{
			this.lexer.parsing_block--;
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-3 + this.yyTop];
			this.yyVal = new FieldDeclarator(new SimpleMemberName(locatedToken.Value, locatedToken.Location), (Expression)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x00053740 File Offset: 0x00051940
		private void case_148()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-1 + this.yyTop];
			this.yyVal = new FieldDeclarator(new SimpleMemberName(locatedToken.Value, locatedToken.Location), (ConstInitializer)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x00053794 File Offset: 0x00051994
		private void case_150()
		{
			this.lexer.parsing_block--;
			this.yyVal = new ConstInitializer(this.current_field, (Expression)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-3 + this.yyTop]));
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x000537EF File Offset: 0x000519EF
		private void case_151()
		{
			this.report.Error(443, this.lexer.Location, "Value or constant expected");
			this.yyVal = null;
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x00052490 File Offset: 0x00050690
		private void case_154()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = null;
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x00053818 File Offset: 0x00051A18
		private void case_155()
		{
			if (this.doc_support)
			{
				this.Lexer.doc_state = XmlCommentState.NotAllowed;
			}
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x00053830 File Offset: 0x00051A30
		private void case_156()
		{
			Method method = (Method)this.yyVals[-2 + this.yyTop];
			method.Block = (ToplevelBlock)this.yyVals[0 + this.yyTop];
			this.async_block = false;
			if (method.Block == null)
			{
				method.ParameterInfo.CheckParameters(method);
				if ((method.ModFlags & Modifiers.ASYNC) != (Modifiers)0)
				{
					this.report.Error(1994, method.Location, "`{0}': The async modifier can only be used with methods that have a body", method.GetSignatureForError());
				}
			}
			else if (this.current_container.Kind == MemberKind.Interface)
			{
				this.report.Error(531, method.Location, "`{0}': interface members cannot have a definition", method.GetSignatureForError());
			}
			this.current_local_parameters = null;
			if (this.doc_support)
			{
				this.Lexer.doc_state = XmlCommentState.Allowed;
			}
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x00053908 File Offset: 0x00051B08
		private void case_158()
		{
			this.valid_param_mod = (CSharpParser.ParameterModifierType)0;
			MemberName name = (MemberName)this.yyVals[-4 + this.yyTop];
			this.current_local_parameters = (ParametersCompiled)this.yyVals[-1 + this.yyTop];
			Method method = Method.Create(this.current_type, (FullNamedExpression)this.yyVals[-5 + this.yyTop], (Modifiers)this.yyVals[-6 + this.yyTop], name, this.current_local_parameters, (Attributes)this.yyVals[-7 + this.yyTop]);
			this.current_type.AddMember(method);
			this.async_block = ((method.ModFlags & Modifiers.ASYNC) > (Modifiers)0);
			if (this.doc_support)
			{
				method.DocComment = this.Lexer.consume_doc_comment();
			}
			this.yyVal = method;
			this.lexer.ConstraintsParsing = true;
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x000539EC File Offset: 0x00051BEC
		private void case_159()
		{
			this.lexer.ConstraintsParsing = false;
			if (this.yyVals[0 + this.yyTop] != null)
			{
				((Method)this.yyVals[-1 + this.yyTop]).SetConstraints((List<Constraints>)this.yyVals[0 + this.yyTop]);
			}
			this.yyVal = this.yyVals[-1 + this.yyTop];
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x00053A58 File Offset: 0x00051C58
		private void case_161()
		{
			this.lexer.parsing_generic_declaration = false;
			this.valid_param_mod = CSharpParser.ParameterModifierType.All;
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x00053A70 File Offset: 0x00051C70
		private void case_163()
		{
			this.lexer.ConstraintsParsing = false;
			this.valid_param_mod = (CSharpParser.ParameterModifierType)0;
			MemberName name = (MemberName)this.yyVals[-6 + this.yyTop];
			this.current_local_parameters = (ParametersCompiled)this.yyVals[-3 + this.yyTop];
			Modifiers modifiers = (Modifiers)this.yyVals[-10 + this.yyTop];
			modifiers |= Modifiers.PARTIAL;
			Method method = Method.Create(this.current_type, new TypeExpression(this.compiler.BuiltinTypes.Void, this.GetLocation(this.yyVals[-8 + this.yyTop])), modifiers, name, this.current_local_parameters, (Attributes)this.yyVals[-11 + this.yyTop]);
			this.current_type.AddMember(method);
			this.async_block = ((method.ModFlags & Modifiers.ASYNC) > (Modifiers)0);
			if (this.yyVals[0 + this.yyTop] != null)
			{
				method.SetConstraints((List<Constraints>)this.yyVals[0 + this.yyTop]);
			}
			if (this.doc_support)
			{
				method.DocComment = this.Lexer.consume_doc_comment();
			}
			this.yyVal = method;
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x00053BA0 File Offset: 0x00051DA0
		private void case_164()
		{
			MemberName memberName = (MemberName)this.yyVals[-3 + this.yyTop];
			this.report.Error(1585, memberName.Location, "Member modifier `{0}' must precede the member type and name", ModifiersExtensions.Name((Modifiers)this.yyVals[-4 + this.yyTop]));
			Method method = Method.Create(this.current_type, (FullNamedExpression)this.yyVals[-5 + this.yyTop], (Modifiers)0, memberName, (ParametersCompiled)this.yyVals[-1 + this.yyTop], (Attributes)this.yyVals[-7 + this.yyTop]);
			this.current_type.AddMember(method);
			this.current_local_parameters = (ParametersCompiled)this.yyVals[-1 + this.yyTop];
			if (this.doc_support)
			{
				method.DocComment = this.Lexer.consume_doc_comment();
			}
			this.yyVal = method;
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x00053C8C File Offset: 0x00051E8C
		private void case_165()
		{
			this.Error_SyntaxError(this.yyToken);
			this.current_local_parameters = ParametersCompiled.Undefined;
			MemberName name = (MemberName)this.yyVals[-1 + this.yyTop];
			Method method = Method.Create(this.current_type, (FullNamedExpression)this.yyVals[-2 + this.yyTop], (Modifiers)this.yyVals[-3 + this.yyTop], name, this.current_local_parameters, (Attributes)this.yyVals[-4 + this.yyTop]);
			this.current_type.AddMember(method);
			if (this.doc_support)
			{
				method.DocComment = this.Lexer.consume_doc_comment();
			}
			this.yyVal = method;
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x00053D44 File Offset: 0x00051F44
		private void case_170()
		{
			if (this.lang_version < LanguageVersion.V_6)
			{
				this.FeatureIsNotAvailable(this.GetLocation(this.yyVals[0 + this.yyTop]), "expression bodied members");
			}
			this.lexer.parsing_block++;
			this.start_block(this.GetLocation(this.yyVals[0 + this.yyTop]));
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x00053DA8 File Offset: 0x00051FA8
		private void case_171()
		{
			this.lexer.parsing_block = 0;
			this.current_block.AddStatement(new ContextualReturn((Expression)this.yyVals[-1 + this.yyTop]));
			Block block = this.end_block(this.GetLocation(this.yyVals[0 + this.yyTop]));
			block.IsCompilerGenerated = true;
			this.yyVal = block;
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x00053E10 File Offset: 0x00052010
		private void case_174()
		{
			List<Parameter> list = (List<Parameter>)this.yyVals[0 + this.yyTop];
			this.yyVal = new ParametersCompiled(list.ToArray());
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x00053E44 File Offset: 0x00052044
		private void case_175()
		{
			List<Parameter> list = (List<Parameter>)this.yyVals[-2 + this.yyTop];
			list.Add((Parameter)this.yyVals[0 + this.yyTop]);
			this.yyVal = new ParametersCompiled(list.ToArray());
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x00053E94 File Offset: 0x00052094
		private void case_176()
		{
			List<Parameter> list = (List<Parameter>)this.yyVals[-2 + this.yyTop];
			list.Add(new ArglistParameter(this.GetLocation(this.yyVals[0 + this.yyTop])));
			this.yyVal = new ParametersCompiled(list.ToArray(), true);
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x00053EEC File Offset: 0x000520EC
		private void case_177()
		{
			if (this.yyVals[-2 + this.yyTop] != null)
			{
				this.report.Error(231, ((Parameter)this.yyVals[-2 + this.yyTop]).Location, "A params parameter must be the last parameter in a formal parameter list");
			}
			this.yyVal = new ParametersCompiled(new Parameter[]
			{
				(Parameter)this.yyVals[-2 + this.yyTop]
			});
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x00053F64 File Offset: 0x00052164
		private void case_178()
		{
			if (this.yyVals[-2 + this.yyTop] != null)
			{
				this.report.Error(231, ((Parameter)this.yyVals[-2 + this.yyTop]).Location, "A params parameter must be the last parameter in a formal parameter list");
			}
			List<Parameter> list = (List<Parameter>)this.yyVals[-4 + this.yyTop];
			list.Add(new ArglistParameter(this.GetLocation(this.yyVals[-2 + this.yyTop])));
			this.yyVal = new ParametersCompiled(list.ToArray(), true);
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x00053FFC File Offset: 0x000521FC
		private void case_179()
		{
			this.report.Error(257, this.GetLocation(this.yyVals[-2 + this.yyTop]), "An __arglist parameter must be the last parameter in a formal parameter list");
			this.yyVal = new ParametersCompiled(new Parameter[]
			{
				new ArglistParameter(this.GetLocation(this.yyVals[-2 + this.yyTop]))
			}, true);
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x00054064 File Offset: 0x00052264
		private void case_180()
		{
			this.report.Error(257, this.GetLocation(this.yyVals[-2 + this.yyTop]), "An __arglist parameter must be the last parameter in a formal parameter list");
			List<Parameter> list = (List<Parameter>)this.yyVals[-4 + this.yyTop];
			list.Add(new ArglistParameter(this.GetLocation(this.yyVals[-2 + this.yyTop])));
			this.yyVal = new ParametersCompiled(list.ToArray(), true);
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x000540E5 File Offset: 0x000522E5
		private void case_183()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = ParametersCompiled.EmptyReadOnlyParameters;
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x00054100 File Offset: 0x00052300
		private void case_184()
		{
			this.parameters_bucket.Clear();
			Parameter parameter = (Parameter)this.yyVals[0 + this.yyTop];
			this.parameters_bucket.Add(parameter);
			this.default_parameter_used = parameter.HasDefaultValue;
			this.yyVal = this.parameters_bucket;
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x00054154 File Offset: 0x00052354
		private void case_185()
		{
			List<Parameter> list = (List<Parameter>)this.yyVals[-2 + this.yyTop];
			Parameter parameter = (Parameter)this.yyVals[0 + this.yyTop];
			if (parameter != null)
			{
				if (parameter.HasExtensionMethodModifier)
				{
					this.report.Error(1100, parameter.Location, "The parameter modifier `this' can only be used on the first parameter");
				}
				else if (!parameter.HasDefaultValue && this.default_parameter_used)
				{
					this.report.Error(1737, parameter.Location, "Optional parameter cannot precede required parameters");
				}
				this.default_parameter_used |= parameter.HasDefaultValue;
				list.Add(parameter);
			}
			this.yyVal = this.yyVals[-2 + this.yyTop];
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x00054210 File Offset: 0x00052410
		private void case_186()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[0 + this.yyTop];
			this.yyVal = new Parameter((FullNamedExpression)this.yyVals[-1 + this.yyTop], locatedToken.Value, (Parameter.Modifier)this.yyVals[-2 + this.yyTop], (Attributes)this.yyVals[-3 + this.yyTop], locatedToken.Location);
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x00054288 File Offset: 0x00052488
		private void case_187()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-2 + this.yyTop];
			this.report.Error(1552, locatedToken.Location, "Array type specifier, [], must appear before parameter name");
			this.yyVal = new Parameter((FullNamedExpression)this.yyVals[-3 + this.yyTop], locatedToken.Value, (Parameter.Modifier)this.yyVals[-4 + this.yyTop], (Attributes)this.yyVals[-5 + this.yyTop], locatedToken.Location);
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x0005431C File Offset: 0x0005251C
		private void case_188()
		{
			this.Error_SyntaxError(this.yyToken);
			Location location = this.GetLocation(this.yyVals[0 + this.yyTop]);
			this.yyVal = new Parameter(null, null, Parameter.Modifier.NONE, (Attributes)this.yyVals[-1 + this.yyTop], location);
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x00054370 File Offset: 0x00052570
		private void case_189()
		{
			this.Error_SyntaxError(this.yyToken);
			Location location = this.GetLocation(this.yyVals[0 + this.yyTop]);
			this.yyVal = new Parameter((FullNamedExpression)this.yyVals[-1 + this.yyTop], null, (Parameter.Modifier)this.yyVals[-2 + this.yyTop], (Attributes)this.yyVals[-3 + this.yyTop], location);
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x000543EC File Offset: 0x000525EC
		private void case_191()
		{
			this.lexer.parsing_block--;
			if (this.lang_version <= LanguageVersion.V_3)
			{
				this.FeatureIsNotAvailable(this.GetLocation(this.yyVals[-2 + this.yyTop]), "optional parameter");
			}
			Parameter.Modifier modifier = (Parameter.Modifier)this.yyVals[-5 + this.yyTop];
			if (modifier != Parameter.Modifier.NONE)
			{
				if (modifier != Parameter.Modifier.REF && modifier != Parameter.Modifier.OUT)
				{
					if (modifier != Parameter.Modifier.This)
					{
						throw new NotImplementedException(modifier.ToString());
					}
					this.report.Error(1743, this.GetLocation(this.yyVals[-5 + this.yyTop]), "Cannot specify a default value for the `{0}' parameter", Parameter.GetModifierSignature(modifier));
				}
				else
				{
					this.report.Error(1741, this.GetLocation(this.yyVals[-5 + this.yyTop]), "Cannot specify a default value for the `{0}' parameter", Parameter.GetModifierSignature(modifier));
				}
				modifier = Parameter.Modifier.NONE;
			}
			if ((this.valid_param_mod & CSharpParser.ParameterModifierType.DefaultValue) == (CSharpParser.ParameterModifierType)0)
			{
				this.report.Error(1065, this.GetLocation(this.yyVals[-2 + this.yyTop]), "Optional parameter is not valid in this context");
			}
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-3 + this.yyTop];
			this.yyVal = new Parameter((FullNamedExpression)this.yyVals[-4 + this.yyTop], locatedToken.Value, modifier, (Attributes)this.yyVals[-6 + this.yyTop], locatedToken.Location);
			if (this.yyVals[0 + this.yyTop] != null)
			{
				((Parameter)this.yyVal).DefaultValue = new DefaultParameterValueExpression((Expression)this.yyVals[0 + this.yyTop]);
			}
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x000545A4 File Offset: 0x000527A4
		private void case_195()
		{
			Parameter.Modifier modifier = (Parameter.Modifier)this.yyVals[0 + this.yyTop];
			Parameter.Modifier modifier2 = (Parameter.Modifier)this.yyVals[-1 + this.yyTop] | modifier;
			if (((Parameter.Modifier)this.yyVals[-1 + this.yyTop] & modifier) == modifier)
			{
				this.Error_DuplicateParameterModifier(this.lexer.Location, modifier);
			}
			else
			{
				Parameter.Modifier modifier3 = modifier2 & ~Parameter.Modifier.This;
				if (modifier3 != Parameter.Modifier.REF)
				{
					if (modifier3 != Parameter.Modifier.OUT)
					{
						this.report.Error(1108, this.lexer.Location, "A parameter cannot have specified more than one modifier");
					}
					else
					{
						this.report.Error(1102, this.lexer.Location, "The parameter modifiers `this' and `out' cannot be used altogether");
					}
				}
				else
				{
					this.report.Error(1101, this.lexer.Location, "The parameter modifiers `this' and `ref' cannot be used altogether");
				}
			}
			this.yyVal = modifier2;
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x0005468C File Offset: 0x0005288C
		private void case_196()
		{
			if ((this.valid_param_mod & CSharpParser.ParameterModifierType.Ref) == (CSharpParser.ParameterModifierType)0)
			{
				this.Error_ParameterModifierNotValid("ref", this.GetLocation(this.yyVals[0 + this.yyTop]));
			}
			this.yyVal = Parameter.Modifier.REF;
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x000546C4 File Offset: 0x000528C4
		private void case_197()
		{
			if ((this.valid_param_mod & CSharpParser.ParameterModifierType.Out) == (CSharpParser.ParameterModifierType)0)
			{
				this.Error_ParameterModifierNotValid("out", this.GetLocation(this.yyVals[0 + this.yyTop]));
			}
			this.yyVal = Parameter.Modifier.OUT;
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x000546FC File Offset: 0x000528FC
		private void case_198()
		{
			if ((this.valid_param_mod & CSharpParser.ParameterModifierType.This) == (CSharpParser.ParameterModifierType)0)
			{
				this.Error_ParameterModifierNotValid("this", this.GetLocation(this.yyVals[0 + this.yyTop]));
			}
			if (this.lang_version <= LanguageVersion.ISO_2)
			{
				this.FeatureIsNotAvailable(this.GetLocation(this.yyVals[0 + this.yyTop]), "extension methods");
			}
			this.yyVal = Parameter.Modifier.This;
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x00054768 File Offset: 0x00052968
		private void case_199()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[0 + this.yyTop];
			this.yyVal = new ParamsParameter((FullNamedExpression)this.yyVals[-1 + this.yyTop], locatedToken.Value, (Attributes)this.yyVals[-3 + this.yyTop], locatedToken.Location);
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x000547CC File Offset: 0x000529CC
		private void case_200()
		{
			this.report.Error(1751, this.GetLocation(this.yyVals[-4 + this.yyTop]), "Cannot specify a default value for a parameter array");
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-2 + this.yyTop];
			this.yyVal = new ParamsParameter((FullNamedExpression)this.yyVals[-3 + this.yyTop], locatedToken.Value, (Attributes)this.yyVals[-5 + this.yyTop], locatedToken.Location);
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x0005485C File Offset: 0x00052A5C
		private void case_201()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new ParamsParameter((FullNamedExpression)this.yyVals[-1 + this.yyTop], null, (Attributes)this.yyVals[-3 + this.yyTop], Location.Null);
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x000548AF File Offset: 0x00052AAF
		private void case_202()
		{
			if ((this.valid_param_mod & CSharpParser.ParameterModifierType.Params) == (CSharpParser.ParameterModifierType)0)
			{
				this.report.Error(1670, this.GetLocation(this.yyVals[0 + this.yyTop]), "The `params' modifier is not allowed in current context");
			}
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x000548E8 File Offset: 0x00052AE8
		private void case_203()
		{
			if (((Parameter.Modifier)this.yyVals[0 + this.yyTop] & Parameter.Modifier.This) != Parameter.Modifier.NONE)
			{
				this.report.Error(1104, this.GetLocation(this.yyVals[-1 + this.yyTop]), "The parameter modifiers `this' and `params' cannot be used altogether");
				return;
			}
			this.report.Error(1611, this.GetLocation(this.yyVals[-1 + this.yyTop]), "The params parameter cannot be declared as ref or out");
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x00054962 File Offset: 0x00052B62
		private void case_205()
		{
			if ((this.valid_param_mod & CSharpParser.ParameterModifierType.Arglist) == (CSharpParser.ParameterModifierType)0)
			{
				this.report.Error(1669, this.GetLocation(this.yyVals[0 + this.yyTop]), "__arglist is not valid in this context");
			}
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x00054999 File Offset: 0x00052B99
		private void case_206()
		{
			this.lexer.parsing_generic_declaration = false;
			if (this.doc_support)
			{
				this.tmpComment = this.Lexer.consume_doc_comment();
			}
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x000549C0 File Offset: 0x00052BC0
		private void case_207()
		{
			FullNamedExpression fullNamedExpression = (FullNamedExpression)this.yyVals[-3 + this.yyTop];
			this.current_property = new Property(this.current_type, fullNamedExpression, (Modifiers)this.yyVals[-4 + this.yyTop], (MemberName)this.yyVals[-2 + this.yyTop], (Attributes)this.yyVals[-5 + this.yyTop]);
			if (fullNamedExpression.Type != null && fullNamedExpression.Type.Kind == MemberKind.Void)
			{
				this.report.Error(547, this.GetLocation(this.yyVals[-3 + this.yyTop]), "`{0}': property or indexer cannot have void type", this.current_property.GetSignatureForError());
			}
			this.current_type.AddMember(this.current_property);
			this.lexer.PropertyParsing = true;
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x00054AA1 File Offset: 0x00052CA1
		private void case_208()
		{
			this.lexer.PropertyParsing = false;
			if (this.doc_support)
			{
				this.current_property.DocComment = this.ConsumeStoredComment();
			}
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x00054AC8 File Offset: 0x00052CC8
		private void case_209()
		{
			this.lexer.parsing_modifiers = true;
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x00054AD6 File Offset: 0x00052CD6
		private void case_211()
		{
			this.lexer.parsing_generic_declaration = false;
			if (this.doc_support)
			{
				this.tmpComment = this.Lexer.consume_doc_comment();
			}
			this.current_local_parameters = ParametersCompiled.EmptyReadOnlyParameters;
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x00054B08 File Offset: 0x00052D08
		private void case_212()
		{
			FullNamedExpression fullNamedExpression = (FullNamedExpression)this.yyVals[-3 + this.yyTop];
			Property property = new Property(this.current_type, fullNamedExpression, (Modifiers)this.yyVals[-4 + this.yyTop], (MemberName)this.yyVals[-2 + this.yyTop], (Attributes)this.yyVals[-5 + this.yyTop]);
			Property property2 = property;
			property2.Get = new PropertyBase.GetMethod(property2, Modifiers.COMPILER_GENERATED, null, property.Location);
			property.Get.Block = (ToplevelBlock)this.yyVals[0 + this.yyTop];
			if (this.current_container.Kind == MemberKind.Interface)
			{
				this.report.Error(531, property.Get.Block.StartLocation, "`{0}': interface members cannot have a definition", property.GetSignatureForError());
			}
			if (fullNamedExpression.Type != null && fullNamedExpression.Type.Kind == MemberKind.Void)
			{
				this.report.Error(547, this.GetLocation(this.yyVals[-3 + this.yyTop]), "`{0}': property or indexer cannot have void type", property.GetSignatureForError());
			}
			this.current_type.AddMember(property);
			this.current_local_parameters = null;
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x00053602 File Offset: 0x00051802
		private void case_214()
		{
			this.lexer.parsing_block++;
			this.current_local_parameters = ParametersCompiled.EmptyReadOnlyParameters;
			this.start_block(this.GetLocation(this.yyVals[0 + this.yyTop]));
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x00054C4C File Offset: 0x00052E4C
		private void case_215()
		{
			this.lexer.parsing_block--;
			((Property)this.current_property).Initializer = (Expression)this.yyVals[-1 + this.yyTop];
			this.end_block(this.GetLocation(this.yyVals[0 + this.yyTop]));
			this.current_local_parameters = null;
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x00054CB4 File Offset: 0x00052EB4
		private void case_219()
		{
			this.valid_param_mod = (CSharpParser.ParameterModifierType)0;
			FullNamedExpression fullNamedExpression = (FullNamedExpression)this.yyVals[-5 + this.yyTop];
			Indexer indexer = new Indexer(this.current_type, fullNamedExpression, (MemberName)this.yyVals[-4 + this.yyTop], (Modifiers)this.yyVals[-6 + this.yyTop], (ParametersCompiled)this.yyVals[-1 + this.yyTop], (Attributes)this.yyVals[-7 + this.yyTop]);
			this.current_property = indexer;
			this.current_type.AddIndexer(indexer);
			if (fullNamedExpression.Type != null && fullNamedExpression.Type.Kind == MemberKind.Void)
			{
				this.report.Error(620, this.GetLocation(this.yyVals[-5 + this.yyTop]), "`{0}': indexer return type cannot be `void'", indexer.GetSignatureForError());
			}
			if (indexer.ParameterInfo.IsEmpty)
			{
				this.report.Error(1551, this.GetLocation(this.yyVals[-3 + this.yyTop]), "Indexers must have at least one parameter");
			}
			if (this.doc_support)
			{
				this.tmpComment = this.Lexer.consume_doc_comment();
				this.Lexer.doc_state = XmlCommentState.Allowed;
			}
			this.lexer.PropertyParsing = true;
			this.current_local_parameters = (ParametersCompiled)this.yyVals[-1 + this.yyTop];
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x00054E20 File Offset: 0x00053020
		private void case_220()
		{
			this.lexer.PropertyParsing = false;
			this.current_local_parameters = null;
			if (this.current_property.AccessorFirst != null && this.current_property.AccessorFirst.Block == null)
			{
				((Indexer)this.current_property).ParameterInfo.CheckParameters(this.current_property);
			}
			if (this.doc_support)
			{
				this.current_property.DocComment = this.ConsumeStoredComment();
			}
			this.current_property = null;
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x00054E9C File Offset: 0x0005309C
		private void case_222()
		{
			this.current_property.Get = new Indexer.GetIndexerMethod(this.current_property, Modifiers.COMPILER_GENERATED, this.current_local_parameters, null, this.current_property.Location);
			this.current_property.Get.Block = (ToplevelBlock)this.yyVals[0 + this.yyTop];
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x00054EFC File Offset: 0x000530FC
		private void case_227()
		{
			if (this.yyToken == 372)
			{
				this.report.Error(548, this.lexer.Location, "`{0}': property or indexer must have at least one accessor", this.current_property.GetSignatureForError());
				return;
			}
			if (this.yyToken == 380)
			{
				this.report.Error(1597, this.lexer.Location, "Semicolon after method or accessor block is not valid");
				return;
			}
			this.report.Error(1014, this.GetLocation(this.yyVals[0 + this.yyTop]), "A get or set accessor expected");
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x00054F9C File Offset: 0x0005319C
		private void case_228()
		{
			if (this.yyVals[-1 + this.yyTop] != CSharpParser.ModifierNone && this.lang_version == LanguageVersion.ISO_1)
			{
				this.FeatureIsNotAvailable(this.GetLocation(this.yyVals[-1 + this.yyTop]), "access modifiers on properties");
			}
			if (this.current_property.Get != null)
			{
				this.report.Error(1007, this.GetLocation(this.yyVals[0 + this.yyTop]), "Property accessor already defined");
			}
			if (this.current_property is Indexer)
			{
				this.current_property.Get = new Indexer.GetIndexerMethod(this.current_property, (Modifiers)this.yyVals[-1 + this.yyTop], ((Indexer)this.current_property).ParameterInfo.Clone(), (Attributes)this.yyVals[-2 + this.yyTop], this.GetLocation(this.yyVals[0 + this.yyTop]));
			}
			else
			{
				this.current_property.Get = new PropertyBase.GetMethod(this.current_property, (Modifiers)this.yyVals[-1 + this.yyTop], (Attributes)this.yyVals[-2 + this.yyTop], this.GetLocation(this.yyVals[0 + this.yyTop]));
			}
			this.current_local_parameters = this.current_property.Get.ParameterInfo;
			this.lexer.PropertyParsing = false;
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x00055110 File Offset: 0x00053310
		private void case_229()
		{
			if (this.yyVals[0 + this.yyTop] != null)
			{
				this.current_property.Get.Block = (ToplevelBlock)this.yyVals[0 + this.yyTop];
				if (this.current_container.Kind == MemberKind.Interface)
				{
					this.report.Error(531, this.current_property.Get.Block.StartLocation, "`{0}': interface members cannot have a definition", this.current_property.Get.GetSignatureForError());
				}
			}
			this.current_local_parameters = null;
			this.lexer.PropertyParsing = true;
			if (this.doc_support && this.Lexer.doc_state == XmlCommentState.Error)
			{
				this.Lexer.doc_state = XmlCommentState.NotAllowed;
			}
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x000551D4 File Offset: 0x000533D4
		private void case_230()
		{
			if (this.yyVals[-1 + this.yyTop] != CSharpParser.ModifierNone && this.lang_version == LanguageVersion.ISO_1)
			{
				this.FeatureIsNotAvailable(this.GetLocation(this.yyVals[-1 + this.yyTop]), "access modifiers on properties");
			}
			if (this.current_property.Set != null)
			{
				this.report.Error(1007, this.GetLocation(this.yyVals[0 + this.yyTop]), "Property accessor already defined");
			}
			if (this.current_property is Indexer)
			{
				this.current_property.Set = new Indexer.SetIndexerMethod(this.current_property, (Modifiers)this.yyVals[-1 + this.yyTop], ParametersCompiled.MergeGenerated(this.compiler, ((Indexer)this.current_property).ParameterInfo, true, new Parameter(this.current_property.TypeExpression, "value", Parameter.Modifier.NONE, null, this.GetLocation(this.yyVals[0 + this.yyTop])), null), (Attributes)this.yyVals[-2 + this.yyTop], this.GetLocation(this.yyVals[0 + this.yyTop]));
			}
			else
			{
				this.current_property.Set = new PropertyBase.SetMethod(this.current_property, (Modifiers)this.yyVals[-1 + this.yyTop], ParametersCompiled.CreateImplicitParameter(this.current_property.TypeExpression, this.GetLocation(this.yyVals[0 + this.yyTop])), (Attributes)this.yyVals[-2 + this.yyTop], this.GetLocation(this.yyVals[0 + this.yyTop]));
			}
			this.current_local_parameters = this.current_property.Set.ParameterInfo;
			this.lexer.PropertyParsing = false;
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x000553A4 File Offset: 0x000535A4
		private void case_231()
		{
			if (this.yyVals[0 + this.yyTop] != null)
			{
				this.current_property.Set.Block = (ToplevelBlock)this.yyVals[0 + this.yyTop];
				if (this.current_container.Kind == MemberKind.Interface)
				{
					this.report.Error(531, this.current_property.Set.Block.StartLocation, "`{0}': interface members cannot have a definition", this.current_property.Set.GetSignatureForError());
				}
			}
			this.current_local_parameters = null;
			this.lexer.PropertyParsing = true;
			if (this.doc_support && this.Lexer.doc_state == XmlCommentState.Error)
			{
				this.Lexer.doc_state = XmlCommentState.NotAllowed;
			}
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x00055467 File Offset: 0x00053667
		private void case_233()
		{
			this.yyVal = null;
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x00055470 File Offset: 0x00053670
		private void case_234()
		{
			this.Error_SyntaxError(1043, this.yyToken, "Invalid accessor body");
			this.yyVal = null;
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x00055490 File Offset: 0x00053690
		private void case_236()
		{
			this.lexer.ConstraintsParsing = true;
			this.push_current_container(new Interface(this.current_container, (MemberName)this.yyVals[0 + this.yyTop], (Modifiers)this.yyVals[-4 + this.yyTop], (Attributes)this.yyVals[-5 + this.yyTop]), this.yyVals[-3 + this.yyTop]);
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x00055508 File Offset: 0x00053708
		private void case_237()
		{
			this.lexer.ConstraintsParsing = false;
			if (this.yyVals[0 + this.yyTop] != null)
			{
				this.current_container.SetConstraints((List<Constraints>)this.yyVals[0 + this.yyTop]);
			}
			if (this.doc_support)
			{
				this.current_container.PartialContainer.DocComment = this.Lexer.consume_doc_comment();
				this.Lexer.doc_state = XmlCommentState.Allowed;
			}
			this.lexer.parsing_modifiers = true;
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x000530D9 File Offset: 0x000512D9
		private void case_238()
		{
			this.lexer.parsing_declaration--;
			if (this.doc_support)
			{
				this.Lexer.doc_state = XmlCommentState.Allowed;
			}
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x0005558C File Offset: 0x0005378C
		private void case_239()
		{
			object obj = this.yyVals[0 + this.yyTop];
			this.yyVal = this.pop_current_class();
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x00052E52 File Offset: 0x00051052
		private void case_243()
		{
			this.lexer.parsing_modifiers = true;
			this.lexer.parsing_block = 0;
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x00052E52 File Offset: 0x00051052
		private void case_244()
		{
			this.lexer.parsing_modifiers = true;
			this.lexer.parsing_block = 0;
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x000555AC File Offset: 0x000537AC
		private void case_255()
		{
			CSharpParser.OperatorDeclaration operatorDeclaration = (CSharpParser.OperatorDeclaration)this.yyVals[-2 + this.yyTop];
			if (operatorDeclaration != null)
			{
				Operator @operator = new Operator(this.current_type, operatorDeclaration.optype, operatorDeclaration.ret_type, (Modifiers)this.yyVals[-3 + this.yyTop], this.current_local_parameters, (ToplevelBlock)this.yyVals[0 + this.yyTop], (Attributes)this.yyVals[-4 + this.yyTop], operatorDeclaration.location);
				if (@operator.Block == null)
				{
					@operator.ParameterInfo.CheckParameters(@operator);
				}
				if (this.doc_support)
				{
					@operator.DocComment = this.tmpComment;
					this.Lexer.doc_state = XmlCommentState.Allowed;
				}
				this.current_type.AddOperator(@operator);
			}
			this.current_local_parameters = null;
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x00055680 File Offset: 0x00053880
		private void case_257()
		{
			this.report.Error(590, this.GetLocation(this.yyVals[0 + this.yyTop]), "User-defined operators cannot return void");
			this.yyVal = new TypeExpression(this.compiler.BuiltinTypes.Void, this.GetLocation(this.yyVals[0 + this.yyTop]));
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x000556E7 File Offset: 0x000538E7
		private void case_258()
		{
			this.valid_param_mod = CSharpParser.ParameterModifierType.DefaultValue;
			if ((Operator.OpType)this.yyVals[-1 + this.yyTop] == Operator.OpType.Is)
			{
				this.valid_param_mod |= CSharpParser.ParameterModifierType.Out;
			}
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x00055718 File Offset: 0x00053918
		private void case_259()
		{
			this.valid_param_mod = (CSharpParser.ParameterModifierType)0;
			Location location = this.GetLocation(this.yyVals[-5 + this.yyTop]);
			Operator.OpType opType = (Operator.OpType)this.yyVals[-4 + this.yyTop];
			this.current_local_parameters = (ParametersCompiled)this.yyVals[-1 + this.yyTop];
			int count = this.current_local_parameters.Count;
			if (count == 1)
			{
				if (opType == Operator.OpType.Addition)
				{
					opType = Operator.OpType.UnaryPlus;
				}
				else if (opType == Operator.OpType.Subtraction)
				{
					opType = Operator.OpType.UnaryNegation;
				}
			}
			if (CSharpParser.IsUnaryOperator(opType))
			{
				if (count == 2)
				{
					this.report.Error(1020, location, "Overloadable binary operator expected");
				}
				else if (count != 1)
				{
					this.report.Error(1535, location, "Overloaded unary operator `{0}' takes one parameter", Operator.GetName(opType));
				}
			}
			else if (opType != Operator.OpType.Is)
			{
				if (count == 1)
				{
					this.report.Error(1019, location, "Overloadable unary operator expected");
				}
				else if (count != 2)
				{
					this.report.Error(1534, location, "Overloaded binary operator `{0}' takes two parameters", Operator.GetName(opType));
				}
			}
			if (this.doc_support)
			{
				this.tmpComment = this.Lexer.consume_doc_comment();
				this.Lexer.doc_state = XmlCommentState.NotAllowed;
			}
			this.yyVal = new CSharpParser.OperatorDeclaration(opType, (FullNamedExpression)this.yyVals[-6 + this.yyTop], location);
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x00055860 File Offset: 0x00053A60
		private void case_283()
		{
			if (this.lang_version != LanguageVersion.Experimental)
			{
				this.FeatureIsNotAvailable(this.GetLocation(this.yyVals[0 + this.yyTop]), "is user operator");
			}
			this.yyVal = Operator.OpType.Is;
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x0005589C File Offset: 0x00053A9C
		private void case_285()
		{
			this.valid_param_mod = (CSharpParser.ParameterModifierType)0;
			Location location = this.GetLocation(this.yyVals[-5 + this.yyTop]);
			this.current_local_parameters = (ParametersCompiled)this.yyVals[-1 + this.yyTop];
			if (this.current_local_parameters.Count != 1)
			{
				this.report.Error(1535, location, "Overloaded unary operator `implicit' takes one parameter");
			}
			if (this.doc_support)
			{
				this.tmpComment = this.Lexer.consume_doc_comment();
				this.Lexer.doc_state = XmlCommentState.NotAllowed;
			}
			this.yyVal = new CSharpParser.OperatorDeclaration(Operator.OpType.Implicit, (FullNamedExpression)this.yyVals[-4 + this.yyTop], location);
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x00055950 File Offset: 0x00053B50
		private void case_287()
		{
			this.valid_param_mod = (CSharpParser.ParameterModifierType)0;
			Location location = this.GetLocation(this.yyVals[-5 + this.yyTop]);
			this.current_local_parameters = (ParametersCompiled)this.yyVals[-1 + this.yyTop];
			if (this.current_local_parameters.Count != 1)
			{
				this.report.Error(1535, location, "Overloaded unary operator `explicit' takes one parameter");
			}
			if (this.doc_support)
			{
				this.tmpComment = this.Lexer.consume_doc_comment();
				this.Lexer.doc_state = XmlCommentState.NotAllowed;
			}
			this.yyVal = new CSharpParser.OperatorDeclaration(Operator.OpType.Explicit, (FullNamedExpression)this.yyVals[-4 + this.yyTop], location);
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x00055A01 File Offset: 0x00053C01
		private void case_288()
		{
			this.Error_SyntaxError(this.yyToken);
			this.current_local_parameters = ParametersCompiled.EmptyReadOnlyParameters;
			this.yyVal = new CSharpParser.OperatorDeclaration(Operator.OpType.Implicit, null, this.GetLocation(this.yyVals[-1 + this.yyTop]));
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x00055A3D File Offset: 0x00053C3D
		private void case_289()
		{
			this.Error_SyntaxError(this.yyToken);
			this.current_local_parameters = ParametersCompiled.EmptyReadOnlyParameters;
			this.yyVal = new CSharpParser.OperatorDeclaration(Operator.OpType.Explicit, null, this.GetLocation(this.yyVals[-1 + this.yyTop]));
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x00055A7C File Offset: 0x00053C7C
		private void case_290()
		{
			Constructor constructor = (Constructor)this.yyVals[-1 + this.yyTop];
			constructor.Block = (ToplevelBlock)this.yyVals[0 + this.yyTop];
			if (this.doc_support)
			{
				constructor.DocComment = this.ConsumeStoredComment();
			}
			this.current_local_parameters = null;
			if (this.doc_support)
			{
				this.Lexer.doc_state = XmlCommentState.Allowed;
			}
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x00055AE7 File Offset: 0x00053CE7
		private void case_291()
		{
			if (this.doc_support)
			{
				this.tmpComment = this.Lexer.consume_doc_comment();
				this.Lexer.doc_state = XmlCommentState.Allowed;
			}
			this.valid_param_mod = CSharpParser.ParameterModifierType.All;
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x00055B18 File Offset: 0x00053D18
		private void case_292()
		{
			this.valid_param_mod = (CSharpParser.ParameterModifierType)0;
			this.current_local_parameters = (ParametersCompiled)this.yyVals[-1 + this.yyTop];
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-4 + this.yyTop];
			Modifiers modifiers = (Modifiers)this.yyVals[-5 + this.yyTop];
			Constructor constructor = new Constructor(this.current_type, locatedToken.Value, modifiers, (Attributes)this.yyVals[-6 + this.yyTop], this.current_local_parameters, locatedToken.Location);
			if (locatedToken.Value != this.current_container.MemberName.Name)
			{
				this.report.Error(1520, constructor.Location, "Class, struct, or interface method must have a return type");
			}
			else if ((modifiers & Modifiers.STATIC) != (Modifiers)0)
			{
				if (!this.current_local_parameters.IsEmpty)
				{
					this.report.Error(132, constructor.Location, "`{0}': The static constructor must be parameterless", constructor.GetSignatureForError());
				}
				if ((modifiers & Modifiers.AccessibilityMask) != (Modifiers)0)
				{
					this.report.Error(515, constructor.Location, "`{0}': static constructor cannot have an access modifier", constructor.GetSignatureForError());
				}
			}
			else if (this.current_type.Kind == MemberKind.Struct && this.current_local_parameters.IsEmpty)
			{
				if (this.lang_version < LanguageVersion.V_6)
				{
					this.FeatureIsNotAvailable(this.GetLocation(this.yyVals[-4 + this.yyTop]), "struct parameterless instance constructor");
				}
				if ((modifiers & Modifiers.PUBLIC) == (Modifiers)0)
				{
					this.report.Error(8075, constructor.Location, "`{0}': Structs parameterless instance constructor must be public", constructor.GetSignatureForError());
				}
			}
			this.current_type.AddConstructor(constructor);
			this.yyVal = constructor;
			this.start_block(this.lexer.Location);
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x00055CDC File Offset: 0x00053EDC
		private void case_293()
		{
			if (this.yyVals[0 + this.yyTop] != null)
			{
				Constructor constructor = (Constructor)this.yyVals[-1 + this.yyTop];
				constructor.Initializer = (ConstructorInitializer)this.yyVals[0 + this.yyTop];
				if (constructor.IsStatic)
				{
					this.report.Error(514, constructor.Location, "`{0}': static constructor cannot have an explicit `this' or `base' constructor call", constructor.GetSignatureForError());
				}
			}
			this.yyVal = this.yyVals[-1 + this.yyTop];
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x00055D68 File Offset: 0x00053F68
		private void case_299()
		{
			this.lexer.parsing_block--;
			this.yyVal = new ConstructorBaseInitializer((Arguments)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-4 + this.yyTop]));
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x00055DC0 File Offset: 0x00053FC0
		private void case_301()
		{
			this.lexer.parsing_block--;
			this.yyVal = new ConstructorThisInitializer((Arguments)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-4 + this.yyTop]));
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x00055E15 File Offset: 0x00054015
		private void case_302()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new ConstructorThisInitializer(null, this.GetLocation(this.yyVals[0 + this.yyTop]));
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x00052490 File Offset: 0x00050690
		private void case_303()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = null;
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x00055E44 File Offset: 0x00054044
		private void case_304()
		{
			if (this.doc_support)
			{
				this.tmpComment = this.Lexer.consume_doc_comment();
				this.Lexer.doc_state = XmlCommentState.NotAllowed;
			}
			this.current_local_parameters = ParametersCompiled.EmptyReadOnlyParameters;
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x00055E78 File Offset: 0x00054078
		private void case_305()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-3 + this.yyTop];
			if (locatedToken.Value != this.current_container.MemberName.Name)
			{
				this.report.Error(574, locatedToken.Location, "Name of destructor must match name of class");
			}
			else if (this.current_container.Kind != MemberKind.Class)
			{
				this.report.Error(575, locatedToken.Location, "Only class types can contain destructor");
			}
			Destructor destructor = new Destructor(this.current_type, (Modifiers)this.yyVals[-6 + this.yyTop], ParametersCompiled.EmptyReadOnlyParameters, (Attributes)this.yyVals[-7 + this.yyTop], locatedToken.Location);
			if (this.doc_support)
			{
				destructor.DocComment = this.ConsumeStoredComment();
			}
			destructor.Block = (ToplevelBlock)this.yyVals[0 + this.yyTop];
			this.current_type.AddMember(destructor);
			this.current_local_parameters = null;
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x00055F84 File Offset: 0x00054184
		private void case_306()
		{
			this.current_event_field = new EventField(this.current_type, (FullNamedExpression)this.yyVals[-1 + this.yyTop], (Modifiers)this.yyVals[-3 + this.yyTop], (MemberName)this.yyVals[0 + this.yyTop], (Attributes)this.yyVals[-4 + this.yyTop]);
			this.current_type.AddMember(this.current_event_field);
			if (this.current_event_field.MemberName.ExplicitInterface != null)
			{
				this.report.Error(71, this.current_event_field.Location, "`{0}': An explicit interface implementation of an event must use property syntax", this.current_event_field.GetSignatureForError());
			}
			this.yyVal = this.current_event_field;
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x0005604B File Offset: 0x0005424B
		private void case_307()
		{
			if (this.doc_support)
			{
				this.current_event_field.DocComment = this.Lexer.consume_doc_comment();
				this.Lexer.doc_state = XmlCommentState.Allowed;
			}
			this.current_event_field = null;
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x00056080 File Offset: 0x00054280
		private void case_308()
		{
			this.current_event = new EventProperty(this.current_type, (FullNamedExpression)this.yyVals[-2 + this.yyTop], (Modifiers)this.yyVals[-4 + this.yyTop], (MemberName)this.yyVals[-1 + this.yyTop], (Attributes)this.yyVals[-5 + this.yyTop]);
			this.current_type.AddMember(this.current_event);
			this.lexer.EventParsing = true;
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x00056110 File Offset: 0x00054310
		private void case_309()
		{
			if (this.current_container.Kind == MemberKind.Interface)
			{
				this.report.Error(69, this.GetLocation(this.yyVals[-2 + this.yyTop]), "Event in interface cannot have add or remove accessors");
			}
			this.lexer.EventParsing = false;
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x00056163 File Offset: 0x00054363
		private void case_310()
		{
			if (this.doc_support)
			{
				this.current_event.DocComment = this.Lexer.consume_doc_comment();
				this.Lexer.doc_state = XmlCommentState.Allowed;
			}
			this.current_event = null;
			this.current_local_parameters = null;
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x000561A0 File Offset: 0x000543A0
		private void case_311()
		{
			this.Error_SyntaxError(this.yyToken);
			this.current_type.AddMember(new EventField(this.current_type, (FullNamedExpression)this.yyVals[-1 + this.yyTop], (Modifiers)this.yyVals[-3 + this.yyTop], MemberName.Null, (Attributes)this.yyVals[-4 + this.yyTop]));
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x00056212 File Offset: 0x00054412
		private void case_314()
		{
			this.lexer.parsing_block--;
			this.current_event_field.Initializer = (Expression)this.yyVals[0 + this.yyTop];
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x00056248 File Offset: 0x00054448
		private void case_319()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[0 + this.yyTop];
			this.yyVal = new FieldDeclarator(new SimpleMemberName(locatedToken.Value, locatedToken.Location), null);
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x00056288 File Offset: 0x00054488
		private void case_321()
		{
			this.lexer.parsing_block--;
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-3 + this.yyTop];
			this.yyVal = new FieldDeclarator(new SimpleMemberName(locatedToken.Value, locatedToken.Location), (Expression)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x000562F0 File Offset: 0x000544F0
		private void case_322()
		{
			if (this.current_container.Kind == MemberKind.Interface)
			{
				this.report.Error(68, this.lexer.Location, "`{0}': event in interface cannot have an initializer", this.current_event_field.GetSignatureForError());
			}
			if ((this.current_event_field.ModFlags & Modifiers.ABSTRACT) != (Modifiers)0)
			{
				this.report.Error(74, this.lexer.Location, "`{0}': abstract event cannot have an initializer", this.current_event_field.GetSignatureForError());
			}
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x0005636F File Offset: 0x0005456F
		private void case_326()
		{
			this.report.Error(65, this.lexer.Location, "`{0}': event property must have both add and remove accessors", this.current_event.GetSignatureForError());
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x0005636F File Offset: 0x0005456F
		private void case_327()
		{
			this.report.Error(65, this.lexer.Location, "`{0}': event property must have both add and remove accessors", this.current_event.GetSignatureForError());
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x00056399 File Offset: 0x00054599
		private void case_328()
		{
			this.report.Error(1055, this.GetLocation(this.yyVals[0 + this.yyTop]), "An add or remove accessor expected");
			this.yyVal = null;
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x000563CC File Offset: 0x000545CC
		private void case_329()
		{
			if (this.yyVals[-1 + this.yyTop] != CSharpParser.ModifierNone)
			{
				this.report.Error(1609, this.GetLocation(this.yyVals[-1 + this.yyTop]), "Modifiers cannot be placed on event accessor declarations");
			}
			this.current_event.Add = new EventProperty.AddDelegateMethod(this.current_event, (Attributes)this.yyVals[-2 + this.yyTop], this.GetLocation(this.yyVals[0 + this.yyTop]));
			this.current_local_parameters = this.current_event.Add.ParameterInfo;
			this.lexer.EventParsing = false;
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x0005647C File Offset: 0x0005467C
		private void case_330()
		{
			this.lexer.EventParsing = true;
			this.current_event.Add.Block = (ToplevelBlock)this.yyVals[0 + this.yyTop];
			if (this.current_container.Kind == MemberKind.Interface)
			{
				this.report.Error(531, this.current_event.Add.Block.StartLocation, "`{0}': interface members cannot have a definition", this.current_event.Add.GetSignatureForError());
			}
			this.current_local_parameters = null;
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x0005650C File Offset: 0x0005470C
		private void case_331()
		{
			if (this.yyVals[-1 + this.yyTop] != CSharpParser.ModifierNone)
			{
				this.report.Error(1609, this.GetLocation(this.yyVals[-1 + this.yyTop]), "Modifiers cannot be placed on event accessor declarations");
			}
			this.current_event.Remove = new EventProperty.RemoveDelegateMethod(this.current_event, (Attributes)this.yyVals[-2 + this.yyTop], this.GetLocation(this.yyVals[0 + this.yyTop]));
			this.current_local_parameters = this.current_event.Remove.ParameterInfo;
			this.lexer.EventParsing = false;
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x000565BC File Offset: 0x000547BC
		private void case_332()
		{
			this.lexer.EventParsing = true;
			this.current_event.Remove.Block = (ToplevelBlock)this.yyVals[0 + this.yyTop];
			if (this.current_container.Kind == MemberKind.Interface)
			{
				this.report.Error(531, this.current_event.Remove.Block.StartLocation, "`{0}': interface members cannot have a definition", this.current_event.Remove.GetSignatureForError());
			}
			this.current_local_parameters = null;
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x0005664C File Offset: 0x0005484C
		private void case_333()
		{
			this.report.Error(73, this.lexer.Location, "An add or remove accessor must have a body");
			this.yyVal = null;
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x00056674 File Offset: 0x00054874
		private void case_335()
		{
			this.current_type.UnattachedAttributes = (Attributes)this.yyVals[-1 + this.yyTop];
			this.report.Error(1519, this.GetLocation(this.yyVals[-1 + this.yyTop]), "An attribute is missing member declaration");
			this.lexer.putback(125);
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x000566D8 File Offset: 0x000548D8
		private void case_336()
		{
			this.report.Error(1519, this.lexer.Location, "Unexpected symbol `}' in class, struct, or interface member declaration");
			this.lexer.putback(125);
			this.lexer.parsing_generic_declaration = false;
			FullNamedExpression type = (FullNamedExpression)this.yyVals[-1 + this.yyTop];
			this.current_field = new Field(this.current_type, type, (Modifiers)this.yyVals[-2 + this.yyTop], MemberName.Null, (Attributes)this.yyVals[-3 + this.yyTop]);
			this.current_type.AddField(this.current_field);
			this.yyVal = this.current_field;
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x00056792 File Offset: 0x00054992
		private void case_337()
		{
			if (this.doc_support)
			{
				this.enumTypeComment = this.Lexer.consume_doc_comment();
			}
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x000567B0 File Offset: 0x000549B0
		private void case_338()
		{
			if (this.doc_support)
			{
				this.Lexer.doc_state = XmlCommentState.Allowed;
			}
			MemberName memberName = (MemberName)this.yyVals[-3 + this.yyTop];
			if (memberName.IsGeneric)
			{
				this.report.Error(1675, memberName.Location, "Enums cannot have type parameters");
			}
			this.push_current_container(new Enum(this.current_container, (FullNamedExpression)this.yyVals[-2 + this.yyTop], (Modifiers)this.yyVals[-5 + this.yyTop], memberName, (Attributes)this.yyVals[-6 + this.yyTop]), null);
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x0005685C File Offset: 0x00054A5C
		private void case_339()
		{
			this.lexer.parsing_modifiers = true;
			if (this.doc_support)
			{
				this.Lexer.doc_state = XmlCommentState.Allowed;
			}
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x0005687E File Offset: 0x00054A7E
		private void case_340()
		{
			if (this.doc_support)
			{
				this.current_container.DocComment = this.enumTypeComment;
			}
			this.lexer.parsing_declaration--;
			this.yyVal = this.pop_current_class();
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x000568B8 File Offset: 0x00054AB8
		private void case_343()
		{
			this.Error_TypeExpected(this.GetLocation(this.yyVals[-1 + this.yyTop]));
			this.yyVal = null;
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x000568DC File Offset: 0x00054ADC
		private void case_348()
		{
			this.yyVal = this.yyVals[0 + this.yyTop];
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x000568F4 File Offset: 0x00054AF4
		private void case_349()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[0 + this.yyTop];
			EnumMember enumMember = new EnumMember((Enum)this.current_type, new MemberName(locatedToken.Value, locatedToken.Location), (Attributes)this.yyVals[-1 + this.yyTop]);
			((Enum)this.current_type).AddEnumMember(enumMember);
			if (this.doc_support)
			{
				enumMember.DocComment = this.Lexer.consume_doc_comment();
				this.Lexer.doc_state = XmlCommentState.Allowed;
			}
			this.yyVal = enumMember;
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x00056989 File Offset: 0x00054B89
		private void case_350()
		{
			this.lexer.parsing_block++;
			if (this.doc_support)
			{
				this.tmpComment = this.Lexer.consume_doc_comment();
				this.Lexer.doc_state = XmlCommentState.NotAllowed;
			}
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x000569C4 File Offset: 0x00054BC4
		private void case_351()
		{
			this.lexer.parsing_block--;
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-3 + this.yyTop];
			EnumMember enumMember = new EnumMember((Enum)this.current_type, new MemberName(locatedToken.Value, locatedToken.Location), (Attributes)this.yyVals[-4 + this.yyTop]);
			EnumMember enumMember2 = enumMember;
			enumMember2.Initializer = new ConstInitializer(enumMember2, (Expression)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-1 + this.yyTop]));
			((Enum)this.current_type).AddEnumMember(enumMember);
			if (this.doc_support)
			{
				enumMember.DocComment = this.ConsumeStoredComment();
			}
			this.yyVal = enumMember;
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x00056A94 File Offset: 0x00054C94
		private void case_352()
		{
			this.Error_SyntaxError(this.yyToken);
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-1 + this.yyTop];
			EnumMember enumMember = new EnumMember((Enum)this.current_type, new MemberName(locatedToken.Value, locatedToken.Location), (Attributes)this.yyVals[-2 + this.yyTop]);
			((Enum)this.current_type).AddEnumMember(enumMember);
			if (this.doc_support)
			{
				enumMember.DocComment = this.Lexer.consume_doc_comment();
				this.Lexer.doc_state = XmlCommentState.Allowed;
			}
			this.yyVal = enumMember;
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x00056B38 File Offset: 0x00054D38
		private void case_355()
		{
			this.valid_param_mod = (CSharpParser.ParameterModifierType)0;
			ParametersCompiled parametersCompiled = (ParametersCompiled)this.yyVals[-1 + this.yyTop];
			Delegate @delegate = new Delegate(this.current_container, (FullNamedExpression)this.yyVals[-5 + this.yyTop], (Modifiers)this.yyVals[-7 + this.yyTop], (MemberName)this.yyVals[-4 + this.yyTop], parametersCompiled, (Attributes)this.yyVals[-8 + this.yyTop]);
			parametersCompiled.CheckParameters(@delegate);
			this.current_container.AddTypeContainer(@delegate);
			this.current_delegate = @delegate;
			this.lexer.ConstraintsParsing = true;
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x00056BE8 File Offset: 0x00054DE8
		private void case_357()
		{
			if (this.doc_support)
			{
				this.current_delegate.DocComment = this.Lexer.consume_doc_comment();
				this.Lexer.doc_state = XmlCommentState.Allowed;
			}
			if (this.yyVals[-2 + this.yyTop] != null)
			{
				this.current_delegate.SetConstraints((List<Constraints>)this.yyVals[-2 + this.yyTop]);
			}
			this.yyVal = this.current_delegate;
			this.current_delegate = null;
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x00056C64 File Offset: 0x00054E64
		private void case_359()
		{
			if (this.lang_version < LanguageVersion.ISO_2)
			{
				this.FeatureIsNotAvailable(this.GetLocation(this.yyVals[0 + this.yyTop]), "nullable types");
			}
			this.yyVal = ComposedTypeSpecifier.CreateNullable(this.GetLocation(this.yyVals[0 + this.yyTop]));
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x00056CBC File Offset: 0x00054EBC
		private void case_361()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-2 + this.yyTop];
			LocatedToken locatedToken2 = (LocatedToken)this.yyVals[-1 + this.yyTop];
			this.yyVal = new QualifiedAliasMember(locatedToken.Value, locatedToken2.Value, (TypeArguments)this.yyVals[0 + this.yyTop], locatedToken.Location);
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x00056D28 File Offset: 0x00054F28
		private void case_362()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-2 + this.yyTop];
			LocatedToken locatedToken2 = (LocatedToken)this.yyVals[-1 + this.yyTop];
			this.yyVal = new QualifiedAliasMember(locatedToken.Value, locatedToken2.Value, (int)this.yyVals[0 + this.yyTop], locatedToken.Location);
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x00056D94 File Offset: 0x00054F94
		private void case_364()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-1 + this.yyTop];
			this.yyVal = new MemberAccess((Expression)this.yyVals[-3 + this.yyTop], locatedToken.Value, (TypeArguments)this.yyVals[0 + this.yyTop], locatedToken.Location);
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x00056DF8 File Offset: 0x00054FF8
		private void case_365()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-1 + this.yyTop];
			this.yyVal = new MemberAccess((Expression)this.yyVals[-3 + this.yyTop], locatedToken.Value, (int)this.yyVals[0 + this.yyTop], locatedToken.Location);
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x00056E5C File Offset: 0x0005505C
		private void case_366()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-1 + this.yyTop];
			this.yyVal = new SimpleName(locatedToken.Value, (TypeArguments)this.yyVals[0 + this.yyTop], locatedToken.Location);
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x00056EAC File Offset: 0x000550AC
		private void case_367()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-1 + this.yyTop];
			this.yyVal = new SimpleName(locatedToken.Value, (int)this.yyVals[0 + this.yyTop], locatedToken.Location);
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x00056EFC File Offset: 0x000550FC
		private void case_369()
		{
			if (this.lang_version < LanguageVersion.ISO_2)
			{
				this.FeatureIsNotAvailable(this.GetLocation(this.yyVals[-2 + this.yyTop]), "generics");
			}
			this.yyVal = this.yyVals[-1 + this.yyTop];
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x00056F48 File Offset: 0x00055148
		private void case_370()
		{
			this.Error_TypeExpected(this.lexer.Location);
			this.yyVal = new TypeArguments(new FullNamedExpression[0]);
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x00056F6C File Offset: 0x0005516C
		private void case_371()
		{
			TypeArguments typeArguments = new TypeArguments(new FullNamedExpression[0]);
			typeArguments.Add((FullNamedExpression)this.yyVals[0 + this.yyTop]);
			this.yyVal = typeArguments;
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x00056FA8 File Offset: 0x000551A8
		private void case_372()
		{
			TypeArguments typeArguments = (TypeArguments)this.yyVals[-2 + this.yyTop];
			typeArguments.Add((FullNamedExpression)this.yyVals[0 + this.yyTop]);
			this.yyVal = typeArguments;
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x00056FEC File Offset: 0x000551EC
		private void case_374()
		{
			this.lexer.parsing_generic_declaration = false;
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-2 + this.yyTop];
			this.yyVal = new MemberName(locatedToken.Value, (TypeParameters)this.yyVals[0 + this.yyTop], locatedToken.Location);
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x00057048 File Offset: 0x00055248
		private void case_375()
		{
			MemberName memberName = (MemberName)this.yyVals[0 + this.yyTop];
			if (memberName.TypeParameters != null)
			{
				this.syntax_error(memberName.Location, string.Format("Member `{0}' cannot declare type arguments", memberName.GetSignatureForError()));
			}
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x00057090 File Offset: 0x00055290
		private void case_377()
		{
			this.lexer.parsing_generic_declaration = false;
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-1 + this.yyTop];
			this.yyVal = new MemberName(locatedToken.Value, (TypeParameters)this.yyVals[0 + this.yyTop], (ATypeNameExpression)this.yyVals[-2 + this.yyTop], locatedToken.Location);
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x000570FE File Offset: 0x000552FE
		private void case_378()
		{
			this.lexer.parsing_generic_declaration = false;
			this.yyVal = new MemberName("Item", this.GetLocation(this.yyVals[0 + this.yyTop]));
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x00057134 File Offset: 0x00055334
		private void case_379()
		{
			this.lexer.parsing_generic_declaration = false;
			this.yyVal = new MemberName("Item", null, (ATypeNameExpression)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[0 + this.yyTop]));
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x00057188 File Offset: 0x00055388
		private void case_380()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-2 + this.yyTop];
			this.yyVal = new SimpleName(locatedToken.Value, (TypeArguments)this.yyVals[-1 + this.yyTop], locatedToken.Location);
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x000571D8 File Offset: 0x000553D8
		private void case_381()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-3 + this.yyTop];
			LocatedToken locatedToken2 = (LocatedToken)this.yyVals[-2 + this.yyTop];
			this.yyVal = new QualifiedAliasMember(locatedToken.Value, locatedToken2.Value, (TypeArguments)this.yyVals[-1 + this.yyTop], locatedToken.Location);
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x00057244 File Offset: 0x00055444
		private void case_382()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-2 + this.yyTop];
			this.yyVal = new MemberAccess((ATypeNameExpression)this.yyVals[-3 + this.yyTop], locatedToken.Value, (TypeArguments)this.yyVals[-1 + this.yyTop], locatedToken.Location);
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x000572A8 File Offset: 0x000554A8
		private void case_384()
		{
			if (this.lang_version < LanguageVersion.ISO_2)
			{
				this.FeatureIsNotAvailable(this.GetLocation(this.yyVals[-2 + this.yyTop]), "generics");
			}
			this.yyVal = this.yyVals[-1 + this.yyTop];
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x000572F4 File Offset: 0x000554F4
		private void case_385()
		{
			TypeParameters typeParameters = new TypeParameters();
			typeParameters.Add((TypeParameter)this.yyVals[0 + this.yyTop]);
			this.yyVal = typeParameters;
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x00057328 File Offset: 0x00055528
		private void case_386()
		{
			TypeParameters typeParameters = (TypeParameters)this.yyVals[-2 + this.yyTop];
			typeParameters.Add((TypeParameter)this.yyVals[0 + this.yyTop]);
			this.yyVal = typeParameters;
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x0005736C File Offset: 0x0005556C
		private void case_387()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[0 + this.yyTop];
			this.yyVal = new TypeParameter(new MemberName(locatedToken.Value, locatedToken.Location), (Attributes)this.yyVals[-2 + this.yyTop], (VarianceDecl)this.yyVals[-1 + this.yyTop]);
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x000573D4 File Offset: 0x000555D4
		private void case_388()
		{
			if (CSharpParser.GetTokenName(this.yyToken) == "type")
			{
				this.report.Error(81, this.GetLocation(this.yyVals[0 + this.yyTop]), "Type parameter declaration must be an identifier not a type");
			}
			else
			{
				this.Error_SyntaxError(this.yyToken);
			}
			this.yyVal = new TypeParameter(MemberName.Null, null, null);
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x00057440 File Offset: 0x00055640
		private void case_397()
		{
			this.report.Error(1536, this.GetLocation(this.yyVals[0 + this.yyTop]), "Invalid parameter type `void'");
			this.yyVal = new TypeExpression(this.compiler.BuiltinTypes.Void, this.GetLocation(this.yyVals[0 + this.yyTop]));
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x000574A8 File Offset: 0x000556A8
		private void case_400()
		{
			if (this.yyVals[0 + this.yyTop] != null)
			{
				this.yyVal = new ComposedCast((ATypeNameExpression)this.yyVals[-1 + this.yyTop], (ComposedTypeSpecifier)this.yyVals[0 + this.yyTop]);
				return;
			}
			SimpleName simpleName = this.yyVals[-1 + this.yyTop] as SimpleName;
			if (simpleName != null && simpleName.Name == "var")
			{
				this.yyVal = new VarExpr(simpleName.Location);
				return;
			}
			this.yyVal = this.yyVals[-1 + this.yyTop];
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x0005754C File Offset: 0x0005574C
		private void case_403()
		{
			Expression.Error_VoidInvalidInTheContext(this.GetLocation(this.yyVals[0 + this.yyTop]), this.report);
			this.yyVal = new TypeExpression(this.compiler.BuiltinTypes.Void, this.GetLocation(this.yyVals[0 + this.yyTop]));
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x000575AC File Offset: 0x000557AC
		private void case_404()
		{
			if (this.yyVals[0 + this.yyTop] != null)
			{
				this.yyVal = new ComposedCast((FullNamedExpression)this.yyVals[-1 + this.yyTop], (ComposedTypeSpecifier)this.yyVals[0 + this.yyTop]);
			}
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x00057600 File Offset: 0x00055800
		private void case_407()
		{
			this.yyVal = new List<FullNamedExpression>(2)
			{
				(FullNamedExpression)this.yyVals[0 + this.yyTop]
			};
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x00057638 File Offset: 0x00055838
		private void case_408()
		{
			List<FullNamedExpression> list = (List<FullNamedExpression>)this.yyVals[-2 + this.yyTop];
			list.Add((FullNamedExpression)this.yyVals[0 + this.yyTop]);
			this.yyVal = list;
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x0005767C File Offset: 0x0005587C
		private void case_409()
		{
			if (this.yyVals[0 + this.yyTop] is ComposedCast)
			{
				this.report.Error(1521, this.GetLocation(this.yyVals[0 + this.yyTop]), "Invalid base type `{0}'", ((ComposedCast)this.yyVals[0 + this.yyTop]).GetSignatureForError());
			}
			this.yyVal = this.yyVals[0 + this.yyTop];
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x000576F8 File Offset: 0x000558F8
		private void case_448()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-1 + this.yyTop];
			this.yyVal = new CompletionSimpleName(MemberName.MakeName(locatedToken.Value, null), locatedToken.Location);
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x00057738 File Offset: 0x00055938
		private void case_457()
		{
			this.yyVal = new List<Expression>
			{
				(InterpolatedStringInsert)this.yyVals[0 + this.yyTop]
			};
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x0005776C File Offset: 0x0005596C
		private void case_458()
		{
			List<Expression> list = (List<Expression>)this.yyVals[-2 + this.yyTop];
			list.Add((StringLiteral)this.yyVals[-1 + this.yyTop]);
			list.Add((InterpolatedStringInsert)this.yyVals[0 + this.yyTop]);
			this.yyVal = list;
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x000577CA File Offset: 0x000559CA
		private void case_460()
		{
			this.yyVal = new InterpolatedStringInsert((Expression)this.yyVals[-2 + this.yyTop])
			{
				Alignment = (Expression)this.yyVals[0 + this.yyTop]
			};
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x00057808 File Offset: 0x00055A08
		private void case_462()
		{
			this.lexer.parsing_interpolation_format = false;
			this.yyVal = new InterpolatedStringInsert((Expression)this.yyVals[-3 + this.yyTop])
			{
				Format = (string)this.yyVals[0 + this.yyTop]
			};
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x0005785C File Offset: 0x00055A5C
		private void case_464()
		{
			this.lexer.parsing_interpolation_format = false;
			this.yyVal = new InterpolatedStringInsert((Expression)this.yyVals[-5 + this.yyTop])
			{
				Alignment = (Expression)this.yyVals[-3 + this.yyTop],
				Format = (string)this.yyVals[0 + this.yyTop]
			};
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x000578CA File Offset: 0x00055ACA
		private void case_469()
		{
			this.yyVal = new ParenthesizedExpression((Expression)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-2 + this.yyTop]));
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x00057904 File Offset: 0x00055B04
		private void case_471()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-1 + this.yyTop];
			this.yyVal = new MemberAccess((Expression)this.yyVals[-3 + this.yyTop], locatedToken.Value, (TypeArguments)this.yyVals[0 + this.yyTop], locatedToken.Location);
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x00057968 File Offset: 0x00055B68
		private void case_472()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-1 + this.yyTop];
			this.yyVal = new MemberAccess((Expression)this.yyVals[-3 + this.yyTop], locatedToken.Value, (int)this.yyVals[0 + this.yyTop], locatedToken.Location);
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x000579CC File Offset: 0x00055BCC
		private void case_473()
		{
			if (this.lang_version < LanguageVersion.V_6)
			{
				this.FeatureIsNotAvailable(this.GetLocation(this.yyVals[-3 + this.yyTop]), "null propagating operator");
			}
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-1 + this.yyTop];
			this.yyVal = new ConditionalMemberAccess((Expression)this.yyVals[-4 + this.yyTop], locatedToken.Value, (TypeArguments)this.yyVals[0 + this.yyTop], locatedToken.Location);
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x00057A58 File Offset: 0x00055C58
		private void case_474()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-1 + this.yyTop];
			this.yyVal = new MemberAccess((Expression)this.yyVals[-3 + this.yyTop], locatedToken.Value, (TypeArguments)this.yyVals[0 + this.yyTop], locatedToken.Location);
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x00057ABC File Offset: 0x00055CBC
		private void case_475()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-1 + this.yyTop];
			this.yyVal = new MemberAccess(new BaseThis(this.GetLocation(this.yyVals[-3 + this.yyTop])), locatedToken.Value, (TypeArguments)this.yyVals[0 + this.yyTop], locatedToken.Location);
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x00057B24 File Offset: 0x00055D24
		private void case_476()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-1 + this.yyTop];
			this.yyVal = new MemberAccess(new SimpleName("await", ((LocatedToken)this.yyVals[-3 + this.yyTop]).Location), locatedToken.Value, (TypeArguments)this.yyVals[0 + this.yyTop], locatedToken.Location);
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x00057B98 File Offset: 0x00055D98
		private void case_477()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-2 + this.yyTop];
			LocatedToken locatedToken2 = (LocatedToken)this.yyVals[-1 + this.yyTop];
			this.yyVal = new QualifiedAliasMember(locatedToken.Value, locatedToken2.Value, (TypeArguments)this.yyVals[0 + this.yyTop], locatedToken.Location);
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x00057C04 File Offset: 0x00055E04
		private void case_478()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-2 + this.yyTop];
			LocatedToken locatedToken2 = (LocatedToken)this.yyVals[-1 + this.yyTop];
			this.yyVal = new QualifiedAliasMember(locatedToken.Value, locatedToken2.Value, (int)this.yyVals[0 + this.yyTop], locatedToken.Location);
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x00057C70 File Offset: 0x00055E70
		private void case_480()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-1 + this.yyTop];
			this.yyVal = new CompletionMemberAccess((Expression)this.yyVals[-3 + this.yyTop], locatedToken.Value, locatedToken.Location);
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x00057CC0 File Offset: 0x00055EC0
		private void case_482()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-1 + this.yyTop];
			this.yyVal = new CompletionMemberAccess((Expression)this.yyVals[-3 + this.yyTop], locatedToken.Value, locatedToken.Location);
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x00057D0E File Offset: 0x00055F0E
		private void case_483()
		{
			this.yyVal = new Invocation((Expression)this.yyVals[-3 + this.yyTop], (Arguments)this.yyVals[-1 + this.yyTop]);
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x00057D44 File Offset: 0x00055F44
		private void case_484()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Invocation((Expression)this.yyVals[-3 + this.yyTop], (Arguments)this.yyVals[-1 + this.yyTop]);
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x00057D91 File Offset: 0x00055F91
		private void case_485()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Invocation((Expression)this.yyVals[-2 + this.yyTop], null);
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x00057DC0 File Offset: 0x00055FC0
		private void case_488()
		{
			if (this.yyVals[-1 + this.yyTop] == null)
			{
				this.yyVal = new CollectionOrObjectInitializers(this.GetLocation(this.yyVals[-2 + this.yyTop]));
				return;
			}
			this.yyVal = new CollectionOrObjectInitializers((List<Expression>)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-2 + this.yyTop]));
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x00057E35 File Offset: 0x00056035
		private void case_489()
		{
			this.yyVal = new CollectionOrObjectInitializers((List<Expression>)this.yyVals[-2 + this.yyTop], this.GetLocation(this.yyVals[-3 + this.yyTop]));
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x00057E70 File Offset: 0x00056070
		private void case_492()
		{
			this.yyVal = new List<Expression>
			{
				(Expression)this.yyVals[0 + this.yyTop]
			};
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x00057EA4 File Offset: 0x000560A4
		private void case_493()
		{
			List<Expression> list = (List<Expression>)this.yyVals[-2 + this.yyTop];
			list.Add((Expression)this.yyVals[0 + this.yyTop]);
			this.yyVal = list;
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x00057EE8 File Offset: 0x000560E8
		private void case_494()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = this.yyVals[-1 + this.yyTop];
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x00057F0C File Offset: 0x0005610C
		private void case_495()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-2 + this.yyTop];
			this.yyVal = new ElementInitializer(locatedToken.Value, (Expression)this.yyVals[0 + this.yyTop], locatedToken.Location);
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x00057F5C File Offset: 0x0005615C
		private void case_496()
		{
			LocatedToken locatedToken = (LocatedToken)this.Error_AwaitAsIdentifier(this.yyVals[-2 + this.yyTop]);
			this.yyVal = new ElementInitializer(locatedToken.Value, (Expression)this.yyVals[0 + this.yyTop], locatedToken.Location);
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x00057FB0 File Offset: 0x000561B0
		private void case_498()
		{
			CompletionSimpleName completionSimpleName = this.yyVals[-1 + this.yyTop] as CompletionSimpleName;
			if (completionSimpleName == null)
			{
				this.yyVal = new CollectionElementInitializer((Expression)this.yyVals[-1 + this.yyTop]);
				return;
			}
			this.yyVal = new CompletionElementInitializer(completionSimpleName.Prefix, completionSimpleName.Location);
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x0005800C File Offset: 0x0005620C
		private void case_499()
		{
			if (this.yyVals[-1 + this.yyTop] == null)
			{
				this.yyVal = new CollectionElementInitializer(this.GetLocation(this.yyVals[-2 + this.yyTop]));
				return;
			}
			this.yyVal = new CollectionElementInitializer((List<Expression>)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-2 + this.yyTop]));
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x00058084 File Offset: 0x00056284
		private void case_500()
		{
			if (this.lang_version < LanguageVersion.V_6)
			{
				this.FeatureIsNotAvailable(this.GetLocation(this.yyVals[-4 + this.yyTop]), "dictionary initializer");
			}
			this.yyVal = new DictionaryElementInitializer((Arguments)this.yyVals[-3 + this.yyTop], (Expression)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-4 + this.yyTop]));
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x00058108 File Offset: 0x00056308
		private void case_501()
		{
			this.report.Error(1920, this.GetLocation(this.yyVals[-1 + this.yyTop]), "An element initializer cannot be empty");
			this.yyVal = new CollectionElementInitializer(this.GetLocation(this.yyVals[-1 + this.yyTop]));
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x00058160 File Offset: 0x00056360
		private void case_506()
		{
			Arguments arguments = new Arguments(4);
			arguments.Add((Argument)this.yyVals[0 + this.yyTop]);
			this.yyVal = arguments;
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x00058198 File Offset: 0x00056398
		private void case_507()
		{
			Arguments arguments = (Arguments)this.yyVals[-2 + this.yyTop];
			Arguments arguments2 = arguments;
			if (arguments2[arguments2.Count - 1] is NamedArgument)
			{
				Arguments arguments3 = arguments;
				this.Error_NamedArgumentExpected((NamedArgument)arguments3[arguments3.Count - 1]);
			}
			arguments.Add((Argument)this.yyVals[0 + this.yyTop]);
			this.yyVal = arguments;
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x0005820C File Offset: 0x0005640C
		private void case_508()
		{
			Arguments arguments = (Arguments)this.yyVals[-2 + this.yyTop];
			NamedArgument namedArgument = (NamedArgument)this.yyVals[0 + this.yyTop];
			for (int i = 0; i < arguments.Count; i++)
			{
				NamedArgument namedArgument2 = arguments[i] as NamedArgument;
				if (namedArgument2 != null && namedArgument2.Name == namedArgument.Name)
				{
					this.report.Error(1740, namedArgument2.Location, "Named argument `{0}' specified multiple times", namedArgument2.Name);
				}
			}
			arguments.Add(namedArgument);
			this.yyVal = arguments;
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x000582A7 File Offset: 0x000564A7
		private void case_509()
		{
			if (this.lexer.putback_char == -1)
			{
				this.lexer.putback(41);
			}
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = this.yyVals[-2 + this.yyTop];
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x000582E6 File Offset: 0x000564E6
		private void case_510()
		{
			this.report.Error(839, this.GetLocation(this.yyVals[-1 + this.yyTop]), "An argument is missing");
			this.yyVal = null;
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x00058319 File Offset: 0x00056519
		private void case_515()
		{
			this.yyVal = new Argument((Expression)this.yyVals[0 + this.yyTop], Argument.AType.Ref);
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x0005833B File Offset: 0x0005653B
		private void case_517()
		{
			this.yyVal = new Argument((Expression)this.yyVals[0 + this.yyTop], Argument.AType.Out);
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x0005835D File Offset: 0x0005655D
		private void case_519()
		{
			this.yyVal = new Argument(new Arglist((Arguments)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-3 + this.yyTop])));
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x00058399 File Offset: 0x00056599
		private void case_520()
		{
			this.yyVal = new Argument(new Arglist(this.GetLocation(this.yyVals[-2 + this.yyTop])));
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x000578CA File Offset: 0x00055ACA
		private void case_521()
		{
			this.yyVal = new ParenthesizedExpression((Expression)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-2 + this.yyTop]));
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x000583C4 File Offset: 0x000565C4
		private void case_522()
		{
			if (this.lang_version != LanguageVersion.Experimental)
			{
				this.FeatureIsNotAvailable(this.GetLocation(this.yyVals[-1 + this.yyTop]), "declaration expression");
			}
			LocatedToken locatedToken = (LocatedToken)this.yyVals[0 + this.yyTop];
			LocalVariable localVariable = new LocalVariable(this.current_block, locatedToken.Value, locatedToken.Location);
			this.current_block.AddLocalName(localVariable);
			this.yyVal = new DeclarationExpression((FullNamedExpression)this.yyVals[-1 + this.yyTop], localVariable);
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x00058454 File Offset: 0x00056654
		private void case_523()
		{
			if (this.lang_version != LanguageVersion.Experimental)
			{
				this.FeatureIsNotAvailable(this.GetLocation(this.yyVals[-3 + this.yyTop]), "declaration expression");
			}
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-2 + this.yyTop];
			LocalVariable localVariable = new LocalVariable(this.current_block, locatedToken.Value, locatedToken.Location);
			this.current_block.AddLocalName(localVariable);
			this.yyVal = new DeclarationExpression((FullNamedExpression)this.yyVals[-3 + this.yyTop], localVariable)
			{
				Initializer = (Expression)this.yyVals[0 + this.yyTop]
			};
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x00058504 File Offset: 0x00056704
		private void case_525()
		{
			this.yyVal = new ElementAccess((Expression)this.yyVals[-3 + this.yyTop], (Arguments)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-2 + this.yyTop]));
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x0005855C File Offset: 0x0005675C
		private void case_526()
		{
			if (this.lang_version < LanguageVersion.V_6)
			{
				this.FeatureIsNotAvailable(this.GetLocation(this.yyVals[-3 + this.yyTop]), "null propagating operator");
			}
			this.yyVal = new ElementAccess((Expression)this.yyVals[-4 + this.yyTop], (Arguments)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-2 + this.yyTop]))
			{
				ConditionalAccess = true
			};
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x000585E4 File Offset: 0x000567E4
		private void case_527()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new ElementAccess((Expression)this.yyVals[-3 + this.yyTop], (Arguments)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-2 + this.yyTop]));
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x00058648 File Offset: 0x00056848
		private void case_528()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new ElementAccess((Expression)this.yyVals[-2 + this.yyTop], null, this.GetLocation(this.yyVals[-1 + this.yyTop]));
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x00058698 File Offset: 0x00056898
		private void case_529()
		{
			this.yyVal = new List<Expression>(4)
			{
				(Expression)this.yyVals[0 + this.yyTop]
			};
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x000586D0 File Offset: 0x000568D0
		private void case_530()
		{
			List<Expression> list = (List<Expression>)this.yyVals[-2 + this.yyTop];
			list.Add((Expression)this.yyVals[0 + this.yyTop]);
			this.yyVal = list;
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x00058714 File Offset: 0x00056914
		private void case_531()
		{
			Arguments arguments = new Arguments(4);
			arguments.Add((Argument)this.yyVals[0 + this.yyTop]);
			this.yyVal = arguments;
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x0005874C File Offset: 0x0005694C
		private void case_532()
		{
			Arguments arguments = (Arguments)this.yyVals[-2 + this.yyTop];
			Arguments arguments2 = arguments;
			if (arguments2[arguments2.Count - 1] is NamedArgument && !(this.yyVals[0 + this.yyTop] is NamedArgument))
			{
				Arguments arguments3 = arguments;
				this.Error_NamedArgumentExpected((NamedArgument)arguments3[arguments3.Count - 1]);
			}
			arguments.Add((Argument)this.yyVals[0 + this.yyTop]);
			this.yyVal = arguments;
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x000587D4 File Offset: 0x000569D4
		private void case_536()
		{
			this.yyVal = new ElementAccess(new BaseThis(this.GetLocation(this.yyVals[-3 + this.yyTop])), (Arguments)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-2 + this.yyTop]));
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x00058831 File Offset: 0x00056A31
		private void case_537()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new ElementAccess(null, null, this.GetLocation(this.yyVals[-1 + this.yyTop]));
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x00058864 File Offset: 0x00056A64
		private void case_540()
		{
			if (this.yyVals[0 + this.yyTop] != null)
			{
				if (this.lang_version <= LanguageVersion.ISO_2)
				{
					this.FeatureIsNotAvailable(this.GetLocation(this.yyVals[-5 + this.yyTop]), "object initializers");
				}
				this.yyVal = new NewInitialize((FullNamedExpression)this.yyVals[-4 + this.yyTop], (Arguments)this.yyVals[-2 + this.yyTop], (CollectionOrObjectInitializers)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-5 + this.yyTop]));
				return;
			}
			this.yyVal = new New((FullNamedExpression)this.yyVals[-4 + this.yyTop], (Arguments)this.yyVals[-2 + this.yyTop], this.GetLocation(this.yyVals[-5 + this.yyTop]));
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x0005895C File Offset: 0x00056B5C
		private void case_541()
		{
			if (this.lang_version <= LanguageVersion.ISO_2)
			{
				this.FeatureIsNotAvailable(this.GetLocation(this.yyVals[-2 + this.yyTop]), "collection initializers");
			}
			this.yyVal = new NewInitialize((FullNamedExpression)this.yyVals[-1 + this.yyTop], null, (CollectionOrObjectInitializers)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-2 + this.yyTop]));
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x000589E0 File Offset: 0x00056BE0
		private void case_542()
		{
			this.yyVal = new ArrayCreation((FullNamedExpression)this.yyVals[-5 + this.yyTop], (List<Expression>)this.yyVals[-3 + this.yyTop], new ComposedTypeSpecifier(((List<Expression>)this.yyVals[-3 + this.yyTop]).Count, this.GetLocation(this.yyVals[-4 + this.yyTop]))
			{
				Next = (ComposedTypeSpecifier)this.yyVals[-1 + this.yyTop]
			}, (ArrayInitializer)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-6 + this.yyTop]));
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x00058A9C File Offset: 0x00056C9C
		private void case_543()
		{
			if (this.yyVals[0 + this.yyTop] == null)
			{
				this.report.Error(1586, this.GetLocation(this.yyVals[-3 + this.yyTop]), "Array creation must have array size or array initializer");
			}
			this.yyVal = new ArrayCreation((FullNamedExpression)this.yyVals[-2 + this.yyTop], (ComposedTypeSpecifier)this.yyVals[-1 + this.yyTop], (ArrayInitializer)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-3 + this.yyTop]));
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x00058B44 File Offset: 0x00056D44
		private void case_544()
		{
			if (this.lang_version <= LanguageVersion.ISO_2)
			{
				this.FeatureIsNotAvailable(this.GetLocation(this.yyVals[-2 + this.yyTop]), "implicitly typed arrays");
			}
			this.yyVal = new ImplicitlyTypedArrayCreation((ComposedTypeSpecifier)this.yyVals[-1 + this.yyTop], (ArrayInitializer)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-2 + this.yyTop]));
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x00058BC4 File Offset: 0x00056DC4
		private void case_545()
		{
			this.report.Error(178, this.GetLocation(this.yyVals[-1 + this.yyTop]), "Invalid rank specifier, expecting `,' or `]'");
			this.yyVal = new ArrayCreation((FullNamedExpression)this.yyVals[-5 + this.yyTop], null, this.GetLocation(this.yyVals[-6 + this.yyTop]));
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x00058C34 File Offset: 0x00056E34
		private void case_546()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new New((FullNamedExpression)this.yyVals[-1 + this.yyTop], null, this.GetLocation(this.yyVals[-2 + this.yyTop]));
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x00058C83 File Offset: 0x00056E83
		private void case_548()
		{
			this.lexer.parsing_type--;
			this.yyVal = this.yyVals[0 + this.yyTop];
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x00058CB0 File Offset: 0x00056EB0
		private void case_549()
		{
			if (this.lang_version <= LanguageVersion.ISO_2)
			{
				this.FeatureIsNotAvailable(this.GetLocation(this.yyVals[-3 + this.yyTop]), "anonymous types");
			}
			this.yyVal = new NewAnonymousType((List<AnonymousTypeParameter>)this.yyVals[-1 + this.yyTop], this.current_container, this.GetLocation(this.yyVals[-3 + this.yyTop]));
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x00058D24 File Offset: 0x00056F24
		private void case_555()
		{
			this.yyVal = new List<AnonymousTypeParameter>(4)
			{
				(AnonymousTypeParameter)this.yyVals[0 + this.yyTop]
			};
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x00058D5C File Offset: 0x00056F5C
		private void case_556()
		{
			List<AnonymousTypeParameter> list = (List<AnonymousTypeParameter>)this.yyVals[-2 + this.yyTop];
			list.Add((AnonymousTypeParameter)this.yyVals[0 + this.yyTop]);
			this.yyVal = list;
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x00058DA0 File Offset: 0x00056FA0
		private void case_559()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-2 + this.yyTop];
			this.yyVal = new AnonymousTypeParameter((Expression)this.yyVals[0 + this.yyTop], locatedToken.Value, locatedToken.Location);
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x00058DF0 File Offset: 0x00056FF0
		private void case_560()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[0 + this.yyTop];
			this.yyVal = new AnonymousTypeParameter(new SimpleName(locatedToken.Value, locatedToken.Location), locatedToken.Value, locatedToken.Location);
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x00058E3C File Offset: 0x0005703C
		private void case_561()
		{
			MemberAccess memberAccess = (MemberAccess)this.yyVals[0 + this.yyTop];
			this.yyVal = new AnonymousTypeParameter(memberAccess, memberAccess.Name, memberAccess.Location);
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x00058E76 File Offset: 0x00057076
		private void case_562()
		{
			this.report.Error(746, this.lexer.Location, "Invalid anonymous type member declarator. Anonymous type members must be a member assignment, simple name or member access expression");
			this.yyVal = null;
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x00058EA0 File Offset: 0x000570A0
		private void case_566()
		{
			((ComposedTypeSpecifier)this.yyVals[-1 + this.yyTop]).Next = (ComposedTypeSpecifier)this.yyVals[0 + this.yyTop];
			this.yyVal = this.yyVals[-1 + this.yyTop];
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x00058EEF File Offset: 0x000570EF
		private void case_567()
		{
			this.yyVal = ComposedTypeSpecifier.CreateArrayDimension(1, this.GetLocation(this.yyVals[-1 + this.yyTop]));
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x00058F12 File Offset: 0x00057112
		private void case_568()
		{
			this.yyVal = ComposedTypeSpecifier.CreateArrayDimension((int)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-2 + this.yyTop]));
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x00058F4C File Offset: 0x0005714C
		private void case_573()
		{
			this.yyVal = new ArrayInitializer(0, this.GetLocation(this.yyVals[-1 + this.yyTop]))
			{
				VariableDeclaration = this.current_variable
			};
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x00058F88 File Offset: 0x00057188
		private void case_574()
		{
			ArrayInitializer arrayInitializer = new ArrayInitializer((List<Expression>)this.yyVals[-2 + this.yyTop], this.GetLocation(this.yyVals[-3 + this.yyTop]));
			arrayInitializer.VariableDeclaration = this.current_variable;
			object obj = this.yyVals[-1 + this.yyTop];
			this.yyVal = arrayInitializer;
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x00058FEC File Offset: 0x000571EC
		private void case_575()
		{
			this.yyVal = new List<Expression>(4)
			{
				(Expression)this.yyVals[0 + this.yyTop]
			};
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x00059024 File Offset: 0x00057224
		private void case_576()
		{
			List<Expression> list = (List<Expression>)this.yyVals[-2 + this.yyTop];
			list.Add((Expression)this.yyVals[0 + this.yyTop]);
			this.yyVal = list;
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x00059068 File Offset: 0x00057268
		private void case_577()
		{
			this.yyVal = new TypeOf((FullNamedExpression)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-3 + this.yyTop]));
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x0005909F File Offset: 0x0005729F
		private void case_579()
		{
			this.Error_TypeExpected(this.lexer.Location);
			this.yyVal = null;
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x000590B9 File Offset: 0x000572B9
		private void case_580()
		{
			if (this.lang_version < LanguageVersion.ISO_2)
			{
				this.FeatureIsNotAvailable(this.GetLocation(this.yyVals[0 + this.yyTop]), "generics");
			}
			this.yyVal = this.yyVals[0 + this.yyTop];
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x000590FC File Offset: 0x000572FC
		private void case_581()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-1 + this.yyTop];
			if (this.lang_version == LanguageVersion.ISO_1)
			{
				this.FeatureIsNotAvailable(locatedToken.Location, "namespace alias qualifier");
			}
			this.yyVal = locatedToken;
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x0005913F File Offset: 0x0005733F
		private void case_582()
		{
			this.yyVal = new SizeOf((Expression)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-3 + this.yyTop]));
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x00059178 File Offset: 0x00057378
		private void case_583()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new SizeOf((Expression)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-3 + this.yyTop]));
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x000591C6 File Offset: 0x000573C6
		private void case_584()
		{
			this.yyVal = new CheckedExpr((Expression)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-3 + this.yyTop]));
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x000591FD File Offset: 0x000573FD
		private void case_585()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new CheckedExpr(null, this.GetLocation(this.yyVals[-1 + this.yyTop]));
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x0005922C File Offset: 0x0005742C
		private void case_586()
		{
			this.yyVal = new UnCheckedExpr((Expression)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-3 + this.yyTop]));
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x00059263 File Offset: 0x00057463
		private void case_587()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new UnCheckedExpr(null, this.GetLocation(this.yyVals[-1 + this.yyTop]));
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x00059294 File Offset: 0x00057494
		private void case_588()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-1 + this.yyTop];
			this.yyVal = new MemberAccess(new Indirection((Expression)this.yyVals[-3 + this.yyTop], this.GetLocation(this.yyVals[-2 + this.yyTop])), locatedToken.Value, (TypeArguments)this.yyVals[0 + this.yyTop], locatedToken.Location);
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x00059311 File Offset: 0x00057511
		private void case_596()
		{
			this.valid_param_mod = (CSharpParser.ParameterModifierType)0;
			this.yyVal = this.yyVals[-1 + this.yyTop];
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x00059330 File Offset: 0x00057530
		private void case_597()
		{
			if (this.lang_version < LanguageVersion.ISO_2)
			{
				this.FeatureIsNotAvailable(this.GetLocation(this.yyVals[-3 + this.yyTop]), "default value expression");
			}
			this.yyVal = new DefaultValueExpression((Expression)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-3 + this.yyTop]));
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x0005939C File Offset: 0x0005759C
		private void case_601()
		{
			this.yyVal = new Cast((FullNamedExpression)this.yyVals[-2 + this.yyTop], (Expression)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-3 + this.yyTop]));
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x000593F4 File Offset: 0x000575F4
		private void case_602()
		{
			if (!this.async_block)
			{
				if (this.current_anonymous_method is LambdaExpression)
				{
					this.report.Error(4034, this.GetLocation(this.yyVals[-1 + this.yyTop]), "The `await' operator can only be used when its containing lambda expression is marked with the `async' modifier");
				}
				else if (this.current_anonymous_method != null)
				{
					this.report.Error(4035, this.GetLocation(this.yyVals[-1 + this.yyTop]), "The `await' operator can only be used when its containing anonymous method is marked with the `async' modifier");
				}
				else if (this.interactive_async != null)
				{
					this.current_block.Explicit.RegisterAsyncAwait();
					this.interactive_async = new bool?(true);
				}
				else
				{
					this.report.Error(4033, this.GetLocation(this.yyVals[-1 + this.yyTop]), "The `await' operator can only be used when its containing method is marked with the `async' modifier");
				}
			}
			else
			{
				this.current_block.Explicit.RegisterAsyncAwait();
			}
			this.yyVal = new Await((Expression)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-1 + this.yyTop]));
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x00059517 File Offset: 0x00057717
		private void case_603()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Unary(Unary.Operator.LogicalNot, null, this.GetLocation(this.yyVals[-1 + this.yyTop]));
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x00059547 File Offset: 0x00057747
		private void case_604()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Unary(Unary.Operator.OnesComplement, null, this.GetLocation(this.yyVals[-1 + this.yyTop]));
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x00059578 File Offset: 0x00057778
		private void case_605()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Cast((FullNamedExpression)this.yyVals[-2 + this.yyTop], null, this.GetLocation(this.yyVals[-3 + this.yyTop]));
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x000595C8 File Offset: 0x000577C8
		private void case_606()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Await(null, this.GetLocation(this.yyVals[-1 + this.yyTop]));
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x000595F7 File Offset: 0x000577F7
		private void case_614()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Unary(Unary.Operator.UnaryPlus, null, this.GetLocation(this.yyVals[-1 + this.yyTop]));
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x00059627 File Offset: 0x00057827
		private void case_615()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Unary(Unary.Operator.UnaryNegation, null, this.GetLocation(this.yyVals[-1 + this.yyTop]));
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x00059657 File Offset: 0x00057857
		private void case_616()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new UnaryMutator(UnaryMutator.Mode.IsIncrement, null, this.GetLocation(this.yyVals[-1 + this.yyTop]));
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x00059687 File Offset: 0x00057887
		private void case_617()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new UnaryMutator(UnaryMutator.Mode.IsDecrement, null, this.GetLocation(this.yyVals[-1 + this.yyTop]));
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x000596B7 File Offset: 0x000578B7
		private void case_618()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Indirection(null, this.GetLocation(this.yyVals[-1 + this.yyTop]));
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x000596E6 File Offset: 0x000578E6
		private void case_619()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Unary(Unary.Operator.AddressOf, null, this.GetLocation(this.yyVals[-1 + this.yyTop]));
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x00059716 File Offset: 0x00057916
		private void case_621()
		{
			this.yyVal = new Binary(Binary.Operator.Multiply, (Expression)this.yyVals[-2 + this.yyTop], (Expression)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x0005974E File Offset: 0x0005794E
		private void case_622()
		{
			this.yyVal = new Binary(Binary.Operator.Division, (Expression)this.yyVals[-2 + this.yyTop], (Expression)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x00059786 File Offset: 0x00057986
		private void case_623()
		{
			this.yyVal = new Binary(Binary.Operator.Modulus, (Expression)this.yyVals[-2 + this.yyTop], (Expression)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x000597BE File Offset: 0x000579BE
		private void case_624()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Binary(Binary.Operator.Multiply, (Expression)this.yyVals[-2 + this.yyTop], null);
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x000597EF File Offset: 0x000579EF
		private void case_625()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Binary(Binary.Operator.Division, (Expression)this.yyVals[-2 + this.yyTop], null);
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x00059820 File Offset: 0x00057A20
		private void case_626()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Binary(Binary.Operator.Modulus, (Expression)this.yyVals[-2 + this.yyTop], null);
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x00059851 File Offset: 0x00057A51
		private void case_628()
		{
			this.yyVal = new Binary(Binary.Operator.Addition, (Expression)this.yyVals[-2 + this.yyTop], (Expression)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x0005988C File Offset: 0x00057A8C
		private void case_629()
		{
			this.yyVal = new Binary(Binary.Operator.Subtraction, (Expression)this.yyVals[-2 + this.yyTop], (Expression)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x000598C7 File Offset: 0x00057AC7
		private void case_630()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Binary(Binary.Operator.Addition, (Expression)this.yyVals[-2 + this.yyTop], null);
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x000598FB File Offset: 0x00057AFB
		private void case_631()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Binary(Binary.Operator.Subtraction, (Expression)this.yyVals[-2 + this.yyTop], null);
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x00059930 File Offset: 0x00057B30
		private void case_633()
		{
			Is @is = new Is((Expression)this.yyVals[-3 + this.yyTop], (Expression)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-2 + this.yyTop]));
			if (this.yyVals[0 + this.yyTop] != null)
			{
				if (this.lang_version != LanguageVersion.Experimental)
				{
					this.FeatureIsNotAvailable(this.GetLocation(this.yyVals[0 + this.yyTop]), "type pattern matching");
				}
				LocatedToken locatedToken = (LocatedToken)this.yyVals[0 + this.yyTop];
				@is.Variable = new LocalVariable(this.current_block, locatedToken.Value, locatedToken.Location);
				this.current_block.AddLocalName(@is.Variable);
			}
			this.yyVal = @is;
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x00059A08 File Offset: 0x00057C08
		private void case_634()
		{
			Is @is = new Is((Expression)this.yyVals[-2 + this.yyTop], (Expression)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-1 + this.yyTop]));
			if (this.lang_version != LanguageVersion.Experimental)
			{
				this.FeatureIsNotAvailable(this.GetLocation(this.yyVals[-1 + this.yyTop]), "pattern matching");
			}
			this.yyVal = @is;
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x00059A8C File Offset: 0x00057C8C
		private void case_635()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new As((Expression)this.yyVals[-2 + this.yyTop], null, this.GetLocation(this.yyVals[-1 + this.yyTop]));
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x00059ADC File Offset: 0x00057CDC
		private void case_636()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Is((Expression)this.yyVals[-2 + this.yyTop], null, this.GetLocation(this.yyVals[-1 + this.yyTop]));
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x00059B2C File Offset: 0x00057D2C
		private void case_637()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-2 + this.yyTop];
			this.yyVal = new Is(new SimpleName(locatedToken.Value, locatedToken.Location), (Expression)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-1 + this.yyTop]));
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x00059B94 File Offset: 0x00057D94
		private void case_638()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-2 + this.yyTop];
			this.yyVal = new As(new SimpleName(locatedToken.Value, locatedToken.Location), (Expression)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-1 + this.yyTop]));
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x00059BFC File Offset: 0x00057DFC
		private void case_645()
		{
			this.yyVal = new Cast((FullNamedExpression)this.yyVals[-2 + this.yyTop], (Expression)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-3 + this.yyTop]));
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x00059C54 File Offset: 0x00057E54
		private void case_651()
		{
			this.yyVal = new List<PropertyPatternMember>
			{
				(PropertyPatternMember)this.yyVals[0 + this.yyTop]
			};
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x00059C88 File Offset: 0x00057E88
		private void case_652()
		{
			List<PropertyPatternMember> list = (List<PropertyPatternMember>)this.yyVals[-2 + this.yyTop];
			list.Add((PropertyPatternMember)this.yyVals[0 + this.yyTop]);
			this.yyVal = list;
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x00059CCC File Offset: 0x00057ECC
		private void case_653()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-2 + this.yyTop];
			this.yyVal = new PropertyPatternMember(locatedToken.Value, (Expression)this.yyVals[0 + this.yyTop], locatedToken.Location);
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x00059D1C File Offset: 0x00057F1C
		private void case_655()
		{
			if (this.yyVals[0 + this.yyTop] != null)
			{
				LocatedToken locatedToken = (LocatedToken)this.yyVals[0 + this.yyTop];
				LocalVariable li = new LocalVariable(this.current_block, locatedToken.Value, locatedToken.Location);
				this.current_block.AddLocalName(li);
			}
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x00059D74 File Offset: 0x00057F74
		private void case_658()
		{
			Arguments arguments = new Arguments(4);
			arguments.Add((Argument)this.yyVals[0 + this.yyTop]);
			this.yyVal = arguments;
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x00059DAC File Offset: 0x00057FAC
		private void case_659()
		{
			Arguments arguments = (Arguments)this.yyVals[-2 + this.yyTop];
			Arguments arguments2 = arguments;
			if (arguments2[arguments2.Count - 1] is NamedArgument && !(this.yyVals[0 + this.yyTop] is NamedArgument))
			{
				Arguments arguments3 = arguments;
				this.Error_NamedArgumentExpected((NamedArgument)arguments3[arguments3.Count - 1]);
			}
			arguments.Add((Argument)this.yyVals[0 + this.yyTop]);
			this.yyVal = arguments;
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x00059E34 File Offset: 0x00058034
		private void case_661()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-2 + this.yyTop];
			this.yyVal = new NamedArgument(locatedToken.Value, locatedToken.Location, (Expression)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x00059E82 File Offset: 0x00058082
		private void case_663()
		{
			this.yyVal = new Binary(Binary.Operator.LeftShift, (Expression)this.yyVals[-2 + this.yyTop], (Expression)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x00059EBA File Offset: 0x000580BA
		private void case_664()
		{
			this.yyVal = new Binary(Binary.Operator.RightShift, (Expression)this.yyVals[-2 + this.yyTop], (Expression)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x00059EF2 File Offset: 0x000580F2
		private void case_665()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Binary(Binary.Operator.LeftShift, (Expression)this.yyVals[-2 + this.yyTop], null);
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x00059F23 File Offset: 0x00058123
		private void case_666()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Binary(Binary.Operator.RightShift, (Expression)this.yyVals[-2 + this.yyTop], null);
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x00059F54 File Offset: 0x00058154
		private void case_668()
		{
			this.yyVal = new Binary(Binary.Operator.LessThan, (Expression)this.yyVals[-2 + this.yyTop], (Expression)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x00059F8F File Offset: 0x0005818F
		private void case_669()
		{
			this.yyVal = new Binary(Binary.Operator.GreaterThan, (Expression)this.yyVals[-2 + this.yyTop], (Expression)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x00059FCA File Offset: 0x000581CA
		private void case_670()
		{
			this.yyVal = new Binary(Binary.Operator.LessThanOrEqual, (Expression)this.yyVals[-2 + this.yyTop], (Expression)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x0005A005 File Offset: 0x00058205
		private void case_671()
		{
			this.yyVal = new Binary(Binary.Operator.GreaterThanOrEqual, (Expression)this.yyVals[-2 + this.yyTop], (Expression)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x0005A040 File Offset: 0x00058240
		private void case_672()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Binary(Binary.Operator.LessThan, (Expression)this.yyVals[-2 + this.yyTop], null);
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x0005A074 File Offset: 0x00058274
		private void case_673()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Binary(Binary.Operator.GreaterThan, (Expression)this.yyVals[-2 + this.yyTop], null);
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x0005A0A8 File Offset: 0x000582A8
		private void case_674()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Binary(Binary.Operator.LessThanOrEqual, (Expression)this.yyVals[-2 + this.yyTop], null);
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x0005A0DC File Offset: 0x000582DC
		private void case_675()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Binary(Binary.Operator.GreaterThanOrEqual, (Expression)this.yyVals[-2 + this.yyTop], null);
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x0005A110 File Offset: 0x00058310
		private void case_677()
		{
			this.yyVal = new Binary(Binary.Operator.Equality, (Expression)this.yyVals[-2 + this.yyTop], (Expression)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x0005A14B File Offset: 0x0005834B
		private void case_678()
		{
			this.yyVal = new Binary(Binary.Operator.Inequality, (Expression)this.yyVals[-2 + this.yyTop], (Expression)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x0005A186 File Offset: 0x00058386
		private void case_679()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Binary(Binary.Operator.Equality, (Expression)this.yyVals[-2 + this.yyTop], null);
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x0005A1BA File Offset: 0x000583BA
		private void case_680()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Binary(Binary.Operator.Inequality, (Expression)this.yyVals[-2 + this.yyTop], null);
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x0005A1EE File Offset: 0x000583EE
		private void case_682()
		{
			this.yyVal = new Binary(Binary.Operator.BitwiseAnd, (Expression)this.yyVals[-2 + this.yyTop], (Expression)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x0005A229 File Offset: 0x00058429
		private void case_683()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Binary(Binary.Operator.BitwiseAnd, (Expression)this.yyVals[-2 + this.yyTop], null);
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x0005A25D File Offset: 0x0005845D
		private void case_685()
		{
			this.yyVal = new Binary(Binary.Operator.ExclusiveOr, (Expression)this.yyVals[-2 + this.yyTop], (Expression)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x0005A298 File Offset: 0x00058498
		private void case_686()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Binary(Binary.Operator.ExclusiveOr, (Expression)this.yyVals[-2 + this.yyTop], null);
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x0005A2CC File Offset: 0x000584CC
		private void case_688()
		{
			this.yyVal = new Binary(Binary.Operator.BitwiseOr, (Expression)this.yyVals[-2 + this.yyTop], (Expression)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x0005A307 File Offset: 0x00058507
		private void case_689()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Binary(Binary.Operator.BitwiseOr, (Expression)this.yyVals[-2 + this.yyTop], null);
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x0005A33B File Offset: 0x0005853B
		private void case_691()
		{
			this.yyVal = new Binary(Binary.Operator.LogicalAnd, (Expression)this.yyVals[-2 + this.yyTop], (Expression)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x0005A376 File Offset: 0x00058576
		private void case_692()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Binary(Binary.Operator.LogicalAnd, (Expression)this.yyVals[-2 + this.yyTop], null);
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x0005A3AA File Offset: 0x000585AA
		private void case_694()
		{
			this.yyVal = new Binary(Binary.Operator.LogicalOr, (Expression)this.yyVals[-2 + this.yyTop], (Expression)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x0005A3E5 File Offset: 0x000585E5
		private void case_695()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Binary(Binary.Operator.LogicalOr, (Expression)this.yyVals[-2 + this.yyTop], null);
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x0005A41C File Offset: 0x0005861C
		private void case_697()
		{
			if (this.lang_version < LanguageVersion.ISO_2)
			{
				this.FeatureIsNotAvailable(this.GetLocation(this.yyVals[-1 + this.yyTop]), "null coalescing operator");
			}
			this.yyVal = new NullCoalescingOperator((Expression)this.yyVals[-2 + this.yyTop], (Expression)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x0005A488 File Offset: 0x00058688
		private void case_699()
		{
			this.yyVal = new Conditional(new BooleanExpression((Expression)this.yyVals[-4 + this.yyTop]), (Expression)this.yyVals[-2 + this.yyTop], (Expression)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-3 + this.yyTop]));
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x0005A4FC File Offset: 0x000586FC
		private void case_700()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Conditional(new BooleanExpression((Expression)this.yyVals[-3 + this.yyTop]), (Expression)this.yyVals[-1 + this.yyTop], null, this.GetLocation(this.yyVals[-2 + this.yyTop]));
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x0005A568 File Offset: 0x00058768
		private void case_701()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Conditional(new BooleanExpression((Expression)this.yyVals[-4 + this.yyTop]), (Expression)this.yyVals[-2 + this.yyTop], null, this.GetLocation(this.yyVals[-3 + this.yyTop]));
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x0005A5D4 File Offset: 0x000587D4
		private void case_702()
		{
			this.Error_SyntaxError(372);
			this.yyVal = new Conditional(new BooleanExpression((Expression)this.yyVals[-4 + this.yyTop]), (Expression)this.yyVals[-2 + this.yyTop], null, this.GetLocation(this.yyVals[-3 + this.yyTop]));
			this.lexer.putback(125);
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x0005A64A File Offset: 0x0005884A
		private void case_703()
		{
			this.yyVal = new SimpleAssign((Expression)this.yyVals[-2 + this.yyTop], (Expression)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x0005A680 File Offset: 0x00058880
		private void case_704()
		{
			this.yyVal = new CompoundAssign(Binary.Operator.Multiply, (Expression)this.yyVals[-2 + this.yyTop], (Expression)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x0005A6B8 File Offset: 0x000588B8
		private void case_705()
		{
			this.yyVal = new CompoundAssign(Binary.Operator.Division, (Expression)this.yyVals[-2 + this.yyTop], (Expression)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x0005A6F0 File Offset: 0x000588F0
		private void case_706()
		{
			this.yyVal = new CompoundAssign(Binary.Operator.Modulus, (Expression)this.yyVals[-2 + this.yyTop], (Expression)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x0005A728 File Offset: 0x00058928
		private void case_707()
		{
			this.yyVal = new CompoundAssign(Binary.Operator.Addition, (Expression)this.yyVals[-2 + this.yyTop], (Expression)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x0005A763 File Offset: 0x00058963
		private void case_708()
		{
			this.yyVal = new CompoundAssign(Binary.Operator.Subtraction, (Expression)this.yyVals[-2 + this.yyTop], (Expression)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x0005A79E File Offset: 0x0005899E
		private void case_709()
		{
			this.yyVal = new CompoundAssign(Binary.Operator.LeftShift, (Expression)this.yyVals[-2 + this.yyTop], (Expression)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x0005A7D6 File Offset: 0x000589D6
		private void case_710()
		{
			this.yyVal = new CompoundAssign(Binary.Operator.RightShift, (Expression)this.yyVals[-2 + this.yyTop], (Expression)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x0005A80E File Offset: 0x00058A0E
		private void case_711()
		{
			this.yyVal = new CompoundAssign(Binary.Operator.BitwiseAnd, (Expression)this.yyVals[-2 + this.yyTop], (Expression)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x0005A849 File Offset: 0x00058A49
		private void case_712()
		{
			this.yyVal = new CompoundAssign(Binary.Operator.BitwiseOr, (Expression)this.yyVals[-2 + this.yyTop], (Expression)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x0005A884 File Offset: 0x00058A84
		private void case_713()
		{
			this.yyVal = new CompoundAssign(Binary.Operator.ExclusiveOr, (Expression)this.yyVals[-2 + this.yyTop], (Expression)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x0005A8C0 File Offset: 0x00058AC0
		private void case_714()
		{
			this.yyVal = new List<Parameter>(4)
			{
				(Parameter)this.yyVals[0 + this.yyTop]
			};
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x0005A8F8 File Offset: 0x00058AF8
		private void case_715()
		{
			List<Parameter> list = (List<Parameter>)this.yyVals[-2 + this.yyTop];
			Parameter parameter = (Parameter)this.yyVals[0 + this.yyTop];
			if (list[0].GetType() != parameter.GetType())
			{
				this.report.Error(748, parameter.Location, "All lambda parameters must be typed either explicitly or implicitly");
			}
			list.Add(parameter);
			this.yyVal = list;
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x0005A970 File Offset: 0x00058B70
		private void case_716()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[0 + this.yyTop];
			this.yyVal = new Parameter((FullNamedExpression)this.yyVals[-1 + this.yyTop], locatedToken.Value, (Parameter.Modifier)this.yyVals[-2 + this.yyTop], null, locatedToken.Location);
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x0005A9D4 File Offset: 0x00058BD4
		private void case_717()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[0 + this.yyTop];
			this.yyVal = new Parameter((FullNamedExpression)this.yyVals[-1 + this.yyTop], locatedToken.Value, Parameter.Modifier.NONE, null, locatedToken.Location);
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x0005AA24 File Offset: 0x00058C24
		private void case_718()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[0 + this.yyTop];
			this.yyVal = new ImplicitLambdaParameter(locatedToken.Value, locatedToken.Location);
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x0005AA60 File Offset: 0x00058C60
		private void case_719()
		{
			LocatedToken locatedToken = (LocatedToken)this.Error_AwaitAsIdentifier(this.yyVals[0 + this.yyTop]);
			this.yyVal = new ImplicitLambdaParameter(locatedToken.Value, locatedToken.Location);
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x0005AAA0 File Offset: 0x00058CA0
		private void case_721()
		{
			List<Parameter> list = (List<Parameter>)this.yyVals[0 + this.yyTop];
			this.yyVal = new ParametersCompiled(list.ToArray());
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x0005AAD4 File Offset: 0x00058CD4
		private void case_723()
		{
			Block block = this.end_block(Location.Null);
			block.IsCompilerGenerated = true;
			block.AddStatement(new ContextualReturn((Expression)this.yyVals[0 + this.yyTop]));
			this.yyVal = block;
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x0005AB1A File Offset: 0x00058D1A
		private void case_725()
		{
			this.end_block(Location.Null).IsCompilerGenerated = true;
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = null;
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x00052490 File Offset: 0x00050690
		private void case_727()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = null;
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x0005AB40 File Offset: 0x00058D40
		private void case_728()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-1 + this.yyTop];
			Parameter parameter = new ImplicitLambdaParameter(locatedToken.Value, locatedToken.Location);
			this.start_anonymous(true, new ParametersCompiled(new Parameter[]
			{
				parameter
			}), false, locatedToken.Location);
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x0005AB91 File Offset: 0x00058D91
		private void case_729()
		{
			this.yyVal = this.end_anonymous((ParametersBlock)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x0005ABB4 File Offset: 0x00058DB4
		private void case_730()
		{
			LocatedToken locatedToken = (LocatedToken)this.Error_AwaitAsIdentifier(this.yyVals[-1 + this.yyTop]);
			Parameter parameter = new ImplicitLambdaParameter(locatedToken.Value, locatedToken.Location);
			this.start_anonymous(true, new ParametersCompiled(new Parameter[]
			{
				parameter
			}), false, locatedToken.Location);
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x0005AB91 File Offset: 0x00058D91
		private void case_731()
		{
			this.yyVal = this.end_anonymous((ParametersBlock)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x0005AC0C File Offset: 0x00058E0C
		private void case_732()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-1 + this.yyTop];
			Parameter parameter = new ImplicitLambdaParameter(locatedToken.Value, locatedToken.Location);
			this.start_anonymous(true, new ParametersCompiled(new Parameter[]
			{
				parameter
			}), true, locatedToken.Location);
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x0005AB91 File Offset: 0x00058D91
		private void case_733()
		{
			this.yyVal = this.end_anonymous((ParametersBlock)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x0005AC5D File Offset: 0x00058E5D
		private void case_735()
		{
			this.valid_param_mod = (CSharpParser.ParameterModifierType)0;
			this.start_anonymous(true, (ParametersCompiled)this.yyVals[-2 + this.yyTop], false, this.GetLocation(this.yyVals[-4 + this.yyTop]));
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x0005AB91 File Offset: 0x00058D91
		private void case_736()
		{
			this.yyVal = this.end_anonymous((ParametersBlock)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x0005AC99 File Offset: 0x00058E99
		private void case_738()
		{
			this.valid_param_mod = (CSharpParser.ParameterModifierType)0;
			this.start_anonymous(true, (ParametersCompiled)this.yyVals[-2 + this.yyTop], true, this.GetLocation(this.yyVals[-5 + this.yyTop]));
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x0005AB91 File Offset: 0x00058D91
		private void case_739()
		{
			this.yyVal = this.end_anonymous((ParametersBlock)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x0005ACD8 File Offset: 0x00058ED8
		private void case_746()
		{
			this.yyVal = new RefValueExpr((Expression)this.yyVals[-3 + this.yyTop], (FullNamedExpression)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-5 + this.yyTop]));
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x0005AD2F File Offset: 0x00058F2F
		private void case_747()
		{
			this.yyVal = new RefTypeExpr((Expression)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-3 + this.yyTop]));
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x0005AD66 File Offset: 0x00058F66
		private void case_748()
		{
			this.yyVal = new MakeRefExpr((Expression)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-3 + this.yyTop]));
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x0005ADA0 File Offset: 0x00058FA0
		private void case_753()
		{
			this.yyVal = this.yyVals[-1 + this.yyTop];
			if (this.lang_version != LanguageVersion.Experimental)
			{
				this.FeatureIsNotAvailable(this.GetLocation(this.yyVals[-2 + this.yyTop]), "primary constructor");
			}
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x0005ADED File Offset: 0x00058FED
		private void case_758()
		{
			this.lexer.parsing_block++;
			this.current_type.PrimaryConstructorBaseArgumentsStart = this.GetLocation(this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x0005AE24 File Offset: 0x00059024
		private void case_759()
		{
			this.current_type.PrimaryConstructorBaseArguments = (Arguments)this.yyVals[-1 + this.yyTop];
			this.lexer.parsing_block--;
			this.yyVal = this.yyVals[-5 + this.yyTop];
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x0005AE7C File Offset: 0x0005907C
		private void case_761()
		{
			this.lexer.ConstraintsParsing = true;
			Class @class = new Class(this.current_container, (MemberName)this.yyVals[0 + this.yyTop], (Modifiers)this.yyVals[-4 + this.yyTop], (Attributes)this.yyVals[-5 + this.yyTop]);
			if ((@class.ModFlags & Modifiers.STATIC) != (Modifiers)0 && this.lang_version == LanguageVersion.ISO_1)
			{
				this.FeatureIsNotAvailable(@class.Location, "static classes");
			}
			this.push_current_container(@class, this.yyVals[-3 + this.yyTop]);
			this.valid_param_mod = CSharpParser.ParameterModifierType.PrimaryConstructor;
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x0005AF28 File Offset: 0x00059128
		private void case_762()
		{
			this.valid_param_mod = (CSharpParser.ParameterModifierType)0;
			this.lexer.ConstraintsParsing = false;
			if (this.yyVals[-1 + this.yyTop] != null)
			{
				this.current_type.PrimaryConstructorParameters = (ParametersCompiled)this.yyVals[-1 + this.yyTop];
			}
			if (this.yyVals[0 + this.yyTop] != null)
			{
				this.current_container.SetConstraints((List<Constraints>)this.yyVals[0 + this.yyTop]);
			}
			if (this.doc_support)
			{
				this.current_container.PartialContainer.DocComment = this.Lexer.consume_doc_comment();
				this.Lexer.doc_state = XmlCommentState.Allowed;
			}
			this.lexer.parsing_modifiers = true;
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x000530D9 File Offset: 0x000512D9
		private void case_763()
		{
			this.lexer.parsing_declaration--;
			if (this.doc_support)
			{
				this.Lexer.doc_state = XmlCommentState.Allowed;
			}
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x0005558C File Offset: 0x0005378C
		private void case_764()
		{
			object obj = this.yyVals[0 + this.yyTop];
			this.yyVal = this.pop_current_class();
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x0005AFE3 File Offset: 0x000591E3
		private void case_767()
		{
			this.mod_locations = null;
			this.yyVal = CSharpParser.ModifierNone;
			this.lexer.parsing_modifiers = false;
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x0005B004 File Offset: 0x00059204
		private void case_770()
		{
			Modifiers modifiers = (Modifiers)this.yyVals[-1 + this.yyTop];
			Modifiers modifiers2 = (Modifiers)this.yyVals[0 + this.yyTop];
			if ((modifiers & modifiers2) != (Modifiers)0)
			{
				this.report.Error(1004, this.lexer.Location - ModifiersExtensions.Name(modifiers2).Length, "Duplicate `{0}' modifier", ModifiersExtensions.Name(modifiers2));
			}
			else if ((modifiers2 & Modifiers.AccessibilityMask) != (Modifiers)0 && (modifiers & Modifiers.AccessibilityMask) != (Modifiers)0 && (modifiers2 | (modifiers & Modifiers.AccessibilityMask)) != (Modifiers.PROTECTED | Modifiers.INTERNAL))
			{
				this.report.Error(107, this.lexer.Location - ModifiersExtensions.Name(modifiers2).Length, "More than one protection modifier specified");
			}
			this.yyVal = (modifiers | modifiers2);
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x0005B0CC File Offset: 0x000592CC
		private void case_771()
		{
			this.yyVal = Modifiers.NEW;
			if (this.current_container.Kind == MemberKind.Namespace)
			{
				this.report.Error(1530, this.GetLocation(this.yyVals[0 + this.yyTop]), "Keyword `new' is not allowed on namespace elements");
			}
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x0005B122 File Offset: 0x00059322
		private void case_772()
		{
			this.yyVal = Modifiers.PUBLIC;
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x0005B130 File Offset: 0x00059330
		private void case_773()
		{
			this.yyVal = Modifiers.PROTECTED;
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x0005B13E File Offset: 0x0005933E
		private void case_774()
		{
			this.yyVal = Modifiers.INTERNAL;
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x0005B14C File Offset: 0x0005934C
		private void case_775()
		{
			this.yyVal = Modifiers.PRIVATE;
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x0005B15A File Offset: 0x0005935A
		private void case_776()
		{
			this.yyVal = Modifiers.ABSTRACT;
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x0005B169 File Offset: 0x00059369
		private void case_777()
		{
			this.yyVal = Modifiers.SEALED;
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x0005B178 File Offset: 0x00059378
		private void case_778()
		{
			this.yyVal = Modifiers.STATIC;
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x0005B18A File Offset: 0x0005938A
		private void case_779()
		{
			this.yyVal = Modifiers.READONLY;
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x0005B19C File Offset: 0x0005939C
		private void case_780()
		{
			this.yyVal = Modifiers.VIRTUAL;
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x0005B1AE File Offset: 0x000593AE
		private void case_781()
		{
			this.yyVal = Modifiers.OVERRIDE;
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x0005B1C0 File Offset: 0x000593C0
		private void case_782()
		{
			this.yyVal = Modifiers.EXTERN;
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x0005B1D2 File Offset: 0x000593D2
		private void case_783()
		{
			this.yyVal = Modifiers.VOLATILE;
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x0005B1E4 File Offset: 0x000593E4
		private void case_784()
		{
			this.yyVal = Modifiers.UNSAFE;
			if (!this.settings.Unsafe)
			{
				this.Error_UnsafeCodeNotAllowed(this.GetLocation(this.yyVals[0 + this.yyTop]));
			}
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x0005B21E File Offset: 0x0005941E
		private void case_785()
		{
			this.yyVal = Modifiers.ASYNC;
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x0005B230 File Offset: 0x00059430
		private void case_789()
		{
			this.Error_SyntaxError(this.yyToken);
			this.current_type.SetBaseTypes((List<FullNamedExpression>)this.yyVals[-1 + this.yyTop]);
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x0005B260 File Offset: 0x00059460
		private void case_792()
		{
			this.yyVal = new List<Constraints>(1)
			{
				(Constraints)this.yyVals[0 + this.yyTop]
			};
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x0005B298 File Offset: 0x00059498
		private void case_793()
		{
			List<Constraints> list = (List<Constraints>)this.yyVals[-1 + this.yyTop];
			Constraints constraints = (Constraints)this.yyVals[0 + this.yyTop];
			foreach (Constraints constraints2 in list)
			{
				if (constraints.TypeParameter.Value == constraints2.TypeParameter.Value)
				{
					this.report.Error(409, constraints.Location, "A constraint clause has already been specified for type parameter `{0}'", constraints.TypeParameter.Value);
				}
			}
			list.Add(constraints);
			this.yyVal = list;
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x0005B35C File Offset: 0x0005955C
		private void case_794()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-2 + this.yyTop];
			this.yyVal = new Constraints(new SimpleMemberName(locatedToken.Value, locatedToken.Location), (List<FullNamedExpression>)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-3 + this.yyTop]));
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x0005B3C8 File Offset: 0x000595C8
		private void case_795()
		{
			this.Error_SyntaxError(this.yyToken);
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-1 + this.yyTop];
			this.yyVal = new Constraints(new SimpleMemberName(locatedToken.Value, locatedToken.Location), null, this.GetLocation(this.yyVals[-2 + this.yyTop]));
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x0005B42C File Offset: 0x0005962C
		private void case_796()
		{
			this.yyVal = new List<FullNamedExpression>(1)
			{
				(FullNamedExpression)this.yyVals[0 + this.yyTop]
			};
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x0005B464 File Offset: 0x00059664
		private void case_797()
		{
			List<FullNamedExpression> list = (List<FullNamedExpression>)this.yyVals[-2 + this.yyTop];
			List<FullNamedExpression> list2 = list;
			SpecialContraintExpr specialContraintExpr = list2[list2.Count - 1] as SpecialContraintExpr;
			if (specialContraintExpr != null && (specialContraintExpr.Constraint & SpecialConstraint.Constructor) != SpecialConstraint.None)
			{
				this.report.Error(401, this.GetLocation(this.yyVals[-1 + this.yyTop]), "The `new()' constraint must be the last constraint specified");
			}
			specialContraintExpr = (this.yyVals[0 + this.yyTop] as SpecialContraintExpr);
			if (specialContraintExpr != null)
			{
				if ((specialContraintExpr.Constraint & (SpecialConstraint.Class | SpecialConstraint.Struct)) != SpecialConstraint.None)
				{
					this.report.Error(449, specialContraintExpr.Location, "The `class' or `struct' constraint must be the first constraint specified");
				}
				else
				{
					specialContraintExpr = (list[0] as SpecialContraintExpr);
					if (specialContraintExpr != null && (specialContraintExpr.Constraint & SpecialConstraint.Struct) != SpecialConstraint.None)
					{
						this.report.Error(451, this.GetLocation(this.yyVals[0 + this.yyTop]), "The `new()' constraint cannot be used with the `struct' constraint");
					}
				}
			}
			list.Add((FullNamedExpression)this.yyVals[0 + this.yyTop]);
			this.yyVal = list;
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x0005B578 File Offset: 0x00059778
		private void case_798()
		{
			if (this.yyVals[0 + this.yyTop] is ComposedCast)
			{
				this.report.Error(706, this.GetLocation(this.yyVals[0 + this.yyTop]), "Invalid constraint type `{0}'", ((ComposedCast)this.yyVals[0 + this.yyTop]).GetSignatureForError());
			}
			this.yyVal = this.yyVals[0 + this.yyTop];
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x0005B5F3 File Offset: 0x000597F3
		private void case_799()
		{
			this.yyVal = new SpecialContraintExpr(SpecialConstraint.Constructor, this.GetLocation(this.yyVals[-2 + this.yyTop]));
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x0005B617 File Offset: 0x00059817
		private void case_803()
		{
			if (this.lang_version <= LanguageVersion.V_3)
			{
				this.FeatureIsNotAvailable(this.lexer.Location, "generic type variance");
			}
			this.yyVal = this.yyVals[0 + this.yyTop];
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x0005B64D File Offset: 0x0005984D
		private void case_806()
		{
			this.lexer.parsing_block++;
			this.start_block(this.GetLocation(this.yyVals[0 + this.yyTop]));
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x0005B67D File Offset: 0x0005987D
		private void case_808()
		{
			this.lexer.parsing_block--;
			this.yyVal = this.end_block(this.GetLocation(this.yyVals[0 + this.yyTop]));
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x0005B6B3 File Offset: 0x000598B3
		private void case_809()
		{
			this.lexer.parsing_block--;
			this.yyVal = this.end_block(this.lexer.Location);
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x0005B6DF File Offset: 0x000598DF
		private void case_810()
		{
			this.lexer.parsing_block++;
			this.current_block.StartLocation = this.GetLocation(this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x0005B67D File Offset: 0x0005987D
		private void case_811()
		{
			this.lexer.parsing_block--;
			this.yyVal = this.end_block(this.GetLocation(this.yyVals[0 + this.yyTop]));
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x00052490 File Offset: 0x00050690
		private void case_819()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = null;
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x0005B714 File Offset: 0x00059914
		private void case_852()
		{
			this.report.Error(1023, this.GetLocation(this.yyVals[0 + this.yyTop]), "An embedded statement may not be a declaration or labeled statement");
			this.yyVal = null;
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x0005B714 File Offset: 0x00059914
		private void case_853()
		{
			this.report.Error(1023, this.GetLocation(this.yyVals[0 + this.yyTop]), "An embedded statement may not be a declaration or labeled statement");
			this.yyVal = null;
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x0005B747 File Offset: 0x00059947
		private void case_854()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new EmptyStatement(this.GetLocation(this.yyVals[0 + this.yyTop]));
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x0005B775 File Offset: 0x00059975
		private void case_855()
		{
			this.yyVal = new EmptyStatement(this.lexer.Location);
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x0005B790 File Offset: 0x00059990
		private void case_856()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-1 + this.yyTop];
			LabeledStatement labeledStatement = new LabeledStatement(locatedToken.Value, this.current_block, locatedToken.Location);
			this.current_block.AddLabel(labeledStatement);
			this.current_block.AddStatement(labeledStatement);
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x0005B7E4 File Offset: 0x000599E4
		private void case_859()
		{
			if (this.yyVals[-1 + this.yyTop] is VarExpr)
			{
				this.yyVals[-1 + this.yyTop] = new SimpleName("var", ((VarExpr)this.yyVals[-1 + this.yyTop]).Location);
			}
			this.yyVal = new ComposedCast((FullNamedExpression)this.yyVals[-1 + this.yyTop], (ComposedTypeSpecifier)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x0005B86C File Offset: 0x00059A6C
		private void case_860()
		{
			ATypeNameExpression atypeNameExpression = (ATypeNameExpression)this.yyVals[-1 + this.yyTop];
			if (this.yyVals[0 + this.yyTop] != null)
			{
				this.yyVal = new ComposedCast(atypeNameExpression, (ComposedTypeSpecifier)this.yyVals[0 + this.yyTop]);
				return;
			}
			if (atypeNameExpression.Name == "var" && atypeNameExpression is SimpleName)
			{
				this.yyVal = new VarExpr(atypeNameExpression.Location);
				return;
			}
			this.yyVal = this.yyVals[-1 + this.yyTop];
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x0005B904 File Offset: 0x00059B04
		private void case_861()
		{
			ATypeNameExpression left = (ATypeNameExpression)this.yyVals[-1 + this.yyTop];
			this.yyVal = new ComposedCast(left, (ComposedTypeSpecifier)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x0005B948 File Offset: 0x00059B48
		private void case_865()
		{
			((ComposedTypeSpecifier)this.yyVals[-1 + this.yyTop]).Next = (ComposedTypeSpecifier)this.yyVals[0 + this.yyTop];
			this.yyVal = this.yyVals[-1 + this.yyTop];
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x0005B998 File Offset: 0x00059B98
		private void case_869()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[0 + this.yyTop];
			LocalVariable li = new LocalVariable(this.current_block, locatedToken.Value, locatedToken.Location);
			this.current_block.AddLocalName(li);
			this.current_variable = new BlockVariable((FullNamedExpression)this.yyVals[-1 + this.yyTop], li);
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x0005B9FE File Offset: 0x00059BFE
		private void case_870()
		{
			this.yyVal = this.current_variable;
			this.current_variable = null;
			object obj = this.yyVals[-2 + this.yyTop];
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x0005BA24 File Offset: 0x00059C24
		private void case_871()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[0 + this.yyTop];
			LocalVariable li = new LocalVariable(this.current_block, locatedToken.Value, LocalVariable.Flags.Constant, locatedToken.Location);
			this.current_block.AddLocalName(li);
			this.current_variable = new BlockConstant((FullNamedExpression)this.yyVals[-1 + this.yyTop], li);
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x0005BA8C File Offset: 0x00059C8C
		private void case_872()
		{
			this.yyVal = this.current_variable;
			this.current_variable = null;
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x0005BAA1 File Offset: 0x00059CA1
		private void case_874()
		{
			this.current_variable.Initializer = (Expression)this.yyVals[0 + this.yyTop];
			this.yyVal = this.current_variable;
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x0005BACE File Offset: 0x00059CCE
		private void case_875()
		{
			if (this.yyToken == 427)
			{
				this.report.Error(650, this.lexer.Location, "Syntax error, bad array declarator. To declare a managed array the rank specifier precedes the variable's identifier. To declare a fixed size buffer field, use the fixed keyword before the field type");
				return;
			}
			this.Error_SyntaxError(this.yyToken);
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x0005BB0C File Offset: 0x00059D0C
		private void case_879()
		{
			foreach (BlockVariableDeclarator blockVariableDeclarator in this.current_variable.Declarators)
			{
				if (blockVariableDeclarator.Initializer == null)
				{
					this.Error_MissingInitializer(blockVariableDeclarator.Variable.Location);
				}
			}
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x0005BB78 File Offset: 0x00059D78
		private void case_882()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[0 + this.yyTop];
			LocalVariable li = new LocalVariable(this.current_variable.Variable, locatedToken.Value, locatedToken.Location);
			BlockVariableDeclarator decl = new BlockVariableDeclarator(li, null);
			this.current_variable.AddDeclarator(decl);
			this.current_block.AddLocalName(li);
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x0005BBD8 File Offset: 0x00059DD8
		private void case_883()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-2 + this.yyTop];
			LocalVariable li = new LocalVariable(this.current_variable.Variable, locatedToken.Value, locatedToken.Location);
			BlockVariableDeclarator decl = new BlockVariableDeclarator(li, (Expression)this.yyVals[0 + this.yyTop]);
			this.current_variable.AddDeclarator(decl);
			this.current_block.AddLocalName(li);
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x0005BC4C File Offset: 0x00059E4C
		private void case_890()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-2 + this.yyTop];
			LocalVariable li = new LocalVariable(this.current_block, locatedToken.Value, LocalVariable.Flags.Constant, locatedToken.Location);
			BlockVariableDeclarator decl = new BlockVariableDeclarator(li, (Expression)this.yyVals[0 + this.yyTop]);
			this.current_variable.AddDeclarator(decl);
			this.current_block.AddLocalName(li);
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x0005BCBC File Offset: 0x00059EBC
		private void case_892()
		{
			this.yyVal = new StackAlloc((Expression)this.yyVals[-3 + this.yyTop], (Expression)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-4 + this.yyTop]));
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x0005BD14 File Offset: 0x00059F14
		private void case_893()
		{
			this.report.Error(1575, this.GetLocation(this.yyVals[-1 + this.yyTop]), "A stackalloc expression requires [] after type");
			this.yyVal = new StackAlloc((Expression)this.yyVals[0 + this.yyTop], null, this.GetLocation(this.yyVals[-1 + this.yyTop]));
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x0005BD80 File Offset: 0x00059F80
		private void case_894()
		{
			this.yyVal = this.yyVals[-1 + this.yyTop];
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x0005BD98 File Offset: 0x00059F98
		private void case_896()
		{
			this.yyVal = this.yyVals[-1 + this.yyTop];
			this.report.Error(1002, this.GetLocation(this.yyVals[0 + this.yyTop]), "; expected");
			this.lexer.putback(125);
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x0005BDF4 File Offset: 0x00059FF4
		private void case_899()
		{
			ExpressionStatement expressionStatement = this.yyVals[0 + this.yyTop] as ExpressionStatement;
			if (expressionStatement == null)
			{
				Expression expr = this.yyVals[0 + this.yyTop] as Expression;
				this.yyVal = new StatementErrorExpression(expr);
				return;
			}
			this.yyVal = new StatementExpression(expressionStatement);
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x0005BE48 File Offset: 0x0005A048
		private void case_900()
		{
			Expression s = (Expression)this.yyVals[0 + this.yyTop];
			this.yyVal = new StatementExpression(new OptionalAssign(s, this.lexer.Location));
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x0005B747 File Offset: 0x00059947
		private void case_901()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new EmptyStatement(this.GetLocation(this.yyVals[0 + this.yyTop]));
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x0005BE88 File Offset: 0x0005A088
		private void case_904()
		{
			if (this.yyVals[0 + this.yyTop] is EmptyStatement)
			{
				this.Warning_EmptyStatement(this.GetLocation(this.yyVals[0 + this.yyTop]));
			}
			this.yyVal = new If((BooleanExpression)this.yyVals[-2 + this.yyTop], (Statement)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-4 + this.yyTop]));
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x0005BF10 File Offset: 0x0005A110
		private void case_905()
		{
			this.yyVal = new If((BooleanExpression)this.yyVals[-4 + this.yyTop], (Statement)this.yyVals[-2 + this.yyTop], (Statement)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-6 + this.yyTop]));
			if (this.yyVals[-2 + this.yyTop] is EmptyStatement)
			{
				this.Warning_EmptyStatement(this.GetLocation(this.yyVals[-2 + this.yyTop]));
			}
			if (this.yyVals[0 + this.yyTop] is EmptyStatement)
			{
				this.Warning_EmptyStatement(this.GetLocation(this.yyVals[0 + this.yyTop]));
			}
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x0005BFE0 File Offset: 0x0005A1E0
		private void case_906()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new If((BooleanExpression)this.yyVals[-1 + this.yyTop], null, this.GetLocation(this.yyVals[-3 + this.yyTop]));
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x0005C030 File Offset: 0x0005A230
		private void case_908()
		{
			this.yyVal = new Switch((Expression)this.yyVals[-5 + this.yyTop], this.current_block.Explicit, this.GetLocation(this.yyVals[-7 + this.yyTop]));
			this.end_block(this.GetLocation(this.yyVals[0 + this.yyTop]));
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x0005C09C File Offset: 0x0005A29C
		private void case_909()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Switch((Expression)this.yyVals[-1 + this.yyTop], null, this.GetLocation(this.yyVals[-3 + this.yyTop]));
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x0005C0EC File Offset: 0x0005A2EC
		private void case_916()
		{
			SwitchLabel switchLabel = (SwitchLabel)this.yyVals[0 + this.yyTop];
			switchLabel.SectionStart = true;
			this.current_block.AddStatement(switchLabel);
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x0005C121 File Offset: 0x0005A321
		private void case_918()
		{
			this.yyVal = new SwitchLabel((Expression)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-2 + this.yyTop]));
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x0005C158 File Offset: 0x0005A358
		private void case_919()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new SwitchLabel((Expression)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-2 + this.yyTop]));
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x0005C1A8 File Offset: 0x0005A3A8
		private void case_925()
		{
			if (this.yyVals[0 + this.yyTop] is EmptyStatement && this.lexer.peek_token() == 371)
			{
				this.Warning_EmptyStatement(this.GetLocation(this.yyVals[0 + this.yyTop]));
			}
			this.yyVal = new While((BooleanExpression)this.yyVals[-2 + this.yyTop], (Statement)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-4 + this.yyTop]));
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x0005C244 File Offset: 0x0005A444
		private void case_926()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new While((BooleanExpression)this.yyVals[-1 + this.yyTop], null, this.GetLocation(this.yyVals[-3 + this.yyTop]));
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x0005C294 File Offset: 0x0005A494
		private void case_927()
		{
			this.yyVal = new Do((Statement)this.yyVals[-5 + this.yyTop], (BooleanExpression)this.yyVals[-2 + this.yyTop], this.GetLocation(this.yyVals[-6 + this.yyTop]), this.GetLocation(this.yyVals[-4 + this.yyTop]));
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x0005C304 File Offset: 0x0005A504
		private void case_928()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Do((Statement)this.yyVals[-1 + this.yyTop], null, this.GetLocation(this.yyVals[-2 + this.yyTop]), Location.Null);
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x0005C358 File Offset: 0x0005A558
		private void case_929()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Do((Statement)this.yyVals[-4 + this.yyTop], (BooleanExpression)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-5 + this.yyTop]), this.GetLocation(this.yyVals[-3 + this.yyTop]));
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x0005C3D4 File Offset: 0x0005A5D4
		private void case_930()
		{
			this.start_block(this.GetLocation(this.yyVals[0 + this.yyTop]));
			this.current_block.IsCompilerGenerated = true;
			For s = new For(this.GetLocation(this.yyVals[-1 + this.yyTop]));
			this.current_block.AddStatement(s);
			this.yyVal = s;
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x0005C438 File Offset: 0x0005A638
		private void case_932()
		{
			((For)this.yyVals[-2 + this.yyTop]).Initializer = (Statement)this.yyVals[-1 + this.yyTop];
			this.oob_stack.Push(this.yyVals[-2 + this.yyTop]);
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x0005C490 File Offset: 0x0005A690
		private void case_933()
		{
			Tuple<Location, Location> tuple = (Tuple<Location, Location>)this.yyVals[-1 + this.yyTop];
			this.oob_stack.Pop();
			if (this.yyVals[0 + this.yyTop] is EmptyStatement && this.lexer.peek_token() == 371)
			{
				this.Warning_EmptyStatement(this.GetLocation(this.yyVals[0 + this.yyTop]));
			}
			((For)this.yyVals[-5 + this.yyTop]).Statement = (Statement)this.yyVals[0 + this.yyTop];
			this.yyVal = this.end_block(this.GetLocation(this.yyVals[-3 + this.yyTop]));
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x0005C551 File Offset: 0x0005A751
		private void case_934()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = this.end_block(this.current_block.StartLocation);
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x0005C576 File Offset: 0x0005A776
		private void case_935()
		{
			((For)this.oob_stack.Peek()).Condition = (BooleanExpression)this.yyVals[-1 + this.yyTop];
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x0005C5A4 File Offset: 0x0005A7A4
		private void case_937()
		{
			this.report.Error(1525, this.GetLocation(this.yyVals[0 + this.yyTop]), "Unexpected symbol `}'");
			((For)this.oob_stack.Peek()).Condition = (BooleanExpression)this.yyVals[-1 + this.yyTop];
			this.yyVal = new Tuple<Location, Location>(this.GetLocation(this.yyVals[0 + this.yyTop]), this.GetLocation(this.yyVals[0 + this.yyTop]));
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x0005C63C File Offset: 0x0005A83C
		private void case_938()
		{
			((For)this.oob_stack.Peek()).Iterator = (Statement)this.yyVals[-1 + this.yyTop];
			this.yyVal = this.GetLocation(this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x0005C694 File Offset: 0x0005A894
		private void case_939()
		{
			this.report.Error(1525, this.GetLocation(this.yyVals[0 + this.yyTop]), "Unexpected symbol expected ')'");
			((For)this.oob_stack.Peek()).Iterator = (Statement)this.yyVals[-1 + this.yyTop];
			this.yyVal = this.GetLocation(this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x0005C714 File Offset: 0x0005A914
		private void case_944()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[0 + this.yyTop];
			LocalVariable li = new LocalVariable(this.current_block, locatedToken.Value, locatedToken.Location);
			this.current_block.AddLocalName(li);
			this.current_variable = new BlockVariable((FullNamedExpression)this.yyVals[-1 + this.yyTop], li);
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x0005C77A File Offset: 0x0005A97A
		private void case_945()
		{
			this.yyVal = this.current_variable;
			object obj = this.yyVals[-1 + this.yyTop];
			this.current_variable = null;
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x0005C7A0 File Offset: 0x0005A9A0
		private void case_953()
		{
			StatementList statementList = this.yyVals[-2 + this.yyTop] as StatementList;
			if (statementList == null)
			{
				statementList = new StatementList((Statement)this.yyVals[-2 + this.yyTop], (Statement)this.yyVals[0 + this.yyTop]);
			}
			else
			{
				statementList.Add((Statement)this.yyVals[0 + this.yyTop]);
			}
			this.yyVal = statementList;
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x0005C818 File Offset: 0x0005AA18
		private void case_954()
		{
			this.report.Error(230, this.GetLocation(this.yyVals[-3 + this.yyTop]), "Type and identifier are both required in a foreach statement");
			this.start_block(this.GetLocation(this.yyVals[-2 + this.yyTop]));
			this.current_block.IsCompilerGenerated = true;
			Foreach s = new Foreach((Expression)this.yyVals[-1 + this.yyTop], null, null, null, null, this.GetLocation(this.yyVals[-3 + this.yyTop]));
			this.current_block.AddStatement(s);
			this.yyVal = this.end_block(this.GetLocation(this.yyVals[0 + this.yyTop]));
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x0005C8DC File Offset: 0x0005AADC
		private void case_955()
		{
			this.Error_SyntaxError(this.yyToken);
			this.start_block(this.GetLocation(this.yyVals[-3 + this.yyTop]));
			this.current_block.IsCompilerGenerated = true;
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-1 + this.yyTop];
			LocalVariable localVariable = new LocalVariable(this.current_block, locatedToken.Value, LocalVariable.Flags.Used | LocalVariable.Flags.ForeachVariable, locatedToken.Location);
			this.current_block.AddLocalName(localVariable);
			Foreach s = new Foreach((Expression)this.yyVals[-2 + this.yyTop], localVariable, null, null, null, this.GetLocation(this.yyVals[-4 + this.yyTop]));
			this.current_block.AddStatement(s);
			this.yyVal = this.end_block(this.GetLocation(this.yyVals[0 + this.yyTop]));
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x0005C9BC File Offset: 0x0005ABBC
		private void case_956()
		{
			this.start_block(this.GetLocation(this.yyVals[-5 + this.yyTop]));
			this.current_block.IsCompilerGenerated = true;
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-3 + this.yyTop];
			LocalVariable li = new LocalVariable(this.current_block, locatedToken.Value, LocalVariable.Flags.Used | LocalVariable.Flags.ForeachVariable, locatedToken.Location);
			this.current_block.AddLocalName(li);
			this.yyVal = li;
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x0005CA34 File Offset: 0x0005AC34
		private void case_957()
		{
			if (this.yyVals[0 + this.yyTop] is EmptyStatement && this.lexer.peek_token() == 371)
			{
				this.Warning_EmptyStatement(this.GetLocation(this.yyVals[0 + this.yyTop]));
			}
			Foreach @foreach = new Foreach((Expression)this.yyVals[-6 + this.yyTop], (LocalVariable)this.yyVals[-1 + this.yyTop], (Expression)this.yyVals[-3 + this.yyTop], (Statement)this.yyVals[0 + this.yyTop], this.current_block, this.GetLocation(this.yyVals[-8 + this.yyTop]));
			this.end_block(this.GetLocation(this.yyVals[-2 + this.yyTop]));
			this.yyVal = @foreach;
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x0005CB1C File Offset: 0x0005AD1C
		private void case_964()
		{
			this.yyVal = new Break(this.GetLocation(this.yyVals[-1 + this.yyTop]));
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x0005CB3E File Offset: 0x0005AD3E
		private void case_965()
		{
			this.yyVal = new Continue(this.GetLocation(this.yyVals[-1 + this.yyTop]));
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x0005CB60 File Offset: 0x0005AD60
		private void case_966()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Continue(this.GetLocation(this.yyVals[-1 + this.yyTop]));
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x0005CB90 File Offset: 0x0005AD90
		private void case_967()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-1 + this.yyTop];
			this.yyVal = new Goto(locatedToken.Value, this.GetLocation(this.yyVals[-2 + this.yyTop]));
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x0005CBD9 File Offset: 0x0005ADD9
		private void case_968()
		{
			this.yyVal = new GotoCase((Expression)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-3 + this.yyTop]));
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x0005CC10 File Offset: 0x0005AE10
		private void case_969()
		{
			this.yyVal = new GotoDefault(this.GetLocation(this.yyVals[-2 + this.yyTop]));
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x0005CC33 File Offset: 0x0005AE33
		private void case_970()
		{
			this.yyVal = new Return((Expression)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-2 + this.yyTop]));
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x0005CC6C File Offset: 0x0005AE6C
		private void case_971()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Return((Expression)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-2 + this.yyTop]));
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x0005CCBA File Offset: 0x0005AEBA
		private void case_972()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Return(null, this.GetLocation(this.yyVals[-1 + this.yyTop]));
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x0005CCE9 File Offset: 0x0005AEE9
		private void case_973()
		{
			this.yyVal = new Throw((Expression)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-2 + this.yyTop]));
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x0005CD20 File Offset: 0x0005AF20
		private void case_974()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Throw((Expression)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-2 + this.yyTop]));
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x0005CD6E File Offset: 0x0005AF6E
		private void case_975()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Throw(null, this.GetLocation(this.yyVals[-1 + this.yyTop]));
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x0005CDA0 File Offset: 0x0005AFA0
		private void case_976()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-3 + this.yyTop];
			if (locatedToken.Value != "yield")
			{
				this.report.Error(1003, locatedToken.Location, "; expected");
			}
			else if (this.yyVals[-1 + this.yyTop] == null)
			{
				this.report.Error(1627, this.GetLocation(this.yyVals[0 + this.yyTop]), "Expression expected after yield return");
			}
			else if (this.lang_version == LanguageVersion.ISO_1)
			{
				this.FeatureIsNotAvailable(locatedToken.Location, "iterators");
			}
			this.current_block.Explicit.RegisterIteratorYield();
			this.yyVal = new Yield((Expression)this.yyVals[-1 + this.yyTop], locatedToken.Location);
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x0005CE80 File Offset: 0x0005B080
		private void case_977()
		{
			this.Error_SyntaxError(this.yyToken);
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-3 + this.yyTop];
			if (locatedToken.Value != "yield")
			{
				this.report.Error(1003, locatedToken.Location, "; expected");
			}
			else if (this.yyVals[-1 + this.yyTop] == null)
			{
				this.report.Error(1627, this.GetLocation(this.yyVals[0 + this.yyTop]), "Expression expected after yield return");
			}
			else if (this.lang_version == LanguageVersion.ISO_1)
			{
				this.FeatureIsNotAvailable(locatedToken.Location, "iterators");
			}
			this.current_block.Explicit.RegisterIteratorYield();
			this.yyVal = new Yield((Expression)this.yyVals[-1 + this.yyTop], locatedToken.Location);
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x0005CF6C File Offset: 0x0005B16C
		private void case_978()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-2 + this.yyTop];
			if (locatedToken.Value != "yield")
			{
				this.report.Error(1003, locatedToken.Location, "; expected");
			}
			else if (this.lang_version == LanguageVersion.ISO_1)
			{
				this.FeatureIsNotAvailable(locatedToken.Location, "iterators");
			}
			this.current_block.ParametersBlock.TopBlock.IsIterator = true;
			this.yyVal = new YieldBreak(locatedToken.Location);
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x0005D000 File Offset: 0x0005B200
		private void case_982()
		{
			this.yyVal = new TryFinally((Statement)this.yyVals[-2 + this.yyTop], (ExplicitBlock)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-3 + this.yyTop]));
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x0005D058 File Offset: 0x0005B258
		private void case_983()
		{
			this.yyVal = new TryFinally(new TryCatch((Block)this.yyVals[-3 + this.yyTop], (List<Catch>)this.yyVals[-2 + this.yyTop], Location.Null, true), (ExplicitBlock)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-4 + this.yyTop]));
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x0005D0D0 File Offset: 0x0005B2D0
		private void case_984()
		{
			this.Error_SyntaxError(1524, this.yyToken);
			this.yyVal = new TryCatch((Block)this.yyVals[-1 + this.yyTop], null, this.GetLocation(this.yyVals[-2 + this.yyTop]), false);
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x0005D128 File Offset: 0x0005B328
		private void case_985()
		{
			this.yyVal = new List<Catch>(2)
			{
				(Catch)this.yyVals[0 + this.yyTop]
			};
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x0005D160 File Offset: 0x0005B360
		private void case_986()
		{
			List<Catch> list = (List<Catch>)this.yyVals[-1 + this.yyTop];
			Catch @catch = (Catch)this.yyVals[0 + this.yyTop];
			List<Catch> list2 = list;
			Catch catch2 = list2[list2.Count - 1];
			if (catch2.IsGeneral && catch2.Filter == null)
			{
				this.report.Error(1017, @catch.loc, "Try statement already has an empty catch block");
			}
			list.Add(@catch);
			this.yyVal = list;
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x0005D1E0 File Offset: 0x0005B3E0
		private void case_989()
		{
			this.yyVal = new Catch((ExplicitBlock)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-2 + this.yyTop]))
			{
				Filter = (CatchFilterExpression)this.yyVals[-1 + this.yyTop]
			};
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x0005D240 File Offset: 0x0005B440
		private void case_990()
		{
			this.start_block(this.GetLocation(this.yyVals[-3 + this.yyTop]));
			Catch @catch = new Catch((ExplicitBlock)this.current_block, this.GetLocation(this.yyVals[-4 + this.yyTop]));
			@catch.TypeExpression = (FullNamedExpression)this.yyVals[-2 + this.yyTop];
			if (this.yyVals[-1 + this.yyTop] != null)
			{
				LocatedToken locatedToken = (LocatedToken)this.yyVals[-1 + this.yyTop];
				@catch.Variable = new LocalVariable(this.current_block, locatedToken.Value, locatedToken.Location);
				this.current_block.AddLocalName(@catch.Variable);
			}
			this.yyVal = @catch;
			this.lexer.parsing_catch_when = true;
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x0005D314 File Offset: 0x0005B514
		private void case_991()
		{
			((Catch)this.yyVals[-1 + this.yyTop]).Filter = (CatchFilterExpression)this.yyVals[0 + this.yyTop];
			this.yyVal = this.yyVals[-1 + this.yyTop];
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x0005D364 File Offset: 0x0005B564
		private void case_992()
		{
			if (this.yyToken == 376)
			{
				this.report.Error(1015, this.lexer.Location, "A type that derives from `System.Exception', `object', or `string' expected");
			}
			else
			{
				this.Error_SyntaxError(this.yyToken);
			}
			this.yyVal = new Catch(null, this.GetLocation(this.yyVals[-2 + this.yyTop]));
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x0005D3CE File Offset: 0x0005B5CE
		private void case_994()
		{
			this.end_block(Location.Null);
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = null;
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x0005D3F0 File Offset: 0x0005B5F0
		private void case_997()
		{
			if (this.lang_version <= LanguageVersion.V_5)
			{
				this.FeatureIsNotAvailable(this.GetLocation(this.yyVals[-4 + this.yyTop]), "exception filter");
			}
			this.yyVal = new CatchFilterExpression((Expression)this.yyVals[-1 + this.yyTop], this.GetLocation(this.yyVals[-4 + this.yyTop]));
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x0005D45C File Offset: 0x0005B65C
		private void case_1000()
		{
			if (!this.settings.Unsafe)
			{
				this.Error_UnsafeCodeNotAllowed(this.GetLocation(this.yyVals[0 + this.yyTop]));
			}
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x0005D488 File Offset: 0x0005B688
		private void case_1002()
		{
			if (this.yyVals[0 + this.yyTop] is EmptyStatement && this.lexer.peek_token() == 371)
			{
				this.Warning_EmptyStatement(this.GetLocation(this.yyVals[0 + this.yyTop]));
			}
			this.yyVal = new Lock((Expression)this.yyVals[-2 + this.yyTop], (Statement)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-4 + this.yyTop]));
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x0005D524 File Offset: 0x0005B724
		private void case_1003()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Lock((Expression)this.yyVals[-1 + this.yyTop], null, this.GetLocation(this.yyVals[-3 + this.yyTop]));
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x0005D574 File Offset: 0x0005B774
		private void case_1004()
		{
			this.start_block(this.GetLocation(this.yyVals[-2 + this.yyTop]));
			this.current_block.IsCompilerGenerated = true;
			LocatedToken locatedToken = (LocatedToken)this.yyVals[0 + this.yyTop];
			LocalVariable li = new LocalVariable(this.current_block, locatedToken.Value, LocalVariable.Flags.Used | LocalVariable.Flags.FixedVariable, locatedToken.Location);
			this.current_block.AddLocalName(li);
			this.current_variable = new Fixed.VariableDeclaration((FullNamedExpression)this.yyVals[-1 + this.yyTop], li);
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x0005BA8C File Offset: 0x00059C8C
		private void case_1005()
		{
			this.yyVal = this.current_variable;
			this.current_variable = null;
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x0005D604 File Offset: 0x0005B804
		private void case_1006()
		{
			if (this.yyVals[0 + this.yyTop] is EmptyStatement && this.lexer.peek_token() == 371)
			{
				this.Warning_EmptyStatement(this.GetLocation(this.yyVals[0 + this.yyTop]));
			}
			Fixed s = new Fixed((Fixed.VariableDeclaration)this.yyVals[-1 + this.yyTop], (Statement)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-9 + this.yyTop]));
			this.current_block.AddStatement(s);
			this.yyVal = this.end_block(this.GetLocation(this.yyVals[-2 + this.yyTop]));
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x0005D6C8 File Offset: 0x0005B8C8
		private void case_1007()
		{
			this.start_block(this.GetLocation(this.yyVals[-2 + this.yyTop]));
			this.current_block.IsCompilerGenerated = true;
			LocatedToken locatedToken = (LocatedToken)this.yyVals[0 + this.yyTop];
			LocalVariable li = new LocalVariable(this.current_block, locatedToken.Value, LocalVariable.Flags.Used | LocalVariable.Flags.UsingVariable, locatedToken.Location);
			this.current_block.AddLocalName(li);
			this.current_variable = new Using.VariableDeclaration((FullNamedExpression)this.yyVals[-1 + this.yyTop], li);
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x0005BA8C File Offset: 0x00059C8C
		private void case_1008()
		{
			this.yyVal = this.current_variable;
			this.current_variable = null;
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x0005D75C File Offset: 0x0005B95C
		private void case_1009()
		{
			if (this.yyVals[0 + this.yyTop] is EmptyStatement && this.lexer.peek_token() == 371)
			{
				this.Warning_EmptyStatement(this.GetLocation(this.yyVals[0 + this.yyTop]));
			}
			Using s = new Using((Using.VariableDeclaration)this.yyVals[-1 + this.yyTop], (Statement)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-8 + this.yyTop]));
			this.current_block.AddStatement(s);
			this.yyVal = this.end_block(this.GetLocation(this.yyVals[-2 + this.yyTop]));
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x0005D820 File Offset: 0x0005BA20
		private void case_1010()
		{
			if (this.yyVals[0 + this.yyTop] is EmptyStatement && this.lexer.peek_token() == 371)
			{
				this.Warning_EmptyStatement(this.GetLocation(this.yyVals[0 + this.yyTop]));
			}
			this.yyVal = new Using((Expression)this.yyVals[-2 + this.yyTop], (Statement)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-4 + this.yyTop]));
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x0005D8BC File Offset: 0x0005BABC
		private void case_1011()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new Using((Expression)this.yyVals[-1 + this.yyTop], null, this.GetLocation(this.yyVals[-3 + this.yyTop]));
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x0005D90B File Offset: 0x0005BB0B
		private void case_1013()
		{
			this.Error_SyntaxError(this.yyToken);
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x0005BAA1 File Offset: 0x00059CA1
		private void case_1015()
		{
			this.current_variable.Initializer = (Expression)this.yyVals[0 + this.yyTop];
			this.yyVal = this.current_variable;
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x0005D91C File Offset: 0x0005BB1C
		private void case_1016()
		{
			this.lexer.query_parsing = false;
			AQueryClause aqueryClause = this.yyVals[-1 + this.yyTop] as AQueryClause;
			aqueryClause.Tail.Next = (AQueryClause)this.yyVals[0 + this.yyTop];
			this.yyVal = aqueryClause;
			this.current_block.SetEndLocation(this.lexer.Location);
			this.current_block = this.current_block.Parent;
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x0005D998 File Offset: 0x0005BB98
		private void case_1017()
		{
			AQueryClause aqueryClause = this.yyVals[-1 + this.yyTop] as AQueryClause;
			aqueryClause.Tail.Next = (AQueryClause)this.yyVals[0 + this.yyTop];
			this.yyVal = aqueryClause;
			this.current_block.SetEndLocation(this.lexer.Location);
			this.current_block = this.current_block.Parent;
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x0005DA08 File Offset: 0x0005BC08
		private void case_1018()
		{
			this.lexer.query_parsing = false;
			this.yyVal = this.yyVals[-1 + this.yyTop];
			this.current_block.SetEndLocation(this.lexer.Location);
			this.current_block = this.current_block.Parent;
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x0005DA5D File Offset: 0x0005BC5D
		private void case_1019()
		{
			this.yyVal = this.yyVals[-1 + this.yyTop];
			this.current_block.SetEndLocation(this.lexer.Location);
			this.current_block = this.current_block.Parent;
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x0005DA9C File Offset: 0x0005BC9C
		private void case_1020()
		{
			this.current_block = new QueryBlock(this.current_block, this.lexer.Location);
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-2 + this.yyTop];
			RangeVariable identifier = new RangeVariable(locatedToken.Value, locatedToken.Location);
			QueryStartClause start = new QueryStartClause((QueryBlock)this.current_block, (Expression)this.yyVals[0 + this.yyTop], identifier, this.GetLocation(this.yyVals[-3 + this.yyTop]));
			this.yyVal = new QueryExpression(start);
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x0005DB38 File Offset: 0x0005BD38
		private void case_1021()
		{
			this.current_block = new QueryBlock(this.current_block, this.lexer.Location);
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-2 + this.yyTop];
			RangeVariable identifier = new RangeVariable(locatedToken.Value, locatedToken.Location);
			QueryStartClause start = new QueryStartClause((QueryBlock)this.current_block, (Expression)this.yyVals[0 + this.yyTop], identifier, this.GetLocation(this.yyVals[-4 + this.yyTop]))
			{
				IdentifierType = (FullNamedExpression)this.yyVals[-3 + this.yyTop]
			};
			this.yyVal = new QueryExpression(start);
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x0005DBEC File Offset: 0x0005BDEC
		private void case_1022()
		{
			this.current_block = new QueryBlock(this.current_block, this.lexer.Location);
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-2 + this.yyTop];
			RangeVariable identifier = new RangeVariable(locatedToken.Value, locatedToken.Location);
			QueryStartClause start = new QueryStartClause((QueryBlock)this.current_block, (Expression)this.yyVals[0 + this.yyTop], identifier, this.GetLocation(this.yyVals[-3 + this.yyTop]));
			this.yyVal = new QueryExpression(start);
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x0005DC88 File Offset: 0x0005BE88
		private void case_1023()
		{
			this.current_block = new QueryBlock(this.current_block, this.lexer.Location);
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-2 + this.yyTop];
			RangeVariable identifier = new RangeVariable(locatedToken.Value, locatedToken.Location);
			QueryStartClause start = new QueryStartClause((QueryBlock)this.current_block, (Expression)this.yyVals[0 + this.yyTop], identifier, this.GetLocation(this.yyVals[-4 + this.yyTop]))
			{
				IdentifierType = (FullNamedExpression)this.yyVals[-3 + this.yyTop]
			};
			this.yyVal = new QueryExpression(start);
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x0005DD3C File Offset: 0x0005BF3C
		private void case_1025()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-3 + this.yyTop];
			RangeVariable rangeVariable = new RangeVariable(locatedToken.Value, locatedToken.Location);
			this.yyVal = new SelectMany((QueryBlock)this.current_block, rangeVariable, (Expression)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-4 + this.yyTop]));
			this.current_block.SetEndLocation(this.lexer.Location);
			this.current_block = this.current_block.Parent;
			((QueryBlock)this.current_block).AddRangeVariable(rangeVariable);
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x0005DDEC File Offset: 0x0005BFEC
		private void case_1027()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-3 + this.yyTop];
			RangeVariable rangeVariable = new RangeVariable(locatedToken.Value, locatedToken.Location);
			this.yyVal = new SelectMany((QueryBlock)this.current_block, rangeVariable, (Expression)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-5 + this.yyTop]))
			{
				IdentifierType = (FullNamedExpression)this.yyVals[-4 + this.yyTop]
			};
			this.current_block.SetEndLocation(this.lexer.Location);
			this.current_block = this.current_block.Parent;
			((QueryBlock)this.current_block).AddRangeVariable(rangeVariable);
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x0005DEB8 File Offset: 0x0005C0B8
		private void case_1028()
		{
			AQueryClause aqueryClause = (AQueryClause)this.yyVals[-1 + this.yyTop];
			if (this.yyVals[0 + this.yyTop] != null)
			{
				aqueryClause.Next = (AQueryClause)this.yyVals[0 + this.yyTop];
			}
			if (this.yyVals[-2 + this.yyTop] != null)
			{
				AQueryClause aqueryClause2 = (AQueryClause)this.yyVals[-2 + this.yyTop];
				aqueryClause2.Tail.Next = aqueryClause;
				aqueryClause = aqueryClause2;
			}
			this.yyVal = aqueryClause;
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x0005DF40 File Offset: 0x0005C140
		private void case_1029()
		{
			AQueryClause next = (AQueryClause)this.yyVals[0 + this.yyTop];
			if (this.yyVals[-1 + this.yyTop] != null)
			{
				AQueryClause aqueryClause = (AQueryClause)this.yyVals[-1 + this.yyTop];
				aqueryClause.Tail.Next = next;
				next = aqueryClause;
			}
			this.yyVal = next;
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x0005DF9C File Offset: 0x0005C19C
		private void case_1031()
		{
			this.report.Error(742, this.GetLocation(this.yyVals[0 + this.yyTop]), "Unexpected symbol `{0}'. A query body must end with select or group clause", this.GetSymbolName(this.yyToken));
			this.yyVal = this.yyVals[-1 + this.yyTop];
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x00052490 File Offset: 0x00050690
		private void case_1032()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = null;
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x0005DFF4 File Offset: 0x0005C1F4
		private void case_1034()
		{
			this.yyVal = new Select((QueryBlock)this.current_block, (Expression)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-2 + this.yyTop]));
			this.current_block.SetEndLocation(this.lexer.Location);
			this.current_block = this.current_block.Parent;
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x0005E068 File Offset: 0x0005C268
		private void case_1035()
		{
			if (this.linq_clause_blocks == null)
			{
				this.linq_clause_blocks = new Stack<QueryBlock>();
			}
			this.current_block = new QueryBlock(this.current_block, this.lexer.Location);
			this.linq_clause_blocks.Push((QueryBlock)this.current_block);
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x0005E0BC File Offset: 0x0005C2BC
		private void case_1036()
		{
			this.current_block.SetEndLocation(this.lexer.Location);
			this.current_block = this.current_block.Parent;
			this.current_block = new QueryBlock(this.current_block, this.lexer.Location);
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x0005E10C File Offset: 0x0005C30C
		private void case_1037()
		{
			object[] array = (object[])this.yyVals[0 + this.yyTop];
			this.yyVal = new GroupBy((QueryBlock)this.current_block, (Expression)this.yyVals[-2 + this.yyTop], this.linq_clause_blocks.Pop(), (Expression)array[0], this.GetLocation(this.yyVals[-4 + this.yyTop]));
			this.current_block.SetEndLocation(this.lexer.Location);
			this.current_block = this.current_block.Parent;
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x0005E1A9 File Offset: 0x0005C3A9
		private void case_1039()
		{
			this.Error_SyntaxError(this.yyToken);
			this.yyVal = new object[]
			{
				null,
				Location.Null
			};
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x0005E1D0 File Offset: 0x0005C3D0
		private void case_1041()
		{
			((AQueryClause)this.yyVals[-1 + this.yyTop]).Tail.Next = (AQueryClause)this.yyVals[0 + this.yyTop];
			this.yyVal = this.yyVals[-1 + this.yyTop];
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x0005E224 File Offset: 0x0005C424
		private void case_1048()
		{
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-3 + this.yyTop];
			RangeVariable rangeVariable = new RangeVariable(locatedToken.Value, locatedToken.Location);
			this.yyVal = new Let((QueryBlock)this.current_block, rangeVariable, (Expression)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-4 + this.yyTop]));
			this.current_block.SetEndLocation(this.lexer.Location);
			this.current_block = this.current_block.Parent;
			((QueryBlock)this.current_block).AddRangeVariable(rangeVariable);
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x0005E2D4 File Offset: 0x0005C4D4
		private void case_1050()
		{
			this.yyVal = new Where((QueryBlock)this.current_block, (Expression)this.yyVals[0 + this.yyTop], this.GetLocation(this.yyVals[-2 + this.yyTop]));
			this.current_block.SetEndLocation(this.lexer.Location);
			this.current_block = this.current_block.Parent;
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x0005E348 File Offset: 0x0005C548
		private void case_1051()
		{
			if (this.linq_clause_blocks == null)
			{
				this.linq_clause_blocks = new Stack<QueryBlock>();
			}
			this.current_block = new QueryBlock(this.current_block, this.lexer.Location);
			this.linq_clause_blocks.Push((QueryBlock)this.current_block);
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x0005E39C File Offset: 0x0005C59C
		private void case_1052()
		{
			this.current_block.SetEndLocation(this.lexer.Location);
			this.current_block = this.current_block.Parent;
			this.current_block = new QueryBlock(this.current_block, this.lexer.Location);
			this.linq_clause_blocks.Push((QueryBlock)this.current_block);
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x0005E404 File Offset: 0x0005C604
		private void case_1053()
		{
			this.current_block.AddStatement(new ContextualReturn((Expression)this.yyVals[-1 + this.yyTop]));
			this.current_block.SetEndLocation(this.lexer.Location);
			this.current_block = this.current_block.Parent;
			this.current_block = new QueryBlock(this.current_block, this.lexer.Location);
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x0005E478 File Offset: 0x0005C678
		private void case_1054()
		{
			this.current_block.AddStatement(new ContextualReturn((Expression)this.yyVals[-1 + this.yyTop]));
			this.current_block.SetEndLocation(this.lexer.Location);
			QueryBlock outerSelector = this.linq_clause_blocks.Pop();
			QueryBlock queryBlock = this.linq_clause_blocks.Pop();
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-10 + this.yyTop];
			RangeVariable rangeVariable = new RangeVariable(locatedToken.Value, locatedToken.Location);
			RangeVariable rangeVariable2;
			if (this.yyVals[0 + this.yyTop] == null)
			{
				rangeVariable2 = rangeVariable;
				this.yyVal = new Join(queryBlock, rangeVariable, (Expression)this.yyVals[-7 + this.yyTop], outerSelector, (QueryBlock)this.current_block, this.GetLocation(this.yyVals[-11 + this.yyTop]));
			}
			else
			{
				Block parent = queryBlock.Parent;
				while (parent is QueryBlock)
				{
					parent = parent.Parent;
				}
				this.current_block.Parent = parent;
				((QueryBlock)this.current_block).AddRangeVariable(rangeVariable);
				locatedToken = (LocatedToken)this.yyVals[0 + this.yyTop];
				rangeVariable2 = new RangeVariable(locatedToken.Value, locatedToken.Location);
				this.yyVal = new GroupJoin(queryBlock, rangeVariable, (Expression)this.yyVals[-7 + this.yyTop], outerSelector, (QueryBlock)this.current_block, rangeVariable2, this.GetLocation(this.yyVals[-11 + this.yyTop]));
			}
			this.current_block = queryBlock.Parent;
			((QueryBlock)this.current_block).AddRangeVariable(rangeVariable2);
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x0005E624 File Offset: 0x0005C824
		private void case_1055()
		{
			if (this.linq_clause_blocks == null)
			{
				this.linq_clause_blocks = new Stack<QueryBlock>();
			}
			this.current_block = new QueryBlock(this.current_block, this.lexer.Location);
			this.linq_clause_blocks.Push((QueryBlock)this.current_block);
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x0005E678 File Offset: 0x0005C878
		private void case_1056()
		{
			this.current_block.SetEndLocation(this.lexer.Location);
			this.current_block = this.current_block.Parent;
			this.current_block = new QueryBlock(this.current_block, this.lexer.Location);
			this.linq_clause_blocks.Push((QueryBlock)this.current_block);
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x0005E6E0 File Offset: 0x0005C8E0
		private void case_1057()
		{
			this.current_block.AddStatement(new ContextualReturn((Expression)this.yyVals[-1 + this.yyTop]));
			this.current_block.SetEndLocation(this.lexer.Location);
			this.current_block = this.current_block.Parent;
			this.current_block = new QueryBlock(this.current_block, this.lexer.Location);
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x0005E754 File Offset: 0x0005C954
		private void case_1058()
		{
			this.current_block.AddStatement(new ContextualReturn((Expression)this.yyVals[-1 + this.yyTop]));
			this.current_block.SetEndLocation(this.lexer.Location);
			QueryBlock outerSelector = this.linq_clause_blocks.Pop();
			QueryBlock queryBlock = this.linq_clause_blocks.Pop();
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-10 + this.yyTop];
			RangeVariable rangeVariable = new RangeVariable(locatedToken.Value, locatedToken.Location);
			RangeVariable rangeVariable2;
			if (this.yyVals[0 + this.yyTop] == null)
			{
				rangeVariable2 = rangeVariable;
				this.yyVal = new Join(queryBlock, rangeVariable, (Expression)this.yyVals[-7 + this.yyTop], outerSelector, (QueryBlock)this.current_block, this.GetLocation(this.yyVals[-12 + this.yyTop]))
				{
					IdentifierType = (FullNamedExpression)this.yyVals[-11 + this.yyTop]
				};
			}
			else
			{
				Block parent = queryBlock.Parent;
				while (parent is QueryBlock)
				{
					parent = parent.Parent;
				}
				this.current_block.Parent = parent;
				((QueryBlock)this.current_block).AddRangeVariable(rangeVariable);
				locatedToken = (LocatedToken)this.yyVals[0 + this.yyTop];
				rangeVariable2 = new RangeVariable(locatedToken.Value, locatedToken.Location);
				this.yyVal = new GroupJoin(queryBlock, rangeVariable, (Expression)this.yyVals[-7 + this.yyTop], outerSelector, (QueryBlock)this.current_block, rangeVariable2, this.GetLocation(this.yyVals[-12 + this.yyTop]))
				{
					IdentifierType = (FullNamedExpression)this.yyVals[-11 + this.yyTop]
				};
			}
			this.current_block = queryBlock.Parent;
			((QueryBlock)this.current_block).AddRangeVariable(rangeVariable2);
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x0005E934 File Offset: 0x0005CB34
		private void case_1062()
		{
			this.current_block.SetEndLocation(this.lexer.Location);
			this.current_block = this.current_block.Parent;
			this.yyVal = this.yyVals[0 + this.yyTop];
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x0005E974 File Offset: 0x0005CB74
		private void case_1064()
		{
			this.current_block.SetEndLocation(this.lexer.Location);
			this.current_block = this.current_block.Parent;
			this.current_block = new QueryBlock(this.current_block, this.lexer.Location);
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x0005E9C4 File Offset: 0x0005CBC4
		private void case_1065()
		{
			((AQueryClause)this.yyVals[-3 + this.yyTop]).Next = (AQueryClause)this.yyVals[0 + this.yyTop];
			this.yyVal = this.yyVals[-3 + this.yyTop];
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x0005EA18 File Offset: 0x0005CC18
		private void case_1067()
		{
			this.current_block.SetEndLocation(this.lexer.Location);
			this.current_block = this.current_block.Parent;
			this.current_block = new QueryBlock((QueryBlock)this.current_block, this.lexer.Location);
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x0005EA70 File Offset: 0x0005CC70
		private void case_1068()
		{
			((AQueryClause)this.yyVals[-3 + this.yyTop]).Tail.Next = (AQueryClause)this.yyVals[0 + this.yyTop];
			this.yyVal = this.yyVals[-3 + this.yyTop];
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x0005EAC6 File Offset: 0x0005CCC6
		private void case_1070()
		{
			this.yyVal = new OrderByAscending((QueryBlock)this.current_block, (Expression)this.yyVals[-1 + this.yyTop]);
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x0005EAF2 File Offset: 0x0005CCF2
		private void case_1071()
		{
			this.yyVal = new OrderByDescending((QueryBlock)this.current_block, (Expression)this.yyVals[-1 + this.yyTop]);
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x0005EB1E File Offset: 0x0005CD1E
		private void case_1073()
		{
			this.yyVal = new ThenByAscending((QueryBlock)this.current_block, (Expression)this.yyVals[-1 + this.yyTop]);
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x0005EB4A File Offset: 0x0005CD4A
		private void case_1074()
		{
			this.yyVal = new ThenByDescending((QueryBlock)this.current_block, (Expression)this.yyVals[-1 + this.yyTop]);
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x0005EB78 File Offset: 0x0005CD78
		private void case_1076()
		{
			this.current_block.SetEndLocation(this.GetLocation(this.yyVals[-1 + this.yyTop]));
			this.current_block = this.current_block.Parent;
			this.current_block = new QueryBlock(this.current_block, this.lexer.Location);
			if (this.linq_clause_blocks == null)
			{
				this.linq_clause_blocks = new Stack<QueryBlock>();
			}
			this.linq_clause_blocks.Push((QueryBlock)this.current_block);
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x0005EBFC File Offset: 0x0005CDFC
		private void case_1077()
		{
			QueryBlock block = this.linq_clause_blocks.Pop();
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-2 + this.yyTop];
			RangeVariable identifier = new RangeVariable(locatedToken.Value, locatedToken.Location);
			this.yyVal = new QueryStartClause(block, null, identifier, this.GetLocation(this.yyVals[-3 + this.yyTop]))
			{
				next = (AQueryClause)this.yyVals[0 + this.yyTop]
			};
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x0005EC7C File Offset: 0x0005CE7C
		private void case_1080()
		{
			this.current_container = (this.current_type = new Class(this.current_container, new MemberName("<InteractiveExpressionClass>"), Modifiers.PUBLIC, null));
			ParametersCompiled parameters = new ParametersCompiled(new Parameter[]
			{
				new Parameter(new TypeExpression(this.compiler.BuiltinTypes.Object, Location.Null), "$retval", Parameter.Modifier.REF, null, Location.Null)
			});
			Modifiers modifiers = Modifiers.PUBLIC | Modifiers.STATIC;
			if (this.settings.Unsafe)
			{
				modifiers |= Modifiers.UNSAFE;
			}
			this.current_local_parameters = parameters;
			InteractiveMethod interactiveMethod = new InteractiveMethod(this.current_type, new TypeExpression(this.compiler.BuiltinTypes.Void, Location.Null), modifiers, parameters);
			this.current_type.AddMember(interactiveMethod);
			this.oob_stack.Push(interactiveMethod);
			this.interactive_async = new bool?(false);
			this.lexer.parsing_block++;
			this.start_block(this.lexer.Location);
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x0005ED7C File Offset: 0x0005CF7C
		private void case_1081()
		{
			this.lexer.parsing_block--;
			InteractiveMethod interactiveMethod = (InteractiveMethod)this.oob_stack.Pop();
			interactiveMethod.Block = (ToplevelBlock)this.end_block(this.lexer.Location);
			if (this.interactive_async == true)
			{
				interactiveMethod.ChangeToAsync();
			}
			this.InteractiveResult = (Class)this.pop_current_class();
			this.current_local_parameters = null;
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x0005EE08 File Offset: 0x0005D008
		private void case_1091()
		{
			this.module.DocumentationBuilder.ParsedBuiltinType = (TypeExpression)this.yyVals[-1 + this.yyTop];
			this.module.DocumentationBuilder.ParsedParameters = (List<DocumentationParameter>)this.yyVals[0 + this.yyTop];
			this.yyVal = null;
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x0005EE64 File Offset: 0x0005D064
		private void case_1092()
		{
			this.module.DocumentationBuilder.ParsedBuiltinType = new TypeExpression(this.compiler.BuiltinTypes.Void, this.GetLocation(this.yyVals[-1 + this.yyTop]));
			this.module.DocumentationBuilder.ParsedParameters = (List<DocumentationParameter>)this.yyVals[0 + this.yyTop];
			this.yyVal = null;
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x0005EED8 File Offset: 0x0005D0D8
		private void case_1093()
		{
			this.module.DocumentationBuilder.ParsedBuiltinType = (TypeExpression)this.yyVals[-3 + this.yyTop];
			this.module.DocumentationBuilder.ParsedParameters = (List<DocumentationParameter>)this.yyVals[0 + this.yyTop];
			LocatedToken locatedToken = (LocatedToken)this.yyVals[-1 + this.yyTop];
			this.yyVal = new MemberName(locatedToken.Value);
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x0005EF54 File Offset: 0x0005D154
		private void case_1096()
		{
			this.module.DocumentationBuilder.ParsedParameters = (List<DocumentationParameter>)this.yyVals[-1 + this.yyTop];
			this.yyVal = new MemberName((MemberName)this.yyVals[-6 + this.yyTop], MemberCache.IndexerNameAlias, Location.Null);
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x0005EFB0 File Offset: 0x0005D1B0
		private void case_1097()
		{
			List<DocumentationParameter> list = ((List<DocumentationParameter>)this.yyVals[0 + this.yyTop]) ?? new List<DocumentationParameter>(1);
			list.Add(new DocumentationParameter((FullNamedExpression)this.yyVals[-1 + this.yyTop]));
			this.module.DocumentationBuilder.ParsedParameters = list;
			this.module.DocumentationBuilder.ParsedOperator = new Operator.OpType?(Operator.OpType.Explicit);
			this.yyVal = null;
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x0005F02C File Offset: 0x0005D22C
		private void case_1098()
		{
			List<DocumentationParameter> list = ((List<DocumentationParameter>)this.yyVals[0 + this.yyTop]) ?? new List<DocumentationParameter>(1);
			list.Add(new DocumentationParameter((FullNamedExpression)this.yyVals[-1 + this.yyTop]));
			this.module.DocumentationBuilder.ParsedParameters = list;
			this.module.DocumentationBuilder.ParsedOperator = new Operator.OpType?(Operator.OpType.Implicit);
			this.yyVal = null;
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x0005F0A8 File Offset: 0x0005D2A8
		private void case_1099()
		{
			List<DocumentationParameter> parsedParameters = (List<DocumentationParameter>)this.yyVals[0 + this.yyTop];
			this.module.DocumentationBuilder.ParsedParameters = parsedParameters;
			this.module.DocumentationBuilder.ParsedOperator = new Operator.OpType?((Operator.OpType)this.yyVals[-1 + this.yyTop]);
			this.yyVal = null;
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x0005F10C File Offset: 0x0005D30C
		private void case_1107()
		{
			this.yyVal = new List<DocumentationParameter>
			{
				(DocumentationParameter)this.yyVals[0 + this.yyTop]
			};
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x0005F140 File Offset: 0x0005D340
		private void case_1108()
		{
			List<DocumentationParameter> list = this.yyVals[-2 + this.yyTop] as List<DocumentationParameter>;
			list.Add((DocumentationParameter)this.yyVals[0 + this.yyTop]);
			this.yyVal = list;
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x0005F184 File Offset: 0x0005D384
		private void case_1109()
		{
			if (this.yyVals[-1 + this.yyTop] != null)
			{
				this.yyVal = new DocumentationParameter((Parameter.Modifier)this.yyVals[-1 + this.yyTop], (FullNamedExpression)this.yyVals[0 + this.yyTop]);
				return;
			}
			this.yyVal = new DocumentationParameter((FullNamedExpression)this.yyVals[0 + this.yyTop]);
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x0005F1F5 File Offset: 0x0005D3F5
		private void Error_ExpectingTypeName(Expression expr)
		{
			if (expr is Invocation)
			{
				this.report.Error(1002, expr.Location, "Expecting `;'");
				return;
			}
			expr.Error_InvalidExpressionStatement(this.report);
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x0005F227 File Offset: 0x0005D427
		private void Error_ParameterModifierNotValid(string modifier, Location loc)
		{
			this.report.Error(631, loc, "The parameter modifier `{0}' is not valid in this context", modifier);
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x0005F240 File Offset: 0x0005D440
		private void Error_DuplicateParameterModifier(Location loc, Parameter.Modifier mod)
		{
			this.report.Error(1107, loc, "Duplicate parameter modifier `{0}'", Parameter.GetModifierSignature(mod));
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x0005F25E File Offset: 0x0005D45E
		private void Error_TypeExpected(Location loc)
		{
			this.report.Error(1031, loc, "Type expected");
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x0005F276 File Offset: 0x0005D476
		private void Error_UnsafeCodeNotAllowed(Location loc)
		{
			this.report.Error(227, loc, "Unsafe code requires the `unsafe' command line option to be specified");
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x0005F28E File Offset: 0x0005D48E
		private void Warning_EmptyStatement(Location loc)
		{
			this.report.Warning(642, 3, loc, "Possible mistaken empty statement");
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x0005F2A7 File Offset: 0x0005D4A7
		private void Error_NamedArgumentExpected(NamedArgument a)
		{
			this.report.Error(1738, a.Location, "Named arguments must appear after the positional arguments");
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x0005F2C4 File Offset: 0x0005D4C4
		private void Error_MissingInitializer(Location loc)
		{
			this.report.Error(210, loc, "You must provide an initializer in a fixed or using statement declaration");
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x0005F2DC File Offset: 0x0005D4DC
		private object Error_AwaitAsIdentifier(object token)
		{
			if (this.async_block)
			{
				this.report.Error(4003, this.GetLocation(token), "`await' cannot be used as an identifier within an async method or lambda expression");
				return new LocatedToken("await", this.GetLocation(token));
			}
			return token;
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x0005F318 File Offset: 0x0005D518
		private void push_current_container(TypeDefinition tc, object partial_token)
		{
			if (this.module.Evaluator != null)
			{
				tc.Definition.Modifiers = (tc.ModFlags = ((tc.ModFlags & ~(Modifiers.PROTECTED | Modifiers.PUBLIC | Modifiers.PRIVATE | Modifiers.INTERNAL)) | Modifiers.PUBLIC));
				if (this.undo == null)
				{
					this.undo = new Undo();
				}
				this.undo.AddTypeContainer(this.current_container, tc);
			}
			if (partial_token != null)
			{
				this.current_container.AddPartial(tc);
			}
			else
			{
				this.current_container.AddTypeContainer(tc);
			}
			this.lexer.parsing_declaration++;
			this.current_container = tc;
			this.current_type = tc;
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x0005F3B4 File Offset: 0x0005D5B4
		private TypeContainer pop_current_class()
		{
			TypeContainer result = this.current_container;
			this.current_container = this.current_container.Parent;
			this.current_type = (this.current_type.Parent as TypeDefinition);
			return result;
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x0005F3E3 File Offset: 0x0005D5E3
		[Conditional("FULL_AST")]
		private void StoreModifierLocation(object token, Location loc)
		{
			if (this.lbag == null)
			{
				return;
			}
			if (this.mod_locations == null)
			{
				this.mod_locations = new List<Tuple<Modifiers, Location>>();
			}
			this.mod_locations.Add(Tuple.Create<Modifiers, Location>((Modifiers)token, loc));
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x0005F418 File Offset: 0x0005D618
		[Conditional("FULL_AST")]
		private void PushLocation(Location loc)
		{
			if (this.location_stack == null)
			{
				this.location_stack = new Stack<Location>();
			}
			this.location_stack.Push(loc);
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x0005F439 File Offset: 0x0005D639
		private Location PopLocation()
		{
			if (this.location_stack == null)
			{
				return Location.Null;
			}
			return this.location_stack.Pop();
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x0005F454 File Offset: 0x0005D654
		private string CheckAttributeTarget(int token, string a, Location l)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(a);
			if (num <= 1361572173U)
			{
				if (num != 601537893U)
				{
					if (num != 1309554226U)
					{
						if (num != 1361572173U)
						{
							goto IL_C4;
						}
						if (!(a == "type"))
						{
							goto IL_C4;
						}
					}
					else if (!(a == "param"))
					{
						goto IL_C4;
					}
				}
				else if (!(a == "assembly"))
				{
					goto IL_C4;
				}
			}
			else if (num <= 2873489200U)
			{
				if (num != 1736598119U)
				{
					if (num != 2873489200U)
					{
						goto IL_C4;
					}
					if (!(a == "method"))
					{
						goto IL_C4;
					}
				}
				else if (!(a == "field"))
				{
					goto IL_C4;
				}
			}
			else if (num != 3602055880U)
			{
				if (num != 3617558685U)
				{
					goto IL_C4;
				}
				if (!(a == "module"))
				{
					goto IL_C4;
				}
			}
			else if (!(a == "property"))
			{
				goto IL_C4;
			}
			return a;
			IL_C4:
			if (!Tokenizer.IsValidIdentifier(a))
			{
				this.Error_SyntaxError(token);
			}
			else
			{
				this.report.Warning(658, 1, l, "`{0}' is invalid attribute target. All attributes in this attribute section will be ignored", a);
			}
			return string.Empty;
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x0005F553 File Offset: 0x0005D753
		private static bool IsUnaryOperator(Operator.OpType op)
		{
			switch (op)
			{
			case Operator.OpType.LogicalNot:
			case Operator.OpType.OnesComplement:
			case Operator.OpType.Increment:
			case Operator.OpType.Decrement:
			case Operator.OpType.True:
			case Operator.OpType.False:
			case Operator.OpType.UnaryPlus:
			case Operator.OpType.UnaryNegation:
				return true;
			}
			return false;
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x0005F588 File Offset: 0x0005D788
		private void syntax_error(Location l, string msg)
		{
			this.report.Error(1003, l, "Syntax error, " + msg);
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x060014CD RID: 5325 RVA: 0x0005F5A6 File Offset: 0x0005D7A6
		public Tokenizer Lexer
		{
			get
			{
				return this.lexer;
			}
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x0005F5AE File Offset: 0x0005D7AE
		public CSharpParser(SeekableStreamReader reader, CompilationSourceFile file, ParserSession session) : this(reader, file, file.Compiler.Report, session)
		{
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x0005F5C4 File Offset: 0x0005D7C4
		public CSharpParser(SeekableStreamReader reader, CompilationSourceFile file, Report report, ParserSession session)
		{
			this.file = file;
			this.current_namespace = file;
			this.current_container = file;
			this.module = file.Module;
			this.compiler = file.Compiler;
			this.settings = this.compiler.Settings;
			this.report = report;
			this.lang_version = this.settings.Version;
			this.yacc_verbose_flag = this.settings.VerboseParserFlag;
			this.doc_support = (this.settings.DocumentationFile != null);
			this.lexer = new Tokenizer(reader, file, session, report);
			this.oob_stack = new Stack<object>();
			this.lbag = session.LocationsBag;
			this.use_global_stacks = session.UseJayGlobalArrays;
			this.parameters_bucket = session.ParametersStack;
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x0005F6A4 File Offset: 0x0005D8A4
		public void parse()
		{
			this.eof_token = 257;
			try
			{
				if (this.yacc_verbose_flag > 1)
				{
					this.yyparse(this.lexer, new yyDebugSimple());
				}
				else
				{
					this.yyparse(this.lexer);
				}
				this.lexer.cleanup();
			}
			catch (Exception ex)
			{
				if (ex is yyUnexpectedEof)
				{
					this.Error_SyntaxError(this.yyToken);
					this.UnexpectedEOF = true;
				}
				else if (ex is yyException)
				{
					if (this.report.Errors == 0)
					{
						this.report.Error(-25, this.lexer.Location, "Parsing error");
					}
				}
				else
				{
					if (this.yacc_verbose_flag > 0 || ex is FatalException)
					{
						throw;
					}
					this.report.Error(589, this.lexer.Location, "Internal compiler error during parsing" + ex);
				}
			}
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x0005F794 File Offset: 0x0005D994
		private void CheckToken(int error, int yyToken, string msg, Location loc)
		{
			if (yyToken >= 260 && yyToken <= 370)
			{
				this.report.Error(error, loc, "{0}: `{1}' is a keyword", msg, CSharpParser.GetTokenName(yyToken));
				return;
			}
			this.report.Error(error, loc, msg);
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x0005F7D0 File Offset: 0x0005D9D0
		private string ConsumeStoredComment()
		{
			string result = this.tmpComment;
			this.tmpComment = null;
			this.Lexer.doc_state = XmlCommentState.Allowed;
			return result;
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x0005F7EB File Offset: 0x0005D9EB
		private void FeatureIsNotAvailable(Location loc, string feature)
		{
			this.report.FeatureIsNotAvailable(this.compiler, loc, feature);
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x0005F800 File Offset: 0x0005DA00
		private Location GetLocation(object obj)
		{
			LocatedToken locatedToken = obj as LocatedToken;
			if (locatedToken != null)
			{
				return locatedToken.Location;
			}
			MemberName memberName = obj as MemberName;
			if (memberName != null)
			{
				return memberName.Location;
			}
			Expression expression = obj as Expression;
			if (expression != null)
			{
				return expression.Location;
			}
			return this.lexer.Location;
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x0005F84C File Offset: 0x0005DA4C
		private void start_block(Location loc)
		{
			if (this.current_block == null)
			{
				this.current_block = new ToplevelBlock(this.compiler, this.current_local_parameters, loc, (Block.Flags)0);
				this.parsing_anonymous_method = false;
				return;
			}
			if (this.parsing_anonymous_method)
			{
				this.current_block = new ParametersBlock(this.current_block, this.current_local_parameters, loc, (Block.Flags)0);
				this.parsing_anonymous_method = false;
				return;
			}
			this.current_block = new ExplicitBlock(this.current_block, loc, Location.Null);
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x0005F8C4 File Offset: 0x0005DAC4
		private Block end_block(Location loc)
		{
			Block @explicit = this.current_block.Explicit;
			@explicit.SetEndLocation(loc);
			this.current_block = @explicit.Parent;
			return @explicit;
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x0005F8F4 File Offset: 0x0005DAF4
		private void start_anonymous(bool isLambda, ParametersCompiled parameters, bool isAsync, Location loc)
		{
			this.oob_stack.Push(this.current_anonymous_method);
			this.oob_stack.Push(this.current_local_parameters);
			this.oob_stack.Push(this.current_variable);
			this.oob_stack.Push(this.async_block);
			this.current_local_parameters = parameters;
			if (isLambda)
			{
				if (this.lang_version <= LanguageVersion.ISO_2)
				{
					this.FeatureIsNotAvailable(loc, "lambda expressions");
				}
				this.current_anonymous_method = new LambdaExpression(loc);
			}
			else
			{
				if (this.lang_version == LanguageVersion.ISO_1)
				{
					this.FeatureIsNotAvailable(loc, "anonymous methods");
				}
				this.current_anonymous_method = new AnonymousMethodExpression(loc);
			}
			this.async_block = isAsync;
			this.parsing_anonymous_method = true;
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x0005F9AC File Offset: 0x0005DBAC
		private AnonymousMethodExpression end_anonymous(ParametersBlock anon_block)
		{
			if (this.async_block)
			{
				anon_block.IsAsync = true;
			}
			this.current_anonymous_method.Block = anon_block;
			AnonymousMethodExpression result = this.current_anonymous_method;
			this.async_block = (bool)this.oob_stack.Pop();
			this.current_variable = (BlockVariable)this.oob_stack.Pop();
			this.current_local_parameters = (ParametersCompiled)this.oob_stack.Pop();
			this.current_anonymous_method = (AnonymousMethodExpression)this.oob_stack.Pop();
			return result;
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x0005FA32 File Offset: 0x0005DC32
		private void Error_SyntaxError(int token)
		{
			this.Error_SyntaxError(0, token);
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x0005FA3C File Offset: 0x0005DC3C
		private void Error_SyntaxError(int error_code, int token)
		{
			this.Error_SyntaxError(error_code, token, "Unexpected symbol");
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x0005FA4C File Offset: 0x0005DC4C
		private void Error_SyntaxError(int error_code, int token, string msg)
		{
			this.Lexer.CompleteOnEOF = false;
			if (token == 259)
			{
				return;
			}
			if (token == 421 && this.lexer.Location.Column == 0)
			{
				return;
			}
			string symbolName = this.GetSymbolName(token);
			string text = this.GetExpecting();
			Location loc = this.lexer.Location - symbolName.Length;
			if (error_code == 0)
			{
				if (text == "`identifier'")
				{
					if (token > 260 && token < 370)
					{
						this.report.Error(1041, loc, "Identifier expected, `{0}' is a keyword", symbolName);
						return;
					}
					error_code = 1001;
					text = "identifier";
				}
				else if (text == "`)'")
				{
					error_code = 1026;
				}
				else
				{
					error_code = 1525;
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				this.report.Error(error_code, loc, "{1} `{0}'", symbolName, msg);
				return;
			}
			this.report.Error(error_code, loc, "{2} `{0}', expecting {1}", new string[]
			{
				symbolName,
				text,
				msg
			});
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x0005FB58 File Offset: 0x0005DD58
		private string GetExpecting()
		{
			int[] array = this.yyExpectingTokens(this.yyExpectingState);
			List<string> list = new List<string>(array.Length);
			bool flag = false;
			bool flag2 = false;
			foreach (int num in array)
			{
				flag2 |= (num == 422);
				string tokenName = CSharpParser.GetTokenName(num);
				if (!(tokenName == "<internal>"))
				{
					flag |= (tokenName == "type");
					if (!list.Contains(tokenName))
					{
						list.Add(tokenName);
					}
				}
			}
			if (list.Count > 8)
			{
				return null;
			}
			if (flag && flag2)
			{
				list.Remove("identifier");
			}
			if (list.Count == 1)
			{
				return "`" + CSharpParser.GetTokenName(array[0]) + "'";
			}
			StringBuilder stringBuilder = new StringBuilder();
			list.Sort();
			int count = list.Count;
			for (int j = 0; j < count; j++)
			{
				bool flag3 = j + 1 == count;
				if (flag3)
				{
					stringBuilder.Append("or ");
				}
				stringBuilder.Append('`');
				stringBuilder.Append(list[j]);
				stringBuilder.Append(flag3 ? "'" : ((count < 3) ? "' " : "', "));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x0005FCA0 File Offset: 0x0005DEA0
		private string GetSymbolName(int token)
		{
			if (token <= 295)
			{
				if (token <= 270)
				{
					if (token == 265)
					{
						return "bool";
					}
					if (token == 267)
					{
						return "byte";
					}
					if (token == 270)
					{
						return "char";
					}
				}
				else if (token <= 279)
				{
					if (token == 275)
					{
						return "decimal";
					}
					if (token == 279)
					{
						return "double";
					}
				}
				else
				{
					if (token == 288)
					{
						return "float";
					}
					if (token == 295)
					{
						return "int";
					}
				}
			}
			else if (token <= 318)
			{
				if (token <= 304)
				{
					if (token == 300)
					{
						return "long";
					}
					if (token == 304)
					{
						return "object";
					}
				}
				else
				{
					if (token == 316)
					{
						return "sbyte";
					}
					if (token == 318)
					{
						return "short";
					}
				}
			}
			else if (token <= 334)
			{
				if (token == 322)
				{
					return "string";
				}
				switch (token)
				{
				case 330:
					return "uint";
				case 331:
					return "ulong";
				case 334:
					return "ushort";
				}
			}
			else
			{
				if (token == 337)
				{
					return "void";
				}
				switch (token)
				{
				case 382:
					return "+";
				case 383:
				case 434:
					return "-";
				case 384:
					return "!";
				case 386:
					return "<";
				case 387:
					return ">";
				case 388:
					return "&";
				case 389:
					return "|";
				case 390:
					return "*";
				case 391:
					return "%";
				case 392:
					return "/";
				case 393:
					return "^";
				case 396:
					return "++";
				case 397:
					return "--";
				case 398:
					return "<<";
				case 399:
					return ">>";
				case 400:
					return "<=";
				case 401:
					return ">=";
				case 402:
					return "==";
				case 403:
					return "!=";
				case 404:
					return "&&";
				case 405:
					return "||";
				case 406:
					return "*=";
				case 407:
					return "/=";
				case 408:
					return "%=";
				case 409:
					return "+=";
				case 410:
					return "-=";
				case 411:
					return "<<=";
				case 412:
					return ">>=";
				case 413:
					return "&=";
				case 414:
					return "^=";
				case 415:
					return "|=";
				case 416:
					return "->";
				case 417:
					return "??";
				case 421:
					return ((Constant)this.lexer.Value).GetValue().ToString();
				case 422:
					return ((LocatedToken)this.lexer.Value).Value;
				}
			}
			return CSharpParser.GetTokenName(token);
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x0005FFEC File Offset: 0x0005E1EC
		private static string GetTokenName(int token)
		{
			switch (token)
			{
			case 257:
				return "end-of-file";
			case 258:
			case 259:
			case 260:
			case 370:
			case 428:
			case 429:
			case 430:
			case 432:
			case 433:
				return "<internal>";
			case 261:
				return "abstract";
			case 262:
				return "as";
			case 263:
				return "add";
			case 264:
				return "base";
			case 265:
			case 267:
			case 270:
			case 275:
			case 279:
			case 288:
			case 295:
			case 300:
			case 304:
			case 316:
			case 318:
			case 322:
			case 330:
			case 331:
			case 334:
			case 337:
				return "type";
			case 266:
				return "break";
			case 268:
				return "case";
			case 269:
				return "catch";
			case 271:
				return "checked";
			case 272:
				return "class";
			case 273:
				return "const";
			case 274:
				return "continue";
			case 276:
				return "default";
			case 277:
				return "delegate";
			case 278:
				return "do";
			case 280:
				return "else";
			case 281:
				return "enum";
			case 282:
				return "event";
			case 283:
				return "explicit";
			case 284:
			case 358:
				return "extern";
			case 285:
				return "false";
			case 286:
				return "finally";
			case 287:
				return "fixed";
			case 289:
				return "for";
			case 290:
				return "foreach";
			case 291:
				return "goto";
			case 292:
				return "if";
			case 293:
				return "implicit";
			case 294:
				return "in";
			case 296:
				return "interface";
			case 297:
				return "internal";
			case 298:
				return "is";
			case 299:
				return "lock";
			case 301:
				return "namespace";
			case 302:
				return "new";
			case 303:
				return "null";
			case 305:
				return "operator";
			case 306:
				return "out";
			case 307:
				return "override";
			case 308:
				return "params";
			case 309:
				return "private";
			case 310:
				return "protected";
			case 311:
				return "public";
			case 312:
				return "readonly";
			case 313:
				return "ref";
			case 314:
				return "return";
			case 315:
				return "remove";
			case 317:
				return "sealed";
			case 319:
				return "sizeof";
			case 320:
				return "stackalloc";
			case 321:
				return "static";
			case 323:
				return "struct";
			case 324:
				return "switch";
			case 325:
				return "this";
			case 326:
				return "throw";
			case 327:
				return "true";
			case 328:
				return "try";
			case 329:
				return "typeof";
			case 332:
				return "unchecked";
			case 333:
				return "unsafe";
			case 335:
				return "using";
			case 336:
				return "virtual";
			case 338:
				return "volatile";
			case 339:
				return "where";
			case 340:
				return "while";
			case 341:
				return "__arglist";
			case 342:
				return "partial";
			case 343:
				return "=>";
			case 344:
			case 345:
				return "from";
			case 346:
				return "join";
			case 347:
				return "on";
			case 348:
				return "equals";
			case 349:
				return "select";
			case 350:
				return "group";
			case 351:
				return "by";
			case 352:
				return "let";
			case 353:
				return "orderby";
			case 354:
				return "ascending";
			case 355:
				return "descending";
			case 356:
				return "into";
			case 357:
			case 394:
				return "?";
			case 359:
				return "__refvalue";
			case 360:
				return "__reftype";
			case 361:
				return "__makeref";
			case 362:
				return "async";
			case 363:
			case 422:
				return "identifier";
			case 364:
			case 382:
			case 383:
			case 384:
			case 386:
			case 387:
			case 388:
			case 389:
			case 390:
			case 391:
			case 392:
			case 393:
			case 396:
			case 397:
			case 398:
			case 399:
			case 400:
			case 401:
			case 402:
			case 403:
			case 404:
			case 405:
			case 406:
			case 407:
			case 408:
			case 409:
			case 410:
			case 411:
			case 412:
			case 413:
			case 414:
			case 415:
			case 416:
			case 417:
			case 434:
				return "<operator>";
			case 365:
				return "when";
			case 366:
				return "${";
			case 367:
				return "}";
			case 368:
				return "get";
			case 369:
				return "set";
			case 371:
				return "{";
			case 372:
				return "}";
			case 373:
			case 427:
				return "[";
			case 374:
				return "]";
			case 375:
			case 423:
			case 424:
				return "(";
			case 376:
				return ")";
			case 377:
				return ".";
			case 378:
				return ",";
			case 379:
				return ":";
			case 380:
				return ";";
			case 381:
				return "~";
			case 385:
				return "=";
			case 395:
				return "::";
			case 418:
			case 425:
				return "<";
			case 420:
				return ">";
			case 421:
				return "value";
			case 426:
				return "default:";
			}
			return CSharpParser.yyNames[token];
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x00060574 File Offset: 0x0005E774
		// Note: this type is marked as 'beforefieldinit'.
		static CSharpParser()
		{
		}

		// Token: 0x040007BF RID: 1983
		private static readonly object ModifierNone = 0;

		// Token: 0x040007C0 RID: 1984
		private NamespaceContainer current_namespace;

		// Token: 0x040007C1 RID: 1985
		private TypeContainer current_container;

		// Token: 0x040007C2 RID: 1986
		private TypeDefinition current_type;

		// Token: 0x040007C3 RID: 1987
		private PropertyBase current_property;

		// Token: 0x040007C4 RID: 1988
		private EventProperty current_event;

		// Token: 0x040007C5 RID: 1989
		private EventField current_event_field;

		// Token: 0x040007C6 RID: 1990
		private FieldBase current_field;

		// Token: 0x040007C7 RID: 1991
		private Block current_block;

		// Token: 0x040007C8 RID: 1992
		private BlockVariable current_variable;

		// Token: 0x040007C9 RID: 1993
		private Delegate current_delegate;

		// Token: 0x040007CA RID: 1994
		private AnonymousMethodExpression current_anonymous_method;

		// Token: 0x040007CB RID: 1995
		private ParametersCompiled current_local_parameters;

		// Token: 0x040007CC RID: 1996
		private bool parsing_anonymous_method;

		// Token: 0x040007CD RID: 1997
		private bool async_block;

		// Token: 0x040007CE RID: 1998
		private Stack<object> oob_stack;

		// Token: 0x040007CF RID: 1999
		private int yacc_verbose_flag;

		// Token: 0x040007D0 RID: 2000
		public bool UnexpectedEOF;

		// Token: 0x040007D1 RID: 2001
		private readonly CompilationSourceFile file;

		// Token: 0x040007D2 RID: 2002
		private string tmpComment;

		// Token: 0x040007D3 RID: 2003
		private string enumTypeComment;

		// Token: 0x040007D4 RID: 2004
		private string current_attr_target;

		// Token: 0x040007D5 RID: 2005
		private CSharpParser.ParameterModifierType valid_param_mod;

		// Token: 0x040007D6 RID: 2006
		private bool default_parameter_used;

		// Token: 0x040007D7 RID: 2007
		public Class InteractiveResult;

		// Token: 0x040007D8 RID: 2008
		public Undo undo;

		// Token: 0x040007D9 RID: 2009
		private bool? interactive_async;

		// Token: 0x040007DA RID: 2010
		private Stack<QueryBlock> linq_clause_blocks;

		// Token: 0x040007DB RID: 2011
		private ModuleContainer module;

		// Token: 0x040007DC RID: 2012
		private readonly CompilerContext compiler;

		// Token: 0x040007DD RID: 2013
		private readonly LanguageVersion lang_version;

		// Token: 0x040007DE RID: 2014
		private readonly bool doc_support;

		// Token: 0x040007DF RID: 2015
		private readonly CompilerSettings settings;

		// Token: 0x040007E0 RID: 2016
		private readonly Report report;

		// Token: 0x040007E1 RID: 2017
		private List<Parameter> parameters_bucket;

		// Token: 0x040007E2 RID: 2018
		private LocationsBag lbag;

		// Token: 0x040007E3 RID: 2019
		private List<Tuple<Modifiers, Location>> mod_locations;

		// Token: 0x040007E4 RID: 2020
		private Stack<Location> location_stack;

		// Token: 0x040007E5 RID: 2021
		public TextWriter ErrorOutput = Console.Out;

		// Token: 0x040007E6 RID: 2022
		public int eof_token;

		// Token: 0x040007E7 RID: 2023
		public yyDebug debug;

		// Token: 0x040007E8 RID: 2024
		protected const int yyFinal = 7;

		// Token: 0x040007E9 RID: 2025
		protected static readonly string[] yyNames = new string[]
		{
			"end-of-file",
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			null,
			"EOF",
			"NONE",
			"ERROR",
			"FIRST_KEYWORD",
			"ABSTRACT",
			"AS",
			"ADD",
			"BASE",
			"BOOL",
			"BREAK",
			"BYTE",
			"CASE",
			"CATCH",
			"CHAR",
			"CHECKED",
			"CLASS",
			"CONST",
			"CONTINUE",
			"DECIMAL",
			"DEFAULT",
			"DELEGATE",
			"DO",
			"DOUBLE",
			"ELSE",
			"ENUM",
			"EVENT",
			"EXPLICIT",
			"EXTERN",
			"FALSE",
			"FINALLY",
			"FIXED",
			"FLOAT",
			"FOR",
			"FOREACH",
			"GOTO",
			"IF",
			"IMPLICIT",
			"IN",
			"INT",
			"INTERFACE",
			"INTERNAL",
			"IS",
			"LOCK",
			"LONG",
			"NAMESPACE",
			"NEW",
			"NULL",
			"OBJECT",
			"OPERATOR",
			"OUT",
			"OVERRIDE",
			"PARAMS",
			"PRIVATE",
			"PROTECTED",
			"PUBLIC",
			"READONLY",
			"REF",
			"RETURN",
			"REMOVE",
			"SBYTE",
			"SEALED",
			"SHORT",
			"SIZEOF",
			"STACKALLOC",
			"STATIC",
			"STRING",
			"STRUCT",
			"SWITCH",
			"THIS",
			"THROW",
			"TRUE",
			"TRY",
			"TYPEOF",
			"UINT",
			"ULONG",
			"UNCHECKED",
			"UNSAFE",
			"USHORT",
			"USING",
			"VIRTUAL",
			"VOID",
			"VOLATILE",
			"WHERE",
			"WHILE",
			"ARGLIST",
			"PARTIAL",
			"ARROW",
			"FROM",
			"FROM_FIRST",
			"JOIN",
			"ON",
			"EQUALS",
			"SELECT",
			"GROUP",
			"BY",
			"LET",
			"ORDERBY",
			"ASCENDING",
			"DESCENDING",
			"INTO",
			"INTERR_NULLABLE",
			"EXTERN_ALIAS",
			"REFVALUE",
			"REFTYPE",
			"MAKEREF",
			"ASYNC",
			"AWAIT",
			"INTERR_OPERATOR",
			"WHEN",
			"INTERPOLATED_STRING",
			"INTERPOLATED_STRING_END",
			"GET",
			"SET",
			"LAST_KEYWORD",
			"OPEN_BRACE",
			"CLOSE_BRACE",
			"OPEN_BRACKET",
			"CLOSE_BRACKET",
			"OPEN_PARENS",
			"CLOSE_PARENS",
			"DOT",
			"COMMA",
			"COLON",
			"SEMICOLON",
			"TILDE",
			"PLUS",
			"MINUS",
			"BANG",
			"ASSIGN",
			"OP_LT",
			"OP_GT",
			"BITWISE_AND",
			"BITWISE_OR",
			"STAR",
			"PERCENT",
			"DIV",
			"CARRET",
			"INTERR",
			"DOUBLE_COLON",
			"OP_INC",
			"OP_DEC",
			"OP_SHIFT_LEFT",
			"OP_SHIFT_RIGHT",
			"OP_LE",
			"OP_GE",
			"OP_EQ",
			"OP_NE",
			"OP_AND",
			"OP_OR",
			"OP_MULT_ASSIGN",
			"OP_DIV_ASSIGN",
			"OP_MOD_ASSIGN",
			"OP_ADD_ASSIGN",
			"OP_SUB_ASSIGN",
			"OP_SHIFT_LEFT_ASSIGN",
			"OP_SHIFT_RIGHT_ASSIGN",
			"OP_AND_ASSIGN",
			"OP_XOR_ASSIGN",
			"OP_OR_ASSIGN",
			"OP_PTR",
			"OP_COALESCING",
			"OP_GENERICS_LT",
			"OP_GENERICS_LT_DECL",
			"OP_GENERICS_GT",
			"LITERAL",
			"IDENTIFIER",
			"OPEN_PARENS_LAMBDA",
			"OPEN_PARENS_CAST",
			"GENERIC_DIMENSION",
			"DEFAULT_COLON",
			"OPEN_BRACKET_EXPR",
			"EVAL_STATEMENT_PARSER",
			"EVAL_COMPILATION_UNIT_PARSER",
			"EVAL_USING_DECLARATIONS_UNIT_PARSER",
			"DOC_SEE",
			"GENERATE_COMPLETION",
			"COMPLETE_COMPLETION",
			"UMINUS"
		};

		// Token: 0x040007EA RID: 2026
		private int yyExpectingState;

		// Token: 0x040007EB RID: 2027
		protected int yyMax;

		// Token: 0x040007EC RID: 2028
		private static int[] global_yyStates;

		// Token: 0x040007ED RID: 2029
		private static object[] global_yyVals;

		// Token: 0x040007EE RID: 2030
		protected bool use_global_stacks;

		// Token: 0x040007EF RID: 2031
		private object[] yyVals;

		// Token: 0x040007F0 RID: 2032
		private object yyVal;

		// Token: 0x040007F1 RID: 2033
		private int yyToken;

		// Token: 0x040007F2 RID: 2034
		private int yyTop;

		// Token: 0x040007F3 RID: 2035
		private static readonly short[] yyLhs = new short[]
		{
			-1,
			0,
			4,
			0,
			0,
			1,
			1,
			1,
			1,
			2,
			2,
			11,
			11,
			12,
			12,
			13,
			13,
			14,
			15,
			15,
			15,
			16,
			16,
			20,
			21,
			18,
			18,
			23,
			23,
			23,
			19,
			19,
			19,
			24,
			24,
			25,
			25,
			7,
			7,
			6,
			6,
			22,
			22,
			8,
			8,
			26,
			26,
			26,
			27,
			27,
			27,
			27,
			27,
			9,
			9,
			10,
			10,
			35,
			33,
			38,
			34,
			34,
			34,
			34,
			36,
			36,
			36,
			37,
			37,
			42,
			39,
			40,
			41,
			41,
			43,
			43,
			43,
			43,
			43,
			44,
			44,
			44,
			48,
			45,
			47,
			51,
			50,
			50,
			50,
			53,
			53,
			54,
			54,
			55,
			55,
			55,
			55,
			55,
			55,
			55,
			55,
			55,
			55,
			55,
			55,
			55,
			55,
			69,
			64,
			74,
			76,
			79,
			80,
			81,
			29,
			29,
			84,
			56,
			56,
			85,
			85,
			86,
			86,
			87,
			89,
			83,
			83,
			88,
			88,
			94,
			57,
			98,
			57,
			57,
			93,
			101,
			93,
			95,
			95,
			102,
			102,
			103,
			104,
			103,
			99,
			99,
			105,
			105,
			106,
			107,
			97,
			97,
			100,
			100,
			100,
			110,
			58,
			113,
			114,
			108,
			115,
			116,
			117,
			108,
			108,
			108,
			109,
			109,
			119,
			119,
			122,
			120,
			112,
			112,
			123,
			123,
			123,
			123,
			123,
			123,
			123,
			123,
			123,
			123,
			124,
			124,
			127,
			127,
			127,
			127,
			130,
			127,
			128,
			128,
			131,
			131,
			132,
			132,
			132,
			125,
			125,
			125,
			133,
			133,
			133,
			126,
			135,
			137,
			138,
			140,
			59,
			141,
			59,
			139,
			143,
			139,
			142,
			142,
			145,
			147,
			61,
			146,
			146,
			136,
			136,
			136,
			136,
			136,
			151,
			148,
			152,
			149,
			150,
			150,
			150,
			153,
			154,
			155,
			157,
			30,
			30,
			156,
			156,
			158,
			158,
			159,
			159,
			159,
			159,
			159,
			159,
			159,
			159,
			159,
			161,
			62,
			162,
			162,
			165,
			160,
			160,
			164,
			164,
			164,
			164,
			164,
			164,
			164,
			164,
			164,
			164,
			164,
			164,
			164,
			164,
			164,
			164,
			164,
			164,
			164,
			164,
			164,
			164,
			164,
			167,
			166,
			168,
			166,
			166,
			166,
			63,
			171,
			173,
			169,
			170,
			170,
			172,
			172,
			177,
			175,
			178,
			175,
			175,
			175,
			179,
			65,
			181,
			60,
			184,
			185,
			60,
			60,
			180,
			187,
			180,
			182,
			182,
			188,
			188,
			189,
			190,
			189,
			191,
			186,
			183,
			183,
			183,
			183,
			183,
			195,
			192,
			196,
			193,
			194,
			194,
			66,
			67,
			198,
			200,
			201,
			31,
			197,
			197,
			197,
			199,
			199,
			199,
			202,
			202,
			203,
			204,
			203,
			203,
			203,
			205,
			206,
			207,
			32,
			208,
			208,
			17,
			17,
			17,
			209,
			209,
			209,
			213,
			213,
			211,
			211,
			211,
			214,
			214,
			216,
			73,
			134,
			111,
			111,
			144,
			144,
			217,
			217,
			217,
			215,
			215,
			218,
			218,
			219,
			219,
			221,
			221,
			92,
			82,
			82,
			96,
			96,
			129,
			129,
			163,
			163,
			223,
			223,
			223,
			222,
			226,
			226,
			226,
			228,
			228,
			229,
			227,
			227,
			227,
			227,
			227,
			227,
			227,
			230,
			230,
			230,
			230,
			230,
			230,
			230,
			230,
			230,
			231,
			231,
			231,
			231,
			231,
			231,
			231,
			231,
			231,
			231,
			231,
			231,
			231,
			231,
			231,
			231,
			231,
			231,
			231,
			231,
			231,
			232,
			232,
			232,
			233,
			233,
			233,
			254,
			254,
			252,
			252,
			255,
			255,
			256,
			256,
			257,
			256,
			258,
			256,
			259,
			259,
			260,
			260,
			235,
			235,
			253,
			253,
			253,
			253,
			253,
			253,
			253,
			253,
			253,
			253,
			253,
			253,
			237,
			237,
			237,
			262,
			262,
			263,
			263,
			264,
			264,
			266,
			266,
			266,
			267,
			267,
			267,
			267,
			267,
			267,
			267,
			268,
			268,
			176,
			176,
			261,
			261,
			261,
			261,
			261,
			273,
			273,
			272,
			272,
			274,
			274,
			274,
			274,
			274,
			274,
			276,
			276,
			276,
			275,
			238,
			238,
			238,
			238,
			271,
			271,
			278,
			278,
			279,
			279,
			239,
			240,
			240,
			241,
			242,
			243,
			243,
			234,
			234,
			234,
			234,
			234,
			284,
			280,
			244,
			244,
			285,
			285,
			286,
			286,
			287,
			287,
			287,
			287,
			288,
			288,
			288,
			288,
			281,
			281,
			224,
			224,
			283,
			283,
			289,
			289,
			282,
			282,
			91,
			91,
			290,
			290,
			245,
			291,
			291,
			212,
			210,
			246,
			246,
			247,
			247,
			248,
			248,
			249,
			293,
			250,
			294,
			250,
			292,
			292,
			296,
			295,
			236,
			297,
			297,
			297,
			297,
			297,
			297,
			297,
			297,
			297,
			298,
			298,
			298,
			298,
			298,
			298,
			298,
			298,
			298,
			298,
			298,
			298,
			298,
			299,
			299,
			299,
			299,
			299,
			299,
			299,
			300,
			300,
			300,
			300,
			300,
			300,
			300,
			300,
			300,
			300,
			300,
			300,
			301,
			303,
			303,
			303,
			303,
			303,
			303,
			303,
			303,
			303,
			304,
			305,
			307,
			307,
			308,
			309,
			309,
			306,
			306,
			310,
			310,
			311,
			311,
			312,
			312,
			312,
			312,
			312,
			313,
			313,
			313,
			313,
			313,
			313,
			313,
			313,
			313,
			314,
			314,
			314,
			314,
			314,
			315,
			315,
			315,
			316,
			316,
			316,
			317,
			317,
			317,
			318,
			318,
			318,
			319,
			319,
			319,
			320,
			320,
			321,
			321,
			321,
			321,
			321,
			322,
			322,
			322,
			322,
			322,
			322,
			322,
			322,
			322,
			322,
			322,
			323,
			323,
			324,
			324,
			324,
			324,
			325,
			325,
			327,
			326,
			326,
			326,
			52,
			52,
			329,
			328,
			330,
			328,
			331,
			328,
			332,
			333,
			328,
			334,
			335,
			328,
			46,
			46,
			269,
			269,
			269,
			269,
			251,
			251,
			251,
			90,
			337,
			75,
			75,
			338,
			339,
			339,
			339,
			339,
			341,
			339,
			342,
			343,
			344,
			345,
			28,
			72,
			72,
			71,
			71,
			118,
			118,
			346,
			346,
			346,
			346,
			346,
			346,
			346,
			346,
			346,
			346,
			346,
			346,
			346,
			346,
			346,
			77,
			77,
			340,
			340,
			78,
			78,
			347,
			347,
			348,
			348,
			349,
			349,
			350,
			350,
			350,
			350,
			220,
			220,
			351,
			351,
			352,
			121,
			70,
			70,
			353,
			174,
			68,
			68,
			354,
			354,
			355,
			355,
			355,
			355,
			359,
			359,
			360,
			360,
			360,
			357,
			357,
			357,
			357,
			357,
			357,
			357,
			357,
			357,
			357,
			357,
			357,
			357,
			361,
			361,
			361,
			361,
			361,
			361,
			361,
			361,
			361,
			361,
			361,
			361,
			361,
			375,
			375,
			375,
			375,
			362,
			376,
			358,
			277,
			277,
			377,
			377,
			377,
			377,
			225,
			225,
			378,
			49,
			49,
			380,
			356,
			383,
			356,
			379,
			379,
			379,
			381,
			381,
			387,
			387,
			386,
			386,
			388,
			388,
			382,
			382,
			384,
			384,
			389,
			389,
			390,
			385,
			385,
			385,
			363,
			363,
			363,
			374,
			374,
			391,
			392,
			392,
			364,
			364,
			393,
			393,
			393,
			396,
			394,
			394,
			395,
			395,
			397,
			397,
			397,
			398,
			399,
			399,
			400,
			400,
			400,
			365,
			365,
			365,
			365,
			401,
			401,
			402,
			402,
			402,
			406,
			403,
			409,
			405,
			405,
			412,
			408,
			408,
			411,
			411,
			413,
			413,
			407,
			407,
			416,
			415,
			415,
			410,
			410,
			414,
			414,
			418,
			417,
			417,
			404,
			404,
			419,
			404,
			366,
			366,
			366,
			366,
			366,
			366,
			420,
			421,
			421,
			422,
			422,
			422,
			423,
			423,
			423,
			424,
			424,
			424,
			425,
			425,
			425,
			426,
			426,
			367,
			367,
			367,
			367,
			427,
			427,
			302,
			302,
			428,
			431,
			428,
			428,
			430,
			430,
			429,
			432,
			429,
			368,
			369,
			433,
			372,
			370,
			370,
			435,
			436,
			373,
			438,
			439,
			371,
			371,
			371,
			437,
			437,
			434,
			434,
			336,
			336,
			336,
			336,
			440,
			440,
			442,
			442,
			444,
			443,
			445,
			443,
			441,
			441,
			441,
			441,
			441,
			449,
			447,
			450,
			452,
			447,
			451,
			451,
			446,
			446,
			453,
			453,
			453,
			453,
			453,
			458,
			454,
			459,
			455,
			460,
			461,
			462,
			456,
			464,
			465,
			466,
			456,
			463,
			463,
			468,
			457,
			467,
			471,
			467,
			470,
			473,
			470,
			469,
			469,
			469,
			472,
			472,
			472,
			448,
			474,
			448,
			3,
			3,
			475,
			3,
			3,
			476,
			476,
			270,
			270,
			265,
			265,
			5,
			477,
			477,
			477,
			477,
			477,
			481,
			477,
			477,
			477,
			477,
			478,
			478,
			479,
			482,
			479,
			480,
			480,
			483,
			483,
			484
		};

		// Token: 0x040007F4 RID: 2036
		private static readonly short[] yyLen = new short[]
		{
			2,
			2,
			0,
			3,
			1,
			2,
			4,
			3,
			1,
			0,
			1,
			1,
			2,
			4,
			2,
			1,
			2,
			1,
			4,
			6,
			2,
			0,
			1,
			0,
			0,
			11,
			3,
			0,
			1,
			1,
			1,
			3,
			1,
			0,
			1,
			0,
			1,
			0,
			1,
			0,
			1,
			0,
			1,
			1,
			2,
			1,
			1,
			2,
			1,
			1,
			1,
			1,
			1,
			0,
			1,
			1,
			2,
			0,
			3,
			0,
			6,
			3,
			2,
			1,
			1,
			1,
			1,
			1,
			3,
			0,
			3,
			1,
			0,
			3,
			0,
			1,
			1,
			3,
			3,
			1,
			1,
			1,
			0,
			4,
			4,
			1,
			0,
			1,
			1,
			0,
			1,
			1,
			2,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			0,
			4,
			0,
			0,
			0,
			0,
			0,
			17,
			5,
			0,
			9,
			5,
			0,
			1,
			1,
			2,
			3,
			0,
			3,
			1,
			1,
			1,
			0,
			8,
			0,
			9,
			6,
			0,
			0,
			3,
			0,
			1,
			1,
			2,
			2,
			0,
			5,
			0,
			1,
			1,
			2,
			3,
			0,
			4,
			2,
			1,
			1,
			1,
			0,
			3,
			0,
			0,
			10,
			0,
			0,
			0,
			12,
			8,
			5,
			1,
			1,
			1,
			1,
			0,
			4,
			0,
			1,
			1,
			3,
			3,
			3,
			5,
			3,
			5,
			1,
			1,
			1,
			1,
			3,
			4,
			6,
			2,
			4,
			0,
			7,
			0,
			1,
			1,
			2,
			1,
			1,
			1,
			4,
			6,
			4,
			1,
			2,
			2,
			1,
			0,
			0,
			0,
			0,
			12,
			0,
			6,
			0,
			0,
			4,
			1,
			1,
			0,
			0,
			10,
			3,
			1,
			1,
			2,
			1,
			2,
			1,
			0,
			5,
			0,
			5,
			1,
			1,
			1,
			0,
			0,
			0,
			0,
			15,
			5,
			0,
			1,
			1,
			2,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			0,
			5,
			1,
			1,
			0,
			7,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			0,
			7,
			0,
			7,
			2,
			2,
			2,
			0,
			0,
			9,
			1,
			1,
			0,
			1,
			0,
			6,
			0,
			6,
			2,
			1,
			0,
			8,
			0,
			9,
			0,
			0,
			10,
			5,
			0,
			0,
			3,
			0,
			1,
			1,
			2,
			2,
			0,
			5,
			0,
			2,
			2,
			2,
			1,
			1,
			1,
			0,
			5,
			0,
			5,
			1,
			1,
			2,
			4,
			0,
			0,
			0,
			12,
			0,
			2,
			2,
			0,
			1,
			2,
			1,
			3,
			2,
			0,
			5,
			3,
			1,
			0,
			0,
			0,
			13,
			0,
			1,
			1,
			3,
			3,
			1,
			4,
			4,
			2,
			2,
			0,
			3,
			2,
			1,
			3,
			0,
			3,
			1,
			1,
			3,
			1,
			2,
			3,
			4,
			4,
			0,
			3,
			1,
			3,
			3,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			2,
			2,
			2,
			1,
			1,
			2,
			2,
			2,
			1,
			3,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			2,
			1,
			1,
			1,
			1,
			1,
			1,
			3,
			1,
			1,
			3,
			1,
			3,
			0,
			4,
			0,
			6,
			1,
			1,
			1,
			1,
			3,
			3,
			4,
			4,
			5,
			4,
			4,
			4,
			3,
			3,
			3,
			4,
			3,
			4,
			4,
			4,
			3,
			0,
			1,
			3,
			4,
			0,
			1,
			1,
			3,
			2,
			3,
			3,
			1,
			2,
			3,
			5,
			2,
			1,
			1,
			0,
			1,
			1,
			3,
			3,
			3,
			2,
			1,
			1,
			1,
			1,
			2,
			2,
			2,
			2,
			4,
			3,
			3,
			2,
			4,
			1,
			4,
			5,
			4,
			3,
			1,
			3,
			1,
			3,
			1,
			1,
			1,
			4,
			3,
			2,
			2,
			6,
			3,
			7,
			4,
			3,
			7,
			3,
			0,
			2,
			4,
			3,
			1,
			2,
			0,
			1,
			1,
			3,
			1,
			2,
			3,
			1,
			1,
			1,
			0,
			1,
			1,
			2,
			2,
			3,
			1,
			2,
			0,
			1,
			2,
			4,
			1,
			3,
			4,
			1,
			1,
			1,
			2,
			4,
			4,
			4,
			2,
			4,
			2,
			4,
			0,
			4,
			0,
			5,
			0,
			1,
			0,
			4,
			4,
			1,
			2,
			2,
			4,
			2,
			2,
			2,
			4,
			2,
			1,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			2,
			1,
			3,
			3,
			3,
			3,
			3,
			3,
			1,
			3,
			3,
			3,
			3,
			3,
			4,
			3,
			3,
			3,
			3,
			3,
			1,
			1,
			2,
			2,
			1,
			1,
			4,
			1,
			1,
			1,
			4,
			4,
			1,
			3,
			3,
			1,
			2,
			0,
			1,
			1,
			3,
			1,
			3,
			1,
			3,
			3,
			3,
			3,
			1,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			1,
			3,
			3,
			3,
			3,
			1,
			3,
			3,
			1,
			3,
			3,
			1,
			3,
			3,
			1,
			3,
			3,
			1,
			3,
			3,
			1,
			3,
			1,
			5,
			4,
			5,
			5,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			3,
			1,
			3,
			3,
			2,
			1,
			1,
			0,
			1,
			0,
			2,
			1,
			1,
			1,
			1,
			0,
			4,
			0,
			4,
			0,
			5,
			0,
			0,
			7,
			0,
			0,
			8,
			1,
			1,
			1,
			1,
			1,
			1,
			6,
			4,
			4,
			1,
			1,
			0,
			1,
			3,
			0,
			1,
			1,
			2,
			0,
			6,
			0,
			0,
			0,
			0,
			15,
			0,
			1,
			0,
			1,
			1,
			2,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			0,
			1,
			2,
			3,
			0,
			1,
			1,
			2,
			4,
			3,
			1,
			3,
			1,
			3,
			1,
			1,
			0,
			1,
			1,
			1,
			0,
			4,
			1,
			1,
			0,
			4,
			0,
			1,
			1,
			2,
			1,
			1,
			1,
			1,
			1,
			2,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			0,
			4,
			1,
			2,
			2,
			2,
			1,
			1,
			1,
			2,
			1,
			1,
			1,
			0,
			6,
			0,
			7,
			0,
			2,
			1,
			0,
			1,
			0,
			1,
			1,
			2,
			2,
			4,
			0,
			2,
			0,
			1,
			1,
			2,
			4,
			1,
			5,
			2,
			2,
			2,
			2,
			2,
			2,
			1,
			1,
			1,
			1,
			1,
			5,
			7,
			4,
			0,
			8,
			4,
			0,
			1,
			1,
			2,
			1,
			2,
			1,
			2,
			3,
			3,
			1,
			1,
			1,
			1,
			1,
			5,
			4,
			7,
			3,
			6,
			0,
			4,
			0,
			5,
			1,
			0,
			4,
			2,
			2,
			2,
			1,
			1,
			0,
			1,
			0,
			5,
			1,
			0,
			1,
			0,
			1,
			1,
			1,
			3,
			4,
			5,
			0,
			9,
			1,
			1,
			1,
			1,
			1,
			1,
			2,
			2,
			2,
			3,
			4,
			3,
			3,
			3,
			2,
			3,
			3,
			2,
			4,
			4,
			3,
			0,
			1,
			3,
			4,
			5,
			3,
			1,
			2,
			0,
			1,
			3,
			0,
			7,
			3,
			2,
			1,
			0,
			0,
			5,
			2,
			2,
			0,
			3,
			5,
			4,
			0,
			0,
			10,
			0,
			0,
			9,
			5,
			4,
			2,
			1,
			0,
			2,
			2,
			2,
			2,
			2,
			4,
			5,
			4,
			5,
			0,
			5,
			0,
			6,
			3,
			2,
			2,
			2,
			1,
			0,
			3,
			0,
			0,
			5,
			2,
			1,
			1,
			2,
			1,
			1,
			1,
			1,
			1,
			0,
			5,
			0,
			3,
			0,
			0,
			0,
			12,
			0,
			0,
			0,
			13,
			0,
			2,
			0,
			3,
			1,
			0,
			4,
			1,
			0,
			4,
			1,
			2,
			2,
			1,
			2,
			2,
			0,
			0,
			4,
			2,
			3,
			0,
			4,
			2,
			2,
			3,
			0,
			1,
			1,
			1,
			2,
			2,
			2,
			2,
			4,
			3,
			0,
			7,
			4,
			4,
			3,
			1,
			3,
			0,
			0,
			4,
			0,
			1,
			1,
			3,
			2
		};

		// Token: 0x040007F5 RID: 2037
		private static readonly short[] yyDefRed = new short[]
		{
			0,
			8,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			2,
			4,
			0,
			0,
			11,
			14,
			0,
			1078,
			0,
			0,
			1082,
			0,
			0,
			15,
			17,
			412,
			418,
			425,
			413,
			415,
			0,
			414,
			0,
			421,
			423,
			410,
			0,
			417,
			419,
			411,
			422,
			424,
			420,
			0,
			373,
			1100,
			0,
			416,
			1089,
			0,
			10,
			1,
			0,
			0,
			0,
			12,
			0,
			901,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			454,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			452,
			0,
			0,
			0,
			535,
			0,
			453,
			0,
			0,
			0,
			1000,
			0,
			0,
			0,
			745,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			456,
			806,
			0,
			855,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			451,
			0,
			734,
			0,
			900,
			0,
			838,
			0,
			447,
			863,
			862,
			0,
			0,
			0,
			427,
			428,
			429,
			430,
			431,
			432,
			433,
			434,
			435,
			436,
			437,
			438,
			439,
			440,
			441,
			442,
			443,
			444,
			445,
			446,
			449,
			450,
			741,
			0,
			607,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			742,
			740,
			743,
			744,
			822,
			824,
			0,
			820,
			823,
			839,
			841,
			842,
			843,
			844,
			845,
			846,
			847,
			848,
			849,
			850,
			840,
			0,
			0,
			902,
			903,
			921,
			922,
			923,
			924,
			958,
			959,
			960,
			961,
			962,
			963,
			0,
			0,
			0,
			20,
			22,
			0,
			1086,
			16,
			1079,
			0,
			0,
			266,
			283,
			265,
			262,
			267,
			268,
			261,
			280,
			279,
			272,
			273,
			269,
			271,
			270,
			274,
			263,
			264,
			275,
			276,
			282,
			281,
			277,
			278,
			0,
			1103,
			1092,
			0,
			0,
			1091,
			0,
			1090,
			3,
			57,
			0,
			0,
			0,
			46,
			43,
			45,
			48,
			49,
			50,
			51,
			52,
			55,
			13,
			0,
			0,
			0,
			964,
			585,
			465,
			466,
			998,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			966,
			965,
			0,
			595,
			589,
			594,
			854,
			899,
			825,
			852,
			851,
			853,
			826,
			827,
			828,
			829,
			830,
			831,
			832,
			833,
			834,
			835,
			836,
			837,
			0,
			0,
			0,
			930,
			0,
			0,
			0,
			868,
			867,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			972,
			0,
			0,
			0,
			0,
			426,
			0,
			0,
			0,
			975,
			0,
			0,
			0,
			0,
			587,
			999,
			0,
			0,
			0,
			866,
			406,
			0,
			0,
			0,
			0,
			0,
			0,
			392,
			360,
			0,
			363,
			393,
			0,
			402,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			737,
			0,
			606,
			0,
			0,
			730,
			0,
			0,
			602,
			0,
			0,
			457,
			0,
			0,
			604,
			600,
			614,
			608,
			615,
			609,
			603,
			599,
			619,
			613,
			618,
			612,
			616,
			610,
			617,
			611,
			728,
			581,
			0,
			580,
			448,
			366,
			367,
			0,
			0,
			0,
			0,
			0,
			856,
			0,
			359,
			0,
			404,
			405,
			0,
			0,
			538,
			539,
			0,
			0,
			0,
			860,
			861,
			869,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1081,
			821,
			859,
			0,
			897,
			898,
			1032,
			1049,
			0,
			0,
			1033,
			1035,
			0,
			1061,
			1018,
			1016,
			1042,
			0,
			0,
			1040,
			1043,
			1044,
			1045,
			1046,
			1019,
			1017,
			0,
			0,
			0,
			0,
			0,
			0,
			1099,
			0,
			0,
			374,
			0,
			0,
			1101,
			0,
			0,
			44,
			776,
			782,
			774,
			0,
			771,
			781,
			775,
			773,
			772,
			779,
			777,
			778,
			784,
			780,
			783,
			785,
			0,
			0,
			769,
			47,
			56,
			537,
			0,
			533,
			534,
			0,
			0,
			531,
			0,
			871,
			0,
			0,
			0,
			928,
			0,
			896,
			894,
			895,
			0,
			0,
			0,
			749,
			0,
			969,
			967,
			750,
			0,
			0,
			562,
			0,
			0,
			550,
			557,
			0,
			0,
			0,
			551,
			0,
			0,
			567,
			569,
			0,
			546,
			0,
			0,
			0,
			0,
			0,
			541,
			0,
			544,
			548,
			395,
			394,
			971,
			970,
			0,
			0,
			974,
			973,
			984,
			0,
			0,
			0,
			985,
			579,
			0,
			389,
			578,
			0,
			0,
			1001,
			0,
			0,
			865,
			0,
			0,
			400,
			401,
			0,
			0,
			0,
			399,
			0,
			0,
			0,
			620,
			0,
			0,
			591,
			0,
			732,
			638,
			637,
			0,
			0,
			0,
			461,
			0,
			455,
			819,
			0,
			0,
			814,
			816,
			817,
			818,
			469,
			470,
			0,
			370,
			371,
			0,
			197,
			196,
			198,
			0,
			719,
			0,
			0,
			0,
			396,
			0,
			714,
			0,
			0,
			978,
			0,
			0,
			0,
			477,
			478,
			0,
			481,
			0,
			0,
			0,
			0,
			479,
			0,
			0,
			528,
			0,
			485,
			0,
			0,
			0,
			0,
			511,
			514,
			0,
			0,
			506,
			513,
			512,
			0,
			703,
			704,
			705,
			706,
			707,
			708,
			709,
			710,
			711,
			713,
			712,
			624,
			621,
			626,
			623,
			625,
			622,
			635,
			632,
			636,
			0,
			0,
			646,
			0,
			0,
			0,
			0,
			0,
			639,
			0,
			634,
			647,
			648,
			630,
			0,
			631,
			0,
			665,
			0,
			0,
			666,
			0,
			672,
			0,
			673,
			0,
			674,
			0,
			675,
			0,
			679,
			0,
			680,
			0,
			683,
			0,
			686,
			0,
			689,
			0,
			692,
			0,
			695,
			0,
			697,
			0,
			566,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1031,
			1030,
			0,
			1041,
			0,
			1029,
			0,
			18,
			1097,
			1098,
			0,
			0,
			194,
			0,
			0,
			1107,
			388,
			0,
			0,
			0,
			385,
			1093,
			1095,
			63,
			65,
			66,
			0,
			0,
			58,
			0,
			0,
			67,
			69,
			32,
			30,
			0,
			0,
			0,
			766,
			0,
			770,
			475,
			0,
			536,
			0,
			584,
			0,
			597,
			183,
			205,
			0,
			0,
			0,
			173,
			0,
			0,
			0,
			184,
			590,
			0,
			1004,
			934,
			0,
			952,
			931,
			0,
			943,
			0,
			954,
			0,
			968,
			906,
			0,
			1003,
			0,
			0,
			549,
			0,
			558,
			568,
			570,
			0,
			0,
			0,
			0,
			497,
			0,
			0,
			492,
			0,
			0,
			727,
			726,
			529,
			0,
			572,
			543,
			0,
			0,
			154,
			573,
			152,
			153,
			575,
			0,
			583,
			582,
			909,
			0,
			996,
			0,
			0,
			982,
			0,
			986,
			577,
			586,
			1011,
			0,
			1007,
			926,
			0,
			0,
			1022,
			0,
			361,
			362,
			1020,
			0,
			0,
			747,
			748,
			0,
			0,
			0,
			725,
			724,
			731,
			0,
			476,
			0,
			0,
			458,
			808,
			809,
			807,
			815,
			729,
			0,
			369,
			717,
			0,
			0,
			0,
			605,
			601,
			977,
			976,
			857,
			482,
			474,
			0,
			0,
			480,
			471,
			472,
			588,
			527,
			525,
			0,
			524,
			517,
			518,
			0,
			515,
			516,
			0,
			510,
			467,
			468,
			483,
			484,
			0,
			875,
			0,
			0,
			641,
			642,
			0,
			0,
			0,
			988,
			633,
			700,
			0,
			1050,
			1024,
			0,
			1051,
			0,
			1034,
			1036,
			1047,
			0,
			1062,
			0,
			1028,
			1076,
			0,
			1109,
			195,
			1104,
			0,
			805,
			804,
			0,
			803,
			0,
			384,
			0,
			62,
			59,
			0,
			0,
			0,
			0,
			0,
			0,
			391,
			0,
			760,
			0,
			0,
			88,
			87,
			0,
			532,
			0,
			0,
			0,
			0,
			0,
			188,
			596,
			0,
			0,
			0,
			0,
			0,
			944,
			932,
			0,
			955,
			0,
			0,
			1002,
			559,
			556,
			0,
			501,
			0,
			0,
			0,
			1087,
			1088,
			488,
			494,
			0,
			498,
			0,
			0,
			0,
			0,
			0,
			0,
			907,
			0,
			992,
			0,
			989,
			983,
			1010,
			0,
			925,
			364,
			365,
			1023,
			1021,
			0,
			592,
			0,
			733,
			723,
			463,
			462,
			372,
			716,
			715,
			735,
			473,
			526,
			0,
			0,
			520,
			0,
			509,
			508,
			507,
			0,
			891,
			874,
			0,
			0,
			0,
			880,
			0,
			0,
			0,
			651,
			0,
			0,
			654,
			0,
			660,
			0,
			658,
			701,
			702,
			699,
			0,
			1026,
			0,
			1055,
			0,
			0,
			1070,
			1071,
			1064,
			0,
			19,
			1108,
			387,
			386,
			0,
			0,
			68,
			61,
			0,
			70,
			31,
			24,
			0,
			0,
			337,
			0,
			240,
			0,
			115,
			0,
			84,
			85,
			885,
			127,
			128,
			0,
			0,
			0,
			888,
			203,
			204,
			0,
			0,
			0,
			0,
			176,
			185,
			177,
			179,
			929,
			0,
			0,
			0,
			0,
			0,
			953,
			0,
			0,
			502,
			503,
			496,
			499,
			495,
			0,
			489,
			493,
			0,
			564,
			0,
			530,
			540,
			487,
			576,
			574,
			0,
			0,
			0,
			1013,
			0,
			0,
			746,
			738,
			0,
			0,
			521,
			0,
			519,
			0,
			0,
			870,
			881,
			645,
			0,
			650,
			0,
			0,
			655,
			649,
			0,
			1025,
			0,
			0,
			0,
			1039,
			0,
			1037,
			1048,
			0,
			1077,
			1096,
			0,
			81,
			0,
			0,
			75,
			76,
			79,
			80,
			0,
			354,
			343,
			342,
			0,
			761,
			236,
			110,
			0,
			872,
			889,
			189,
			0,
			201,
			0,
			0,
			0,
			927,
			1015,
			0,
			0,
			0,
			948,
			0,
			0,
			956,
			905,
			0,
			545,
			542,
			914,
			0,
			920,
			0,
			0,
			912,
			0,
			916,
			0,
			990,
			1012,
			1008,
			0,
			464,
			736,
			523,
			0,
			0,
			653,
			652,
			661,
			659,
			1027,
			1052,
			0,
			1038,
			0,
			0,
			1066,
			0,
			82,
			73,
			0,
			0,
			0,
			338,
			0,
			0,
			0,
			0,
			0,
			190,
			0,
			180,
			178,
			1005,
			945,
			933,
			941,
			940,
			935,
			937,
			0,
			500,
			0,
			908,
			913,
			0,
			917,
			997,
			0,
			0,
			739,
			0,
			883,
			0,
			1056,
			1073,
			1074,
			1067,
			60,
			0,
			77,
			78,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			755,
			0,
			787,
			0,
			752,
			890,
			187,
			0,
			200,
			0,
			0,
			957,
			919,
			918,
			994,
			0,
			991,
			1009,
			892,
			0,
			0,
			0,
			83,
			0,
			0,
			355,
			0,
			0,
			353,
			339,
			0,
			347,
			0,
			409,
			0,
			407,
			0,
			0,
			762,
			0,
			792,
			237,
			0,
			191,
			1006,
			936,
			0,
			0,
			950,
			810,
			993,
			1053,
			0,
			1068,
			0,
			0,
			0,
			335,
			0,
			0,
			753,
			789,
			0,
			758,
			0,
			0,
			793,
			0,
			111,
			939,
			938,
			0,
			0,
			1057,
			29,
			28,
			25,
			356,
			352,
			0,
			0,
			348,
			408,
			0,
			795,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			34,
			340,
			0,
			800,
			0,
			801,
			798,
			0,
			796,
			106,
			107,
			0,
			103,
			0,
			0,
			91,
			93,
			94,
			95,
			96,
			97,
			98,
			99,
			100,
			101,
			102,
			104,
			105,
			155,
			0,
			0,
			253,
			245,
			246,
			247,
			248,
			249,
			250,
			251,
			252,
			0,
			0,
			243,
			112,
			811,
			0,
			1054,
			0,
			357,
			351,
			759,
			0,
			0,
			0,
			0,
			763,
			92,
			0,
			295,
			290,
			294,
			0,
			238,
			244,
			0,
			1060,
			1058,
			799,
			797,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			304,
			0,
			0,
			254,
			0,
			0,
			260,
			0,
			170,
			169,
			156,
			166,
			167,
			168,
			0,
			0,
			0,
			108,
			0,
			0,
			289,
			0,
			0,
			288,
			0,
			160,
			0,
			0,
			378,
			336,
			0,
			376,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			764,
			0,
			239,
			113,
			118,
			116,
			311,
			0,
			375,
			0,
			0,
			0,
			0,
			131,
			0,
			0,
			0,
			0,
			0,
			0,
			165,
			157,
			0,
			0,
			0,
			218,
			0,
			379,
			0,
			255,
			0,
			0,
			0,
			0,
			308,
			0,
			286,
			133,
			0,
			284,
			0,
			0,
			0,
			135,
			0,
			380,
			0,
			0,
			207,
			212,
			0,
			0,
			0,
			377,
			258,
			171,
			114,
			126,
			124,
			0,
			0,
			313,
			0,
			0,
			0,
			0,
			0,
			161,
			0,
			292,
			0,
			0,
			0,
			0,
			139,
			0,
			0,
			0,
			0,
			381,
			382,
			0,
			0,
			0,
			0,
			0,
			121,
			328,
			0,
			309,
			0,
			0,
			322,
			0,
			0,
			0,
			317,
			0,
			151,
			0,
			0,
			0,
			0,
			146,
			0,
			0,
			305,
			0,
			136,
			0,
			130,
			140,
			158,
			164,
			227,
			0,
			208,
			0,
			0,
			219,
			0,
			125,
			0,
			117,
			122,
			0,
			0,
			0,
			324,
			0,
			325,
			314,
			0,
			0,
			307,
			318,
			287,
			0,
			0,
			132,
			147,
			285,
			0,
			303,
			0,
			293,
			297,
			142,
			0,
			0,
			0,
			224,
			226,
			0,
			259,
			123,
			329,
			331,
			310,
			0,
			0,
			323,
			320,
			150,
			148,
			162,
			302,
			0,
			0,
			0,
			159,
			228,
			230,
			209,
			0,
			222,
			220,
			0,
			0,
			322,
			0,
			298,
			300,
			143,
			0,
			0,
			0,
			0,
			333,
			334,
			330,
			332,
			321,
			163,
			0,
			0,
			234,
			233,
			232,
			229,
			231,
			214,
			210,
			221,
			0,
			0,
			0,
			299,
			301,
			216,
			217,
			0,
			215
		};

		// Token: 0x040007F6 RID: 2038
		protected static readonly short[] yyDgoto = new short[]
		{
			7,
			8,
			50,
			9,
			51,
			10,
			11,
			52,
			238,
			784,
			785,
			12,
			13,
			53,
			22,
			23,
			199,
			332,
			241,
			769,
			960,
			1181,
			1316,
			1368,
			1691,
			957,
			242,
			243,
			244,
			245,
			246,
			247,
			248,
			249,
			762,
			479,
			763,
			764,
			1078,
			765,
			766,
			1082,
			958,
			1176,
			1177,
			1178,
			274,
			651,
			1282,
			113,
			969,
			1093,
			827,
			1399,
			1400,
			1401,
			1402,
			1403,
			1404,
			1405,
			1406,
			1407,
			1408,
			1409,
			1410,
			1411,
			1412,
			1413,
			603,
			1439,
			879,
			498,
			773,
			1494,
			1092,
			1295,
			1249,
			1293,
			1330,
			1380,
			1450,
			1535,
			1325,
			1562,
			1536,
			1587,
			1588,
			1589,
			1095,
			1585,
			1096,
			836,
			961,
			1547,
			1521,
			1575,
			553,
			1568,
			1541,
			1604,
			1043,
			1573,
			1576,
			1577,
			1672,
			1605,
			1606,
			1602,
			1414,
			1473,
			1443,
			1495,
			786,
			1549,
			1651,
			1518,
			1608,
			1683,
			499,
			1474,
			1475,
			275,
			1504,
			787,
			788,
			789,
			790,
			791,
			744,
			621,
			1299,
			745,
			746,
			975,
			1497,
			1526,
			1619,
			1580,
			1653,
			1705,
			1689,
			1527,
			1714,
			1709,
			1498,
			1553,
			1679,
			1656,
			1620,
			1621,
			1702,
			1687,
			1688,
			1090,
			1248,
			1359,
			1426,
			1478,
			1427,
			1428,
			1466,
			1501,
			1467,
			335,
			228,
			1584,
			1469,
			1569,
			1566,
			1415,
			1445,
			1490,
			1648,
			1610,
			1342,
			1649,
			652,
			1697,
			1698,
			1489,
			1565,
			1538,
			1597,
			1592,
			1563,
			1629,
			1634,
			1595,
			1598,
			1599,
			1682,
			1635,
			1593,
			1594,
			1693,
			1680,
			1681,
			1087,
			1185,
			1321,
			1287,
			1350,
			1322,
			1323,
			1371,
			1245,
			1347,
			1384,
			395,
			336,
			115,
			384,
			385,
			116,
			614,
			475,
			231,
			1513,
			753,
			754,
			949,
			962,
			117,
			340,
			442,
			328,
			341,
			312,
			1326,
			1327,
			46,
			120,
			313,
			122,
			123,
			124,
			125,
			126,
			127,
			128,
			129,
			130,
			131,
			132,
			133,
			134,
			135,
			136,
			137,
			138,
			139,
			140,
			141,
			142,
			143,
			359,
			360,
			875,
			1145,
			259,
			914,
			832,
			1133,
			1122,
			820,
			999,
			821,
			822,
			1123,
			144,
			202,
			828,
			654,
			655,
			656,
			905,
			906,
			145,
			508,
			509,
			305,
			1131,
			830,
			443,
			307,
			537,
			538,
			539,
			540,
			543,
			838,
			571,
			271,
			514,
			866,
			272,
			513,
			146,
			147,
			148,
			149,
			1054,
			926,
			1055,
			689,
			690,
			1056,
			1051,
			1052,
			1057,
			1058,
			1059,
			150,
			151,
			152,
			153,
			154,
			155,
			156,
			157,
			158,
			159,
			160,
			624,
			625,
			626,
			871,
			872,
			161,
			611,
			596,
			868,
			386,
			1146,
			592,
			1223,
			162,
			528,
			1290,
			1291,
			1294,
			1375,
			1088,
			1247,
			1357,
			1470,
			500,
			1331,
			1332,
			1393,
			1394,
			950,
			361,
			1363,
			604,
			605,
			276,
			277,
			278,
			165,
			166,
			167,
			279,
			280,
			281,
			282,
			283,
			284,
			285,
			286,
			287,
			288,
			289,
			290,
			179,
			291,
			631,
			180,
			329,
			919,
			657,
			1046,
			972,
			780,
			1099,
			1044,
			1047,
			1201,
			1048,
			1100,
			1101,
			292,
			181,
			182,
			183,
			1214,
			1137,
			1215,
			1216,
			1217,
			1218,
			184,
			185,
			186,
			187,
			798,
			521,
			799,
			1204,
			1117,
			1205,
			1337,
			1302,
			1262,
			1338,
			800,
			1116,
			801,
			1340,
			1263,
			188,
			189,
			190,
			191,
			192,
			193,
			314,
			565,
			566,
			845,
			1308,
			1271,
			1010,
			325,
			1115,
			982,
			1301,
			1142,
			1016,
			1272,
			194,
			455,
			195,
			456,
			1063,
			1163,
			457,
			458,
			739,
			730,
			731,
			1168,
			1067,
			459,
			460,
			461,
			462,
			463,
			1068,
			725,
			1065,
			1276,
			1364,
			1432,
			1165,
			1312,
			1383,
			938,
			733,
			939,
			1238,
			1170,
			1239,
			1313,
			1072,
			17,
			19,
			47,
			48,
			230,
			747,
			953,
			473,
			748,
			749
		};

		// Token: 0x040007F7 RID: 2039
		protected static readonly short[] yySindex = new short[]
		{
			-104,
			0,
			-145,
			-119,
			138,
			196,
			19722,
			0,
			313,
			0,
			0,
			196,
			138,
			0,
			0,
			255,
			0,
			8816,
			196,
			0,
			250,
			-32,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			273,
			0,
			292,
			0,
			0,
			0,
			12227,
			0,
			0,
			0,
			0,
			0,
			0,
			278,
			0,
			0,
			560,
			0,
			0,
			832,
			0,
			0,
			313,
			296,
			196,
			0,
			360,
			0,
			342,
			397,
			-171,
			19138,
			-110,
			-251,
			316,
			8977,
			0,
			-251,
			-251,
			-251,
			86,
			-251,
			-251,
			-6,
			0,
			11168,
			-251,
			-251,
			0,
			11329,
			0,
			422,
			-251,
			-160,
			0,
			-251,
			448,
			-251,
			0,
			9477,
			9477,
			669,
			-251,
			-251,
			57,
			11492,
			18023,
			0,
			0,
			18023,
			0,
			12543,
			12678,
			12813,
			12948,
			13083,
			13218,
			13353,
			13488,
			0,
			192,
			0,
			10243,
			0,
			247,
			0,
			-35,
			0,
			0,
			0,
			467,
			476,
			432,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			-35,
			0,
			1040,
			907,
			245,
			614,
			682,
			815,
			474,
			483,
			608,
			651,
			-272,
			673,
			0,
			0,
			0,
			0,
			0,
			0,
			4218,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			726,
			185,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			172,
			310,
			296,
			0,
			0,
			717,
			0,
			0,
			0,
			10243,
			10243,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			278,
			0,
			0,
			697,
			722,
			0,
			-209,
			0,
			0,
			0,
			296,
			12849,
			898,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			901,
			-35,
			18163,
			0,
			0,
			0,
			0,
			0,
			18023,
			-189,
			-143,
			877,
			795,
			555,
			476,
			-35,
			0,
			0,
			10243,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			293,
			231,
			19138,
			0,
			10243,
			18023,
			818,
			0,
			0,
			844,
			18023,
			18023,
			7064,
			735,
			-134,
			821,
			10243,
			0,
			11492,
			192,
			975,
			881,
			0,
			870,
			10243,
			18023,
			0,
			1008,
			930,
			480,
			1429,
			0,
			0,
			18023,
			422,
			17463,
			0,
			0,
			448,
			18023,
			636,
			570,
			984,
			-35,
			0,
			0,
			905,
			0,
			0,
			726,
			0,
			432,
			1025,
			-35,
			18023,
			18023,
			18023,
			316,
			0,
			987,
			0,
			10243,
			10243,
			0,
			12408,
			-35,
			0,
			913,
			939,
			0,
			9138,
			79,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1582,
			0,
			0,
			0,
			0,
			19602,
			636,
			969,
			970,
			18023,
			0,
			660,
			0,
			182,
			0,
			0,
			290,
			187,
			0,
			0,
			938,
			11627,
			9460,
			0,
			0,
			0,
			18023,
			18023,
			18023,
			18023,
			18023,
			18023,
			18023,
			18023,
			18023,
			18023,
			18023,
			13623,
			13758,
			13893,
			4963,
			16188,
			14028,
			14163,
			14298,
			14433,
			14568,
			14703,
			14838,
			14973,
			15108,
			15243,
			15378,
			15513,
			15648,
			15783,
			15918,
			18583,
			18023,
			0,
			0,
			0,
			726,
			0,
			0,
			0,
			0,
			9477,
			9477,
			0,
			0,
			-35,
			0,
			0,
			0,
			0,
			326,
			1018,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			296,
			898,
			530,
			387,
			278,
			278,
			0,
			737,
			-100,
			0,
			278,
			991,
			0,
			-188,
			12849,
			0,
			0,
			0,
			0,
			-121,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			331,
			12984,
			0,
			0,
			0,
			0,
			992,
			0,
			0,
			1022,
			764,
			0,
			1035,
			0,
			1036,
			217,
			422,
			0,
			-251,
			0,
			0,
			0,
			-35,
			10387,
			-154,
			0,
			1033,
			0,
			0,
			0,
			-174,
			122,
			0,
			795,
			555,
			0,
			0,
			1031,
			0,
			1048,
			0,
			1043,
			993,
			0,
			0,
			771,
			0,
			9740,
			782,
			11788,
			821,
			17323,
			0,
			9904,
			0,
			0,
			0,
			0,
			0,
			0,
			126,
			168,
			0,
			0,
			0,
			275,
			422,
			388,
			0,
			0,
			448,
			0,
			0,
			1046,
			1051,
			0,
			177,
			-35,
			0,
			197,
			1007,
			0,
			0,
			18023,
			1136,
			660,
			0,
			18023,
			1137,
			1054,
			0,
			1058,
			1059,
			0,
			19602,
			0,
			0,
			0,
			91,
			992,
			18023,
			0,
			18023,
			0,
			0,
			-255,
			9138,
			0,
			0,
			0,
			0,
			0,
			0,
			91,
			0,
			0,
			376,
			0,
			0,
			0,
			448,
			0,
			636,
			-35,
			10404,
			0,
			1060,
			0,
			1061,
			16053,
			0,
			1180,
			1064,
			9138,
			0,
			0,
			1009,
			0,
			992,
			-35,
			18163,
			1013,
			0,
			660,
			992,
			0,
			210,
			0,
			17603,
			17603,
			1065,
			1183,
			0,
			0,
			269,
			-180,
			0,
			0,
			0,
			-114,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			18711,
			18711,
			0,
			10243,
			639,
			0,
			0,
			0,
			0,
			-35,
			0,
			0,
			0,
			0,
			907,
			0,
			907,
			0,
			12273,
			245,
			0,
			245,
			0,
			614,
			0,
			614,
			0,
			614,
			0,
			614,
			0,
			682,
			0,
			682,
			0,
			815,
			0,
			474,
			0,
			483,
			0,
			608,
			0,
			651,
			0,
			-42,
			0,
			11788,
			1162,
			-35,
			1163,
			-35,
			11788,
			11788,
			1074,
			18023,
			0,
			0,
			1018,
			0,
			-35,
			0,
			1039,
			0,
			0,
			0,
			10404,
			737,
			0,
			1086,
			1085,
			0,
			0,
			69,
			296,
			443,
			0,
			0,
			0,
			0,
			0,
			0,
			-196,
			1088,
			0,
			1087,
			1089,
			0,
			0,
			0,
			0,
			1093,
			11024,
			1049,
			0,
			439,
			0,
			0,
			789,
			0,
			18163,
			0,
			1090,
			0,
			0,
			0,
			734,
			61,
			1098,
			0,
			1099,
			1100,
			1101,
			0,
			0,
			18023,
			0,
			0,
			-35,
			0,
			0,
			1096,
			0,
			1103,
			0,
			-129,
			0,
			0,
			8977,
			0,
			8977,
			10548,
			0,
			16407,
			0,
			0,
			0,
			10711,
			10846,
			551,
			17323,
			0,
			-23,
			58,
			0,
			1050,
			1055,
			0,
			0,
			0,
			812,
			0,
			0,
			1108,
			1111,
			0,
			0,
			0,
			0,
			0,
			1114,
			0,
			0,
			0,
			1115,
			0,
			5452,
			422,
			0,
			422,
			0,
			0,
			0,
			0,
			8977,
			0,
			0,
			8977,
			660,
			0,
			18023,
			0,
			0,
			0,
			18023,
			10243,
			0,
			0,
			422,
			1117,
			91,
			0,
			0,
			0,
			18023,
			0,
			1116,
			1073,
			0,
			0,
			0,
			0,
			0,
			0,
			10243,
			0,
			0,
			-35,
			19602,
			1153,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			992,
			830,
			0,
			0,
			0,
			0,
			0,
			0,
			17603,
			0,
			0,
			0,
			-35,
			0,
			0,
			17183,
			0,
			0,
			0,
			0,
			0,
			10065,
			0,
			10226,
			1119,
			0,
			0,
			1122,
			-35,
			18839,
			0,
			0,
			0,
			11007,
			0,
			0,
			1210,
			0,
			1211,
			0,
			0,
			0,
			960,
			0,
			1128,
			0,
			0,
			501,
			0,
			0,
			0,
			737,
			0,
			0,
			1091,
			0,
			-100,
			0,
			737,
			0,
			0,
			1039,
			1133,
			1134,
			1097,
			1139,
			1049,
			0,
			1129,
			0,
			1255,
			1256,
			0,
			0,
			11788,
			0,
			17743,
			1140,
			734,
			10404,
			10243,
			0,
			0,
			434,
			1265,
			1266,
			203,
			1138,
			0,
			0,
			18023,
			0,
			18023,
			1244,
			0,
			0,
			0,
			17883,
			0,
			519,
			17883,
			838,
			0,
			0,
			0,
			0,
			9600,
			0,
			1271,
			726,
			11788,
			1157,
			10548,
			1158,
			0,
			-251,
			0,
			-35,
			0,
			0,
			0,
			-67,
			0,
			0,
			0,
			0,
			0,
			1155,
			0,
			1186,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1156,
			1151,
			0,
			856,
			0,
			0,
			0,
			10243,
			0,
			0,
			-35,
			1159,
			1119,
			0,
			18711,
			1242,
			685,
			0,
			487,
			-35,
			0,
			1165,
			0,
			1164,
			0,
			0,
			0,
			0,
			11788,
			0,
			11788,
			0,
			63,
			11788,
			0,
			0,
			0,
			481,
			0,
			0,
			0,
			0,
			1171,
			1039,
			0,
			0,
			11949,
			0,
			0,
			0,
			1177,
			5615,
			0,
			1049,
			0,
			1049,
			0,
			1049,
			0,
			0,
			0,
			0,
			0,
			-35,
			1168,
			1140,
			0,
			0,
			0,
			-144,
			-120,
			1175,
			1181,
			0,
			0,
			0,
			0,
			0,
			1178,
			10548,
			1119,
			-114,
			18023,
			0,
			1179,
			8977,
			0,
			0,
			0,
			0,
			0,
			1182,
			0,
			0,
			1188,
			0,
			821,
			0,
			0,
			0,
			0,
			0,
			-179,
			18023,
			1184,
			0,
			1119,
			1187,
			0,
			0,
			1144,
			91,
			0,
			18023,
			0,
			1142,
			1185,
			0,
			0,
			0,
			18948,
			0,
			-35,
			18948,
			0,
			0,
			18839,
			0,
			11788,
			1219,
			11788,
			0,
			11788,
			0,
			0,
			18023,
			0,
			0,
			1089,
			0,
			566,
			903,
			0,
			0,
			0,
			0,
			138,
			0,
			0,
			0,
			1200,
			0,
			0,
			0,
			1190,
			0,
			0,
			0,
			502,
			0,
			1191,
			1316,
			1323,
			0,
			0,
			1119,
			1204,
			1119,
			0,
			8977,
			665,
			0,
			0,
			17883,
			0,
			0,
			0,
			18023,
			0,
			1209,
			-195,
			0,
			8653,
			0,
			1206,
			0,
			0,
			0,
			91,
			0,
			0,
			0,
			18023,
			10226,
			0,
			0,
			0,
			0,
			0,
			0,
			1236,
			0,
			979,
			1208,
			0,
			1213,
			0,
			0,
			11949,
			196,
			217,
			0,
			873,
			1205,
			1214,
			17743,
			1216,
			0,
			18023,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			8977,
			0,
			99,
			0,
			0,
			9138,
			0,
			0,
			-69,
			8977,
			0,
			1217,
			0,
			11788,
			0,
			0,
			0,
			0,
			0,
			18023,
			0,
			0,
			296,
			1212,
			296,
			217,
			10243,
			1205,
			1253,
			0,
			1253,
			0,
			1205,
			0,
			0,
			0,
			18023,
			0,
			8977,
			18023,
			0,
			0,
			0,
			0,
			1222,
			0,
			0,
			0,
			1247,
			11788,
			18023,
			0,
			296,
			1224,
			0,
			1176,
			982,
			0,
			0,
			1221,
			0,
			1225,
			0,
			113,
			0,
			1227,
			1189,
			0,
			1253,
			0,
			0,
			1253,
			0,
			0,
			0,
			879,
			1103,
			0,
			0,
			0,
			0,
			1252,
			0,
			41,
			1253,
			1357,
			0,
			1243,
			296,
			0,
			0,
			10243,
			0,
			103,
			1245,
			0,
			1246,
			0,
			0,
			0,
			9138,
			11788,
			0,
			0,
			0,
			0,
			0,
			0,
			1229,
			1239,
			0,
			0,
			17323,
			0,
			19790,
			-161,
			296,
			1249,
			1251,
			1268,
			11788,
			1248,
			18023,
			0,
			0,
			1257,
			0,
			1250,
			0,
			0,
			1254,
			0,
			0,
			0,
			12984,
			0,
			1259,
			-161,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			-237,
			12984,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1263,
			296,
			0,
			0,
			0,
			-35,
			0,
			1268,
			0,
			0,
			0,
			1260,
			19790,
			9138,
			19381,
			0,
			0,
			606,
			0,
			0,
			0,
			19449,
			0,
			0,
			-161,
			0,
			0,
			0,
			0,
			-255,
			10243,
			10243,
			258,
			10243,
			415,
			448,
			1290,
			0,
			636,
			17084,
			0,
			1324,
			0,
			0,
			1239,
			0,
			0,
			0,
			0,
			0,
			0,
			17119,
			1239,
			1267,
			0,
			-117,
			-116,
			0,
			10243,
			-81,
			0,
			10243,
			0,
			1215,
			1269,
			0,
			0,
			156,
			0,
			115,
			1081,
			0,
			1270,
			1220,
			174,
			606,
			12227,
			0,
			18023,
			0,
			0,
			0,
			0,
			0,
			156,
			0,
			1275,
			1226,
			1272,
			1261,
			0,
			1278,
			1232,
			1281,
			217,
			1273,
			1280,
			0,
			0,
			1284,
			1292,
			1318,
			0,
			992,
			0,
			952,
			0,
			1289,
			1285,
			1239,
			-62,
			0,
			1282,
			0,
			0,
			1293,
			0,
			1294,
			1296,
			1297,
			0,
			1301,
			0,
			217,
			217,
			0,
			0,
			217,
			1291,
			1298,
			0,
			0,
			0,
			0,
			0,
			0,
			1304,
			221,
			0,
			1305,
			217,
			1428,
			1308,
			217,
			0,
			-208,
			0,
			10548,
			1276,
			1307,
			1301,
			0,
			1312,
			1313,
			249,
			1319,
			0,
			0,
			217,
			17743,
			1279,
			1310,
			1304,
			0,
			0,
			12984,
			0,
			296,
			296,
			0,
			1283,
			1315,
			1305,
			0,
			1321,
			0,
			18023,
			1288,
			1320,
			1308,
			0,
			1326,
			217,
			0,
			116,
			0,
			1322,
			0,
			0,
			0,
			0,
			0,
			12984,
			0,
			249,
			249,
			0,
			1327,
			0,
			-62,
			0,
			0,
			270,
			1339,
			12984,
			0,
			12984,
			0,
			0,
			10548,
			1328,
			0,
			0,
			0,
			1342,
			1293,
			0,
			0,
			0,
			1343,
			0,
			282,
			0,
			0,
			0,
			1253,
			1004,
			1346,
			0,
			0,
			-211,
			0,
			0,
			0,
			0,
			0,
			1406,
			1459,
			0,
			0,
			0,
			0,
			0,
			0,
			1350,
			1351,
			10548,
			0,
			0,
			0,
			0,
			249,
			0,
			0,
			652,
			652,
			0,
			1253,
			0,
			0,
			0,
			112,
			112,
			1345,
			1355,
			0,
			0,
			0,
			0,
			0,
			0,
			17323,
			17323,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1352,
			1356,
			17743,
			0,
			0,
			0,
			0,
			1354,
			0
		};

		// Token: 0x040007F8 RID: 2040
		protected static readonly short[] yyRindex = new short[]
		{
			3692,
			0,
			0,
			9299,
			3692,
			0,
			0,
			0,
			1735,
			0,
			0,
			3833,
			2158,
			0,
			0,
			0,
			0,
			0,
			3833,
			0,
			1314,
			51,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1737,
			0,
			0,
			1737,
			0,
			0,
			1737,
			0,
			0,
			1735,
			3880,
			3739,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1367,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			11185,
			0,
			1359,
			0,
			0,
			0,
			1359,
			0,
			0,
			0,
			0,
			0,
			0,
			2220,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			301,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			5840,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			6373,
			5777,
			4376,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			6481,
			6661,
			7066,
			7345,
			1617,
			7985,
			8129,
			8273,
			8417,
			5065,
			1888,
			4598,
			0,
			0,
			0,
			0,
			0,
			0,
			51,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			6553,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			3943,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1737,
			0,
			0,
			66,
			0,
			0,
			0,
			0,
			0,
			0,
			3986,
			662,
			4029,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			4636,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1368,
			0,
			0,
			0,
			0,
			0,
			0,
			4799,
			1362,
			0,
			0,
			0,
			0,
			0,
			0,
			1362,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			3121,
			0,
			95,
			3271,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			3421,
			0,
			3271,
			0,
			0,
			0,
			0,
			0,
			1367,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1015,
			0,
			0,
			143,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1370,
			1990,
			0,
			0,
			1359,
			0,
			4636,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			272,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			2354,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			4761,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			4092,
			4139,
			650,
			0,
			1737,
			1737,
			0,
			9921,
			45,
			0,
			1737,
			1743,
			0,
			0,
			158,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			457,
			19062,
			0,
			0,
			0,
			0,
			4636,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			19489,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1364,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			253,
			755,
			0,
			0,
			264,
			850,
			0,
			0,
			1376,
			751,
			0,
			0,
			0,
			0,
			228,
			0,
			0,
			5288,
			1373,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1381,
			0,
			2670,
			0,
			0,
			311,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			2971,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1370,
			0,
			0,
			0,
			18303,
			4636,
			0,
			0,
			0,
			0,
			0,
			0,
			265,
			0,
			0,
			0,
			0,
			0,
			0,
			18303,
			0,
			0,
			0,
			0,
			0,
			0,
			87,
			0,
			648,
			0,
			0,
			0,
			1379,
			0,
			0,
			0,
			0,
			1362,
			0,
			0,
			0,
			0,
			4473,
			0,
			4636,
			0,
			0,
			4309,
			0,
			4636,
			5451,
			0,
			0,
			0,
			0,
			0,
			-173,
			0,
			0,
			0,
			0,
			279,
			0,
			0,
			0,
			922,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			5937,
			6046,
			6155,
			6264,
			0,
			6733,
			0,
			0,
			0,
			0,
			6841,
			0,
			6913,
			0,
			0,
			7172,
			0,
			7244,
			0,
			7417,
			0,
			7518,
			0,
			7590,
			0,
			7691,
			0,
			7841,
			0,
			7913,
			0,
			8057,
			0,
			8201,
			0,
			8345,
			0,
			8489,
			0,
			5228,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			4761,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			11805,
			0,
			0,
			927,
			0,
			0,
			1334,
			16573,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			799,
			701,
			0,
			0,
			1384,
			0,
			0,
			0,
			0,
			2525,
			0,
			0,
			0,
			0,
			0,
			0,
			12110,
			0,
			0,
			0,
			929,
			0,
			0,
			0,
			11966,
			19642,
			0,
			0,
			944,
			948,
			949,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1382,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1392,
			0,
			0,
			0,
			0,
			0,
			7750,
			0,
			0,
			0,
			271,
			0,
			97,
			4962,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1389,
			0,
			0,
			0,
			0,
			0,
			1396,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			2971,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			18303,
			0,
			0,
			0,
			0,
			0,
			1020,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			4636,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1390,
			0,
			0,
			0,
			0,
			1393,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			-182,
			0,
			357,
			0,
			0,
			0,
			0,
			0,
			0,
			11966,
			0,
			0,
			0,
			0,
			45,
			0,
			10082,
			0,
			0,
			1398,
			0,
			882,
			0,
			0,
			0,
			0,
			1402,
			0,
			1358,
			1361,
			0,
			0,
			0,
			0,
			0,
			1394,
			17200,
			0,
			0,
			0,
			0,
			19682,
			0,
			0,
			0,
			950,
			0,
			0,
			0,
			0,
			0,
			2841,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			5125,
			0,
			5614,
			1404,
			0,
			0,
			0,
			0,
			1403,
			0,
			0,
			0,
			950,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			-153,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1401,
			0,
			0,
			0,
			0,
			0,
			2493,
			746,
			0,
			0,
			0,
			1409,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			961,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1407,
			0,
			0,
			0,
			0,
			0,
			964,
			967,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1410,
			922,
			716,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			5288,
			0,
			0,
			0,
			0,
			0,
			1416,
			0,
			0,
			0,
			1410,
			0,
			0,
			0,
			0,
			18303,
			0,
			0,
			0,
			825,
			859,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1384,
			0,
			16408,
			0,
			0,
			0,
			0,
			0,
			19838,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			791,
			0,
			867,
			0,
			0,
			0,
			0,
			1413,
			0,
			1390,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1418,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			18303,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			-170,
			395,
			0,
			0,
			0,
			0,
			0,
			19881,
			19489,
			0,
			545,
			624,
			471,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			-139,
			0,
			0,
			1381,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			19924,
			0,
			384,
			19489,
			0,
			629,
			1420,
			0,
			1420,
			0,
			624,
			0,
			0,
			0,
			0,
			0,
			0,
			891,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			19967,
			0,
			0,
			0,
			16846,
			0,
			0,
			1423,
			0,
			0,
			0,
			567,
			0,
			632,
			0,
			0,
			621,
			0,
			0,
			1420,
			0,
			0,
			0,
			0,
			897,
			0,
			0,
			0,
			0,
			0,
			0,
			3786,
			1417,
			688,
			0,
			0,
			407,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1431,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			3590,
			0,
			0,
			1373,
			0,
			0,
			16682,
			16928,
			0,
			0,
			465,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			601,
			0,
			0,
			0,
			19245,
			0,
			0,
			16764,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			19313,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			17010,
			0,
			0,
			0,
			0,
			0,
			465,
			0,
			0,
			0,
			0,
			0,
			143,
			457,
			0,
			0,
			0,
			0,
			0,
			0,
			457,
			0,
			0,
			16682,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			5774,
			459,
			0,
			1799,
			0,
			0,
			0,
			17052,
			0,
			3590,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			3590,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			-1,
			0,
			525,
			0,
			663,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			709,
			0,
			725,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			19489,
			966,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1424,
			0,
			546,
			0,
			0,
			0,
			3590,
			0,
			0,
			971,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1425,
			0,
			19489,
			19489,
			0,
			0,
			19529,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1426,
			2200,
			0,
			1427,
			19489,
			18443,
			1430,
			19489,
			0,
			0,
			0,
			0,
			0,
			0,
			1433,
			0,
			0,
			0,
			20047,
			0,
			0,
			0,
			19489,
			0,
			0,
			0,
			1435,
			0,
			0,
			332,
			0,
			1767,
			12714,
			0,
			0,
			0,
			1436,
			0,
			0,
			0,
			0,
			0,
			0,
			1437,
			0,
			0,
			19489,
			0,
			694,
			0,
			983,
			0,
			0,
			0,
			0,
			0,
			1026,
			0,
			18984,
			20009,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1485,
			0,
			1541,
			0,
			0,
			0,
			989,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			622,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			20047,
			0,
			0,
			12444,
			12579,
			0,
			622,
			0,
			0,
			0,
			0,
			0,
			16318,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1373,
			1373,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0
		};

		// Token: 0x040007F9 RID: 2041
		protected static readonly short[] yyGindex = new short[]
		{
			0,
			0,
			1758,
			0,
			0,
			0,
			4,
			-14,
			-184,
			-47,
			-43,
			0,
			1808,
			1806,
			869,
			0,
			0,
			-168,
			0,
			0,
			0,
			0,
			0,
			0,
			-1285,
			-793,
			-222,
			-627,
			0,
			0,
			0,
			0,
			0,
			-229,
			0,
			0,
			0,
			744,
			0,
			872,
			0,
			0,
			0,
			0,
			580,
			582,
			-17,
			-221,
			0,
			-46,
			0,
			0,
			813,
			379,
			0,
			426,
			-685,
			-649,
			-584,
			-581,
			-560,
			-525,
			-520,
			-514,
			0,
			0,
			-1190,
			0,
			-1292,
			0,
			375,
			-1297,
			0,
			78,
			0,
			0,
			0,
			536,
			-1243,
			0,
			0,
			0,
			431,
			209,
			0,
			0,
			0,
			248,
			-1197,
			0,
			-279,
			-305,
			-332,
			0,
			0,
			0,
			-1002,
			194,
			0,
			0,
			-537,
			0,
			0,
			261,
			0,
			0,
			234,
			0,
			0,
			339,
			0,
			-529,
			-1137,
			0,
			0,
			0,
			0,
			0,
			-483,
			274,
			-1439,
			-10,
			0,
			0,
			0,
			863,
			866,
			875,
			1062,
			-567,
			0,
			0,
			-334,
			883,
			368,
			0,
			-896,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			176,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			433,
			0,
			0,
			0,
			-306,
			361,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			444,
			0,
			-539,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			183,
			0,
			0,
			268,
			0,
			0,
			277,
			281,
			181,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			517,
			0,
			0,
			0,
			0,
			-84,
			0,
			337,
			-367,
			-359,
			1446,
			0,
			341,
			0,
			-449,
			0,
			924,
			0,
			1558,
			1173,
			-304,
			-277,
			-83,
			201,
			868,
			0,
			526,
			0,
			-41,
			854,
			-398,
			0,
			0,
			-387,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			-364,
			0,
			0,
			0,
			0,
			0,
			0,
			-264,
			0,
			0,
			1299,
			0,
			0,
			39,
			0,
			-362,
			0,
			-278,
			0,
			0,
			0,
			884,
			-941,
			-319,
			-135,
			1067,
			0,
			968,
			0,
			1258,
			-583,
			38,
			-343,
			1112,
			0,
			0,
			758,
			1819,
			0,
			0,
			0,
			0,
			1082,
			0,
			0,
			0,
			1544,
			0,
			0,
			0,
			0,
			0,
			1503,
			973,
			974,
			1474,
			-124,
			1475,
			0,
			0,
			0,
			0,
			745,
			29,
			0,
			740,
			858,
			977,
			1470,
			1472,
			1473,
			1471,
			1477,
			0,
			1479,
			0,
			0,
			0,
			1023,
			1332,
			-577,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			-309,
			671,
			0,
			-886,
			0,
			0,
			0,
			0,
			0,
			-479,
			0,
			611,
			0,
			473,
			0,
			0,
			0,
			727,
			-575,
			-15,
			-348,
			-11,
			0,
			1778,
			0,
			62,
			0,
			75,
			101,
			111,
			124,
			127,
			167,
			171,
			173,
			175,
			180,
			0,
			-762,
			0,
			0,
			0,
			829,
			0,
			747,
			0,
			0,
			0,
			718,
			-328,
			806,
			-972,
			0,
			848,
			-499,
			0,
			0,
			0,
			0,
			0,
			0,
			736,
			0,
			733,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			653,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			-30,
			0,
			1387,
			686,
			0,
			0,
			0,
			0,
			937,
			0,
			0,
			0,
			0,
			0,
			0,
			-177,
			0,
			0,
			0,
			0,
			0,
			1501,
			1223,
			0,
			0,
			0,
			0,
			1505,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			527,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			664,
			0,
			0,
			0,
			0,
			0,
			0,
			17,
			1028,
			0,
			0,
			0,
			1029
		};

		// Token: 0x040007FA RID: 2042
		protected static readonly short[] yyTable = new short[]
		{
			112,
			552,
			163,
			555,
			196,
			239,
			164,
			114,
			18,
			240,
			831,
			502,
			466,
			607,
			837,
			569,
			481,
			524,
			465,
			265,
			774,
			577,
			797,
			300,
			683,
			632,
			587,
			550,
			548,
			880,
			440,
			469,
			506,
			633,
			881,
			684,
			396,
			404,
			405,
			536,
			1150,
			653,
			333,
			343,
			988,
			1008,
			989,
			350,
			319,
			258,
			1333,
			1085,
			622,
			1297,
			1125,
			885,
			892,
			311,
			685,
			644,
			954,
			318,
			233,
			584,
			909,
			235,
			383,
			255,
			757,
			392,
			320,
			1381,
			323,
			1212,
			1069,
			1153,
			915,
			1211,
			358,
			168,
			623,
			362,
			805,
			745,
			44,
			255,
			1072,
			1387,
			1552,
			1212,
			1015,
			1360,
			169,
			1017,
			758,
			1395,
			322,
			1320,
			266,
			406,
			1440,
			269,
			802,
			522,
			1369,
			293,
			294,
			295,
			1286,
			301,
			302,
			14,
			1192,
			322,
			315,
			316,
			477,
			877,
			170,
			1447,
			321,
			324,
			544,
			326,
			256,
			330,
			759,
			986,
			171,
			915,
			346,
			347,
			1471,
			437,
			1341,
			767,
			1194,
			775,
			16,
			1507,
			1509,
			172,
			917,
			1444,
			173,
			438,
			267,
			1455,
			112,
			239,
			163,
			1324,
			1,
			467,
			164,
			114,
			750,
			1069,
			6,
			403,
			1677,
			1320,
			1069,
			97,
			1069,
			987,
			724,
			1069,
			1069,
			1072,
			1069,
			1069,
			1472,
			257,
			1072,
			1515,
			1072,
			943,
			878,
			1072,
			1072,
			506,
			1072,
			1072,
			174,
			1503,
			256,
			1306,
			175,
			1140,
			176,
			480,
			177,
			1505,
			1560,
			467,
			1069,
			178,
			916,
			380,
			97,
			745,
			806,
			745,
			256,
			745,
			504,
			507,
			1072,
			298,
			1396,
			97,
			237,
			43,
			927,
			256,
			859,
			1678,
			118,
			298,
			511,
			522,
			381,
			522,
			860,
			522,
			823,
			168,
			1153,
			382,
			873,
			1213,
			256,
			915,
			760,
			257,
			505,
			545,
			502,
			546,
			169,
			256,
			510,
			829,
			481,
			472,
			576,
			1213,
			579,
			580,
			1559,
			1069,
			265,
			257,
			2,
			373,
			607,
			535,
			622,
			396,
			745,
			118,
			265,
			1072,
			257,
			118,
			170,
			1264,
			299,
			894,
			268,
			918,
			990,
			237,
			898,
			900,
			171,
			15,
			299,
			523,
			522,
			257,
			899,
			607,
			527,
			529,
			623,
			915,
			582,
			172,
			257,
			1025,
			173,
			547,
			1628,
			896,
			843,
			1366,
			586,
			559,
			324,
			768,
			1195,
			20,
			403,
			1508,
			1510,
			572,
			1085,
			574,
			597,
			761,
			478,
			527,
			1000,
			573,
			623,
			976,
			1114,
			1166,
			1035,
			1652,
			383,
			1561,
			3,
			4,
			5,
			6,
			298,
			589,
			590,
			520,
			174,
			1662,
			348,
			1663,
			175,
			928,
			176,
			53,
			177,
			1516,
			373,
			397,
			549,
			178,
			606,
			869,
			636,
			997,
			608,
			53,
			641,
			1085,
			296,
			1304,
			507,
			507,
			1207,
			1376,
			630,
			1292,
			297,
			947,
			575,
			303,
			118,
			304,
			1699,
			1353,
			373,
			1523,
			1646,
			629,
			373,
			948,
			368,
			129,
			807,
			129,
			1240,
			265,
			839,
			1545,
			129,
			505,
			650,
			299,
			1624,
			867,
			658,
			659,
			660,
			661,
			662,
			663,
			664,
			665,
			666,
			667,
			668,
			200,
			726,
			728,
			1328,
			383,
			732,
			1104,
			1673,
			383,
			998,
			944,
			1578,
			1579,
			1167,
			6,
			1581,
			506,
			373,
			239,
			298,
			1367,
			723,
			467,
			841,
			337,
			337,
			751,
			446,
			1600,
			767,
			752,
			1607,
			851,
			237,
			767,
			1001,
			383,
			623,
			767,
			1696,
			383,
			1258,
			383,
			383,
			383,
			383,
			1623,
			337,
			298,
			397,
			383,
			368,
			854,
			767,
			609,
			996,
			1485,
			368,
			1112,
			686,
			397,
			97,
			397,
			569,
			397,
			901,
			53,
			368,
			1085,
			1135,
			1645,
			368,
			782,
			794,
			1085,
			803,
			1590,
			1305,
			299,
			349,
			767,
			1377,
			97,
			981,
			368,
			1118,
			742,
			743,
			1018,
			1524,
			1354,
			1700,
			755,
			118,
			1647,
			2,
			1019,
			808,
			1530,
			767,
			1303,
			840,
			507,
			792,
			1617,
			197,
			421,
			299,
			397,
			1309,
			447,
			610,
			389,
			1483,
			812,
			448,
			368,
			449,
			334,
			344,
			450,
			451,
			502,
			452,
			453,
			683,
			118,
			1033,
			853,
			826,
			20,
			650,
			1659,
			835,
			379,
			337,
			684,
			1669,
			1336,
			337,
			337,
			388,
			422,
			842,
			298,
			1670,
			536,
			1038,
			515,
			298,
			380,
			622,
			852,
			846,
			793,
			502,
			506,
			783,
			796,
			685,
			390,
			118,
			1484,
			857,
			444,
			446,
			868,
			861,
			1225,
			49,
			198,
			942,
			855,
			381,
			884,
			812,
			1199,
			203,
			1113,
			623,
			874,
			734,
			358,
			902,
			1660,
			870,
			380,
			778,
			606,
			237,
			895,
			507,
			608,
			237,
			767,
			1531,
			204,
			404,
			405,
			490,
			870,
			844,
			517,
			634,
			454,
			337,
			1671,
			770,
			639,
			381,
			518,
			771,
			1063,
			635,
			868,
			606,
			382,
			445,
			640,
			608,
			505,
			237,
			118,
			383,
			868,
			391,
			423,
			424,
			904,
			904,
			868,
			337,
			516,
			470,
			471,
			560,
			813,
			868,
			1102,
			843,
			925,
			560,
			491,
			337,
			912,
			1273,
			767,
			504,
			447,
			256,
			1065,
			337,
			229,
			448,
			505,
			449,
			563,
			337,
			450,
			451,
			490,
			452,
			453,
			519,
			447,
			1097,
			637,
			623,
			237,
			448,
			1486,
			449,
			772,
			847,
			450,
			451,
			55,
			452,
			453,
			868,
			931,
			823,
			933,
			907,
			907,
			868,
			390,
			1002,
			337,
			337,
			270,
			941,
			880,
			1418,
			1040,
			1063,
			560,
			813,
			257,
			512,
			1063,
			913,
			1063,
			491,
			504,
			1063,
			1063,
			826,
			1063,
			1063,
			964,
			505,
			826,
			826,
			251,
			937,
			638,
			337,
			252,
			1487,
			1059,
			118,
			337,
			1654,
			1655,
			522,
			1130,
			1134,
			765,
			1419,
			766,
			507,
			390,
			1065,
			965,
			562,
			446,
			555,
			1065,
			250,
			1065,
			1418,
			464,
			1065,
			1065,
			558,
			1065,
			1065,
			563,
			983,
			1398,
			1417,
			765,
			882,
			766,
			344,
			683,
			337,
			735,
			683,
			505,
			966,
			683,
			578,
			535,
			564,
			741,
			684,
			253,
			265,
			684,
			507,
			1398,
			684,
			1106,
			527,
			254,
			1419,
			346,
			765,
			1690,
			766,
			594,
			595,
			337,
			337,
			1200,
			761,
			393,
			1063,
			685,
			835,
			97,
			685,
			1420,
			883,
			685,
			1421,
			826,
			1417,
			650,
			383,
			797,
			1059,
			118,
			53,
			237,
			1203,
			1059,
			751,
			1059,
			613,
			1200,
			1059,
			1059,
			337,
			1059,
			1059,
			1422,
			447,
			951,
			327,
			1398,
			393,
			448,
			829,
			449,
			1065,
			53,
			450,
			451,
			118,
			452,
			453,
			1013,
			1388,
			1014,
			327,
			1030,
			397,
			1020,
			751,
			1420,
			394,
			1021,
			1421,
			118,
			118,
			963,
			751,
			256,
			676,
			398,
			1423,
			1026,
			1023,
			327,
			870,
			1424,
			1180,
			1036,
			433,
			952,
			507,
			1425,
			1158,
			1422,
			375,
			607,
			507,
			121,
			399,
			400,
			45,
			1251,
			434,
			1050,
			578,
			727,
			729,
			1073,
			380,
			265,
			754,
			119,
			362,
			1252,
			1139,
			383,
			201,
			1124,
			401,
			650,
			379,
			1171,
			375,
			1005,
			1059,
			650,
			257,
			835,
			1423,
			402,
			751,
			381,
			788,
			1424,
			752,
			379,
			761,
			1062,
			382,
			1425,
			121,
			740,
			754,
			383,
			121,
			383,
			607,
			383,
			201,
			368,
			383,
			380,
			383,
			393,
			119,
			337,
			1159,
			383,
			119,
			1265,
			767,
			229,
			995,
			232,
			788,
			767,
			794,
			907,
			788,
			767,
			794,
			1097,
			380,
			578,
			381,
			1471,
			380,
			1241,
			826,
			1511,
			523,
			382,
			342,
			342,
			767,
			337,
			327,
			380,
			686,
			786,
			791,
			790,
			925,
			1525,
			756,
			381,
			1119,
			757,
			794,
			381,
			1300,
			1121,
			382,
			97,
			1121,
			342,
			382,
			794,
			1496,
			383,
			381,
			767,
			1472,
			383,
			826,
			1543,
			835,
			382,
			791,
			790,
			1496,
			786,
			393,
			435,
			383,
			1151,
			756,
			791,
			790,
			757,
			767,
			368,
			211,
			118,
			925,
			118,
			923,
			368,
			425,
			426,
			924,
			607,
			1500,
			774,
			337,
			121,
			1335,
			368,
			1180,
			97,
			718,
			368,
			718,
			368,
			1500,
			327,
			368,
			380,
			1386,
			119,
			206,
			507,
			1611,
			1259,
			368,
			1085,
			615,
			1260,
			973,
			615,
			345,
			1261,
			826,
			616,
			826,
			1138,
			616,
			826,
			1189,
			118,
			381,
			436,
			118,
			1156,
			1193,
			617,
			349,
			382,
			617,
			1157,
			1179,
			296,
			349,
			439,
			427,
			428,
			368,
			342,
			342,
			350,
			296,
			71,
			71,
			337,
			381,
			71,
			373,
			337,
			429,
			430,
			373,
			382,
			368,
			373,
			947,
			373,
			502,
			607,
			947,
			481,
			373,
			967,
			947,
			835,
			1664,
			304,
			527,
			1315,
			968,
			306,
			118,
			306,
			1435,
			337,
			1465,
			541,
			306,
			1050,
			922,
			542,
			265,
			1477,
			474,
			265,
			987,
			368,
			265,
			1219,
			987,
			555,
			987,
			118,
			1522,
			867,
			373,
			555,
			368,
			1226,
			368,
			867,
			1468,
			1686,
			870,
			342,
			777,
			468,
			867,
			1468,
			778,
			1522,
			476,
			813,
			826,
			121,
			826,
			814,
			826,
			368,
			368,
			1237,
			348,
			555,
			824,
			503,
			1707,
			1708,
			542,
			119,
			1554,
			342,
			1555,
			186,
			1186,
			186,
			1187,
			186,
			1188,
			368,
			356,
			368,
			368,
			342,
			368,
			368,
			64,
			368,
			121,
			337,
			368,
			342,
			1229,
			1244,
			1004,
			1231,
			867,
			342,
			1005,
			1121,
			551,
			686,
			119,
			523,
			686,
			507,
			525,
			686,
			337,
			893,
			606,
			893,
			1034,
			893,
			608,
			229,
			778,
			234,
			1274,
			835,
			1126,
			870,
			449,
			121,
			916,
			431,
			432,
			337,
			342,
			342,
			561,
			337,
			526,
			449,
			1179,
			449,
			561,
			119,
			1285,
			556,
			1149,
			523,
			916,
			882,
			523,
			882,
			239,
			882,
			1318,
			199,
			467,
			199,
			1319,
			199,
			449,
			449,
			1288,
			342,
			557,
			1361,
			1289,
			606,
			342,
			1362,
			72,
			608,
			394,
			826,
			72,
			339,
			339,
			949,
			560,
			1314,
			449,
			949,
			239,
			951,
			501,
			237,
			467,
			951,
			449,
			1012,
			682,
			449,
			581,
			1242,
			1097,
			1243,
			523,
			561,
			339,
			701,
			703,
			705,
			707,
			342,
			119,
			598,
			599,
			337,
			1022,
			826,
			1237,
			418,
			419,
			420,
			873,
			1106,
			873,
			1106,
			1318,
			600,
			601,
			884,
			1319,
			884,
			561,
			337,
			337,
			1029,
			1069,
			1070,
			342,
			342,
			174,
			585,
			174,
			118,
			181,
			182,
			181,
			182,
			1014,
			583,
			1014,
			507,
			593,
			1397,
			1416,
			1278,
			1279,
			1319,
			752,
			74,
			205,
			74,
			205,
			175,
			482,
			175,
			134,
			627,
			134,
			826,
			606,
			312,
			628,
			312,
			608,
			1397,
			1349,
			237,
			118,
			1319,
			650,
			118,
			642,
			141,
			118,
			141,
			756,
			483,
			826,
			319,
			523,
			319,
			381,
			474,
			1674,
			1675,
			738,
			121,
			339,
			339,
			484,
			337,
			1416,
			459,
			459,
			486,
			752,
			1451,
			460,
			460,
			487,
			119,
			488,
			489,
			490,
			491,
			767,
			767,
			692,
			694,
			492,
			697,
			699,
			776,
			493,
			1397,
			1713,
			118,
			1105,
			1319,
			709,
			711,
			381,
			779,
			781,
			804,
			494,
			337,
			809,
			495,
			118,
			496,
			810,
			811,
			849,
			337,
			606,
			407,
			812,
			850,
			608,
			856,
			858,
			862,
			863,
			1476,
			864,
			865,
			890,
			887,
			886,
			911,
			910,
			893,
			339,
			497,
			891,
			897,
			408,
			409,
			410,
			411,
			412,
			413,
			414,
			415,
			416,
			417,
			930,
			932,
			121,
			936,
			342,
			387,
			945,
			946,
			118,
			578,
			955,
			956,
			339,
			118,
			959,
			43,
			119,
			118,
			977,
			971,
			984,
			978,
			979,
			980,
			554,
			985,
			1003,
			200,
			1006,
			121,
			1009,
			1534,
			339,
			916,
			342,
			1476,
			1007,
			1024,
			1028,
			1027,
			1032,
			1045,
			1049,
			119,
			121,
			121,
			118,
			1510,
			1064,
			1066,
			1071,
			1080,
			1086,
			1081,
			1084,
			1089,
			1091,
			1075,
			119,
			119,
			1591,
			1184,
			1098,
			1083,
			752,
			1110,
			1111,
			1114,
			1120,
			339,
			339,
			1129,
			545,
			1144,
			1136,
			1143,
			1147,
			1618,
			338,
			338,
			1148,
			752,
			929,
			1152,
			1155,
			1160,
			1161,
			934,
			935,
			1172,
			1630,
			1632,
			1190,
			342,
			752,
			752,
			1182,
			1196,
			339,
			1206,
			835,
			338,
			1198,
			1197,
			1220,
			1476,
			1209,
			1222,
			118,
			1224,
			1234,
			1208,
			523,
			1227,
			1228,
			1246,
			1254,
			1618,
			1618,
			1250,
			1253,
			752,
			752,
			1255,
			1256,
			1266,
			1270,
			1277,
			1289,
			1640,
			1280,
			1281,
			1317,
			1288,
			1298,
			1310,
			1329,
			1341,
			339,
			1343,
			1346,
			357,
			1348,
			1351,
			1365,
			1352,
			1355,
			364,
			366,
			368,
			370,
			372,
			374,
			376,
			378,
			1356,
			342,
			1370,
			1385,
			1372,
			1378,
			1379,
			835,
			1386,
			1429,
			339,
			339,
			1430,
			1431,
			1437,
			337,
			1488,
			1434,
			1502,
			1618,
			1441,
			1438,
			1436,
			752,
			1448,
			1453,
			1519,
			342,
			1506,
			118,
			1540,
			1529,
			1528,
			1520,
			338,
			1537,
			1539,
			1531,
			338,
			338,
			507,
			507,
			1542,
			1510,
			835,
			1544,
			1548,
			1546,
			1550,
			121,
			1471,
			121,
			1551,
			1557,
			1558,
			1567,
			1564,
			1582,
			1570,
			1692,
			1692,
			1571,
			1572,
			119,
			1583,
			119,
			1701,
			1701,
			1574,
			650,
			650,
			1586,
			1596,
			1601,
			567,
			1603,
			1613,
			1615,
			1616,
			1626,
			337,
			1712,
			1622,
			24,
			1637,
			25,
			1639,
			1612,
			26,
			1642,
			1625,
			1644,
			1657,
			27,
			1636,
			121,
			1650,
			28,
			121,
			1641,
			1661,
			342,
			1665,
			337,
			338,
			1666,
			30,
			1676,
			1668,
			119,
			1660,
			1659,
			119,
			32,
			1684,
			1685,
			1706,
			1710,
			33,
			1704,
			342,
			1711,
			34,
			1715,
			9,
			21,
			1102,
			593,
			979,
			553,
			338,
			980,
			1094,
			942,
			36,
			720,
			37,
			554,
			504,
			342,
			38,
			995,
			338,
			342,
			721,
			802,
			121,
			35,
			39,
			40,
			338,
			946,
			41,
			552,
			505,
			568,
			338,
			35,
			656,
			876,
			119,
			36,
			341,
			886,
			337,
			36,
			337,
			682,
			987,
			235,
			877,
			1094,
			109,
			337,
			657,
			878,
			887,
			910,
			879,
			911,
			790,
			119,
			337,
			337,
			345,
			337,
			790,
			338,
			338,
			767,
			368,
			1499,
			812,
			767,
			137,
			119,
			315,
			1392,
			236,
			144,
			21,
			357,
			138,
			1499,
			120,
			316,
			145,
			1132,
			1499,
			54,
			337,
			1173,
			1283,
			337,
			1284,
			1442,
			338,
			1079,
			1479,
			1480,
			1334,
			338,
			1499,
			1658,
			1667,
			1627,
			1614,
			612,
			1643,
			1532,
			1107,
			342,
			342,
			1108,
			1609,
			974,
			24,
			588,
			25,
			1512,
			387,
			26,
			1109,
			339,
			1499,
			1103,
			27,
			357,
			1446,
			1449,
			28,
			1694,
			1533,
			1703,
			1695,
			1638,
			338,
			1373,
			1392,
			30,
			1633,
			1556,
			676,
			1631,
			1076,
			1162,
			32,
			1164,
			570,
			1374,
			1169,
			33,
			994,
			1041,
			1128,
			34,
			1481,
			1482,
			1210,
			970,
			306,
			591,
			991,
			338,
			338,
			687,
			688,
			36,
			876,
			37,
			1232,
			1230,
			713,
			38,
			908,
			715,
			719,
			717,
			1031,
			342,
			1454,
			39,
			40,
			721,
			1514,
			41,
			722,
			1517,
			85,
			1296,
			670,
			672,
			674,
			867,
			338,
			588,
			588,
			588,
			588,
			588,
			588,
			588,
			588,
			588,
			588,
			588,
			588,
			588,
			588,
			588,
			588,
			1358,
			441,
			1268,
			1202,
			1275,
			1221,
			1191,
			1257,
			1269,
			1267,
			848,
			1141,
			342,
			1339,
			676,
			1307,
			736,
			940,
			1452,
			676,
			737,
			676,
			676,
			676,
			676,
			676,
			676,
			676,
			676,
			676,
			676,
			676,
			121,
			1074,
			1233,
			1345,
			1235,
			0,
			1236,
			1077,
			0,
			676,
			676,
			0,
			0,
			0,
			119,
			676,
			368,
			676,
			0,
			676,
			0,
			676,
			676,
			676,
			0,
			0,
			0,
			0,
			0,
			0,
			387,
			676,
			676,
			0,
			0,
			682,
			676,
			676,
			682,
			0,
			0,
			682,
			0,
			339,
			0,
			676,
			676,
			676,
			676,
			119,
			0,
			0,
			119,
			0,
			53,
			119,
			0,
			0,
			0,
			0,
			676,
			0,
			339,
			0,
			338,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			588,
			0,
			676,
			53,
			0,
			0,
			0,
			339,
			0,
			0,
			121,
			0,
			368,
			0,
			0,
			0,
			53,
			0,
			0,
			0,
			338,
			53,
			0,
			121,
			119,
			0,
			53,
			0,
			53,
			53,
			53,
			53,
			0,
			0,
			53,
			368,
			53,
			119,
			0,
			0,
			53,
			1311,
			0,
			0,
			0,
			0,
			0,
			0,
			368,
			0,
			0,
			0,
			53,
			368,
			0,
			53,
			368,
			53,
			368,
			0,
			368,
			368,
			368,
			368,
			0,
			0,
			0,
			0,
			368,
			121,
			0,
			0,
			368,
			0,
			121,
			0,
			368,
			1344,
			121,
			338,
			0,
			53,
			889,
			119,
			368,
			0,
			0,
			368,
			119,
			368,
			0,
			326,
			119,
			0,
			0,
			0,
			696,
			0,
			0,
			0,
			339,
			0,
			0,
			0,
			0,
			0,
			0,
			121,
			368,
			342,
			40,
			0,
			0,
			368,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			119,
			0,
			368,
			368,
			0,
			291,
			0,
			368,
			1382,
			0,
			0,
			0,
			920,
			921,
			0,
			0,
			0,
			338,
			0,
			0,
			368,
			338,
			0,
			0,
			0,
			0,
			0,
			1433,
			0,
			0,
			357,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			554,
			338,
			121,
			0,
			0,
			403,
			368,
			342,
			0,
			0,
			0,
			0,
			696,
			0,
			0,
			0,
			119,
			696,
			0,
			696,
			696,
			696,
			696,
			696,
			696,
			696,
			696,
			696,
			696,
			696,
			342,
			368,
			0,
			0,
			0,
			0,
			0,
			368,
			0,
			696,
			696,
			0,
			0,
			0,
			339,
			696,
			0,
			696,
			0,
			696,
			0,
			696,
			696,
			696,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			696,
			0,
			0,
			0,
			0,
			0,
			368,
			0,
			338,
			0,
			0,
			121,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			342,
			119,
			342,
			338,
			0,
			0,
			0,
			0,
			0,
			342,
			0,
			0,
			357,
			0,
			0,
			696,
			0,
			0,
			342,
			342,
			0,
			342,
			338,
			368,
			0,
			0,
			338,
			368,
			368,
			0,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			0,
			0,
			0,
			0,
			342,
			368,
			565,
			342,
			368,
			368,
			0,
			0,
			0,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			0,
			368,
			368,
			0,
			0,
			368,
			368,
			368,
			368,
			368,
			0,
			0,
			368,
			368,
			0,
			0,
			0,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			0,
			0,
			0,
			0,
			0,
			0,
			338,
			0,
			0,
			0,
			0,
			368,
			0,
			0,
			368,
			0,
			368,
			0,
			368,
			40,
			0,
			368,
			0,
			40,
			338,
			338,
			0,
			368,
			0,
			0,
			0,
			0,
			0,
			0,
			40,
			0,
			0,
			0,
			0,
			40,
			0,
			0,
			0,
			40,
			0,
			0,
			40,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			40,
			40,
			0,
			0,
			0,
			40,
			40,
			53,
			339,
			53,
			0,
			40,
			0,
			40,
			40,
			40,
			40,
			0,
			0,
			0,
			0,
			40,
			403,
			0,
			0,
			40,
			0,
			40,
			403,
			0,
			53,
			0,
			0,
			0,
			338,
			0,
			0,
			40,
			0,
			40,
			40,
			0,
			40,
			53,
			0,
			0,
			40,
			0,
			53,
			0,
			588,
			0,
			0,
			53,
			0,
			53,
			53,
			53,
			53,
			0,
			0,
			53,
			0,
			53,
			403,
			0,
			40,
			53,
			0,
			0,
			338,
			26,
			0,
			339,
			0,
			0,
			40,
			40,
			338,
			53,
			0,
			0,
			53,
			0,
			53,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			339,
			0,
			1154,
			0,
			0,
			0,
			0,
			0,
			0,
			403,
			0,
			0,
			53,
			403,
			403,
			0,
			403,
			403,
			403,
			403,
			403,
			403,
			403,
			403,
			403,
			403,
			403,
			0,
			0,
			0,
			0,
			0,
			0,
			403,
			0,
			0,
			403,
			403,
			0,
			0,
			0,
			403,
			403,
			403,
			403,
			403,
			403,
			0,
			403,
			403,
			403,
			0,
			403,
			403,
			0,
			0,
			403,
			403,
			403,
			403,
			565,
			339,
			0,
			403,
			403,
			565,
			565,
			0,
			403,
			403,
			403,
			403,
			403,
			403,
			403,
			403,
			0,
			0,
			0,
			339,
			339,
			0,
			554,
			0,
			0,
			0,
			0,
			403,
			565,
			0,
			403,
			0,
			403,
			0,
			403,
			0,
			0,
			403,
			0,
			0,
			0,
			565,
			565,
			403,
			0,
			0,
			565,
			339,
			0,
			565,
			339,
			565,
			0,
			565,
			565,
			565,
			565,
			0,
			0,
			0,
			981,
			565,
			0,
			0,
			0,
			565,
			0,
			0,
			0,
			565,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			565,
			0,
			0,
			565,
			0,
			565,
			565,
			0,
			0,
			0,
			565,
			565,
			0,
			565,
			565,
			565,
			565,
			565,
			565,
			565,
			565,
			565,
			565,
			565,
			0,
			0,
			0,
			0,
			0,
			565,
			565,
			565,
			0,
			565,
			565,
			0,
			0,
			0,
			565,
			565,
			0,
			565,
			565,
			565,
			565,
			565,
			565,
			565,
			338,
			565,
			565,
			0,
			565,
			565,
			565,
			565,
			565,
			565,
			565,
			565,
			565,
			565,
			0,
			565,
			565,
			565,
			565,
			565,
			565,
			565,
			565,
			565,
			565,
			565,
			565,
			565,
			565,
			565,
			565,
			565,
			565,
			565,
			565,
			565,
			565,
			0,
			0,
			565,
			0,
			565,
			0,
			565,
			0,
			0,
			565,
			26,
			0,
			0,
			0,
			26,
			565,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			26,
			0,
			0,
			338,
			0,
			26,
			0,
			0,
			0,
			26,
			0,
			0,
			26,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			26,
			26,
			338,
			0,
			0,
			26,
			26,
			0,
			0,
			0,
			0,
			26,
			0,
			26,
			26,
			26,
			26,
			0,
			0,
			0,
			904,
			26,
			0,
			0,
			0,
			26,
			0,
			26,
			0,
			368,
			0,
			0,
			0,
			0,
			0,
			368,
			368,
			26,
			0,
			0,
			26,
			0,
			26,
			368,
			0,
			368,
			26,
			368,
			368,
			368,
			368,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			368,
			338,
			0,
			338,
			26,
			0,
			368,
			368,
			0,
			0,
			338,
			0,
			0,
			23,
			26,
			26,
			0,
			0,
			0,
			338,
			338,
			0,
			338,
			0,
			0,
			0,
			368,
			0,
			0,
			0,
			0,
			0,
			368,
			0,
			368,
			0,
			0,
			368,
			0,
			0,
			0,
			0,
			0,
			981,
			981,
			0,
			0,
			338,
			0,
			0,
			338,
			981,
			981,
			981,
			981,
			981,
			0,
			981,
			981,
			0,
			981,
			981,
			981,
			981,
			981,
			981,
			981,
			981,
			0,
			0,
			0,
			0,
			981,
			0,
			981,
			981,
			981,
			981,
			981,
			981,
			0,
			0,
			981,
			0,
			0,
			0,
			981,
			981,
			368,
			981,
			981,
			981,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			981,
			0,
			981,
			0,
			981,
			981,
			0,
			0,
			981,
			0,
			981,
			981,
			981,
			981,
			981,
			981,
			981,
			981,
			981,
			981,
			981,
			981,
			0,
			981,
			0,
			0,
			981,
			981,
			0,
			0,
			981,
			981,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			981,
			981,
			981,
			981,
			981,
			0,
			0,
			981,
			981,
			0,
			0,
			0,
			981,
			981,
			0,
			0,
			981,
			0,
			0,
			0,
			0,
			981,
			981,
			981,
			981,
			981,
			0,
			0,
			0,
			981,
			0,
			981,
			0,
			0,
			0,
			0,
			0,
			981,
			981,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			981,
			981,
			981,
			981,
			0,
			981,
			904,
			904,
			0,
			0,
			0,
			0,
			981,
			0,
			904,
			904,
			904,
			904,
			904,
			0,
			904,
			904,
			0,
			904,
			904,
			904,
			904,
			904,
			904,
			904,
			864,
			0,
			0,
			0,
			0,
			904,
			0,
			904,
			904,
			904,
			904,
			904,
			904,
			0,
			0,
			904,
			0,
			0,
			0,
			904,
			904,
			0,
			904,
			904,
			904,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			904,
			0,
			904,
			0,
			904,
			904,
			0,
			0,
			904,
			0,
			904,
			904,
			904,
			904,
			904,
			904,
			904,
			904,
			904,
			904,
			904,
			904,
			0,
			904,
			0,
			0,
			904,
			904,
			0,
			0,
			904,
			904,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			904,
			904,
			904,
			904,
			904,
			0,
			0,
			904,
			904,
			0,
			0,
			0,
			904,
			904,
			0,
			0,
			904,
			0,
			0,
			0,
			0,
			904,
			904,
			904,
			904,
			904,
			0,
			368,
			0,
			904,
			0,
			904,
			368,
			368,
			0,
			0,
			0,
			904,
			904,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			368,
			0,
			0,
			0,
			0,
			0,
			0,
			904,
			904,
			904,
			904,
			0,
			904,
			368,
			368,
			0,
			358,
			0,
			368,
			904,
			0,
			368,
			0,
			368,
			0,
			368,
			368,
			368,
			368,
			0,
			0,
			0,
			0,
			368,
			0,
			0,
			0,
			368,
			0,
			0,
			0,
			368,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			368,
			0,
			0,
			368,
			0,
			368,
			368,
			0,
			0,
			0,
			368,
			368,
			0,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			0,
			0,
			0,
			0,
			368,
			368,
			0,
			0,
			368,
			368,
			0,
			0,
			0,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			0,
			368,
			368,
			0,
			0,
			368,
			368,
			368,
			368,
			368,
			0,
			0,
			368,
			368,
			0,
			0,
			0,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			864,
			0,
			0,
			0,
			0,
			864,
			864,
			0,
			0,
			0,
			0,
			368,
			0,
			0,
			368,
			0,
			368,
			0,
			368,
			0,
			0,
			368,
			0,
			0,
			0,
			0,
			0,
			368,
			864,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			864,
			864,
			0,
			398,
			0,
			864,
			0,
			0,
			864,
			0,
			864,
			0,
			864,
			864,
			864,
			864,
			0,
			0,
			0,
			0,
			864,
			0,
			0,
			0,
			864,
			0,
			0,
			0,
			864,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			864,
			0,
			0,
			864,
			0,
			864,
			864,
			0,
			0,
			0,
			864,
			864,
			0,
			864,
			864,
			864,
			864,
			864,
			864,
			864,
			864,
			864,
			864,
			864,
			0,
			0,
			0,
			0,
			0,
			864,
			864,
			0,
			0,
			864,
			864,
			0,
			0,
			0,
			864,
			864,
			864,
			864,
			864,
			864,
			0,
			864,
			864,
			864,
			0,
			864,
			864,
			0,
			0,
			864,
			864,
			864,
			864,
			0,
			0,
			0,
			864,
			864,
			0,
			0,
			0,
			864,
			864,
			864,
			864,
			864,
			864,
			864,
			864,
			358,
			0,
			0,
			0,
			0,
			358,
			358,
			0,
			0,
			0,
			0,
			864,
			0,
			0,
			864,
			0,
			864,
			0,
			864,
			0,
			0,
			864,
			0,
			0,
			0,
			0,
			0,
			864,
			358,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			358,
			358,
			0,
			0,
			0,
			358,
			0,
			0,
			358,
			0,
			358,
			0,
			358,
			358,
			358,
			358,
			0,
			0,
			0,
			0,
			358,
			0,
			33,
			0,
			358,
			0,
			0,
			0,
			358,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			358,
			0,
			0,
			358,
			0,
			358,
			358,
			0,
			0,
			0,
			358,
			358,
			0,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			0,
			0,
			0,
			0,
			0,
			358,
			358,
			0,
			0,
			358,
			358,
			0,
			0,
			0,
			358,
			358,
			358,
			358,
			358,
			358,
			0,
			358,
			358,
			358,
			0,
			358,
			358,
			0,
			0,
			358,
			358,
			358,
			358,
			0,
			0,
			0,
			358,
			358,
			0,
			0,
			0,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			398,
			0,
			0,
			0,
			0,
			398,
			398,
			0,
			0,
			0,
			0,
			358,
			0,
			0,
			358,
			39,
			358,
			0,
			358,
			0,
			0,
			358,
			0,
			0,
			0,
			0,
			0,
			358,
			398,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			398,
			398,
			0,
			0,
			0,
			398,
			0,
			0,
			398,
			0,
			398,
			0,
			398,
			398,
			398,
			398,
			0,
			0,
			0,
			0,
			398,
			38,
			0,
			0,
			398,
			0,
			0,
			0,
			398,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			398,
			0,
			0,
			398,
			0,
			398,
			398,
			0,
			0,
			0,
			398,
			398,
			0,
			398,
			398,
			398,
			398,
			398,
			398,
			398,
			398,
			398,
			398,
			398,
			0,
			0,
			0,
			0,
			0,
			398,
			398,
			0,
			27,
			398,
			398,
			0,
			0,
			0,
			398,
			398,
			0,
			398,
			398,
			398,
			0,
			398,
			398,
			398,
			0,
			398,
			398,
			0,
			0,
			398,
			398,
			398,
			398,
			0,
			0,
			0,
			398,
			398,
			0,
			0,
			0,
			398,
			398,
			398,
			398,
			398,
			398,
			398,
			398,
			0,
			0,
			0,
			0,
			0,
			0,
			37,
			0,
			0,
			0,
			0,
			398,
			0,
			0,
			398,
			0,
			398,
			0,
			0,
			33,
			33,
			0,
			0,
			0,
			33,
			0,
			0,
			398,
			33,
			0,
			33,
			0,
			0,
			33,
			0,
			33,
			33,
			0,
			33,
			0,
			33,
			0,
			33,
			0,
			33,
			33,
			33,
			33,
			0,
			0,
			33,
			33,
			0,
			5,
			0,
			0,
			33,
			0,
			33,
			33,
			33,
			0,
			0,
			33,
			33,
			33,
			0,
			33,
			0,
			0,
			33,
			0,
			33,
			33,
			33,
			33,
			0,
			0,
			0,
			33,
			33,
			33,
			0,
			0,
			33,
			33,
			33,
			0,
			0,
			0,
			0,
			0,
			0,
			33,
			33,
			0,
			33,
			33,
			0,
			33,
			33,
			33,
			0,
			0,
			0,
			33,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1083,
			0,
			0,
			0,
			0,
			0,
			39,
			0,
			0,
			33,
			39,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			33,
			33,
			33,
			39,
			0,
			0,
			0,
			0,
			39,
			0,
			33,
			0,
			39,
			0,
			0,
			39,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			53,
			0,
			39,
			39,
			0,
			0,
			0,
			39,
			39,
			0,
			38,
			0,
			0,
			39,
			38,
			39,
			39,
			39,
			39,
			0,
			0,
			0,
			0,
			39,
			0,
			38,
			33,
			39,
			0,
			39,
			38,
			0,
			0,
			0,
			38,
			0,
			0,
			38,
			0,
			39,
			0,
			39,
			39,
			7,
			39,
			0,
			0,
			0,
			39,
			38,
			38,
			0,
			0,
			0,
			38,
			38,
			0,
			27,
			0,
			0,
			38,
			27,
			38,
			38,
			38,
			38,
			0,
			0,
			39,
			0,
			38,
			0,
			27,
			0,
			38,
			0,
			38,
			27,
			0,
			39,
			0,
			27,
			0,
			0,
			27,
			0,
			38,
			0,
			0,
			38,
			0,
			38,
			0,
			0,
			0,
			38,
			27,
			27,
			0,
			0,
			0,
			27,
			27,
			0,
			37,
			0,
			1084,
			27,
			37,
			27,
			27,
			27,
			27,
			0,
			0,
			38,
			0,
			27,
			0,
			37,
			0,
			27,
			0,
			27,
			37,
			38,
			38,
			0,
			37,
			0,
			0,
			37,
			0,
			27,
			0,
			0,
			27,
			0,
			27,
			0,
			0,
			0,
			27,
			37,
			37,
			0,
			0,
			0,
			37,
			37,
			0,
			5,
			0,
			54,
			37,
			53,
			37,
			37,
			37,
			37,
			0,
			0,
			27,
			0,
			37,
			0,
			53,
			0,
			37,
			0,
			37,
			53,
			27,
			27,
			0,
			53,
			0,
			0,
			53,
			0,
			37,
			0,
			0,
			37,
			0,
			37,
			0,
			0,
			0,
			37,
			53,
			53,
			0,
			0,
			0,
			53,
			53,
			0,
			0,
			0,
			0,
			53,
			0,
			53,
			53,
			53,
			53,
			0,
			0,
			37,
			0,
			53,
			0,
			0,
			1083,
			53,
			0,
			53,
			53,
			0,
			37,
			0,
			0,
			0,
			0,
			0,
			0,
			53,
			0,
			53,
			53,
			0,
			53,
			0,
			53,
			0,
			53,
			0,
			53,
			0,
			0,
			53,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			53,
			53,
			0,
			53,
			53,
			53,
			53,
			0,
			53,
			0,
			0,
			53,
			0,
			53,
			53,
			53,
			53,
			0,
			0,
			53,
			0,
			53,
			0,
			0,
			53,
			53,
			0,
			53,
			53,
			0,
			0,
			53,
			0,
			0,
			0,
			0,
			0,
			53,
			0,
			0,
			53,
			0,
			53,
			53,
			53,
			0,
			53,
			7,
			53,
			53,
			0,
			54,
			0,
			0,
			53,
			0,
			53,
			53,
			53,
			53,
			0,
			0,
			54,
			0,
			53,
			0,
			53,
			54,
			53,
			0,
			53,
			54,
			0,
			0,
			54,
			0,
			0,
			0,
			0,
			0,
			53,
			0,
			0,
			53,
			0,
			53,
			54,
			54,
			0,
			53,
			0,
			54,
			54,
			0,
			0,
			0,
			0,
			54,
			0,
			54,
			54,
			54,
			54,
			0,
			0,
			0,
			0,
			54,
			0,
			53,
			1084,
			54,
			0,
			54,
			53,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			54,
			0,
			53,
			54,
			0,
			54,
			0,
			53,
			0,
			54,
			0,
			53,
			0,
			0,
			53,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			53,
			53,
			0,
			54,
			0,
			53,
			53,
			0,
			54,
			0,
			0,
			53,
			54,
			53,
			53,
			53,
			53,
			0,
			0,
			0,
			0,
			53,
			0,
			54,
			0,
			53,
			0,
			53,
			54,
			0,
			0,
			0,
			54,
			0,
			0,
			54,
			0,
			53,
			0,
			0,
			53,
			0,
			53,
			0,
			0,
			0,
			53,
			54,
			54,
			0,
			0,
			0,
			54,
			54,
			0,
			0,
			0,
			0,
			54,
			0,
			54,
			54,
			54,
			54,
			0,
			0,
			53,
			0,
			54,
			0,
			0,
			0,
			54,
			0,
			54,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			54,
			0,
			56,
			54,
			0,
			54,
			0,
			0,
			0,
			54,
			57,
			24,
			58,
			25,
			0,
			0,
			26,
			59,
			0,
			60,
			61,
			27,
			62,
			63,
			64,
			28,
			0,
			0,
			0,
			54,
			0,
			65,
			0,
			66,
			30,
			67,
			68,
			69,
			70,
			0,
			0,
			32,
			0,
			0,
			0,
			71,
			33,
			0,
			72,
			73,
			34,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			74,
			0,
			36,
			0,
			37,
			75,
			0,
			0,
			38,
			0,
			76,
			77,
			78,
			79,
			80,
			81,
			39,
			40,
			82,
			83,
			41,
			84,
			0,
			85,
			0,
			0,
			86,
			87,
			0,
			0,
			88,
			89,
			0,
			867,
			0,
			0,
			0,
			0,
			0,
			867,
			0,
			0,
			0,
			0,
			0,
			90,
			91,
			92,
			93,
			94,
			0,
			0,
			95,
			96,
			0,
			0,
			0,
			97,
			0,
			0,
			0,
			98,
			0,
			0,
			0,
			0,
			99,
			100,
			101,
			102,
			103,
			0,
			0,
			0,
			104,
			867,
			105,
			0,
			0,
			0,
			0,
			0,
			106,
			107,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			426,
			0,
			0,
			0,
			0,
			0,
			426,
			108,
			109,
			110,
			111,
			0,
			0,
			0,
			0,
			0,
			867,
			0,
			0,
			200,
			0,
			867,
			0,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			0,
			0,
			0,
			0,
			0,
			867,
			867,
			426,
			867,
			867,
			0,
			0,
			0,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			0,
			867,
			867,
			0,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			0,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			0,
			867,
			0,
			867,
			0,
			867,
			867,
			867,
			867,
			0,
			0,
			358,
			426,
			0,
			867,
			0,
			0,
			0,
			0,
			0,
			426,
			358,
			426,
			426,
			426,
			426,
			426,
			0,
			426,
			0,
			426,
			426,
			0,
			426,
			426,
			426,
			426,
			426,
			0,
			426,
			426,
			426,
			426,
			867,
			426,
			426,
			426,
			426,
			426,
			426,
			426,
			426,
			426,
			426,
			426,
			426,
			426,
			426,
			426,
			426,
			426,
			426,
			426,
			426,
			426,
			426,
			0,
			0,
			0,
			0,
			358,
			0,
			426,
			0,
			0,
			426,
			0,
			0,
			0,
			0,
			0,
			426,
			0,
			0,
			867,
			0,
			0,
			0,
			0,
			867,
			0,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			0,
			0,
			0,
			0,
			0,
			867,
			867,
			0,
			867,
			867,
			0,
			0,
			0,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			698,
			867,
			867,
			0,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			0,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			867,
			368,
			0,
			0,
			867,
			0,
			867,
			368,
			0,
			867,
			0,
			0,
			0,
			0,
			0,
			867,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			368,
			0,
			0,
			698,
			0,
			0,
			0,
			0,
			698,
			0,
			698,
			698,
			698,
			698,
			698,
			698,
			698,
			698,
			698,
			698,
			698,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			698,
			698,
			0,
			0,
			0,
			0,
			698,
			0,
			698,
			0,
			698,
			368,
			698,
			698,
			698,
			0,
			368,
			0,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			0,
			0,
			0,
			0,
			0,
			368,
			368,
			0,
			368,
			368,
			0,
			0,
			0,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			1075,
			368,
			368,
			0,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			698,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			0,
			368,
			0,
			0,
			368,
			0,
			368,
			368,
			0,
			368,
			0,
			0,
			0,
			0,
			0,
			368,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			368,
			0,
			0,
			1075,
			0,
			0,
			0,
			0,
			1075,
			0,
			1075,
			1075,
			1075,
			1075,
			1075,
			1075,
			1075,
			1075,
			1075,
			1075,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1075,
			1075,
			0,
			0,
			0,
			0,
			1075,
			0,
			1075,
			0,
			1075,
			368,
			1075,
			1075,
			1075,
			0,
			368,
			0,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			0,
			0,
			0,
			0,
			0,
			368,
			368,
			0,
			368,
			368,
			0,
			0,
			0,
			0,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			0,
			368,
			368,
			0,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			1075,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			0,
			567,
			675,
			0,
			368,
			0,
			368,
			567,
			0,
			368,
			0,
			24,
			0,
			25,
			0,
			368,
			26,
			0,
			0,
			0,
			0,
			27,
			0,
			0,
			0,
			28,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			30,
			0,
			0,
			0,
			0,
			0,
			0,
			32,
			0,
			567,
			0,
			0,
			33,
			0,
			0,
			0,
			34,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			36,
			0,
			37,
			0,
			0,
			0,
			38,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			39,
			40,
			0,
			0,
			41,
			0,
			0,
			85,
			567,
			0,
			0,
			0,
			0,
			567,
			0,
			567,
			567,
			567,
			567,
			567,
			567,
			567,
			567,
			567,
			567,
			567,
			0,
			0,
			693,
			0,
			0,
			0,
			0,
			567,
			0,
			567,
			567,
			0,
			0,
			0,
			567,
			567,
			567,
			567,
			567,
			567,
			567,
			567,
			567,
			567,
			0,
			567,
			567,
			0,
			567,
			567,
			567,
			567,
			567,
			567,
			567,
			567,
			567,
			567,
			0,
			567,
			567,
			567,
			567,
			567,
			567,
			567,
			567,
			567,
			567,
			567,
			567,
			567,
			567,
			567,
			567,
			567,
			567,
			567,
			567,
			567,
			567,
			0,
			563,
			0,
			0,
			0,
			387,
			567,
			563,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			567,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			693,
			0,
			0,
			0,
			0,
			693,
			0,
			693,
			693,
			693,
			693,
			693,
			693,
			693,
			693,
			693,
			693,
			693,
			0,
			563,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			693,
			693,
			0,
			0,
			0,
			0,
			693,
			0,
			693,
			0,
			693,
			0,
			693,
			693,
			693,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			693,
			0,
			0,
			0,
			0,
			563,
			0,
			0,
			0,
			0,
			563,
			693,
			563,
			563,
			563,
			563,
			563,
			563,
			563,
			563,
			563,
			563,
			563,
			693,
			0,
			694,
			0,
			0,
			0,
			0,
			563,
			0,
			563,
			563,
			0,
			0,
			0,
			563,
			563,
			693,
			563,
			563,
			563,
			563,
			563,
			563,
			563,
			0,
			563,
			563,
			0,
			563,
			563,
			563,
			563,
			563,
			563,
			563,
			563,
			563,
			563,
			0,
			563,
			563,
			563,
			563,
			563,
			563,
			563,
			563,
			563,
			563,
			563,
			563,
			563,
			563,
			563,
			563,
			563,
			563,
			563,
			563,
			563,
			563,
			0,
			571,
			0,
			0,
			0,
			0,
			563,
			571,
			0,
			563,
			0,
			0,
			0,
			0,
			0,
			563,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			694,
			0,
			0,
			0,
			0,
			694,
			0,
			694,
			694,
			694,
			694,
			694,
			694,
			694,
			694,
			694,
			694,
			694,
			0,
			571,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			694,
			694,
			0,
			0,
			0,
			0,
			694,
			0,
			694,
			0,
			694,
			0,
			694,
			694,
			694,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			694,
			0,
			0,
			0,
			0,
			571,
			0,
			0,
			0,
			0,
			571,
			694,
			571,
			571,
			571,
			571,
			571,
			571,
			571,
			571,
			571,
			571,
			571,
			694,
			0,
			0,
			0,
			0,
			0,
			0,
			571,
			0,
			571,
			571,
			0,
			0,
			0,
			0,
			571,
			694,
			571,
			571,
			571,
			571,
			571,
			571,
			571,
			0,
			571,
			571,
			0,
			571,
			571,
			571,
			571,
			571,
			571,
			571,
			571,
			571,
			571,
			0,
			571,
			571,
			571,
			571,
			571,
			571,
			571,
			571,
			571,
			571,
			571,
			571,
			571,
			571,
			571,
			571,
			571,
			571,
			571,
			571,
			571,
			571,
			0,
			368,
			1011,
			0,
			0,
			0,
			571,
			368,
			0,
			571,
			0,
			24,
			0,
			25,
			0,
			571,
			26,
			0,
			0,
			0,
			0,
			27,
			0,
			0,
			0,
			28,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			30,
			0,
			0,
			0,
			0,
			0,
			0,
			32,
			0,
			368,
			0,
			0,
			33,
			0,
			0,
			0,
			34,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			36,
			0,
			37,
			0,
			0,
			0,
			38,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			39,
			40,
			0,
			0,
			41,
			0,
			0,
			85,
			368,
			0,
			0,
			0,
			0,
			368,
			0,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			368,
			0,
			368,
			368,
			0,
			0,
			0,
			0,
			368,
			0,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			0,
			368,
			368,
			0,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			0,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			0,
			486,
			1183,
			0,
			0,
			387,
			368,
			486,
			0,
			368,
			0,
			24,
			0,
			25,
			0,
			368,
			26,
			0,
			0,
			0,
			0,
			27,
			0,
			0,
			0,
			28,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			30,
			0,
			0,
			0,
			0,
			0,
			0,
			32,
			0,
			486,
			0,
			0,
			33,
			0,
			0,
			0,
			34,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			36,
			0,
			37,
			0,
			0,
			0,
			38,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			39,
			40,
			0,
			0,
			41,
			0,
			0,
			85,
			486,
			0,
			0,
			0,
			0,
			486,
			0,
			486,
			486,
			486,
			486,
			486,
			486,
			486,
			486,
			486,
			486,
			486,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			486,
			0,
			486,
			486,
			0,
			0,
			0,
			0,
			486,
			0,
			486,
			486,
			486,
			486,
			486,
			486,
			486,
			0,
			486,
			486,
			0,
			486,
			486,
			486,
			486,
			486,
			486,
			486,
			486,
			486,
			486,
			0,
			486,
			486,
			486,
			486,
			486,
			486,
			486,
			486,
			486,
			486,
			486,
			486,
			486,
			486,
			486,
			486,
			486,
			486,
			486,
			486,
			486,
			486,
			0,
			598,
			0,
			390,
			0,
			387,
			486,
			598,
			0,
			486,
			0,
			0,
			0,
			0,
			0,
			486,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			390,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			390,
			0,
			0,
			0,
			598,
			390,
			0,
			0,
			257,
			0,
			390,
			0,
			390,
			390,
			390,
			390,
			0,
			0,
			0,
			0,
			390,
			0,
			0,
			0,
			390,
			368,
			0,
			0,
			390,
			0,
			0,
			368,
			0,
			0,
			0,
			867,
			390,
			0,
			0,
			390,
			0,
			390,
			0,
			0,
			0,
			598,
			0,
			0,
			0,
			0,
			598,
			0,
			598,
			598,
			598,
			598,
			598,
			598,
			598,
			598,
			598,
			598,
			598,
			0,
			0,
			390,
			0,
			368,
			0,
			0,
			0,
			0,
			598,
			598,
			0,
			390,
			0,
			0,
			598,
			0,
			598,
			0,
			598,
			867,
			598,
			598,
			598,
			0,
			598,
			598,
			0,
			598,
			598,
			598,
			598,
			598,
			598,
			598,
			598,
			598,
			598,
			0,
			0,
			0,
			598,
			598,
			598,
			598,
			598,
			598,
			598,
			598,
			598,
			598,
			598,
			598,
			598,
			598,
			598,
			598,
			598,
			598,
			358,
			598,
			0,
			390,
			368,
			0,
			358,
			0,
			0,
			0,
			368,
			368,
			0,
			0,
			0,
			0,
			0,
			598,
			0,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			867,
			368,
			0,
			368,
			368,
			0,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			358,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			0,
			0,
			0,
			0,
			368,
			0,
			368,
			0,
			0,
			368,
			0,
			0,
			0,
			0,
			0,
			368,
			0,
			0,
			358,
			0,
			0,
			0,
			0,
			358,
			0,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			0,
			0,
			0,
			0,
			0,
			0,
			358,
			426,
			640,
			358,
			358,
			0,
			0,
			0,
			640,
			358,
			358,
			358,
			0,
			358,
			426,
			358,
			358,
			358,
			0,
			358,
			358,
			0,
			0,
			358,
			358,
			358,
			358,
			0,
			0,
			0,
			358,
			358,
			0,
			426,
			426,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			0,
			640,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			426,
			358,
			0,
			0,
			0,
			0,
			358,
			0,
			426,
			0,
			0,
			426,
			0,
			0,
			0,
			0,
			0,
			358,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			640,
			0,
			0,
			0,
			0,
			640,
			0,
			640,
			640,
			640,
			640,
			640,
			640,
			640,
			640,
			640,
			640,
			640,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			427,
			644,
			640,
			640,
			0,
			0,
			0,
			644,
			640,
			0,
			640,
			427,
			640,
			427,
			640,
			640,
			640,
			0,
			640,
			640,
			0,
			0,
			640,
			640,
			640,
			640,
			0,
			0,
			0,
			640,
			640,
			0,
			427,
			427,
			640,
			640,
			640,
			640,
			640,
			640,
			640,
			640,
			0,
			644,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			427,
			640,
			0,
			0,
			0,
			0,
			0,
			0,
			427,
			0,
			0,
			427,
			0,
			0,
			0,
			0,
			0,
			640,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			644,
			0,
			0,
			0,
			0,
			644,
			0,
			644,
			644,
			644,
			644,
			644,
			644,
			644,
			644,
			644,
			644,
			644,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			430,
			643,
			644,
			644,
			0,
			0,
			0,
			643,
			644,
			0,
			644,
			430,
			644,
			430,
			644,
			644,
			644,
			0,
			644,
			644,
			0,
			0,
			644,
			644,
			644,
			644,
			0,
			0,
			0,
			644,
			644,
			0,
			430,
			430,
			644,
			644,
			644,
			644,
			644,
			644,
			644,
			644,
			0,
			643,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			430,
			644,
			0,
			0,
			0,
			0,
			0,
			0,
			430,
			0,
			0,
			430,
			0,
			0,
			0,
			0,
			0,
			644,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			643,
			0,
			0,
			0,
			0,
			643,
			0,
			643,
			643,
			643,
			643,
			643,
			643,
			643,
			643,
			643,
			643,
			643,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			440,
			358,
			643,
			643,
			0,
			0,
			0,
			358,
			643,
			0,
			643,
			440,
			643,
			440,
			643,
			643,
			643,
			0,
			643,
			643,
			0,
			0,
			643,
			643,
			643,
			643,
			0,
			0,
			0,
			643,
			643,
			0,
			440,
			440,
			643,
			643,
			643,
			643,
			643,
			643,
			643,
			643,
			0,
			358,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			440,
			643,
			0,
			0,
			0,
			0,
			0,
			0,
			440,
			0,
			0,
			440,
			0,
			0,
			0,
			0,
			0,
			643,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			358,
			0,
			0,
			0,
			0,
			358,
			0,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			0,
			0,
			0,
			0,
			0,
			0,
			358,
			620,
			0,
			358,
			358,
			0,
			0,
			620,
			0,
			358,
			358,
			358,
			0,
			358,
			0,
			358,
			358,
			358,
			0,
			358,
			358,
			0,
			0,
			358,
			358,
			358,
			358,
			0,
			0,
			0,
			358,
			358,
			0,
			0,
			0,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			358,
			620,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			358,
			0,
			0,
			0,
			0,
			358,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			358,
			0,
			0,
			858,
			0,
			0,
			0,
			0,
			0,
			858,
			0,
			0,
			0,
			0,
			620,
			0,
			0,
			0,
			0,
			620,
			0,
			620,
			620,
			620,
			620,
			620,
			620,
			620,
			620,
			620,
			620,
			620,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			620,
			620,
			0,
			0,
			858,
			0,
			620,
			0,
			620,
			0,
			620,
			0,
			620,
			620,
			620,
			0,
			620,
			620,
			0,
			0,
			620,
			620,
			620,
			620,
			620,
			620,
			620,
			620,
			620,
			0,
			0,
			0,
			620,
			620,
			620,
			620,
			620,
			620,
			620,
			620,
			0,
			0,
			0,
			0,
			0,
			858,
			0,
			0,
			0,
			0,
			858,
			620,
			858,
			858,
			858,
			858,
			858,
			858,
			858,
			858,
			858,
			858,
			858,
			0,
			0,
			0,
			0,
			620,
			0,
			858,
			627,
			0,
			858,
			858,
			0,
			0,
			627,
			0,
			858,
			0,
			858,
			0,
			858,
			0,
			858,
			858,
			858,
			0,
			858,
			858,
			0,
			0,
			858,
			858,
			858,
			858,
			0,
			0,
			0,
			858,
			858,
			0,
			0,
			0,
			858,
			858,
			858,
			858,
			858,
			858,
			858,
			858,
			627,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			858,
			0,
			0,
			0,
			0,
			858,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			858,
			0,
			0,
			987,
			0,
			0,
			0,
			0,
			0,
			987,
			0,
			0,
			0,
			0,
			627,
			0,
			0,
			0,
			0,
			627,
			0,
			627,
			627,
			627,
			627,
			627,
			627,
			627,
			627,
			627,
			627,
			627,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			627,
			627,
			0,
			0,
			987,
			0,
			627,
			0,
			627,
			0,
			627,
			0,
			627,
			627,
			627,
			0,
			627,
			627,
			0,
			0,
			627,
			627,
			627,
			627,
			0,
			0,
			0,
			627,
			627,
			0,
			0,
			0,
			627,
			627,
			627,
			627,
			627,
			627,
			627,
			627,
			0,
			0,
			0,
			0,
			0,
			987,
			0,
			0,
			0,
			0,
			987,
			627,
			987,
			987,
			987,
			987,
			987,
			987,
			987,
			987,
			987,
			987,
			987,
			0,
			0,
			0,
			0,
			627,
			0,
			0,
			628,
			0,
			987,
			987,
			0,
			0,
			628,
			0,
			987,
			0,
			987,
			0,
			987,
			0,
			987,
			987,
			987,
			0,
			987,
			987,
			0,
			0,
			987,
			987,
			987,
			987,
			0,
			0,
			0,
			987,
			987,
			0,
			0,
			0,
			987,
			987,
			987,
			987,
			987,
			987,
			987,
			987,
			628,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			987,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			987,
			0,
			0,
			629,
			0,
			0,
			0,
			0,
			0,
			629,
			0,
			0,
			0,
			0,
			628,
			0,
			0,
			0,
			0,
			628,
			0,
			628,
			628,
			628,
			628,
			628,
			628,
			628,
			628,
			628,
			628,
			628,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			628,
			628,
			0,
			0,
			629,
			0,
			628,
			0,
			628,
			0,
			628,
			0,
			628,
			628,
			628,
			0,
			628,
			628,
			0,
			0,
			628,
			628,
			628,
			628,
			0,
			0,
			0,
			628,
			628,
			0,
			0,
			0,
			628,
			628,
			628,
			628,
			628,
			628,
			628,
			628,
			0,
			0,
			0,
			0,
			0,
			629,
			0,
			0,
			0,
			0,
			629,
			628,
			629,
			629,
			629,
			629,
			629,
			629,
			629,
			629,
			629,
			629,
			629,
			0,
			0,
			0,
			0,
			628,
			0,
			0,
			0,
			0,
			629,
			629,
			0,
			0,
			0,
			0,
			629,
			0,
			629,
			0,
			629,
			0,
			629,
			629,
			629,
			0,
			629,
			629,
			0,
			0,
			629,
			629,
			629,
			629,
			0,
			0,
			0,
			629,
			629,
			0,
			0,
			0,
			629,
			629,
			629,
			629,
			629,
			629,
			629,
			629,
			0,
			530,
			0,
			662,
			0,
			0,
			0,
			0,
			0,
			57,
			24,
			629,
			25,
			0,
			0,
			26,
			260,
			0,
			0,
			0,
			27,
			62,
			63,
			0,
			28,
			0,
			0,
			629,
			0,
			0,
			65,
			0,
			0,
			30,
			0,
			0,
			0,
			0,
			0,
			0,
			32,
			0,
			0,
			0,
			0,
			33,
			0,
			72,
			73,
			34,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			36,
			0,
			37,
			75,
			0,
			0,
			38,
			0,
			0,
			77,
			0,
			79,
			0,
			81,
			39,
			40,
			261,
			0,
			41,
			0,
			0,
			0,
			0,
			0,
			0,
			662,
			0,
			0,
			0,
			0,
			662,
			0,
			662,
			662,
			662,
			662,
			662,
			662,
			662,
			662,
			662,
			662,
			662,
			90,
			91,
			92,
			262,
			531,
			663,
			0,
			95,
			96,
			662,
			662,
			0,
			0,
			0,
			0,
			662,
			98,
			662,
			0,
			662,
			0,
			662,
			662,
			662,
			0,
			0,
			0,
			0,
			0,
			662,
			662,
			662,
			662,
			0,
			0,
			0,
			662,
			662,
			0,
			0,
			0,
			662,
			662,
			662,
			662,
			662,
			662,
			662,
			662,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			662,
			0,
			108,
			532,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			533,
			534,
			0,
			662,
			664,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			663,
			0,
			0,
			0,
			0,
			663,
			0,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			663,
			663,
			0,
			0,
			0,
			0,
			663,
			0,
			663,
			0,
			663,
			0,
			663,
			663,
			663,
			0,
			0,
			0,
			0,
			0,
			663,
			663,
			663,
			663,
			0,
			0,
			0,
			663,
			663,
			0,
			0,
			0,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			663,
			0,
			0,
			0,
			0,
			0,
			664,
			0,
			0,
			0,
			0,
			664,
			663,
			664,
			664,
			664,
			664,
			664,
			664,
			664,
			664,
			664,
			664,
			664,
			667,
			0,
			0,
			0,
			663,
			0,
			0,
			0,
			0,
			664,
			664,
			0,
			0,
			0,
			0,
			664,
			0,
			664,
			0,
			664,
			0,
			664,
			664,
			664,
			0,
			0,
			0,
			0,
			0,
			664,
			664,
			664,
			664,
			0,
			0,
			0,
			664,
			664,
			0,
			0,
			0,
			664,
			664,
			664,
			664,
			664,
			664,
			664,
			664,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			664,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			668,
			0,
			0,
			0,
			664,
			0,
			0,
			0,
			0,
			0,
			0,
			667,
			0,
			0,
			0,
			0,
			667,
			0,
			667,
			667,
			667,
			667,
			667,
			667,
			667,
			667,
			667,
			667,
			667,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			667,
			667,
			0,
			0,
			0,
			0,
			667,
			0,
			667,
			0,
			667,
			0,
			667,
			667,
			667,
			0,
			0,
			0,
			0,
			0,
			667,
			667,
			667,
			667,
			0,
			0,
			0,
			667,
			667,
			0,
			0,
			0,
			0,
			0,
			667,
			667,
			667,
			667,
			667,
			667,
			0,
			0,
			0,
			0,
			0,
			668,
			0,
			0,
			0,
			0,
			668,
			667,
			668,
			668,
			668,
			668,
			668,
			668,
			668,
			668,
			668,
			668,
			668,
			669,
			0,
			0,
			0,
			667,
			0,
			0,
			0,
			0,
			668,
			668,
			0,
			0,
			0,
			0,
			668,
			0,
			668,
			0,
			668,
			0,
			668,
			668,
			668,
			0,
			0,
			0,
			0,
			0,
			668,
			668,
			668,
			668,
			0,
			0,
			0,
			668,
			668,
			0,
			0,
			0,
			0,
			0,
			668,
			668,
			668,
			668,
			668,
			668,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			668,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			670,
			0,
			0,
			0,
			668,
			0,
			0,
			0,
			0,
			0,
			0,
			669,
			0,
			0,
			0,
			0,
			669,
			0,
			669,
			669,
			669,
			669,
			669,
			669,
			669,
			669,
			669,
			669,
			669,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			669,
			669,
			0,
			0,
			0,
			0,
			669,
			0,
			669,
			0,
			669,
			0,
			669,
			669,
			669,
			0,
			0,
			0,
			0,
			0,
			669,
			669,
			669,
			669,
			0,
			0,
			0,
			669,
			669,
			0,
			0,
			0,
			0,
			0,
			669,
			669,
			669,
			669,
			669,
			669,
			0,
			0,
			0,
			0,
			0,
			670,
			0,
			0,
			0,
			0,
			670,
			669,
			670,
			670,
			670,
			670,
			670,
			670,
			670,
			670,
			670,
			670,
			670,
			671,
			0,
			0,
			0,
			669,
			0,
			0,
			0,
			0,
			670,
			670,
			0,
			0,
			0,
			0,
			670,
			0,
			670,
			0,
			670,
			0,
			670,
			670,
			670,
			0,
			0,
			0,
			0,
			0,
			670,
			670,
			670,
			670,
			0,
			0,
			0,
			670,
			670,
			0,
			0,
			0,
			0,
			0,
			670,
			670,
			670,
			670,
			670,
			670,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			368,
			670,
			0,
			0,
			0,
			0,
			368,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			670,
			0,
			0,
			0,
			0,
			0,
			0,
			671,
			0,
			0,
			0,
			0,
			671,
			0,
			671,
			671,
			671,
			671,
			671,
			671,
			671,
			671,
			671,
			671,
			671,
			368,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			671,
			671,
			0,
			0,
			0,
			0,
			671,
			0,
			671,
			0,
			671,
			0,
			671,
			671,
			671,
			0,
			0,
			0,
			0,
			0,
			671,
			671,
			671,
			671,
			0,
			0,
			0,
			671,
			671,
			0,
			0,
			0,
			0,
			0,
			671,
			671,
			671,
			671,
			671,
			671,
			677,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			671,
			0,
			0,
			0,
			0,
			0,
			368,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			368,
			0,
			671,
			368,
			0,
			368,
			368,
			0,
			0,
			0,
			368,
			368,
			0,
			0,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			0,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			368,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			368,
			368,
			0,
			678,
			0,
			0,
			0,
			0,
			368,
			0,
			0,
			368,
			0,
			0,
			677,
			0,
			0,
			368,
			0,
			677,
			0,
			677,
			677,
			677,
			677,
			677,
			677,
			677,
			677,
			677,
			677,
			677,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			677,
			677,
			0,
			0,
			0,
			0,
			677,
			0,
			677,
			0,
			677,
			0,
			677,
			677,
			677,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			677,
			677,
			0,
			0,
			0,
			677,
			677,
			0,
			0,
			0,
			0,
			0,
			681,
			0,
			677,
			677,
			677,
			677,
			0,
			0,
			0,
			0,
			0,
			678,
			0,
			0,
			0,
			0,
			678,
			677,
			678,
			678,
			678,
			678,
			678,
			678,
			678,
			678,
			678,
			678,
			678,
			0,
			0,
			0,
			0,
			677,
			0,
			0,
			0,
			0,
			678,
			678,
			0,
			0,
			0,
			0,
			678,
			0,
			678,
			0,
			678,
			0,
			678,
			678,
			678,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			678,
			678,
			0,
			0,
			0,
			678,
			678,
			0,
			0,
			0,
			0,
			0,
			682,
			0,
			678,
			678,
			678,
			678,
			0,
			0,
			0,
			0,
			0,
			681,
			0,
			0,
			0,
			0,
			681,
			678,
			681,
			681,
			681,
			681,
			681,
			681,
			681,
			681,
			681,
			681,
			681,
			0,
			0,
			0,
			0,
			678,
			0,
			0,
			0,
			0,
			681,
			681,
			0,
			0,
			0,
			0,
			681,
			0,
			681,
			0,
			681,
			0,
			681,
			681,
			681,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			681,
			681,
			0,
			0,
			0,
			681,
			681,
			0,
			0,
			0,
			0,
			0,
			684,
			0,
			0,
			0,
			681,
			681,
			0,
			0,
			0,
			0,
			0,
			682,
			0,
			0,
			0,
			0,
			682,
			681,
			682,
			682,
			682,
			682,
			682,
			682,
			682,
			682,
			682,
			682,
			682,
			0,
			0,
			0,
			0,
			681,
			0,
			0,
			0,
			0,
			682,
			682,
			0,
			0,
			0,
			0,
			682,
			0,
			682,
			0,
			682,
			0,
			682,
			682,
			682,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			682,
			682,
			0,
			0,
			0,
			682,
			682,
			0,
			0,
			0,
			0,
			0,
			685,
			0,
			0,
			0,
			682,
			682,
			0,
			0,
			0,
			0,
			0,
			684,
			0,
			0,
			0,
			0,
			684,
			682,
			684,
			684,
			684,
			684,
			684,
			684,
			684,
			684,
			684,
			684,
			684,
			0,
			0,
			0,
			0,
			682,
			0,
			0,
			0,
			0,
			684,
			684,
			0,
			0,
			0,
			0,
			684,
			0,
			684,
			0,
			684,
			0,
			684,
			684,
			684,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			684,
			0,
			0,
			0,
			684,
			684,
			0,
			0,
			0,
			0,
			0,
			687,
			0,
			0,
			0,
			684,
			684,
			0,
			0,
			0,
			0,
			0,
			685,
			0,
			0,
			0,
			0,
			685,
			684,
			685,
			685,
			685,
			685,
			685,
			685,
			685,
			685,
			685,
			685,
			685,
			0,
			0,
			0,
			0,
			684,
			0,
			0,
			0,
			0,
			685,
			685,
			0,
			0,
			0,
			0,
			685,
			0,
			685,
			0,
			685,
			0,
			685,
			685,
			685,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			685,
			0,
			0,
			0,
			685,
			685,
			0,
			0,
			0,
			0,
			0,
			688,
			0,
			0,
			0,
			685,
			685,
			0,
			0,
			0,
			0,
			0,
			687,
			0,
			0,
			0,
			0,
			687,
			685,
			687,
			687,
			687,
			687,
			687,
			687,
			687,
			687,
			687,
			687,
			687,
			0,
			0,
			0,
			0,
			685,
			0,
			0,
			0,
			0,
			687,
			687,
			0,
			0,
			0,
			0,
			687,
			0,
			687,
			0,
			687,
			0,
			687,
			687,
			687,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			687,
			0,
			0,
			0,
			0,
			687,
			0,
			0,
			0,
			0,
			0,
			690,
			0,
			0,
			0,
			687,
			687,
			0,
			0,
			0,
			0,
			0,
			688,
			0,
			0,
			0,
			0,
			688,
			687,
			688,
			688,
			688,
			688,
			688,
			688,
			688,
			688,
			688,
			688,
			688,
			0,
			0,
			0,
			0,
			687,
			0,
			0,
			0,
			0,
			688,
			688,
			0,
			0,
			0,
			0,
			688,
			0,
			688,
			0,
			688,
			0,
			688,
			688,
			688,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			688,
			0,
			0,
			0,
			0,
			688,
			0,
			0,
			0,
			0,
			0,
			691,
			0,
			0,
			0,
			688,
			688,
			0,
			0,
			0,
			0,
			0,
			690,
			0,
			0,
			0,
			0,
			690,
			688,
			690,
			690,
			690,
			690,
			690,
			690,
			690,
			690,
			690,
			690,
			690,
			0,
			0,
			0,
			0,
			688,
			0,
			0,
			0,
			0,
			690,
			690,
			0,
			0,
			0,
			0,
			690,
			0,
			690,
			0,
			690,
			0,
			690,
			690,
			690,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			690,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			690,
			690,
			0,
			0,
			0,
			0,
			0,
			691,
			0,
			0,
			0,
			0,
			691,
			690,
			691,
			691,
			691,
			691,
			691,
			691,
			691,
			691,
			691,
			691,
			691,
			0,
			0,
			0,
			0,
			690,
			0,
			0,
			0,
			0,
			691,
			691,
			0,
			0,
			0,
			0,
			691,
			0,
			691,
			0,
			691,
			0,
			691,
			691,
			691,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			691,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			691,
			691,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			691,
			0,
			0,
			602,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			57,
			24,
			58,
			25,
			1212,
			691,
			26,
			59,
			0,
			60,
			61,
			27,
			62,
			63,
			64,
			28,
			0,
			0,
			0,
			0,
			0,
			65,
			0,
			66,
			30,
			67,
			68,
			69,
			70,
			0,
			0,
			32,
			0,
			0,
			0,
			71,
			33,
			0,
			72,
			73,
			34,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			74,
			0,
			36,
			0,
			37,
			75,
			0,
			0,
			38,
			0,
			76,
			77,
			78,
			79,
			80,
			81,
			39,
			40,
			82,
			83,
			41,
			84,
			0,
			85,
			0,
			0,
			86,
			87,
			0,
			0,
			88,
			89,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			90,
			91,
			92,
			93,
			94,
			0,
			0,
			95,
			96,
			0,
			0,
			0,
			97,
			0,
			0,
			0,
			98,
			0,
			0,
			0,
			0,
			99,
			100,
			101,
			102,
			103,
			0,
			0,
			0,
			104,
			0,
			105,
			0,
			0,
			0,
			0,
			0,
			106,
			107,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			56,
			0,
			108,
			109,
			110,
			111,
			0,
			1213,
			57,
			24,
			58,
			25,
			0,
			0,
			26,
			59,
			0,
			60,
			61,
			27,
			62,
			63,
			64,
			28,
			0,
			0,
			0,
			0,
			0,
			65,
			0,
			66,
			30,
			67,
			68,
			69,
			70,
			0,
			0,
			32,
			0,
			0,
			0,
			71,
			33,
			0,
			72,
			73,
			34,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			74,
			0,
			36,
			0,
			37,
			75,
			0,
			0,
			38,
			0,
			76,
			77,
			78,
			79,
			80,
			81,
			39,
			40,
			82,
			83,
			41,
			84,
			0,
			85,
			0,
			0,
			86,
			87,
			0,
			0,
			88,
			89,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			90,
			91,
			92,
			93,
			94,
			0,
			0,
			95,
			96,
			0,
			0,
			0,
			97,
			0,
			0,
			0,
			98,
			0,
			0,
			0,
			0,
			99,
			100,
			101,
			102,
			103,
			0,
			0,
			0,
			104,
			0,
			105,
			0,
			0,
			0,
			0,
			0,
			106,
			107,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			273,
			0,
			0,
			0,
			108,
			109,
			110,
			111,
			57,
			24,
			58,
			25,
			0,
			0,
			26,
			59,
			0,
			60,
			61,
			27,
			62,
			63,
			64,
			28,
			0,
			0,
			0,
			0,
			0,
			65,
			0,
			66,
			30,
			67,
			68,
			69,
			70,
			0,
			0,
			32,
			0,
			0,
			0,
			71,
			33,
			0,
			72,
			73,
			34,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			74,
			0,
			36,
			0,
			37,
			75,
			0,
			0,
			38,
			0,
			76,
			77,
			78,
			79,
			80,
			81,
			39,
			40,
			82,
			83,
			41,
			84,
			0,
			85,
			0,
			0,
			86,
			87,
			0,
			0,
			88,
			89,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			90,
			91,
			92,
			93,
			94,
			0,
			0,
			95,
			96,
			0,
			0,
			0,
			97,
			0,
			0,
			0,
			98,
			0,
			0,
			0,
			0,
			99,
			100,
			101,
			102,
			103,
			0,
			0,
			0,
			104,
			0,
			105,
			0,
			0,
			0,
			0,
			0,
			106,
			107,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			602,
			0,
			0,
			0,
			108,
			109,
			110,
			111,
			57,
			24,
			58,
			25,
			0,
			0,
			26,
			59,
			0,
			60,
			61,
			27,
			62,
			63,
			64,
			28,
			0,
			0,
			0,
			0,
			0,
			65,
			0,
			66,
			30,
			67,
			68,
			69,
			70,
			0,
			0,
			32,
			0,
			0,
			0,
			71,
			33,
			0,
			72,
			73,
			34,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			74,
			0,
			36,
			0,
			37,
			75,
			0,
			0,
			38,
			0,
			76,
			77,
			78,
			79,
			80,
			81,
			39,
			40,
			82,
			83,
			41,
			84,
			0,
			85,
			0,
			0,
			86,
			87,
			0,
			0,
			88,
			89,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			90,
			91,
			92,
			93,
			94,
			0,
			0,
			95,
			96,
			0,
			0,
			0,
			97,
			0,
			0,
			0,
			98,
			0,
			0,
			0,
			0,
			99,
			100,
			101,
			102,
			103,
			0,
			0,
			0,
			104,
			0,
			105,
			0,
			0,
			0,
			0,
			0,
			106,
			107,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1080,
			0,
			0,
			0,
			108,
			109,
			110,
			111,
			1080,
			1080,
			1080,
			1080,
			0,
			0,
			1080,
			1080,
			0,
			1080,
			1080,
			1080,
			1080,
			1080,
			1080,
			1080,
			0,
			0,
			0,
			0,
			0,
			1080,
			0,
			1080,
			1080,
			1080,
			1080,
			1080,
			1080,
			0,
			0,
			1080,
			0,
			0,
			0,
			1080,
			1080,
			0,
			1080,
			1080,
			1080,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1080,
			0,
			1080,
			0,
			1080,
			1080,
			0,
			0,
			1080,
			0,
			1080,
			1080,
			1080,
			1080,
			1080,
			1080,
			1080,
			1080,
			1080,
			1080,
			1080,
			1080,
			0,
			1080,
			0,
			0,
			1080,
			1080,
			0,
			0,
			1080,
			1080,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1080,
			1080,
			1080,
			1080,
			1080,
			0,
			0,
			1080,
			1080,
			0,
			0,
			0,
			1080,
			0,
			0,
			0,
			1080,
			0,
			0,
			0,
			0,
			1080,
			1080,
			1080,
			1080,
			1080,
			0,
			0,
			0,
			1080,
			0,
			1080,
			0,
			0,
			0,
			0,
			0,
			1080,
			1080,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			645,
			0,
			0,
			0,
			1080,
			1080,
			1080,
			1080,
			57,
			24,
			0,
			25,
			0,
			0,
			26,
			260,
			0,
			0,
			0,
			27,
			62,
			63,
			0,
			28,
			0,
			0,
			24,
			0,
			25,
			65,
			0,
			26,
			30,
			0,
			0,
			0,
			27,
			0,
			0,
			32,
			28,
			0,
			0,
			0,
			33,
			0,
			72,
			73,
			34,
			30,
			646,
			0,
			0,
			0,
			0,
			0,
			32,
			647,
			0,
			0,
			36,
			33,
			37,
			75,
			0,
			34,
			38,
			0,
			0,
			77,
			0,
			79,
			0,
			81,
			39,
			40,
			261,
			36,
			41,
			37,
			0,
			0,
			0,
			38,
			0,
			648,
			0,
			0,
			88,
			89,
			0,
			39,
			40,
			0,
			0,
			41,
			0,
			0,
			85,
			0,
			0,
			0,
			0,
			90,
			91,
			92,
			93,
			94,
			0,
			0,
			95,
			96,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			98,
			0,
			0,
			649,
			0,
			298,
			100,
			101,
			102,
			103,
			0,
			0,
			0,
			104,
			0,
			105,
			0,
			0,
			0,
			0,
			0,
			106,
			107,
			0,
			0,
			0,
			0,
			0,
			0,
			57,
			24,
			0,
			25,
			0,
			0,
			26,
			260,
			0,
			0,
			0,
			27,
			62,
			63,
			0,
			28,
			0,
			108,
			109,
			110,
			111,
			65,
			0,
			0,
			30,
			0,
			0,
			0,
			0,
			0,
			0,
			32,
			0,
			0,
			0,
			331,
			33,
			0,
			72,
			73,
			34,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			36,
			0,
			37,
			75,
			0,
			0,
			38,
			0,
			0,
			77,
			0,
			79,
			0,
			81,
			39,
			40,
			261,
			0,
			41,
			0,
			0,
			0,
			0,
			0,
			0,
			87,
			0,
			0,
			88,
			89,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			90,
			91,
			92,
			93,
			815,
			0,
			0,
			95,
			96,
			0,
			0,
			0,
			816,
			1127,
			0,
			0,
			98,
			0,
			0,
			0,
			0,
			0,
			100,
			101,
			102,
			103,
			0,
			0,
			0,
			104,
			0,
			105,
			0,
			0,
			0,
			0,
			0,
			106,
			107,
			0,
			"Not showing all elements because this array is too big (20417 elements)"
		};

		// Token: 0x040007FB RID: 2043
		protected static readonly short[] yyCheck = new short[]
		{
			17,
			306,
			17,
			307,
			18,
			52,
			17,
			17,
			4,
			52,
			549,
			240,
			196,
			361,
			551,
			321,
			238,
			296,
			195,
			60,
			499,
			330,
			521,
			69,
			422,
			392,
			345,
			305,
			305,
			604,
			165,
			199,
			253,
			392,
			611,
			422,
			119,
			121,
			121,
			303,
			1042,
			403,
			88,
			89,
			806,
			838,
			808,
			93,
			78,
			59,
			1293,
			0,
			386,
			1250,
			995,
			622,
			631,
			74,
			422,
			402,
			256,
			78,
			45,
			340,
			647,
			48,
			0,
			256,
			256,
			115,
			80,
			1363,
			82,
			268,
			256,
			1047,
			256,
			256,
			95,
			17,
			386,
			98,
			256,
			256,
			6,
			256,
			256,
			1372,
			1527,
			268,
			852,
			1334,
			17,
			855,
			282,
			256,
			256,
			1287,
			60,
			145,
			1397,
			62,
			256,
			256,
			1347,
			66,
			67,
			68,
			1245,
			70,
			71,
			256,
			256,
			256,
			75,
			76,
			325,
			372,
			17,
			1416,
			81,
			82,
			256,
			84,
			375,
			86,
			314,
			256,
			17,
			268,
			91,
			92,
			343,
			405,
			371,
			256,
			256,
			504,
			257,
			256,
			256,
			17,
			256,
			380,
			17,
			417,
			256,
			1439,
			165,
			196,
			165,
			1288,
			256,
			196,
			165,
			165,
			256,
			339,
			0,
			120,
			371,
			1351,
			344,
			371,
			346,
			294,
			443,
			349,
			350,
			339,
			352,
			353,
			380,
			424,
			344,
			256,
			346,
			744,
			433,
			349,
			350,
			402,
			352,
			353,
			17,
			1470,
			375,
			256,
			17,
			256,
			17,
			238,
			17,
			1478,
			256,
			238,
			378,
			17,
			378,
			395,
			371,
			374,
			376,
			376,
			375,
			378,
			252,
			253,
			378,
			363,
			371,
			371,
			373,
			422,
			256,
			375,
			583,
			1656,
			17,
			363,
			266,
			374,
			418,
			376,
			583,
			378,
			545,
			165,
			1200,
			425,
			597,
			426,
			375,
			372,
			422,
			424,
			253,
			371,
			467,
			373,
			165,
			375,
			259,
			548,
			466,
			228,
			329,
			426,
			332,
			332,
			1535,
			433,
			293,
			424,
			358,
			256,
			604,
			303,
			592,
			342,
			433,
			60,
			303,
			433,
			424,
			64,
			165,
			1208,
			422,
			636,
			380,
			385,
			809,
			373,
			641,
			642,
			165,
			422,
			422,
			296,
			433,
			424,
			641,
			631,
			301,
			302,
			592,
			426,
			334,
			165,
			424,
			868,
			165,
			427,
			1591,
			638,
			365,
			256,
			344,
			316,
			261,
			422,
			422,
			335,
			265,
			422,
			422,
			324,
			257,
			326,
			356,
			479,
			234,
			330,
			256,
			325,
			622,
			256,
			385,
			256,
			903,
			1618,
			256,
			385,
			428,
			429,
			430,
			431,
			363,
			346,
			347,
			293,
			165,
			1630,
			277,
			1632,
			165,
			379,
			165,
			294,
			165,
			422,
			343,
			256,
			305,
			165,
			361,
			256,
			394,
			372,
			361,
			306,
			398,
			256,
			268,
			256,
			402,
			403,
			1120,
			256,
			390,
			1247,
			276,
			294,
			326,
			371,
			165,
			373,
			256,
			256,
			371,
			256,
			256,
			390,
			375,
			306,
			377,
			378,
			256,
			380,
			1173,
			422,
			256,
			1520,
			385,
			402,
			403,
			422,
			1585,
			294,
			407,
			408,
			409,
			410,
			411,
			412,
			413,
			414,
			415,
			416,
			417,
			433,
			448,
			449,
			1290,
			339,
			452,
			974,
			1651,
			343,
			433,
			745,
			1549,
			1550,
			351,
			257,
			1553,
			638,
			419,
			466,
			363,
			380,
			439,
			466,
			256,
			88,
			89,
			474,
			256,
			1566,
			272,
			474,
			1569,
			256,
			373,
			277,
			378,
			371,
			744,
			281,
			1683,
			375,
			1204,
			377,
			378,
			379,
			380,
			1584,
			111,
			363,
			363,
			385,
			357,
			256,
			296,
			376,
			818,
			1459,
			363,
			256,
			422,
			374,
			371,
			376,
			770,
			378,
			256,
			422,
			373,
			372,
			1007,
			1608,
			377,
			256,
			520,
			378,
			522,
			256,
			379,
			422,
			423,
			323,
			379,
			371,
			793,
			390,
			985,
			470,
			471,
			856,
			375,
			378,
			380,
			476,
			293,
			379,
			358,
			856,
			376,
			325,
			342,
			1263,
			376,
			549,
			514,
			256,
			256,
			262,
			422,
			422,
			1272,
			339,
			433,
			266,
			256,
			372,
			344,
			422,
			346,
			88,
			89,
			349,
			350,
			752,
			352,
			353,
			924,
			326,
			895,
			575,
			547,
			335,
			549,
			263,
			551,
			343,
			199,
			924,
			256,
			1301,
			203,
			204,
			111,
			298,
			376,
			363,
			264,
			811,
			910,
			256,
			363,
			395,
			886,
			376,
			564,
			516,
			785,
			778,
			341,
			521,
			924,
			314,
			361,
			305,
			581,
			380,
			256,
			266,
			585,
			1146,
			257,
			321,
			740,
			376,
			418,
			621,
			433,
			1114,
			305,
			376,
			886,
			598,
			256,
			600,
			374,
			315,
			596,
			395,
			378,
			604,
			373,
			637,
			638,
			604,
			373,
			263,
			422,
			305,
			682,
			682,
			372,
			611,
			563,
			372,
			422,
			433,
			269,
			325,
			277,
			422,
			418,
			380,
			281,
			256,
			432,
			314,
			631,
			425,
			433,
			432,
			631,
			638,
			373,
			422,
			432,
			372,
			379,
			382,
			383,
			646,
			647,
			378,
			295,
			340,
			203,
			204,
			372,
			372,
			385,
			973,
			365,
			687,
			378,
			372,
			307,
			376,
			1223,
			315,
			376,
			339,
			375,
			256,
			315,
			375,
			344,
			376,
			346,
			269,
			321,
			349,
			350,
			433,
			352,
			353,
			433,
			339,
			971,
			377,
			974,
			373,
			344,
			256,
			346,
			342,
			286,
			349,
			350,
			422,
			352,
			353,
			379,
			727,
			1001,
			729,
			646,
			647,
			433,
			376,
			823,
			352,
			353,
			375,
			738,
			1268,
			1379,
			916,
			339,
			433,
			433,
			424,
			269,
			344,
			433,
			346,
			433,
			433,
			349,
			350,
			725,
			352,
			353,
			272,
			433,
			730,
			731,
			373,
			733,
			427,
			381,
			377,
			305,
			256,
			521,
			386,
			1620,
			1621,
			295,
			1004,
			1006,
			272,
			1379,
			272,
			778,
			422,
			339,
			296,
			256,
			256,
			1042,
			344,
			380,
			346,
			1427,
			433,
			349,
			350,
			315,
			352,
			353,
			269,
			796,
			1378,
			1379,
			296,
			378,
			296,
			372,
			1155,
			421,
			433,
			1158,
			778,
			323,
			1161,
			377,
			811,
			286,
			380,
			1155,
			427,
			811,
			1158,
			818,
			1400,
			1161,
			341,
			793,
			380,
			1427,
			372,
			323,
			1677,
			323,
			352,
			353,
			448,
			449,
			1115,
			956,
			357,
			433,
			1155,
			809,
			371,
			1158,
			1379,
			420,
			1161,
			1379,
			816,
			1427,
			818,
			256,
			1302,
			339,
			604,
			422,
			373,
			1117,
			344,
			339,
			346,
			381,
			1141,
			349,
			350,
			479,
			352,
			353,
			1379,
			339,
			378,
			390,
			1450,
			357,
			344,
			1131,
			346,
			433,
			422,
			349,
			350,
			631,
			352,
			353,
			845,
			1375,
			847,
			390,
			885,
			364,
			858,
			371,
			1427,
			377,
			862,
			1427,
			646,
			647,
			771,
			379,
			375,
			421,
			377,
			1379,
			872,
			866,
			390,
			868,
			1379,
			1081,
			907,
			388,
			420,
			910,
			1379,
			379,
			1427,
			343,
			1217,
			916,
			17,
			396,
			397,
			6,
			373,
			393,
			923,
			377,
			448,
			449,
			380,
			395,
			924,
			339,
			17,
			903,
			385,
			1012,
			343,
			21,
			372,
			416,
			910,
			343,
			1072,
			371,
			378,
			433,
			916,
			424,
			918,
			1427,
			427,
			951,
			418,
			339,
			1427,
			951,
			343,
			1078,
			928,
			425,
			1427,
			60,
			385,
			371,
			371,
			64,
			432,
			1268,
			375,
			53,
			377,
			378,
			395,
			380,
			357,
			60,
			592,
			1054,
			385,
			64,
			1212,
			272,
			375,
			385,
			377,
			371,
			277,
			339,
			903,
			375,
			281,
			343,
			1250,
			395,
			377,
			418,
			343,
			395,
			385,
			969,
			1482,
			971,
			425,
			88,
			89,
			296,
			622,
			390,
			395,
			924,
			339,
			343,
			343,
			1012,
			1496,
			339,
			418,
			987,
			339,
			371,
			418,
			1253,
			992,
			425,
			371,
			995,
			111,
			425,
			380,
			1465,
			432,
			418,
			323,
			380,
			432,
			1005,
			1518,
			1007,
			425,
			371,
			371,
			1477,
			371,
			357,
			389,
			432,
			1045,
			371,
			380,
			380,
			371,
			342,
			357,
			343,
			806,
			1054,
			808,
			371,
			363,
			398,
			399,
			375,
			1363,
			1465,
			1496,
			681,
			165,
			1299,
			373,
			1243,
			371,
			376,
			377,
			378,
			377,
			1477,
			390,
			380,
			395,
			380,
			165,
			371,
			1081,
			1573,
			372,
			390,
			961,
			306,
			376,
			308,
			306,
			375,
			380,
			1063,
			313,
			1065,
			1010,
			313,
			1068,
			1098,
			852,
			418,
			404,
			855,
			372,
			1104,
			325,
			372,
			425,
			325,
			378,
			1081,
			371,
			378,
			394,
			386,
			387,
			422,
			203,
			204,
			385,
			380,
			374,
			375,
			740,
			418,
			378,
			371,
			744,
			400,
			401,
			375,
			425,
			377,
			378,
			372,
			380,
			1319,
			1439,
			376,
			1315,
			385,
			306,
			380,
			1114,
			1635,
			373,
			1117,
			1285,
			313,
			378,
			903,
			380,
			1385,
			770,
			1440,
			374,
			385,
			1157,
			681,
			378,
			1155,
			1447,
			419,
			1158,
			372,
			364,
			1161,
			1138,
			376,
			372,
			378,
			924,
			1493,
			372,
			419,
			378,
			375,
			1148,
			377,
			378,
			1440,
			1672,
			1146,
			269,
			374,
			422,
			385,
			1447,
			378,
			1510,
			422,
			374,
			1163,
			293,
			1165,
			378,
			1167,
			396,
			397,
			1170,
			277,
			1459,
			374,
			256,
			1697,
			1698,
			378,
			293,
			1529,
			295,
			1531,
			374,
			1088,
			376,
			1090,
			378,
			1092,
			416,
			377,
			374,
			375,
			307,
			377,
			378,
			379,
			424,
			326,
			844,
			427,
			315,
			1155,
			1181,
			374,
			1158,
			433,
			321,
			378,
			1208,
			371,
			1155,
			326,
			1212,
			1158,
			1243,
			380,
			1161,
			863,
			376,
			1217,
			378,
			374,
			380,
			1217,
			375,
			378,
			377,
			1227,
			1228,
			374,
			1223,
			364,
			361,
			378,
			402,
			403,
			882,
			352,
			353,
			372,
			886,
			380,
			375,
			1243,
			377,
			378,
			361,
			1244,
			256,
			376,
			1250,
			378,
			376,
			1253,
			378,
			1285,
			380,
			1287,
			374,
			1285,
			376,
			1287,
			378,
			396,
			397,
			375,
			381,
			380,
			372,
			379,
			1268,
			386,
			376,
			374,
			1268,
			377,
			1276,
			378,
			88,
			89,
			372,
			256,
			1282,
			416,
			376,
			1315,
			372,
			372,
			373,
			1315,
			376,
			424,
			844,
			422,
			427,
			294,
			376,
			1585,
			378,
			1299,
			433,
			111,
			427,
			428,
			429,
			430,
			421,
			422,
			378,
			379,
			956,
			863,
			1312,
			1313,
			390,
			391,
			392,
			378,
			374,
			380,
			376,
			1351,
			366,
			367,
			378,
			1351,
			380,
			380,
			974,
			975,
			882,
			354,
			355,
			448,
			449,
			374,
			294,
			376,
			1120,
			374,
			374,
			376,
			376,
			376,
			422,
			378,
			1375,
			343,
			1378,
			1379,
			354,
			355,
			1378,
			1379,
			376,
			374,
			378,
			376,
			374,
			261,
			376,
			378,
			376,
			380,
			1364,
			1363,
			378,
			380,
			380,
			1363,
			1400,
			372,
			373,
			1155,
			1400,
			1375,
			1158,
			422,
			378,
			1161,
			380,
			373,
			284,
			1383,
			378,
			1385,
			380,
			418,
			419,
			368,
			369,
			356,
			521,
			203,
			204,
			297,
			1042,
			1427,
			366,
			367,
			302,
			1427,
			1431,
			366,
			367,
			307,
			521,
			309,
			310,
			311,
			312,
			368,
			369,
			423,
			424,
			317,
			425,
			426,
			379,
			321,
			1450,
			1709,
			1204,
			975,
			1450,
			431,
			432,
			418,
			376,
			376,
			380,
			333,
			1078,
			385,
			336,
			1217,
			338,
			372,
			378,
			376,
			1086,
			1439,
			385,
			433,
			376,
			1439,
			422,
			294,
			294,
			378,
			1443,
			376,
			376,
			256,
			376,
			378,
			256,
			375,
			432,
			269,
			362,
			380,
			432,
			406,
			407,
			408,
			409,
			410,
			411,
			412,
			413,
			414,
			415,
			294,
			294,
			604,
			385,
			592,
			422,
			376,
			378,
			1263,
			377,
			379,
			378,
			295,
			1268,
			377,
			422,
			604,
			1272,
			376,
			385,
			380,
			378,
			378,
			378,
			307,
			378,
			427,
			433,
			376,
			631,
			371,
			1504,
			315,
			378,
			622,
			1501,
			378,
			376,
			421,
			379,
			343,
			378,
			376,
			631,
			646,
			647,
			1301,
			422,
			294,
			294,
			378,
			374,
			379,
			375,
			371,
			256,
			256,
			422,
			646,
			647,
			1563,
			1086,
			378,
			422,
			1563,
			256,
			256,
			385,
			280,
			352,
			353,
			256,
			371,
			343,
			372,
			376,
			376,
			1580,
			88,
			89,
			385,
			1580,
			725,
			380,
			298,
			376,
			378,
			730,
			731,
			374,
			1593,
			1594,
			380,
			681,
			1593,
			1594,
			375,
			378,
			381,
			376,
			1573,
			111,
			380,
			378,
			376,
			1571,
			374,
			376,
			1363,
			421,
			347,
			385,
			1585,
			427,
			385,
			371,
			256,
			1620,
			1621,
			385,
			385,
			1620,
			1621,
			256,
			376,
			372,
			376,
			347,
			379,
			1602,
			378,
			374,
			376,
			375,
			374,
			374,
			339,
			371,
			421,
			348,
			372,
			94,
			422,
			378,
			348,
			376,
			375,
			100,
			101,
			102,
			103,
			104,
			105,
			106,
			107,
			422,
			744,
			256,
			385,
			372,
			371,
			371,
			1635,
			380,
			371,
			448,
			449,
			372,
			356,
			375,
			1289,
			337,
			380,
			305,
			1677,
			372,
			378,
			376,
			1677,
			372,
			376,
			422,
			770,
			372,
			1439,
			380,
			422,
			373,
			375,
			199,
			371,
			375,
			422,
			203,
			204,
			1697,
			1698,
			375,
			422,
			1672,
			375,
			377,
			385,
			375,
			806,
			343,
			808,
			371,
			375,
			380,
			373,
			385,
			377,
			375,
			1680,
			1681,
			376,
			376,
			806,
			377,
			808,
			1687,
			1688,
			378,
			1697,
			1698,
			378,
			378,
			256,
			256,
			378,
			380,
			376,
			376,
			380,
			1354,
			1709,
			374,
			265,
			380,
			267,
			376,
			422,
			270,
			380,
			422,
			376,
			376,
			275,
			422,
			852,
			385,
			279,
			855,
			422,
			372,
			844,
			385,
			1377,
			269,
			374,
			288,
			372,
			376,
			852,
			315,
			263,
			855,
			295,
			375,
			375,
			372,
			376,
			300,
			385,
			863,
			376,
			304,
			380,
			0,
			422,
			0,
			371,
			380,
			372,
			295,
			380,
			0,
			380,
			316,
			376,
			318,
			372,
			376,
			882,
			322,
			371,
			307,
			886,
			376,
			422,
			903,
			374,
			330,
			331,
			315,
			380,
			334,
			372,
			376,
			337,
			321,
			372,
			376,
			380,
			903,
			374,
			371,
			380,
			1438,
			372,
			1440,
			924,
			376,
			422,
			380,
			969,
			422,
			1447,
			376,
			376,
			380,
			372,
			376,
			372,
			371,
			924,
			1456,
			1457,
			372,
			1459,
			380,
			352,
			353,
			315,
			377,
			1465,
			372,
			263,
			380,
			380,
			380,
			1377,
			51,
			380,
			5,
			309,
			380,
			1477,
			380,
			380,
			380,
			1005,
			1482,
			12,
			1484,
			1078,
			1243,
			1487,
			1243,
			1400,
			381,
			956,
			1450,
			1455,
			1295,
			386,
			1496,
			1625,
			1641,
			1588,
			1576,
			256,
			1605,
			1501,
			978,
			974,
			975,
			978,
			1571,
			784,
			265,
			345,
			267,
			1482,
			422,
			270,
			978,
			681,
			1518,
			973,
			275,
			355,
			1415,
			1427,
			279,
			1681,
			1502,
			1688,
			1682,
			1598,
			421,
			1351,
			1438,
			288,
			1594,
			1531,
			256,
			1593,
			951,
			1063,
			295,
			1065,
			321,
			1354,
			1068,
			300,
			816,
			916,
			1001,
			304,
			1456,
			1457,
			1131,
			778,
			72,
			348,
			811,
			448,
			449,
			422,
			422,
			316,
			600,
			318,
			1161,
			1157,
			433,
			322,
			647,
			434,
			436,
			435,
			886,
			1042,
			1438,
			330,
			331,
			437,
			1484,
			334,
			438,
			1487,
			337,
			1249,
			418,
			419,
			420,
			592,
			479,
			423,
			424,
			425,
			426,
			427,
			428,
			429,
			430,
			431,
			432,
			433,
			434,
			435,
			436,
			437,
			438,
			1331,
			165,
			1217,
			1116,
			1228,
			1141,
			1100,
			1202,
			1217,
			1215,
			565,
			1016,
			1086,
			1302,
			339,
			1271,
			457,
			736,
			1433,
			344,
			457,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			1120,
			946,
			1163,
			1313,
			1165,
			-1,
			1167,
			953,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			1120,
			372,
			0,
			374,
			-1,
			376,
			-1,
			378,
			379,
			380,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			422,
			388,
			389,
			-1,
			-1,
			1155,
			393,
			394,
			1158,
			-1,
			-1,
			1161,
			-1,
			844,
			-1,
			402,
			403,
			404,
			405,
			1155,
			-1,
			-1,
			1158,
			-1,
			261,
			1161,
			-1,
			-1,
			-1,
			-1,
			417,
			-1,
			863,
			-1,
			592,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			545,
			-1,
			433,
			284,
			-1,
			-1,
			-1,
			882,
			-1,
			-1,
			1204,
			-1,
			261,
			-1,
			-1,
			-1,
			297,
			-1,
			-1,
			-1,
			622,
			302,
			-1,
			1217,
			1204,
			-1,
			307,
			-1,
			309,
			310,
			311,
			312,
			-1,
			-1,
			315,
			284,
			317,
			1217,
			-1,
			-1,
			321,
			1276,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			297,
			-1,
			-1,
			-1,
			333,
			302,
			-1,
			336,
			305,
			338,
			307,
			-1,
			309,
			310,
			311,
			312,
			-1,
			-1,
			-1,
			-1,
			317,
			1263,
			-1,
			-1,
			321,
			-1,
			1268,
			-1,
			325,
			1312,
			1272,
			681,
			-1,
			362,
			627,
			1263,
			333,
			-1,
			-1,
			336,
			1268,
			338,
			-1,
			372,
			1272,
			-1,
			-1,
			-1,
			256,
			-1,
			-1,
			-1,
			975,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			1301,
			357,
			1289,
			0,
			-1,
			-1,
			362,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			1301,
			-1,
			372,
			373,
			-1,
			375,
			-1,
			377,
			1364,
			-1,
			-1,
			-1,
			678,
			679,
			-1,
			-1,
			-1,
			740,
			-1,
			-1,
			390,
			744,
			-1,
			-1,
			-1,
			-1,
			-1,
			1383,
			-1,
			-1,
			696,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			1042,
			770,
			1363,
			-1,
			-1,
			0,
			422,
			1354,
			-1,
			-1,
			-1,
			-1,
			339,
			-1,
			-1,
			-1,
			1363,
			344,
			-1,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			1377,
			256,
			-1,
			-1,
			-1,
			-1,
			-1,
			262,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			1086,
			372,
			-1,
			374,
			-1,
			376,
			-1,
			378,
			379,
			380,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			394,
			-1,
			-1,
			-1,
			-1,
			-1,
			298,
			-1,
			844,
			-1,
			-1,
			1439,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			1438,
			1439,
			1440,
			863,
			-1,
			-1,
			-1,
			-1,
			-1,
			1447,
			-1,
			-1,
			815,
			-1,
			-1,
			433,
			-1,
			-1,
			1456,
			1457,
			-1,
			1459,
			882,
			339,
			-1,
			-1,
			886,
			343,
			344,
			-1,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			357,
			-1,
			-1,
			-1,
			-1,
			1484,
			363,
			0,
			1487,
			366,
			367,
			-1,
			-1,
			-1,
			371,
			372,
			373,
			374,
			375,
			376,
			377,
			378,
			379,
			380,
			-1,
			382,
			383,
			-1,
			-1,
			386,
			387,
			388,
			389,
			390,
			-1,
			-1,
			393,
			394,
			-1,
			-1,
			-1,
			398,
			399,
			400,
			401,
			402,
			403,
			404,
			405,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			956,
			-1,
			-1,
			-1,
			-1,
			417,
			-1,
			-1,
			420,
			-1,
			422,
			-1,
			424,
			257,
			-1,
			427,
			-1,
			261,
			974,
			975,
			-1,
			433,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			272,
			-1,
			-1,
			-1,
			-1,
			277,
			-1,
			-1,
			-1,
			281,
			-1,
			-1,
			284,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			296,
			297,
			-1,
			-1,
			-1,
			301,
			302,
			261,
			1289,
			263,
			-1,
			307,
			-1,
			309,
			310,
			311,
			312,
			-1,
			-1,
			-1,
			-1,
			317,
			256,
			-1,
			-1,
			321,
			-1,
			323,
			262,
			-1,
			284,
			-1,
			-1,
			-1,
			1042,
			-1,
			-1,
			333,
			-1,
			335,
			336,
			-1,
			338,
			297,
			-1,
			-1,
			342,
			-1,
			302,
			-1,
			1001,
			-1,
			-1,
			307,
			-1,
			309,
			310,
			311,
			312,
			-1,
			-1,
			315,
			-1,
			317,
			298,
			-1,
			362,
			321,
			-1,
			-1,
			1078,
			0,
			-1,
			1354,
			-1,
			-1,
			372,
			373,
			1086,
			333,
			-1,
			-1,
			336,
			-1,
			338,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			1377,
			-1,
			1049,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			339,
			-1,
			-1,
			362,
			343,
			344,
			-1,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			363,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			371,
			372,
			373,
			374,
			375,
			376,
			-1,
			378,
			379,
			380,
			-1,
			382,
			383,
			-1,
			-1,
			386,
			387,
			388,
			389,
			256,
			1438,
			-1,
			393,
			394,
			261,
			262,
			-1,
			398,
			399,
			400,
			401,
			402,
			403,
			404,
			405,
			-1,
			-1,
			-1,
			1456,
			1457,
			-1,
			1459,
			-1,
			-1,
			-1,
			-1,
			417,
			284,
			-1,
			420,
			-1,
			422,
			-1,
			424,
			-1,
			-1,
			427,
			-1,
			-1,
			-1,
			297,
			298,
			433,
			-1,
			-1,
			302,
			1484,
			-1,
			305,
			1487,
			307,
			-1,
			309,
			310,
			311,
			312,
			-1,
			-1,
			-1,
			0,
			317,
			-1,
			-1,
			-1,
			321,
			-1,
			-1,
			-1,
			325,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			333,
			-1,
			-1,
			336,
			-1,
			338,
			339,
			-1,
			-1,
			-1,
			343,
			344,
			-1,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			-1,
			-1,
			-1,
			-1,
			-1,
			362,
			363,
			364,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			371,
			372,
			-1,
			374,
			375,
			376,
			377,
			378,
			379,
			380,
			1289,
			382,
			383,
			-1,
			385,
			386,
			387,
			388,
			389,
			390,
			391,
			392,
			393,
			394,
			-1,
			396,
			397,
			398,
			399,
			400,
			401,
			402,
			403,
			404,
			405,
			406,
			407,
			408,
			409,
			410,
			411,
			412,
			413,
			414,
			415,
			416,
			417,
			-1,
			-1,
			420,
			-1,
			422,
			-1,
			424,
			-1,
			-1,
			427,
			257,
			-1,
			-1,
			-1,
			261,
			433,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			272,
			-1,
			-1,
			1354,
			-1,
			277,
			-1,
			-1,
			-1,
			281,
			-1,
			-1,
			284,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			296,
			297,
			1377,
			-1,
			-1,
			301,
			302,
			-1,
			-1,
			-1,
			-1,
			307,
			-1,
			309,
			310,
			311,
			312,
			-1,
			-1,
			-1,
			0,
			317,
			-1,
			-1,
			-1,
			321,
			-1,
			323,
			-1,
			357,
			-1,
			-1,
			-1,
			-1,
			-1,
			363,
			364,
			333,
			-1,
			-1,
			336,
			-1,
			338,
			371,
			-1,
			373,
			342,
			375,
			376,
			377,
			378,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			390,
			1438,
			-1,
			1440,
			362,
			-1,
			396,
			397,
			-1,
			-1,
			1447,
			-1,
			-1,
			371,
			372,
			373,
			-1,
			-1,
			-1,
			1456,
			1457,
			-1,
			1459,
			-1,
			-1,
			-1,
			416,
			-1,
			-1,
			-1,
			-1,
			-1,
			422,
			-1,
			424,
			-1,
			-1,
			427,
			-1,
			-1,
			-1,
			-1,
			-1,
			256,
			257,
			-1,
			-1,
			1484,
			-1,
			-1,
			1487,
			264,
			265,
			266,
			267,
			268,
			-1,
			270,
			271,
			-1,
			273,
			274,
			275,
			276,
			277,
			278,
			279,
			280,
			-1,
			-1,
			-1,
			-1,
			285,
			-1,
			287,
			288,
			289,
			290,
			291,
			292,
			-1,
			-1,
			295,
			-1,
			-1,
			-1,
			299,
			300,
			0,
			302,
			303,
			304,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			314,
			-1,
			316,
			-1,
			318,
			319,
			-1,
			-1,
			322,
			-1,
			324,
			325,
			326,
			327,
			328,
			329,
			330,
			331,
			332,
			333,
			334,
			335,
			-1,
			337,
			-1,
			-1,
			340,
			341,
			-1,
			-1,
			344,
			345,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			359,
			360,
			361,
			362,
			363,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			371,
			372,
			-1,
			-1,
			375,
			-1,
			-1,
			-1,
			-1,
			380,
			381,
			382,
			383,
			384,
			-1,
			-1,
			-1,
			388,
			-1,
			390,
			-1,
			-1,
			-1,
			-1,
			-1,
			396,
			397,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			421,
			422,
			423,
			424,
			-1,
			426,
			256,
			257,
			-1,
			-1,
			-1,
			-1,
			433,
			-1,
			264,
			265,
			266,
			267,
			268,
			-1,
			270,
			271,
			-1,
			273,
			274,
			275,
			276,
			277,
			278,
			279,
			0,
			-1,
			-1,
			-1,
			-1,
			285,
			-1,
			287,
			288,
			289,
			290,
			291,
			292,
			-1,
			-1,
			295,
			-1,
			-1,
			-1,
			299,
			300,
			-1,
			302,
			303,
			304,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			314,
			-1,
			316,
			-1,
			318,
			319,
			-1,
			-1,
			322,
			-1,
			324,
			325,
			326,
			327,
			328,
			329,
			330,
			331,
			332,
			333,
			334,
			335,
			-1,
			337,
			-1,
			-1,
			340,
			341,
			-1,
			-1,
			344,
			345,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			359,
			360,
			361,
			362,
			363,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			371,
			372,
			-1,
			-1,
			375,
			-1,
			-1,
			-1,
			-1,
			380,
			381,
			382,
			383,
			384,
			-1,
			256,
			-1,
			388,
			-1,
			390,
			261,
			262,
			-1,
			-1,
			-1,
			396,
			397,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			284,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			421,
			422,
			423,
			424,
			-1,
			426,
			297,
			298,
			-1,
			0,
			-1,
			302,
			433,
			-1,
			305,
			-1,
			307,
			-1,
			309,
			310,
			311,
			312,
			-1,
			-1,
			-1,
			-1,
			317,
			-1,
			-1,
			-1,
			321,
			-1,
			-1,
			-1,
			325,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			333,
			-1,
			-1,
			336,
			-1,
			338,
			339,
			-1,
			-1,
			-1,
			343,
			344,
			-1,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			357,
			-1,
			-1,
			-1,
			-1,
			362,
			363,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			371,
			372,
			373,
			374,
			375,
			376,
			377,
			378,
			379,
			380,
			-1,
			382,
			383,
			-1,
			-1,
			386,
			387,
			388,
			389,
			390,
			-1,
			-1,
			393,
			394,
			-1,
			-1,
			-1,
			398,
			399,
			400,
			401,
			402,
			403,
			404,
			405,
			256,
			-1,
			-1,
			-1,
			-1,
			261,
			262,
			-1,
			-1,
			-1,
			-1,
			417,
			-1,
			-1,
			420,
			-1,
			422,
			-1,
			424,
			-1,
			-1,
			427,
			-1,
			-1,
			-1,
			-1,
			-1,
			433,
			284,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			297,
			298,
			-1,
			0,
			-1,
			302,
			-1,
			-1,
			305,
			-1,
			307,
			-1,
			309,
			310,
			311,
			312,
			-1,
			-1,
			-1,
			-1,
			317,
			-1,
			-1,
			-1,
			321,
			-1,
			-1,
			-1,
			325,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			333,
			-1,
			-1,
			336,
			-1,
			338,
			339,
			-1,
			-1,
			-1,
			343,
			344,
			-1,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			-1,
			-1,
			-1,
			-1,
			-1,
			362,
			363,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			371,
			372,
			373,
			374,
			375,
			376,
			-1,
			378,
			379,
			380,
			-1,
			382,
			383,
			-1,
			-1,
			386,
			387,
			388,
			389,
			-1,
			-1,
			-1,
			393,
			394,
			-1,
			-1,
			-1,
			398,
			399,
			400,
			401,
			402,
			403,
			404,
			405,
			256,
			-1,
			-1,
			-1,
			-1,
			261,
			262,
			-1,
			-1,
			-1,
			-1,
			417,
			-1,
			-1,
			420,
			-1,
			422,
			-1,
			424,
			-1,
			-1,
			427,
			-1,
			-1,
			-1,
			-1,
			-1,
			433,
			284,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			297,
			298,
			-1,
			-1,
			-1,
			302,
			-1,
			-1,
			305,
			-1,
			307,
			-1,
			309,
			310,
			311,
			312,
			-1,
			-1,
			-1,
			-1,
			317,
			-1,
			0,
			-1,
			321,
			-1,
			-1,
			-1,
			325,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			333,
			-1,
			-1,
			336,
			-1,
			338,
			339,
			-1,
			-1,
			-1,
			343,
			344,
			-1,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			-1,
			-1,
			-1,
			-1,
			-1,
			362,
			363,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			371,
			372,
			373,
			374,
			375,
			376,
			-1,
			378,
			379,
			380,
			-1,
			382,
			383,
			-1,
			-1,
			386,
			387,
			388,
			389,
			-1,
			-1,
			-1,
			393,
			394,
			-1,
			-1,
			-1,
			398,
			399,
			400,
			401,
			402,
			403,
			404,
			405,
			256,
			-1,
			-1,
			-1,
			-1,
			261,
			262,
			-1,
			-1,
			-1,
			-1,
			417,
			-1,
			-1,
			420,
			0,
			422,
			-1,
			424,
			-1,
			-1,
			427,
			-1,
			-1,
			-1,
			-1,
			-1,
			433,
			284,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			297,
			298,
			-1,
			-1,
			-1,
			302,
			-1,
			-1,
			305,
			-1,
			307,
			-1,
			309,
			310,
			311,
			312,
			-1,
			-1,
			-1,
			-1,
			317,
			0,
			-1,
			-1,
			321,
			-1,
			-1,
			-1,
			325,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			333,
			-1,
			-1,
			336,
			-1,
			338,
			339,
			-1,
			-1,
			-1,
			343,
			344,
			-1,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			-1,
			-1,
			-1,
			-1,
			-1,
			362,
			363,
			-1,
			0,
			366,
			367,
			-1,
			-1,
			-1,
			371,
			372,
			-1,
			374,
			375,
			376,
			-1,
			378,
			379,
			380,
			-1,
			382,
			383,
			-1,
			-1,
			386,
			387,
			388,
			389,
			-1,
			-1,
			-1,
			393,
			394,
			-1,
			-1,
			-1,
			398,
			399,
			400,
			401,
			402,
			403,
			404,
			405,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			0,
			-1,
			-1,
			-1,
			-1,
			417,
			-1,
			-1,
			420,
			-1,
			422,
			-1,
			-1,
			256,
			257,
			-1,
			-1,
			-1,
			261,
			-1,
			-1,
			433,
			265,
			-1,
			267,
			-1,
			-1,
			270,
			-1,
			272,
			273,
			-1,
			275,
			-1,
			277,
			-1,
			279,
			-1,
			281,
			282,
			283,
			284,
			-1,
			-1,
			287,
			288,
			-1,
			0,
			-1,
			-1,
			293,
			-1,
			295,
			296,
			297,
			-1,
			-1,
			300,
			301,
			302,
			-1,
			304,
			-1,
			-1,
			307,
			-1,
			309,
			310,
			311,
			312,
			-1,
			-1,
			-1,
			316,
			317,
			318,
			-1,
			-1,
			321,
			322,
			323,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			330,
			331,
			-1,
			333,
			334,
			-1,
			336,
			337,
			338,
			-1,
			-1,
			-1,
			342,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			0,
			-1,
			-1,
			-1,
			-1,
			-1,
			257,
			-1,
			-1,
			362,
			261,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			371,
			372,
			373,
			272,
			-1,
			-1,
			-1,
			-1,
			277,
			-1,
			381,
			-1,
			281,
			-1,
			-1,
			284,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			0,
			-1,
			296,
			297,
			-1,
			-1,
			-1,
			301,
			302,
			-1,
			257,
			-1,
			-1,
			307,
			261,
			309,
			310,
			311,
			312,
			-1,
			-1,
			-1,
			-1,
			317,
			-1,
			272,
			422,
			321,
			-1,
			323,
			277,
			-1,
			-1,
			-1,
			281,
			-1,
			-1,
			284,
			-1,
			333,
			-1,
			335,
			336,
			0,
			338,
			-1,
			-1,
			-1,
			342,
			296,
			297,
			-1,
			-1,
			-1,
			301,
			302,
			-1,
			257,
			-1,
			-1,
			307,
			261,
			309,
			310,
			311,
			312,
			-1,
			-1,
			362,
			-1,
			317,
			-1,
			272,
			-1,
			321,
			-1,
			323,
			277,
			-1,
			373,
			-1,
			281,
			-1,
			-1,
			284,
			-1,
			333,
			-1,
			-1,
			336,
			-1,
			338,
			-1,
			-1,
			-1,
			342,
			296,
			297,
			-1,
			-1,
			-1,
			301,
			302,
			-1,
			257,
			-1,
			0,
			307,
			261,
			309,
			310,
			311,
			312,
			-1,
			-1,
			362,
			-1,
			317,
			-1,
			272,
			-1,
			321,
			-1,
			323,
			277,
			372,
			373,
			-1,
			281,
			-1,
			-1,
			284,
			-1,
			333,
			-1,
			-1,
			336,
			-1,
			338,
			-1,
			-1,
			-1,
			342,
			296,
			297,
			-1,
			-1,
			-1,
			301,
			302,
			-1,
			257,
			-1,
			0,
			307,
			261,
			309,
			310,
			311,
			312,
			-1,
			-1,
			362,
			-1,
			317,
			-1,
			272,
			-1,
			321,
			-1,
			323,
			277,
			372,
			373,
			-1,
			281,
			-1,
			-1,
			284,
			-1,
			333,
			-1,
			-1,
			336,
			-1,
			338,
			-1,
			-1,
			-1,
			342,
			296,
			297,
			-1,
			-1,
			-1,
			301,
			302,
			-1,
			-1,
			-1,
			-1,
			307,
			-1,
			309,
			310,
			311,
			312,
			-1,
			-1,
			362,
			-1,
			317,
			-1,
			-1,
			257,
			321,
			-1,
			323,
			261,
			-1,
			373,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			333,
			-1,
			272,
			336,
			-1,
			338,
			-1,
			277,
			-1,
			342,
			-1,
			281,
			-1,
			-1,
			284,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			296,
			297,
			-1,
			362,
			257,
			301,
			302,
			-1,
			261,
			-1,
			-1,
			307,
			-1,
			309,
			310,
			311,
			312,
			-1,
			-1,
			272,
			-1,
			317,
			-1,
			-1,
			277,
			321,
			-1,
			323,
			281,
			-1,
			-1,
			284,
			-1,
			-1,
			-1,
			-1,
			-1,
			333,
			-1,
			-1,
			336,
			-1,
			338,
			296,
			297,
			-1,
			342,
			257,
			301,
			302,
			-1,
			261,
			-1,
			-1,
			307,
			-1,
			309,
			310,
			311,
			312,
			-1,
			-1,
			272,
			-1,
			317,
			-1,
			362,
			277,
			321,
			-1,
			323,
			281,
			-1,
			-1,
			284,
			-1,
			-1,
			-1,
			-1,
			-1,
			333,
			-1,
			-1,
			336,
			-1,
			338,
			296,
			297,
			-1,
			342,
			-1,
			301,
			302,
			-1,
			-1,
			-1,
			-1,
			307,
			-1,
			309,
			310,
			311,
			312,
			-1,
			-1,
			-1,
			-1,
			317,
			-1,
			362,
			257,
			321,
			-1,
			323,
			261,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			333,
			-1,
			272,
			336,
			-1,
			338,
			-1,
			277,
			-1,
			342,
			-1,
			281,
			-1,
			-1,
			284,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			296,
			297,
			-1,
			362,
			-1,
			301,
			302,
			-1,
			257,
			-1,
			-1,
			307,
			261,
			309,
			310,
			311,
			312,
			-1,
			-1,
			-1,
			-1,
			317,
			-1,
			272,
			-1,
			321,
			-1,
			323,
			277,
			-1,
			-1,
			-1,
			281,
			-1,
			-1,
			284,
			-1,
			333,
			-1,
			-1,
			336,
			-1,
			338,
			-1,
			-1,
			-1,
			342,
			296,
			297,
			-1,
			-1,
			-1,
			301,
			302,
			-1,
			-1,
			-1,
			-1,
			307,
			-1,
			309,
			310,
			311,
			312,
			-1,
			-1,
			362,
			-1,
			317,
			-1,
			-1,
			-1,
			321,
			-1,
			323,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			333,
			-1,
			256,
			336,
			-1,
			338,
			-1,
			-1,
			-1,
			342,
			264,
			265,
			266,
			267,
			-1,
			-1,
			270,
			271,
			-1,
			273,
			274,
			275,
			276,
			277,
			278,
			279,
			-1,
			-1,
			-1,
			362,
			-1,
			285,
			-1,
			287,
			288,
			289,
			290,
			291,
			292,
			-1,
			-1,
			295,
			-1,
			-1,
			-1,
			299,
			300,
			-1,
			302,
			303,
			304,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			314,
			-1,
			316,
			-1,
			318,
			319,
			-1,
			-1,
			322,
			-1,
			324,
			325,
			326,
			327,
			328,
			329,
			330,
			331,
			332,
			333,
			334,
			335,
			-1,
			337,
			-1,
			-1,
			340,
			341,
			-1,
			-1,
			344,
			345,
			-1,
			256,
			-1,
			-1,
			-1,
			-1,
			-1,
			262,
			-1,
			-1,
			-1,
			-1,
			-1,
			359,
			360,
			361,
			362,
			363,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			371,
			-1,
			-1,
			-1,
			375,
			-1,
			-1,
			-1,
			-1,
			380,
			381,
			382,
			383,
			384,
			-1,
			-1,
			-1,
			388,
			298,
			390,
			-1,
			-1,
			-1,
			-1,
			-1,
			396,
			397,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			256,
			-1,
			-1,
			-1,
			-1,
			-1,
			262,
			421,
			422,
			423,
			424,
			-1,
			-1,
			-1,
			-1,
			-1,
			339,
			-1,
			-1,
			433,
			-1,
			344,
			-1,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			357,
			-1,
			-1,
			-1,
			-1,
			-1,
			363,
			364,
			298,
			366,
			367,
			-1,
			-1,
			-1,
			371,
			372,
			373,
			374,
			375,
			376,
			377,
			378,
			379,
			380,
			-1,
			382,
			383,
			-1,
			385,
			386,
			387,
			388,
			389,
			390,
			391,
			392,
			393,
			394,
			-1,
			396,
			397,
			398,
			399,
			400,
			401,
			402,
			403,
			404,
			405,
			406,
			407,
			408,
			409,
			410,
			411,
			412,
			413,
			414,
			415,
			416,
			417,
			418,
			-1,
			256,
			-1,
			422,
			-1,
			424,
			425,
			262,
			427,
			-1,
			-1,
			363,
			364,
			-1,
			433,
			-1,
			-1,
			-1,
			-1,
			-1,
			372,
			373,
			374,
			375,
			376,
			377,
			378,
			-1,
			380,
			-1,
			382,
			383,
			-1,
			385,
			386,
			387,
			388,
			389,
			-1,
			391,
			392,
			393,
			394,
			298,
			396,
			397,
			398,
			399,
			400,
			401,
			402,
			403,
			404,
			405,
			406,
			407,
			408,
			409,
			410,
			411,
			412,
			413,
			414,
			415,
			416,
			417,
			-1,
			-1,
			-1,
			-1,
			422,
			-1,
			424,
			-1,
			-1,
			427,
			-1,
			-1,
			-1,
			-1,
			-1,
			433,
			-1,
			-1,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			-1,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			357,
			-1,
			-1,
			-1,
			-1,
			-1,
			363,
			364,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			371,
			372,
			373,
			374,
			375,
			376,
			377,
			378,
			379,
			380,
			256,
			382,
			383,
			-1,
			385,
			386,
			387,
			388,
			389,
			390,
			391,
			392,
			393,
			394,
			-1,
			396,
			397,
			398,
			399,
			400,
			401,
			402,
			403,
			404,
			405,
			406,
			407,
			408,
			409,
			410,
			411,
			412,
			413,
			414,
			415,
			416,
			417,
			418,
			256,
			-1,
			-1,
			422,
			-1,
			424,
			262,
			-1,
			427,
			-1,
			-1,
			-1,
			-1,
			-1,
			433,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			298,
			-1,
			-1,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			-1,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			-1,
			372,
			-1,
			374,
			-1,
			376,
			339,
			378,
			379,
			380,
			-1,
			344,
			-1,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			357,
			-1,
			-1,
			-1,
			-1,
			-1,
			363,
			364,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			371,
			372,
			373,
			374,
			375,
			376,
			377,
			378,
			379,
			380,
			256,
			382,
			383,
			-1,
			385,
			386,
			387,
			388,
			389,
			390,
			391,
			392,
			393,
			394,
			433,
			396,
			397,
			398,
			399,
			400,
			401,
			402,
			403,
			404,
			405,
			406,
			407,
			408,
			409,
			410,
			411,
			412,
			413,
			414,
			415,
			416,
			417,
			-1,
			256,
			-1,
			-1,
			422,
			-1,
			424,
			262,
			-1,
			427,
			-1,
			-1,
			-1,
			-1,
			-1,
			433,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			298,
			-1,
			-1,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			-1,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			-1,
			372,
			-1,
			374,
			-1,
			376,
			339,
			378,
			379,
			380,
			-1,
			344,
			-1,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			357,
			-1,
			-1,
			-1,
			-1,
			-1,
			363,
			364,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			-1,
			372,
			373,
			374,
			375,
			376,
			377,
			378,
			379,
			380,
			-1,
			382,
			383,
			-1,
			385,
			386,
			387,
			388,
			389,
			390,
			391,
			392,
			393,
			394,
			433,
			396,
			397,
			398,
			399,
			400,
			401,
			402,
			403,
			404,
			405,
			406,
			407,
			408,
			409,
			410,
			411,
			412,
			413,
			414,
			415,
			416,
			417,
			-1,
			256,
			256,
			-1,
			422,
			-1,
			424,
			262,
			-1,
			427,
			-1,
			265,
			-1,
			267,
			-1,
			433,
			270,
			-1,
			-1,
			-1,
			-1,
			275,
			-1,
			-1,
			-1,
			279,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			288,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			295,
			-1,
			298,
			-1,
			-1,
			300,
			-1,
			-1,
			-1,
			304,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			316,
			-1,
			318,
			-1,
			-1,
			-1,
			322,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			330,
			331,
			-1,
			-1,
			334,
			-1,
			-1,
			337,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			-1,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			-1,
			-1,
			256,
			-1,
			-1,
			-1,
			-1,
			364,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			371,
			372,
			373,
			374,
			375,
			376,
			377,
			378,
			379,
			380,
			-1,
			382,
			383,
			-1,
			385,
			386,
			387,
			388,
			389,
			390,
			391,
			392,
			393,
			394,
			-1,
			396,
			397,
			398,
			399,
			400,
			401,
			402,
			403,
			404,
			405,
			406,
			407,
			408,
			409,
			410,
			411,
			412,
			413,
			414,
			415,
			416,
			417,
			-1,
			256,
			-1,
			-1,
			-1,
			422,
			424,
			262,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			433,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			-1,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			-1,
			298,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			-1,
			372,
			-1,
			374,
			-1,
			376,
			-1,
			378,
			379,
			380,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			394,
			-1,
			-1,
			-1,
			-1,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			405,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			417,
			-1,
			256,
			-1,
			-1,
			-1,
			-1,
			364,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			371,
			372,
			433,
			374,
			375,
			376,
			377,
			378,
			379,
			380,
			-1,
			382,
			383,
			-1,
			385,
			386,
			387,
			388,
			389,
			390,
			391,
			392,
			393,
			394,
			-1,
			396,
			397,
			398,
			399,
			400,
			401,
			402,
			403,
			404,
			405,
			406,
			407,
			408,
			409,
			410,
			411,
			412,
			413,
			414,
			415,
			416,
			417,
			-1,
			256,
			-1,
			-1,
			-1,
			-1,
			424,
			262,
			-1,
			427,
			-1,
			-1,
			-1,
			-1,
			-1,
			433,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			-1,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			-1,
			298,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			-1,
			372,
			-1,
			374,
			-1,
			376,
			-1,
			378,
			379,
			380,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			394,
			-1,
			-1,
			-1,
			-1,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			405,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			417,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			364,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			-1,
			372,
			433,
			374,
			375,
			376,
			377,
			378,
			379,
			380,
			-1,
			382,
			383,
			-1,
			385,
			386,
			387,
			388,
			389,
			390,
			391,
			392,
			393,
			394,
			-1,
			396,
			397,
			398,
			399,
			400,
			401,
			402,
			403,
			404,
			405,
			406,
			407,
			408,
			409,
			410,
			411,
			412,
			413,
			414,
			415,
			416,
			417,
			-1,
			256,
			256,
			-1,
			-1,
			-1,
			424,
			262,
			-1,
			427,
			-1,
			265,
			-1,
			267,
			-1,
			433,
			270,
			-1,
			-1,
			-1,
			-1,
			275,
			-1,
			-1,
			-1,
			279,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			288,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			295,
			-1,
			298,
			-1,
			-1,
			300,
			-1,
			-1,
			-1,
			304,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			316,
			-1,
			318,
			-1,
			-1,
			-1,
			322,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			330,
			331,
			-1,
			-1,
			334,
			-1,
			-1,
			337,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			-1,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			364,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			-1,
			372,
			-1,
			374,
			375,
			376,
			377,
			378,
			379,
			380,
			-1,
			382,
			383,
			-1,
			385,
			386,
			387,
			388,
			389,
			390,
			391,
			392,
			393,
			394,
			-1,
			396,
			397,
			398,
			399,
			400,
			401,
			402,
			403,
			404,
			405,
			406,
			407,
			408,
			409,
			410,
			411,
			412,
			413,
			414,
			415,
			416,
			417,
			-1,
			256,
			256,
			-1,
			-1,
			422,
			424,
			262,
			-1,
			427,
			-1,
			265,
			-1,
			267,
			-1,
			433,
			270,
			-1,
			-1,
			-1,
			-1,
			275,
			-1,
			-1,
			-1,
			279,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			288,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			295,
			-1,
			298,
			-1,
			-1,
			300,
			-1,
			-1,
			-1,
			304,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			316,
			-1,
			318,
			-1,
			-1,
			-1,
			322,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			330,
			331,
			-1,
			-1,
			334,
			-1,
			-1,
			337,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			-1,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			364,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			-1,
			372,
			-1,
			374,
			375,
			376,
			377,
			378,
			379,
			380,
			-1,
			382,
			383,
			-1,
			385,
			386,
			387,
			388,
			389,
			390,
			391,
			392,
			393,
			394,
			-1,
			396,
			397,
			398,
			399,
			400,
			401,
			402,
			403,
			404,
			405,
			406,
			407,
			408,
			409,
			410,
			411,
			412,
			413,
			414,
			415,
			416,
			417,
			-1,
			256,
			-1,
			261,
			-1,
			422,
			424,
			262,
			-1,
			427,
			-1,
			-1,
			-1,
			-1,
			-1,
			433,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			284,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			297,
			-1,
			-1,
			-1,
			298,
			302,
			-1,
			-1,
			305,
			-1,
			307,
			-1,
			309,
			310,
			311,
			312,
			-1,
			-1,
			-1,
			-1,
			317,
			-1,
			-1,
			-1,
			321,
			256,
			-1,
			-1,
			325,
			-1,
			-1,
			262,
			-1,
			-1,
			-1,
			266,
			333,
			-1,
			-1,
			336,
			-1,
			338,
			-1,
			-1,
			-1,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			-1,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			-1,
			-1,
			362,
			-1,
			298,
			-1,
			-1,
			-1,
			-1,
			366,
			367,
			-1,
			372,
			-1,
			-1,
			372,
			-1,
			374,
			-1,
			376,
			314,
			378,
			379,
			380,
			-1,
			382,
			383,
			-1,
			385,
			386,
			387,
			388,
			389,
			390,
			391,
			392,
			393,
			394,
			-1,
			-1,
			-1,
			398,
			399,
			400,
			401,
			402,
			403,
			404,
			405,
			406,
			407,
			408,
			409,
			410,
			411,
			412,
			413,
			414,
			415,
			256,
			417,
			-1,
			422,
			357,
			-1,
			262,
			-1,
			-1,
			-1,
			363,
			364,
			-1,
			-1,
			-1,
			-1,
			-1,
			433,
			-1,
			372,
			373,
			374,
			375,
			376,
			377,
			378,
			379,
			380,
			-1,
			382,
			383,
			-1,
			385,
			386,
			387,
			388,
			389,
			390,
			391,
			392,
			393,
			394,
			298,
			396,
			397,
			398,
			399,
			400,
			401,
			402,
			403,
			404,
			405,
			406,
			407,
			408,
			409,
			410,
			411,
			412,
			413,
			414,
			415,
			416,
			417,
			-1,
			-1,
			-1,
			-1,
			422,
			-1,
			424,
			-1,
			-1,
			427,
			-1,
			-1,
			-1,
			-1,
			-1,
			433,
			-1,
			-1,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			-1,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			363,
			364,
			256,
			366,
			367,
			-1,
			-1,
			-1,
			262,
			372,
			373,
			374,
			-1,
			376,
			377,
			378,
			379,
			380,
			-1,
			382,
			383,
			-1,
			-1,
			386,
			387,
			388,
			389,
			-1,
			-1,
			-1,
			393,
			394,
			-1,
			396,
			397,
			398,
			399,
			400,
			401,
			402,
			403,
			404,
			405,
			-1,
			298,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			416,
			417,
			-1,
			-1,
			-1,
			-1,
			422,
			-1,
			424,
			-1,
			-1,
			427,
			-1,
			-1,
			-1,
			-1,
			-1,
			433,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			-1,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			364,
			256,
			366,
			367,
			-1,
			-1,
			-1,
			262,
			372,
			-1,
			374,
			375,
			376,
			377,
			378,
			379,
			380,
			-1,
			382,
			383,
			-1,
			-1,
			386,
			387,
			388,
			389,
			-1,
			-1,
			-1,
			393,
			394,
			-1,
			396,
			397,
			398,
			399,
			400,
			401,
			402,
			403,
			404,
			405,
			-1,
			298,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			416,
			417,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			424,
			-1,
			-1,
			427,
			-1,
			-1,
			-1,
			-1,
			-1,
			433,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			-1,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			364,
			256,
			366,
			367,
			-1,
			-1,
			-1,
			262,
			372,
			-1,
			374,
			375,
			376,
			377,
			378,
			379,
			380,
			-1,
			382,
			383,
			-1,
			-1,
			386,
			387,
			388,
			389,
			-1,
			-1,
			-1,
			393,
			394,
			-1,
			396,
			397,
			398,
			399,
			400,
			401,
			402,
			403,
			404,
			405,
			-1,
			298,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			416,
			417,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			424,
			-1,
			-1,
			427,
			-1,
			-1,
			-1,
			-1,
			-1,
			433,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			-1,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			364,
			256,
			366,
			367,
			-1,
			-1,
			-1,
			262,
			372,
			-1,
			374,
			375,
			376,
			377,
			378,
			379,
			380,
			-1,
			382,
			383,
			-1,
			-1,
			386,
			387,
			388,
			389,
			-1,
			-1,
			-1,
			393,
			394,
			-1,
			396,
			397,
			398,
			399,
			400,
			401,
			402,
			403,
			404,
			405,
			-1,
			298,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			416,
			417,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			424,
			-1,
			-1,
			427,
			-1,
			-1,
			-1,
			-1,
			-1,
			433,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			-1,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			363,
			256,
			-1,
			366,
			367,
			-1,
			-1,
			262,
			-1,
			372,
			373,
			374,
			-1,
			376,
			-1,
			378,
			379,
			380,
			-1,
			382,
			383,
			-1,
			-1,
			386,
			387,
			388,
			389,
			-1,
			-1,
			-1,
			393,
			394,
			-1,
			-1,
			-1,
			398,
			399,
			400,
			401,
			402,
			403,
			404,
			405,
			298,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			417,
			-1,
			-1,
			-1,
			-1,
			422,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			433,
			-1,
			-1,
			256,
			-1,
			-1,
			-1,
			-1,
			-1,
			262,
			-1,
			-1,
			-1,
			-1,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			-1,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			298,
			-1,
			372,
			-1,
			374,
			-1,
			376,
			-1,
			378,
			379,
			380,
			-1,
			382,
			383,
			-1,
			-1,
			386,
			387,
			388,
			389,
			390,
			391,
			392,
			393,
			394,
			-1,
			-1,
			-1,
			398,
			399,
			400,
			401,
			402,
			403,
			404,
			405,
			-1,
			-1,
			-1,
			-1,
			-1,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			417,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			-1,
			-1,
			-1,
			-1,
			433,
			-1,
			363,
			256,
			-1,
			366,
			367,
			-1,
			-1,
			262,
			-1,
			372,
			-1,
			374,
			-1,
			376,
			-1,
			378,
			379,
			380,
			-1,
			382,
			383,
			-1,
			-1,
			386,
			387,
			388,
			389,
			-1,
			-1,
			-1,
			393,
			394,
			-1,
			-1,
			-1,
			398,
			399,
			400,
			401,
			402,
			403,
			404,
			405,
			298,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			417,
			-1,
			-1,
			-1,
			-1,
			422,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			433,
			-1,
			-1,
			256,
			-1,
			-1,
			-1,
			-1,
			-1,
			262,
			-1,
			-1,
			-1,
			-1,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			-1,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			298,
			-1,
			372,
			-1,
			374,
			-1,
			376,
			-1,
			378,
			379,
			380,
			-1,
			382,
			383,
			-1,
			-1,
			386,
			387,
			388,
			389,
			-1,
			-1,
			-1,
			393,
			394,
			-1,
			-1,
			-1,
			398,
			399,
			400,
			401,
			402,
			403,
			404,
			405,
			-1,
			-1,
			-1,
			-1,
			-1,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			417,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			-1,
			-1,
			-1,
			-1,
			433,
			-1,
			-1,
			256,
			-1,
			366,
			367,
			-1,
			-1,
			262,
			-1,
			372,
			-1,
			374,
			-1,
			376,
			-1,
			378,
			379,
			380,
			-1,
			382,
			383,
			-1,
			-1,
			386,
			387,
			388,
			389,
			-1,
			-1,
			-1,
			393,
			394,
			-1,
			-1,
			-1,
			398,
			399,
			400,
			401,
			402,
			403,
			404,
			405,
			298,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			417,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			433,
			-1,
			-1,
			256,
			-1,
			-1,
			-1,
			-1,
			-1,
			262,
			-1,
			-1,
			-1,
			-1,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			-1,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			298,
			-1,
			372,
			-1,
			374,
			-1,
			376,
			-1,
			378,
			379,
			380,
			-1,
			382,
			383,
			-1,
			-1,
			386,
			387,
			388,
			389,
			-1,
			-1,
			-1,
			393,
			394,
			-1,
			-1,
			-1,
			398,
			399,
			400,
			401,
			402,
			403,
			404,
			405,
			-1,
			-1,
			-1,
			-1,
			-1,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			417,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			-1,
			-1,
			-1,
			-1,
			433,
			-1,
			-1,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			-1,
			372,
			-1,
			374,
			-1,
			376,
			-1,
			378,
			379,
			380,
			-1,
			382,
			383,
			-1,
			-1,
			386,
			387,
			388,
			389,
			-1,
			-1,
			-1,
			393,
			394,
			-1,
			-1,
			-1,
			398,
			399,
			400,
			401,
			402,
			403,
			404,
			405,
			-1,
			256,
			-1,
			256,
			-1,
			-1,
			-1,
			-1,
			-1,
			264,
			265,
			417,
			267,
			-1,
			-1,
			270,
			271,
			-1,
			-1,
			-1,
			275,
			276,
			277,
			-1,
			279,
			-1,
			-1,
			433,
			-1,
			-1,
			285,
			-1,
			-1,
			288,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			295,
			-1,
			-1,
			-1,
			-1,
			300,
			-1,
			302,
			303,
			304,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			316,
			-1,
			318,
			319,
			-1,
			-1,
			322,
			-1,
			-1,
			325,
			-1,
			327,
			-1,
			329,
			330,
			331,
			332,
			-1,
			334,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			-1,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			359,
			360,
			361,
			362,
			363,
			256,
			-1,
			366,
			367,
			366,
			367,
			-1,
			-1,
			-1,
			-1,
			372,
			375,
			374,
			-1,
			376,
			-1,
			378,
			379,
			380,
			-1,
			-1,
			-1,
			-1,
			-1,
			386,
			387,
			388,
			389,
			-1,
			-1,
			-1,
			393,
			394,
			-1,
			-1,
			-1,
			398,
			399,
			400,
			401,
			402,
			403,
			404,
			405,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			417,
			-1,
			421,
			422,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			432,
			433,
			-1,
			433,
			256,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			-1,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			-1,
			372,
			-1,
			374,
			-1,
			376,
			-1,
			378,
			379,
			380,
			-1,
			-1,
			-1,
			-1,
			-1,
			386,
			387,
			388,
			389,
			-1,
			-1,
			-1,
			393,
			394,
			-1,
			-1,
			-1,
			398,
			399,
			400,
			401,
			402,
			403,
			404,
			405,
			-1,
			-1,
			-1,
			-1,
			-1,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			417,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			256,
			-1,
			-1,
			-1,
			433,
			-1,
			-1,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			-1,
			372,
			-1,
			374,
			-1,
			376,
			-1,
			378,
			379,
			380,
			-1,
			-1,
			-1,
			-1,
			-1,
			386,
			387,
			388,
			389,
			-1,
			-1,
			-1,
			393,
			394,
			-1,
			-1,
			-1,
			398,
			399,
			400,
			401,
			402,
			403,
			404,
			405,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			417,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			256,
			-1,
			-1,
			-1,
			433,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			-1,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			-1,
			372,
			-1,
			374,
			-1,
			376,
			-1,
			378,
			379,
			380,
			-1,
			-1,
			-1,
			-1,
			-1,
			386,
			387,
			388,
			389,
			-1,
			-1,
			-1,
			393,
			394,
			-1,
			-1,
			-1,
			-1,
			-1,
			400,
			401,
			402,
			403,
			404,
			405,
			-1,
			-1,
			-1,
			-1,
			-1,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			417,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			256,
			-1,
			-1,
			-1,
			433,
			-1,
			-1,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			-1,
			372,
			-1,
			374,
			-1,
			376,
			-1,
			378,
			379,
			380,
			-1,
			-1,
			-1,
			-1,
			-1,
			386,
			387,
			388,
			389,
			-1,
			-1,
			-1,
			393,
			394,
			-1,
			-1,
			-1,
			-1,
			-1,
			400,
			401,
			402,
			403,
			404,
			405,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			417,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			256,
			-1,
			-1,
			-1,
			433,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			-1,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			-1,
			372,
			-1,
			374,
			-1,
			376,
			-1,
			378,
			379,
			380,
			-1,
			-1,
			-1,
			-1,
			-1,
			386,
			387,
			388,
			389,
			-1,
			-1,
			-1,
			393,
			394,
			-1,
			-1,
			-1,
			-1,
			-1,
			400,
			401,
			402,
			403,
			404,
			405,
			-1,
			-1,
			-1,
			-1,
			-1,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			417,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			256,
			-1,
			-1,
			-1,
			433,
			-1,
			-1,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			-1,
			372,
			-1,
			374,
			-1,
			376,
			-1,
			378,
			379,
			380,
			-1,
			-1,
			-1,
			-1,
			-1,
			386,
			387,
			388,
			389,
			-1,
			-1,
			-1,
			393,
			394,
			-1,
			-1,
			-1,
			-1,
			-1,
			400,
			401,
			402,
			403,
			404,
			405,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			256,
			417,
			-1,
			-1,
			-1,
			-1,
			262,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			433,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			-1,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			298,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			-1,
			372,
			-1,
			374,
			-1,
			376,
			-1,
			378,
			379,
			380,
			-1,
			-1,
			-1,
			-1,
			-1,
			386,
			387,
			388,
			389,
			-1,
			-1,
			-1,
			393,
			394,
			-1,
			-1,
			-1,
			-1,
			-1,
			400,
			401,
			402,
			403,
			404,
			405,
			256,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			417,
			-1,
			-1,
			-1,
			-1,
			-1,
			364,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			372,
			-1,
			433,
			375,
			-1,
			377,
			378,
			-1,
			-1,
			-1,
			382,
			383,
			-1,
			-1,
			386,
			387,
			388,
			389,
			390,
			391,
			392,
			393,
			394,
			-1,
			396,
			397,
			398,
			399,
			400,
			401,
			402,
			403,
			404,
			405,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			416,
			417,
			-1,
			256,
			-1,
			-1,
			-1,
			-1,
			424,
			-1,
			-1,
			427,
			-1,
			-1,
			339,
			-1,
			-1,
			433,
			-1,
			344,
			-1,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			-1,
			372,
			-1,
			374,
			-1,
			376,
			-1,
			378,
			379,
			380,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			388,
			389,
			-1,
			-1,
			-1,
			393,
			394,
			-1,
			-1,
			-1,
			-1,
			-1,
			256,
			-1,
			402,
			403,
			404,
			405,
			-1,
			-1,
			-1,
			-1,
			-1,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			417,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			-1,
			-1,
			-1,
			-1,
			433,
			-1,
			-1,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			-1,
			372,
			-1,
			374,
			-1,
			376,
			-1,
			378,
			379,
			380,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			388,
			389,
			-1,
			-1,
			-1,
			393,
			394,
			-1,
			-1,
			-1,
			-1,
			-1,
			256,
			-1,
			402,
			403,
			404,
			405,
			-1,
			-1,
			-1,
			-1,
			-1,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			417,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			-1,
			-1,
			-1,
			-1,
			433,
			-1,
			-1,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			-1,
			372,
			-1,
			374,
			-1,
			376,
			-1,
			378,
			379,
			380,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			388,
			389,
			-1,
			-1,
			-1,
			393,
			394,
			-1,
			-1,
			-1,
			-1,
			-1,
			256,
			-1,
			-1,
			-1,
			404,
			405,
			-1,
			-1,
			-1,
			-1,
			-1,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			417,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			-1,
			-1,
			-1,
			-1,
			433,
			-1,
			-1,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			-1,
			372,
			-1,
			374,
			-1,
			376,
			-1,
			378,
			379,
			380,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			388,
			389,
			-1,
			-1,
			-1,
			393,
			394,
			-1,
			-1,
			-1,
			-1,
			-1,
			256,
			-1,
			-1,
			-1,
			404,
			405,
			-1,
			-1,
			-1,
			-1,
			-1,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			417,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			-1,
			-1,
			-1,
			-1,
			433,
			-1,
			-1,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			-1,
			372,
			-1,
			374,
			-1,
			376,
			-1,
			378,
			379,
			380,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			389,
			-1,
			-1,
			-1,
			393,
			394,
			-1,
			-1,
			-1,
			-1,
			-1,
			256,
			-1,
			-1,
			-1,
			404,
			405,
			-1,
			-1,
			-1,
			-1,
			-1,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			417,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			-1,
			-1,
			-1,
			-1,
			433,
			-1,
			-1,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			-1,
			372,
			-1,
			374,
			-1,
			376,
			-1,
			378,
			379,
			380,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			389,
			-1,
			-1,
			-1,
			393,
			394,
			-1,
			-1,
			-1,
			-1,
			-1,
			256,
			-1,
			-1,
			-1,
			404,
			405,
			-1,
			-1,
			-1,
			-1,
			-1,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			417,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			-1,
			-1,
			-1,
			-1,
			433,
			-1,
			-1,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			-1,
			372,
			-1,
			374,
			-1,
			376,
			-1,
			378,
			379,
			380,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			389,
			-1,
			-1,
			-1,
			-1,
			394,
			-1,
			-1,
			-1,
			-1,
			-1,
			256,
			-1,
			-1,
			-1,
			404,
			405,
			-1,
			-1,
			-1,
			-1,
			-1,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			417,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			-1,
			-1,
			-1,
			-1,
			433,
			-1,
			-1,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			-1,
			372,
			-1,
			374,
			-1,
			376,
			-1,
			378,
			379,
			380,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			389,
			-1,
			-1,
			-1,
			-1,
			394,
			-1,
			-1,
			-1,
			-1,
			-1,
			256,
			-1,
			-1,
			-1,
			404,
			405,
			-1,
			-1,
			-1,
			-1,
			-1,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			417,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			-1,
			-1,
			-1,
			-1,
			433,
			-1,
			-1,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			-1,
			372,
			-1,
			374,
			-1,
			376,
			-1,
			378,
			379,
			380,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			394,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			404,
			405,
			-1,
			-1,
			-1,
			-1,
			-1,
			339,
			-1,
			-1,
			-1,
			-1,
			344,
			417,
			346,
			347,
			348,
			349,
			350,
			351,
			352,
			353,
			354,
			355,
			356,
			-1,
			-1,
			-1,
			-1,
			433,
			-1,
			-1,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			-1,
			372,
			-1,
			374,
			-1,
			376,
			-1,
			378,
			379,
			380,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			394,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			404,
			405,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			417,
			-1,
			-1,
			256,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			264,
			265,
			266,
			267,
			268,
			433,
			270,
			271,
			-1,
			273,
			274,
			275,
			276,
			277,
			278,
			279,
			-1,
			-1,
			-1,
			-1,
			-1,
			285,
			-1,
			287,
			288,
			289,
			290,
			291,
			292,
			-1,
			-1,
			295,
			-1,
			-1,
			-1,
			299,
			300,
			-1,
			302,
			303,
			304,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			314,
			-1,
			316,
			-1,
			318,
			319,
			-1,
			-1,
			322,
			-1,
			324,
			325,
			326,
			327,
			328,
			329,
			330,
			331,
			332,
			333,
			334,
			335,
			-1,
			337,
			-1,
			-1,
			340,
			341,
			-1,
			-1,
			344,
			345,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			359,
			360,
			361,
			362,
			363,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			371,
			-1,
			-1,
			-1,
			375,
			-1,
			-1,
			-1,
			-1,
			380,
			381,
			382,
			383,
			384,
			-1,
			-1,
			-1,
			388,
			-1,
			390,
			-1,
			-1,
			-1,
			-1,
			-1,
			396,
			397,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			256,
			-1,
			421,
			422,
			423,
			424,
			-1,
			426,
			264,
			265,
			266,
			267,
			-1,
			-1,
			270,
			271,
			-1,
			273,
			274,
			275,
			276,
			277,
			278,
			279,
			-1,
			-1,
			-1,
			-1,
			-1,
			285,
			-1,
			287,
			288,
			289,
			290,
			291,
			292,
			-1,
			-1,
			295,
			-1,
			-1,
			-1,
			299,
			300,
			-1,
			302,
			303,
			304,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			314,
			-1,
			316,
			-1,
			318,
			319,
			-1,
			-1,
			322,
			-1,
			324,
			325,
			326,
			327,
			328,
			329,
			330,
			331,
			332,
			333,
			334,
			335,
			-1,
			337,
			-1,
			-1,
			340,
			341,
			-1,
			-1,
			344,
			345,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			359,
			360,
			361,
			362,
			363,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			371,
			-1,
			-1,
			-1,
			375,
			-1,
			-1,
			-1,
			-1,
			380,
			381,
			382,
			383,
			384,
			-1,
			-1,
			-1,
			388,
			-1,
			390,
			-1,
			-1,
			-1,
			-1,
			-1,
			396,
			397,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			256,
			-1,
			-1,
			-1,
			421,
			422,
			423,
			424,
			264,
			265,
			266,
			267,
			-1,
			-1,
			270,
			271,
			-1,
			273,
			274,
			275,
			276,
			277,
			278,
			279,
			-1,
			-1,
			-1,
			-1,
			-1,
			285,
			-1,
			287,
			288,
			289,
			290,
			291,
			292,
			-1,
			-1,
			295,
			-1,
			-1,
			-1,
			299,
			300,
			-1,
			302,
			303,
			304,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			314,
			-1,
			316,
			-1,
			318,
			319,
			-1,
			-1,
			322,
			-1,
			324,
			325,
			326,
			327,
			328,
			329,
			330,
			331,
			332,
			333,
			334,
			335,
			-1,
			337,
			-1,
			-1,
			340,
			341,
			-1,
			-1,
			344,
			345,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			359,
			360,
			361,
			362,
			363,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			371,
			-1,
			-1,
			-1,
			375,
			-1,
			-1,
			-1,
			-1,
			380,
			381,
			382,
			383,
			384,
			-1,
			-1,
			-1,
			388,
			-1,
			390,
			-1,
			-1,
			-1,
			-1,
			-1,
			396,
			397,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			256,
			-1,
			-1,
			-1,
			421,
			422,
			423,
			424,
			264,
			265,
			266,
			267,
			-1,
			-1,
			270,
			271,
			-1,
			273,
			274,
			275,
			276,
			277,
			278,
			279,
			-1,
			-1,
			-1,
			-1,
			-1,
			285,
			-1,
			287,
			288,
			289,
			290,
			291,
			292,
			-1,
			-1,
			295,
			-1,
			-1,
			-1,
			299,
			300,
			-1,
			302,
			303,
			304,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			314,
			-1,
			316,
			-1,
			318,
			319,
			-1,
			-1,
			322,
			-1,
			324,
			325,
			326,
			327,
			328,
			329,
			330,
			331,
			332,
			333,
			334,
			335,
			-1,
			337,
			-1,
			-1,
			340,
			341,
			-1,
			-1,
			344,
			345,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			359,
			360,
			361,
			362,
			363,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			371,
			-1,
			-1,
			-1,
			375,
			-1,
			-1,
			-1,
			-1,
			380,
			381,
			382,
			383,
			384,
			-1,
			-1,
			-1,
			388,
			-1,
			390,
			-1,
			-1,
			-1,
			-1,
			-1,
			396,
			397,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			256,
			-1,
			-1,
			-1,
			421,
			422,
			423,
			424,
			264,
			265,
			266,
			267,
			-1,
			-1,
			270,
			271,
			-1,
			273,
			274,
			275,
			276,
			277,
			278,
			279,
			-1,
			-1,
			-1,
			-1,
			-1,
			285,
			-1,
			287,
			288,
			289,
			290,
			291,
			292,
			-1,
			-1,
			295,
			-1,
			-1,
			-1,
			299,
			300,
			-1,
			302,
			303,
			304,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			314,
			-1,
			316,
			-1,
			318,
			319,
			-1,
			-1,
			322,
			-1,
			324,
			325,
			326,
			327,
			328,
			329,
			330,
			331,
			332,
			333,
			334,
			335,
			-1,
			337,
			-1,
			-1,
			340,
			341,
			-1,
			-1,
			344,
			345,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			359,
			360,
			361,
			362,
			363,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			371,
			-1,
			-1,
			-1,
			375,
			-1,
			-1,
			-1,
			-1,
			380,
			381,
			382,
			383,
			384,
			-1,
			-1,
			-1,
			388,
			-1,
			390,
			-1,
			-1,
			-1,
			-1,
			-1,
			396,
			397,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			256,
			-1,
			-1,
			-1,
			421,
			422,
			423,
			424,
			264,
			265,
			-1,
			267,
			-1,
			-1,
			270,
			271,
			-1,
			-1,
			-1,
			275,
			276,
			277,
			-1,
			279,
			-1,
			-1,
			265,
			-1,
			267,
			285,
			-1,
			270,
			288,
			-1,
			-1,
			-1,
			275,
			-1,
			-1,
			295,
			279,
			-1,
			-1,
			-1,
			300,
			-1,
			302,
			303,
			304,
			288,
			306,
			-1,
			-1,
			-1,
			-1,
			-1,
			295,
			313,
			-1,
			-1,
			316,
			300,
			318,
			319,
			-1,
			304,
			322,
			-1,
			-1,
			325,
			-1,
			327,
			-1,
			329,
			330,
			331,
			332,
			316,
			334,
			318,
			-1,
			-1,
			-1,
			322,
			-1,
			341,
			-1,
			-1,
			344,
			345,
			-1,
			330,
			331,
			-1,
			-1,
			334,
			-1,
			-1,
			337,
			-1,
			-1,
			-1,
			-1,
			359,
			360,
			361,
			362,
			363,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			375,
			-1,
			-1,
			378,
			-1,
			363,
			381,
			382,
			383,
			384,
			-1,
			-1,
			-1,
			388,
			-1,
			390,
			-1,
			-1,
			-1,
			-1,
			-1,
			396,
			397,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			264,
			265,
			-1,
			267,
			-1,
			-1,
			270,
			271,
			-1,
			-1,
			-1,
			275,
			276,
			277,
			-1,
			279,
			-1,
			421,
			422,
			423,
			424,
			285,
			-1,
			-1,
			288,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			295,
			-1,
			-1,
			-1,
			422,
			300,
			-1,
			302,
			303,
			304,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			316,
			-1,
			318,
			319,
			-1,
			-1,
			322,
			-1,
			-1,
			325,
			-1,
			327,
			-1,
			329,
			330,
			331,
			332,
			-1,
			334,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			341,
			-1,
			-1,
			344,
			345,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			359,
			360,
			361,
			362,
			363,
			-1,
			-1,
			366,
			367,
			-1,
			-1,
			-1,
			371,
			372,
			-1,
			-1,
			375,
			-1,
			-1,
			-1,
			-1,
			-1,
			381,
			382,
			383,
			384,
			-1,
			-1,
			-1,
			388,
			-1,
			390,
			-1,
			-1,
			-1,
			-1,
			-1,
			396,
			397,
			-1,
			"Not showing all elements because this array is too big (20417 elements)"
		};

		// Token: 0x040007FC RID: 2044
		private Tokenizer lexer;

		// Token: 0x02000399 RID: 921
		[Flags]
		private enum ParameterModifierType
		{
			// Token: 0x04000FA7 RID: 4007
			Ref = 2,
			// Token: 0x04000FA8 RID: 4008
			Out = 4,
			// Token: 0x04000FA9 RID: 4009
			This = 8,
			// Token: 0x04000FAA RID: 4010
			Params = 16,
			// Token: 0x04000FAB RID: 4011
			Arglist = 32,
			// Token: 0x04000FAC RID: 4012
			DefaultValue = 64,
			// Token: 0x04000FAD RID: 4013
			All = 126,
			// Token: 0x04000FAE RID: 4014
			PrimaryConstructor = 86
		}

		// Token: 0x0200039A RID: 922
		private class YYRules : MarshalByRefObject
		{
			// Token: 0x060026D9 RID: 9945 RVA: 0x000B7720 File Offset: 0x000B5920
			public static string getRule(int index)
			{
				return CSharpParser.YYRules.yyRule[index];
			}

			// Token: 0x060026DA RID: 9946 RVA: 0x0006AD0C File Offset: 0x00068F0C
			public YYRules()
			{
			}

			// Token: 0x060026DB RID: 9947 RVA: 0x000B772C File Offset: 0x000B592C
			// Note: this type is marked as 'beforefieldinit'.
			static YYRules()
			{
			}

			// Token: 0x04000FAF RID: 4015
			public static readonly string[] yyRule = new string[]
			{
				"$accept : compilation_unit",
				"compilation_unit : outer_declaration opt_EOF",
				"$$1 :",
				"compilation_unit : interactive_parsing $$1 opt_EOF",
				"compilation_unit : documentation_parsing",
				"outer_declaration : opt_extern_alias_directives opt_using_directives",
				"outer_declaration : opt_extern_alias_directives opt_using_directives namespace_or_type_declarations opt_attributes",
				"outer_declaration : opt_extern_alias_directives opt_using_directives attribute_sections",
				"outer_declaration : error",
				"opt_EOF :",
				"opt_EOF : EOF",
				"extern_alias_directives : extern_alias_directive",
				"extern_alias_directives : extern_alias_directives extern_alias_directive",
				"extern_alias_directive : EXTERN_ALIAS IDENTIFIER IDENTIFIER SEMICOLON",
				"extern_alias_directive : EXTERN_ALIAS error",
				"using_directives : using_directive",
				"using_directives : using_directives using_directive",
				"using_directive : using_namespace",
				"using_namespace : USING opt_static namespace_or_type_expr SEMICOLON",
				"using_namespace : USING opt_static IDENTIFIER ASSIGN namespace_or_type_expr SEMICOLON",
				"using_namespace : USING error",
				"opt_static :",
				"opt_static : STATIC",
				"$$2 :",
				"$$3 :",
				"namespace_declaration : opt_attributes NAMESPACE namespace_name $$2 OPEN_BRACE $$3 opt_extern_alias_directives opt_using_directives opt_namespace_or_type_declarations CLOSE_BRACE opt_semicolon_error",
				"namespace_declaration : opt_attributes NAMESPACE namespace_name",
				"opt_semicolon_error :",
				"opt_semicolon_error : SEMICOLON",
				"opt_semicolon_error : error",
				"namespace_name : IDENTIFIER",
				"namespace_name : namespace_name DOT IDENTIFIER",
				"namespace_name : error",
				"opt_semicolon :",
				"opt_semicolon : SEMICOLON",
				"opt_comma :",
				"opt_comma : COMMA",
				"opt_using_directives :",
				"opt_using_directives : using_directives",
				"opt_extern_alias_directives :",
				"opt_extern_alias_directives : extern_alias_directives",
				"opt_namespace_or_type_declarations :",
				"opt_namespace_or_type_declarations : namespace_or_type_declarations",
				"namespace_or_type_declarations : namespace_or_type_declaration",
				"namespace_or_type_declarations : namespace_or_type_declarations namespace_or_type_declaration",
				"namespace_or_type_declaration : type_declaration",
				"namespace_or_type_declaration : namespace_declaration",
				"namespace_or_type_declaration : attribute_sections CLOSE_BRACE",
				"type_declaration : class_declaration",
				"type_declaration : struct_declaration",
				"type_declaration : interface_declaration",
				"type_declaration : enum_declaration",
				"type_declaration : delegate_declaration",
				"opt_attributes :",
				"opt_attributes : attribute_sections",
				"attribute_sections : attribute_section",
				"attribute_sections : attribute_sections attribute_section",
				"$$4 :",
				"attribute_section : OPEN_BRACKET $$4 attribute_section_cont",
				"$$5 :",
				"attribute_section_cont : attribute_target COLON $$5 attribute_list opt_comma CLOSE_BRACKET",
				"attribute_section_cont : attribute_list opt_comma CLOSE_BRACKET",
				"attribute_section_cont : IDENTIFIER error",
				"attribute_section_cont : error",
				"attribute_target : IDENTIFIER",
				"attribute_target : EVENT",
				"attribute_target : RETURN",
				"attribute_list : attribute",
				"attribute_list : attribute_list COMMA attribute",
				"$$6 :",
				"attribute : attribute_name $$6 opt_attribute_arguments",
				"attribute_name : namespace_or_type_expr",
				"opt_attribute_arguments :",
				"opt_attribute_arguments : OPEN_PARENS attribute_arguments CLOSE_PARENS",
				"attribute_arguments :",
				"attribute_arguments : positional_or_named_argument",
				"attribute_arguments : named_attribute_argument",
				"attribute_arguments : attribute_arguments COMMA positional_or_named_argument",
				"attribute_arguments : attribute_arguments COMMA named_attribute_argument",
				"positional_or_named_argument : expression",
				"positional_or_named_argument : named_argument",
				"positional_or_named_argument : error",
				"$$7 :",
				"named_attribute_argument : IDENTIFIER ASSIGN $$7 expression",
				"named_argument : identifier_inside_body COLON opt_named_modifier named_argument_expr",
				"named_argument_expr : expression_or_error",
				"opt_named_modifier :",
				"opt_named_modifier : REF",
				"opt_named_modifier : OUT",
				"opt_class_member_declarations :",
				"opt_class_member_declarations : class_member_declarations",
				"class_member_declarations : class_member_declaration",
				"class_member_declarations : class_member_declarations class_member_declaration",
				"class_member_declaration : constant_declaration",
				"class_member_declaration : field_declaration",
				"class_member_declaration : method_declaration",
				"class_member_declaration : property_declaration",
				"class_member_declaration : event_declaration",
				"class_member_declaration : indexer_declaration",
				"class_member_declaration : operator_declaration",
				"class_member_declaration : constructor_declaration",
				"class_member_declaration : primary_constructor_body",
				"class_member_declaration : destructor_declaration",
				"class_member_declaration : type_declaration",
				"class_member_declaration : attributes_without_members",
				"class_member_declaration : incomplete_member",
				"class_member_declaration : error",
				"$$8 :",
				"primary_constructor_body : OPEN_BRACE $$8 opt_statement_list block_end",
				"$$9 :",
				"$$10 :",
				"$$11 :",
				"$$12 :",
				"$$13 :",
				"struct_declaration : opt_attributes opt_modifiers opt_partial STRUCT $$9 type_declaration_name $$10 opt_primary_parameters opt_class_base opt_type_parameter_constraints_clauses $$11 OPEN_BRACE $$12 opt_class_member_declarations CLOSE_BRACE $$13 opt_semicolon",
				"struct_declaration : opt_attributes opt_modifiers opt_partial STRUCT error",
				"$$14 :",
				"constant_declaration : opt_attributes opt_modifiers CONST type IDENTIFIER $$14 constant_initializer opt_constant_declarators SEMICOLON",
				"constant_declaration : opt_attributes opt_modifiers CONST type error",
				"opt_constant_declarators :",
				"opt_constant_declarators : constant_declarators",
				"constant_declarators : constant_declarator",
				"constant_declarators : constant_declarators constant_declarator",
				"constant_declarator : COMMA IDENTIFIER constant_initializer",
				"$$15 :",
				"constant_initializer : ASSIGN $$15 constant_initializer_expr",
				"constant_initializer : error",
				"constant_initializer_expr : constant_expression",
				"constant_initializer_expr : array_initializer",
				"$$16 :",
				"field_declaration : opt_attributes opt_modifiers member_type IDENTIFIER $$16 opt_field_initializer opt_field_declarators SEMICOLON",
				"$$17 :",
				"field_declaration : opt_attributes opt_modifiers FIXED simple_type IDENTIFIER $$17 fixed_field_size opt_fixed_field_declarators SEMICOLON",
				"field_declaration : opt_attributes opt_modifiers FIXED simple_type error SEMICOLON",
				"opt_field_initializer :",
				"$$18 :",
				"opt_field_initializer : ASSIGN $$18 variable_initializer",
				"opt_field_declarators :",
				"opt_field_declarators : field_declarators",
				"field_declarators : field_declarator",
				"field_declarators : field_declarators field_declarator",
				"field_declarator : COMMA IDENTIFIER",
				"$$19 :",
				"field_declarator : COMMA IDENTIFIER ASSIGN $$19 variable_initializer",
				"opt_fixed_field_declarators :",
				"opt_fixed_field_declarators : fixed_field_declarators",
				"fixed_field_declarators : fixed_field_declarator",
				"fixed_field_declarators : fixed_field_declarators fixed_field_declarator",
				"fixed_field_declarator : COMMA IDENTIFIER fixed_field_size",
				"$$20 :",
				"fixed_field_size : OPEN_BRACKET $$20 expression CLOSE_BRACKET",
				"fixed_field_size : OPEN_BRACKET error",
				"variable_initializer : expression",
				"variable_initializer : array_initializer",
				"variable_initializer : error",
				"$$21 :",
				"method_declaration : method_header $$21 method_body_expression_block",
				"$$22 :",
				"$$23 :",
				"method_header : opt_attributes opt_modifiers member_type method_declaration_name OPEN_PARENS $$22 opt_formal_parameter_list CLOSE_PARENS $$23 opt_type_parameter_constraints_clauses",
				"$$24 :",
				"$$25 :",
				"$$26 :",
				"method_header : opt_attributes opt_modifiers PARTIAL VOID $$24 method_declaration_name OPEN_PARENS $$25 opt_formal_parameter_list CLOSE_PARENS $$26 opt_type_parameter_constraints_clauses",
				"method_header : opt_attributes opt_modifiers member_type modifiers method_declaration_name OPEN_PARENS opt_formal_parameter_list CLOSE_PARENS",
				"method_header : opt_attributes opt_modifiers member_type method_declaration_name error",
				"method_body_expression_block : method_body",
				"method_body_expression_block : expression_block",
				"method_body : block",
				"method_body : SEMICOLON",
				"$$27 :",
				"expression_block : ARROW $$27 expression SEMICOLON",
				"opt_formal_parameter_list :",
				"opt_formal_parameter_list : formal_parameter_list",
				"formal_parameter_list : fixed_parameters",
				"formal_parameter_list : fixed_parameters COMMA parameter_array",
				"formal_parameter_list : fixed_parameters COMMA arglist_modifier",
				"formal_parameter_list : parameter_array COMMA error",
				"formal_parameter_list : fixed_parameters COMMA parameter_array COMMA error",
				"formal_parameter_list : arglist_modifier COMMA error",
				"formal_parameter_list : fixed_parameters COMMA ARGLIST COMMA error",
				"formal_parameter_list : parameter_array",
				"formal_parameter_list : arglist_modifier",
				"formal_parameter_list : error",
				"fixed_parameters : fixed_parameter",
				"fixed_parameters : fixed_parameters COMMA fixed_parameter",
				"fixed_parameter : opt_attributes opt_parameter_modifier parameter_type identifier_inside_body",
				"fixed_parameter : opt_attributes opt_parameter_modifier parameter_type identifier_inside_body OPEN_BRACKET CLOSE_BRACKET",
				"fixed_parameter : attribute_sections error",
				"fixed_parameter : opt_attributes opt_parameter_modifier parameter_type error",
				"$$28 :",
				"fixed_parameter : opt_attributes opt_parameter_modifier parameter_type identifier_inside_body ASSIGN $$28 constant_expression",
				"opt_parameter_modifier :",
				"opt_parameter_modifier : parameter_modifiers",
				"parameter_modifiers : parameter_modifier",
				"parameter_modifiers : parameter_modifiers parameter_modifier",
				"parameter_modifier : REF",
				"parameter_modifier : OUT",
				"parameter_modifier : THIS",
				"parameter_array : opt_attributes params_modifier type IDENTIFIER",
				"parameter_array : opt_attributes params_modifier type IDENTIFIER ASSIGN constant_expression",
				"parameter_array : opt_attributes params_modifier type error",
				"params_modifier : PARAMS",
				"params_modifier : PARAMS parameter_modifier",
				"params_modifier : PARAMS params_modifier",
				"arglist_modifier : ARGLIST",
				"$$29 :",
				"$$30 :",
				"$$31 :",
				"$$32 :",
				"property_declaration : opt_attributes opt_modifiers member_type member_declaration_name $$29 OPEN_BRACE $$30 accessor_declarations $$31 CLOSE_BRACE $$32 opt_property_initializer",
				"$$33 :",
				"property_declaration : opt_attributes opt_modifiers member_type member_declaration_name $$33 expression_block",
				"opt_property_initializer :",
				"$$34 :",
				"opt_property_initializer : ASSIGN $$34 property_initializer SEMICOLON",
				"property_initializer : expression",
				"property_initializer : array_initializer",
				"$$35 :",
				"$$36 :",
				"indexer_declaration : opt_attributes opt_modifiers member_type indexer_declaration_name OPEN_BRACKET $$35 opt_formal_parameter_list CLOSE_BRACKET $$36 indexer_body",
				"indexer_body : OPEN_BRACE accessor_declarations CLOSE_BRACE",
				"indexer_body : expression_block",
				"accessor_declarations : get_accessor_declaration",
				"accessor_declarations : get_accessor_declaration accessor_declarations",
				"accessor_declarations : set_accessor_declaration",
				"accessor_declarations : set_accessor_declaration accessor_declarations",
				"accessor_declarations : error",
				"$$37 :",
				"get_accessor_declaration : opt_attributes opt_modifiers GET $$37 accessor_body",
				"$$38 :",
				"set_accessor_declaration : opt_attributes opt_modifiers SET $$38 accessor_body",
				"accessor_body : block",
				"accessor_body : SEMICOLON",
				"accessor_body : error",
				"$$39 :",
				"$$40 :",
				"$$41 :",
				"$$42 :",
				"interface_declaration : opt_attributes opt_modifiers opt_partial INTERFACE $$39 type_declaration_name $$40 opt_class_base opt_type_parameter_constraints_clauses $$41 OPEN_BRACE opt_interface_member_declarations CLOSE_BRACE $$42 opt_semicolon",
				"interface_declaration : opt_attributes opt_modifiers opt_partial INTERFACE error",
				"opt_interface_member_declarations :",
				"opt_interface_member_declarations : interface_member_declarations",
				"interface_member_declarations : interface_member_declaration",
				"interface_member_declarations : interface_member_declarations interface_member_declaration",
				"interface_member_declaration : constant_declaration",
				"interface_member_declaration : field_declaration",
				"interface_member_declaration : method_declaration",
				"interface_member_declaration : property_declaration",
				"interface_member_declaration : event_declaration",
				"interface_member_declaration : indexer_declaration",
				"interface_member_declaration : operator_declaration",
				"interface_member_declaration : constructor_declaration",
				"interface_member_declaration : type_declaration",
				"$$43 :",
				"operator_declaration : opt_attributes opt_modifiers operator_declarator $$43 method_body_expression_block",
				"operator_type : type_expression_or_array",
				"operator_type : VOID",
				"$$44 :",
				"operator_declarator : operator_type OPERATOR overloadable_operator OPEN_PARENS $$44 opt_formal_parameter_list CLOSE_PARENS",
				"operator_declarator : conversion_operator_declarator",
				"overloadable_operator : BANG",
				"overloadable_operator : TILDE",
				"overloadable_operator : OP_INC",
				"overloadable_operator : OP_DEC",
				"overloadable_operator : TRUE",
				"overloadable_operator : FALSE",
				"overloadable_operator : PLUS",
				"overloadable_operator : MINUS",
				"overloadable_operator : STAR",
				"overloadable_operator : DIV",
				"overloadable_operator : PERCENT",
				"overloadable_operator : BITWISE_AND",
				"overloadable_operator : BITWISE_OR",
				"overloadable_operator : CARRET",
				"overloadable_operator : OP_SHIFT_LEFT",
				"overloadable_operator : OP_SHIFT_RIGHT",
				"overloadable_operator : OP_EQ",
				"overloadable_operator : OP_NE",
				"overloadable_operator : OP_GT",
				"overloadable_operator : OP_LT",
				"overloadable_operator : OP_GE",
				"overloadable_operator : OP_LE",
				"overloadable_operator : IS",
				"$$45 :",
				"conversion_operator_declarator : IMPLICIT OPERATOR type OPEN_PARENS $$45 opt_formal_parameter_list CLOSE_PARENS",
				"$$46 :",
				"conversion_operator_declarator : EXPLICIT OPERATOR type OPEN_PARENS $$46 opt_formal_parameter_list CLOSE_PARENS",
				"conversion_operator_declarator : IMPLICIT error",
				"conversion_operator_declarator : EXPLICIT error",
				"constructor_declaration : constructor_declarator constructor_body",
				"$$47 :",
				"$$48 :",
				"constructor_declarator : opt_attributes opt_modifiers IDENTIFIER $$47 OPEN_PARENS opt_formal_parameter_list CLOSE_PARENS $$48 opt_constructor_initializer",
				"constructor_body : block_prepared",
				"constructor_body : SEMICOLON",
				"opt_constructor_initializer :",
				"opt_constructor_initializer : constructor_initializer",
				"$$49 :",
				"constructor_initializer : COLON BASE OPEN_PARENS $$49 opt_argument_list CLOSE_PARENS",
				"$$50 :",
				"constructor_initializer : COLON THIS OPEN_PARENS $$50 opt_argument_list CLOSE_PARENS",
				"constructor_initializer : COLON error",
				"constructor_initializer : error",
				"$$51 :",
				"destructor_declaration : opt_attributes opt_modifiers TILDE $$51 IDENTIFIER OPEN_PARENS CLOSE_PARENS method_body",
				"$$52 :",
				"event_declaration : opt_attributes opt_modifiers EVENT type member_declaration_name $$52 opt_event_initializer opt_event_declarators SEMICOLON",
				"$$53 :",
				"$$54 :",
				"event_declaration : opt_attributes opt_modifiers EVENT type member_declaration_name OPEN_BRACE $$53 event_accessor_declarations $$54 CLOSE_BRACE",
				"event_declaration : opt_attributes opt_modifiers EVENT type error",
				"opt_event_initializer :",
				"$$55 :",
				"opt_event_initializer : ASSIGN $$55 event_variable_initializer",
				"opt_event_declarators :",
				"opt_event_declarators : event_declarators",
				"event_declarators : event_declarator",
				"event_declarators : event_declarators event_declarator",
				"event_declarator : COMMA IDENTIFIER",
				"$$56 :",
				"event_declarator : COMMA IDENTIFIER ASSIGN $$56 event_variable_initializer",
				"$$57 :",
				"event_variable_initializer : $$57 variable_initializer",
				"event_accessor_declarations : add_accessor_declaration remove_accessor_declaration",
				"event_accessor_declarations : remove_accessor_declaration add_accessor_declaration",
				"event_accessor_declarations : add_accessor_declaration",
				"event_accessor_declarations : remove_accessor_declaration",
				"event_accessor_declarations : error",
				"$$58 :",
				"add_accessor_declaration : opt_attributes opt_modifiers ADD $$58 event_accessor_block",
				"$$59 :",
				"remove_accessor_declaration : opt_attributes opt_modifiers REMOVE $$59 event_accessor_block",
				"event_accessor_block : opt_semicolon",
				"event_accessor_block : block",
				"attributes_without_members : attribute_sections CLOSE_BRACE",
				"incomplete_member : opt_attributes opt_modifiers member_type CLOSE_BRACE",
				"$$60 :",
				"$$61 :",
				"$$62 :",
				"enum_declaration : opt_attributes opt_modifiers ENUM type_declaration_name opt_enum_base $$60 OPEN_BRACE $$61 opt_enum_member_declarations $$62 CLOSE_BRACE opt_semicolon",
				"opt_enum_base :",
				"opt_enum_base : COLON type",
				"opt_enum_base : COLON error",
				"opt_enum_member_declarations :",
				"opt_enum_member_declarations : enum_member_declarations",
				"opt_enum_member_declarations : enum_member_declarations COMMA",
				"enum_member_declarations : enum_member_declaration",
				"enum_member_declarations : enum_member_declarations COMMA enum_member_declaration",
				"enum_member_declaration : opt_attributes IDENTIFIER",
				"$$63 :",
				"enum_member_declaration : opt_attributes IDENTIFIER $$63 ASSIGN constant_expression",
				"enum_member_declaration : opt_attributes IDENTIFIER error",
				"enum_member_declaration : attributes_without_members",
				"$$64 :",
				"$$65 :",
				"$$66 :",
				"delegate_declaration : opt_attributes opt_modifiers DELEGATE member_type type_declaration_name OPEN_PARENS $$64 opt_formal_parameter_list CLOSE_PARENS $$65 opt_type_parameter_constraints_clauses $$66 SEMICOLON",
				"opt_nullable :",
				"opt_nullable : INTERR_NULLABLE",
				"namespace_or_type_expr : member_name",
				"namespace_or_type_expr : qualified_alias_member IDENTIFIER opt_type_argument_list",
				"namespace_or_type_expr : qualified_alias_member IDENTIFIER generic_dimension",
				"member_name : simple_name_expr",
				"member_name : namespace_or_type_expr DOT IDENTIFIER opt_type_argument_list",
				"member_name : namespace_or_type_expr DOT IDENTIFIER generic_dimension",
				"simple_name_expr : IDENTIFIER opt_type_argument_list",
				"simple_name_expr : IDENTIFIER generic_dimension",
				"opt_type_argument_list :",
				"opt_type_argument_list : OP_GENERICS_LT type_arguments OP_GENERICS_GT",
				"opt_type_argument_list : OP_GENERICS_LT error",
				"type_arguments : type",
				"type_arguments : type_arguments COMMA type",
				"$$67 :",
				"type_declaration_name : IDENTIFIER $$67 opt_type_parameter_list",
				"member_declaration_name : method_declaration_name",
				"method_declaration_name : type_declaration_name",
				"method_declaration_name : explicit_interface IDENTIFIER opt_type_parameter_list",
				"indexer_declaration_name : THIS",
				"indexer_declaration_name : explicit_interface THIS",
				"explicit_interface : IDENTIFIER opt_type_argument_list DOT",
				"explicit_interface : qualified_alias_member IDENTIFIER opt_type_argument_list DOT",
				"explicit_interface : explicit_interface IDENTIFIER opt_type_argument_list DOT",
				"opt_type_parameter_list :",
				"opt_type_parameter_list : OP_GENERICS_LT_DECL type_parameters OP_GENERICS_GT",
				"type_parameters : type_parameter",
				"type_parameters : type_parameters COMMA type_parameter",
				"type_parameter : opt_attributes opt_type_parameter_variance IDENTIFIER",
				"type_parameter : error",
				"type_and_void : type_expression_or_array",
				"type_and_void : VOID",
				"member_type : type_and_void",
				"type : type_expression_or_array",
				"type : void_invalid",
				"simple_type : type_expression",
				"simple_type : void_invalid",
				"parameter_type : type_expression_or_array",
				"parameter_type : VOID",
				"type_expression_or_array : type_expression",
				"type_expression_or_array : type_expression rank_specifiers",
				"type_expression : namespace_or_type_expr opt_nullable",
				"type_expression : namespace_or_type_expr pointer_stars",
				"type_expression : builtin_type_expression",
				"void_invalid : VOID",
				"builtin_type_expression : builtin_types opt_nullable",
				"builtin_type_expression : builtin_types pointer_stars",
				"builtin_type_expression : VOID pointer_stars",
				"type_list : base_type_name",
				"type_list : type_list COMMA base_type_name",
				"base_type_name : type",
				"builtin_types : OBJECT",
				"builtin_types : STRING",
				"builtin_types : BOOL",
				"builtin_types : DECIMAL",
				"builtin_types : FLOAT",
				"builtin_types : DOUBLE",
				"builtin_types : integral_type",
				"integral_type : SBYTE",
				"integral_type : BYTE",
				"integral_type : SHORT",
				"integral_type : USHORT",
				"integral_type : INT",
				"integral_type : UINT",
				"integral_type : LONG",
				"integral_type : ULONG",
				"integral_type : CHAR",
				"primary_expression : type_name_expression",
				"primary_expression : literal",
				"primary_expression : array_creation_expression",
				"primary_expression : parenthesized_expression",
				"primary_expression : default_value_expression",
				"primary_expression : invocation_expression",
				"primary_expression : element_access",
				"primary_expression : this_access",
				"primary_expression : base_access",
				"primary_expression : post_increment_expression",
				"primary_expression : post_decrement_expression",
				"primary_expression : object_or_delegate_creation_expression",
				"primary_expression : anonymous_type_expression",
				"primary_expression : typeof_expression",
				"primary_expression : sizeof_expression",
				"primary_expression : checked_expression",
				"primary_expression : unchecked_expression",
				"primary_expression : pointer_member_access",
				"primary_expression : anonymous_method_expression",
				"primary_expression : undocumented_expressions",
				"primary_expression : interpolated_string",
				"type_name_expression : simple_name_expr",
				"type_name_expression : IDENTIFIER GENERATE_COMPLETION",
				"type_name_expression : member_access",
				"literal : boolean_literal",
				"literal : LITERAL",
				"literal : NULL",
				"boolean_literal : TRUE",
				"boolean_literal : FALSE",
				"interpolated_string : INTERPOLATED_STRING interpolations INTERPOLATED_STRING_END",
				"interpolated_string : INTERPOLATED_STRING_END",
				"interpolations : interpolation",
				"interpolations : interpolations INTERPOLATED_STRING interpolation",
				"interpolation : expression",
				"interpolation : expression COMMA expression",
				"$$68 :",
				"interpolation : expression COLON $$68 LITERAL",
				"$$69 :",
				"interpolation : expression COMMA expression COLON $$69 LITERAL",
				"open_parens_any : OPEN_PARENS",
				"open_parens_any : OPEN_PARENS_CAST",
				"close_parens : CLOSE_PARENS",
				"close_parens : COMPLETE_COMPLETION",
				"parenthesized_expression : OPEN_PARENS expression CLOSE_PARENS",
				"parenthesized_expression : OPEN_PARENS expression COMPLETE_COMPLETION",
				"member_access : primary_expression DOT identifier_inside_body opt_type_argument_list",
				"member_access : primary_expression DOT identifier_inside_body generic_dimension",
				"member_access : primary_expression INTERR_OPERATOR DOT identifier_inside_body opt_type_argument_list",
				"member_access : builtin_types DOT identifier_inside_body opt_type_argument_list",
				"member_access : BASE DOT identifier_inside_body opt_type_argument_list",
				"member_access : AWAIT DOT identifier_inside_body opt_type_argument_list",
				"member_access : qualified_alias_member identifier_inside_body opt_type_argument_list",
				"member_access : qualified_alias_member identifier_inside_body generic_dimension",
				"member_access : primary_expression DOT GENERATE_COMPLETION",
				"member_access : primary_expression DOT IDENTIFIER GENERATE_COMPLETION",
				"member_access : builtin_types DOT GENERATE_COMPLETION",
				"member_access : builtin_types DOT IDENTIFIER GENERATE_COMPLETION",
				"invocation_expression : primary_expression open_parens_any opt_argument_list close_parens",
				"invocation_expression : primary_expression open_parens_any argument_list error",
				"invocation_expression : primary_expression open_parens_any error",
				"opt_object_or_collection_initializer :",
				"opt_object_or_collection_initializer : object_or_collection_initializer",
				"object_or_collection_initializer : OPEN_BRACE opt_member_initializer_list close_brace_or_complete_completion",
				"object_or_collection_initializer : OPEN_BRACE member_initializer_list COMMA CLOSE_BRACE",
				"opt_member_initializer_list :",
				"opt_member_initializer_list : member_initializer_list",
				"member_initializer_list : member_initializer",
				"member_initializer_list : member_initializer_list COMMA member_initializer",
				"member_initializer_list : member_initializer_list error",
				"member_initializer : IDENTIFIER ASSIGN initializer_value",
				"member_initializer : AWAIT ASSIGN initializer_value",
				"member_initializer : GENERATE_COMPLETION",
				"member_initializer : non_assignment_expression opt_COMPLETE_COMPLETION",
				"member_initializer : OPEN_BRACE expression_list CLOSE_BRACE",
				"member_initializer : OPEN_BRACKET_EXPR argument_list CLOSE_BRACKET ASSIGN initializer_value",
				"member_initializer : OPEN_BRACE CLOSE_BRACE",
				"initializer_value : expression",
				"initializer_value : object_or_collection_initializer",
				"opt_argument_list :",
				"opt_argument_list : argument_list",
				"argument_list : argument_or_named_argument",
				"argument_list : argument_list COMMA argument",
				"argument_list : argument_list COMMA named_argument",
				"argument_list : argument_list COMMA error",
				"argument_list : COMMA error",
				"argument : expression",
				"argument : non_simple_argument",
				"argument_or_named_argument : argument",
				"argument_or_named_argument : named_argument",
				"non_simple_argument : REF variable_reference",
				"non_simple_argument : REF declaration_expression",
				"non_simple_argument : OUT variable_reference",
				"non_simple_argument : OUT declaration_expression",
				"non_simple_argument : ARGLIST OPEN_PARENS argument_list CLOSE_PARENS",
				"non_simple_argument : ARGLIST OPEN_PARENS CLOSE_PARENS",
				"declaration_expression : OPEN_PARENS declaration_expression CLOSE_PARENS",
				"declaration_expression : variable_type identifier_inside_body",
				"declaration_expression : variable_type identifier_inside_body ASSIGN expression",
				"variable_reference : expression",
				"element_access : primary_expression OPEN_BRACKET_EXPR expression_list_arguments CLOSE_BRACKET",
				"element_access : primary_expression INTERR_OPERATOR OPEN_BRACKET_EXPR expression_list_arguments CLOSE_BRACKET",
				"element_access : primary_expression OPEN_BRACKET_EXPR expression_list_arguments error",
				"element_access : primary_expression OPEN_BRACKET_EXPR error",
				"expression_list : expression_or_error",
				"expression_list : expression_list COMMA expression_or_error",
				"expression_list_arguments : expression_list_argument",
				"expression_list_arguments : expression_list_arguments COMMA expression_list_argument",
				"expression_list_argument : expression",
				"expression_list_argument : named_argument",
				"this_access : THIS",
				"base_access : BASE OPEN_BRACKET_EXPR expression_list_arguments CLOSE_BRACKET",
				"base_access : BASE OPEN_BRACKET error",
				"post_increment_expression : primary_expression OP_INC",
				"post_decrement_expression : primary_expression OP_DEC",
				"object_or_delegate_creation_expression : NEW new_expr_type open_parens_any opt_argument_list CLOSE_PARENS opt_object_or_collection_initializer",
				"object_or_delegate_creation_expression : NEW new_expr_type object_or_collection_initializer",
				"array_creation_expression : NEW new_expr_type OPEN_BRACKET_EXPR expression_list CLOSE_BRACKET opt_rank_specifier opt_array_initializer",
				"array_creation_expression : NEW new_expr_type rank_specifiers opt_array_initializer",
				"array_creation_expression : NEW rank_specifier array_initializer",
				"array_creation_expression : NEW new_expr_type OPEN_BRACKET CLOSE_BRACKET OPEN_BRACKET_EXPR error CLOSE_BRACKET",
				"array_creation_expression : NEW new_expr_type error",
				"$$70 :",
				"new_expr_type : $$70 simple_type",
				"anonymous_type_expression : NEW OPEN_BRACE anonymous_type_parameters_opt_comma CLOSE_BRACE",
				"anonymous_type_expression : NEW OPEN_BRACE GENERATE_COMPLETION",
				"anonymous_type_parameters_opt_comma : anonymous_type_parameters_opt",
				"anonymous_type_parameters_opt_comma : anonymous_type_parameters COMMA",
				"anonymous_type_parameters_opt :",
				"anonymous_type_parameters_opt : anonymous_type_parameters",
				"anonymous_type_parameters : anonymous_type_parameter",
				"anonymous_type_parameters : anonymous_type_parameters COMMA anonymous_type_parameter",
				"anonymous_type_parameters : COMPLETE_COMPLETION",
				"anonymous_type_parameters : anonymous_type_parameter COMPLETE_COMPLETION",
				"anonymous_type_parameter : identifier_inside_body ASSIGN variable_initializer",
				"anonymous_type_parameter : identifier_inside_body",
				"anonymous_type_parameter : member_access",
				"anonymous_type_parameter : error",
				"opt_rank_specifier :",
				"opt_rank_specifier : rank_specifiers",
				"rank_specifiers : rank_specifier",
				"rank_specifiers : rank_specifier rank_specifiers",
				"rank_specifier : OPEN_BRACKET CLOSE_BRACKET",
				"rank_specifier : OPEN_BRACKET dim_separators CLOSE_BRACKET",
				"dim_separators : COMMA",
				"dim_separators : dim_separators COMMA",
				"opt_array_initializer :",
				"opt_array_initializer : array_initializer",
				"array_initializer : OPEN_BRACE CLOSE_BRACE",
				"array_initializer : OPEN_BRACE variable_initializer_list opt_comma CLOSE_BRACE",
				"variable_initializer_list : variable_initializer",
				"variable_initializer_list : variable_initializer_list COMMA variable_initializer",
				"typeof_expression : TYPEOF open_parens_any typeof_type_expression CLOSE_PARENS",
				"typeof_type_expression : type_and_void",
				"typeof_type_expression : error",
				"generic_dimension : GENERIC_DIMENSION",
				"qualified_alias_member : IDENTIFIER DOUBLE_COLON",
				"sizeof_expression : SIZEOF open_parens_any type CLOSE_PARENS",
				"sizeof_expression : SIZEOF open_parens_any type error",
				"checked_expression : CHECKED open_parens_any expression CLOSE_PARENS",
				"checked_expression : CHECKED error",
				"unchecked_expression : UNCHECKED open_parens_any expression CLOSE_PARENS",
				"unchecked_expression : UNCHECKED error",
				"pointer_member_access : primary_expression OP_PTR IDENTIFIER opt_type_argument_list",
				"$$71 :",
				"anonymous_method_expression : DELEGATE opt_anonymous_method_signature $$71 block",
				"$$72 :",
				"anonymous_method_expression : ASYNC DELEGATE opt_anonymous_method_signature $$72 block",
				"opt_anonymous_method_signature :",
				"opt_anonymous_method_signature : anonymous_method_signature",
				"$$73 :",
				"anonymous_method_signature : OPEN_PARENS $$73 opt_formal_parameter_list CLOSE_PARENS",
				"default_value_expression : DEFAULT open_parens_any type CLOSE_PARENS",
				"unary_expression : primary_expression",
				"unary_expression : BANG prefixed_unary_expression",
				"unary_expression : TILDE prefixed_unary_expression",
				"unary_expression : OPEN_PARENS_CAST type CLOSE_PARENS prefixed_unary_expression",
				"unary_expression : AWAIT prefixed_unary_expression",
				"unary_expression : BANG error",
				"unary_expression : TILDE error",
				"unary_expression : OPEN_PARENS_CAST type CLOSE_PARENS error",
				"unary_expression : AWAIT error",
				"prefixed_unary_expression : unary_expression",
				"prefixed_unary_expression : PLUS prefixed_unary_expression",
				"prefixed_unary_expression : MINUS prefixed_unary_expression",
				"prefixed_unary_expression : OP_INC prefixed_unary_expression",
				"prefixed_unary_expression : OP_DEC prefixed_unary_expression",
				"prefixed_unary_expression : STAR prefixed_unary_expression",
				"prefixed_unary_expression : BITWISE_AND prefixed_unary_expression",
				"prefixed_unary_expression : PLUS error",
				"prefixed_unary_expression : MINUS error",
				"prefixed_unary_expression : OP_INC error",
				"prefixed_unary_expression : OP_DEC error",
				"prefixed_unary_expression : STAR error",
				"prefixed_unary_expression : BITWISE_AND error",
				"multiplicative_expression : prefixed_unary_expression",
				"multiplicative_expression : multiplicative_expression STAR prefixed_unary_expression",
				"multiplicative_expression : multiplicative_expression DIV prefixed_unary_expression",
				"multiplicative_expression : multiplicative_expression PERCENT prefixed_unary_expression",
				"multiplicative_expression : multiplicative_expression STAR error",
				"multiplicative_expression : multiplicative_expression DIV error",
				"multiplicative_expression : multiplicative_expression PERCENT error",
				"additive_expression : multiplicative_expression",
				"additive_expression : additive_expression PLUS multiplicative_expression",
				"additive_expression : additive_expression MINUS multiplicative_expression",
				"additive_expression : additive_expression PLUS error",
				"additive_expression : additive_expression MINUS error",
				"additive_expression : additive_expression AS type",
				"additive_expression : additive_expression IS pattern_type_expr opt_identifier",
				"additive_expression : additive_expression IS pattern_expr",
				"additive_expression : additive_expression AS error",
				"additive_expression : additive_expression IS error",
				"additive_expression : AWAIT IS type",
				"additive_expression : AWAIT AS type",
				"pattern_type_expr : variable_type",
				"pattern_expr : literal",
				"pattern_expr : PLUS prefixed_unary_expression",
				"pattern_expr : MINUS prefixed_unary_expression",
				"pattern_expr : sizeof_expression",
				"pattern_expr : default_value_expression",
				"pattern_expr : OPEN_PARENS_CAST type CLOSE_PARENS prefixed_unary_expression",
				"pattern_expr : STAR",
				"pattern_expr : pattern_expr_invocation",
				"pattern_expr : pattern_property",
				"pattern_expr_invocation : type_name_expression OPEN_PARENS opt_pattern_list CLOSE_PARENS",
				"pattern_property : type_name_expression OPEN_BRACE pattern_property_list CLOSE_BRACE",
				"pattern_property_list : pattern_property_entry",
				"pattern_property_list : pattern_property_list COMMA pattern_property_entry",
				"pattern_property_entry : identifier_inside_body IS pattern",
				"pattern : pattern_expr",
				"pattern : pattern_type_expr opt_identifier",
				"opt_pattern_list :",
				"opt_pattern_list : pattern_list",
				"pattern_list : pattern_argument",
				"pattern_list : pattern_list COMMA pattern_argument",
				"pattern_argument : pattern",
				"pattern_argument : IDENTIFIER COLON pattern",
				"shift_expression : additive_expression",
				"shift_expression : shift_expression OP_SHIFT_LEFT additive_expression",
				"shift_expression : shift_expression OP_SHIFT_RIGHT additive_expression",
				"shift_expression : shift_expression OP_SHIFT_LEFT error",
				"shift_expression : shift_expression OP_SHIFT_RIGHT error",
				"relational_expression : shift_expression",
				"relational_expression : relational_expression OP_LT shift_expression",
				"relational_expression : relational_expression OP_GT shift_expression",
				"relational_expression : relational_expression OP_LE shift_expression",
				"relational_expression : relational_expression OP_GE shift_expression",
				"relational_expression : relational_expression OP_LT error",
				"relational_expression : relational_expression OP_GT error",
				"relational_expression : relational_expression OP_LE error",
				"relational_expression : relational_expression OP_GE error",
				"equality_expression : relational_expression",
				"equality_expression : equality_expression OP_EQ relational_expression",
				"equality_expression : equality_expression OP_NE relational_expression",
				"equality_expression : equality_expression OP_EQ error",
				"equality_expression : equality_expression OP_NE error",
				"and_expression : equality_expression",
				"and_expression : and_expression BITWISE_AND equality_expression",
				"and_expression : and_expression BITWISE_AND error",
				"exclusive_or_expression : and_expression",
				"exclusive_or_expression : exclusive_or_expression CARRET and_expression",
				"exclusive_or_expression : exclusive_or_expression CARRET error",
				"inclusive_or_expression : exclusive_or_expression",
				"inclusive_or_expression : inclusive_or_expression BITWISE_OR exclusive_or_expression",
				"inclusive_or_expression : inclusive_or_expression BITWISE_OR error",
				"conditional_and_expression : inclusive_or_expression",
				"conditional_and_expression : conditional_and_expression OP_AND inclusive_or_expression",
				"conditional_and_expression : conditional_and_expression OP_AND error",
				"conditional_or_expression : conditional_and_expression",
				"conditional_or_expression : conditional_or_expression OP_OR conditional_and_expression",
				"conditional_or_expression : conditional_or_expression OP_OR error",
				"null_coalescing_expression : conditional_or_expression",
				"null_coalescing_expression : conditional_or_expression OP_COALESCING null_coalescing_expression",
				"conditional_expression : null_coalescing_expression",
				"conditional_expression : null_coalescing_expression INTERR expression COLON expression",
				"conditional_expression : null_coalescing_expression INTERR expression error",
				"conditional_expression : null_coalescing_expression INTERR expression COLON error",
				"conditional_expression : null_coalescing_expression INTERR expression COLON CLOSE_BRACE",
				"assignment_expression : prefixed_unary_expression ASSIGN expression",
				"assignment_expression : prefixed_unary_expression OP_MULT_ASSIGN expression",
				"assignment_expression : prefixed_unary_expression OP_DIV_ASSIGN expression",
				"assignment_expression : prefixed_unary_expression OP_MOD_ASSIGN expression",
				"assignment_expression : prefixed_unary_expression OP_ADD_ASSIGN expression",
				"assignment_expression : prefixed_unary_expression OP_SUB_ASSIGN expression",
				"assignment_expression : prefixed_unary_expression OP_SHIFT_LEFT_ASSIGN expression",
				"assignment_expression : prefixed_unary_expression OP_SHIFT_RIGHT_ASSIGN expression",
				"assignment_expression : prefixed_unary_expression OP_AND_ASSIGN expression",
				"assignment_expression : prefixed_unary_expression OP_OR_ASSIGN expression",
				"assignment_expression : prefixed_unary_expression OP_XOR_ASSIGN expression",
				"lambda_parameter_list : lambda_parameter",
				"lambda_parameter_list : lambda_parameter_list COMMA lambda_parameter",
				"lambda_parameter : parameter_modifier parameter_type identifier_inside_body",
				"lambda_parameter : parameter_type identifier_inside_body",
				"lambda_parameter : IDENTIFIER",
				"lambda_parameter : AWAIT",
				"opt_lambda_parameter_list :",
				"opt_lambda_parameter_list : lambda_parameter_list",
				"$$74 :",
				"lambda_expression_body : $$74 expression",
				"lambda_expression_body : block",
				"lambda_expression_body : error",
				"expression_or_error : expression",
				"expression_or_error : error",
				"$$75 :",
				"lambda_expression : IDENTIFIER ARROW $$75 lambda_expression_body",
				"$$76 :",
				"lambda_expression : AWAIT ARROW $$76 lambda_expression_body",
				"$$77 :",
				"lambda_expression : ASYNC identifier_inside_body ARROW $$77 lambda_expression_body",
				"$$78 :",
				"$$79 :",
				"lambda_expression : OPEN_PARENS_LAMBDA $$78 opt_lambda_parameter_list CLOSE_PARENS ARROW $$79 lambda_expression_body",
				"$$80 :",
				"$$81 :",
				"lambda_expression : ASYNC OPEN_PARENS_LAMBDA $$80 opt_lambda_parameter_list CLOSE_PARENS ARROW $$81 lambda_expression_body",
				"expression : assignment_expression",
				"expression : non_assignment_expression",
				"non_assignment_expression : conditional_expression",
				"non_assignment_expression : lambda_expression",
				"non_assignment_expression : query_expression",
				"non_assignment_expression : ARGLIST",
				"undocumented_expressions : REFVALUE OPEN_PARENS non_assignment_expression COMMA type CLOSE_PARENS",
				"undocumented_expressions : REFTYPE open_parens_any expression CLOSE_PARENS",
				"undocumented_expressions : MAKEREF open_parens_any expression CLOSE_PARENS",
				"constant_expression : expression",
				"boolean_expression : expression",
				"opt_primary_parameters :",
				"opt_primary_parameters : primary_parameters",
				"primary_parameters : OPEN_PARENS opt_formal_parameter_list CLOSE_PARENS",
				"opt_primary_parameters_with_class_base :",
				"opt_primary_parameters_with_class_base : class_base",
				"opt_primary_parameters_with_class_base : primary_parameters",
				"opt_primary_parameters_with_class_base : primary_parameters class_base",
				"$$82 :",
				"opt_primary_parameters_with_class_base : primary_parameters class_base OPEN_PARENS $$82 opt_argument_list CLOSE_PARENS",
				"$$83 :",
				"$$84 :",
				"$$85 :",
				"$$86 :",
				"class_declaration : opt_attributes opt_modifiers opt_partial CLASS $$83 type_declaration_name $$84 opt_primary_parameters_with_class_base opt_type_parameter_constraints_clauses $$85 OPEN_BRACE opt_class_member_declarations CLOSE_BRACE $$86 opt_semicolon",
				"opt_partial :",
				"opt_partial : PARTIAL",
				"opt_modifiers :",
				"opt_modifiers : modifiers",
				"modifiers : modifier",
				"modifiers : modifiers modifier",
				"modifier : NEW",
				"modifier : PUBLIC",
				"modifier : PROTECTED",
				"modifier : INTERNAL",
				"modifier : PRIVATE",
				"modifier : ABSTRACT",
				"modifier : SEALED",
				"modifier : STATIC",
				"modifier : READONLY",
				"modifier : VIRTUAL",
				"modifier : OVERRIDE",
				"modifier : EXTERN",
				"modifier : VOLATILE",
				"modifier : UNSAFE",
				"modifier : ASYNC",
				"opt_class_base :",
				"opt_class_base : class_base",
				"class_base : COLON type_list",
				"class_base : COLON type_list error",
				"opt_type_parameter_constraints_clauses :",
				"opt_type_parameter_constraints_clauses : type_parameter_constraints_clauses",
				"type_parameter_constraints_clauses : type_parameter_constraints_clause",
				"type_parameter_constraints_clauses : type_parameter_constraints_clauses type_parameter_constraints_clause",
				"type_parameter_constraints_clause : WHERE IDENTIFIER COLON type_parameter_constraints",
				"type_parameter_constraints_clause : WHERE IDENTIFIER error",
				"type_parameter_constraints : type_parameter_constraint",
				"type_parameter_constraints : type_parameter_constraints COMMA type_parameter_constraint",
				"type_parameter_constraint : type",
				"type_parameter_constraint : NEW OPEN_PARENS CLOSE_PARENS",
				"type_parameter_constraint : CLASS",
				"type_parameter_constraint : STRUCT",
				"opt_type_parameter_variance :",
				"opt_type_parameter_variance : type_parameter_variance",
				"type_parameter_variance : OUT",
				"type_parameter_variance : IN",
				"$$87 :",
				"block : OPEN_BRACE $$87 opt_statement_list block_end",
				"block_end : CLOSE_BRACE",
				"block_end : COMPLETE_COMPLETION",
				"$$88 :",
				"block_prepared : OPEN_BRACE $$88 opt_statement_list CLOSE_BRACE",
				"opt_statement_list :",
				"opt_statement_list : statement_list",
				"statement_list : statement",
				"statement_list : statement_list statement",
				"statement : block_variable_declaration",
				"statement : valid_declaration_statement",
				"statement : labeled_statement",
				"statement : error",
				"interactive_statement_list : interactive_statement",
				"interactive_statement_list : interactive_statement_list interactive_statement",
				"interactive_statement : block_variable_declaration",
				"interactive_statement : interactive_valid_declaration_statement",
				"interactive_statement : labeled_statement",
				"valid_declaration_statement : block",
				"valid_declaration_statement : empty_statement",
				"valid_declaration_statement : expression_statement",
				"valid_declaration_statement : selection_statement",
				"valid_declaration_statement : iteration_statement",
				"valid_declaration_statement : jump_statement",
				"valid_declaration_statement : try_statement",
				"valid_declaration_statement : checked_statement",
				"valid_declaration_statement : unchecked_statement",
				"valid_declaration_statement : lock_statement",
				"valid_declaration_statement : using_statement",
				"valid_declaration_statement : unsafe_statement",
				"valid_declaration_statement : fixed_statement",
				"interactive_valid_declaration_statement : block",
				"interactive_valid_declaration_statement : empty_statement",
				"interactive_valid_declaration_statement : interactive_expression_statement",
				"interactive_valid_declaration_statement : selection_statement",
				"interactive_valid_declaration_statement : iteration_statement",
				"interactive_valid_declaration_statement : jump_statement",
				"interactive_valid_declaration_statement : try_statement",
				"interactive_valid_declaration_statement : checked_statement",
				"interactive_valid_declaration_statement : unchecked_statement",
				"interactive_valid_declaration_statement : lock_statement",
				"interactive_valid_declaration_statement : using_statement",
				"interactive_valid_declaration_statement : unsafe_statement",
				"interactive_valid_declaration_statement : fixed_statement",
				"embedded_statement : valid_declaration_statement",
				"embedded_statement : block_variable_declaration",
				"embedded_statement : labeled_statement",
				"embedded_statement : error",
				"empty_statement : SEMICOLON",
				"$$89 :",
				"labeled_statement : identifier_inside_body COLON $$89 statement",
				"variable_type : variable_type_simple",
				"variable_type : variable_type_simple rank_specifiers",
				"variable_type_simple : type_name_expression opt_nullable",
				"variable_type_simple : type_name_expression pointer_stars",
				"variable_type_simple : builtin_type_expression",
				"variable_type_simple : void_invalid",
				"pointer_stars : pointer_star",
				"pointer_stars : pointer_star pointer_stars",
				"pointer_star : STAR",
				"identifier_inside_body : IDENTIFIER",
				"identifier_inside_body : AWAIT",
				"$$90 :",
				"block_variable_declaration : variable_type identifier_inside_body $$90 opt_local_variable_initializer opt_variable_declarators SEMICOLON",
				"$$91 :",
				"block_variable_declaration : CONST variable_type identifier_inside_body $$91 const_variable_initializer opt_const_declarators SEMICOLON",
				"opt_local_variable_initializer :",
				"opt_local_variable_initializer : ASSIGN block_variable_initializer",
				"opt_local_variable_initializer : error",
				"opt_variable_declarators :",
				"opt_variable_declarators : variable_declarators",
				"opt_using_or_fixed_variable_declarators :",
				"opt_using_or_fixed_variable_declarators : variable_declarators",
				"variable_declarators : variable_declarator",
				"variable_declarators : variable_declarators variable_declarator",
				"variable_declarator : COMMA identifier_inside_body",
				"variable_declarator : COMMA identifier_inside_body ASSIGN block_variable_initializer",
				"const_variable_initializer :",
				"const_variable_initializer : ASSIGN constant_initializer_expr",
				"opt_const_declarators :",
				"opt_const_declarators : const_declarators",
				"const_declarators : const_declarator",
				"const_declarators : const_declarators const_declarator",
				"const_declarator : COMMA identifier_inside_body ASSIGN constant_initializer_expr",
				"block_variable_initializer : variable_initializer",
				"block_variable_initializer : STACKALLOC simple_type OPEN_BRACKET_EXPR expression CLOSE_BRACKET",
				"block_variable_initializer : STACKALLOC simple_type",
				"expression_statement : statement_expression SEMICOLON",
				"expression_statement : statement_expression COMPLETE_COMPLETION",
				"expression_statement : statement_expression CLOSE_BRACE",
				"interactive_expression_statement : interactive_statement_expression SEMICOLON",
				"interactive_expression_statement : interactive_statement_expression COMPLETE_COMPLETION",
				"statement_expression : expression",
				"interactive_statement_expression : expression",
				"interactive_statement_expression : error",
				"selection_statement : if_statement",
				"selection_statement : switch_statement",
				"if_statement : IF open_parens_any boolean_expression CLOSE_PARENS embedded_statement",
				"if_statement : IF open_parens_any boolean_expression CLOSE_PARENS embedded_statement ELSE embedded_statement",
				"if_statement : IF open_parens_any boolean_expression error",
				"$$92 :",
				"switch_statement : SWITCH open_parens_any expression CLOSE_PARENS OPEN_BRACE $$92 opt_switch_sections CLOSE_BRACE",
				"switch_statement : SWITCH open_parens_any expression error",
				"opt_switch_sections :",
				"opt_switch_sections : switch_sections",
				"switch_sections : switch_section",
				"switch_sections : switch_sections switch_section",
				"switch_sections : error",
				"switch_section : switch_labels statement_list",
				"switch_labels : switch_label",
				"switch_labels : switch_labels switch_label",
				"switch_label : CASE constant_expression COLON",
				"switch_label : CASE constant_expression error",
				"switch_label : DEFAULT_COLON",
				"iteration_statement : while_statement",
				"iteration_statement : do_statement",
				"iteration_statement : for_statement",
				"iteration_statement : foreach_statement",
				"while_statement : WHILE open_parens_any boolean_expression CLOSE_PARENS embedded_statement",
				"while_statement : WHILE open_parens_any boolean_expression error",
				"do_statement : DO embedded_statement WHILE open_parens_any boolean_expression CLOSE_PARENS SEMICOLON",
				"do_statement : DO embedded_statement error",
				"do_statement : DO embedded_statement WHILE open_parens_any boolean_expression error",
				"$$93 :",
				"for_statement : FOR open_parens_any $$93 for_statement_cont",
				"$$94 :",
				"for_statement_cont : opt_for_initializer SEMICOLON $$94 for_condition_and_iterator_part embedded_statement",
				"for_statement_cont : error",
				"$$95 :",
				"for_condition_and_iterator_part : opt_for_condition SEMICOLON $$95 for_iterator_part",
				"for_condition_and_iterator_part : opt_for_condition close_parens_close_brace",
				"for_iterator_part : opt_for_iterator CLOSE_PARENS",
				"for_iterator_part : opt_for_iterator CLOSE_BRACE",
				"close_parens_close_brace : CLOSE_PARENS",
				"close_parens_close_brace : CLOSE_BRACE",
				"opt_for_initializer :",
				"opt_for_initializer : for_initializer",
				"$$96 :",
				"for_initializer : variable_type identifier_inside_body $$96 opt_local_variable_initializer opt_variable_declarators",
				"for_initializer : statement_expression_list",
				"opt_for_condition :",
				"opt_for_condition : boolean_expression",
				"opt_for_iterator :",
				"opt_for_iterator : for_iterator",
				"for_iterator : statement_expression_list",
				"statement_expression_list : statement_expression",
				"statement_expression_list : statement_expression_list COMMA statement_expression",
				"foreach_statement : FOREACH open_parens_any type error",
				"foreach_statement : FOREACH open_parens_any type identifier_inside_body error",
				"$$97 :",
				"foreach_statement : FOREACH open_parens_any type identifier_inside_body IN expression CLOSE_PARENS $$97 embedded_statement",
				"jump_statement : break_statement",
				"jump_statement : continue_statement",
				"jump_statement : goto_statement",
				"jump_statement : return_statement",
				"jump_statement : throw_statement",
				"jump_statement : yield_statement",
				"break_statement : BREAK SEMICOLON",
				"continue_statement : CONTINUE SEMICOLON",
				"continue_statement : CONTINUE error",
				"goto_statement : GOTO identifier_inside_body SEMICOLON",
				"goto_statement : GOTO CASE constant_expression SEMICOLON",
				"goto_statement : GOTO DEFAULT SEMICOLON",
				"return_statement : RETURN opt_expression SEMICOLON",
				"return_statement : RETURN expression error",
				"return_statement : RETURN error",
				"throw_statement : THROW opt_expression SEMICOLON",
				"throw_statement : THROW expression error",
				"throw_statement : THROW error",
				"yield_statement : identifier_inside_body RETURN opt_expression SEMICOLON",
				"yield_statement : identifier_inside_body RETURN expression error",
				"yield_statement : identifier_inside_body BREAK SEMICOLON",
				"opt_expression :",
				"opt_expression : expression",
				"try_statement : TRY block catch_clauses",
				"try_statement : TRY block FINALLY block",
				"try_statement : TRY block catch_clauses FINALLY block",
				"try_statement : TRY block error",
				"catch_clauses : catch_clause",
				"catch_clauses : catch_clauses catch_clause",
				"opt_identifier :",
				"opt_identifier : identifier_inside_body",
				"catch_clause : CATCH opt_catch_filter block",
				"$$98 :",
				"catch_clause : CATCH open_parens_any type opt_identifier CLOSE_PARENS $$98 opt_catch_filter_or_error",
				"catch_clause : CATCH open_parens_any error",
				"opt_catch_filter_or_error : opt_catch_filter block_prepared",
				"opt_catch_filter_or_error : error",
				"opt_catch_filter :",
				"$$99 :",
				"opt_catch_filter : WHEN $$99 open_parens_any expression CLOSE_PARENS",
				"checked_statement : CHECKED block",
				"unchecked_statement : UNCHECKED block",
				"$$100 :",
				"unsafe_statement : UNSAFE $$100 block",
				"lock_statement : LOCK open_parens_any expression CLOSE_PARENS embedded_statement",
				"lock_statement : LOCK open_parens_any expression error",
				"$$101 :",
				"$$102 :",
				"fixed_statement : FIXED open_parens_any variable_type identifier_inside_body $$101 using_or_fixed_variable_initializer opt_using_or_fixed_variable_declarators CLOSE_PARENS $$102 embedded_statement",
				"$$103 :",
				"$$104 :",
				"using_statement : USING open_parens_any variable_type identifier_inside_body $$103 using_initialization CLOSE_PARENS $$104 embedded_statement",
				"using_statement : USING open_parens_any expression CLOSE_PARENS embedded_statement",
				"using_statement : USING open_parens_any expression error",
				"using_initialization : using_or_fixed_variable_initializer opt_using_or_fixed_variable_declarators",
				"using_initialization : error",
				"using_or_fixed_variable_initializer :",
				"using_or_fixed_variable_initializer : ASSIGN variable_initializer",
				"query_expression : first_from_clause query_body",
				"query_expression : nested_from_clause query_body",
				"query_expression : first_from_clause COMPLETE_COMPLETION",
				"query_expression : nested_from_clause COMPLETE_COMPLETION",
				"first_from_clause : FROM_FIRST identifier_inside_body IN expression",
				"first_from_clause : FROM_FIRST type identifier_inside_body IN expression",
				"nested_from_clause : FROM identifier_inside_body IN expression",
				"nested_from_clause : FROM type identifier_inside_body IN expression",
				"$$105 :",
				"from_clause : FROM identifier_inside_body IN $$105 expression_or_error",
				"$$106 :",
				"from_clause : FROM type identifier_inside_body IN $$106 expression_or_error",
				"query_body : query_body_clauses select_or_group_clause opt_query_continuation",
				"query_body : select_or_group_clause opt_query_continuation",
				"query_body : query_body_clauses COMPLETE_COMPLETION",
				"query_body : query_body_clauses error",
				"query_body : error",
				"$$107 :",
				"select_or_group_clause : SELECT $$107 expression_or_error",
				"$$108 :",
				"$$109 :",
				"select_or_group_clause : GROUP $$108 expression_or_error $$109 by_expression",
				"by_expression : BY expression_or_error",
				"by_expression : error",
				"query_body_clauses : query_body_clause",
				"query_body_clauses : query_body_clauses query_body_clause",
				"query_body_clause : from_clause",
				"query_body_clause : let_clause",
				"query_body_clause : where_clause",
				"query_body_clause : join_clause",
				"query_body_clause : orderby_clause",
				"$$110 :",
				"let_clause : LET identifier_inside_body ASSIGN $$110 expression_or_error",
				"$$111 :",
				"where_clause : WHERE $$111 expression_or_error",
				"$$112 :",
				"$$113 :",
				"$$114 :",
				"join_clause : JOIN identifier_inside_body IN $$112 expression_or_error ON $$113 expression_or_error EQUALS $$114 expression_or_error opt_join_into",
				"$$115 :",
				"$$116 :",
				"$$117 :",
				"join_clause : JOIN type identifier_inside_body IN $$115 expression_or_error ON $$116 expression_or_error EQUALS $$117 expression_or_error opt_join_into",
				"opt_join_into :",
				"opt_join_into : INTO identifier_inside_body",
				"$$118 :",
				"orderby_clause : ORDERBY $$118 orderings",
				"orderings : order_by",
				"$$119 :",
				"orderings : order_by COMMA $$119 orderings_then_by",
				"orderings_then_by : then_by",
				"$$120 :",
				"orderings_then_by : orderings_then_by COMMA $$120 then_by",
				"order_by : expression",
				"order_by : expression ASCENDING",
				"order_by : expression DESCENDING",
				"then_by : expression",
				"then_by : expression ASCENDING",
				"then_by : expression DESCENDING",
				"opt_query_continuation :",
				"$$121 :",
				"opt_query_continuation : INTO identifier_inside_body $$121 query_body",
				"interactive_parsing : EVAL_STATEMENT_PARSER EOF",
				"interactive_parsing : EVAL_USING_DECLARATIONS_UNIT_PARSER using_directives opt_COMPLETE_COMPLETION",
				"$$122 :",
				"interactive_parsing : EVAL_STATEMENT_PARSER $$122 interactive_statement_list opt_COMPLETE_COMPLETION",
				"interactive_parsing : EVAL_COMPILATION_UNIT_PARSER interactive_compilation_unit",
				"interactive_compilation_unit : opt_extern_alias_directives opt_using_directives",
				"interactive_compilation_unit : opt_extern_alias_directives opt_using_directives namespace_or_type_declarations",
				"opt_COMPLETE_COMPLETION :",
				"opt_COMPLETE_COMPLETION : COMPLETE_COMPLETION",
				"close_brace_or_complete_completion : CLOSE_BRACE",
				"close_brace_or_complete_completion : COMPLETE_COMPLETION",
				"documentation_parsing : DOC_SEE doc_cref",
				"doc_cref : doc_type_declaration_name opt_doc_method_sig",
				"doc_cref : builtin_types opt_doc_method_sig",
				"doc_cref : VOID opt_doc_method_sig",
				"doc_cref : builtin_types DOT IDENTIFIER opt_doc_method_sig",
				"doc_cref : doc_type_declaration_name DOT THIS",
				"$$123 :",
				"doc_cref : doc_type_declaration_name DOT THIS OPEN_BRACKET $$123 opt_doc_parameters CLOSE_BRACKET",
				"doc_cref : EXPLICIT OPERATOR type opt_doc_method_sig",
				"doc_cref : IMPLICIT OPERATOR type opt_doc_method_sig",
				"doc_cref : OPERATOR overloadable_operator opt_doc_method_sig",
				"doc_type_declaration_name : type_declaration_name",
				"doc_type_declaration_name : doc_type_declaration_name DOT type_declaration_name",
				"opt_doc_method_sig :",
				"$$124 :",
				"opt_doc_method_sig : OPEN_PARENS $$124 opt_doc_parameters CLOSE_PARENS",
				"opt_doc_parameters :",
				"opt_doc_parameters : doc_parameters",
				"doc_parameters : doc_parameter",
				"doc_parameters : doc_parameters COMMA doc_parameter",
				"doc_parameter : opt_parameter_modifier parameter_type"
			};
		}

		// Token: 0x0200039B RID: 923
		private class OperatorDeclaration
		{
			// Token: 0x060026DC RID: 9948 RVA: 0x000BA9C7 File Offset: 0x000B8BC7
			public OperatorDeclaration(Operator.OpType op, FullNamedExpression ret_type, Location location)
			{
				this.optype = op;
				this.ret_type = ret_type;
				this.location = location;
			}

			// Token: 0x04000FB0 RID: 4016
			public readonly Operator.OpType optype;

			// Token: 0x04000FB1 RID: 4017
			public readonly FullNamedExpression ret_type;

			// Token: 0x04000FB2 RID: 4018
			public readonly Location location;
		}
	}
}
