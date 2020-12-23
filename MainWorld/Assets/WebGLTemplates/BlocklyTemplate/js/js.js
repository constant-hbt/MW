//Responsável por excluir os blocos caso os blocos escolhidos para passar a fase não sejam suficientes para a conclusão da fase
function passeiFase(conclusaoFase){
    passeiFase = conclusaoFase;
}

function focusCanvas(focus){
    
    unityInstance.SendMessage('GameControllerEntreFases','FocusCanvas',focus );
}

function HabilitarEDesabilitarBlocos(situacao){
    var arrayBlocos = document.getElementsByClassName("bloco");
    var capacidadeRestanteBlocos = document.getElementById("capacRestanteBloco");


    if(situacao == true){
        for(var i=0; i< arrayBlocos.length; i++){       
            arrayBlocos[i].setAttribute("disabled","true");
            }        
    }else{
        for(var i=0; i< arrayBlocos.length; i++){       
            arrayBlocos[i].setAttribute("disabled","false");
            }  
    }
    

    capacidadeRestanteBlocos.hidden = situacao;
}

  //Funcoes resposanveis por colocar e retirar a trasparencia da div do toolbox
  //E responsavel também por centralizar de descentralizar a div do webGl

  function centralizarWebGl(){
    document.getElementById('divToolboxButton').classList.remove('semTransparencia');
    document.getElementById('divToolboxButton').classList.add('transparencia');
    
    
    document.getElementById('divWebGl').classList.remove('divRightAlterada');
    document.getElementById('divWebGl').classList.add('divRightInicial');
   // document.getElementById('conteudoUnity').style.float = "none";
    //document.getElementById('conteudoUnity').style.marginLeft = "0px";
}

 function disponibilizarDivToolbox(){
   // document.getElementById('blocklyDiv').classList.toggle('DivEsquerda');
    document.getElementById('divToolboxButton').classList.remove('transparencia');
    document.getElementById('divToolboxButton').classList.add('semTransparencia');
   
    document.getElementById('divWebGl').classList.remove('divRightInicial');
    document.getElementById('divWebGl').classList.add('divRightAlterada');
}

//verifica se o navegador do player já possui dados gravados
function verificarDadosPlayer(){
    if(localStorage.getItem('playerMW') != null){
        unityInstance.SendMessage('ControllerTelaInicial', 'VerificarPlayerL', 'haRegistro');
    }else{
        unityInstance.SendMessage('ControllerTelaInicial', 'VerificarPlayerL', 'naoHaRegistro');
    }
}

function gravarDesempenhoPlayer(p_id_usuario, p_fase_concluida,p_moedas,p_vidas,p_estrelas,p_ultima_fase_concluida){
    
    let playerMW = {id_usuario:p_id_usuario, fase_concluida:p_fase_concluida,moedas:p_moedas,vidas:p_vidas,estrelas:p_estrelas,ultima_fase_concluida:p_ultima_fase_concluida};
    localStorage.setItem('playerMW',JSON.stringify(playerMW));
}

function retornarDadosPlayer(){
    if(verificarDadosPlayer()){
        let playerMW = localStorage.getItem('playerMW');
        unityInstance.SendMessage('ControllerTelaInicial','PreencherDadosPlayer', playerMW);
    }    
}