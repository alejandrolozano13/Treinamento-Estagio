sap.ui.define([
    "sap/ui/core/mvc/Controller",
    "sap/m/MessageToast",
    "sap/ui/model/json/JSONModel",
    "sap/ui/model/resource/ResourceModel"
], function(Controller, MessageToast, JSONModel, ResourceModel) {
    'use strict';
    return Controller.extend("sap.ui.demo.cadastro.controller.App", {
        onInit : function(){
            var oData = {
                recipient : {
                    name : ""
                }
            };
            var oModel = new JSONModel(oData);
            this.getView().setModel(oModel);

            var i18nModel = new ResourceModel({
                bundleName: "sap.ui.demo.cadastro.i18n.i18n"
            });
            this.getView().setModel(i18nModel, "i18n");
        },

        AoEntrar : function(){
            MessageToast.show("Bem vindo ao ManitoMark");
        },
    });
});