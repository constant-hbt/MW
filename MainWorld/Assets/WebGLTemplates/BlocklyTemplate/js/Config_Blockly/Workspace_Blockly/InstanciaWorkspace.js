
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

            //TESTE MANA -- CASO DER ERRADO APAGAR APARTIR DAQUI
          //#region FUNÇÕES RESPONSAVEIS POR CAPTAR A ENTRADA, ATUALIZAÇÃO E REMOCÃO DO BLOCO forcaAttack
                    //#region ENTRADA
                    workspace.addChangeListener(captarInsercao);
                    function captarInsercao(event){
                        
                        console.log("Esse é o evento" +event);
                                if(event.type == Blockly.Events.BLOCK_CREATE
                                 && event.xml.attributes[0].textContent == "valor_ataque"){

                                    console.log(event);
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
                            var valManaAntesAtt;
                            
                            clearTimeout(usuParouDigit);
                            usuParouDigit = setTimeout(function(){
                                console.log(event);
                                for(var i=0; i<idBlocoAttack.length;i++){
                                        if(idBlocoAttack[i].id == event.blockId){
                                            console.log( "Valor antes de alterar no array ids = "+ idBlocoAttack[i].valor);
                                            valManaAntesAtt = idBlocoAttack[i].valor;
                                            idBlocoAttack[i].valor = event.newValue;
                                            console.log("Valor depois de alterar o array ids = "+ idBlocoAttack[i].valor);
                                            valManaatt = idBlocoAttack[i].valor;
                                        }   
                                }
                                var valorAntigoNovoMana = valManaAntesAtt.toString()+","+valManaatt.toString(); //pos 0 = valor antigo mana , pos 1 = valor da mana apos att
                               // valorAntigoNovoMana.push(valManaAntesAtt);
                                //valorAntigoNovoMana.push(valManaatt);
                                console.log("Array com valor antigos e novos mana = "+valorAntigoNovoMana);
                                unityInstance.SendMessage('ControllerFase','alteracaoDisponibilidadeManaRemocao', valorAntigoNovoMana);
                                //mana.value -= valManaatt; //VAI ENVIAR UMA REQUISICAO AO UNITY PARA MUDAR O VALOR DA MANA
                            }, 1000);
                            console.log("Se der erro depois disso aqui é por causa do teste");
                            //let tmp = Blockly.mainWorkspace.blockDB_(event.blockId);
                            //tmp.update.call(tmp);
                            console.log(Blockly.mainWorkspace.getAllBlocks().length);
                            console.log(Blockly.mainWorkspace.getAllBlocks()[0].inputList[0].fieldRow[1].max_);
                            /*
                            var limitForcaAtaque = document.getElementById('limiteForcaAtaque');
                            var blocos_temp = Blockly.mainWorkspace.getAllBlocks();
                            console.log(blocos_temp);
                                for(var i=0; i< blocos_temp.length; i++ ){
                                     if(blocos_temp[i].type == "valor_ataque"){
                                         console.log("Entrei dentro do if");
                                        blocos_temp[i].inputList[0].fieldRow[1].max_ = blocos_temp[i].inputList[0].fieldRow[1].value_ + parseInt(limitForcaAtaque.value);
                                  }
                                }
                                    */
                            
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
