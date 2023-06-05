sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/ui/model/json/JSONModel"
], function(Controller, JSONModel) {
    'use strict';
    
    return Controller.extend("sap.ui.demo.cadastro.Detalhe", {
        buscaDetalhe : function(oEvent){
            var oRouter = this.getOwnerComponent().getRouter();
            oRouter.navTo("detail");
        }
    })
});