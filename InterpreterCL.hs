module InterpreterCL where

--definindo a sintaxe abstrata do calculo lambda
 
data TLam = Var Char      -- variaveis
          | LamAbs Char TLam  -- abstração
          | LamApp TLam TLam      -- aplicação
          deriving (Show,Eq)



freeVars :: TLam -> [Char]
freeVars (Var x) = [x]
freeVars (LamAbs x t) = remove x (freeVars t) 
freeVars (LamApp t1 t2) = (freeVars t1) ++ (freeVars t2)

-- LamAbs 'x' (LamAbs 'y' (LamApp (LamApp (LamApp (LamAbs 'x' (LamApp (Var 'x') (Var 'x'))) (Var 'x')) (Var 'y')) (Var 'z'))) 

isfreeVar :: Char -> TLam -> Bool
isfreeVar x (Var y) = x == y
isfreeVar x (LamAbs y t) = x /= y 
isfreeVar x (LamApp t1 t2) = (isfreeVar x t1) && (isfreeVar x t2)


remove :: Char -> [Char] -> [Char]
remove x [] = []
remove x (h:t) = if (x==h) then remove x t else h:(remove x t)

--remove usando o filter: usando funções de alta ordem
remo :: Char -> [Char] -> [Char]
remo x xs = filter (/= x) xs


--------------------------------------------------------
--------------------------------------------------------
-- Interpreter -----------------------------------------

--substituiçao

subs :: Char -> TLam -> TLam -> TLam
subs x s (Var y) = if (x==y) then s else (Var y)
subs x s (LamAbs y t1) = if ((x /= y) && not(elem y (freeVars s)))
                         then LamAbs y (subs x s t1)
                         else error "problem with subs in LamAbs"
--usar renameTLam na subs acima no error
--subs x s (LamApp t1 t2) =


-- auxiliares
isval :: TLam -> Bool
-- deve retornar True caso o termo seja um valor, False cc.
isval (Var x) = True
isval _ = undefined

-- deve implementar a semântica operacional estrutural vista em aula
-- um passo de avaliação de termos lambda
eval :: TLam -> TLam 
eval (Var x) = Var x
eval (LamAbs x t1) = LamAbs x t1
-- terminar a implementacao comentada aqui
-- casos EAPPABS e EAPP2
--eval (LamApp (LamAbs x t1) t2) = if (isval t2)
--                                 then subs...
--                                 else let t2'= eval t2
--                                      in (LamApp t1 t2')
-- caso EAPP1
eval (LamApp t1 t2) = let t1'= eval t1
                      in (LamApp t1' t2)
                       

-- recebe um TLam (LamABs) e um nome de variavel x e retorna um novo TLam com a primeira variavel ligada renomeada. O novo nome deve ser diferente de x e de
-- qualquer outra var que nao aparece livre em TLam.
-- exemplo: renameTLam (LamAbs y (LamApp (Var y)  (Var z))) = 
--                                 LamAbs w (LamApp (Var w) (Var z))
renameTLam :: Char -> Char -> TLam -> TLam
renameTLam x w (Var y) = if (x == y) then (Var w) else (Var y)
renameTLam x w (LamAbs y t1) = if (x == y) then (LamAbs y t1) 
                               else renameTLam x w t1
renameTLam x w (LamApp t1 t2) = LamApp (renameTLam x w t1) (renameTLam x w t2) 

-- teste  
-- renameTLam 'x' 'w' (LamAbs 'y' (LamApp (LamApp (LamApp (LamAbs 'x' (LamApp (Var 'x') (Var 'x'))) (Var 'x')) (Var 'y')) (Var 'z')))

