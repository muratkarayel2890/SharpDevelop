using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using ICSharpCode.NRefactory.Ast;
using ICSharpCode.NRefactory.Parser.VB;
using ASTAttribute = ICSharpCode.NRefactory.Ast.Attribute;



namespace ICSharpCode.NRefactory.Parser.VBNet.Experimental {



partial class ExpressionFinder {

	const bool T = true;
	const bool x = false;

int currentState = 0;

	readonly Stack<int> stateStack = new Stack<int>();
	bool nextTokenIsPotentialStartOfXmlMode = false;
	bool readXmlIdentifier = false;
	
	public ExpressionFinder()
	{
		stateStack.Push(-1); // required so that we don't crash when leaving the root production
	}

	void Expect(int expectedKind, Token t)
	{
		if (t.kind != expectedKind)
			Error(t);
	}
	
	void Error(Token t) 
	{
	}
	
	Token consumedEndToken;
	
	public void InformToken(Token t) 
	{
		if (consumedEndToken != null) {
			if (t.kind == Tokens.EOL || t.kind == Tokens.Colon) {
				consumedEndToken = null;
				return; // ignore End statement
			}
			InformTokenInternal(consumedEndToken);
			consumedEndToken = null;
		}
		if (t.kind == Tokens.End) {
			consumedEndToken = t;
		} else {
			InformTokenInternal(t);
		}
	}

	void InformTokenInternal(Token t) 
	{
		nextTokenIsPotentialStartOfXmlMode = false;
		ApplyToken(t);
		switchlbl: switch (currentState) {
			case 0: {
				PushContext(Context.Global, t);
				goto case 1;
			}
			case 1: {
				if (t == null) { currentState = 1; break; }
				if (t.kind == 172) {
					stateStack.Push(1);
					goto case 327;
				} else {
					goto case 2;
				}
			}
			case 2: {
				if (t == null) { currentState = 2; break; }
				if (t.kind == 136) {
					stateStack.Push(2);
					goto case 324;
				} else {
					goto case 3;
				}
			}
			case 3: {
				if (t == null) { currentState = 3; break; }
				if (t.kind == 39) {
					stateStack.Push(3);
					goto case 257;
				} else {
					goto case 4;
				}
			}
			case 4: {
				if (t == null) { currentState = 4; break; }
				if (set[0, t.kind]) {
					stateStack.Push(4);
					goto case 5;
				} else {
					PopContext();
					currentState = stateStack.Pop();
					goto switchlbl;
				}
			}
			case 5: {
				if (t == null) { currentState = 5; break; }
				if (t.kind == 159) {
					goto case 320;
				} else {
					if (set[1, t.kind]) {
						goto case 7;
					} else {
						goto case 6;
					}
				}
			}
			case 6: {
				Error(t);
				currentState = stateStack.Pop();
				goto switchlbl;
			}
			case 7: {
				if (t == null) { currentState = 7; break; }
				if (t.kind == 39) {
					stateStack.Push(7);
					goto case 257;
				} else {
					goto case 8;
				}
			}
			case 8: {
				if (t == null) { currentState = 8; break; }
				if (set[2, t.kind]) {
					currentState = 8;
					break;
				} else {
					if (t.kind == 83 || t.kind == 154) {
						goto case 319;
					} else {
						Error(t);
						goto case 9;
					}
				}
			}
			case 9: {
				if (t == null) { currentState = 9; break; }
				if (set[3, t.kind]) {
					goto case 319;
				} else {
					stateStack.Push(10);
					goto case 15;
				}
			}
			case 10: {
				PushContext(Context.Type, t);
				goto case 11;
			}
			case 11: {
				if (t == null) { currentState = 11; break; }
				if (set[4, t.kind]) {
					stateStack.Push(11);
					goto case 17;
				} else {
					Expect(112, t); // "End"
					currentState = 12;
					break;
				}
			}
			case 12: {
				if (t == null) { currentState = 12; break; }
				if (t.kind == 83 || t.kind == 154) {
					currentState = 13;
					break;
				} else {
					Error(t);
					goto case 13;
				}
			}
			case 13: {
				stateStack.Push(14);
				goto case 15;
			}
			case 14: {
				PopContext();
				currentState = stateStack.Pop();
				goto switchlbl;
			}
			case 15: {
				if (t == null) { currentState = 15; break; }
				if (t.kind == 1 || t.kind == 22) {
					goto case 16;
				} else {
					goto case 6;
				}
			}
			case 16: {
				if (t == null) { currentState = 16; break; }
				currentState = stateStack.Pop();
				break;
			}
			case 17: {
				PushContext(Context.Member, t);
				goto case 18;
			}
			case 18: {
				if (t == null) { currentState = 18; break; }
				if (t.kind == 39) {
					stateStack.Push(18);
					goto case 257;
				} else {
					goto case 19;
				}
			}
			case 19: {
				if (t == null) { currentState = 19; break; }
				if (set[5, t.kind]) {
					currentState = 19;
					break;
				} else {
					if (set[6, t.kind]) {
						stateStack.Push(20);
						goto case 312;
					} else {
						if (t.kind == 126 || t.kind == 208) {
							stateStack.Push(20);
							goto case 300;
						} else {
							if (t.kind == 100) {
								stateStack.Push(20);
								goto case 289;
							} else {
								if (t.kind == 118) {
									stateStack.Push(20);
									goto case 279;
								} else {
									if (t.kind == 97) {
										stateStack.Push(20);
										goto case 267;
									} else {
										if (t.kind == 171) {
											stateStack.Push(20);
											goto case 21;
										} else {
											Error(t);
											goto case 20;
										}
									}
								}
							}
						}
					}
				}
			}
			case 20: {
				PopContext();
				currentState = stateStack.Pop();
				goto switchlbl;
			}
			case 21: {
				if (t == null) { currentState = 21; break; }
				Expect(171, t); // "Operator"
				currentState = 22;
				break;
			}
			case 22: {
				if (t == null) { currentState = 22; break; }
				currentState = 23;
				break;
			}
			case 23: {
				if (t == null) { currentState = 23; break; }
				Expect(36, t); // "("
				currentState = 24;
				break;
			}
			case 24: {
				stateStack.Push(25);
				goto case 262;
			}
			case 25: {
				if (t == null) { currentState = 25; break; }
				Expect(37, t); // ")"
				currentState = 26;
				break;
			}
			case 26: {
				if (t == null) { currentState = 26; break; }
				if (t.kind == 62) {
					currentState = 256;
					break;
				} else {
					goto case 27;
				}
			}
			case 27: {
				stateStack.Push(28);
				goto case 31;
			}
			case 28: {
				if (t == null) { currentState = 28; break; }
				Expect(112, t); // "End"
				currentState = 29;
				break;
			}
			case 29: {
				if (t == null) { currentState = 29; break; }
				Expect(171, t); // "Operator"
				currentState = 30;
				break;
			}
			case 30: {
				goto case 15;
			}
			case 31: {
				PushContext(Context.Body, t);
				goto case 32;
			}
			case 32: {
				stateStack.Push(33);
				goto case 15;
			}
			case 33: {
				if (t == null) { currentState = 33; break; }
				if (set[7, t.kind]) {
					if (set[8, t.kind]) {
						stateStack.Push(32);
						goto case 34;
					} else {
						goto case 32;
					}
				} else {
					PopContext();
					currentState = stateStack.Pop();
					goto switchlbl;
				}
			}
			case 34: {
				if (t == null) { currentState = 34; break; }
				if (t.kind == 87 || t.kind == 104 || t.kind == 202) {
					goto case 239;
				} else {
					if (t.kind == 209 || t.kind == 231) {
						goto case 234;
					} else {
						if (t.kind == 55 || t.kind == 191) {
							goto case 231;
						} else {
							if (t.kind == 187) {
								goto case 228;
							} else {
								if (t.kind == 134) {
									goto case 209;
								} else {
									if (t.kind == 195) {
										goto case 194;
									} else {
										if (t.kind == 229) {
											goto case 189;
										} else {
											if (t.kind == 107) {
												goto case 180;
											} else {
												if (t.kind == 123) {
													goto case 153;
												} else {
													if (t.kind == 117 || t.kind == 170 || t.kind == 192) {
														goto case 145;
													} else {
														if (t.kind == 213) {
															goto case 143;
														} else {
															if (t.kind == 216) {
																goto case 132;
															} else {
																if (set[9, t.kind]) {
																	goto case 126;
																} else {
																	if (set[10, t.kind]) {
																		goto case 35;
																	} else {
																		goto case 6;
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			case 35: {
				if (t == null) { currentState = 35; break; }
				if (t.kind == 72) {
					goto case 125;
				} else {
					goto case 36;
				}
			}
			case 36: {
				goto case 37;
			}
			case 37: {
				nextTokenIsPotentialStartOfXmlMode = true;
				goto case 38;
			}
			case 38: {
				if (t == null) { currentState = 38; break; }
				if (set[11, t.kind]) {
					goto case 48;
				} else {
					if (t.kind == 134) {
						goto case 39;
					} else {
						goto case 6;
					}
				}
			}
			case 39: {
				if (t == null) { currentState = 39; break; }
				Expect(134, t); // "If"
				currentState = 40;
				break;
			}
			case 40: {
				if (t == null) { currentState = 40; break; }
				Expect(36, t); // "("
				currentState = 41;
				break;
			}
			case 41: {
				stateStack.Push(42);
				goto case 37;
			}
			case 42: {
				if (t == null) { currentState = 42; break; }
				Expect(23, t); // ","
				currentState = 43;
				break;
			}
			case 43: {
				stateStack.Push(44);
				goto case 37;
			}
			case 44: {
				if (t == null) { currentState = 44; break; }
				if (t.kind == 23) {
					goto case 46;
				} else {
					goto case 45;
				}
			}
			case 45: {
				if (t == null) { currentState = 45; break; }
				Expect(37, t); // ")"
				currentState = stateStack.Pop();
				break;
			}
			case 46: {
				if (t == null) { currentState = 46; break; }
				currentState = 47;
				break;
			}
			case 47: {
				stateStack.Push(45);
				goto case 37;
			}
			case 48: {
				stateStack.Push(49);
				goto case 50;
			}
			case 49: {
				if (t == null) { currentState = 49; break; }
				if (set[12, t.kind]) {
					currentState = 48;
					break;
				} else {
					currentState = stateStack.Pop();
					goto switchlbl;
				}
			}
			case 50: {
				nextTokenIsPotentialStartOfXmlMode = true;
				goto case 51;
			}
			case 51: {
				if (t == null) { currentState = 51; break; }
				if (set[13, t.kind]) {
					currentState = 51;
					break;
				} else {
					if (set[14, t.kind]) {
						stateStack.Push(95);
						goto case 104;
					} else {
						if (t.kind == 218) {
							currentState = 92;
							break;
						} else {
							if (t.kind == 161) {
								goto case 52;
							} else {
								goto case 6;
							}
						}
					}
				}
			}
			case 52: {
				if (t == null) { currentState = 52; break; }
				Expect(161, t); // "New"
				currentState = 53;
				break;
			}
			case 53: {
				if (t == null) { currentState = 53; break; }
				if (set[15, t.kind]) {
					stateStack.Push(64);
					goto case 69;
				} else {
					goto case 54;
				}
			}
			case 54: {
				if (t == null) { currentState = 54; break; }
				if (t.kind == 231) {
					currentState = 55;
					break;
				} else {
					currentState = stateStack.Pop();
					goto switchlbl;
				}
			}
			case 55: {
				goto case 56;
			}
			case 56: {
				if (t == null) { currentState = 56; break; }
				Expect(34, t); // "{"
				currentState = 57;
				break;
			}
			case 57: {
				if (t == null) { currentState = 57; break; }
				if (t.kind == 146) {
					currentState = 58;
					break;
				} else {
					goto case 58;
				}
			}
			case 58: {
				if (t == null) { currentState = 58; break; }
				Expect(27, t); // "."
				currentState = 59;
				break;
			}
			case 59: {
				stateStack.Push(60);
				goto case 16;
			}
			case 60: {
				if (t == null) { currentState = 60; break; }
				Expect(21, t); // "="
				currentState = 61;
				break;
			}
			case 61: {
				stateStack.Push(62);
				goto case 37;
			}
			case 62: {
				if (t == null) { currentState = 62; break; }
				if (t.kind == 23) {
					currentState = 57;
					break;
				} else {
					goto case 63;
				}
			}
			case 63: {
				if (t == null) { currentState = 63; break; }
				Expect(35, t); // "}"
				currentState = stateStack.Pop();
				break;
			}
			case 64: {
				if (t == null) { currentState = 64; break; }
				if (t.kind == 125) {
					currentState = 65;
					break;
				} else {
					goto case 54;
				}
			}
			case 65: {
				stateStack.Push(54);
				goto case 66;
			}
			case 66: {
				if (t == null) { currentState = 66; break; }
				Expect(34, t); // "{"
				currentState = 67;
				break;
			}
			case 67: {
				if (t == null) { currentState = 67; break; }
				if (set[16, t.kind]) {
					stateStack.Push(68);
					goto case 37;
				} else {
					if (t.kind == 34) {
						stateStack.Push(68);
						goto case 66;
					} else {
						Error(t);
						goto case 68;
					}
				}
			}
			case 68: {
				if (t == null) { currentState = 68; break; }
				if (t.kind == 23) {
					currentState = 67;
					break;
				} else {
					goto case 63;
				}
			}
			case 69: {
				if (t == null) { currentState = 69; break; }
				if (t.kind == 129) {
					goto case 88;
				} else {
					if (set[17, t.kind]) {
						stateStack.Push(70);
						goto case 89;
					} else {
						if (set[18, t.kind]) {
							goto case 88;
						} else {
							Error(t);
							goto case 70;
						}
					}
				}
			}
			case 70: {
				if (t == null) { currentState = 70; break; }
				if (t.kind == 36) {
					stateStack.Push(70);
					goto case 74;
				} else {
					goto case 71;
				}
			}
			case 71: {
				if (t == null) { currentState = 71; break; }
				if (t.kind == 27) {
					currentState = 72;
					break;
				} else {
					currentState = stateStack.Pop();
					goto switchlbl;
				}
			}
			case 72: {
				stateStack.Push(73);
				goto case 16;
			}
			case 73: {
				if (t == null) { currentState = 73; break; }
				if (t.kind == 36) {
					stateStack.Push(73);
					goto case 74;
				} else {
					goto case 71;
				}
			}
			case 74: {
				if (t == null) { currentState = 74; break; }
				Expect(36, t); // "("
				currentState = 75;
				break;
			}
			case 75: {
				if (t == null) { currentState = 75; break; }
				if (t.kind == 168) {
					goto case 85;
				} else {
					if (set[19, t.kind]) {
						goto case 76;
					} else {
						Error(t);
						goto case 45;
					}
				}
			}
			case 76: {
				if (t == null) { currentState = 76; break; }
				if (set[20, t.kind]) {
					goto case 77;
				} else {
					goto case 45;
				}
			}
			case 77: {
				stateStack.Push(45);
				goto case 78;
			}
			case 78: {
				if (t == null) { currentState = 78; break; }
				if (set[16, t.kind]) {
					goto case 82;
				} else {
					if (t.kind == 23) {
						goto case 79;
					} else {
						goto case 6;
					}
				}
			}
			case 79: {
				if (t == null) { currentState = 79; break; }
				currentState = 80;
				break;
			}
			case 80: {
				if (t == null) { currentState = 80; break; }
				if (set[16, t.kind]) {
					stateStack.Push(81);
					goto case 37;
				} else {
					goto case 81;
				}
			}
			case 81: {
				if (t == null) { currentState = 81; break; }
				if (t.kind == 23) {
					goto case 79;
				} else {
					currentState = stateStack.Pop();
					goto switchlbl;
				}
			}
			case 82: {
				stateStack.Push(83);
				goto case 37;
			}
			case 83: {
				if (t == null) { currentState = 83; break; }
				if (t.kind == 23) {
					currentState = 84;
					break;
				} else {
					currentState = stateStack.Pop();
					goto switchlbl;
				}
			}
			case 84: {
				if (t == null) { currentState = 84; break; }
				if (set[16, t.kind]) {
					goto case 82;
				} else {
					goto case 83;
				}
			}
			case 85: {
				if (t == null) { currentState = 85; break; }
				currentState = 86;
				break;
			}
			case 86: {
				if (t == null) { currentState = 86; break; }
				if (set[15, t.kind]) {
					stateStack.Push(87);
					goto case 69;
				} else {
					goto case 87;
				}
			}
			case 87: {
				if (t == null) { currentState = 87; break; }
				if (t.kind == 23) {
					goto case 85;
				} else {
					goto case 45;
				}
			}
			case 88: {
				if (t == null) { currentState = 88; break; }
				currentState = 70;
				break;
			}
			case 89: {
				PushContext(Context.IdentifierExpected, t);
				goto case 90;
			}
			case 90: {
				if (t == null) { currentState = 90; break; }
				if (set[17, t.kind]) {
					currentState = 91;
					break;
				} else {
					Error(t);
					goto case 91;
				}
			}
			case 91: {
				PopContext();
				currentState = stateStack.Pop();
				goto switchlbl;
			}
			case 92: {
				stateStack.Push(93);
				goto case 50;
			}
			case 93: {
				if (t == null) { currentState = 93; break; }
				Expect(143, t); // "Is"
				currentState = 94;
				break;
			}
			case 94: {
				goto case 69;
			}
			case 95: {
				if (t == null) { currentState = 95; break; }
				if (t.kind == 27 || t.kind == 28 || t.kind == 36) {
					stateStack.Push(95);
					goto case 96;
				} else {
					currentState = stateStack.Pop();
					goto switchlbl;
				}
			}
			case 96: {
				if (t == null) { currentState = 96; break; }
				if (t.kind == 36) {
					currentState = 99;
					break;
				} else {
					if (t.kind == 27 || t.kind == 28) {
						goto case 97;
					} else {
						goto case 6;
					}
				}
			}
			case 97: {
				if (t == null) { currentState = 97; break; }
				currentState = 98;
				break;
			}
			case 98: {
				goto case 16;
			}
			case 99: {
				nextTokenIsPotentialStartOfXmlMode = true;
				goto case 100;
			}
			case 100: {
				if (t == null) { currentState = 100; break; }
				if (t.kind == 168) {
					goto case 101;
				} else {
					if (set[20, t.kind]) {
						goto case 77;
					} else {
						goto case 6;
					}
				}
			}
			case 101: {
				if (t == null) { currentState = 101; break; }
				currentState = 102;
				break;
			}
			case 102: {
				stateStack.Push(103);
				goto case 69;
			}
			case 103: {
				if (t == null) { currentState = 103; break; }
				if (t.kind == 23) {
					goto case 101;
				} else {
					goto case 45;
				}
			}
			case 104: {
				nextTokenIsPotentialStartOfXmlMode = true;
				goto case 105;
			}
			case 105: {
				if (t == null) { currentState = 105; break; }
				if (set[21, t.kind]) {
					goto case 16;
				} else {
					if (t.kind == 36) {
						goto case 46;
					} else {
						if (set[17, t.kind]) {
							goto case 124;
						} else {
							if (t.kind == 27 || t.kind == 28) {
								goto case 97;
							} else {
								if (t.kind == 128) {
									currentState = 123;
									break;
								} else {
									if (t.kind == 235) {
										currentState = 121;
										break;
									} else {
										if (t.kind == 10 || t.kind == 17) {
											goto case 113;
										} else {
											if (set[22, t.kind]) {
												goto case 106;
											} else {
												goto case 6;
											}
										}
									}
								}
							}
						}
					}
				}
			}
			case 106: {
				if (t == null) { currentState = 106; break; }
				if (set[23, t.kind]) {
					goto case 111;
				} else {
					if (t.kind == 93 || t.kind == 105 || t.kind == 217) {
						currentState = 107;
						break;
					} else {
						goto case 6;
					}
				}
			}
			case 107: {
				if (t == null) { currentState = 107; break; }
				Expect(36, t); // "("
				currentState = 108;
				break;
			}
			case 108: {
				stateStack.Push(109);
				goto case 37;
			}
			case 109: {
				if (t == null) { currentState = 109; break; }
				Expect(23, t); // ","
				currentState = 110;
				break;
			}
			case 110: {
				stateStack.Push(45);
				goto case 69;
			}
			case 111: {
				if (t == null) { currentState = 111; break; }
				if (set[23, t.kind]) {
					currentState = 112;
					break;
				} else {
					Error(t);
					goto case 112;
				}
			}
			case 112: {
				if (t == null) { currentState = 112; break; }
				Expect(36, t); // "("
				currentState = 47;
				break;
			}
			case 113: {
				PushContext(Context.Xml, t);
				goto case 114;
			}
			case 114: {
				if (t == null) { currentState = 114; break; }
				if (t.kind == 17) {
					currentState = 114;
					break;
				} else {
					stateStack.Push(115);
					goto case 116;
				}
			}
			case 115: {
				if (t == null) { currentState = 115; break; }
				if (t.kind == 17) {
					currentState = 115;
					break;
				} else {
					PopContext();
					currentState = stateStack.Pop();
					goto switchlbl;
				}
			}
			case 116: {
				if (t == null) { currentState = 116; break; }
				Expect(10, t); // XmlOpenTag
				currentState = 117;
				break;
			}
			case 117: {
				if (t == null) { currentState = 117; break; }
				if (set[24, t.kind]) {
					currentState = 117;
					break;
				} else {
					if (t.kind == 14) {
						goto case 16;
					} else {
						if (t.kind == 11) {
							goto case 118;
						} else {
							goto case 6;
						}
					}
				}
			}
			case 118: {
				if (t == null) { currentState = 118; break; }
				currentState = 119;
				break;
			}
			case 119: {
				if (t == null) { currentState = 119; break; }
				if (set[25, t.kind]) {
					if (set[26, t.kind]) {
						goto case 118;
					} else {
						if (t.kind == 10) {
							stateStack.Push(119);
							goto case 116;
						} else {
							Error(t);
							goto case 119;
						}
					}
				} else {
					Expect(15, t); // XmlOpenEndTag
					currentState = 120;
					break;
				}
			}
			case 120: {
				if (t == null) { currentState = 120; break; }
				if (set[27, t.kind]) {
					currentState = 120;
					break;
				} else {
					Expect(11, t); // XmlCloseTag
					currentState = stateStack.Pop();
					break;
				}
			}
			case 121: {
				if (t == null) { currentState = 121; break; }
				Expect(36, t); // "("
				currentState = 122;
				break;
			}
			case 122: {
				readXmlIdentifier = true;
				stateStack.Push(45);
				goto case 89;
			}
			case 123: {
				if (t == null) { currentState = 123; break; }
				Expect(36, t); // "("
				currentState = 110;
				break;
			}
			case 124: {
				goto case 89;
			}
			case 125: {
				if (t == null) { currentState = 125; break; }
				currentState = 36;
				break;
			}
			case 126: {
				if (t == null) { currentState = 126; break; }
				if (t.kind == 131) {
					currentState = 131;
					break;
				} else {
					if (t.kind == 119) {
						currentState = 130;
						break;
					} else {
						if (t.kind == 88) {
							currentState = 129;
							break;
						} else {
							if (t.kind == 204) {
								goto case 16;
							} else {
								if (t.kind == 193) {
									currentState = 127;
									break;
								} else {
									goto case 6;
								}
							}
						}
					}
				}
			}
			case 127: {
				nextTokenIsPotentialStartOfXmlMode = true;
				goto case 128;
			}
			case 128: {
				if (t == null) { currentState = 128; break; }
				if (set[16, t.kind]) {
					goto case 36;
				} else {
					currentState = stateStack.Pop();
					goto switchlbl;
				}
			}
			case 129: {
				if (t == null) { currentState = 129; break; }
				if (t.kind == 107 || t.kind == 123 || t.kind == 229) {
					goto case 16;
				} else {
					goto case 6;
				}
			}
			case 130: {
				if (t == null) { currentState = 130; break; }
				if (set[28, t.kind]) {
					goto case 16;
				} else {
					goto case 6;
				}
			}
			case 131: {
				if (t == null) { currentState = 131; break; }
				if (set[17, t.kind]) {
					goto case 124;
				} else {
					if (t.kind == 5) {
						goto case 16;
					} else {
						goto case 6;
					}
				}
			}
			case 132: {
				if (t == null) { currentState = 132; break; }
				Expect(216, t); // "Try"
				currentState = 133;
				break;
			}
			case 133: {
				stateStack.Push(134);
				goto case 31;
			}
			case 134: {
				if (t == null) { currentState = 134; break; }
				if (t.kind == 74) {
					currentState = 138;
					break;
				} else {
					if (t.kind == 122) {
						currentState = 137;
						break;
					} else {
						goto case 135;
					}
				}
			}
			case 135: {
				if (t == null) { currentState = 135; break; }
				Expect(112, t); // "End"
				currentState = 136;
				break;
			}
			case 136: {
				if (t == null) { currentState = 136; break; }
				Expect(216, t); // "Try"
				currentState = stateStack.Pop();
				break;
			}
			case 137: {
				stateStack.Push(135);
				goto case 31;
			}
			case 138: {
				if (t == null) { currentState = 138; break; }
				if (set[17, t.kind]) {
					stateStack.Push(141);
					goto case 89;
				} else {
					goto case 139;
				}
			}
			case 139: {
				if (t == null) { currentState = 139; break; }
				if (t.kind == 227) {
					currentState = 140;
					break;
				} else {
					goto case 133;
				}
			}
			case 140: {
				stateStack.Push(133);
				goto case 37;
			}
			case 141: {
				if (t == null) { currentState = 141; break; }
				if (t.kind == 62) {
					currentState = 142;
					break;
				} else {
					goto case 139;
				}
			}
			case 142: {
				stateStack.Push(139);
				goto case 69;
			}
			case 143: {
				if (t == null) { currentState = 143; break; }
				Expect(213, t); // "Throw"
				currentState = 144;
				break;
			}
			case 144: {
				nextTokenIsPotentialStartOfXmlMode = true;
				goto case 128;
			}
			case 145: {
				if (t == null) { currentState = 145; break; }
				if (t.kind == 117 || t.kind == 170) {
					if (t.kind == 170) {
						currentState = 148;
						break;
					} else {
						goto case 148;
					}
				} else {
					if (t.kind == 192) {
						currentState = 146;
						break;
					} else {
						goto case 6;
					}
				}
			}
			case 146: {
				if (t == null) { currentState = 146; break; }
				if (t.kind == 5 || t.kind == 162) {
					goto case 16;
				} else {
					goto case 147;
				}
			}
			case 147: {
				if (t == null) { currentState = 147; break; }
				if (set[17, t.kind]) {
					goto case 124;
				} else {
					goto case 6;
				}
			}
			case 148: {
				if (t == null) { currentState = 148; break; }
				Expect(117, t); // "Error"
				currentState = 149;
				break;
			}
			case 149: {
				nextTokenIsPotentialStartOfXmlMode = true;
				goto case 150;
			}
			case 150: {
				if (t == null) { currentState = 150; break; }
				if (set[16, t.kind]) {
					goto case 36;
				} else {
					if (t.kind == 131) {
						currentState = 152;
						break;
					} else {
						if (t.kind == 192) {
							currentState = 151;
							break;
						} else {
							goto case 6;
						}
					}
				}
			}
			case 151: {
				if (t == null) { currentState = 151; break; }
				Expect(162, t); // "Next"
				currentState = stateStack.Pop();
				break;
			}
			case 152: {
				if (t == null) { currentState = 152; break; }
				if (t.kind == 5) {
					goto case 16;
				} else {
					goto case 147;
				}
			}
			case 153: {
				if (t == null) { currentState = 153; break; }
				Expect(123, t); // "For"
				currentState = 154;
				break;
			}
			case 154: {
				nextTokenIsPotentialStartOfXmlMode = true;
				goto case 155;
			}
			case 155: {
				if (t == null) { currentState = 155; break; }
				if (set[14, t.kind]) {
					goto case 169;
				} else {
					if (t.kind == 109) {
						goto case 156;
					} else {
						goto case 6;
					}
				}
			}
			case 156: {
				if (t == null) { currentState = 156; break; }
				Expect(109, t); // "Each"
				currentState = 157;
				break;
			}
			case 157: {
				stateStack.Push(158);
				goto case 166;
			}
			case 158: {
				if (t == null) { currentState = 158; break; }
				Expect(137, t); // "In"
				currentState = 159;
				break;
			}
			case 159: {
				stateStack.Push(160);
				goto case 37;
			}
			case 160: {
				stateStack.Push(161);
				goto case 31;
			}
			case 161: {
				if (t == null) { currentState = 161; break; }
				Expect(162, t); // "Next"
				currentState = 162;
				break;
			}
			case 162: {
				nextTokenIsPotentialStartOfXmlMode = true;
				goto case 163;
			}
			case 163: {
				if (t == null) { currentState = 163; break; }
				if (set[16, t.kind]) {
					goto case 164;
				} else {
					currentState = stateStack.Pop();
					goto switchlbl;
				}
			}
			case 164: {
				stateStack.Push(165);
				goto case 37;
			}
			case 165: {
				if (t == null) { currentState = 165; break; }
				if (t.kind == 23) {
					currentState = 164;
					break;
				} else {
					currentState = stateStack.Pop();
					goto switchlbl;
				}
			}
			case 166: {
				stateStack.Push(167);
				goto case 104;
			}
			case 167: {
				if (t == null) { currentState = 167; break; }
				if (t.kind == 32) {
					currentState = 168;
					break;
				} else {
					goto case 168;
				}
			}
			case 168: {
				if (t == null) { currentState = 168; break; }
				if (t.kind == 27 || t.kind == 28 || t.kind == 36) {
					stateStack.Push(168);
					goto case 96;
				} else {
					currentState = stateStack.Pop();
					goto switchlbl;
				}
			}
			case 169: {
				stateStack.Push(170);
				goto case 166;
			}
			case 170: {
				if (t == null) { currentState = 170; break; }
				Expect(21, t); // "="
				currentState = 171;
				break;
			}
			case 171: {
				stateStack.Push(172);
				goto case 37;
			}
			case 172: {
				if (t == null) { currentState = 172; break; }
				if (t.kind == 203) {
					currentState = 179;
					break;
				} else {
					goto case 173;
				}
			}
			case 173: {
				stateStack.Push(174);
				goto case 31;
			}
			case 174: {
				if (t == null) { currentState = 174; break; }
				Expect(162, t); // "Next"
				currentState = 175;
				break;
			}
			case 175: {
				nextTokenIsPotentialStartOfXmlMode = true;
				goto case 176;
			}
			case 176: {
				if (t == null) { currentState = 176; break; }
				if (set[16, t.kind]) {
					goto case 177;
				} else {
					currentState = stateStack.Pop();
					goto switchlbl;
				}
			}
			case 177: {
				stateStack.Push(178);
				goto case 37;
			}
			case 178: {
				if (t == null) { currentState = 178; break; }
				if (t.kind == 23) {
					currentState = 177;
					break;
				} else {
					currentState = stateStack.Pop();
					goto switchlbl;
				}
			}
			case 179: {
				stateStack.Push(173);
				goto case 37;
			}
			case 180: {
				if (t == null) { currentState = 180; break; }
				Expect(107, t); // "Do"
				currentState = 181;
				break;
			}
			case 181: {
				if (t == null) { currentState = 181; break; }
				if (t.kind == 222 || t.kind == 229) {
					goto case 185;
				} else {
					if (t.kind == 1 || t.kind == 22) {
						goto case 182;
					} else {
						goto case 6;
					}
				}
			}
			case 182: {
				stateStack.Push(183);
				goto case 31;
			}
			case 183: {
				if (t == null) { currentState = 183; break; }
				Expect(151, t); // "Loop"
				currentState = 184;
				break;
			}
			case 184: {
				if (t == null) { currentState = 184; break; }
				if (t.kind == 222 || t.kind == 229) {
					goto case 125;
				} else {
					currentState = stateStack.Pop();
					goto switchlbl;
				}
			}
			case 185: {
				if (t == null) { currentState = 185; break; }
				if (t.kind == 222 || t.kind == 229) {
					currentState = 186;
					break;
				} else {
					Error(t);
					goto case 186;
				}
			}
			case 186: {
				stateStack.Push(187);
				goto case 37;
			}
			case 187: {
				stateStack.Push(188);
				goto case 31;
			}
			case 188: {
				if (t == null) { currentState = 188; break; }
				Expect(151, t); // "Loop"
				currentState = stateStack.Pop();
				break;
			}
			case 189: {
				if (t == null) { currentState = 189; break; }
				Expect(229, t); // "While"
				currentState = 190;
				break;
			}
			case 190: {
				stateStack.Push(191);
				goto case 37;
			}
			case 191: {
				stateStack.Push(192);
				goto case 31;
			}
			case 192: {
				if (t == null) { currentState = 192; break; }
				Expect(112, t); // "End"
				currentState = 193;
				break;
			}
			case 193: {
				if (t == null) { currentState = 193; break; }
				Expect(229, t); // "While"
				currentState = stateStack.Pop();
				break;
			}
			case 194: {
				if (t == null) { currentState = 194; break; }
				Expect(195, t); // "Select"
				currentState = 195;
				break;
			}
			case 195: {
				if (t == null) { currentState = 195; break; }
				if (t.kind == 73) {
					currentState = 196;
					break;
				} else {
					goto case 196;
				}
			}
			case 196: {
				stateStack.Push(197);
				goto case 37;
			}
			case 197: {
				stateStack.Push(198);
				goto case 15;
			}
			case 198: {
				if (t == null) { currentState = 198; break; }
				if (t.kind == 73) {
					currentState = 200;
					break;
				} else {
					Expect(112, t); // "End"
					currentState = 199;
					break;
				}
			}
			case 199: {
				if (t == null) { currentState = 199; break; }
				Expect(195, t); // "Select"
				currentState = stateStack.Pop();
				break;
			}
			case 200: {
				nextTokenIsPotentialStartOfXmlMode = true;
				goto case 201;
			}
			case 201: {
				if (t == null) { currentState = 201; break; }
				if (t.kind == 110) {
					currentState = 202;
					break;
				} else {
					if (set[29, t.kind]) {
						goto case 203;
					} else {
						Error(t);
						goto case 202;
					}
				}
			}
			case 202: {
				stateStack.Push(198);
				goto case 31;
			}
			case 203: {
				if (t == null) { currentState = 203; break; }
				if (set[30, t.kind]) {
					if (t.kind == 143) {
						currentState = 206;
						break;
					} else {
						goto case 206;
					}
				} else {
					if (set[16, t.kind]) {
						stateStack.Push(204);
						goto case 37;
					} else {
						Error(t);
						goto case 204;
					}
				}
			}
			case 204: {
				if (t == null) { currentState = 204; break; }
				if (t.kind == 23) {
					currentState = 205;
					break;
				} else {
					goto case 202;
				}
			}
			case 205: {
				nextTokenIsPotentialStartOfXmlMode = true;
				goto case 203;
			}
			case 206: {
				stateStack.Push(207);
				goto case 208;
			}
			case 207: {
				stateStack.Push(204);
				goto case 50;
			}
			case 208: {
				if (t == null) { currentState = 208; break; }
				if (set[31, t.kind]) {
					goto case 16;
				} else {
					goto case 6;
				}
			}
			case 209: {
				if (t == null) { currentState = 209; break; }
				Expect(134, t); // "If"
				currentState = 210;
				break;
			}
			case 210: {
				stateStack.Push(211);
				goto case 37;
			}
			case 211: {
				if (t == null) { currentState = 211; break; }
				if (t.kind == 212) {
					currentState = 220;
					break;
				} else {
					goto case 212;
				}
			}
			case 212: {
				if (t == null) { currentState = 212; break; }
				if (t.kind == 1 || t.kind == 22) {
					goto case 213;
				} else {
					goto case 6;
				}
			}
			case 213: {
				stateStack.Push(214);
				goto case 31;
			}
			case 214: {
				if (t == null) { currentState = 214; break; }
				if (t.kind == 110 || t.kind == 111) {
					if (t.kind == 110) {
						currentState = 219;
						break;
					} else {
						if (t.kind == 111) {
							goto case 216;
						} else {
							Error(t);
							goto case 213;
						}
					}
				} else {
					Expect(112, t); // "End"
					currentState = 215;
					break;
				}
			}
			case 215: {
				if (t == null) { currentState = 215; break; }
				Expect(134, t); // "If"
				currentState = stateStack.Pop();
				break;
			}
			case 216: {
				if (t == null) { currentState = 216; break; }
				currentState = 217;
				break;
			}
			case 217: {
				stateStack.Push(218);
				goto case 37;
			}
			case 218: {
				if (t == null) { currentState = 218; break; }
				if (t.kind == 212) {
					currentState = 213;
					break;
				} else {
					goto case 213;
				}
			}
			case 219: {
				if (t == null) { currentState = 219; break; }
				if (t.kind == 134) {
					goto case 216;
				} else {
					goto case 213;
				}
			}
			case 220: {
				if (t == null) { currentState = 220; break; }
				if (set[8, t.kind]) {
					goto case 221;
				} else {
					goto case 212;
				}
			}
			case 221: {
				stateStack.Push(222);
				goto case 34;
			}
			case 222: {
				if (t == null) { currentState = 222; break; }
				if (t.kind == 22) {
					currentState = 227;
					break;
				} else {
					if (t.kind == 110) {
						goto case 224;
					} else {
						goto case 223;
					}
				}
			}
			case 223: {
				if (t == null) { currentState = 223; break; }
				Expect(1, t); // EOL
				currentState = stateStack.Pop();
				break;
			}
			case 224: {
				if (t == null) { currentState = 224; break; }
				currentState = 225;
				break;
			}
			case 225: {
				if (t == null) { currentState = 225; break; }
				if (set[8, t.kind]) {
					stateStack.Push(226);
					goto case 34;
				} else {
					goto case 226;
				}
			}
			case 226: {
				if (t == null) { currentState = 226; break; }
				if (t.kind == 22) {
					goto case 224;
				} else {
					goto case 223;
				}
			}
			case 227: {
				if (t == null) { currentState = 227; break; }
				if (set[8, t.kind]) {
					goto case 221;
				} else {
					goto case 222;
				}
			}
			case 228: {
				if (t == null) { currentState = 228; break; }
				Expect(187, t); // "RaiseEvent"
				currentState = 229;
				break;
			}
			case 229: {
				stateStack.Push(230);
				goto case 16;
			}
			case 230: {
				if (t == null) { currentState = 230; break; }
				if (t.kind == 36) {
					currentState = 76;
					break;
				} else {
					currentState = stateStack.Pop();
					goto switchlbl;
				}
			}
			case 231: {
				if (t == null) { currentState = 231; break; }
				if (t.kind == 55 || t.kind == 191) {
					currentState = 232;
					break;
				} else {
					Error(t);
					goto case 232;
				}
			}
			case 232: {
				stateStack.Push(233);
				goto case 37;
			}
			case 233: {
				if (t == null) { currentState = 233; break; }
				Expect(23, t); // ","
				currentState = 36;
				break;
			}
			case 234: {
				if (t == null) { currentState = 234; break; }
				if (t.kind == 209 || t.kind == 231) {
					currentState = 235;
					break;
				} else {
					Error(t);
					goto case 235;
				}
			}
			case 235: {
				stateStack.Push(236);
				goto case 37;
			}
			case 236: {
				stateStack.Push(237);
				goto case 31;
			}
			case 237: {
				if (t == null) { currentState = 237; break; }
				Expect(112, t); // "End"
				currentState = 238;
				break;
			}
			case 238: {
				if (t == null) { currentState = 238; break; }
				if (t.kind == 209 || t.kind == 231) {
					goto case 16;
				} else {
					goto case 6;
				}
			}
			case 239: {
				if (t == null) { currentState = 239; break; }
				if (t.kind == 87 || t.kind == 104 || t.kind == 202) {
					currentState = 240;
					break;
				} else {
					Error(t);
					goto case 240;
				}
			}
			case 240: {
				stateStack.Push(241);
				goto case 89;
			}
			case 241: {
				if (t == null) { currentState = 241; break; }
				if (t.kind == 32) {
					currentState = 242;
					break;
				} else {
					goto case 242;
				}
			}
			case 242: {
				if (t == null) { currentState = 242; break; }
				if (t.kind == 36) {
					goto case 254;
				} else {
					goto case 243;
				}
			}
			case 243: {
				if (t == null) { currentState = 243; break; }
				if (t.kind == 23) {
					currentState = 248;
					break;
				} else {
					if (t.kind == 62) {
						currentState = 245;
						break;
					} else {
						goto case 244;
					}
				}
			}
			case 244: {
				if (t == null) { currentState = 244; break; }
				if (t.kind == 21) {
					goto case 125;
				} else {
					currentState = stateStack.Pop();
					goto switchlbl;
				}
			}
			case 245: {
				if (t == null) { currentState = 245; break; }
				if (t.kind == 161) {
					goto case 247;
				} else {
					goto case 246;
				}
			}
			case 246: {
				stateStack.Push(244);
				goto case 69;
			}
			case 247: {
				if (t == null) { currentState = 247; break; }
				currentState = 246;
				break;
			}
			case 248: {
				stateStack.Push(249);
				goto case 89;
			}
			case 249: {
				if (t == null) { currentState = 249; break; }
				if (t.kind == 32) {
					currentState = 250;
					break;
				} else {
					goto case 250;
				}
			}
			case 250: {
				if (t == null) { currentState = 250; break; }
				if (t.kind == 36) {
					goto case 251;
				} else {
					goto case 243;
				}
			}
			case 251: {
				if (t == null) { currentState = 251; break; }
				currentState = 252;
				break;
			}
			case 252: {
				if (t == null) { currentState = 252; break; }
				if (t.kind == 23) {
					goto case 251;
				} else {
					goto case 253;
				}
			}
			case 253: {
				if (t == null) { currentState = 253; break; }
				Expect(37, t); // ")"
				currentState = 243;
				break;
			}
			case 254: {
				if (t == null) { currentState = 254; break; }
				currentState = 255;
				break;
			}
			case 255: {
				if (t == null) { currentState = 255; break; }
				if (t.kind == 23) {
					goto case 254;
				} else {
					goto case 253;
				}
			}
			case 256: {
				if (t == null) { currentState = 256; break; }
				if (t.kind == 39) {
					stateStack.Push(256);
					goto case 257;
				} else {
					stateStack.Push(27);
					goto case 69;
				}
			}
			case 257: {
				if (t == null) { currentState = 257; break; }
				Expect(39, t); // "<"
				currentState = 258;
				break;
			}
			case 258: {
				PushContext(Context.Attribute, t);
				goto case 259;
			}
			case 259: {
				if (t == null) { currentState = 259; break; }
				if (set[32, t.kind]) {
					currentState = 259;
					break;
				} else {
					Expect(38, t); // ">"
					currentState = 260;
					break;
				}
			}
			case 260: {
				PopContext();
				goto case 261;
			}
			case 261: {
				if (t == null) { currentState = 261; break; }
				if (t.kind == 1) {
					goto case 16;
				} else {
					currentState = stateStack.Pop();
					goto switchlbl;
				}
			}
			case 262: {
				stateStack.Push(263);
				goto case 264;
			}
			case 263: {
				if (t == null) { currentState = 263; break; }
				if (t.kind == 23) {
					currentState = 262;
					break;
				} else {
					currentState = stateStack.Pop();
					goto switchlbl;
				}
			}
			case 264: {
				if (t == null) { currentState = 264; break; }
				if (t.kind == 39) {
					stateStack.Push(264);
					goto case 257;
				} else {
					goto case 265;
				}
			}
			case 265: {
				if (t == null) { currentState = 265; break; }
				if (set[33, t.kind]) {
					currentState = 265;
					break;
				} else {
					stateStack.Push(266);
					goto case 89;
				}
			}
			case 266: {
				if (t == null) { currentState = 266; break; }
				if (t.kind == 62) {
					goto case 247;
				} else {
					goto case 244;
				}
			}
			case 267: {
				if (t == null) { currentState = 267; break; }
				Expect(97, t); // "Custom"
				currentState = 268;
				break;
			}
			case 268: {
				stateStack.Push(269);
				goto case 279;
			}
			case 269: {
				if (t == null) { currentState = 269; break; }
				if (set[34, t.kind]) {
					goto case 271;
				} else {
					Expect(112, t); // "End"
					currentState = 270;
					break;
				}
			}
			case 270: {
				if (t == null) { currentState = 270; break; }
				Expect(118, t); // "Event"
				currentState = 30;
				break;
			}
			case 271: {
				if (t == null) { currentState = 271; break; }
				if (t.kind == 39) {
					stateStack.Push(271);
					goto case 257;
				} else {
					if (t.kind == 55 || t.kind == 187 || t.kind == 191) {
						currentState = 272;
						break;
					} else {
						Error(t);
						goto case 272;
					}
				}
			}
			case 272: {
				if (t == null) { currentState = 272; break; }
				Expect(36, t); // "("
				currentState = 273;
				break;
			}
			case 273: {
				stateStack.Push(274);
				goto case 262;
			}
			case 274: {
				if (t == null) { currentState = 274; break; }
				Expect(37, t); // ")"
				currentState = 275;
				break;
			}
			case 275: {
				stateStack.Push(276);
				goto case 31;
			}
			case 276: {
				if (t == null) { currentState = 276; break; }
				Expect(112, t); // "End"
				currentState = 277;
				break;
			}
			case 277: {
				if (t == null) { currentState = 277; break; }
				if (t.kind == 55 || t.kind == 187 || t.kind == 191) {
					currentState = 278;
					break;
				} else {
					Error(t);
					goto case 278;
				}
			}
			case 278: {
				stateStack.Push(269);
				goto case 15;
			}
			case 279: {
				if (t == null) { currentState = 279; break; }
				Expect(118, t); // "Event"
				currentState = 280;
				break;
			}
			case 280: {
				stateStack.Push(281);
				goto case 89;
			}
			case 281: {
				if (t == null) { currentState = 281; break; }
				if (t.kind == 62) {
					currentState = 288;
					break;
				} else {
					if (set[35, t.kind]) {
						if (t.kind == 36) {
							currentState = 286;
							break;
						} else {
							goto case 282;
						}
					} else {
						Error(t);
						goto case 282;
					}
				}
			}
			case 282: {
				if (t == null) { currentState = 282; break; }
				if (t.kind == 135) {
					goto case 283;
				} else {
					goto case 30;
				}
			}
			case 283: {
				if (t == null) { currentState = 283; break; }
				currentState = 284;
				break;
			}
			case 284: {
				stateStack.Push(285);
				goto case 69;
			}
			case 285: {
				if (t == null) { currentState = 285; break; }
				if (t.kind == 23) {
					goto case 283;
				} else {
					goto case 30;
				}
			}
			case 286: {
				if (t == null) { currentState = 286; break; }
				if (set[36, t.kind]) {
					stateStack.Push(287);
					goto case 262;
				} else {
					goto case 287;
				}
			}
			case 287: {
				if (t == null) { currentState = 287; break; }
				Expect(37, t); // ")"
				currentState = 282;
				break;
			}
			case 288: {
				stateStack.Push(282);
				goto case 69;
			}
			case 289: {
				if (t == null) { currentState = 289; break; }
				Expect(100, t); // "Declare"
				currentState = 290;
				break;
			}
			case 290: {
				if (t == null) { currentState = 290; break; }
				if (t.kind == 61 || t.kind == 65 || t.kind == 221) {
					currentState = 291;
					break;
				} else {
					goto case 291;
				}
			}
			case 291: {
				if (t == null) { currentState = 291; break; }
				if (t.kind == 126 || t.kind == 208) {
					currentState = 292;
					break;
				} else {
					Error(t);
					goto case 292;
				}
			}
			case 292: {
				stateStack.Push(293);
				goto case 89;
			}
			case 293: {
				if (t == null) { currentState = 293; break; }
				Expect(148, t); // "Lib"
				currentState = 294;
				break;
			}
			case 294: {
				if (t == null) { currentState = 294; break; }
				Expect(3, t); // LiteralString
				currentState = 295;
				break;
			}
			case 295: {
				if (t == null) { currentState = 295; break; }
				if (t.kind == 58) {
					currentState = 299;
					break;
				} else {
					goto case 296;
				}
			}
			case 296: {
				if (t == null) { currentState = 296; break; }
				if (t.kind == 36) {
					currentState = 297;
					break;
				} else {
					goto case 30;
				}
			}
			case 297: {
				if (t == null) { currentState = 297; break; }
				if (set[36, t.kind]) {
					stateStack.Push(298);
					goto case 262;
				} else {
					goto case 298;
				}
			}
			case 298: {
				if (t == null) { currentState = 298; break; }
				Expect(37, t); // ")"
				currentState = 30;
				break;
			}
			case 299: {
				if (t == null) { currentState = 299; break; }
				Expect(3, t); // LiteralString
				currentState = 296;
				break;
			}
			case 300: {
				if (t == null) { currentState = 300; break; }
				if (t.kind == 126 || t.kind == 208) {
					currentState = 301;
					break;
				} else {
					Error(t);
					goto case 301;
				}
			}
			case 301: {
				PushContext(Context.IdentifierExpected, t);
				goto case 302;
			}
			case 302: {
				if (t == null) { currentState = 302; break; }
				currentState = 303;
				break;
			}
			case 303: {
				PopContext();
				goto case 304;
			}
			case 304: {
				if (t == null) { currentState = 304; break; }
				if (t.kind == 36) {
					currentState = 310;
					break;
				} else {
					goto case 305;
				}
			}
			case 305: {
				if (t == null) { currentState = 305; break; }
				if (t.kind == 62) {
					currentState = 309;
					break;
				} else {
					goto case 306;
				}
			}
			case 306: {
				stateStack.Push(307);
				goto case 31;
			}
			case 307: {
				if (t == null) { currentState = 307; break; }
				Expect(112, t); // "End"
				currentState = 308;
				break;
			}
			case 308: {
				if (t == null) { currentState = 308; break; }
				if (t.kind == 126 || t.kind == 208) {
					currentState = 30;
					break;
				} else {
					Error(t);
					goto case 30;
				}
			}
			case 309: {
				stateStack.Push(306);
				goto case 69;
			}
			case 310: {
				if (t == null) { currentState = 310; break; }
				if (set[36, t.kind]) {
					stateStack.Push(311);
					goto case 262;
				} else {
					goto case 311;
				}
			}
			case 311: {
				if (t == null) { currentState = 311; break; }
				Expect(37, t); // ")"
				currentState = 305;
				break;
			}
			case 312: {
				if (t == null) { currentState = 312; break; }
				if (t.kind == 87) {
					currentState = 313;
					break;
				} else {
					goto case 313;
				}
			}
			case 313: {
				stateStack.Push(314);
				goto case 318;
			}
			case 314: {
				if (t == null) { currentState = 314; break; }
				if (t.kind == 62) {
					currentState = 317;
					break;
				} else {
					goto case 315;
				}
			}
			case 315: {
				if (t == null) { currentState = 315; break; }
				if (t.kind == 21) {
					currentState = 316;
					break;
				} else {
					goto case 30;
				}
			}
			case 316: {
				stateStack.Push(30);
				goto case 37;
			}
			case 317: {
				stateStack.Push(315);
				goto case 69;
			}
			case 318: {
				if (t == null) { currentState = 318; break; }
				if (set[37, t.kind]) {
					goto case 16;
				} else {
					goto case 6;
				}
			}
			case 319: {
				if (t == null) { currentState = 319; break; }
				currentState = 9;
				break;
			}
			case 320: {
				if (t == null) { currentState = 320; break; }
				Expect(159, t); // "Namespace"
				currentState = 321;
				break;
			}
			case 321: {
				if (t == null) { currentState = 321; break; }
				if (set[3, t.kind]) {
					currentState = 321;
					break;
				} else {
					stateStack.Push(322);
					goto case 15;
				}
			}
			case 322: {
				if (t == null) { currentState = 322; break; }
				if (set[38, t.kind]) {
					stateStack.Push(322);
					goto case 5;
				} else {
					Expect(112, t); // "End"
					currentState = 323;
					break;
				}
			}
			case 323: {
				if (t == null) { currentState = 323; break; }
				Expect(159, t); // "Namespace"
				currentState = 30;
				break;
			}
			case 324: {
				if (t == null) { currentState = 324; break; }
				Expect(136, t); // "Imports"
				currentState = 325;
				break;
			}
			case 325: {
				nextTokenIsPotentialStartOfXmlMode = true;
				goto case 326;
			}
			case 326: {
				if (t == null) { currentState = 326; break; }
				if (set[3, t.kind]) {
					currentState = 326;
					break;
				} else {
					goto case 30;
				}
			}
			case 327: {
				if (t == null) { currentState = 327; break; }
				Expect(172, t); // "Option"
				currentState = 328;
				break;
			}
			case 328: {
				if (t == null) { currentState = 328; break; }
				if (set[3, t.kind]) {
					currentState = 328;
					break;
				} else {
					goto case 30;
				}
			}
		}

	}
	
	public void Advance()
	{
		Console.WriteLine("Advance");
		InformTokenInternal(null);
	}
	
	static readonly bool[,] set = {
		{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,T,T,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x},
		{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,T,T,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x},
		{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,T,T,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x},
		{x,x,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,x,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,x},
		{x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,T,x,T, T,T,T,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,T, x,x,x,x, x,x,x,x, x,T,x,x, T,x,x,T, T,x,T,x, x,x,x,x, x,x,x,T, x,x,T,x, T,x,x,x, T,T,T,x, x,x,x,x, T,x,x,x, x,x,T,x, x,x,T,x, x,T,T,x, x,x,x,x, x,x,x,x, T,x,x,x, T,x,x,x, x,x,T,x, x,T,x,T, x,x,x,T, x,T,T,T, x,T,T,T, x,T,T,x, x,x,x,x, x,x,x,x, x,T,T,x, x,T,x,x, x,x,x,x, T,x,T,T, x,x,x,x, x,x,x,x, x,T,T,x, x,x,x,x, T,x,T,x, T,x,x,x, x,x},
		{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, T,x,x,x, x,x,T,x, x,x,x,x, x,x,x,x, x,T,T,T, x,T,x,T, x,T,T,x, x,x,x,x, x,x,x,x, x,T,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, T,x,x,x, x,x},
		{x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,T,x,T, T,T,T,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,T,x, x,x,x,x, x,x,x,T, x,x,x,x, T,x,x,x, x,T,x,x, x,x,x,x, T,x,x,x, x,x,T,x, x,x,T,x, x,T,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,T, x,x,x,x, x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,T,T, x,x,x,x, x,x,x,x, x,T,T,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x},
		{x,T,T,T, T,T,T,T, T,T,T,x, x,x,x,x, x,T,x,x, x,x,T,x, x,x,x,T, T,T,T,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, T,T,x,x, x,T,x,T, T,T,T,x, x,T,x,x, T,x,x,T, T,T,T,T, T,x,T,x, T,T,T,T, T,T,T,T, T,T,T,T, T,T,x,x, x,x,x,T, T,T,T,T, x,x,x,x, x,x,x,T, x,T,x,T, T,T,x,T, x,T,x,x, T,x,x,T, T,x,T,x, x,x,T,x, x,x,T,x, x,T,T,x, x,x,x,x, T,x,x,x, x,T,T,x, x,T,x,T, T,x,x,x, x,T,T,x, x,x,x,T, x,x,x,x, x,x,T,x, x,x,x,T, x,x,x,T, T,T,x,T, x,x,x,x, x,T,T,x, T,x,x,x, x,T,T,T, x,T,x,T, T,T,T,x, x,T,T,x, x,x,x,x, T,T,x,T, x,x,x,T, x,x},
		{x,x,T,T, T,T,T,T, T,T,T,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,T, T,T,T,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, T,T,x,x, x,T,x,T, T,T,T,x, x,T,x,x, T,x,x,T, T,T,T,T, T,x,T,x, T,T,T,T, T,T,T,T, T,T,T,T, T,T,x,x, x,x,x,T, T,T,T,T, x,x,x,x, x,x,x,T, x,T,x,T, T,T,x,T, x,T,x,x, T,x,x,T, T,x,T,x, x,x,T,x, x,x,T,x, x,T,T,x, x,x,x,x, T,x,x,x, x,T,T,x, x,T,x,T, T,x,x,x, x,T,T,x, x,x,x,T, x,x,x,x, x,x,T,x, x,x,x,T, x,x,x,T, T,T,x,T, x,x,x,x, x,T,T,x, T,x,x,x, x,T,T,T, x,T,x,T, T,T,T,x, x,T,T,x, x,x,x,x, T,T,x,T, x,x,x,T, x,x},
		{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x},
		{x,x,T,T, T,T,T,T, T,T,T,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,T, T,T,T,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,T,x,x, x,T,x,T, T,T,T,x, x,T,x,x, T,x,x,T, T,T,T,T, T,x,T,x, T,T,T,x, x,T,T,T, T,T,T,T, T,T,x,x, x,x,x,T, x,T,T,x, x,x,x,x, x,x,x,T, x,x,x,x, T,T,x,x, x,T,x,x, T,x,x,x, T,x,T,x, x,x,T,x, x,x,T,x, x,T,T,x, x,x,x,x, T,x,x,x, x,T,T,x, x,T,x,T, T,x,x,x, x,T,x,x, x,x,x,T, x,x,x,x, x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,T,T, x,x,x,T, x,T,T,x, x,T,T,x, x,x,x,x, T,x,x,x, x,x,x,T, x,x},
		{x,x,T,T, T,T,T,T, T,T,T,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,T, T,T,T,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,T,x,x, x,T,x,T, T,T,T,x, x,T,x,x, x,x,x,T, T,T,T,T, T,x,T,x, T,T,T,x, x,T,T,T, T,T,T,T, T,T,x,x, x,x,x,T, x,T,T,x, x,x,x,x, x,x,x,T, x,x,x,x, T,T,x,x, x,T,x,x, T,x,x,x, T,x,x,x, x,x,T,x, x,x,T,x, x,T,T,x, x,x,x,x, T,x,x,x, x,T,T,x, x,T,x,T, T,x,x,x, x,T,x,x, x,x,x,T, x,x,x,x, x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,T,T, x,x,x,T, x,T,T,x, x,T,T,x, x,x,x,x, T,x,x,x, x,x,x,T, x,x},
		{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, T,T,T,x, x,T,T,T, x,T,x,x, x,x,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,x, x,x,x,T, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, T,x,x,x, x,T,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, x,x},
		{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x},
		{x,x,T,T, T,T,T,T, T,T,T,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,T, T,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,T,x,T, T,T,T,x, x,T,x,x, x,x,x,T, T,T,T,T, T,x,T,x, T,T,T,x, x,T,T,T, T,T,T,T, T,T,x,x, x,x,x,T, x,T,T,x, x,x,x,x, x,x,x,T, x,x,x,x, T,T,x,x, x,T,x,x, T,x,x,x, T,x,x,x, x,x,T,x, x,x,T,x, x,T,T,x, x,x,x,x, T,x,x,x, x,T,T,x, x,x,x,x, T,x,x,x, x,T,x,x, x,x,x,T, x,x,x,x, x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,T,T, x,x,x,T, x,T,x,x, x,T,T,x, x,x,x,x, T,x,x,x, x,x,x,T, x,x},
		{x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,T,x,T, T,T,T,T, x,T,T,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,T,x, x,x,x,x, x,x,x,x, x,T,T,T, x,x,x,T, x,x,T,x, T,x,x,x, x,x,x,T, x,x,x,x, T,x,x,x, x,T,x,x, x,T,x,x, T,x,x,x, x,x,T,x, T,x,T,x, x,T,T,x, x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,T,x,x, x,x,x,T, x,x,x,x, x,x,T,x, x,x,x,x, x,x,x,x, x,x,T,x, x,x,x,T, T,T,x,x, x,x,T,x, x,x,T,T, x,x,x,x, x,x,x,T, T,T,T,T, x,x,x,x, T,x,x,x, x,x,x,x, x,x},
		{x,x,T,T, T,T,T,T, T,T,T,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,T, T,T,T,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,T,x,x, x,T,x,T, T,T,T,x, x,T,x,x, x,x,x,T, T,T,T,T, T,x,T,x, T,T,T,x, x,T,T,T, T,T,T,T, T,T,x,x, x,x,x,T, x,T,T,x, x,x,x,x, x,x,x,T, x,x,x,x, T,T,x,x, x,T,x,x, T,x,x,x, T,x,T,x, x,x,T,x, x,x,T,x, x,T,T,x, x,x,x,x, T,x,x,x, x,T,T,x, x,T,x,T, T,x,x,x, x,T,x,x, x,x,x,T, x,x,x,x, x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,T,T, x,x,x,T, x,T,T,x, x,T,T,x, x,x,x,x, T,x,x,x, x,x,x,T, x,x},
		{x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,T,x,T, T,T,T,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,T, x,x,T,x, x,x,x,x, x,x,x,T, x,x,x,x, T,x,x,x, x,T,x,x, x,x,x,x, T,x,x,x, x,x,T,x, x,x,T,x, x,T,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,T, x,x,x,x, x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,T,T, x,x,x,x, x,x,x,x, x,T,T,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x},
		{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,T,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,T, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, x,x,x,T, T,x,x,x, x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,T, T,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x},
		{x,x,T,T, T,T,T,T, T,T,T,x, x,x,x,x, x,T,x,x, x,x,x,T, x,x,x,T, T,T,T,x, x,x,x,x, T,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,T,x,x, x,T,x,T, T,T,T,x, x,T,x,x, x,x,x,T, T,T,T,T, T,x,T,x, T,T,T,x, x,T,T,T, T,T,T,T, T,T,x,x, x,x,x,T, x,T,T,x, x,x,x,x, x,x,x,T, x,x,x,x, T,T,x,x, x,T,x,x, T,x,x,x, T,x,T,x, x,x,T,x, x,x,T,x, x,T,T,x, x,x,x,x, T,x,x,x, x,T,T,x, x,T,x,T, T,x,x,x, x,T,x,x, x,x,x,T, x,x,x,x, x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,T,T, x,x,x,T, x,T,T,x, x,T,T,x, x,x,x,x, T,x,x,x, x,x,x,T, x,x},
		{x,x,T,T, T,T,T,T, T,T,T,x, x,x,x,x, x,T,x,x, x,x,x,T, x,x,x,T, T,T,T,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,T,x,x, x,T,x,T, T,T,T,x, x,T,x,x, x,x,x,T, T,T,T,T, T,x,T,x, T,T,T,x, x,T,T,T, T,T,T,T, T,T,x,x, x,x,x,T, x,T,T,x, x,x,x,x, x,x,x,T, x,x,x,x, T,T,x,x, x,T,x,x, T,x,x,x, T,x,T,x, x,x,T,x, x,x,T,x, x,T,T,x, x,x,x,x, T,x,x,x, x,T,T,x, x,T,x,T, T,x,x,x, x,T,x,x, x,x,x,T, x,x,x,x, x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,T,T, x,x,x,T, x,T,T,x, x,T,T,x, x,x,x,x, T,x,x,x, x,x,x,T, x,x},
		{x,x,x,T, T,T,T,T, T,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,T,T,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x},
		{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, T,T,T,T, T,x,T,x, T,T,x,x, x,T,T,T, T,T,T,T, T,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x},
		{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, T,T,T,T, T,x,T,x, T,T,x,x, x,T,T,T, T,x,T,T, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x},
		{x,T,T,T, T,T,T,T, T,T,T,x, T,T,x,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,x},
		{x,T,T,T, T,T,T,T, T,T,T,T, T,T,T,x, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,x},
		{x,T,T,T, T,T,T,T, T,T,x,T, T,T,T,x, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,x},
		{x,T,T,T, T,T,T,T, T,T,T,x, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,x},
		{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x},
		{x,x,T,T, T,T,T,T, T,T,T,x, x,x,x,x, x,T,x,x, x,T,x,x, x,x,x,T, T,T,T,x, x,x,x,x, T,x,T,T, T,T,T,x, x,x,x,x, x,x,x,x, x,x,x,x, T,T,x,x, x,T,x,T, T,T,T,x, x,T,x,x, x,x,x,T, T,T,T,T, T,x,T,x, T,T,T,x, x,T,T,T, T,T,T,T, T,T,x,x, x,x,x,T, x,T,T,x, x,x,x,x, x,x,x,T, x,x,x,x, T,T,x,x, x,T,x,x, T,x,x,x, T,x,T,x, x,x,T,x, x,x,T,T, x,T,T,x, x,x,x,x, T,x,x,x, x,T,T,x, x,T,x,T, T,x,x,x, x,T,x,x, x,x,x,T, x,x,x,x, x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,T,T, x,x,x,T, x,T,T,x, x,T,T,x, x,x,x,x, T,x,x,x, x,x,x,T, x,x},
		{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,T, T,T,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x},
		{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,T, T,T,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x},
		{x,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,x,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,T,T,T, T,x},
		{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x},
		{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x},
		{x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x},
		{x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,T,x,T, T,T,T,x, T,T,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,T, x,x,T,x, x,x,x,x, x,x,x,T, x,x,x,x, T,x,x,x, x,T,x,x, x,x,x,x, T,x,x,x, x,x,T,x, x,x,T,x, x,T,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,T,x,T, x,x,x,x, T,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,T,T, x,x,x,x, x,x,x,x, x,T,T,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x},
		{x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,T,x,T, T,T,T,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,T,x, x,x,x,x, x,x,x,T, x,x,x,x, T,x,x,x, x,T,x,x, x,x,x,x, T,x,x,x, x,x,T,x, x,x,T,x, x,T,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,T, x,x,x,x, x,x,T,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,T,T, x,x,x,x, x,x,x,x, x,T,T,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x},
		{x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, T,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,T,x, x,x,x,T, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,T, x,T,T,x, x,x,x,x, x,x,x,x, x,T,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x,x,x, x,x}

	};

} // end Parser


}