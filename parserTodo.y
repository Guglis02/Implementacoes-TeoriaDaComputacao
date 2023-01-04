{
module ParserLambda where
import Data.Char
}

%name parserlamb
%tokentype { Token }
%error { parseError }

%token
	lam { TokenLam } 
	var { TokenVar $$ }
	'.' { TokenPoint }
	'(' { TokenOB }
	')' { TokenCB }
	
%%

-- regras de producao da gramatica


Term : var                         { (Var $1) }
	 |'(' lam var  '.' Term ')'    { (Abs $3 $5 )}
	 |'(' Term Term ')'            { (App $2 $3 ) }


{

parseError :: [Token] -> a
parseError b = error "Parse Error"


data Term = Var Char
          | Abs Char Term
          | App Term Term deriving (Show)



data Token 
		= TokenVar Char
		| TokenPoint
		| TokenOB
		| TokenCB
		| TokenLam 
	deriving Show


-- ToDo
-- Função que recebe uma string e gera uma lista de tokens
lexer :: String -> [Token]
lexer [] = []
lexer (c:cs)
	| isSpace c = lexer cs
	| c == '.' = TokenPoint : lexer cs
	| c == '(' = TokenOB : lexer cs
	| c == ')' = TokenCB : lexer cs
	| isAlpha c = let (a, rest) = span isAlpha(c:cs)
				  in if (a == "lam") then TokenLam : lexer rest
					 else (TokenVar c) : lexer rest



main = getContents >>= print . parserlamb . lexer

}
