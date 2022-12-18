
module TLam where

import Data.List

-- Definindo a sintaxe abstrata do calculo lambda

data Term = Var Char
          | Abs Char Term
          | App Term Term deriving (Show)


-- Sintaxe concreta
-- "(lam x . x) y"
-- Sintaxe abstrata
-- App (Abs 'x' (Var 'x'))(Var 'y')



freeVars :: Term -> [Char]
freeVars (Var x)     = [x] 
freeVars (Abs x t)   = delete x (freeVars t)
freeVars (App t1 t2) = freeVars(t1) ++ freeVars(t2) 

-- Funcoes que serao utilizadas na semantica do CL

subs :: Char -> Term -> Term -> Term
subs x t (Var y) = if (x == y) then t else (Var y)
subs x t (Abs y t12) = if ((x /= y) && (notIn x (freeVars t))) then (Abs x (subs x t t12)) else (Abs y t12)
subs x t (App t1 t2) = App (subs x t t1) (subs x t t2)


notIn :: Char -> [Char] -> Bool
notIn x y = notElem x y          

isVal :: Term -> Bool
isVal (Abs x t21) = True
isVal (Var x)     = True
isVal _           = False


eval :: Term -> Term
eval (App (Abs x t12) t2) = if (isVal t2) then subs x t2 t12 
                            else let t2' = eval t2
                                 in (App (Abs x t12) t2')
eval (App t1 t2) = let t1'= eval t1
                   in (App t1' t2)
eval x                    = x              

-- ToDo: funcao que aplica recursivamente eval ateh que nao tenha mais redex
interpret :: Term -> Term
interpret t = let t' = eval t
              in if (t==t') then t else interpret t'



            
















