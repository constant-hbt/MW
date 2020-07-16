 //Funções da API    
 function initApi(interpreter , scope){
    //Adiciona uma função de API ao alert
    var wrapper = function(text) {
        return alert(arguments.length ? text : '');
        };
        interpreter.setProperty(scope, 'alert',
             interpreter.createNativeFunction(wrapper));

    //Adiciona uma função de API para o bloco de prompt
    var wrapper = function(text){
        return interpreter.createPrimitive(prompt(text));
        };
        interpreter.setProperty(scope, prompt , 
                 interpreter.createNativeFunction(wrapper));


        //Adicionando uma função de API para destacar blocos.
         var wrapper = function(id) {
                return interpreter.createPrimitive(highlightBlock(id));
                };
                interpreter.setProperty(scope, 'highlightBlock',
                   interpreter.createNativeFunction(wrapper));

        // MOVIMENTAÇÃO DO PERSONAGEM        
            
        //Adicionando uma função de API para movimentação para frente
        var wrapper = function(){
            return  unityInstance.SendMessage('playerKnight','Movimentacao' , 'avancar');//arrumar depois para movimentar o player para frente
        }
        interpreter.setProperty(scope, 'avancar',
            interpreter.createNativeFunction(wrapper));

        //Adicionando uma função de API para pular
        var wrapper = function(){
            return unityInstance.SendMessage('playerKnight','Movimentacao','puloSimples');
        }
        interpreter.setProperty(scope,'pular',
            interpreter.createNativeFunction(wrapper));
        
        //Adicionando uma função de API para movimentação de salto com impulso para frente
        var wrapper = function(){
            return unityInstance.SendMessage('playerKnight','Movimentacao', 'puloLateral');//arrumar depois
        }
        interpreter.setProperty(scope, 'pular_frente',
            interpreter.createNativeFunction(wrapper));
        
        //Adicionando uma função de API para movimentação de defesa
        var wrapper = function(){
            return unityInstance.SendMessage('playerKnight','Movimentacao', 'defender');
        }
        interpreter.setProperty(scope, 'defender',
            interpreter.createNativeFunction(wrapper));
        
        //Adicionando uma função de API para movimentação de ataque
        var wrapper = function(value_ataque){
            

            return unityInstance.SendMessage('playerKnight','Ataque',value_ataque);
        }
        interpreter.setProperty(scope, 'atacar',
            interpreter.createNativeFunction(wrapper));

    // BLOCOS DE CONDIÇÃO

    //Adicionando uma função de API para bool há inimigos
    var wrapper = function(){
        console.log("Antes de entrar na promise RetornoSendM = " + retornoSendHaInim);

                return retornoSendHaInim;
                 }
        interpreter.setProperty(scope, 'ha_inimigos',
            interpreter.createNativeFunction(wrapper));
    
    //Adicionando uma função de API para bool não há inimigos
    var wrapper = function(){
        console.log("Antes de entrar na promise RetornoSendM = " + retornoSendNaoHaInim);

            return retornoSendNaoHaInim;   
            
        }
        interpreter.setProperty(scope, 'nao_ha_inimigos',
            interpreter.createNativeFunction(wrapper)); 

            var wrapper = function(){
            return unityInstance.SendMessage('playerKnight','Flip');
        }
        interpreter.setProperty(scope, 'virar',
            interpreter.createNativeFunction(wrapper));
}
 //--------------