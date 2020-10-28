
var botaoExecutar = document.getElementById('execute');

//TESTE MANA -- SE NAO FUNCIONAR APAGAR DAQUI PARA BAIXO
var idBlocoAttack = []; //responsavel por guardar todos os ids blocos de força de ataque que forem criados no workspace
var usuParouDigit ;
// --> Criação do espaço de trabalho blockly
var workspace = Blockly.inject('blocklyDiv',
{toolbox: document.getElementById('toolbox'),
zoom:
{controls: true,
 wheel: true,
 startScale: 1.0,
 maxScale: 3,
 minScale: 0.3,
 scaleSpeed: 1.2},
 trashcan: true,
 maxBlocks:0 } //define o maximo de bloco que pode ser usado por fase
 );

//-------------------
//FUNÇÕES Blockly
            
        //Carregue o intérprete agora e após alterações futuras
            generateCodeAndLoadIntoInterpreter();
            workspace.addChangeListener(function(event){
                if(!(event instanceof Blockly.Events.Ui)){
                    resetInterpreter();
                    generateCodeAndLoadIntoInterpreter();
                    //habilita o botão de execução somente se houver blocos no espaço de trabalho
                    if(code != ''){
                    botaoExecute.disabled = false;
                    botaoReset.disabled = false;//mexer aqui depois
                    
                }else{
                    botaoExecute.disabled = true;
                    botaoReset.disabled = true;
                }///linkar isso ao novo espaco de trabalho

                }
            });
             //Atualiza a quantidade de blocos
           workspace.addChangeListener(onchange); 
           function onchange(event){
                       document.getElementById('capacidade').textContent = 
                           workspace.remainingCapacity();
                   }

               //funcao responsavel por recriar o workspace toda vez que for iniciar uma nova fase,  alterando o limite de blocos
        function recriarWorkspace(qtdMaxBlocos, toolbox){// -> função criada por mim
            console.log("Valor do id toolbox dentro da func recriarWorkspace = "+toolbox);
            code = '';
            qtdBlocosUsados = 0;
            workspace.dispose();
            //console.log(toolbox.value);
            let valorToolbox = toolbox;
                    if(valorToolbox == 0){
                         valorToolbox = '';//utilizado para garantir que o document.getElementById('toolbox') vá sempre pegar um valor valido
                        }
           workspace = Blockly.inject('blocklyDiv',
            {toolbox: document.getElementById('toolbox'+ valorToolbox/*+toolbox*/),//voltar para toolbox
            zoom:
            {controls: true,
             wheel: true,
             startScale: 1.0,
             maxScale: 3,
             minScale: 0.3,
             scaleSpeed: 1.2},
             trashcan: true,
             maxBlocks:qtdMaxBlocos } //define o maximo de bloco que pode ser usado por fase
             );
             console.log("Antes do updateToolbox");
             workspace.updateToolbox(document.getElementById('toolbox'+toolbox));
             console.log("Depois do updateToolbox");
             document.getElementById('capacidade').innerHTML =  workspace.remainingCapacity();
                //aplica a funcao ao novo espaco de trabalho
             function onchange(event){
                    document.getElementById('capacidade').textContent = 
                        workspace.remainingCapacity();//funcao responsavel por diminuir o numero de blocos disponiveis cada vez que um bloco é acrescentado ao espaco de trabalho
                }

            workspace.addChangeListener(onchange);
            onchange();

            workspace.addChangeListener(function(event){
            if(!(event instanceof Blockly.Events.Ui)){
                resetInterpreter();
                generateCodeAndLoadIntoInterpreter();
                //habilita o botão de execução somente se houver blocos no espaço de trabalho
                if(code != ''){
               botaoExecutar.disabled = false;
            }else{
                botaoExecutar.disabled = true;
            }
         }
        });

       
    }
