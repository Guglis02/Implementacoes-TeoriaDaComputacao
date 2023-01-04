
module TLam where

import Data.List

-- Definindo a sintaxe abstrata do calculo lambda

data Term = Var Char
          | Abs Char Term
          | App Term Term deriving (Show, Eq)


-- Sintaxe concreta
-- "(lam x . x) y"
-- Sintaxe abstrata
-- App (Abs 'x' (Var 'x'))(Var 'y')


freeVars :: Term -> [Char]
freeVars (Var x)     = [x] 
freeVars (Abs x t)   = delete x (freeVars t)
freeVars (App t1 t2) = freeVars(t1) ++ freeVars(t2) 


-- Trabalhando representacao canonica: distancia estatica
-- Termos lambda Nameless
data TermNL = VarNL Int
            | AbsNL TermNL
            | AppNL TermNL TermNL
     deriving (Show,Eq)

-- Contexto de variaveis livres
type Contexto = [(Char,Int)]

-- Exemplo de contexto
gamma :: Contexto
gamma = [ ('z', 2), ('y', 1), ('x', 0)]


-- Removenames (Abs 'x' (Var 'x')) = AbsNL (VarNL 0)
-- nota : (Contexto, TermNL) 
removenames :: Term -> Contexto -> TermNL
removenames (Var x) c   = VarNL (findfirst x c)
removenames (Abs x t) c = let t' = removenames t (insertC x c) 
                          in  AbsNL t'
removenames (App t1 t2) c = let t1' = removenames t1 c
                                t2' = removenames t2 c
                            in AppNL t1' t2' 

t1 = (App (Abs 'b' (App (Var 'b') (Abs 'x' (Var 'b')))) (App (Var 'a') (Abs 'z' (Var 'a'))))

context1 :: Contexto
context1 = [('a',1),('b',0)]

findfirst :: Char -> Contexto -> Int
findfirst x [] = error "Variável não encontrada no contexto"
findfirst x c = if x == (fst (last c)) then snd(last c) else findfirst x (init c)

insertC :: Char -> Contexto -> Contexto
insertC x c = [(fst a,(snd a)+1) |  a <- c]++[(x,0)]

findfirst' :: Int -> Contexto -> Char
findfirst' x c = if x == (snd (last c)) then fst(last c) else findfirst' x (init c)

restorenames :: TermNL -> Contexto -> Term
restorenames (VarNL x) c = Var (findfirst' x c)
restorenames (AbsNL t) c = let charc = geraChar c ['a'..'z']
                               c' = insertC charc c
                           in (Abs charc (restorenames t c'))
restorenames (AppNL t1 t2) c = let t1' = restorenames t1 c
                                   t2' = restorenames t2 c
                               in App t1' t2'

-- Verifica se o char c está no contexto
verificaCC :: Contexto -> Char -> Bool
verificaCC [] c = False
verificaCC ((a,_):b) c = if (a==c) then True 
                         else verificaCC b c

-- Gera uma var nova que não está no contexto 
geraChar :: Contexto -> [Char] -> Char
geraChar c [] = error "todas as letras usadas"
geraChar c (a:b) = if (verificaCC c a) then (geraChar c b) else a
-----------------------------------------------------




-- Funcoes que serao utilizadas na semantica do CL

subs :: Char -> Term -> Term -> Term
subs x t (Var y) = if (x == y) then t else (Var y)
subs x t (Abs y t12) = if ((x /= y) && (notIn x (freeVars t12))) then (Abs x (subs x t t12)) else (Abs y t12)
subs x t (App t1 t2) = App (subs x t t1) (subs x t t2)

notIn :: Char -> [Char] -> Bool
notIn x y = notElem x y          

isVal :: Term -> Bool
isVal (Abs x t21) = True
isVal (Var x)     = True
isVal _           = False


-- Semantica operacional Call-by-value (ordem de avaliacao)
eval :: Term -> Term
eval (App (Abs x t12) t2) = if (isVal t2) then subs x t2 t12 
                            else let t2' = eval t2
                                 in (App (Abs x t12) t2')
eval (App t1 t2) = let t1'= eval t1
                   in (App t1' t2)
eval x                    = x              

-- Funcao que aplica recursivamente eval ate que nao tenha mais redex
interpret :: Term -> Term
interpret t = let t' = eval t
              in if (t==t') then t else interpret t'



--main = getContents >>= print . interpret . parserlamb .lexer
            
















