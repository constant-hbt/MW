
mergeInto(LibraryManager.library, {
    
    SistemaLimiteBloco: function(qtdBlocoFase, toolbox){
        recriarWorkspace(qtdBlocoFase, toolbox);
        console.log("VALOR DE TOOLBOX DENTRO DA FUNCAO SISTEMALIMITEBLOCO = "+ toolbox);
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
    AlterarToolboxFases:function(idToolbox){
        mudarToolbox(idToolbox);
        console.log('Mudei o toolbox da fase'+idToolbox);
    },
    DisponibilizarToobox:function(){
        disponibilizarDivToolbox();
    },
    CentralizarWebGl:function(){
        centralizarWebGl();
    }
});