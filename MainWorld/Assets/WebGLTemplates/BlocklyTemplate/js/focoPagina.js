//game -- quer dizer que o foco esta no game
//pagina -- o foco está na pagina

//var statusFoco = false;

/*
function mudarFocoWebGl(){
    alert("Foco no WebGL");
    if(statusFoco == 'pagina'){
        unityInstance.SendMessage('GameController','mudandoFocoParaWebGL');
    }
    statusFoco = 'game';
}



function mudarFocoPagina(){
    alert("Foco na pagina");
    if(statusFoco == 'game'){
        unityInstance.SendMessage('GameController','mudandoFocoParaPagina' );
    }
    statusFoco = 'pagina';
    

}*/
function focusCanvas(focus){
    
        unityInstance.SendMessage('GameControllerEntreFases','FocusCanvas',focus );
}
