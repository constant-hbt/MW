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