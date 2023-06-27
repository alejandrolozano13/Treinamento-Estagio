sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel",
    "sap/ui/model/Filter",
    "sap/ui/model/FilterOperator",
    "sap/ui/core/BusyIndicator"
], function (Controller, JSONModel, FilterOperator, Filter, BusyIndicator) {
    "use strict";
    return Controller.extend("sap.ui.demo.cadastro.controller.Lista", {
        onInit: function () {
            
            var oRouter = this.getOwnerComponent().getRouter();
            oRouter.getRoute("listaDeclientes").attachPatternMatched(this._aoCoincidirRota, this);
            
        },
        modeloClientes: function(JSONModel){
            const nomeModelo = "clientes";
            
            if (!JSONModel){
                return this.getView().getModel(nomeModelo);
            } else{
                
                return this.getView().setModel(JSONModel, nomeModelo);   
            }
        },

        _aoCoincidirRota : function(){
            BusyIndicator.show();
            fetch("api/Cliente")
                .then(function (response) {
                    BusyIndicator.hide();
                    return response.json();
                })
                .then((dados) => {
                    dados.forEach(cliente=>{
                        if(cliente.imagemUsuario){
                            let arquivo = this.dadosParaArquivo(cliente.imagemUsuario, "imagem.jpeg")
                            cliente.imagemUsuarioTraduzido = this.criandoArquivo(arquivo)
                        }
                    })
                    this.modeloClientes(new JSONModel(dados));
                })
                .catch(function (error) {
                    console.error(error);
                });
        },

        aoPesquisarClientes : function(oEvent){
            BusyIndicator.show();
            let sQuery = oEvent.getParameter("newValue");
            fetch(`api/Cliente?nome=${sQuery}`)
                .then(function (response) {
                    BusyIndicator.hide()
                    return response.json();
                })
                .then((dados) => {
                    dados.forEach(cliente=> {
                        let arquivo = this.dadosParaArquivo(cliente.imagemUsuario, "imagem.jpeg");
                        cliente.imagemUsuarioTraduzido = this.criandoArquivo(arquivo);
                    })
                    this.modeloClientes(new JSONModel(dados));
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
            oRouter.navTo("detalhesDoCliente",{
                id:cliente.id
            });
        },

        aoAdicionarCliente : function(){
            let oRouter = this.getOwnerComponent().getRouter();
            oRouter.navTo("cadastrarCliente");
        },

        dadosParaArquivo(bse64, filename) {
            let bstr = atob(bse64)
            let n = bstr.length
            let u8arr = new Uint8Array(n)
            while (n--) {
                u8arr[n] = bstr.charCodeAt(n);
            }
            return new File([u8arr], filename, { type: "image/jpeg" });
        },

        criandoArquivo(file){
            return URL.createObjectURL(file);
        }
    });
});
