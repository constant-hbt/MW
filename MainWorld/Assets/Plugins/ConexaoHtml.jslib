
mergeInto(LibraryManager.library, {
    
    SistemaLimiteBloco: function(qtdBlocoFase, toolbox){
        recriarWorkspace(qtdBlocoFase, toolbox);
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
    ReiniciarVarBlocosTotais: function(){
        qtdBlocosTotais = 0;
    },
    ResetarInterprete: function(){
        resetInterpreter();
    }
    ,
    CondicaoHaInimigo: function(temp_situacaoInimigo){
        retornoSendHaInim = temp_situacaoInimigo;
    }
    ,
    CondicaoNaoHaInimigo: function(temp_situacaoInimigo){
        retornoSendNaoHaInim = temp_situacaoInimigo;
    }
    ,
    AlterarToolboxFases:function(idToolbox){
        mudarToolbox(idToolbox);
    },
    DisponibilizarToobox:function(){
        disponibilizarDivToolbox();
    },
    CentralizarWebGl:function(){
        centralizarWebGl();
    },
    PegarIdPlayer:function(){
        buscarUsuarioLogado();
    }


});