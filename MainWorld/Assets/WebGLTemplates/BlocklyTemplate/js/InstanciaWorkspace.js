//#region VARIÁVEIS
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


var botaoExecutar = document.getElementById('execute');


var idBlocoAttack = []; //responsavel por guardar todos os ids blocos de força de ataque que forem criados no workspace
var usuParouDigit ;
//#endregion

//#region CRIAÇÃO DO ESPAÇO DE TRABALHO BLOCKLY

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

 //#endregion

//#region FUNÇÕES
            
        //Carregue o intérprete agora e após alterações futuras
            generateCodeAndLoadIntoInterpreter();
            workspace.addChangeListener(function(event){
                if(!(event instanceof Blockly.Events.Ui)){
                    resetInterpreter();
                    generateCodeAndLoadIntoInterpreter();
                    //habilita o botão de execução somente se houver blocos no espaço de trabalho
                    if(code != ''){
                    botaoExecute.disabled = false;
                    
                }else{
                    botaoExecute.disabled = true;
                }

                }
            });

             //Atualiza a quantidade de blocos
           workspace.addChangeListener(onchange); 
           function onchange(event){
                       document.getElementById('capacidade').textContent = 
                           workspace.remainingCapacity();
                   }

        //funcao responsavel por recriar o workspace toda vez que for iniciar uma nova fase,  alterando o limite de blocos
        function recriarWorkspace(qtdMaxBlocos, toolbox){
            code = '';
            qtdBlocosUsados = 0;
            workspace.dispose();
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
             workspace.updateToolbox(document.getElementById('toolbox'+toolbox));
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


    

      //função responsavel pelo Botão executar
      function stepCode(){
        var btnExecutar = document.getElementById('execute');
        btnExecutar.disabled = true;
        codeCompleto += Blockly.JavaScript.workspaceToCode(workspace);
        unityInstance.SendMessage('ControllerFase','PegarBlocosUtilizados',mostrarCodigo(codeCompleto));//enviado a sequencia de blocos utilizados na parte da fase
        qtdBlocosUsados = workspace.getAllBlocks().length;
        qtdBlocosTotais += qtdBlocosUsados;
        enviarQtdBlocosUsados(qtdBlocosUsados);
        unityInstance.SendMessage('playerKnight', 'mudarValidar');
        unityInstance.SendMessage('playerKnight', 'habilitarPerdaTentativa','false');
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

        //ENVIAR A QUANTIDADE DE BLOCOS QUE FORAM UTILIZADOS PARA PASSAR A FASE
        function enviarQtdBlocosUsados(qtdBloco){
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

        function chamandoAlertDesempenho(){
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