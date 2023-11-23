# ReversibleTuringMachine

Trabalho desenvolvido para a disciplina de Teoria da Computação.
Consiste em uma implementação de um simulador de Máquina de Turing reversível em C#, baseada no artigo **Logical Reversibility of Computation**

Para rodar o programa, basta executar o comando:  
`.\MaquinaDeTuringReversivel.exe arquivo_de_entrada`

O arquivo de entrada deve seguir a seguinte formatação:

- A primeira linha apresenta números, que indicam: número de estados, número de símbolos no alfabeto de entrada, número de símbolos no alfabeto da fita e número de transições, respectivamente.
- A seguir, temos os estados sendo o estado de aceitação, o último.
- Na próxima linha alfabeto de entrada.
- E após, alfabeto da fita.
- Nas linhas sequentes temos a funcão de transição (como explicada no artigo).
- Depois da funcão de transição, segue uma entrada.

C. H. Bennett, "Logical Reversibility of Computation," in *IBM Journal of Research and Development*
, vol. 17, no. 6, pp. 525-532, Nov. 1973, doi: 10.1147/rd.176.0525.
