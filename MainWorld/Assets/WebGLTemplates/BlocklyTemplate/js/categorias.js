function mudarToolbox(idToolbox){
    
    var body = document.getElementById('body');
    
    //criando tag div
    var div = document.createElement('div');
    div.id = "divBlockly";
    div.style = " height : 480px ; width : 350px ; display: none";

     //criando tag xml
     var xml = document.createElement('xml');
     xml.id = "toolbox"+idToolbox;
     xml.style="display: none";

     //criando tag category
    var category= [];

    //verifica para o toolbox de qual fase deseja ser alterado
    switch(idToolbox){
         
        case '1':
            category = toolboxFase1(idToolbox);
            break;
        case '2':
            category = toolboxFase2e3(idToolbox);
            break;
        case '3':
            category = toolboxFase2e3(idToolbox);
            break;
        case '4':
            category = toolboxFase4(idToolbox);
            break;
        case '5':
            category = toolboxFase5(idToolbox);
            break;
        case '6':
            category = toolboxFase6e7(idToolbox);
            break;
        case '7':
            category = toolboxFase6e7(idToolbox);
            break;
        case '8':
            category = toolboxFase8e9(idToolbox);
            break;
        case '9':
            category = toolboxFase8e9(idToolbox);
            break;
    }

             if(category.length == 1){
                var categoryTemp = category[0];
                xml.appendChild(categoryTemp);
            }else if(category.length > 1){
                for(var i=0; i< category.length;i++){
                    var categoryTemp = category[i];
                    xml.appendChild(categoryTemp);
                }
            }

    // xml.appendChild(category);
     div.appendChild(xml);
     body.appendChild(div);
 
     console.log(div);
     workspace.updateToolbox(document.getElementById('toolbox'+idToolbox));
   
}

function toolboxFase1(idToolbox){
   
    var arrayCategory = [];
    var category= document.createElement('category');
    category.setAttribute("class","fase"+idToolbox);
    category.setAttribute("name","Personagem");
   
   
    //criando tag dos blocos
    //bloco avancar
    var blocoAvancar = document.createElement('block');
    blocoAvancar.setAttribute('type', "avancar");
    blocoAvancar.setAttribute('disabled','true');

    //bloco virar
    var blocoVirar = document.createElement('block');
    blocoVirar.setAttribute('type', 'virar');
    blocoVirar.setAttribute('disabled','true');

    //criando a estrutura html da div
    category.appendChild(blocoAvancar);
    category.appendChild(blocoVirar);
    
    arrayCategory.push(category);
    return arrayCategory;
}

function toolboxFase2e3(idToolbox){
    
    var arrayCategory = toolboxFase1(idToolbox) ;

    //criando tag dos blocos
    //bloco avancar
    var blocoPular = document.createElement('block');
    blocoPular.setAttribute('type', "pular");
    blocoPular.setAttribute('disabled','true');

    //bloco virar
    var blocoPularFrente = document.createElement('block');
    blocoPularFrente.setAttribute('type', 'pular_frente');
    blocoPularFrente.setAttribute('disabled','true');

    //criando a estrutura html da div
    arrayCategory[0].appendChild(blocoPular);
    arrayCategory[0].appendChild(blocoPularFrente);

    return arrayCategory;
}

function toolboxFase4(idToolbox){
    var arrayCategory = toolboxFase2e3(idToolbox);

    //criando tag dos blocos
    //bloco Defender
    var blocoDefender = document.createElement('block');
    blocoDefender.setAttribute('type',"defender");
    blocoDefender.setAttribute('disabled','true');

    arrayCategory[0].appendChild(blocoDefender);
    console.log(arrayCategory[0]);

    return arrayCategory;
}

function toolboxFase5(idToolbox){
    var arrayCategory = toolboxFase4(idToolbox);

    //criando tag dos blocos
    //bloco atacar
    var blocoAtacar = document.createElement('block');
    blocoAtacar.setAttribute('type','atacar');
    blocoAtacar.setAttribute('disabled','true');

    var blocoAdaptadorAtaq = document.createElement('block');
    blocoAdaptadorAtaq.setAttribute('type', 'adaptadorAtack');
    blocoAdaptadorAtaq.setAttribute('disabled','true');

    var blocoValorAtaq = document.createElement('block');
    blocoValorAtaq.setAttribute('type', 'valor_ataque');
    blocoValorAtaq.setAttribute('disabled','true');

    arrayCategory[0].appendChild(blocoAtacar);
    arrayCategory[0].appendChild(blocoAdaptadorAtaq);
    arrayCategory[0].appendChild(blocoValorAtaq);

    var categoryVariaveis = document.createElement('category');
    categoryVariaveis.setAttribute('id','catVariables');
    categoryVariaveis.setAttribute('class', 'bloco');
    categoryVariaveis.setAttribute('color', '330');
    categoryVariaveis.setAttribute('custom', 'VARIABLE');
    categoryVariaveis.setAttribute('name','Variaveis');
    categoryVariaveis.setAttribute('disabled','true');

    arrayCategory.push(categoryVariaveis);

    return arrayCategory;
}

function toolboxFase6e7(idToolbox){
        var arrayCategory = toolboxFase5(idToolbox);

        var categoryEstruturas = document.createElement('category');
        categoryEstruturas.setAttribute('name', 'Estruturas');

        var blocoRepeticao = document.createElement('block');
        blocoRepeticao.setAttribute('type', 'controls_repeat_ext');
        blocoRepeticao.setAttribute('class', 'bloco');
        blocoRepeticao.setAttribute('disabled','true');
        
        var blocoValue = document.createElement('value');
        blocoValue.setAttribute('name', 'TIMES');

        var blocoMathNumber = document.createElement('shadow');
        blocoMathNumber.setAttribute('type','math_number');

        var blocoField = document.createElement('field');
        blocoField.setAttribute('name', 'NUM');
        blocoField.innerHTML="0";

        blocoMathNumber.appendChild(blocoField);
        blocoValue.appendChild(blocoMathNumber);
        blocoRepeticao.appendChild(blocoValue);
        categoryEstruturas.appendChild(blocoRepeticao);

        arrayCategory.push(categoryEstruturas);

        return arrayCategory;
}

function toolboxFase8e9(idToolbox){
    var arrayCategory = toolboxFase6e7(idToolbox);

    var blocoIfElse = document.createElement('block');
    blocoIfElse.setAttribute('type', 'controls_if');
    blocoIfElse.setAttribute('class', 'bloco');
    blocoIfElse.setAttribute('disabled','true');
    
    arrayCategory[2].appendChild(blocoIfElse);

    var categoryCondições = document.createElement('category');
    categoryCondições.setAttribute('name','Condições');

    var blocoHaInimigo = document.createElement('block');
    blocoHaInimigo.setAttribute('type','ha_inimigos');
    blocoHaInimigo.setAttribute('class','bloco');
    blocoHaInimigo.setAttribute('disabled','true');

    var blocoNaoHaInimigo = document.createElement('block');
    blocoNaoHaInimigo.setAttribute('type','nao_ha_inimigos');
    blocoNaoHaInimigo.setAttribute('class','bloco');
    blocoNaoHaInimigo.setAttribute('disabled','true');

    categoryCondições.appendChild(blocoHaInimigo);
    categoryCondições.appendChild(blocoNaoHaInimigo);
    arrayCategory.push(categoryCondições);

    return arrayCategory;
}