
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

//----------------------

         //#region ENTRADA
         workspace.addChangeListener(captarInsercao);
         function captarInsercao(event){
             
             console.log("Esse é o evento" +event);
                     if(event.type == Blockly.Events.BLOCK_CREATE
                      && event.xml.attributes[0].textContent == "valor_ataque"){
                         console.log("ENTREI DENTRO DO OUVINTE DE CAPTAR INSERCAO");
                         obj = 
                            {
                                id:workspace.getBlockById(event.blockId).id,
                                valor:event.xml.textContent,
                            };
                            idBlocoAttack.push(obj);
                        console.log(idBlocoAttack);//APAGAR DEPOIS
                     }
                 }
         //#endregion 
          
         //#region ATUALIZAÇÃO
         workspace.addChangeListener(captarAtualizacao);
         function captarAtualizacao(event){
             console.log("Dentro do change" + event.name);
             if(event.type == Blockly.Events.BLOCK_CHANGE
             && event.name == "valor_ataque"){
                 console.log("ENTREI DENTRO DO OUVINTE DE CAPTAR ATUALIZACAO");
                 var valManaatt;
                 clearTimeout(usuParouDigit);
                 usuParouDigit = setTimeout(function(){
                     console.log(event);
                     for(var i=0; i<idBlocoAttack.length;i++){
                             if(idBlocoAttack[i].id == event.blockId){
                                 console.log( "Valor antes de alterar no array ids = "+ idBlocoAttack[i].valor);
                                 idBlocoAttack[i].valor = event.newValue;
                                 console.log("Valor depois de alterar o array ids = "+ idBlocoAttack[i].valor);
                                 valManaatt = idBlocoAttack[i].valor;
                             }   
                     }
                     unityInstance.SendMessage('ControllerFase','alteracaoDisponibilidadeManaRemocao', valManaatt);
                     //mana.value -= valManaatt; //VAI ENVIAR UMA REQUISICAO AO UNITY PARA MUDAR O VALOR DA MANA
                 }, 1000);
 
             }
         }       
         //#endregion

      //#region REMOCAO
      workspace.addChangeListener(captarRemocao);
      function captarRemocao(event){
         
          if(event.type == Blockly.Events.BLOCK_DELETE){
              var bool = false;
              console.log("ENTREI DENTRO DO OUVINTE DE CAPTAR REMOCAO");
              for(var x =0; x< idBlocoAttack.length; x++){//verifica se o bloco é do tipo forcaAttack
                  if(idBlocoAttack[x].id == event.blockId){//caso não for, nao executa as instrucoes a seguir
                      bool = true;
                  }
              }

              if(bool){
                  console.log(event);
                  var valManaAntigo ;
                      for(var i=0; i< idBlocoAttack.length; i++){
                          if(idBlocoAttack[i].id == event.blockId){
                              valManaAntigo = idBlocoAttack[i].valor;
                              idBlocoAttack.splice(i,1);
                        
                              console.log("Opa deletou um bloco do loop em");
                          }
                   
                      }
                  console.log(idBlocoAttack);
                      //var voltarValMana;APAGAR
                        //  voltarValMana = parseInt(mana.value) + parseInt(valManaAntigo);APAGAR
                          unityInstance.SendMessage('ControllerFase', 'alteracaoDisponibilidadeManaAdicao',valManaAntigo);
                         // mana.value = voltarValMana;//VAI ENVIAR UMA REQUISICAO PRO UNITY
              }else{
                  console.log("Não sou o bloco desejado");
              }
              
          }
      }
     //#endregion
//#endregion 


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

            //TESTE MANA -- CASO DER ERRADO APAGAR APARTIR DAQUI
          //#region FUNÇÕES RESPONSAVEIS POR CAPTAR A ENTRADA, ATUALIZAÇÃO E REMOCÃO DO BLOCO forcaAttack
                    //#region ENTRADA
                    workspace.addChangeListener(captarInsercao);
                    function captarInsercao(event){
                        
                        console.log("Esse é o evento" +event);
                                if(event.type == Blockly.Events.BLOCK_CREATE
                                 && event.xml.attributes[0].textContent == "valor_ataque"){
                                    console.log("ENTREI DENTRO DO OUVINTE DE CAPTAR INSERCAO");
                                    obj = 
                                       {
                                           id:workspace.getBlockById(event.blockId).id,
                                           valor:event.xml.textContent,
                                       };
                                       idBlocoAttack.push(obj);
                                   console.log(idBlocoAttack);//APAGAR DEPOIS
                                }
                            }
                    //#endregion 
                     
                    //#region ATUALIZAÇÃO
                    workspace.addChangeListener(captarAtualizacao);
                    function captarAtualizacao(event){
                        console.log("Dentro do change" + event.name);
                        if(event.type == Blockly.Events.BLOCK_CHANGE
                        && event.name == "valor_ataque"){
                            console.log("ENTREI DENTRO DO OUVINTE DE CAPTAR ATUALIZACAO");
                            var valManaatt;
                            clearTimeout(usuParouDigit);
                            usuParouDigit = setTimeout(function(){
                                console.log(event);
                                for(var i=0; i<idBlocoAttack.length;i++){
                                        if(idBlocoAttack[i].id == event.blockId){
                                            console.log( "Valor antes de alterar no array ids = "+ idBlocoAttack[i].valor);
                                            idBlocoAttack[i].valor = event.newValue;
                                            console.log("Valor depois de alterar o array ids = "+ idBlocoAttack[i].valor);
                                            valManaatt = idBlocoAttack[i].valor;
                                        }   
                                }
                                unityInstance.SendMessage('ControllerFase','alteracaoDisponibilidadeManaRemocao', valManaatt);
                                //mana.value -= valManaatt; //VAI ENVIAR UMA REQUISICAO AO UNITY PARA MUDAR O VALOR DA MANA
                            }, 1000);
            
                        }
                    }       
                    //#endregion

                 //#region REMOCAO
                 workspace.addChangeListener(captarRemocao);
                 function captarRemocao(event){
                    
                     if(event.type == Blockly.Events.BLOCK_DELETE){
                         var bool = false;
                         console.log("ENTREI DENTRO DO OUVINTE DE CAPTAR REMOCAO");
                         for(var x =0; x< idBlocoAttack.length; x++){//verifica se o bloco é do tipo forcaAttack
                             if(idBlocoAttack[x].id == event.blockId){//caso não for, nao executa as instrucoes a seguir
                                 bool = true;
                             }
                         }
         
                         if(bool){
                             console.log(event);
                             var valManaAntigo ;
                                 for(var i=0; i< idBlocoAttack.length; i++){
                                     if(idBlocoAttack[i].id == event.blockId){
                                         valManaAntigo = idBlocoAttack[i].valor;
                                         idBlocoAttack.splice(i,1);
                                   
                                         console.log("Opa deletou um bloco do loop em");
                                     }
                              
                                 }
                             console.log(idBlocoAttack);
                                 //var voltarValMana;
                                  //   voltarValMana = parseInt(mana.value) + parseInt(valManaAntigo);
                                     unityInstance.SendMessage('ControllerFase', 'alteracaoDisponibilidadeManaAdicao',valManaAntigo);
                                    // mana.value = voltarValMana;//VAI ENVIAR UMA REQUISICAO PRO UNITY
                         }else{
                             console.log("Não sou o bloco desejado");
                         }
                         
                     }
                 }
                //#endregion
          //#endregion 

    }
