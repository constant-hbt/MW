      // #region VARIÁVEIS ---------
       var myInterpreter = null;
       var runner;
       var code = '';
       var codeCompleto = '';//Contém o código contido em todas as etapas da fase
       var highlightPause = false;
       var qtdBlocosUsados = 0;//quantidade de blocos usados em cada etapa da fase
       var qtdBlocosMinimos = 0;//quantidade de blocos minimos necessarios para passar de fase
       var qtdBlocosTotais = 0;//quantidade de blocos usados em toda fase(pensando nas fases que ha mais de uma etapa)
       
       var retornoSendHaInim = false;//retorno da requisição sendMessage do ha_inimigo
       var retornoSendNaoHaInim = false;//retorno da requisição sendMessage do nao_ha_inimigo

       //#endregion

      


      //#region     FUNÇÕES BLOCKY 

        //função responsavel pelo Botão executar
        function stepCode(){

            codeCompleto += Blockly.JavaScript.workspaceToCode(workspace);
            unityInstance.SendMessage('ControllerFase','PegarBlocosUtilizados',mostrarCodigo(codeCompleto));//enviado a sequencia de blocos utilizados na parte da fase
            qtdBlocosUsados = workspace.getAllBlocks().length;
            qtdBlocosTotais += qtdBlocosUsados;
            enviarQtdBlocosUsados(qtdBlocosUsados);
            unityInstance.SendMessage('playerKnight', 'mudarValidar');
            unityInstance.SendMessage('playerKnight', 'chamarResetarStatusParaPainelFaseInc');
           // unityInstance.SendMessage('ControllerFase','EnviarHistorico');//envia a solicitacao para o envio do registro de historico no banco
            if(!myInterpreter){
                //primeira declaração deste código
                //limpe a saída do programa
                resetStepUi(true);
                execute.disable = 'disable';
                //E, em seguida, mostre o código gerado em um alerta
                //Em um tempo limite para permitir que o textarea.value seja redefinido primeiro
                setTimeout(function(){
            highlightPause = false;
            myInterpreter = new Interpreter(code , initApi);
            runner = function(){
                if(myInterpreter){
                    var masCodigo = myInterpreter.step();
                    if(masCodigo){
                        setTimeout(runner,80);
                    }else{
                        setTimeout(function(){
                            unityInstance.SendMessage('playerKnight', 'respostaInterprete');
                        },1000);
                    }
                }
            };
            runner();
        },1);
        return;
    }
}

              //Adiciona destaque ao bloco que esta sendo executado
              function highlightBlock(id) {
                    workspace.highlightBlock(id);
                    highlightPause = true;
                }

            //reseta o interprete de codigo
              function resetInterpreter(){
                     myInterpreter = null;
                    if(runner){
                        clearTimeout(runner);
                        runner = null;
                    }
                }

                function resetStepUi(clearOutput){
                    workspace.highlightBlock(null);
                    highlightPause = false;
                }

                function generateCodeAndLoadIntoInterpreter(){
                    //Código JavaScript gerado e analisado
                    Blockly.JavaScript.STATEMENT_PREFIX = 'highlightBlock(%1);\n'; 
                    Blockly.JavaScript.addReservedWords('highlightBlock');
                    code = Blockly.JavaScript.workspaceToCode(workspace);
                    resetStepUi(true);
                }
      //#endregion

      //#region FUNÇÕES AUTORAIS 
         

            //ENVIAR A QUANTIDADE DE BLOCOS QUE FORAM UTILIZADOS PARA PASSAR A FASE
            function enviarQtdBlocosUsados(qtdBloco){//-> função criada por mim
                unityInstance.SendMessage('ControllerFase','quantidadeBlocoUsadosNaFase',qtdBloco );
                unityInstance.SendMessage('playerKnight', 'receberBlocos', qtdBloco);
                
            }

            function mostrarCodigo(code){//mostra o codigo utilizado contido nos blocos escolhidos || Função criada por mim
                var arrayCode = code.split("\n");
                var novoCode = '';

                for(var i=0; i< arrayCode.length; i++){
                    if(arrayCode[i].indexOf("highlightBlock") == -1){
                        novoCode += arrayCode[i];
                    }
                }

                return novoCode;
            }

            function msgDesempenhoBloco(qtdBlocosMinimos , qtdBlocosUsados){// -> função criada por mim
                if(qtdBlocosUsados <= qtdBlocosMinimos){
                    return "Parabéns pelo seu desempenho!!";
                }else if(qtdBlocosUsados > qtdBlocosMinimos){
                    return "Parabéns, você concluiu a fase usando "+qtdBlocosUsados+" blocos, mais a fase poderia ter sido concluída usando "+qtdBlocosMinimos+" !!";
                }
            }

            function chamandoAlertDesempenho(){//Função criada por mim
                setTimeout(function(){
                  /*  console.log( msgDesempenhoBloco(qtdBlocosMinimos, qtdBlocosTotais)+"\n" + 
                                        "Código utilizado para concluir a fase: \n"+
                                        mostrarCodigo(codeCompleto) );*/
                codeCompleto = '';
                qtdBlocosTotais = 0;
                }, 1000);
            }

             //funcao responsavel por limpar a área de trabalho blockly
             function resetarEspacoBlockly(){// --> função criada por mim
                resetInterpreter();
                resetStepUi(false);
                setTimeout(function(){
                    workspace.clear();
                    
                }, 1000); 
}
   //-----
   function alterarLimiteBlocoForca(val_forca){
    var limitForcaAtaque = document.getElementById('limiteForcaAtaque');
    var blocos_temp = Blockly.mainWorkspace.getAllBlocks();
        for(var i=0; i< blocos_temp.length; i++ ){
            if(blocos_temp[i].type == "valor_ataque"){
 blocos_temp[i].inputList[0].fieldRow[1].max_ = blocos_temp[i].inputList[0].fieldRow[1].value_ + parseInt(limitForcaAtaque.value);
            }
        }
}
                
      //#endregion