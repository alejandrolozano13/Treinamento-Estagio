sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel"
], function (Controller, JSONModel) {
    "use strict";
    return Controller.extend("sap.ui.demo.cadastro.controller.App", {
        onInit: function () {
            let tela = this.getView();
            fetch("https://localhost:7035/api/Cliente")
                .then(function (response) {
                    return response.json();
                })
                .then(function (data) {
                    tela.setModel(new JSONModel(data), "clientes")
                })
                .catch(function (error) {
                    console.error(error);
                });
        }
    });
});