
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
    ReiniciarVarBlocosTotais: function(){
        qtdBlocosTotais = 0;
    },
    CondicaoHaInimigo: function(temp_situacaoInimigo){
        retornoSendHaInim = temp_situacaoInimigo;
    }
    ,
    CondicaoNaoHaInimigo: function(temp_situacaoInimigo){
        retornoSendNaoHaInim = temp_situacaoInimigo;
    }
    ,
    Teste: function(condInim){
        console.log("Valor de retorno da funcao CondicaoInimigo: "+ condInim);
    },
    AlterarLimiteBlocoForcaAtaque:function(limitForcaAtaque){
        var limit = document.getElementById('limiteForcaAtaque');
        limit.value =limitForcaAtaque;
        console.log("Valor do limite do bloco força ataque = " + limiteForcaAtaque.value);
        console.log("Valor passado para variavel limit = " + limit.value);

        alterarLimiteBlocoForca(limitForcaAtaque);
    },
    
});