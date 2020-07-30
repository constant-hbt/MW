
var botaoExecutar = document.getElementById('execute');

//TESTE MANA -- SE NAO FUNCIONAR APAGAR DAQUI PARA BAIXO
//var blocosVarForcaMana = []; //responsavel por guardar todos os ids blocos de força de ataque que forem criados no workspace
 
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

       //TESTE MANA -- CASO DER ERRADO APAGAR APARTIR DAQUI
    /*
       workspace.addChangeListener(captarInsercao);
       function captarInsercao(event){
                   if(event.type == Blockly.Events.BLOCK_CREATE
                    && workspace.getBlockById(event.blockId).styleName_ == "forca_atack"){
                      console.log(workspace.getBlockById(event.blockId));
                       //console.log(event);
                       //console.log("Entrei");
                      ids.push(workspace.getBlockById(event.blockId).id);
                      console.log(ids);
                   }
               }
               workspace.addChangeListener(captarRemocao);
       function captarRemocao(event){
           if(event.type == Blockly.Events.BLOCK_DELETE
           ){
               console.log(event.oldXml.textContent);
               console.log(ids);
               for(var i=0; i<= ids.length; i++){
                     if(ids[i] == event.ids){
                         ids.splice(i,1);
                         console.log("Opa deletou um bloco do loop em");
                     }
                    
               }
               console.log(ids);
           }
       }         

*/
       //CASO TESTE MANA DER ERRADO APAGAR ATE AQUI

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
