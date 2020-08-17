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

//Capta o event beforeunload e envia uma req sendMessage ao unity para salvar o Historico do jogo no momento que o usuario tentar atualizar ou fechar a pagina

window.addEventListener('beforeunload', function (e) {
    // Cancel the event
    e.preventDefault(); // If you prevent default behavior in Mozilla Firefox prompt will always be shown
    // Chrome requires returnValue to be set
    e.returnValue = '';
    console.log("Chamando a funcao EnviarRegistroHistorico de dentro do unity");
    unityInstance.SendMessage("GameControllerEntreFases","EnviarRegistroHistorico");
    return ;
  });