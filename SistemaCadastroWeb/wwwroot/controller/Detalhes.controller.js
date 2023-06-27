sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel",
    "sap/ui/core/routing/History",
    "sap/m/MessageBox",
    "sap/m/MessageToast"
], function (Controller, JSONModel, History, MessageBox, MessageToast) {
    'use strict';

    return Controller.extend("sap.ui.demo.cadastro.controller.Detalhes", {
        onInit: function () {
            var oRouter = this.getOwnerComponent().getRouter();
            oRouter.getRoute("detalhesDoCliente").attachPatternMatched(this._aoCoincidirRota, this);
        },

        _aoCoincidirRota: function (oEvent) {
            let id = oEvent.getParameter("arguments").id
            this._obterPorId(id);
        },

        _modeloClientes: function(modelo){
            const nomeModelo = "cliente";
            if (modelo){
                return this.getView().setModel(modelo, nomeModelo);   
            } else{
                return this.getView().getModel(nomeModelo);
            }
        },

        aoEditarCliente: function(){
            let cliente = this._modeloClientes().getData();
            var oRouter = this.getOwnerComponent().getRouter();
            oRouter.navTo("editarCliente", {
                id: cliente.id
            });
        },
        
        aoRemoverCliente: function(){
            let cliente = this._modeloClientes().getData();
            MessageBox.warning("Deseja mesmo remover esse cliente?", {
                EmphasizedAction: MessageBox.Action.OK,
                actions: ["YES", MessageBox.Action.CANCEL], onClose: (acao) => {
                    if (acao == "YES") {
                        fetch(`api/Cliente/${cliente.id}`,{
                            method: 'DELETE'
                        })
                        .then(resp => {
                            if(resp.status != 200){
                                return resp.text();
                            } else{
                                MessageToast.show("Cliente removido com sucesso.")
                                this.aoRetroceder();
                            }
                        })
                        .catch(function(error){
                            console.error(error);
                        });
                    }
                }
            })
        },

        _obterPorId: function (id) {
            fetch(`api/Cliente/${id}`)
                .then(function (response) {
                    return response.json();
                })
                .then((data) => {
                    let arquivo = this._converteCaminhoParaArquivo(data.imagemUsuario, "imagem.jpeg");
                    data.imagemUsuario = this._criandoArquivo(arquivo)
                    
                    this._modeloClientes(new JSONModel(data))
                })
                .catch(function (error) {
                    console.error(error);
                });
        },
        
        aoRetroceder: function () {
            var oRouter = this.getOwnerComponent().getRouter();
            oRouter.navTo("listaDeclientes", {}, true);
        },

        _converteCaminhoParaArquivo(bse64, filename) {
            let bstr = atob(bse64)
            let n = bstr.length
            let u8arr = new Uint8Array(n)
            while (n--) {
                u8arr[n] = bstr.charCodeAt(n);
            }
            return new File([u8arr], filename, { type: "image/jpeg" });
        },

        _criandoArquivo(file){
            return URL.createObjectURL(file);
        }
    });
});