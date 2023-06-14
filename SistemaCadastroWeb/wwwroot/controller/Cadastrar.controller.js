sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel",
    "sap/ui/core/routing/History",
    "sap/m/MessageBox"
], function (Controller, JSONModel, History, MessageBox) {
    'use strict';

    return Controller.extend("sap.ui.demo.cadastro.controller.Cadastrar", {
        onInit: function () {
            var oRouter = this.getOwnerComponent().getRouter();
            oRouter.getRoute("cadastrar").attachPatternMatched(this._aoCoincidirRota, this);
            
            
        },
        
        _aoCoincidirRota: function () {
            let cliente = {
                nome: "",
                email: "",
                cpf: "",
                data: "",
                telefone: ""
            }
            this.getView().setModel(new JSONModel(cliente), "cliente");
        },

        aoCancelarCadastro: function () {
            this.voltarAoMenu()
            this.aoLimparCampos()
        },

        voltarAoMenu: function () {
            let oHistory = History.getInstance();
            let sPreviousHash = oHistory.getPreviousHash();

            if (sPreviousHash !== undefined) {
                window.history.go(-1);
            } else {
                var oRouter = this.getOwnerComponent().getRouter();
                oRouter.navTo("listaClientes");
            }
            this.aoLimparCampos()
        },

        

        aoSalvarCliente: function () {
            let clientes = this.getView().getModel("cliente").getData();
            fetch("https://localhost:7035/api/Cliente", {
                method: 'POST',
                headers: {

                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(clientes)
            })
            .then(resp => resp.json())
            .then(response=>{
                MessageBox.information(
                    "Cliente criado com sucesso", {
                        actions: [MessageBox.Action.Ok], onClose:(acao)=>{
                        if (acao == MessageBox.Action.Ok){
                            this.aoNavegar(response);
                        }
                    }
                }
                )
            })
            .catch(function(error){
                console.error(error);
            });
        },

        aoNavegar : function(id){
            let oRouter = this.getOwnerComponent().getRouter();
            oRouter.navTo("detail",{
                id: id
            });
        },

        aoLimparCampos() {
            let nome = this.byId("campoNome");
            let cpf = this.byId("campoCpf");
            let email = this.byId("campoEmail");
            let telefone = this.byId("campoTelefone");
            let data = this.byId("campoData");

            nome.setValue("");
            cpf.setValue("");
            email.setValue("");
            telefone.setValue("");
            data.setValue("");

        },
    })
});