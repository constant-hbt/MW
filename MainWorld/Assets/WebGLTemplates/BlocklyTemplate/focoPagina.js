//game -- quer dizer que o foco esta no game
//pagina -- o foco está na pagina
var statusFoco = 'game';

function mudarFocoWebGl(){
    if(statusFoco == 'pagina'){
        unityInstance.SendMessage('GameController','mudandoFocoParaWebGL');
    }
    statusFoco = 'game';
}



function mudarFocoPagina(){
    if(statusFoco == 'game'){
        unityInstance.SendMessage('GameController','mudandoFocoParaPagina' );
    }
    statusFoco = 'pagina';
    

}
