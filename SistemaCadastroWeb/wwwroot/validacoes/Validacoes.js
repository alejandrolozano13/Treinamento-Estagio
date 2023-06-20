sap.ui.define([

], function () {
    'use strict';

    return {
        validarNome: function (nome, campoNome) {
            //debugger
            const regex = /^[a-záàâãéèêíïóôõöúçñA-ZÁÀÂÃÉÈÊÍÏÓÔÕÖÚÇÑ\s]+$/
            let erro = "Erro, nome não pode ser nulo";
            let erroo = "Erro, nome não pode conter número";
            if (nome == "") {
                return campoNome.setValueState(sap.ui.core.ValueState.Error), campoNome.setValueStateText(erro)
            }
            else {
                if (!regex.test(nome)) {
                    console.log(erroo)
                    return campoNome.setValueState(sap.ui.core.ValueState.Error), campoNome.setValueStateText(erroo)
                } else {
                    return campoNome.setValueState(sap.ui.core.ValueState.None);
                }
            }
        },
        validarCpf: function (cpf, campoCpf) {

            let cpfCorrigido = cpf.replace(/[^0-9]/g, '');
            let erro = "Erro, CPF incompleto";
            let erroo = "Erro, CPF não pode estar vazio";
            let cpfValido = this.validarCpfForm(cpf);

            if (cpfCorrigido.length == 0) {
                return campoCpf.setValueState(sap.ui.core.ValueState.Error), campoCpf.setValueStateText(erroo);
            }
            else {
                if (cpfCorrigido.length < 11 || !cpfValido) {
                    return campoCpf.setValueState(sap.ui.core.ValueState.Error), campoCpf.setValueStateText(erro);
                }
                else {
                    return campoCpf.setValueState(sap.ui.core.ValueState.None);
                }
            }
        },
        validarTelefone: function (telefone, campoTelefone) {
            let telefoneCorrigido = telefone.replace(/[^0-9]/g, '');
            let telefoneInvalido = "Erro, número inválido";
            let telefoneVazio = "Erro, o número não pode estar vazio";

            if (telefoneCorrigido.length == 0) {
                return campoTelefone.setValueState(sap.ui.core.ValueState.Error), campoTelefone.setValueStateText(telefoneVazio);
            } else {
                if (telefoneCorrigido.length < 11 || telefoneCorrigido[2] != 9) {
                    return campoTelefone.setValueState(sap.ui.core.ValueState.Error), campoTelefone.setValueStateText(telefoneInvalido);
                }
                else {
                    return campoTelefone.setValueState(sap.ui.core.ValueState.None);
                }
            }
        },
        validarCpfForm: function (cpf) {
            const maximoTamanCaracteresRepetidos = 11;
            let strCPF = cpf.replace(/[^0-9]/g, '');
            let erros = [];
            let expressaoRegular = new RegExp(`${strCPF[0]}`, 'g');
            let Soma;
            let Resto;
            let tamanhoCaracteresRepetidos = (strCPF.match(expressaoRegular)||[]).length;
            Soma = 0;
            
            
            if (tamanhoCaracteresRepetidos === maximoTamanCaracteresRepetidos)
                erros.push("Esse formato de cpf não é válido!");
            
            for (let i=1; i<=9; i++) Soma = Soma + parseInt(strCPF.substring(i-1, i)) * (11 - i);
            Resto = (Soma * 10) % 11;
    
            if ((Resto == 10) || (Resto == 11))  Resto = 0;
            if (Resto != parseInt(strCPF.substring(9, 10)) ) erros.push("Esse formato de cpf não é válido!");
    
            Soma = 0;
            for (let i = 1; i <= 10; i++) Soma = Soma + parseInt(strCPF.substring(i-1, i)) * (12 - i);
            Resto = (Soma * 10) % 11;
    
            if ((Resto == 10) || (Resto == 11))  Resto = 0;
            if (Resto != parseInt(strCPF.substring(10, 11) ) ) erros.push("Esse formato de cpf não é válido!");
            return erros;
        },
        validarEmail: function(email, campoEmail){
            debugger
            let validRegex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
            let emailInvalido = "E-mail inválido";
            
            if(email.match(validRegex)){
                return campoEmail.setValueState(sap.ui.core.ValueState.None);
            } else{
                return campoEmail.setValueState(sap.ui.core.ValueState.Error), campoEmail.setValueStateText(emailInvalido);;
            }
        },
        validarData: function(data, campoData){
            // verificar se tem como remover o campo input da data e se tem como escolher direto do datepicker
        }
    };
});