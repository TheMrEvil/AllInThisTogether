using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using Mono.CSharp.yyParser;

namespace Mono.CSharp
{
	// Token: 0x02000189 RID: 393
	public class Tokenizer : yyInput
	{
		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x060014E6 RID: 5350 RVA: 0x00060F64 File Offset: 0x0005F164
		// (set) Token: 0x060014E7 RID: 5351 RVA: 0x00060F6C File Offset: 0x0005F16C
		public bool PropertyParsing
		{
			get
			{
				return this.handle_get_set;
			}
			set
			{
				this.handle_get_set = value;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x060014E8 RID: 5352 RVA: 0x00060F75 File Offset: 0x0005F175
		// (set) Token: 0x060014E9 RID: 5353 RVA: 0x00060F7D File Offset: 0x0005F17D
		public bool EventParsing
		{
			get
			{
				return this.handle_remove_add;
			}
			set
			{
				this.handle_remove_add = value;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x060014EA RID: 5354 RVA: 0x00060F86 File Offset: 0x0005F186
		// (set) Token: 0x060014EB RID: 5355 RVA: 0x00060F8E File Offset: 0x0005F18E
		public bool ConstraintsParsing
		{
			get
			{
				return this.handle_where;
			}
			set
			{
				this.handle_where = value;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x060014EC RID: 5356 RVA: 0x00060F97 File Offset: 0x0005F197
		// (set) Token: 0x060014ED RID: 5357 RVA: 0x00060F9F File Offset: 0x0005F19F
		public XmlCommentState doc_state
		{
			get
			{
				return this.xml_doc_state;
			}
			set
			{
				if (value == XmlCommentState.Allowed)
				{
					this.check_incorrect_doc_comment();
					this.reset_doc_comment();
				}
				this.xml_doc_state = value;
			}
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x00060FB7 File Offset: 0x0005F1B7
		private void AddEscapedIdentifier(Location loc)
		{
			if (this.escaped_identifiers == null)
			{
				this.escaped_identifiers = new List<Location>();
			}
			this.escaped_identifiers.Add(loc);
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x00060FD8 File Offset: 0x0005F1D8
		public bool IsEscapedIdentifier(ATypeNameExpression name)
		{
			return this.escaped_identifiers != null && this.escaped_identifiers.Contains(name.Location);
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x060014F0 RID: 5360 RVA: 0x00060FF5 File Offset: 0x0005F1F5
		public int Line
		{
			get
			{
				return this.ref_line;
			}
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x00061000 File Offset: 0x0005F200
		public Tokenizer(SeekableStreamReader input, CompilationSourceFile file, ParserSession session, Report report)
		{
			this.source_file = file;
			this.context = file.Compiler;
			this.current_source = file.SourceFile;
			this.identifiers = session.Identifiers;
			this.id_builder = session.IDBuilder;
			this.number_builder = session.NumberBuilder;
			this.ltb = new Tokenizer.LocatedTokenBuffer(session.LocatedTokens);
			this.Report = report;
			this.reader = input;
			this.putback_char = -1;
			this.xml_comment_buffer = new StringBuilder();
			this.doc_processing = (this.context.Settings.DocumentationFile != null);
			this.tab_size = this.context.Settings.TabSize;
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x000610DE File Offset: 0x0005F2DE
		public void PushPosition()
		{
			this.position_stack.Push(new Tokenizer.Position(this));
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x000610F4 File Offset: 0x0005F2F4
		public void PopPosition()
		{
			Tokenizer.Position position = this.position_stack.Pop();
			this.reader.Position = position.position;
			this.ref_line = position.ref_line;
			this.line = position.line;
			this.col = position.col;
			this.hidden_block_start = position.hidden;
			this.putback_char = position.putback_char;
			this.previous_col = position.previous_col;
			this.ifstack = position.ifstack;
			this.parsing_generic_less_than = position.parsing_generic_less_than;
			this.parsing_string_interpolation = position.parsing_string_interpolation;
			this.parsing_string_interpolation_quoted = position.parsing_string_interpolation_quoted;
			this.current_token = position.current_token;
			this.val = position.val;
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x000611AE File Offset: 0x0005F3AE
		public void DiscardPosition()
		{
			this.position_stack.Pop();
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x000611BC File Offset: 0x0005F3BC
		private static void AddKeyword(string kw, int token)
		{
			Tokenizer.keyword_strings.Add(kw);
			Tokenizer.AddKeyword<int>(Tokenizer.keywords, kw, token);
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x000611D6 File Offset: 0x0005F3D6
		private static void AddPreprocessorKeyword(string kw, Tokenizer.PreprocessorDirective directive)
		{
			Tokenizer.AddKeyword<Tokenizer.PreprocessorDirective>(Tokenizer.keywords_preprocessor, kw, directive);
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x000611E4 File Offset: 0x0005F3E4
		private static void AddKeyword<T>(Tokenizer.KeywordEntry<T>[][] keywords, string kw, T token)
		{
			int length = kw.Length;
			if (keywords[length] == null)
			{
				keywords[length] = new Tokenizer.KeywordEntry<T>[28];
			}
			int num = (int)(kw[0] - '_');
			Tokenizer.KeywordEntry<T> keywordEntry = keywords[length][num];
			if (keywordEntry == null)
			{
				keywords[length][num] = new Tokenizer.KeywordEntry<T>(kw, token);
				return;
			}
			while (keywordEntry.Next != null)
			{
				keywordEntry = keywordEntry.Next;
			}
			keywordEntry.Next = new Tokenizer.KeywordEntry<T>(kw, token);
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x00061244 File Offset: 0x0005F444
		static Tokenizer()
		{
			Tokenizer.keyword_strings = new HashSet<string>();
			Tokenizer.keywords = new Tokenizer.KeywordEntry<int>[11][];
			Tokenizer.AddKeyword("__arglist", 341);
			Tokenizer.AddKeyword("__makeref", 361);
			Tokenizer.AddKeyword("__reftype", 360);
			Tokenizer.AddKeyword("__refvalue", 359);
			Tokenizer.AddKeyword("abstract", 261);
			Tokenizer.AddKeyword("as", 262);
			Tokenizer.AddKeyword("add", 263);
			Tokenizer.AddKeyword("base", 264);
			Tokenizer.AddKeyword("bool", 265);
			Tokenizer.AddKeyword("break", 266);
			Tokenizer.AddKeyword("byte", 267);
			Tokenizer.AddKeyword("case", 268);
			Tokenizer.AddKeyword("catch", 269);
			Tokenizer.AddKeyword("char", 270);
			Tokenizer.AddKeyword("checked", 271);
			Tokenizer.AddKeyword("class", 272);
			Tokenizer.AddKeyword("const", 273);
			Tokenizer.AddKeyword("continue", 274);
			Tokenizer.AddKeyword("decimal", 275);
			Tokenizer.AddKeyword("default", 276);
			Tokenizer.AddKeyword("delegate", 277);
			Tokenizer.AddKeyword("do", 278);
			Tokenizer.AddKeyword("double", 279);
			Tokenizer.AddKeyword("else", 280);
			Tokenizer.AddKeyword("enum", 281);
			Tokenizer.AddKeyword("event", 282);
			Tokenizer.AddKeyword("explicit", 283);
			Tokenizer.AddKeyword("extern", 284);
			Tokenizer.AddKeyword("false", 285);
			Tokenizer.AddKeyword("finally", 286);
			Tokenizer.AddKeyword("fixed", 287);
			Tokenizer.AddKeyword("float", 288);
			Tokenizer.AddKeyword("for", 289);
			Tokenizer.AddKeyword("foreach", 290);
			Tokenizer.AddKeyword("goto", 291);
			Tokenizer.AddKeyword("get", 368);
			Tokenizer.AddKeyword("if", 292);
			Tokenizer.AddKeyword("implicit", 293);
			Tokenizer.AddKeyword("in", 294);
			Tokenizer.AddKeyword("int", 295);
			Tokenizer.AddKeyword("interface", 296);
			Tokenizer.AddKeyword("internal", 297);
			Tokenizer.AddKeyword("is", 298);
			Tokenizer.AddKeyword("lock", 299);
			Tokenizer.AddKeyword("long", 300);
			Tokenizer.AddKeyword("namespace", 301);
			Tokenizer.AddKeyword("new", 302);
			Tokenizer.AddKeyword("null", 303);
			Tokenizer.AddKeyword("object", 304);
			Tokenizer.AddKeyword("operator", 305);
			Tokenizer.AddKeyword("out", 306);
			Tokenizer.AddKeyword("override", 307);
			Tokenizer.AddKeyword("params", 308);
			Tokenizer.AddKeyword("private", 309);
			Tokenizer.AddKeyword("protected", 310);
			Tokenizer.AddKeyword("public", 311);
			Tokenizer.AddKeyword("readonly", 312);
			Tokenizer.AddKeyword("ref", 313);
			Tokenizer.AddKeyword("remove", 315);
			Tokenizer.AddKeyword("return", 314);
			Tokenizer.AddKeyword("sbyte", 316);
			Tokenizer.AddKeyword("sealed", 317);
			Tokenizer.AddKeyword("set", 369);
			Tokenizer.AddKeyword("short", 318);
			Tokenizer.AddKeyword("sizeof", 319);
			Tokenizer.AddKeyword("stackalloc", 320);
			Tokenizer.AddKeyword("static", 321);
			Tokenizer.AddKeyword("string", 322);
			Tokenizer.AddKeyword("struct", 323);
			Tokenizer.AddKeyword("switch", 324);
			Tokenizer.AddKeyword("this", 325);
			Tokenizer.AddKeyword("throw", 326);
			Tokenizer.AddKeyword("true", 327);
			Tokenizer.AddKeyword("try", 328);
			Tokenizer.AddKeyword("typeof", 329);
			Tokenizer.AddKeyword("uint", 330);
			Tokenizer.AddKeyword("ulong", 331);
			Tokenizer.AddKeyword("unchecked", 332);
			Tokenizer.AddKeyword("unsafe", 333);
			Tokenizer.AddKeyword("ushort", 334);
			Tokenizer.AddKeyword("using", 335);
			Tokenizer.AddKeyword("virtual", 336);
			Tokenizer.AddKeyword("void", 337);
			Tokenizer.AddKeyword("volatile", 338);
			Tokenizer.AddKeyword("while", 340);
			Tokenizer.AddKeyword("partial", 342);
			Tokenizer.AddKeyword("where", 339);
			Tokenizer.AddKeyword("from", 344);
			Tokenizer.AddKeyword("join", 346);
			Tokenizer.AddKeyword("on", 347);
			Tokenizer.AddKeyword("equals", 348);
			Tokenizer.AddKeyword("select", 349);
			Tokenizer.AddKeyword("group", 350);
			Tokenizer.AddKeyword("by", 351);
			Tokenizer.AddKeyword("let", 352);
			Tokenizer.AddKeyword("orderby", 353);
			Tokenizer.AddKeyword("ascending", 354);
			Tokenizer.AddKeyword("descending", 355);
			Tokenizer.AddKeyword("into", 356);
			Tokenizer.AddKeyword("async", 362);
			Tokenizer.AddKeyword("await", 363);
			Tokenizer.AddKeyword("when", 365);
			Tokenizer.keywords_preprocessor = new Tokenizer.KeywordEntry<Tokenizer.PreprocessorDirective>[10][];
			Tokenizer.AddPreprocessorKeyword("region", Tokenizer.PreprocessorDirective.Region);
			Tokenizer.AddPreprocessorKeyword("endregion", Tokenizer.PreprocessorDirective.Endregion);
			Tokenizer.AddPreprocessorKeyword("if", Tokenizer.PreprocessorDirective.If);
			Tokenizer.AddPreprocessorKeyword("endif", Tokenizer.PreprocessorDirective.Endif);
			Tokenizer.AddPreprocessorKeyword("elif", Tokenizer.PreprocessorDirective.Elif);
			Tokenizer.AddPreprocessorKeyword("else", Tokenizer.PreprocessorDirective.Else);
			Tokenizer.AddPreprocessorKeyword("define", Tokenizer.PreprocessorDirective.Define);
			Tokenizer.AddPreprocessorKeyword("undef", Tokenizer.PreprocessorDirective.Undef);
			Tokenizer.AddPreprocessorKeyword("error", Tokenizer.PreprocessorDirective.Error);
			Tokenizer.AddPreprocessorKeyword("warning", Tokenizer.PreprocessorDirective.Warning);
			Tokenizer.AddPreprocessorKeyword("pragma", Tokenizer.PreprocessorDirective.Pragma);
			Tokenizer.AddPreprocessorKeyword("line", Tokenizer.PreprocessorDirective.Line);
			Tokenizer.csharp_format_info = NumberFormatInfo.InvariantInfo;
			Tokenizer.styles = NumberStyles.Float;
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x00061990 File Offset: 0x0005FB90
		private int GetKeyword(char[] id, int id_len)
		{
			if (id_len >= Tokenizer.keywords.Length || Tokenizer.keywords[id_len] == null)
			{
				return -1;
			}
			int num = (int)(id[0] - '_');
			if (num > 27)
			{
				return -1;
			}
			Tokenizer.KeywordEntry<int> keywordEntry = Tokenizer.keywords[id_len][num];
			if (keywordEntry == null)
			{
				return -1;
			}
			int num2;
			do
			{
				num2 = keywordEntry.Token;
				for (int i = 1; i < id_len; i++)
				{
					if (id[i] != keywordEntry.Value[i])
					{
						num2 = 0;
						keywordEntry = keywordEntry.Next;
						break;
					}
				}
			}
			while (num2 == 0 && keywordEntry != null);
			if (num2 == 0)
			{
				return -1;
			}
			if (num2 > 284)
			{
				if (num2 != 301)
				{
					if (num2 == 315)
					{
						goto IL_161;
					}
					switch (num2)
					{
					case 335:
						break;
					case 336:
					case 337:
					case 338:
					case 340:
					case 341:
					case 343:
					case 345:
					case 357:
					case 358:
					case 359:
					case 360:
					case 361:
					case 364:
					case 366:
					case 367:
						return num2;
					case 339:
						if ((!this.handle_where || this.current_token == 379) && !this.query_parsing)
						{
							return -1;
						}
						return num2;
					case 342:
					{
						if (this.parsing_block > 0)
						{
							return -1;
						}
						this.PushPosition();
						int num3 = this.token();
						bool flag = num3 == 272 || num3 == 323 || num3 == 296 || num3 == 337;
						this.PopPosition();
						if (flag)
						{
							if (num3 == 337)
							{
								if (this.context.Settings.Version <= LanguageVersion.ISO_2)
								{
									this.Report.FeatureIsNotAvailable(this.context, this.Location, "partial methods");
								}
							}
							else if (this.context.Settings.Version == LanguageVersion.ISO_1)
							{
								this.Report.FeatureIsNotAvailable(this.context, this.Location, "partial types");
							}
							return num2;
						}
						if (num3 < 370)
						{
							this.Report.Error(267, this.Location, "The `partial' modifier can be used only immediately before `class', `struct', `interface', or `void' keyword");
							return this.token();
						}
						this.id_builder[0] = 'p';
						this.id_builder[1] = 'a';
						this.id_builder[2] = 'r';
						this.id_builder[3] = 't';
						this.id_builder[4] = 'i';
						this.id_builder[5] = 'a';
						this.id_builder[6] = 'l';
						return -1;
					}
					case 344:
					{
						if (this.query_parsing)
						{
							return num2;
						}
						if (this.lambda_arguments_parsing || this.parsing_block == 0)
						{
							return -1;
						}
						this.PushPosition();
						this.parsing_generic_less_than = 1;
						int num4 = this.xtoken();
						if (num4 <= 295)
						{
							if (num4 <= 270)
							{
								if (num4 != 265 && num4 != 267 && num4 != 270)
								{
									goto IL_376;
								}
							}
							else if (num4 <= 279)
							{
								if (num4 != 275 && num4 != 279)
								{
									goto IL_376;
								}
							}
							else if (num4 != 288 && num4 != 295)
							{
								goto IL_376;
							}
						}
						else if (num4 <= 322)
						{
							if (num4 != 300 && num4 != 304 && num4 != 322)
							{
								goto IL_376;
							}
						}
						else if (num4 <= 331)
						{
							if (num4 != 330 && num4 != 331)
							{
								goto IL_376;
							}
						}
						else
						{
							if (num4 == 337)
							{
								Expression.Error_VoidInvalidInTheContext(this.Location, this.Report);
								goto IL_3A6;
							}
							if (num4 != 422)
							{
								goto IL_376;
							}
						}
						int num3 = this.xtoken();
						if (num3 != 380 && num3 != 378 && num3 != 348 && num3 != 385)
						{
							num2 = 345;
							this.query_parsing = true;
							if (this.context.Settings.Version <= LanguageVersion.ISO_2)
							{
								this.Report.FeatureIsNotAvailable(this.context, this.Location, "query expressions");
								goto IL_3A6;
							}
							goto IL_3A6;
						}
						IL_376:
						this.PopPosition();
						this.id_builder[0] = 'f';
						this.id_builder[1] = 'r';
						this.id_builder[2] = 'o';
						this.id_builder[3] = 'm';
						return -1;
						IL_3A6:
						this.PopPosition();
						return num2;
					}
					case 346:
					case 347:
					case 348:
					case 349:
					case 350:
					case 351:
					case 352:
					case 353:
					case 354:
					case 355:
					case 356:
						if (!this.query_parsing)
						{
							return -1;
						}
						return num2;
					case 362:
						if (this.parsing_modifiers)
						{
							if (this.parsing_attribute_section || this.peek_token() == 375)
							{
								num2 = -1;
							}
						}
						else if (this.parsing_block > 0)
						{
							int num4 = this.peek_token();
							if (num4 != 277)
							{
								if (num4 != 422)
								{
									if (num4 == 423)
									{
										goto IL_5B8;
									}
								}
								else
								{
									this.PushPosition();
									this.xtoken();
									if (this.xtoken() == 343)
									{
										this.PopPosition();
										goto IL_5B8;
									}
									this.PopPosition();
								}
								this.id_builder[0] = 'a';
								this.id_builder[1] = 's';
								this.id_builder[2] = 'y';
								this.id_builder[3] = 'n';
								this.id_builder[4] = 'c';
								num2 = -1;
							}
						}
						else
						{
							num2 = -1;
						}
						IL_5B8:
						if (num2 == 362 && this.context.Settings.Version <= LanguageVersion.V_4)
						{
							this.Report.FeatureIsNotAvailable(this.context, this.Location, "asynchronous functions");
							return num2;
						}
						return num2;
					case 363:
						if (this.parsing_block == 0)
						{
							return -1;
						}
						return num2;
					case 365:
						if (this.current_token != 269 && !this.parsing_catch_when)
						{
							return -1;
						}
						return num2;
					case 368:
					case 369:
						if (!this.handle_get_set)
						{
							return -1;
						}
						return num2;
					default:
						return num2;
					}
				}
				this.check_incorrect_doc_comment();
				this.parsing_modifiers = false;
				return num2;
			}
			if (num2 != 263)
			{
				if (num2 != 276)
				{
					if (num2 != 284)
					{
						return num2;
					}
					if (this.parsing_declaration == 0)
					{
						return 358;
					}
					return num2;
				}
				else
				{
					if (this.peek_token() == 379)
					{
						this.token();
						return 426;
					}
					return num2;
				}
			}
			IL_161:
			if (!this.handle_remove_add)
			{
				num2 = -1;
			}
			return num2;
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x00061F9C File Offset: 0x0006019C
		private static Tokenizer.PreprocessorDirective GetPreprocessorDirective(char[] id, int id_len)
		{
			if (id_len >= Tokenizer.keywords_preprocessor.Length || Tokenizer.keywords_preprocessor[id_len] == null)
			{
				return Tokenizer.PreprocessorDirective.Invalid;
			}
			int num = (int)(id[0] - '_');
			if (num > 27)
			{
				return Tokenizer.PreprocessorDirective.Invalid;
			}
			Tokenizer.KeywordEntry<Tokenizer.PreprocessorDirective> keywordEntry = Tokenizer.keywords_preprocessor[id_len][num];
			if (keywordEntry == null)
			{
				return Tokenizer.PreprocessorDirective.Invalid;
			}
			Tokenizer.PreprocessorDirective preprocessorDirective;
			do
			{
				preprocessorDirective = keywordEntry.Token;
				for (int i = 1; i < id_len; i++)
				{
					if (id[i] != keywordEntry.Value[i])
					{
						preprocessorDirective = Tokenizer.PreprocessorDirective.Invalid;
						keywordEntry = keywordEntry.Next;
						break;
					}
				}
			}
			while (preprocessorDirective == Tokenizer.PreprocessorDirective.Invalid && keywordEntry != null);
			return preprocessorDirective;
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x060014FB RID: 5371 RVA: 0x0006200F File Offset: 0x0006020F
		public Location Location
		{
			get
			{
				return new Location(this.current_source, this.ref_line, this.col);
			}
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x00062028 File Offset: 0x00060228
		private static bool is_identifier_start_character(int c)
		{
			return (c >= 97 && c <= 122) || (c >= 65 && c <= 90) || c == 95 || (c >= 128 && Tokenizer.is_identifier_start_character_slow_part((char)c));
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x00062056 File Offset: 0x00060256
		private static bool is_identifier_part_character(char c)
		{
			return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c == '_' || (c >= '0' && c <= '9')) || (c >= '\u0080' && Tokenizer.is_identifier_part_character_slow_part(c));
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x00062094 File Offset: 0x00060294
		private static bool is_identifier_start_character_slow_part(char c)
		{
			switch (char.GetUnicodeCategory(c))
			{
			case UnicodeCategory.UppercaseLetter:
			case UnicodeCategory.LowercaseLetter:
			case UnicodeCategory.TitlecaseLetter:
			case UnicodeCategory.ModifierLetter:
			case UnicodeCategory.OtherLetter:
			case UnicodeCategory.LetterNumber:
				return true;
			}
			return false;
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x000620DC File Offset: 0x000602DC
		private static bool is_identifier_part_character_slow_part(char c)
		{
			switch (char.GetUnicodeCategory(c))
			{
			case UnicodeCategory.UppercaseLetter:
			case UnicodeCategory.LowercaseLetter:
			case UnicodeCategory.TitlecaseLetter:
			case UnicodeCategory.ModifierLetter:
			case UnicodeCategory.OtherLetter:
			case UnicodeCategory.NonSpacingMark:
			case UnicodeCategory.SpacingCombiningMark:
			case UnicodeCategory.DecimalDigitNumber:
			case UnicodeCategory.LetterNumber:
			case UnicodeCategory.ConnectorPunctuation:
				return true;
			case UnicodeCategory.Format:
				return c != '﻿';
			}
			return false;
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x00062153 File Offset: 0x00060353
		public static bool IsKeyword(string s)
		{
			return Tokenizer.keyword_strings.Contains(s);
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x00062160 File Offset: 0x00060360
		private int TokenizeOpenParens()
		{
			this.current_token = -1;
			int num = 0;
			bool flag = false;
			bool flag2 = false;
			for (;;)
			{
				int num2 = this.current_token;
				this.token();
				int num3 = this.current_token;
				if (num3 <= 313)
				{
					if (num3 <= 279)
					{
						if (num3 <= 267)
						{
							if (num3 != 265 && num3 != 267)
							{
								break;
							}
						}
						else if (num3 != 270 && num3 != 275 && num3 != 279)
						{
							break;
						}
					}
					else if (num3 <= 300)
					{
						if (num3 != 288 && num3 != 295 && num3 != 300)
						{
							break;
						}
					}
					else if (num3 != 304)
					{
						if (num3 != 306 && num3 != 313)
						{
							break;
						}
						flag = (flag2 = false);
						continue;
					}
				}
				else if (num3 <= 337)
				{
					if (num3 <= 318)
					{
						if (num3 != 316 && num3 != 318)
						{
							break;
						}
					}
					else if (num3 != 322)
					{
						switch (num3)
						{
						case 330:
						case 331:
						case 334:
							break;
						case 332:
						case 333:
							return 375;
						default:
							if (num3 != 337)
							{
								goto Block_22;
							}
							break;
						}
					}
				}
				else
				{
					if (num3 <= 378)
					{
						if (num3 == 357)
						{
							goto IL_412;
						}
						if (num3 != 363)
						{
							switch (num3)
							{
							case 373:
								goto IL_3F8;
							case 374:
								goto IL_409;
							case 376:
								goto IL_1D8;
							case 377:
								goto IL_369;
							case 378:
								if (num == 0)
								{
									num = 100;
									flag = (flag2 = false);
									continue;
								}
								continue;
							}
							break;
						}
						goto IL_384;
					}
					else
					{
						if (num3 == 390)
						{
							goto IL_412;
						}
						if (num3 != 395)
						{
							switch (num3)
							{
							case 418:
								goto IL_3F8;
							case 420:
								goto IL_409;
							case 422:
								goto IL_384;
							}
							break;
						}
					}
					IL_369:
					if (num2 != 422 && num2 != 420)
					{
						break;
					}
					continue;
					IL_384:
					if (num2 <= 377)
					{
						if (num2 != -1)
						{
							if (num2 != 377)
							{
								goto IL_3D0;
							}
							if (num == 0)
							{
								flag = false;
								flag2 = true;
								continue;
							}
							continue;
						}
					}
					else if (num2 != 378 && num2 != 395 && num2 != 418)
					{
						goto IL_3D0;
					}
					if (num == 0)
					{
						flag2 = true;
						continue;
					}
					continue;
					IL_3D0:
					flag = (flag2 = false);
					continue;
					IL_3F8:
					if (num++ == 0)
					{
						flag = true;
						continue;
					}
					continue;
					IL_409:
					num--;
					continue;
					IL_412:
					if (num == 0)
					{
						flag = true;
						continue;
					}
					continue;
				}
				if (num == 0)
				{
					flag = true;
				}
			}
			Block_22:
			return 375;
			IL_1D8:
			this.token();
			if (this.current_token == 343)
			{
				return 423;
			}
			if (!flag)
			{
				if (flag2)
				{
					int num3 = this.current_token;
					if (num3 <= 334)
					{
						if (num3 <= 288)
						{
							switch (num3)
							{
							case 264:
							case 265:
							case 267:
							case 270:
							case 271:
							case 275:
							case 276:
							case 277:
							case 279:
								break;
							case 266:
							case 268:
							case 269:
							case 272:
							case 273:
							case 274:
							case 278:
								return 375;
							default:
								switch (num3)
								{
								case 285:
								case 287:
								case 288:
									break;
								case 286:
									return 375;
								default:
									return 375;
								}
								break;
							}
						}
						else if (num3 != 295)
						{
							switch (num3)
							{
							case 300:
							case 302:
							case 303:
								break;
							case 301:
								return 375;
							default:
								switch (num3)
								{
								case 318:
								case 319:
								case 322:
								case 325:
								case 326:
								case 327:
								case 329:
								case 330:
								case 331:
								case 332:
								case 333:
								case 334:
									break;
								case 320:
								case 321:
								case 323:
								case 324:
								case 328:
									return 375;
								default:
									return 375;
								}
								break;
							}
						}
					}
					else if (num3 <= 381)
					{
						if (num3 != 363 && num3 != 375 && num3 != 381)
						{
							return 375;
						}
					}
					else if (num3 != 384 && num3 != 421 && num3 != 422)
					{
						return 375;
					}
					return 424;
				}
				return 375;
			}
			if (this.current_token == 380)
			{
				return 375;
			}
			return 424;
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x0006259C File Offset: 0x0006079C
		public static bool IsValidIdentifier(string s)
		{
			if (s == null || s.Length == 0)
			{
				return false;
			}
			if (!Tokenizer.is_identifier_start_character((int)s[0]))
			{
				return false;
			}
			for (int i = 1; i < s.Length; i++)
			{
				if (!Tokenizer.is_identifier_part_character(s[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x000625E8 File Offset: 0x000607E8
		private bool parse_less_than(ref int genericDimension)
		{
			int num;
			for (;;)
			{
				num = this.token();
				if (num == 373)
				{
					do
					{
						num = this.token();
						if (num == 257)
						{
							return true;
						}
					}
					while (num != 374);
					num = this.token();
				}
				else if (num == 294 || num == 306)
				{
					num = this.token();
				}
				if (num <= 300)
				{
					if (num <= 275)
					{
						if (num <= 267)
						{
							if (num != 265 && num != 267)
							{
								goto Block_7;
							}
						}
						else if (num != 270 && num != 275)
						{
							goto Block_9;
						}
					}
					else if (num <= 288)
					{
						if (num != 279 && num != 288)
						{
							goto Block_12;
						}
					}
					else
					{
						if (num == 294)
						{
							return true;
						}
						if (num != 295 && num != 300)
						{
							goto Block_15;
						}
					}
				}
				else if (num <= 322)
				{
					if (num <= 306)
					{
						if (num != 304)
						{
							goto Block_18;
						}
					}
					else if (num != 316 && num != 318 && num != 322)
					{
						goto Block_22;
					}
				}
				else if (num <= 337)
				{
					switch (num)
					{
					case 330:
					case 331:
					case 334:
						break;
					case 332:
					case 333:
						return false;
					default:
						if (num != 337)
						{
							goto Block_25;
						}
						break;
					}
				}
				else
				{
					if (num == 378)
					{
						break;
					}
					if (num == 420)
					{
						goto IL_176;
					}
					if (num != 422)
					{
						goto Block_28;
					}
				}
				for (;;)
				{
					num = this.token();
					if (num == 420)
					{
						return true;
					}
					if (num == 378 || num == 377 || num == 395)
					{
						break;
					}
					if (num != 357 && num != 390)
					{
						if (num == 418)
						{
							if (!this.parse_less_than(ref genericDimension))
							{
								return false;
							}
						}
						else
						{
							if (num != 373)
							{
								return false;
							}
							for (;;)
							{
								num = this.token();
								if (num == 374)
								{
									break;
								}
								if (num != 378)
								{
									return false;
								}
							}
						}
					}
				}
			}
			do
			{
				genericDimension++;
				num = this.token();
			}
			while (num == 378);
			if (num == 420)
			{
				genericDimension++;
				return true;
			}
			return false;
			Block_7:
			Block_9:
			Block_12:
			Block_15:
			return false;
			Block_18:
			if (num == 306)
			{
				return true;
			}
			Block_22:
			Block_25:
			Block_28:
			return false;
			IL_176:
			genericDimension = 1;
			return true;
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x00062812 File Offset: 0x00060A12
		public int peek_token()
		{
			this.PushPosition();
			int result = this.token();
			this.PopPosition();
			return result;
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x00062828 File Offset: 0x00060A28
		private int TokenizePossibleNullableType()
		{
			if (this.parsing_block == 0 || this.parsing_type > 0)
			{
				return 357;
			}
			int num = this.peek_char();
			if (num == 63)
			{
				int @char = this.get_char();
				return 417;
			}
			if (num == 46)
			{
				return 364;
			}
			if (num != 32)
			{
				if (num == 44 || num == 59 || num == 62)
				{
					return 357;
				}
				if (num == 42 || (num >= 48 && num <= 57))
				{
					return 394;
				}
			}
			this.PushPosition();
			this.current_token = 258;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = this.xtoken();
			int num6;
			if (num5 > 325)
			{
				if (num5 <= 380)
				{
					if (num5 == 327)
					{
						goto IL_182;
					}
					switch (num5)
					{
					case 366:
						goto IL_182;
					case 367:
					case 368:
					case 369:
					case 370:
					case 371:
					case 372:
					case 374:
						goto IL_1A2;
					case 373:
					case 376:
					case 378:
					case 379:
					case 380:
						goto IL_18A;
					case 375:
						goto IL_192;
					case 377:
						break;
					default:
						goto IL_1A2;
					}
				}
				else
				{
					if (num5 == 394)
					{
						goto IL_18A;
					}
					switch (num5)
					{
					case 417:
					case 420:
						goto IL_18A;
					case 418:
					case 419:
					case 425:
						num6 = -1;
						num3++;
						goto IL_1A4;
					case 421:
						goto IL_182;
					case 422:
					case 426:
						goto IL_1A2;
					case 423:
					case 424:
						goto IL_192;
					case 427:
						break;
					default:
						goto IL_1A2;
					}
				}
				num6 = 364;
				goto IL_1A4;
				IL_18A:
				num6 = 357;
				goto IL_1A4;
				IL_192:
				num6 = -1;
				num2++;
				goto IL_1A4;
			}
			if (num5 <= 302)
			{
				if (num5 != 285 && num5 != 302)
				{
					goto IL_1A2;
				}
			}
			else if (num5 != 303 && num5 != 325)
			{
				goto IL_1A2;
			}
			IL_182:
			num6 = 394;
			goto IL_1A4;
			IL_1A2:
			num6 = -1;
			IL_1A4:
			if (num6 == -1)
			{
				int num7 = this.xtoken();
				if (num7 != 294)
				{
					switch (num7)
					{
					case 371:
					case 378:
					case 380:
						goto IL_228;
					case 372:
					case 374:
					case 377:
						goto IL_256;
					case 373:
						goto IL_244;
					case 375:
						break;
					case 376:
						num2--;
						goto IL_256;
					case 379:
						num6 = 394;
						goto IL_3A2;
					default:
						switch (num7)
						{
						case 418:
						case 419:
						case 425:
							num3++;
							goto IL_256;
						case 420:
						case 421:
						case 422:
						case 426:
							goto IL_256;
						case 423:
						case 424:
							break;
						case 427:
							goto IL_244;
						default:
							goto IL_256;
						}
						break;
					}
					num2++;
					goto IL_256;
					IL_244:
					num4++;
					IL_256:
					int num8 = 1;
					int num9 = 0;
					int num10 = 0;
					int num11;
					while ((num11 = this.xtoken()) != 257)
					{
						switch (num11)
						{
						case 371:
							num10++;
							continue;
						case 372:
							num10--;
							continue;
						case 373:
							goto IL_2E8;
						case 374:
							num4--;
							continue;
						case 375:
							break;
						case 376:
							if (num2 > 0)
							{
								num2--;
								continue;
							}
							this.PopPosition();
							return 357;
						default:
							switch (num11)
							{
							case 418:
							case 419:
							case 425:
								num3++;
								continue;
							case 420:
								if (num3 > 0)
								{
									num3--;
									continue;
								}
								this.PopPosition();
								return 357;
							case 423:
							case 424:
								goto IL_2CB;
							case 427:
								goto IL_2E8;
							}
							if (num10 != 0)
							{
								continue;
							}
							if (num11 == 380)
							{
								goto IL_38B;
							}
							if (num2 != 0)
							{
								continue;
							}
							if (num11 == 378)
							{
								if (num3 == 0 && num4 == 0)
								{
									this.PopPosition();
									return 357;
								}
								continue;
							}
							else if (num11 == 379)
							{
								if (++num9 == num8)
								{
									goto IL_38B;
								}
								continue;
							}
							else
							{
								if (num11 == 394)
								{
									num8++;
									continue;
								}
								continue;
							}
							break;
						}
						IL_2CB:
						num2++;
						continue;
						IL_2E8:
						num4++;
					}
					IL_38B:
					num6 = ((num9 != num8 && num10 == 0) ? 357 : 394);
					goto IL_3A2;
				}
				IL_228:
				num6 = 357;
			}
			IL_3A2:
			this.PopPosition();
			return num6;
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x00062BE0 File Offset: 0x00060DE0
		private bool decimal_digits(int c)
		{
			bool result = false;
			if (c != -1)
			{
				if (this.number_pos == 512)
				{
					this.Error_NumericConstantTooLong();
				}
				char[] array = this.number_builder;
				int num = this.number_pos;
				this.number_pos = num + 1;
				array[num] = (ushort)c;
			}
			int num2;
			while ((num2 = this.peek_char2()) != -1 && num2 >= 48 && num2 <= 57)
			{
				if (this.number_pos == 512)
				{
					this.Error_NumericConstantTooLong();
				}
				char[] array2 = this.number_builder;
				int num = this.number_pos;
				this.number_pos = num + 1;
				array2[num] = (ushort)num2;
				int @char = this.get_char();
				result = true;
			}
			return result;
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x00062C6E File Offset: 0x00060E6E
		private static bool is_hex(int e)
		{
			return (e >= 48 && e <= 57) || (e >= 65 && e <= 70) || (e >= 97 && e <= 102);
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x00062C95 File Offset: 0x00060E95
		private static TypeCode real_type_suffix(int c)
		{
			if (c <= 77)
			{
				if (c == 68)
				{
					return TypeCode.Double;
				}
				if (c != 70)
				{
					if (c != 77)
					{
						return TypeCode.Empty;
					}
					return TypeCode.Decimal;
				}
			}
			else
			{
				if (c == 100)
				{
					return TypeCode.Double;
				}
				if (c != 102)
				{
					if (c != 109)
					{
						return TypeCode.Empty;
					}
					return TypeCode.Decimal;
				}
			}
			return TypeCode.Single;
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x00062CC8 File Offset: 0x00060EC8
		private ILiteralConstant integer_type_suffix(ulong ul, int c, Location loc)
		{
			bool flag = false;
			bool flag2 = false;
			if (c != -1)
			{
				bool flag3 = true;
				for (;;)
				{
					if (c <= 85)
					{
						if (c == 76)
						{
							goto IL_51;
						}
						if (c != 85)
						{
							goto IL_61;
						}
						goto IL_25;
					}
					else if (c != 108)
					{
						if (c == 117)
						{
							goto IL_25;
						}
						goto IL_61;
					}
					else
					{
						if (!flag)
						{
							this.Report.Warning(78, 4, this.Location, "The `l' suffix is easily confused with the digit `1' (use `L' for clarity)");
							goto IL_51;
						}
						goto IL_51;
					}
					IL_63:
					c = this.peek_char();
					if (!flag3)
					{
						break;
					}
					continue;
					IL_25:
					if (flag)
					{
						flag3 = false;
					}
					flag = true;
					int @char = this.get_char();
					goto IL_63;
					IL_51:
					if (flag2)
					{
						flag3 = false;
					}
					flag2 = true;
					int char2 = this.get_char();
					goto IL_63;
					IL_61:
					flag3 = false;
					goto IL_63;
				}
			}
			if (flag2 && flag)
			{
				return new ULongLiteral(this.context.BuiltinTypes, ul, loc);
			}
			if (flag)
			{
				if ((ul & 18446744069414584320UL) == 0UL)
				{
					return new UIntLiteral(this.context.BuiltinTypes, (uint)ul, loc);
				}
				return new ULongLiteral(this.context.BuiltinTypes, ul, loc);
			}
			else if (flag2)
			{
				if ((ul & 9223372036854775808UL) != 0UL)
				{
					return new ULongLiteral(this.context.BuiltinTypes, ul, loc);
				}
				return new LongLiteral(this.context.BuiltinTypes, (long)ul, loc);
			}
			else if ((ul & 18446744069414584320UL) == 0UL)
			{
				uint num = (uint)ul;
				if ((num & 2147483648U) != 0U)
				{
					return new UIntLiteral(this.context.BuiltinTypes, num, loc);
				}
				return new IntLiteral(this.context.BuiltinTypes, (int)num, loc);
			}
			else
			{
				if ((ul & 9223372036854775808UL) != 0UL)
				{
					return new ULongLiteral(this.context.BuiltinTypes, ul, loc);
				}
				return new LongLiteral(this.context.BuiltinTypes, (long)ul, loc);
			}
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x00062E3C File Offset: 0x0006103C
		private ILiteralConstant adjust_int(int c, Location loc)
		{
			ILiteralConstant result;
			try
			{
				if (this.number_pos > 9)
				{
					ulong num = (ulong)(this.number_builder[0] - '0');
					for (int i = 1; i < this.number_pos; i++)
					{
						num = checked(num * 10UL + unchecked((ulong)(checked((uint)(this.number_builder[i] - '0')))));
					}
					result = this.integer_type_suffix(num, c, loc);
				}
				else
				{
					uint num2 = (uint)(this.number_builder[0] - '0');
					for (int j = 1; j < this.number_pos; j++)
					{
						num2 = checked(num2 * 10U + (uint)(this.number_builder[j] - '0'));
					}
					result = this.integer_type_suffix((ulong)num2, c, loc);
				}
			}
			catch (OverflowException)
			{
				this.Error_NumericConstantTooLong();
				result = new IntLiteral(this.context.BuiltinTypes, 0, loc);
			}
			catch (FormatException)
			{
				this.Report.Error(1013, this.Location, "Invalid number");
				result = new IntLiteral(this.context.BuiltinTypes, 0, loc);
			}
			return result;
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x00062F40 File Offset: 0x00061140
		private ILiteralConstant adjust_real(TypeCode t, Location loc)
		{
			string s = new string(this.number_builder, 0, this.number_pos);
			if (t != TypeCode.Single)
			{
				if (t != TypeCode.Decimal)
				{
					goto IL_E7;
				}
				try
				{
					return new DecimalLiteral(this.context.BuiltinTypes, decimal.Parse(s, Tokenizer.styles, Tokenizer.csharp_format_info), loc);
				}
				catch (OverflowException)
				{
					this.Report.Error(594, this.Location, "Floating-point constant is outside the range of type `{0}'", "decimal");
					return new DecimalLiteral(this.context.BuiltinTypes, 0m, loc);
				}
			}
			try
			{
				return new FloatLiteral(this.context.BuiltinTypes, float.Parse(s, Tokenizer.styles, Tokenizer.csharp_format_info), loc);
			}
			catch (OverflowException)
			{
				this.Report.Error(594, this.Location, "Floating-point constant is outside the range of type `{0}'", "float");
				return new FloatLiteral(this.context.BuiltinTypes, 0f, loc);
			}
			IL_E7:
			ILiteralConstant result;
			try
			{
				result = new DoubleLiteral(this.context.BuiltinTypes, double.Parse(s, Tokenizer.styles, Tokenizer.csharp_format_info), loc);
			}
			catch (OverflowException)
			{
				this.Report.Error(594, loc, "Floating-point constant is outside the range of type `{0}'", "double");
				result = new DoubleLiteral(this.context.BuiltinTypes, 0.0, loc);
			}
			return result;
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x000630BC File Offset: 0x000612BC
		private ILiteralConstant handle_hex(Location loc)
		{
			int @char = this.get_char();
			int num;
			while ((num = this.peek_char()) != -1 && Tokenizer.is_hex(num))
			{
				char[] array = this.number_builder;
				int num2 = this.number_pos;
				this.number_pos = num2 + 1;
				array[num2] = (ushort)num;
				int char2 = this.get_char();
			}
			string s = new string(this.number_builder, 0, this.number_pos);
			ILiteralConstant result;
			try
			{
				ulong ul;
				if (this.number_pos <= 8)
				{
					ul = (ulong)uint.Parse(s, NumberStyles.HexNumber);
				}
				else
				{
					ul = ulong.Parse(s, NumberStyles.HexNumber);
				}
				result = this.integer_type_suffix(ul, this.peek_char(), loc);
			}
			catch (OverflowException)
			{
				this.Error_NumericConstantTooLong();
				result = new IntLiteral(this.context.BuiltinTypes, 0, loc);
			}
			catch (FormatException)
			{
				this.Report.Error(1013, this.Location, "Invalid number");
				result = new IntLiteral(this.context.BuiltinTypes, 0, loc);
			}
			return result;
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x000631BC File Offset: 0x000613BC
		private int is_number(int c, bool dotLead)
		{
			this.number_pos = 0;
			Location location = this.Location;
			if (!dotLead)
			{
				if (c == 48)
				{
					int num = this.peek_char();
					if (num == 120 || num == 88)
					{
						this.val = this.handle_hex(location);
						return 421;
					}
				}
				this.decimal_digits(c);
				c = this.peek_char();
			}
			bool flag = false;
			if (c == 46)
			{
				if (!dotLead)
				{
					int @char = this.get_char();
				}
				if (!this.decimal_digits(46))
				{
					this.putback(46);
					this.number_pos--;
					this.val = this.adjust_int(-1, location);
					return 421;
				}
				flag = true;
				c = this.peek_char();
			}
			if (c == 101 || c == 69)
			{
				flag = true;
				int char2 = this.get_char();
				if (this.number_pos == 512)
				{
					this.Error_NumericConstantTooLong();
				}
				char[] array = this.number_builder;
				int num2 = this.number_pos;
				this.number_pos = num2 + 1;
				array[num2] = (ushort)c;
				c = this.get_char();
				if (c == 43)
				{
					if (this.number_pos == 512)
					{
						this.Error_NumericConstantTooLong();
					}
					char[] array2 = this.number_builder;
					num2 = this.number_pos;
					this.number_pos = num2 + 1;
					array2[num2] = 43;
					c = -1;
				}
				else if (c == 45)
				{
					if (this.number_pos == 512)
					{
						this.Error_NumericConstantTooLong();
					}
					char[] array3 = this.number_builder;
					num2 = this.number_pos;
					this.number_pos = num2 + 1;
					array3[num2] = 45;
					c = -1;
				}
				else
				{
					if (this.number_pos == 512)
					{
						this.Error_NumericConstantTooLong();
					}
					char[] array4 = this.number_builder;
					num2 = this.number_pos;
					this.number_pos = num2 + 1;
					array4[num2] = 43;
				}
				this.decimal_digits(c);
				c = this.peek_char();
			}
			TypeCode typeCode = Tokenizer.real_type_suffix(c);
			ILiteralConstant literalConstant;
			if (typeCode == TypeCode.Empty && !flag)
			{
				literalConstant = this.adjust_int(c, location);
			}
			else
			{
				if (typeCode != TypeCode.Empty)
				{
					int char3 = this.get_char();
				}
				literalConstant = this.adjust_real(typeCode, location);
			}
			this.val = literalConstant;
			return 421;
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x000633A8 File Offset: 0x000615A8
		private int getHex(int count, out int surrogate, out bool error)
		{
			int num = 0;
			int num2 = (count != -1) ? count : 4;
			int @char = this.get_char();
			error = false;
			surrogate = 0;
			for (int i = 0; i < num2; i++)
			{
				int num3 = this.get_char();
				if (num3 >= 48 && num3 <= 57)
				{
					num3 -= 48;
				}
				else if (num3 >= 65 && num3 <= 70)
				{
					num3 = num3 - 65 + 10;
				}
				else
				{
					if (num3 < 97 || num3 > 102)
					{
						error = true;
						return 0;
					}
					num3 = num3 - 97 + 10;
				}
				num = num * 16 + num3;
				if (count == -1)
				{
					int num4 = this.peek_char();
					if (num4 == -1 || !Tokenizer.is_hex((int)((ushort)num4)))
					{
						break;
					}
				}
			}
			if (num2 == 8)
			{
				if (num > 1114111)
				{
					error = true;
					return 0;
				}
				if (num >= 65536)
				{
					surrogate = (num - 65536) % 1024 + 56320;
					num = (num - 65536) / 1024 + 55296;
				}
			}
			return num;
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x00063484 File Offset: 0x00061684
		private int escape(int c, out int surrogate)
		{
			int num = this.peek_char();
			if (c != 92)
			{
				surrogate = 0;
				return c;
			}
			int result;
			if (num <= 85)
			{
				if (num <= 39)
				{
					if (num == 34)
					{
						result = 34;
						goto IL_111;
					}
					if (num != 39)
					{
						goto IL_E7;
					}
					result = 39;
					goto IL_111;
				}
				else
				{
					if (num == 48)
					{
						result = 0;
						goto IL_111;
					}
					if (num != 85)
					{
						goto IL_E7;
					}
				}
			}
			else if (num <= 97)
			{
				if (num == 92)
				{
					result = 92;
					goto IL_111;
				}
				if (num != 97)
				{
					goto IL_E7;
				}
				result = 7;
				goto IL_111;
			}
			else
			{
				if (num == 98)
				{
					result = 8;
					goto IL_111;
				}
				if (num == 102)
				{
					result = 12;
					goto IL_111;
				}
				switch (num)
				{
				case 110:
					result = 10;
					goto IL_111;
				case 111:
				case 112:
				case 113:
				case 115:
				case 119:
					goto IL_E7;
				case 114:
					result = 13;
					goto IL_111;
				case 116:
					result = 9;
					goto IL_111;
				case 117:
					break;
				case 118:
					result = 11;
					goto IL_111;
				case 120:
				{
					bool flag;
					result = this.getHex(-1, out surrogate, out flag);
					if (!flag)
					{
						return result;
					}
					goto IL_E7;
				}
				default:
					goto IL_E7;
				}
			}
			return this.EscapeUnicode(num, out surrogate);
			IL_E7:
			surrogate = 0;
			this.Report.Error(1009, this.Location, "Unrecognized escape sequence `\\{0}'", ((char)num).ToString());
			return num;
			IL_111:
			int @char = this.get_char();
			surrogate = 0;
			return result;
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x000635B0 File Offset: 0x000617B0
		private int EscapeUnicode(int ch, out int surrogate)
		{
			bool flag;
			if (ch == 85)
			{
				ch = this.getHex(8, out surrogate, out flag);
			}
			else
			{
				ch = this.getHex(4, out surrogate, out flag);
			}
			if (flag)
			{
				this.Report.Error(1009, this.Location, "Unrecognized escape sequence");
			}
			return ch;
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x000635FC File Offset: 0x000617FC
		private int get_char()
		{
			int num;
			if (this.putback_char != -1)
			{
				num = this.putback_char;
				this.putback_char = -1;
			}
			else
			{
				num = this.reader.Read();
			}
			if (num <= 13)
			{
				if (num == 13)
				{
					if (this.peek_char() == 10)
					{
						this.putback_char = -1;
					}
					num = 10;
					this.advance_line();
				}
				else if (num == 10)
				{
					this.advance_line();
				}
				else
				{
					this.col++;
				}
			}
			else if (num >= 8232 && num <= 8233)
			{
				this.advance_line();
			}
			else
			{
				this.col++;
			}
			return num;
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x00063698 File Offset: 0x00061898
		private void advance_line()
		{
			this.line++;
			this.ref_line++;
			this.previous_col = this.col;
			this.col = 0;
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x000636C9 File Offset: 0x000618C9
		private int peek_char()
		{
			if (this.putback_char == -1)
			{
				this.putback_char = this.reader.Read();
			}
			return this.putback_char;
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x000636EB File Offset: 0x000618EB
		private int peek_char2()
		{
			if (this.putback_char != -1)
			{
				return this.putback_char;
			}
			return this.reader.Peek();
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x00063708 File Offset: 0x00061908
		public void putback(int c)
		{
			if (this.putback_char != -1)
			{
				throw new InternalErrorException(string.Format("Secondary putback [{0}] putting back [{1}] is not allowed", (char)this.putback_char, (char)c), new object[]
				{
					this.Location
				});
			}
			if (c == 10 || this.col == 0 || (c >= 8232 && c <= 8233))
			{
				this.line--;
				this.ref_line--;
				this.col = this.previous_col;
			}
			else
			{
				this.col--;
			}
			this.putback_char = c;
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x000637B1 File Offset: 0x000619B1
		public bool advance()
		{
			return this.peek_char() != -1 || this.CompleteOnEOF;
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001517 RID: 5399 RVA: 0x000637C4 File Offset: 0x000619C4
		public object Value
		{
			get
			{
				return this.val;
			}
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x000637C4 File Offset: 0x000619C4
		public object value()
		{
			return this.val;
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x000637CC File Offset: 0x000619CC
		public int token()
		{
			this.current_token = this.xtoken();
			return this.current_token;
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x000637E0 File Offset: 0x000619E0
		private int TokenizePreprocessorKeyword(out int c)
		{
			do
			{
				c = this.get_char();
			}
			while (c == 32 || c == 9);
			int result = 0;
			while (c != -1 && c >= 97 && c <= 122)
			{
				this.id_builder[result++] = (char)c;
				c = this.get_char();
				if (c == 92)
				{
					int num = this.peek_char();
					if (num == 85 || num == 117)
					{
						int num2;
						c = this.EscapeUnicode(c, out num2);
						if (num2 != 0)
						{
							if (Tokenizer.is_identifier_part_character((char)c))
							{
								this.id_builder[result++] = (char)c;
							}
							c = num2;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x00063870 File Offset: 0x00061A70
		private Tokenizer.PreprocessorDirective get_cmd_arg(out string arg)
		{
			this.tokens_seen = false;
			arg = "";
			int num;
			Tokenizer.PreprocessorDirective preprocessorDirective = Tokenizer.GetPreprocessorDirective(this.id_builder, this.TokenizePreprocessorKeyword(out num));
			if ((preprocessorDirective & Tokenizer.PreprocessorDirective.CustomArgumentsParsing) != Tokenizer.PreprocessorDirective.Invalid)
			{
				return preprocessorDirective;
			}
			while (num == 32 || num == 9)
			{
				num = this.get_char();
			}
			int num2 = (int)(preprocessorDirective & Tokenizer.PreprocessorDirective.RequiresArgument);
			int num3 = 0;
			while (num != -1 && num != 10 && num != 8232 && num != 8233)
			{
				if (num == 92 && num2 >= 0)
				{
					if (num2 != 0)
					{
						num2 = 1;
						int num4 = this.peek_char();
						if (num4 == 85 || num4 == 117)
						{
							int num5;
							num = this.EscapeUnicode(num, out num5);
							if (num5 != 0)
							{
								if (Tokenizer.is_identifier_part_character((char)num))
								{
									if (num3 == this.value_builder.Length)
									{
										Array.Resize<char>(ref this.value_builder, num3 * 2);
									}
									this.value_builder[num3++] = (char)num;
								}
								num = num5;
							}
						}
					}
					else
					{
						num2 = -1;
					}
				}
				else if (num == 47 && this.peek_char() == 47)
				{
					int @char = this.get_char();
					this.ReadToEndOfLine();
					break;
				}
				if (num3 == this.value_builder.Length)
				{
					Array.Resize<char>(ref this.value_builder, num3 * 2);
				}
				this.value_builder[num3++] = (char)num;
				num = this.get_char();
			}
			if (num3 != 0)
			{
				if (num3 > 512)
				{
					arg = new string(this.value_builder, 0, num3);
				}
				else
				{
					arg = this.InternIdentifier(this.value_builder, num3);
				}
				arg = arg.Trim(Tokenizer.simple_whitespaces);
			}
			return preprocessorDirective;
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x000639D8 File Offset: 0x00061BD8
		private bool PreProcessLine()
		{
			Location location = this.Location;
			int num2;
			int num = this.TokenizePreprocessorKeyword(out num2);
			if (num == Tokenizer.line_default.Length)
			{
				if (!this.IsTokenIdentifierEqual(Tokenizer.line_default))
				{
					return false;
				}
				this.current_source = this.source_file.SourceFile;
				if (!this.hidden_block_start.IsNull)
				{
					this.current_source.RegisterHiddenScope(this.hidden_block_start, location);
					this.hidden_block_start = Location.Null;
				}
				this.ref_line = this.line;
				return true;
			}
			else if (num == Tokenizer.line_hidden.Length)
			{
				if (!this.IsTokenIdentifierEqual(Tokenizer.line_hidden))
				{
					return false;
				}
				if (this.hidden_block_start.IsNull)
				{
					this.hidden_block_start = location;
				}
				return true;
			}
			else
			{
				if (num != 0 || num2 < 48 || num2 > 57)
				{
					this.ReadToEndOfLine();
					return false;
				}
				int num3 = this.TokenizeNumber(num2);
				if (num3 < 1)
				{
					this.ReadToEndOfLine();
					return num3 != 0;
				}
				num2 = this.get_char();
				if (num2 == 32)
				{
					do
					{
						num2 = this.get_char();
					}
					while (num2 == 32 || num2 == 9);
				}
				else if (num2 == 34)
				{
					num2 = 0;
				}
				if (num2 != 10 && num2 != 47 && num2 != 34 && num2 != 8232 && num2 != 8233)
				{
					this.ReadToEndOfLine();
					this.Report.Error(1578, location, "Filename, single-line comment or end-of-line expected");
					return true;
				}
				string text = null;
				if (num2 == 34)
				{
					text = this.TokenizeFileName(ref num2);
					while (num2 == 32 || num2 == 9)
					{
						num2 = this.get_char();
					}
				}
				if (num2 != 10 && num2 != 8232 && num2 != 8233)
				{
					if (num2 != 47)
					{
						this.ReadToEndOfLine();
						this.Error_EndLineExpected();
						return true;
					}
					this.ReadSingleLineComment();
				}
				if (text != null)
				{
					this.current_source = this.context.LookupFile(this.source_file, text);
					this.source_file.AddIncludeFile(this.current_source);
				}
				if (!this.hidden_block_start.IsNull)
				{
					this.current_source.RegisterHiddenScope(this.hidden_block_start, location);
					this.hidden_block_start = Location.Null;
				}
				this.ref_line = num3;
				return true;
			}
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x00063BCC File Offset: 0x00061DCC
		private void PreProcessDefinition(bool is_define, string ident, bool caller_is_taking)
		{
			if (ident.Length == 0 || ident == "true" || ident == "false")
			{
				this.Report.Error(1001, this.Location, "Missing identifier to pre-processor directive");
				return;
			}
			if (ident.IndexOfAny(Tokenizer.simple_whitespaces) != -1)
			{
				this.Error_EndLineExpected();
				return;
			}
			if (!Tokenizer.is_identifier_start_character((int)ident[0]))
			{
				this.Report.Error(1001, this.Location, "Identifier expected: {0}", ident);
			}
			string text = ident.Substring(1);
			for (int i = 0; i < text.Length; i++)
			{
				if (!Tokenizer.is_identifier_part_character(text[i]))
				{
					this.Report.Error(1001, this.Location, "Identifier expected: {0}", ident);
					return;
				}
			}
			if (!caller_is_taking)
			{
				return;
			}
			if (!is_define)
			{
				this.source_file.AddUndefine(ident);
				return;
			}
			if (this.context.Settings.IsConditionalSymbolDefined(ident))
			{
				return;
			}
			this.source_file.AddDefine(ident);
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x00063CD0 File Offset: 0x00061ED0
		private byte read_hex(out bool error)
		{
			int @char = this.get_char();
			int num;
			if (@char >= 48 && @char <= 57)
			{
				num = @char - 48;
			}
			else if (@char >= 65 && @char <= 70)
			{
				num = @char - 65 + 10;
			}
			else
			{
				if (@char < 97 || @char > 102)
				{
					error = true;
					return 0;
				}
				num = @char - 97 + 10;
			}
			num *= 16;
			@char = this.get_char();
			if (@char >= 48 && @char <= 57)
			{
				num += @char - 48;
			}
			else if (@char >= 65 && @char <= 70)
			{
				num += @char - 65 + 10;
			}
			else
			{
				if (@char < 97 || @char > 102)
				{
					error = true;
					return 0;
				}
				num += @char - 97 + 10;
			}
			error = false;
			return (byte)num;
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x00063D78 File Offset: 0x00061F78
		private bool ParsePragmaChecksum()
		{
			int num = this.get_char();
			if (num != 34)
			{
				return false;
			}
			string name = this.TokenizeFileName(ref num);
			if (num != 32)
			{
				return false;
			}
			SourceFile sourceFile = this.context.LookupFile(this.source_file, name);
			if (this.get_char() != 34 || this.get_char() != 123)
			{
				return false;
			}
			byte[] array = new byte[16];
			int i;
			for (i = 0; i < 4; i++)
			{
				bool flag;
				array[i] = this.read_hex(out flag);
				if (flag)
				{
					return false;
				}
			}
			if (this.get_char() != 45)
			{
				return false;
			}
			while (i < 10)
			{
				bool flag;
				array[i] = this.read_hex(out flag);
				if (flag)
				{
					return false;
				}
				array[i++] = this.read_hex(out flag);
				if (flag)
				{
					return false;
				}
				if (this.get_char() != 45)
				{
					return false;
				}
				i++;
			}
			while (i < 16)
			{
				bool flag;
				array[i] = this.read_hex(out flag);
				if (flag)
				{
					return false;
				}
				i++;
			}
			if (this.get_char() != 125 || this.get_char() != 34)
			{
				return false;
			}
			num = this.get_char();
			if (num != 32)
			{
				return false;
			}
			if (this.get_char() != 34)
			{
				return false;
			}
			List<byte> list = new List<byte>(16);
			Location location = this.Location;
			num = this.peek_char();
			while (num != 34 && num != -1)
			{
				bool flag;
				list.Add(this.read_hex(out flag));
				if (flag)
				{
					return false;
				}
				num = this.peek_char();
			}
			if (num == 47)
			{
				this.ReadSingleLineComment();
			}
			else if (this.get_char() != 34)
			{
				return false;
			}
			if (this.context.Settings.GenerateDebugInfo)
			{
				byte[] array2 = list.ToArray();
				if (sourceFile.HasChecksum && !ArrayComparer.IsEqual<byte>(sourceFile.Checksum, array2))
				{
					this.Report.Warning(1697, 1, location, "Different checksum values specified for file `{0}'", sourceFile.Name);
				}
				sourceFile.SetChecksum(array, array2);
				this.current_source.AutoGenerated = true;
			}
			return true;
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x00063F54 File Offset: 0x00062154
		private bool IsTokenIdentifierEqual(char[] identifier)
		{
			for (int i = 0; i < identifier.Length; i++)
			{
				if (identifier[i] != this.id_builder[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x00063F80 File Offset: 0x00062180
		private bool ScanClosingInterpolationBrace()
		{
			this.PushPosition();
			bool? flag = null;
			int num = 0;
			do
			{
				int num2 = this.reader.Read();
				if (num2 != -1)
				{
					if (num2 != 34)
					{
						if (num2 == 125)
						{
							if (num % 2 == 1)
							{
								flag = new bool?(true);
							}
						}
					}
					else
					{
						num++;
					}
				}
				else
				{
					flag = new bool?(false);
				}
			}
			while (flag == null);
			this.PopPosition();
			return flag.Value;
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x00063FF0 File Offset: 0x000621F0
		private int TokenizeNumber(int value)
		{
			this.number_pos = 0;
			this.decimal_digits(value);
			uint num = (uint)(this.number_builder[0] - '0');
			int result;
			try
			{
				for (int i = 1; i < this.number_pos; i++)
				{
					num = checked(num * 10U + (uint)(this.number_builder[i] - '0'));
				}
				result = (int)num;
			}
			catch (OverflowException)
			{
				this.Error_NumericConstantTooLong();
				result = -1;
			}
			return result;
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x0006405C File Offset: 0x0006225C
		private string TokenizeFileName(ref int c)
		{
			StringBuilder stringBuilder = new StringBuilder();
			while (c != -1 && c != 10 && c != 8232 && c != 8233)
			{
				c = this.get_char();
				if (c == 34)
				{
					c = this.get_char();
					break;
				}
				stringBuilder.Append((char)c);
			}
			if (stringBuilder.Length == 0)
			{
				this.Report.Warning(1709, 1, this.Location, "Filename specified for preprocessor directive is empty");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x000640DC File Offset: 0x000622DC
		private int TokenizePragmaWarningIdentifier(ref int c, ref bool identifier)
		{
			if ((c >= 48 && c <= 57) || Tokenizer.is_identifier_start_character(c))
			{
				int num;
				if (c >= 48 && c <= 57)
				{
					this.number_pos = 0;
					num = this.TokenizeNumber(c);
					c = this.get_char();
					if (c != 32 && c != 9 && c != 44 && c != 10 && c != -1 && c != 8232 && c != 8233)
					{
						return this.ReadPragmaWarningComment(c);
					}
				}
				else
				{
					int num2 = 0;
					num = -1;
					this.id_builder[num2++] = (char)c;
					while (c < 512)
					{
						c = this.reader.Read();
						this.id_builder[num2] = (char)c;
						if (c >= 48 && c <= 57)
						{
							if (num2 == 6 && this.id_builder[0] == 'C' && this.id_builder[1] == 'S')
							{
								num = 0;
								int num3 = 1000;
								for (int i = 0; i < 4; i++)
								{
									char c2 = this.id_builder[i + 2];
									if (c2 < '0' || c2 > '9')
									{
										num = -1;
										break;
									}
									num += (int)(c2 - '0') * num3;
									num3 /= 10;
								}
							}
						}
						else if ((c < 97 || c > 122) && (c < 65 || c > 90) && c != 95)
						{
							break;
						}
						num2++;
					}
					if (num < 0)
					{
						identifier = true;
						num = num2;
					}
				}
				while (c == 32 || c == 9)
				{
					c = this.get_char();
				}
				if (c == 44)
				{
					c = this.get_char();
				}
				while (c == 32 || c == 9)
				{
					c = this.get_char();
				}
				return num;
			}
			return this.ReadPragmaWarningComment(c);
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x00064286 File Offset: 0x00062486
		private int ReadPragmaWarningComment(int c)
		{
			if (c == 47)
			{
				this.ReadSingleLineComment();
			}
			else
			{
				this.Report.Warning(1692, 1, this.Location, "Invalid number");
				this.ReadToEndOfLine();
			}
			return -1;
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x000642B8 File Offset: 0x000624B8
		private void ReadToEndOfLine()
		{
			int @char;
			do
			{
				@char = this.get_char();
			}
			while (@char != -1 && @char != 10 && @char != 8232 && @char != 8233);
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x000642E5 File Offset: 0x000624E5
		private void ReadSingleLineComment()
		{
			if (this.peek_char() != 47)
			{
				this.Report.Warning(1696, 1, this.Location, "Single-line comment or end-of-line expected");
			}
			this.ReadToEndOfLine();
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x00064314 File Offset: 0x00062514
		private void ParsePragmaDirective()
		{
			int @char;
			int num = this.TokenizePreprocessorKeyword(out @char);
			if (num == Tokenizer.pragma_warning.Length && this.IsTokenIdentifierEqual(Tokenizer.pragma_warning))
			{
				num = this.TokenizePreprocessorKeyword(out @char);
				if (num == Tokenizer.pragma_warning_disable.Length)
				{
					bool flag = this.IsTokenIdentifierEqual(Tokenizer.pragma_warning_disable);
					if (!flag)
					{
						if (!this.IsTokenIdentifierEqual(Tokenizer.pragma_warning_restore))
						{
							goto IL_144;
						}
					}
					while (@char == 32 || @char == 9)
					{
						@char = this.get_char();
					}
					Location location = this.Location;
					if (@char != 10 && @char != 47 && @char != 8232 && @char != 8233)
					{
						int num2;
						do
						{
							bool flag2 = false;
							num2 = this.TokenizePragmaWarningIdentifier(ref @char, ref flag2);
							if (num2 > 0 && !flag2)
							{
								if (flag)
								{
									this.Report.RegisterWarningRegion(location).WarningDisable(location, num2, this.context.Report);
								}
								else
								{
									this.Report.RegisterWarningRegion(location).WarningEnable(location, num2, this.context);
								}
							}
						}
						while (num2 >= 0 && @char != 10 && @char != -1 && @char != 8232 && @char != 8233);
						return;
					}
					if (@char == 47)
					{
						this.ReadSingleLineComment();
					}
					if (flag)
					{
						this.Report.RegisterWarningRegion(location).WarningDisable(location.Row);
						return;
					}
					this.Report.RegisterWarningRegion(location).WarningEnable(location.Row);
					return;
				}
				IL_144:
				this.Report.Warning(1634, 1, this.Location, "Expected disable or restore");
				this.ReadToEndOfLine();
				return;
			}
			if (num == Tokenizer.pragma_checksum.Length && this.IsTokenIdentifierEqual(Tokenizer.pragma_checksum))
			{
				if (@char != 32 || !this.ParsePragmaChecksum())
				{
					this.Report.Warning(1695, 1, this.Location, "Invalid #pragma checksum syntax. Expected \"filename\" \"{XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX}\" \"XXXX...\"");
				}
				return;
			}
			this.Report.Warning(1633, 1, this.Location, "Unrecognized #pragma directive");
			this.ReadToEndOfLine();
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x000644EB File Offset: 0x000626EB
		private bool eval_val(string s)
		{
			return s == "true" || (!(s == "false") && this.source_file.IsConditionalDefined(s));
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x00064518 File Offset: 0x00062718
		private bool pp_primary(ref string s)
		{
			s = s.Trim();
			int length = s.Length;
			if (length > 0)
			{
				char c = s[0];
				if (c == '(')
				{
					s = s.Substring(1);
					bool result = this.pp_expr(ref s, false);
					if (s.Length > 0 && s[0] == ')')
					{
						s = s.Substring(1);
						return result;
					}
					this.Error_InvalidDirective();
					return false;
				}
				else if (Tokenizer.is_identifier_start_character((int)c))
				{
					for (int i = 1; i < length; i++)
					{
						c = s[i];
						if (!Tokenizer.is_identifier_part_character(c))
						{
							bool result2 = this.eval_val(s.Substring(0, i));
							s = s.Substring(i);
							return result2;
						}
					}
					bool result3 = this.eval_val(s);
					s = "";
					return result3;
				}
			}
			this.Error_InvalidDirective();
			return false;
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x000645E0 File Offset: 0x000627E0
		private bool pp_unary(ref string s)
		{
			s = s.Trim();
			int length = s.Length;
			if (length <= 0)
			{
				this.Error_InvalidDirective();
				return false;
			}
			if (s[0] != '!')
			{
				return this.pp_primary(ref s);
			}
			if (length > 1 && s[1] == '=')
			{
				this.Error_InvalidDirective();
				return false;
			}
			s = s.Substring(1);
			return !this.pp_primary(ref s);
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x0006464C File Offset: 0x0006284C
		private bool pp_eq(ref string s)
		{
			bool flag = this.pp_unary(ref s);
			s = s.Trim();
			int length = s.Length;
			if (length > 0)
			{
				if (s[0] == '=')
				{
					if (length > 2 && s[1] == '=')
					{
						s = s.Substring(2);
						return flag == this.pp_unary(ref s);
					}
					this.Error_InvalidDirective();
					return false;
				}
				else if (s[0] == '!' && length > 1 && s[1] == '=')
				{
					s = s.Substring(2);
					return flag != this.pp_unary(ref s);
				}
			}
			return flag;
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x000646E4 File Offset: 0x000628E4
		private bool pp_and(ref string s)
		{
			bool flag = this.pp_eq(ref s);
			s = s.Trim();
			int length = s.Length;
			if (length <= 0 || s[0] != '&')
			{
				return flag;
			}
			if (length > 2 && s[1] == '&')
			{
				s = s.Substring(2);
				return flag & this.pp_and(ref s);
			}
			this.Error_InvalidDirective();
			return false;
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x00064748 File Offset: 0x00062948
		private bool pp_expr(ref string s, bool isTerm)
		{
			bool flag = this.pp_and(ref s);
			s = s.Trim();
			int length = s.Length;
			if (length > 0)
			{
				if (s[0] == '|')
				{
					if (length > 2 && s[1] == '|')
					{
						s = s.Substring(2);
						return flag | this.pp_expr(ref s, isTerm);
					}
					this.Error_InvalidDirective();
					return false;
				}
				else if (isTerm)
				{
					this.Error_EndLineExpected();
					return false;
				}
			}
			return flag;
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x000647B8 File Offset: 0x000629B8
		private bool eval(string s)
		{
			bool flag = this.pp_expr(ref s, true);
			s = s.Trim();
			return s.Length == 0 && flag;
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x000647E2 File Offset: 0x000629E2
		private void Error_NumericConstantTooLong()
		{
			this.Report.Error(1021, this.Location, "Integral constant is too large");
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x000647FF File Offset: 0x000629FF
		private void Error_InvalidDirective()
		{
			this.Report.Error(1517, this.Location, "Invalid preprocessor directive");
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x0006481C File Offset: 0x00062A1C
		private void Error_UnexpectedDirective(string extra)
		{
			this.Report.Error(1028, this.Location, "Unexpected processor directive ({0})", extra);
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x0006483A File Offset: 0x00062A3A
		private void Error_TokensSeen()
		{
			this.Report.Error(1032, this.Location, "Cannot define or undefine preprocessor symbols after first token in file");
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x00064857 File Offset: 0x00062A57
		private void Eror_WrongPreprocessorLocation()
		{
			this.Report.Error(1040, this.Location, "Preprocessor directives must appear as the first non-whitespace character on a line");
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x00064874 File Offset: 0x00062A74
		private void Error_EndLineExpected()
		{
			this.Report.Error(1025, this.Location, "Single-line comment or end-of-line expected");
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x00064891 File Offset: 0x00062A91
		private void WarningMisplacedComment(Location loc)
		{
			if (this.doc_state != XmlCommentState.Error)
			{
				this.doc_state = XmlCommentState.Error;
				this.Report.Warning(1587, 2, loc, "XML comment is not placed on a valid language element");
			}
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x000648BC File Offset: 0x00062ABC
		private bool ParsePreprocessingDirective(bool caller_is_taking)
		{
			bool flag = false;
			string text;
			Tokenizer.PreprocessorDirective preprocessorDirective = this.get_cmd_arg(out text);
			switch (preprocessorDirective)
			{
			case Tokenizer.PreprocessorDirective.Invalid:
				this.Report.Error(1024, this.Location, "Wrong preprocessor directive");
				return true;
			case Tokenizer.PreprocessorDirective.Region:
				flag = true;
				text = "true";
				break;
			case Tokenizer.PreprocessorDirective.Endregion:
				if (this.ifstack == null || this.ifstack.Count == 0)
				{
					this.Error_UnexpectedDirective("no #region for this #endregion");
					return true;
				}
				if ((this.ifstack.Pop() & 16) == 0)
				{
					this.Report.Error(1027, this.Location, "Expected `#endif' directive");
				}
				return caller_is_taking;
			case (Tokenizer.PreprocessorDirective)3:
			case (Tokenizer.PreprocessorDirective)5:
				goto IL_351;
			case Tokenizer.PreprocessorDirective.Endif:
				if (this.ifstack == null || this.ifstack.Count == 0)
				{
					this.Error_UnexpectedDirective("no #if for this #endif");
					return true;
				}
				if ((this.ifstack.Pop() & 16) != 0)
				{
					this.Report.Error(1038, this.Location, "#endregion directive expected");
				}
				if (text.Length != 0)
				{
					this.Error_EndLineExpected();
				}
				return this.ifstack.Count == 0 || (this.ifstack.Peek() & 1) != 0;
			case Tokenizer.PreprocessorDirective.Else:
			{
				if (this.ifstack == null || this.ifstack.Count == 0)
				{
					this.Error_UnexpectedDirective("no #if for this #else");
					return true;
				}
				int num = this.ifstack.Peek();
				if ((num & 16) != 0)
				{
					this.Report.Error(1038, this.Location, "#endregion directive expected");
					return true;
				}
				if ((num & 4) != 0)
				{
					this.Error_UnexpectedDirective("#else within #else");
					return true;
				}
				this.ifstack.Pop();
				if (text.Length != 0)
				{
					this.Error_EndLineExpected();
					return true;
				}
				bool flag2 = false;
				if ((num & 8) != 0)
				{
					flag2 = ((num & 1) == 0);
					if (flag2)
					{
						num |= 1;
					}
					else
					{
						num &= -2;
					}
				}
				this.ifstack.Push(num | 4);
				return flag2;
			}
			default:
				switch (preprocessorDirective)
				{
				case Tokenizer.PreprocessorDirective.If:
					break;
				case (Tokenizer.PreprocessorDirective)2052:
				case (Tokenizer.PreprocessorDirective)2054:
					goto IL_351;
				case Tokenizer.PreprocessorDirective.Elif:
				{
					if (this.ifstack == null || this.ifstack.Count == 0)
					{
						this.Error_UnexpectedDirective("no #if for this #elif");
						return true;
					}
					int num2 = this.ifstack.Pop();
					if ((num2 & 16) != 0)
					{
						this.Report.Error(1038, this.Location, "#endregion directive expected");
						return true;
					}
					if ((num2 & 4) != 0)
					{
						this.Error_UnexpectedDirective("#elif not valid after #else");
						return true;
					}
					if ((num2 & 1) != 0)
					{
						this.ifstack.Push(0);
						return false;
					}
					if (this.eval(text) && (num2 & 8) != 0)
					{
						this.ifstack.Push(num2 | 1);
						return true;
					}
					this.ifstack.Push(num2);
					return false;
				}
				case Tokenizer.PreprocessorDirective.Define:
					if (this.any_token_seen)
					{
						if (caller_is_taking)
						{
							this.Error_TokensSeen();
						}
						return caller_is_taking;
					}
					this.PreProcessDefinition(true, text, caller_is_taking);
					return caller_is_taking;
				case Tokenizer.PreprocessorDirective.Undef:
					if (this.any_token_seen)
					{
						if (caller_is_taking)
						{
							this.Error_TokensSeen();
						}
						return caller_is_taking;
					}
					this.PreProcessDefinition(false, text, caller_is_taking);
					return caller_is_taking;
				default:
					goto IL_351;
				}
				break;
			}
			if (this.ifstack == null)
			{
				this.ifstack = new Stack<int>(2);
			}
			int num3 = flag ? 16 : 0;
			if (this.ifstack.Count == 0)
			{
				num3 |= 8;
			}
			else if ((this.ifstack.Peek() & 1) != 0)
			{
				num3 |= 8;
			}
			if (this.eval(text) && caller_is_taking)
			{
				this.ifstack.Push(num3 | 1);
				return true;
			}
			this.ifstack.Push(num3);
			return false;
			IL_351:
			if (!caller_is_taking)
			{
				return false;
			}
			if (preprocessorDirective <= Tokenizer.PreprocessorDirective.Warning)
			{
				if (preprocessorDirective == Tokenizer.PreprocessorDirective.Error)
				{
					this.Report.Error(1029, this.Location, "#error: '{0}'", text);
					return true;
				}
				if (preprocessorDirective == Tokenizer.PreprocessorDirective.Warning)
				{
					this.Report.Warning(1030, 1, this.Location, "#warning: `{0}'", text);
					return true;
				}
			}
			else
			{
				if (preprocessorDirective == Tokenizer.PreprocessorDirective.Pragma)
				{
					if (this.context.Settings.Version == LanguageVersion.ISO_1)
					{
						this.Report.FeatureIsNotAvailable(this.context, this.Location, "#pragma");
					}
					this.ParsePragmaDirective();
					return true;
				}
				if (preprocessorDirective == Tokenizer.PreprocessorDirective.Line)
				{
					Location location = this.Location;
					if (!this.PreProcessLine())
					{
						this.Report.Error(1576, location, "The line number specified for #line directive is missing or invalid");
					}
					return caller_is_taking;
				}
			}
			throw new NotImplementedException(preprocessorDirective.ToString());
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x00064CF8 File Offset: 0x00062EF8
		private int consume_string(bool quoted)
		{
			int num = 0;
			Location loc = this.Location;
			if (quoted)
			{
				loc -= 1;
			}
			for (;;)
			{
				int num2;
				if (this.putback_char != -1)
				{
					num2 = this.putback_char;
					this.putback_char = -1;
				}
				else
				{
					num2 = this.reader.Read();
				}
				if (num2 == 34)
				{
					this.col++;
					if (!quoted || this.peek_char() != 34)
					{
						break;
					}
					if (num == this.value_builder.Length)
					{
						Array.Resize<char>(ref this.value_builder, num * 2);
					}
					this.value_builder[num++] = (char)num2;
					int @char = this.get_char();
				}
				else
				{
					if (num2 == 10 || num2 == 8232 || num2 == 8233)
					{
						if (!quoted)
						{
							goto Block_9;
						}
						this.advance_line();
					}
					else if (num2 == 92 && !quoted)
					{
						this.col++;
						int num3;
						num2 = this.escape(num2, out num3);
						if (num2 == -1)
						{
							return 259;
						}
						if (num3 != 0)
						{
							if (num == this.value_builder.Length)
							{
								Array.Resize<char>(ref this.value_builder, num * 2);
							}
							this.value_builder[num++] = (char)num2;
							num2 = num3;
						}
					}
					else
					{
						if (num2 == -1)
						{
							goto Block_17;
						}
						this.col++;
					}
					if (num == this.value_builder.Length)
					{
						Array.Resize<char>(ref this.value_builder, num * 2);
					}
					this.value_builder[num++] = (char)num2;
				}
			}
			ILiteralConstant literalConstant = new StringLiteral(this.context.BuiltinTypes, this.CreateStringFromBuilder(num), loc);
			this.val = literalConstant;
			return 421;
			Block_9:
			this.Report.Error(1010, this.Location, "Newline in constant");
			this.advance_line();
			if (num > 1 && this.value_builder[num - 1] == '\r')
			{
				num--;
			}
			this.val = new StringLiteral(this.context.BuiltinTypes, new string(this.value_builder, 0, num), loc);
			return 421;
			Block_17:
			this.Report.Error(1039, this.Location, "Unterminated string literal");
			return 257;
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x00064EF0 File Offset: 0x000630F0
		private int consume_identifier(int s)
		{
			int result = this.consume_identifier(s, false);
			if (this.doc_state == XmlCommentState.Allowed)
			{
				this.doc_state = XmlCommentState.NotAllowed;
			}
			return result;
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x00064F0C File Offset: 0x0006310C
		private int consume_identifier(int c, bool quoted)
		{
			int num = 0;
			int num2 = this.col;
			if (quoted)
			{
				num2--;
			}
			if (c == 92)
			{
				int num3;
				c = this.escape(c, out num3);
				if (!quoted && !Tokenizer.is_identifier_start_character(c))
				{
					if (num3 == 0)
					{
						this.Report.Error(1056, this.Location, "Unexpected character `\\{0}'", c.ToString("x4"));
						return 259;
					}
					this.id_builder[num++] = (char)c;
					c = num3;
				}
			}
			this.id_builder[num++] = (char)c;
			try
			{
				for (;;)
				{
					c = this.reader.Read();
					if ((c >= 97 && c <= 122) || (c >= 65 && c <= 90) || c == 95 || (c >= 48 && c <= 57))
					{
						this.id_builder[num++] = (char)c;
					}
					else if (c < 128)
					{
						if (c != 92)
						{
							goto IL_163;
						}
						int num4;
						c = this.escape(c, out num4);
						if (Tokenizer.is_identifier_part_character((char)c))
						{
							this.id_builder[num++] = (char)c;
						}
						else if (num4 != 0)
						{
							c = num4;
						}
						else
						{
							if (c != 65279)
							{
								break;
							}
							this.putback_char = c;
						}
					}
					else
					{
						if (!Tokenizer.is_identifier_part_character_slow_part((char)c))
						{
							goto IL_163;
						}
						this.id_builder[num++] = (char)c;
					}
				}
				this.Report.Error(1056, this.Location, "Unexpected character `\\{0}'", c.ToString("x4"));
				return 259;
				IL_163:
				this.putback_char = c;
			}
			catch (IndexOutOfRangeException)
			{
				this.Report.Error(645, this.Location, "Identifier too long (limit is 512 chars)");
				num--;
				this.col += num;
			}
			this.col += num - 1;
			if (this.id_builder[0] >= '_' && !quoted)
			{
				int keyword = this.GetKeyword(this.id_builder, num);
				if (keyword != -1)
				{
					this.val = this.ltb.Create((keyword == 363) ? "await" : null, this.current_source, this.ref_line, num2);
					return keyword;
				}
			}
			string value = this.InternIdentifier(this.id_builder, num);
			this.val = this.ltb.Create(value, this.current_source, this.ref_line, num2);
			if (quoted && this.parsing_attribute_section)
			{
				this.AddEscapedIdentifier(((LocatedToken)this.val).Location);
			}
			return 422;
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x00065180 File Offset: 0x00063380
		private string InternIdentifier(char[] charBuffer, int length)
		{
			Dictionary<char[], string> dictionary = this.identifiers[length];
			string text;
			if (dictionary != null)
			{
				if (dictionary.TryGetValue(charBuffer, out text))
				{
					return text;
				}
			}
			else
			{
				dictionary = new Dictionary<char[], string>((length > 20) ? 10 : 100, new Tokenizer.IdentifiersComparer(length));
				this.identifiers[length] = dictionary;
			}
			char[] array = new char[length];
			Array.Copy(charBuffer, array, length);
			text = new string(charBuffer, 0, length);
			dictionary.Add(array, text);
			return text;
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x000651E8 File Offset: 0x000633E8
		public int xtoken()
		{
			if (this.parsing_interpolation_format)
			{
				return this.TokenizeInterpolationFormat();
			}
			bool flag = false;
			int @char;
			while ((@char = this.get_char()) != -1)
			{
				if (@char <= 160)
				{
					if (@char <= 94)
					{
						switch (@char)
						{
						case 0:
						case 11:
						case 12:
						case 32:
							continue;
						case 1:
						case 2:
						case 3:
						case 4:
						case 5:
						case 6:
						case 7:
						case 8:
						case 13:
						case 14:
						case 15:
						case 16:
						case 17:
						case 18:
						case 19:
						case 20:
						case 21:
						case 22:
						case 23:
						case 24:
						case 25:
						case 26:
						case 27:
						case 28:
						case 29:
						case 30:
						case 31:
							goto IL_D1C;
						case 9:
							this.col = (this.col - 1 + this.tab_size) / this.tab_size * this.tab_size;
							continue;
						case 10:
							goto IL_B2D;
						case 33:
							this.val = this.ltb.Create(this.current_source, this.ref_line, this.col);
							if (this.peek_char() == 61)
							{
								int char2 = this.get_char();
								return 403;
							}
							return 384;
						case 34:
							if (this.parsing_string_interpolation > 0 && !this.ScanClosingInterpolationBrace())
							{
								this.parsing_string_interpolation = 0;
								this.Report.Error(8076, this.Location, "Missing close delimiter `}' for interpolated expression");
								this.val = new StringLiteral(this.context.BuiltinTypes, "", this.Location);
								return 367;
							}
							return this.consume_string(false);
						case 35:
						{
							if (this.tokens_seen || flag)
							{
								this.Eror_WrongPreprocessorLocation();
								return 259;
							}
							if (this.ParsePreprocessingDirective(true))
							{
								continue;
							}
							bool flag2 = false;
							while ((@char = this.get_char()) != -1)
							{
								if (this.col == 1)
								{
									flag2 = true;
								}
								else if (!flag2)
								{
									continue;
								}
								if (@char != 32 && @char != 9 && @char != 10 && @char != 12 && @char != 11 && @char != 8232 && @char != 8233)
								{
									if (@char == 35 && this.ParsePreprocessingDirective(false))
									{
										break;
									}
									flag2 = false;
								}
							}
							if (@char != -1)
							{
								this.tokens_seen = false;
								continue;
							}
							return 257;
						}
						case 36:
						{
							int num = this.peek_char();
							if (num == 34)
							{
								int char3 = this.get_char();
								return this.TokenizeInterpolatedString(false);
							}
							if (num != 64)
							{
								goto IL_D1C;
							}
							int char4 = this.get_char();
							if (this.peek_char() == 34)
							{
								int char5 = this.get_char();
								return this.TokenizeInterpolatedString(true);
							}
							goto IL_D1C;
						}
						case 37:
							this.val = this.ltb.Create(this.current_source, this.ref_line, this.col);
							if (this.peek_char() == 61)
							{
								int char6 = this.get_char();
								return 408;
							}
							return 391;
						case 38:
						{
							this.val = this.ltb.Create(this.current_source, this.ref_line, this.col);
							int num2 = this.peek_char();
							if (num2 == 38)
							{
								int char7 = this.get_char();
								return 404;
							}
							if (num2 == 61)
							{
								int char8 = this.get_char();
								return 413;
							}
							return 388;
						}
						case 39:
							return this.TokenizeBackslash();
						case 40:
							this.val = this.ltb.Create(this.current_source, this.ref_line, this.col);
							if (this.parsing_block != 0 && !this.lambda_arguments_parsing)
							{
								int num = this.current_token;
								if (num <= 324)
								{
									if (num <= 277)
									{
										if (num != 276 && num != 277)
										{
											goto IL_438;
										}
									}
									else
									{
										switch (num)
										{
										case 289:
										case 290:
										case 292:
											break;
										case 291:
											goto IL_438;
										default:
											if (num != 324)
											{
												goto IL_438;
											}
											break;
										}
									}
								}
								else if (num <= 335)
								{
									if (num != 329 && num != 335)
									{
										goto IL_438;
									}
								}
								else if (num != 340 && num != 420 && num != 422)
								{
									goto IL_438;
								}
								return 375;
								IL_438:
								int num3 = this.peek_char();
								if (num3 <= 39)
								{
									if (num3 != 34 && num3 != 39)
									{
										goto IL_46C;
									}
								}
								else if (num3 != 40 && num3 != 48 && num3 != 49)
								{
									goto IL_46C;
								}
								return 375;
								IL_46C:
								this.lambda_arguments_parsing = true;
								this.PushPosition();
								int num2 = this.TokenizeOpenParens();
								this.PopPosition();
								this.lambda_arguments_parsing = false;
								return num2;
							}
							return 375;
						case 41:
							return 376;
						case 42:
							this.val = this.ltb.Create(this.current_source, this.ref_line, this.col);
							if (this.peek_char() == 61)
							{
								int char9 = this.get_char();
								return 406;
							}
							return 390;
						case 43:
						{
							this.val = this.ltb.Create(this.current_source, this.ref_line, this.col);
							int num2 = this.peek_char();
							if (num2 == 43)
							{
								num2 = 396;
							}
							else
							{
								if (num2 != 61)
								{
									return 382;
								}
								num2 = 409;
							}
							int char10 = this.get_char();
							return num2;
						}
						case 44:
							return 378;
						case 45:
						{
							this.val = this.ltb.Create(this.current_source, this.ref_line, this.col);
							int num2 = this.peek_char();
							if (num2 == 45)
							{
								num2 = 397;
							}
							else if (num2 == 61)
							{
								num2 = 410;
							}
							else
							{
								if (num2 != 62)
								{
									return 383;
								}
								num2 = 416;
							}
							int char11 = this.get_char();
							return num2;
						}
						case 46:
						{
							this.tokens_seen = true;
							int num2 = this.peek_char();
							if (num2 >= 48 && num2 <= 57)
							{
								return this.is_number(@char, true);
							}
							return 377;
						}
						case 47:
						{
							int num2 = this.peek_char();
							if (num2 == 61)
							{
								this.val = this.ltb.Create(this.current_source, this.ref_line, this.col);
								int char12 = this.get_char();
								return 407;
							}
							if (num2 == 47)
							{
								if (this.parsing_string_interpolation <= 0)
								{
									int char13 = this.get_char();
									if (this.doc_processing)
									{
										if (this.peek_char() == 47)
										{
											int char14 = this.get_char();
											if (this.peek_char() != 47)
											{
												if (this.doc_state == XmlCommentState.Allowed)
												{
													this.handle_one_line_xml_comment();
												}
												else if (this.doc_state == XmlCommentState.NotAllowed)
												{
													this.WarningMisplacedComment(this.Location - 3);
												}
											}
										}
										else if (this.xml_comment_buffer.Length > 0)
										{
											this.doc_state = XmlCommentState.NotAllowed;
										}
									}
									this.ReadToEndOfLine();
									this.any_token_seen |= this.tokens_seen;
									this.tokens_seen = false;
									flag = false;
									continue;
								}
								this.Report.Error(8077, this.Location, "A single-line comment may not be used in an interpolated string");
							}
							else
							{
								if (num2 != 42)
								{
									this.val = this.ltb.Create(this.current_source, this.ref_line, this.col);
									return 392;
								}
								int char15 = this.get_char();
								bool flag3 = false;
								if (this.doc_processing && this.peek_char() == 42)
								{
									int char16 = this.get_char();
									if (this.peek_char() == 47)
									{
										int char17 = this.get_char();
										continue;
									}
									if (this.doc_state == XmlCommentState.Allowed)
									{
										flag3 = true;
									}
									else if (this.doc_state == XmlCommentState.NotAllowed)
									{
										this.WarningMisplacedComment(this.Location - 2);
									}
								}
								int current_comment_start = 0;
								if (flag3)
								{
									current_comment_start = this.xml_comment_buffer.Length;
									this.xml_comment_buffer.Append(Environment.NewLine);
								}
								while ((num2 = this.get_char()) != -1)
								{
									if (num2 == 42 && this.peek_char() == 47)
									{
										int char18 = this.get_char();
										flag = true;
										break;
									}
									if (flag3)
									{
										this.xml_comment_buffer.Append((char)num2);
									}
									if (num2 == 10 || num2 == 8232 || num2 == 8233)
									{
										this.any_token_seen |= this.tokens_seen;
										this.tokens_seen = false;
										flag = false;
									}
								}
								if (!flag)
								{
									this.Report.Error(1035, this.Location, "End-of-file found, '*/' expected");
								}
								if (flag3)
								{
									this.update_formatted_doc_comment(current_comment_start);
									continue;
								}
								continue;
							}
							break;
						}
						case 48:
						case 49:
						case 50:
						case 51:
						case 52:
						case 53:
						case 54:
						case 55:
						case 56:
						case 57:
							this.tokens_seen = true;
							return this.is_number(@char, false);
						case 58:
							this.val = this.ltb.Create(this.current_source, this.ref_line, this.col);
							if (this.peek_char() == 58)
							{
								int char19 = this.get_char();
								return 395;
							}
							return 379;
						case 59:
							return 380;
						case 60:
						{
							this.val = this.ltb.Create(this.current_source, this.ref_line, this.col);
							int num = this.parsing_generic_less_than;
							this.parsing_generic_less_than = num + 1;
							if (num > 0)
							{
								return 418;
							}
							return this.TokenizeLessThan();
						}
						case 61:
						{
							this.val = this.ltb.Create(this.current_source, this.ref_line, this.col);
							int num2 = this.peek_char();
							if (num2 == 61)
							{
								int char20 = this.get_char();
								return 402;
							}
							if (num2 == 62)
							{
								int char21 = this.get_char();
								return 343;
							}
							return 385;
						}
						case 62:
						{
							this.val = this.ltb.Create(this.current_source, this.ref_line, this.col);
							int num2 = this.peek_char();
							if (num2 == 61)
							{
								int char22 = this.get_char();
								return 401;
							}
							if (this.parsing_generic_less_than > 1 || (this.parsing_generic_less_than == 1 && num2 != 62))
							{
								this.parsing_generic_less_than--;
								return 420;
							}
							if (num2 != 62)
							{
								return 387;
							}
							int char23 = this.get_char();
							num2 = this.peek_char();
							if (num2 == 61)
							{
								int char24 = this.get_char();
								return 412;
							}
							return 399;
						}
						case 63:
							this.val = this.ltb.Create(this.current_source, this.ref_line, this.col);
							return this.TokenizePossibleNullableType();
						case 64:
							@char = this.get_char();
							if (@char == 34)
							{
								this.tokens_seen = true;
								return this.consume_string(true);
							}
							if (Tokenizer.is_identifier_start_character(@char))
							{
								return this.consume_identifier(@char, true);
							}
							this.Report.Error(1646, this.Location, "Keyword, identifier, or string expected after verbatim specifier: @");
							return 259;
						default:
							switch (@char)
							{
							case 91:
							{
								if (this.doc_state == XmlCommentState.Allowed)
								{
									this.doc_state = XmlCommentState.NotAllowed;
								}
								this.val = this.ltb.Create(this.current_source, this.ref_line, this.col);
								if (this.parsing_block == 0 || this.lambda_arguments_parsing)
								{
									return 373;
								}
								int num4 = this.peek_char();
								if (num4 <= 44)
								{
									switch (num4)
									{
									case 10:
									case 11:
									case 12:
									case 13:
										goto IL_33C;
									default:
										if (num4 == 32)
										{
											goto IL_33C;
										}
										if (num4 != 44)
										{
											return 427;
										}
										break;
									}
								}
								else if (num4 <= 93)
								{
									if (num4 == 47)
									{
										goto IL_33C;
									}
									if (num4 != 93)
									{
										return 427;
									}
								}
								else
								{
									if (num4 != 8232 && num4 != 8233)
									{
										return 427;
									}
									goto IL_33C;
								}
								return 373;
								IL_33C:
								num4 = this.peek_token();
								if (num4 == 378 || num4 == 374)
								{
									return 373;
								}
								return 427;
							}
							case 92:
								this.tokens_seen = true;
								return this.consume_identifier(@char);
							case 93:
								return 374;
							case 94:
								this.val = this.ltb.Create(this.current_source, this.ref_line, this.col);
								if (this.peek_char() == 61)
								{
									int char25 = this.get_char();
									return 414;
								}
								return 393;
							default:
								goto IL_D1C;
							}
							break;
						}
					}
					else
					{
						switch (@char)
						{
						case 123:
							this.val = this.ltb.Create(this.current_source, this.ref_line, this.col);
							return 371;
						case 124:
						{
							this.val = this.ltb.Create(this.current_source, this.ref_line, this.col);
							int num2 = this.peek_char();
							if (num2 == 124)
							{
								int char26 = this.get_char();
								return 405;
							}
							if (num2 == 61)
							{
								int char27 = this.get_char();
								return 415;
							}
							return 389;
						}
						case 125:
							break;
						case 126:
							this.val = this.ltb.Create(this.current_source, this.ref_line, this.col);
							return 381;
						default:
							if (@char != 160)
							{
								goto IL_D1C;
							}
							continue;
						}
					}
					if (this.parsing_string_interpolation > 0)
					{
						this.parsing_string_interpolation--;
						bool quoted = this.parsing_string_interpolation_quoted != null && this.parsing_string_interpolation_quoted.Count > 0 && this.parsing_string_interpolation_quoted.Pop();
						return this.TokenizeInterpolatedString(quoted);
					}
					this.val = this.ltb.Create(this.current_source, this.ref_line, this.col);
					return 372;
				}
				else if (@char <= 8233)
				{
					if (@char != 8232 && @char != 8233)
					{
						goto IL_D1C;
					}
				}
				else
				{
					if (@char == 65279)
					{
						continue;
					}
					switch (@char)
					{
					case 1048576:
						return 428;
					case 1048577:
						return 429;
					case 1048578:
						return 430;
					case 1048579:
						return 431;
					default:
						goto IL_D1C;
					}
				}
				IL_B2D:
				this.any_token_seen |= this.tokens_seen;
				this.tokens_seen = false;
				flag = false;
				continue;
				IL_D1C:
				if (Tokenizer.is_identifier_start_character(@char))
				{
					this.tokens_seen = true;
					return this.consume_identifier(@char);
				}
				if (!char.IsWhiteSpace((char)@char))
				{
					this.Report.Error(1056, this.Location, "Unexpected character `{0}'", ((char)@char).ToString());
				}
			}
			if (!this.CompleteOnEOF)
			{
				return 257;
			}
			if (this.generated)
			{
				return 433;
			}
			this.generated = true;
			return 432;
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x00065F90 File Offset: 0x00064190
		private int TokenizeBackslash()
		{
			Location location = this.Location;
			int num = this.get_char();
			this.tokens_seen = true;
			if (num == 39)
			{
				this.val = new CharLiteral(this.context.BuiltinTypes, (char)num, location);
				this.Report.Error(1011, location, "Empty character literal");
				return 421;
			}
			if (num == 10 || num == 8232 || num == 8233)
			{
				this.Report.Error(1010, location, "Newline in constant");
				return 259;
			}
			int num2;
			num = this.escape(num, out num2);
			if (num == -1)
			{
				return 259;
			}
			if (num2 != 0)
			{
				throw new NotImplementedException();
			}
			ILiteralConstant literalConstant = new CharLiteral(this.context.BuiltinTypes, (char)num, location);
			this.val = literalConstant;
			num = this.get_char();
			if (num != 39)
			{
				this.Report.Error(1012, location, "Too many characters in character literal");
				while ((num = this.get_char()) != -1 && num != 10 && num != 39 && num != 8232 && num != 8233)
				{
				}
			}
			return 421;
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x000660A4 File Offset: 0x000642A4
		private int TokenizeLessThan()
		{
			this.PushPosition();
			int num = 0;
			int num2;
			if (this.parse_less_than(ref num))
			{
				if (this.parsing_generic_declaration && (this.parsing_generic_declaration_doc || this.token() != 377))
				{
					num2 = 419;
				}
				else
				{
					if (num > 0)
					{
						this.val = num;
						this.DiscardPosition();
						return 425;
					}
					num2 = 418;
				}
				this.PopPosition();
				return num2;
			}
			this.PopPosition();
			this.parsing_generic_less_than = 0;
			num2 = this.peek_char();
			if (num2 == 60)
			{
				int @char = this.get_char();
				num2 = this.peek_char();
				if (num2 == 61)
				{
					int char2 = this.get_char();
					return 411;
				}
				return 398;
			}
			else
			{
				if (num2 == 61)
				{
					int char3 = this.get_char();
					return 400;
				}
				return 386;
			}
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x00066168 File Offset: 0x00064368
		private int TokenizeInterpolatedString(bool quoted)
		{
			int num = 0;
			Location location = this.Location;
			for (;;)
			{
				int num2 = this.get_char();
				if (num2 <= 34)
				{
					if (num2 == -1)
					{
						return 257;
					}
					if (num2 == 34)
					{
						if (!quoted || this.peek_char() != 34)
						{
							break;
						}
						int @char = this.get_char();
					}
				}
				else if (num2 != 92)
				{
					if (num2 == 123)
					{
						if (this.peek_char() != 123)
						{
							goto IL_99;
						}
						this.value_builder[num++] = (char)num2;
						int char2 = this.get_char();
					}
				}
				else if (!quoted)
				{
					this.col++;
					int num3;
					num2 = this.escape(num2, out num3);
					if (num2 == -1)
					{
						return 259;
					}
					if (num2 == 123 || num2 == 125)
					{
						this.Report.Error(8087, this.Location, "A `{0}' character may only be escaped by doubling `{0}{0}' in an interpolated string", ((char)num2).ToString());
					}
					if (num3 != 0)
					{
						if (num == this.value_builder.Length)
						{
							Array.Resize<char>(ref this.value_builder, num * 2);
						}
						if (num == this.value_builder.Length)
						{
							Array.Resize<char>(ref this.value_builder, num * 2);
						}
						this.value_builder[num++] = (char)num2;
						num2 = num3;
					}
				}
				this.col++;
				if (num == this.value_builder.Length)
				{
					Array.Resize<char>(ref this.value_builder, num * 2);
				}
				this.value_builder[num++] = (char)num2;
			}
			this.val = new StringLiteral(this.context.BuiltinTypes, this.CreateStringFromBuilder(num), location);
			return 367;
			IL_99:
			this.parsing_string_interpolation++;
			if (quoted && this.parsing_string_interpolation_quoted == null)
			{
				this.parsing_string_interpolation_quoted = new Stack<bool>();
			}
			if (this.parsing_string_interpolation_quoted != null)
			{
				this.parsing_string_interpolation_quoted.Push(quoted);
			}
			this.val = new StringLiteral(this.context.BuiltinTypes, this.CreateStringFromBuilder(num), location);
			return 366;
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x00066348 File Offset: 0x00064548
		private int TokenizeInterpolationFormat()
		{
			int num = 0;
			int num2 = 0;
			int num3;
			for (;;)
			{
				num3 = this.get_char();
				if (num3 <= 92)
				{
					if (num3 == -1)
					{
						return 257;
					}
					if (num3 == 92)
					{
						this.col++;
						int num4;
						num3 = this.escape(num3, out num4);
						if (num3 == -1)
						{
							return 259;
						}
						if (num3 == 123 || num3 == 125)
						{
							this.Report.Error(8087, this.Location, "A `{0}' character may only be escaped by doubling `{0}{0}' in an interpolated string", ((char)num3).ToString());
						}
						if (num4 != 0)
						{
							if (num == this.value_builder.Length)
							{
								Array.Resize<char>(ref this.value_builder, num * 2);
							}
							this.value_builder[num++] = (char)num3;
							num3 = num4;
						}
					}
				}
				else if (num3 != 123)
				{
					if (num3 == 125)
					{
						if (num2 == 0)
						{
							break;
						}
						num2--;
					}
				}
				else
				{
					num2++;
				}
				this.col++;
				this.value_builder[num++] = (char)num3;
			}
			this.putback_char = num3;
			if (num == 0)
			{
				this.Report.Error(8089, this.Location, "Empty interpolated expression format specifier");
			}
			else if (Array.IndexOf<char>(Tokenizer.simple_whitespaces, this.value_builder[num - 1]) >= 0)
			{
				this.Report.Error(8088, this.Location, "A interpolated expression format specifier may not contain trailing whitespace");
			}
			this.val = this.CreateStringFromBuilder(num);
			return 421;
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x000664AF File Offset: 0x000646AF
		private string CreateStringFromBuilder(int pos)
		{
			if (pos == 0)
			{
				return string.Empty;
			}
			if (pos <= 4)
			{
				return this.InternIdentifier(this.value_builder, pos);
			}
			return new string(this.value_builder, 0, pos);
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x000664DC File Offset: 0x000646DC
		private void handle_one_line_xml_comment()
		{
			while (this.peek_char() == 32)
			{
				int @char = this.get_char();
			}
			int num;
			while ((num = this.peek_char()) != -1 && num != 10 && num != 13)
			{
				this.xml_comment_buffer.Append((char)this.get_char());
			}
			if (num == 13 || num == 10)
			{
				this.xml_comment_buffer.Append(Environment.NewLine);
			}
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x00066544 File Offset: 0x00064744
		private void update_formatted_doc_comment(int current_comment_start)
		{
			int length = this.xml_comment_buffer.Length - current_comment_start;
			string[] array = this.xml_comment_buffer.ToString(current_comment_start, length).Replace("\r", "").Split(new char[]
			{
				'\n'
			});
			for (int i = 1; i < array.Length; i++)
			{
				string text = array[i];
				int num = text.IndexOf('*');
				string text2;
				if (num < 0)
				{
					if (i < array.Length - 1)
					{
						return;
					}
					text2 = text;
				}
				else
				{
					text2 = text.Substring(0, num);
				}
				string text3 = text2;
				for (int j = 0; j < text3.Length; j++)
				{
					if (text3[j] != ' ')
					{
						return;
					}
				}
				array[i] = text.Substring(num + 1);
			}
			this.xml_comment_buffer.Remove(current_comment_start, length);
			this.xml_comment_buffer.Insert(current_comment_start, string.Join(Environment.NewLine, array));
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x00066624 File Offset: 0x00064824
		public void check_incorrect_doc_comment()
		{
			if (this.xml_comment_buffer.Length > 0)
			{
				this.WarningMisplacedComment(this.Location);
			}
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x00066640 File Offset: 0x00064840
		public string consume_doc_comment()
		{
			if (this.xml_comment_buffer.Length > 0)
			{
				string result = this.xml_comment_buffer.ToString();
				this.reset_doc_comment();
				return result;
			}
			return null;
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x00066663 File Offset: 0x00064863
		private void reset_doc_comment()
		{
			this.xml_comment_buffer.Length = 0;
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x00066674 File Offset: 0x00064874
		public void cleanup()
		{
			if (this.ifstack != null && this.ifstack.Count >= 1)
			{
				if ((this.ifstack.Pop() & 16) != 0)
				{
					this.Report.Error(1038, this.Location, "#endregion directive expected");
					return;
				}
				this.Report.Error(1027, this.Location, "Expected `#endif' directive");
			}
		}

		// Token: 0x040008B4 RID: 2228
		private readonly SeekableStreamReader reader;

		// Token: 0x040008B5 RID: 2229
		private readonly CompilationSourceFile source_file;

		// Token: 0x040008B6 RID: 2230
		private readonly CompilerContext context;

		// Token: 0x040008B7 RID: 2231
		private readonly Report Report;

		// Token: 0x040008B8 RID: 2232
		private SourceFile current_source;

		// Token: 0x040008B9 RID: 2233
		private Location hidden_block_start;

		// Token: 0x040008BA RID: 2234
		private int ref_line = 1;

		// Token: 0x040008BB RID: 2235
		private int line = 1;

		// Token: 0x040008BC RID: 2236
		private int col;

		// Token: 0x040008BD RID: 2237
		private int previous_col;

		// Token: 0x040008BE RID: 2238
		private int current_token;

		// Token: 0x040008BF RID: 2239
		private readonly int tab_size;

		// Token: 0x040008C0 RID: 2240
		private bool handle_get_set;

		// Token: 0x040008C1 RID: 2241
		private bool handle_remove_add;

		// Token: 0x040008C2 RID: 2242
		private bool handle_where;

		// Token: 0x040008C3 RID: 2243
		private bool lambda_arguments_parsing;

		// Token: 0x040008C4 RID: 2244
		private List<Location> escaped_identifiers;

		// Token: 0x040008C5 RID: 2245
		private int parsing_generic_less_than;

		// Token: 0x040008C6 RID: 2246
		private readonly bool doc_processing;

		// Token: 0x040008C7 RID: 2247
		private readonly Tokenizer.LocatedTokenBuffer ltb;

		// Token: 0x040008C8 RID: 2248
		public int parsing_block;

		// Token: 0x040008C9 RID: 2249
		public bool query_parsing;

		// Token: 0x040008CA RID: 2250
		public int parsing_type;

		// Token: 0x040008CB RID: 2251
		public bool parsing_generic_declaration;

		// Token: 0x040008CC RID: 2252
		public bool parsing_generic_declaration_doc;

		// Token: 0x040008CD RID: 2253
		public int parsing_declaration;

		// Token: 0x040008CE RID: 2254
		public bool parsing_attribute_section;

		// Token: 0x040008CF RID: 2255
		public bool parsing_modifiers;

		// Token: 0x040008D0 RID: 2256
		public bool parsing_catch_when;

		// Token: 0x040008D1 RID: 2257
		private int parsing_string_interpolation;

		// Token: 0x040008D2 RID: 2258
		private Stack<bool> parsing_string_interpolation_quoted;

		// Token: 0x040008D3 RID: 2259
		public bool parsing_interpolation_format;

		// Token: 0x040008D4 RID: 2260
		public const int EvalStatementParserCharacter = 1048576;

		// Token: 0x040008D5 RID: 2261
		public const int EvalCompilationUnitParserCharacter = 1048577;

		// Token: 0x040008D6 RID: 2262
		public const int EvalUsingDeclarationsParserCharacter = 1048578;

		// Token: 0x040008D7 RID: 2263
		public const int DocumentationXref = 1048579;

		// Token: 0x040008D8 RID: 2264
		private const int UnicodeLS = 8232;

		// Token: 0x040008D9 RID: 2265
		private const int UnicodePS = 8233;

		// Token: 0x040008DA RID: 2266
		private StringBuilder xml_comment_buffer;

		// Token: 0x040008DB RID: 2267
		private XmlCommentState xml_doc_state;

		// Token: 0x040008DC RID: 2268
		private bool tokens_seen;

		// Token: 0x040008DD RID: 2269
		private bool generated;

		// Token: 0x040008DE RID: 2270
		private bool any_token_seen;

		// Token: 0x040008DF RID: 2271
		private static readonly Tokenizer.KeywordEntry<int>[][] keywords;

		// Token: 0x040008E0 RID: 2272
		private static readonly Tokenizer.KeywordEntry<Tokenizer.PreprocessorDirective>[][] keywords_preprocessor;

		// Token: 0x040008E1 RID: 2273
		private static readonly HashSet<string> keyword_strings;

		// Token: 0x040008E2 RID: 2274
		private static readonly NumberStyles styles;

		// Token: 0x040008E3 RID: 2275
		private static readonly NumberFormatInfo csharp_format_info;

		// Token: 0x040008E4 RID: 2276
		private static readonly char[] pragma_warning = "warning".ToCharArray();

		// Token: 0x040008E5 RID: 2277
		private static readonly char[] pragma_warning_disable = "disable".ToCharArray();

		// Token: 0x040008E6 RID: 2278
		private static readonly char[] pragma_warning_restore = "restore".ToCharArray();

		// Token: 0x040008E7 RID: 2279
		private static readonly char[] pragma_checksum = "checksum".ToCharArray();

		// Token: 0x040008E8 RID: 2280
		private static readonly char[] line_hidden = "hidden".ToCharArray();

		// Token: 0x040008E9 RID: 2281
		private static readonly char[] line_default = "default".ToCharArray();

		// Token: 0x040008EA RID: 2282
		private static readonly char[] simple_whitespaces = new char[]
		{
			' ',
			'\t'
		};

		// Token: 0x040008EB RID: 2283
		public bool CompleteOnEOF;

		// Token: 0x040008EC RID: 2284
		public int putback_char;

		// Token: 0x040008ED RID: 2285
		private object val;

		// Token: 0x040008EE RID: 2286
		private const int TAKING = 1;

		// Token: 0x040008EF RID: 2287
		private const int ELSE_SEEN = 4;

		// Token: 0x040008F0 RID: 2288
		private const int PARENT_TAKING = 8;

		// Token: 0x040008F1 RID: 2289
		private const int REGION = 16;

		// Token: 0x040008F2 RID: 2290
		private Stack<int> ifstack;

		// Token: 0x040008F3 RID: 2291
		public const int MaxIdentifierLength = 512;

		// Token: 0x040008F4 RID: 2292
		public const int MaxNumberLength = 512;

		// Token: 0x040008F5 RID: 2293
		private readonly char[] id_builder;

		// Token: 0x040008F6 RID: 2294
		private readonly Dictionary<char[], string>[] identifiers;

		// Token: 0x040008F7 RID: 2295
		private readonly char[] number_builder;

		// Token: 0x040008F8 RID: 2296
		private int number_pos;

		// Token: 0x040008F9 RID: 2297
		private char[] value_builder = new char[64];

		// Token: 0x040008FA RID: 2298
		private Stack<Tokenizer.Position> position_stack = new Stack<Tokenizer.Position>(2);

		// Token: 0x0200039C RID: 924
		private class KeywordEntry<T>
		{
			// Token: 0x060026DD RID: 9949 RVA: 0x000BA9E4 File Offset: 0x000B8BE4
			public KeywordEntry(string value, T token)
			{
				this.Value = value.ToCharArray();
				this.Token = token;
			}

			// Token: 0x04000FB3 RID: 4019
			public readonly T Token;

			// Token: 0x04000FB4 RID: 4020
			public Tokenizer.KeywordEntry<T> Next;

			// Token: 0x04000FB5 RID: 4021
			public readonly char[] Value;
		}

		// Token: 0x0200039D RID: 925
		private sealed class IdentifiersComparer : IEqualityComparer<char[]>
		{
			// Token: 0x060026DE RID: 9950 RVA: 0x000BA9FF File Offset: 0x000B8BFF
			public IdentifiersComparer(int length)
			{
				this.length = length;
			}

			// Token: 0x060026DF RID: 9951 RVA: 0x000BAA10 File Offset: 0x000B8C10
			public bool Equals(char[] x, char[] y)
			{
				for (int i = 0; i < this.length; i++)
				{
					if (x[i] != y[i])
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x060026E0 RID: 9952 RVA: 0x000BAA3C File Offset: 0x000B8C3C
			public int GetHashCode(char[] obj)
			{
				int num = 0;
				for (int i = 0; i < this.length; i++)
				{
					num = (num << 5) - num + (int)obj[i];
				}
				return num;
			}

			// Token: 0x04000FB6 RID: 4022
			private readonly int length;
		}

		// Token: 0x0200039E RID: 926
		public class LocatedTokenBuffer
		{
			// Token: 0x060026E1 RID: 9953 RVA: 0x000BAA67 File Offset: 0x000B8C67
			public LocatedTokenBuffer()
			{
				this.buffer = new LocatedToken[0];
			}

			// Token: 0x060026E2 RID: 9954 RVA: 0x000BAA7B File Offset: 0x000B8C7B
			public LocatedTokenBuffer(LocatedToken[] buffer)
			{
				this.buffer = (buffer ?? new LocatedToken[0]);
			}

			// Token: 0x060026E3 RID: 9955 RVA: 0x000BAA94 File Offset: 0x000B8C94
			public LocatedToken Create(SourceFile file, int row, int column)
			{
				return this.Create(null, file, row, column);
			}

			// Token: 0x060026E4 RID: 9956 RVA: 0x000BAAA0 File Offset: 0x000B8CA0
			public LocatedToken Create(string value, SourceFile file, int row, int column)
			{
				LocatedToken locatedToken;
				if (this.pos >= this.buffer.Length)
				{
					locatedToken = new LocatedToken();
				}
				else
				{
					locatedToken = this.buffer[this.pos];
					if (locatedToken == null)
					{
						locatedToken = new LocatedToken();
						this.buffer[this.pos] = locatedToken;
					}
					this.pos++;
				}
				locatedToken.value = value;
				locatedToken.file = file;
				locatedToken.row = row;
				locatedToken.column = column;
				return locatedToken;
			}

			// Token: 0x060026E5 RID: 9957 RVA: 0x000BAB16 File Offset: 0x000B8D16
			[Conditional("FULL_AST")]
			public void CreateOptional(SourceFile file, int row, int col, ref object token)
			{
				token = this.Create(file, row, col);
			}

			// Token: 0x04000FB7 RID: 4023
			private readonly LocatedToken[] buffer;

			// Token: 0x04000FB8 RID: 4024
			public int pos;
		}

		// Token: 0x0200039F RID: 927
		public enum PreprocessorDirective
		{
			// Token: 0x04000FBA RID: 4026
			Invalid,
			// Token: 0x04000FBB RID: 4027
			Region,
			// Token: 0x04000FBC RID: 4028
			Endregion,
			// Token: 0x04000FBD RID: 4029
			If = 2051,
			// Token: 0x04000FBE RID: 4030
			Endif = 4,
			// Token: 0x04000FBF RID: 4031
			Elif = 2053,
			// Token: 0x04000FC0 RID: 4032
			Else = 6,
			// Token: 0x04000FC1 RID: 4033
			Define = 2055,
			// Token: 0x04000FC2 RID: 4034
			Undef,
			// Token: 0x04000FC3 RID: 4035
			Error = 9,
			// Token: 0x04000FC4 RID: 4036
			Warning,
			// Token: 0x04000FC5 RID: 4037
			Pragma = 1035,
			// Token: 0x04000FC6 RID: 4038
			Line,
			// Token: 0x04000FC7 RID: 4039
			CustomArgumentsParsing = 1024,
			// Token: 0x04000FC8 RID: 4040
			RequiresArgument = 2048
		}

		// Token: 0x020003A0 RID: 928
		private class Position
		{
			// Token: 0x060026E6 RID: 9958 RVA: 0x000BAB24 File Offset: 0x000B8D24
			public Position(Tokenizer t)
			{
				this.position = t.reader.Position;
				this.line = t.line;
				this.ref_line = t.ref_line;
				this.col = t.col;
				this.hidden = t.hidden_block_start;
				this.putback_char = t.putback_char;
				this.previous_col = t.previous_col;
				if (t.ifstack != null && t.ifstack.Count != 0)
				{
					int[] array = t.ifstack.ToArray();
					Array.Reverse(array);
					this.ifstack = new Stack<int>(array);
				}
				this.parsing_generic_less_than = t.parsing_generic_less_than;
				this.current_token = t.current_token;
				this.val = t.val;
				this.parsing_string_interpolation = t.parsing_string_interpolation;
				if (t.parsing_string_interpolation_quoted != null && t.parsing_string_interpolation_quoted.Count != 0)
				{
					bool[] array2 = t.parsing_string_interpolation_quoted.ToArray();
					Array.Reverse(array2);
					this.parsing_string_interpolation_quoted = new Stack<bool>(array2);
				}
			}

			// Token: 0x04000FC9 RID: 4041
			public int position;

			// Token: 0x04000FCA RID: 4042
			public int line;

			// Token: 0x04000FCB RID: 4043
			public int ref_line;

			// Token: 0x04000FCC RID: 4044
			public int col;

			// Token: 0x04000FCD RID: 4045
			public Location hidden;

			// Token: 0x04000FCE RID: 4046
			public int putback_char;

			// Token: 0x04000FCF RID: 4047
			public int previous_col;

			// Token: 0x04000FD0 RID: 4048
			public Stack<int> ifstack;

			// Token: 0x04000FD1 RID: 4049
			public int parsing_generic_less_than;

			// Token: 0x04000FD2 RID: 4050
			public int current_token;

			// Token: 0x04000FD3 RID: 4051
			public object val;

			// Token: 0x04000FD4 RID: 4052
			public int parsing_string_interpolation;

			// Token: 0x04000FD5 RID: 4053
			public Stack<bool> parsing_string_interpolation_quoted;
		}
	}
}
