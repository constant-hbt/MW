
mergeInto(LibraryManager.library, {
    
    Win: function(){
        window.alert("Parabéns , você venceu!!");
        return true;
    },
    Teste: function(str){
        window.document.getElementById('textarea').value = Pointer_stringify(str);
    },
   

});