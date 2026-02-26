1. Apresentação do projeto 


    .Calculadora similar à calculadora padrão do Windows

    .Consiste do meu primeiro projeto. Fiz com a intenção de praticar a programação C# relacionada apenas aos elementos gráficos (sem banco de dados)





2. Descrição geral do projeto


    .O projeto teve como base os tutoriais padrões que se encontram sobre o tema, e desta base houve um desenvolvimento até se aproximar do funcionamento      da calculadora padrão do Windows. Se fez uso de variáveis globais, e de um evento para cada botão

    .Os cálculos possuem a seguinte base: primeiro número - operação - segundo número - resultado

    .O projeto possui dois visores: Um que exibe o número sendo informado pelo usuário (o visor maior), e o visor que exibe o cálculo (visor menor)

    .As variáveis do projeto podem ser divididas em três tipos: para armazenar números, para armazenar o tipo de operação, e variáveis booleanas que           indicam um estado

    .Foi aplicado a seguinte regra: Todos os números que possuem mais de 16 dígitos serão apresentados em notação científica. E não é possível o usuário       informar um número maior que 16 dígitos

    .O tipo de dado numérico usado para fazer cálculos foi o tipo decimal






3. Detalhes e desafios do projeto

 
    .O número informado pelo usuário é inicialmente captado pela variável 'visorNumero'

    .A operação informada é captada pela variável 'operacao'

    .Ao se pressionar um botão de operação, o valor contido em 'visorNumero' é convertido para decimal e armazenada na variável 'primeiroNumero', que          armazena o primeiro número de um cálculo

    .As variáveis de estado são as seguintes:


      -segundoNumero: Comunica que o segundo número de uma operação foi informado. Recebe o valor 'true' ao se informar um número quando a variável              'operacao' tiver um valor atribuído, habilitando o uso do botão de igual (na calculadora do Windows, ao apertar o botão de igual tendo informado           apenas o primeiro número, o segundo número recebe o mesmo valor do primeiro. Neste projeto, o botão de igual fica inoperante enquanto o segundo            número não for informado)

      -novoNumero: Comunica que quando o usuário apertar um botão de número, este número irá sobrepor o número que está no visor

      -visorResultado: Comunica que o número da tabela é um resultado de um cálculo pelo botão de igual

      -visorResultado2: Comunica que o número da tabela é um resultado de um cálculo pelo botões 1/x, raiz quadrada, elevar ao quadrado, ou porcentagem

      -calculoInvalido: Comunica que o cálculo é inválido (como fazer a raiz quadrada de um número negativo), ou o limite do tipo de dado decimal foi            excedido

   

    .A inserção de um número se dá por duas maneiras: sobrepor o número do visor, ou ser adicionado ao(s) número(s) no visor. As situações são controladas     pela variável 'novoNumero'

    .Levou-se em consideração que, ao usuário fazer um cálculo, pode-se exceder o limite do tipo de dado decimal. Assim,  todos os códigos de cálculos         foram colocados dentro de estruturas 'try-catch'

    .Cada evento relacionado a um número verifica se o visor tem um número com menos de 16 dígitos antes de autorizar a inserção de um novo número. Um         visor pode ter no máximo 16 (se o número for positivo e não tiver vírgula), 17 (se o número por positivo e tiver vírgula ou for negativo e não tiver       vírgula) ou 18 (se o número tiver vírgula e for negativo) caracteres

    .Para facilitar a identificação do número pelo usuário, foi determinado que o número no visor deve ser exibido com ponto de milhar, ou seja, com           formatação. Mas a formatação apresentou os seguintes problemas:


      -Caso o número seja fracionado (tenha vírgula), os números zeros adicionados na parte decimal não são exibidos para o usuário no momento da                inserção, pois a formatação remove da visualização os números zero supérfluos. Por exemplo, se o usuário informar o número 22,001, os números zero         informados só ficarão visíveis quando se informar o último número um.

      -Caso o número seja fracionado e a parte inteira do número for o número zero, a formatação remove este número zero da visualização.

      -Usei a seguinte solução: os números fracionados serão armazenados em duas variáveis. A parte inteira do número será armazenada na variável                'parteInteira', e a parte decimal será armazenada na variável 'parteDecimal'. Quando a parte inteira não for o número zero, ela será exibida com           formatação e a parte decimal sem formatação. Quando a parte inteira for o número zero, as duas partes serão exibidas sem formatação


    .Após apertar um  botão de operação ou calcular uma operação (botão de igual, raiz quadrada, elevar ao quadrado, 1/x, porcentagem), as duas partes do      número fracionado são unidas na variável 'visorNumero'. É levado em consideração o uso indevido da vírgula e da casa decimal pelo usuário, portanto        ocorrem os seguintes procedimentos:


      -Se o usuário usar a vírgula, mas não inserir números na casa decimal, a vírgula é removida

      -Se o usuário informar um valor supérfluo na casa decimal (apenas números zero), a casa decimal e a vírgula são removidas

      -Se o usuário informar um valor válido na casa decimal, mas conter números zero supérfluos, estes números zero são removidos



    .Para adicionar a vírgula se levou em consideração que no usuário não pode inserir números acima de 16 dígitos. Ou seja, só pode inserir vírgula           quando o visor tiver no máximo 15 dígitos. E o usuário só pode inserir uma  vírgula quando a variável 'novoNumero' tiver o valor 'false' (com exceção      se o número do visor for o número zero). Ou seja, levou-se em consideração que em resultados de cálculos não se pode adicionar vírgula

    .Levou-se em consideração que o botão de backspace só pode funcionar quando o usuário estiver informando um número, ou seja, não funciona quando o         número exibido pelo visor for o resultado de um cálculo. Caso a variável 'visorNumero' fique sem caracteres, o visor exibirá o número zero, e o botão      de backspace fica inerte 

    .O usuário pode inserir ou remover o sinal de negativo do primeiro ou segundo número de uma operação, ou no resultado de um cálculo (botão de igual,       raiz quadrada, elevar ao quadrado, 1/x, porcentagem)

    .Levou-se em consideração que o usuário pode ficar apertando botões de operação em sequência (o último botão apertado será a operação do cálculo)

    .É possível o usuário encadear vários cálculos antes de apertar o botão de igual. Para que isto seja possível, todos botões de operação realizam o         cálculo informado antes de o usuário ter apertado um novo botão de operação (quando se aperta um botão de operação quando a variável segundoNumero         tiver o valor 'true'). O resultado deste cálculo será o primeiro número do novo cálculo

    .Levou-se em consideração que o usuário pode apertar um botão de operação sem ter informado nenhum primeiro número (quando o visor estiver exibindo o      número zero inicial). Neste caso, o número zero é tomado como o primeiro número do cálculo

    .Levou-se em consideração que o usuário pode apertar um botão de operação quando o visor estiver exibindo o resultado de um cálculo (pelo botão de         igual). Neste caso este resultado é tomado como o primeiro número do próximo cálculo (isto é auxiliado pela variável 'visorResultado')

    .As operações 1/x, raiz quadrada e elevar ao quadrado podem ser aplicados sobre o primeiro e segundo número de uma operação, ou sobre um resultado de      um cálculo (botão de igual). Caso o usuário use um botão de número quando o visor estiver exibindo o resultado de um dos cálculos mencionados              anteriormente, o número pressionado irá soprepor o resultado no visor

    .Se usou a variável 'visorResultado2' para solucionar o seguinte problema: O resultado das operações  1/x, raiz quadrada e elevar ao quadrado podem        ser sobrepostos, portanto seria necessário atribuir o valor 'true' para a variável 'novoNumero'. Mas ao fazer isto, seria possível o usuário usar os       botões das operações anteriores após o usuário apertar um botão das quatro operações (adição, suubtração, multiplicação, divisão), sem ter informado o     segundo número do cálculo. Eu queria que, ao usuário usar uma das quatro operações, mas sem informar o segundo número, os botões das operações             mencionadas no começo ficassem inertes. Portanto, a variável 'visorResultado2' permite a sobreposição do resultado na tela ao mesmo tempo que a            variável 'novoNumero' contém o valor 'false', solucionando o problema apresentado

    .O botão de porcentagem funciona apenas no segundo número de um cálculo. A operações sobre este número são iguais às da calculadora padrão do Windows:


      -Se a operação do cálculo for multiplicação ou divisão, o novo segundo número será ele dividido por 100

      -Se a operação do cálculo for adição ou subtração, o novo segundo número será ele divido por 100 e depois multiplicado pelo primeiro número


    .O botão de limpeza (C) retorna a calculadora ao estado inicial

    .A variável 'calculoInvalido' faz com que ao ocorrer um cálculo inválido os únicos botões que o usuário possa usar são os numéricos (o número irá          sobrepor a mensagem de erro e se iniciará um novo cálculo) ou o botão de limpeza (retornar a calculadora ao seu estado inicial)

    .Único problema que não consegui resolver: Todos os números que possuem mais de 16 dígitos são exibidos em notação científica. Porém, queria abrir uma     exceção para as dízimas periódicas. Gostaria que elas fossem exibidas normalmente com os 16 dígitos. Mas não consegui achar uma maneira de descobrir       quando um cálculo de divisão resulta em dízima periódica, e aplicar esta exceção a elas

    .Tudo aqui mencionado consta no 'commit 1'






4. Refatorações planejadas no futuro


    .Remover as variáveis globais e fazer uso de classes []

    .Substituir as variáveis de estado por um enumerador []

    .Aplicar o padrão repositório e remover o código de dentro dos eventos []

    .Reduzir o número de eventos. Usar um evento para os números e um evento para as operações básicas []

    .Achar uma maneira de resolver o problema das dízimas periódicas mencionado anteriormente []
