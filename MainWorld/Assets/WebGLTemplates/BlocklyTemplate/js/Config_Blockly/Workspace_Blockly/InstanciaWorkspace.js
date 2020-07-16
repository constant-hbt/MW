
var botaoExecutar = document.getElementById('execute');

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

//----------------------

       

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
        function recriarWorkspace(qtdMaxBlocos){// -> função criada por mim
            code = '';
            qtdBlocosUsados = 0;
            workspace.dispose();
           workspace = Blockly.inject('blocklyDiv',
            {toolbox: document.getElementById('toolbox'),
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
