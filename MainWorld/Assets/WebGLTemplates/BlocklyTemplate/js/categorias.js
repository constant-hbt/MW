function mudarToolbox(idToolbox){
    
    var body = document.getElementById('body');
    
    //criando tag div
    var divBlockly = document.getElementById('blocklyDiv');
    /*var div = document.createElement('div');
    div.id = "divBlockly";
    div.style = " height : 480px ; width : 350px ;"; APAGAR DEPOIS*/
   
    let toolboxAtivas = document.getElementsByName("toolbox");
    let toolboxJaExiste = false;
    for(let i=0; i<toolboxAtivas.length; i++){
        if(toolboxAtivas[i].id != 'toolbox'+idToolbox){
            divBlockly.removeChild(toolboxAtivas[i]);
        }else if(toolboxAtivas[i].id == 'toolbox'+idToolbox && !toolboxJaExiste){
            toolboxJaExiste = true;
        }/*else if(toolboxAtivas[i].id == 'toolbox'+idToolbox && toolboxJaExiste){
            divBlockly.removeChild(toolboxAtivas[i]);
        }*/
    }


    if(!toolboxJaExiste){
     //criando tag xml
     var xml = document.createElement('xml');
     xml.id = "toolbox"+idToolbox;
     xml.style="display: none";
     xml.setAttribute("name","toolbox" );
     //criando tag category
    var category= [];

    //verifica para o toolbox de qual fase deseja ser alterado
    switch(idToolbox){
         
        case 1:
            category = toolboxFase1(idToolbox);
            console.log("Alterei a toolbox para 1");
            break;
        case 2:
            category = toolboxFase2e3(idToolbox);
            console.log("Alterei a toolbox para 2");
            break;
        case 3:
            category = toolboxFase2e3(idToolbox);
            console.log("Alterei a toolbox para 3");
            break;
        case 4:
            category = toolboxFase4(idToolbox);
            console.log("Alterei a toolbox para 4");
            break;
        case 5:
            category = toolboxFase5(idToolbox);
            console.log("Alterei a toolbox para 5");
            break;
        case 6:
            category = toolboxFase6e7(idToolbox);
            console.log("Alterei a toolbox para 6");
            break;
        case 7:
            category = toolboxFase8e9(idToolbox);
            console.log("Alterei a toolbox para 7");
            break;
        case 8:
            category = toolboxFase8e9(idToolbox);
            console.log("Alterei a toolbox para 8");
            break;
        case 9:
            category = toolboxFase8e9(idToolbox);
            console.log("Alterei a toolbox para 9");
            break;
    }

    console.log("Tamanho do array category = "+category.length);

             if(category.length == 1){
                var categoryTemp = category[0];
                xml.appendChild(categoryTemp);
            }else if(category.length > 1){
                for(var i=0; i< category.length;i++){
                   var categoryTemp = category[i];
                    xml.appendChild(categoryTemp);
                }
            }

    
     divBlockly.appendChild(xml);
     //body.appendChild(div);   APAGAR DEPOIS
    }else{
        console.log("Ja existe esse toolbox");
    }
     
    
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
    blocoAvancar.setAttribute('class','bloco');

    //bloco virar
    var blocoVirar = document.createElement('block');
    blocoVirar.setAttribute('type', 'virar');
    blocoVirar.setAttribute('disabled','true');
    blocoVirar.setAttribute('class','bloco');

    //criando a estrutura html da div
    category.appendChild(blocoAvancar);
    category.appendChild(blocoVirar);
    
    arrayCategory.push(category);
    console.log(arrayCategory);
    return arrayCategory;
}

function toolboxFase2e3(idToolbox){
    console.log("Estou dentro da func toolbox2e3");
    var arrayCategory = toolboxFase1(idToolbox) ;
    console.log("Passei o arrayCategory da funcao toolbox2e3");
    //criando tag dos blocos
    //bloco avancar
    var blocoPular = document.createElement('block');
    blocoPular.setAttribute('type', "pular");
    blocoPular.setAttribute('disabled','true');
    blocoPular.setAttribute('class','bloco');
    //bloco virar
    var blocoPularFrente = document.createElement('block');
    blocoPularFrente.setAttribute('type', 'pular_frente');
    blocoPularFrente.setAttribute('disabled','true');
    blocoPularFrente.setAttribute('class','bloco');

    //criando a estrutura html da div
    arrayCategory[0].appendChild(blocoPular);
    arrayCategory[0].appendChild(blocoPularFrente);
    console.log("Array category na fase2 = "+arrayCategory[0]);
    return arrayCategory;
}

function toolboxFase4(idToolbox){
    var arrayCategory = toolboxFase2e3(idToolbox);

    //criando tag dos blocos
    //bloco Defender
    var blocoDefender = document.createElement('block');
    blocoDefender.setAttribute('type',"defender");
    blocoDefender.setAttribute('disabled','true');
    blocoDefender.setAttribute('class','bloco');

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
    blocoAtacar.setAttribute('class','bloco');

    var blocoAdaptadorAtaq = document.createElement('block');
    blocoAdaptadorAtaq.setAttribute('type', 'adaptadorAtack');
    blocoAdaptadorAtaq.setAttribute('disabled','true');
    blocoAdaptadorAtaq.setAttribute('class','bloco');

    var blocoValorAtaq = document.createElement('block');
    blocoValorAtaq.setAttribute('type', 'valor_ataque');
    blocoValorAtaq.setAttribute('disabled','true');
    blocoValorAtaq.setAttribute('class','bloco');

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