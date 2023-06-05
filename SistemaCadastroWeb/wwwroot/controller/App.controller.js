sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel",
    "sap/ui/model/Filter",
    "sap/ui/model/FilterOperator"
], function (Controller, JSONModel, FilterOperator, Filter) {
    "use strict";
    return Controller.extend("sap.ui.demo.cadastro.controller.App", {
        onInit: function () {
            let tela = this.getView();
            fetch("api/Cliente")
                .then(function (response) {
                    return response.json();
                })
                .then(function (data) {
                    tela.setModel(new JSONModel(data), "clientes")
                })
                .catch(function (error) {
                    console.error(error);
                });
        },
        aoPesquisarClientes : function(oEvent){
            var aFiltro = [];
            var sQuery = oEvent.getParameter("query");
            let tela = this.getView();
            fetch(`api/Cliente?nome=${sQuery}`)
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