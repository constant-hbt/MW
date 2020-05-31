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