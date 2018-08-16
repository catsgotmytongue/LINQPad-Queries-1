<Query Kind="FSharpProgram">
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.dll</Reference>
  <NuGetReference>FParsec</NuGetReference>
  <Namespace>FParsec</Namespace>
  <Namespace>FParsec.Internal</Namespace>
  <Namespace>Microsoft.FSharp.Collections</Namespace>
  <Namespace>Microsoft.FSharp.Control</Namespace>
  <Namespace>Microsoft.FSharp.Core</Namespace>
  <Namespace>Microsoft.FSharp.Core.CompilerServices</Namespace>
  <Namespace>Microsoft.FSharp.Data.UnitSystems.SI.UnitNames</Namespace>
  <Namespace>Microsoft.FSharp.Linq</Namespace>
  <Namespace>Microsoft.FSharp.Linq.QueryRunExtensions</Namespace>
  <Namespace>Microsoft.FSharp.Linq.RuntimeHelpers</Namespace>
  <Namespace>Microsoft.FSharp.NativeInterop</Namespace>
  <Namespace>Microsoft.FSharp.Quotations</Namespace>
  <Namespace>Microsoft.FSharp.Reflection</Namespace>
</Query>

(*
http://chronos-st.blogspot.com/2007/12/smalltalk-in-one-page.html
http://www.csci.csusb.edu/dick/samples/smalltalk.syntax.html
*)
open FParsec

type Name = string
type Ident = Name
type PseudoVariableReference = 
    |  Self of string
    | Super of string
    | ThisContext of string
    

let test parser text =
    match (run parser text) with
    | Success(result,_,_) -> printfn "Success: %A" result
    | Failure(_,error,_) -> printfn "Error: %A" error


let Character = letter
let WhitespaceCharacter = spaces
let ws = WhitespaceCharacter

(* literals start *)
let Letter = asciiLetter // [A-Z][a-z]
let DecimalDigit = digit // [0-9]

let self = stringReturn "self" Self
let super = stringReturn "Super" Super
let thisContext = stringReturn "thisContext" ThisContext
let pseudoRef = self <|> super <|> thisContext


    
//let pPseudoVariableReference =                   
//                   [ "self" ; "super" ; "thisContext" ] |> Seq.map pstring 
//                   
//test pPseudoVariableReference "self"
//let LetterOrDigit = (DecimalDigit <|> Letter)
//let identStart = (Letter <|> underscore)
//let Identifier =  pipe2 identStart (zeroOrMore (Letter <|> underscore <|> DecimalDigit))
let pIdentifier str =
    let isIdentifierFirstChar c = isLetter c || c = '_'
    let isIdentifierChar c = isLetter c || isDigit c || c = '_'
    many1Satisfy2L isIdentifierFirstChar isIdentifierChar "identifier"
    //|>> fun s -> Ident(s)
    
//  Comment = '"', {CommentCharacter}, '"'
let commentDelim = "\""
let notCommentDelim = satisfy(fun c-> c <> ( commentDelim.[0] ) ) (* Any character other than a double quote *)
let CommentStart = pstring commentDelim
let CommentEnd = pstring commentDelim
let Comment = between CommentStart CommentEnd (manyChars notCommentDelim)

//let OptionalWhitespace = WhitespaceCharacter <|> Comment

let id = pIdentifier "_qwo8i_djc_A"

printfn "%A" id
            
test (Comment) "\" a comment should be contained in quotes\"" |> ignore

//http://chronos-st.blogspot.com/2007/12/smalltalk-in-one-page.html
//http://www.csci.csusb.edu/dick/samples/smalltalk.syntax.html
//
//Formal EBNF Specification of Smalltalk Syntax
//
//   1. Character = ? Any Unicode character ?;
//   2. WhitespaceCharacter = ? Any space, newline or horizontal tab character ?;
//   3. DecimalDigit = "0" | "1" | "2" | "3" | "4" | "5" | "6" | "7" | "8" | "9";
//   4. Letter = "A" | "B" | "C" | "D" | "E" | "F" | "G" | "H" | "I" | "J" | "K" | "L" | "M"
//                      | "N" | "O" | "P" | "Q" | "R" | "S" | "T" | "U" | "V" | "W" | "X" | "Y" | "Z"
//                      | "a" | "b" | "c" | "d" | "e" | "f" | "g" | "h" | "i" | "j" | "k" | "l" | "m"
//                      | "n" | "o" | "p" | "q" | "r" | "s" | "t" | "u" | "v" | "w" | "x" | "y" | "z";
//   5. CommentCharacter = Character - '"';
//              (* Any character other than a double quote *)
//   6. Comment = '"', {CommentCharacter}, '"';
//   7. OptionalWhitespace = {WhitespaceCharacter | Comment};
//   8. Whitespace = (WhitespaceCharacter | Comment), OptionalWhitespace;
//   9. LetterOrDigit =
//                      DecimalDigit
//                      | Letter;
//  10. Identifier = (Letter | "_"), {(LetterOrDigit | "_")};
//  11. Reference = Identifier;
//  12. ConstantReference =
//                      "nil"
//                      | "false"
//                      | "true";
//  13. PseudoVariableReference =
//                      "self"
//                      | "super"
//                      | "thisContext";
//              (* "thisContext" is not defined by the ANSI Standard, but is widely used anyway *)
//  14. ReservedIdentifier =
//                      PseudoVariableReference
//                      | ConstantReference;
//  15. BindableIdentifier = Identifier - ReservedIdentifier;
//  16. UnaryMessageSelector = Identifier;
//  17. Keyword = Identifier, ":";
//  18. KeywordMessageSelector = Keyword, {Keyword};
//  19. BinarySelectorChar = "~" | "!" | "@" | "%" | "&" | "*" | "-" | "+" | "=" | "|" | "\" | "<" | ">" | "," | "?" | "/";
//  20. BinaryMessageSelector = BinarySelectorChar, [BinarySelectorChar];
//
//  21. IntegerLiteral = ["-"], UnsignedIntegerLiteral;
//  22. UnsignedIntegerLiteral =
//                      DecimalIntegerLiteral
//                      | Radix, "r", BaseNIntegerLiteral;
//  23. DecimalIntegerLiteral = DecimalDigit, {DecimalDigit};
//  24. Radix = DecimalIntegerLiteral;
//  25. BaseNIntegerLiteral = LetterOrDigit, {LetterOrDigit};
//  26. ScaledDecimalLiteral = ["-"], DecimalIntegerLiteral, [".", DecimalIntegerLiteral], "s", [DecimalIntegerLiteral];
//  27. FloatingPointLiteral = ["-"], DecimalIntegerLiteral, (".", DecimalIntegerLiteral, [Exponent] | Exponent);
//  28. Exponent = ("e" | "d" | "q"), [["-"], DecimalIntegerLiteral];
//  29. CharacterLiteral = "$", Character;
//  30. StringLiteral = "'", {StringLiteralCharacter | "''"}, "'";
//              (* To embed a "'" character in a String literal, use two consecutive single quotes *)
//  31. StringLiteralCharacter = Character - "'";
//              (* Any character other than a single quote *)
//  32. SymbolInArrayLiteral =
//                      UnaryMessageSelector - ConstantReference
//                      | KeywordMessageSelector
//                      | BinaryMessageSelector;
//  33. SymbolLiteral = "#", (SymbolInArrayLiteral | ConstantReference | StringLiteral);
//  34. ArrayLiteral =
//                      ObjectArrayLiteral
//                      | ByteArrayLiteral;
//  35. ObjectArrayLiteral = "#", NestedObjectArrayLiteral;
//  36. NestedObjectArrayLiteral = "(", OptionalWhitespace, [LiteralArrayElement, {Whitespace, LiteralArrayElement}], OptionalWhitespace, ")";
//  37. LiteralArrayElement =
//                      Literal - BlockLiteral
//                      | NestedObjectArrayLiteral
//                      | SymbolInArrayLiteral
//                      | ConstantReference;
//  38. ByteArrayLiteral = "#[", OptionalWhitespace, [UnsignedIntegerLiteral, {Whitespace, UnsignedIntegerLiteral}], OptionalWhitespace,"]";
//
//      (* The preceding production rules would usually be handled by the lexical analyzer;
//           the following production rules would usually be handled by the parser *)
//  39. FormalBlockArgumentDeclaration = ":", BindableIdentifier;
//  40. FormalBlockArgumentDeclarationList = FormalBlockArgumentDeclaration, {Whitespace, FormalBlockArgumentDeclaration};
//  41. BlockLiteral = "[", [OptionalWhitespace, FormalBlockArgumentDeclarationList, OptionalWhitespace, "|"], ExecutableCode, OptionalWhitespace, "]";
//
//  42. Literal = ConstantReference
//                      | IntegerLiteral
//                      | ScaledDecimalLiteral
//                      | FloatingPointLiteral
//                      | CharacterLiteral
//                      | StringLiteral
//                      | SymbolLiteral
//                      | ArrayLiteral
//                      | BlockLiteral;
//
//  43. NestedExpression = "(", Statement, OptionalWhitespace, ")";
//  44. Operand =
//                      Literal
//                      | Reference
//                      | NestedExpression;
//
//  45. UnaryMessage = UnaryMessageSelector;
//  46. UnaryMessageChain = {OptionalWhitespace, UnaryMessage};
//  47. BinaryMessageOperand = Operand, UnaryMessageChain;
//  48. BinaryMessage = BinaryMessageSelector, OptionalWhitespace, BinaryMessageOperand;
//  49. BinaryMessageChain = {OptionalWhitespace, BinaryMessage};
//  50. KeywordMessageArgument = BinaryMessageOperand, BinaryMessageChain;
//  51. KeywordMessageSegment = Keyword, OptionalWhitespace, KeywordMessageArgument;
//  52. KeywordMessage = KeywordMessageSegment, {OptionalWhitespace, KeywordMessageSegment};
//  53. MessageChain =
//                      UnaryMessage, UnaryMessageChain, BinaryMessageChain, [KeywordMessage]
//                      | BinaryMessage, BinaryMessageChain, [KeywordMessage]
//                      | KeywordMessage;
//  54. CascadedMessage = ";", OptionalWhitespace, MessageChain;
//  55. Expression = Operand, [OptionalWhitespace, MessageChain, {OptionalWhitespace, CascadedMessage}];
//
//  56. AssignmentOperation = OptionalWhitespace, BindableIdentifier, OptionalWhitespace, ":=";
//  57. Statement = {AssignmentOperation}, OptionalWhitespace, Expression;
//  58. MethodReturnOperator = OptionalWhitespace, "^";
//  59. FinalStatement = [MethodReturnOperator], Statement;
//  60. LocalVariableDeclarationList = OptionalWhitespace, "|", OptionalWhitespace, [BindableIdentifier, {Whitespace, BindableIdentifier}], OptionalWhitespace, "|";
//  61. ExecutableCode = [LocalVariableDeclarationList], [{Statement, OptionalWhitespace, "."}, FinalStatement, ["."]];
//
//  62. UnaryMethodHeader = UnaryMessageSelector;
//  63. BinaryMethodHeader = BinaryMessageSelector, OptionalWhitespace, BindableIdentifier;
//  64. KeywordMethodHeaderSegment = Keyword, OptionalWhitespace, BindableIdentifier;
//  65. KeywordMethodHeader = KeywordMethodHeaderSegment, {Whitespace, KeywordMethodHeaderSegment};
//  66. MethodHeader =
//                      UnaryMethodHeader
//                      | BinaryMethodHeader
//                      | KeywordMethodHeader;
//  67. MethodDeclaration = OptionalWhiteSpace, MethodHeader, ExecutableCode;
