Blockly.JavaScript['avancar'] = function(block) {
  // TODO: Assemble JavaScript into code variable.
  var code = '...;\n';
  return "avancar();\n";
};

Blockly.JavaScript['pular'] = function(block) {
  // TODO: Assemble JavaScript into code variable.
  var code = '...;\n';
  return "pular();\n";
};

Blockly.JavaScript['defender'] = function(block) {
  // TODO: Assemble JavaScript into code variable.
  var code = '...;\n';
  return "defender();\n";
};

Blockly.JavaScript['atacar'] = function(block) {
  var value_ataque = Blockly.JavaScript.valueToCode(block, 'Ataque', Blockly.JavaScript.ORDER_ATOMIC);
  // TODO: Assemble JavaScript into code variable.
  var code = '...;\n';
  return "atacar("+value_ataque+");\n";
};

Blockly.JavaScript['pular_frente'] = function(block) {
  // TODO: Assemble JavaScript into code variable.
  var code = '...;\n';
  return "pular_frente();\n";
};

Blockly.JavaScript['ha_inimigos'] = function(block) {
  // TODO: Assemble JavaScript into code variable.
  var code = '...';
  // TODO: Change ORDER_NONE to the correct strength.
  return ["ha_inimigos()", Blockly.JavaScript.ORDER_NONE];
};

Blockly.JavaScript['nao_ha_inimigos'] = function(block) {
  // TODO: Assemble JavaScript into code variable.
  var code = '...';
  // TODO: Change ORDER_NONE to the correct strength.
  return ["nao_ha_inimigos()", Blockly.JavaScript.ORDER_NONE];
};

Blockly.JavaScript['virar'] = function(block) {
  // TODO: Assemble JavaScript into code variable.
  var code = '...;\n';
  return "virar();\n";
};

Blockly.JavaScript['valor_ataque'] = function(block) {
 
  var number_number = block.getFieldValue('valor_ataque');
 // TODO: Assemble JavaScript into code variable.
 var code = '...';
 // TODO: Change ORDER_NONE to the correct strength.
 return [parseInt(number_number), Blockly.JavaScript.ORDER_NONE];
};


Blockly.JavaScript['adaptadorAtack'] = function(block) {
  var value_forca_atack = Blockly.JavaScript.valueToCode(block, 'adaptadorAtack', Blockly.JavaScript.ORDER_ATOMIC);
  // TODO: Assemble JavaScript into code variable.
  var code = '...';
  // TODO: Change ORDER_NONE to the correct strength.
  return [value_forca_atack, Blockly.JavaScript.ORDER_NONE];
};