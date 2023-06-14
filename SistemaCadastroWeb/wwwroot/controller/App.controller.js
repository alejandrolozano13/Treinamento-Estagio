sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel",
    "sap/ui/model/Filter",
    "sap/ui/model/FilterOperator"
], function (Controller, JSONModel, FilterOperator, Filter) {
    "use strict";
    return Controller.extend("sap.ui.demo.cadastro.controller.App", {
        onInit: function () {
            this.aoCoincidirRota();
        },

        aoCoincidirRota : function(){
            let tela = this.getView();
            fetch("api/Cliente")
                .then(function (response) {
                    return response.json();
                })
                .then((dados) => {
                    dados.forEach(cliente=>{
                        let arquivo = this.dataURLtoFile(cliente.imagemUsuario, "imagem.jpeg");
                        cliente.imagemUsuarioTraduzido = this.dataCreateObject(arquivo)
                    })
                    
                    tela.setModel(new JSONModel(dados), "clientes")
                })
                .catch(function (error) {
                    console.error(error);
                });
        },

        aoPesquisarClientes : function(oEvent){
            let sQuery = oEvent.getParameter("query");
            let tela = this.getView();
            fetch(`api/Cliente?nome=${sQuery}`)
                .then(function (response) {
                    return response.json();
                })
                .then((dados) => {
                    dados.forEach(cliente=> {
                        let arquivo = this.dataURLtoFile(cliente.imagemUsuario, "imagem.jpeg");
                        cliente.imagemUsuarioTraduzido = this.dataCreateObject(arquivo);
                    })
                    tela.setModel(new JSONModel(dados), "clientes")
                })
                .catch(function (error) {
                    console.error(error);
                });
        },
        
        buscaDetalhes : function(oEvent){
            let cliente = oEvent
                .getSource()
                .getBindingContext("clientes")
                .getObject();
            let oRouter = this.getOwnerComponent().getRouter();
            oRouter.navTo("detail",{
                id:cliente.id
            });
        },

        aoAdicionarCliente : function(oEvent){
            let oRouter = this.getOwnerComponent().getRouter();
            oRouter.navTo("cadastrar");
        },

        dataURLtoFile(bse64, filename) {
            let bstr = atob(bse64)
            let n = bstr.length
            let u8arr = new Uint8Array(n)
            while (n--) {
                u8arr[n] = bstr.charCodeAt(n);
            }
            return new File([u8arr], filename, { type: "image/jpeg" });
        },

        dataCreateObject(file){
            return URL.createObjectURL(file);
        }
    });
});