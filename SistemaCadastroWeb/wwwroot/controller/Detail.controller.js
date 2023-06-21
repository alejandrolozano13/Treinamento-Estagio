sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel",
    "sap/ui/core/routing/History"
], function (Controller, JSONModel, History) {
    'use strict';

    return Controller.extend("sap.ui.demo.cadastro.controller.Detail", {
        onInit: function () {
            var oRouter = this.getOwnerComponent().getRouter();
            oRouter.getRoute("detail").attachPatternMatched(this._onObjectMatched, this);
        },
        _onObjectMatched: function (oEvent) {
            console.log(oEvent);
            debugger



            let id = oEvent.getParameter("arguments").id
            this._obterPorId(id);
        },
        aoEditarCliente: function(){
            let cliente = this.getView().getModel("clientes").getData();
            var oRouter = this.getOwnerComponent().getRouter();
            oRouter.navTo("editarCliente", {
                id: cliente.id
            });
        },
        _obterPorId: function (id) {

            let tela = this.getView();

            fetch(`api/Cliente/${id}`)
                .then(function (response) {
                    return response.json();
                })
                .then((data) => {
                    let arquivo = this.dataURLtoFile(data.imagemUsuario, "imagem.jpeg");
                    data.imagemUsuarioTraduzido = this.dataCreateObject(arquivo)
                    tela.setModel(new JSONModel(data), "cliente")
                })
                .catch(function (error) {
                    console.error(error);
                });
        },
        aoRetroceder: function () {
            var oRouter = this.getOwnerComponent().getRouter();
            oRouter.navTo("listaclientes", {}, true);
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