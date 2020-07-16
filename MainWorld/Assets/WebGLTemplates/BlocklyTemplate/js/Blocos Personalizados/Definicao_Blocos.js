Blockly.Blocks['avancar'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("Avançar");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(345);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['pular'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("Pular");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(345);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['defender'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("Defender");
        this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(345);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['atacar'] = {
  init: function() {
    this.appendValueInput("Ataque")
        .setCheck(["variable_get","forca_atack"])
        .appendField("Ataque");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(230);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['pular_frente'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("Pular para frente");
    this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(345);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['ha_inimigos'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("Há inimigos");
    this.setOutput(true, "Boolean");
    this.setColour(300);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['nao_ha_inimigos'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("Não há inimigos");
    this.setOutput(true, "Boolean");
    this.setColour(300);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['virar'] = {
  init: function() {
    this.appendDummyInput()
        .appendField("Virar");
        this.setPreviousStatement(true, null);
    this.setNextStatement(true, null);
    this.setColour(345);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['valor_ataque'] = {
  
  init: function() {
    var valid = document.getElementById('valorInicialCampoForca');
    var limit = document.getElementById('limiteForcaAtaque');

    function verifValidar(valid,limit){
      return new Blockly.FieldNumber(valid, 0, limit, 1);
    }
    

    this.appendDummyInput()
      .appendField("Força")
        .appendField(verifValidar(valid.value, limit.value), "valor_ataque");
    this.setOutput(true, "valor_forca");
    this.setColour(230);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};

Blockly.Blocks['forca_atack'] = {
  init: function() {
    this.appendValueInput("forca_atack")
        .setCheck(["valor_forca", "variable_get"])
        .appendField("Força");
    this.setOutput(true, "forca_atack");
    this.setColour(230);
 this.setTooltip("");
 this.setHelpUrl("");
  }
};