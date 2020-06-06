
mergeInto(LibraryManager.library, {
    
    SistemaLimiteBloco: function(qtdBlocoFase){
        recriarWorkspace(qtdBlocoFase);
    },
    SistemaDeEnableDisableBlocos: function(situacao){
        HabilitarEDesabilitarBlocos(situacao);
    },
    SistemaReiniciarWorkspaceBlockly: function(){
        resetarEspacoBlockly();
    },
    SistemaVerifConclusaoFase: function(situacaoFase){
        passeiFase = situacaoFase;
    },
    EnviarQTDBlocosMinimosParaPassarFase:function(qtdBlocos){
        qtdBlocosMinimos = qtdBlocos;
    },
    ChamandoAlertFinalFase: function(){
            chamandoAlertDesempenho();
    },
    ReiniciarVarCodeCompleto: function(){
        codeCompleto = '';
    },
    Teste:function(qtdBlocosUsados, veloX , veloY , grounded){
        console.log("Entrei no if dentro do player para mostrar que nao passei de fase");
        console.log("Quantidade de blocos usados = "+qtdBlocosUsados );
        console.log("Velocidade x = "+ veloX+" Velocidade y ="+veloY);
        console.log("Estou no chao ="+ grounded);
    },
});